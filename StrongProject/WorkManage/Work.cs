using System;
using System.Collections.Generic;

using System.Threading;
namespace StrongProject
{
	/// <summary>
	/// 手动调试参数
	/// </summary>
	public class manual
	{
		/// <summary>
		/// 屏蔽作用
		/// </summary>
		public bool tag_induction;
		/// <summary>
		/// 单步运行，
		/// </summary>
		public int tag_stepName;
		/// <summary>
		/// 单步调试开关
		/// </summary>
		public int tag_step;
		/// <summary>
		/// 是否暂停
		/// </summary>
		public bool tag_isSuspend;
		/// <summary>
		/// 暂停函数
		/// </summary>
		public delegate_PointModule tag_SuspendFun;
		/// <summary>
		/// 暂停继续函数
		/// </summary>
		public delegate_PointModule tag_ContinueFun;

		/// <summary>
		/// 当前正在运行的步骤
		/// </summary>
		public PointAggregate tag_ExePointAggregate;



		/// <summary>
		/// 构造函数，初始化的时候，表示从第几步开始 
		/// </summary>
		/// <param name="ndStep"></param>
		public manual(int ndStep)
		{
			tag_stepName = ndStep;
		}

	}
	public class Work
	{
		/*
        * 点位信息，配置
        * 
        */
		public Config _Config;
		/// <summary>
		/// 复位工位
		/// </summary>
		public Rest tag_Rest;
		/// <summary>
		/// 工位管理，主要用于添加，初始化 
		/// </summary>
		public workObjectManage tag_workObjectManage;
		/// <summary>
		/// 工作工位列表，集中管理
		/// </summary>
		public List<object> tag_workObject = new List<object>();

		public List<JSerialPort> tag_JSerialPort = new List<JSerialPort>();

		public List<SocketClient> tag_SocketClient = new List<SocketClient>();

		public int[] tag_CardHave = new int[100];

		public LJV7IF_ETHERNET_CONFIG _ethernetConfig;
		/// <summary>
		/// 整个工作流程
		/// </summary>
		public Work()
		{
			_Config = Config.Load() as Config;


			StationManage._Config = _Config;
			foreach (PortParameter pp in _Config.tag_PortParameterList)
			{
				tag_JSerialPort.Add(new JSerialPort(null, pp));
			}
			foreach (IPAdrr pp in _Config.tag_IPAdrrList)
			{
				tag_SocketClient.Add(new SocketClient(pp));
			}
			tag_workObjectManage = new workObjectManage(this, tag_workObject);
			Init();
			IoCheckThreadStart();
			AllAxisAndIOCardInit();
			return;
		}
		/// <summary>
		/// 初始化轴的模式设置，初始化运用了那些卡
		/// </summary>
		/// <returns></returns>
		public short AllAxisAndIOCardInit()
		{
			int i = 0;
			while (i < _Config.axisArray.Count)
			{

				tag_CardHave[(int)_Config.axisArray[i].tag_MotionCardManufacturer] = 1;
				i++;
			}
			i = 0;
			while (i < _Config.arrWorkStation.Count)
			{
				int j = 0;
				while (j < _Config.arrWorkStation[i].arrInputIo.Count)
				{
					tag_CardHave[(int)_Config.arrWorkStation[i].arrInputIo[j].tag_MotionCardManufacturer] = 1;

					j++;
				}
				j = 0;
				while (j < _Config.arrWorkStation[i].arrOutputIo.Count)
				{
					tag_CardHave[(int)_Config.arrWorkStation[i].arrOutputIo[j].tag_MotionCardManufacturer] = 1;

					j++;
				}

				i++;
			}


			return 0;
		}

		/// <summary>
		/// 初始化轴的模式设置，初始化运用了那些卡
		/// </summary>
		/// <returns></returns>
		public short AllAxismodeInit()
		{
			int i = 0;

			while (i < _Config.axisArray.Count)
			{
				AxisConfig ax = _Config.axisArray[i];

				tag_CardHave[(int)ax.tag_MotionCardManufacturer] = 1;
				NewCtrlCardV0.set_limit_mode(ax);
				NewCtrlCardV0.set_pulse_mode(ax);
				// NewCtrlCardV0.set_io_mode(ax);
				i++;
			}



			return 0;
		}
		/// <summary>
		/// 判断是否配置该轴
		/// </summary>
		/// <param name="manu"></param>
		/// <param name="axis"></param>
		/// <returns></returns>
		public AxisConfig IshaveAxis(int manu, int card, int axis)
		{
			int i = 0;
			while (i < _Config.axisArray.Count)
			{
				AxisConfig ax = _Config.axisArray[i];
				if (manu == (int)ax.tag_MotionCardManufacturer && axis == ax.AxisNum && card == ax.CardNum)
				{
					return ax;
				}

				i++;
			}
			return null;
		}
		/// <summary>
		/// 急停所有
		/// </summary>
		public void Stop()
		{
			object[] po = new object[1];
			StationManage.StopAllAxis();
			threadStop();
			Global.WorkVar.tag_StopState = 1;
			Global.WorkVar.tag_SuspendState = 0;
			Global.WorkVar.tag_ResetState = 0;
			Global.WorkVar.tag_workState = 0;

			Global.WorkVar.InitCommunicateStatus();
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
				System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i].Name == "Stop")
					{
						po[0] = o;
						methods[i].Invoke(o, po);
					}
				}
			}
		}
		/// <summary>
		/// 暂停
		/// </summary>
		/// <param name="o1"></param>
		/// <returns></returns>
		public short Suspend(object o1)
		{

			Global.WorkVar.tag_SuspendState = 1;
			object[] po = new object[1];
			po[0] = o1;
			if (_Config.tag_PrivateSave.tag_SuspendType == 0)
			{
				StationManage.StopAllAxis();
			}
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
				System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i].Name == "Suspend")
					{
						methods[i].Invoke(o, po);
					}
				}
			}
			return 0;
		}

		/// <summary>
		/// 左工位暂停
		/// </summary>
		/// <param name="o1"></param>
		/// <returns></returns>
		public short Suspend_L(object o1)
		{
			Global.WorkVar.bSuspendState_L = true;
			object[] po = new object[1];
			po[0] = o1;
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				if (t.Name == "LeftStation")
				{
					System.Reflection.MethodInfo[] methods = t.GetMethods();
					for (int i = 0; i < methods.Length; i++)
					{
						if (methods[i].Name == "Suspend")
						{
							methods[i].Invoke(o, po);
						}
					}
				}
			}
			return 0;
		}

		/// <summary>
		/// 右工位暂停
		/// </summary>
		/// <param name="o1"></param>
		/// <returns></returns>
		public short Suspend_R(object o1)
		{
			Global.WorkVar.bSuspendState_R = true;
			object[] po = new object[1];
			po[0] = o1;
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				if (t.Name == "RightStation")
				{
					System.Reflection.MethodInfo[] methods = t.GetMethods();
					for (int i = 0; i < methods.Length; i++)
					{
						if (methods[i].Name == "Suspend")
						{
							methods[i].Invoke(o, po);
						}
					}
				}
			}
			return 0;
		}

		/// <summary>
		/// 继续函数
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public short Continue(object o1)
		{
			if (Global.WorkVar.tag_ResetState == 0)
			{
				UserControl_LogOut.OutLog("没有复位，请复位", 0);
				return -1;
			}
			if (Global.WorkVar.tag_SuspendState == 1 || Global.WorkVar.bSuspendState_L || Global.WorkVar.bSuspendState_R)
			{

				object[] po = new object[1];
				po[0] = o1;
				foreach (object o in tag_workObject)
				{
					Type t = o.GetType();
					System.Reflection.MethodInfo[] methods = t.GetMethods();
					System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
					System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
					for (int i = 0; i < methods.Length; i++)
					{
						if (methods[i].Name == "Continue")
						{
							methods[i].Invoke(o, po);
						}
					}
				}
			}
			Global.WorkVar.tag_SuspendState = 0;
			Global.WorkVar.bSuspendState_L = false;
			Global.WorkVar.bSuspendState_R = false;
			return 0;
		}

		/// <summary>
		/// 左工位继续函数
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public short Continue_L(object o1)
		{
			if (Global.WorkVar.tag_ResetState == 0)
			{
				UserControl_LogOut.OutLog("没有复位，请复位", 0);
				return -1;
			}
			object[] po = new object[1];
			po[0] = o1;
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				if (t.Name == "LeftStation")
				{
					System.Reflection.MethodInfo[] methods = t.GetMethods();
					for (int i = 0; i < methods.Length; i++)
					{
						if (methods[i].Name == "Continue")
						{
							methods[i].Invoke(o, po);
						}
					}
				}
			}

			Global.WorkVar.bSuspendState_L = false;

			return 0;
		}

		/// <summary>
		/// 右工位继续函数
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public short Continue_R(object o1)
		{
			if (Global.WorkVar.tag_ResetState == 0)
			{
				UserControl_LogOut.OutLog("没有复位，请复位", 0);
				return -1;
			}
			object[] po = new object[1];
			po[0] = o1;
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				if (t.Name == "RightStation")
				{
					System.Reflection.MethodInfo[] methods = t.GetMethods();
					for (int i = 0; i < methods.Length; i++)
					{
						if (methods[i].Name == "Continue")
						{
							methods[i].Invoke(o, po);
						}
					}
				}
			}

			Global.WorkVar.bSuspendState_R = false;

			return 0;
		}

		/// <summary>
		/// 继续函数
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public object GetStation(string stationname)
		{

			foreach (object o in tag_workObject)
			{

				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
				System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
				workBase wb = (workBase)o;
				for (int i = 0; i < methods.Length; i++)
				{

					if (wb.tag_stationName == stationname)
					{
						return wb;
					}


				}
			}
			return null;
		}

		/// <summary>
		/// 工位初始化
		/// </summary>
		/// <returns></returns>
		public short Init()
		{
			foreach (object o in tag_workObject)
			{
				if (o == null)
					return 1;
				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i].Name == "Init")
					{
						methods[i].Invoke(o, null);
					}
				}
			}

			return 0;
		}
		/// <summary>
		/// 停止所有线程
		/// </summary>
		/// <returns></returns>
		public bool threadStop()
		{
			foreach (object o in tag_workObject)
			{

				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
				System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
				workBase wb = (workBase)o;
				if (wb.tag_workThread != null && wb.tag_workThread.IsAlive)
				{
					wb.tag_workThread.Abort();


				}
			}
			return true;
		}
		public bool IsWork()
		{
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
				System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
				workBase wb = (workBase)o;
				if (wb.tag_isWork == 1)
				{
					UserControl_LogOut.OutLog(wb.tag_stationName + "：工位正在工作", 0);
					return true;
				}
			}
			return false;
		}
		/// <summary>
		/// 安全光栅是否OK
		/// </summary>
		/// <returns></returns>
		public bool IsafeLightOk()
		{
			//return true;
			if (_Config.tag_PrivateSave.tag_safeLightOffOn)
			{
				return true;
			}
			if (_Config.tag_PrivateSave.tag_safeGateOffOn)
			{
				return true;
			}
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				workBase wb = (workBase)o;
				if (wb.tag_safe != null && !wb.tag_safe(wb))
				{
					UserControl_LogOut.OutLog(wb.tag_stationName + "触发安全光栅或门限触发，急停", 0);
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// IO是否安全
		/// </summary>
		/// <returns></returns>
		public bool IsafeIOOk()
		{
			return true;
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				workBase wb = (workBase)o;
				if (pointMotion.IOIsSafe(wb.tag_manual.tag_ExePointAggregate) != 0)
				{
					return false;
				}

			}
			if (pointMotion.IOIsSafe(Global.WorkVar.tag_ExePointAggregate) != 0)
			{
				return false;
			}
			return true;
		}
		/// <summary>
		///启动列表
		/// </summary>
		public void startList()
		{
			foreach (object o in tag_workObject)
			{

				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
				System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
				workBase wb = (workBase)o;
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i].Name == "StartThread")
					{
						if (wb.tag_isRestStation == 0)
						{
							wb.tag_isWork = 0;
							wb.tag_manual.tag_stepName = 0;
							methods[i].Invoke(o, null);
						}
					}

				}
			}

			//Rc rc = Rc.Ok;
			//rc = (Rc)NativeMethods.LJV7IF_Initialize();
			//if (rc != Rc.Ok)
			//{
			//    UserControl_LogOut.OutLog("测量仪初始化失败", 0);
			//}

			try
			{
				_ethernetConfig.abyIpAddress = new byte[] {
						Convert.ToByte("192"),
						Convert.ToByte("168"),
						Convert.ToByte("1"),
						Convert.ToByte("111")
					};
				_ethernetConfig.wPortNo = Convert.ToUInt16("10000");
			}
			catch (Exception ex)
			{
				UserControl_LogOut.OutLog(ex.Message, 0);
				return;
			}

			//rc = (Rc)NativeMethods.LJV7IF_EthernetOpen(0, ref _ethernetConfig);

			//if (rc != Rc.Ok)
			//{
			//    UserControl_LogOut.OutLog("与测量仪连接失败", 0);
			//}
		}
		/// <summary>
		/// 开始启动
		/// </summary>
		public bool start()
		{
			if (Global.WorkVar.tag_ResetState == 1)
			{
				UserControl_LogOut.OutLog("复位中，请等待", 0);
				return false;
			}
			if (Global.WorkVar.tag_ResetState == 0)
			{
				UserControl_LogOut.OutLog("没有复位，请复位", 0);
				return false;
			}

			if (Global.WorkVar.tag_IsExit == 1)
			{
				UserControl_LogOut.OutLog("请到主界面按启动键", 0);
				return false;
			}
			if (Global.WorkVar.tag_StopState == 1)
			{

				UserControl_LogOut.OutLog("急停中", 0);
				return false;
			}
			if (IsWork())
			{
				UserControl_LogOut.OutLog("当前有工位正在工作，请停止正在工作的工位", 0);
				return false;
			}
			if (Global.WorkVar.tag_workState == 0)
			{
				Global.WorkVar.tag_workState = 1;
				startList();
				return true;
			}
			else
			{
				if (Global.WorkVar.tag_ResetState == 1)
				{
					UserControl_LogOut.OutLog("工作中", 0);

				}
			}
			return false;
		}
		public static bool IsMove(int type)
		{
			if (Global.WorkVar.tag_ResetState == 1)
			{

				MessageBoxLog.Show("复位中，请等待");
				return false;
			}
			if (Global.WorkVar.tag_ResetState == 0 && (!Global.WorkVar.tag_isAxisMove))
			{
				MessageBoxLog.Show("没有复位，请复位");
				return false;
			}


			if (Global.WorkVar.tag_StopState == 1 && (!Global.WorkVar.tag_isAxisMove))
			{
				MessageBoxLog.Show("急停中");
				return false;
			}
			if (Global.WorkVar.tag_workState == 1)
			{
				MessageBoxLog.Show("工作中，请调为自动状态");
				return false;
			}
			return true;
		}
		/// <summary>
		/// 复位 动作
		/// </summary>
		public void Rest()
		{

			if (tag_Rest == null)
			{
				tag_Rest = new Rest(this);
			}
			tag_Rest.start();
		}
		/// <summary>
		/// IO是否安全
		/// </summary>
		/// <returns></returns>
		public bool IsafeIOInit()
		{
			foreach (object o in tag_workObject)
			{
				Type t = o.GetType();
				workBase wb = (workBase)o;
				wb.tag_manual.tag_ExePointAggregate = null;
				Global.WorkVar.tag_ExePointAggregate = null;

			}

			return true;
		}

		/// <summary>
		/// 轴，iO安全检查
		/// </summary>
		/// <returns></returns>
		public bool AxisSafeCheck()
		{
			int count = _Config.axisArray.Count;
			int i = 0;
			while (i < count)
			{
				AxisConfig ac = _Config.axisArray[i];
				if (ac.AxisNum >= 0 && ac.CardNum >= 0 && NewCtrlCardV0.SR_GetAxisStatus(ac) == 1)
				{
					if (!AxisSafeManage.AxisIsSafe(_Config.tag_safeStationModule, ac, 0, 0))
					{
						if (Global.WorkVar.tag_isFangDaiJieChu)
						{
							return true;
						}
						return false;
					}
				}
				i++;

			}
			return true;
		}


		/// <summary>
		/// 轴，iO安全检查
		/// </summary>
		/// <returns></returns>
		public bool IoSafeCheck(StationModule sm)
		{
			try
			{
				int count = _Config.axisArray.Count;
				int i = 0;

				int CountIo = _Config.arrWorkStation.Count;
				if (sm == null)
					return true;
				while (i < count)
				{
					AxisConfig ac = _Config.axisArray[i];
					if (ac.AxisNum >= 0 && NewCtrlCardV0.SR_GetAxisStatus(ac) == 1)
					{
						if (!AxisSafeManage.IoAxisIsSafe(sm, ac, 0, 0))
						{
							if (Global.WorkVar.tag_isFangDaiJieChu)
							{
								return true;
							}
							return false;
						}
					}
					i++;

				}
				return true;
			}
			catch
			{
				return true;
			}
		}

		private bool IoAllSafeCheck()
		{
			try
			{
				if (StationManage._Config == null || StationManage._Config.arrWorkStation == null)
					return true; ;
				int i = 0;
				int j = 0;

				List<object> listObject = new List<object>();
				foreach (StationModule sm in StationManage._Config.arrWorkStation)
				{


					foreach (IOParameter ioP in sm.arrOutputIo)
					{
						if (!IoSafeCheck(ioP.tag_StationModule))
						{
							return false;
						}

					}

				}
				return true;
			}
			catch
			{
				return true;
			}
		}
		/// <summary>
		/// 判断是否右轴报警 TRUE 报警
		/// </summary>
		public bool IsAxisAlarm()
		{
			bool axio = false;
			//NewCtrlCardV0.GetInputIoBitStatus("", "Z1报警", out axio);
			if (axio)
			{
				UserControl_LogOut.OutLog("轴报警", 0);

				return true;
			}
			int CardIndex = 0;
			int axisIndex = 0;
			bool ret = false;
			for (MotionCardManufacturer i = 0; i < MotionCardManufacturer.MotionCardManufacturer_max; i++)
			{
				NewCtrlCardBase Base = NewCtrlCardV0.tag_NewCtrlCardBase[(int)i];
				short j = 0;
				short n = 0;
				short mInIo = 0;
				if (Base == null || NewCtrlCardIO.tag_CardHave[(int)i] == 0)
				{
					continue;
				}
				while (j < NewCtrlCardV0.tag_CardCount[(int)i])
				{
					axisIndex = 0;
					while (n < NewCtrlCardV0.tag_CardAxisCount[(int)i])
					{
						//tag_CardAxisAlarm[CardIndex]
						AxisConfig name = IshaveAxis((int)i, j, n);
						if (name != null)
						{
							if (name.tag_IoAlarmNHighEnable == 1)
							{
								if ((((int)NewCtrlCardIO.tag_CardAxisAlarm[CardIndex] & (1 << n)) > 0))
								{

									UserControl_LogOut.OutLog(NewCtrlCardBase.GetManufacturerName((int)i) + "第" + j + "卡" + n + "轴(" + name.AxisName + ")报警", 0);
									ret = true;
								}
							}
							else
							{
								if ((((int)NewCtrlCardIO.tag_CardAxisAlarm[CardIndex] & (1 << n)) == 0))
								{

									UserControl_LogOut.OutLog(NewCtrlCardBase.GetManufacturerName((int)i) + "第" + j + "卡" + n + "轴(" + name.AxisName + ")报警", 0);
									ret = true;
								}
							}

						}
						axisIndex++;
						n++;
					}
					CardIndex++;
					j++;
				}
				// 
			}
			return ret;
		}
		/// <summary>
		/// 判断是否报警 TRUE 报警
		/// </summary>
		public bool IsAxisLimitAlarm()
		{

			int CardIndex = 0;
			int axisIndex = 0;
			bool ret = false;
			for (MotionCardManufacturer i = 0; i < MotionCardManufacturer.MotionCardManufacturer_max; i++)
			{
				NewCtrlCardBase Base = NewCtrlCardV0.tag_NewCtrlCardBase[(int)i];
				short j = 0;
				short n = 0;
				short mInIo = 0;
				if (Base == null || NewCtrlCardIO.tag_CardHave[(int)i] == 0)
				{
					continue;
				}
				while (j < NewCtrlCardV0.tag_CardCount[(int)i])
				{
					axisIndex = 0;
					while (n < NewCtrlCardV0.tag_CardAxisCount[(int)i])
					{
						AxisConfig name = IshaveAxis((int)i, j, n);
						if (name != null)
						{
							if (name.tag_IoLimtPNHighEnable == 1)
							{
								if ((((int)NewCtrlCardIO.tag_CardAxisLimitNIO[CardIndex] & (1 << n)) > 0) && name.tag_IoLimtNEnable == 0)
								{
									UserControl_LogOut.OutLog(NewCtrlCardBase.GetManufacturerName((int)i) + "第" + j + "卡" + n + "轴负限位(" + name.AxisName + ")报警", 0);
									ret = true;
								}
								if ((((int)NewCtrlCardIO.tag_CardAxisLimitPIO[CardIndex] & (1 << n)) > 0) && name.tag_IoLimtPEnable == 0)
								{
									UserControl_LogOut.OutLog(NewCtrlCardBase.GetManufacturerName((int)i) + "第" + j + "卡" + n + "轴正限位(" + name.AxisName + ")报警", 0);
									ret = true;
								}
							}
							else
							{
								if ((((int)NewCtrlCardIO.tag_CardAxisLimitNIO[CardIndex] & (1 << n)) == 0) && name.tag_IoLimtNEnable == 0)
								{
									UserControl_LogOut.OutLog(NewCtrlCardBase.GetManufacturerName((int)i) + "第" + j + "卡" + n + "轴负限位(" + name.AxisName + ")报警", 0);
									ret = true;
								}
								if ((((int)NewCtrlCardIO.tag_CardAxisLimitPIO[CardIndex] & (1 << n)) == 0) && name.tag_IoLimtPEnable == 0)
								{
									UserControl_LogOut.OutLog(NewCtrlCardBase.GetManufacturerName((int)i) + "第" + j + "卡" + n + "轴正限位(" + name.AxisName + ")报警", 0);
									ret = true;
								}
							}

						}
						axisIndex++;
						n++;
					}

					CardIndex++;
					j++;
				}
				// 
			}
			return ret;
		}

		public void CheckIoThread(object o)
		{
			while (true)
			{
				bool suspendIo = false;
				bool stopIoS = false;
				bool RestIoS = false;
				bool AxisAlarm = false;
				bool AxisLimitAlarm = false;
				bool SafetyDoorIoS = false;//安全门
				bool RasterIoS_L = false;//左安全光栅
				bool RasterIoS_R = false;//右安全光栅


				if (NewCtrlCardV0.tag_isInit == 0)
				{
					Thread.Sleep(100);
					continue;
				}
				IOParameter SuspendIo = StationManage.FindInputIo("暂停");
				IOParameter StopIo = StationManage.FindInputIo("急停");
				IOParameter RestIo = StationManage.FindInputIo("复位");
				IOParameter Raster_L = StationManage.FindInputIo("左安全光栅");
				IOParameter Raster_R = StationManage.FindInputIo("右安全光栅");
				IOParameter SafetyDoor = StationManage.FindInputIo("安全门");


				//AxisAlarm =  IsAxisAlarm();
				AxisLimitAlarm = IsAxisLimitAlarm();
				if (SuspendIo == null)
				{
					UserControl_LogOut.OutLog("请配置<暂停IO>", 0);
					Thread.Sleep(100);
					continue;
				}
				if (StopIo == null)
				{
					UserControl_LogOut.OutLog("请配置<急停IO>", 0);
					Thread.Sleep(100);
					continue;
				}
				if (RestIo == null)
				{
					UserControl_LogOut.OutLog("请配置<复位IO>", 0);
					Thread.Sleep(100);
					continue;
				}
				if (Raster_L == null)
				{
					UserControl_LogOut.OutLog("请配置<左安全光栅IO>", 0);
					Thread.Sleep(100);
					continue;
				}
				if (Raster_R == null)
				{
					UserControl_LogOut.OutLog("请配置<右安全光栅IO>", 0);
					Thread.Sleep(100);
					continue;
				}
				if (SafetyDoor == null)
				{
					UserControl_LogOut.OutLog("请配置<安全门IO>", 0);
					Thread.Sleep(100);
					continue;
				}


				NewCtrlCardV0.GetInputIoBitStatus("", "暂停", out suspendIo);
				NewCtrlCardV0.GetInputIoBitStatus("", "复位", out RestIoS);
				NewCtrlCardV0.GetInputIoBitStatus("", "急停", out stopIoS);
				NewCtrlCardV0.GetInputIoBitStatus("", "安全门", out SafetyDoorIoS);
				NewCtrlCardV0.GetInputIoBitStatus("", "左安全光栅", out RasterIoS_L);
				NewCtrlCardV0.GetInputIoBitStatus("", "右安全光栅", out RasterIoS_R);
				//if (!stopIoS || Global.WorkVar.tag_StopState == 2 || !IsafeIOOk() || !IoAllSafeCheck() || !AxisSafeCheck() || AxisAlarm || (AxisLimitAlarm && Global.WorkVar.tag_ResetState != 1))
				if (!stopIoS || Global.WorkVar.tag_StopState == 2 || !IsafeIOOk() || !IoAllSafeCheck() || !AxisSafeCheck() || AxisAlarm)
				{
					IsafeIOInit();
					Stop();
					Thread.Sleep(100);
					continue;
				}
				else if (((!SafetyDoorIoS && !_Config.tag_PrivateSave.tag_safeGateOffOn) || suspendIo) && Global.WorkVar.tag_SuspendState == 0 && (Global.WorkVar.bWork_L || Global.WorkVar.bWork_R))
				{
					Suspend(null);
					Thread.Sleep(500);
				}
				else if (RestIoS)
				{
					Global.WorkVar.tag_StopState = 0;
					if (Global.WorkVar.tag_ResetState != 1 && Global.WorkVar.tag_workState == 0)
					{
						Rest();
					}
					else
					{

					}
					continue;
				}
				else if (suspendIo && Global.WorkVar.tag_SuspendState == 1)
				{
					Continue(null);
					Thread.Sleep(500);
					continue;
				}
				else if (Global.WorkVar.bcanRunFalg == true && Global.WorkVar.tag_ResetState == 2 && Global.WorkVar.tag_workState == 0)
				{
					Global.WorkVar.bcanRunFalg = false;
					if (start())
					{
						Global.WorkVar.bcanRunFalg = true;
					}
					continue;
				}
				if (!RasterIoS_L && !_Config.tag_PrivateSave.tag_safeLightOffOn && Global.WorkVar.bWork_L && !Global.WorkVar.bSuspendState_L)
				{
					Suspend_L(null);
				}
				else if (RasterIoS_L && Global.WorkVar.bSuspendState_L)
				{
					Continue_L(null);
				}
				if (!RasterIoS_R && !_Config.tag_PrivateSave.tag_safeLightOffOn && Global.WorkVar.bWork_R && !Global.WorkVar.bSuspendState_R)
				{
					Suspend_R(null);
				}
				else if (RasterIoS_R && Global.WorkVar.bSuspendState_R)
				{
					Continue_R(null);
				}
				Thread.Sleep(100);
			}
		}


		/// <summary>
		/// io检查，
		/// </summary>
		public void IoCheckThreadStart()
		{
			Thread tag_IoCheckThread = new Thread(new ParameterizedThreadStart(CheckIoThread));
			tag_IoCheckThread.IsBackground = true;
			tag_IoCheckThread.Start(null);
		}

		/// <summary>
		/// 
		/// </summary>
		public Config Config
		{
			get { return _Config; }
			set { _Config = value; }
		}

	}

}
