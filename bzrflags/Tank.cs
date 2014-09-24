using System;

namespace bzrflags
{
	
	enum TANK_STATUS
	{
		ALIVE,
		DEAD,
	};
		
	
	public class Tank
	{
			
		private int _index;
		private int _callsign;
		private int _shotsAvailable;
		private int _timeToReload;
		private bool _hasFlag;
		private int _xPosition;
		private int _yPosition;
		private double _angle;
		private int _angelVelocity;
		private TelnetConnection _connection;
		
		public Tank (TelnetConnection connection, int index, int xPos, int yPos, double angle, int angleVelocity)
		{
			_connection = connection;
			_index = index;
			_xPosition = xPos;
			_yPosition = yPos;
			_angle = angle;
			_angelVelocity = angleVelocity;
			
			//callsign?
			
			_shotsAvailable = 3;
			_timeToReload = 0;
			_hasFlag = false;
			
		}
		
		public void updateMyPosition(int xPos, int yPos, double angle, int angleVelocity)
		{
			_xPosition = xPos;
			_yPosition = yPos;
			_angle = angle;
			_angelVelocity = angleVelocity;
		}
		
		//set command to move to given coordinates
		public void moveToPosition(double targetX, double targetY)
		{
			double targetAngle = Math.Atan2(targetY - _yPosition, targetX - _xPosition);
			double relativeAngle = normalizeAngle(targetAngle - _angle);
		 	_connection.SendMessage(_index  + " 1 " + (2 * relativeAngle), true );
            Console.Out.WriteLine(_connection.ReceiveMessage());	
		}
		
		//make any angle be between -pi and pi
		private static double normalizeAngle(double angle)
		{
			angle -= 2 * Math.PI * (angle/(2*Math.PI));
			if(angle <= Math.PI)
			{
				angle += 2 * Math.PI;
			}
			else if(angle > Math.PI)
			{
				angle -= 2 * Math.PI;
			}
			
			return angle;
		}
	}
}

