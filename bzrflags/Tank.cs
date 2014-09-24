using System;
using System.Collections.Generic;

namespace bzrflags
{
	
	enum TANK_STATUS
	{
		ALIVE,
		DEAD,
	};
		
	
	public class Tank
	{
			
		private int _index {set;get;}
		private string _callsign {set;get;}
		private int _shotsAvailable {set;get;}
		private int _timeToReload {set;get;}
		private bool _hasFlag {set;get;}
		private int _xPosition {set;get;}
		private int _yPosition {set;get;}
		private double _angle {set;get;}
		private double _angelVelocity {set;get;}
		private TelnetConnection _connection {set;get;}
		
		public Tank ()
		{		
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
		 	_connection.SendMessage(_index  + " 1 " + (2 * relativeAngle) );
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
		
	public static List<Tank> createTanks(string rawTanksData)
	{
		List<Tank> myTanks = new List<Tank>();
			
		string[] splitTankData = rawTanksData.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
			
			foreach(string individualTankData in splitTankData)
			{
				if(individualTankData == "begin" || individualTankData == "end")
					continue;
				
				string[] tokens = individualTankData.Split(null);
				
				int tankIndex = int.Parse (tokens[1]);
				string callsign = tokens[2];
				string status = tokens[3];
				int shotsAvailable = int.Parse(tokens[4]);
				int timeToReload = int.Parse(tokens[5]);
				char flag = char.Parse(tokens[6]);
				int xPos = int.Parse(tokens[34]);
				int yPos = int.Parse (tokens[35]);
				double angle = double.Parse (tokens[36]);
				double velocityX = double.Parse (tokens[37]);
				double velocityY = double.Parse (tokens[38]);
				double angleVelocity = double.Parse (tokens[39]);
				
				
				//convert flag status to bool
					
				//convert status to enum
				
				
				//create tank, add it to myTanks
				Tank tempTank = new Tank()
				{
					_index = tankIndex,
					_callsign = callsign,
					//connection????
					//status?????
					//flag?????
					
					_shotsAvailable = shotsAvailable,
					_timeToReload = timeToReload,
					_xPosition = xPos,
					_yPosition = yPos,
					_angle = angle,
					//velocityX
					//""Y
					_angelVelocity = angleVelocity
					
				};
				
				
			}
		return myTanks;
	}
		
		
		
	}
	
	
	
	
}

