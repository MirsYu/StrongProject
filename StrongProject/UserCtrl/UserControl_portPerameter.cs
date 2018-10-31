using System;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject.UserCtrl
{
	public partial class UserControl_portPerameter : UserControl
	{
		public Work tag_Work;
		public UserControl_portPerameter()
		{
			InitializeComponent();
		}

		private void UserControl_portPerameter_Load(object sender, EventArgs e)
		{
			int i = 0;
			if (tag_Work == null)
				return;
			groupBox1.Controls.Clear();
			foreach (PortParameter pp in tag_Work._Config.tag_PortParameterList)
			{
				CTLSerialPort ctlsp = new CTLSerialPort(pp);
				ctlsp.Location = new Point(10 + i % 2 * (ctlsp.Size.Width + 10), 10 + i / 2 * (ctlsp.Size.Height + 10));
				groupBox1.Controls.Add(ctlsp);
				i++;
			}
		}

		private void 添加一个ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int i = 0;
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (MessageBoxLog.Show("确定要添加最后一个？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}

			tag_Work._Config.tag_PortParameterList.Add(new PortParameter());
			UserControl_portPerameter_Load(null, null);
		}

		private void 删除一个ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			int i = 0;
			if (MessageBoxLog.Show("确定要删除最后一个？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			int count = tag_Work._Config.tag_PortParameterList.Count;
			if (count > 0)
				tag_Work._Config.tag_PortParameterList.RemoveAt(count - 1);
			UserControl_portPerameter_Load(null, null);
		}

		private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int i = 0;
			if (MessageBoxLog.Show("确定要保存？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			tag_Work._Config.Save();
		}

		private void UserControl_portPerameter_SizeChanged(object sender, EventArgs e)
		{
			groupBox1.Location = new Point(1, 1);
			groupBox1.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);
		}
	}
}
