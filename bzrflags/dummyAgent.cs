using System;
using System.Timers;

class dummyAgent
{

    private static TelnetConnection connection;
    private static Timer shootTimer;

        public void runAgent()
        {
                connection = new TelnetConnection();
                connection.Connect(41665);
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
                        int timeToWait = rand.Next(3,8);
                        int timeToShoot = rand.Next(1500,2500);
                        shootTimer = new System.Timers.Timer(timeToShoot);
                        shootTimer.Elapsed += shootTank;
                        shootTimer.Enabled = true;

                        connection.SendMessage("speed 0 1");
                        Console.Out.WriteLine(connection.ReceiveMessage());
                        WaitForSeconds(timeToWait);
                        connection.SendMessage("speed 0 0");
                        Console.Out.WriteLine(connection.ReceiveMessage());

                        connection.SendMessage("angvel 0 1");
                        Console.Out.WriteLine(connection.ReceiveMessage());
                        WaitForSeconds(1);
                        connection.SendMessage("angvel 0 0");
                        Console.Out.WriteLine(connection.ReceiveMessage());
                }
        }

        
	private static void shootTank(Object source, ElapsedEventArgs e)
    {
        connection.SendMessage("shoot 0 1");
		Console.Out.WriteLine(connection.ReceiveMessage());
        Console.WriteLine("The shoot event was raised at {0}", e.SignalTime);
    }


}