using System;
using System.Collections.Generic;

namespace bzrflags
{
	public class BaseCollection
	{
		public List<Base> Bases { get; set; }
		
		public BaseCollection (string baseList)
		{
			Bases = new List<Base>();
			
			//string[] lines = baseList.Split('\n');
			//baseList = "bases\nack 7.45767807961 bases\nbegin\nbase blue 30.0 400.0 30.0 340.0 -30.0 340.0 -30.0 400.0\nbase purple 30.0 -340.0 30.0 -400.0 -30.0 -400.0 -30.0 -340.0\nbase green 400.0 30.0 400.0 -30.0 340.0 -30.0 340.0 30.0\nbase red -340.0 30.0 -340.0 -30.0 -400.0 -30.0 -400.0 30.0\nend\n";	
			string[] lines = baseList.Split('\n');
			for (int i = 0; i < lines.Length; i++)
			{
				if(lines[i].StartsWith("base"))
				{
					Base b = Base.Parse(lines[i]);
					if(b != null)
					{
						Bases.Add(b);
					}
				}
			}
		}
		
		public Base GetMyBase(Vector v, FlagColor myColor)
		{
			double shortestDistance = double.MaxValue;
			Base myBase = null;
			foreach (Base theBase in Bases)
			{
				if(theBase.Color == myColor)
				{
					myBase = theBase;
				}
			}
			return myBase;
		}
		
		public PotentialField GetFieldForMyBase(Vector v, FlagColor myColor)
		{
			return GetMyBase(v, myColor).GetBaseField();
		}
	}
}

