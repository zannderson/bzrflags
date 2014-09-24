using System;
using System.Collections;
using System.Collections.Generic;

namespace bzrflags
{
	public class ObstacleCollection
	{
		public static List<PotentialField> GetFieldsForObstacles(string obstacles)
		{
			List<PotentialField> fields = new List<PotentialField>();
			string[] lines = obstacles.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				if(lines[i].StartsWith("obstacle"))
				{
					string[] parts = lines[i].Split(' ');
					Vector startPoint = null;
					Vector endPoint = null;
					for(int j = 1; j < parts.Length; j += 2)
					{
						double x, y;
						if(double.TryParse (parts[j], out x) && double.TryParse(parts[j+1], out y))
						{
							if(startPoint == null)
							{
								startPoint = new Vector(x, y);
							}
							else if(endPoint == null)
							{
								endPoint = new Vector(x, y);
							}
							else
							{
								double xIncrement = 0.5;
								double xStart = startPoint.X;
								double xEnd = endPoint.X;
								if(startPoint.X > endPoint.X)
								{
									xStart = endPoint.X;
									xEnd = startPoint.X;
								}
								double yIncrement = 0.5;
								double yStart = startPoint.Y;
								double yEnd = endPoint.Y;
								if(startPoint.Y > endPoint.Y)
								{
									yStart = endPoint.Y;
									yEnd = startPoint.Y;
								}
								for(double xPoint = xStart; xPoint <= xEnd; xPoint += xIncrement)
								{									
									for(double yPoint = yStart; yPoint <= yEnd; yPoint += yIncrement)
									{
										fields.Add(new TangentialField(xPoint, yPoint, 20, 0.75, 5));
									}
								}
								startPoint = null;
								endPoint = null;
							}
						}
					}
				}
			}
			return fields;
		}
	}
}

