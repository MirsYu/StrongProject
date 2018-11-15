using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace StrongProject
{
	public class AxisCheck : workBase
	{
		public Work tag_Work;

		DateTime begin;

		public AxisCheck(Work _Work)
		{
			tag_Work = _Work;
			tag_stationName = "轴整定";
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

			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "Z轴下降&R轴旋转", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;

			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "Z轴,R轴回0位", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;

			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "X,Y轴运动到正极限", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_BeginFun = Step3;

			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "X,Y轴运动到负极限", 3);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;

			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "工位结束", 4);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5;
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
			begin = DateTime.Now;
			return 0;
		}

		public short Step1(object o)
		{
			return 0;
		}

		public short Step2(object o)
		{
			return 0;
		}

		public short Step3(object o)
		{
			return 0;
		}

		public short Step4(object o)
		{
			return 0;
		}

		public short Step5(object o)
		{
			if (DateTime.Now - begin < new TimeSpan(0,30,0))
			{
				tag_manual.tag_stepName = 0;
			}
			return 0;
		}
	}
}
