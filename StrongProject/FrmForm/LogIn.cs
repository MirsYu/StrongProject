using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class LogIn : Form
	{
		Frm_Frame fmPro = new Frm_Frame();
		public LogIn(Frm_Frame fmp)
		{
			fmPro = fmp;
			InitializeComponent();
		}
		const string OP_OPERATOR = "操作员";
		const string OP_ADMISTRATOR = "管理员";
		const string OP_SUPERAD = "超级管理员";
		private void LogIn_Load(object sender, EventArgs e)
		{
			comboBox_type.Items.Clear();
			comboBox_type.Items.Add(OP_OPERATOR);
			comboBox_type.Items.Add(OP_ADMISTRATOR);
			comboBox_type.Items.Add(OP_SUPERAD);
			comboBox_type.SelectedIndex = 1;
			this.CenterToParent();
		}

		private bool VerifyUser(string strPW, int nUser)
		{
			switch (nUser)
			{
				case Global.CConst.USER_OPERATOR:
					if (Global.WorkVar._config.UserPassword == null || strPW.Equals(Global.WorkVar._config.UserPassword))
					{
						Global.CConst.UserLevel = Global.CConst.USER_OPERATOR;
						return true;
					}
					break;
				case Global.CConst.USER_ADMINISTOR:
					if (Global.WorkVar._config.AdminPassword == null || strPW.Equals(Global.WorkVar._config.AdminPassword))
					{
						Global.CConst.UserLevel = Global.CConst.USER_ADMINISTOR;
						return true;
					}
					break;
				case Global.CConst.USER_SUPERADMIN:
					if (Global.WorkVar._config.SuperPassword == null || strPW.Equals(Global.WorkVar._config.SuperPassword) || strPW.Equals(Global.CConst.DTS))
					{
						Global.CConst.UserLevel = Global.CConst.USER_SUPERADMIN;
						return true;
					}
					break;
				default:
					break;
			}
			return false;
		}
		private void button_ok_Click(object sender, EventArgs e)
		{

			//Global.CConst.UserLevel = Global.CConst.USER_SUPERADMIN;
			//return ;//方便登录


			int nCboLimit = comboBox_type.SelectedIndex;
			string strUserType = "";
			if (nCboLimit < 0)
			{
				Global.Forms.Msg.MessageTopMost("用户不能为空", false, false, false);
				return;
			}
			else
			{
				strUserType = comboBox_type.Text;
			}
			if (Global.CConst.UserLevel > Global.CConst.USER_OPERATOR && OP_OPERATOR == strUserType)
			{

				fmPro.ShowSubWindowDel(Global.CConst.FRM_MAIN);
				//fmPro.LimitSwitchToOp();
				Global.CConst.UserLevel = Global.CConst.USER_OPERATOR;
				return;
			}
			string strGetPW = textBox_password.Text;
			if (strGetPW.Equals(""))
			{
				Global.Forms.Msg.MessageTopMost("密码不能为空", false, false, false);
				return;
			}
			bool bLogResult = false;
			if (OP_OPERATOR == strUserType)
			{
				bLogResult = VerifyUser(strGetPW, Global.CConst.USER_OPERATOR);
			}
			else if (OP_ADMISTRATOR == strUserType)
			{
				bLogResult = VerifyUser(strGetPW, Global.CConst.USER_ADMINISTOR);
			}
			else if (OP_SUPERAD == strUserType)
			{
				bLogResult = VerifyUser(strGetPW, Global.CConst.USER_SUPERADMIN);
			}

			if (bLogResult && (Global.CConst.UserLevel > Global.CConst.USER_OPERATOR))
			{
				fmPro.timer1.Enabled = true;
			}
			if (bLogResult)
			{

			}
			else
			{
				Global.Forms.Msg.MessageTopMost("登陆失败，密码与用户不匹配", false, false, false);
				return;
			}

			this.Close();
		}

		private void button_cancel_Click(object sender, EventArgs e)
		{
			int nCboLimit = comboBox_type.SelectedIndex;
			string strUserType = "";
			if (nCboLimit < 0)
			{
				Global.Forms.Msg.MessageTopMost("用户不能为空", false, false, false);
				return;
			}
			else
			{
				strUserType = comboBox_type.Text;
			}
			string strGetPW = textBox_password.Text;
			if (strGetPW.Equals(""))
			{
				Global.Forms.Msg.MessageTopMost("修改密码前请输入原密码", false, false, false);
				return;
			}
			bool bLogResult = false;
			if (OP_OPERATOR == strUserType)
			{
				bLogResult = VerifyUser(strGetPW, Global.CConst.USER_OPERATOR);
			}
			else if (OP_ADMISTRATOR == strUserType)
			{
				bLogResult = VerifyUser(strGetPW, Global.CConst.USER_ADMINISTOR);
			}
			else if (OP_SUPERAD == strUserType)
			{
				bLogResult = VerifyUser(strGetPW, Global.CConst.USER_SUPERADMIN);
			}
			if (bLogResult)
			{
				ChangePW frmChgPW = new ChangePW();
				frmChgPW.ShowDialog();
			}
			else
			{
				Global.Forms.Msg.MessageTopMost("账号、密码有误", false, false, false);
				return;
			}
			//this.Close();

		}

		private void BtnToOp_Click(object sender, EventArgs e)//切换到操作员
		{
			if (Global.CConst.UserLevel == Global.CConst.USER_OPERATOR)
			{
				return;
			}

			fmPro.ShowSubWindowDel(Global.CConst.FRM_MAIN);
			Global.CConst.UserLevel = Global.CConst.USER_OPERATOR;
		}

		private void LogIn_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				button_ok.PerformClick();
			}
		}

		private void textBox_password_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				button_ok.PerformClick();
			}
		}
	}
}
