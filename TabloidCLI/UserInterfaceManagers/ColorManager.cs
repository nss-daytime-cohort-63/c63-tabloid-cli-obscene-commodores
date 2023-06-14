using System;

namespace TabloidCLI.UserInterfaceManagers
{
    public class ColorManager : IUserInterfaceManager
    {
        private MainMenuManager mainMenuManager;
        private string cONNECTION_STRING;
        private readonly IUserInterfaceManager _parentUI;

        public ColorManager(IUserInterfaceManager parentUI, string cONNECTION_STRING)
        {
            _parentUI = parentUI;
            /* this.mainMenuManager = mainMenuManager;*/
            this.cONNECTION_STRING = cONNECTION_STRING;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Color Menu");
            Console.WriteLine("1) Green");
            Console.WriteLine("2) Blue");
            Console.WriteLine("3) Red");
            Console.WriteLine("4) Magenta");
            Console.WriteLine("5) Yellow");
            Console.WriteLine("0) Return to menu");

            Console.WriteLine("Enter the number corresponding to color: ");
            int input = Int32.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    return this;
                case 2:
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    return this;
                case 3:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    return this;
                case 4:
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    return this;
                case 5:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    return this;
                case 0:
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid color choice.");
                    return this;
            }
        }
    }
}