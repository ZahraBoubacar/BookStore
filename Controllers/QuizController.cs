using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class QuizController : Controller
    {
        private readonly IBookService _books;

        public QuizController(IBookService books)
        {
            _books = books;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _books.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Result(string genre, string mood, string length, string theme)
        {
            ViewBag.Categories = _books.GetAllCategories();
            var allBooks = _books.GetAllBooks();

            // Smart filtering based on answers
            var filtered = allBooks.AsEnumerable();

            // Filter by genre/category name
            if (!string.IsNullOrEmpty(genre) && genre != "all")
            {
                filtered = filtered.Where(b =>
                    b.Category.Name.ToLower().Contains(genre.ToLower()) ||
                    b.Tags.Any(t => t.ToLower().Contains(genre.ToLower())));
            }

            // Filter by mood
            filtered = mood switch
            {
                "feel" => filtered.Where(b => b.Tags.Any(t =>
                    t.Contains("Amour") || t.Contains("Romance") || t.Contains("Mélancolie") || t.Contains("Émotion"))),
                "think" => filtered.Where(b => b.Tags.Any(t =>
                    t.Contains("Philosophie") || t.Contains("Histoire") || t.Contains("Science") || t.Contains("Essai"))),
                "escape" => filtered.Where(b => b.Tags.Any(t =>
                    t.Contains("Fantastique") || t.Contains("Mystère") || t.Contains("Aventure") || t.Contains("Quête"))),
                "grow" => filtered.Where(b =>
                    b.Category.Name.Contains("Développement") || b.Category.Name.Contains("Essai") ||
                    b.Tags.Any(t => t.Contains("Spiritualité") || t.Contains("Féminisme"))),
                _ => filtered
            };

            // Filter by length
            filtered = length switch
            {
                "short" => filtered.Where(b => b.Pages < 200),
                "medium" => filtered.Where(b => b.Pages >= 200 && b.Pages <= 400),
                "long" => filtered.Where(b => b.Pages > 400),
                _ => filtered
            };

            var result = filtered
                .OrderByDescending(b => b.Rating)
                .Take(3)
                .ToList();

            // Fallback if too filtered
            if (result.Count == 0)
                result = allBooks.OrderByDescending(b => b.Rating).Take(3).ToList();

            var vm = new QuizViewModel
            {
                RecommendedBooks = result,
                Genre = genre,
                Mood = mood
            };

            return View(vm);
        }
    }
}
