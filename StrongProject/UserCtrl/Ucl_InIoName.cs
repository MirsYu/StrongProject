using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class Ucl_InIoName : UserControl
	{
		public InIOParameterPoint tag_io;
		public int tag_type;
		public Ucl_InIoName()
		{

			InitializeComponent();
		}
		public void show(InIOParameterPoint _io, int type)
		{
			tag_io = _io;
			tag_type = type;

			label_name.Text = tag_io.tag_IOName;
			if (tag_io.tag_IOName == null)
			{
				checkBox_en.Checked = false;
			}
			else
			{
				checkBox_en.Checked = true;
			}
			textBox_WaitTime.Text = tag_io.tag_IOParameterOutTime.ToString();
			if (_io.tag_var)
			{
				comboBox_IO.SelectedIndex = 1;
			}
			else
			{
				comboBox_IO.SelectedIndex = 0;
			}
		}

		private void checkBox_en_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBox_en.Checked == true)
			{

			}
			else
			{

				label_name.Text = "请选择输出IO";

				tag_io.tag_IOName = null;

			}
		}

		private void comboBox_IO_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{

				if (comboBox_IO.SelectedIndex == 0)
				{
					tag_io.tag_var = false;
				}
				else
				{
					tag_io.tag_var = true;
				}


			}
			catch { }
		}

		private void textBox_WaitTime_TextChanged(object sender, EventArgs e)
		{
			try
			{
				tag_io.tag_IOParameterOutTime = long.Parse(textBox_WaitTime.Text);
			}
			catch
			{ }
		}
	}
}
