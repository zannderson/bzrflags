using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class PFAgent
	{		
		private int _agentNumber;
		
		public void runAgent()
		{
			while(true)
			{
				System.Threading.Thread.Sleep(100);
				tick();
			}
		}
		
		private void tick()
		{
			Console.Out.WriteLine("TICK!");
			bool fieldsPlotted = false;
			
			string constantString = TelnetConnection.Connection.SendMessage("constants", true);
			string otherTanksString = TelnetConnection.Connection.SendMessage("othertanks", true);
			string obstaclesString = TelnetConnection.Connection.SendMessage("obstacles", true);
			string flagsString = TelnetConnection.Connection.SendMessage("flags", true);
			string baseString = TelnetConnection.Connection.SendMessage("bases", true);
			
			Constants constants = new Constants(constantString);
			ObstacleCollection obstacles = new ObstacleCollection(obstaclesString);
			FlagCollection flags = new FlagCollection(flagsString);
			List<Tank> myTanks = populateTanks();
			BaseCollection bases = new BaseCollection(baseString);
			
			
			Tank me = null;
			foreach (Tank tank in myTanks)
			{
				if(tank._index == _agentNumber)
				{
					me = tank;
				}
			}
			if(me == null)
			{
				throw new Exception("Where am I??  I can't find myself!!");
			}
			
			Vector myPosition = new Vector(me._xPosition, me._yPosition);
			
			List<PotentialField> fields = new List<PotentialField>();
			fields.Add(new RandomField());
			fields.AddRange(obstacles.ObstacleFields);
			fields.Add (flags.GetFieldForNearestFlag(myPosition, constants.MyColor));
			
			PotentialFieldsCollection fieldCollection = new PotentialFieldsCollection(fields);
			Vector delta = fieldCollection.GetCombinedVectorForPoint(myPosition);
			Vector moveHere = myPosition + delta;
			
			me.moveToPosition(myPosition, moveHere);
			if(!fieldsPlotted)
			{
				GnuPlotGenerator.PlotMyFields("fields", fieldCollection);
				fieldsPlotted = true;
			}
		}
		
		public PFAgent (int agentNumber)
		{
			_agentNumber = agentNumber;
			return;
		}
		
		public List<Tank> populateTanks()
		{
			//query server for list of MY tank data
			
			string rawMyTankData = TelnetConnection.Connection.SendMessage("mytanks",true);
			//rawTankData = _connection.ReceiveMessage();
			//rawTankData = connection.ReceiveMessage();
			//string rawTankData = "begin\nmytank 0 blue0 alive 10 0 -                            18 335 -0.000270977392677 0.0 -0.0 0\nmytank 1 blue1 alive 10 0 -                            41 371 -0.78155669959 0.0 -0.0 0\nmytank 2 blue2 alive 10 0 -                            12 344 0.380635770818 0.0 0.0 0\nmytank 3 blue3 alive 10 0 -                            45 382 2.09007281032 -0.0 0.0 0\nmytank 4 blue4 alive 10 0 -                            29 373 -0.904861453278 0.0 -0.0 0\nmytank 5 blue5 alive 10 0 -                            9 363 0.105536563687 0.0 0.0 0\nmytank 6 blue6 alive 10 0 -                            -1 373 2.91803939018 -0.0 0.0 0\nmytank 7 blue7 alive 10 0 -                            1 358 3.07034089609 -0.0 0.0 0\nmytank 8 blue8 alive 10 0 -                            20 360 1.65203820612 -0.0 0.0 0\nmytank 9 blue9 alive 10 0 -                            -23 381 1.90299673345 -0.0 0.0 0\nend";
			List<Tank> myTanks = Tank.createMyTanks(rawMyTankData);
			
			string rawEnemyTankData = TelnetConnection.Connection.SendMessage("othertanks",true);
			List<Tank> otherTanks = Tank.createEnemyTanks(rawEnemyTankData);
			
			
				
			return myTanks;		
				
		}
			
		}
		
	}


