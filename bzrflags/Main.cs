using System;

namespace bzrflags
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			TelnetConnection connection = new TelnetConnection();
			connection.Connect(50221);
			string response = connection.SendMessage("agent 1");
			Console.Out.Write("HELLO!!");
		}
	}
}
