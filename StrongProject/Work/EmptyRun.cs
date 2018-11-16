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

		private static readonly ILog log = LogManager.GetLogger("EmptyRun.cs");

		public EmptyRun(Work _Work)
		{
			tag_Work = _Work;
			tag_stationName = "空跑";
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

			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "泡棉拍照", 2);
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
			return 0;
		}

		public short Step1_Move(object o)
		{
			// 拍泡棉位,Tray流入 (运动中)
			// (线程处理并行)
			// 1.检测流水线状态，处理不同状态机制
			// (空跑)流水线测试 100反转 100正转 200反转 200正转
			return 0;
		}

		public short Step2_Begin(object o)
		{
			// 泡棉拍照 (运动前)
			// 1.触发拍照,等待拍照完成
			return 0;
		}

		public short Step2_Move(object o)
		{
			// 泡棉拍照 (运动中)
			// 运动到一个大概位置
			// 1.开启线程获取相机返回值
			// 2.一旦获取到位置重新设定位置坐标X,Y,D
			// 3.NG 拍下一个
			return 0;
		}

		public short Step3_End(object o)
		{
			// Z轴下降(运动完成)
			// 吸取物料
			return 0;
		}

		public short Step4_End(object o)
		{
			// Z轴上升(运动完成)
			// 判断是否取到物料
			// NG 跳过
			return 0;
		}

		public short Step5(object o)
		{
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
			if (!bTemp) { iResult  = 1 << 7; }

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
	}
}
