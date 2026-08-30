using BookStore.Models;
using System.Text.Json;

namespace BookStore.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBookService _bookService;
        private const string CartKey = "BookStore_Cart";

        public CartService(IHttpContextAccessor httpContextAccessor, IBookService bookService)
        {
            _httpContextAccessor = httpContextAccessor;
            _bookService = bookService;
        }

        private ISession Session => _httpContextAccessor.HttpContext!.Session;

        public Cart GetCart()
        {
            var json = Session.GetString(CartKey);
            if (string.IsNullOrEmpty(json)) return new Cart();

            var items = JsonSerializer.Deserialize<List<CartItemData>>(json) ?? new();
            var cart = new Cart();
            foreach (var item in items)
            {
                var book = _bookService.GetBookById(item.BookId);
                if (book != null)
                {
                    cart.Items.Add(new CartItem
                    {
                        BookId = item.BookId,
                        Book = book,
                        Quantity = item.Quantity,
                        UnitPrice = book.Price
                    });
                }
            }
            return cart;
        }

        public void AddToCart(int bookId, int quantity = 1)
        {
            var items = GetCartData();
            var existing = items.FirstOrDefault(i => i.BookId == bookId);
            if (existing != null)
                existing.Quantity += quantity;
            else
                items.Add(new CartItemData { BookId = bookId, Quantity = quantity });
            SaveCart(items);
        }

        public void RemoveFromCart(int bookId)
        {
            var items = GetCartData();
            items.RemoveAll(i => i.BookId == bookId);
            SaveCart(items);
        }

        public void UpdateQuantity(int bookId, int quantity)
        {
            var items = GetCartData();
            var item = items.FirstOrDefault(i => i.BookId == bookId);
            if (item != null)
            {
                if (quantity <= 0) items.Remove(item);
                else item.Quantity = quantity;
            }
            SaveCart(items);
        }

        public void ClearCart()
        {
            Session.Remove(CartKey);
        }

        public int GetCartCount()
        {
            return GetCartData().Sum(i => i.Quantity);
        }

        private List<CartItemData> GetCartData()
        {
            var json = Session.GetString(CartKey);
            return string.IsNullOrEmpty(json)
                ? new List<CartItemData>()
                : JsonSerializer.Deserialize<List<CartItemData>>(json) ?? new();
        }

        private void SaveCart(List<CartItemData> items)
        {
            Session.SetString(CartKey, JsonSerializer.Serialize(items));
        }

        private class CartItemData
        {
            public int BookId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
