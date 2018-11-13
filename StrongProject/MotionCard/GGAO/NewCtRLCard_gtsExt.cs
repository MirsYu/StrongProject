using System;
using System.Threading;
using System.Windows.Forms;

namespace StrongProject
{
	public class NewCtRLCard_gtsExt : NewCtrlCardBase
	{
		public NewCtRLCard_gtsExt( )
		{
			tag_AxisCount = 0;
			SR_FunInit();
			tag_Manufacturer = MotionCardManufacturer.MotionCardManufacturer_gtsExt;
		}

		public static bool blnRun = false;  //程序启动/退出标志需在程序初始化时赋值 
		private const int intCardCount = 3; //卡的数量 (两个8轴 1个四轴)
		private const int intAxisCountForCard = 8;//卡轴的数量
		private const int intExtendIoCount = 3;   //扩展IO模块数量 
		private const int intExtendStartId = 4; //扩展模块起始号
		private static short shrGtsSuccess = 0;    //固高函数成功
		private static short shrSuccess = 0;    //二次封装函数成功
		private static short shrFail = -1;      //二次封装函数失败
		private static short shrFail2 = -2;      //二次封装函数失败
		private static double dblAxisAcc = 7;   //减速度
		private static double dblAxisDec = 7;   //加速度
		private static short shtSmoothTime = 25; //平滑时间
		private static short shtPstMoveDir = 0; //正方向运动
		public const int Torlerance = 50; //最大误差脉冲数        
		public const int DtTime = 20;  //单位微秒，持续保存小于误差数后置位到位信号
		private static double dblMoveOutOriginTime = 5; //移出原点感应器信号超时时间，单位秒
		public const int AxisMotionError = 500;  //轴移动误差，单位脉冲
		public const int IoClose = 1;  //关闭IO
		public const int IoOpen = 0;  //打开IO


		public void SR_FunInit()
		{

			SR_InitCard = _SR_InitCard;
			SR_GetInputBit = _SR_GetInputBit;
			SR_GetOutputBit = _SR_GetOutputBit;
			SR_SetOutputBit = _SR_SetOutputBit;
		}

		/// <summary>
		/// 卡初始化    HUA
		/// <param name="card"></param>
		/// <param name="axisCount"></param>
		/// <param name="configFileName"></param>
		/// <returns></returns>
		public short _SR_InitCard()
		{
			if (InitExtCard(0,"\\ExtModule.cfg") == false)
			{
				string str = "固高扩展卡初始化失败!";
				MessageBoxLog.Show(str);
				return -1;
			}
			return 1;
		}

		/// <summary>
		/// 程序初始化卡函数
		/// </summary>
		/// <returns></returns>
		public bool InitExtCard(short card,string path)
		{
			short shrResult;
			shrResult = mc.GT_OpenExtMdlGts(card, "Gts.dll");
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_OpenExtMdlGts", shrResult);
				return false;
			}
			shrResult = mc.GT_LoadExtConfigGts(card, Application.StartupPath + path);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_OpenExtMdlGts", shrResult);
				return false;
			}
			return true;
		}

		/// <summary>
		/// 获取单点 输入信号 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="ioBit">0开始， 0-15</param>
		/// <param name="bStatus"></param>
		/// <returns></returns>
		public short _SR_GetInputBit(short card, short ioBit, out bool bStatus)
		{
			short shrResult;
			int pValue = 0;
			bStatus = false;
			ushort ushortValue = 0;

			//if (card >= intExtendStartId)
			{
				shrResult = mc.GT_GetExtIoBitGts(0, (short)(card), ioBit, ref ushortValue);
				if (shrResult != shrGtsSuccess)
				{
					CommandResult("GT_GetExtIoValueGts", shrResult);
					return shrFail;
				}
			}
			//else
			//{
			//	shrResult = mc.GT_GetDi(card, mc.MC_GPI, out pValue);
			//	if (shrResult != shrGtsSuccess)
			//	{
			//		CommandResult("GT_GetDi", shrResult);
			//		return shrFail;
			//	}
			//}
			if (ushortValue == 0)
			{
				bStatus = false;
			}
			else
			{
				bStatus = true; 
			}
			//bStatus = (pValue & (1 << ioBit)) > 0 ? true : false;
			return shrResult;

		}

		/// <summary>
		/// 获取单个输出IO状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="ioBit"></param>
		/// <param name="outputIoStatus"></param>
		/// <returns></returns>
		public short _SR_GetOutputBit(short card, short ioBit, out bool outputIoStatus)
		{
			short shrResult;
			int pValue = 0;
			outputIoStatus = false;

			shrResult = mc.GT_GetDo(card, mc.MC_GPO, out pValue);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetDi", shrResult);
				return shrFail;
			}
			outputIoStatus = (pValue & (1 << ioBit)) <= 0 ? true : false;
			return 0;
		}

		/// <summary>
		/// 设置单点 输出信号 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="ioBit">0开始， 0-15</param>
		/// <param name="value"></param>
		/// <returns></returns>
		public short _SR_SetOutputBit(short card, short ioBit, short value)
		{
			short shrResult = 0;
			if (value == 0)
			{
				value = 1;
			}
			else
				value = 0;
			if (card >= intExtendStartId)
			{
				shrResult = mc.GT_SetExtIoBitGts(0, (short)(card - intExtendStartId), ioBit, (ushort)value);
				if (shrResult != shrGtsSuccess)
				{
					CommandResult("GT_SetExtIoBitGts", shrResult);
					return shrFail;
				}
			}
			else
			{
				shrResult = mc.GT_SetDoBit(card, mc.MC_GPO, (short)(ioBit + 1), value);
				if (shrResult != shrGtsSuccess)
				{
					CommandResult("GT_SetDoBit", shrResult);
					return shrFail;
				}
			}
			return shrResult;
		}
	}
}