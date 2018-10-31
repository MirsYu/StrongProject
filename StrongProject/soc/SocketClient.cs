using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace StrongProject
{
	public class SocketClient
	{
		/// <summary>
		/// SOCK
		/// </summary>
		public Socket tag_Socket;
		/// <summary>
		/// 委托，
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="len"></param>
		public delegate void delegate_SocketClientRead(string outStr);
		/// <summary>
		/// 
		/// </summary>
		public delegate_SocketClientRead tag_SocketClientRead;
		/// <summary>
		/// 读取数据的BUFFER
		/// </summary>
		public Byte[] tag_readByte = new Byte[512];
		/// <summary>
		/// 读取数据的长度
		/// </summary>
		public int tag_readLen = 0;
		public List<string> tag_readListList;

		public IPEndPoint tag_IPEndPoint;
		/// <summary>
		/// 表示连接的时候是否需要发送数据
		/// </summary>
		public string tag_strconnect;
		/// <summary>
		/// 表示是否发送
		/// </summary>
		public int tag_isSend = 0;
		/// <summary>
		/// 
		/// </summary>
		public Thread tag_myThread;
		/// <summary>
		/// 
		/// </summary>
		public Thread tag_myThreadConnect;

		public int tag_SendLock = 0;

		public IPAdrr tag_IPAdrr;
		public int tag_port;

		/// <summary>
		/// 构造函数，连接时候不需要发送数据
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="port"></param>

		public SocketClient(IPAdrr ipa)
		{
			tag_IPAdrr = ipa;
			//设定服务器IP地址   
			try
			{

			}
			catch (Exception ex)
			{
				UserControl_LogOut.OutLog(ex.Message, 0);

			}

		}
		~SocketClient()
		{
			if (tag_Socket != null)
			{
				CLose();
				tag_Socket = null;
			}
			if (tag_myThread != null)
			{
				tag_myThread.Abort();
			}
			tag_readByte = null;

		}

		/// <summary>
		/// 读服务器下发的数据,几台通信
		/// </summary>
		public void Connect()
		{
			try
			{
				IPAddress _ip = IPAddress.Parse(tag_IPAdrr.tag_ip);
				tag_IPEndPoint = new IPEndPoint(_ip, tag_IPAdrr.tag_port);
				tag_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				tag_Socket.Connect(tag_IPEndPoint); //配置服务器IP与端口 if
				if (!tag_Socket.Connected)
				{
					return;
				}

			}
			catch (Exception msg)
			{
				UserControl_LogOut.OutLog(msg.Message, 0);
			}
		}
		/// <summary>
		/// 读服务器下发的数据,几台通信
		/// </summary>
		public void ConnectThread()
		{
			try
			{
				IPAddress _ip = IPAddress.Parse(tag_IPAdrr.tag_ip);
				tag_IPEndPoint = new IPEndPoint(_ip, tag_IPAdrr.tag_port);
				tag_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				tag_Socket.Connect(tag_IPEndPoint); //配置服务器IP与端口 if
				if (!tag_Socket.Connected)
				{
					return;
				}
				tag_myThread = new Thread(new ParameterizedThreadStart(SocketClient_Read));
				tag_myThread.Start();
				tag_myThread.IsBackground = true;
			}
			catch (Exception msg)
			{
				UserControl_LogOut.OutLog(msg.Message, 0);
			}
		}
		/// <summary>
		/// 读服务器下发的数据,几台通信
		/// </summary>
		private void SocketClient_Read(object o)
		{
			try
			{
				while (true)
				{
					int n = 1;
					if (tag_Socket != null)
					{
						if (tag_readLen < 0)
							tag_readLen = 0;
						tag_readLen = tag_Socket.Receive(tag_readByte, tag_readByte.Length, 0);
						if (tag_readLen == 0)
						{
							CLose();
							break;
						}
						string retstr = Encoding.ASCII.GetString(tag_readByte, 0, tag_readLen);
						if (tag_SocketClientRead != null)
						{
							tag_SocketClientRead(retstr);
						}
						if (!tag_Socket.Connected)
						{
							break;
						}


					}
					Thread.Sleep(100);
				}
			}
			catch (Exception ex)
			{
				UserControl_LogOut.OutLog(ex.Message, 0);
				CLose();
			}


		}
		/// <summary>
		/// connsendstr 当没有连接时候，发送的串，sendstring 发送的穿
		/// </summary>
		/// <param name="connsendstr"></param>
		/// <param name="sendstring"></param>
		/// <returns></returns>
		public string send(string sendstring, int outTime, delegate_SocketClientRead readCall)
		{
			try
			{
				byte[] sendBuffer = Encoding.ASCII.GetBytes(sendstring);
				byte[] read = new byte[256];
				tag_isSend = 1;
				tag_SocketClientRead = readCall;
				if (tag_Socket == null)
				{
					tag_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				}
				if (!tag_Socket.Connected)
				{
					ConnectThread();
				}
				if (tag_Socket.Connected)
				{
					tag_Socket.Send(sendBuffer);
					tag_Socket.ReceiveTimeout = 1000 * outTime;

					return "";
				}
				else
				{
					return "";
				}

			}
			catch (Exception ex)
			{
				tag_SendLock = 0;
				UserControl_LogOut.OutLog(ex.Message, 0);
				CLose();
				return ex.Message;
			}

		}
		/// <summary>
		/// connsendstr 当没有连接时候，发送的串，sendstring 发送的穿
		/// </summary>
		/// <param name="connsendstr"></param>
		/// <param name="sendstring"></param>
		/// <returns></returns>
		public string send(string sendstring, int outTime)
		{
			try
			{
				byte[] sendBuffer = Encoding.ASCII.GetBytes(sendstring);
				byte[] read = new byte[512];
				tag_isSend = 1;
				if (tag_Socket == null || !tag_Socket.Connected)
				{
					tag_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				}
				if (!tag_Socket.Connected)
				{
					Connect();
				}
				if (tag_Socket.Connected)
				{
					//1123
					try
					{
						tag_Socket.Send(sendBuffer);
						tag_Socket.ReceiveTimeout = outTime;
						tag_readLen = tag_Socket.Receive(read, read.Length, 0);
						string retstr = Encoding.ASCII.GetString(read, 0, tag_readLen);
						//UserControl_LogOut.OutLog(retstr, 0);
						return retstr;
					}
					catch (Exception exx)
					{
						tag_Socket = null;
						tag_Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
						tag_Socket.Send(sendBuffer);
						tag_Socket.ReceiveTimeout = outTime;
						tag_readLen = tag_Socket.Receive(read, read.Length, 0);
						string retstr = Encoding.ASCII.GetString(read, 0, tag_readLen);
						UserControl_LogOut.OutLog(retstr, 0);
						return retstr;
					}

				}
				else
				{
					return "error,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,\r\n";
				}

			}
			catch (Exception ex)
			{
				tag_SendLock = 0;
				UserControl_LogOut.OutLog(ex.Message, 0);
				CLose();
				return "";
			}

		}

		/// <summary>
		/// connsendstr 当没有连接时候，发送的串，sendstring 发送的穿
		/// </summary>
		/// <param name="connsendstr"></param>
		/// <param name="sendstring"></param>
		/// <returns></returns>
		public string Read(int outTime)
		{
			try
			{
				if (tag_Socket.Connected)
				{
					//1123
					try
					{
						byte[] read = new byte[512];
						tag_Socket.ReceiveTimeout = outTime;
						tag_readLen = tag_Socket.Receive(read, read.Length, 0);
						string retstr = Encoding.ASCII.GetString(read, 0, tag_readLen);
						//UserControl_LogOut.OutLog(retstr, 0);
						return retstr;
					}
					catch (Exception exx)
					{
						UserControl_LogOut.OutLog(exx.Message, 0);
						return "";
					}

				}
				else
				{
					return "error,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,\r\n";
				}

			}
			catch (Exception ex)
			{
				tag_SendLock = 0;
				UserControl_LogOut.OutLog(ex.Message, 0);
				CLose();
				return "error,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,\r\n"; ;
			}

		}

		/// <summary>
		/// 
		/// </summary>
		void CLose()
		{
			try
			{
				if (tag_Socket != null)
				{
					tag_Socket.Close();
				}
			}
			catch
			{ }

			tag_Socket = null;
		}
	}
}
