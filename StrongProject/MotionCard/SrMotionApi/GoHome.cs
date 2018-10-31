using System.Threading;

namespace StrongProject
{
	public class GoHome
	{
		/// <summary>
		/// 工位名称 
		/// </summary>
		public string tag_stationName;

		/// <summary>
		/// 轴名称
		/// </summary>
		public string tag_AxisName;

		/// <summary>
		///-1 复位失败, 0 没有复位 , 1  复位中 ,2 复位完成 ,
		/// </summary>
		public short tag_work_state;

		/// <summary>
		/// 复位的结果，成功0，失败非0
		/// </summary>
		public short tag_GohomeResult;

		/// <summary>
		/// 执行复位步骤 
		/// </summary>
		public void GoHomeRun(object o)
		{
			tag_work_state = 1;
			tag_GohomeResult = NewCtrlCardV0.GoHome(tag_stationName, tag_AxisName);
			if (tag_GohomeResult == 0)
			{
				tag_work_state = 2;
			}
			else
			{
				tag_work_state = -1;
			}
		}

		/// <summary>
		/// 等待复位完成
		/// </summary>
		/// <returns></returns>
		public short waitEnd()
		{
			while (true)
			{
				if (Global.WorkVar.tag_StopState == 1) //暂停
				{

					break;
				}

				if (tag_work_state == 2 || tag_work_state == -1) //复位done、fail
				{
					break;
				}

				Thread.Sleep(10);
			}
			return tag_work_state;
		}

		/// <summary>
		///  线程回原
		/// </summary>
		/// <param name="stationName"></param>
		/// <param name="axisName"></param>
		/// <returns></returns>        
		public GoHome(string stationname, string AxisName)
		{

			StationModule stationM = StationManage.FindStation(tag_AxisName);
			tag_stationName = stationname;
			tag_AxisName = AxisName;
			Thread homeThread = new Thread(new ParameterizedThreadStart(GoHomeRun));
			homeThread.Start();
			homeThread.IsBackground = true;

		}
	}
}
