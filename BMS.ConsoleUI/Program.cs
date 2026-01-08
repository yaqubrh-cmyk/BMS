using BMS.DAL.Models;
using BMS.DAL.Reporsitories;


namespace BMS.ConsoleUI
{
    internal class Program
    {

        static BookRepository bookRepo = new BookRepository();
        static CategoryRepository catRepo = new CategoryRepository();
        static MemberRepository memberRepo = new MemberRepository();

        static void Main()
        {
            SeedData();

            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("===== 📚 LIBRARY MANAGEMENT SYSTEM =====");
                Console.ResetColor();

                Console.WriteLine("1. Show Books");
                Console.WriteLine("2. Search Books");
                Console.WriteLine("3. Show Categories");
                Console.WriteLine("4. Show Members");
                Console.WriteLine("5. Add Book");
                Console.WriteLine("6. Add Member");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine()!;
                switch (choice)
                {
                    case "1": ShowBooks(); break;
                    case "2": SearchBooks(); break;
                    case "3": ShowCategories(); break;
                    case "4": ShowMembers(); break;
                    case "5": AddBook(); break;
                    case "6": AddMember(); break;
                    case "0": return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option!");
                        Console.ResetColor();
                        Pause(); break;
                }
            }
        }

        static void ShowBooks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📚 BOOKS");
            Console.ResetColor();

            foreach (var b in bookRepo.GetAll())
            {
                var category = catRepo.GetById(b.CategoryId);
                Console.WriteLine($"{b.Id}. {b.Title} - {b.Author} | Category: {category?.Name} | Year: {b.PublishedYear}");
            }
            Pause();
        }

        static void SearchBooks()
        {
            Console.Clear();
            Console.Write("Enter keyword: ");
            string keyword = Console.ReadLine()!;
            var results = bookRepo.Search(keyword);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nRESULTS:");
            Console.ResetColor();

            if (!results.Any())
                Console.WriteLine("No books found!");
            else
                foreach (var b in results)
                    Console.WriteLine($"{b.Title} - {b.Author}");

            Pause();
        }

        static void ShowCategories()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("📂 CATEGORIES");
            Console.ResetColor();

            foreach (var c in catRepo.GetAll())
                Console.WriteLine($"{c.Id}. {c.Name} - {c.Description}");
            Pause();
        }

        static void ShowMembers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("👤 MEMBERS");
            Console.ResetColor();

            foreach (var m in memberRepo.GetAll())
                Console.WriteLine($"{m.Id}. {m.FullName} - {m.Email} | Active: {m.IsActive}");
            Pause();
        }

        static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("Enter book details:");

            Console.Write("Title: "); string title = Console.ReadLine()!;
            Console.Write("Author: "); string author = Console.ReadLine()!;
            Console.Write("ISBN: "); string isbn = Console.ReadLine()!;
            Console.Write("Published Year: "); int year = int.Parse(Console.ReadLine()!);
            Console.Write("Category Id: "); int catId = int.Parse(Console.ReadLine()!);

            int newId = bookRepo.GetAll().Count + 1;
            bookRepo.Add(new Book
            {
                Id = newId,
                Title = title,
                Author = author,
                ISBN = isbn,
                PublishedYear = year,
                CategoryId = catId,
                IsAvailable = true
            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book added successfully!");
            Console.ResetColor();
            Pause();
        }

        static void AddMember()
        {
            Console.Clear();
            Console.WriteLine("Enter member details:");

            Console.Write("Full Name: "); string name = Console.ReadLine()!;
            Console.Write("Email: "); string email = Console.ReadLine()!;
            Console.Write("Phone: "); string phone = Console.ReadLine()!;

            int newId = memberRepo.GetAll().Count + 1;
            memberRepo.Add(new Member
            {
                Id = newId,
                FullName = name,
                Email = email,
                PhoneNumber = phone,
                MembershipDate = DateTime.Now,
                IsActive = true
            });

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Member added successfully!");
            Console.ResetColor();
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        static void SeedData()
        {
            if (!catRepo.GetAll().Any())
            {
                catRepo.Add(new Category { Id = 1, Name = "Science Fiction", Description = "Books about future worlds" });
                catRepo.Add(new Category { Id = 2, Name = "Fantasy", Description = "Magic and supernatural" });
            }

            if (!bookRepo.GetAll().Any())
            {
                bookRepo.Add(new Book { Id = 1, Title = "Dune", Author = "Frank Herbert", ISBN = "123", PublishedYear = 1965, CategoryId = 1, IsAvailable = true });
                bookRepo.Add(new Book { Id = 2, Title = "The Hobbit", Author = "Tolkien", ISBN = "456", PublishedYear = 1937, CategoryId = 2, IsAvailable = true });
            }

            if (!memberRepo.GetAll().Any())
            {
                memberRepo.Add(new Member { Id = 1, FullName = "Ali Aliyev", Email = "ali@mail.com", PhoneNumber = "0551234567", MembershipDate = DateTime.Now, IsActive = true });
                memberRepo.Add(new Member { Id = 2, FullName = "Aysel Karimova", Email = "aysel@mail.com", PhoneNumber = "0557654321", MembershipDate = DateTime.Now, IsActive = true });
            }
        }
    }
}
