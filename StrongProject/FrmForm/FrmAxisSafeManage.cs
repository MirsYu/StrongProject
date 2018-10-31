using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class FrmAxisSafeManage : Form
	{
		/// <summary>
		///点位
		/// </summary>
		public PointAggregate tag_PointAggregate;
		/// <summary>
		/// 
		/// </summary>
		public List<UserControl_AxisSafe> tag_AxisSafeList;
		/// <summary>
		/// 
		/// </summary>
		public FrmAxisSafeManage()
		{
			InitializeComponent();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="pa"></param>
		public FrmAxisSafeManage(PointAggregate pa)
		{
			tag_PointAggregate = pa;
			tag_AxisSafeList = new List<UserControl_AxisSafe>();
			InitializeComponent();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmAxisSafeManage_Load(object sender, EventArgs e)
		{
			listBox2.Items.Clear();
			listBox1.Items.Clear();
			groupBox1.Controls.Clear();

			foreach (AxisConfig ac in StationManage._Config.axisArray)
			{

				listBox2.Items.Add(ac.AxisName);
			}

			if (tag_PointAggregate != null)
			{

				int i = 0;
				if (tag_PointAggregate.tag_AxisSafeManage == null)
				{
					tag_PointAggregate.tag_AxisSafeManage = new AxisSafeManage(tag_PointAggregate);
				}
				foreach (AxisSafe asf in tag_PointAggregate.tag_AxisSafeManage.tag_AxisSafeList)
				{
					string AsixName = null;

					foreach (AxisConfig ac in StationManage._Config.axisArray)
					{

						int axis = (int)ac.tag_MotionCardManufacturer * 1000 + ac.CardNum * 100 + ac.AxisNum;
						if (axis == asf.tag_AxisId)
						{
							AsixName = ac.AxisName;
							break;
						}
					}
					listBox1.Items.Add(AsixName);
					UserControl_AxisSafe uscAs = new UserControl_AxisSafe(asf, AsixName);
					uscAs.Location = new Point(10, 30 + i * uscAs.Size.Height);
					tag_AxisSafeList.Add(uscAs);
					i++;
					groupBox1.Controls.Add(uscAs);
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{

			if (MessageBoxLog.Show("是否全部保存", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}

			foreach (UserControl_AxisSafe usc_ax in tag_AxisSafeList)
			{
				usc_ax.Save(false);
			}
		}

		private void comboBox_Station_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{
				AxisConfig axis = null;
				string axisanem = null;
				foreach (StationModule sm in StationManage._Config.arrWorkStation)
				{
					int i = 0;
					if (!string.IsNullOrEmpty(sm.strStationName))
					{
						for (i = 0; i < sm.intUseAxisCount; i++)
						{
							string axisFind = sm.arrAxis[i].AxisName;
							if (axisFind == listBox2.SelectedItem.ToString())
							{
								axis = sm.arrAxis[i];
								axisanem = axisFind;
								break;
							}

						}
					}
					if (axis != null)
						break;
				}

				foreach (AxisSafe asf in tag_PointAggregate.tag_AxisSafeManage.tag_AxisSafeList)
				{
					int axisid = ((int)(axis.tag_MotionCardManufacturer)) * 1000 + axis.CardNum * 100 + axis.AxisNum;
					if (axisid == asf.tag_AxisId)
					{
						return;
					}
				}
				tag_PointAggregate.tag_AxisSafeManage.Add((short)axis.tag_MotionCardManufacturer, axis.CardNum, axis.AxisNum);
				listBox1.Items.Add(axisanem);

			}
			catch
			{ }
		}

		private void button4_Click(object sender, EventArgs e)
		{
			FrmAxisSafeManage_Load(null, null);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex > -1)
			{
				tag_PointAggregate.tag_AxisSafeManage.tag_AxisSafeList.RemoveAt(listBox1.SelectedIndex);
				listBox1.Items.RemoveAt(listBox1.SelectedIndex);
			}
		}
	}
}
