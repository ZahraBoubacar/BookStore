namespace BookStore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string FullName => $"{FirstName} {LastName}";
        public string Initials => $"{FirstName[0]}{LastName[0]}".ToUpper();
    }

    public class Order
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Confirmed;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string ShippingAddress { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = "Carte bancaire";
        public DateTime? EstimatedDelivery => OrderDate.AddDays(Status == OrderStatus.Delivered ? 3 : 5);

        public int TotalItems => Items.Sum(i => i.Quantity);
    }

    public class OrderItem
    {
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string AuthorName { get; set; } = string.Empty;
        public string CoverImageUrl { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => UnitPrice * Quantity;
    }

    public enum OrderStatus
    {
        Confirmed,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }

    public class PaginatedList<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>
            {
                Items = items,
                TotalCount = count,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }
    }
}
