using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class TangentialField : PotentialField
	{
		private double SpreadAndRadius
		{
			get { return _radius + _spread; }
		}
		
		public TangentialField(double x, double y, double radius, double strength, double spread) : base(x, y, radius, strength, spread)
		{
		}

		public static List<PotentialField> GetFieldsForObstacle(string obstacleDescription)
		{
			List<PotentialField> fields = new List<PotentialField>();

			if (!obstacleDescription.StartsWith("obstacle"))
			{
				return null;
			}

			string[] parts = obstacleDescription.Split(' ');
			Vector startPoint = null;
			Vector endPoint = null;
			for (int j = 1; j < parts.Length; j += 2)
			{
				double x, y;
				if (double.TryParse(parts[j], out x) && double.TryParse(parts[j + 1], out y))
				{
					if (startPoint == null)
					{
						startPoint = new Vector(x, y);
					}
					else if (endPoint == null)
					{
						endPoint = new Vector(x, y);
					}
					else
					{
						double xIncrement = 0.5;
						double xStart = startPoint.X;
						double xEnd = endPoint.X;
						if (startPoint.X > endPoint.X)
						{
							xStart = endPoint.X;
							xEnd = startPoint.X;
						}
						double yIncrement = 0.5;
						double yStart = startPoint.Y;
						double yEnd = endPoint.Y;
						if (startPoint.Y > endPoint.Y)
						{
							yStart = endPoint.Y;
							yEnd = startPoint.Y;
						}
						for (double xPoint = xStart; xPoint <= xEnd; xPoint += xIncrement)
						{
							for (double yPoint = yStart; yPoint <= yEnd; yPoint += yIncrement)
							{
								fields.Add(new TangentialField(xPoint, yPoint, 20, 0.75, 5));
							}
						}
						startPoint = null;
						endPoint = null;
					}
				}
			}

			return fields;
		}
		
		#region IPotentialField implementation
		
		public override Vector GetVectorForMapPoint (double x, double y)
		{
			/*
			 * This field is obtained by finding the magnitude and direction in the same way as for the repulsive obstacle.
				However, theta is modified before changeInX  and changeInY are defined by setting theta = theta + 90 degrees
				which causes the vector to shift from pointing away from the center of the obstacle to pointing in the direction of the
				tangent to the circle. The sign of the shift controls whether the tangential field causes a clockwise or counterclockwise spin.
			*/
			
			double distance = Math.Sqrt(Math.Pow(_x - x, 2.0) + Math.Pow(_y - y, 2.0));
			double angle = Math.Atan2 ((_y - y), (_x - x));
			angle = angle + 1.570796;
			
			if(distance < _radius)
			{
				return new Vector(0.0, 0.0);
			}
			else if(_radius <= distance && distance <= SpreadAndRadius)
			{
				return new Vector(_strength * (distance - _radius) * Math.Cos(angle), _strength * (distance - _radius) * Math.Sin(angle));
			}
			else
			{
				return new Vector(0.0, 0.0);
			}

		}
		
		#endregion
	}
}

