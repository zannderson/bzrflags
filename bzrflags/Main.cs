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
			
			PFAgent pf = new PFAgent(50103,1);
			pf.updateTanks();
			
			
			/*
			List<PotentialField> fields = new List<PotentialField>();
			fields.Add(new AttractField(350.0, 350.0, 0.5, 0.5, 40));
			fields.Add(new RejectField(250.0, -100.0, 0.5, 0.5, 70));
			fields.Add(new TangentialField(-300.0, 230.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 231.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 232.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 233.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 234.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 235.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 236.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 237.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 238.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 239.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 240.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 241.0, 20, 0.5, 10));
			fields.Add(new TangentialField(-300.0, 242.0, 20, 0.5, 10));
			PotentialFieldsCollection coll = new PotentialFieldsCollection();
			coll.PotentialFields = fields;
			GnuPlotGenerator.PlotMyFields("newTest", coll);
			*/
		}
	}
}
