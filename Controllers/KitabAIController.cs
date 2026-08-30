using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BookStore.Controllers
{
    public class KitabAIController : Controller
    {
        private readonly IBookService _books;

        public KitabAIController(IBookService books)
        {
            _books = books;
        }

        public IActionResult Index()
        {
            ViewBag.Categories = _books.GetAllCategories();
            return View();
        }

        [HttpPost]
        public IActionResult Chat([FromBody] ChatRequest request)
        {
            var message = request.Messages.LastOrDefault()?.Content ?? "";
            var allBooks = _books.GetAllBooks();
            var reply = GenerateReply(message.ToLower(), allBooks);
            return Json(new { reply });
        }

        private string GenerateReply(string msg, List<Book> books)
        {
            // ── Salutations ────────────────────────────────────────────
            if (ContainsAny(msg, "bonjour", "salut", "salam", "hello", "bonsoir", "hi"))
                return "Bonjour ! Je suis **Kitab AI**, votre conseiller libraire personnel. 📚\n\nJe connais tout notre catalogue et je suis là pour vous aider à trouver le livre parfait. Quel type de lecture vous ferait plaisir aujourd'hui ?";

            if (ContainsAny(msg, "merci", "شكرا", "thank"))
                return "Avec plaisir ! 😊 C'est tout le bonheur de Kitab AI de vous aider à trouver votre prochaine lecture. N'hésitez pas si vous avez d'autres questions !";

            if (ContainsAny(msg, "comment tu t'appelles", "qui es-tu", "c'est quoi kitab ai", "tu es quoi"))
                return "Je suis **Kitab AI** 🤖 — l'assistant libraire intelligent de **Kitab كتاب**, la plateforme culturelle tunisienne. Je connais tous les livres de notre catalogue et je suis disponible 24h/24 pour vous conseiller. Que puis-je faire pour vous ?";

            // ── Humeur / Stress ────────────────────────────────────────
            if (ContainsAny(msg, "stress", "soutenance", "anxieu", "peur", "angoiss", "nerveu"))
            {
                var book = books.FirstOrDefault(b => b.Title.Contains("Alchimiste")) ?? books.OrderByDescending(b => b.Rating).First();
                return $"Je comprends ce stress ! 😅 Dans ces moments, un livre inspirant peut vraiment aider.\n\nJe vous recommande **{book.Title}** de *{book.Author.Name}* — {book.ShortDescription}\n\n💰 Prix : **{BookStore.Helpers.CurrencyHelper.FormatTND(book.Price)}** · ⭐ {book.Rating}/5\n\nCourt, inspirant et plein d'énergie positive. Parfait pour retrouver confiance en soi ! 💪";
            }

            if (ContainsAny(msg, "triste", "déprim", "blues", "cafard", "mélancolie", "malheureu"))
            {
                var book = books.FirstOrDefault(b => b.Title.Contains("Norwegian")) ?? books.OrderByDescending(b => b.Rating).First();
                return $"Quand on se sent triste, un beau roman peut être un compagnon précieux. 🌧️\n\nJe vous propose **{book.Title}** de *{book.Author.Name}* — {book.ShortDescription}\n\n💰 Prix : **{BookStore.Helpers.CurrencyHelper.FormatTND(book.Price)}** · ⭐ {book.Rating}/5\n\nParfois lire quelque chose de beau et mélancolique nous aide à nous sentir moins seuls. 💙";
            }

            if (ContainsAny(msg, "motivé", "inspir", "énergie", "objectif", "ambitieu"))
            {
                var book = books.FirstOrDefault(b => b.Category.Name.Contains("Développement")) ?? books.OrderByDescending(b => b.Rating).First();
                return $"Pour trouver motivation et inspiration, j'ai exactement ce qu'il faut ! 🚀\n\n**{book.Title}** de *{book.Author.Name}* — {book.ShortDescription}\n\n💰 Prix : **{BookStore.Helpers.CurrencyHelper.FormatTND(book.Price)}** · ⭐ {book.Rating}/5\n\nUn livre qui change vraiment la façon de voir les choses ! 💪";
            }

            // ── Ce soir / maintenant ───────────────────────────────────
            if (ContainsAny(msg, "ce soir", "maintenant", "tout de suite", "aujourd'hui", "rapidement", "vite"))
            {
                var book = books.Where(b => b.Pages < 300).OrderByDescending(b => b.Rating).First();
                return $"Pour une lecture rapide ce soir, je vous suggère quelque chose de court et captivant ! ⚡\n\n**{book.Title}** de *{book.Author.Name}*\n_{book.ShortDescription}_\n\n📄 {book.Pages} pages · 💰 **{BookStore.Helpers.CurrencyHelper.FormatTND(book.Price)}** · ⭐ {book.Rating}/5\n\nParfait pour une soirée lecture ! 🌙";
            }

            // ── Résumés de livres ─────────────────────────────────────
            if (ContainsAny(msg, "résume", "c'est quoi", "parle-moi de", "raconte", "about"))
            {
                foreach (var book in books)
                {
                    if (msg.Contains(book.Title.ToLower()) ||
                        msg.Contains(book.Slug.Replace("-", " ")))
                    {
                        return $"📖 **{book.Title}** de *{book.Author.Name}*\n\n{book.Description}\n\n**Infos :**\n• Genre : {book.Category.Name}\n• {book.Pages} pages · Publié en {book.PublishedDate.Year}\n• ⭐ {book.Rating}/5 ({book.ReviewCount} avis)\n• 💰 **{BookStore.Helpers.CurrencyHelper.FormatTND(book.Price)}**";
                    }
                }
            }

            // ── Recommandations par genre ──────────────────────────────
            if (ContainsAny(msg, "roman", "fiction", "histoire fictive"))
            {
                var recs = books.Where(b => b.Category.Name.Contains("Roman")).OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("romans", recs);
            }

            if (ContainsAny(msg, "classique", "littérature classique", "chef d'oeuvre"))
            {
                var recs = books.Where(b => b.Category.Name.Contains("classique")).OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("classiques", recs);
            }

            if (ContainsAny(msg, "philosophi", "essai", "réfléchir", "penser"))
            {
                var recs = books.Where(b => b.Category.Name.Contains("Essai") || b.Category.Name.Contains("Philosophie")).OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("essais et philosophie", recs);
            }

            if (ContainsAny(msg, "science", "histoire", "humanité", "cosmos", "apprendre"))
            {
                var recs = books.Where(b => b.Category.Name.Contains("Science") || b.Category.Name.Contains("Histoire")).OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("sciences et histoire", recs);
            }

            if (ContainsAny(msg, "romance", "amour", "histoire d'amour", "romantique"))
            {
                var recs = books.Where(b => b.Category.Name.Contains("Romance") || b.Tags.Contains("Amour")).OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("romance et amour", recs);
            }

            if (ContainsAny(msg, "développement", "personnel", "progresser", "s'améliorer", "grandir"))
            {
                var recs = books.Where(b => b.Category.Name.Contains("Développement")).OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("développement personnel", recs);
            }

            // ── Auteurs spécifiques ───────────────────────────────────
            foreach (var author in _books.GetAllAuthors())
            {
                if (msg.Contains(author.Name.ToLower()) ||
                    msg.Contains(author.Name.Split(' ').Last().ToLower()))
                {
                    var authorBooks = books.Where(b => b.AuthorId == author.Id).ToList();
                    var bookList = string.Join("\n", authorBooks.Select(b =>
                        $"• **{b.Title}** — {BookStore.Helpers.CurrencyHelper.FormatTND(b.Price)} · ⭐ {b.Rating}/5"));
                    return $"✍️ **{author.Name}** ({author.Nationality}, né en {author.BirthYear})\n\n_{author.Bio}_\n\n**Disponible chez Kitab :**\n{bookList}";
                }
            }

            // ── Prix / Promotions ─────────────────────────────────────
            if (ContainsAny(msg, "promo", "solde", "réduction", "pas cher", "moins cher", "prix"))
            {
                var onSale = books.Where(b => b.IsOnSale).ToList();
                if (onSale.Any())
                {
                    var list = string.Join("\n", onSale.Select(b =>
                        $"• **{b.Title}** — ~~{BookStore.Helpers.CurrencyHelper.FormatTND(b.OriginalPrice!.Value)}~~ → **{BookStore.Helpers.CurrencyHelper.FormatTND(b.Price)}** (-{b.DiscountPercent}%)"));
                    return $"🏷️ Voici nos livres en promotion en ce moment :\n\n{list}\n\nDépêchez-vous, les offres sont limitées ! ⏰";
                }
                return "Nos prix commencent à partir de **19,900 DT** et la livraison est gratuite dès **50 DT** ! Découvrez tout notre catalogue sur la page Livres. 😊";
            }

            // ── Meilleurs livres ──────────────────────────────────────
            if (ContainsAny(msg, "meilleur", "top", "bestseller", "populaire", "recommande", "conseille", "suggère"))
            {
                var top = books.OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("meilleures notes", top);
            }

            // ── Nouveautés ────────────────────────────────────────────
            if (ContainsAny(msg, "nouveau", "nouveauté", "récent", "dernier", "vient de sortir"))
            {
                var newBooks = books.Where(b => b.IsNew).Take(3).ToList();
                return FormatRecommendations("nouveautés", newBooks);
            }

            // ── Court / Long ──────────────────────────────────────────
            if (ContainsAny(msg, "court", "rapide", "petit", "peu de pages"))
            {
                var shortBooks = books.Where(b => b.Pages < 250).OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("livres courts (moins de 250 pages)", shortBooks);
            }

            if (ContainsAny(msg, "long", "épique", "gros", "beaucoup de pages"))
            {
                var longBooks = books.Where(b => b.Pages > 400).OrderByDescending(b => b.Rating).Take(3).ToList();
                return FormatRecommendations("livres longs et épiques", longBooks);
            }

            // ── Comparaison de livres ─────────────────────────────────
            if (ContainsAny(msg, "comparer", "différence entre", "lequel choisir", "ou bien"))
            {
                return "Pour vous aider à choisir, dites-moi les **titres ou genres** que vous hésitez entre. Par exemple : *\"L'Étranger ou Norwegian Wood ?\"* et je vous expliquerai les différences ! 📚";
            }

            // ── Catalogue complet ─────────────────────────────────────
            if (ContainsAny(msg, "catalogue", "liste", "tous les livres", "qu'est-ce que vous avez"))
            {
                var list = string.Join("\n", books.Select(b => $"• **{b.Title}** — *{b.Author.Name}* · {BookStore.Helpers.CurrencyHelper.FormatTND(b.Price)}"));
                return $"📚 Voici tous nos livres disponibles :\n\n{list}\n\nN'hésitez pas à me demander plus de détails sur l'un d'eux !";
            }

            // ── Livraison / Info pratique ─────────────────────────────
            if (ContainsAny(msg, "livraison", "délai", "expédition", "recevoir"))
                return "🚚 **Livraison Kitab :**\n\n• **Gratuite** dès 50 DT d'achat\n• Standard : 3-5 jours ouvrés · **7,000 DT**\n• Express 24h : **12,000 DT**\n• Retrait en magasin à Tunis : Gratuit\n\nNous livrons dans toute la Tunisie ! 🇹🇳";

            if (ContainsAny(msg, "paiement", "payer", "d17", "carte", "espèce"))
                return "💳 **Modes de paiement acceptés chez Kitab :**\n\n• Carte bancaire (Visa / Mastercard)\n• Paiement à la livraison\n• D17 (paiement mobile)\n\nTous les paiements sont 100% sécurisés SSL 🔒";

            // ── Réponse par défaut intelligente ──────────────────────
            var randomBooks = books.OrderByDescending(b => b.Rating).Take(3).ToList();
            return $"Je n'ai pas tout à fait compris votre question, mais je peux vous suggérer nos livres les mieux notés en ce moment ! 😊\n\n{FormatRecommendationsInline(randomBooks)}\n\nEssayez de me demander par exemple :\n• *\"Quel livre pour ce soir ?\"*\n• *\"Résume-moi Sapiens\"*\n• *\"Je veux un roman court\"*";
        }

        private string FormatRecommendations(string category, List<Book> books)
        {
            if (!books.Any())
                return $"Je n'ai pas trouvé de livres dans la catégorie **{category}** pour le moment. Essayez une autre recherche ! 😊";

            var lines = books.Select(b =>
                $"📖 **{b.Title}** de *{b.Author.Name}*\n   {b.ShortDescription}\n   💰 **{BookStore.Helpers.CurrencyHelper.FormatTND(b.Price)}** · ⭐ {b.Rating}/5 ({b.ReviewCount} avis)");

            return $"Voici mes recommandations en **{category}** :\n\n{string.Join("\n\n", lines)}";
        }

        private string FormatRecommendationsInline(List<Book> books)
        {
            return string.Join("\n", books.Select(b =>
                $"• **{b.Title}** de *{b.Author.Name}* — {BookStore.Helpers.CurrencyHelper.FormatTND(b.Price)} · ⭐ {b.Rating}/5"));
        }

        private bool ContainsAny(string text, params string[] keywords)
        {
            return keywords.Any(k => text.Contains(k, StringComparison.OrdinalIgnoreCase));
        }
    }

    public class ChatRequest
    {
        public List<ChatMessage> Messages { get; set; } = new();
    }

    public class ChatMessage
    {
        public string Role    { get; set; } = "user";
        public string Content { get; set; } = string.Empty;
    }
}
