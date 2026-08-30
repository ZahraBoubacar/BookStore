using BookStore.Models;

namespace BookStore.Services
{
    public interface ILibraryService
    {
        List<UserBook> GetUserLibrary(string userId);
        UserBook? GetUserBook(string userId, int bookId);
        void AddToLibrary(string userId, int bookId, ReadingStatus status = ReadingStatus.ToRead);
        void UpdateStatus(string userId, int bookId, ReadingStatus status);
        void UpdateNote(string userId, int bookId, string note, int rating);
        void RemoveFromLibrary(string userId, int bookId);
        bool IsInLibrary(string userId, int bookId);
        LibraryStats GetStats(string userId);

        // Challenge
        ReadingChallenge? GetChallenge(string userId, int year);
        void SetChallenge(string userId, int goal, int year);
        void DeleteChallenge(string userId, int year);
    }

    public class LibraryService : ILibraryService
    {
        private readonly IBookService _bookService;

        // In-memory storage per user
        private static readonly List<UserBook> _userBooks = new()
        {
            // Seed demo data for marie@exemple.fr
            new UserBook
            {
                Id = 1, UserId = "marie@exemple.fr", BookId = 1,
                Status = ReadingStatus.Finished,
                PersonalNote = "Un chef-d'œuvre absolu ! Je relirai ce livre chaque année.",
                PersonalRating = 5,
                AddedAt = DateTime.Now.AddMonths(-6),
                StartedAt = DateTime.Now.AddMonths(-6),
                FinishedAt = DateTime.Now.AddMonths(-5)
            },
            new UserBook
            {
                Id = 2, UserId = "marie@exemple.fr", BookId = 2,
                Status = ReadingStatus.Finished,
                PersonalNote = "Mélancolique et magnifique. Murakami est un génie.",
                PersonalRating = 5,
                AddedAt = DateTime.Now.AddMonths(-4),
                StartedAt = DateTime.Now.AddMonths(-4),
                FinishedAt = DateTime.Now.AddMonths(-3)
            },
            new UserBook
            {
                Id = 3, UserId = "marie@exemple.fr", BookId = 6,
                Status = ReadingStatus.Reading,
                PersonalNote = "Fascinant ! Je n'arrive pas à m'arrêter.",
                PersonalRating = 0,
                AddedAt = DateTime.Now.AddDays(-15),
                StartedAt = DateTime.Now.AddDays(-10)
            },
            new UserBook
            {
                Id = 4, UserId = "marie@exemple.fr", BookId = 4,
                Status = ReadingStatus.ToRead,
                PersonalNote = "",
                PersonalRating = 0,
                AddedAt = DateTime.Now.AddDays(-5)
            },
            new UserBook
            {
                Id = 5, UserId = "marie@exemple.fr", BookId = 8,
                Status = ReadingStatus.Finished,
                PersonalNote = "Inspirant et philosophique. À relire une fois par an.",
                PersonalRating = 4,
                AddedAt = DateTime.Now.AddMonths(-2),
                StartedAt = DateTime.Now.AddMonths(-2),
                FinishedAt = DateTime.Now.AddMonths(-1)
            }
        };

        private static readonly List<ReadingChallenge> _challenges = new()
        {
            new ReadingChallenge
            {
                UserId = "marie@exemple.fr",
                Year = DateTime.Now.Year,
                Goal = 12,
                BooksRead = 3,
                CreatedAt = new DateTime(DateTime.Now.Year, 1, 1)
            }
        };

        private static int _nextId = 10;

        public LibraryService(IBookService bookService)
        {
            _bookService = bookService;
        }

        private void HydrateBooks(List<UserBook> userBooks)
        {
            foreach (var ub in userBooks)
            {
                ub.Book = _bookService.GetBookById(ub.BookId) ?? new Book { Title = "Livre inconnu" };
            }
        }

        public List<UserBook> GetUserLibrary(string userId)
        {
            var books = _userBooks
                .Where(ub => ub.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(ub => ub.AddedAt)
                .ToList();
            HydrateBooks(books);
            return books;
        }

        public UserBook? GetUserBook(string userId, int bookId)
        {
            var ub = _userBooks.FirstOrDefault(u =>
                u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase) &&
                u.BookId == bookId);
            if (ub != null) ub.Book = _bookService.GetBookById(bookId) ?? new Book();
            return ub;
        }

        public void AddToLibrary(string userId, int bookId, ReadingStatus status = ReadingStatus.ToRead)
        {
            if (IsInLibrary(userId, bookId)) return;
            _userBooks.Add(new UserBook
            {
                Id = _nextId++,
                UserId = userId,
                BookId = bookId,
                Status = status,
                AddedAt = DateTime.Now,
                StartedAt = status == ReadingStatus.Reading ? DateTime.Now : null
            });
        }

        public void UpdateStatus(string userId, int bookId, ReadingStatus status)
        {
            var ub = _userBooks.FirstOrDefault(u =>
                u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase) && u.BookId == bookId);
            if (ub == null) return;

            ub.Status = status;
            if (status == ReadingStatus.Reading && ub.StartedAt == null)
                ub.StartedAt = DateTime.Now;
            if (status == ReadingStatus.Finished)
            {
                ub.FinishedAt = DateTime.Now;
                // Update challenge
                var challenge = GetChallenge(userId, DateTime.Now.Year);
                if (challenge != null) challenge.BooksRead++;
            }
        }

        public void UpdateNote(string userId, int bookId, string note, int rating)
        {
            var ub = _userBooks.FirstOrDefault(u =>
                u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase) && u.BookId == bookId);
            if (ub == null) return;
            ub.PersonalNote = note;
            ub.PersonalRating = rating;
        }

        public void RemoveFromLibrary(string userId, int bookId)
        {
            _userBooks.RemoveAll(u =>
                u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase) && u.BookId == bookId);
        }

        public bool IsInLibrary(string userId, int bookId)
        {
            return _userBooks.Any(u =>
                u.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase) && u.BookId == bookId);
        }

        public LibraryStats GetStats(string userId)
        {
            var books = GetUserLibrary(userId);
            var finished = books.Where(b => b.Status == ReadingStatus.Finished).ToList();

            var favoriteGenre = books
                .GroupBy(b => b.Book.Category.Name)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "—";

            var favoriteAuthor = books
                .GroupBy(b => b.Book.Author.Name)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "—";

            var ratings = finished.Where(b => b.PersonalRating > 0).ToList();

            return new LibraryStats
            {
                TotalBooks    = books.Count,
                BooksRead     = finished.Count,
                BooksReading  = books.Count(b => b.Status == ReadingStatus.Reading),
                BooksToRead   = books.Count(b => b.Status == ReadingStatus.ToRead),
                TotalPages    = finished.Sum(b => b.Book.Pages),
                FavoriteGenre = favoriteGenre,
                FavoriteAuthor = favoriteAuthor,
                AverageRating = ratings.Any() ? Math.Round(ratings.Average(b => b.PersonalRating), 1) : 0
            };
        }

        public ReadingChallenge? GetChallenge(string userId, int year)
        {
            return _challenges.FirstOrDefault(c =>
                c.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase) && c.Year == year);
        }

        public void SetChallenge(string userId, int goal, int year)
        {
            var existing = GetChallenge(userId, year);
            if (existing != null)
            {
                existing.Goal = goal;
            }
            else
            {
                var booksRead = GetUserLibrary(userId)
                    .Count(b => b.Status == ReadingStatus.Finished &&
                                b.FinishedAt?.Year == year);
                _challenges.Add(new ReadingChallenge
                {
                    UserId = userId,
                    Year = year,
                    Goal = goal,
                    BooksRead = booksRead,
                    CreatedAt = DateTime.Now
                });
            }
        }

        public void DeleteChallenge(string userId, int year)
        {
            _challenges.RemoveAll(c =>
                c.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase) && c.Year == year);
        }
    }
}
