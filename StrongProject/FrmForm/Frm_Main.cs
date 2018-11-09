using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StrongProject
{
	public partial class Frm_Main : Form
	{
		Frm_Frame frmFrame = null;// new Frm_Frame();
		public Work tag_work;
		public MyNGControl tag_NGControl1;
		public UserControl_LogOut logOutControl = new UserControl_LogOut();

		public WebControl webControl_NG = new WebControl();
		public WebControl webControl_Charts = new WebControl();

		public Frm_Main(Frm_Frame frm, Work work)
		{
			tag_work = work;
			NewCtrlCardV0.tag_initResult = NewCtrlCardV0.initCard(tag_work.tag_CardHave);
			if (NewCtrlCardV0.tag_initResult == 0)
			{
				NewCtrlCardIO.StartListenSignal(tag_work.tag_CardHave);
			}
			tag_work.AllAxismodeInit();
			frmFrame = frm;
			InitializeComponent();
			if (frmFrame != null)
			{
				this.Size = new Size(frmFrame.Size.Width, frmFrame.Size.Height - 50);
			}
			webControl_NG.RefreshURL(new Uri("http://www.mirsyu.xyz/HTML/NG.html"));
			panel_NG.Controls.Add(webControl_NG);
			webControl_NG.Location = new Point(0, 0);
			webControl_NG.Size = webControl_NG.Parent.Size;

			webControl_Charts.RefreshURL(new Uri("http://www.mirsyu.xyz/HTML/Charts.html"));
			panel_Charts.Controls.Add(webControl_Charts);
			webControl_Charts.Location = new Point(0, 0);
			webControl_Charts.Size = webControl_Charts.Parent.Size;


			groupBox_RunInfo.Controls.Add(logOutControl);
			logOutControl.Location = new Point(5, 20);
			logOutControl.Size = new Size(groupBox_RunInfo.Size.Width - 10, groupBox_RunInfo.Size.Height - 25);
		}
		private void myChartPieShow(object sender, EventArgs e)
		{

		}
		private void Frm_Main_Load(object sender, EventArgs e)
		{
			timer1.Start();


			string[] xName = { "月NG", "日NG" };//
			double[] yPercent = new double[xName.Count()];
			string[] yValue = new string[xName.Count()];
			for (int i = 0; i < xName.Count(); i++)
			{
				int a = new Random().Next(0, 200);
				Thread.Sleep(20);
				int b = new Random().Next(500, 1000);
				yValue[i] = a.ToString() + "/" + "\r\n" + b.ToString();
				yPercent[i] = (double)100 * a / b;
				Thread.Sleep(20);
			}
			Color[] color = { Global.WorkVar.SRRed, Global.WorkVar.SRRed };// 

			string[] seriesName1 = { "△X_1", "△X_2", "△Y_1", "△Y_2" };
			string[] xName1 = new string[10];
			double[,] yValue1 = new double[xName1.Count(), seriesName1.Count()];
			for (int i = 0; i < xName1.Count(); i++)
			{
				xName1[i] = "穴位" + (i + 1).ToString();
				for (int j = 0; j < seriesName1.Count(); j++)
				{
					yValue1[i, j] = (double)new Random().Next(50, 70);
					Thread.Sleep(10);
				}
			}
			SeriesChartType[] type = { SeriesChartType.Line, SeriesChartType.Line, SeriesChartType.Line, SeriesChartType.Line };
			Color[] color1 = { Color.FromArgb(175, 216, 153), Color.FromArgb(135, 244, 153), Color.FromArgb(225, 116, 153), Color.FromArgb(175, 156, 253) };
		}



		public static Form_MessageBos tag_Form_MessageBos;

		private void timerUI_Tick(object sender, EventArgs e)
		{
			if (Global.WorkVar.tag_MessageoxStr != null && tag_Form_MessageBos == null)
			{
				string messbox = Global.WorkVar.tag_MessageoxStr;
				tag_Form_MessageBos = new Form_MessageBos(Global.WorkVar.tag_MessageoxStr, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
				tag_Form_MessageBos.Show();
			}
			else if (Global.WorkVar.tag_MessageoxStr == null && tag_Form_MessageBos != null)
			{
				tag_Form_MessageBos.Close();
				tag_Form_MessageBos = null;
			}




			if (Global.WorkVar.tag_StopState != 0)
			{
				label_state.Text = "急停中";
				label_state.Image = global::StrongProject.Properties.Resources.bigbk_red;
				frmFrame.LightandBuzzer("红灯");
			}
			else
			{
				if (Global.WorkVar.tag_ResetState == 1)
				{
					label_state.Text = "复位中";
					label_state.Image = global::StrongProject.Properties.Resources.bigbk;
					frmFrame.LightandBuzzer("红灯");
				}
				else
				{
					if (Global.WorkVar.tag_ResetState == 0)
					{
						label_state.Text = "请复位";
						label_state.Image = global::StrongProject.Properties.Resources.bigbk;
						frmFrame.LightandBuzzer("红灯");
					}
					else
					{
						if (Global.WorkVar.tag_workState == 1)
						{
							if (Global.WorkVar.tag_SuspendState == 1)
							{
								label_state.Text = "暂停中";
								label_state.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
								frmFrame.LightandBuzzer("黄灯");
							}
							else
							{
								if (Global.WorkVar.bEmptyRun)
								{
									label_state.Text = "空跑中";
									frmFrame.LightandBuzzer("绿灯");
								}
								else
								{
									label_state.Text = "工作中";
									frmFrame.LightandBuzzer("绿灯");
								}
								label_state.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
							}
						}
						else
						{
							label_state.Text = "待机中";
							label_state.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
							frmFrame.LightandBuzzer("黄灯");
						}
					}
				}
			}

		}


		private void button1_Click_1(object sender, EventArgs e)
		{
			if (MessageBoxLog.Show("确定要清空所有计数吗?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				Global.WorkVar._config.tag_leftShenChanNGTotal = 0;
				Global.WorkVar._config.tag_leftShenChanOKTotal = 0;
				Global.WorkVar._config.tag_leftShenChanCT = 0;
				Global.WorkVar._config.tag_RightShenChanNGTotal = 0;
				Global.WorkVar._config.tag_RightShenChanOKTotal = 0;
				Global.WorkVar._config.tag_RightShenChanCT = 0;
				Global.WorkVar._config.Save();
			}
		}
	}
}
