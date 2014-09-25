using System;
using System.Collections;
using System.Collections.Generic;

namespace bzrflags
{
	public class FlagCollection
	{
		public List<Flag> Flags { get; set; }
		
		public Flag GetNearestFlag(Vector v, FlagColor myColor)
		{
			double shortestDistance = double.MaxValue;
			Flag nearest = null;
			foreach (Flag flag in Flags)
			{
				double distance = Vector.FindDistance(v, flag.Location);
				if(distance < shortestDistance && flag.Color != myColor)
				{
					nearest = flag;
					shortestDistance = distance;
				}
			}
			return nearest;
		}
		
		public PotentialField GetFieldForNearestFlag(Vector v, FlagColor myColor)
		{
			return GetNearestFlag(v, myColor).GetFlagField();
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

