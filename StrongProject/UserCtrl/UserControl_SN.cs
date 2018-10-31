using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject.UserCtrl
{
	public partial class UserControl_SN : UserControl
	{
		public Work tag_Work;
		public List<TextBox> tag_listObj = new List<TextBox>();
		public int tag_snIndex = 0;
		public UserControl_SN()
		{
			InitializeComponent();
		}
		public void EventHandler(object sender, EventArgs e)
		{
			TextBox tb = (TextBox)sender;
			workBase wb = (workBase)tb.Tag;
			wb.tag_sn = tb.Text;
		}
		public void UserControl_SN_Load(object sender, EventArgs e)
		{
			if (tag_Work == null)
			{
				return;
			}
			int x = checkBox_SnEm.Location.X + checkBox_SnEm.Size.Width + 5;
			int y = (this.Size.Height - checkBox_PDCA.Size.Height) / 2;

			checkBox_PDCA.Location = new Point(checkBox_PDCA.Location.X, y);
			checkBox_SnEm.Location = new Point(checkBox_SnEm.Location.X, y);
			int i = 0;
			foreach (object o in tag_Work.tag_workObject)
			{


				workBase wb = (workBase)o;
				if (wb.tag_isRestStation == 0)
				{
					TextBox tb = new TextBox();
					Label lab = new Label();
					lab.Name = wb.tag_stationName + "SN";
					lab.Text = wb.tag_stationName + "SN";
					y = (this.Size.Height - tb.Size.Height) / 2 + 5;
					lab.Location = new Point(x, y);

					x = x + 60;

					tb.Tag = wb;
					tb.TextChanged += EventHandler;
					y = (this.Size.Height - tb.Size.Height) / 2;
					tb.Location = new Point(x, y);
					tb.Name = wb.tag_stationName;
					tb.Text = "" /*"SN123456789" + tag_snIndex*/;
					tag_snIndex++;
					x = x + 100;
					tag_listObj.Add(tb);
					this.Controls.Add(tb);
					this.Controls.Add(lab);

				}
			}
			timer1.Start();
		}

		private void checkBox_PDCA_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox_PDCA.Checked)
			{
				foreach (TextBox tb in tag_listObj)
				{
					tb.Enabled = false;
					tb.Text = "";
				}
			}
			else
			{
				foreach (TextBox tb in tag_listObj)
				{
					tb.Text = "" /*"SN123456789" + tag_snIndex*/;
					tb.Enabled = true;
					tag_snIndex++;
				}
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			if (checkBox_PDCA.Checked)
			{
				foreach (TextBox tb in tag_listObj)
				{
					workBase wb = (workBase)tb.Tag;
					if (wb.tag_sn == null || wb.tag_sn == "")
					{
						//tb.Text = wb.tag_PDCA.GetSn();

					}
				}
			}
			else
			{
				foreach (TextBox tb in tag_listObj)
				{
					workBase wb = (workBase)tb.Tag;
					tb.Text = wb.tag_sn;
				}

			}
		}

		private void checkBox_SnEm_CheckedChanged(object sender, EventArgs e)
		{
			Global.WorkVar.tag_SnIsbempty = checkBox_SnEm.Checked;
		}
	}
}
