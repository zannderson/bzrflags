using System;
using System.Threading;

namespace bzrflags
{

class DummyAgent
{

    private static TelnetConnection connection;

        public void runAgent(int agentNumber, int socketNumber)
        {
                connection = new TelnetConnection(socketNumber);
                string response = connection.SendMessage("agent 1", false);

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
				
                        Console.Out.WriteLine (connection.SendMessage("speed " + agentNumber +" 1", true));
						System.Threading.Thread.Sleep(timeToWait);
                        Console.Out.WriteLine (connection.SendMessage("speed " + agentNumber +" 0", true));

                        Console.Out.WriteLine (connection.SendMessage("angvel " + agentNumber +" 1", true));
                        System.Threading.Thread.Sleep(2000);
                        Console.Out.WriteLine (connection.SendMessage("angvel " + agentNumber +" 0", true));
				
						//System.Threading.Thread.Sleep(timeToShoot);
						Console.Out.WriteLine (connection.SendMessage("shoot " + agentNumber, true));
                }
        }

}
}
