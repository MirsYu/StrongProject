using System.Linq;
using System.Threading;
namespace StrongProject
{
	public class LeftStation : workBase
	{   /// <summary>
		///  work
		/// </summary>
		public Work tag_Work;
		/// <summary>
		/// 线程
		/// </summary>
		public Thread tag_workThread;
		/// <summary>
		/// <summary>
		/// 
		/// </summary>
		public JSerialPort tag_JSerialPort0;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="ConstructCreate"></param>
		/// <returns></returns>
		public LeftStation(Work _Work)
		{
			tag_Work = _Work;
			tag_JSerialPort0 = _Work.tag_JSerialPort[0];
			tag_stationName = "左工位";
			tag_isRestStation = 0;
		}
		/// <summary>
		/// 启动函数，主要是线程启动
		/// </summary>
		/// <param name="start"></param>
		/// <returns></returns>
		public bool StartThread()
		{
			if (tag_workThread != null)
			{
				tag_workThread.Abort();
			}
			tag_workThread = new Thread(new ParameterizedThreadStart(workThread));
			tag_manual.tag_stepName = 0;
			tag_workThread.Start();
			tag_workThread.IsBackground = true;
			return true;
		}
		/// <summary>
		/// 退出函数，调用本函数，流程推出
		/// </summary>
		/// <param name="exit"></param>
		/// <returns></returns>
		public bool exit()
		{
			tag_manual.tag_stepName = 100000;
			tag_isWork = 0;
			return true;
		}
		/// <summary>
		/// 工位开始，第0步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step0"></param>
		/// <returns></returns>
		public short Step0(object o)
		{

			tag_isWork = 1; ;
			return 0;
		}
		/// <summary>
		/// 等待左工位双启，第1步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step1"></param>
		/// <returns></returns>
		public short Step1(object o)
		{
			bool start_L;
			bool start_M;
			if (Global.WorkVar.bEmptyRun && !Global.WorkVar.bEmptyRunWithBattery)
			{
				Global.WorkVar.tag_LeftStart = true;
				Global.WorkVar.bWork_L = true;
				return 0;
			}
			while (true)
			{
				if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "左启动", out start_L) != 0)
				{
					UserControl_LogOut.OutLog("获取左启动按钮输入状态失败!", 0);
					return -1;
				}
				if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "中启动", out start_M) != 0)
				{
					UserControl_LogOut.OutLog("获取中启动按钮输入状态失败!", 0);
					return -1;
				}
				if ((start_L && start_M))
				{
					Global.WorkVar.tag_LeftStart = true;
					Global.WorkVar.bWork_L = true;
					return 0;
				}
				else
				{
					Thread.Sleep(10);
				}

				if (IsExit())
				{
					return -1;
				}
			}
			return 0;
		}
		/// <summary>
		/// 开真空并检测真空吸，第2步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step2"></param>
		/// <returns></returns>
		public short Step2(object o)
		{

			return 0;
		}
		/// <summary>
		/// 左Y到拍照位，第3步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step3"></param>
		/// <returns></returns>
		public short Step3(object o)
		{

			return 0;
		}
		/// <summary>
		/// 左测试离合气缸下降，第4步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step4"></param>
		/// <returns></returns>
		public short Step4(object o)
		{

			return 0;
		}
		/// <summary>
		/// 等待电路板测试，第5步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step5"></param>
		/// <returns></returns>
		public short Step5(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fTestCYTime));
			string strRec;
			int startIndex, endIndex;
			strRec = tag_JSerialPort0.sendCommand("Read Battery SN\r\n", 2000);
			if (strRec == "Read Battery SN Fail\r\n")
			{

			}
			else if (strRec.Contains("Battery SN:"))
			{
				startIndex = strRec.IndexOf(":", 0, strRec.Length - 1);
				endIndex = strRec.IndexOf("\r\n", 0, strRec.Length - 1);
				Global.WorkVar.strBatterySN_L = strRec.Substring(startIndex + 1, endIndex - startIndex - 1);
			}
			else
			{

			}
			strRec = tag_JSerialPort0.sendCommand("Read Battery Voltage\r\n", 2000);
			if (strRec == "Read Battery Voltage Fail\r\n")
			{

			}
			else if (strRec.Contains("Battery Voltage:"))
			{
				startIndex = strRec.IndexOf(":", 0, strRec.Length - 1);
				endIndex = strRec.IndexOf("mV\r\n", 0, strRec.Length - 1);
				Global.WorkVar.strBatteryVoltage_L = strRec.Substring(startIndex + 1, endIndex - startIndex - 1);
			}
			else
			{

			}
			return 0;
		}
		/// <summary>
		/// 左测试离合气缸上升，第6步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step6"></param>
		/// <returns></returns>
		public short Step6(object o)
		{

			return 0;
		}
		/// <summary>
		/// 左Y到拍照位并等待拍照，第7步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step7"></param>
		/// <returns></returns>
		public short Step7(object o)
		{
			PointAggregate p = (PointAggregate)o;
			Global.WorkVar.strCCDPosition_Y_L = p.arrPoint[0].dblPonitValue.ToString();
			Global.WorkVar.bPCBTestFinish_L = true;
			while (true)
			{
				if (Global.WorkVar.bCCDFinish_L == true)
				{
					//Global.WorkVar.bCCDFinish_L = false;
					return 0;
				}
				else
				{
					Thread.Sleep(10);
				}

				if (IsExit())
				{
					return -1;
				}
			}
			return 0;
		}
		/// <summary>
		/// 左Y到切电池1位，第8步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step8"></param>
		/// <returns></returns>
		public short Step8(object o)
		{
			Global.WorkVar.bCCDFinish_L = false;
			if (Global.WorkVar.bEmptyRun)
			{
				//NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "左Y轴", "左切电池1位", 0);
				//NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[0]);
				if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "左Y轴", "左切电池1位", 0) != 0)
				{
					return -1;
				}
			}
			else
			{
				StationModule stationM = StationManage.FindStation("空跑点位");
				AxisConfig axisC = StationManage.FindAxis(stationM, "左Y轴");
				PointAggregate pointA = StationManage.FindPoint(stationM, "左切电池1位");
				double speed = pointA.arrPoint[0].dblPonitSpeed;
				//NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[0], Global.WorkVar.dCCDPosition_Y_L, speed);
				//NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[0]);
				if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[0], Global.WorkVar.dCCDPosition_Y_L, speed) != 0)
				{
					return -1;
				}
			}
			Global.WorkVar.bYAxisArrive_L[0] = true;
			return 0;
		}
		/// <summary>
		/// 等待切电池完成1，第9步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step9"></param>
		/// <returns></returns>
		public short Step9(object o)
		{
			while (true)
			{
				if (Global.WorkVar.bExcisionFinish_L[0] == true)
				{
					//Global.WorkVar.bExcisionFinish_L[0] = false;
					return 0;
				}
				else
				{
					Thread.Sleep(10);
				}

				if (IsExit())
				{
					return -1;
				}
			}
		}
		/// <summary>
		/// 左Y到切电池2位，第10步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step10"></param>
		/// <returns></returns>
		public short Step10(object o)
		{
			Global.WorkVar.bExcisionFinish_L[0] = false;
			if (Global.WorkVar.bEmptyRun)
			{
				if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "左Y轴", "左切电池2位", 0) != 0)
				{
					return -1;
				}
				//NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "左Y轴", "左切电池2位", 0);
				//NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[0]);
			}
			else
			{
				StationModule stationM = StationManage.FindStation("空跑点位");
				AxisConfig axisC = StationManage.FindAxis(stationM, "左Y轴");
				PointAggregate pointA = StationManage.FindPoint(stationM, "左切电池2位");
				double speed = pointA.arrPoint[0].dblPonitSpeed;
				//NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[0], Global.WorkVar.dCCDPosition_Y_L, speed);
				//NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[0]);
				if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[0], Global.WorkVar.dCCDPosition_Y_L, speed) != 0)
				{
					return -1;
				}
			}
			Global.WorkVar.bYAxisArrive_L[1] = true;
			return 0;
		}
		/// <summary>
		/// 等待切电池完成2，第11步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step11"></param>
		/// <returns></returns>
		public short Step11(object o)
		{
			//return 0;
			while (true)
			{
				if (Global.WorkVar.bExcisionFinish_L[1] == true)
				{
					//Global.WorkVar.bExcisionFinish_L[1] = false;
					return 0;
				}
				else
				{
					Thread.Sleep(10);
				}

				if (IsExit())
				{
					return -1;
				}
			}
			return 0;
		}
		/// <summary>
		/// 左Y到带料位，第12步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step16"></param>
		/// <returns></returns>
		public short Step12(object o)
		{
			Global.WorkVar.bExcisionFinish_L[1] = false;
			return 0;
		}
		/// <summary>
		/// 关真空，第13步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step17"></param>
		/// <returns></returns>
		public short Step13(object o)
		{

			return 0;
		}
		/// <summary>
		/// 工位结束，第14步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step18"></param>
		/// <returns></returns>
		public short Step14(object o)
		{

			Global.WorkVar.bWork_L = false;
			tag_manual.tag_stepName = -1;
			return 0;
		}
		/// <summary>
		/// 初始化函数，初始化流程嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="init"></param>
		/// <returns></returns>
		public short Init()
		{
			PointAggregate _Step0 = pointMotion.FindPoint(tag_stationName, "工位开始", 0);
			if (_Step0 == null)
			{
				return -1;
			}
			_Step0.tag_BeginFun = Step0;
			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "等待左工位双启", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;
			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "开真空并检测真空吸", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;
			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "左Y到测试位", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_BeginFun = Step3;
			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "左测试离合气缸下降", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "等待电路板测试", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5;
			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "左测试离合气缸上升", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_EndFun = Step6;
			PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "左Y到拍照位并等待拍照", 7);
			if (_Step7 == null)
			{
				return -1;
			}
			_Step7.tag_EndFun = Step7;
			PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "左Y到切电池1位", 8);
			if (_Step8 == null)
			{
				return -1;
			}
			_Step8.tag_EndFun = Step8;
			PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "等待切电池完成1", 9);
			if (_Step9 == null)
			{
				return -1;
			}
			_Step9.tag_BeginFun = Step9;
			PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "左Y到切电池2位", 10);
			if (_Step10 == null)
			{
				return -1;
			}
			_Step10.tag_EndFun = Step10;
			PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "等待切电池完成2", 11);
			if (_Step11 == null)
			{
				return -1;
			}
			_Step11.tag_BeginFun = Step11;
			PointAggregate _Step12 = pointMotion.FindPoint(tag_stationName, "左Y到待料位", 12);
			if (_Step12 == null)
			{
				return -1;
			}
			_Step12.tag_BeginFun = Step12;
			PointAggregate _Step13 = pointMotion.FindPoint(tag_stationName, "关真空", 13);
			if (_Step13 == null)
			{
				return -1;
			}
			_Step13.tag_BeginFun = Step13;
			PointAggregate _Step14 = pointMotion.FindPoint(tag_stationName, "工位结束", 14);
			if (_Step14 == null)
			{
				return -1;
			}
			_Step14.tag_BeginFun = Step14;
			return 0;
		}
		/// <summary>
		/// 中断函数
		/// </summary>
		/// <param name="Suspend"></param>
		/// <returns></returns>
		public short Suspend(object o)
		{
			StationModule stationM = StationManage.FindStation(tag_stationName);
			for (int i = 0; i < stationM.arrAxis.Count(); i++)
			{
				if (NewCtrlCardV0.SR_AxisStop((int)stationM.arrAxis[i].tag_MotionCardManufacturer, stationM.arrAxis[i].CardNum, stationM.arrAxis[i].AxisNum) == 0)
				{

				}
				else
				{
					return -1;
				}
			}
			if (tag_Work._Config.tag_PrivateSave.tag_SuspendType == 0)
			{
				if (tag_manual.tag_stepName > 0)
				{
					tag_manual.tag_stepName = tag_manual.tag_stepName - 1;
				}
			}
			return 0;
		}
		/// <summary>
		/// 继续函数
		/// </summary>
		/// <param name="Continue"></param>
		/// <returns></returns>
		public short Continue(object o)
		{

			return 0;
		}
		/// <summary>
		/// 流程执行的函数 返回0 表示成功
		/// </summary>
		/// <param name="work"></param>
		/// <returns></returns>
		public short Start(object o)
		{
			short ret = 0;
			if (Init() == 0)
			{
				if (tag_manual.tag_step == 0)
				{
					ret = pointMotion.StationRun(tag_stationName, tag_manual);
					tag_manual.tag_stepName = 0;
				}
				tag_isWork = -1;
				return ret;
			}
			else
			{
				return -1;
			}
			return 0;
		}
		/// <summary>
		/// 流程用线程执行执行的函数 无返回值
		/// </summary>
		/// <param name="workThreadCreate"></param>
		/// <returns></returns>
		public void workThread(object o)
		{
			short ret = 0;
			if (Init() == 0)
			{
				//while (true)
				//{
				if (tag_manual.tag_step == 0)
				{
					tag_isWork = pointMotion.StationRun(tag_stationName, tag_manual);
					tag_manual.tag_stepName = 0;
				}
				//    Thread.Sleep(10);
				//}
			}
		}
	}
}
