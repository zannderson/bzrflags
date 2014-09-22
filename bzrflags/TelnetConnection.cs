using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.IO;

namespace bzrflags
{
	public class TelnetConnection : IDisposable
	{
		private Socket _socket;
		private TcpClient _client;
		private NetworkStream _stream;
		private StreamReader _reader;
		private StreamWriter _writer;
		
		public TelnetConnection ()
		{
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}
		
		public void Connect(int port)
		{
			_client = new TcpClient("127.0.0.1", port);
			_stream = _client.GetStream();
			_reader = new StreamReader(_stream);
			_writer = new StreamWriter(_stream);
		}
		
		public string SendMessage(string message)
		{
			byte[] messageBytes = Encoding.ASCII.GetBytes(message + "\n");
			_writer.Write(messageBytes);
			return string.Empty;
		}
		
		public string GetMessage()
		{
			return string.Empty;
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
	}
}

