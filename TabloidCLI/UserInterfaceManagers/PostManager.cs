using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Post Details");
            Console.WriteLine(" 3) Add Post");
            Console.WriteLine(" 4) Edit Post");
            Console.WriteLine(" 5) Remove Post");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "2":
                    return this;
                   
                case "3":
                    Add();
                    return this;
                case "4":
                   // Edit();
                    return this;
                case "5":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void Add()
        {
            Console.WriteLine("New Post");
            Post post = new Post();

            Console.WriteLine("Title: ");

            post.Title = Console.ReadLine();

            Console.Write("Url: ");
            post.Url = Console.ReadLine();

            post.PublishDateTime =  DateTime.Now;
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"Id: {author.Id} - {author.FirstName} {author.LastName}");
            }
            Console.Write("AuthorId: ");
            int matchingId = int.Parse(Console.ReadLine());
            post.Author = authors.Where(a => a.Id == matchingId).FirstOrDefault();

            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"Id: {blog.Id} - {blog.Title} {blog.Url}");
            }
            Console.Write("BlogId: ");
            int matchingBlogId = int.Parse(Console.ReadLine());
            post.Blog = blogs.Where(b => b.Id == matchingBlogId).FirstOrDefault();
            


           _postRepository.Insert(post);

            Console.WriteLine("Post added successfully");
        }
        private void List()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"{post.Title} from {post.Url} by {post.Author.FirstName} {post.Author.LastName} on the {post.Blog.Title} blog.");
            }
        }

        private void Remove()
        {
            List<Post> posts = _postRepository.GetAll();
            foreach (Post post in posts)
            {
                Console.WriteLine($"Id: {post.Id} - {post.Title} by {post.Author.FirstName} {post.Author.LastName}.");
            }
            int postIdToDelete = int.Parse(Console.ReadLine());
            
            
                _postRepository.Delete(postIdToDelete);
            
        }
    }
}
