using BMS.DAL.DataContext;
using BMS.DAL.Models;
using BMS.DAL.Reporsitories.Contracts;

namespace BMS.DAL.Reporsitories
{
    public class BookRepository : IRepository<Book>
    {
        public void Add(Book entity)
        {
            BMSDataBase.Books.Add(entity);
        }

       public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                BMSDataBase.Books.Remove(book);
                return;
            }
            throw new Exception("Book not found");
        }

        public List<Book> GetAll()
        {
            return BMSDataBase.Books;
        }
        public Book GetById(int id)
        {
           var book=BMSDataBase.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                return book;
            }
            throw new Exception("Book not found");

        }
        public List<Book> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Book>();

            keyword = keyword.Trim();

            return BMSDataBase.Books
                .Where(b =>
                    (!string.IsNullOrEmpty(b.Title) &&
                     b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||

                    (!string.IsNullOrEmpty(b.Author) &&
                     b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase)) ||

                    (!string.IsNullOrEmpty(b.ISBN) &&
                     b.ISBN.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                )
                .ToList();
        }
        public void Update(Book entity)
        {
           var existingBook = GetById(entity.Id);
            if (existingBook != null)
            {
                existingBook.Title = entity.Title;
                existingBook.Author = entity.Author;
                existingBook.ISBN = entity.ISBN;
                existingBook.PublishedYear = entity.PublishedYear;
                existingBook.CategoryId = entity.CategoryId;
                existingBook.IsAvailable = entity.IsAvailable;
                return;
            }
            throw new Exception("Book not found");
        }

        
    }
}