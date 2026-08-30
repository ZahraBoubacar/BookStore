using BookStore.Models;

namespace BookStore.Services
{
    public interface IBookService
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        Book? GetBookBySlug(string slug);
        List<Book> GetFeaturedBooks();
        List<Book> GetBestsellerBooks();
        List<Book> GetNewArrivals();
        List<Book> GetBooksByCategory(int categoryId);
        List<Book> GetBooksByAuthor(int authorId);
        List<Book> SearchBooks(string query);
        List<Book> GetRelatedBooks(int bookId, int count = 4);
        List<Category> GetAllCategories();
        List<Author> GetAllAuthors();
        Author? GetAuthorById(int id);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
    }

    public interface ICartService
    {
        Cart GetCart();
        void AddToCart(int bookId, int quantity = 1);
        void RemoveFromCart(int bookId);
        void UpdateQuantity(int bookId, int quantity);
        void ClearCart();
        int GetCartCount();
    }
}
