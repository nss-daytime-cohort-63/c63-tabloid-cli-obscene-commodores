using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;
using System.Data;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Add a Blog");
            Console.WriteLine(" 3) Delete a Blog");
            Console.WriteLine(" 4) Edit a Blog");
            Console.WriteLine(" 0) Go back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    AddNewBlog();
                    return this;
                case "3":
                    DeleteBlog();
                    return this;
                case "4":
                    EditBlog();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach(Blog blog in blogs)
            {
                Console.WriteLine(blog.Title);
            }
        }
        private void AddNewBlog()
        {
            Console.Write("What is the Title of the new blog? : ");
            string blogTitle = Console.ReadLine();
            Console.Write("What is the url of the new blog? : ");
            string bogUrl = Console.ReadLine();
            Blog newBlog = new Blog()
            {
                Title = blogTitle,
                Url = bogUrl
            };
            _blogRepository.Insert(newBlog);
        }
        private void DeleteBlog()
        {
            Console.Write("Enter Blog Id to Delete: ");
            int delBlogId = Int32.Parse(Console.ReadLine());
            _blogRepository.Delete(delBlogId);
        }
        private void EditBlog()
        {
            List<Blog> blogList = _blogRepository.GetAll();
            foreach (Blog b in blogList)
            {
                Console.WriteLine($" {b.Id}) {b.Title}");
            }
            Console.Write("Which blog would you like to edit? : ");
            int updateId = Int32.Parse(Console.ReadLine());
            Blog updatedBlog = blogList.FirstOrDefault(blog => blog.Id == updateId);
            Console.Write("Update the Title");
            string updatedBlogTitle = Console.ReadLine();
            Console.Write("Update the Url");
            string updateBlogUrl = Console.ReadLine();
            updatedBlog.Title = updatedBlogTitle;
            updatedBlog.Url = updateBlogUrl;
            _blogRepository.Update(updatedBlog);
            Console.WriteLine("Your blog has been successfully updated.");
        }
    }
}
