using BMS.DAL.DataContext;
using BMS.DAL.Models;
using BMS.DAL.Reporsitories;
using System;
using System.Linq;

namespace BMS.UI
{
    internal class Program
    {
        private static BookRepository _bookRepository = new BookRepository();
        private static CategoryRepository _categoryRepository = new CategoryRepository();
        private static MemberRepository _memberRepository = new MemberRepository();

        static void Main(string[] args)
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
                Console.WriteLine("7. Add Category");
                Console.WriteLine("0. Exit");
                Console.Write("\nSelect option: ");

                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1": ShowBooks(); break;
                    case "2": SearchBooks(); break;
                    case "3": ShowCategories(); break;
                    case "4": ShowMembers(); break;
                    case "5": AddBook(); break;
                    case "6": AddMember(); break;
                    case "7": AddCategory(); break;
                    case "0": return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid option!");
                        Console.ResetColor();
                        Pause(); break;
                }
            }
        }

        #region Show Methods
        private static void ShowBooks()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("📚 BOOKS");
            Console.ResetColor();

            foreach (var b in _bookRepository.GetAll())
            {
                var category = _categoryRepository.GetById(b.CategoryId);
                Console.WriteLine($"{b.Id}. {b.Title} - {b.Author} | Category: {category?.Name ?? "Unknown"} | Year: {b.PublishedYear}");
            }

            Pause();
        }

        private static void ShowCategories()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("📂 CATEGORIES");
            Console.ResetColor();

            foreach (var c in _categoryRepository.GetAll())
            {
                Console.WriteLine($"{c.Id}. {c.Name} - {c.Description}");
            }

            Pause();
        }

        private static void ShowMembers()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("👤 MEMBERS");
            Console.ResetColor();

            foreach (var m in _memberRepository.GetAll())
            {
                Console.WriteLine($"{m.Id}. {m.FullName} - {m.Email} | Active: {m.IsActive}");
            }

            Pause();
        }
        #endregion

        #region Add Methods
        private static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("Add New Book");

            try
            {
                Console.Write("Title: ");
                string title = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Author: ");
                string author = Console.ReadLine()?.Trim() ?? "";

                Console.Write("ISBN: ");
                string isbn = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Published Year: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                    throw new Exception("Invalid year!");

                Console.Write("Category Id: ");
                if (!int.TryParse(Console.ReadLine(), out int categoryId) || _categoryRepository.GetById(categoryId) == null)
                    throw new Exception("Invalid or non-existing category!");

                int newId = _bookRepository.GetAll().Count + 1;

                _bookRepository.Add(new Book
                {
                    Id = newId,
                    Title = title,
                    Author = author,
                    ISBN = isbn,
                    PublishedYear = year,
                    CategoryId = categoryId,
                    IsAvailable = true
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Book added successfully!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Pause();
            }
        }

        private static void AddMember()
        {
            Console.Clear();
            Console.WriteLine("Add New Member");

            try
            {
                Console.Write("Full Name: ");
                string fullName = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Email: ");
                string email = Console.ReadLine()?.Trim() ?? "";

                Console.Write("Phone Number: ");
                string phone = Console.ReadLine()?.Trim() ?? "";

                int newId = _memberRepository.GetAll().Count + 1;

                _memberRepository.Add(new Member
                {
                    Id = newId,
                    FullName = fullName,
                    Email = email,
                    PhoneNumber = phone,
                    MembershipDate = DateTime.Now,
                    IsActive = true
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Member added successfully!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Pause();
            }
        }

        private static void AddCategory()
        {
            Console.Clear();
            Console.WriteLine("Add New Category");

            try
            {
                Console.Write("Category Name: ");
                string name = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrEmpty(name)) throw new Exception("Category name cannot be empty.");

                Console.Write("Description: ");
                string description = Console.ReadLine()?.Trim() ?? "";

                int newId = _categoryRepository.GetAll().Count + 1;

                _categoryRepository.Add(new Category
                {
                    Id = newId,
                    Name = name,
                    Description = description
                });

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Category added successfully!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                Console.ResetColor();
                Pause();
            }
        }
        #endregion

        #region Search Methods
        private static void SearchBooks()
        {
            Console.Clear();
            Console.Write("Enter keyword: ");
            string keyword = Console.ReadLine()?.Trim() ?? "";

            var results = _bookRepository.Search(keyword);

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
        #endregion

        private static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }

        private static void SeedData()
        {
            if (!_categoryRepository.GetAll().Any())
            {
                _categoryRepository.Add(new Category { Id = 1, Name = "Science Fiction", Description = "Books about future worlds" });
                _categoryRepository.Add(new Category { Id = 2, Name = "Fantasy", Description = "Magic and supernatural" });
            }

            if (!_bookRepository.GetAll().Any())
            {
                _bookRepository.Add(new Book { Id = 1, Title = "Dune", Author = "Frank Herbert", ISBN = "123", PublishedYear = 1965, CategoryId = 1, IsAvailable = true });
                _bookRepository.Add(new Book { Id = 2, Title = "The Hobbit", Author = "Tolkien", ISBN = "456", PublishedYear = 1937, CategoryId = 2, IsAvailable = true });
            }

            if (!_memberRepository.GetAll().Any())
            {
                _memberRepository.Add(new Member { Id = 1, FullName = "Ali Aliyev", Email = "ali@mail.com", PhoneNumber = "0551234567", MembershipDate = DateTime.Now, IsActive = true });
                _memberRepository.Add(new Member { Id = 2, FullName = "Aysel Karimova", Email = "aysel@mail.com", PhoneNumber = "0557654321", MembershipDate = DateTime.Now, IsActive = true });
            }
        }
    }
}
