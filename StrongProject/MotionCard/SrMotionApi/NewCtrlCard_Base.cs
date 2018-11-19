using log4net;
using System;
using System.Threading;
namespace StrongProject
{
	public class NewCtrlCardBase
	{

		private static readonly ILog log = LogManager.GetLogger("NewCtrlCardBase.cs");
		public MotionCardManufacturer tag_Manufacturer;
		public int tag_AxisCount;
		public int[] tag_InIo = new int[128];
		public short shrFail = -1;
		public short shrGtsSuccess = 0;

		public NewCtrlCardBase()
		{

		}

		public short CommandResult(string command, short result)
		{
			UserControl_LogOut.OutLog(command, 0);
			return 0;
		}

		/// <summary>
		/// 获取卡厂商名称
		/// </summary>
		/// <param name="Manufacturer"></param>
		/// <returns></returns>
		public static string GetManufacturerName(int Manufacturer)
		{
			switch ((MotionCardManufacturer)Manufacturer)
			{

				case MotionCardManufacturer.MotionCardManufacturer_8940:
					return "8940";
					break;
				case MotionCardManufacturer.MotionCardManufacturer_8960m:
					return "8960m";

				case MotionCardManufacturer.MotionCardManufacturer_DMC3800:
					return "DMC3800";

				case MotionCardManufacturer.MotionCardManufacturer_DMC3600:
					return "DMC3600";
				case MotionCardManufacturer.MotionCardManufacturer_DMC3400:
					return "DMC3400";
				case MotionCardManufacturer.MotionCardManufacturer_DMC1000B:
					return "DMC1000B";
					break;
				case MotionCardManufacturer.MotionCardManufacturer_gts:
					return "GTS800";
					break;
				case MotionCardManufacturer.MotionCardManufacturer_gtsExt:
					return "GTS800Ext";
					break;
				case MotionCardManufacturer.MotionCardManufacturer_IO3224:
					return "IO3224 ";
					break;
			}
			return "设置卡错误\r\n";
		}

		/// <summary>
		/// 卡初始化
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axisCount"></param>
		/// <param name="configFileName"></param>
		/// <returns></returns>
		public delegate short delegate_SR_InitCard();
		public delegate_SR_InitCard SR_InitCard;

		/// <summary>
		/// 卡初始化
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axisCount"></param>
		/// <param name="configFileName"></param>
		/// <returns></returns>
		public delegate short delegate_SR_Close();
		public delegate_SR_Close SR_Close;

		/// <summary>
		/// 设置单轴紧急停止
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <returns></returns>
		public delegate short delegate_SR_AxisEmgStop(short card, short axis);
		public delegate_SR_AxisEmgStop SR_AxisEmgStop;

		/// <summary>
		/// 获取轴状态信息
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="axisStatus">获取的轴状态</param>
		/// <returns>返回0与非0   0代表轴停止， 非0轴在运动中</returns>
		public delegate short delegate_SR_GetAxisStatus(short card, short axis, out int axisStatus);
		public delegate_SR_GetAxisStatus SR_GetAxisStatus;

		/// <summary>
		/// 单轴相对运动
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="postion">目标点位</param>
		/// <param name="speed">速度</param>
		/// <returns></returns>
		public delegate short delegate_SR_RelativeMove(AxisConfig axisC, PointModule point);
		public delegate_SR_RelativeMove SR_RelativeMove;

		/// <summary>
		/// 单轴绝对运动
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="postion">目标点位</param>
		/// <param name="speed">速度</param>
		/// <returns></returns>
		public delegate short delegate_SR_AbsoluteMove(AxisConfig axisC, PointModule point);
		public delegate_SR_AbsoluteMove SR_AbsoluteMove;

		/// <summary>
		/// 直线插补运动
		/// </summary>
		/// <param name="card"></param>
		/// <param name="postion">目标点位</param>
		/// <param name="crd">坐标系</param>
		/// <param name="posi_mode">运动模式，0:相对坐标模式，1:绝对坐标模式</param>
		/// <returns></returns>
		public delegate short delegate_SR_LineMulticoorMove(AxisConfig[] axisC, PointModule[] point, short crd, short posi_mode);
		public delegate_SR_LineMulticoorMove SR_LineMulticoorMove;

		/// <summary>
		/// 单轴连续运动
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="postion">目标点位</param>
		/// <param name="speed">速度</param>
		/// <returns></returns>
		public delegate short delegate_SR_continue_move(AxisConfig axisC, PointModule point, int dir);
		public delegate_SR_continue_move SR_continue_move;

		/// <summary>
		/// 伺服回原函数
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="hightSpeed">回原高速</param>
		/// <param name="lowSpeed">回原低速</param>
		/// <param name="dir">回原方向 0正方向回原  非0 负方向</param>
		/// <returns>返回0表示回原成功，非0轴回原失败或函数异常</returns>
		public delegate short delegate_SR_GoHome(AxisConfig _ac);
		//public delegate_SR_GoHome SR_GoHome;

		/// <summary>
		/// 伺服回原函数
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="hightSpeed">回原高速</param>
		/// <param name="lowSpeed">回原低速</param>
		/// <param name="dir">回原方向 0正方向回原  非0 负方向</param>
		/// <returns>返回0表示回原成功，非0轴回原失败或函数异常</returns>
		public delegate short delegate_SR_GoOneHome(AxisConfig _ac);
		//public delegate_SR_GoOneHome SR_GoOneHome;

		/// <summary>
		/// 获取单点输入信号状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="ioBit">0开始， 0-15</param>
		/// <param name="bStatus"></param>
		/// <returns></returns>
		public delegate short delegate_SR_GetInputBit(short card, short ioBit, out bool bStatus);
		public delegate_SR_GetInputBit SR_GetInputBit;

		/// <summary>
		/// 获取单个输出IO状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="ioBit"></param>
		/// <param name="outputIoStatus"></param>
		/// <returns></returns>
		public delegate short delegate_SR_GetOutputBit(short card, short ioBit, out bool outputIoStatus);
		public delegate_SR_GetOutputBit SR_GetOutputBit;

		/// <summary>
		/// 设置单点 输出信号 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="ioBit">0开始， 0-15</param>
		/// <param name="value"></param>
		/// <returns></returns>
		public delegate short delegate_SR_SetOutputBit(short card, short ioBit, short value);
		public delegate_SR_SetOutputBit SR_SetOutputBit;

		/// <summary>
		/// 读取板卡规划位置
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="pos"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public delegate short delegate_SR_GetPrfPos(short card, short axis, ref double pos);
		public delegate_SR_GetPrfPos SR_GetPrfPos;

		/// <summary>
		/// 读取板卡编码器位置
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="pos"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public delegate short delegate_SR_GetEncPos(short card, short axis, ref double pos);
		public delegate_SR_GetEncPos SR_GetEncPos;



		public delegate short delegate_SR_SetPrfPos(short card, short axis, double pos);
		public delegate_SR_SetPrfPos SR_SetPrfPos;
		public delegate short delegate_SR_SetEncPos(short card, short axis, double pos);
		public delegate_SR_SetEncPos SR_SetEncPos;




		/// <summary>
		/// 设置单轴停止 减速停止
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <returns></returns>
		public delegate short delegate_SR_AxisStop(short card, short axis);
		public delegate_SR_AxisStop SR_AxisStop;

		/// <summary>
		///     清除各轴异常状态 暂时没找到
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="axisCount">轴数量，默认为1，清除单轴</param>
		/// <returns></returns>
		public delegate short delegate_SR_ClrStatus(short card, short axis, short axisCount = 1);
		public delegate_SR_ClrStatus SR_ClrStatus;

		/// <summary>
		/// 清除卡上所有轴状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axisCount">轴数量，默认为8</param>
		/// <returns></returns>
		public delegate short delegate_SR_ClrAllStatus(short card, short axisCount = 8);
		public delegate_SR_ClrAllStatus SR_ClrAllStatus;

		/// <summary>
		/// 轴使能 未找到
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="bEanble">使能状态 true 使能  false 使能关闭</param>
		/// <returns></returns>
		public delegate short delegate_SR_GetServoEnable(short card, short axis, out bool bEanble);
		public delegate_SR_GetServoEnable SR_GetServoEnable;

		/// <summary>
		/// 轴使能  未找到
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axis"></param>
		/// <param name="bEanble">使能状态 true 使能  false 使能关闭</param>
		/// <returns></returns>
		public delegate short delegate_SR_SetServoEnable(short card, short axis, bool bEanble);
		public delegate_SR_SetServoEnable SR_SetServoEnable;

		/// <summary>
		/// 获取单卡 报警输入状态 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public delegate short delegate_SR_GetAlarmInput(short card, short axisNum, out bool pValue);
		public delegate_SR_GetAlarmInput SR_GetAlarmInput;

		/// <summary>
		/// 获取单卡 正极限输入状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public delegate short delegate_SR_GetLimitPInput(short card, short axisNum, out bool pValue);
		public delegate_SR_GetLimitPInput SR_GetLimitPInput;

		/// <summary>
		/// 获取单卡 负极限输入状态
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue"></param>
		/// <returns></returns>
		public delegate short delegate_SR_GetLimitNInput(short card, short axisNum, out bool pValue);
		public delegate_SR_GetLimitNInput SR_GetLimitNInput;

		/// <summary>
		/// 获取单卡 原点输入状态 
		/// </summary>
		/// <param name="card"></param>
		/// <param name="pValue">原点状态值，按位取</param>
		/// <returns></returns>        
		public delegate short delegate_SR_GetOriginInput(short card, short axisNum, out bool pValue);
		public delegate_SR_GetOriginInput SR_GetOriginInput;



		/// <summary>
		/// 设定正负方向限位输入nLMT信号的模式
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="axis"></param>
		/// <param name="v1">轴号(1-4) 0：正限位有效			1：正限位无效 </param>
		/// <param name="v2"> 0：负限位有效			1：负限位无效</param>
		/// <param name="logic">0：低电平有效			1：高电平有效</param>
		/// <returns>0：正确					1：错误 </returns>
		public delegate short delegate_SR_set_limit_mode(int cardno, int axis, int v1, int v2, int logic);
		public delegate_SR_set_limit_mode SR_set_limit_mode;


		/// <summary>
		/// 设定输入输出
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="v1">0:前面8个点定义为输入 1:前面的8个点定义为输出</param>
		/// <param name="v2">0:后面8个点定义为输入 1:后面的8个点定义为输出</param>
		/// <returns>0:正确				  1:错误</returns>
		public delegate short delegate_SR_set_io_mode(int cardno, int v1, int v2);
		public delegate_SR_set_io_mode SR_set_io_mode;

		/// <summary>
		/// 设置输出脉冲的工作方式    默认模式：脉冲+方向，正逻辑脉冲，方向输出信号正逻辑
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="axis"></param>
		/// <param name="value"> 0：脉冲+脉冲方式		1：脉冲+方向方式</param>
		/// <param name="logic">0：	正逻辑脉冲			1：	负逻辑脉冲</param>
		/// <param name="dir_logic">	0：方向输出信号正逻辑	1：方向输出信号负逻辑</param>
		/// <returns>	0：正确					1：错误</returns>
		public delegate Int32 delegate_SR_set_pulse_mode(Int32 cardno, Int32 axis, Int32 value, Int32 logic, Int32 dir_logic);
		public delegate_SR_set_pulse_mode SR_set_pulse_mode;

		/// <summary>
		/// 设置输出脉冲的工作方式    默认模式：脉冲+方向，正逻辑脉冲，方向输出信号正逻辑
		/// </summary>
		/// <param name="cardno"></param>
		/// <param name="axis"></param>
		/// <param name="value"> 0：脉冲+脉冲方式		1：脉冲+方向方式</param>
		/// <param name="logic">0：	正逻辑脉冲			1：	负逻辑脉冲</param>
		/// <param name="dir_logic">	0：方向输出信号正逻辑	1：方向输出信号负逻辑</param>
		/// <returns>	0：正确					1：错误</returns>
		public delegate short delegate_SR_set_softlimit(AxisConfig axis);
		public delegate_SR_set_softlimit SR_set_softlimit;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="axisC"></param>
		/// <returns></returns>
		public short HomeFindLimit(AxisConfig axisC)
		{
			short shrResult = 0;
			int Isfined = 0;
			bool pIo = false;
			int AxisState = 0;
			PointModule point = new PointModule(true, false, 0, axisC.Speed, axisC.Acc, axisC.Dec, axisC.tag_accTime, axisC.tag_delTime, axisC.StartSpeed, axisC.tag_S_Time, axisC.tag_StopSpeed);

			if (axisC.HomeDir == 1) //回零方向，默认为0
			{
				point.dblPonitValue = 0 - axisC.intFirstFindOriginDis; //第一次找原点距离

			}
			else
			{
				point.dblPonitValue = axisC.intFirstFindOriginDis;
			}

			if (axisC.HomeDir == 0)
			{
				SR_GetLimitPInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo); //获取正限输入状态
			}
			else
			{
				SR_GetLimitNInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);//获取负限输入状态
			}

			if (axisC.tag_IoLimtPNHighEnable == 0) //正负限位高电平有效
			{
				if (!pIo)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 0;
				}
			}
			else
			{
				if (pIo)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 0;
				}
			}

			shrResult = SR_RelativeMove(axisC, point); //单轴的相对运动

			if (shrResult != 0)
			{
				CommandResult("_SR_AbsoluteMove", shrResult);
				return -1;
			}
			Thread.Sleep(10);

			while (true)
			{
				if (workBase.IsRestExit())
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -1;
				}

				if (axisC.HomeDir == 0)
				{
					SR_GetLimitPInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
				}

				else
				{
					SR_GetLimitNInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
				}
				if (axisC.tag_IoLimtPNHighEnable == 0)
				{
					if (!pIo)
					{
						SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						return 0;
					}
				}
				else
				{
					if (pIo)
					{
						SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						return 0;
					}
				}

				if (SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					if (axisC.HomeDir == 0)
					{
						SR_GetLimitPInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
					}

					else
					{
						SR_GetLimitNInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
					}
					if (axisC.tag_IoLimtPNHighEnable == 0)
					{
						if (!pIo)
						{
							SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
							return 0;
						}
					}
					else
					{
						if (pIo)
						{
							SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
							return 0;
						}
					}
					return 3;
				}
				//   Thread.Sleep(1);
			}
			SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
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
			PointModule point = new PointModule(true, false, 0, axisC.Speed, axisC.Acc, axisC.Dec, axisC.tag_accTime, axisC.tag_delTime, axisC.StartSpeed, axisC.tag_S_Time, axisC.tag_StopSpeed);

			point.dblPonitValue = axisC.intSecondFindOriginDis;
			if (axisC.HomeDir == 0)
				point.dblPonitValue = 0 - point.dblPonitValue;

			shrResult = SR_RelativeMove(axisC, point); //单轴的相对运动
			if (shrResult != 0)
			{
				return -1;
			}
			if (point.dblPonitValue == 0)
			{
				return -1;
			}
			Thread.Sleep(10);

			while (true)
			{
				if (workBase.IsRestExit())
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -1;
				}

				if (SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 0;
				}
				Thread.Sleep(1);
			}
			SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			return 4;
		}

		public short HomeFindHomeIO(AxisConfig axisC)
		{
			short shrResult = 0;
			int Isfined = 0;
			bool pIo = false;
			int AxisState = 0;

			PointModule point = new PointModule(true, false, 0, axisC.HomeSpeed, axisC.Acc, axisC.Dec, axisC.tag_accTime, axisC.tag_delTime, axisC.StartSpeed, axisC.tag_S_Time, axisC.tag_StopSpeed);
			point.dblPonitValue = axisC.intThreeFindOriginDis;

			if (axisC.HomeDir == 1) //回零方向，默认为0
			{
				point.dblPonitValue = 0 - point.dblPonitValue; ;
			}
			else
			{
				point.dblPonitValue = axisC.intFirstFindOriginDis;
			}

			shrResult = SR_RelativeMove(axisC, point);
			if (shrResult != 0)
			{
				return -1;
			}


			while (true)
			{
				if (workBase.IsRestExit())
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -1;
				}

				SR_GetOriginInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
				if (axisC.tag_homeIoHighLow) //原点是否高低电平有效
				{
					if (pIo)
					{
						SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						shrResult = 0;
						break;
					}
				}
				else
				{
					if (!pIo)
					{
						SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						shrResult = 0;
						break;
					}
				}
				if (SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 3;
				}
				Thread.Sleep(1);
			}

			SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			Thread.Sleep(1000);

			int countzeor = 30;


			while (countzeor >= 0)
			{
				if (workBase.IsRestExit())
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -1;
				}
				int status = 0;
				short r = SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out status);
				if (r != 0)
				{
					return -1;
				}
				if (status == 0)
				{
					break;
				}
			}

			while (countzeor >= 0)
			{
				double pos = 0;
				double Enc = 0;
				if (workBase.IsRestExit())
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -1;
				}
				Thread.Sleep(1000);
				SR_GetPrfPos(axisC.CardNum, (short)(axisC.AxisNum), ref pos);
				SR_GetEncPos(axisC.CardNum, (short)(axisC.AxisNum), ref Enc);
				SR_SetPrfPos(axisC.CardNum, (short)(axisC.AxisNum), 0);
				SR_SetEncPos(axisC.CardNum, (short)(axisC.AxisNum), 0);
				SR_GetPrfPos(axisC.CardNum, (short)(axisC.AxisNum), ref pos);
				SR_GetEncPos(axisC.CardNum, (short)(axisC.AxisNum), ref Enc);

				if (pos < 5 && pos > -5 && Enc < 5 && Enc > -5)
				{
					return 0;
				}

				countzeor--;
				Thread.Sleep(10);
			}
			if (countzeor >= 0)
			{
				return 0;
			}

			return -1;
		}

		/// <summary>
		/// 寻找限位 -有问题
		/// </summary>
		/// <param name="_acf"></param>
		/// <returns></returns>
		public short SR_GoHome(AxisConfig _acf)
		{
			if (HomeFindLimit(_acf) == 0)
			{
				if (workBase.IsRestExit())
				{
					return -1;
				}

				Thread.Sleep(1000);
				if (HomeMoveTwoDis(_acf) == 0)
				{
					if (workBase.IsRestExit())
					{
						return -1;
					}

					return HomeFindHomeIO(_acf);
				}
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
			PointModule point = new PointModule(true, false, 0, axisC.Speed, axisC.Acc, axisC.Dec, axisC.tag_accTime, axisC.tag_delTime, axisC.StartSpeed, axisC.tag_S_Time, axisC.tag_StopSpeed);

			point.dblPonitValue = pos1;
			shrResult = SR_RelativeMove(axisC, point);
			if (shrResult != 0)
			{
				return -1;
			}
			Thread.Sleep(10);
			while (true)
			{
				if (workBase.IsRestExit())
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -1;
				}

				SR_GetOriginInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);         //采集原点信号

				if (!axisC.tag_homeIoHighLow)                                               //如果信号位高电平
				{
					if (!pIo)
					{
						SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));              //停止轴
						return 0;
					}
				}
				else
				{
					if (pIo)
					{
						SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						return 0;
					}
				}

				if (SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -2;
				}
				if (AxisState == 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -3;
				}
				Thread.Sleep(1);
			}
			SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			return -4;
		}

		/// <summary>
		/// 寻找原点 多次来回寻找,如果向负方向找到返回0，向正方向找到放回1
		/// </summary>
		/// <param name="axisC"></param>
		/// <returns></returns>
		public short MutHomeFindHomeSinge(AxisConfig axisC1, int count)
		{
			int stepPos = (int)axisC1.intFirstFindOriginDis;

			int i = 0;



			if (HomeFindHomeSinge(axisC1, stepPos) == 0)
			{
				if (stepPos < 0)
					return 0;
				else
				{
					return 1;
				}
			}

			if (workBase.IsRestExit())
			{
				return -1;
			}

			if (HomeFindHomeSinge(axisC1, 0 - 2 * stepPos) == 0)
			{
				if (stepPos < 0)
					return 0;
				else
				{
					return 1;
				}
			}
			return -1;
		}





		public short HomeFindOneHomeIO(AxisConfig axisC)
		{
			short shrResult = 0;
			int Isfined = 0;
			bool pIo = false;
			int AxisState = 0;
			PointModule point = new PointModule(true, false, 0, axisC.HomeSpeed, axisC.Acc, axisC.Dec, axisC.tag_accTime, axisC.tag_delTime, axisC.StartSpeed, axisC.tag_S_Time, axisC.tag_StopSpeed);
			point.dblPonitValue = axisC.intThreeFindOriginDis;

			shrResult = SR_RelativeMove(axisC, point);
			if (shrResult != 0)
			{
				return -1;
			}

			while (true)
			{
				if (workBase.IsRestExit())
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -1;
				}
				SR_GetOriginInput(axisC.CardNum, (short)(axisC.AxisNum), out pIo);
				if (axisC.tag_homeIoHighLow)
				{
					if (pIo)
					{
						SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						shrResult = 0;
						break;
					}
				}
				else
				{
					if (!pIo)
					{
						SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
						shrResult = 0;
						break;
					}
				}
				if (SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out AxisState) != 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 1;
				}
				if (AxisState == 0)
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return 3;
				}
				Thread.Sleep(1);
			}

			SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
			Thread.Sleep(1000);
			int countzeor = 30;
			while (countzeor >= 0)
			{
				if (workBase.IsRestExit())
				{
					SR_AxisEmgStop(axisC.CardNum, (short)(axisC.AxisNum));
					return -1;
				}
				int status = 0;
				short r = SR_GetAxisStatus(axisC.CardNum, (short)(axisC.AxisNum), out status);
				if (r != 0)
				{
					return -1;
				}
				if (status == 0)
				{
					break;
				}
			}

			while (countzeor >= 0)
			{
				double pos = 0;
				double Enc = 0;

				Thread.Sleep(1000);

				SR_GetPrfPos(axisC.CardNum, (short)(axisC.AxisNum), ref pos);
				SR_GetEncPos(axisC.CardNum, (short)(axisC.AxisNum), ref Enc);
				SR_SetPrfPos(axisC.CardNum, (short)(axisC.AxisNum), 0);
				SR_SetEncPos(axisC.CardNum, (short)(axisC.AxisNum), 0);
				SR_GetPrfPos(axisC.CardNum, (short)(axisC.AxisNum), ref pos);
				SR_GetEncPos(axisC.CardNum, (short)(axisC.AxisNum), ref Enc);

				if (pos < 5 && pos > -5 && Enc < 5 && Enc > -5)
				{
					return 0;
				}

				countzeor--;
				Thread.Sleep(10);
			}
			if (countzeor >= 0)
			{
				return 0;
			}

			return -1;
		}



		// 
		/// <summary>
		/// 单原点回原，如果第一次找不到原点即失败，第一段距离位寻找距离，第二段距离为反退距离。，第三段距离为反退距离+感应器的长度
		/// </summary>
		/// <param name="_acf"></param>
		/// <returns></returns>
		public short SR_GoOneHome(AxisConfig _acf)
		{
			short ret = MutHomeFindHomeSinge(_acf, 16);
			if (ret >= 0)
			{
				if (workBase.IsRestExit())
				{
					return -1;
				}
				Thread.Sleep(1000);
				if (HomeMoveTwoDis(_acf) == 0)
				{
					if (workBase.IsRestExit())
					{
						return -1;
					}
					Thread.Sleep(1000);
					return HomeFindOneHomeIO(_acf);
				}
			}
			return -5;
		}
		/// <summary>
		///单原点回原，如果第一次找不到原点即失败，第一段距离位寻找距离，第二段距离为反退距离。，第三段距离为反退距离+感应器的长度
		/// </summary>
		/// <param name="_acf"></param>
		/// <returns></returns>
		public short SR_GoOneHomeOrg(AxisConfig _acf)
		{
			short ret = MutHomeFindHomeSinge(_acf, 1);
			if (ret >= 0)
			{
				if (workBase.IsRestExit())
				{
					return -1;
				}
				Thread.Sleep(1000);
				if (HomeMoveTwoDis(_acf) == 0)
				{
					Thread.Sleep(1000);
					if (workBase.IsRestExit())
					{
						return -1;
					}

					return HomeFindOneHomeIO(_acf);
				}
			}
			return -5;
		}

	}

}
