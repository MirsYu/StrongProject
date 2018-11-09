using log4net;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace StrongProject
{
	public partial class Frm_Frame : Form
	{

		private static readonly ILog log = LogManager.GetLogger("TEST");
		public Work tag_work;
		public UserCtrl.UserControl_portShow tag_UserControl_portShow = null;

		public UserCtrl.UserControl_SN tag_UserControl_SN = null;

		public Frm_Frame()
		{
			InitializeComponent();
		}
		public Frm_Main fMain = null;
		//// public Frm_Main fMain = null;
		public FrmDebug fDebug = null;
		public Frm_Alarm fAlarm = null;

		private MyAutoSizeForm myAutoSize;
		DateTime currentTime;
		private void Frm_Frame_Load(object sender, EventArgs e)
		{
			currentTime = DateTime.Now;
			tag_work = new Work();
			Global.WorkVar._config = SolderConfig.Load();
			fMain = new Frm_Main(this, tag_work);
			fMain.TopLevel = false;
			fMain.Parent = this.panelForm;
			fDebug = new FrmDebug(this, tag_work);
			fDebug.TopLevel = false;
			fDebug.Parent = this.panelForm;
			fAlarm = new Frm_Alarm();
			fAlarm.TopLevel = false;
			fAlarm.Parent = this.panelForm;

			/*  ShowSubWindow(Global.CConst.FORM_DEBUG);
			  ShowSubWindow(Global.CConst.FORM_ALARM);
			  ShowSubWindow(Global.CConst.FORM_MAIN);
			  #endregion
			 */
			ShowSubWindow(Global.CConst.FRM_MAIN);

			myAutoSize = new MyAutoSizeForm();
			myAutoSize.RecordOldSizeScale(this.panelForm);
			timer1.Start();

			var version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			lblVersion.Text = "版本号:" + version.ToString();
			//lblDay.Text = "当前时间:" + DateTime.UtcNow.ToString();
			//lblDay.Text = "程序生成时间:"+System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString();

			if (Global.WorkVar.tag_StopState == 1)
			{
				IOParameter redlight = StationManage.FindOutputIo("主控系统", "三色灯_红");
				IOParameter yellowlight = StationManage.FindOutputIo("主控系统", "三色灯_黄");
				IOParameter greenlight = StationManage.FindOutputIo("主控系统", "三色灯_绿");
				NewCtrlCardV0.SetOutputIoBit(redlight, 1);
				NewCtrlCardV0.SetOutputIoBit(yellowlight, 0);
				NewCtrlCardV0.SetOutputIoBit(greenlight, 0);
			}
			tag_UserControl_portShow = new UserCtrl.UserControl_portShow();
			tag_UserControl_portShow.tag_Work = tag_work;
			tag_UserControl_portShow.Location = new Point(lbl_User.Location.X + lbl_User.Size.Width + 100, (panel3.Size.Height - tag_UserControl_portShow.Size.Height) / 2);

			tag_UserControl_portShow.Size = new Size(lblDay.Location.X - tag_UserControl_portShow.Location.X - 10, tag_UserControl_portShow.Size.Height);
			panel3.Controls.Add(tag_UserControl_portShow);


			tag_UserControl_SN = new UserCtrl.UserControl_SN();
			tag_UserControl_SN.tag_Work = tag_work;
			tag_UserControl_SN.Location = new Point(tag_UserControl_SN.Location.X + tag_UserControl_SN.Size.Width + 50, (panel3.Size.Height - tag_UserControl_SN.Size.Height) / 2);

			tag_UserControl_SN.Size = new Size(lblDay.Location.X - tag_UserControl_SN.Location.X - 10, tag_UserControl_SN.Size.Height);
			panel3.Controls.Add(tag_UserControl_SN);




			IOParameter servoOnSwith = StationManage.FindOutputIo("总复位", "使能");
			if (servoOnSwith != null)
			{
				NewCtrlCardV0.SetOutputIoBit(servoOnSwith, 1);
			}

		}

		private void toolStripMenuMain_Click(object sender, EventArgs e)
		{
			ShowSubWindowDel(Global.CConst.FRM_MAIN);
		}

		private void toolStripMenuDebug_Click(object sender, EventArgs e)
		{
			//ShowSubWindowDel(Global.CConst.FRM_DEBUG, true);// 
			ShowSubWindowDel(Global.CConst.FRM_DEBUG);// 
		}

		private void toolStripMenuAlarm_Click(object sender, EventArgs e)
		{
			ShowSubWindowDel(Global.CConst.FORM_ALARM, true);
		}

		private void toolStripMenuLogin_Click(object sender, EventArgs e)
		{
			LogIn frmLogin = new LogIn(this);
			frmLogin.ShowDialog();


		}

		private void toolStripMenuBoard_Click(object sender, EventArgs e)
		{
			try
			{
				string path = "";
				if (Environment.Is64BitOperatingSystem)
				{
					path = Application.StartupPath + "\\osk_x64.exe";
				}
				else
				{
					path = Application.StartupPath + "\\osk_x86.exe";
				}
				System.Diagnostics.Process.Start("explorer.exe", path);
			}
			catch
			{
				//Global.Forms.Msg.MessageTopMost("打开软键盘出错", false, false, false);
			}
		}

		private void toolStripMenuFolder_Click(object sender, EventArgs e)
		{
			/*  try
              {
                  string path = Path.Combine(Application.StartupPath, "DATA");
                  if (Directory.Exists(path) == false)
                  {
                      Directory.CreateDirectory(path);
                  }
                  System.Diagnostics.Process.Start("explorer.exe", path);
              }
              catch
              {
                  Global.Forms.Msg.MessageTopMost("打开数据文件夹出错", false, false, false);
              }*/
		}



		private delegate void delShowSubWindow(int wndIndex, bool isNeedPermit);
		public void ShowSubWindowDel(int wndIndex, bool isNeedPermit = false)
		{
			if (this.IsHandleCreated)
			{
				this.Invoke(new delShowSubWindow(ShowSubWindow), wndIndex, isNeedPermit);
			}
		}
		private void ShowSubWindow(int wndIndex, bool isNeedPermit = false)
		{
			if (isNeedPermit)//权限限制
			{
				if (Global.CConst.UserLevel < Global.CConst.USER_ADMINISTOR)
				{
					Global.Forms.Msg.MessageTopMost("你没有权限进行此操作，请先登录", false, false, false);
					return;
				}
			}
			if (Global.CConst.Form_Var == wndIndex)
				return;

			this.fMain.Hide();
			this.fDebug.Hide();
			this.fAlarm.Hide();
			this.toolStripMenuMain.Image = global::StrongProject.Properties.Resources.Home;
			this.toolStripMenuDebug.Image = global::StrongProject.Properties.Resources.Set;
			this.toolStripMenuAlarm.Image = global::StrongProject.Properties.Resources.Alarm;
			switch (wndIndex)
			{
				case Global.CConst.FRM_MAIN:
					this.fMain.Location = new Point(0, 0);
					this.fMain.Show();
					Global.CConst.Form_Var = Global.CConst.FRM_MAIN;
					this.toolStripMenuMain.Image = global::StrongProject.Properties.Resources.Home_sel;
					break;
				case Global.CConst.FRM_DEBUG:
					this.fDebug.Location = new Point(0, 0);
					this.fDebug.Show();
					Global.CConst.Form_Var = Global.CConst.FRM_DEBUG;
					this.toolStripMenuDebug.Image = global::StrongProject.Properties.Resources.Set__sel;
					break;
				case Global.CConst.FORM_ALARM:
					this.fAlarm.Location = new Point(0, 0);
					this.fAlarm.Show();
					Global.CConst.Form_Var = Global.CConst.FORM_ALARM;
					this.toolStripMenuAlarm.Image = global::StrongProject.Properties.Resources.Alarm_sel;
					break;
				default:
					break;
			}
		}


		private void Frm_Frame_SizeChanged(object sender, EventArgs e)
		{
			if (myAutoSize != null)
				myAutoSize.AutoSizeFormsContrl(this.panelForm);
		}

		private void Frm_Frame_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBoxLog.Show("确定要退出程序吗?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				IOParameter redlight = StationManage.FindOutputIo("主控系统", "三色灯_红");
				IOParameter yellowlight = StationManage.FindOutputIo("主控系统", "三色灯_黄");
				IOParameter greenlight = StationManage.FindOutputIo("主控系统", "三色灯_绿");
				NewCtrlCardV0.SetOutputIoBit(redlight, 0);
				NewCtrlCardV0.SetOutputIoBit(yellowlight, 0);
				NewCtrlCardV0.SetOutputIoBit(greenlight, 0);
				Global.WorkVar.tag_ButtonStopState = 1;
				tag_work.Stop();
				NewCtrlCardV0.CloseCard(tag_work.tag_CardHave);
				// Dmc1000.d1000_board_close();
				//    LTDMC.dmc_board_close();
			}
			else
			{
				e.Cancel = true;
			}
		}

		private void Frm_Frame_FormClosed(object sender, FormClosedEventArgs e)
		{

			Application.Exit();
		}


		private void timer1_Tick(object sender, EventArgs e)
		{
			lblDay.Text = "当前时间:" + DateTime.Now.ToString();

			if (Global.CConst.UserLevel == Global.CConst.USER_OPERATOR)
			{
				this.toolStripMenuLogin.Image = global::StrongProject.Properties.Resources.User;
				lbl_User.Text = "权限：操作员";
			}
			else
			{
				this.toolStripMenuLogin.Image = global::StrongProject.Properties.Resources.User_sel;
				if (Global.CConst.UserLevel == Global.CConst.USER_ADMINISTOR)
				{
					lbl_User.Text = "权限：管理员";
				}
				if (Global.CConst.UserLevel == Global.CConst.USER_SUPERADMIN)
				{
					lbl_User.Text = "权限：超级管理员";
				}
			}

			if (Global.WorkVar.tag_ResetState != 1)
			{
				toolStripMenuReset.Image = global::StrongProject.Properties.Resources.GoHome_Gray;
			}
			else
			{
				toolStripMenuReset.Image = global::StrongProject.Properties.Resources.GoHome;
			}

			if (Global.WorkVar.tag_SuspendState == 1)
			{
				toolStripMenuStop.Image = global::StrongProject.Properties.Resources.Pause;
			}
			else
			{
				toolStripMenuStop.Image = global::StrongProject.Properties.Resources.Pause_Gray;

			}
			if (Global.WorkVar.tag_workState == 1)
			{
				toolStripMenuStart.Image = global::StrongProject.Properties.Resources.Run;
			}
			else
			{
				toolStripMenuStart.Image = global::StrongProject.Properties.Resources.Run_Gray;
			}
			if (Global.WorkVar.tag_StopState == 0)
			{
				toolStripMenuEStop.Image = global::StrongProject.Properties.Resources.Stop_Gray;

			}
			else
			{
				toolStripMenuEStop.Image = global::StrongProject.Properties.Resources.Stop;
			}

			if (Global.WorkVar.tag_StopState != 0)
			{
				//  label_state.Text = "急停中";
				//  label_state.Image = global::StrongProject.Properties.Resources.bigbk_red;
			}
			else
			{
				if (Global.WorkVar.tag_ResetState == 1)
				{
					//      label_state.Text = "复位中";
					//      label_state.Image = global::StrongProject.Properties.Resources.bigbk;
				}
				else
				{
					if (Global.WorkVar.tag_ResetState == 0)
					{
						//       label_state.Text = "请复位";
						//       label_state.Image = global::StrongProject.Properties.Resources.bigbk;
					}
					else
					{
						if (Global.WorkVar.tag_workState == 1)
						{
							if (Global.WorkVar.tag_SuspendState == 1)
							{
								//            label_state.Text = "暂停中";
								//            label_state.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
							}
							else
							{
								if (Global.WorkVar.bEmptyRun)
								{
									//               label_state.Text = "空跑中";
								}
								else
								{
									//              label_state.Text = "工作中";
								}
								//          label_state.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
							}
						}
						else
						{
							//      label_state.Text = "待机中";
							//      label_state.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
						}
					}
				}
			}
		}



		private void toolStripMenuReset_Click(object sender, EventArgs e)
		{
			tag_work.Rest();
		}

		private void toolStripMenuStart_Click(object sender, EventArgs e)
		{
			if (Global.WorkVar.tag_workState == 0)
			{
				if (Global.WorkVar.tag_ResetState != 2)
				{
					MessageBoxLog.Show("请复位");
				}
				else
				{
					Global.WorkVar.tag_IsExit = 0;
					Global.WorkVar.bcanRunFalg = true;
				}
			}
			else
			{
				Global.WorkVar.bcanRunFalg = false;
				Global.WorkVar.tag_IsExit = 1;
				Global.WorkVar.tag_ResetState = 0;
				Global.WorkVar.tag_workState = 0;
			}
		}

		private void toolStripMenuStop_Click(object sender, EventArgs e)
		{
			if (Global.WorkVar.tag_SuspendState == 1)
			{
				if (Global.WorkVar.tag_ResetState != 1)
				{
					IOParameter redlight = StationManage.FindOutputIo("主控系统", "三色灯_红");
					IOParameter yellowlight = StationManage.FindOutputIo("主控系统", "三色灯_黄");
					IOParameter greenlight = StationManage.FindOutputIo("主控系统", "三色灯_绿");
					NewCtrlCardV0.SetOutputIoBit(redlight, 0);
					NewCtrlCardV0.SetOutputIoBit(yellowlight, 0);
					NewCtrlCardV0.SetOutputIoBit(greenlight, 1);
				}
				tag_work.Continue(null);
			}
			else
			{
				if (Global.WorkVar.tag_ResetState != 1)
				{
					IOParameter redlight = StationManage.FindOutputIo("主控系统", "三色灯_红");
					IOParameter yellowlight = StationManage.FindOutputIo("主控系统", "三色灯_黄");
					IOParameter greenlight = StationManage.FindOutputIo("主控系统", "三色灯_绿");
					NewCtrlCardV0.SetOutputIoBit(redlight, 0);
					NewCtrlCardV0.SetOutputIoBit(yellowlight, 1);
					NewCtrlCardV0.SetOutputIoBit(greenlight, 0);
				}
				tag_work.Suspend(null);
			}
		}

		private void toolStripMenuEStop_Click(object sender, EventArgs e)
		{
			IOParameter redlight = StationManage.FindOutputIo("主控系统", "三色灯_红");
			IOParameter yellowlight = StationManage.FindOutputIo("主控系统", "三色灯_黄");
			IOParameter greenlight = StationManage.FindOutputIo("主控系统", "三色灯_绿");
			//IOParameter fengmingqi = StationManage.FindOutputIo("主控系统", "蜂鸣器");
			NewCtrlCardV0.SetOutputIoBit(redlight, 1);
			NewCtrlCardV0.SetOutputIoBit(yellowlight, 0);
			NewCtrlCardV0.SetOutputIoBit(greenlight, 0);
			Global.WorkVar.tag_ButtonStopState = 1;
			//NewCtrlCardSR.SetOutputIoBit(fengmingqi, 1);
			tag_work.Stop();
		}

		private void toolStripMenuLogin_Click_1(object sender, EventArgs e)
		{
			LogIn frmLogin = new LogIn(this);
			frmLogin.ShowDialog();
		}



		public void LightandBuzzer(string lightColor, bool bBuzzer = false)
		{
			IOParameter redlight = StationManage.FindOutputIo("主控系统", "三色灯_红");
			IOParameter yellowlight = StationManage.FindOutputIo("主控系统", "三色灯_黄");
			IOParameter greenlight = StationManage.FindOutputIo("主控系统", "三色灯_绿");
			IOParameter buzzer = StationManage.FindOutputIo("主控系统", "蜂鸣器");
			NewCtrlCardV0.SetOutputIoBit(redlight, 0);
			NewCtrlCardV0.SetOutputIoBit(yellowlight, 0);
			NewCtrlCardV0.SetOutputIoBit(greenlight, 0);
			if (bBuzzer)
			{
				NewCtrlCardV0.SetOutputIoBit(buzzer, 1);
			}
			if (lightColor == "红灯")
			{
				NewCtrlCardV0.SetOutputIoBit(redlight, 1);
			}
			else if (lightColor == "黄灯")
			{
				NewCtrlCardV0.SetOutputIoBit(yellowlight, 1);
			}
			else if (lightColor == "绿灯")
			{
				NewCtrlCardV0.SetOutputIoBit(greenlight, 1);
			}
		}
	}
}