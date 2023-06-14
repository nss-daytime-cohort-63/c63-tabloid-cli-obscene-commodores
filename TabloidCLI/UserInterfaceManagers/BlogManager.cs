using System;
using System.Collections.Generic;
using System.Linq;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private PostRepository _postRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _connectionString = connectionString;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Add a Blog");
            Console.WriteLine(" 3) Delete a Blog");
            Console.WriteLine(" 4) Edit a Blog");
            Console.WriteLine(" 5) View Blog Details");
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
                case "5":
                    BlogDetails();
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
            while(string.IsNullOrEmpty(updatedBlogTitle))
            {
                Console.WriteLine("Invalid Entry");
                Console.Write("Update the Title");
                updatedBlogTitle = Console.ReadLine();
            }
            Console.Write("Update the Url : ");
            string updatedBlogUrl = Console.ReadLine();
            while (string.IsNullOrEmpty(updatedBlogUrl))
            {
                Console.WriteLine("Invalid Entry");
                Console.Write("Update the Url : ");
                updatedBlogUrl = Console.ReadLine();
            }
            updatedBlog.Title = updatedBlogTitle;
            updatedBlog.Url = updatedBlogUrl;
            _blogRepository.Update(updatedBlog);
            Console.WriteLine("Your blog has been successfully updated.");
        }
        private void BlogDetails()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog b in blogs)
            {
                Console.WriteLine($" {b.Id}) {b.Title}");
            }
            Console.Write("Select a blog to view in more detail : ");
            int selBlogId = Int32.Parse(Console.ReadLine());
            Blog selBlog = blogs.FirstOrDefault(b => b.Id == selBlogId);
            Console.WriteLine($"{selBlog.Title} : {selBlog.Url}");
            List<string> detailOptions = new List<string>
            {
                "View", "Add Tag", "Remove Tag", "View Posts", "Return"
            };
            for (int i = 0; i < detailOptions.Count; i++)
            {
                Console.WriteLine($" {i + 1} ) {detailOptions[i]}");
            }
            Console.Write("Select an option : ");
            int select = Int32.Parse(Console.ReadLine());
            switch (select)
            {
                case 1:
                    Console.WriteLine($"{selBlog.Title} : {selBlog.Url}");
                    break;
                case 2:
                    Console.Write("Add a tag to this blog : ");
                    break;
                case 3:
                    Console.Write("Remove a tag from this blog : ");
                    break;
                case 4:
                    List<Post> posts = _postRepository.GetAll();
                    List<Post> blogPosts = new List<Post>();
                    foreach (Post p in posts)
                    {
                        if (p.Blog.Id == selBlog.Id)
                        {
                            blogPosts.Add(p);
                        }
                    }
                    foreach(Post post in blogPosts)
                    {
                        Console.WriteLine($"Title: {post.Title}, Author: {post.Author}, Url: {post.Url}");
                    }
                    break;
                case 5:
                    return;
            }
        }
    }
}
