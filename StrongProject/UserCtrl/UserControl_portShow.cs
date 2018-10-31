using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace StrongProject.UserCtrl
{
	public partial class UserControl_portShow : UserControl
	{
		public Work tag_Work;
		public List<Button> tag_List = new List<Button>();
		public List<Button> tag_SocketList = new List<Button>();
		public int tag_isLoad = 0;
		public int tag_isLoadNet = 0;
		public UserControl_portShow()
		{
			InitializeComponent();
		}
		private void OutputBT_Click(object sender, EventArgs e)
		{
			Button butt = (Button)sender;
			JSerialPort jsp = (JSerialPort)butt.Tag;

			if (jsp.tag_SerialPort != null && jsp.tag_SerialPort.IsOpen)
			{
				jsp.tag_SerialPort.Close();
			}
			else
			{

				jsp.open();

			}

		}
		private void OutputSocket_Click(object sender, EventArgs e)
		{
			Button butt = (Button)sender;
			SocketClient jsp = (SocketClient)butt.Tag;

			if (jsp.tag_Socket != null && jsp.tag_Socket.Connected)
			{
				jsp.tag_Socket.Close();
			}
			else
			{

				jsp.Connect();

			}

		}
		private void UserControl_portShow_Load(object sender, EventArgs e)
		{

			int i = 0;

			if (tag_Work == null || tag_Work.tag_JSerialPort == null)
			{

			}
			else
			{
				if (tag_isLoad == 0)
				{

					foreach (JSerialPort jsp in tag_Work.tag_JSerialPort)
					{
						if (jsp.tag_PortParameter.tag_name == "" || jsp.tag_PortParameter.tag_name == null)
						{
							continue;
						}

						Button butt = new Button();
						butt.Text = jsp.tag_PortParameter.tag_name;
						butt.Location = new Point(10 + i * 80, (this.Size.Height - butt.Size.Height) / 2);
						butt.Tag = jsp;
						butt.Click += new System.EventHandler(OutputBT_Click);
						this.Controls.Add(butt);
						tag_List.Add(butt);
						i++;
					}
					tag_isLoad = 1;
				}
			}

			if (tag_Work == null || tag_Work.tag_SocketClient == null)
			{

			}
			else
			{
				if (tag_isLoadNet == 0)
				{
					foreach (SocketClient jsp in tag_Work.tag_SocketClient)
					{
						if (jsp.tag_IPAdrr.tag_name == "" || jsp.tag_IPAdrr.tag_name == null)
						{
							continue;
						}

						Button butt = new Button();
						butt.Text = jsp.tag_IPAdrr.tag_name;
						butt.Location = new Point(10 + i * 80, (this.Size.Height - butt.Size.Height) / 2);
						butt.Tag = jsp;
						butt.Click += new System.EventHandler(OutputSocket_Click);
						this.Controls.Add(butt);
						tag_SocketList.Add(butt);
						i++;
					}
					tag_isLoadNet = 1;
				}
			}



		}
		public void UserControl_portShow_Load()
		{
			if (tag_Work == null || tag_Work.tag_JSerialPort == null)
				return;

			UserControl_portShow_Load(null, null);



			if (tag_Work == null || tag_Work.tag_JSerialPort == null)
			{

			}
			else
			{
				foreach (Button butt in tag_List)
				{
					JSerialPort jsp = (JSerialPort)butt.Tag;
					if (jsp.tag_SerialPort != null && jsp.tag_SerialPort.IsOpen)
					{
						butt.Text = jsp.tag_PortParameter.tag_name + "已开";
						butt.BackColor = Color.Green;
					}
					else
					{
						butt.Text = jsp.tag_PortParameter.tag_name + "未开";
						butt.BackColor = Color.Yellow;
					}
				}
			}

			if (tag_Work == null || tag_Work.tag_SocketClient == null)
			{

			}
			else
			{
				foreach (Button butt in tag_SocketList)
				{
					SocketClient jsp = (SocketClient)butt.Tag;
					if (jsp.tag_Socket != null && jsp.tag_Socket.Connected)
					{
						butt.Text = jsp.tag_IPAdrr.tag_name + "已连接";
						butt.BackColor = Color.Green;
					}
					else
					{
						butt.Text = jsp.tag_IPAdrr.tag_name + "未连接";
						butt.BackColor = Color.Yellow;
					}

				}
			}
		}
	}
}
