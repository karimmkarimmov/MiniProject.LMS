using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace BusinessLogicLayer.Services.Contracts
{
    public interface IBookService
    {
        void Add(Book book); 
        Book Get(int id);
        List<Book> GetAll();
        void Update(Book book);
        void Delete(int id);
        List<Book> Search(string keyword);
    }
}
