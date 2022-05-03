namespace my_books.Data.Models
{
    public class Book_Author
    {
        public int Id { get; set; }

        //Navigatons Properties
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
