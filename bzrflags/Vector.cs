using System;

namespace bzrflags
{
	public class Vector
	{
		public double X { get; set; }
		public double Y { get; set; }
		public Vector (double x, double y)
		{
			X = x;
			Y = y;
		}
		
		public static Vector operator +(Vector v1, Vector v2)
		{
			return new Vector(v1.X + v2.X, v1.Y + v2.Y);
		}
		
		public static double FindDistance(Vector v1, Vector v2)
		{			
			return Math.Sqrt(Math.Pow(v1.X - v2.X, 2.0) + Math.Pow(v1.Y - v2.Y, 2.0));
		}
	}
}

