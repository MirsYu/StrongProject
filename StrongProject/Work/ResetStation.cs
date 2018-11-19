using log4net;
using System.Threading;
using System.Windows.Forms;

namespace StrongProject
{
	public class ResetStation : workBase
	{
		private static readonly ILog log = LogManager.GetLogger("ResetStation.cs");

		public Work tag_Work;
		public JSerialPort tag_Assemblyline;

		public ResetStation(Work _Work)
		{
			tag_Work = _Work;
			tag_stationName = "总复位";
			tag_Assemblyline = _Work.tag_JSerialPort[0];
			tag_isRestStation = 1;
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
			short ret = 0;
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
			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "有料检测", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;
			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "皮带,供料器停止", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;
			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "Z,R轴回原", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_BeginFun = Step3;
			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "关真空,气缸原位", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "X,Y轴回原", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5;
			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "工位结束", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_BeginFun = Step6;

			#region 检测串口存在
			if (tag_Assemblyline.tag_PortParameter.tag_name != "皮带线")
			{
				return -1;
			}
			#endregion
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
			tag_isWork = 1;
			return 0;
		}

		public short Step1(object o)
		{
			// 有料检测
			// 1.检测吸头上是否有料
			// 2.检测皮带线上是否有料
			if (!CheckLineProduce())
			{
				if (MessageBoxLog.Show("皮带线上有料,请取走", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				{
					// 返回上一步
					--tag_manual.tag_stepName;
					return 0;
				}
				else
					return -1;
			}
			return 0;
		}

		public short Step2(object o)
		{
			// 皮带,供料器停止
			// 1.皮带线(Assembly line)停止
			for (int i = 0; i < 6; i++)
			{
				if (!StopLine(tag_Assemblyline,i + 1))
				{
					if (MessageBoxLog.Show("流水线" + i + "停止失败", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
					{
						// 返回上一步
						--tag_manual.tag_stepName;
						return 0;
					}
					else
						return -1;
				}
			}
			// 2.供料器停止
			#region 供料器脉冲停止并且方向初始化
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "放料脉冲", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "放料方向", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "剥料脉冲", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "剥料方向", 0);
			#endregion
			return 0;
		}

		public short Step3(object o)
		{
			// Z,R轴回原
			return 0;
		}

		public short Step4(object o)
		{
			// 关真空,气缸原位
			// 1.关真空
			#region 关真空
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "剥料前真空", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "剥料后真空", 0);

			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "吹废料", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "收废料", 0);

			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "左吸泡棉", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "左吸废料", 0);

			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "右吸泡棉", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "右吸废料", 0);
			#endregion
			// 2.气缸回原位,并检测是否真的回到位
			#region 气缸回原
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "挡料气缸", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "顶升气缸", 0);

			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "左抛泡棉气缸", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "右抛泡棉气缸", 0);
			#endregion
			return 0;
		}

		public short Step5(object o)
		{
			// X,Y轴回原
			return 0;
		}

		public short Step6(object o)
		{
			// 工位结束
			return 0;
		}

		/// <summary>
		/// 检查流水线上是否还有产品
		/// </summary>
		/// <returns></returns>
		private bool CheckLineProduce()
		{
			bool bTemp = false;
			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "左段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取左段皮带进料状态失败!", 0);
				log.Warn("获取左段皮带进料状态失败");
				return false;
			}
			if (bTemp)	return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "左段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取左段皮带出料状态失败!", 0);
				log.Warn("获取左段皮带出料状态失败");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "中段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带进料状态失败!", 0);
				log.Warn("获取中段皮带进料状态失败");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "中段皮带减速", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带减速状态失败!", 0);
				log.Warn("获取中段皮带减速状态失败");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "中段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取中段皮带出料状态失败!", 0);
				log.Warn("获取中段皮带出料状态失败");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "右段皮带进料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取右段皮带进料状态失败!", 0);
				log.Warn("获取右段皮带进料状态失败");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "右段皮带出料", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("获取右段皮带出料状态失败!", 0);
				log.Warn("获取右段皮带出料状态失败");
				return false;
			}
			if (bTemp) return false;

			return true;
		}

		/// <summary>
		/// 停止单个流水线
		/// </summary>
		/// <param name="index">流水线索引</param>
		/// <returns></returns>
		public static bool StopLine(JSerialPort jSerialPort, int index)
		{
			string lineStopCmd = "06 00 28 00 00";
			byte[] bCmd;
			byte[] bResult;
			bCmd = JSerialPort.CreateLineCode(lineStopCmd, index);
			bResult = jSerialPort.sendCommand(bCmd, bCmd.Length, 20000);
			for (int i = 0; i < bCmd.Length; i++)
			{
				if (bCmd[i] != bResult[i])
				{
					log.Warn("停止" + index + "流水线失败,错误代码:" + bResult[2].ToString());
					return false;
				}
			}
			return true;
		}
	}
}
