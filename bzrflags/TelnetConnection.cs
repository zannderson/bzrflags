using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;
using System.Threading;

namespace bzrflags
{
	public class TelnetConnection : IDisposable
	{
		private Socket _socket;
		private TcpClient _client;
		private NetworkStream _stream;
		private StreamReader _reader;
		
		private static TelnetConnection _connection;
		private static int _port;
		
		private TelnetConnection ()
		{
			//_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_client = new TcpClient("127.0.0.1", _port);
			_stream = _client.GetStream();
			_reader = new StreamReader(_stream, Encoding.ASCII);
			string bzrobots = ReceiveMessage();
			SendMessage("agent 1", false);
		}
		
		public static void setPort(int port)
		{
			_port = port;
		}
		
		public string SendMessage(string message, bool receive)
		{
			try
			{
				byte[] messageBytes = Encoding.ASCII.GetBytes(message + "\n");
				_stream.Write(messageBytes, 0, messageBytes.Length);
			}
			catch (Exception ex)
			{
				Console.Out.WriteLine(string.Format("Exception sending message to server: {0}", ex.Message));
			}
			
			if(receive)
			{
				return ReceiveMessage();			
			}
			else
			{
				return string.Empty;
			}
		}
		
		public string ReceiveMessage()
		{
			try
			{		
				bool inAMessage = false;
				StringBuilder sb = new StringBuilder();
				int lineCounter = 1;
				while(!_stream.DataAvailable)
				{
					Thread.Sleep (5);
				}
				while(_stream.DataAvailable)
				{
					inAMessage = true;
					while(inAMessage)
					{
						string line = _reader.ReadLine();
						if(line.StartsWith("ack"))
						{
							inAMessage = true;
						}
						if(line.StartsWith("ok") || line.StartsWith("end") || line.StartsWith("bzrobots") || line.StartsWith("fail"))
						{
							inAMessage = false;
						}
						if(inAMessage)
						{
							sb.Append(line);
							sb.Append ("\n");
						}
						lineCounter++;
					}
				}
				return sb.ToString();
			}
			catch (Exception ex)
			{
				Console.Out.WriteLine("Exception trying to read from stream.", ex.Message);
				return string.Empty;
			}
		}

		#region IDisposable implementation
		
		public void Dispose ()
		{
			if(_socket != null && _socket.Connected)
			{
				_socket.Close();
			}
		}
		
		#endregion
		
		public static TelnetConnection Connection
		{
			get
			{
				if(_connection == null)
				{
					_connection = new TelnetConnection();
				}
				return _connection;
			}
		}
	}
}

