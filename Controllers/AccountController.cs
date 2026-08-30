using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthService _auth;
        private readonly IOrderService _orders;
        private readonly IBookService _books;

        public AccountController(IAuthService auth, IOrderService orders, IBookService books)
        {
            _auth = auth; _orders = orders; _books = books;
        }

        // ── GET /compte/connexion ──────────────────────────────────────
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Dashboard");
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            var user = _auth.Login(vm.Email, vm.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Email ou mot de passe incorrect.");
                return View(vm);
            }
            await SignInUser(user, vm.RememberMe);
            TempData["Success"] = $"Bienvenue, {user.FirstName} ! 👋";
            if (!string.IsNullOrEmpty(vm.ReturnUrl) && Url.IsLocalUrl(vm.ReturnUrl))
                return Redirect(vm.ReturnUrl);
            return RedirectToAction("Dashboard");
        }

        // ── GET /compte/inscription ───────────────────────────────────
        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Dashboard");
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!ModelState.IsValid) return View(vm);
            if (_auth.EmailExists(vm.Email))
            {
                ModelState.AddModelError("Email", "Cet email est déjà utilisé.");
                return View(vm);
            }
            var user = _auth.Register(vm.FirstName, vm.LastName, vm.Email, vm.Password);
            if (user == null)
            {
                ModelState.AddModelError("", "Une erreur est survenue. Réessayez.");
                return View(vm);
            }
            await SignInUser(user, false);
            TempData["Success"] = $"Bienvenue chez Kitab, {user.FirstName} ! 🎉";
            return RedirectToAction("Dashboard");
        }

        // ── POST /compte/deconnexion ──────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Success"] = "Vous êtes déconnecté(e). À bientôt !";
            return RedirectToAction("Index", "Home");
        }

        // ── Dashboard ─────────────────────────────────────────────────
        [Authorize]
        public IActionResult Dashboard()
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var user  = _auth.GetUserByEmail(email);
            if (user == null) return RedirectToAction("Login");
            var orders = _orders.GetOrdersByUser(email);
            ViewBag.Categories = _books.GetAllCategories();
            return View(new AccountViewModel
            {
                User         = user,
                RecentOrders = orders.Take(3).ToList(),
                TotalOrders  = orders.Count,
                TotalSpent   = orders.Sum(o => o.Total)
            });
        }

        // ── Orders ────────────────────────────────────────────────────
        [Authorize]
        public IActionResult Orders(int page = 1)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var user  = _auth.GetUserByEmail(email);
            if (user == null) return RedirectToAction("Login");
            var all = _orders.GetOrdersByUser(email);
            ViewBag.Categories = _books.GetAllCategories();
            return View(new OrderHistoryViewModel
            {
                User             = user,
                PaginatedOrders  = PaginatedList<Order>.Create(all, page, 5),
                Orders           = all
            });
        }

        [Authorize]
        public IActionResult OrderDetail(string id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email)!;
            var order = _orders.GetOrderById(id, email);
            if (order == null) return NotFound();
            ViewBag.Categories = _books.GetAllCategories();
            return View(order);
        }

        // ── Helper ────────────────────────────────────────────────────
        private async Task SignInUser(User user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Email,     user.Email),
                new(ClaimTypes.Name,      user.FullName),
                new(ClaimTypes.GivenName, user.FirstName),
            };
            var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var props     = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc   = rememberMe ? DateTimeOffset.UtcNow.AddDays(30) : null
            };
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);
        }
    }
}
