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