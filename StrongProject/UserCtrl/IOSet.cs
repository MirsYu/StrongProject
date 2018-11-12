using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class IOSet : UserControl
	{
		public List<SetIOPanel> tag_list;
		public IOSet()
		{
			tag_list = new List<SetIOPanel>();
			InitializeComponent();
		}
		public void save()
		{
			if (MessageBoxLog.Show("是否SAVE", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			foreach (SetIOPanel sip in tag_list)
			{
				sip.Save();
			}
			StationManage._Config.Save();
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
		private void IOSet_Load(object sender, EventArgs e)
		{


			if (StationManage._Config == null || StationManage._Config.arrWorkStation == null)
				return;
			int i = 0;
			int j = 0;
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
					SetIOPanel ioinput = new SetIOPanel();
					ioinput.IO = ioP;
					ioinput.Location = new Point(i % 2 * 340, i / 2 * 30);
					panel_IO.Controls.Add(ioinput);
					tag_list.Add(ioinput);
					i++;
				}
				foreach (IOParameter ioP in sm.arrOutputIo)
				{
					if (IoisInList(listObject, ioP))
					{
						continue;
					}
					listObject.Add(ioP);
					SetIOPanel ioinput = new SetIOPanel();
					ioinput.IO = ioP;
					ioinput.Location = new Point(0, j * 30);
					panel_out.Controls.Add(ioinput);
					tag_list.Add(ioinput);
					j++;


				}

			}

		}

		public void IOSet_Load_Station(string stationName)
		{


			if (StationManage._Config == null || StationManage._Config.arrWorkStation == null)
				return;
			int i = 0;
			int j = 0;
			List<object> listObject = new List<object>();
			panel_out.Controls.Clear();
			panel_IO.Controls.Clear();
			tag_list.Clear();
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				if (sm.strStationName == stationName)
				{
					foreach (IOParameter ioP in sm.arrInputIo)
					{
						if (IoisInList(listObject, ioP))
						{
							continue;
						}
						listObject.Add(ioP);
						SetIOPanel ioinput = new SetIOPanel();
						ioinput.IO = ioP;
						ioinput.Location = new Point(i % 2 * 340, i / 2 * 30);
						panel_IO.Controls.Add(ioinput);
						tag_list.Add(ioinput);
						i++;
					}
					foreach (IOParameter ioP in sm.arrOutputIo)
					{
						if (IoisInList(listObject, ioP))
						{
							continue;
						}
						listObject.Add(ioP);
						SetIOPanel ioinput = new SetIOPanel();
						ioinput.IO = ioP;
						ioinput.Location = new Point(0, j * 30);
						panel_out.Controls.Add(ioinput);
						tag_list.Add(ioinput);
						j++;


					}

				}
			}

		}
	}
}
