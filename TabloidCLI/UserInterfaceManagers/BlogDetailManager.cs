﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TabloidCLI.Models;
//using TabloidCLI.Repositories;

//namespace TabloidCLI.UserInterfaceManagers
//{
//    internal class BlogDetailManager : IUserInterfaceManager
//    {
//        private IUserInterfaceManager _parentUI;
//        private BlogRepository _blogRepository;
//        private PostRepository _postRepository;
//        private TagRepository _tagRepository;
//        private int _blogId;
//    }
//    public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
//    {
//        _parentUI = parentUI;
//        _blogRepository = new BlogRepository(connectionString);
//        _postRepository = new PostRepository(connectionString);
//        _tagRepository = new TagRepository(connectionString);
//        _blogId = blogId;
//    }
//}
