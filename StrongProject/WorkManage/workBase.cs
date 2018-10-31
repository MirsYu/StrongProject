using System;
using System.Threading;
namespace StrongProject
{

	public class workBase
	{
		public delegate bool delegate_safe(object o);
		/// <summary>
		/// 
		/// </summary>
		public manual tag_manual = new manual(0);
		/// <summary>
		/// 是否工作中 0表示没工作，1表示工作
		/// </summary>
		public int tag_isWork;

		//public int tag_laserIsWork;

		/// <summary>
		/// 判断是否是复位工位,0:不是，1:是
		/// </summary>
		public int tag_isRestStation;//
									 /// <summary>
									 /// 工位名
									 /// </summary>
		public string tag_stationName;

		/// <summary>
		/// 线程
		/// </summary>
		public Thread tag_workThread;
		/// <summary>
		/// SN
		/// </summary>
		public string tag_sn;
		/// <summary>
		/// 工作耗时
		/// </summary>
		public long tag_workTime;
		/// <summary>
		/// 工位开始时间
		/// </summary>
		public DateTime tag_beginDate;

		/// <summary>
		/// 工位结束时间
		/// </summary>
		public DateTime tag_endDate;

		public PDCA tag_PDCA;

		public delegate_safe tag_safe;



		public bool IsExit()
		{
			if (Global.WorkVar.tag_StopState == 1 || Global.WorkVar.tag_StopState == 2)
			{
				return true;
			}
			if (Global.WorkVar.tag_IsExit == 1)
			{

				return true;
			}
			while (Global.WorkVar.tag_SuspendState == 1)
			{
				if (Global.WorkVar.tag_StopState == 1 || Global.WorkVar.tag_StopState == 2)
				{
					return true;
				}
				if (Global.WorkVar.tag_IsExit == 1)
				{

					return true;
				}
				Thread.Sleep(10);
			}
			return false;
		}

		public static bool IsRestExit()
		{
			bool axio = false;
			bool stopIoS = false;
			NewCtrlCardV0.GetInputIoBitStatus("", "急停", out stopIoS);
			//NewCtrlCardV0.GetInputIoBitStatus("", "Z1报警", out axio);
			if (axio || !stopIoS)
			{
				UserControl_LogOut.OutLog("轴报警", 0);

				return true;
			}
			if (Global.WorkVar.tag_SuspendState == 1 || Global.WorkVar.tag_ButtonStopState == 1)
			{
				Global.WorkVar.tag_StopState = 1;
				Global.WorkVar.tag_SuspendState = 0;
				return true;
			}
			return false;
			if (Global.WorkVar.tag_StopState == 2)
			{
				return true;
			}
			if (Global.WorkVar.tag_IsExit == 1)
			{


				return true;
			}


			return false;
		}


	}
}
