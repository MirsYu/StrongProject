using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{

	public delegate void delegate_FormMain_Log(string info, int type);


	/// <summary>
	/// 日志管理
	/// </summary>

	public partial class UserControl_LogOut : UserControl
	{
		public static string tag_upstring;
		/// <summary>
		/// ui定时显示
		/// </summary>
		private System.Windows.Forms.Timer tag_ShowLogTimer;//
															/// <summary>
															/// 
															/// </summary>
		private static ArrayList tag_arrMsg = new ArrayList();
		/// <summary>
		/// 锁
		/// </summary>
		public static object tag_locker;
		public static int is_showMsg = 10;
		public static string tag_upmsg = null;

		public static LogDataBase tag_logdatabase = new LogDataBase();
		public UserControl_LogOut()
		{
			InitializeComponent();
			tag_locker = new object();
			tag_ShowLogTimer = new System.Windows.Forms.Timer();
			tag_ShowLogTimer.Tick += new EventHandler(UserControl_LogOut_Show);
			tag_ShowLogTimer.Interval = 1000;
			tag_ShowLogTimer.Start();
			Console.SetOut(new ConsoleOut(textBox_Log));
		}

		public void dataGridViewShow(List<Log> log, DataGridView daview)
		{

			int i = 0;
			int cunt = log.Count;
			while (i < cunt)
			{
				//   daview.ForeColor =
				daview[0, i].Value = log[i].tag_dateTime.ToString();
				daview[1, i].Value = log[i].tag_info.ToString();
				i++;
				;
			}
		}

		/// <summary>
		/// 定时显示串 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserControl_LogOut_Show(object sender, EventArgs e)
		{
			try
			{
				if (tag_logdatabase == null)
					return;
				if (is_showMsg > 0)
				{

					is_showMsg--; ;

					dataGridView_Alarm.ForeColor = Color.Red;
					List<Log> listlog = tag_logdatabase.Get(0, 100);
					dataGridView_Alarm.ColumnCount = 2;
					dataGridView_Alarm.RowCount = 200;
					dataGridViewShow(listlog, dataGridView_Alarm);//0为报警
					listlog = tag_logdatabase.Get(1, 100);
					dataGridView_Step.ColumnCount = 2;
					dataGridView_Step.RowCount = 200;
					dataGridViewShow(listlog, dataGridView_Step);//1为运行信息
				}
			}
			catch (Exception mss)
			{

			}
		}
		/// <summary>
		/// 输出日志 ErrorType 0为红色异常信息，1为黑色运行信息
		/// </summary>
		/// <param name="Msg"></param>
		/// <param name="ErrorType"></param>
		public static void OutLog(string Msg, int ErrorType)
		{
			try
			{
				if (Msg == tag_upmsg)
					return;
				is_showMsg = 10;
				tag_logdatabase.addLog(ErrorType, Msg, DateTime.Now);
			}
			catch (Exception ex)
			{
				string str = ex.Message;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UserControl_LogOut_Load(object sender, EventArgs e)
		{

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_clean_Click(object sender, EventArgs e)
		{
			// richTextBox_log.Clear();
		}

		private void UserControl_LogOut_SizeChanged(object sender, EventArgs e)
		{
			//return;
			tabControl_msg.Location = new Point(0, 0);
			tabControl_msg.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);

			dataGridView_Alarm.Location = new Point(0, 0);
			dataGridView_Alarm.Size = new Size(tabControl_msg.Size.Width - 10, tabControl_msg.Size.Height - tabControl_msg.ItemSize.Height - 20);
			dataGridView_Alarm.Columns[1].Width = dataGridView_Alarm.Width - dataGridView_Alarm.Columns[0].Width - 2;

			//dataGridView_msg.Location = new Point(0, 0);
			//dataGridView_msg.Size = new Size(tabControl_msg.Size.Width, tabControl_msg.Size.Height - tabControl_msg.ItemSize.Height - 20);

			dataGridView_Step.Location = new Point(0, 0);
			dataGridView_Step.Size = new Size(tabControl_msg.Size.Width - 10, tabControl_msg.Size.Height - tabControl_msg.ItemSize.Height - 20);
			dataGridView_Step.Columns[1].Width = dataGridView_Step.Width - dataGridView_Step.Columns[0].Width - 2;

			//dataGridView_rightStep.Location = new Point(tabControl_msg.Size.Width / 3*2, 0);
			//dataGridView_rightStep.Size = new Size(tabControl_msg.Size.Width / 3, tabControl_msg.Size.Height - tabControl_msg.ItemSize.Height - 20);






		}

		private void tag_groupBox_Paint(object sender, PaintEventArgs e)
		{
			UserControl_LogOut_SizeChanged(sender, e);
		}



		private void checkBox_stop_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			tag_upmsg = null;

			is_showMsg = 10;
		}

	}

}
