using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class UserControl1_offset : UserControl
	{
		public UserControl1_offset()
		{
			InitializeComponent();
		}
		public Work tag_work;
		private void UserControl1_offset_Load(object sender, EventArgs e)
		{
			if (tag_work == null)
			{
				return;
			}
			//textBox_X1.Text = tag_work._Config.tag_PrivateSave.tag_Cam4_x1.ToString();
			//textBox_Y1.Text = tag_work._Config.tag_PrivateSave.tag_Cam4_y1.ToString();
			//textBox_A1.Text = tag_work._Config.tag_PrivateSave.tag_Cam4_a1.ToString();
			//textBox_x2.Text = tag_work._Config.tag_PrivateSave.tag_Cam4_x2.ToString();
			//textBox_Y2.Text = tag_work._Config.tag_PrivateSave.tag_Cam4_y2.ToString();
			//textBox_A2.Text = tag_work._Config.tag_PrivateSave.tag_Cam4_a2.ToString();

			//textBox_PMWLXoffset_2.Text = tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_2.ToString();
			//textBox_PMWLYoffset_3.Text = tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_3.ToString();
			//textBox_PMWLYoffset_4.Text = tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_4.ToString();
			//textBox_PMWLXoffset_5.Text = tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_5.ToString();
			//textBox_PMWLXoffset_6.Text = tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_6.ToString();
			//textBox_PMWLYoffset_1.Text = tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_1.ToString();
			//textBox_PMWLYoffset_2.Text = tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_2.ToString();
			//textBox_PMWLXoffset_3.Text = tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_3.ToString();
			//textBox_PMWLXoffset_4.Text = tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_4.ToString();
			//textBox_PMWLYoffset_5.Text = tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_5.ToString();
			//textBox_PMWLYoffset_6.Text = tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_6.ToString();



			num_CutterCY.Value = Convert.ToDecimal(tag_work._Config.tag_PrivateSave.fCutterCYTime);
			num_TestCY.Value = Convert.ToDecimal(tag_work._Config.tag_PrivateSave.fTestCYTime);

		}

		private void button1_Save_Click(object sender, EventArgs e)
		{
			if (MessageBoxLog.Show("确定要保存数据？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{

				try
				{
					//tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_1 = double.Parse(textBox_PMWLXoffset_1.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_2 = double.Parse(textBox_PMWLXoffset_2.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_3 = double.Parse(textBox_PMWLXoffset_3.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_4 = double.Parse(textBox_PMWLXoffset_4.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_5 = double.Parse(textBox_PMWLXoffset_5.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLXoffset_6 = double.Parse(textBox_PMWLXoffset_6.Text);


					//tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_1 = double.Parse(textBox_PMWLYoffset_1.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_2 = double.Parse(textBox_PMWLYoffset_2.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_3 = double.Parse(textBox_PMWLYoffset_3.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_4 = double.Parse(textBox_PMWLYoffset_4.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_5 = double.Parse(textBox_PMWLYoffset_5.Text);
					//tag_work._Config.tag_PrivateSave.tag_PMWLYoffset_6 = double.Parse(textBox_PMWLYoffset_6.Text);

					//tag_work._Config.tag_PrivateSave.tag_PCXUp = double.Parse(textBox_PCXUp.Text);
					//tag_work._Config.tag_PrivateSave.tag_PCXDown = double.Parse(textBox_PCXDown.Text);
					//tag_work._Config.tag_PrivateSave.tag_PCYUp = double.Parse(textBox_PCYUp.Text);
					//tag_work._Config.tag_PrivateSave.tag_PCYDown = double.Parse(textBox_PCYDown.Text);
					//tag_work._Config.tag_PrivateSave.tag_LongthUp = double.Parse(textBox_LongthUp.Text);
					//tag_work._Config.tag_PrivateSave.tag_LongthDown = double.Parse(textBox_LongthDown.Text);
					//tag_work._Config.tag_PrivateSave.tag_WideUp = double.Parse(textBox_WideUp.Text);
					//tag_work._Config.tag_PrivateSave.tag_WideDown = double.Parse(textBox_WideDown.Text);

					//tag_work._Config.tag_PrivateSave.tag_NGPZLocationTimes = int.Parse(textBox_NGPZLocationTimes.Text);
					//tag_work._Config.tag_PrivateSave.tag_NGFJTimes = int.Parse(textBox_NGFJTimes.Text);
					//tag_work._Config.tag_PrivateSave.tag_ScanTimes = int.Parse(textBox_ScanTimes.Text);

					//tag_work._Config.tag_PrivateSave.tag_Cam4_x1 = int.Parse(textBox_X1.Text);

					//tag_work._Config.tag_PrivateSave.tag_Cam4_y1 = int.Parse(textBox_Y1.Text);
					//tag_work._Config.tag_PrivateSave.tag_Cam4_a1 = int.Parse(textBox_A1.Text);
					//tag_work._Config.tag_PrivateSave.tag_Cam4_x2 = int.Parse(textBox_x2.Text);
					//tag_work._Config.tag_PrivateSave.tag_Cam4_y2 = int.Parse(textBox_Y2.Text);
					//tag_work._Config.tag_PrivateSave.tag_Cam4_a2 = int.Parse(textBox_A2.Text);

					tag_work._Config.tag_PrivateSave.fCutterCYTime = Convert.ToDouble(num_CutterCY.Value);
					tag_work._Config.tag_PrivateSave.fTestCYTime = Convert.ToDouble(num_TestCY.Value);



					tag_work._Config.Save();
				}
				catch (Exception mess)
				{
					UserControl_LogOut.OutLog(mess.Message, 0);
				}

			}
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}
	}
}
