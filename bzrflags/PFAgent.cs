using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class PFAgent
	{
		private static TelnetConnection connection;
		public List<Tank> myTanks { get; set; }
		
		public PFAgent (int socketNumber, int agentNumber)
		{
			connection = new TelnetConnection(socketNumber);
            connection.ReceiveMessage();
            string response = connection.SendMessage("agent " + agentNumber);
			myTanks = new List<Tank>();
		}
		
		public void updateTanks()
		{
			//query server for list of MY tank data
			//string rawTankData = connection.SendMessage("mytanks");
			//rawTankData = connection.ReceiveMessage();
			
			string rawTankData = "begin\nmytank 0 blue0 alive 10 0 -                            18 335 -0.000270977392677 0.0 -0.0 0\nmytank 1 blue1 alive 10 0 -                            41 371 -0.78155669959 0.0 -0.0 0\nmytank 2 blue2 alive 10 0 -                            12 344 0.380635770818 0.0 0.0 0\nmytank 3 blue3 alive 10 0 -                            45 382 2.09007281032 -0.0 0.0 0\nmytank 4 blue4 alive 10 0 -                            29 373 -0.904861453278 0.0 -0.0 0\nmytank 5 blue5 alive 10 0 -                            9 363 0.105536563687 0.0 0.0 0\nmytank 6 blue6 alive 10 0 -                            -1 373 2.91803939018 -0.0 0.0 0\nmytank 7 blue7 alive 10 0 -                            1 358 3.07034089609 -0.0 0.0 0\nmytank 8 blue8 alive 10 0 -                            20 360 1.65203820612 -0.0 0.0 0\nmytank 9 blue9 alive 10 0 -                            -23 381 1.90299673345 -0.0 0.0 0\nend";
			string[] splitTankData = rawTankData.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
			
			foreach(string individualTankData in splitTankData)
			{
				if(individualTankData == "begin" || individualTankData == "end")
					continue;
				
				
				
			}
			
			//populate my internal tank data structures
			foreach(Tank tank in myTanks)
			{
				
			}
		}
		
	}
}

