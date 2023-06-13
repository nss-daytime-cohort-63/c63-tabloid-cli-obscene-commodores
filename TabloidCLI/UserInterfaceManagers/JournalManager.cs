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
            //Console.WriteLine(" 3) Add Author");
            //Console.WriteLine(" 4) Edit Author");
            //Console.WriteLine(" 5) Remove Author");
            //Console.WriteLine(" 0) Go Back");

            Console.WriteLine("Choose your Journal: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    List();
                    return this;
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
                  Console.WriteLine(journal.Title);
                }
            }
    }
}
