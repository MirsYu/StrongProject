using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class ucl_InIo : UserControl
	{
		public InIOParameterPoint tag_io;

		public int tag_type;
		public ucl_InIo()
		{
			InitializeComponent();
		}
		public void show(InIOParameterPoint _io)
		{
			tag_io = _io;
			if (_io == null)
			{
				if (label_name.Text != null)
				{
					checkBox1.Checked = false;
				}
				return;
			}

			label_name.Text = tag_io.tag_IOName;
			if (label_name.Text == null || label_name.Text.Length < 1)
			{
				checkBox1.Checked = false;
				return;
			}
			if (_io.tag_var)
			{
				comboBox_poe.SelectedIndex = 1;
			}
			else
			{
				comboBox_poe.SelectedIndex = 0;
			}
			tbxTime.Text = tag_io.tag_IOParameterOutTime.ToString();
			if (label_name.Text != null)
			{
				checkBox1.Checked = true;
			}

		}

		private void comboBox_poe_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox_poe.SelectedIndex == 0)
			{
				tag_io.tag_var = false;
			}
			else
			{
				tag_io.tag_var = true;
			}
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			try
			{
				if (checkBox1.Checked == true)
				{

					tag_io.tag_IOParameterOutTime = long.Parse(tbxTime.Text);
				}
				else
				{


					label_name.Text = "请选择输出IO";
					tag_io.tag_IOName = null;

				}
			}
			catch
			{ }
		}

		private void tbxTime_TextChanged(object sender, EventArgs e)
		{
			try
			{
				tag_io.tag_IOParameterOutTime = long.Parse(tbxTime.Text);
			}
			catch
			{ }
		}


	}
}
