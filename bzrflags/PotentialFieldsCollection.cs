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
		
		public Vector GetCombinedVectorForPoint(Vector point)
		{
			Vector sum = new Vector(0.0, 0.0);
			foreach (PotentialField field in Fields)
			{
				Vector currentVector = field.GetVectorForMapPoint(point.X, point.Y);
				sum += currentVector;
			}
			
			return sum;
		}
	}
}

