namespace BookStore.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Bio { get; set; } = string.Empty;
        public string PhotoUrl { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public int BirthYear { get; set; }
        public List<string> Awards { get; set; } = new();
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }

    public class Review
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public DateTime Date { get; set; }
        public string Avatar { get; set; } = string.Empty;
    }

    public class CartItem
    {
        public int BookId { get; set; }
        public Book Book { get; set; } = new();
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => UnitPrice * Quantity;
    }

    public class Cart
    {
        public List<CartItem> Items { get; set; } = new();
        public decimal Subtotal => Items.Sum(i => i.Total);
        public decimal Shipping => Subtotal > 50 ? 0 : 7.000m;
        public decimal Total => Subtotal + Shipping;
        public int TotalItems => Items.Sum(i => i.Quantity);
    }
}
