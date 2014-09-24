using System;
using System.Collections;
using System.Collections.Generic;

namespace bzrflags
{
	public class FlagCollection
	{
		public List<Flag> Flags { get; set; }
		
		public Flag GetNearestFlag(Vector v)
		{
			double greatestDistance = 0.0;
			Flag nearest = null;
			foreach (Flag flag in Flags)
			{
				double distance = Vector.FindDistance(v, flag.Location);
				if(distance > greatestDistance)
				{
					nearest = flag;
					greatestDistance = distance;
				}
			}
			return nearest;
		}
		
		public PotentialField GetFieldForNearestFlag(Vector v)
		{
			return GetNearestFlag(v).GetFlagField();
		}
		
		public FlagCollection(string flagDescription)
		{
			Flags = new List<Flag>();
			
			string[] lines = flagDescription.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				if(lines[i].StartsWith("flag"))
				{
					Flags.Add (Flag.Parse(lines[i]));
				}
			}
		}
	}
}

