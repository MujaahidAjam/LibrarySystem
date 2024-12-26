namespace LibrarySystem.Models
{
    public class Book
    {
        public int Id { get; set; } // Primary Key
        public string Title { get; set; } // Book Title
        public int AuthorId { get; set; } // Foreign Key to Author
        public bool IsAvailable { get; set; } = true; // Availability Status

        // Navigation Properties
        public Author Author { get; set; }
        public ICollection<Checkout> Checkouts { get; set; }

        // Add this property
        public ICollection<Librarian> Managers { get; set; } // Librarians managing the book
    }
}
