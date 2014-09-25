using System;
using System.Threading;

namespace bzrflags
{

class DummyAgent
{

        public void runAgent(int agentNumber)
        {
				
				//new TelnetConnection(socketNumber);
                string response = TelnetConnection.Connection.SendMessage("agent 1", false);

                Random rand = new Random();

                bool runForever=true;
                while(runForever)
                {
                        //This agent should repeat the following forever:
                        //1) Move forward for 3-8 seconds
                        //2) Turn left about 60 degrees and then start going straight again
                        //3 Also shoot every 2 seconds (random between 1.5 and 2.5 seconds) or so.
                        int timeToWait = rand.Next(3000,8000);
						int timeToShoot = rand.Next(1500,2000);	
				
                        Console.Out.WriteLine (TelnetConnection.Connection.SendMessage("speed " + agentNumber +" 1", true));
						System.Threading.Thread.Sleep(timeToWait);
                        Console.Out.WriteLine (TelnetConnection.Connection.SendMessage("speed " + agentNumber +" 0", true));

                        Console.Out.WriteLine (TelnetConnection.Connection.SendMessage("angvel " + agentNumber +" 1", true));
                        System.Threading.Thread.Sleep(2000);
                        Console.Out.WriteLine (TelnetConnection.Connection.SendMessage("angvel " + agentNumber +" 0", true));
				
						//System.Threading.Thread.Sleep(timeToShoot);
						Console.Out.WriteLine (TelnetConnection.Connection.SendMessage("shoot " + agentNumber, true));
                }
        }

}
}
