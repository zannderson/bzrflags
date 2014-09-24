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
		private double _xPosition {set;get;}
		private double _yPosition {set;get;}
		private double _angle {set;get;}
		private double _angelVelocity {set;get;}
		private double _velocityX {get;set;}
		private double _velocityY {get;set;}
		private TANK_STATUS _status {get;set;}
		private bool _isEnemy {get;set;}
		
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
			TelnetConnection connection = TelnetConnection.getConnection();
			double targetAngle = Math.Atan2(targetY - _yPosition, targetX - _xPosition);
			double relativeAngle = normalizeAngle(targetAngle - _angle);
		 	connection.SendMessage(_index  + " 1 " + (2 * relativeAngle),true );
            Console.Out.WriteLine(connection.ReceiveMessage());	
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
		
	public static List<Tank> createMyTanks(string rawTanksData)
	{
		List<Tank> myTanks = new List<Tank>();
			
		string[] splitTankData = rawTanksData.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
			
			foreach(string individualTankData in splitTankData)
			{
				if(!individualTankData.StartsWith("mytank"))
					continue;
				
				string[] tokens = individualTankData.Split(null);
				
				int tankIndex = int.Parse (tokens[1]);
				string callsign = tokens[2];
				string status = tokens[3];
				int shotsAvailable = int.Parse(tokens[4]);
				int timeToReload = int.Parse(tokens[5]);
				char flag = char.Parse(tokens[6]);
				double xPos = double.Parse(tokens[34]);
				double yPos = double.Parse (tokens[35]);
				double angle = double.Parse (tokens[36]);
				double velocityX = double.Parse (tokens[37]);
				double velocityY = double.Parse (tokens[38]);
				double angleVelocity = double.Parse (tokens[39]);
				
				bool hasFlag = false;
				//convert flag status to bool
				if(flag != '-')
				{
					hasFlag = true;	
				}
				
				
				//convert status to enum
				TANK_STATUS enumStatus = TANK_STATUS.ALIVE;
				if(status == "alive")
				{
					enumStatus = TANK_STATUS.ALIVE;
				}
				else if(status == "dead")
				{
					enumStatus = TANK_STATUS.DEAD;
				}
					
				
				//create tank, add it to myTanks
				Tank tempTank = new Tank()
				{
					_isEnemy = false,
					_index = tankIndex,
					_callsign = callsign,
					_status = enumStatus,
					_shotsAvailable = shotsAvailable,
					_timeToReload = timeToReload,
					_xPosition = xPos,
					_yPosition = yPos,
					_angle = angle,
					_velocityX = velocityX,
					_velocityY = velocityY,
					_angelVelocity = angleVelocity,
					_hasFlag = hasFlag
					
				};
				
				myTanks.Add(tempTank);
				
			}
		return myTanks;
	}
		
		
		public static List<Tank> createEnemyTanks(string rawEnemyTankData)
		{
			List<Tank> myTanks = new List<Tank>();
			
			string[] splitTankData = rawEnemyTankData.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
			
			foreach(string individualTankData in splitTankData)
			{
				if(!individualTankData.StartsWith("othertank"))
					continue;
				
				string[] tokens = individualTankData.Split(null);
				
				//othertank [callsign] [color] [status] [flag] [x] [y] [angle]
				string callsign = tokens[1];
				string color = tokens[2];
				string status = tokens[3];
				char flag = char.Parse(tokens[4]);
				double xPos = double.Parse(tokens[5]);
				double yPos = double.Parse (tokens[6]);
				double angle = double.Parse (tokens[7]);
				
				bool hasFlag = false;
				//convert flag status to bool
				if(flag != '-')
				{
					hasFlag = true;	
				}
				
				
				//convert status to enum
				TANK_STATUS enumStatus = TANK_STATUS.ALIVE;
				if(status == "alive")
				{
					enumStatus = TANK_STATUS.ALIVE;
				}
				else if(status == "dead")
				{
					enumStatus = TANK_STATUS.DEAD;
				}
							
				//create tank, add it to myTanks
				Tank tempTank = new Tank()
				{
					_isEnemy = true,
					_callsign = callsign,
					_status = enumStatus,
					_xPosition = xPos,
					_yPosition = yPos,
					_angle = angle,
					_hasFlag = hasFlag
					
				};
				
				myTanks.Add(tempTank);
				
				
			}
			
			
			
			
			
			
			return myTanks;
			
		}
		
		
		
	}
	
	
	
	
}

