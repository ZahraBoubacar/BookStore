using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IBookService _bookService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IBookService bookService, IOrderService orderService)
        {
            _cartService = cartService;
            _bookService = bookService;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            var vm = new CartViewModel
            {
                Cart = _cartService.GetCart(),
                Recommendations = _bookService.GetBestsellerBooks().Take(3).ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Add(int bookId, int quantity = 1)
        {
            _cartService.AddToCart(bookId, quantity);
            TempData["Success"] = "Livre ajouté au panier !";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddFromDetails(int bookId, int quantity = 1, string? returnUrl = null)
        {
            _cartService.AddToCart(bookId, quantity);
            TempData["Success"] = "Livre ajouté au panier !";
            if (!string.IsNullOrEmpty(returnUrl)) return Redirect(returnUrl);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Remove(int bookId)
        {
            _cartService.RemoveFromCart(bookId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int bookId, int quantity)
        {
            _cartService.UpdateQuantity(bookId, quantity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Clear()
        {
            _cartService.ClearCart();
            return RedirectToAction("Index");
        }

        public IActionResult Count()
        {
            return Json(new { count = _cartService.GetCartCount() });
        }

        public IActionResult Checkout()
        {
            var cart = _cartService.GetCart();
            if (!cart.Items.Any()) return RedirectToAction("Index");
            return View(cart);
        }

        [HttpPost]
        public IActionResult PlaceOrder(string address = "12 rue des Livres, 75001 Paris")
        {
            var cart = _cartService.GetCart();
            var email = User.FindFirstValue(ClaimTypes.Email) ?? "invite@lumiere.fr";
            var order = _orderService.CreateOrder(cart, email, address);
            _cartService.ClearCart();
            TempData["OrderId"] = order.Id;
            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }
    }
}
