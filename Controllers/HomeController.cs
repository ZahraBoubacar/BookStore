using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;

        public HomeController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                FeaturedBooks = _bookService.GetFeaturedBooks(),
                BestsellerBooks = _bookService.GetBestsellerBooks(),
                NewArrivals = _bookService.GetNewArrivals(),
                Categories = _bookService.GetAllCategories()
            };
            return View(vm);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
