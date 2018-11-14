using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
//using Manual_Debug.From;


namespace StrongProject
{
	public partial class SetPoint : UserControl
	{
		public SetPoint()
		{
			InitializeComponent();
		}
		public int intAxisCount = 0;
		public List<NumericUpDown> tag_list = new List<NumericUpDown>();
		private PointAggregate _PointAggregate = null;

		public PointAggregate PointAggregate
		{
			get { return _PointAggregate; }
			set
			{
				if (value != null)
				{
					_PointAggregate = value;
					InitControl();
				}
			}
		}
		public bool Save()
		{
			try
			{
				PointAggregate.strName = textBox_Name.Text;
				StationModule sm = StationManage.FindStation(PointAggregate.strStationName);
				int eucf = 1000;
				for (int i = 0; i < PointAggregate.arrPoint.Length; i++)
				{
					NumericUpDown PonitSpeed = tag_list[i * 2];
					NumericUpDown PonitValue = tag_list[i * 2 + 1];
					if (sm != null)
					{
						eucf = (int)sm.arrAxis[i].Eucf;
					}
					else
					{
						eucf = (int)1000; ;
					}
					PointAggregate.arrPoint[i].dblPonitSpeed = Convert.ToDouble(PonitSpeed.Value) * eucf;
					PointAggregate.arrPoint[i].dblPonitValue = Convert.ToDouble(PonitValue.Value);
				}

			}
			catch (System.Exception ex)
			{
				UserControl_LogOut.OutLog(ex.Message, 0);
				Console.Write(ex.ToString());
				return false;
			}
			return true;
		}
		public void ChangeControlVisable()
		{
			StationModule stationM = StationManage.FindStation(PointAggregate.strStationName);
			if (stationM != null)
			{
				foreach (Control con in this.Controls)
				{
					if (con is NumericUpDown)
					{
						if (con.Name.Length > 0 && Convert.ToInt16(con.Name.Substring(con.Name.Length - 1, 1)) > intAxisCount)
						{
							con.Visible = false;
						}
						else
						{
							con.Visible = true;
						}
					}
				}
			}
		}
		public bool InitControl()
		{

			try
			{
				textBox_Name.Text = PointAggregate.strName;

				ChangeControlVisable();
				StationModule sm = StationManage.FindStation(PointAggregate.strStationName);
				int eucf = 1000;
				for (int i = 0; i < PointAggregate.arrPoint.Length && i < intAxisCount; i++)
				{
					if (PointAggregate.arrPoint[i] == null)
						continue;

					if (sm != null)
					{
						eucf = (int)sm.arrAxis[i].Eucf;
					}
					else
					{
						eucf = (int)1000; ;
					}
					NumericUpDown numericUpDown_ASpeed1 = new NumericUpDown();
					numericUpDown_ASpeed1.Size = new Size(49, 20);
					numericUpDown_ASpeed1.Location = new Point(200 + i * 100, 0);
					numericUpDown_ASpeed1.Minimum = -999999999;
					numericUpDown_ASpeed1.Maximum = 999999999;
					numericUpDown_ASpeed1.DecimalPlaces = 3;

					tag_list.Add(numericUpDown_ASpeed1);
					this.Controls.Add(numericUpDown_ASpeed1);
					NumericUpDown numericUpDown_Vaule1 = new NumericUpDown();
					numericUpDown_Vaule1.Size = new Size(49, 20);
					numericUpDown_Vaule1.Location = new Point(200 + i * 100 + 50, 0);
					numericUpDown_Vaule1.Minimum = -999999999;
					numericUpDown_Vaule1.Maximum = 999999999;
					numericUpDown_Vaule1.DecimalPlaces = 3;
					tag_list.Add(numericUpDown_Vaule1);
					this.Controls.Add(numericUpDown_Vaule1);

					if (PointAggregate.arrPoint[i].blnPointEnable)
					{
						numericUpDown_ASpeed1.Enabled = true;
						numericUpDown_Vaule1.Enabled = true;
						numericUpDown_ASpeed1.Value = new System.Decimal(PointAggregate.arrPoint[i].dblPonitSpeed / eucf);
						numericUpDown_Vaule1.Value = new System.Decimal(PointAggregate.arrPoint[i].dblPonitValue);
					}
					else
					{
						numericUpDown_ASpeed1.Enabled = false;
						numericUpDown_Vaule1.Enabled = false;
						numericUpDown_ASpeed1.Value = new System.Decimal(PointAggregate.arrPoint[i].dblPonitSpeed / eucf);
						numericUpDown_Vaule1.Value = new System.Decimal(PointAggregate.arrPoint[i].dblPonitValue);

					}
				}


			}
			catch (System.Exception ex)
			{
				Console.Write(ex.ToString());
				return false;
			}
			return true;
		}

		public bool Refres()
		{
			try
			{
				StationModule sm = StationManage.FindStation(PointAggregate.strStationName);
				int eucf = 1000;
				PointAggregate.strName = textBox_Name.Text;
				for (int i = 0; i < PointAggregate.arrPoint.Length; i++)
				{
					NumericUpDown PonitSpeed = tag_list[i * 2];
					NumericUpDown PonitValue = tag_list[i * 2 + 1];
					if (sm != null)
					{
						eucf = (int)sm.arrAxis[i].Eucf;
					}
					else
					{
						eucf = 1000;
					}

					PonitSpeed.Value = new System.Decimal(PointAggregate.arrPoint[i].dblPonitSpeed / eucf);
					PonitValue.Value = new System.Decimal(PointAggregate.arrPoint[i].dblPonitValue);
				}

			}
			catch (System.Exception ex)
			{
				UserControl_LogOut.OutLog(ex.Message, 0);
				Console.Write(ex.ToString());
				return false;
			}
			return true;
		}

		private void btnPointSet_Click(object sender, EventArgs e)
		{
			if (_PointAggregate != null)
			{
				PointSet pointS = new PointSet(_PointAggregate);
				pointS.ShowDialog();
				InitControl();
			}
		}

		private void SetPoint_Load(object sender, EventArgs e)
		{

		}


	}
}
