using my_books.Data.Models;
using my_books.Data.ViewModels;
using my_books.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace my_books.Data.Services
{
    public class PublishersService
    {

        private AppDbContext _context;
        public PublishersService(AppDbContext context)
        {
            _context = context;
        }

        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartsWithNumber(publisher.Name)) 
                throw new PublisherNameException("Name starts with number",publisher.Name);

            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };
            _context.Publishers.Add(_publisher);
            _context.SaveChanges(); 

            return _publisher;
        }

        public Publisher GetPublisherById(int id)=>_context.Publishers.FirstOrDefault(x => x.Id == id);
       
        public PublisherWithBooksAndAuthorsVM GetPublisherWithBooksAndAuthors(int publisherId)
        {
            var _publsherWithBooksAndAuthors = _context.Publishers.Where(p => p.Id == publisherId)
                .Select(p => new PublisherWithBooksAndAuthorsVM()
                {
                    Name = p.Name,
                    BookAuthors = p.Books.Select(b=>new BookAuthorVM()
                    {
                       BookName=b.Title,
                       BookAuthorsNames=b.Book_Authors.Select(b=>b.Author.FullName).ToList()
                    }).ToList()
                }).FirstOrDefault();

            return _publsherWithBooksAndAuthors;
        }

        public void DeletePublisherById(int id)
        {
           var _publisher=_context.Publishers.FirstOrDefault(p => p.Id == id);
            if(_publisher!=null)
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id: {id} does not exist");
            }
        }

        private bool StringStartsWithNumber(string name)=>(Regex.IsMatch(name, @"^\d"));
        
            
        
    }
}
