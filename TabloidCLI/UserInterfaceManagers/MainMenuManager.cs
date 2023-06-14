using System;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI.UserInterfaceManagers
{
    public class MainMenuManager : IUserInterfaceManager
    {
        private const string CONNECTION_STRING = 
            @"Data Source=localhost\SQLEXPRESS;Database=TabloidCLI;Integrated Security=True";

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Welcome to Tabloid! You look attractive today!");
            Console.WriteLine("Main Menu");

            Console.WriteLine(" 1) Journal Management");
            Console.WriteLine(" 2) Blog Management");
            Console.WriteLine(" 3) Author Management");
            Console.WriteLine(" 4) Post Management");
            Console.WriteLine(" 5) Tag Management");
            Console.WriteLine(" 6) Search by Tag");
            Console.WriteLine(" 7) Change your background color!");
            Console.WriteLine(" 0) Exit");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": return new JournalManager(this, CONNECTION_STRING);
                case "2": return new BlogManager(this, CONNECTION_STRING);
                case "3": return new AuthorManager(this, CONNECTION_STRING);
                case "4": return new PostManager(this, CONNECTION_STRING);
                case "5": return new TagManager(this, CONNECTION_STRING);
                case "6": return new SearchManager(this, CONNECTION_STRING);
                case "7":
                    Console.WriteLine("Color Menu");
                    Console.WriteLine("1) Green");
                    Console.WriteLine("2) Blue");
                    Console.WriteLine("3) Red");
                    Console.WriteLine("4) Magenta");
                    Console.WriteLine("5) Yellow");

                    Console.WriteLine("Enter the number corresponding to color: ");
                    int input = Int32.Parse(Console.ReadLine());
                    
                    ConsoleColor backgroundColor;
                    switch (input)
                    {
                        case 1:
                            backgroundColor = ConsoleColor.DarkGreen;
                            return this;
                        case 2:
                            backgroundColor = ConsoleColor.DarkBlue;
                            return this;
                        case 3:
                            backgroundColor = ConsoleColor.DarkRed;
                            return this;
                        case 4:
                            backgroundColor = ConsoleColor.DarkMagenta;
                            return this;
                        case 5:
                            backgroundColor = ConsoleColor.DarkYellow;
                            return this;
                        default:
                            Console.WriteLine("Invalid color choice.");
                            return this;
                    }
                    Console.BackgroundColor = backgroundColor;
                    return this;
                case "0":
                    Console.WriteLine("Good bye");
                    return null;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
    }
}