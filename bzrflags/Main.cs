using System;

namespace bzrflags
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			DummyAgent da = new DummyAgent();
			da.runAgent(0,50103);
		}
	}
}
