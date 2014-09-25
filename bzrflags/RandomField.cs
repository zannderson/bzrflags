using System;

namespace bzrflags
{
	public class RandomField : PotentialField
	{		
		private static Random _theRandom;
		public static Random TheRandom
		{
			get
			{
				if(_theRandom == null)
				{
					_theRandom = new Random();
				}
				return _theRandom;
			}
		}
		
		public RandomField() : base(0.0, 0.0, 0.0, 1, 0.0)
		{
		}
		
		public RandomField(double x, double y, double radius, double strength, double spread) : base(x, y, radius, strength, spread)
		{
		}
		
		public override Vector GetVectorForMapPoint (double x, double y)
		{			
			double xMagnitude = TheRandom.NextDouble();
			double xDirection = TheRandom.NextDouble();
			double yMagnitude = TheRandom.NextDouble();
			double yDirection = TheRandom.NextDouble();
			
			double xDisplacement = xMagnitude * _strength;
			double vectorX = xDirection > 0.5 ? xDisplacement : -1 * xDisplacement;
			double yDisplacement = yMagnitude * _strength;
			double vectorY = yDirection > 0.5 ? yDisplacement : -1 * yDisplacement;
			
			return new Vector(vectorX, vectorY);
		}
	}
}

