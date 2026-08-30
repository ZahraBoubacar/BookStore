namespace BookStore.Models
{
    public enum ReadingStatus
    {
        ToRead,     // À lire
        Reading,    // En cours
        Finished    // Terminé
    }

    public class UserBook
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int BookId { get; set; }
        public Book Book { get; set; } = new();
        public ReadingStatus Status { get; set; } = ReadingStatus.ToRead;
        public string PersonalNote { get; set; } = string.Empty;
        public int PersonalRating { get; set; } = 0; // 0-5
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public DateTime? StartedAt { get; set; }
        public DateTime? FinishedAt { get; set; }

        public string StatusLabel => Status switch
        {
            ReadingStatus.ToRead   => "À lire",
            ReadingStatus.Reading  => "En cours",
            ReadingStatus.Finished => "Terminé",
            _ => ""
        };
        public string StatusIcon => Status switch
        {
            ReadingStatus.ToRead   => "📖",
            ReadingStatus.Reading  => "🔄",
            ReadingStatus.Finished => "✅",
            _ => ""
        };
        public string StatusCss => Status switch
        {
            ReadingStatus.ToRead   => "status-toread",
            ReadingStatus.Reading  => "status-reading",
            ReadingStatus.Finished => "status-finished",
            _ => ""
        };
    }

    public class ReadingChallenge
    {
        public string UserId { get; set; } = string.Empty;
        public int Year { get; set; } = DateTime.Now.Year;
        public int Goal { get; set; } = 12;
        public int BooksRead { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public double ProgressPercent => Goal > 0
            ? Math.Min(100, Math.Round((double)BooksRead / Goal * 100))
            : 0;
        public int Remaining => Math.Max(0, Goal - BooksRead);
        public bool IsCompleted => BooksRead >= Goal;

        public string CurrentBadge => BooksRead switch
        {
            0      => "🌱 Débutant",
            >= 1 and < 3  => "📖 Lecteur",
            >= 3 and < 6  => "⭐ Passionné",
            >= 6 and < 10 => "🔥 Avide",
            >= 10 and < 12 => "🏆 Champion",
            >= 12  => "👑 Légendaire",
            _      => "🌱 Débutant"
        };

        public List<ChallengeBadge> Badges => new()
        {
            new() { Icon="🌱", Label="Premier pas",   Condition="1 livre lu",   Unlocked = BooksRead >= 1  },
            new() { Icon="📖", Label="Lecteur",        Condition="3 livres lus", Unlocked = BooksRead >= 3  },
            new() { Icon="⭐", Label="Passionné",      Condition="6 livres lus", Unlocked = BooksRead >= 6  },
            new() { Icon="🔥", Label="Avide de pages", Condition="10 livres lus",Unlocked = BooksRead >= 10 },
            new() { Icon="🏆", Label="Champion",       Condition="Objectif atteint", Unlocked = IsCompleted },
            new() { Icon="👑", Label="Légendaire",     Condition="20 livres lus",Unlocked = BooksRead >= 20 },
        };
    }

    public class ChallengeBadge
    {
        public string Icon { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public string Condition { get; set; } = string.Empty;
        public bool Unlocked { get; set; } = false;
    }

    public class LibraryStats
    {
        public int TotalBooks { get; set; }
        public int BooksRead { get; set; }
        public int BooksReading { get; set; }
        public int BooksToRead { get; set; }
        public int TotalPages { get; set; }
        public string FavoriteGenre { get; set; } = string.Empty;
        public string FavoriteAuthor { get; set; } = string.Empty;
        public double AverageRating { get; set; }
    }
}
