using System;
using System.Collections.Generic;

namespace bzrflags
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*
			TelnetConnection connection = new TelnetConnection(50103);
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
			*/
			
//			PFAgent pf = new PFAgent(50103,1);
//			pf.populateTanks();
	
			TelnetConnection connection = new TelnetConnection(51997);
			connection.SendMessage("agent 1", false);
			string obstacles = connection.SendMessage("obstacles", true);
			List<PotentialField> obstacleFields = ObstacleCollection.GetFieldsForObstacles(obstacles);
			string flags = connection.SendMessage("flags", true);
			List<PotentialField> flagFields = Flags.GetFlagFields(flags, "blue");
			PotentialFieldsCollection collection = new PotentialFieldsCollection();
			List<PotentialField> manualFields = new List<PotentialField>();
			manualFields.Add(new RandomField(0.0, 0.0, 0.0, 0.1, 0.0));
			//manualFields.Add(new AttractField(0.0, 370.0, 1.0, 0.1, 300));
			//collection.PotentialFields = manualFields;
			collection.PotentialFields = flagFields;
			collection.PotentialFields.AddRange(manualFields);
			//collection.PotentialFields.AddRange(flagFields);
			GnuPlotGenerator.PlotMyFields("fields", collection);
		}
	}
}
