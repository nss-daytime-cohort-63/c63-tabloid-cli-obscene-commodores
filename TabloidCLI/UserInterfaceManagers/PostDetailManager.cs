using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private Post _post;
        private string _connectionString;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, Post chosenPost)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _post = chosenPost;
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _post;
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add tag");
            Console.WriteLine(" 3) Remove tag");
            Console.WriteLine(" 4) Manage Notes");

            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;
                case "2":
                    //AddTag();
                    return this;
                case "3":
                    //RemoveTag();
                    return this;
                case "4":
                    return new NoteManager(this, _connectionString);
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void View()
        {
            Post post = _post;
            Console.WriteLine($"Title: {post.Title}");
            Console.WriteLine($"Url: {post.Url}");
            Console.WriteLine($"Author: {post.Author.FullName}");
            Console.WriteLine($"Blog: {post.Blog.Title}");
            Console.Write("Tags:  ");
            if (post.Tags.Count == 0)
            {
                Console.Write("None");
                Console.WriteLine();
            }
            else
            {
                foreach (Tag tag in post.Tags)
                {
                    Console.Write($"{tag}  ");
                    Console.WriteLine();
                }
            }
        }
    }
}