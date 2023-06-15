using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;


namespace TabloidCLI.UserInterfaceManagers
{
    public class NoteManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private NoteRepository _noteRepository;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private string _connectionString;
        private Post _post;

        public NoteManager (IUserInterfaceManager parentUI, string connectionString, Post chosenPost)
        {
            _parentUI = parentUI;
            _noteRepository = new NoteRepository (connectionString);
            _postRepository = new PostRepository (connectionString);
            _authorRepository = new AuthorRepository (connectionString);
            _connectionString = connectionString;
            _post = chosenPost;
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
                    ListNotes();
                    return this;
                case 2:
                    AddNote();
                    return this;
                case 3:
                    DeleteNote();
                    return this;
                case 0:
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid selection");
                    return this;
            }
        }
        public void AddNote()
        {
            Console.Write("Input title of new note : ");
            string newTitle = Console.ReadLine();
            while (string.IsNullOrEmpty(newTitle))
            {
                Console.Write("Input title of new note : ");
                newTitle = Console.ReadLine();
            }
            Console.Write("Input the content of your new note : ");
            string newContent = Console.ReadLine();
            while (string.IsNullOrEmpty(newContent))
            {
                Console.Write("Input the content of your new note : ");
                newContent = Console.ReadLine();
            }
            DateTime newDate = DateTime.Now;
            Note newNote = new Note()
            {
                Title = newTitle,
                Content = newContent,
                CreateDateTime = newDate,
                PostId = _post.Id
            };
            _noteRepository.Insert(newNote);
        }
        public void ListNotes()
        {
            List<Note> listedNotes = _noteRepository.GetAll();
            foreach (Note n in listedNotes)
            {
                Console.WriteLine(@$"Title: {n.Title}, Time: {n.CreateDateTime}
Content: {n.Content}");
            }
        }
        public void DeleteNote()
        {
            List<Note> notes = _noteRepository.GetAll();
            foreach (Note n in notes)
            {
                Console.WriteLine(@$" {n.Id}) {n.Title}
                                    {n.Content}");
            }
            Console.Write("Which note would you like to delete? : ");
            int delNoteId = int.Parse(Console.ReadLine());
            while (!notes.Select(n => n.Id).Contains(delNoteId))
            {
                Console.WriteLine("Invalid Selection");
                Console.Write("Which note would you like to delete? : ");
                delNoteId = int.Parse(Console.ReadLine());
            }
            _noteRepository.Delete(delNoteId);
            Note foundNote = notes.FirstOrDefault(n => n.Id == delNoteId);
            Console.WriteLine($"The {foundNote.Title} note has been deleted.");
        }
    }
}
