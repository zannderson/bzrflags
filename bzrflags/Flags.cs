using System;
using System.Collections;
using System.Collections.Generic;

namespace bzrflags
{
	public class Flags
	{
		public static List<PotentialField> GetFlagFields(string flags)
		{
			List<PotentialField> flagFields = new List<PotentialField>();
			
			string[] lines = flags.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				if(lines[i].StartsWith("flag"))
				{
					string[] parts = lines[i].Split(' ');
					double x, y;
					if(double.TryParse (parts[3], out x) && double.TryParse(parts[4], out y))
					{
						flagFields.Add(new AttractField(x, y, 0.5, 0.5, 10));
					}
				}
			}
			
			return flagFields;
		}
	}
}

