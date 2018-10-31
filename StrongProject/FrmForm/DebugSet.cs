using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace StrongProject
{
	public partial class DebugFrmSet : Form
	{
		//PointAggregate pagt = new PointAggregate();
		public DebugFrmSet(Work worker, StationModule station)
		{
			_Worker = worker;
			_StationM = station;
			intAxisControlCount = _StationM.arrAxis.Count;
			intInputControlCount = _StationM.arrInputIo.Count;
			intOutputControlCount = _StationM.arrOutputIo.Count;
			intPointControlCount = _StationM.arrPoint.Count;
			InitializeComponent();

		}
		public Work _Worker = null;
		private StationModule _StationM = null;
		private SetPointLabel lab;
		public int intAxisControlCount = 0;
		public int intInputControlCount = 0;
		public int intOutputControlCount = 0;
		public int intPointControlCount = 0;
		public List<PointAggregate> tag_PointAggregateList = new List<PointAggregate>();
		public bool IsAdd(PointAggregate a)
		{
			foreach (PointAggregate pa in tag_PointAggregateList)
			{
				if (pa == a)
				{
					return false;
				}
			}
			return true;
		}
		public void InitForm()
		{
			try
			{
				int i = 0;
				if (_StationM != null && !string.IsNullOrEmpty(_StationM.strStationName))
				{
					for (i = 0; i < intAxisControlCount; i++)
					{
						if (i == 0)
						{
							AxisLabel axisL = new AxisLabel();
							panel_Axis.Controls.Add(axisL);
							axisL.Location = new Point(42, 5);
						}
						SetAxisConfig setC = new SetAxisConfig();
						panel_Axis.Controls.Add((Control)setC);
						setC.Location = new Point(3, 32 + i * setC.Height + i * 5);
						setC.AxisSet = _StationM.arrAxis[i];

						cboAxisName.Items.Add(_StationM.arrAxis[i].AxisName);

					}
					if (cboAxisName.Items.Count > 0)
					{
						cboAxisName.SelectedIndex = 0;
					}
					for (i = 0; i < intInputControlCount; i++)
					{
						if (i == 0)
						{
							IOInputLable ioinlab = new IOInputLable();
							panel_InputIo.Controls.Add(ioinlab);
							ioinlab.Location = new Point(3, 1);
						}
						SetIOPanel inputIo = new SetIOPanel();
						panel_InputIo.Controls.Add((Control)inputIo);
						inputIo.Location = new Point(3, 27 + i * inputIo.Height + i * 5);
						inputIo.IO = _StationM.arrInputIo[i];
					}
					for (i = 0; i < intOutputControlCount; i++)
					{
						if (i == 0)
						{
							IOOutputLable iooutlb = new IOOutputLable();
							panel_OutputIo.Controls.Add(iooutlb);
							iooutlb.Location = new Point(3, 1);
						}
						SetIOPanel outputIo = new SetIOPanel();
						panel_OutputIo.Controls.Add((Control)outputIo);
						outputIo.Location = new Point(3, 27 + i * outputIo.Height + i * 5);
						outputIo.IO = _StationM.arrOutputIo[i];
					}

					int m = 0;
					for (i = 0; i < intPointControlCount; i++)
					{
						if (i == 0)
						{
							lab = new SetPointLabel(_StationM, intAxisControlCount);
							panel_Point.Controls.Add((Control)lab);
							lab.Location = new Point(85, 18);
						}


						if (_StationM.arrPoint[i].tag_BeginPointAggregateList != null)
						{
							foreach (PointAggregate pa in _StationM.arrPoint[i].tag_BeginPointAggregateList)
							{
								if (IsAdd(pa) == true)
								{
									SetPoint setP = new SetPoint();
									panel_Point.Controls.Add((Control)setP);
									setP.Location = new Point(17, 53 + (m) * setP.Height + m * 5);
									setP.intAxisCount = intAxisControlCount;
									setP.PointAggregate = pa;
									tag_PointAggregateList.Add(pa);
									m++;
								}
							}

						}
						if (IsAdd(_StationM.arrPoint[i]) == true)
						{
							SetPoint setP1 = new SetPoint();
							panel_Point.Controls.Add((Control)setP1);
							setP1.Location = new Point(17, 53 + (m) * setP1.Height + m * 5);
							setP1.intAxisCount = intAxisControlCount;
							setP1.PointAggregate = _StationM.arrPoint[i];
							tag_PointAggregateList.Add(_StationM.arrPoint[i]);
							m++;
						}
						if (_StationM.arrPoint[i].tag_EndPointAggregateList != null)
						{
							foreach (PointAggregate pa in _StationM.arrPoint[i].tag_EndPointAggregateList)
							{
								if (IsAdd(pa) == true)
								{
									SetPoint setP = new SetPoint();
									panel_Point.Controls.Add((Control)setP);
									setP.Location = new Point(17, 53 + (m) * setP.Height + m * 5);
									setP.intAxisCount = intAxisControlCount;
									setP.PointAggregate = pa;
									tag_PointAggregateList.Add(pa);
									m++;
								}
							}

						}

					}
				}
			}
			catch (Exception mess)
			{
				UserControl_LogOut.OutLog(mess.Message, 0);
			}
		}

		private void button_AddAxis_Click(object sender, EventArgs e)
		{
			AddAxisForm axis = new AddAxisForm(_Worker);
			axis.Show();
			return;
			if (intAxisControlCount >= StationManage.intStationAxisCount)
			{
				MessageBoxLog.Show("工位可配轴数已达最大");
				return;
			}
			if (intAxisControlCount < 1)
			{
				AxisLabel axisL = new AxisLabel();
				panel_Axis.Controls.Add(axisL);
				axisL.Location = new Point(42, 5);
			}
			if (_StationM.arrAxis.Count <= intAxisControlCount)
			{
				_StationM.arrAxis.Add(new AxisConfig());
			}

			SetAxisConfig setC = new SetAxisConfig();
			panel_Axis.Controls.Add((Control)setC);
			setC.Location = new Point(3, 32 + intAxisControlCount * setC.Height + intAxisControlCount * 5);
			setC.AxisSet = _StationM.arrAxis[intAxisControlCount];
			setC.AxisSet.AxisIndex = intAxisControlCount;
			intAxisControlCount++;
			RefreshPoint();

		}


		public void RestorStationInfo()
		{
			while (_StationM.intUseAxisCount < _StationM.arrAxis.Count)
			{
				_StationM.arrAxis.RemoveAt(_StationM.intUseAxisCount);
			}
			while (_StationM.intUseInputIoCount < _StationM.arrInputIo.Count)
			{
				_StationM.arrInputIo.RemoveAt(_StationM.intUseInputIoCount);
			}
			while (_StationM.intUseOutputIoCount < _StationM.arrOutputIo.Count)
			{
				_StationM.arrOutputIo.RemoveAt(_StationM.intUseOutputIoCount);
			}
			while (_StationM.intUsePointCount < _StationM.arrPoint.Count)
			{
				_StationM.arrPoint.RemoveAt(_StationM.intUsePointCount);
			}
		}
		private void button_Save_Click(object sender, EventArgs e)
		{
			int i = 0;
			_StationM.intUseAxisCount = intAxisControlCount;
			_StationM.intUseInputIoCount = intInputControlCount;
			_StationM.intUseOutputIoCount = intOutputControlCount;
			_StationM.intUsePointCount = intPointControlCount;
			if (intAxisControlCount > 0)
			{
				foreach (Control con in panel_Axis.Controls)
				{
					if (con is SetAxisConfig)
					{
						SetAxisConfig setA = (SetAxisConfig)con;
						setA.Save();
					}
				}
				for (i = intAxisControlCount; i < _StationM.arrAxis.Count; i++)
				{
					_StationM.arrAxis.RemoveAt(i);
					i = intAxisControlCount;
				}
			}
			else
			{
				_StationM.arrAxis.Clear();
			}
			if (intInputControlCount > 0)
			{
				foreach (Control con in panel_InputIo.Controls)
				{
					if (con is SetIOPanel)
					{
						SetIOPanel setInput = (SetIOPanel)con;
						setInput.Save();
					}
				}
				for (i = intInputControlCount; i < _StationM.arrInputIo.Count; i++)
				{
					_StationM.arrInputIo.RemoveAt(i);
					i = intInputControlCount;
				}
			}
			else
			{
				_StationM.arrInputIo.Clear();
			}
			if (intOutputControlCount > 0)
			{
				foreach (Control con in panel_OutputIo.Controls)
				{
					if (con is SetIOPanel)
					{
						SetIOPanel setOutput = (SetIOPanel)con;
						setOutput.Save();
					}
				}
				for (i = intOutputControlCount; i < _StationM.arrOutputIo.Count; i++)
				{
					_StationM.arrOutputIo.RemoveAt(i);
					i = intOutputControlCount;
				}
			}
			else
			{
				_StationM.arrOutputIo.Clear();
			}
			if (intPointControlCount > 0)
			{
				foreach (Control con in panel_Point.Controls)
				{
					if (con is SetPoint)
					{
						SetPoint setP = (SetPoint)con;
						setP.Save();
					}
				}
				for (i = intPointControlCount; i < _StationM.arrPoint.Count; i++)
				{
					_StationM.arrPoint.RemoveAt(i);
					i = intPointControlCount;
				}
			}
			else
			{
				_StationM.arrPoint.Clear();
			}
			if (!_Worker.Config.Save())
			{
				MessageBoxLog.Show("保存参数异常");
			}
			else
			{
				MessageBoxLog.Show("保存成功");
				this.Close();
				//RefreshStation();
			}
		}

		private void DebugSet_Load(object sender, EventArgs e)
		{
			InitForm();
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
				panel_Axis.Controls.Remove(finChild[i - 1]);
				intAxisControlCount--;
				if (intAxisControlCount < 1)
				{
					panel_Axis.Controls.Clear();
				}

			}
			else
			{
				MessageBoxLog.Show("轴已清空！");
			}

			RefreshPoint();
		}

		private void button_AddInputIo_Click(object sender, EventArgs e)
		{
			bool blnScroll = false;
			int intVerticalScroll = 0;
			if (intInputControlCount >= StationManage.intStationIoInputCount)
			{
				MessageBoxLog.Show("工位可配输入IO已达最大");
				return;
			}
			if (intInputControlCount < 1)
			{
				IOInputLable ioinlab = new IOInputLable();
				panel_InputIo.Controls.Add(ioinlab);
				ioinlab.Location = new Point(3, 1);
			}

			if (_StationM.arrInputIo.Count == intInputControlCount)
			{
				_StationM.arrInputIo.Add(new IOParameter());
			}
			else
			{
				if (intInputControlCount >= 0)
					_StationM.arrInputIo[intInputControlCount] = new IOParameter();
			}

			if (panel_InputIo.VerticalScroll.Value > 0)
			{
				intVerticalScroll = panel_InputIo.VerticalScroll.Value;
				panel_InputIo.VerticalScroll.Value = panel_InputIo.VerticalScroll.Minimum;
				blnScroll = true;

			}
			SetIOPanel setC = new SetIOPanel();
			panel_InputIo.Controls.Add((Control)setC);
			setC.Location = new Point(3, 27 + intInputControlCount * setC.Height + intInputControlCount * 5);
			setC.IO = _StationM.arrInputIo[intInputControlCount];
			if (blnScroll)
			{
				panel_InputIo.AutoScrollPosition = new Point(panel_InputIo.HorizontalScroll.Value, intVerticalScroll);
			}

			intInputControlCount++;
		}

		private void button_RemoveInputIo_Click(object sender, EventArgs e)
		{
			Control[] finChild = new Control[intInputControlCount];
			int i = 0;
			foreach (Control con in panel_InputIo.Controls)
			{
				if (con is SetIOPanel)
				{
					finChild[i] = con;
					i++;
				}
			}

			if (i > 0)
			{
				//_StationM.arrInputIo.RemoveAt(i - 1);
				panel_InputIo.Controls.Remove(finChild[i - 1]);
				intInputControlCount--;
				if (intInputControlCount < 1)
				{
					panel_InputIo.Controls.Clear();
				}
			}
			else
			{
				MessageBoxLog.Show("输入IO已清空！");
			}
		}

		private void button_AddOutputIo_Click(object sender, EventArgs e)
		{
			int intVerticalScroll = 0;
			bool blnScroll = false;
			if (intOutputControlCount >= StationManage.intStationIoOutPutCount)
			{
				MessageBoxLog.Show("工位可配输出IO已达最大");
				return;
			}
			if (intOutputControlCount < 1)
			{
				IOOutputLable iooutlb = new IOOutputLable();
				panel_OutputIo.Controls.Add(iooutlb);
				iooutlb.Location = new Point(3, 1);
			}
			if (_StationM.arrOutputIo.Count == intOutputControlCount)
			{
				_StationM.arrOutputIo.Add(new IOParameter());
			}
			else
			{
				if (intInputControlCount >= 0)
					_StationM.arrOutputIo[intInputControlCount] = new IOParameter();
			}

			if (panel_OutputIo.VerticalScroll.Value > 0)
			{
				intVerticalScroll = panel_InputIo.VerticalScroll.Value;
				panel_OutputIo.VerticalScroll.Value = panel_OutputIo.VerticalScroll.Minimum;
				blnScroll = true;

			}
			SetIOPanel setC = new SetIOPanel();
			panel_OutputIo.Controls.Add((Control)setC);
			setC.Location = new Point(3, 27 + intOutputControlCount * setC.Height + intOutputControlCount * 5);
			setC.IO = _StationM.arrOutputIo[intOutputControlCount];
			intOutputControlCount++;

			if (blnScroll)
			{
				panel_OutputIo.AutoScrollPosition = new Point(panel_OutputIo.HorizontalScroll.Value, intVerticalScroll);
			}
		}

		private void button_RemoveOutputIo_Click(object sender, EventArgs e)
		{
			Control[] finChild = new Control[intOutputControlCount];
			int i = 0;
			foreach (Control con in panel_OutputIo.Controls)
			{
				if (con is SetIOPanel)
				{
					finChild[i] = con;
					i++;
				}
			}

			if (i > 0)
			{
				//_StationM.arrOutputIo.RemoveAt(i-1);
				panel_OutputIo.Controls.Remove(finChild[i - 1]);
				intOutputControlCount--;
				if (intOutputControlCount < 1)
				{
					panel_OutputIo.Controls.Clear();
				}
			}
			else
			{
				MessageBoxLog.Show("输出IO已清空！");
			}
		}

		private void button_AddPoint_Click(object sender, EventArgs e)
		{
			int intVerticalScroll = 0;
			bool blnScroll = false;
			/*  if (intAxisControlCount < 1)
			  {
				  MessageBoxLog.Show("轴数量为空，请先配置轴");
				  return;
			  }*/
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (intPointControlCount >= StationManage.intStationPointCount)
			{
				MessageBoxLog.Show("工位可配点位数已达最大");
				return;
			}

			if (intPointControlCount < 1)
			{
				lab = new SetPointLabel(_StationM, intAxisControlCount);
				panel_Point.Controls.Add((Control)lab);
				lab.Location = new Point(85, 14);

			}
			if (_StationM.arrPoint.Count < intPointControlCount)
			{
				MessageBoxLog.Show("点位数据计数异常");
				return;
			}
			else if (_StationM.arrPoint.Count <= intPointControlCount)
			{
				_StationM.arrPoint.Add(new PointAggregate(_StationM.strStationName));
			}
			if (panel_Point.VerticalScroll.Value > 0)
			{
				intVerticalScroll = panel_Point.VerticalScroll.Value;
				panel_Point.VerticalScroll.Value = panel_Point.VerticalScroll.Minimum;
				blnScroll = true;

			}
			SetPoint setC = new SetPoint();
			panel_Point.Controls.Add((Control)setC);
			setC.Location = new Point(17, 53 + intPointControlCount * setC.Height + intPointControlCount * 5);
			setC.intAxisCount = intAxisControlCount;
			setC.PointAggregate = _StationM.arrPoint[intPointControlCount];

			intPointControlCount++;

			if (blnScroll)
			{
				panel_Point.AutoScrollPosition = new Point(panel_Point.HorizontalScroll.Value, intVerticalScroll);
			}



		}

		private void button_RemovePoint_Click(object sender, EventArgs e)
		{
			RefreshPoint2();
		}

		public void RefreshPoint2()
		{
			foreach (Control con in panel_Point.Controls)
			{
				if (con is SetPoint)
				{
					SetPoint setP = (SetPoint)con;
					setP.Refres();
				}
			}

		}

		public void RefreshPoint()
		{
			foreach (Control con in panel_Point.Controls)
			{
				if (con is SetPoint)
				{
					SetPoint setP = (SetPoint)con;
					setP.intAxisCount = intAxisControlCount;
					setP.InitControl();
				}
			}
			if (lab != null)
			{
				lab.RefreshControl(intAxisControlCount);
			}
		}

		private void DebugFrmSet_FormClosing(object sender, FormClosingEventArgs e)
		{
			RestorStationInfo();
		}

		public void SetStartSpeed()
		{
			double dblSpeed = 0;
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			dblSpeed = Convert.ToDouble(numSpeed.Value) * _StationM.arrAxis[i].Eucf;
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblPonitStartSpeed = dblSpeed;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblPonitStartSpeed = dblSpeed;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblPonitStartSpeed = dblSpeed;
					}

				}

			}
		}

		public void SetAcc()
		{
			double dblAcc = Convert.ToDouble(numSpeed.Value);
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblAcc = dblAcc;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblAcc = dblAcc;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblAcc = dblAcc;
					}

				}

			}
		}

		private void SetSpeed()
		{
			double dblSpeed = Convert.ToDouble(numSpeed.Value) * 10;
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			dblSpeed = Convert.ToDouble(numSpeed.Value) * _StationM.arrAxis[i].Eucf;
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblPonitSpeed = dblSpeed;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblPonitSpeed = dblSpeed;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblPonitSpeed = dblSpeed;
					}

				}

			}
		}
		public void SetAccTime()
		{
			double dblAccTime = Convert.ToDouble(numSpeed.Value);
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblAccTime = dblAccTime;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblAccTime = dblAccTime;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblAccTime = dblAccTime;
					}

				}

			}
		}
		public void SetDecTime()
		{
			double dblDecTime = Convert.ToDouble(numSpeed.Value);
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblDecTime = dblDecTime;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblDecTime = dblDecTime;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblDecTime = dblDecTime;
					}

				}

			}
		}
		public void SetDec()
		{
			double dblDec = Convert.ToDouble(numSpeed.Value);
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblDec = dblDec;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblDec = dblDec;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblDec = dblDec;
					}

				}

			}
		}




		private void btnSetSpeed_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(cboAxisName.Text))
			{
				MessageBoxLog.Show("轴名为空，请选择轴名!");
				return;
			}
			/*
            启始速度  加速度
            加速时间
            运行速度
            减速度
            减速时间 
            */
			switch (comboBox_SetType.SelectedIndex)
			{
				case 0:
					SetStartSpeed();
					break;
				case 1:

					SetAcc();
					break;
				case 2:
					SetAccTime();
					break;
				case 3:
					SetSpeed();
					break;
				case 4:
					SetDec();
					break;
				case 5:
					SetAccTime();
					break;

			}

			RefreshPoint2();


		}



		public void SetStartSpeedScale()
		{
			double dblSpeed = Convert.ToDouble(numSpeed.Value) * 10;
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblPonitStartSpeed = _StationM.arrPoint[j].arrPoint[i].dblPonitStartSpeed * dblSpeed;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblPonitStartSpeed = pa.arrPoint[i].dblPonitStartSpeed * dblSpeed;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblPonitStartSpeed = pa.arrPoint[i].dblPonitStartSpeed * dblSpeed;
					}

				}

			}
		}

		public void SetAccScale()
		{
			double dblAcc = Convert.ToDouble(numSpeed.Value);
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblAcc = _StationM.arrPoint[j].arrPoint[i].dblAcc * dblAcc;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblAcc = pa.arrPoint[i].dblAcc * dblAcc;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblAcc = pa.arrPoint[i].dblAcc * dblAcc;
					}

				}

			}
		}

		public void SetSpeedScale()
		{
			double dblSpeed = Convert.ToDouble(numSpeed.Value) * 10;
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblPonitSpeed = _StationM.arrPoint[j].arrPoint[i].dblPonitSpeed * dblSpeed;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblPonitSpeed = pa.arrPoint[i].dblPonitSpeed * dblSpeed;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblPonitSpeed = pa.arrPoint[i].dblPonitSpeed * dblSpeed;
					}

				}

			}
		}
		public void SetAccTimeScale()
		{
			double dblAccTime = Convert.ToDouble(numSpeed.Value);
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblAccTime = _StationM.arrPoint[j].arrPoint[i].dblAccTime * dblAccTime;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblAccTime = pa.arrPoint[i].dblAccTime * dblAccTime;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblAccTime = pa.arrPoint[i].dblAccTime * dblAccTime;
					}

				}

			}
		}
		public void SetDecTimeScale()
		{
			double dblDecTime = Convert.ToDouble(numSpeed.Value);
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblDecTime = _StationM.arrPoint[j].arrPoint[i].dblDecTime * dblDecTime;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblDecTime = pa.arrPoint[i].dblDecTime * dblDecTime;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblDecTime = pa.arrPoint[i].dblDecTime * dblDecTime;
					}
				}

			}
		}
		public void SetDecScale()
		{
			double dblDec = Convert.ToDouble(numSpeed.Value);
			int i = 0;
			for (i = 0; i < _StationM.arrAxis.Count; i++)
			{
				if (cboAxisName.Text == _StationM.arrAxis[i].AxisName)
				{
					break;
				}
			}
			for (int j = 0; j < _StationM.arrPoint.Count; j++)
			{
				if (_StationM.arrPoint[j].arrPoint[i].blnPointEnable && !_StationM.arrPoint[j].arrPoint[i].blnIsSpecialPoint)
				{
					_StationM.arrPoint[j].arrPoint[i].dblDec = _StationM.arrPoint[j].arrPoint[i].dblDec * dblDec;
				}


				if (_StationM.arrPoint[j].tag_EndPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_EndPointAggregateList)
					{
						pa.arrPoint[i].dblDec = _StationM.arrPoint[j].arrPoint[i].dblDec * dblDec;
					}

				}

				if (_StationM.arrPoint[j].tag_BeginPointAggregateList != null)
				{
					foreach (PointAggregate pa in _StationM.arrPoint[j].tag_BeginPointAggregateList)
					{
						pa.arrPoint[i].dblDec = pa.arrPoint[i].dblDec * dblDec;
					}

				}

			}
		}


		private void button12_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(cboAxisName.Text))
			{
				MessageBoxLog.Show("轴名为空，请选择轴名!");
				return;
			}
			/*
            启始速度  加速度
            加速时间
            运行速度
            减速度
            减速时间 
            */
			switch (comboBox_SetType.SelectedIndex)
			{
				case 0:
					SetStartSpeedScale();
					break;
				case 1:

					SetAccScale();
					break;
				case 2:
					SetAccTimeScale();
					break;
				case 3:
					SetSpeedScale();
					break;
				case 4:
					SetDecScale();
					break;
				case 5:
					SetAccTimeScale();
					break;

			}

			RefreshPoint2();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == saveFileDialog1.ShowDialog())
			{
				string filename = saveFileDialog1.FileName;
				StationManage.GetAllOutIO(filename);
			}
		}
		private void button1_Click(object sender, EventArgs e)
		{

			if (DialogResult.OK == saveFileDialog1.ShowDialog())
			{

				string filename = saveFileDialog1.FileName;
				StationManage.GetAllInIO(filename);

			}
		}
		private void button_out_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == saveFileDialog1.ShowDialog())
			{

				string filename = saveFileDialog1.FileName;
				StationManage.GetAllStep(filename);

			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{

				string filename = openFileDialog1.FileName;
				string fcontent = StationManage.ReadFile(filename);
				StationManage.StepImport(fcontent);

			}

		}

		private void button_delAll_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (MessageBoxLog.Show("是否删除所有?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			StationManage.StepDellAll();
		}

		private void button7_Click(object sender, EventArgs e)
		{

			if (DialogResult.OK == saveFileDialog1.ShowDialog())
			{
				string filename = saveFileDialog1.FileName;
				StationManage.GetAllAxis(filename);
			}
		}

		private void button6_Click(object sender, EventArgs e)
		{

			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{

				string filename = openFileDialog1.FileName;
				string fcontent = StationManage.ReadFile(filename);
				StationManage.AxisImport(fcontent);

			}
		}

		private void button8_Click(object sender, EventArgs e)
		{
			FrmAxisManage axis = new FrmAxisManage(_StationM);
			axis.Show();
		}

		private void button9_Click(object sender, EventArgs e)
		{
			int i = 0;
			panel_Axis.Controls.Clear();
			cboAxisName.Items.Clear();
			if (_StationM != null && !string.IsNullOrEmpty(_StationM.strStationName))
			{
				for (i = 0; i < _StationM.intUseAxisCount; i++)
				{
					if (i == 0)
					{
						AxisLabel axisL = new AxisLabel();
						panel_Axis.Controls.Add(axisL);
						axisL.Location = new Point(42, 5);
					}
					SetAxisConfig setC = new SetAxisConfig();
					panel_Axis.Controls.Add((Control)setC);
					setC.Location = new Point(3, 32 + i * setC.Height + i * 5);
					setC.AxisSet = _StationM.arrAxis[i];

					cboAxisName.Items.Add(_StationM.arrAxis[i].AxisName);

				}
				intAxisControlCount = _StationM.intUseAxisCount;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{

				string filename = openFileDialog1.FileName;
				string fcontent = StationManage.ReadFile(filename);
				StationManage.InIoImport(fcontent);

			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{

				string filename = openFileDialog1.FileName;
				string fcontent = StationManage.ReadFile(filename);
				StationManage.OutIoImport(fcontent);

			}
		}

		private void button10_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{

				string filename = openFileDialog1.FileName;
				string fcontent = StationManage.ReadFile(filename);
				StationManage.InIoCardImport(fcontent);

			}
		}

		private void button11_Click(object sender, EventArgs e)
		{
			if (DialogResult.OK == openFileDialog1.ShowDialog())
			{

				string filename = openFileDialog1.FileName;
				string fcontent = StationManage.ReadFile(filename);
				StationManage.OutIoCardImport(fcontent);

			}
		}



	}
}
