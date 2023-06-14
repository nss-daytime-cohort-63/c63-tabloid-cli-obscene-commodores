using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {

        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {

            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journals");
            //Console.WriteLine(" 2) Author Details");
            Console.WriteLine(" 3) Add Journal");
            
            //Console.WriteLine(" 4) Edit Author");
            Console.WriteLine(" 5) Remove Journal");
            Console.WriteLine(" 0) Go Back");

            Console.WriteLine("Choose your Journal: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    List();
                    return this;
                case "3":
                    Add();
                    return this;
                case "5":
                    Delete();
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
                List<Journal> journals = _journalRepository.GetAll();
                foreach (Journal journal in journals) 
                {
                  Console.WriteLine($"{journal.Id}: {journal.Title}");
                }
            }

        private void Add()
        {
            Journal addJournal = new Journal();

            Console.WriteLine("What is the New Journal's Title?");

            addJournal.Title = Console.ReadLine();

            Console.WriteLine("Fill the content of the journal");
            addJournal.Content = Console.ReadLine();
            Console.WriteLine("now adding current date");
            addJournal.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(addJournal);
        }



        private void Delete()
        {
            List();
            Console.WriteLine("please select a Journal to remove.");
            int selectedJournal = int.Parse(Console.ReadLine());

            _journalRepository.Delete(selectedJournal);
        }

        private void Edit()
        {
            List();
            Console.WriteLine();
        }
    }
}
