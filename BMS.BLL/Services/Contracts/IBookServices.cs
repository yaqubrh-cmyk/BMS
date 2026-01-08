using BMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.BLL.Services.Contracts
{
    public interface IBookServices
    {
        List<Book> GetAllBooks();
        Book? GetBookById(int id);
        void AddBook(Book book);
        void UpdateBook(int id, Book book);
        void DeleteBook(int id);
        List<Book> SearchBooks(string keyword);

    }
}
