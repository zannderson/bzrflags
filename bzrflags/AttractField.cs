using System;

namespace bzrflags
{
	public class AttractField : PotentialField
	{		
		private double SpreadAndRadius
		{
			get { return _radius + _spread; }
		}
		
		public AttractField(double x, double y, double radius, double strength, double spread) : base(x, y, radius, strength, spread)
		{
		}	
		
		#region IPotentialField implementation
		
		public override Vector GetVectorForMapPoint (double x, double y)
		{
			double distance = Math.Sqrt(Math.Pow(_x - x, 2.0) + Math.Pow(_y - y, 2.0));
			double angle = Math.Atan2 ((_y - y), (_x - x));
			if(distance < _radius)
			{
				return new Vector(0.0, 0.0);
			}
			else if(_radius <= distance && distance <= SpreadAndRadius)
			{
				return new Vector(_strength * (distance - _radius) * Math.Cos(angle), _strength * (distance - _radius) * Math.Sin(angle));
			}
			else if(distance > SpreadAndRadius)
			{
				return new Vector(_strength * (_spread * Math.Cos(angle)), _strength * (_spread * Math.Sin(angle)));
			}
			else
			{
				return new Vector(0.0, 0.0);
			}
		}
		
		#endregion
	}
}

