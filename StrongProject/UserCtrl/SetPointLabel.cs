using System.Windows.Forms;

namespace StrongProject
{
	public partial class SetPointLabel : UserControl
	{
		public StationModule stationM;
		public SetPointLabel(StationModule sta, int axisCount)
		{
			InitializeComponent();
			stationM = sta;
			RefreshControl(axisCount);

		}

		public void RefreshControl(int axisCount)
		{
			if (stationM == null)
			{
				return;
			}

			for (int i = 0; i < axisCount; i++)
			{
				Label labAxis1 = new Label();
				labAxis1.BackColor = System.Drawing.SystemColors.AppWorkspace;
				labAxis1.Location = new System.Drawing.Point(130 + i * 100, 0);
				labAxis1.Name = "labAxis1";
				labAxis1.Size = new System.Drawing.Size(98, 16);
				labAxis1.TabIndex = 133;
				labAxis1.Text = stationM.arrAxis[i].AxisName;
				labAxis1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


				Label label1 = new Label();
				label1.AutoSize = true;
				label1.Location = new System.Drawing.Point(130 + i * 100, 16);
				label1.Name = "label1";
				label1.Size = new System.Drawing.Size(60, 12);
				label1.TabIndex = 131;
				label1.Text = "速度mm/s";
				// 
				// label3
				// 
				Label label3 = new Label();
				label3.AutoSize = true;
				label3.Location = new System.Drawing.Point(130 + i * 100 + 60, 16);
				label3.Name = "label3";
				label3.Size = new System.Drawing.Size(40, 12);
				label3.TabIndex = 130;
				label3.Text = "值mm";

				this.Controls.Add(label3);
				this.Controls.Add(label1);
				this.Controls.Add(labAxis1);
			}




		}


	}


}
