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
                connection.ReceiveMessage();
                string response = connection.SendMessage("agent 1");

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
				
                        connection.SendMessage("speed " + agentNumber +" 1");
                        Console.Out.WriteLine(connection.ReceiveMessage());
						System.Threading.Thread.Sleep(timeToWait);
                        connection.SendMessage("speed " + agentNumber +" 0");
                        Console.Out.WriteLine(connection.ReceiveMessage());

                        connection.SendMessage("angvel " + agentNumber +" 1");
                        Console.Out.WriteLine(connection.ReceiveMessage());
                        System.Threading.Thread.Sleep(2000);
                        connection.SendMessage("angvel " + agentNumber +" 0");
                        Console.Out.WriteLine(connection.ReceiveMessage());
				
						//System.Threading.Thread.Sleep(timeToShoot);
						connection.SendMessage("shoot " + agentNumber);
						Console.Out.WriteLine(connection.ReceiveMessage());
                }
        }

}
}