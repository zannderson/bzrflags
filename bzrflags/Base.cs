using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class Base
	{
		public List<Vector> _Vertices { get; set; }
		Vector _center {get;set;}
		FlagColor _baseColor;
		
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
			

				
			Vector p1 = new Vector(double.Parse(parts[2]),double.Parse(parts[3]));
			Vector p2 = new Vector(double.Parse(parts[4]),double.Parse(parts[5]));
			Vector p3 = new Vector(double.Parse(parts[6]),double.Parse(parts[7]));
			Vector p4 = new Vector(double.Parse(parts[8]),double.Parse(parts[9]));
			
			double xCenter = (p1.X + p3.X)/2;
			double yCenter = (p1.Y + p3.Y)/2;
			
			newBase._center = new Vector(xCenter,yCenter);
			newBase._Vertices.Add(p1);
			newBase._Vertices.Add(p2);
			newBase._Vertices.Add(p3);
			newBase._Vertices.Add(p4);
				

			
			
			return newBase;
		}
	}
}

