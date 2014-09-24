using System;

namespace bzrflags
{
	public class LineSegment
	{
		public Vector V1 { get; set; }
		
		public Vector V2 { get; set; }
		
		public LineSegment (Vector v1, Vector v2)
		{
			V1 = v1;
			V2 = v2;
		}
	}
}

