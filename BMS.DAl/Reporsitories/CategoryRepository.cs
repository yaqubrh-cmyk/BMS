using BMS.DAL.DataContext;
using BMS.DAL.Models;
using BMS.DAL.Reporsitories.Contracts;
namespace BMS.DAL.Reporsitories;


    public class CategoryRepository : IRepository<Category>
{

    public void Add(Category category)
    {
        BMSDataBase.Categories.Add(category);
    }

    public void Delete(int id)
    {
        var category = GetById(id);
        if (category != null)
        {
            BMSDataBase.Categories.Remove(category);
        }
    }

    public List<Category> GetAll()
    {
        return BMSDataBase.Categories;
    }

    public Category GetById(int id)
    {
        var category = BMSDataBase.Categories.FirstOrDefault(c => c.Id == id);

        if (category == null)
            throw new Exception("Category not found");

        return category;

    }
    
    public List<Category> Search(string keyword)
    {
        return BMSDataBase.Categories
            .Where(c =>
                c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    public void Update(Category category)
    {
        var existingCategory = GetById(category.Id);
        if (existingCategory != null)
        {
            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;
        }
    }
}
