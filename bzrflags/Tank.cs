using System;
using System.Collections.Generic;

namespace bzrflags
{
	
	public enum TANK_STATUS
	{
		ALIVE,
		DEAD,
	};
		
	
	public class Tank
	{
			
		public int _index {set;get;}
		public string _callsign {set;get;}
		public int _shotsAvailable {set;get;}
		public double _timeToReload {set;get;}
		public bool _hasFlag {set;get;}
		public double _xPosition {set;get;}
		public double _yPosition {set;get;}
		public double _angle {set;get;}
		public double _angelVelocity {set;get;}
		public double _velocityX {get;set;}
		public double _velocityY {get;set;}
		public TANK_STATUS _status {get;set;}
		public bool _isEnemy {get;set;}
		
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
		public void moveToPosition(Vector current, Vector target)
		{
			double targetAngle = Math.Atan2(target.Y - current.Y, target.X - current.X);
			double relativeAngle = NormalizeAngle(targetAngle - _angle);
		 	TelnetConnection.Connection.SendMessage("speed " + _index  + " 1 ", true);
			TelnetConnection.Connection.SendMessage("angvel " + _index + " " + relativeAngle.ToString(), true);
			TelnetConnection.Connection.SendMessage("shoot " + _index, true);
		}
		
		//make any angle be between -pi and pi
		private double NormalizeAngle(double angle)
		{
			decimal decimalPi = (decimal)Math.PI;
			decimal decimalAngle = (decimal)angle;
			decimal doublePi = Decimal.Multiply(2.0m, decimalPi);
			decimal intermediate = decimal.Divide(decimalAngle, doublePi);
			int intermediate2 = (int)intermediate;
			decimalAngle = decimal.Subtract(decimalAngle, decimal.Multiply(doublePi, intermediate2));
			//angle -= 2.0 * pi * (int)(angle/(2.0*pi));
			if(decimalAngle <= decimal.Multiply(-1.0m,decimalPi))
			{
				decimalAngle = decimal.Add(decimalAngle, doublePi);
				//angle += (double)doublePi;
			}
			else if(decimalAngle > decimalPi)
			{
				decimalAngle = decimal.Subtract(decimalAngle, doublePi);
				//angle -= (double)doublePi;
			}
			
			decimal.Multiply(decimalAngle, 2.0m);
			
			return (double)decimalAngle;
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
				double timeToReload = double.Parse(tokens[5]);
				FlagColor flag = Flag.ParseFlagColor(tokens[6]);
				double xPos = double.Parse(tokens[34]);
				double yPos = double.Parse (tokens[35]);
				double angle = double.Parse (tokens[36]);
				double velocityX = double.Parse (tokens[37]);
				double velocityY = double.Parse (tokens[38]);
				double angleVelocity = double.Parse (tokens[39]);
				
				bool hasFlag = false;
				//convert flag status to bool
				if(flag != FlagColor.None)
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
				FlagColor flag = Flag.ParseFlagColor(tokens[4]);
				double xPos = double.Parse(tokens[5]);
				double yPos = double.Parse (tokens[6]);
				double angle = double.Parse (tokens[7]);
				
				bool hasFlag = false;
				//convert flag status to bool
				if(flag != FlagColor.None)
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

