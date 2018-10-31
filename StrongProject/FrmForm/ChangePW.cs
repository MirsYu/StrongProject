using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class ChangePW : Form
	{
		public ChangePW()
		{
			InitializeComponent();
		}

		private void changePassword(TextBox txtpw1, TextBox txtpw2, int nUser)
		{
			if (nUser > Global.CConst.UserLevel)
			{
				Global.Forms.Msg.MessageTopMost("当前权限比要修改密码的权限低，不能执行此操作!", false, false, false);
				return;
			}
			if (txtpw1.Text.Trim() != txtpw2.Text.Trim())
			{
				Global.Forms.Msg.MessageTopMost("两次输入密码不一致!", false, false, false);
				return;
			}
			switch (nUser)
			{
				case Global.CConst.USER_OPERATOR:
					Global.WorkVar._config.UserPassword = txtpw1.Text.Trim();
					break;
				case Global.CConst.USER_ADMINISTOR:
					Global.WorkVar._config.AdminPassword = txtpw1.Text.Trim();
					break;
				case Global.CConst.USER_SUPERADMIN:
					Global.WorkVar._config.SuperPassword = txtpw1.Text.Trim();
					break;
				default:
					break;
			}
			Global.WorkVar._config.Save();
			Global.Forms.Msg.MessageTopMost("修改密码成功!", false, false, false);
		}
		private void button_superpassword_Click(object sender, EventArgs e)
		{
			changePassword(textBox_superpassword, textBox_superpassword_, Global.CConst.USER_SUPERADMIN);
		}

		private void button_adminpassword_Click(object sender, EventArgs e)
		{
			changePassword(textBox_adminpassword, textBox_adminpassword_, Global.CConst.USER_ADMINISTOR);
		}

		private void button_userpassword_Click(object sender, EventArgs e)
		{
			changePassword(textBox_userpassword, textBox_userpassword_, Global.CConst.USER_OPERATOR);
		}
		private void ChangPW_Load(object sender, EventArgs e)
		{

		}
	}
}
