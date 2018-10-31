using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class UserControl_AxisSafe : UserControl
	{
		public AxisSafe tag_AxisSafe;
		public string tag_axisName;
		public UserControl_AxisSafe()
		{
			InitializeComponent();
		}
		public UserControl_AxisSafe(AxisSafe asf, string axisName)
		{
			tag_axisName = axisName;
			tag_AxisSafe = asf;
			InitializeComponent();
		}
		private void UserControl_AxisSafe_Load(object sender, EventArgs e)
		{
			if (tag_AxisSafe != null)
			{
				textBox_Max.Text = tag_AxisSafe.tag_max.ToString();
				textBox_Min.Text = tag_AxisSafe.tag_min.ToString();
				label_Asix.Text = tag_axisName;
			}
		}
		public void Save(bool isShowMessageBox)
		{
			if (isShowMessageBox)
			{
				if (MessageBoxLog.Show("是否保存", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
				{
					return;
				}
			}
			if (tag_AxisSafe != null)
			{
				try
				{
					tag_AxisSafe.tag_max = double.Parse(textBox_Max.Text);
					tag_AxisSafe.tag_min = double.Parse(textBox_Min.Text);
				}
				catch
				{
					UserControl_LogOut.OutLog(tag_axisName + "防呆保存失败", 0);
				}

			}
		}

		private void button_Save_Click(object sender, EventArgs e)
		{
			Save(true);
		}

	}
}
