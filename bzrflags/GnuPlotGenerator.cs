using System;
using System.Text;
using System.IO;

namespace bzrflags
{
	public class GnuPlotGenerator
	{
		public GnuPlotGenerator ()
		{
			
		}
		
		public static void PlotMyFields(string filename, PotentialFieldsCollection fields)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine ("set xrange [-400.0: 400.0]");
			sb.AppendLine ("set yrange [-400.0: 400.0]");
			sb.AppendLine ("unset key");
			sb.AppendLine ("set size square");
			
			sb.AppendLine("plot '-' with vectors head");
			
			double increment = 800.0 / 25.0;
			
			for (double xVal = -400.0; xVal <= 400.0; xVal += increment)
			{
				for(double yVal = -400.0; yVal <= 400.0; yVal += increment)
				{
					Vector theVector = fields.GetCombinedVectorForPoint(xVal, yVal);
					sb.AppendLine(xVal + " " + yVal + " " + theVector.X + " " + theVector.Y);
				}
			}
			
			FileStream stream = new FileStream(filename + ".gpi", FileMode.Create);
			StreamWriter writer = new StreamWriter(stream);
			writer.Write(sb.ToString());
			writer.Close();
		}
	}
}

