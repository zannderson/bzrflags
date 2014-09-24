using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class LineSegment
	{
		private const double INCREMENT = 1.0;
		
		public Vector V1 { get; set; }
		
		public Vector V2 { get; set; }
		
		public LineSegment (Vector v1, Vector v2)
		{
			V1 = v1;
			V2 = v2;
		}
		
		private double GetXStart()
		{
			return V1.X < V2.X ? V1.X : V2.X;
		}
		
		private double GetXEnd()
		{
			return V1.X < V2.X ? V2.X : V1.X;
		}
		
		private double GetYStart()
		{
			return V1.Y < V2.Y ? V1.Y : V2.Y;
		}
		
		private double GetYEnd()
		{
			return V1.Y < V2.Y ? V2.Y : V1.Y;
		}
		
		public List<PotentialField> GetSegmentFields()
		{
			List<PotentialField> fields = new List<PotentialField>();
			
			for (double xPoint = GetXStart(); xPoint <= GetXEnd(); xPoint += INCREMENT)
			{
				for (double yPoint = GetYStart(); yPoint <= GetYEnd(); yPoint += INCREMENT)
				{
					fields.Add(new TangentialField(xPoint, yPoint, 20, 1, 5));
				}
			}
			
			return fields;
		}
	}
}

