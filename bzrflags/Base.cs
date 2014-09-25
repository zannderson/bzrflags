using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class Base
	{
		public List<Vector> _Vertices { get; set; }
		public Vector Center {get;set;}
		public FlagColor Color { get; set; }
		
		public Base ()
		{
			_Vertices = new List<Vector>();	
		}
		
		public static Base Parse(string rawBaseData)
		{
			Base newBase = new Base();
			
			if(!rawBaseData.StartsWith("base") || rawBaseData == "bases")
			{
				return null;
			}
			
			string[] parts = rawBaseData.Split(' ');
			
			newBase.Color = Flag.ParseFlagColor(parts[1]);
				
			Vector p1 = new Vector(double.Parse(parts[2]),double.Parse(parts[3]));
			Vector p2 = new Vector(double.Parse(parts[4]),double.Parse(parts[5]));
			Vector p3 = new Vector(double.Parse(parts[6]),double.Parse(parts[7]));
			Vector p4 = new Vector(double.Parse(parts[8]),double.Parse(parts[9]));
			
			double xCenter = (p1.X + p3.X)/2;
			double yCenter = (p1.Y + p3.Y)/2;
			
			newBase.Center = new Vector(xCenter,yCenter);
			newBase._Vertices.Add(p1);
			newBase._Vertices.Add(p2);
			newBase._Vertices.Add(p3);
			newBase._Vertices.Add(p4);
				

			
			
			return newBase;
		}
		
		public PotentialField GetBaseField()
		{
			return new AttractField(Center.X, Center.Y, 1.0, 0.1, 400);
		}
	}
}

