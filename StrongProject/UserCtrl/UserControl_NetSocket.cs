using System;
using System.Windows.Forms;
namespace StrongProject
{

	public partial class UserControl_NetSocket : UserControl
	{
		public delegate void delegate_UIShow(string outStr);
		public IPAdrr tag_IPAdrr;
		public SocketClient tag_SocketClient;
		public void SocketClient(IPAdrr _IPAdrr)
		{
			tag_IPAdrr = _IPAdrr;
		}
		private void ButtonConnectShowUi(object o)
		{

		}


		/// <summary>
		/// 初始化
		/// </summary>
		public UserControl_NetSocket(SocketClient sc)
		{
			tag_SocketClient = sc;
			tag_IPAdrr = tag_SocketClient.tag_IPAdrr;
			InitializeComponent();

		}
		private void UserControl_NetSocket_Load(object sender, EventArgs e)
		{
			textBox_Ip.Text = tag_IPAdrr.tag_ip;
			textBox_port.Text = tag_IPAdrr.tag_port.ToString();
			txtName.Text = tag_IPAdrr.tag_name;

			checkBox_Enable.Checked = tag_IPAdrr.tag_Enable;
			textBox3.Text = tag_IPAdrr.tag_defineRet;
		}
		private void button_Save_Click(object sender, EventArgs e)
		{
			try
			{
				tag_IPAdrr.tag_name = txtName.Text;
				tag_IPAdrr.tag_ip = textBox_Ip.Text;
				tag_IPAdrr.tag_port = int.Parse(textBox_port.Text);
				tag_IPAdrr.tag_Enable = checkBox_Enable.Checked;
				tag_IPAdrr.tag_defineRet = textBox3.Text;
			}
			catch (Exception mes)
			{

			}
		}
		void UIShow(string outStr)
		{
			textBox2.Text = textBox2.Text + outStr;
		}
		void delegate_SocketClientRead(string outStr)
		{

			if (this.textBox2.InvokeRequired)//如果调用控件的线程和创建创建控件的线程不是同一个则为True
			{

				delegate_UIShow d = new delegate_UIShow(UIShow);
				this.textBox1.Invoke(d, new object[] { outStr });
			}
			else
			{
				textBox2.Text = textBox2.Text + outStr;
			}
		}
		private void SetText(string text)
		{
			// InvokeRequired required compares the thread ID of the 
			// calling thread to the thread ID of the creating thread. 
			// If these threads are different, it returns true. 

		}
		private void button_Send_Click(object sender, EventArgs e)
		{

			string t = tag_SocketClient.send(textBox1.Text, 0, delegate_SocketClientRead);
			textBox2.Text = textBox2.Text + t;
		}

		private void button_Connect_Click(object sender, EventArgs e)
		{
			if (tag_SocketClient.tag_Socket == null || !tag_SocketClient.tag_Socket.Connected)
			{
				tag_SocketClient.ConnectThread();
				if (tag_SocketClient.tag_Socket.Connected)
				{
					button_Connect.Text = "关闭";
				}
			}
			else
				if (tag_SocketClient.tag_Socket.Connected)
			{
				tag_SocketClient.tag_Socket.Close();
				button_Connect.Text = "连接";
			}


		}
	}
	[Serializable]
	public class IPAdrr
	{
		public string tag_name = "Net";
		public string tag_ip = "127.0.0.1";
		public int tag_port = 10000;
		/// <summary>
		/// 是否启用相机
		/// </summary>
		public bool tag_Enable;
		/// <summary>
		/// 默认返回值，当不启用相机时候的返回值
		/// </summary>
		public string tag_defineRet;
	}
}
