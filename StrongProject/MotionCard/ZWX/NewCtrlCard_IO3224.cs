namespace StrongProject
{
	public class NewCtrlCard_IO3224 : NewCtrlCardBase
	{
		public void SR_FunInit()
		{

			SR_InitCard = _SR_InitCard;
			/* SR_AxisEmgStop = _SR_AxisEmgStop;
             SR_GetAxisStatus = _SR_GetAxisStatus;
             SR_RelativeMove = _SR_RelativeMove;
             SR_AbsoluteMove = _SR_AbsoluteMove;
             SR_GoHome = _SR_GoHome;*/
			SR_GetInputBit = _SR_GetInputBit;
			SR_GetOutputBit = _SR_GetOutputBit;
			SR_SetOutputBit = _SR_SetOutputBit;
			/* SR_GetPrfPos = _SR_GetPrfPos;
             SR_GetEncPos = _SR_GetEncPos;
             SR_AxisStop = _SR_AxisStop;*/
			//SR_ClrStatus = _SR_ClrStatus;
			//SR_ClrAllStatus = _SR_ClrAllStatus;
			//SR_GetServoEnable = _SR_GetServoEnable;
			//SR_SetServoEnable = _SR_SetServoEnable;
			/*  SR_GetAlarmInput = _SR_GetAlarmInput;
              SR_GetLimitPInput = _SR_GetLimitPInput;
              SR_GetLimitNInput = _SR_GetLimitNInput;
              SR_GetOriginInput = _SR_GetOriginInput;*/
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

			return (short)adtIO3224.adtIO3224_initial();

		}
		public NewCtrlCard_IO3224()
		{
			tag_AxisCount = 0;
			SR_FunInit();
			tag_Manufacturer = MotionCardManufacturer.MotionCardManufacturer_IO3224;
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
			short shrResult = 0;
			int intValue = 0;
			ushort ushortValue = 0;
			int iioBit = 0;
			bStatus = false;

			shrResult = (short)adtIO3224.adtIO3224_read_in(card, out iioBit);
			int s = iioBit & (1 << ioBit);
			if (shrResult != 0)
			{
				MessageBoxLog.Show("IO3224控制卡初始化失败!");
				CommandResult("GT_GetExtIoValueGts", shrResult);
				return shrResult;
			}
			if (s > 0)
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
			int iioBit = 0;
			bool blnOutStatus;
			shrResult = (short)adtIO3224.adtIO3224_read_out(card, out iioBit);
			int s = iioBit & (1 << ioBit);
			if (shrResult != shrGtsSuccess)
			{
				CommandResult("GT_SetExtIoBitGts", shrResult);
				return shrFail;
			}

			if (s > 0)
			{
				outputIoStatus = false;
			}
			else
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
			shrResult = (short)adtIO3224.adtIO3224_write_bit(card, ioBit, value);
			if (shrResult != 0)
			{
				CommandResult("adt8940a1_write_bit", shrResult);
				return shrResult;
			}
			return shrResult;
		}

	}
}
