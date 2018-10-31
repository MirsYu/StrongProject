using System;
namespace StrongProject
{
	public class NewCtrlCard_ZWX8940 : NewCtrlCardBase
	{

		public NewCtrlCard_ZWX8940()
		{
			tag_AxisCount = 4;
			SR_FunInit();
			tag_Manufacturer = MotionCardManufacturer.MotionCardManufacturer_8940;
		}

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
			// SR_GoHome = _SR_GoHome;
			//  SR_GoOneHome = _SR_GoOneHome;
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
		/// 卡初始化
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axisCount"></param>
		/// <param name="configFileName"></param>
		/// <returns></returns>
		public short _SR_InitCard()
		{
			short returnValue;
			returnValue = (short)adt8940a1m.adt8940a1_initial();
			if (returnValue <= 0)
			{
				string str = "8940控制卡初始化失败!";


				if (returnValue == 0) str = str + "\r\n没有安装ADT卡";
				if (returnValue == -1) str = str + "没有安装端口驱动程序！";
				if (returnValue == -2) str = str + "PCI桥故障！";

				MessageBoxLog.Show(str);
				return -1;
			}

			return returnValue;
		}
		/// <summary>
		/// 退出板卡
		/// </summary>
		/// <returns></returns>
		public short _SR_CloseCard()
		{

			return 0;
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
			int returnValue = adt8940a1m.adt8940a1_set_pulse_mode(cardno, axis + 1, value, logic, dir_logic); //设定限位模式，设正负限位有效，低电平有效
			if (returnValue != 0)
			{
				return -1;
			}
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
			int returnValue = adt8940a1m.adt8940a1_set_limit_mode(cardno, axis + 1, v1, v2, logic); //设定限位模式，设正负限位有效，低电平有效
			if (returnValue != 0)
			{
				return -1;
			}
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
			short shrResult;
			shrResult = (short)adt8940a1m.adt8940a1_sudden_stop(card, axis + 1);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("adt8940a1_sudden_stop", shrResult);
				return shrResult;
			}
			return shrResult;
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
			shrResult = (short)adt8940a1m.adt8940a1_get_status(card, axis + 1, out axisStatus);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("adt8940a1_get_status", shrResult);
				return shrResult;
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
			short shrResult = 0;
			if (point.dblPonitSpeed == 0.0)
			{
				MessageBoxLog.Show(axisC.AxisName + "速度设置位0,急停请设置");
				return -1;
			}

			adt8940a1m.adt8940a1_set_startv(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblPonitStartSpeed);        //
			adt8940a1m.adt8940a1_set_speed(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblPonitSpeed);         //
			adt8940a1m.adt8940a1_set_acc(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblAcc);    //

			shrResult = (short)adt8940a1m.adt8940a1_symmetry_relative_move(axisC.CardNum, axisC.AxisNum + 1, (int)(point.dblPonitValue * axisC.Eucf), (int)point.dblPonitStartSpeed, (int)point.dblPonitSpeed, point.dblAccTime);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("adt8940a1_symmetry_relative_move", shrResult);
				return shrFail;
			}
			return shrResult;
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


			short shrResult = 0;
			if (point.dblPonitSpeed == 0.0)
			{
				MessageBoxLog.Show(axisC.AxisName + "速度设置位0,急停请设置");
				return -1;
			}

			adt8940a1m.adt8940a1_set_startv(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblPonitStartSpeed);        //
			adt8940a1m.adt8940a1_set_speed(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblPonitSpeed);         //
			adt8940a1m.adt8940a1_set_acc(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblAcc);    //

			shrResult = (short)adt8940a1m.adt8940a1_symmetry_absolute_move(axisC.CardNum, axisC.AxisNum + 1, (int)(point.dblPonitValue * axisC.Eucf), (int)point.dblPonitStartSpeed, (int)point.dblPonitSpeed, point.dblAccTime);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("adt8940a1_symmetry_relative_move", shrResult);
				return shrFail;
			}
			return shrResult;
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
			adt8940a1m.adt8940a1_set_startv(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblPonitStartSpeed);        //
			adt8940a1m.adt8940a1_set_speed(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblPonitSpeed);         //
			adt8940a1m.adt8940a1_set_acc(axisC.CardNum, axisC.AxisNum + 1, (int)point.dblAcc);    //


			return (short)adt8940a1m.adt8940a1_continue_move(axisC.CardNum, axisC.AxisNum + 1, dir);
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
			int intValue = 0;
			ushort ushortValue = 0;
			int iioBit = 0;
			bStatus = false;

			if (ioBit > 32)
			{
				ioBit = ioBit;
			}
			shrResult = (short)adt8940a1m.adt8940a1_read_bit(card, ioBit);
			if (shrResult < 0)
			{
				CommandResult("adt8940a1_read_bit", shrResult);
				return shrResult;
			}

			//else
			//{
			//    return 0;
			//}
			if (shrResult == 0)
			{
				bStatus = false;
			}
			else
			{
				bStatus = true;
			}

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
			outputIoStatus = false;

			shrResult = (short)adt8940a1m.adt8940a1_get_out(card, ioBit);
			if (shrResult == -1)
			{
				CommandResult("adt8940a1_get_out", shrResult);
				return shrFail;
			}
			if (shrResult == 1)
			{
				outputIoStatus = true;
			}
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
			shrResult = (short)adt8940a1m.adt8940a1_write_bit(card, ioBit, value);
			if (shrResult != 0)
			{
				CommandResult("adt8940a1_write_bit", shrResult);
				return shrResult;
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
			int posV = 0;
			shrResult = (short)adt8940a1m.adt8940a1_get_command_pos(card, (short)axis + 1, out posV);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("SR_GetPrfPos", shrResult);
				return -1;
			}
			if (posV != 0)
			{
				pos = posV;
			}
			pos = posV;
			return 0;
		}
		public short _SR_GetEncPos(short card, short axis, ref double pos)
		{
			short shrResult;
			int posV = 0;

			shrResult = (short)adt8940a1m.adt8940a1_get_actual_pos(card, (short)axis + 1, out posV);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("SR_GetEncPos", shrResult);
				return -1;
			}
			if (posV != 0)
			{
				pos = posV;
			}
			pos = posV;

			return 0;
		}
		/// <summary>
		/// 设置板卡规划位置
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="pos"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public short _SR_SetPrfPos(short card, short axis, double pos)
		{
			return (short)adt8940a1m.adt8940a1_set_command_pos((int)card, (int)(axis + 1), (int)pos);


		}
		/// <summary>
		/// 设置板卡规划位置
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="pos"></param>
		/// <returns></returns>
		public short _SR_SetEncPos(short card, short axis, double pos)
		{
			return (short)adt8940a1m.adt8940a1_set_actual_pos((int)card, (int)(axis + 1), (int)pos);

		}

		/// <summary>
		/// 设置单轴停止 减速停止
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <returns></returns>
		public short _SR_AxisStop(short card, short axis)
		{
			short shrResult;
			shrResult = (short)adt8940a1m.adt8940a1_sudden_stop(card, axis + 1);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("adt8940a1_dec_stop", shrResult);
				return shrFail;
			}
			return shrResult;
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

			shrResult = SR_GetInputBit(card, (short)(3 + axisNum * 6), out pValue);
			if (shrResult < 0)
			{
				return shrFail;
			}
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
			shrResult = SR_GetInputBit(card, (short)(1 + axisNum * 6), out pValue);
			if (shrResult < 0)
			{
				return shrFail;
			}
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
			shrResult = SR_GetInputBit(card, (short)(0 + axisNum * 6), out pValue);
			if (shrResult < 0)
			{
				return shrFail;
			}
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
			shrResult = SR_GetInputBit(card, (short)(2 + axisNum * 6), out pValue);
			if (shrResult < 0)
			{
				return shrFail;
			}
			return shrResult;

		}
	}
}
