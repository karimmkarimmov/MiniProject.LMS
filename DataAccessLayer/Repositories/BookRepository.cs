using DataAccessLayer.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccessLayer.Models;
using DataAccessLayer.DataContext;


namespace DataAccessLayer.Repositories
{
    public class BookRepository : IBookRepository
    {
        public void Add(Book entity)
        {
            DataBase.Books.Add(entity);
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            {
                DataBase.Books.Remove(book);
            }
            else
            {
                throw new Exception("Book not found");
            }
        }

        public List<Book> GetAll()
        {
            return DataBase.Books;
        }

        public Book GetById(int id)
        {
            var book = DataBase.Books.Find(b => b.Id == id);
            if (book == null)
                throw new Exception("Book not found");
            return book;
        }

        public List<Book> Search(string keyword)
        {
            return DataBase.Books
                .Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            b.Author.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            b.ISBN.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
        

        public void Update(Book entity)
        {
            var book = GetById(entity.Id);
            if (book != null)
            {
                book.Title = entity.Title;
                book.Author = entity.Author;
                book.CategoryId = entity.CategoryId;
                book.PublishedYear = entity.PublishedYear;
                return;
            }
            throw new Exception("Book not found");
        }
    }
}
