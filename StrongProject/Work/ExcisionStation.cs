using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace StrongProject
{
	public class ExcisionStation : workBase
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
		public SocketClient tag_SocketClient0;

		public bool isLeftStation;

		char[] SpinLock = new char[1];
		string[] retArr;
		IOParameter axisRCoil;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="ConstructCreate"></param>
		/// <returns></returns>
		public ExcisionStation(Work _Work)
		{
			tag_Work = _Work;
			tag_SocketClient0 = _Work.tag_SocketClient[0];
			tag_stationName = "切电池工位";
			tag_isRestStation = 0;
			SpinLock[0] = ',';
			axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
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
		/// 切刀气缸上升，第1步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step1"></param>
		/// <returns></returns>
		public short Step1(object o)
		{

			return 0;
		}
		/// <summary>
		/// 等待左或右工位启动，第2步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step2"></param>
		/// <returns></returns>
		public short Step2(object o)
		{

			while (true)
			{
				if (Global.WorkVar.tag_LeftStart == true)
				{
					//Global.WorkVar.tag_LeftStart = false;
					isLeftStation = true;
					return 0;
				}
				else if (Global.WorkVar.tag_RightStart == true)
				{
					//Global.WorkVar.tag_RightStart = false;
					tag_manual.tag_stepName = 3;
					isLeftStation = false;
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
		/// X轴到左工作位，第3步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step3"></param>
		/// <returns></returns>
		public short Step3(object o)
		{
			Global.WorkVar.tag_LeftStart = false;
			tag_manual.tag_stepName = 4;
			return 0;
		}
		/// <summary>
		/// X轴到右工作位，第4步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step4"></param>
		/// <returns></returns>
		public short Step4(object o)
		{
			Global.WorkVar.tag_RightStart = false;
			return 0;
		}
		/// <summary>
		/// 等待电池测试完成，第5步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step5"></param>
		/// <returns></returns>
		public short Step5(object o)
		{
			//return 0;
			while (true)
			{
				if (isLeftStation && Global.WorkVar.bPCBTestFinish_L == true)
				{
					//Global.WorkVar.bPCBTestFinish_L = false;
					return 0;
				}
				else if (!isLeftStation && Global.WorkVar.bPCBTestFinish_R == true)
				{
					//Global.WorkVar.bPCBTestFinish_R = false;
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
		/// 相机拍照，第6步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step6"></param>
		/// <returns></returns>
		public short Step6(object o)
		{
			if (isLeftStation)
				Global.WorkVar.bPCBTestFinish_L = false;
			else
				Global.WorkVar.bPCBTestFinish_R = false;
			if (Global.WorkVar.bEmptyRun)
			{
				tag_SocketClient0.Send("T1,1\r\n", 1000);
			}
			else
			{
				try
				{
					string strSend;
					PointAggregate p = (PointAggregate)o;
					Global.WorkVar.strCCDPosition_X = p.arrPoint[0].dblPonitValue.ToString();
					Global.WorkVar.strCCDPosition_A = p.arrPoint[1].dblPonitValue.ToString();
					if (isLeftStation)
					{
						strSend = "T1,1,0," + Global.WorkVar.strCCDPosition_X + "," + Global.WorkVar.strCCDPosition_Y_L
							+ "," + Global.WorkVar.strCCDPosition_A + "," + Global.WorkVar.CCDMode + "\r\n";
					}
					else
					{
						strSend = "T1,1,1," + Global.WorkVar.strCCDPosition_X + "," + Global.WorkVar.strCCDPosition_Y_R
							+ "," + Global.WorkVar.strCCDPosition_A + "," + Global.WorkVar.CCDMode + "\r\n";
					}
					CFile.WriteCCDData(DateTime.Now.ToString() + "  Send  " + strSend);
					string strReceive = tag_SocketClient0.Send(strSend, 1000);
					CFile.WriteCCDData(DateTime.Now.ToString() + "  Receive  " + strReceive + "\r\n");

					if (strReceive == "")
					{
						if (MessageBox.Show("相机通讯异常,确认是否重新拍照？重新拍照点击确认", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
						{
							tag_manual.tag_stepName = tag_manual.tag_stepName - 1;
							return 0;
						}
						else
						{
							return -1;
						}
					}
					retArr = strReceive.Split(SpinLock);
					int endIndex = retArr[retArr.Count() - 1].IndexOf("\r\n", 0, retArr[retArr.Count() - 1].Length - 1);
					retArr[retArr.Count() - 1] = retArr[retArr.Count() - 1].Substring(0, endIndex);

					if (retArr[2] == "0")
					{
						if (MessageBox.Show("相机拍照失败,确认是否重新拍照？重新拍照点击确认", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
						{
							tag_manual.tag_stepName = tag_manual.tag_stepName - 1;
							return 0;
						}
						else
						{
							return -1;
						}
					}
					else
					{
						Global.WorkVar.dCCDPosition_X = Convert.ToDouble(retArr[3]);
						if (isLeftStation)
							Global.WorkVar.dCCDPosition_Y_L = Convert.ToDouble(retArr[4]);
						else
							Global.WorkVar.dCCDPosition_Y_R = Convert.ToDouble(retArr[4]);
						Global.WorkVar.dCCDPosition_A = Convert.ToDouble(retArr[5]);
					}

				}
				catch
				{
					MessageBox.Show("相机返回数据异常，请检查后，重新启动");
					return -1;
				}
			}
			if (isLeftStation)
			{
				Global.WorkVar.bCCDFinish_L = true;
			}
			else
			{
				Global.WorkVar.bCCDFinish_R = true;
			}
			return 0;
		}
		/// <summary>
		/// X轴、R轴到切电池1位，第7步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step7"></param>
		/// <returns></returns>
		public short Step7(object o)
		{
			//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
			Thread.Sleep(100);
			if (isLeftStation)
			{
				if (Global.WorkVar.bEmptyRun)
				{
					NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "X轴", "左切电池1位", 2);
					NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "R轴", "左切电池1位", 3);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "X轴", "左切电池1位", 2) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "R轴", "左切电池1位", 3) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
				else
				{
					StationModule stationM = StationManage.FindStation("空跑点位");
					AxisConfig axisX = StationManage.FindAxis(stationM, "X轴");
					AxisConfig axisA = StationManage.FindAxis(stationM, "R轴");
					PointAggregate pointA = StationManage.FindPoint(stationM, "左切电池1位");
					double speedX = pointA.arrPoint[0].dblPonitSpeed;
					double speedA = pointA.arrPoint[1].dblPonitSpeed;
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX);
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
			}
			else
			{
				if (Global.WorkVar.bEmptyRun)
				{
					NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "X轴", "右切电池1位", 2);
					NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "R轴", "右切电池1位", 3);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "X轴", "右切电池1位", 2) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "R轴", "右切电池1位", 3) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
				else
				{
					StationModule stationM = StationManage.FindStation("空跑点位");
					AxisConfig axisX = StationManage.FindAxis(stationM, "X轴");
					AxisConfig axisA = StationManage.FindAxis(stationM, "R轴");
					PointAggregate pointA = StationManage.FindPoint(stationM, "右切电池1位");
					double speedX = pointA.arrPoint[0].dblPonitSpeed;
					double speedA = pointA.arrPoint[1].dblPonitSpeed;
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX);
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
			}

			return 0;
		}
		/// <summary>
		/// 等待Y轴到切电池1位，第8步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step8"></param>
		/// <returns></returns>
		public short Step8(object o)
		{
			while (true)
			{
				if (isLeftStation && Global.WorkVar.bYAxisArrive_L[0] == true)
				{
					//Global.WorkVar.bYAxisArrive_L[0] = false;
					NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
					return 0;
				}
				else if (!isLeftStation && Global.WorkVar.bYAxisArrive_R[0] == true)
				{
					//Global.WorkVar.bYAxisArrive_R[0] = false;
					NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
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
			//return 0;
		}
		/// <summary>
		/// 切刀气缸下降1，第9步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step9"></param>
		/// <returns></returns>
		public short Step9(object o)
		{
			if (isLeftStation)
			{
				Global.WorkVar.bYAxisArrive_L[0] = false;
			}
			else
			{
				Global.WorkVar.bYAxisArrive_R[0] = false;
			}
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 切刀气缸上升1，第10步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step10"></param>
		/// <returns></returns>
		public short Step10(object o)
		{
			if (isLeftStation)
			{
				Global.WorkVar.bExcisionFinish_L[0] = true;
			}
			else
			{
				Global.WorkVar.bExcisionFinish_R[0] = true;
			}

			NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
			return 0;
		}
		/// <summary>
		/// X轴、R轴到切电池2位，第11步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step11"></param>
		/// <returns></returns>
		public short Step11(object o)
		{
			//IOParameter axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
			//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
			Thread.Sleep(100);
			if (isLeftStation)
			{
				if (Global.WorkVar.bEmptyRun)
				{
					NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "X轴", "左切电池2位", 2);
					NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "R轴", "左切电池2位", 3);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "X轴", "左切电池2位", 2) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "R轴", "左切电池2位", 3) != 0)
					{
						return -1;
					}

					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
				else
				{
					StationModule stationM = StationManage.FindStation("空跑点位");
					AxisConfig axisX = StationManage.FindAxis(stationM, "X轴");
					AxisConfig axisA = StationManage.FindAxis(stationM, "R轴");
					PointAggregate pointA = StationManage.FindPoint(stationM, "左切电池2位");
					double speedX = pointA.arrPoint[0].dblPonitSpeed;
					double speedA = pointA.arrPoint[1].dblPonitSpeed;
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX);
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
			}
			else
			{
				if (Global.WorkVar.bEmptyRun)
				{
					NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "X轴", "右切电池2位", 2);
					NewCtrlCardV0.AxisAbsoluteMove("空跑点位", "R轴", "右切电池2位", 3);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "X轴", "右切电池2位", 2) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("空跑点位", "R轴", "右切电池2位", 3) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
				else
				{
					StationModule stationM = StationManage.FindStation("空跑点位");
					AxisConfig axisX = StationManage.FindAxis(stationM, "X轴");
					AxisConfig axisA = StationManage.FindAxis(stationM, "R轴");
					PointAggregate pointA = StationManage.FindPoint(stationM, "右切电池2位");
					double speedX = pointA.arrPoint[0].dblPonitSpeed;
					double speedA = pointA.arrPoint[1].dblPonitSpeed;
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX);
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("切电池工位", "R轴刹车线圈");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
			}
			return 0;
		}
		/// <summary>
		/// 等待Y轴到切电池2位，第12步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step12"></param>
		/// <returns></returns>
		public short Step12(object o)
		{
			while (true)
			{
				if (isLeftStation && Global.WorkVar.bYAxisArrive_L[1] == true)
				{
					//Global.WorkVar.bYAxisArrive_L[1] = false;

					NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
					return 0;
				}
				else if (!isLeftStation && Global.WorkVar.bYAxisArrive_R[1] == true)
				{
					//Global.WorkVar.bYAxisArrive_R[1] = false;

					NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
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
		/// 切刀气缸下降2，第13步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step13"></param>
		/// <returns></returns>
		public short Step13(object o)
		{
			if (isLeftStation)
			{
				Global.WorkVar.bYAxisArrive_L[1] = false;
			}
			else
			{
				Global.WorkVar.bYAxisArrive_R[1] = false;
			}
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 切刀气缸上升2，第14步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step14"></param>
		/// <returns></returns>
		public short Step14(object o)
		{
			NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
			if (isLeftStation)
			{
				Global.WorkVar.bExcisionFinish_L[1] = true;
			}
			else
			{
				Global.WorkVar.bExcisionFinish_R[1] = true;
			}
			if (Global.WorkVar.tag_LeftStart == true)
			{
				//Global.WorkVar.tag_LeftStart = false;
				tag_manual.tag_stepName = 2;
				isLeftStation = true;
				return 0;
			}
			else if (Global.WorkVar.tag_RightStart == true)
			{
				//Global.WorkVar.tag_RightStart = false;
				tag_manual.tag_stepName = 3;
				isLeftStation = false;
				return 0;
			}
			return 0;
		}

		/// <summary>
		/// X轴、R轴到待机位，第15步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step23"></param>
		/// <returns></returns>
		public short Step15(object o)
		{

			return 0;
		}
		/// <summary>
		/// 工位结束，第16步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step24"></param>
		/// <returns></returns>
		public short Step16(object o)
		{
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
			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;
			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "等待左或右工位启动", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;
			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "X轴到左工作位", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_EndFun = Step3;
			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "X轴到右工作位", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "等待电池测试完成", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5;
			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "相机拍照", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_BeginFun = Step6;
			PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "X轴、R轴到切电池1位", 7);
			if (_Step7 == null)
			{
				return -1;
			}
			_Step7.tag_BeginFun = Step7;
			PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "等待Y轴到切电池1位", 8);
			if (_Step8 == null)
			{
				return -1;
			}
			_Step8.tag_BeginFun = Step8;
			PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降1", 9);
			if (_Step9 == null)
			{
				return -1;
			}
			_Step9.tag_EndFun = Step9;
			PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升1", 10);
			if (_Step10 == null)
			{
				return -1;
			}
			_Step10.tag_EndFun = Step10;
			PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "X轴、R轴到切电池2位", 11);
			if (_Step11 == null)
			{
				return -1;
			}
			_Step11.tag_BeginFun = Step11;
			PointAggregate _Step12 = pointMotion.FindPoint(tag_stationName, "等待Y轴到切电池2位", 12);
			if (_Step12 == null)
			{
				return -1;
			}
			_Step12.tag_BeginFun = Step12;
			PointAggregate _Step13 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降2", 13);
			if (_Step13 == null)
			{
				return -1;
			}
			_Step13.tag_EndFun = Step13;
			PointAggregate _Step14 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升2", 14);
			if (_Step14 == null)
			{
				return -1;
			}
			_Step14.tag_EndFun = Step14;
			PointAggregate _Step15 = pointMotion.FindPoint(tag_stationName, "X轴、R轴到待机位", 15);
			if (_Step15 == null)
			{
				return -1;
			}
			_Step15.tag_BeginFun = Step15;
			PointAggregate _Step16 = pointMotion.FindPoint(tag_stationName, "工位结束", 16);
			if (_Step16 == null)
			{
				return -1;
			}
			_Step16.tag_BeginFun = Step16;
			return 0;
		}
		/// <summary>
		/// 中断函数
		/// </summary>
		/// <param name="Suspend"></param>
		/// <returns></returns>
		public short Suspend(object o)
		{
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
