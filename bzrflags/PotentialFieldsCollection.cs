using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class PotentialFieldsCollection
	{
		public List<PotentialField> PotentialFields { get; set; }
		
		public PotentialFieldsCollection ()
		{
		}
		
		public Vector GetCombinedVectorForPoint(double x, double y)
		{
			double xSum = 0.0;
			double ySum = 0.0;
			foreach (PotentialField field in PotentialFields)
			{
				Vector currentVector = field.GetVectorForMapPoint(x, y);
				xSum += currentVector.X;
				ySum += currentVector.Y;
			}
			
			return new Vector(xSum, ySum);
		}
	}
}

