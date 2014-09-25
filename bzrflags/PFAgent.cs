using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class PFAgent
	{		
		private int _agentNumber;
		private bool _fieldsPlotted = false;
		
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
			
			List<Tank> myTanks = populateTanks();			
			
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
			
			Console.Out.WriteLine("Tank: " + _agentNumber + " Location: " + me._xPosition + ", " + me._yPosition);
			
			string constantString = TelnetConnection.Connection.SendMessage("constants", true);
			Constants constants = new Constants(constantString);
			string obstaclesString = TelnetConnection.Connection.SendMessage("obstacles", true);
			ObstacleCollection obstacles = new ObstacleCollection(obstaclesString);			
			List<PotentialField> fields = new List<PotentialField>();
			fields.Add(new RandomField());
			fields.AddRange(obstacles.ObstacleFields);	
			Vector myPosition = new Vector(me._xPosition, me._yPosition);
			
			if(me._hasFlag)
			{
				//get bases
				string baseString = TelnetConnection.Connection.SendMessage("bases", true);
				BaseCollection bases = new BaseCollection(baseString);
				fields.Add(bases.GetFieldForMyBase(myPosition, constants.MyColor));
				//get my base
				//set up field for base
			}
			else
			{
				string flagsString = TelnetConnection.Connection.SendMessage("flags", true);			
				FlagCollection flags = new FlagCollection(flagsString);							
				fields.Add (flags.GetFieldForNearestFlag(myPosition, constants.MyColor));
			}			
			
			PotentialFieldsCollection fieldCollection = new PotentialFieldsCollection(fields);
			Vector delta = fieldCollection.GetCombinedVectorForPoint(myPosition);
			Vector moveHere = myPosition + delta;
			
			me.moveToPosition(myPosition, moveHere);
			if(!_fieldsPlotted)
			{
				GnuPlotGenerator.PlotMyFields("fields", fieldCollection, obstacles);
				_fieldsPlotted = true;
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


