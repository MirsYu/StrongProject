using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
//using Leadshine.CNC.Solder.Core;

using gts;
using System.Windows.Forms;

#region CardVarType
using TCard = System.Int16;
using TAxis = System.Int16;
using TMode = System.Int16;
using TSpeed = System.Double;
using TAcc = System.Single;

using TReturn = System.Int16;
using TPusle = System.Int32;

using TIOPoint = System.Int16;
#endregion



namespace StrongProject
{

	#region InterfaceClass

	public class CustomIOArgument : EventArgs
	{
		private int IOIder;
		public CustomIOArgument(int IOid)
		{
			//IOStatus=arrStatus;
			this.IOIder = IOid;
		}
		public int IOid
		{
			get
			{
				return this.IOIder;
			}
		}
	}
	[Serializable]
	public class IOinfoInput
	{
		public int IOid;//io 的标示
		public string IOname;//io描述 根据IOActualPoint加载数组
		public bool bState;//IO的状态。true为有输入
		public bool bStatePulseUp;//输入上升沿
		public bool bStatePulseDown;//输入下降沿
		public bool bLogicHigh;//IO的状态配置 最好能不放开 ture为高电平有效
		public bool bConfigRepeat;//配置出现重复
		public int IOcard;//卡号
		public int IOpoint;//IO点号
		
		public IOinfoInput()
		{
			IOid=0;
			//IOActualPoint=0;
			IOname="";
			bState=false;
			bLogicHigh = false;
			bConfigRepeat=false;
			IOcard=0;
			IOpoint=0;
		}
	}
	[Serializable]
	public class IOinfoOutput
	{
		public TIOPoint  IOid;//io 的标示。初始值，可以用来比对IO是否有改动
		public int  IOcard;//卡号
		public int  IOpoint;//IO点号
		public string IOname;//io描述 根据IOActualPoint加载数组
		public bool bState;//IO的状态。true为有输入
		public bool bLogicHigh;//IO的状态配置 最好能不放开 ture为高电平有效
		public bool bConfigRepeat;//配置出现重复
		public IOinfoOutput()
		{
			IOid=0;
			IOname="";
			bState=false;
			bLogicHigh = false;
			bConfigRepeat=false;
			IOcard = 0;
			IOpoint = 0;
		}
        public bool setOut(bool set,bool bShowMsg=false)
        {
            string result="";
            if (Limit.getPermit(IOid, out result))
            {
                CtrlCard.WriteOutputP((TIOPoint)IOcard, (TIOPoint)IOpoint, set ? CtrlCard.BCARD.OUT_TRUE : CtrlCard.BCARD.OUT_FALSE);
            }
            else
            {
                if (bShowMsg)
                {
                    MessageBoxLog.Show(result);
                }
                return false;
            }
            return true;
        }
	}
	public class CardBase
	{//原则上基类只提供常量
		protected  static bool canRun()
		{
            return true;
            //return Global.FN.IsPermitRun();
		}
		public const TReturn RET_SUCCESS = 0; //执行返回值，成功
		public const TReturn RET_FAIL = -1;//执行返回值，失败
		public const TMode FIND_ORI = 1;
		public const TMode FIND_NEG = 2;
		public const TMode FIND_POSITIVE = 3;
		public class HMode
		{
			public const string  HOME_MODE_NEGATIVE = "负限位回原";
			public const string HOME_MODE_POSITIVE="正限位回原";
			public const string  HOME_MODE_ORI = "原点回原";
			public const string  HOME_MODE_NEG_ORI = "负限加原点";		
			public const string  HOME_MODE_POSITIVE_ORI = "正限加原点";
			public static string[] homeModes = { HOME_MODE_NEGATIVE, HOME_MODE_POSITIVE,HOME_MODE_ORI, HOME_MODE_NEG_ORI, HOME_MODE_POSITIVE_ORI };
		}
		public static class BCARD
		{
			public const TAxis AXIS_QTY_ALL = 20;
			public const TAxis AXIS_QTY_SINGLE = 8;

			public const short CARD_NUM_0 = 0;
			public const short CARD_NUM_1 = 1;
			public const short CARD_NUM_2 = 2;
			public const TCard CARD_QTY = 3;//卡数量

			public const TIOPoint INPUT_QTY = 48;//输入点数
			public const TIOPoint OUTPUT_QTY = 48;//输出点数
			public const TIOPoint  POINT_QTY_SINGLE=16;//单卡点数量

			public const TMode OUT_TRUE = 0;//输出开
			public const TMode OUT_FALSE = 1;//输出关
		}
		public static class BAX
		{
			public const int STS_FLAG_ALARM = 1; //伺服报警标志 报警置1
			public const int STS_FLAG_MERROR = 4; //跟随误差越限标志 触发置1
			public const int STS_FLAG_LIMIT_POSITIVE = 5; //正限触发标志 触发置1
			public const int STS_FLAG_LIMIT_NEGATIVE = 6; //负限触发标志 触发置1
			public const int STS_FLAG_STOP_SMOOTH = 7; //平滑停止标志 平滑停止置1
			public const int STS_FLAG_STOP_EMG = 8; //急停标志 急停触发置1
			public const int STS_FLAG_SEVER_ON = 9; //伺服使能标志 使能置1
			public const int STS_FLAG_MOTION = 10; //规划运动标志 运动中置1
			public const int STS_FLAG_INPOSITION = 11; //电机到位标志 到位置1

			public const TMode DIR_NEGATIVE = 0;//运动方向 -负
			public const TMode DIR_POSITIVE = 1;

			public const int SMOOTHTIME = 30;//平滑时间
			public const int torlerance=40;
			public const int iTime = 40;//误差带保持时间   微秒

			public const double AXIS_DEC = 7;//0.25
			public const double AXIS_ACC = 7;//0.25
			public const double AXIS_VEL_START = 0;//


			public static  TPusle [] LogicPos0=new TPusle[16];
			public static  TPusle [] LogicEnc0=new TPusle[16];
		}


	}
	public class CAxisParams : CardBase
	{
		public TCard CardNo;//卡号
		public TAxis AxisNo;//轴号
		//public TSpeed Lead;//导程
		//public TPusle PulsePerR;//脉冲每圈
		public TSpeed PulsePerMM;//脉冲每毫米 浮点数
		public TSpeed Acc;//加速度
		public TSpeed Dec;//减速度
		public TSpeed VelStart;//启动速度
		public TSpeed VelTarget;//目标速度
		public TSpeed VelHomeHigh;//回原高速
		public TSpeed VelHomeLow;//回原低速
		public TSpeed VelAuto;//自动速度
		public TSpeed VelManu;//手动速度

		public TPusle DisHomeNeg;//回原负向搜索最大距离 负值 -4000000
		public TPusle DisHomePositive;//回原正向搜索最大距离 正向 4000000
		public string  HomeMode;//回原模式


		public TPusle posLogic;//当前逻辑位置
		public TPusle posEnc;//当前编码器位置
		public TSpeed  posLogicMM;//当前逻辑位置转实际位置=posLogic/PulsePerMM,显示和保存参数用这个变量
		public TSpeed  posEncMM;//当前编码器位置转实际位置=posEnc/PulsePerMM

		public TPusle SoftLimitNegativeSet;//设置的负软限位  正负为零时不判断
		public TPusle SoftLimitPositiveSet;//正软限位
		public TSpeed  homeOffset;//回原原点偏移量

		public stAxisStatus ax;

		public CAxisParams(TCard card, TAxis axis)
		{
			CardNo = card;
			AxisNo = axis;//轴号
			//Lead = 10;
			//PulsePerR = 10000;
			PulsePerMM  = 100000;
			Acc = 3;//加速度
			Dec = 3;//减速度
			VelStart = 0;
			VelTarget = 100;
			VelHomeHigh = 10;//回原高速
			VelHomeLow = 3;//回原低速
			VelAuto = 100;//启动速度
			VelManu = 10;//目标速度
			SoftLimitNegativeSet = -100;//负软限位
			SoftLimitPositiveSet = 100;//正软限位
			DisHomeNeg = -4000000;//回原负向搜索最大距离 负值 -4000000
			DisHomePositive = 4000000;//回原正向搜索最大距离 正向 4000000
			HomeMode =HMode.HOME_MODE_NEG_ORI ;//回原模式
			//HomeMode =CardBase.HMode.HOME_MODE_NEG_ORI ;//回原模式
			ax = new stAxisStatus();

		}
		public override string ToString()
		{
			string str=string.Format("Card:{0},Axis{1},Pos{2},PosMm{3}",CardNo,AxisNo,posLogic ,posLogicMM );
			return str;
		}

		private TAxis GetAxisArrayIndex(TCard card, TAxis axis)
		{
			return (TAxis)(BCARD.AXIS_QTY_SINGLE * card + axis - 1);
		}
		//脉冲转换为毫米
		public TSpeed  pulse_to_mm(TPusle pulse)
		{
			return pulse/ PulsePerMM;
		}
		//毫米转换为脉冲
		public TPusle mm_to_pulse(TSpeed mm)
		{
			return (int)(mm*PulsePerMM);
		}
		//速度脉冲转换为毫米
		public TSpeed  pulseV_to_mmV(TPusle pulse)
		{
			return (1000*pulse)/ PulsePerMM;
		}
		//速度毫米转换为脉冲
        public TSpeed mmV_to_pulseV(TSpeed mm)
		{
            return (TSpeed)(mm * PulsePerMM / 1000);
		}
        
		public void setHomeFinish()
		{
			ax.HomeFinish=true;
			ax.HomeFail=false;
			ax.Homing=false;
		}
		public void setHomeFail()
		{
			ax.HomeFinish=false ;
			ax.HomeFail=true ;
			ax.Homing=false;
		}
		public void setHoming()
		{
			ax.HomeFinish=false ;
			ax.HomeFail=false;
			ax.Homing=true ;
		}
		public void ResetHome()
		{
			ax.HomeFinish=false ;
			ax.HomeFail=false;
			ax.Homing=false  ;
		}
		private void setSoftLimit(ref stAxisStatus ax, TSpeed posNow)
		{
			if (SoftLimitNegativeSet != 0 || SoftLimitPositiveSet != 0)
			 {
				 ax.InLimitNegativeSL = (posLogicMM  < SoftLimitNegativeSet) ? true : false;
                 ax.InLimitPositiveSL = (posLogicMM > SoftLimitPositiveSet) ? true : false;				
			 }
			 else
			 {
				ax.InLimitNegativeSL=ax.InLimitPositiveSL=false;
			 }
		}
		public void UpdateStatus()
		{
			int axisIndex = GetAxisArrayIndex(CardNo, AxisNo);

			posLogic = CARDSTS.LogicPos0[axisIndex];
			posEnc = CARDSTS.EncodePos0[axisIndex];
			posEncMM = pulse_to_mm(posEnc);
			posLogicMM = pulse_to_mm(posLogic);
			setSoftLimit(ref ax,posLogicMM);

			ax.AxisMoving = CARDSTS.AxisMoving[axisIndex];
			ax.InLimitOri = CARDSTS.InLimitOri[axisIndex];
			ax.InServerAlarm = CARDSTS.InServerAlarm[axisIndex];
			ax.InLimitPositive = CARDSTS.InLimitPositive[axisIndex];
			ax.InLimitNegative = CARDSTS.InLimitNegative[axisIndex];

            ax.HomeFail = CtrlCardSR.AxisParams[axisIndex].ax.HomeFail;
            ax.HomeFinish = CtrlCardSR.AxisParams[axisIndex].ax.HomeFinish;
            ax.Homing = CtrlCardSR.AxisParams[axisIndex].ax.Homing;
            //用下面赋值回原成功率低
            //ax.HomeFail = CARDSTS.HomeFail[axisIndex];
            //ax.HomeFinish = CARDSTS.HomeFinish[axisIndex];
            //ax.Homing = CARDSTS.Homing[axisIndex];

            //ax.AxisMoving = CARDSTS.AxisMoving[axisIndex];
		}

	}
	public struct stAxisStatus
	{
		public bool IsServe;//是否是伺服电机
		public bool InLimitNegative;// input of axis negative
		public bool InLimitPositive;//input of axis positive
		public bool InServerAlarm;//input of axis alarm
		public bool InLimitOri;//input of axis ori
		public bool AxisMoving;//axis moving

		public bool InLimitNegativeSL;//负软限位
		public bool InLimitPositiveSL;//正软限位

		public bool HomeFinish;//home finish 三个状态只能同时满足一个
		public bool HomeFail;//home fail
		public bool Homing;// 回原中
	}
	#endregion


	public class CtrlCard : CardBase
	{

		//轴允许运动函数

		////初始全局参数
		public static void InitPara()
		{

		}
        public static TReturn EnableServe()
        {
            for (short card = 0; card < CARDSTS.BCARD.CARD_QTY; card++)
            {
                for (short axis = 0; axis < 8; axis++)
                {
                    mc.GT_ClrSts(card, (short)(axis+1), 1);//1001 09
 
                    gts.mc.GT_AxisOn(card, (short)(axis + 1));
                    if (card == CARDSTS.BCARD.CARD_QTY - 1 && axis == 3)
                    {
                        break;
                    }
                }

            }
            return RET_SUCCESS;
        }
		//卡初始化 返回卡数量
		public static TReturn InitCard()
		{
			return CtrlCardSR.SR_InitCard();
		}
		//关闭控制卡
		public static TReturn CloseCard()
		{//RET_SUCCESS success . RET_FAIL fail
			return CtrlCardSR.SR_CloseCard();
		}
		//判断某个轴是否停止运动
		public static bool IsAxisMoving()
		{
			for (TCard card = 0; card < BCARD.CARD_QTY; card++)
			{
				for (TAxis axis = 0; axis < 8; axis++)
				{
					if (CARDSTS.AxisMoving[axis   + 8 * card])
						return true;
                    if ((8 * card + axis) == BCARD.AXIS_QTY_ALL - 1) break;//0927
				}
			}
			return false;
		}
		//更新轴运动状态
		public static void GetAxisMoveStatus()
		{
			for (TCard card = 0; card < BCARD.CARD_QTY; card++)
			{
				for (TAxis axis = 0; axis < 8; axis++)
				{
					CARDSTS.AxisMoving[axis  + 8 * card] = (CtrlCardSR.SR_IsAxisStop  (card, (short)(axis+1)) == RET_SUCCESS) ? false : true;
                    if ((8 * card + axis) == BCARD.AXIS_QTY_ALL - 1) break;//0927
				}
			}
		}
		//更新轴运动状态
		public static void GetAxisHomeStatuw()
		{
			for (TCard card = 0; card < BCARD.CARD_QTY; card++)
			{
				for (TAxis axis = 0; axis < 8; axis++)
				{
                    CARDSTS.HomeFinish[axis + 8 * card] = CtrlCardSR.AxisParams[axis + 8 * card].ax.HomeFinish ? true : false;
                    CARDSTS.HomeFail[axis + 8 * card] = CtrlCardSR.AxisParams[axis + 8 * card].ax.HomeFail ? true : false;
                    CARDSTS.Homing[axis + 8 * card] = CtrlCardSR.AxisParams[axis + 8 * card].ax.Homing ? true : false ;
                    if ((8 * card + axis) == BCARD.AXIS_QTY_ALL - 1) break;//0927
				}
			}
		}
		//读出编码器位置
		public static TReturn ReadAxisPosEnc()
		{
			return CtrlCardSR.ReadAxisPosEnc();
		}

		//读取逻辑位置
		public static TReturn ReadAxisPosLogic()
		{
			return CtrlCardSR.ReadAxisPosLogic();
		}

        public static TReturn AxisHome(TCard card, TAxis[] axis)
		{
			return CtrlCardSR.SR_GoHome(card, axis);
		}
        public static TReturn AxisHome(TCard card, TAxis axis)
		{
			return CtrlCardSR.SR_GoHome(card, axis);
		}
		//停止单轴
		public static TReturn StopAxis(TCard card, TAxis axis, int option = 0)
		{//card start with 0
			//axis start with 1
			//success return RET_SUCCESS;
			return CtrlCardSR.SR_AxisStop(card, axis);
		}
		//直接停止不等待
		public static TReturn StopAllAxisNoWait(TCard card)
		{
			return CtrlCardSR.SR_AllAxisStopNoWait (card);

		}
		//停止单卡
		public static TReturn StopAllAxis(TCard card)
		{
			StopAllAxisNoWait(card);
			return CtrlCardSR.SR_AllAxisStop(card);
		}
		//<连续运动> 有最大距离限制
		public static TReturn AxisContinueMoveWithLimit(TCard CardNo, TAxis AxisNo, TSpeed spd, TMode dir)
		{
			TPusle dist = (dir == BAX.DIR_POSITIVE) ? 99999 : -99999;
			return  CtrlCardSR.SR_RelativeMove(CardNo, AxisNo, dist, spd);
		}
		//<连续运动> 没有距离限制
		public static TReturn AxisContinueMove(TCard CardNo, TAxis AxisNo, TSpeed spd, TMode dir)
		{
			return  CtrlCardSR.SR_ContinueMove(CardNo, AxisNo, spd, dir);
		}
		//<轴点动/相对运动>
		public static TReturn AxisPMoveRelative(TCard CardNo, TAxis AxisNo, TPusle dist, TSpeed spd)
		{
			return CtrlCardSR.SR_RelativeMove(CardNo, AxisNo, dist, spd);
		}

		//<轴点动/绝对运动>
		public static TReturn AxisPMoveAbsolute(TCard CardNo, TAxis AxisNo, TPusle pos, TSpeed spd)
		{
			return CtrlCardSR.SR_AbsoluteMove(CardNo, AxisNo, pos, spd);
		}

		//单轴绝对定位，以及等待运动停止
		public static TReturn AxisPMoveAbsoluteToStop(TCard card, TAxis axis, double postion, double vel, double TimeOut = 100)
		{//success return RET_SUCCESS
			//fail return RET_FAIL
			int iResult;
			int nPosition = (int)(postion);
			iResult = CtrlCardSR.SR_AbsoluteMove(card, axis, nPosition, vel);
			if (iResult != 1)
			{
				return RET_FAIL;
			}
			DateTime axisStartTime = DateTime.Now;
			Thread.Sleep(10);
			while (true )
			{
				iResult = CtrlCardSR.SR_IsAxisStop(card, axis);
				if (iResult == RET_SUCCESS)
				{
					return RET_SUCCESS;
				}
				if ((DateTime.Now - axisStartTime).TotalSeconds > TimeOut)
				{
					return RET_FAIL;
				}
				Thread.Sleep(10);
			}
		}
		//双轴绝对定位，以及等待运动停止
		public static TReturn AxisPPMoveAbsoluteToStop(TCard card1, TCard card2, TAxis axis1, TAxis axis2,
			TPusle pos1, TPusle pos2, TSpeed vel1, TSpeed vel2, double TimeOut = 20)
		{//success return RET_SUCCESS
			//超时 return RET_FAIL
			int iResult;
			int iResult2;
			iResult = CtrlCardSR.SR_AbsoluteMove(card1, axis1, pos1, vel1);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}
			iResult = CtrlCardSR.SR_AbsoluteMove(card2, axis2, pos2, vel2);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}

			DateTime axisStartTime = DateTime.Now;
			Thread.Sleep(10);
			while (canRun())
			{
				iResult = CtrlCardSR.SR_IsAxisStop(card1, axis1);
				iResult2 = CtrlCardSR.SR_IsAxisStop(card2, axis2);
				if (iResult == RET_SUCCESS && iResult2 == RET_SUCCESS)
				{
					return RET_SUCCESS;
				}
				if ((DateTime.Now - axisStartTime).TotalSeconds > TimeOut)
				{
					return RET_FAIL;
				}
				Thread.Sleep(10);
			}
			return RET_FAIL;
		}

		public static bool bIOEx = false;

		public static TReturn UpdateCardStatus()
		{
			TReturn ret=RET_SUCCESS;
			ret =ReadAllInput();//
			if(ret!=RET_SUCCESS) 
			{
				ret=RET_FAIL ;
			}
			ret = UpdateStatus();//
			if(ret!=RET_SUCCESS) 
			{
				ret=RET_FAIL ;
			}

			return ret;
		}

		// 读取全部IO输入，包括输入，输出，轴限位、原点、轴报警
		public static TReturn ReadAllInput(TMode portNo = 0)
		{
			return CtrlCardSR.ReadAllInput();
		}
		//更新轴状态，放在ReadAllInput后
		public static TReturn UpdateStatus()
		{
			for (int i = 0; i < CARDSTS.IO.IOInput.Length; i++)
			{
				CARDSTS.IO.IOInput[i].bState = CARDSTS.InputRead[BCARD.POINT_QTY_SINGLE * CARDSTS.IO.IOInput[i].IOcard + CARDSTS.IO.IOInput[i].IOpoint];
				if(CARDSTS.IO.IOInput[i].bLogicHigh) //高电平有效时，信号反向
				{
					CARDSTS.IO.IOInput[i].bState=!CARDSTS.IO.IOInput[i].bState;
				}
				CARDSTS.IO.IOInput[i].bStatePulseUp = CARDSTS.InputPulseUp[BCARD.POINT_QTY_SINGLE * CARDSTS.IO.IOInput[i].IOcard + CARDSTS.IO.IOInput[i].IOpoint];
				CARDSTS.IO.IOInput[i].bStatePulseDown = CARDSTS.InputPulseDown[BCARD.POINT_QTY_SINGLE * CARDSTS.IO.IOInput[i].IOcard + CARDSTS.IO.IOInput[i].IOpoint];
			}
			for (int i = 0; i < CARDSTS.IO.IOOutput.Length; i++)
			{
				CARDSTS.IO.IOOutput[i].bState = CARDSTS.OutputRead[BCARD.POINT_QTY_SINGLE * CARDSTS.IO.IOOutput[i].IOcard + CARDSTS.IO.IOOutput[i].IOpoint];
			}
			CheckIOconfig();
			GetAxisMoveStatus();//轴运动状态
            GetAxisHomeStatuw();//轴回原状态1004
            CtrlCardSR.UpdateAxises();//
			CtrlCard.UpdateCtrlIO(0);//更新自定义控件状态

			return RET_SUCCESS;
		}
		//检查IO配置是否出现点位重复
		public static bool CheckIOconfig()
		{
			for (int i = 0; i < CARDSTS.IO.IOInput.Length; i++)
			{
				CARDSTS.IO.IOInput[i].bConfigRepeat = false;
			}
			for (int i = 0; i < CARDSTS.IO.IOOutput.Length; i++)
			{
				CARDSTS.IO.IOOutput[i].bConfigRepeat = false;
			}

			bool bHasRepeat=false;
			for (int i = 0; i < CARDSTS.IO.IOInput.Length; i++)
			{
				if (CARDSTS.IO.IOInput[i].bConfigRepeat == false)
				{
					for (int j = i; j < CARDSTS.IO.IOInput.Length; j++)
					{
						if(i!=j)
						{
							if (CARDSTS.IO.IOInput[i].IOcard * BCARD.POINT_QTY_SINGLE + CARDSTS.IO.IOInput[i].IOpoint ==
								CARDSTS.IO.IOInput[j].IOcard * BCARD.POINT_QTY_SINGLE + CARDSTS.IO.IOInput[j].IOpoint)
									{
										CARDSTS.IO.IOInput[i].bConfigRepeat = CARDSTS.IO.IOInput[j].bConfigRepeat = true;
										bHasRepeat=true;
									}	
						}
					}
				}
			}
			for (int i = 0; i < CARDSTS.IO.IOOutput.Length; i++)
			{
				if (CARDSTS.IO.IOOutput[i].bConfigRepeat == false)
				{
					for (int j = i; j < CARDSTS.IO.IOOutput.Length; j++)
					{
						if(i!=j)
						{
							if (CARDSTS.IO.IOOutput[i].IOcard * BCARD.POINT_QTY_SINGLE + CARDSTS.IO.IOOutput[i].IOpoint ==
								CARDSTS.IO.IOOutput[j].IOcard * BCARD.POINT_QTY_SINGLE + CARDSTS.IO.IOOutput[j].IOpoint)
									{
										CARDSTS.IO.IOOutput[i].bConfigRepeat = CARDSTS.IO.IOOutput[j].bConfigRepeat = true;
										bHasRepeat=true;
									}	
						}
					}
				}
			}
			return bHasRepeat;
		}

		private static void UpdateStatusArray(bool[] arrDes, ref bool[] arrAssign)
		{
			arrAssign=arrDes;
			//for (int i = 0; i < arrAssign.Length ; i++)
			//{
			//    arrAssign[i]=arrDes[i];
			//}
		}
		//写单点输出，OutPoint：输出点，bOut：输出电平//card号自动运算, outPoint从0开始
		public static TReturn WriteOutput(TIOPoint OutPoint, TMode bOut)
		{
			return CtrlCardSR.WriteOutput(OutPoint, bOut);
		}

		//伺服报警点报警 //以卡号区分工站
		public static bool IsServerAlarm(TCard card)
		{
			//for (TCard card = 0; card < AX.CARD_QTY; card++)
			//{
			for (int i = 0; i < 5; i++)
			{
				if (CARDSTS.InServerAlarm[8 * card + i] == true)
				{
					return true;
				}
			}
			//}
			return false;
		}
		//伺服硬限位报警
		public static bool IsServerHLAlarm(TCard card)
		{
			//for (TCard card = 0; card < AX.CARD_QTY; card++)
			//{
			for (int i = 0; i < 5; i++)
			{
				if (CARDSTS.InLimitNegative[8 * card + i] == true)
				{
					return true;
				}
				if (CARDSTS.InLimitPositive[8 * card + i] == true)
				{
					return true;
				}
			}
			//}
			return false;
		}

		public static TPusle GetAxisPosLogic(TCard card, TAxis axis)
		{
			uint pClock;
			short sResult;
			double dPostValue;
			sResult = gts.mc.GT_GetAxisPrfPos(card, axis, out dPostValue, 1, out pClock);
			return (TPusle)dPostValue;
		}
		public static TPusle GetAxisPosEnc(TCard card, TAxis axis)
		{
			uint pClock;
			short sResult;
			double dPostValue;
			sResult = gts.mc.GT_GetAxisEncPos(card, axis, out dPostValue, 1, out pClock);
			return (TPusle)dPostValue;
		}

		#region OtherInterface
		public delegate void CustomIOHandle(object sender, CustomIOArgument e);
		public static event CustomIOHandle CustomIOEvent ;
		//主要用于更新自定义控件状态
		public static void UpdateCtrlIO(int a)
		{
			if (CustomIOEvent != null)
			{
				CustomIOArgument e = new CustomIOArgument(a);
				CustomIOEvent(null, e);
			}
		}
		//获取此卡号前共有多少输入IO
		private static TIOPoint GetCardPointInput(TCard card)
		{
			switch(card)	
			{
				case 0:
				return 0;
				case 1:
				return BCARD.POINT_QTY_SINGLE;
				case 2:
				return (TIOPoint )(card * BCARD.POINT_QTY_SINGLE);
				default:
				return (TIOPoint)(card * BCARD.POINT_QTY_SINGLE);
			}
		}
		//获取此卡号前共有多少输出IO
		private static TIOPoint GetCardPointOutput(TCard card)
		{
			return GetCardPointInput(card);
		}
		//根据IO id控制输出 待验证
		public static TReturn WriteOutputP(TIOPoint point, TMode bOut)
		{
			TIOPoint OutPoint = GetCardPointOutput((TCard)(CARDSTS.IO.IOInput[point].IOcard));
			OutPoint += (TIOPoint)(CARDSTS.IO.IOInput[point].IOpoint);
			return CtrlCardSR.WriteOutput(OutPoint, bOut);			
		}
		//根据IO 卡号，点位输出
		public static TReturn WriteOutputP(TCard card, TIOPoint point, TMode bOut)
		{
			return CtrlCardSR.WriteOutput(card, point, bOut);			
		}
		//根据IO 卡号，点位输出
		public static TReturn WriteOutputPP(IOinfoOutput pt, TMode bOut)
		{
			return WriteOutputP((TCard )pt.IOcard, (TIOPoint) pt.IOpoint, bOut);			
		}
		//根据IO id读取输入 实时性较差
		public static bool ReadInputP(TIOPoint point)
		{
			TIOPoint inPoint = GetCardPointInput((TCard)(CARDSTS.IO.IOInput[point].IOcard));
			inPoint += (TIOPoint)(CARDSTS.IO.IOInput[point].IOpoint);
			return CARDSTS.InputRead[inPoint];	
		}
		#endregion

	}
}
