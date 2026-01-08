using BMS.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.BLL.Services.Contracts
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
        List<Category> Search(string keyword);
    }
}
