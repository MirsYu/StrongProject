using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
namespace StrongProject
{
	public class pointMotion
	{
		/// <summary>
		/// 获取某步骤的步数， StationName工位名 tepame步骤名
		/// </summary>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static short GetStepName(string StationName, string tepame)
		{

			StationModule station = StationManage.FindStation(StationName);
			if (station == null)
			{
				return 1000;
			}

			for (short i = 0; i < station.arrPoint.Count; i++)
			{
				PointAggregate pointA = station.arrPoint[i];
				if (Global.WorkVar.tag_StopState == 1)
				{
					return 1000;
				}
				if (pointA.strName == tepame)
					return i;

			}
			return 1000;
		}

		/// 移动到StationName工位上pointName点位 ： StationName工位名称，pointName点位名称
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="pointName"></param>
		/// <returns></returns>
		public static PointAggregate FindPoint(string StationName, string pointName)
		{
			StationModule tag_at = StationManage.FindStation(StationName);
			if (tag_at == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show("工位：\r\n\t" + StationName + "\r\n没有找到，请添加工位", "错误") == DialogResult.OK)
				{

				}
				return null;
			}

			PointAggregate pointA1 = StationManage.FindPoint(tag_at, pointName);
			if (pointA1 == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show("工位：\r\n\t" + StationName + "\r\n步骤" + pointName + "\r\n没有找到，请添加步骤", "错误") == DialogResult.OK)
				{

				}
			}
			return pointA1;
		}

		/// 移动到StationName工位上pointName点位 ： StationName工位名称，pointName点位名称  step 第几步
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="pointName"></param>
		/// <returns></returns>
		public static PointAggregate FindPoint(string StationName, string pointName, short step)
		{
			//return FindPoint(StationName, pointName);
			StationModule tag_at = StationManage.FindStation(StationName);
			if (tag_at == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show("工位：\r\n\t" + StationName + "\r\n没有找到，请添加工位", "错误") == DialogResult.OK)
				{

				}
				return null;
			}


			foreach (PointAggregate point1 in tag_at.arrPoint)
			{

				if (point1.strName == pointName)
				{
					return point1;
				}
				if (point1.tag_BeginPointAggregateList != null)
					foreach (PointAggregate point1_b in point1.tag_BeginPointAggregateList)
					{
						if (point1_b.strName == pointName)
						{
							return point1_b;
						}
					}
				if (point1.tag_EndPointAggregateList != null)
					foreach (PointAggregate point1_b in point1.tag_EndPointAggregateList)
					{
						if (point1_b.strName == pointName)
						{
							return point1_b;
						}
					}
			}
			Global.WorkVar.tag_StopState = 2;
			if (MessageBoxLog.Show("工位：" + StationName + "\r\n第" + step + "步：" + pointName + "\r\n没有找到，请添加工位", "错误") == DialogResult.OK)
			{

			}
			return null;

		}
		/// <summary>
		///  等待判断 IO感应器  是否==var   如果等于 返回为0 否则非0，station工位名称，OutIoName 输出点位名称，var 高低电平，inPutIoName 输入点位名称，var 判断是否达到改值,
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="InductionIoName"></param>
		/// <returns></returns>
		public static short WaitIoTrigger(string StationName, string InductionIoName, bool var, long outtime)
		{
			bool io_status = false;
			int i = 0;
			long runTime = 1;
			if (StationName == null || InductionIoName == null)
			{
				return 0;
			}
			while (true)
			{
				if (Global.WorkVar.tag_StopState == 1)
				{
					return -3;
				}

				if (Global.WorkVar.tag_SuspendState == 1)
				{
					Thread.Sleep(10);
					continue;
				}
				if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
				{
					Thread.Sleep(10);
					continue;
				}
				if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
				{
					Thread.Sleep(10);
					continue;
				}
				if (Global.WorkVar.tag_IsExit == 1)
				{
					Thread.Sleep(10);
					return 0;
				}

				if (NewCtrlCardV0.GetInputIoBitStatus(StationName, InductionIoName, out io_status) == 0)
				{
					if (io_status == var)
					{
						//感应有料
						return 0;
					}
				}
				else
				{
					Global.WorkVar.tag_StopState = 2;
					if (MessageBoxLog.Show("运行过程中：\r\n\t" + InductionIoName + "(IO口,在" + StationName + "中)\t执行错误.\r\n 解决方法：\r\n\t1:请检查伺服驱动\r\n\t2:重新检测点位配置！", "错误") == DialogResult.OK)
					{

					}
					return -4;
				}
				i++;
				if (runTime > outtime)
				{

					if (outtime == 0)
					{
						return 1;
					}
					Global.WorkVar.tag_SuspendState = 1;
					DialogResult showRet = MessageBoxLog.Show("运行过程中\r\n" + InductionIoName + "(IO口,在" + StationName + "中)" + ":超时，是否继续执行,\r\n点忽略继续，\r\n点终止急停处理，并需要复位", "警告", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question);
					if (showRet == DialogResult.Retry)
					{
						Global.WorkVar.tag_SuspendState = 0;
						runTime = 0;
						continue;

					}
					else
						if (showRet == DialogResult.Abort)
					{
						Global.WorkVar.tag_SuspendState = 0;
						return -5;
					}
					else
					{
						Global.WorkVar.tag_SuspendState = 0;
						return 0;
					}

				}
				Thread.Sleep(10);
				runTime = runTime + 10;
			}
			/*  if (outtime == 0)
              {
                  return 1;
              }
         
              if (MessageBoxLog.Show("模块:<" + StationName + "><" + InductionIoName + ">超时，是否继续执行,点OK继续，点取消急停处理，并需要复位", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
              {
                  Global.WorkVar.tag_SuspendState = 0;
                  return 0;
              }
              else
              {
                
                  return -5;
              }*/


		}
		/// <summary>
		///  等待判断 IO感应器  是否==var   如果等于 返回为0 否则非0，station工位名称，OutIoName 输出点位名称，var 高低电平，inPutIoName 输入点位名称，var 判断是否达到改值,
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="InductionIoName"></param>
		/// <returns></returns>
		public static short WaitIoMutIoTrigger(PointAggregate pa, manual man)
		{
			bool io_status = false;
			int i = 0;
			long runTime = 1;
			while (true)
			{
				if (Global.WorkVar.tag_StopState == 1)
				{
					return -3;
				}
				if (Global.WorkVar.tag_SuspendState == 1)
				{
					Thread.Sleep(10);
					continue;
				}
				if ((string)pa.strStationName == "左工位" && Global.WorkVar.bSuspendState_L)
				{
					Thread.Sleep(10);
					continue;
				}
				if ((string)pa.strStationName == "右工位" && Global.WorkVar.bSuspendState_R)
				{
					Thread.Sleep(10);
					continue;
				}
				if (Global.WorkVar.tag_IsExit == 1)
				{
					Thread.Sleep(10);
					return 0;
				}
				long outtime = 0;
				int isOk = 0;
				OutIOParameterPoint ioMax = null;
				foreach (OutIOParameterPoint io in pa.tag_AxisSafeManage.tag_InIoList)
				{
					if (io.tag_IniO1.tag_IOParameterOutTime > outtime)
					{
						outtime = io.tag_IniO1.tag_IOParameterOutTime;
						ioMax = io;
					}
				}
				isOk = 0;
				string iostr = null;
				foreach (OutIOParameterPoint iop in pa.tag_AxisSafeManage.tag_InIoList)
				{
					if (iop.tag_IniO1 == null || iop.tag_IniO1.tag_IOName == null || NewCtrlCardV0.GetInputIoBitStatus(iop.tag_IniO1.tag_name, iop.tag_IniO1.tag_IOName, out io_status) == 0)
					{
						if (iop.tag_IniO1 == null || iop.tag_IniO1.tag_IOName == null || io_status == iop.tag_IniO1.tag_var)
						{
							//感应有料

						}
						else
						{
							isOk = 1;
						}
					}
					else
					{
						Global.WorkVar.tag_StopState = 2;
						if (MessageBoxLog.Show("第" + man.tag_stepName + "步：" + pa.strName + "\r\n" + "运行过程中：\r\n\t" + iop.tag_IniO1.tag_IOName + "(IO口)\t执行错误.\r\n 解决方法：\r\n\t1:请检查伺服驱动\r\n\t2:重新检测点位配置！", "错误") == DialogResult.OK)
						{

						}
						return -4;
					}
					iostr = iostr + iop.tag_IniO1.tag_IOName + "\r\n";
				}
				if (isOk == 0)
					return 0;
				i++;
				if (runTime > outtime)
				{


					Global.WorkVar.tag_SuspendState = 1;
					if (man != null && man.tag_SuspendFun != null)
					{
						man.tag_SuspendFun(null);
					}
					DialogResult showRet = MessageBoxLog.Show("第" + man.tag_stepName + "步：" + pa.strName + "\r\n" + "运行过程中：\r\n\t" + iostr + "(IO口)\t超时\r\n是否继续执行,点忽略继续，点终止急停处理，并需要复位", "警告", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question);
					if (showRet == DialogResult.Retry)
					{
						if (man != null && man.tag_ContinueFun != null)
						{
							man.tag_ContinueFun(null);
						}
						Global.WorkVar.tag_SuspendState = 0;

						runTime = 0;
						continue;

					}
					else
						if (showRet == DialogResult.Abort)
					{
						Global.WorkVar.tag_SuspendState = 0;
						return -5;
					}
					else
					{
						if (man != null && man.tag_ContinueFun != null)
						{
							man.tag_ContinueFun(null);
						}
						Global.WorkVar.tag_SuspendState = 0;
						return 0;
					}

				}
				Thread.Sleep(10);
				runTime = runTime + 10;
			}
		}

		/// <summary>
		///  等待判断 IO感应器  是否==var   如果等于 返回为0 否则非0，station工位名称，OutIoName 输出点位名称，var 高低电平，inPutIoName 输入点位名称，var 判断是否达到改值,
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="InductionIoName"></param>
		/// <returns></returns>
		public static short WaitIoTrigger(string StationName, string InductionIoName, bool var, long outtime, string PointName, manual man)
		{
			bool io_status = false;
			int i = 0;
			long runTime = 1;
			while (true)
			{
				if (Global.WorkVar.tag_StopState == 1)
				{
					return -3;
				}
				if (Global.WorkVar.tag_SuspendState == 1)
				{
					Thread.Sleep(10);
					continue;
				}
				if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
				{
					Thread.Sleep(10);
					continue;
				}
				if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
				{
					Thread.Sleep(10);
					continue;
				}
				if (Global.WorkVar.tag_IsExit == 1)
				{
					Thread.Sleep(10);
					return 0;
				}
				if (NewCtrlCardV0.GetInputIoBitStatus(StationName, InductionIoName, out io_status) == 0)
				{
					if (io_status == var)
					{
						//感应有料
						return 0;
					}
				}
				else
				{
					Global.WorkVar.tag_StopState = 2;
					if (MessageBoxLog.Show(StationName + "\r\n第" + man.tag_stepName + "步：" + PointName + "\r\n" + "运行过程中：\r\n\t" + InductionIoName + "(IO口)\t执行错误.\r\n 解决方法：\r\n\t1:请检查伺服驱动\r\n\t2:重新检测点位配置！", "错误") == DialogResult.OK)
					{

					}
					return -4;
				}
				i++;
				if (runTime > outtime)
				{

					if (outtime == 0)
					{
						return 1;
					}
					Global.WorkVar.tag_SuspendState = 1;
					if (man != null && man.tag_SuspendFun != null)
					{
						man.tag_SuspendFun(null);
					}
					DialogResult showRet = MessageBoxLog.Show(StationName + "\r\n第" + man.tag_stepName + "步：" + PointName + "\r\n" + "运行过程中：\r\n\t" + InductionIoName + "(IO口)\t超时\r\n是否继续执行,\r\n1.点忽略继续，\r\n2.点取终止停处理，并需要复位\t", "警告", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question);
					if (showRet == DialogResult.Retry)
					{
						if (man != null && man.tag_ContinueFun != null)
						{
							man.tag_ContinueFun(null);
						}
						Global.WorkVar.tag_SuspendState = 0;

						runTime = 0;
						continue;

					}
					else
						if (showRet == DialogResult.Abort)
					{
						Global.WorkVar.tag_SuspendState = 0;
						return -5;
					}
					else
					{
						if (man != null && man.tag_ContinueFun != null)
						{
							man.tag_ContinueFun(null);
						}
						Global.WorkVar.tag_SuspendState = 0;
						return 0;
					}

				}
				Thread.Sleep(10);
				runTime = runTime + 10;
			}

			/*  if (outtime == 0)
              {
                  return 1;
              }
         
              if (MessageBoxLog.Show("模块:<" + StationName + "><" + InductionIoName + ">超时，是否继续执行,点OK继续，点取消急停处理，并需要复位", "警告", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
              {
                  Global.WorkVar.tag_SuspendState = 0;
                  return 0;
              }
              else
              {
                
                  return -5;
              }*/


		}
		/// <summary>
		///  感应器是否触发，station工位名称，OutIoName 输出点位名称，var 高低电平，inPutIoName 输入点位名称，varI 判断是否到达汽缸位置,
		/// _manual 步骤名称
		/// </summary>
		/// <param name="station"></param>
		/// <param name="OutIoName"></param>
		/// <param name="var"></param>
		/// <param name="inPutIoName"></param>
		/// <param name="varI"></param>
		/// <param name="_manual"></param>
		/// <returns></returns>
		public static short IoTrigger(string station, string OutIoName, bool var)
		{
			short ret = WaitIoTrigger(station, OutIoName, var, 0);
			if (ret == 0)
			{
				return 0;
			}
			if (Global.WorkVar.tag_IsExit == 1)
			{
				Thread.Sleep(10);
				return 0;
			}
			if (ret == 1)
			{
				Global.WorkVar.tag_SuspendState = 1;
				DialogResult showRet = MessageBoxLog.Show(station + "\r\n运行过程中：\r\n\t" + OutIoName + "(IO口,在" + station + "中)\t没有触发，影响安全，是否继续执行,\r\n点\"中止\"急停处理，并需要复位", "警告", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question);

				if (showRet == DialogResult.Ignore)
				{
					Global.WorkVar.tag_SuspendState = 0;
					return 0;
				}
				else
					if (showRet == DialogResult.Abort)
				{
					Global.WorkVar.tag_SuspendState = 0;
					return -5;
				}
				else
				{
					Global.WorkVar.tag_SuspendState = 0;
					return IoTrigger(station, OutIoName, var);
				}


			}
			else
			{
				return ret;
			}
		}
		/// <summary>
		///  感应器是否触发，station工位名称，OutIoName 输出点位名称，var 高低电平，inPutIoName 输入点位名称，varI 判断是否到达汽缸位置,
		/// _manual 步骤名称
		/// </summary>
		/// <param name="station"></param>
		/// <param name="OutIoName"></param>
		/// <param name="var"></param>
		/// <param name="inPutIoName"></param>
		/// <param name="varI"></param>
		/// <param name="_manual"></param>
		/// <returns></returns>
		public static short IoTrigger(string station, string OutIoName, bool var, string stepName, manual man)
		{

			short ret = WaitIoTrigger(station, OutIoName, var, 0, stepName, man);
			if (ret == 0)
			{
				return 0;
			}
			if (Global.WorkVar.tag_IsExit == 1)
			{
				Thread.Sleep(10);
				return 0;
			}
			if (ret == 1)
			{
				Global.WorkVar.tag_SuspendState = 1;
				DialogResult showRet = MessageBoxLog.Show(station + "\r\n第" + man.tag_stepName + "步：" + stepName + "\r\n" + "运行过程中：\r\n\t" + OutIoName + "(IO口)\t没有触发，影响安全，是否继续执行,\r\n点\"中止\"急停处理，并需要复位", "警告", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Question);

				if (showRet == DialogResult.Ignore)
				{
					Global.WorkVar.tag_SuspendState = 0;
					return 0;
				}
				else
					if (showRet == DialogResult.Abort)
				{
					Global.WorkVar.tag_SuspendState = 0;
					return -5;
				}
				else
				{
					Global.WorkVar.tag_SuspendState = 0;
					return IoTrigger(station, OutIoName, var, stepName, man);
				}


			}
			else
			{
				return ret;
			}
		}

		/// <summary>
		/// 汽缸上升或则下降，station工位名称，OutIoName 输出点位名称，var 高低电平，inPutIoName 输入点位名称，varI 判断是否到达汽缸位置
		/// _manual 步骤名称
		/// </summary>
		/// <param name="station"></param>
		/// <param name="OutIoName"></param>
		/// <param name="var"></param>
		/// <param name="inPutIoName"></param>
		/// <param name="varI"></param>
		/// <param name="_manual"></param>
		/// <returns></returns>
		public static short OutIoUpOrDown(string station, string OutIoName, short var)
		{
			StationModule stationName = StationManage.FindStation(station);
			if (stationName == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show("工位：\r\n" + station + "  没有找到，请添加工位", "错误") == DialogResult.OK)
				{

				}
				return -1;
			}
			IOParameter ioCY1 = StationManage.FindOutputIo(stationName, OutIoName);
			if (stationName == null || ioCY1 == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show("工位：\r\n" + station + "\r\nIO名：" + OutIoName + "\t没有找到，请添加相应IO", "错误") == DialogResult.OK)
				{

				}
				return -2;
			}
			if (NewCtrlCardV0.SetOutputIoBit(ioCY1, var) != 0)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(OutIoName + "(IO口)\t执行错误.\r\n 解决方法：\r\n\t1:请检查伺服驱动\r\n\t2:重新检测点位配置！", "错误") == DialogResult.OK)
				{
				}
				return -3;
			}
			return 0;

		}
		/// <summary>
		/// 汽缸上升或则下降，station工位名称，OutIoName 输出点位名称，var 高低电平，inPutIoName 输入点位名称，varI 判断是否到达汽缸位置
		/// _manual 步骤名称
		/// </summary>
		/// <param name="station"></param>
		/// <param name="OutIoName"></param>
		/// <param name="var"></param>
		/// <param name="inPutIoName"></param>
		/// <param name="varI"></param>
		/// <param name="_manual"></param>
		/// <returns></returns>
		public static short OutIoUpOrDown(string station, string OutIoName, short var, string StepName)
		{
			StationModule stationName = StationManage.FindStation(station);
			if (stationName == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show("工位：\r\n" + station + "  没有找到，请添加工位") == DialogResult.OK)
				{

				}
				return -1;
			}
			IOParameter ioCY1 = StationManage.FindOutputIo(stationName, OutIoName);
			if (stationName == null || ioCY1 == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show("工位：\r\n" + station + "\r\nIO名：" + OutIoName + "\t没有找到，请添加相应IO", "错误") == DialogResult.OK)
				{

				}
				return -2;
			}
			if (NewCtrlCardV0.SetOutputIoBit(ioCY1, var) != 0)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show("工位：\r\n" + station + "\r\n步骤：\r\n" + StepName + "\r\nIO名：\r\n" + OutIoName + "\r\n" + "执行错误，请检查伺服驱动！", "提示") == DialogResult.OK)
				{
				}
				return -3;
			}
			return 0;

		}
		/// <summary>
		/// io 运行， io执行 成功返回0
		/// </summary>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static short IORunMut(PointAggregate pa, manual man)//
		{
			string strStationName = null;
			short ret = 0;
			foreach (OutIOParameterPoint io in pa.tag_AxisSafeManage.tag_InIoList)
			{
				if (io != null && io.tag_IniO2 != null && io.tag_IniO2.tag_IOName != null)
				{

					if (io.tag_IniO2.tag_name == null)
					{
						strStationName = pa.strStationName;
					}
					else
					{
						strStationName = io.tag_IniO2.tag_name;
					}
					if ((ret = IoTrigger(strStationName, io.tag_IniO2.tag_IOName, io.tag_IniO2.tag_var, pa.strName, man)) != 0)
					{

						return ret;
					}
				}
			}

			long maxTtimeOut = 0;
			OutIOParameterPoint ioMax = null;
			foreach (OutIOParameterPoint io in pa.tag_AxisSafeManage.tag_InIoList)
			{
				if (io.tag_InOut1.tag_IOParameterOutTime > maxTtimeOut)
				{
					maxTtimeOut = io.tag_IniO1.tag_IOParameterOutTime;
					ioMax = io;
				}
			}
			foreach (OutIOParameterPoint io in pa.tag_AxisSafeManage.tag_InIoList)
			{
				if (io != null && io.tag_InOut1.tag_IOName != null)
				{
					short var = Global.CConst.OUTPUTOFF;
					if (io.tag_InOut1.tag_var)
					{
						var = Global.CConst.OUTPUTON;
					}
					if (io.tag_InOut1.tag_name == null)
					{
						strStationName = pa.strStationName;
					}
					else
					{
						strStationName = io.tag_InOut1.tag_name;
					}
					if ((ret = OutIoUpOrDown(strStationName, io.tag_InOut1.tag_IOName, var, pa.strName)) != 0)
					{
						if (io.tag_InOut1.tag_IOParameterOutTime > 0)
							Thread.Sleep((int)io.tag_InOut1.tag_IOParameterOutTime);
						return ret;
					}

				}
				if (io != null && io.tag_InOut2.tag_IOName != null)
				{
					short var = Global.CConst.OUTPUTOFF;
					if (io.tag_InOut2.tag_var)
					{
						var = Global.CConst.OUTPUTON;
					}
					if (io.tag_InOut2.tag_name == null)
					{
						strStationName = pa.strStationName;
					}
					else
					{
						strStationName = io.tag_InOut2.tag_name;
					}
					if ((ret = OutIoUpOrDown(strStationName, io.tag_InOut2.tag_IOName, var, pa.strName)) != 0)
					{
						if (io.tag_InOut2.tag_IOParameterOutTime > 0)
							Thread.Sleep((int)io.tag_InOut2.tag_IOParameterOutTime);
						return ret;
					}
				}
			}

			Thread.Sleep((int)maxTtimeOut);
			return WaitIoMutIoTrigger(pa, man);

		}
		/// <summary>
		/// IO是否安全 0表示成功
		/// </summary>
		/// <param name="pa"></param>
		/// <param name="io"></param>
		/// <param name="man"></param>
		/// <returns></returns>
		public static short IOIsSafe(PointAggregate pa)
		{
			short ret = 0;
			bool io_status = true;
			if (pa == null || pa.tag_AxisSafeManage == null || pa.tag_AxisSafeManage.tag_InIoList == null)
			{
				return 0;
			}
			foreach (OutIOParameterPoint io in pa.tag_AxisSafeManage.tag_InIoList)
			{
				if (io != null && io.tag_IniO2 != null && io.tag_IniO2.tag_IOName != null)
				{

					if (NewCtrlCardV0.GetInputIoBitStatus(pa.strStationName, io.tag_IniO2.tag_IOName, out io_status) == 0)
					{
						if (io_status != io.tag_IniO2.tag_var)
						{
							UserControl_LogOut.OutLog(pa.strStationName + "-" + pa.strName + "运动不安全，" + io.tag_IniO2.tag_IOName + ",没有到位", 0);
							return -1;
						}
					}
					else
					{
						UserControl_LogOut.OutLog(pa.strStationName + "-" + pa.strName + "运动不安全，" + io.tag_IniO2.tag_IOName + ",执行异常", 0);

						return -2;
					}

				}
			}
			return ret;
		}
		/// <summary>
		/// io 运行， io执行 成功返回0
		/// </summary>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static short IORun(PointAggregate pa, OutIOParameterPoint io, manual man)//
		{
			short ret = 0;
			string strStationName = null;
			try
			{
				/*
                 * 判断安全IO是否触发，
                 */
				if (io != null && io.tag_IniO2 != null && io.tag_IniO2.tag_IOName != null)
				{

					if (io.tag_IniO2.tag_name == null)
					{
						strStationName = pa.strStationName;
					}
					else
					{
						strStationName = io.tag_IniO2.tag_name;
					}
					if ((ret = IoTrigger(strStationName, io.tag_IniO2.tag_IOName, io.tag_IniO2.tag_var, pa.strName, man)) != 0)
					{

						return ret;
					}
				}

				if (io != null && io.tag_InOut1.tag_IOName != null)
				{
					short var = Global.CConst.OUTPUTOFF;
					if (io.tag_InOut1.tag_var)
					{
						var = Global.CConst.OUTPUTON;
					}
					if (io.tag_InOut1.tag_name == null)
					{
						strStationName = pa.strStationName;
					}
					else
					{
						strStationName = io.tag_InOut1.tag_name;
					}
					if ((ret = OutIoUpOrDown(strStationName, io.tag_InOut1.tag_IOName, var, pa.strName)) != 0)
					{
						if (io.tag_InOut1.tag_IOParameterOutTime > 0)
							Thread.Sleep((int)io.tag_InOut1.tag_IOParameterOutTime);
						return ret;
					}
					if (io.tag_InOut1.tag_IOParameterOutTime > 0)
						Thread.Sleep((int)io.tag_InOut1.tag_IOParameterOutTime);
				}
				if (io != null && io.tag_InOut2.tag_IOName != null)
				{
					short var = Global.CConst.OUTPUTOFF;
					if (io.tag_InOut2.tag_var)
					{
						var = Global.CConst.OUTPUTON;
					}
					if (io.tag_InOut2.tag_name == null)
					{
						strStationName = pa.strStationName;
					}
					else
					{
						strStationName = io.tag_InOut2.tag_name;
					}
					if ((ret = OutIoUpOrDown(strStationName, io.tag_InOut2.tag_IOName, var, pa.strName)) != 0)
					{
						if (io.tag_InOut2.tag_IOParameterOutTime > 0)
							Thread.Sleep((int)io.tag_InOut2.tag_IOParameterOutTime);
						return ret;
					}
				}
				if (io != null && io.tag_IniO1 != null && io.tag_IniO1.tag_IOName != null)
				{
					if (io.tag_IniO1.tag_name == null)
					{
						strStationName = pa.strStationName;
					}
					else
					{
						strStationName = io.tag_IniO1.tag_name;
					}
					ret = WaitIoTrigger(strStationName, io.tag_IniO1.tag_IOName, io.tag_IniO1.tag_var, io.tag_IniO1.tag_IOParameterOutTime, pa.strName, man);
				}
				return ret;
			}
			catch (Exception ex)
			{
				Global.WorkVar.tag_StopState = 2;

				return -100;
			}

		}

		/// <summary>
		/// 移动到StationName工位上pointName点位 ： StationName工位名称，pointName点位名称
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="pointName"></param>
		/// <returns></returns>
		public static short stepMovePointLineZRWait(PointAggregate pointA1)
		{

			StationModule tag_at = StationManage.FindStation(pointA1.strStationName);
			if (tag_at == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(pointA1.strStationName + "没有找到，请添加工位") == DialogResult.OK)
				{

				}
				return -11;
			}

			int i = 0;
			while (i < tag_at.arrAxis.Count)
			{
				AxisConfig axisC = tag_at.arrAxis[i];

				if (axisC.tag_XYZU_Type == 0 || !pointA1.arrPoint[i].blnPointEnable)
				{
					//不运行
				}
				else
				{
					if (NewCtrlCardV0.AxisAbsoluteMove(pointA1.strStationName, axisC.AxisName, pointA1.strName, i) != 0)
					{
						Global.WorkVar.tag_StopState = 2;
						if (MessageBoxLog.Show("工位:\r\n" + pointA1.strStationName + "\r\n轴名：\r\n" + axisC.AxisName + "\t移动异常，请检查伺服驱动！") == DialogResult.OK)
						{

						}
						return -4;
					}
				}
				i++;
			}
			return 0;
		}

		public static short stepMovePointLineNoWait(PointAggregate pointA1)
		{
			short ret = 0;
			AxisConfig[] axisArray;
			PointModule[] PointArray;
			StationModule tag_at = StationManage.FindStation(pointA1.strStationName);
			if (tag_at == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(pointA1.strStationName + "没有找到，请添加工位") == DialogResult.OK)
				{

				}
				return -11;
			}
			int i = 0;
			int count = 0;
			while (i < tag_at.arrAxis.Count)
			{
				if (tag_at.arrAxis[i].tag_XYZU_Type == 0 && pointA1.arrPoint[i].blnPointEnable)
				{
					count++;
				}
				i++;
			}
			i = 0;
			if (count > 0)
			{
				axisArray = new AxisConfig[count];
				PointArray = new PointModule[count];
				int j = 0;
				while (i < tag_at.arrAxis.Count)
				{
					if (tag_at.arrAxis[i].tag_XYZU_Type == 0 && pointA1.arrPoint[i].blnPointEnable)
					{
						axisArray[j] = tag_at.arrAxis[i];
						PointArray[j] = pointA1.arrPoint[i];
						j++;
					}
					i++;
				}
				if ((ret = NewCtrlCardV0.SR_LineMulticoorMove(axisArray, PointArray, 0, (short)pointA1.tag_motionType)) != 0)
				{
					return ret;
				}
			}
			ret = stepMovePointLineZRWait(pointA1);
			return ret;
		}

		/// <summary>
		/// 移动到StationName工位上pointName点位 ： StationName工位名称，pointName点位名称
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="pointName"></param>
		/// <returns></returns>
		public static short stepMovePointNoWait(PointAggregate pointA1)
		{

			if (pointA1.tag_MotionLineType == 1)
			{
				return stepMovePointLineNoWait(pointA1);
			}

			StationModule tag_at = StationManage.FindStation(pointA1.strStationName);
			if (tag_at == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(pointA1.strStationName + "没有找到，请添加工位") == DialogResult.OK)
				{

				}
				return -11;
			}

			int i = 0;
			while (i < tag_at.arrAxis.Count)
			{
				AxisConfig axisC = tag_at.arrAxis[i];

				if (pointA1.arrPoint[i].dblPonitValue >= 100000 || !pointA1.arrPoint[i].blnPointEnable)
				{
					//不运行
				}
				else
				{

					switch (axisC.MotionType)
					{
						case 0:
							if (NewCtrlCardV0.AxisAbsoluteMove(pointA1.strStationName, axisC.AxisName, pointA1.strName, i) != 0)
							{
								Global.WorkVar.tag_StopState = 2;
								if (MessageBoxLog.Show("工位:\r\n" + pointA1.strStationName + "\r\n轴名：\r\n" + axisC.AxisName + "\t移动异常，请检查伺服驱动！") == DialogResult.OK)
								{

								}
								return -4;
							}
							break;

					}

				}
				i++;
			}
			return 0;
		}
		/// <summary>
		/// 移动到StationName工位上pointName点位 ： StationName工位名称，pointName点位名称
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="pointName"></param>
		/// <returns></returns>
		public static short stepMovePoint(PointAggregate pointA1)
		{
			StationModule tag_at = StationManage.FindStation(pointA1.strStationName);
			if (tag_at == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(pointA1.strStationName + "没有找到，请添加工位") == DialogResult.OK)
				{

				}
				return -11;
			}


			int i = 0;
			short ret1 = 0;

			if ((ret1 = stepMovePointNoWait(pointA1)) != 0)
				return ret1;


			if (pointA1.tag_AxisMoveFun != null && Global.WorkVar.tag_GuanLian)
			{

				if ((ret1 = pointA1.tag_AxisMoveFun(pointA1)) != 0)
					return ret1;
			}
			i = 0;
			foreach (AxisConfig af in tag_at.arrAxis)
			{
				if (pointA1.arrPoint[i].blnPointEnable)
				{
					NewCtrlCardV0.WaitAxisStop(af);
				}
				i++;
			}
			i = 0;
			double pos_loadY = 0;
			foreach (AxisConfig af in tag_at.arrAxis)
			{
				if (pointA1.arrPoint[i].blnPointEnable)
				{
					if (pointA1.tag_motionType == 0 && Global.WorkVar.tag_SuspendState == 0)
					{
						short ret = NewCtrlCardV0.SR_GetPrfPos((int)af.tag_MotionCardManufacturer, af.CardNum, af.AxisNum, ref pos_loadY);
						if (Global.WorkVar.tag_SuspendState == 1 || (Global.WorkVar.bSuspendState_L && af.AxisName == "左Y轴") || (Global.WorkVar.bSuspendState_R && af.AxisName == "右Y轴"))
						{

						}
						else if (pos_loadY / af.Eucf < pointA1.arrPoint[i].dblPonitValue - 1 || pos_loadY / af.Eucf > pointA1.arrPoint[i].dblPonitValue + 1)
						{
							Global.WorkVar.tag_StopState = 2;
							if (MessageBoxLog.Show("工位:\r\n" + pointA1.strStationName + "\r\n轴：\r\n" + af.AxisName + "移动异常，请检查！") == DialogResult.OK)
							{
								return -1;
							}
						}

						//if ((Global.WorkVar.tag_SuspendState == 0 ) && (pos_loadY / af.Eucf < pointA1.arrPoint[i].dblPonitValue - 1 || pos_loadY / af.Eucf > pointA1.arrPoint[i].dblPonitValue + 1))
						//{
						//    Global.WorkVar.tag_StopState = 2;
						//    if (MessageBoxLog.Show("工位:\r\n" + pointA1.strStationName + "\r\n轴：\r\n" + af.AxisName + "移动异常，请检查！") == DialogResult.OK)
						//    {
						//        return -1;
						//    }
						//}
					}
					else
					{


					}
				}
				i++;
			}
			return 0;
		}
		/// <summary>
		/// 点位上的轴停止 ： StationName工位名称，pointName点位名称
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="pointName"></param>
		/// <returns></returns>
		public static short stepMovePointAxisStop(string StationName, string pointName)
		{
			StationModule tag_at = StationManage.FindStation(StationName);
			if (tag_at == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(StationName + "没有找到，请添加工位") == DialogResult.OK)
				{

				}
				return -11;
			}
			PointAggregate pointA1 = StationManage.FindPoint(tag_at, pointName);
			if (tag_at == null || pointA1 == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(StationName + "\r\n步骤:\r\n" + pointName + "\r\n没有找到，请添加点位") == DialogResult.OK)
				{

				}
				return -12;
			}
			int i = 0;
			while (i < tag_at.arrAxis.Count)
			{
				AxisConfig axisC = tag_at.arrAxis[i];


				if (!pointA1.arrPoint[i].blnPointEnable)
				{
					//不运行
				}
				else
				{
					if (NewCtrlCardV0.SR_AxisStop((int)axisC.tag_MotionCardManufacturer, axisC.CardNum, axisC.AxisNum) != 0)
					{
						Global.WorkVar.tag_StopState = 2;
						if (MessageBoxLog.Show("工位:" + StationName + "\r\n轴:\r\n" + axisC.AxisName + "停止异常，请检查伺服驱动！") == DialogResult.OK)
						{

						}
						return -4;
					}
				}
				i++;
			}
			i = 0;
			foreach (AxisConfig af in tag_at.arrAxis)
			{
				if (pointA1.arrPoint[i].blnPointEnable)
				{
					NewCtrlCardV0.WaitAxisStop(af);
				}
				i++;

			}
			double pos_loadY = 0;
			foreach (AxisConfig af in tag_at.arrAxis)
			{
				if (pointA1.arrPoint[i].blnPointEnable)
				{
					short ret = NewCtrlCardV0.SR_GetPrfPos((int)af.tag_MotionCardManufacturer, af.CardNum, af.AxisNum, ref pos_loadY);
					if (pointA1.tag_motionType == 0 && Global.WorkVar.tag_SuspendState == 0)
					{
						ret = NewCtrlCardV0.SR_GetPrfPos((int)af.tag_MotionCardManufacturer, af.CardNum, af.AxisNum, ref pos_loadY);
						if (Global.WorkVar.tag_SuspendState == 0 && (pos_loadY / af.Eucf < pointA1.arrPoint[i].dblPonitValue - 1 || pos_loadY / af.Eucf > pointA1.arrPoint[i].dblPonitValue + 1))
						{
							Global.WorkVar.tag_StopState = 2;
							if (MessageBoxLog.Show("工位:\r\n" + pointA1.strStationName + "\r\n轴：\r\n" + af.AxisName + "移动异常，请检查！") == DialogResult.OK)
							{
								return -1;
							}
						}
					}
					else
					{


					}
				}
				i++;
			}
			return 0;
		}
		/// <summary>
		/// 点位回零 StationName 工位名， o 步骤名， 执行成功返回0
		/// </summary>
		/// <param name="StationName"></param>
		/// <param name="o"></param>
		/// <returns></returns>
		public static short pointGoHome(string StationName, object o)
		{
			short ret = 0;
			PointAggregate pa = (PointAggregate)o;
			string pointName = pa.strName; ;
			StationModule tag_at = StationManage.FindStation(StationName);
			List<GoHome> gohoneLiset = new List<GoHome>();
			if (tag_at == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(StationName + "没有找到，请添加工位") == DialogResult.OK)
				{

				}
				return -11;
			}
			PointAggregate pointA1 = StationManage.FindPoint(tag_at, pointName);
			if (tag_at == null || pointA1 == null)
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(StationName + "\r\n步骤:\r\n" + pointName + "没有找到，请添加点位") == DialogResult.OK)
				{

				}
				return -12;
			}
			int i = 0;
			while (i < tag_at.arrAxis.Count)
			{
				AxisConfig axisC = tag_at.arrAxis[i];

				if (!pointA1.arrPoint[i].blnPointEnable)
				{
					//不运行
				}
				else
				{
					gohoneLiset.Add(new GoHome(StationName, axisC.AxisName));

				}
				i++;
			}
			foreach (GoHome go in gohoneLiset)
			{
				short r = go.waitEnd();
				if (r != 0)
					ret = r;
			}
			return 0;
		}
		/// <summary>
		/// 运行一个步骤， o 一个步骤，name 工位名称 ，成功返回 0 ，失败返回非零
		/// </summary>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static short pointRunStep(object o, string strStationName)
		{
			PointAggregate a = (PointAggregate)o;
			string StationName = (string)a.strStationName;

			short ret = 0;
			StationModule station = StationManage.FindStation(StationName);
			if (station != null)
			{
				//  if (step < station.arrPoint.Count)
				{
					PointAggregate pointA = a;
					if (pointA.tag_isEnable)
					{
						station.tag_stepId++;
						return 0;
					}
					if (pointA.tag_BeginPointAggregateList != null)
					{
						int j = 0;
						// 
						while (pointA.tag_BeginPointAggregateListIsEnable && !pointA.tag_isEnable && j < pointA.tag_BeginPointAggregateList.Count)
						{
							if (Global.WorkVar.tag_StopState == 1)
							{
								return -3;
							}
							if (Global.WorkVar.tag_SuspendState == 1)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
							{
								Thread.Sleep(10);
								continue;
							}
							if (Global.WorkVar.tag_IsExit == 1)
							{
								Thread.Sleep(10);
								return 0;
							}
							if ((ret = pointRun(pointA.tag_BeginPointAggregateList[j], StationName, null)) != 0)
							{
								Global.WorkVar.tag_StopState = 2;
								return ret;
							}
							j++;
						}

					}
					if (pointA != null)
					{
						ret = pointRun(pointA, strStationName, null);
						if (ret == 0)
						{
							station.tag_stepId++;

						}
						else
						{
							return ret;
						}
					}
					if (pointA.tag_EndPointAggregateList != null)
					{
						int j = 0;
						//
						while (pointA.tag_EndPointAggregateListIsEnable && !pointA.tag_isEnable && j < pointA.tag_EndPointAggregateList.Count)
						{
							if (Global.WorkVar.tag_StopState == 1)
							{
								return -3;
							}
							if (Global.WorkVar.tag_SuspendState == 1)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
							{
								Thread.Sleep(10);
								continue;
							}
							if (Global.WorkVar.tag_IsExit == 1)
							{
								Thread.Sleep(10);
								return 0;
							}
							if ((ret = pointRun(pointA.tag_EndPointAggregateList[j], StationName, null)) != 0)
							{
								Global.WorkVar.tag_StopState = 2;
								return ret;
							}
							j++;
						}

					}
				}
			}
			return -1;
		}


		/// <summary>
		/// 运行一个步骤， o 一个步骤，name 工位名称 ，成功返回 0 ，失败返回非零
		/// </summary>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static short pointRunStep(object o)
		{
			PointAggregate a = (PointAggregate)o;
			string StationName = (string)a.strStationName;

			short ret = 0;
			StationModule station = StationManage.FindStation(StationName);
			if (station != null)
			{
				{
					PointAggregate pointA = a;
					if (pointA.tag_isEnable)
					{
						station.tag_stepId++;
						return 0;
					}

					if (pointA != null)
					{
						ret = pointRun(pointA, a.strStationName, null);
						if (ret == 0)
							station.tag_stepId++;

						return ret;
					}

				}
			}
			return -1;
		}

		/// <summary>
		/// 运行一个步骤， o 一个步骤，name 工位名称 ，成功返回 0 ，失败返回非零
		/// </summary>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static short pointRun(object o, object name, manual _man)
		{
			manual man = null;
			if (_man == null)
			{
				man = new manual(0);
			}
			else
			{
				man = _man;
			}
			man.tag_ExePointAggregate = (PointAggregate)o;
			Global.WorkVar.tag_ExePointAggregate = (PointAggregate)o;
			string StationName = (string)name;
			PointAggregate pa = (PointAggregate)o;
			short ret = 0;
			if (StationName == null)
			{
				StationName = Global.WorkVar.tag_ExePointAggregate.strStationName;
			}
			int stationindex = StationManage.getStationIndex(StationName);
			UserControl_LogOut.OutLog(StationName + "->" + pa.strName, 1);
			if (pa.tag_isEnable)
			{
				return 0;
			}
			if (pa.tag_AxisSafeManage != null && !pa.tag_AxisSafeManage.PointIsSafe(pa))
			{
				Global.WorkVar.tag_StopState = 2;
				if (MessageBoxLog.Show(StationName + "\r\n步骤:\r\n" + pa.strName + "\r\n不在安全区，移动会有撞机的可能，请急停，复位！") == DialogResult.OK)
				{

				}
				return -5;
			}

			if (pa.tag_BeginFun != null && Global.WorkVar.tag_GuanLian)
			{
				if ((ret = pa.tag_BeginFun(pa)) != 0)
					return ret;
			}
			if (!pa.tag_isEnable && pa.tag_AxisSafeManage != null && pa.tag_AxisSafeManage.tag_InIoList != null && pa.tag_AxisSafeManage.tag_InIoList.Count > 0)
			{
				if (pa.tag_AxisSafeManage.tag_isAndCheck)
				{
					if ((ret = IORunMut(pa, man)) < 0)
					{
						return ret;
					}
				}
				else
				{
					for (int i = 0; i < pa.tag_AxisSafeManage.tag_InIoList.Count; i++)
					{
						if ((ret = IORun(pa, pa.tag_AxisSafeManage.tag_InIoList[i], man)) < 0)
							return ret;
					}
				}
			}
			if (pa.tag_isAxisStop)
			{
				if ((ret = stepMovePointAxisStop(pa.strStationName, pa.strName)) != 0) ;
				return ret;
			}
			{
				if (pa.tag_isRest == 1)
					ret = pointGoHome(pa.strStationName, o);
				if (ret != 0)
					return ret;
				//  if (pa.tag_isRest == 0)
				{

					if (pa.tag_isWait == 0)
					{
						if ((ret = stepMovePoint(pa)) != 0)
							return ret;
					}
					else
					{
						if ((ret = stepMovePointNoWait(pa)) != 0)
							return ret;
					}
				}

			}
			if (pa.tag_Sleep > 0)
			{
				Thread.Sleep(pa.tag_Sleep);
			}
			if (!pa.tag_isEnable && pa.tag_EndFun != null && Global.WorkVar.tag_GuanLian)
			{
				if ((ret = pa.tag_EndFun(pa)) != 0)
					return ret;
			}
			return 0;
		}
		/// <summary>
		/// 运行一个工位流程 StationName 工位名
		/// </summary>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static short StationRun(string StationName)
		{
			short ret = 0;
			StationModule station = StationManage.FindStation(StationName);
			if (station == null)
			{
				return -21;
			}
			int i = 0;

			try
			{
				while (i < station.intUsePointCount && i < station.arrPoint.Count)
				{
					PointAggregate pointA = station.arrPoint[i];
					if (Global.WorkVar.tag_StopState == 1)
					{
						return -3;
					}
					if (Global.WorkVar.tag_SuspendState == 1)
					{
						Thread.Sleep(10);
						continue;
					}
					if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
					{
						Thread.Sleep(10);
						continue;
					}
					if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
					{
						Thread.Sleep(10);
						continue;
					}
					if (Global.WorkVar.tag_IsExit == 1)
					{
						Thread.Sleep(10);
						return 0;
					}
					Thread.Sleep(10);
					if (pointA.tag_BeginPointAggregateList != null)
					{
						int j = 0;
						//  
						while (pointA.tag_BeginPointAggregateListIsEnable && !pointA.tag_isEnable && j < pointA.tag_BeginPointAggregateList.Count)
						{
							if (Global.WorkVar.tag_StopState == 1)
							{
								return -3;
							}
							if (Global.WorkVar.tag_IsExit == 1)
							{

								return 0;
							}
							if (Global.WorkVar.tag_SuspendState == 1)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
							{
								Thread.Sleep(10);
								continue;
							}
							if ((ret = pointRun(pointA.tag_BeginPointAggregateList[j], StationName, null)) != 0)
							{
								Global.WorkVar.tag_StopState = 2;
								return ret;
							}
							j++;
						}

					}
					if ((ret = pointRun(pointA, StationName, null)) != 0)
					{
						Global.WorkVar.tag_StopState = 2;
						return ret;
					}
					if (pointA.tag_EndPointAggregateList != null)
					{
						int j = 0;
						//
						while (pointA.tag_EndPointAggregateListIsEnable && !pointA.tag_isEnable && j < pointA.tag_EndPointAggregateList.Count)
						{
							if (Global.WorkVar.tag_StopState == 1)
							{
								return -3;
							}
							if (Global.WorkVar.tag_IsExit == 1)
							{

								return 0;
							}
							if (Global.WorkVar.tag_SuspendState == 1)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
							{
								Thread.Sleep(10);
								continue;
							}
							if ((ret = pointRun(pointA.tag_EndPointAggregateList[j], StationName, null)) != 0)
							{
								Global.WorkVar.tag_StopState = 2;
								return ret;
							}
							j++;
						}

					}
					i++;
				}
			}
			catch (Exception mes)
			{
				UserControl_LogOut.OutLog(mes.Message, 0);
				Global.WorkVar.tag_StopState = 2;
			}
			return 0;
		}
		/// <summary>
		/// 运行一个工位流程 StationName 工位名，man 步骤
		/// </summary>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static short StationRun(string StationName, manual man)
		{
			short ret = 0;
			StationModule station = StationManage.FindStation(StationName);
			if (station == null)
			{
				return -21;
			}
			if (man.tag_induction)
			{
				return 0;
			}
			try
			{
				while (man.tag_stepName < station.arrPoint.Count)
				{
					PointAggregate pointA = station.arrPoint[man.tag_stepName];
					if (Global.WorkVar.tag_StopState == 1)
					{
						return -3;
					}
					if (Global.WorkVar.tag_IsExit == 1)
					{

						return 0;
					}
					if (Global.WorkVar.tag_SuspendState == 1 || man.tag_isSuspend)
					{
						Thread.Sleep(10);

						continue;
					}
					if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
					{
						Thread.Sleep(10);
						continue;
					}
					if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
					{
						Thread.Sleep(10);
						continue;
					}
					if (pointA.tag_BeginPointAggregateList != null)
					{
						int i = 0;
						//
						while (pointA.tag_BeginPointAggregateListIsEnable && !pointA.tag_isEnable && i < pointA.tag_BeginPointAggregateList.Count)
						{
							if (Global.WorkVar.tag_StopState == 1)
							{
								return -3;
							}
							if (Global.WorkVar.tag_IsExit == 1)
							{
								Thread.Sleep(10);
								return 0;
							}

							if (Global.WorkVar.tag_SuspendState == 1 || man.tag_isSuspend)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
							{
								Thread.Sleep(10);
								continue;
							}
							if ((ret = pointRun(pointA.tag_BeginPointAggregateList[i], StationName, man)) != 0)
							{
								Global.WorkVar.tag_StopState = 2;
								return ret;
							}
							i++;
						}
					}
					if ((ret = pointRun(pointA, StationName, man)) != 0)
					{
						Global.WorkVar.tag_StopState = 2;
						return ret;
					}
					if (pointA.tag_EndPointAggregateList != null)
					{
						int i = 0;
						while (pointA.tag_EndPointAggregateListIsEnable && !pointA.tag_isEnable && i < pointA.tag_EndPointAggregateList.Count)
						{
							if (Global.WorkVar.tag_StopState == 1)
							{
								return -3;
							}
							if (Global.WorkVar.tag_IsExit == 1)
							{
								Thread.Sleep(10);
								return 0;
							}
							if (Global.WorkVar.tag_SuspendState == 1 || man.tag_isSuspend)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "左工位" && Global.WorkVar.bSuspendState_L)
							{
								Thread.Sleep(10);
								continue;
							}
							if (StationName == "右工位" && Global.WorkVar.bSuspendState_R)
							{
								Thread.Sleep(10);
								continue;
							}
							if ((ret = pointRun(pointA.tag_EndPointAggregateList[i], StationName, man)) != 0)
							{
								Global.WorkVar.tag_StopState = 2;

								return ret;
							}
							i++;
						}
					}
					man.tag_stepName++;
					Thread.Sleep(10);
				}

			}
			catch (Exception mes)
			{
				UserControl_LogOut.OutLog(mes.Message, 0);
				Global.WorkVar.tag_StopState = 2;
			}
			return 0;
		}
	}
}
