using System;

namespace bzrflags
{
	public interface IPotentialField
	{		
		Vector GetVectorForMapPoint(float x, float y);
	}
}

