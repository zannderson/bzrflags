using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class PotentialFieldsCollection
	{
		public List<PotentialField> Fields { get; set; }
		
		public PotentialFieldsCollection (List<PotentialField> fields)
		{
			Fields = fields;
		}
		
		public Vector GetCombinedVectorForPoint(double x, double y)
		{
			Vector sum = new Vector(0.0, 0.0);
			foreach (PotentialField field in Fields)
			{
				Vector currentVector = field.GetVectorForMapPoint(x, y);
				sum += currentVector;
			}
			
			return sum;
		}
	}
}

