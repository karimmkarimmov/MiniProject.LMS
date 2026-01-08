using DataAccessLayer.DataContext;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Services
{
    public class CategoryService
    {
        private readonly CategoryRepository _repo = new CategoryRepository();

        public void Add(Category c)
        {
            Validate(c);

            
            var existing = DataBase.Categories.Find(cat => cat.Id == c.Id);
            if (existing != null)
                throw new Exception("Category Id already exists.");

            _repo.Add(c);
        }

        public List<Category> GetAll() => _repo.GetAll();

        public Category GetById(int id) => _repo.GetById(id);

        public void Update(Category c)
        {
            Validate(c);

            var existing = DataBase.Categories.Find(cat => cat.Id == c.Id);
            if (existing == null)
                throw new Exception("Category not found.");

            _repo.Update(c);
        }

        public void Delete(int id) => _repo.Delete(id);

        public List<Category> Search(string keyword) => _repo.Search(keyword);

        private static void Validate(Category c)
        {
            if (c == null) throw new Exception("Category is null.");
            if (c.Id <= 0) throw new Exception("Category Id must be > 0.");
            if (string.IsNullOrWhiteSpace(c.Name)) throw new Exception("Category Name is required.");
        }
    }
}