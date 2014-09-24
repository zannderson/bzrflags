using System;

namespace bzrflags
{
	public class Constants
	{
		public double TankRadius { get; set; }
		
		public FlagColor MyColor { get; set; }
		
		public double FlagRadius { get; set; }
		
		public Constants (string input)
		{
			string[] lines = input.Split('\n');
			if(lines.Length < 1)
			{
				return;
			}
			
			for (int i = 0; i < lines.Length; i++)
			{
				string[] parts = lines[i].Split(' ');
				
				if(parts.Length > 2 && parts[1] == "tankradius")
				{
					double radius = 0.5;
					double.TryParse(parts[2], out radius);
					TankRadius = radius;
				}
				
				if(parts.Length > 2 && parts[1] == "team")
				{
					MyColor = Flag.ParseFlagColor(parts[2]);
				}
				
				if(parts.Length > 2 && parts[1] == "flagradius")
				{
					double radius = 0.5;
					double.TryParse(parts[2], out radius);
					FlagRadius = radius;
				}
			}
		}
	}
}

