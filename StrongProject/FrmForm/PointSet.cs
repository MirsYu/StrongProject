using System;
using System.Drawing;
using System.Windows.Forms;



namespace StrongProject
{
	public partial class PointSet : Form
	{
		public PointAggregate _PointA;
		//public DebugFrmSet debugF;
		public PointSet(PointAggregate pointA)
		{
			_PointA = pointA;
			InitializeComponent();

			//debugF=dbf;
		}

		public void InitForm()
		{
			if (_PointA == null)
			{
				return;
			}
			StationModule _stationM = StationManage.FindStation(_PointA.strStationName);
			if (_stationM != null)
			{
				for (int i = 0; i < _stationM.arrAxis.Count; i++)
				{
					QueryPointInfo queryP = new QueryPointInfo(_stationM.arrAxis[i]);
					queryP.PointM = _PointA.arrPoint[i];
					queryP.lblAxisName.Text = _stationM.arrAxis[i].AxisName;


					queryP.Location = new Point(3, 3 + i * queryP.Height + i * 5);
					panel1.Controls.Add(queryP);

				}
			}
		}


		private void PointSet_Load(object sender, EventArgs e)
		{
			InitForm();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			foreach (Control con in panel1.Controls)
			{
				if (con is QueryPointInfo)
				{
					QueryPointInfo queryP = (QueryPointInfo)con;
					queryP.Save();
				}
			}
		}
	}
}
