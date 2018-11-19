using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StrongProject
{
	public class EmptyRun : workBase
	{
		public Work tag_Work;
		public JSerialPort tag_Assemblyline;

		private static readonly ILog log = LogManager.GetLogger("EmptyRun.cs");

		public EmptyRun(Work _Work)
		{
			tag_Work = _Work;
			tag_stationName = "空跑";
			tag_Assemblyline = _Work.tag_JSerialPort[0];
			tag_isRestStation = 2;
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

		public short Init()
		{
			PointAggregate _Step0 = pointMotion.FindPoint(tag_stationName, "工位开始", 0);
			if (_Step0 == null)
			{
				return -1;
			}
			_Step0.tag_BeginFun = Step0;

			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "拍泡棉位,Tray流入", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1_Begin;
			_Step1.tag_AxisMoveFun = Step1_Move;

			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "吸嘴位置,泡棉拍照", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2_Begin;
			_Step2.tag_AxisMoveFun = Step2_Move;

			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "Z轴下降", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_EndFun = Step3_End;

			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "Z轴上升", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_EndFun = Step4_End;

			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "下相机拍泡棉校正", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5_Begin;
			_Step5.tag_EndFun = Step5_End;

			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "预校正位", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_AxisMoveFun = Step6_Move;

			PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "Tray穴位", 7);
			if (_Step7 == null)
			{
				return -1;
			}
			_Step7.tag_BeginFun = Step7_Begin;
			_Step7.tag_EndFun = Step7_End;

			PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "Tray预贴合位", 8);
			if (_Step8 == null)
			{
				return -1;
			}
			_Step8.tag_AxisMoveFun = Step8_Move;

			PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "Z轴下贴合", 9);
			if (_Step9 == null)
			{
				return -1;
			}
			_Step9.tag_EndFun = Step9_End;

			PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "Z轴上贴合", 10);
			if (_Step10 == null)
			{
				return -1;
			}
			_Step10.tag_EndFun = Step10_End;

			PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "工位结束", 11);
			if (_Step11 == null)
			{
				return -1;
			}
			_Step11.tag_BeginFun = Step11_Begin;

			return 0;
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

		public short Step0(object o)
		{
			return 0;
		}

		public short Step1_Begin(object o)
		{
			// 拍泡棉位,Tray流入 (运动前)
			// 根据不同泡棉设置成不同的拍照位
			PointAggregate p1 = (PointAggregate)o;
			log.Debug("#1泡棉拍照起始位(X:" + p1.arrPoint[0].dblPonitValue +
									 ",Y:" + p1.arrPoint[1].dblPonitValue + ")");
			return 0;
		}


		public short Step1_Move(object o)
		{
			// 拍泡棉位,Tray流入 (运动中)
			// (线程处理并行)
			// 1.检测流水线状态，处理不同状态机制
			// (空跑)流水线测试 100反转 100正转 200反转 200正转
			new Thread(() =>
			{
				for (int i = 0; i < 6; i++)
				{
					RunLine(tag_Assemblyline, i + 1, -100);
				}
				for (int i = 0; i < 6; i++)
				{
					RunLine(tag_Assemblyline, i + 1, 100);
				}
				for (int i = 0; i < 6; i++)
				{
					RunLine(tag_Assemblyline, i + 1, -200);
				}
				for (int i = 0; i < 6; i++)
				{
					RunLine(tag_Assemblyline, i + 1, 200);
				}
			});
			return 0;
		}

		public short Step2_Begin(object o)
		{
			// 泡棉拍照 (运动前)
			// 1.触发拍照,等待拍照完成
			Thread.Sleep(10);
			return 0;
		}

		public short Step2_Move(object o)
		{
			// 泡棉拍照 (运动中)
			// 运动到一个大概位置
			// 1.开启线程获取相机返回值
			// 2.一旦获取到位置重新设定位置坐标X,Y,D
			// 3.NG 拍下一个
			PointAggregate p1 = (PointAggregate)o;
			double camX = -1, camY = -1, camR = -1;

			new Thread(() =>
			{
				Thread.Sleep(50);
				camX = 0.5;
				camY = 0.5;
				camR = 0.5;
				log.Debug("相机发送模拟值(X:" + camX + ",Y:" + camY + ",R:" + camR + ")");
			}).Start();

			new Thread(() =>
			{
				int waitTime = 10;
				while (waitTime > 0)
				{
					if (camX != -1 && camY != -1 && camR != -1)
					{
						p1.arrPoint[0].dblPonitValue += camX;
						p1.arrPoint[1].dblPonitValue += camY;
						p1.arrPoint[4].dblPonitValue += camR;
						break;
					}
					else
					{
						Thread.Sleep(1);
						waitTime--;
					}
				}
			}).Start();

			log.Debug("#1泡棉拍照调整位(X:" + p1.arrPoint[0].dblPonitValue +
										",Y:" + p1.arrPoint[1].dblPonitValue +
										",R:" + p1.arrPoint[4].dblPonitValue +
									")");
			return 0;
		}

		public short Step3_End(object o)
		{
			// Z轴下降(运动完成)
			// 吸取物料
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "左吸泡棉", 1);
			Thread.Sleep(500);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "左吸泡棉", 0);
			return 0;
		}

		public short Step4_End(object o)
		{
			// Z轴上升(运动完成)
			// 判断是否取到物料
			bool bTemp = false;
			NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "左吸料真空", out bTemp);
			log.Debug("检测真空:" + bTemp);
			// NG 跳过
			return 0;
		}

		public short Step5_Begin(object o)
		{
			// 下相机拍泡棉校正
			// 1.获取下相机拍照位
			PointAggregate p1 = (PointAggregate)o;
			p1.arrPoint[0].dblPonitValue += 1;
			p1.arrPoint[1].dblPonitValue += 1;
			return 0;
		}

		public short Step5_End(object o)
		{
			// 下相机拍泡棉校正
			// 1.触发拍照
			return 0;
		}

		public short Step6_Move(object o)
		{
			// 预校正位
			// 1.获取相机结果
			// 2.更新位置
			PointAggregate p1 = (PointAggregate)o;
			double camX = -1, camY = -1, camR = -1;

			new Thread(() =>
			{
				Thread.Sleep(50);
				camX = 0.5;
				camY = 0.5;
				camR = 0.5;
				log.Debug("相机发送模拟值(X:" + camX + ",Y:" + camY + ",R:" + camR + ")");
			}).Start();

			new Thread(() =>
			{
				int waitTime = 10;
				while (waitTime > 0)
				{
					if (camX != -1 && camY != -1 && camR != -1)
					{
						p1.arrPoint[0].dblPonitValue += camX;
						p1.arrPoint[1].dblPonitValue += camY;
						p1.arrPoint[4].dblPonitValue += camR;

						break;
					}
					else
					{
						Thread.Sleep(1);
						waitTime--;
					}
				}
			}).Start();
			log.Debug("#1泡棉校正(X:" + p1.arrPoint[0].dblPonitValue +
								",Y:" + p1.arrPoint[1].dblPonitValue +
								",R:" + p1.arrPoint[4].dblPonitValue +
								")");
			return 0;
		}

		public short Step7_Begin(object o)
		{
			// Tray穴位
			// 1.获取Tary穴位位置并设置
			PointAggregate p1 = (PointAggregate)o;
			p1.arrPoint[0].dblPonitValue += 1;
			p1.arrPoint[1].dblPonitValue += 1;
			return 0;
		}

		public short Step7_End(object o)
		{
			// Tray穴位
			// 1.触发拍照

			return 0;
		}

		public short Step8_Move(object o)
		{
			// Tray预贴合位
			// 1.获取相机结果
			// 2.更新位置
			PointAggregate p1 = (PointAggregate)o;
			double camX = -1, camY = -1, camR = -1;

			new Thread(() =>
			{
				Thread.Sleep(50);
				camX = 0.5;
				camY = 0.5;
				camR = 0.5;
				log.Debug("相机发送模拟值(X:" + camX + ",Y:" + camY + ",R:" + camR + ")");
			}).Start();

			new Thread(() =>
			{
				int waitTime = 10;
				while (waitTime > 0)
				{
					if (camX != -1 && camY != -1 && camR != -1)
					{
						p1.arrPoint[0].dblPonitValue += camX;
						p1.arrPoint[1].dblPonitValue += camY;
						p1.arrPoint[4].dblPonitValue += camR;

						break;
					}
					else
					{
						Thread.Sleep(1);
						waitTime--;
					}
				}
			}).Start();

			log.Debug("#1Tray校正(X:" + p1.arrPoint[0].dblPonitValue +
								",Y:" + p1.arrPoint[1].dblPonitValue +
								",R:" + p1.arrPoint[4].dblPonitValue +
								")");
			return 0;
		}

		public short Step9_End(object o)
		{
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "左吸泡棉", 0);
			Thread.Sleep(500);
			return 0;
		}

		public short Step10_End(object o)
		{
			// 检测吸嘴上是否还有泡棉
			return 0;
		}

		public short Step11_Begin(object o)
		{
			tag_manual.tag_stepName = 0;
			return 0;
		}

		/// <summary>
		/// 检测皮带线信号
		/// </summary>
		/// <returns></returns>
		private int CheckLineSensor()
		{
			bool bTemp = false;
			int iResult = -1;
			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "左段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取左段皮带进料状态失败!", 0);
				log.Warn("获取左段皮带进料状态失败");
				return -1;
			}
			if (!bTemp) { iResult = 1 << 7; }

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "左段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取左段皮带出料状态失败!", 0);
				log.Warn("获取左段皮带出料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 6; }

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "中段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带进料状态失败!", 0);
				log.Warn("获取中段皮带进料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 5; }

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "中段皮带减速", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带减速状态失败!", 0);
				log.Warn("获取中段皮带减速状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 4; }

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "中段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带出料状态失败!", 0);
				log.Warn("获取中段皮带出料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 3; }

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "右段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取右段皮带进料状态失败!", 0);
				log.Warn("获取右段皮带进料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 2; }

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "右段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取右段皮带出料状态失败!", 0);
				log.Warn("获取右段皮带出料状态失败");
				return -1;
			}
			if (!bTemp) { iResult += 1 << 1; }
			return iResult;
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
	}
}
