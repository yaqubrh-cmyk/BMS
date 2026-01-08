using BMS.DAL.Models;
namespace BMS.DAL.Reporsitories.Contracts
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        Book? GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(int id,Book book);
        void DeleteBook(int id);
        List<Book> Search(string keyword);
    }
}
