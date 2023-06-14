using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    internal class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private PostRepository _postRepository;
        private string _connectionString;

        public NoteManager (IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository (connectionString);
            _postRepository = new PostRepository (connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine(" 1) List Notes");
            Console.WriteLine(" 2) Add Note");
            Console.WriteLine(" 3) Remove Note");
            Console.WriteLine(" 0) Return");
            Console.Write("> ");
            int selection = int.Parse(Console.ReadLine());
            switch (selection)
            {
                case 1:
                    Console.WriteLine("List notes here");
                    return this;
                case 2:
                    Console.WriteLine("Add a note goes here");
                    return this;
                case 3:
                    Console.WriteLine("Remove a note goes here");
                    return this;
                case 0:
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid selection");
                    return this;
            }
        }
    }
}
