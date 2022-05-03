using my_books.Data.Models;
using my_books.Data.ViewModels;
using System.Linq;

namespace my_books.Data.Services
{
    public class AuthorsService
    {

        private AppDbContext _context;
        public AuthorsService(AppDbContext context)
        {
            _context = context;
        }

        public void AddAuthor(AuthorVM book)
        {
            var _author = new Author()
            {
                FullName = book.FullName
            };
            _context.Authors.Add(_author);
            _context.SaveChanges();
        }

        public AuthorWithBooksVM GetAuthorWithBooks(int authorId)
        {
            var _authorWithBooks = _context.Authors.Where(a=>a.Id==authorId)
                .Select(a=>new AuthorWithBooksVM()
            {
                    FullName=a.FullName,
                    BookTitles=a.Book_Authors.Select(t=>t.Book.Title).ToList()
            }).FirstOrDefault();

            return _authorWithBooks;
        }
    }
}
