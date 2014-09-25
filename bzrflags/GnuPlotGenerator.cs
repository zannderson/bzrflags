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
		
		public static void PlotMyFields(string filename, PotentialFieldsCollection fields, ObstacleCollection obstacles)
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine ("set xrange [-400.0: 400.0]");
			sb.AppendLine ("set yrange [-400.0: 400.0]");
			sb.AppendLine ("unset key");
			sb.AppendLine ("set size square");
			
			if(obstacles != null)
			{
				sb.AppendLine("unset arrow");
				foreach (Obstacle obstacle in obstacles.Obstacles)
				{
					foreach (var segment in obstacle.Lines)
					{
						sb.Append ("set arrow from ");
						sb.Append(segment.V1.X);
						sb.Append(", ");
						sb.Append(segment.V1.Y);
						sb.Append(" to ");
						sb.Append(segment.V2.X);
						sb.Append(", ");
						sb.Append(segment.V2.Y);
						sb.Append(" nohead lt 3");
						sb.AppendLine();
					}				
				}
			}
			
			sb.AppendLine("plot '-' with vectors head");
			
			double increment = 800.0 / 30.0;
			
			for (double xVal = -400.0; xVal <= 400.0; xVal += increment)
			{
				for(double yVal = -400.0; yVal <= 400.0; yVal += increment)
				{
					Vector theVector = fields.GetCombinedVectorForPoint(new Vector(xVal, yVal));
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

