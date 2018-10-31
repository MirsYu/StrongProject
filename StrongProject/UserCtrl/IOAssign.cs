using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class IOAssign : UserControl
	{
		public StationModule tag_StationModule;
		public IOAssign()
		{
			InitializeComponent();
		}

		private void IOAssign_Load(object sender, EventArgs e)
		{
			if (StationManage._Config == null)
				return;

			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				comboBox_Station.Items.Add(sm.strStationName);
				comboBox1.Items.Add(sm.strStationName);


			}
			int i = 0;
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				i = 0;

				if (!string.IsNullOrEmpty(sm.strStationName))
				{
					for (i = 0; i < sm.intUseInputIoCount; i++)
					{
						listBox1.Items.Add(sm.arrInputIo[i].StrIoName);

					}
				}
				break;

			}
		}

		private void comboBox_Station_SelectedIndexChanged(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				int i = 0;

				if (!string.IsNullOrEmpty(sm.strStationName) && (sm.strStationName == comboBox_Station.SelectedItem.ToString()))
				{
					for (i = 0; i < sm.intUseInputIoCount; i++)
					{
						listBox1.Items.Add(sm.arrInputIo[i].StrIoName);
					}
				}

			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			listBox2.Items.Clear();
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				int i = 0;

				if (!string.IsNullOrEmpty(sm.strStationName) && (sm.strStationName == comboBox1.SelectedItem.ToString()))
				{
					for (i = 0; i < sm.intUseInputIoCount; i++)
					{
						listBox2.Items.Add(sm.arrInputIo[i].StrIoName);
					}
					tag_StationModule = sm;
					break;

				}

			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				IOParameter IO = null;
				foreach (StationModule sm in StationManage._Config.arrWorkStation)
				{
					int i = 0;

					if (!string.IsNullOrEmpty(sm.strStationName))
					{
						for (i = 0; i < sm.intUseInputIoCount; i++)
						{
							string axisFind = sm.arrInputIo[i].StrIoName;
							if (axisFind == listBox1.SelectedItem.ToString())
							{
								IO = sm.arrInputIo[i];
								break;
							}

						}
					}
					if (IO != null)
						break;
				}

				foreach (IOParameter io in tag_StationModule.arrInputIo)
				{
					if (io == IO)
					{
						return;
					}
				}
				tag_StationModule.arrInputIo.Add(IO);
				tag_StationModule.intUseInputIoCount++;
				listBox2.Items.Add(IO.StrIoName);


			}
			catch
			{ }
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				IOParameter IO = null;
				int i = 0;
				if (!string.IsNullOrEmpty(tag_StationModule.strStationName))
				{
					for (i = 0; i < tag_StationModule.intUseInputIoCount; i++)
					{
						string axisFind = tag_StationModule.arrInputIo[i].StrIoName;
						if (axisFind == listBox2.SelectedItem.ToString())
						{
							IO = tag_StationModule.arrInputIo[i];
							break;
						}

					}
				}

				if (IO == null)
				{
					return;
				}
				tag_StationModule.arrInputIo.Remove(IO);
				tag_StationModule.intUseInputIoCount--;
				listBox2.Items.Remove(IO.StrIoName);


			}
			catch
			{ }
		}
	}
}
