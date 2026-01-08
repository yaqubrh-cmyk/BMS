using BMS.BLL.Services.Contracts;
using BMS.DAL.Models;
using BMS.DAL.Reporsitories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMS.BLL.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IRepository<Category> _categoryRepository;
        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public List<Category> GetAll() => _categoryRepository.GetAll();


        public Category? GetById(int id) => _categoryRepository.GetById(id);


        public void Add(Category category) => _categoryRepository.Add(category);


        public void Update(Category category)
        {
            var existingCategory = _categoryRepository.GetById(category.Id);
            if (existingCategory == null)
                throw new Exception("Category not found");
            _categoryRepository.Update(category);
        }
        public void Delete(int id) => _categoryRepository.Delete(id);
        public List<Category> Search(string keyword) =>
            _categoryRepository.Search(keyword)
                .Where(c => c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();




    }
}
