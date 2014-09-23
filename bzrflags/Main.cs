using System;

namespace bzrflags
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			TelnetConnection connection = new TelnetConnection();
			connection.Connect(41665);
			connection.ReceiveMessage();
			string response = connection.SendMessage("agent 1");
			string command;
			while(true)
			{
				Console.Out.Write("Input a command: ");
				command = Console.In.ReadLine();
				connection.SendMessage(command);
				Console.Out.WriteLine();
				Console.Out.WriteLine(connection.ReceiveMessage());
			}
			Console.Out.Write("HELLO!!");
		}
	}
}
