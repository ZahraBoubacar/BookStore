using BookStore.Models;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace BookStore.Services
{
    public interface IAuthService
    {
        User? Register(string firstName, string lastName, string email, string password);
        User? Login(string email, string password);
        User? GetUserByEmail(string email);
        bool EmailExists(string email);
    }

    public interface IOrderService
    {
        Order CreateOrder(Cart cart, string userId, string address);
        List<Order> GetOrdersByUser(string userId);
        Order? GetOrderById(string orderId, string userId);
    }

    // ─── Auth Service ───────────────────────────────────────────────
    public class MockAuthService : IAuthService
    {
        // In-memory store (resets on restart — no DB needed)
        private static readonly List<User> _users = new()
        {
            new User
            {
                Id = 1,
                FirstName = "Marie",
                LastName = "Dupont",
                Email = "marie@exemple.fr",
                PasswordHash = HashPassword("password123"),
                CreatedAt = DateTime.Now.AddMonths(-6)
            }
        };
        private static int _nextId = 2;

        public User? Register(string firstName, string lastName, string email, string password)
        {
            if (EmailExists(email)) return null;

            var user = new User
            {
                Id = _nextId++,
                FirstName = firstName,
                LastName = lastName,
                Email = email.ToLower().Trim(),
                PasswordHash = HashPassword(password),
                CreatedAt = DateTime.Now
            };
            _users.Add(user);
            return user;
        }

        public User? Login(string email, string password)
        {
            var user = _users.FirstOrDefault(u =>
                u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
            if (user == null) return null;
            return user.PasswordHash == HashPassword(password) ? user : null;
        }

        public User? GetUserByEmail(string email) =>
            _users.FirstOrDefault(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));

        public bool EmailExists(string email) =>
            _users.Any(u => u.Email.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password + "lumiere_salt"));
            return Convert.ToBase64String(bytes);
        }
    }

    // ─── Order Service ───────────────────────────────────────────────
    public class OrderService : IOrderService
    {
        private static readonly List<Order> _orders = new()
        {
            // Seed demo orders for the demo user
            new Order
            {
                Id = "LM-240115-4821",
                UserId = "marie@exemple.fr",
                Status = OrderStatus.Delivered,
                OrderDate = DateTime.Now.AddDays(-45),
                ShippingAddress = "12 rue des Livres, 75001 Paris",
                Subtotal = 62.800m,
                Shipping = 0m,
                Total = 62.800m,
                Items = new()
                {
                    new OrderItem { BookId = 1, BookTitle = "Cent ans de solitude", AuthorName = "Gabriel García Márquez", Slug = "cent-ans-de-solitude", CoverImageUrl = "https://covers.openlibrary.org/b/id/9256486-L.jpg", Quantity = 1, UnitPrice = 39.900m },
                    new OrderItem { BookId = 4, BookTitle = "L'Étranger", AuthorName = "Albert Camus", Slug = "l-etranger", CoverImageUrl = "https://covers.openlibrary.org/b/id/9781098-L.jpg", Quantity = 1, UnitPrice = 22.900m }
                }
            },
            new Order
            {
                Id = "LM-240302-7743",
                UserId = "marie@exemple.fr",
                Status = OrderStatus.Shipped,
                OrderDate = DateTime.Now.AddDays(-12),
                ShippingAddress = "12 rue des Livres, 75001 Paris",
                Subtotal = 100.700m,
                Shipping = 0m,
                Total = 100.700m,
                Items = new()
                {
                    new OrderItem { BookId = 6, BookTitle = "Sapiens", AuthorName = "Yuval Noah Harari", Slug = "sapiens", CoverImageUrl = "https://covers.openlibrary.org/b/id/8915418-L.jpg", Quantity = 1, UnitPrice = 42.900m },
                    new OrderItem { BookId = 2, BookTitle = "Norwegian Wood", AuthorName = "Haruki Murakami", Slug = "norwegian-wood", CoverImageUrl = "https://covers.openlibrary.org/b/id/8228100-L.jpg", Quantity = 1, UnitPrice = 29.900m },
                    new OrderItem { BookId = 8, BookTitle = "L'Alchimiste", AuthorName = "Paulo Coelho", Slug = "l-alchimiste", CoverImageUrl = "https://covers.openlibrary.org/b/id/8745985-L.jpg", Quantity = 1, UnitPrice = 27.900m }
                }
            },
            new Order
            {
                Id = "LM-240320-1122",
                UserId = "marie@exemple.fr",
                Status = OrderStatus.Processing,
                OrderDate = DateTime.Now.AddDays(-3),
                ShippingAddress = "12 rue des Livres, 75001 Paris",
                Subtotal = 34.900m,
                Shipping = 7.000m,
                Total = 41.900m,
                Items = new()
                {
                    new OrderItem { BookId = 5, BookTitle = "Americanah", AuthorName = "Chimamanda Ngozi Adichie", Slug = "americanah", CoverImageUrl = "https://covers.openlibrary.org/b/id/7989624-L.jpg", Quantity = 1, UnitPrice = 34.900m }
                }
            }
        };

        public Order CreateOrder(Cart cart, string userId, string address)
        {
            var order = new Order
            {
                Id = $"LM-{DateTime.Now:yyMMdd}-{new Random().Next(1000, 9999)}",
                UserId = userId,
                Status = OrderStatus.Confirmed,
                OrderDate = DateTime.Now,
                ShippingAddress = address,
                Subtotal = cart.Subtotal,
                Shipping = cart.Shipping,
                Total = cart.Total,
                Items = cart.Items.Select(i => new OrderItem
                {
                    BookId = i.BookId,
                    BookTitle = i.Book.Title,
                    AuthorName = i.Book.Author.Name,
                    CoverImageUrl = i.Book.CoverImageUrl,
                    Slug = i.Book.Slug,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
            _orders.Add(order);
            return order;
        }

        public List<Order> GetOrdersByUser(string userId) =>
            _orders.Where(o => o.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase))
                   .OrderByDescending(o => o.OrderDate)
                   .ToList();

        public Order? GetOrderById(string orderId, string userId) =>
            _orders.FirstOrDefault(o =>
                o.Id == orderId &&
                o.UserId.Equals(userId, StringComparison.OrdinalIgnoreCase));
    }
}
