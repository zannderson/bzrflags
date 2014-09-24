using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bzrflags
{
	public class Flag
	{
		public FlagColor Color { get; set; }

		public Vector Location { get; set; }

		public FlagColor PosessingTeamColor { get; set; }

		public Flag Parse(string input)
		{
			if (!input.StartsWith("flag"))
			{
				return null;
			}

			string[] parts = input.Split(' ');

			double x, y;
			if (double.TryParse(parts[3], out x) && double.TryParse(parts[4], out y))
			{
				return new Flag
				{
					Color = ParseFlagColor(parts[1]),
					PosessingTeamColor = ParseFlagColor(parts[2]),
					Location = new Vector(x, y)
				};
			}

			return null;
		}

		private FlagColor ParseFlagColor(string input)
		{
			switch (input)
			{
				case "red":
					return FlagColor.Red;
				case "blue":
					return FlagColor.Blue;
				case "green":
					return FlagColor.Green;
				case "purple":
					return FlagColor.Purple;
				default:
					return FlagColor.None;
			}
		}
	}

	public enum FlagColor
	{
		Red,
		Blue,
		Purple,
		Green,
		None
	}
}
