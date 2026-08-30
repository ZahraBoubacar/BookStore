using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index(string? q, int? categoryId, int? authorId, string sortBy = "featured", int page = 1)
        {
            var books = _bookService.GetAllBooks();

            if (!string.IsNullOrWhiteSpace(q))
                books = _bookService.SearchBooks(q);
            else if (categoryId.HasValue)
                books = _bookService.GetBooksByCategory(categoryId.Value);
            else if (authorId.HasValue)
                books = _bookService.GetBooksByAuthor(authorId.Value);

            books = sortBy switch
            {
                "price_asc"  => books.OrderBy(b => b.Price).ToList(),
                "price_desc" => books.OrderByDescending(b => b.Price).ToList(),
                "rating"     => books.OrderByDescending(b => b.Rating).ToList(),
                "newest"     => books.OrderByDescending(b => b.PublishedDate).ToList(),
                _            => books.OrderByDescending(b => b.IsFeatured).ThenByDescending(b => b.ReviewCount).ToList()
            };

            string categoryName = "Tous les livres";
            if (categoryId.HasValue)
            {
                var cat = _bookService.GetAllCategories().FirstOrDefault(c => c.Id == categoryId.Value);
                categoryName = cat?.Name ?? "Livres";
            }

            const int pageSize = 8;
            var paginated = PaginatedList<Book>.Create(books, page, pageSize);

            var vm = new BookListViewModel
            {
                Books            = paginated.Items,
                PaginatedBooks   = paginated,
                Categories       = _bookService.GetAllCategories(),
                Authors          = _bookService.GetAllAuthors(),
                SearchQuery      = q,
                CategoryId       = categoryId,
                AuthorId         = authorId,
                SortBy           = sortBy,
                TotalCount       = books.Count,
                CategoryName     = categoryName,
                PageIndex        = page
            };
            return View(vm);
        }

        public IActionResult Details(string slug)
        {
            var book = _bookService.GetBookBySlug(slug);
            if (book == null) return NotFound();

            var vm = new BookDetailViewModel
            {
                Book         = book,
                RelatedBooks = _bookService.GetRelatedBooks(book.Id)
            };
            return View(vm);
        }
    }
}
