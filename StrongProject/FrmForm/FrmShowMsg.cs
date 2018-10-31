using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class FrmShowMsg : Form
	{

		//showNoButton 有没有否按钮
		//str提示的内容
		//strTitle标题
		int Index = 0;
		int ErrCode = 0;
		public FrmShowMsg(string str, string strTitle, bool showNoButton, int ShowIndex, bool activeAlarm, int errorCode)
		{
			InitializeComponent();
			lblMsg.Text = str;
			Global.WorkVar.NowPopUpMsg = str;
			Text = strTitle;
			Index = ShowIndex;
			ErrCode = errorCode;
			if (showNoButton == false)
			{
				btnNo.Visible = false;
				btnYes.Text = "确定";
				btnYes.Left = this.Width / 2 - btnYes.Width / 2;
			}
			if (activeAlarm)
			{
				Global.WorkVar.BuzzerActiveByMsgForms = true;
			}
			if (ErrCode != 0)
			{
				//if(ECC.lstNowErrCode.Contains(errorCode)==false)
				//{
				//    ECC.lstNowErrCode.Add(ErrCode);//
				//}
				//RunA.ErrRecordDT(ErrCode);
				//RunA.ErrRecordAdd(ErrCode, false); //弹框
			}
		}

		private void btnYes_Click(object sender, EventArgs e)
		{
			Global.WorkVar.TopMostFormChooseYes = true;
			//Global.CVar.TopMostFormShowing = false ;//0217
			this.Close();
		}

		private void btnNo_Click(object sender, EventArgs e)
		{
			Global.WorkVar.TopMostFormChooseYes = false;
			//Global.CVar.TopMostFormShowing = false ;
			this.Close();
		}

		private void lblMsg_Click(object sender, EventArgs e)
		{

		}

		private void FrmShowMsg_Load(object sender, EventArgs e)
		{
			Global.WorkVar.TopMostFormShowing = true;
		}

		private void FrmShowMsg_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Global.WorkVar.TopMostFormShowing)
			{
				//Global.CVar.TopMostFormChooseYes = false;
				Global.WorkVar.TopMostFormShowing = false;
				if (Index == Global.FormFlash.CONFIRM_YES_ON)
				{
					Global.FormFlash.bConfirmYesNo = false;
				}
				else if (Index == Global.FormFlash.CONFIRM_YES)
				{
					Global.FormFlash.bConfirmYes = false;
				}
			}
			Global.WorkVar.BuzzerActiveByMsgForms = false;
			Global.WorkVar.NowPopUpMsg = "";
			//if(ErrCode!=0)
			//{
			//    RunA.ErrRecordAdd(ErrCode, true);//非弹框
			//}
		}
	}
}
