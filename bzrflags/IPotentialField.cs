using System;

namespace bzrflags
{
	public class PotentialField
	{		
		protected double _x;
		protected double _y;
		protected double _radius;
		protected double _spread;
		protected double _strength;
		
		public PotentialField(double x, double y, double radius, double strength, double spread)
		{
			_x = x;
			_y = y;
			_radius = radius;
			_strength = strength;
			_spread = spread;
		}
		
		public virtual Vector GetVectorForMapPoint(double x, double y)
		{
			return new Vector(0.0, 0.0);
		}
	}
}

