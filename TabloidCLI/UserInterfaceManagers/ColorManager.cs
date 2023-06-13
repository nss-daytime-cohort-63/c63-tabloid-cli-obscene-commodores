using System;
using System.Collections.Generic;
using TabloidCLI.Models;


namespace TabloidCLI.UserInterfaceManagers
{
	public class ColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private string ConnectionString;
	{

        public ColorManager()
		{
				
		}
        public string SelectedColor() 
		{
			Console.WriteLine("Color Menu");
			Console.WriteLine("1) Black");
			Console.WriteLine("2) Blue");
			Console.WriteLine("3) Cyan");
			Console.WriteLine("4) Dark Blue");
			Console.WriteLine("5) Dark Cyan");
			Console.WriteLine("6) Dark Gray");
			Console.WriteLine("7) Dark Green");
			Console.WriteLine("8) Dark Magenta");
			Console.WriteLine("9) Dark Red");
			Console.WriteLine("10) Dark Yellow");
			Console.WriteLine("11) Gray");
			Console.WriteLine("12) Green");
			Console.WriteLine("13) Magenta");
			Console.WriteLine("14) Red");
			Console.WriteLine("15) White");
			Console.WriteLine("16) Yellow");

			Console.Write("> ");
			string choice = Console.ReadLine();
			switch (choice)
			{
				case "1":
					/*newColor.BackgroundColor = Blue;*/
					Console.BackgroundColor = ConsoleColor.Blue;
					string choice2 = "";
                    return choice2;
				/*case "2":
					Console.BackgroundColor = BackgroundColor.Blue
					return this;
				case "3":
					return this;
				case "4":
					return this;
				case "5":
					return this;*/
				case "0":
					return _parentUI;
				default:
					string invalid = "Invalid Selection";
					return invalid;
			}
		}

        public IUserInterfaceManager Execute()
        {
            throw new NotImplementedException();
        }
    }
}