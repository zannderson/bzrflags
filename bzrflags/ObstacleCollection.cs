using System;
using System.Collections;
using System.Collections.Generic;

namespace bzrflags
{
	public class ObstacleCollection
	{
		public static List<PotentialField> GetFieldsForObstacles(string obstacles)
		{
			List<PotentialField> fields = new List<PotentialField>();
			string[] lines = obstacles.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				if(lines[i].StartsWith("obstacle"))
				{
					List<PotentialField> currentLineFields = TangentialField.GetFieldsForObstacle(lines[i]);
					fields.AddRange(currentLineFields);
				}
			}
			return fields;
		}
	}
}

