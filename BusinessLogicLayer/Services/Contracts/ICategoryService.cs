using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Services.Contracts
{
    public interface ICategoryService
    {
        void Add(Category category); 
        Category Get(int id);
        List<Category> GetAll();
        void Update(Category category);
        void Delete(int id);
        List<Category> Search(string keyword);
    }
}
