using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Media3D;
using static StrongProject.Global;

namespace StrongProject
{
	public class PasteStation : workBase
	{
		public Work tag_Work;
		public Thread tag_workThread;
		/// <summary>
		/// 皮带线串口
		/// </summary>
		public JSerialPort tag_Assemblyline;
		/// <summary>
		/// 上相机套接字
		/// </summary>
		public SocketClient tag_UpCam;

		public List<Point6D> FoamPoints = new List<Point6D>();
		public List<Point6D> NozzlePoints = new List<Point6D>();
		public List<Point6D> TrayPoints = new List<Point6D>();
		public int NozzleStaut = -1;
		

		public string ErrMsg = "";
		public string strOBG = "";

		/// <summary>
		/// 泡棉数量
		/// </summary>
		private int FoamCount = 6;
		/// <summary>
		/// 吸嘴数量
		/// </summary>
		private int NozzleCount = 2;
		/// <summary>
		/// Tray盘穴位
		/// </summary>
		private int TrayCount = 60;
		private int TrayIndex = 0;

		private int loop1Index = -1, loop2Index = -1, loop3Index = -1,
			loop1Count = -1, loop2Count = -1, loop3Count = -1;

		private double camX = -1, camY = -1, camR = -1;

		private Thread thread = null;
		private static readonly ILog log = LogManager.GetLogger("EmptyRun.cs");

		public PasteStation(Work _Work)
		{
			tag_Work = _Work;
			tag_stationName = "贴合工位";
			tag_Assemblyline = _Work.tag_JSerialPort[0];
			tag_UpCam = _Work.tag_SocketClient[0];
			tag_isRestStation = 0;
		}

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

		public void workThread(object o)
		{
			if (Init() == 0)
			{
				if (tag_manual.tag_step == 0)
				{
					tag_isWork = pointMotion.StationRun(tag_stationName, tag_manual);
					tag_manual.tag_stepName = 0;
				}
			}
		}

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
		}

		public short Suspend(object o)
		{
			return 0;
		}

		public short Continue(object o)
		{

			return 0;
		}

		public bool exit()
		{
			tag_manual.tag_stepName = 100000;
			tag_isWork = 0;
			return true;
		}

		public short Init()
		{
			PointAggregate _Step0 = pointMotion.FindPoint(tag_stationName, "工位开始", 0);
			if (_Step0 == null)
			{
				return -1;
			}
			_Step0.tag_BeginFun = Step0;

			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "信号检测", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;

			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "Tray盘定位拍照", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_EndFun = Step2_End;

			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "循环开始1", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_BeginFun = Step3;

			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "上相机拍泡棉", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			_Step4.tag_EndFun = Step4_End;

			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "泡棉待取位", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5_Begin;
			_Step5.tag_AxisMoveFun = Step5_Move;

			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "Z下降", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_BeginFun = Step6;

			PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "取泡棉", 7);
			if (_Step7 == null)
			{
				return -1;
			}
			_Step7.tag_BeginFun = Step7;

			PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "Z上升", 8);
			if (_Step8 == null)
			{
				return -1;
			}
			_Step8.tag_BeginFun = Step8;

			PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "循环结束1", 9);
			if (_Step9 == null)
			{
				return -1;
			}
			_Step9.tag_BeginFun = Step9;

			PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "循环开始2", 10);
			if (_Step10 == null)
			{
				return -1;
			}
			_Step10.tag_BeginFun = Step10;

			PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "泡棉待校正位", 11);
			if (_Step11 == null)
			{
				return -1;
			}
			_Step11.tag_BeginFun = Step11;

			PointAggregate _Step12 = pointMotion.FindPoint(tag_stationName, "下相机拍泡棉", 12);
			if (_Step12 == null)
			{
				return -1;
			}
			_Step12.tag_BeginFun = Step12_Begin;

			PointAggregate _Step13 = pointMotion.FindPoint(tag_stationName, "飞拍位1#_S", 13);
			if (_Step13 == null)
			{
				return -1;
			}
			_Step13.tag_BeginFun = Step13;

			PointAggregate _Step14 = pointMotion.FindPoint(tag_stationName, "飞拍位2#_E", 14);
			if (_Step14 == null)
			{
				return -1;
			}
			_Step14.tag_BeginFun = Step14;

			PointAggregate _Step15 = pointMotion.FindPoint(tag_stationName, "循环结束2", 15);
			if (_Step15 == null)
			{
				return -1;
			}
			_Step15.tag_BeginFun = Step15;

			PointAggregate _Step16 = pointMotion.FindPoint(tag_stationName, "循环开始3", 16);
			if (_Step16 == null)
			{
				return -1;
			}
			_Step16.tag_BeginFun = Step16;

			PointAggregate _Step17 = pointMotion.FindPoint(tag_stationName, "Tray盘待拍照位", 17);
			if (_Step17 == null)
			{
				return -1;
			}
			_Step17.tag_BeginFun = Step17;

			PointAggregate _Step18 = pointMotion.FindPoint(tag_stationName, "上相机拍物料", 18);
			if (_Step18 == null)
			{
				return -1;
			}
			_Step18.tag_BeginFun = Step18;

			PointAggregate _Step19 = pointMotion.FindPoint(tag_stationName, "Tray盘待贴合位", 19);
			if (_Step19 == null)
			{
				return -1;
			}
			_Step19.tag_BeginFun = Step19_Begin;
			_Step19.tag_AxisMoveFun = Step19_Move;

			PointAggregate _Step20 = pointMotion.FindPoint(tag_stationName, "Z贴合下降", 20);
			if (_Step20 == null)
			{
				return -1;
			}
			_Step20.tag_BeginFun = Step20;

			PointAggregate _Step21 = pointMotion.FindPoint(tag_stationName, "贴合", 21);
			if (_Step21 == null)
			{
				return -1;
			}
			_Step21.tag_BeginFun = Step21;

			PointAggregate _Step22 = pointMotion.FindPoint(tag_stationName, "Z贴合上升", 22);
			if (_Step22 == null)
			{
				return -1;
			}
			_Step22.tag_BeginFun = Step22;

			PointAggregate _Step23 = pointMotion.FindPoint(tag_stationName, "循环结束3", 23);
			if (_Step23 == null)
			{
				return -1;
			}
			_Step23.tag_BeginFun = Step23;

			PointAggregate _Step24 = pointMotion.FindPoint(tag_stationName, "工位结束", 24);
			if (_Step24 == null)
			{
				return -1;
			}
			_Step24.tag_BeginFun = Step24;

			return 0;
		}

		public short Step0(object o)
		{
			return 0;
		}

		public short Step1(object o)
		{
			// 信号检测
			LineThread();
			return 0;
		}

		public short Step2_End(object o)
		{
			// Tray盘定位拍照
			//CameraTrigger(o, "&OBG,1,1");
			return 0;
		}

		public short Step3(object o)
		{
			PointAggregate point = (PointAggregate)o;
			loop1Index++;
			point.arrPoint[0].dblPonitValue = loop1Index;
			point.arrPoint[1].dblPonitValue = FoamCount;
			point.tag_isAxisStop = true;
			return 0;
		}

		public short Step4(object o)
		{
			PointAggregate point = (PointAggregate)o;

			point.arrPoint[0].dblPonitValue = FoamPoints[loop1Index].X;
			point.arrPoint[0].dblPonitValue = FoamPoints[loop1Index].Y;
			return 0;
		}

		public short Step4_End(object o)
		{
			string ret = "";

			if ((NozzleStaut & 1) == 0)
			{
				ret = CameraTrigger(o, "&OBG,1,2");
				if (ret != "&OBG,1,2")
				{
					log.Debug("相机拍照失败");
					return -1;
				}
			}
			else if ((NozzleStaut & 2) == 0)
			{
				ret = CameraTrigger(o, "&OBG,1,1");
				if (ret != "&OBG,1,1")
				{
					log.Debug("相机拍照失败");
					return -1;
				}
			}
			return 0;
		}

		public short Step5_Begin(object o)
		{
			PointAggregate point = (PointAggregate)o;
			point.arrPoint[0].dblPonitValue = NozzlePoints[loop1Index].X;
			point.arrPoint[1].dblPonitValue = NozzlePoints[loop1Index].Y;
			if ((NozzleStaut & 1) == 0)
			{
				point.arrPoint[4].blnPointEnable = false;
				point.arrPoint[5].dblPonitValue = NozzlePoints[loop1Index].R2;
			}
			else if ((NozzleStaut & 2) == 0)
			{
				point.arrPoint[4].dblPonitValue = NozzlePoints[loop1Index].R1;
				point.arrPoint[5].blnPointEnable = false;
			}
			return 0;
		}

		public short Step5_Move(object o)
		{
			string ret = "";
			ret = tag_UpCam.Read(2000);
			string[] strArr = ret.Split(',');
			camX = double.Parse(strArr[3]);
			camY = double.Parse(strArr[4]);
			camR = double.Parse(strArr[5]);
			PointAggregate point = (PointAggregate)o;
			point.arrPoint[0].dblPonitValue = camX;
			point.arrPoint[1].dblPonitValue = camY;
			NewCtrlCardV0.AxisAbsoluteMove(tag_stationName, "X", "泡棉待取位", 0);
			NewCtrlCardV0.AxisAbsoluteMove(tag_stationName, "Y", "泡棉待取位", 1);
			if ((NozzleStaut & 1) == 0)
			{
				point.arrPoint[4].blnPointEnable = false;
				point.arrPoint[5].dblPonitValue = camR;
				NewCtrlCardV0.AxisAbsoluteMove(tag_stationName, "R2", "泡棉待取位", 5);
			}
			else if ((NozzleStaut & 2) == 0)
			{
				point.arrPoint[4].dblPonitValue = camR;
				point.arrPoint[5].blnPointEnable = false;
				NewCtrlCardV0.AxisAbsoluteMove(tag_stationName, "R1", "泡棉待取位", 4);
			}
			return 0;
		}

		public short Step6(object o)
		{
			return 0;
		}

		public short Step7(object o)
		{
			if ((NozzleStaut & 1) == 0)
			{
				NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "右吸泡棉", 1);
				NozzleStaut = 1 << 1;
			}
			else if ((NozzleStaut & 2) == 0)
			{
				NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "左吸泡棉", 1);
				NozzleStaut += 1 << 2;
			}
			return 0;
		}

		public short Step8(object o)
		{
			return 0;
		}

		public short Step9(object o)
		{
			if (loop1Index > FoamCount)
			{
				loop1Index = 0;
				tag_manual.tag_stepName++;
			}
			else
			{
				tag_manual.tag_stepName = pointMotion.GetStepName(tag_stationName, "循环开始1") - 1;
			}
			return 0;
		}

		public short Step10(object o)
		{
			PointAggregate point = (PointAggregate)o;
			loop2Index++;
			point.arrPoint[0].dblPonitValue = loop2Index;
			point.arrPoint[1].dblPonitValue = NozzleCount;
			point.tag_isAxisStop = true;
			return 0;
		}

		public short Step11(object o)
		{

			return 0;
		}

		public short Step12_Begin(object o)
		{
			string ret = "";
			PointAggregate point = (PointAggregate)o;
			ret = CameraTrigger(o, "&OBG,2");
			int i = NewCtrlCardV0.MoveCameraCheckPointforPlus(tag_stationName, "X", point.strName, 2, true);
			if (i != 0)
			{
				Global.ShowMesgeInfon.MessageTopMost("1#、2#相机飞拍比较点位压入异常");
				return -1;
			}
				//if (ret != "&OBG,1,2")
				//{
				//	log.Debug("相机拍照失败");
				//	return -1;
				//}
				return 0;
		}

		public short Step13(object o)
		{
			return 0;
		}

		public short Step14(object o)
		{
			return 0;
		}

		public short Step15(object o)
		{
			if (loop2Index > NozzleCount)
			{
				loop2Index = 0;
				//tag_manual.tag_stepName++;
			}
			else
			{
				tag_manual.tag_stepName = pointMotion.GetStepName(tag_stationName, "循环开始2") - 1;
			}
			tag_manual.tag_stepName++;
			return 0;
		}

		public short Step16(object o)
		{
			PointAggregate point = (PointAggregate)o;
			loop3Index++;
			point.arrPoint[0].dblPonitValue = loop3Index;
			point.arrPoint[1].dblPonitValue = TrayCount;
			point.tag_isAxisStop = true;
			return 0;
		}

		public short Step17(object o)
		{
			PointAggregate point = (PointAggregate)o;
			point.arrPoint[0].dblPonitValue = TrayPoints[loop3Index].X;
			point.arrPoint[1].dblPonitValue = TrayPoints[loop3Index].Y;
			return 0;
		}

		public short Step18(object o)
		{
			string ret = "";
			if ((NozzleStaut & 1) == 0)
			{
				ret = CameraTrigger(o, "&TGG,1");
				if (ret != "&OBG,1,2")
				{
					log.Debug("相机拍照失败");
					return -1;
				}
			}
			else if ((NozzleStaut & 2) == 0)
			{
				ret = CameraTrigger(o, "&TGG,2");
				if (ret != "&OBG,1,1")
				{
					log.Debug("相机拍照失败");
					return -1;
				}
			}
			return 0;
		}
		
		public short Step19_Begin(object o)
		{
			PointAggregate point = (PointAggregate)o;
			point.arrPoint[0].dblPonitValue = TrayPoints[loop3Index].X;
			point.arrPoint[1].dblPonitValue = TrayPoints[loop3Index].Y;
			if ((NozzleStaut & 1) == 1)
			{
				point.arrPoint[4].blnPointEnable = false;
				point.arrPoint[5].dblPonitValue = TrayPoints[loop3Index].R2;
			}
			else if ((NozzleStaut & 2) == 2)
			{
				point.arrPoint[4].dblPonitValue = TrayPoints[loop3Index].R1;
				point.arrPoint[5].blnPointEnable = false;
			}
			return 0;
		}

		public short Step19_Move(object o)
		{
			string ret = "";
			ret = tag_UpCam.Read(2000);
			string[] strArr = ret.Split(',');
			camX = double.Parse(strArr[3]);
			camY = double.Parse(strArr[4]);
			camR = double.Parse(strArr[5]);
			PointAggregate point = (PointAggregate)o;
			point.arrPoint[0].dblPonitValue = camX;
			point.arrPoint[1].dblPonitValue = camY;
			NewCtrlCardV0.AxisAbsoluteMove(tag_stationName, "X", "Tray盘待贴合位", 0);
			NewCtrlCardV0.AxisAbsoluteMove(tag_stationName, "Y", "Tray盘待贴合位", 1);
			if ((NozzleStaut & 1) == 1)
			{
				point.arrPoint[4].blnPointEnable = false;
				point.arrPoint[5].dblPonitValue = camR;
				NewCtrlCardV0.AxisAbsoluteMove(tag_stationName, "R2", "Tray盘待贴合位", 5);
			}
			else if ((NozzleStaut & 2) == 2)
			{
				point.arrPoint[4].dblPonitValue = camR;
				point.arrPoint[5].blnPointEnable = false;
				NewCtrlCardV0.AxisAbsoluteMove(tag_stationName, "R1", "Tray盘待贴合位", 4);
			}
			return 0;
		}

		public short Step20(object o)
		{
			return 0;
		}

		public short Step21(object o)
		{
			if ((NozzleStaut & 1) == 1)
			{
				NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "右吸泡棉", 0);
			}
			else if ((NozzleStaut & 2) == 2)
			{
				NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "左吸泡棉", 0);
			}
			return 0;
		}

		public short Step22(object o)
		{
			return 0;
		}

		public short Step23(object o)
		{
			if (loop3Index > TrayCount)
			{
				loop3Index = 0;
				tag_manual.tag_stepName++;
			}
			else
			{
				tag_manual.tag_stepName = pointMotion.GetStepName(tag_stationName, "循环开始3") - 1;
			}
			return 0;
		}

		public short Step24(object o)
		{
			tag_manual.tag_stepName = -1;
			tag_isWork = 0;
			return 0;
		}

		/// <summary>
		/// 检测皮带线信号
		/// </summary>
		/// <returns></returns>
		private static int CheckLineSensor()
		{
			bool bTemp = false;
			int iResult = -1;
			if (NewCtrlCardV0.GetInputIoBitStatus("", "左段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取左段皮带进料状态失败!", 0);
				log.Warn("获取左段皮带进料状态失败");
				return -1;
			}
			if (!bTemp) { iResult = 1 << 7; }

			if (NewCtrlCardV0.GetInputIoBitStatus("", "左段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取左段皮带出料状态失败!", 0);
				log.Warn("获取左段皮带出料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 6; }

			if (NewCtrlCardV0.GetInputIoBitStatus("", "中段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带进料状态失败!", 0);
				log.Warn("获取中段皮带进料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 5; }

			if (NewCtrlCardV0.GetInputIoBitStatus("", "中段皮带减速", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带减速状态失败!", 0);
				log.Warn("获取中段皮带减速状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 4; }

			if (NewCtrlCardV0.GetInputIoBitStatus("", "中段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带出料状态失败!", 0);
				log.Warn("获取中段皮带出料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 3; }

			if (NewCtrlCardV0.GetInputIoBitStatus("", "右段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取右段皮带进料状态失败!", 0);
				log.Warn("获取右段皮带进料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 2; }

			if (NewCtrlCardV0.GetInputIoBitStatus("", "右段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取右段皮带出料状态失败!", 0);
				log.Warn("获取右段皮带出料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 1; }
			log.Debug("皮带线状态为:" + iResult);
			return iResult;
		}

		private void LineThread()
		{
			if (thread == null)
			{
				thread = new Thread(() =>
				{
					bool bTemp = false;
					while (true)
					{
						int lineSensor = CheckLineSensor();
					// 3  -->0000011
					// 16 -->0010000
					// 24 -->0011000
					// 28 -->0011100
					// 96 -->1100000
					// 127-->1111111
					if ((lineSensor & 3) == 3)
						{
						// 后段没料
						RunLine(tag_Assemblyline, 5, 200);
							RunLine(tag_Assemblyline, 6, 200);
						}
						else
						{
							RunLine(tag_Assemblyline, 5, 200);
							RunLine(tag_Assemblyline, 6, 200);
						}

						if ((lineSensor & 16) == 16)
						{
						// 判断是否顶升
						NewCtrlCardV0.GetInputIoBitStatus("", "顶升气缸下", out bTemp);
							if (!bTemp)
							{
							// 在上面 停中段
							ResetStation.StopLine(tag_Assemblyline, 3);
								ResetStation.StopLine(tag_Assemblyline, 4);
							}
							else
							{
								RunLine(tag_Assemblyline, 1, 200);
								RunLine(tag_Assemblyline, 2, 200);
								RunLine(tag_Assemblyline, 3, 200);
								RunLine(tag_Assemblyline, 4, 200);
							}
						}

						if ((lineSensor & 24) == 24)
						{

						}
						else
						{
							RunLine(tag_Assemblyline, 3, 100);
							RunLine(tag_Assemblyline, 4, 100);
							NewCtrlCardV0.SetOutputIoBitStatus("", "挡料气缸", 0);
						}

						if ((lineSensor & 28) == 28)
						{

						}
						else if (TrayIndex != 60)
						{
							NewCtrlCardV0.SetOutputIoBitStatus("", "挡料气缸", 1);
							NewCtrlCardV0.SetOutputIoBitStatus("", "顶升气缸", 0);
						}
						else
						{
							NewCtrlCardV0.SetOutputIoBitStatus("", "挡料气缸", 1);
							NewCtrlCardV0.SetOutputIoBitStatus("", "顶升气缸", 1);
							RunLine(tag_Assemblyline, 3, 200);
							RunLine(tag_Assemblyline, 4, 200);
						}

						if ((lineSensor & 96) == 96)
						{
						// 前段没料
						RunLine(tag_Assemblyline, 1, 200);
							RunLine(tag_Assemblyline, 2, 200);
						}
						else
						{
							ResetStation.StopLine(tag_Assemblyline, 1);
							ResetStation.StopLine(tag_Assemblyline, 2);
						}
					}
				});
				thread.IsBackground = true;
				thread.Start();
			}
		}

		public static bool RunLine(JSerialPort jSerialPort, int index, int speed)
		{
			string lineRunCmd = "10 00 23 00 01 02";
			string strHex = "";
			byte[] bCmd;
			byte[] bResult;
			if (index % 2 == 0)
			{
				speed = 0 - speed;
			}
			ResetStation.StopLine(jSerialPort, index);
			strHex = Convert.ToString(speed, 16);
			if (strHex.Length == 2)
			{
				strHex = "00 " + strHex;
				lineRunCmd += strHex;
			}
			else
				lineRunCmd += strHex.Substring(strHex.Length - 4);
			bCmd = JSerialPort.CreateLineCode(lineRunCmd, index);
			bResult = jSerialPort.sendCommand(bCmd, bCmd.Length, 20000);
			for (int i = 0; i < 6; i++)
			{
				if (bCmd[i] != bResult[i])
				{
					log.Warn("设置" + index + "流水线速度失败,错误代码:" + bResult[2].ToString());
					return false;
				}
			}

			lineRunCmd = "06 00 27 00 02";
			ResetStation.StopLine(jSerialPort, index);
			bCmd = JSerialPort.CreateLineCode(lineRunCmd, index);
			bResult = jSerialPort.sendCommand(bCmd, bCmd.Length, 20000);
			for (int i = 0; i < 6; i++)
			{
				if (bCmd[i] != bResult[i])
				{
					log.Warn("启动" + index + "流水线失败,错误代码:" + bResult[2].ToString());
					return false;
				}
			}
			return true;
		}

		public void SetMsg(string Msg, int ErrorType, bool showTiShiForm = false)
		{
			if (ErrorType == 0)//代表是报警信息
				ErrMsg = Msg;
			UserControl_LogOut.OutLog(Msg, ErrorType);
			if (showTiShiForm)//需要弹窗
			{
				Global.WorkVar.tag_MessageoxStr = null;
				Thread.Sleep(500);
				Global.WorkVar.tag_MessageoxStr = Msg;
			}

		}

		public string CameraTrigger(object o, string cmd)
		{
			PointAggregate pp = (PointAggregate)o;
			int i = 0;
			string strend = cmd + "\r\n", ret = "";
			{
				while (true)
				{
					if (IsExit())
					{
						return "";
					}
					ret = tag_UpCam.Send(strend, 1000);
					string[] strArr = ret.Split(',');
					if (strArr != null && strArr.Length > 1)
					{
						if (double.Parse(strArr[1]) == 0 || strArr[2].IndexOf("0") >= 0)
						{
							DialogResult dig = System.Windows.Forms.MessageBox.Show("相机抓边异常", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
							if (dig == DialogResult.Yes)
								continue;
							else
								return ret;
						}
						return ret;
					}
					else
					{
						SetMsg("相机返回数据异常", 3);
						i++;
						if (i > 2)
						{
							break;
						}
						continue;
					}
				}
				return "";
			}
		}
	}
}
