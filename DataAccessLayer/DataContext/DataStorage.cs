using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccessLayer.DataContext
{
    public static class FileStorage
    {
        private static readonly string BaseFolder =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

        private static string GetPath(string fileName)
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);

            return Path.Combine(BaseFolder, fileName);
        }

        // ===================== BOOK =====================

        public static void SaveBooks(string fileName, List<Book> books)
        {
            string path = GetPath(fileName);

            using StreamWriter sw = new(path, false, Encoding.UTF8);

            foreach (var b in books)
            {
                string line =
                    b.Id.ToString("D5") + "|" +
                    b.Title.PadRight(30).Substring(0, 30) + "|" +
                    b.Author.PadRight(25).Substring(0, 25) + "|" +
                    b.ISBN.PadRight(13).Substring(0, 13) + "|" +
                    b.PublishedYear.ToString("D4") + "|" +
                    b.CategoryId.ToString("D5") + "|" +
                    (b.IsAvailable ? "1" : "0");

                sw.WriteLine(line);
            }
        }

        public static List<Book> LoadBooks(string fileName)
        {
            List<Book> books = new();
            string path = GetPath(fileName);

            if (!File.Exists(path))
                return books;

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
            {
                try
                {
                    string[] parts = line.Split('|');
                    if (parts.Length != 7) continue;

                    books.Add(new Book
                    {
                        Id = int.Parse(parts[0].Trim()),
                        Title = parts[1].Trim(),
                        Author = parts[2].Trim(),
                        ISBN = parts[3].Trim(),
                        PublishedYear = int.Parse(parts[4].Trim()),
                        CategoryId = int.Parse(parts[5].Trim()),
                        IsAvailable = parts[6].Trim() == "1"
                    });
                }
                catch { }
            }

            return books;
        }

        // ===================== CATEGORY =====================

        public static void SaveCategories(string fileName, List<Category> categories)
        {
            string path = GetPath(fileName);

            using StreamWriter sw = new(path, false, Encoding.UTF8);

            foreach (var c in categories)
            {
                string line =
                    c.Id.ToString("D5") + "|" +
                    c.Name.PadRight(30).Substring(0, 30) + "|" +
                    c.Description.PadRight(50).Substring(0, 50);

                sw.WriteLine(line);
            }
        }

        public static List<Category> LoadCategories(string fileName)
        {
            List<Category> categories = new();
            string path = GetPath(fileName);

            if (!File.Exists(path))
                return categories;

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
            {
                try
                {
                    string[] parts = line.Split('|');
                    if (parts.Length != 3) continue;

                    categories.Add(new Category
                    {
                        Id = int.Parse(parts[0].Trim()),
                        Name = parts[1].Trim(),
                        Description = parts[2].Trim()
                    });
                }
                catch { }
            }

            return categories;
        }

        // ===================== MEMBER =====================

        public static void SaveMembers(string fileName, List<Member> members)
        {
            string path = GetPath(fileName);

            using StreamWriter sw = new(path, false, Encoding.UTF8);

            foreach (var m in members)
            {
                string line =
                    m.Id.ToString("D5") + "|" +
                    m.FullName.PadRight(30).Substring(0, 30) + "|" +
                    m.Email.PadRight(30).Substring(0, 30) + "|" +
                    m.PhoneNumber.PadRight(15).Substring(0, 15) + "|" +
                    m.MembershipDate.ToString("yyyy-MM-dd") + "|" +
                    (m.IsActive ? "1" : "0");

                sw.WriteLine(line);
            }
        }

        public static List<Member> LoadMembers(string fileName)
        {
            List<Member> members = new();
            string path = GetPath(fileName);

            if (!File.Exists(path))
                return members;

            foreach (var line in File.ReadAllLines(path, Encoding.UTF8))
            {
                try
                {
                    string[] parts = line.Split('|');
                    if (parts.Length != 6) continue;

                    members.Add(new Member
                    {
                        Id = int.Parse(parts[0].Trim()),
                        FullName = parts[1].Trim(),
                        Email = parts[2].Trim(),
                        PhoneNumber = parts[3].Trim(),
                        MembershipDate = DateTime.Parse(parts[4].Trim()),
                        IsActive = parts[5].Trim() == "1"
                    });
                }
                catch { }
            }

            return members;
        }
    }
}

