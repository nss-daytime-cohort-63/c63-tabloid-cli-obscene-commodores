using System;
using System.Collections.Generic;
using TabloidCLI.Models;


namespace TabloidCLI.UserInrefaceManagers
{
	public class ColorManager : IUserInterfaceManager
	{
		private readonly IUserInterfaceManager _parentUI;
		private ColorRepository _colorRepository;
		private string _connectionString;

		public ColorManager(IUserInterfaceManager parentUI, string connectionString)
		{
			_parentUI = parentUI;
			_colorRepository = new ColorRepository(connectionString);
			_connectionString = connectionString;
		}

		public IUserInterfaceManager Execute()
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
					Console.BackgroundColor = ConsoleColor.Blue;
					return this;
				case "2":
					Add();
					return this;
				case "3":
					Add();
					return this;
				case "4":
					Edit();
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
	}
}