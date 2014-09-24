using System;
using System.Collections;
using System.Collections.Generic;

namespace bzrflags
{
	public class ObstacleCollection
	{
		public List<Obstacle> Obstacles { get; set; }
		
		public List<PotentialField> ObstacleFields { get; set; }
		
		public ObstacleCollection(string obstacleList)
		{
			Obstacles = new List<Obstacle>();
			ObstacleFields = new List<PotentialField>();
			
			string[] lines = obstacleList.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				if(lines[i].StartsWith("obstacle"))
				{
					Obstacle o = Obstacle.Parse(lines[i]);
					if(o != null)
					{
						Obstacles.Add(o);
						ObstacleFields.AddRange(o.GetObstacleFields());
					}
				}
			}
		}
	}
}

