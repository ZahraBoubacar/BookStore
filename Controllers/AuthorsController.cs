using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IBookService _books;

        public AuthorsController(IBookService books)
        {
            _books = books;
        }

        public IActionResult Details(int id, int page = 1)
        {
            var author = _books.GetAuthorById(id);
            if (author == null) return NotFound();

            var allBooks = _books.GetBooksByAuthor(id);
            var paginated = PaginatedList<Book>.Create(allBooks, page, 6);

            var vm = new AuthorDetailViewModel
            {
                Author = author,
                Books = paginated,
                PageIndex = page
            };
            return View(vm);
        }

        public IActionResult Index()
        {
            var authors = _books.GetAllAuthors();
            return View(authors);
        }
    }
}
