namespace BookStore.Models.ViewModels
{
    public class LibraryViewModel
    {
        public List<UserBook> AllBooks { get; set; } = new();
        public List<UserBook> ToReadBooks { get; set; } = new();
        public List<UserBook> ReadingBooks { get; set; } = new();
        public List<UserBook> FinishedBooks { get; set; } = new();
        public LibraryStats Stats { get; set; } = new();
        public ReadingChallenge? Challenge { get; set; }
        public string ActiveTab { get; set; } = "all";
    }

    public class ChallengeViewModel
    {
        public ReadingChallenge? Challenge { get; set; }
        public List<UserBook> FinishedBooks { get; set; } = new();
        public bool HasChallenge { get; set; }
    }

    public class QuizViewModel
    {
        public List<Book> RecommendedBooks { get; set; } = new();
        public string Genre { get; set; } = string.Empty;
        public string Mood { get; set; } = string.Empty;
    }
}
