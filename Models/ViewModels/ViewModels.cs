namespace BookStore.Models.ViewModels
{
    public class HomeViewModel
    {
        public List<Book> FeaturedBooks { get; set; } = new();
        public List<Book> BestsellerBooks { get; set; } = new();
        public List<Book> NewArrivals { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }

    public class BookListViewModel
    {
        public List<Book> Books { get; set; } = new();
        public PaginatedList<Book> PaginatedBooks { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
        public List<Author> Authors { get; set; } = new();
        public string? SearchQuery { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public string SortBy { get; set; } = "featured";
        public int TotalCount { get; set; }
        public string CategoryName { get; set; } = "Tous les livres";
        public int PageIndex { get; set; } = 1;
    }

    public class BookDetailViewModel
    {
        public Book Book { get; set; } = new();
        public List<Book> RelatedBooks { get; set; } = new();
    }

    public class CartViewModel
    {
        public Cart Cart { get; set; } = new();
        public List<Book> Recommendations { get; set; } = new();
    }

    public class SearchResultViewModel
    {
        public string Query { get; set; } = string.Empty;
        public List<Book> Results { get; set; } = new();
        public int TotalResults { get; set; }
    }
}
