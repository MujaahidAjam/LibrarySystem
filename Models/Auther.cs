namespace LibrarySystem.Models
{
    public class Author
    {
        public int Id { get; set; } // Primary Key
        public string Name { get; set; } // Author's Name

        // Navigation Property: An Author can have many Books
        public ICollection<Book> Books { get; set; }
    }
}
