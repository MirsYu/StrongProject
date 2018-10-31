using System;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class AddAxisForm : Form
	{
		public Work _Worker = null;
		public AddAxisForm(Work Worker)
		{
			_Worker = Worker;
			InitializeComponent();
		}

		private void AddAxisForm_Load(object sender, EventArgs e)
		{
			if (_Worker._Config.axisArray != null)
			{
				int i = 0;
				if (i == 0)
				{
					AxisLabel axisL = new AxisLabel();
					panel_Axis.Controls.Add(axisL);
					axisL.Location = new Point(42, 5);
				}
				while (i < _Worker._Config.axisArray.Count)
				{

					SetAxisConfig setC = new SetAxisConfig();
					panel_Axis.Controls.Add((Control)setC);
					setC.Location = new Point(3, 32 + i * setC.Height + i * 5);
					setC.AxisSet = _Worker._Config.axisArray[i];
					i++;

				}
			}
		}
		private void button_AddAxis_Click(object sender, EventArgs e)
		{

			AxisConfig axis = new AxisConfig();
			axis.AxisIndex = _Worker._Config.axisArray.Count; ;
			_Worker._Config.axisArray.Add(axis);
			SetAxisConfig setC = new SetAxisConfig();
			panel_Axis.Controls.Add((Control)setC);
			setC.Location = new Point(3, 32 + axis.AxisIndex * setC.Height + axis.AxisIndex * 5);
			setC.AxisSet = axis;


		}

		private void button9_Click(object sender, EventArgs e)
		{
			foreach (Control con in panel_Axis.Controls)
			{
				if (con is SetAxisConfig)
				{
					SetAxisConfig setA = (SetAxisConfig)con;
					setA.Save();
				}
			}
		}

		private void button_RemoveAxis_Click(object sender, EventArgs e)
		{
			Control[] finChild = new Control[CardManager.iMaxAxisCount];
			int i = 0;
			foreach (Control con in panel_Axis.Controls)
			{
				if (con is SetAxisConfig)
				{
					finChild[i] = con;
					i++;
				}
			}
			if (i > 0)
			{
				SetAxisConfig sac = (SetAxisConfig)finChild[i - 1];
				panel_Axis.Controls.Remove(sac);
				_Worker._Config.axisArray.Remove(sac.AxisSet);

			}
			else
			{
				MessageBoxLog.Show("轴已清空！");
			}
		}
	}
}
