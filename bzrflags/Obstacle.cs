using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class Obstacle
	{
		public List<Vector> Vertices { get; set; }
		
		public List<LineSegment> Lines { get; set; }
		
		private Obstacle()
		{
			Vertices = new List<Vector>();
			Lines = new List<LineSegment>();
		}
		
		public Obstacle Parse(string input)
		{
			Obstacle newObstacle = new Obstacle();
			
			if(!input.StartsWith("obstacle"))
			{
				return null;
			}
			
			string[] parts = input.Split(' ');
			
			for (int i = 1; i < parts.Length; i += 2)
			{
				double x, y;
				if(double.TryParse(parts[i], out x) && double.TryParse(parts[i + 1], out y))
				{
					Vector newVertex = new Vector(x, y);
					newObstacle.Vertices.Add(newVertex);
				}
			}
			
			for (int i = 0; i < Vertices.Count; i++)
			{
				int next = i == Vertices.Count - 1 ? 0 : i + 1;
				LineSegment segment = new LineSegment(Vertices[i], Vertices[next]);
				newObstacle.Lines.Add(segment);
			}
			
			return newObstacle;
		}
	}
}

