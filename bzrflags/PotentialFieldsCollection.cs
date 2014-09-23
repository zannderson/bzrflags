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
			Vector sum = new Vector(0.0, 0.0);
			foreach (PotentialField field in PotentialFields) {
				
			}
			
			return new Vector(0.0, 0.0);
		}
	}
}

