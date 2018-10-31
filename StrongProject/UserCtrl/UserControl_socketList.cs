using System;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class UserControl_socketList : UserControl
	{
		public Work tag_Work;
		public UserControl_socketList()
		{
			InitializeComponent();
		}

		private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (MessageBoxLog.Show("确定要删除最后一个？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			int count = tag_Work._Config.tag_IPAdrrList.Count;
			if (count > 0)
				tag_Work._Config.tag_IPAdrrList.RemoveAt(count - 1);
			UserControl_socketList_Load(null, null);
		}

		private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (MessageBoxLog.Show("确定要添加最后一个？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}

			tag_Work._Config.tag_IPAdrrList.Add(new IPAdrr());
			UserControl_socketList_Load(null, null);
		}

		private void 保存ToolStripMenuItem_Click_1(object sender, EventArgs e)
		{

			if (MessageBoxLog.Show("确定要保存？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			tag_Work._Config.Save();
		}

		private void UserControl_socketList_Load(object sender, EventArgs e)
		{
			int i = 0;
			if (tag_Work == null)
				return;
			this.Controls.Clear();
			int j = 0;
			foreach (SocketClient pp in tag_Work.tag_SocketClient)
			{
				UserControl_NetSocket ctlsp = new UserControl_NetSocket(pp);
				ctlsp.Location = new Point(10 + j % 2 * ctlsp.Size.Width, 10 + (i) / 2 * (ctlsp.Size.Height + 10));
				this.Controls.Add(ctlsp);

				j++;
				i++;

			}
		}
	}
}
