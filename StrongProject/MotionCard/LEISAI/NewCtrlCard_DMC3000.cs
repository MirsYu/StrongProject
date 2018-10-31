using System;
namespace StrongProject
{





	public class NewCtrlCard_DMC3000 : NewCtrlCardBase
	{
		public static ushort[] tag_cardids = new ushort[8];
		public static uint[] tag_cardtypes = new uint[8];
		public static int tag_isInit = 0;
		public static int tag_CardCount = 0;

		public NewCtrlCard_DMC3000(int axisCount, int index)
		{
			tag_AxisCount = axisCount;
			SR_FunInit();
			tag_Manufacturer = MotionCardManufacturer.MotionCardManufacturer_DMC3800 + index;
		}


		public void SR_FunInit()
		{

			SR_InitCard = _SR_InitCard;
			SR_set_io_mode = _SR_set_io_mode;
			SR_set_limit_mode = _SR_set_limit_mode;
			SR_set_pulse_mode = _SR_set_pulse_mode;
			SR_AxisEmgStop = _SR_AxisEmgStop;
			SR_GetAxisStatus = _SR_GetAxisStatus;
			SR_RelativeMove = _SR_RelativeMove;
			SR_AbsoluteMove = _SR_AbsoluteMove;
			SR_LineMulticoorMove = _SR_LineMulticoorMove;
			SR_continue_move = _SR_continue_move;
			// SR_GoHome = _SR_GoHome;
			//   SR_GoOneHome = _SR_GoOneHome;
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
			SR_set_softlimit = _SR_set_softlimit;

		}
		/// <summary>
		/// 卡初始化
		/// </summary>
		/// <param name="card"></param>4
		/// <param name="axisCount"></param>
		/// <param name="configFileName"></param>
		/// <returns></returns>
		public short _SR_InitCard()
		{
			ushort _num = 0;
			if (tag_isInit == 0)
			{
				short returnValue = LTDMC.dmc_board_init();//获取卡数量
				tag_isInit = 1;
				tag_CardCount = returnValue;
				if (returnValue <= 0 || returnValue > 8)
				{
					string str = "DMC3" + tag_AxisCount + "00控制卡初始化失败!";
					if (returnValue == 0) str = str + "\r\n没有安装雷赛卡";
					if (returnValue == -1) str = str + "没有安装端口驱动程序！";
					if (returnValue == -2) str = str + "PCI桥故障！";

					MessageBoxLog.Show(str);
					return -1;
				}
			}
			short res = LTDMC.dmc_get_CardInfList(ref _num, tag_cardtypes, tag_cardids);
			if (res != 0)
			{
				MessageBoxLog.Show("获取卡信息失败!");
			}
			short i = 0;
			short retCount = 0;
			while (i < _num)
			{
				short j = 0;
				uint TotalAxis = 0;
				LTDMC.dmc_get_total_axes(tag_cardids[i], ref TotalAxis);
				if (TotalAxis == tag_AxisCount)
				{
					retCount++;
				}
				while (j < TotalAxis)
				{
					_SR_SetEncPos(i, j, 0);

					j++;
				}
				i++;
			}
			return retCount;
		}

		public short _SR_Close()
		{
			short returnValue = 0;
			if (tag_isInit == 1)
			{
				returnValue = (short)Dmc1000.d1000_board_close();//获取卡数量
				tag_isInit = 0;
			}
			return 0;
		}
		/// <summary>
		/// 设定正负方向限位输入nLMT信号的模式
		/*功能：
        参数：
        cardno	    卡号
        axis		轴号(1-4)
        v1          0：正限位有效			1：正限位无效
        v2          0：负限位有效			1：负限位无效
        logic       0：低电平有效			1：高电平有效
        返回值			0：正确					1：错误 
        默认模式为：正限位有效，负限位有效，低电平有效
        *****************************************************/
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="axis"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <param name="logic"></param>
		/// <returns></returns>
		public short _SR_set_limit_mode(int cardno, int axis, int v1, int v2, int logic)
		{

			/*功 能：设置 EL 限位信号
                参 数：CardNo 控制卡卡号
                axis 指定轴号，取值范围：DMC3410：0~3，DMC3800：0~7，DMC3600：0~5
                                    DMC3400A：0~3
                 el_enable EL 信号的使能状态：
                                        0：正负限位禁止
                                        1：正负限位允许
                                        2：正限位禁止、负限位允许（DMC3800/3600/3400A 专用）
                                        3：正限位允许、负限位禁止（DMC3800/3600/3400A 专用）
                el_logic EL 信号的有效电平：
                                        0：正负限位低电平有效
                                        1：正负限位高电平有效
                                        2：正限位低有效，负限位高有效（DMC3800/3600/3400A 专用）
                                        3：正限位高有效，负限位低有效（DMC3800/3600/3400A 专用）
                el_mode EL 制动方式：
                                        0：正负限位立即停止
                                        1：正负限位减速停止
                                        2：正限位立即停止，负限位减速停止（DMC3800/3600/3400A 专
                                        用）
                                        3：正限位减速停止，负限位立即停止（DMC3800/3600/3400A 专
                                        用）
                                           返回值：错误代码*/
			ushort el_enable = 0;
			ushort el_logic = 0;
			ushort el_mode = 0;
			if (v1 == 1 && v2 == 1)
			{
				el_enable = 0;
			}
			else
				if (v1 == 0 && v2 == 0)
			{
				el_enable = 1;
			}
			else
					if (v1 == 1 && v2 == 0)
			{
				el_enable = 2;
			}
			else
						if (v1 == 0 && v2 == 1)
			{
				el_enable = 3;
			}
			if (logic == 0)
			{
				el_logic = 1;
			}
			else
			{
				el_logic = 0;
			}
			ushort CArdNo = (ushort)cardno;
			ushort Axis = (ushort)axis;

			if (tag_CardCount < 1 || cardno >= tag_CardCount)
			{
				return -1;
			}

			short returnValue = LTDMC.dmc_set_el_mode((ushort)tag_cardids[cardno], (ushort)Axis, el_enable, el_logic, 0); //设定限位模式，设正负限位有效，低电平有效
			if (returnValue != 0)
			{
				return -1;
			}
			return 0;
		}


		/// <summary>
		///设定输入输出
		/*参数： 
		v1		    0:前面8个点定义为输入 1:前面的8个点定义为输出
				
	    v2		    0:后面8个点定义为输入 1:后面的8个点定义为输出
				
		返回值		0:正确				  1:错误
				
		注：当IO点作为输出点用的时候且能同时读到输入状态
		*********************************************************************/
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		public short _SR_set_io_mode(int cardno, int v1, int v2)
		{
			/*   int returnValue = adt8960m.adt8960_set_io_mode(cardno, v1, v2);//设定输入输出

			   if (returnValue != 0)
			   {
				   return -1;
			   }
			 */
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
		public int _SR_set_pulse_mode(Int32 CardNo, Int32 axis, Int32 value, Int32 logic, Int32 dir_logic)
		{

			/*       参 数：CardNo 控制卡卡号
								   axis 指定轴号，取值范围：DMC3410：0~3，DMC3800：0~7，DMC3600：0~5
															 DMC3400A：0~3
													   outmode 脉冲输出方式选择，其值如表 8.1 所示
							   返回值：错误代码*/

			ushort outmode = 0;
			if (value == 0)
			{
				if (logic == 0)
				{
					outmode = 5;
				}
				else
				{
					outmode = 4;
				}
			}
			else
			{
				if ((logic == 0 && dir_logic == 0))//正脉冲逻辑，方向正
				{
					outmode = 0;
				}
				else if (logic == 1 && dir_logic == 0) //正脉冲逻辑，方向负
				{
					outmode = 2;
				}
				else if (logic == 0 && dir_logic == 1)//负方脉冲逻辑，方向正
				{
					outmode = 1;
				}
				else if (logic == 1 && dir_logic == 1)//负方脉冲逻辑，方向负
				{
					outmode = 3;
				}
			}
			if (tag_CardCount < 1 || CardNo >= tag_CardCount)
			{
				return -1;
			}
			int returnValue = LTDMC.dmc_set_pulse_outmode((ushort)tag_cardids[CardNo], (ushort)axis, outmode); //设定限位模式，设正负限位有效，低电平有效
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
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			shrResult = (short)LTDMC.dmc_stop((ushort)tag_cardids[card], (ushort)axis, 1);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("dmc_stop", shrResult);
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
			short shrResult = 0;
			axisStatus = 0;
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			short staus = (short)LTDMC.dmc_check_done((ushort)tag_cardids[card], (ushort)axis);
			if (staus == 1)
			{
				axisStatus = 0;
			}
			else

			{
				axisStatus = 1;
			}


			return shrResult;
		}
		/// <summary>
		/// 直线查补运动
		/// </summary>
		/// <param name="card"></param>
		/// <param name="postion">目标点位</param>
		/// <param name="crd">坐标系</param>
		/// <param name="posi_mode">运动模式，0:相对坐标模式，1:绝对坐标模式</param>
		/// <returns></returns>
		public short _SR_LineMulticoorMove(AxisConfig[] axisC, PointModule[] point, short crd, short posi_mode)
		{
			int i = 0;
			short cardId = 0;
			if (axisC.Length == 0)
			{
				return 0;
			}
			while (i < axisC.Length - 1)
			{
				if (axisC[i].CardNum != axisC[i + 1].CardNum)
				{
					return -1;
				}
				i++;
			}
			i = 0;
			ushort[] axis = new ushort[axisC.Length];
			int[] pos = new int[axisC.Length];
			double Min_Vel = 0;
			double Max_Vel = 0;
			double Tacc = 0;
			double Tdec = 0;
			double Stop_Vel = 0;
			while (i < point.Length)
			{
				Min_Vel = Min_Vel + point[i].dblPonitStartSpeed;
				Max_Vel = Max_Vel + point[i].dblPonitSpeed;
				Stop_Vel = Stop_Vel + point[i].dblPonitSpeed;
				Tacc = Tacc + point[i].dblAccTime;
				Tdec = Tdec + point[i].dblDecTime;
				axis[i] = (ushort)axisC[i].AxisNum;
				pos[i] = (int)(point[i].dblPonitValue * axisC[i].Eucf);

				i++;
			}
			Min_Vel = Min_Vel / i;
			Max_Vel = Max_Vel / i;
			Stop_Vel = Stop_Vel / i;
			Tacc = Tacc / i;
			Tdec = Tdec / i;






			LTDMC.dmc_set_vector_profile_multicoor(tag_cardids[(int)axisC[0].CardNum], (ushort)crd, Min_Vel, Max_Vel, Tacc, Tdec, Stop_Vel);
			return LTDMC.dmc_line_multicoor(tag_cardids[(int)axisC[0].CardNum], (ushort)crd, (ushort)axisC.Length, axis, pos, (ushort)posi_mode);

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

			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}

			LTDMC.dmc_set_s_profile((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, 0, point.db_S_Time);//设置S段时间（0-0.05s)
			LTDMC.dmc_set_profile((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, axisC.StartSpeed, point.dblPonitSpeed, point.db_StopSpeed, point.dblAccTime, point.dblDecTime);//设置起始速度、运行速度、停止速度、加速时间、减速时间
			shrResult = LTDMC.dmc_pmove((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, (int)(point.dblPonitValue * axisC.Eucf), 0);//定长运动


			if (shrResult != shrGtsSuccess)
			{
				CommandResult("_SR_RelativeMove Result：" + shrResult, shrResult);
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

			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			LTDMC.dmc_set_s_profile((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, 0, point.db_S_Time);//设置S段时间（0-0.05s)
			LTDMC.dmc_set_profile((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, axisC.StartSpeed, point.dblPonitSpeed, point.db_StopSpeed, point.dblAccTime, point.dblDecTime);//设置起始速度、运行速度、停止速度、加速时间、减速时间
			shrResult = LTDMC.dmc_pmove((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, (int)(point.dblPonitValue * axisC.Eucf), 1);//定长运动


			if (shrResult != shrGtsSuccess)
			{
				CommandResult("_SR_RelativeMove", shrResult);
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
			if (dir == 0)
				dir = 1;
			else
				dir = 0;
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			LTDMC.dmc_set_s_profile((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, 0, point.db_S_Time);//设置S段时间（0-0.05s)
			LTDMC.dmc_set_profile((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, axisC.StartSpeed, point.dblPonitSpeed, point.db_StopSpeed, point.dblAccTime, point.dblDecTime);//设置起始速度、运行速度、停止速度、加速时间、减速时间
			short ret = LTDMC.dmc_vmove((ushort)tag_cardids[axisC.CardNum], (ushort)axisC.AxisNum, (ushort)dir);//连续运动
			return ret;
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
			bStatus = false;
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			uint IOBool = LTDMC.dmc_read_inport((ushort)tag_cardids[card], 0);

			if (((IOBool >> ioBit) & 1) == 1)
			{
				bStatus = true;
			}

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
			outputIoStatus = false;

			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			uint IOBool = LTDMC.dmc_read_outport((ushort)tag_cardids[card], 0);

			if (((IOBool >> ioBit) & 1) == 1)
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
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			shrResult = LTDMC.dmc_write_outbit((ushort)tag_cardids[card], (ushort)ioBit, (ushort)value);
			if (shrResult != 0)
			{
				CommandResult("dmc_write_outbit", shrResult);
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
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			pos = (double)LTDMC.dmc_get_position((ushort)tag_cardids[card], (ushort)axis);
			return 0;
		}
		public short _SR_GetEncPos(short card, short axis, ref double pos)
		{
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			pos = (double)LTDMC.dmc_get_position((ushort)tag_cardids[card], (ushort)axis);
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
			short ret = 0;
			//   short ret = (short)LTDMC.dmc_reset_target_position((ushort)tag_cardids[card], (ushort)axis, (int)pos, 0);
			return ret;
		}
		public short _SR_SetEncPos(short card, short axis, double pos)
		{
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			short ret = (short)LTDMC.dmc_set_position((ushort)tag_cardids[card], (ushort)axis, (int)pos);
			return ret;
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
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			shrResult = (short)LTDMC.dmc_stop((ushort)tag_cardids[card], (ushort)axis, 1);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("adt8960m_dec_stop", shrResult);
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
			pValue = false;
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			uint status = LTDMC.dmc_axis_io_status((ushort)tag_cardids[card], (ushort)axisNum);

			int bit = (int)Math.Pow(2, 0);
			if ((status & bit) == bit)
			{
				pValue = true;
			}
			else
			{
				pValue = false;
			}
			return 0;


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
			pValue = false;
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			uint status = LTDMC.dmc_axis_io_status((ushort)tag_cardids[card], (ushort)axisNum);

			if (card == 0 && axisNum == 0)
			{
				int i = 0;
			}

			int bit = (int)Math.Pow(2, 1);
			if ((status & bit) == bit)
			{
				pValue = true;
			}
			else
			{
				pValue = false;
			}
			return 0;
		}
		/// <summary>
		/// 获取单卡 负极限输入状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public short _SR_GetLimitNInput(short card, short axisNum, out bool pValue)
		{
			pValue = false;
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			uint status = LTDMC.dmc_axis_io_status((ushort)tag_cardids[card], (ushort)axisNum);
			if (card == 0 && axisNum == 0)
			{
				int i = 0;
			}
			int bit = (int)Math.Pow(2, 2);
			if ((status & bit) == bit)
			{
				pValue = true;
			}
			else
			{
				pValue = false;
			}
			return 0;
		}

		/// <summary>
		/// 获取单卡 原点输入状态 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue">原点状态值，按位取</param>
		/// <returns></returns>        
		public short _SR_GetOriginInput(short card, short axisNum, out bool pValue)
		{
			pValue = false;
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}
			uint status = LTDMC.dmc_axis_io_status((ushort)tag_cardids[card], (ushort)axisNum);
			if (card == 0 && axisNum == 0)
			{
				int i = 0;
			}
			int bit = (int)Math.Pow(2, 4);
			if ((status & bit) == bit)
			{
				pValue = true;
			}
			else
			{
				pValue = false;
			}
			return 0;

		}
		/// <summary>
		/// 获取急停 原点输入状态 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue">原点状态值，按位取</param>
		/// <returns></returns>        
		public short _SR_GetStopInput(short card, short axisNum, out bool pValue)
		{
			pValue = false;
			if (tag_CardCount < 1 || card >= tag_CardCount)
			{
				return -1;
			}

			uint status = LTDMC.dmc_axis_io_status((ushort)tag_cardids[card], (ushort)axisNum);
			int bit = (int)Math.Pow(2, 3);
			if ((status & bit) == bit)

			{
				pValue = true;
			}
			else
			{
				pValue = false;
			}
			return 0;

		}
		/// <summary>
		///  功 能:设置高速比较模式
		///  比较模式:
		/// 1)当选择模式 1 时，只有当前位置等于比较位置时，CMP 端口才输出有效电平
		///2)当选择模式 2 时，只要当前位置小于比较位置时，CMP 端口就一直保持有效电平 
		///3)当选择模式 3 时，只要当前位置大于比较位置时，CMP 端口就一直保持有效电平 
		///4)当选择模式 4 或 5 时，CMP 端口输出有效电平的时间通过 dmc_hcmp_set_config函数的 time 参数(脉冲宽度)设置 5)DMC5C00 后四轴不支持高速位置比较功能
		/// </summary>
		/// <param name="axis"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <param name="cmp_mode">比较模式:0:禁止(默认值)1:等于2:小于3:大于</param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_set_mode(AxisConfig axisC, short hcmp, short cmp_mode)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_hcmp_set_mode((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, (ushort)cmp_mode);
		}

		/// <summary>
		///  功 能:设置高速比较模式
		///  比较模式:
		/// 1)当选择模式 1 时，只有当前位置等于比较位置时，CMP 端口才输出有效电平
		///2)当选择模式 2 时，只要当前位置小于比较位置时，CMP 端口就一直保持有效电平 
		///3)当选择模式 3 时，只要当前位置大于比较位置时，CMP 端口就一直保持有效电平 
		///4)当选择模式 4 或 5 时，CMP 端口输出有效电平的时间通过 dmc_hcmp_set_config函数的 time 参数(脉冲宽度)设置 5)DMC5C00 后四轴不支持高速位置比较功能
		/// </summary>
		/// <param name="axis"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <param name="cmp_mode">比较模式:0:禁止(默认值)1:等于2:小于3:大于</param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_get_mode(AxisConfig axisC, short hcmp, ref ushort cmp_mode)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_hcmp_get_mode((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, ref cmp_mode);
		}
		/// <summary>
		/// 配置高速比较器 time参数(脉冲宽度)只对队列和线性比较模式起作用
		/// </summary>
		/// <param name="axisC"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <param name="cmp_source">比较位置源:0:指令位置计数器，1:编码器计数器</param>
		/// <param name="cmp_logic">有效电平:0:低电平，1:高电平</param>
		/// <param name="time">脉冲宽度，单位:us，取值范围:1us~20s</param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_set_config(AxisConfig axisC, short hcmp, short cmp_source, short cmp_logic, long time)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_hcmp_set_config((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, (ushort)axisC.AxisNum, (ushort)cmp_source, (ushort)cmp_logic, (Int32)time);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CardNo"></param>
		/// <param name="hcmp"></param>
		/// <param name="axis"></param>
		/// <param name="cmp_source"></param>
		/// <param name="cmp_logic"></param>
		/// <param name="time"></param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_get_config(AxisConfig axisC, ushort hcmp, ref ushort axis, ref ushort cmp_source, ref ushort cmp_logic, ref int time)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_hcmp_get_config((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, ref axis, ref cmp_source, ref cmp_logic, ref time);

		}
		/// <summary>
		/// 高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)
		/// </summary>
		/// <param name="axisC"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_clear_points(AxisConfig axisC, ushort hcmp)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_hcmp_clear_points((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp);

		}
		/// <summary>
		/// 添加/更新高速比较位置
		/// </summary>
		/// <param name="axisC"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <param name="cmp_pos">队列模式下:添加比较位置，单位:pulse</param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_add_point(AxisConfig axisC, short hcmp, int cmp_pos)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_hcmp_add_point((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, (int)cmp_pos);
		}
		/// <summary>
		/// 设置高速比较线性模式参数
		/// </summary>
		/// <param name="axisC"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <param name="Increment">位置增量值，单位:pulse(正值表示位置递增，负值表示位置递减)</param>
		/// <param name="Count">比较次数，取值范围:1~65535</param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_set_liner(AxisConfig axisC, short hcmp, int Increment, int Count)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_hcmp_set_liner((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, (int)Increment, Count);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="axisC"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <param name="Increment">位置增量值，单位:pulse(正值表示位置递增，负值表示位置递减)</param>
		/// <param name="Count">比较次数，取值范围:1~65535</param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_get_liner(AxisConfig axisC, short hcmp, ref int Increment, ref int Count)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}

			return LTDMC.dmc_hcmp_get_liner((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, ref Increment, ref Count);
		}
		/// <summary>
		/// 读取高速比较参数
		/// </summary>
		/// <param name="axisC"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <param name="remained_points">返回可添加比较点数</param>
		/// <param name="current_point">返回当前比较点位置，单位:pulse</param>
		/// <param name="runned_points">返回已比较点数</param>
		/// <returns></returns>
		public short _SR_dmc_hcmp_get_current_state(AxisConfig axisC, short hcmp, ref int remained_points, ref int current_point, ref int runned_points)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}

			return LTDMC.dmc_hcmp_get_current_state((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, ref remained_points, ref current_point, ref runned_points);
		}
		/// <summary>
		/// 控制指定 CMP 端口的输出
		/// </summary>
		/// <param name="axisC"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <param name="on_off">设置 CMP 端口电平，0:低电平，1:高电平</param>
		/// <returns></returns>
		public short _SR_dmc_write_cmp_pin(AxisConfig axisC, short hcmp, short on_off)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_write_cmp_pin((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp, (ushort)on_off);
		}
		/// <summary>
		/// 控制指定 CMP 端口的输出
		/// </summary>
		/// <param name="axisC"></param>
		/// <param name="hcmp">高速比较器，取值范围:0~3(对应硬件 CMP0~CMP3 端口)</param>
		/// <returns></returns>
		public short _SR_dmc_read_cmp_pin(AxisConfig axisC, short hcmp)
		{
			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_read_cmp_pin((ushort)tag_cardids[axisC.CardNum], (ushort)hcmp);
		}

		/// <summary>
		/// 设置软限位
		/// </summary>
		/// <param name="axisC"></param>
		/// <returns></returns>
		public short _SR_set_softlimit(AxisConfig axisC)
		{

			/*功 能：设置软限位
				  参 数：CardNo 控制卡卡号
			  axis 指定轴号，取值范围：DMC3410：0~3，DMC3800：0~7，DMC3600：0~5
			  DMC3400A：0~3
			  enable 使能状态，0：禁止，1：允许
			  source_sel 计数器选择，0：指令位置计数器，1：编码器计数器
			  SL_action 限位停止方式，0：减速停止，1：立即停止
			  N_limit 负限位位置，单位：pulse
			  P_limit 正限位位置，单位：pulse*/

			if (tag_CardCount < 1 || axisC.CardNum >= tag_CardCount)
			{
				return -1;
			}
			return LTDMC.dmc_set_softlimit((ushort)tag_cardids[axisC.CardNum],
				(ushort)axisC.AxisNum, (ushort)axisC.SoftLimitEnablel, 0, 1, (int)axisC.SoftLimitMinValue, (int)axisC.SoftLimitMaxValue);

		}


	}
}
