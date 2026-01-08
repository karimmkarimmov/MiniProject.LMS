using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using DataAccessLayer.DataContext;
using DataAccessLayer.Repositories.Contracts;

namespace DataAccessLayer.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public void Add(Category entity)
        {
            DataBase.Categories.Add(entity);
        }

        public void Delete(int id)
        {
            var category = GetById(id);
            if (category != null)
            {
                DataBase.Categories.Remove(category);
            }
            else
            {
                throw new Exception("Category not found");
            }
        }

        public List<Category> GetAll()
        {
            return DataBase.Categories;
        }

        public Category GetById(int id)
        {
            var category = DataBase.Categories.Find(b => b.Id == id);
            if (category == null)
                throw new Exception("Category not found");
            return category;
        }

        public List<Category> Search(string keyword)
        {
            return DataBase.Categories
                .Where(c => c.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            c.Description.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void Update(Category entity)
        {
            var category = GetById(entity.Id);
            if (category != null)
            {
                category.Name = entity.Name;
                category.Description = entity.Description;
                return;
            }
            throw new Exception("Category not found");
        }
    }
}



