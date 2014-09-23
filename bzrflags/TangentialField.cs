using System;

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

