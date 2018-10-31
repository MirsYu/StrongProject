using System;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public delegate void delegate_reset();
	public delegate void delegate_start();
	public delegate void delegate_stop();
	public partial class stepTrialRun : UserControl
	{
		public manual tag_manual;
		public string tag_titleName;
		public object tag_object;
		public workBase tag_workBase;
		public StationModule tag_StationModule;
		public stepTrialRun()
		{
			InitializeComponent();
		}
		/// <summary>
		/// 工位名
		/// </summary>
		/// <param name="name"></param>
		/// <param name="_manual"></param>
		/// <param name="reset"></param>
		/// <param name="start"></param>
		/// <param name="stop"></param>
		public stepTrialRun(object o)
		{
			tag_object = o;
			tag_workBase = (workBase)o;
			tag_titleName = tag_workBase.tag_stationName; ;
			tag_manual = tag_workBase.tag_manual;
			InitializeComponent();
		}

		private void stepTrialRun_Load(object sender, EventArgs e)
		{
			button_LineCheck.Text = tag_titleName;
			StationModule sm = StationManage.FindStation(tag_workBase.tag_stationName);
			if (sm == null)
			{
				return;
			}
			tag_manual.tag_induction = sm.tag_Enable;
			checkBox2.Checked = sm.tag_Enable;
		}

		private void button_LineCheck_Click(object sender, EventArgs e)
		{
			Type t = tag_object.GetType();
			System.Reflection.MethodInfo[] methods = t.GetMethods();
			System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
			System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
			if (Global.WorkVar.tag_ResetState == 0)
			{
				MessageBoxLog.Show("没有复位，请复位");
				return;
			}
			if (Global.WorkVar.tag_ResetState == 1)
			{
				MessageBoxLog.Show("复位中，请等待");
				return;
			}
			if (Global.WorkVar.tag_IsExit == 1)
			{
				MessageBoxLog.Show("请到主界面按启动键");
				return;
			}
			for (int i = 0; i < methods.Length; i++)
			{
				if (tag_workBase.tag_isWork == 0)
				{
					if (methods[i].Name == "StartThread")
					{
						methods[i].Invoke(tag_object, null);
						button_LineCheck.BackColor = Color.Green;
						break;
					}
				}
				else
				{
					if (methods[i].Name == "exit")
					{
						methods[i].Invoke(tag_object, null);
						button_LineCheck.BackColor = Color.Yellow;
						break;
					}
				}

			}

		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			tag_manual.tag_isSuspend = checkBox1.Checked;
		}

		private void checkBox2_CheckedChanged(object sender, EventArgs e)
		{
			tag_manual.tag_induction = checkBox2.Checked;
			StationModule sm = StationManage.FindStation(tag_workBase.tag_stationName);
			sm.tag_Enable = tag_manual.tag_induction;
		}

	}
}
