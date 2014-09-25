using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bzrflags
{
	class MainClass
	{		
		public static void Main (string[] args)
		{			
			int port = 0;
			if(args.Length > 0 && int.TryParse(args[0], out port))
			{
				TelnetConnection.setPort(port);
			}
			else
			{
				TelnetConnection.setPort(42664);
			}
			
			if(args.Length > 1)
			{
				int index1 = 0;
				int index2 = 1;
				if(args.Length == 4)
				{
					int.TryParse(args[2], out index1);
					int.TryParse(args[3], out index2);
				}
				switch(args[1])
				{
					case "pf":				
						Task[] tasks = new Task[2];
						tasks[0] = Task.Factory.StartNew(() => 
						{
							PFAgent pf = new PFAgent(index1);
							pf.runAgent();				
						});
						tasks[1] = Task.Factory.StartNew (() =>
						{
							PFAgent pf2 = new PFAgent(index2);
							pf2.runAgent();
						});
			
						Task.WaitAll(tasks);
					break;
					case "dumb":				
						Task[] tasks2 = new Task[2];
						tasks2[0] = Task.Factory.StartNew(() => 
						{
							DummyAgent dummy = new DummyAgent();
							dummy.runAgent(index1);			
						});
						tasks2[1] = Task.Factory.StartNew (() =>
						{
							DummyAgent dummy2 = new DummyAgent();
							dummy2.runAgent(index2);
						});
			
						Task.WaitAll(tasks2);
					break;
				}
			}
			else
			{
				
			}
			
//			Task[] tasks3 = new Task[2];
//						tasks3[0] = Task.Factory.StartNew(() => 
//						{
							PFAgent pf1 = new PFAgent(0);
							pf1.runAgent();				
//						});
//						tasks3[1] = Task.Factory.StartNew (() =>
//						{
//							PFAgent pf2 = new PFAgent(1);
//							pf2.runAgent();
//						});
//			
//						Task.WaitAll(tasks3);
			
//			AttractField attract = new AttractField(0.0, 370.0, 1.0, 0.1, 400);
//			string obstacleString = TelnetConnection.Connection.SendMessage("obstacles", true);
//			ObstacleCollection obstacles = new ObstacleCollection(obstacleString);
//			PotentialFieldsCollection fields = new PotentialFieldsCollection(obstacles.ObstacleFields);
//			fields.Fields.Add(attract);
//			GnuPlotGenerator.PlotMyFields("new_tangential", fields, obstacles);
		}
	}
}
