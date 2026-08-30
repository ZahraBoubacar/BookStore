using BookStore.Models;
using BookStore.Models.ViewModels;
using BookStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookStore.Controllers
{
    [Authorize]
    public class LibraryController : Controller
    {
        private readonly ILibraryService _library;
        private readonly IBookService _books;

        public LibraryController(ILibraryService library, IBookService books)
        {
            _library = library;
            _books = books;
        }

        private string UserId => User.FindFirstValue(ClaimTypes.Email)!;

        private void SetCategories() =>
            ViewBag.Categories = _books.GetAllCategories();

        // ── Ma Bibliothèque ───────────────────────────────────────────
        public IActionResult Index(string tab = "all")
        {
            SetCategories();
            var all = _library.GetUserLibrary(UserId);
            var vm = new LibraryViewModel
            {
                AllBooks     = all,
                ToReadBooks  = all.Where(b => b.Status == ReadingStatus.ToRead).ToList(),
                ReadingBooks = all.Where(b => b.Status == ReadingStatus.Reading).ToList(),
                FinishedBooks= all.Where(b => b.Status == ReadingStatus.Finished).ToList(),
                Stats        = _library.GetStats(UserId),
                Challenge    = _library.GetChallenge(UserId, DateTime.Now.Year),
                ActiveTab    = tab
            };
            return View(vm);
        }

        // ── Ajouter un livre ─────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(int bookId, string status = "ToRead", string? returnUrl = null)
        {
            var readingStatus = Enum.Parse<ReadingStatus>(status);
            _library.AddToLibrary(UserId, bookId, readingStatus);
            TempData["Success"] = "Livre ajouté à votre bibliothèque ! 📚";
            return returnUrl != null ? Redirect(returnUrl) : RedirectToAction("Index");
        }

        // ── Changer le statut ─────────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int bookId, string status)
        {
            var readingStatus = Enum.Parse<ReadingStatus>(status);
            _library.UpdateStatus(UserId, bookId, readingStatus);
            TempData["Success"] = "Statut mis à jour !";
            return RedirectToAction("Index");
        }

        // ── Modifier note et avis ─────────────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateNote(int bookId, string note, int rating)
        {
            _library.UpdateNote(UserId, bookId, note, rating);
            TempData["Success"] = "Note sauvegardée !";
            return RedirectToAction("Index");
        }

        // ── Supprimer de la bibliothèque ──────────────────────────────
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Remove(int bookId)
        {
            _library.RemoveFromLibrary(UserId, bookId);
            TempData["Success"] = "Livre retiré de votre bibliothèque.";
            return RedirectToAction("Index");
        }

        // ── Défi Lecture ──────────────────────────────────────────────
        public new IActionResult Challenge()
        {
            SetCategories();
            var challenge = _library.GetChallenge(UserId, DateTime.Now.Year);
            var finished  = _library.GetUserLibrary(UserId)
                .Where(b => b.Status == ReadingStatus.Finished).ToList();

            return View(new ChallengeViewModel
            {
                Challenge    = challenge,
                FinishedBooks= finished,
                HasChallenge = challenge != null
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetChallenge(int goal)
        {
            if (goal < 1 || goal > 365) goal = 12;
            _library.SetChallenge(UserId, goal, DateTime.Now.Year);
            TempData["Success"] = $"Défi fixé : {goal} livres en {DateTime.Now.Year} ! 🏆";
            return RedirectToAction("Challenge");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteChallenge()
        {
            _library.DeleteChallenge(UserId, DateTime.Now.Year);
            TempData["Success"] = "Défi supprimé.";
            return RedirectToAction("Challenge");
        }
    }
}
