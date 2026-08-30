namespace BookStore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public Author Author { get; set; } = new();
        public int AuthorId { get; set; }
        public Category Category { get; set; } = new();
        public int CategoryId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal? OriginalPrice { get; set; }
        public string CoverImageUrl { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int Pages { get; set; }
        public string Language { get; set; } = "Français";
        public string Publisher { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public double Rating { get; set; }
        public int ReviewCount { get; set; }
        public bool IsAvailable { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public bool IsNew { get; set; } = false;
        public bool IsBestseller { get; set; } = false;
        public List<string> Tags { get; set; } = new();
        public List<Review> Reviews { get; set; } = new();

        public bool IsOnSale => OriginalPrice.HasValue && OriginalPrice.Value > Price;
        public int DiscountPercent => IsOnSale
            ? (int)Math.Round((1 - Price / OriginalPrice!.Value) * 100)
            : 0;
    }
}
