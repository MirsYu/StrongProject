using System;
using System.Threading;
using System.Windows.Forms;
namespace StrongProject
{
	public class Rest : workBase
	{
		public Work tag_Work;
		/// <summary>
		/// 
		/// </summary>
		public Thread tag_workThread;

		/// <summary>
		/// 上一个步骤的名称
		/// </summary>
		public Rest(Work _Work)
		{
			tag_Work = _Work;

		}
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public bool start()
		{
			if (Global.WorkVar.tag_ResetState == 1)
			{
				UserControl_LogOut.OutLog("正在复位", 0);
				return false;
			}
			if (Global.WorkVar.tag_workState == 1)
			{
				UserControl_LogOut.OutLog("工作中", 0);
				return false;
			}
			if (tag_workThread != null)
			{
				tag_workThread.Abort();
			}
			Global.WorkVar.tag_ResetState = 1;
			Global.WorkVar.InitCommunicateStatus();
			tag_workThread = new Thread(new ParameterizedThreadStart(work));
			tag_workThread.Start();
			tag_workThread.IsBackground = true;
			return true;
		}
		/// <summary>
		/// 退出函数
		/// </summary>
		/// <returns></returns>
		public bool Exit()
		{
			return true;
		}
		/// <summary>
		/// 工作函数
		/// </summary>
		/// <param name="o"></param>
		public void work(object o)
		{

			tag_isWork = 0;
			work1(null);
		}
		/// <summary>
		///启动列表
		/// </summary>
		public void startList()
		{
			foreach (object o in tag_Work.tag_workObject)
			{

				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
				System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
				workBase wb = (workBase)o;
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i].Name == "StartThread")
					{
						if (wb.tag_isRestStation == 1)
						{
							wb.tag_isWork = 1;
							wb.tag_manual.tag_stepName = 0;
							methods[i].Invoke(o, null);
						}
					}

				}
			}
		}
		/// <summary>
		/// IO是否安全
		/// </summary>
		/// <returns></returns>
		public bool IsafeIOInit()
		{
			foreach (object o in tag_Work.tag_workObject)
			{
				Type t = o.GetType();
				workBase wb = (workBase)o;
				wb.tag_manual.tag_ExePointAggregate = null;
				Global.WorkVar.tag_ExePointAggregate = null;

			}

			return true;
		}

		/// <summary>
		///启动列表
		/// </summary>
		public void startWorkInit(int isrest, int j)
		{
			foreach (object o in tag_Work.tag_workObject)
			{

				Type t = o.GetType();
				System.Reflection.MethodInfo[] methods = t.GetMethods();
				System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
				System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
				workBase wb = (workBase)o;
				for (int i = 0; i < methods.Length; i++)
				{

					if (wb.tag_isRestStation == isrest)
					{
						wb.tag_isWork = j;

					}
				}
			}
		}
		/// <summary>
		/// 复位是否完成 0:未完成 1 完成
		/// </summary>
		public int restIsComplete()
		{
			foreach (object o in tag_Work.tag_workObject)
			{

				workBase wb = (workBase)o;
				if (wb.tag_isRestStation == 1)
				{
					if (wb.tag_isWork == 1)
						return 0;
				}
			}
			return 1;
		}
		/// <summary>
		/// 复位是否陈宫0:不成功 1 成功
		/// </summary>
		public int restIsSuccess()
		{
			foreach (object o in tag_Work.tag_workObject)
			{

				workBase wb = (workBase)o;
				if (tag_isWork != 0)
					return 0;
			}
			return 1;
		}
		/// <summary>
		/// 工作流程
		/// </summary>
		/// <param name="o"></param>
		public short work1(object o)
		{
			Global.WorkVar.tag_StopState = 0;
			IsafeIOInit();
			if (NewCtrlCardV0.tag_initResult != 0)
			{
				if (MessageBoxLog.Show("板卡没有初始化，是否当做复位成功?,确认当做复位成功", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
				{
					Global.WorkVar.tag_ResetState = 0;
					Global.WorkVar.tag_IsExit = 0;
					Global.WorkVar.bcanRunFalg = false;
					return -1;
				}
				else
				{
					Global.WorkVar.tag_StopState = 0;
					Global.WorkVar.tag_ResetState = 2;
					Global.WorkVar.tag_IsExit = 0;
					Global.WorkVar.bcanRunFalg = false;
					return 0;
				}
			}
			if (StationManage.OpenSevroAllAxis() != 0)
			{
				Global.WorkVar.tag_ResetState = 0;
				Global.WorkVar.tag_IsExit = 0;
				Global.WorkVar.bcanRunFalg = false;
				UserControl_LogOut.OutLog("使能失败", 0);
				tag_isWork = 0;
				return -1;
			}
			Global.WorkVar.tag_IsExit = 0;
			Global.WorkVar.tag_ButtonStopState = 0;
			startWorkInit(1, 1);
			startList();
			Thread.Sleep(3000);
			while (true)
			{

				if (Global.WorkVar.tag_StopState != 0)
				{
					Global.WorkVar.tag_ResetState = 0;
					Global.WorkVar.tag_IsExit = 0;
					Global.WorkVar.bcanRunFalg = false;
					UserControl_LogOut.OutLog("复位失败", 0);
					tag_isWork = 0;
					return -3;
				}
				if (restIsComplete() == 1)
				{
					break;
				}
				Thread.Sleep(10);
			}
			if (restIsSuccess() == 0)
			{
				Global.WorkVar.tag_ResetState = 0;
				Global.WorkVar.tag_SuspendState = 0;
				Global.WorkVar.tag_IsExit = 0;
				Global.WorkVar.bcanRunFalg = false;
				UserControl_LogOut.OutLog("复位失败", 0);
				tag_isWork = 0;
				return -4;
			}
			else
			{
				Global.WorkVar.tag_StopState = 0;
				Global.WorkVar.tag_SuspendState = 0;
				Global.WorkVar.tag_IsExit = 0;
				Global.WorkVar.bcanRunFalg = false;
				Global.WorkVar.tag_ResetState = 2;
				startWorkInit(0, 0);
				IsafeIOInit();
				UserControl_LogOut.OutLog("复位成功", 1);
			}
			tag_isWork = 0;
			return 0;
		}
	}
}
