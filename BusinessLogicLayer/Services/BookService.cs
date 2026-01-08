using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BusinessLogicLayer.Service
{
    public class BookService
    {
        private readonly BookRepository _bookRepo = new BookRepository();
        private readonly CategoryRepository _catRepo = new CategoryRepository();

        public void Add(Book b)
        {
            Validate(b);

            
            var existing = DataBase.Books.Find(book => book.Id == b.Id);
            if (existing != null)
                throw new Exception("Book Id already exists.");

            var category = DataBase.Categories.Find(c => c.Id == b.CategoryId);
            if (category == null)
                throw new Exception("CategoryId not found.");

            _bookRepo.Add(b);
        }

        public List<Book> GetAll() => _bookRepo.GetAll();

        public Book GetById(int id) => _bookRepo.GetById(id);

        public void Update(Book b)
        {
            Validate(b);

            var existing = DataBase.Books.Find(book => book.Id == b.Id);
            if (existing == null)
                throw new Exception("Book not found.");

            var category = DataBase.Categories.Find(c => c.Id == b.CategoryId);
            if (category == null)
                throw new Exception("CategoryId not found.");

            _bookRepo.Update(b);
        }

        public void Delete(int id) => _bookRepo.Delete(id);

        public List<Book> Search(string keyword)
        {
            keyword ??= "";
            var k = keyword.Trim().ToLower();

            var books = _bookRepo.GetAll();
            var cats = _catRepo.GetAll();

            var matchedCatIds = cats
                .Where(c => (c.Name ?? "").ToLower().Contains(k))
                .Select(c => c.Id)
                .ToHashSet();

            return books
                .Where(b =>
                    (b.Title ?? "").ToLower().Contains(k) ||
                    (b.Author ?? "").ToLower().Contains(k) ||
                    matchedCatIds.Contains(b.CategoryId))
                .ToList();
        }

        private static void Validate(Book b)
        {
            if (b == null) throw new Exception("Book is null.");
            if (b.Id <= 0) throw new Exception("Book Id must be > 0.");
            if (string.IsNullOrWhiteSpace(b.Title)) throw new Exception("Title is required.");
            if (string.IsNullOrWhiteSpace(b.Author)) throw new Exception("Author is required.");
            if (string.IsNullOrWhiteSpace(b.ISBN)) throw new Exception("ISBN is required.");

            if (b.PublishedYear < 1000 || b.PublishedYear > DateTime.Now.Year)
                throw new Exception("PublishedYear is invalid.");
        }
    }
}