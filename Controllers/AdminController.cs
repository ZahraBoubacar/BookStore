using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Services;
using BookStore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    // Admin access: email must end with @admin.kitab or be the admin email
    public class AdminController : Controller
    {
        private readonly IBookService _books;
        private readonly IOrderService _orders;
        private readonly IAuthService _auth;

        public AdminController(IBookService books, IOrderService orders, IAuthService auth)
        {
            _books = books;
            _orders = orders;
            _auth = auth;
        }

        private bool IsAdmin()
        {
            if (!User.Identity?.IsAuthenticated == true) return false;
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value ?? "";
            return email == "admin@kitab.tn" || email == "marie@exemple.fr"; // demo: marie is admin
        }

        private IActionResult RequireAdmin()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Account",
                new { returnUrl = "/admin" });
            return null!;
        }

        public IActionResult Index()
        {
            var check = RequireAdmin(); if (check != null) return check;

            var allBooks  = _books.GetAllBooks();
            var allOrders = _orders.GetOrdersByUser("marie@exemple.fr")
                           .Concat(_orders.GetOrdersByUser("invite@kitab.tn")).ToList();

            var stats = new AdminStats
            {
                TotalBooks      = allBooks.Count,
                TotalAuthors    = _books.GetAllAuthors().Count,
                TotalCategories = _books.GetAllCategories().Count,
                TotalOrders     = allOrders.Count,
                TotalRevenue    = allOrders.Sum(o => o.Total),
                RecentOrders    = allOrders.OrderByDescending(o => o.OrderDate).Take(5).ToList(),
            };
            ViewBag.Categories = _books.GetAllCategories();
            ViewBag.Books      = allBooks;
            ViewBag.Stats      = stats;
            return View();
        }

        [HttpGet]
        public IActionResult CreateBook()
        {
            var check = RequireAdmin(); if (check != null) return check;
            ViewBag.Categories = _books.GetAllCategories();
            ViewBag.Authors    = _books.GetAllAuthors();
            ViewBag.Categories2 = _books.GetAllCategories();
            return View(new Book());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBook(Book book)
        {
            var check = RequireAdmin(); if (check != null) return check;
            // Assign Author and Category objects
            book.Author   = _books.GetAllAuthors().FirstOrDefault(a => a.Id == book.AuthorId) ?? new Author { Name = "Inconnu" };
            book.Category = _books.GetAllCategories().FirstOrDefault(c => c.Id == book.CategoryId) ?? new Category { Name = "Autre" };
            book.Slug     = book.Title.ToLower().Replace(" ", "-").Replace("'", "-").Replace("é","e").Replace("è","e").Replace("ê","e").Replace("à","a");
            book.Id       = _books.GetAllBooks().Max(b => b.Id) + 1;
            _books.AddBook(book);
            TempData["Success"] = $"Livre « {book.Title} » ajouté avec succès !";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult EditBook(int id)
        {
            var check = RequireAdmin(); if (check != null) return check;
            var book = _books.GetBookById(id);
            if (book == null) return NotFound();
            ViewBag.Categories  = _books.GetAllCategories();
            ViewBag.Authors     = _books.GetAllAuthors();
            ViewBag.Categories2 = _books.GetAllCategories();
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditBook(Book book)
        {
            var check = RequireAdmin(); if (check != null) return check;
            book.Author   = _books.GetAllAuthors().FirstOrDefault(a => a.Id == book.AuthorId) ?? new Author { Name = "Inconnu" };
            book.Category = _books.GetAllCategories().FirstOrDefault(c => c.Id == book.CategoryId) ?? new Category { Name = "Autre" };
            _books.UpdateBook(book);
            TempData["Success"] = $"Livre « {book.Title} » modifié !";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteBook(int id)
        {
            var check = RequireAdmin(); if (check != null) return check;
            _books.DeleteBook(id);
            TempData["Success"] = "Livre supprimé.";
            return RedirectToAction("Index");
        }
    }

    // Extend AdminStats to be accessible
    public class AdminStats
    {
        public int TotalBooks { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalCategories { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<Order> RecentOrders { get; set; } = new();
    }
}
