using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace bzrflags
{
	class MainClass
	{
		private static int _index;
		
		public static void Main (string[] args)
		{			
			int port = 0;
			if(args.Length > 0 && int.TryParse(args[0], out port))
			{
				TelnetConnection.setPort(port);
			}
			else
			{
				TelnetConnection.setPort(50103);
			}
			
			_index = 0;
			Task[] tasks = new Task[10];
			for(int i = 0; i < 10; i++ )
			{
				tasks[i] = Task.Factory.StartNew(() =>
				{
					PFAgent pf = new PFAgent(_index);
					pf.runAgent();
					_index++;
				});
			}
			
			Task.WaitAll(tasks);
		}
	}
}
