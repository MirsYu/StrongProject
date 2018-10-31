using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class FrmAxisManage : Form
	{
		public StationModule tag_StationManage;
		public FrmAxisManage(StationModule sm)
		{
			tag_StationManage = sm;
			InitializeComponent();
		}

		private void FrmAxisManage_Load(object sender, EventArgs e)
		{
			if (tag_StationManage != null)
				this.Text = tag_StationManage.strStationName;

			int i = 0;

			while (i < StationManage._Config.axisArray.Count)
			{
				listBox1.Items.Add(StationManage._Config.axisArray[i].AxisName);
				i++;
			}

			i = 0;
			if (!string.IsNullOrEmpty(tag_StationManage.strStationName))
			{
				for (i = 0; i < tag_StationManage.intUseAxisCount; i++)
				{
					listBox2.Items.Add(tag_StationManage.arrAxis[i].AxisName);

				}
			}


		}

		private void comboBox_Station_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				AxisConfig axis = null;

				int i = 0;

				while (i < StationManage._Config.axisArray.Count)
				{
					string axisFind = StationManage._Config.axisArray[i].AxisName;
					if (axisFind == listBox1.SelectedItem.ToString())
					{
						axis = StationManage._Config.axisArray[i];
						break;
					}
					i++;

				}



				foreach (AxisConfig ax in tag_StationManage.arrAxis)
				{
					if (ax == axis)
					{
						return;
					}
				}
				tag_StationManage.arrAxis.Add(axis);
				tag_StationManage.intUseAxisCount++;
				listBox2.Items.Add(axis.AxisName);


			}
			catch
			{ }
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				AxisConfig axis = null;
				int i = 0;
				if (!string.IsNullOrEmpty(tag_StationManage.strStationName))
				{
					for (i = 0; i < tag_StationManage.intUseAxisCount; i++)
					{
						string axisFind = tag_StationManage.arrAxis[i].AxisName;
						if (axisFind == listBox2.SelectedItem.ToString())
						{
							axis = tag_StationManage.arrAxis[i];
							break;
						}

					}
				}

				if (axis == null)
				{
					return;
				}
				tag_StationManage.arrAxis.Remove(axis);
				tag_StationManage.intUseAxisCount--;
				listBox2.Items.Remove(axis.AxisName);


			}
			catch
			{ }
		}
	}
}
