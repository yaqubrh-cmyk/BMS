using BMS.DAL.Models;
namespace BMS.DAL.Reporsitories.Contracts
{
    public interface ICategoryRepository
    {
        List<Category> GetAll();
        Category GetById(int id);
        void Add(Category entity);
        void Update(Category entity);
        void Delete(int id);
        List<Category> Search(string keyword);
    }
}
