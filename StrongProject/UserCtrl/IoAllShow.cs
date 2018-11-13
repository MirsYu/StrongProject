using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class IoAllShow : UserControl
	{
		private IOSet ioSet1;
		private IOAssign ioAssign1;
		private OutIOAssign outIOAssign1;
		public IoAllShow()
		{
			InitializeComponent();
			LoadUI();
		}
		public bool IoisInList(List<object> list_, object o)
		{
			foreach (object o1 in list_)
			{
				if (o1 == o)
					return true;

			}
			return false;
		}
		public void LoadUI()
		{
			this.ioSet1 = new IOSet();
			this.ioSet1.Location = new System.Drawing.Point(3, 3);
			this.ioSet1.Name = "ioSet1";
			this.ioSet1.Size = new System.Drawing.Size(1110, 610);
			this.ioSet1.TabIndex = 0;
			this.tabPage2.Controls.Add(this.ioSet1);
			this.tabPage3.Controls.Add(this.ioAssign1);
			this.tabPage4.Controls.Add(this.outIOAssign1);
			this.ioAssign1 = new StrongProject.IOAssign();
			this.ioAssign1.Location = new System.Drawing.Point(3, 3);
			this.ioAssign1.Name = "ioAssign1";
			this.ioAssign1.Size = new System.Drawing.Size(1110, 610);
			this.ioAssign1.TabIndex = 0;
			this.outIOAssign1 = new StrongProject.OutIOAssign();
			this.outIOAssign1.Location = new System.Drawing.Point(3, 3);
			this.outIOAssign1.Name = "outIOAssign1";
			this.outIOAssign1.Size = new System.Drawing.Size(1110, 610);
			this.outIOAssign1.TabIndex = 0;


			this.ioAssign1 = new StrongProject.IOAssign();
			this.ioAssign1.Location = new System.Drawing.Point(0, 3);
			this.ioAssign1.Name = "IOAssign1";
			this.ioAssign1.Size = new System.Drawing.Size(1032, 500);
			this.ioAssign1.TabIndex = 0;

		}
		private void IoAllShow_Load(object sender, EventArgs e)
		{

			if (StationManage._Config == null || StationManage._Config.arrWorkStation == null)
				return;
			int i = 0;
			int j = 0;
			tabPage2.Controls.Add(ioSet1);
			tabPage3.Controls.Add(ioAssign1);

			tabPage4.Controls.Add(outIOAssign1);
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				comboBox_Stat.Items.Add(sm.strStationName);

			}
			List<object> listObject = new List<object>();
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{

				foreach (IOParameter ioP in sm.arrInputIo)
				{
					if (IoisInList(listObject, ioP))
					{
						continue;
					}
					listObject.Add(ioP);
					IOinputStatus ioinput = new IOinputStatus(ioP);
					ioinput.Location = new Point(i % 4 * 160, i / 4 * 30);
					panel_IO.Controls.Add(ioinput);
					i++;
				}
				foreach (IOParameter ioP in sm.arrOutputIo)
				{
					if (IoisInList(listObject, ioP))
					{
						continue;
					}
					listObject.Add(ioP);
					IOoutputStatus ioinput = new IOoutputStatus(ioP, null);
					string car = ioP.CardNum.ToString();


					ioinput.Location = new Point(0, j * 30);
					panel_out.Controls.Add(ioinput);
					j++;


				}

			}

		}
		public string GetCarId()
		{
			switch (comboBox_motiontype.SelectedIndex)
			{
				case 0:
					return "-1";
					break;
				case 1:

					return "0";
					break;
				case 2:
					return "1";
					break;
				case 3:
					return "2";
					break;
				case 4:
					return "11";
					break;
				case 5:

					return "12";
					break;
				case 6:

					return "13";
					break;
				case 7:

					return "14";
					break;
				case 8:

					return "15";
					break;
				case 9:

					return "16";
					break;
				case 10:

					return "17";
					break;
				case 11:

					return "18";
					break;
				case 12:

					return "19";
					break;
				case 13:

					return "20";
					break;
			}
			return "-1";

		}
		private void button1_Click(object sender, EventArgs e)
		{
			int i = 0;
			int j = 0;
			panel_IO.Controls.Clear();

			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{

				foreach (IOParameter ioP in sm.arrInputIo)
				{

					IOinputStatus ioinput = new IOinputStatus(ioP);
					string car = ioP.CardNum.ToString();
					string scar = GetCarId();


					if ((scar == car) || scar == "-1")
					{
						ioinput.Location = new Point(i % 4 * 160, i / 4 * 30);
						panel_IO.Controls.Add(ioinput);
						i++;
					}

				}


			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			int i = 0;
			int j = 0;

			panel_out.Controls.Clear();
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{


				foreach (IOParameter ioP in sm.arrOutputIo)
				{

					IOoutputStatus ioinput = new IOoutputStatus(ioP, null);
					string car = ioP.CardNum.ToString();
					string scar = GetCarId();


					if ((scar == car) || scar == "-1")
					{
						ioinput.Location = new Point(0, j * 30);
						panel_out.Controls.Add(ioinput);
						j++;
					}

				}

			}
		}

		private void button_save_Click(object sender, EventArgs e)
		{
			ioSet1.save();
		}
		private void comboBox_Stat_SelectedIndexChanged(object sender, EventArgs e)
		{


		}
		private void button_IOSet_Click(object sender, EventArgs e)
		{
			ioSet1.IOSet_Load_Station(comboBox_Stat.SelectedItem.ToString());

		}

		private void panel_out_Paint(object sender, PaintEventArgs e)
		{

		}
	}
}
