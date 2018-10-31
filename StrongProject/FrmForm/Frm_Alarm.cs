using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class Frm_Alarm : Form
	{
		Frm_Frame frmFrame = new Frm_Frame();
		public Frm_Alarm()
		{
			InitializeComponent();
		}

		private void Frm_Alarm_Load(object sender, EventArgs e)
		{

			this.dataGridView1.Columns[0].HeaderCell.Value = "时间";
			this.dataGridView1.Columns[1].HeaderCell.Value = "类型";
			this.dataGridView1.Columns[2].HeaderCell.Value = "消息";

		}

		private void button1_Click(object sender, EventArgs e)
		{
			List<Log> log = UserControl_LogOut.tag_logdatabase.Get(dateTimePicker_begin.Value, dateTimePicker_end.Value);

			this.dataGridView1.Rows.Clear();
			foreach (Log l in log)
			{
				this.dataGridView1.Rows.Add(l.tag_dateTime, l.tag_id, l.tag_info);
			}
		}
	}
}
