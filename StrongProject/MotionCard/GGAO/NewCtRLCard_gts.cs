using System;
using System.Threading;
using System.Windows.Forms;

namespace StrongProject
{
	public class NewCtRLCard_gts : NewCtrlCardBase
	{
		public NewCtRLCard_gts(int axisCount)
		{
			tag_AxisCount = axisCount;
			SR_FunInit();
			tag_Manufacturer = MotionCardManufacturer.MotionCardManufacturer_gts;
		}

		private const int intCardCount = 3; //卡的数量 (两个8轴 1个四轴)
		private const int intAxisCountForCard = 8;//卡轴的数量
		private const int intExtendIoCount = 3;   //扩展IO模块数量 
		private const int intExtendStartId = 11; //扩展模块起始号


		public void SR_FunInit()
		{

			SR_InitCard = _SR_InitCard;
			SR_set_limit_mode = _SR_set_limit_mode;
			SR_set_pulse_mode = _SR_set_pulse_mode;
			SR_AxisEmgStop = _SR_AxisEmgStop;
			SR_GetAxisStatus = _SR_GetAxisStatus;
			SR_RelativeMove = _SR_RelativeMove;
			SR_AbsoluteMove = _SR_AbsoluteMove;
			SR_continue_move = _SR_continue_move;
			//SR_GoHome = _SR_GoHome;
			//SR_GoOneHome = _SR_GoOneHome;
			SR_GetInputBit = _SR_GetInputBit;
			SR_GetOutputBit = _SR_GetOutputBit;
			SR_SetOutputBit = _SR_SetOutputBit;
			SR_GetPrfPos = _SR_GetPrfPos;
			SR_GetEncPos = _SR_GetEncPos;
			SR_AxisStop = _SR_AxisStop;
			//SR_ClrStatus = _SR_ClrStatus;
			//SR_ClrAllStatus = _SR_ClrAllStatus;
			//SR_GetServoEnable = _SR_GetServoEnable;
			//SR_SetServoEnable = _SR_SetServoEnable;
			SR_SetPrfPos = _SR_SetPrfPos;
			SR_SetEncPos = _SR_SetEncPos;
			SR_GetAlarmInput = _SR_GetAlarmInput;
			SR_GetLimitPInput = _SR_GetLimitPInput;
			SR_GetLimitNInput = _SR_GetLimitNInput;
			SR_GetOriginInput = _SR_GetOriginInput;
		}

		/// <summary>
		/// 卡初始化    HUA
		/// <param name="card"></param>
		/// <param name="axisCount"></param>
		/// <param name="configFileName"></param>
		/// <returns></returns>
		public short _SR_InitCard()
		{
			if (InitCard(0, "\\GTS800.cfg") == false)
			{
				string str = "固高控制卡初始化失败!";
				MessageBoxLog.Show(str);
				return -1;
			}
			return 1;
		}

		private bool InitCard(short car, string str)
		{
			short sRtn;
			//打开控制器
			sRtn = mc.GT_Open(car, 0, 1);
			if (sRtn != shrGtsSuccess)
			{
				CommandResult("GT_GetSts", sRtn);
				return false;
			}
			//复位控制器
			sRtn = mc.GT_Reset(car);
			if (sRtn != shrGtsSuccess)
			{
				CommandResult("GT_GetSts", sRtn);
				return false;
			}
			//配置运动控制器
			sRtn = mc.GT_LoadConfig(car, Application.StartupPath + str);
			if (sRtn != shrGtsSuccess)
			{
				CommandResult("GT_GetSts", sRtn);
				return false;
			}
			//清除轴状态、使能轴 
			for (short axis = 0; axis < 6; axis++)
			{
				sRtn = mc.GT_ClrSts(car, (short)(axis + 1), 1);
				if (sRtn != shrGtsSuccess)
				{
					CommandResult("GT_GetSts", sRtn);
					return false;
				}
				sRtn = mc.GT_AxisOn(car, (short)(axis + 1));
				if (sRtn != shrGtsSuccess)
				{
					CommandResult("GT_GetSts", sRtn);
					return false;
				}
			}
			//清除指定轴的报警和限位
			sRtn = mc.GT_ClrSts(car, 1, 6);
			if (sRtn != shrGtsSuccess)
			{
				CommandResult("GT_GetSts", sRtn);
				return false;
			}
			return true;
		}

		public short _SR_SetPrfPos(short card, short axis, double pos)
		{
			short shrResult;

			shrResult = mc.GT_SetEncPos(card, axis, (int)pos);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SetEncPos", shrResult);
				return shrFail;
			}
			//设置规划器位
			shrResult = mc.GT_SetPrfPos(card, axis, (int)pos);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SetPrfPos", shrResult);
				return shrFail;
			}

			shrResult = mc.GT_SynchAxisPos(card, 1 << (axis - 1));
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SynchAxisPos", shrResult);
				return shrFail;
			}
			return shrResult;
		}

		public short _SR_SetEncPos(short card, short axis, double pos)
		{
			short shrResult;
			//设置编码器位置
			shrResult = mc.GT_SetEncPos(card, axis, (int)pos);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SetEncPos", shrResult);
				return shrFail;
			}
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SetPrfPos", shrResult);
				return shrFail;
			}
			shrResult = mc.GT_SynchAxisPos(card, 1 << (axis - 1));
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SynchAxisPos", shrResult);
				return shrFail;
			}
			return shrResult;
		}

		/// <summary>
		/// 设置输出脉冲的工作方式    默认模式：脉冲+方向，正逻辑脉冲，方向输出信号正逻辑
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="axis"></param>
		/// <param name="value"> 0：脉冲+脉冲方式		1：脉冲+方向方式</param>
		/// <param name="logic">0：	正逻辑脉冲			1：	负逻辑脉冲</param>
		/// <param name="dir_logic">	0：方向输出信号正逻辑	1：方向输出信号负逻辑</param>
		/// <returns>	0：正确					1：错误</returns>
		public int _SR_set_pulse_mode(Int32 cardno, Int32 axis, Int32 value, Int32 logic, Int32 dir_logic)
		{

			return 0;
		}

		/// <summary>
		///功能：设定正负方向限位输入nLMT信号的模式
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="axis"></param>
		/// <param name="value">  0：正限位有效			1：正限位无效</param>
		/// <param name="logic"> 0：负限位有效			1：负限位无效</param>
		/// <param name="dir_logic">	0：低电平有效			1：高电平有效</param>
		/// <returns>	0：正确					1：错误</returns>
		public short _SR_set_limit_mode(int cardno, int axis, int v1, int v2, int logic)
		{

			return 0;
		}

		/// <summary>
		/// 设置单轴紧急停止
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <returns></returns>
		public short _SR_AxisEmgStop(short card, short axis)
		{
			short sResult = mc.GT_Stop(card, 1 << (axis - 1), 1);
			//if (axis == 4)
			//{
			//    _SR_SetOutputBit(0, 15, 0);//R轴停止后开刹车
			//}
			return sResult;
			//return mc.GT_Stop(card, 1 << (axis - 1), 0);

		}

		/// <summary>
		/// 获取轴状态信息
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="axisStatus">获取的轴状态</param>
		/// <returns>返回0与非0   0代表轴停止， 非0轴在运动中</returns>
		public short _SR_GetAxisStatus(short card, short axis, out int axisStatus)
		{
			short shrResult;
			uint pClock;

			shrResult = mc.GT_GetSts(card, axis, out axisStatus, 1, out pClock);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetSts", shrResult);
				return shrFail;
			}
			if ((axisStatus & 0x400) < 1)
			{
				axisStatus = 0;
				//if (card == 0 && axis == 4)
				//{
				//    _SR_SetOutputBit(0, 15, 0);//R轴停止后开刹车
				//}
			}

			return shrResult;
		}

		/// <summary>
		/// 单轴相对运动
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="postion">目标点位</param>
		/// <param name="speed">速度</param>
		/// <returns></returns>
		public short _SR_RelativeMove(AxisConfig axisC, PointModule point)
		{
			short card = axisC.CardNum;
			short axis = axisC.AxisNum;
			int postion = (int)(point.dblPonitValue * axisC.Eucf);
			//if (axis == 4)
			//{
			//    _SR_SetOutputBit(0, 15, 1);//R轴动前关刹车
			//}            
			short sResult;
			int ipos;

			sResult = mc.GT_Stop(card, 1 << (axis - 1), 0);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}

			//设置为点位模试
			sResult = mc.GT_PrfTrap(card, axis);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			mc.TTrapPrm tprm;
			//读取点位运动参数           
			sResult = mc.GT_GetTrapPrm(card, axis, out tprm);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//tprm.acc = axisC.Acc;
			//tprm.dec = axisC.Acc;
			tprm.acc = (point.dblPonitSpeed * axisC.Eucf / 1000) / axisC.tag_accTime;
			tprm.dec = (point.dblPonitSpeed * axisC.Eucf / 1000) / axisC.tag_delTime;
			tprm.smoothTime = 0;
			//设置点位运动参数
			sResult = mc.GT_SetTrapPrm(card, axis, ref tprm);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_ClrSts(card, axis, 1);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_ClrSts(card, axis, 1);//1001 09
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_GetPos(card, axis, out ipos);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//设置目标位置
			postion = ipos + postion;
			sResult = mc.GT_SetPos(card, axis, postion);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//设置轴运动速度
			sResult = mc.GT_SetVel(card, axis, point.dblPonitSpeed * axisC.Eucf/ 1000);/// 1000
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//启动轴运动
			sResult = mc.GT_Update(card, 1 << (axis - 1));
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			return shrGtsSuccess;

		}

		//单点点位
		private short AxisPrfTrap_(AxisConfig af, int postion, double vel)
		{
			short sResult;
			int ipos;
			short card = af.CardNum;
			short axis = af.AxisNum;
			sResult = mc.GT_Stop(card, 1 << (axis - 1), 0);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}

			//设置为点位模试
			sResult = mc.GT_PrfTrap(card, axis);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			mc.TTrapPrm tprm;
			//读取点位运动参数           
			sResult = mc.GT_GetTrapPrm(card, axis, out tprm);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//tprm.acc = af.Acc;
			//tprm.dec = af.Acc;
			tprm.acc = (vel * af.Eucf / 1000) / af.tag_accTime;
			tprm.dec = (vel * af.Eucf / 1000) / af.tag_delTime;
			tprm.smoothTime = 0;
			//设置点位运动参数
			sResult = mc.GT_SetTrapPrm(card, axis, ref tprm);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_ClrSts(card, axis, 1);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_ClrSts(card, axis, 1);//1001 09
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_GetPos(card, axis, out ipos);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//设置目标位置
			postion = ipos + postion;
			sResult = mc.GT_SetPos(card, axis, postion);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//设置轴运动速度
			sResult = mc.GT_SetVel(card, axis, vel * af.Eucf / 1000);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//启动轴运动
			sResult = mc.GT_Update(card, 1 << (axis - 1));
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			return shrGtsSuccess;
		}

		/// <summary>
		/// 单轴绝对运动
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="postion">目标点位</param>
		/// <param name="speed">速度</param>
		/// <returns></returns>
		public short _SR_AbsoluteMove(AxisConfig axisC, PointModule point)
		{
			short card = axisC.CardNum;
			short axis = axisC.AxisNum;
			//if (axis == 4)
			//{
			//    _SR_SetOutputBit(0, 15, 1);//R轴动前关刹车
			//}            
			short sResult;
			int ipos;
			sResult = mc.GT_Stop(card, 1 << (axis - 1), 0);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}

			//设置为点位模试
			sResult = mc.GT_PrfTrap(card, axis);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			mc.TTrapPrm tprm;
			//读取点位运动参数           
			sResult = mc.GT_GetTrapPrm(card, axis, out tprm);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//tprm.acc = axisC.Acc;
			//tprm.dec = axisC.Acc;
			tprm.acc = (point.dblPonitSpeed * axisC.Eucf / 1000) / axisC.tag_accTime;
			tprm.dec = (point.dblPonitSpeed * axisC.Eucf / 1000) / axisC.tag_delTime;
			tprm.smoothTime = 0;
			//设置点位运动参数
			sResult = mc.GT_SetTrapPrm(card, axis, ref tprm);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_ClrSts(card, axis, 1);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_ClrSts(card, axis, 1);//1001 09
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			sResult = mc.GT_GetPos(card, axis, out ipos);
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//设置目标位置
			//ipos = ipos + postion;
			sResult = mc.GT_SetPos(card, axis, (int)(point.dblPonitValue * axisC.Eucf));
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//设置轴运动速度
			sResult = mc.GT_SetVel(card, axis, point.dblPonitSpeed * axisC.Eucf / 1000);//
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			//启动轴运动
			sResult = mc.GT_Update(card, 1 << (axis - 1));
			if (sResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfTrap", sResult);
				return shrFail;
			}
			return shrGtsSuccess;
		}

		/// <summary>
		/// 单轴连续运动
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="postion">目标点位</param>
		/// <param name="speed">速度</param>
		/// <returns></returns>
		public short _SR_continue_move(AxisConfig axisC, PointModule point, int dir)
		{

			short shrResult;
			mc.TJogPrm jog;
			//if (axisC.AxisNum == 4)
			//{
			//    _SR_SetOutputBit(0, 15, 1);//R轴动前关刹车
			//}
			shrResult = mc.GT_PrfJog(axisC.CardNum, axisC.AxisNum);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_PrfJog", shrResult);
				return shrFail;
			}
			shrResult = mc.GT_GetJogPrm(axisC.CardNum, (short)(axisC.AxisNum), out jog);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetJogPrm", shrResult);
				return shrFail;
			}
			//jog.acc = point.dblAcc;
			//jog.dec = point.dblDec;

			jog.acc = (point.dblPonitSpeed * axisC.Eucf / 1000) / axisC.tag_accTime;
			jog.dec = (point.dblPonitSpeed * axisC.Eucf / 1000) / axisC.tag_delTime;

			jog.smooth = 0;
			shrResult = mc.GT_SetJogPrm(axisC.CardNum, (short)(axisC.AxisNum), ref jog);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SetJogPrm", shrResult);
				return shrFail;
			}
			if (dir == 0)
				shrResult = mc.GT_SetVel(axisC.CardNum, (short)(axisC.AxisNum), (point.dblPonitSpeed * axisC.Eucf / 1000));
			else
				shrResult = mc.GT_SetVel(axisC.CardNum, (short)(axisC.AxisNum), -(point.dblPonitSpeed * axisC.Eucf / 1000));


			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SetVel", shrResult);
				return shrFail;
			}
			shrResult = mc.GT_ClrSts(axisC.CardNum, (short)(axisC.AxisNum), 1);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_ClrSts", shrResult);
				return shrFail;
			}
			shrResult = mc.GT_Update(axisC.CardNum, 1 << (axisC.AxisNum - 1));
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_Update", shrResult);
				return shrFail;
			}
			return 0;
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

			shrResult = mc.GT_GetDi(card, mc.MC_GPI, out pValue);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetDi", shrResult);
				return shrFail;
			}
			bStatus = (pValue & (1 << ioBit)) > 0 ? true : false;
			return 0;

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
			shrResult = mc.GT_SetDoBit(card, mc.MC_GPO, (short)(ioBit + 1), value);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SetDoBit", shrResult);
				return shrFail;
			}
			return shrResult;
		}

		/// <summary>
		/// 读取板卡规划位置
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="pos"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public short _SR_GetPrfPos(short card, short axis, ref double pos)
		{
			short shrResult;
			uint pClock = 0;
			shrResult = mc.GT_GetAxisPrfPos(card, (short)axis, out pos, 1, out pClock);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetAxisEncPos", shrResult);
				return -1;
			}
			return 0;
		}

		public short _SR_GetEncPos(short card, short axis, ref double pos)
		{
			short shrResult;
			uint pClock = 0;
			shrResult = mc.GT_GetAxisEncPos(card, (short)axis, out pos, 1, out pClock);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetAxisEncPos", shrResult);
				return -1;
			}
			return 0;

		}

		/// <summary>
		/// 设置单轴停止 减速停止
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <returns></returns>
		public short _SR_AxisStop(short card, short axis)
		{
			short sResult = mc.GT_Stop(card, 1 << (axis - 1), 0);
			//if (axis == 4)
			//{
			//    _SR_SetOutputBit(0, 15, 0);//R轴停止后开刹车
			//}
			return sResult;
		}

		/// <summary>
		/// 获取单卡 报警输入状态 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public short _SR_GetAlarmInput(short card, short axisNum, out bool pValue)
		{
			short shrResult;
			int iValue = 0;
			pValue = false;
			shrResult = mc.GT_GetDi(card, mc.MC_ALARM, out iValue);

			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetDi", shrResult);
				return shrResult;
			}
			pValue = (iValue & (1 << (axisNum - 1))) <= 0 ? true : false;
			return shrResult;
		}

		/// <summary>
		/// 获取单卡 正极限输入状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public short _SR_GetLimitPInput(short card, short axisNum, out bool pValue)
		{
			short shrResult;
			int iValue = 0;
			pValue = false;
			shrResult = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out iValue);

			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetDi", shrResult);
				return shrResult;
			}
			pValue = (iValue & (1 << (axisNum - 1))) <= 0 ? true : false;
			return shrResult;
		}

		/// <summary>
		/// 获取单卡 负极限输入状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public short _SR_GetLimitNInput(short card, short axisNum, out bool pValue)
		{
			short shrResult;
			int iValue = 0;
			pValue = false;
			shrResult = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out iValue);

			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetDi", shrResult);
				return shrResult;
			}
			pValue = (iValue & (1 << (axisNum - 1))) <= 0 ? true : false;

			return shrResult;
		}

		/// <summary>
		/// 获取单卡 原点输入状态 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue">原点状态值，按位取</param>
		/// <returns></returns>
		public short _SR_GetOriginInput(short card, short axisNum, out bool pValue)
		{
			short shrResult;
			int iValue = 0;
			pValue = false;
			shrResult = mc.GT_GetDi(card, mc.MC_HOME, out iValue);

			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_GetDi", shrResult);
				return shrResult;
			}
			pValue = (iValue & (1 << (axisNum - 1))) > 0 ? true : false;

			return shrResult;

		}

		/// <summary>
		/// 寻找原点 多次来回寻找,如果向负方向找到返回0，向正方向找到放回1
		/// </summary>
		/// <param name="axisC"></param>
		/// <returns></returns>
		public short MutHomeFindHomeSinge(AxisConfig axisC1)
		{
			int stepPos = 45;
			int pos = 45;
			int i = 0;
			do
			{
				if (HomeFindHomeSinge(axisC1, stepPos) == 0)
				{
					if (stepPos < 0)
						return 0;
					else
					{
						return 1;
					}
				}
				stepPos = 0 - stepPos;
				if (stepPos < 0)
				{
					stepPos = stepPos - 45;
					pos = 0 - stepPos;

				}
				else
				{
					stepPos = stepPos + 45;
					pos = stepPos;
				}
				i++;
			}
			while (pos < axisC1.intFirstFindOriginDis && i < 100);
			return -1;
		}

		public short HomeFindLimit(AxisConfig axisC)
		{
			short shrResult = 0;
			int Isfined = 0;
			bool pIo = false;
			int AxisState = 0;
			adt8940a1m.adt8940a1_set_startv(axisC.CardNum, axisC.AxisNum + 1, (int)axisC.StartSpeed);        //
			adt8940a1m.adt8940a1_set_speed(axisC.CardNum, axisC.AxisNum + 1, (int)axisC.Speed);         //
			adt8940a1m.adt8940a1_set_acc(axisC.CardNum, axisC.AxisNum + 1, (int)axisC.Acc);    //
			int pos = (int)(axisC.intFirstFindOriginDis * axisC.Eucf);
			if (axisC.HomeDir == 1)
				pos = 0 - pos;


			if (axisC.HomeDir == 0)
			{
				_SR_GetLimitPInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
			}

			else
			{
				_SR_GetLimitNInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
			}
			if (axisC.tag_IoLimtPNHighEnable == 0)
			{
				if (!pIo)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 0;
				}
			}
			else
			{
				if (pIo)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 0;
				}
			}

			shrResult = (short)adt8940a1m.adt8940a1_symmetry_relative_move(axisC.CardNum, axisC.AxisNum + 1, pos, (int)axisC.StartSpeed, (int)axisC.HomeSpeedHight, (int)axisC.tag_accTime);
			if (shrResult == 1)
			{
				CommandResult("adt8940a1_symmetry_relative_move", shrResult);
				return 1;
			}



			Thread.Sleep(10);
			while (true)
			{
				if (axisC.HomeDir == 0)
				{
					_SR_GetLimitPInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
				}
				else
				{
					_SR_GetLimitNInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
				}
				if (axisC.tag_IoLimtPNHighEnable == 0)
				{
					if (!pIo)
					{
						_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						return 0;
					}
				}
				else
				{
					if (pIo)
					{
						_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						return 0;
					}
				}

				if (_SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 3;
				}
				Thread.Sleep(1);
			}
			_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			return 4;
		}

		/// <summary>
		/// 运行第二段距离
		/// </summary>
		/// <param name="axisC"></param>
		/// <returns></returns>
		public short HomeMoveTwoDis(AxisConfig axisC)
		{
			short shrResult = 0;
			int Isfined = 0;
			bool pIo = false;
			int AxisState = 0;
			adt8940a1m.adt8940a1_set_startv(axisC.CardNum, axisC.AxisNum + 1, (int)axisC.StartSpeed);        //
			adt8940a1m.adt8940a1_set_speed(axisC.CardNum, axisC.AxisNum + 1, (int)axisC.Speed);         //
			adt8940a1m.adt8940a1_set_acc(axisC.CardNum, axisC.AxisNum + 1, (int)axisC.Acc);    //
			int pos = (int)(axisC.intSecondFindOriginDis * axisC.Eucf);
			if (axisC.HomeDir == 0)
				pos = 0 - pos;
			shrResult = (short)adt8940a1m.adt8940a1_symmetry_relative_move(axisC.CardNum, axisC.AxisNum + 1, pos, (int)axisC.StartSpeed, (int)axisC.HomeSpeedHight, (int)axisC.tag_accTime);
			if (shrResult == 1)
			{
				CommandResult("adt8940a1_symmetry_relative_move", shrResult);
				return 1;
			}
			Thread.Sleep(10);
			while (true)
			{


				if (_SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 0;
				}
				Thread.Sleep(1);
			}
			_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			return 4;
		}

		public short HomeFindHomeIO(AxisConfig axisC)
		{
			short shrResult = 0;
			int Isfined = 0;
			bool pIo = false;
			int AxisState = 0;
			int pos = 0;
			if (axisC.HomeDir == 0)
				pos = 0 - pos;



			PointModule point = new PointModule();
			point.dblPonitSpeed = axisC.HomeSpeed;
			point.dblAcc = axisC.Acc;
			point.dblDec = axisC.Dec;
			point.dblDec = axisC.tag_accTime;
			point.dblPonitValue = pos;
			shrResult = _SR_RelativeMove(axisC, point);

			if (shrResult != 0)
			{
				CommandResult("_SR_RelativeMove", shrResult);
				return 1;
			}
			while (true)
			{
				_SR_GetOriginInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
				if (axisC.tag_homeIoHighLow)
				{
					if (pIo)
					{
						_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						shrResult = 0;
						break;
					}
				}
				else
				{
					if (!pIo)
					{
						_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						shrResult = 0;
						break;
					}
				}
				if (_SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 3;
				}
				Thread.Sleep(1);
			}

			_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			Thread.Sleep(700);
			shrResult = mc.GT_ZeroPos(axisC.CardNum, axisC.AxisNum, 1);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_ZeroPos", shrResult);
				return shrFail;
			}
			return shrResult;
		}

		public short _SR_GoHome(AxisConfig _acf)
		{
			if (HomeFindLimit(_acf) == 0)
			{
				Thread.Sleep(1000);
				if (HomeMoveTwoDis(_acf) == 0)
					return HomeFindHomeIO(_acf);
			}
			return -5;
		}

		/// <summary>
		/// 寻找原点,一次寻找pos1 ,
		/// </summary>
		/// <param name="axisC"></param>
		/// <returns></returns>
		public short HomeFindHomeSinge(AxisConfig axisC, int pos1)
		{
			short shrResult = 0;
			int Isfined = 0;
			bool pIo = false;
			int AxisState = 0;
			PointModule point = new PointModule();
			point.dblPonitSpeed = axisC.Speed;
			point.dblAcc = axisC.Acc;
			point.dblDec = axisC.Dec;
			point.dblDec = axisC.tag_accTime;
			point.dblPonitValue = pos1;
			shrResult = _SR_RelativeMove(axisC, point);
			if (shrResult != 0)
			{
				CommandResult("_SR_RelativeMove", shrResult);
				return 1;
			}
			Thread.Sleep(10);
			while (true)
			{

				_SR_GetOriginInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);

				if (!axisC.tag_homeIoHighLow)
				{
					if (!pIo)
					{
						_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						return 0;
					}
				}
				else
				{
					if (pIo)
					{
						_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						return 0;
					}
				}

				if (_SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 3;
				}
				Thread.Sleep(1);
			}
			_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			return 4;
		}

		public short HomeFindOneHomeIO(AxisConfig axisC)
		{
			short shrResult = 0;
			int Isfined = 0;
			bool pIo = false;
			int AxisState = 0;

			PointModule point = new PointModule();
			point.dblPonitSpeed = axisC.HomeSpeed;
			point.dblAcc = axisC.Acc;
			point.dblDec = axisC.Dec;
			point.dblDec = axisC.tag_accTime;
			point.dblPonitValue = axisC.intThreeFindOriginDis;
			shrResult = _SR_RelativeMove(axisC, point);

			if (shrResult != 0)
			{
				CommandResult("_SR_RelativeMove", shrResult);
				return 1;
			}
			while (true)
			{
				_SR_GetOriginInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
				if (!axisC.tag_homeIoHighLow)
				{
					if (pIo)
					{
						_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						shrResult = 0;
						break;
					}
				}
				else
				{
					if (!pIo)
					{
						_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						shrResult = 0;
						break;
					}
				}
				if (_SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 3;
				}
				Thread.Sleep(1);
			}

			_SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			Thread.Sleep(700);
			shrResult = mc.GT_ZeroPos(axisC.CardNum, axisC.AxisNum, 1);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_ZeroPos", shrResult);
				return shrFail;
			}
			return shrResult;
		}

		/// <summary>
		/// 单原点回原,多次寻找
		/// </summary>
		/// <param name="_acf"></param>
		/// <returns></returns>
		public short _SR_GoOneHome(AxisConfig _acf)
		{
			short ret = MutHomeFindHomeSinge(_acf);
			if (ret >= 0)
			{
				if (HomeMoveTwoDis(_acf) == 0)
				{
					return HomeFindOneHomeIO(_acf);
				}
			}
			return -5;
		}



	}
}