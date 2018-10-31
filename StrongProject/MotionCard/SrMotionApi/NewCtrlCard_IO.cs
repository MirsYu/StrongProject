using System;
using System.Threading;
namespace StrongProject
{
	public class NewCtrlCardIO
	{
		/// <summary>
		/// 原点IO
		/// </summary>
		public static ulong[] tag_CardAxisOrgIO = new ulong[100];

		/// <summary>
		/// 正限位IO
		/// </summary>
		public static ulong[] tag_CardAxisLimitPIO = new ulong[100];

		/// <summary>
		/// 负限位IO
		/// </summary>
		public static ulong[] tag_CardAxisLimitNIO = new ulong[100];

		/// <summary>
		/// 轴报警IO
		/// </summary>
		public static ulong[] tag_CardAxisAlarm = new ulong[100];

		/// <summary>
		/// 轴使能
		/// </summary>
		public static ulong[] tag_OnAxisEnable = new ulong[100];

		/// <summary>
		/// 输入IO
		/// </summary>
		public static ulong[] tag_CardInIO = new ulong[100];

		/// <summary>
		/// 输出IO
		/// </summary>
		public static ulong[] tag_CardOutIO = new ulong[100];



		/// <summary>
		/// 
		/// </summary>
		public static double[] tag_OnAxisEncPosChange = new double[100];

		/// <summary>
		/// 
		/// </summary>
		public static double[] tag_OnAxisPrfPosChange = new double[100];


		public static event EventHandler ControlCtrlChange;

		public static int tag_IO_refresh = 0;
		public static int[] tag_CardHave = new int[(int)MotionCardManufacturer.MotionCardManufacturer_max];

		protected void OnControlCtrlChange()
		{
			if (ControlCtrlChange != null)
			{
				ControlCtrlChange(this, null);
			}
		}

		public static event EventHandler InputIOChange;
		protected static void OnInputIOChange(int type, int cardNum, ulong ioStatus)
		{
			if (InputIOChange != null)
				InputIOChange(cardNum, new CardInputIOEvengArgs(type, cardNum, ioStatus));
		}

		public static event EventHandler OutputIOChange;
		protected static void OnOutputIOChange(int type, int cardNum, ulong ioStatus)
		{
			if (OutputIOChange != null)
				OutputIOChange(cardNum, new CardInputIOEvengArgs(type, cardNum, ioStatus));
		}

		public static event EventHandler AxisEnableChange;
		protected static void OnAxisEnableChange(int type, int cardNum, int axis, ulong EnableStatus)
		{
			if (AxisEnableChange != null)
				AxisEnableChange(cardNum, new CardAxisSignalEvengArgs(type, cardNum, axis, EnableStatus));
		}

		public static event EventHandler AxisAlarmChange;
		protected static void OnAxisAlarmChange(int type, int cardNum, int axis, ulong alarmValue)
		{
			if (AxisAlarmChange != null)
			{
				AxisAlarmChange(cardNum, new CardAxisSignalEvengArgs(type, cardNum, axis, alarmValue));
			}
		}

		public static event EventHandler AxisHomeChange;
		protected static void OnAxisHomeChange(int type, int cardNum, int axis, ulong homeValue)
		{
			if (AxisHomeChange != null)
			{
				AxisHomeChange(cardNum, new CardAxisSignalEvengArgs(type, cardNum, axis, homeValue));
			}
		}

		public static event EventHandler AxisLimitPChange;
		protected static void OnAxisLimitPChange(int type, int cardNum, int axis, ulong limitPValue)
		{
			if (AxisLimitPChange != null)
			{
				AxisLimitPChange(cardNum, new CardAxisSignalEvengArgs(type, cardNum, axis, limitPValue));
			}
		}

		public static event EventHandler AxisLimitNChange;
		protected static void OnAxisLimitNChange(int type, int cardNum, int axis, ulong limitNValue)
		{
			if (AxisLimitNChange != null)
			{
				AxisLimitNChange(cardNum, new CardAxisSignalEvengArgs(type, cardNum, axis, limitNValue));
			}
		}

		public static event EventHandler AxisEncPosChange;
		protected static void OnAxisEncPosChange(int type, int cardNum, int axis, double posValue)
		{
			if (AxisEncPosChange != null)
			{
				AxisEncPosChange(cardNum, new CardAxisPosEvengArgs(type, cardNum, axis, posValue));
			}
		}

		public static event EventHandler AxisPrfPosChange;
		protected static void OnAxisPrfPosChange(int type, int cardNum, int axis, double posValue)
		{
			if (AxisPrfPosChange != null)
			{
				AxisPrfPosChange(null, new CardAxisPosEvengArgs(type, cardNum, axis, posValue));
			}
		}

		//板卡开始读取所有信号
		public static void StartListenSignal(int[] CardHave)
		{
			int i = 0;
			Thread thdSignal = new Thread(ReadAllIo);
			while (i < CardHave.Length && i < (int)MotionCardManufacturer.MotionCardManufacturer_max)
			{
				tag_CardHave[i] = CardHave[i];
				i++;
			}
			thdSignal.Start();
			thdSignal.IsBackground = true;
		}

		public static void ReadAllIo(object o)
		{

			while (true)
			{
				int CardIndex = 0;
				int axisIndex = 0;
				for (MotionCardManufacturer i = 0; i < MotionCardManufacturer.MotionCardManufacturer_max; i++)
				{
					NewCtrlCardBase Base = NewCtrlCardV0.tag_NewCtrlCardBase[(int)i];
					short j = 0;
					short n = 0;
					short mInIo = 0;
					if (Base == null || tag_CardHave[(int)i] == 0)
					{
						continue;
					}

					while (j < NewCtrlCardV0.tag_CardCount[(int)i])
					{
						ulong pLimitNValue = 0;
						ulong pLimitPValue = 0;
						ulong pOrgValue = 0;

						ulong pCardAxisAlarmValue = 0;
						ulong pOnAxisEnableValue = 0;

						ulong pCardInIO = 0;
						ulong pCardOutIO = 0;
						ulong one = 1;
						mInIo = 0;
						while (mInIo < 64)
						{
							bool bInio = true;
							bool bOutio = true;
							Base.SR_GetInputBit(j, mInIo, out bInio);
							Base.SR_GetOutputBit(j, mInIo, out bOutio);
							if (bInio == true)
							{
								pCardInIO = pCardInIO + (ulong)(one << mInIo);
							}
							else
							{
								pCardInIO = pCardInIO;
							}
							if (bOutio == true)
							{
								pCardOutIO = pCardOutIO + (ulong)(one << mInIo);
							}
							mInIo++;
						}

						n = 1;
						while (n <= NewCtrlCardV0.tag_CardAxisCount[(int)i])
						{
							bool LimitNvar = true;
							bool LimitPvar = true;
							bool LimitOrgvar = true;
							bool bCardAxisAlarmValue = true;
							bool bOnAxisEnableValue = true;
							double bOnAxisEncPosChange = 0;
							double bOnAxisPrfPosChange = 0;
							Base.SR_GetLimitNInput(j, n, out LimitNvar);
							Base.SR_GetLimitPInput(j, n, out LimitPvar);
							Base.SR_GetOriginInput(j, n, out LimitOrgvar);
							Base.SR_GetAlarmInput(j, n, out bCardAxisAlarmValue);
							//  Base.SR_GetServoEnable(j, n, out bOnAxisEnableValue);
							//Base.SR_GetEncPos(j, n, ref bOnAxisEncPosChange);
							//Base.SR_GetPrfPos(j, n, ref bOnAxisPrfPosChange);
							if (LimitNvar == true)
							{
								pLimitNValue = pLimitNValue + (ulong)(one << n);
							}
							if (LimitPvar == true)
							{
								pLimitPValue = pLimitPValue + (ulong)(one << n);
							}
							if (LimitOrgvar == true)
							{
								pOrgValue = pOrgValue + (ulong)(one << n);
							}
							if (bCardAxisAlarmValue == true)
							{
								pCardAxisAlarmValue = pCardAxisAlarmValue + (ulong)(one << n);
							}
							if (bOnAxisEnableValue == true)
							{
								pOnAxisEnableValue = pOnAxisEnableValue + (ulong)(one << n);
							}

							Base.SR_GetEncPos(j, n, ref bOnAxisEncPosChange);
							Base.SR_GetPrfPos(j, n, ref bOnAxisPrfPosChange);
							if (tag_OnAxisEncPosChange[axisIndex] != bOnAxisEncPosChange || tag_IO_refresh > 0)
							{
								tag_OnAxisEncPosChange[axisIndex] = bOnAxisEncPosChange;
								OnAxisEncPosChange((int)i, j, n, (int)bOnAxisEncPosChange);
							}
							if (tag_OnAxisPrfPosChange[axisIndex] != bOnAxisPrfPosChange || tag_IO_refresh > 0)
							{
								tag_OnAxisPrfPosChange[axisIndex] = bOnAxisPrfPosChange;
								OnAxisPrfPosChange((int)i, j, n, (int)bOnAxisPrfPosChange);
							}
							axisIndex++;
							n++;
						}
						if (tag_CardAxisLimitNIO[CardIndex] != pLimitNValue || tag_IO_refresh > 0)
						{
							tag_CardAxisLimitNIO[CardIndex] = pLimitNValue;
							OnAxisLimitNChange((int)i, j, n, pLimitNValue);
						}
						if (tag_CardAxisLimitPIO[CardIndex] != pLimitPValue || tag_IO_refresh > 0)
						{
							tag_CardAxisLimitPIO[CardIndex] = pLimitPValue;
							OnAxisLimitPChange((int)i, j, n, pLimitPValue);
						}
						if (tag_CardAxisOrgIO[CardIndex] != pOrgValue || tag_IO_refresh > 0)
						{
							tag_CardAxisOrgIO[CardIndex] = pOrgValue;
							OnAxisHomeChange((int)i, j, n, pOrgValue);
						}
						if (tag_CardAxisAlarm[CardIndex] != pCardAxisAlarmValue || tag_IO_refresh > 0)
						{
							tag_CardAxisAlarm[CardIndex] = pCardAxisAlarmValue;
							OnAxisAlarmChange((int)i, j, n, pCardAxisAlarmValue);
						}

						if (pCardInIO != tag_CardInIO[CardIndex] || tag_IO_refresh > 0)
						{
							tag_CardInIO[CardIndex] = pCardInIO;
							OnInputIOChange((int)i, j, pCardInIO);

						}
						if (pCardOutIO != tag_CardInIO[CardIndex] || tag_IO_refresh > 0)
						{
							tag_CardOutIO[CardIndex] = pCardOutIO;
							OnOutputIOChange((int)i, j, pCardOutIO);
						}
						if (tag_IO_refresh > 0)
							tag_IO_refresh--;
						CardIndex++;
						j++;

					}
					// 
				}
				Thread.Sleep(1);
			}
		}
	}
}
