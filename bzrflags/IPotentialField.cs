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
		
		public PotentialField(double x, double y)
		{
			_x = x;
			_y = y;
			_radius = 0.5;
			_spread = 300.0;
			_strength = 1.0;
		}
		
		public virtual Vector GetVectorForMapPoint(float x, float y)
		{
			return new Vector(0.0, 0.0);
		}
	}
}

