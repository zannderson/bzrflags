using System;

namespace bzrflags
{
	public class AttractField : PotentialField
	{		
		private double SpreadAndRadius
		{
			get { return _radius + _spread; }
		}
		
		public AttractField (double x, double y) : base(x, y)
		{
			_radius = 0.5;
			_spread = 300.0;
			_spread = 1.0;
		}		
		
		#region IPotentialField implementation
		
		public Vector GetVectorForMapPoint (double x, double y)
		{
			double distance = Math.Sqrt(Math.Pow(_x - x, 2.0) + Math.Pow(_y - y, 2.0));
			double angle = Math.Atan2 ((_y - y), (_x - x));
			if(distance > _radius)
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

