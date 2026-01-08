using BMS.DAL.Models;
using BMS.DAL.Reporsitories.Contracts;
using BMS.BLL.Services.Contracts;

namespace BMS.BLL.Services
{
    public class BookService : IBookServices
    {
        private readonly IRepository<Book> _bookRepository;

        public BookService(IRepository<Book> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public List<Book> GetAllBooks() => _bookRepository.GetAll();

        public Book? GetBookById(int id) => _bookRepository.GetById(id);

        public void AddBook(Book book) => _bookRepository.Add(book);

        public void UpdateBook(int id, Book book)
        {
            var existingBook = _bookRepository.GetById(id);
            if (existingBook == null)
                throw new Exception("Book not found");

            book.Id = id;
            _bookRepository.Update(book);
        }

        public void DeleteBook(int id) => _bookRepository.Delete(id);

        public List<Book> SearchBooks(string keyword) => _bookRepository.Search(keyword);
    }
}
