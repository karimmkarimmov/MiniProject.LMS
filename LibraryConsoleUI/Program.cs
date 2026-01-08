using BusinessLogicLayer.Service;
using BusinessLogicLayer.Services;
using DataAccessLayer.DataContext;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            

            
            string dataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Console.WriteLine($"📁 Məlumatlar buraya saxlanılacaq: {dataPath}\n");

            
            DataBase.Books = FileStorage.LoadBooks("books.txt");
            DataBase.Categories = FileStorage.LoadCategories("categories.txt");
            DataBase.Members = FileStorage.LoadMembers("members.txt");

            Console.WriteLine("🔷 Kitabxana İdarəetmə Sisteminə Xoş Gəlmisiniz! 🔷\n");

            
            ShowMainMenu();

            
            Console.WriteLine("\n💾 Məlumatlar avtomatik saxlanılır...");
            SaveData();
            Console.WriteLine("✅ Saxlanıldı!");
        }
        static void ShowMainMenu()
        {
            while (true)
            {
                VoiceManagement.Menu();
                Console.Clear();
                Console.WriteLine("╔═══════════════════════════════════════════════════╗");
                Console.WriteLine("║  ƏSAS MENYU                                       ║");
                Console.WriteLine("╚═══════════════════════════════════════════════════╝\n");

                Console.WriteLine("1️📚  Kitab İdarəetməsi");
                Console.WriteLine("2️📁  Kateqoriya İdarəetməsi");
                Console.WriteLine("3️👥  Üzv İdarəetməsi");
                Console.WriteLine("4️🚪  Məlumatları Saxla və Çıx");
                Console.WriteLine("0️🚪  Çıxış");

                Console.Write("\n👉 Seçim edin: ");
                
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1": BookManager.Menu(); break;
                    case "2": CategoryManager.Menu(); break;
                    case "3": MemberManager.Menu(); break;
                    case "4":
                        SaveData();
                        Console.WriteLine("\n✅ Məlumatlar saxlanıldı!");
                        VoiceManagement.Success();
                        Console.WriteLine("👋 Sağ olun! Görüşənədək...");

                        return;
                    case "0":
                        VoiceManagement.Exit();
                        Console.WriteLine("\n👋 Sağ olun! Görüşənədək...");
                        return;
                    default:
                        VoiceManagement.Error();
                        Console.WriteLine("\n❌ Yanlış seçim! Enter basın...");
                        Console.ReadLine();
                        break;
                }
            }
        }
        static void SaveData()
        {
            FileStorage.SaveBooks("books.txt", DataBase.Books);
            FileStorage.SaveCategories("categories.txt", DataBase.Categories);
            FileStorage.SaveMembers("members.txt", DataBase.Members);
        }
        static void PrintHeader(string title)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {title.PadRight(47)}║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝\n");
        }

        static void SaveAndExit()
        {
            Console.WriteLine("\n💾 Məlumatlar saxlanılır...");

            FileStorage.SaveBooks("books.txt", DataBase.Books);
            FileStorage.SaveCategories("categories.txt", DataBase.Categories);
            FileStorage.SaveMembers("members.txt", DataBase.Members);

            Console.WriteLine("✅ Bütün məlumatlar uğurla saxlanıldı!");
            Console.WriteLine("\n👋 Sağ olun! Görüşənədək...");
        }
    }

    // ==================== BOOK MANAGER ====================
    public static class BookManager
    {
        private static readonly BookService _service = new BookService();

        public static void Menu()
        {
            while (true)
            {
                VoiceManagement.Menu();
                Console.Clear();
                PrintHeader("KİTAB İDARƏETMƏSİ");

                Console.WriteLine("1️➕ Yeni Kitab Əlavə Et");
                Console.WriteLine("2️📋 Bütün Kitabları Göstər");
                Console.WriteLine("3️🔍 Kitab Axtar");
                Console.WriteLine("4️✏️ Kitab Yenilə");
                Console.WriteLine("5️🗑️ Kitab Sil");
                Console.WriteLine("0️⬅️ Geri");

                Console.Write("\n👉 Seçim: ");
                
                string choice = Console.ReadLine() ?? "";

                try
                {
                    switch (choice)
                    {
                        case "1": AddBook(); break;
                        case "2": ShowAllBooks(); break;
                        case "3": SearchBook(); break;
                        case "4": UpdateBook(); break;
                        case "5": DeleteBook(); break;
                        case "0": return;
                        default:
                            VoiceManagement.Error();
                            Console.WriteLine("\n❌ Yanlış seçim!");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Xəta: {ex.Message}");
                    Pause();
                }
            }
        }

        static void AddBook()
        {
            Console.Clear();
            PrintHeader("YENİ KİTAB ƏLAVƏ ET");
            VoiceManagement.Select();

            Console.Write("📖 Kitab ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            VoiceManagement.Success();

            Console.Write("📚 Başlıq: ");
            string title = Console.ReadLine() ?? "";
            VoiceManagement.Success();

            Console.Write("✍️  Müəllif: ");
            string author = Console.ReadLine() ?? "";
            VoiceManagement.Success();

            Console.Write("📅 Nəşr ili: ");
            int year = int.Parse(Console.ReadLine() ?? "0");
            VoiceManagement.Success();

            Console.Write("🔢 ISBN: ");
            string isbn = Console.ReadLine() ?? "";
            VoiceManagement.Success();

            // Show categories
            var categories = new CategoryService().GetAll();
            Console.WriteLine("\n📂 Mövcud Kateqoriyalar:");
            foreach (var cat in categories)
                Console.WriteLine($"   {cat.Id}. {cat.Name}");

            Console.Write("\n🏷️  Kateqoriya ID: ");
            int catId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("✅ Əlçatandır? (b/x): ");
            bool isAvailable = Console.ReadLine()?.ToLower() == "b";

            var book = new Book
            {
                Id = id,
                Title = title,
                Author = author,
                PublishedYear = year,
                ISBN = isbn,
                CategoryId = catId,
                IsAvailable = isAvailable
            };

            _service.Add(book);
            Console.WriteLine("\n✅ Kitab uğurla əlavə edildi!");
            Pause();
        }

        static void ShowAllBooks()
        {
            Console.Clear();
            PrintHeader("BÜTÜN KİTABLAR");

            var books = _service.GetAll();
            var categories = new CategoryService().GetAll();

        if (books.Count == 0)
        {
            Console.WriteLine("📭 Heç bir kitab tapılmadı.");
            Pause();
            return;
        }

        Console.WriteLine(
        $"{"ID",-4} {"Başlıq",-25} {"Müəllif",-20} {"Kateqoriya",-15} {"İl",-6} {"Status",-10}"
        );
        Console.WriteLine(new string('─', 90));

        foreach (var book in books)
        {
            string categoryName = categories 
            .FirstOrDefault(c => c.Id == book.CategoryId)?.Name ?? "—";

            string status = book.IsAvailable ? "✅ Var" : "❌ Yox";

             Console.WriteLine(
            $"{book.Id,-4} {book.Title,-25} {book.Author,-20} {categoryName,-15} {book.PublishedYear,-6} {status,-10}"
             );
        }

        Console.WriteLine($"\n📊 Toplam: {books.Count} kitab");
        Pause();
        }

        static void SearchBook()
        {
            Console.Clear();
            PrintHeader("KİTAB AXTAR");

            Console.Write("🔍 Axtarış sözü (başlıq/müəllif/kateqoriya): ");
            string keyword = Console.ReadLine() ?? "";

            var books = _service.Search(keyword);

            if (books.Count == 0)
            {
                Console.WriteLine("\n📭 Heç bir nəticə tapılmadı.");
                Pause();
                return;
            }

            Console.WriteLine($"\n{"ID",-6} {"Başlıq",-30} {"Müəllif",-25} {"İl",-6}");
            Console.WriteLine(new string('─', 70));

            foreach (var book in books)
                Console.WriteLine($"{book.Id,-6} {book.Title,-30} {book.Author,-25} {book.PublishedYear,-6}");

            Console.WriteLine($"\n📊 Tapıldı: {books.Count} kitab");
            Pause();
        }

        static void UpdateBook()
        {
            Console.Clear();
            PrintHeader("KİTAB YENİLƏ");

            Console.Write("📖 Kitab ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var book = _service.GetById(id);
            if (book == null)
            {
                Console.WriteLine("\n❌ Kitab tapılmadı!");
                Pause();
                return;
            }

            Console.WriteLine($"\nHazırkı məlumat: {book.Title} - {book.Author}");

            Console.Write("\n📚 Yeni Başlıq (boş qoysan dəyişməz): ");
            string title = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(title)) book.Title = title;

            Console.Write("✍️  Yeni Müəllif: ");
            string author = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(author)) book.Author = author;

            Console.Write("📅 Yeni İl: ");
            string yearStr = Console.ReadLine() ?? "";
            if (int.TryParse(yearStr, out int year)) book.PublishedYear = year;

            Console.Write("🔢 Yeni ISBN: ");
            string isbn = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(isbn)) book.ISBN = isbn;

            Console.Write("✅ Əlçatandır? (b/x): ");
            string avail = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(avail)) book.IsAvailable = avail.ToLower() == "b";

            _service.Update(book);
            Console.WriteLine("\n✅ Kitab yeniləndi!");
            Pause();
        }

        static void DeleteBook()
        {
            Console.Clear();
            PrintHeader("KİTAB SİL");

            Console.Write("📖 Kitab ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var book = _service.GetById(id);
            Console.WriteLine($"\n⚠️  Silmək istədiyiniz kitab: {book.Title}");
            Console.Write("Əminsiniz? (b/x): ");

            if (Console.ReadLine()?.ToLower() == "b")
            {
                _service.Delete(id);
                Console.WriteLine("\n✅ Kitab silindi!");
            }
            else
            {
                Console.WriteLine("\n🚫 Əməliyyat ləğv edildi.");
            }

            Pause();
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {title.PadRight(47)}║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝\n");
        }

        static void Pause()
        {
            Console.Write("\n⏸️  Davam etmək üçün Enter basın...");
            Console.ReadLine();
        }
    }

    // ==================== CATEGORY MANAGER ====================
    public static class CategoryManager
    {
        private static readonly CategoryService _service = new CategoryService();

        public static void Menu()
        {
            while (true)
            {
                VoiceManagement.Menu();
                Console.Clear();
                PrintHeader("KATEQORİYA İDARƏETMƏSİ");

                Console.WriteLine("1️➕ Yeni Kateqoriya Əlavə Et");
                Console.WriteLine("2 📋 Bütün Kateqoriyaları Göstər");
                Console.WriteLine("3️🔍 Kateqoriya Axtar");
                Console.WriteLine("4️✏️ Kateqoriya Yenilə");
                Console.WriteLine("5️🗑️ Kateqoriya Sil");
                Console.WriteLine("0️⬅️ Geri");

                Console.Write("\n👉 Seçim: ");
                string choice = Console.ReadLine() ?? "";

                try
                {
                    switch (choice)
                    {
                        case "1": AddCategory(); break;
                        case "2": ShowAllCategories(); break;
                        case "3": SearchCategory(); break;
                        case "4": UpdateCategory(); break;
                        case "5": DeleteCategory(); break;
                        case "0": return;
                        default:
                            Console.WriteLine("\n❌ Yanlış seçim!");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Xəta: {ex.Message}");
                    Pause();
                }
            }
        }

        static void AddCategory()
        {
            Console.Clear();
            PrintHeader("YENİ KATEQORİYA ƏLAVƏ ET");

            Console.Write("🔢 Kateqoriya ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("📂 Ad: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("📝 Təsvir: ");
            string desc = Console.ReadLine() ?? "";

            var category = new Category
            {
                Id = id,
                Name = name,
                Description = desc
            };

            _service.Add(category);
            Console.WriteLine("\n✅ Kateqoriya əlavə edildi!");
            Pause();
        }

        static void ShowAllCategories()
        {
            Console.Clear();
            PrintHeader("BÜTÜN KATEQORİYALAR");

            var categories = _service.GetAll();

            if (categories.Count == 0)
            {
                Console.WriteLine("📭 Heç bir kateqoriya tapılmadı.");
                Pause();
                return;
            }

            Console.WriteLine($"{"ID",-6} {"Ad",-30} {"Təsvir",-50}");
            Console.WriteLine(new string('─', 90));

            foreach (var cat in categories)
                Console.WriteLine($"{cat.Id,-6} {cat.Name,-30} {cat.Description,-50}");

            Console.WriteLine($"\n📊 Toplam: {categories.Count} kateqoriya");
            Pause();
        }

        static void SearchCategory()
        {
            Console.Clear();
            PrintHeader("KATEQORİYA AXTAR");

            Console.Write("🔍 Axtarış sözü: ");
            string keyword = Console.ReadLine() ?? "";

            var categories = _service.Search(keyword);

            if (categories.Count == 0)
            {
                Console.WriteLine("\n📭 Heç bir nəticə tapılmadı.");
                Pause();
                return;
            }

            Console.WriteLine($"\n{"ID",-6} {"Ad",-30} {"Təsvir",-50}");
            Console.WriteLine(new string('─', 90));

            foreach (var cat in categories)
                Console.WriteLine($"{cat.Id,-6} {cat.Name,-30} {cat.Description,-50}");

            Console.WriteLine($"\n📊 Tapıldı: {categories.Count} kateqoriya");
            Pause();
        }

        static void UpdateCategory()
        {
            Console.Clear();
            PrintHeader("KATEQORİYA YENİLƏ");

            Console.Write("🔢 Kateqoriya ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var cat = _service.GetById(id);
            Console.WriteLine($"\nHazırkı: {cat.Name}");

            Console.Write("\n📂 Yeni Ad: ");
            string name = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(name)) cat.Name = name;

            Console.Write("📝 Yeni Təsvir: ");
            string desc = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(desc)) cat.Description = desc;

            _service.Update(cat);
            Console.WriteLine("\n✅ Kateqoriya yeniləndi!");
            Pause();
        }

        static void DeleteCategory()
        {
            Console.Clear();
            PrintHeader("KATEQORİYA SİL");

            Console.Write("🔢 Kateqoriya ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var cat = _service.GetById(id);
            Console.WriteLine($"\n⚠️  Silmək istədiyiniz: {cat.Name}");
            Console.Write("Əminsiniz? (b/x): ");

            if (Console.ReadLine()?.ToLower() == "b")
            {
                _service.Delete(id);
                Console.WriteLine("\n✅ Kateqoriya silindi!");
            }
            else
            {
                Console.WriteLine("\n🚫 Əməliyyat ləğv edildi.");
            }

            Pause();
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {title.PadRight(47)}║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝\n");
        }

        static void Pause()
        {
            Console.Write("\n⏸️  Davam etmək üçün Enter basın...");
            Console.ReadLine();
        }
    }

    // ==================== MEMBER MANAGER ====================
    public static class MemberManager
    {
        private static readonly MemberService _service = new MemberService();

        public static void Menu()
        {
            while (true)
            {
                VoiceManagement.Menu();
                Console.Clear();
                PrintHeader("ÜZV İDARƏETMƏSİ");

                Console.WriteLine("1️➕  Yeni Üzv Əlavə Et");
                Console.WriteLine("2️📋  Bütün Üzvləri Göstər");
                Console.WriteLine("3️🔍  Üzv Axtar");
                Console.WriteLine("4️✏️  Üzv Yenilə");
                Console.WriteLine("5️🗑️  Üzv Sil");
                Console.WriteLine("0️⬅️  Geri");
                Console.Write("\n👉 Seçim: ");
                string choice = Console.ReadLine() ?? "";

                try
                {
                    switch (choice)
                    {
                        case "1": AddMember(); break;
                        case "2": ShowAllMembers(); break;
                        case "3": SearchMember(); break;
                        case "4": UpdateMember(); break;
                        case "5": DeleteMember(); break;
                        case "0": return;
                        default:
                            Console.WriteLine("\n❌ Yanlış seçim!");
                            Pause();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n❌ Xəta: {ex.Message}");
                    Pause();
                }
            }
        }

        static void AddMember()
        {
            Console.Clear();
            PrintHeader("YENİ ÜZV ƏLAVƏ ET");

            Console.Write("🔢 Üzv ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("👤 Ad Soyad: ");
            string fullName = Console.ReadLine() ?? "";

            Console.Write("📧 Email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("📱 Telefon: ");
            string phone = Console.ReadLine() ?? "";

            Console.Write("✅ Aktiv? (b/x): ");
            bool isActive = Console.ReadLine()?.ToLower() == "b";

            var member = new Member
            {
                Id = id,
                FullName = fullName,
                Email = email,
                PhoneNumber = phone,
                IsActive = isActive,
                MembershipDate = DateTime.Now
            };

            _service.Add(member);
            Console.WriteLine("\n✅ Üzv əlavə edildi!");
            Pause();
        }

        static void ShowAllMembers()
        {
            Console.Clear();
            PrintHeader("BÜTÜN ÜZVLƏR");

            var members = _service.GetAll();

            if (members.Count == 0)
            {
                Console.WriteLine("📭 Heç bir üzv tapılmadı.");
                Pause();
                return;
            }

            Console.WriteLine($"{"ID",-6} {"Ad Soyad",-30} {"Email",-30} {"Telefon",-15} {"Status",-10}");
            Console.WriteLine(new string('─', 100));

            foreach (var m in members)
            {
                string status = m.IsActive ? "✅ Aktiv" : "❌ Passiv";
                Console.WriteLine($"{m.Id,-6} {m.FullName,-30} {m.Email,-30} {m.PhoneNumber,-15} {status,-10}");
            }

            Console.WriteLine($"\n📊 Toplam: {members.Count} üzv");
            Pause();
        }

        static void SearchMember()
        {
            Console.Clear();
            PrintHeader("ÜZV AXTAR");

            Console.Write("🔍 Axtarış sözü (ad/email/telefon): ");
            string keyword = Console.ReadLine() ?? "";

            var members = _service.Search(keyword);

            if (members.Count == 0)
            {
                Console.WriteLine("\n📭 Heç bir nəticə tapılmadı.");
                Pause();
                return;
            }

            Console.WriteLine($"\n{"ID",-6} {"Ad Soyad",-30} {"Email",-30}");
            Console.WriteLine(new string('─', 70));

            foreach (var m in members)
                Console.WriteLine($"{m.Id,-6} {m.FullName,-30} {m.Email,-30}");

            Console.WriteLine($"\n📊 Tapıldı: {members.Count} üzv");
            Pause();
        }

        static void UpdateMember()
        {
            Console.Clear();
            PrintHeader("ÜZV YENİLƏ");

            Console.Write("🔢 Üzv ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var member = _service.GetById(id);
            Console.WriteLine($"\nHazırkı: {member.FullName}");

            Console.Write("\n👤 Yeni Ad (boş saxlasan dəyişməz): ");
            string name = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(name)) member.FullName = name;

            Console.Write("📧 Yeni Email: ");
            string email = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(email)) member.Email = email;

            Console.Write("📱 Yeni Telefon: ");
            string phone = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(phone)) member.PhoneNumber = phone;

            Console.Write("✅ Aktiv? (b/x): ");
            string active = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(active)) member.IsActive = active.ToLower() == "b";

            _service.Update(member);
            Console.WriteLine("\n✅ Üzv yeniləndi!");
            Pause();
        }

        static void DeleteMember()
        {
            Console.Clear();
            PrintHeader("ÜZV SİL");

            Console.Write("🔢 Üzv ID: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var member = _service.GetById(id);
            Console.WriteLine($"\n⚠️  Silmək istədiyiniz: {member.FullName}");
            Console.Write("Əminsiniz? (b/x): ");

            if (Console.ReadLine()?.ToLower() == "b")
            {
                _service.Delete(id);
                Console.WriteLine("\n✅ Üzv silindi!");
            }
            else
            {
                Console.WriteLine("\n🚫 Əməliyyat ləğv edildi.");
            }

            Pause();
        }

        static void PrintHeader(string title)
        {
            Console.WriteLine("╔═══════════════════════════════════════════════════╗");
            Console.WriteLine($"║  {title.PadRight(47)}║");
            Console.WriteLine("╚═══════════════════════════════════════════════════╝\n");
        }

        static void Pause()
        {
            Console.Write("\n⏸️  Davam etmək üçün Enter basın...");
            Console.ReadLine();
        }
    }
}