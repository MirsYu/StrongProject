using System;
using System.Drawing;
using System.Windows.Forms;


namespace StrongProject
{
	public partial class AxisDebugCtrl : UserControl
	{
		private bool bIsAlarm = false;
		double nowpoint;
		double nowpset;
		short resutl;
		public AxisConfig arrAxis; //定义一个AxisConfig的对象
		public Work tag_work;
		String stfile;
		Color cl;
		public StationModule tag_StationModule;
		public AxisDebugCtrl()
		{
			InitializeComponent();

		}
		public AxisDebugCtrl(AxisConfig arrAxis_, Work _Work) //初始化轴参数数组
		{
			tag_work = _Work;
			arrAxis = arrAxis_;
			//_Speed = arrAxis.ManualSpeedNormal;
			InitializeComponent();
			NewCtrlCardIO.AxisAlarmChange += new EventHandler(NewCtrlCardSR_AxisAlarmChange);
			NewCtrlCardIO.AxisHomeChange += new EventHandler(NewCtrlCardSR_AxisHomeChange);
			NewCtrlCardIO.AxisLimitNChange += new EventHandler(NewCtrlCardSR_AxisLimitNChange);
			NewCtrlCardIO.AxisLimitPChange += new EventHandler(NewCtrlCardSR_AxisLimitPChange);
			NewCtrlCardIO.AxisEncPosChange += new EventHandler(NewCtrlCardSR_AxisEncPosChange);
			NewCtrlCardIO.AxisPrfPosChange += new EventHandler(NewCtrlCardSR_AxisPrfPosChange);
			NewCtrlCardIO.AxisEnableChange += new EventHandler(NewCtrlCardSR_AxisEnableChange);
			NewCtrlCardIO.tag_IO_refresh = 1;

		}

		#region Axis Status

		//Axis使能
		void NewCtrlCardSR_AxisEnableChange(object sender, EventArgs e)
		{
			if (!bIsAlarm)
			{
				ulong one = 1;
				CardAxisSignalEvengArgs cASE = (CardAxisSignalEvengArgs)e;
				if (cASE != null && cASE.axisNum == arrAxis.AxisNum && cASE.CardNum == arrAxis.CardNum && cASE.type == (int)arrAxis.tag_MotionCardManufacturer)
				{
					this.Invoke((MethodInvoker)delegate
					{
						if ((cASE.Value & (one << arrAxis.AxisNum)) == 0)
						{
							BTServoSigle.Text = "Servo ON";
							BTServoSigle.BackColor = Color.LawnGreen;
						}
						else
						{
							BTServoSigle.Text = "Servo Off";
							BTServoSigle.BackColor = Color.DarkGreen;
						}
					});

				}
			}

		}

		//Axis报警
		void NewCtrlCardSR_AxisAlarmChange(object sender, EventArgs e)
		{
			if (arrAxis == null)
			{
				return;
			}
			CardAxisSignalEvengArgs cASE = e as CardAxisSignalEvengArgs;
			if (cASE.CardNum == arrAxis.CardNum && cASE.type == (int)arrAxis.tag_MotionCardManufacturer)
			{
				try
				{
					ulong one = 1;
					this.Invoke((MethodInvoker)delegate
					{
						if (arrAxis.tag_IoAlarmNHighEnable == 1)
						{
							if ((cASE.Value & (one << arrAxis.AxisNum)) > 0)
							{
								stfile = BTServoSigle.Text;
								cl = BTServoSigle.BackColor;
								BTServoSigle.Text = "Alarm";
								BTServoSigle.BackColor = Color.Red;
								bIsAlarm = true;
							}
							else
							{
								if (bIsAlarm)
								{
									bIsAlarm = false;
									BTServoSigle.Text = stfile;
									BTServoSigle.BackColor = cl;
								}
							}
						}
						else
						{
							if ((cASE.Value & (one << arrAxis.AxisNum)) == 0)
							{
								stfile = BTServoSigle.Text;
								cl = BTServoSigle.BackColor;
								BTServoSigle.Text = "Alarm";
								BTServoSigle.BackColor = Color.Red;
								bIsAlarm = true;
							}
							else
							{
								if (bIsAlarm)
								{
									bIsAlarm = false;
									BTServoSigle.Text = stfile;
									BTServoSigle.BackColor = cl;
								}
							}
						}
					});
				}
				catch (System.Exception ex)
				{

				}
			}
		}

		//Axis规划位置
		void NewCtrlCardSR_AxisPrfPosChange(object sender, EventArgs e)
		{
			if (arrAxis == null)
			{
				return;
			}
			CardAxisPosEvengArgs cAPE = e as CardAxisPosEvengArgs;
			if (cAPE.CardNum == arrAxis.CardNum && cAPE.type == (int)arrAxis.tag_MotionCardManufacturer && arrAxis.AxisNum == cAPE.AxisNum)
			{
				try
				{
					this.Invoke((MethodInvoker)delegate
					{
						//  if (arrAxis.AxisNum >= 0 )
						{
							nowpoint = (cAPE.Value / arrAxis.Eucf);
							point1.Text = (cAPE.Value / arrAxis.Eucf).ToString("f3");
							arrAxis.dblPrfPos = (cAPE.Value / arrAxis.Eucf);
						}

					});
				}
				catch (System.Exception ex)
				{

				}
			}
		}

		//Axis编码器反馈位置
		void NewCtrlCardSR_AxisEncPosChange(object sender, EventArgs e)
		{
			if (arrAxis == null)
			{
				return;
			}
			CardAxisPosEvengArgs cAPE = e as CardAxisPosEvengArgs;
			if (cAPE != null && cAPE.AxisNum == arrAxis.AxisNum && cAPE.AxisNum == arrAxis.AxisNum && cAPE.CardNum == arrAxis.CardNum && cAPE.type == (int)arrAxis.tag_MotionCardManufacturer)
			{
				try
				{
					this.Invoke((MethodInvoker)delegate
					{
						// if (arrAxis.AxisNum >= 0)
						{
							point2.Text = (cAPE.Value / arrAxis.Eucf).ToString("f3");
							//arrAxis.dblEncPos = (8 * cAPE.CardNum + cAPE.Value / arrAxis.Eucf);
							arrAxis.dblEncPos = (cAPE.Value / arrAxis.Eucf);
						}
					});
				}
				catch (System.Exception ex)
				{

				}



			}
		}

		//Axis正限位触发
		void NewCtrlCardSR_AxisLimitPChange(object sender, EventArgs e)
		{
			if (arrAxis == null || arrAxis.tag_IoLimtPEnable == 1)
			{
				return;
			}
			CardAxisSignalEvengArgs cASE = e as CardAxisSignalEvengArgs;
			if (cASE.CardNum == arrAxis.CardNum && cASE.type == (int)arrAxis.tag_MotionCardManufacturer)
			{
				ulong one = 1;
				try
				{
					this.Invoke((MethodInvoker)delegate
					{
						if (arrAxis.tag_IoLimtPNHighEnable == 1)
						{
							lblPEL.Image = ((cASE.Value & (one << arrAxis.AxisNum)) > 0) ? StrongProject.Properties.Resources.led_green_on_16 : StrongProject.Properties.Resources.led_off_16;
						}
						else
						{
							lblPEL.Image = ((cASE.Value & (one << arrAxis.AxisNum)) == 0) ? StrongProject.Properties.Resources.led_green_on_16 : StrongProject.Properties.Resources.led_off_16;

						}
					});
				}
				catch (System.Exception ex)
				{

				}


			}
		}

		//Axis负极限触发
		void NewCtrlCardSR_AxisLimitNChange(object sender, EventArgs e)
		{
			if (arrAxis == null || arrAxis.tag_IoLimtNEnable == 1)
			{
				return;
			}
			CardAxisSignalEvengArgs cASE = e as CardAxisSignalEvengArgs;
			if (cASE.CardNum == arrAxis.CardNum && cASE.type == (int)arrAxis.tag_MotionCardManufacturer)
			{
				try
				{
					ulong one = 1;
					this.Invoke((MethodInvoker)delegate
					{
						if (arrAxis.tag_IoLimtPNHighEnable == 1)
						{
							lblMEL.Image = ((cASE.Value & (one << arrAxis.AxisNum)) > 0) ? StrongProject.Properties.Resources.led_green_on_16 : StrongProject.Properties.Resources.led_off_16;
						}
						else
						{
							lblMEL.Image = ((cASE.Value & (one << arrAxis.AxisNum)) == 0) ? StrongProject.Properties.Resources.led_green_on_16 : StrongProject.Properties.Resources.led_off_16;

						}

					});
				}
				catch (System.Exception ex)
				{

				}
			}
		}

		//Axis原点触发
		void NewCtrlCardSR_AxisHomeChange(object sender, EventArgs e)
		{
			if (arrAxis == null)
			{
				return;
			}
			CardAxisSignalEvengArgs cASE = e as CardAxisSignalEvengArgs;
			if (cASE.CardNum == arrAxis.CardNum && cASE.type == (int)arrAxis.tag_MotionCardManufacturer)
			{
				this.Invoke((MethodInvoker)delegate
				{
					ulong one = 1;
					if (arrAxis.tag_homeIoHighLow)
					{
						bool VAR = (cASE.Value & (one << arrAxis.AxisNum)) > 0;

						lblORG.Image = VAR ? StrongProject.Properties.Resources.led_green_on_16 : StrongProject.Properties.Resources.led_off_16;
					}
					else
					{
						lblORG.Image = ((cASE.Value & (one << arrAxis.AxisNum)) == 0) ? StrongProject.Properties.Resources.led_green_on_16 : StrongProject.Properties.Resources.led_off_16;

					}

				});

			}
		}

		#endregion


		private string _StrRunModule = "";

		public string StrRunModule
		{
			get { return _StrRunModule; }
			set { _StrRunModule = value; }
		}

		private double _Pos = 0; //设置一个长距离点位

		public double Pos
		{
			get { return _Pos; }
			set { _Pos = value; }
		}

		//private double _speed = 0;

		//public double speed
		//{
		//    get { return _speed; }
		//    set { _speed = value; }
		//}

		private const string STR_M_CONTINUE = "连续";
		private const string STR_M_LONGDIS = "长距离";
		private const string STR_M_SHORTDIS = "短距离";
		Color clrErr = Color.Red;
		Color clrNormal = Color.Green;




		//AxisDebugCtrl控件加载
		private void AxisDebugCtrl_Load(object sender, EventArgs e)
		{

			if (arrAxis != null)
			{
				LB_AxisName.Text = arrAxis.AxisName;//从ArrAxis拿轴名称

			}
		}

		//按下停止按钮
		private void JogSTOPBT_Click(object sender, EventArgs e)
		{
			if (arrAxis != null)
			{
				//轴急停
				resutl = NewCtrlCardV0.SR_AxisEmgStop((int)arrAxis.tag_MotionCardManufacturer, arrAxis.CardNum, arrAxis.AxisNum);
				if (resutl != 0)
				{
					MessageBoxLog.Show("轴停止异常");
				}

			}
			else
			{
				MessageBoxLog.Show("操作轴参数不能为空");

			}

		}



		//往上移动
		private void Jog_plusBT_MouseDown(object sender, MouseEventArgs e)
		{

			if (!Work.IsMove(1))
			{
				return;
			}
			setspeed();
			if (arrAxis == null)
			{
				MessageBoxLog.Show("");
				return;
			}
			double pos = 0;
			if (arrAxis.SoftLimitEnablel == 0)
			{
				pos = arrAxis.SoftLimitMaxValue;
			}
			else
			{
				pos = 1000;
			}

			if (StationManage.Distancemode == "短距")
			{

				nowpset = StationManage.Shortdistanceset;

				//   AxisSafe.
				//  tag_work._Config.tag_safeStationModule..
				/*  if (!AxisSafeManage.AxisIsSafe(tag_work._Config.tag_safeStationModule, arrAxis,0, (nowpoint + nowpset) * arrAxis.Eucf))
				  {
					  return;
				  }*/
				PointModule _pointModule = new PointModule();
				_pointModule.dblAcc = arrAxis.Acc;
				_pointModule.blnPointEnable = true;//表示点位是否启用
				_pointModule.blnIsSpecialPoint = true;//表示此点位是否是特殊点位 方便对应轴速度快速改变        
				_pointModule.dblPonitValue = nowpoint + nowpset;//点位数据
				_pointModule.dblPonitSpeed = StationManage.Speedvalue;//点位速度        
				_pointModule.dblAcc = arrAxis.Acc; //加速度
				_pointModule.dblDec = arrAxis.Dec;  //减速度
				_pointModule.dblAccTime = arrAxis.tag_accTime;//加速时间
				_pointModule.dblDecTime = arrAxis.tag_accTime;//减速时间
				_pointModule.dblPonitStartSpeed = arrAxis.StartSpeed;//初始速度
				_pointModule.db_S_Time = arrAxis.tag_S_Time;//初始速度
				resutl = NewCtrlCardV0.SR_AbsoluteMove(arrAxis, _pointModule);
				if (resutl != 0)
				{
					MessageBoxLog.Show("轴运动异常");
				}

			}
			else
			{
				if (StationManage.Distancemode == "长距")
				{

					nowpset = StationManage.Longdistanceset;


					/*  if (!AxisSafeManage.AxisIsSafe(tag_work._Config.tag_safeStationModule, arrAxis, 0, (nowpoint + nowpset) * arrAxis.Eucf))
					  {
						  return;
					  }*/
					PointModule _pointModule = new PointModule();
					_pointModule.dblAcc = arrAxis.Acc;
					_pointModule.blnPointEnable = true;//表示点位是否启用
					_pointModule.blnIsSpecialPoint = true;//表示此点位是否是特殊点位 方便对应轴速度快速改变        
					_pointModule.dblPonitValue = nowpoint + nowpset;//点位数据
					_pointModule.dblPonitSpeed = StationManage.Speedvalue;//点位速度        
					_pointModule.dblAcc = arrAxis.Acc; //加速度
					_pointModule.dblDec = arrAxis.Dec;  //减速度
					_pointModule.dblAccTime = arrAxis.tag_accTime;//加速时间
					_pointModule.dblDecTime = arrAxis.tag_accTime;//减速时间
					_pointModule.dblPonitStartSpeed = arrAxis.StartSpeed;//初始速度
					_pointModule.db_S_Time = arrAxis.tag_S_Time;//初始速度
					resutl = NewCtrlCardV0.SR_AbsoluteMove(arrAxis, _pointModule);
					if (resutl != 0)
					{
						MessageBoxLog.Show("轴运动异常");
					}


				}
				else
				{
					/*  if (!AxisSafeManage.AxisIsSafe(tag_work._Config.tag_safeStationModule, arrAxis, 0, (nowpoint + nowpset) * arrAxis.Eucf))
					  {
						  return;
					  }*/
					PointModule _pointModule = new PointModule();
					_pointModule.dblAcc = arrAxis.Acc;
					_pointModule.blnPointEnable = true;//表示点位是否启用
					_pointModule.blnIsSpecialPoint = true;//表示此点位是否是特殊点位 方便对应轴速度快速改变        
					_pointModule.dblPonitValue = nowpoint + 6000;//点位数据
					_pointModule.dblPonitSpeed = StationManage.Speedvalue;//点位速度        
					_pointModule.dblAcc = arrAxis.Acc; //加速度
					_pointModule.dblDec = arrAxis.Dec;  //减速度
					_pointModule.dblAccTime = arrAxis.tag_accTime;//加速时间
					_pointModule.dblDecTime = arrAxis.tag_accTime;//减速时间
					_pointModule.dblPonitStartSpeed = arrAxis.StartSpeed;//初始速度
					_pointModule.db_S_Time = arrAxis.tag_S_Time;//初始速度
					resutl = NewCtrlCardV0.SR_continue_move(arrAxis, _pointModule, 0);
					if (resutl != 0)
					{
						MessageBoxLog.Show("轴运动异常");
					}

				}

			}



		}
		//往上移动停止
		private void Jog_plusBT_MouseUp(object sender, MouseEventArgs e)
		{
			Global.WorkVar.tag_isFangDaiJieChu = false;
			if (arrAxis == null)
			{
				return;
			}
			if (StationManage.Distancemode == "长距" || StationManage.Distancemode == "短距")
			{
				return;
			}

			else
			{
				resutl = NewCtrlCardV0.SR_AxisEmgStop((int)arrAxis.tag_MotionCardManufacturer, arrAxis.CardNum, arrAxis.AxisNum);
				if (resutl != 0)
				{
					MessageBoxLog.Show("轴停止异常");
				}

			}


		}
		public int FindeAxisArrayIndexNum()
		{
			int intFindeIndex = -1;
			for (int i = 0; i < tag_StationModule.arrAxis.Count; i++)
			{
				if (tag_StationModule.arrAxis[i].AxisName == arrAxis.AxisName)
				{
					intFindeIndex = i;
					break;
				}
			}
			return intFindeIndex;

		}
		//往下移动
		private void Jog_minusBT_MouseDown(object sender, MouseEventArgs e)
		{
			if (!Work.IsMove(1))
			{
				return;
			}
			setspeed();
			if (arrAxis == null)
			{
				MessageBoxLog.Show("");
				return;
			}
			double pos = 0;
			if (arrAxis.SoftLimitEnablel == 0)
			{
				pos = arrAxis.SoftLimitMaxValue;
			}
			else
			{
				pos = -1000;
			}

			if (StationManage.Distancemode == "短距")
			{

				nowpset = StationManage.Shortdistanceset;

				/* if (!AxisSafeManage.AxisIsSafe(tag_work._Config.tag_safeStationModule, arrAxis, 0, (nowpoint - nowpset) * arrAxis.Eucf))
				 {
					 return;
				 }*/
				PointModule _pointModule = new PointModule();
				_pointModule.dblAcc = arrAxis.Acc;
				_pointModule.blnPointEnable = true;//表示点位是否启用
				_pointModule.blnIsSpecialPoint = true;//表示此点位是否是特殊点位 方便对应轴速度快速改变        
				_pointModule.dblPonitValue = nowpoint - nowpset;//点位数据
				_pointModule.dblPonitSpeed = StationManage.Speedvalue;//点位速度        
				_pointModule.dblAcc = arrAxis.Acc; //加速度
				_pointModule.dblDec = arrAxis.Dec;  //减速度
				_pointModule.dblAccTime = arrAxis.tag_accTime;//加速时间
				_pointModule.dblDecTime = arrAxis.tag_accTime;//减速时间
				_pointModule.dblPonitStartSpeed = arrAxis.StartSpeed;//初始速度
				_pointModule.db_S_Time = arrAxis.tag_S_Time;//初始速度
				resutl = NewCtrlCardV0.SR_AbsoluteMove(arrAxis, _pointModule);
				if (resutl != 0)
				{
					MessageBoxLog.Show("轴运动异常");
				}

			}
			else
			{
				if (StationManage.Distancemode == "长距")
				{

					nowpset = StationManage.Longdistanceset;
					/*  if (!AxisSafeManage.AxisIsSafe(tag_work._Config.tag_safeStationModule, arrAxis, 0, (nowpoint - nowpset) * arrAxis.Eucf))
					  {
						  return;
					  }*/
					PointModule _pointModule = new PointModule();
					_pointModule.dblAcc = arrAxis.Acc;
					_pointModule.blnPointEnable = true;//表示点位是否启用
					_pointModule.blnIsSpecialPoint = true;//表示此点位是否是特殊点位 方便对应轴速度快速改变        
					_pointModule.dblPonitValue = nowpoint - nowpset;//点位数据
					_pointModule.dblPonitSpeed = StationManage.Speedvalue;//点位速度        
					_pointModule.dblAcc = arrAxis.Acc; //加速度
					_pointModule.dblDec = arrAxis.Dec;  //减速度
					_pointModule.dblAccTime = arrAxis.tag_accTime;//加速时间
					_pointModule.dblDecTime = arrAxis.tag_accTime;//减速时间
					_pointModule.dblPonitStartSpeed = arrAxis.StartSpeed;//初始速度
					_pointModule.db_S_Time = arrAxis.tag_S_Time;//初始速度
					resutl = NewCtrlCardV0.SR_AbsoluteMove(arrAxis, _pointModule);
					if (resutl != 0)
					{
						MessageBoxLog.Show("轴运动异常");
					}

				}
				else
				{
					/*   if (!AxisSafeManage.AxisIsSafe(tag_work._Config.tag_safeStationModule, arrAxis, 0, (nowpoint + nowpset) * arrAxis.Eucf))
					   {
						   return;
					   }*/
					PointModule _pointModule = new PointModule();
					_pointModule.dblAcc = arrAxis.Acc;
					_pointModule.blnPointEnable = true;//表示点位是否启用
					_pointModule.blnIsSpecialPoint = true;//表示此点位是否是特殊点位 方便对应轴速度快速改变        
					_pointModule.dblPonitValue = nowpoint - 6000;//点位数据
					_pointModule.dblPonitSpeed = StationManage.Speedvalue;//点位速度        
					_pointModule.dblAcc = arrAxis.Acc; //加速度
					_pointModule.dblDec = arrAxis.Dec;  //减速度
					_pointModule.dblAccTime = arrAxis.tag_accTime;//加速时间
					_pointModule.dblDecTime = arrAxis.tag_accTime;//减速时间
					_pointModule.dblPonitStartSpeed = arrAxis.StartSpeed;//初始速度
					_pointModule.db_S_Time = arrAxis.tag_S_Time;//初始速度
					resutl = NewCtrlCardV0.SR_continue_move(arrAxis, _pointModule, 1);
					if (resutl != 0)
					{
						MessageBoxLog.Show("轴运动异常");
					}

				}
			}

		}
		//往下移动停止
		private void Jog_minusBT_MouseUp(object sender, MouseEventArgs e)
		{
			Global.WorkVar.tag_isFangDaiJieChu = false;
			if (arrAxis == null)
			{
				return;
			}
			if (StationManage.Distancemode == "长距" || StationManage.Distancemode == "短距")
			{
				return;
			}

			else
			{
				resutl = NewCtrlCardV0.SR_AxisEmgStop((int)arrAxis.tag_MotionCardManufacturer, arrAxis.CardNum, arrAxis.AxisNum);
				if (resutl != 0)
				{
					MessageBoxLog.Show("轴停止异常");
				}

			}

		}
		//清除事件
		public void ClearEvent()
		{
			NewCtrlCardIO.AxisAlarmChange -= new EventHandler(NewCtrlCardSR_AxisAlarmChange);
			NewCtrlCardIO.AxisHomeChange -= new EventHandler(NewCtrlCardSR_AxisHomeChange);
			NewCtrlCardIO.AxisLimitNChange -= new EventHandler(NewCtrlCardSR_AxisLimitNChange);
			NewCtrlCardIO.AxisLimitPChange -= new EventHandler(NewCtrlCardSR_AxisLimitPChange);
			NewCtrlCardIO.AxisEncPosChange -= new EventHandler(NewCtrlCardSR_AxisEncPosChange);
			NewCtrlCardIO.AxisPrfPosChange -= new EventHandler(NewCtrlCardSR_AxisPrfPosChange);
			NewCtrlCardIO.AxisEnableChange -= new EventHandler(NewCtrlCardSR_AxisEnableChange);
		}


		public void setspeed()
		{
			//速度赋值
			switch (StationManage.Speedlevel)
			{
				case "1":
					StationManage.Speedvalue = arrAxis.ManualSpeedNormal; break;
				case "2":
					StationManage.Speedvalue = arrAxis.ManualSpeedHigh; break;
				case "3":
					StationManage.Speedvalue = arrAxis.ManualSpeedLow; break;
			}



		}



	}

}
