using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
//using Leadshine.CNC.Solder.Core;

using gts;
using System.Windows.Forms;

using StrongProject;


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


//不管轴号从0或1开始可兼容
namespace StrongProject
{
	//IO相关变量
	//有ReadAllInput()更新
	public class CARDSTS:CardBase
	{
		//ReadAllInput();
		public static bool[] InputPulseUp = new bool[BCARD.INPUT_QTY];//输入上升沿
		public static bool[] InputPulseDown = new bool[BCARD.INPUT_QTY];//输入上升沿
		public static bool[] InputRead = new bool[BCARD.INPUT_QTY];//通用输入，右输入为TRUE 由ReadAllInput 更新
		public static bool[] OutputRead = new bool[BCARD.OUTPUT_QTY];//通用输出，右输出为TRUE
		public static bool[] InLimitNegative = new bool[BCARD.AXIS_QTY_ALL];// input of axis negative
		public static bool[] InLimitPositive = new bool[BCARD.AXIS_QTY_ALL];//input of axis positive
		public static bool[] InServerAlarm = new bool[BCARD.AXIS_QTY_ALL];//input of axis alarm
		public static bool[] InLimitOri = new bool[BCARD.AXIS_QTY_ALL];//input of axis ori

		public static bool[] AxisMoving = new bool[BCARD.AXIS_QTY_ALL];//axis moving
		public static TPusle[] LogicPos0 = new TPusle[BCARD.AXIS_QTY_ALL];//axis moving
		public static TPusle[] EncodePos0 = new TPusle[BCARD.AXIS_QTY_ALL];

		public static bool[] InLimitSoftN = new bool[BCARD.AXIS_QTY_ALL];
		public static bool[] InLimitSoftP = new bool[BCARD.AXIS_QTY_ALL];//正软限位



		public static bool[] HomeFinish = new bool[BCARD.AXIS_QTY_ALL];//axis moving
		public static bool[] HomeFail = new bool[BCARD.AXIS_QTY_ALL];
		public static bool[] Homing = new bool[BCARD.AXIS_QTY_ALL];
		public static class IO
		{
			static IO()
			{
				//由参数读出或初始化，但定时器可能在初始化前
				for (TIOPoint i = 0; i < IOInput.Length; i++)
				{
					IOInput[i] = new IOinfoInput();
					IOInput[i].IOid = i;
					IOInput[i].IOname = IOinputString[i].getIOname() ;//需用于控件初始化
				}
				for (TIOPoint i = 0; i < IOOutput.Length; i++)
				{
					IOOutput[i] = new IOinfoOutput();
					IOOutput[i].IOid = i;
					IOOutput[i].IOname = IOoutputString[i].getIOname();
				}
			}

			//存放io point&name
			public class ION
			{
				public ION(int point, string pname)
				{
					ioID = point;
					ioName = pname;
				}
				int ioID;
				string ioName;
				public int getIOID()
				{
					return ioID;
				}
				public string getIOname()
				{
					return ioName;
				}
			}

			public static IOinfoInput[] IOInput = new IOinfoInput[BCARD.INPUT_QTY];
			public static IOinfoOutput[] IOOutput = new IOinfoOutput[BCARD.OUTPUT_QTY];

			#region gt IO define&string
			#region IOinputString
			public static ION[] IOinputString ={
                                                new ION (IOdefineIn.PIN_SEN_SIDE_FILM_LEFT_0,"左侧出膜感应"),new ION(IOdefineIn.PIN_SEN_SIDE_FILM_OUT_START_LEFT_1,"左侧收膜开始"),
                                                new ION (IOdefineIn.PIN_SEN_SIDE_FILM_OUT_STOP_LFET_2,"左侧收膜停止"),new ION(IOdefineIn.PIN_VACUUM_SIDE_FILM_LEFT_3,"左侧膜真空吸"),
                                                new ION (IOdefineIn.PIN_SEN_LOAD_BELT_HAS,"前段来料感应"),new ION(IOdefineIn.PIN_SEN_LOAD_BELT_FRONT_REAR,"前段正面检测"),
                                                new ION (IOdefineIn.PIN_SEN_LOAD_BELT_HAS1_6,"前段来料感应2"),new ION(IOdefineIn.PIN_SEN_LOAD_BELT_INPLACE,"前段到位检测"),

                                                new ION (IOdefineIn.PIN_SEN_WORK_BELT_HAS_8,"中段来料判断"),new ION(IOdefineIn.PIN_SEN_WORK_BELT_INPLACE_9,"中段到料判断"),
                                                new ION (IOdefineIn.PIN_SEN_WORK_BELT_OUTPLACE_10,"中段出料判断"),new ION(IOdefineIn.PIN_BTN_LEFT_START_11,"启动按钮"),
                                                new ION (IOdefineIn.PIN_BTN_RIGHT_START_12,"备用"),new ION(IOdefineIn.PIN_BTN_STOP_13,"停止按钮"),
                                                new ION (IOdefineIn.PIN_BTN_RESET_14,"复位按钮"),new ION(IOdefineIn.PIN_BTN_EMG_15,"急停按钮"),


                                                new ION (IOdefineIn.PIN_SEN_SIDE_FILM_RIGHT_16,"右侧出膜感应"),new ION(IOdefineIn.PIN_SEN_SIDE_FILM_OUT_START_RIGHT_17,"右侧收膜开始"),
                                                new ION (IOdefineIn.PIN_SEN_SIDE_FILM_OUT_STOP_RIGHT_18,"右侧收膜停止"),new ION(IOdefineIn.PIN_VACUUM_SIDE_FILM_RIGHT_19,"右侧膜真空吸"),
                                                new ION (IOdefineIn.PIN_SEN_UNLOAD_BELT_HAS_20,"后段来料感应"),new ION(IOdefineIn.PIN_SEN_UNLOAD_BELT_INPLACE_21,"后段出料检测"),
                                                new ION (IOdefineIn.PIN_VACUUM_PAW_LEFT_22,"移载左真空吸"),new ION(IOdefineIn.PIN_VACUUM_PAW_RIGHT_23,"移载右真空吸"),

                                                new ION (IOdefineIn.PIN_CY_PAW_LEFT_ORI_24,"左移载升降原位"),new ION(IOdefineIn.PIN_CY_PAW_LEFT_REACH_25,"左移载升降置位"),
                                                new ION (IOdefineIn.PIN_CY_PAW_RIGHT_ORI_26,"右移载升降原位"),new ION(IOdefineIn.PIN_CY_PAW_RIGHT_REACH_27,"右移载升降置位"),
                                                new ION (IOdefineIn.PIN_SEN_DOOR1_28,"门限1"),new ION(IOdefineIn.PIN_SEN_DOOR2_29,"门限2"),
                                                new ION (IOdefineIn.PIN_RESERVE1_30,"备用"),new ION(IOdefineIn.PIN_RESERVE2_31,"备用"),

                                                new ION (IOdefineIn.PIN_SEN_BOTTOM_FILE_OUT_START_32,"底膜收膜开始"),new ION(IOdefineIn.PIN_SEN_BOTTOM_FILE_OUT_STOP_33,"底膜收膜停止"),
                                                new ION (IOdefineIn.PIN_SEN_BOTTOM_FILE_34,"底膜出膜感应"),new ION(IOdefineIn.PIN_SEN_RESERVE_35,"备用"),
                                                new ION (IOdefineIn.PIN_SEN_RESERVE_36,"备用"),new ION(IOdefineIn.PIN_SEN_RESERVE_37,"备用"),
                                                new ION (IOdefineIn.PIN_SEN_RESERVE_38,"备用"),new ION(IOdefineIn.PIN_SEN_RESERVE_39,"备用"),

                                                new ION (IOdefineIn.PIN_SEN_RESERVE_40,"备用"),new ION(IOdefineIn.PIN_SEN_RESERVE_41,"备用"),
                                                new ION (IOdefineIn.PIN_SEN_RESERVE_42,"备用"),new ION(IOdefineIn.PIN_SEN_RESERVE_43,"备用"),
                                                new ION (IOdefineIn.PIN_CY_SIDE_FILM_PUSH_LEFT_REACH_44,"左侧膜压膜置位"),new ION(IOdefineIn.PIN_CY_SIDE_FILM_PUSH_RIGHT_REACH_45,"右侧膜压膜置位"),
                                                new ION (IOdefineIn.PIN_CY_SIDE_FILM_PASTE_LEFT_REACH_46,"左侧膜气缸置位"),new ION(IOdefineIn.PIN_CY_SIDE_FILM_PASTE_RIGHT_REACH_47,"右侧膜气缸置位")
												 };
			#endregion 
			#region IO input Const// start with 0
			public class IOdefineIn
			{
				public const TIOPoint PIN_SEN_SIDE_FILM_LEFT_0 = 0;//左侧膜料感应
				public const TIOPoint PIN_SEN_SIDE_FILM_OUT_START_LEFT_1 = 1;//左侧膜拉料感应
				public const TIOPoint PIN_SEN_SIDE_FILM_OUT_STOP_LFET_2 = 2;//左侧膜拉料停止
				public const TIOPoint PIN_VACUUM_SIDE_FILM_LEFT_3 = 3;//左侧膜真空吸
				public const TIOPoint PIN_SEN_LOAD_BELT_HAS = 4;//前段来料感应
				public const TIOPoint PIN_SEN_LOAD_BELT_FRONT_REAR = 5;//前段正反检测
				public const TIOPoint PIN_SEN_LOAD_BELT_HAS1_6 = 6;//前段物料有无感应 2
				public const TIOPoint PIN_SEN_LOAD_BELT_INPLACE = 7;//前段到位检测
				public const TIOPoint PIN_SEN_WORK_BELT_HAS_8 = 8;//中段来料判断
				public const TIOPoint PIN_SEN_WORK_BELT_INPLACE_9 = 9;//中段到料判断
				public const TIOPoint PIN_SEN_WORK_BELT_OUTPLACE_10 = 10;//中段出料判断
				public const TIOPoint PIN_BTN_LEFT_START_11 = 11;//左启动按钮
				public const TIOPoint PIN_BTN_RIGHT_START_12 = 12;//右启动按钮
				public const TIOPoint PIN_BTN_STOP_13 = 13;//停止按钮
				public const TIOPoint PIN_BTN_RESET_14 = 14;//复位按钮
				public const TIOPoint PIN_BTN_EMG_15 = 15;//急停按钮

				public const TIOPoint PIN_SEN_SIDE_FILM_RIGHT_16 = 16;//右侧膜料感应
				public const TIOPoint PIN_SEN_SIDE_FILM_OUT_START_RIGHT_17 = 17;//右侧膜拉料感应
				public const TIOPoint PIN_SEN_SIDE_FILM_OUT_STOP_RIGHT_18 = 18;//右侧膜拉料停止
				public const TIOPoint PIN_VACUUM_SIDE_FILM_RIGHT_19 = 19;//右侧膜真空吸
				public const TIOPoint PIN_SEN_UNLOAD_BELT_HAS_20 = 20;//后段来料感应
				public const TIOPoint PIN_SEN_UNLOAD_BELT_INPLACE_21= 21;//后段正面检测
				public const TIOPoint PIN_VACUUM_PAW_LEFT_22 = 22;//移载左真空吸
				public const TIOPoint PIN_VACUUM_PAW_RIGHT_23= 23;//移载右真空吸
				public const TIOPoint PIN_CY_PAW_LEFT_ORI_24 = 24;//左移载升降原位
				public const TIOPoint PIN_CY_PAW_LEFT_REACH_25 = 25;//左移载升降置位
				public const TIOPoint PIN_CY_PAW_RIGHT_ORI_26 = 26;//右移载升降原位
				public const TIOPoint PIN_CY_PAW_RIGHT_REACH_27 = 27;//右移载升降置位
				public const TIOPoint PIN_SEN_DOOR1_28 = 28;//门限1
				public const TIOPoint PIN_SEN_DOOR2_29= 29;//门限2
				public const TIOPoint PIN_RESERVE1_30 = 30;//备用
				public const TIOPoint PIN_RESERVE2_31= 31;//备用

				public const TIOPoint PIN_SEN_BOTTOM_FILE_OUT_START_32 = 32;//低膜拉料感应
				public const TIOPoint PIN_SEN_BOTTOM_FILE_OUT_STOP_33 = 33;//低膜停止感应
				public const TIOPoint PIN_SEN_BOTTOM_FILE_34 = 34;//低膜膜料感应
				public const TIOPoint PIN_SEN_RESERVE_35 = 35;//备用
				public const TIOPoint PIN_SEN_RESERVE_36 = 36;//备用1
				public const TIOPoint PIN_SEN_RESERVE_37 = 37;//备用2
				public const TIOPoint PIN_SEN_RESERVE_38 = 38;//备用
				public const TIOPoint PIN_SEN_RESERVE_39= 39;//备用
				public const TIOPoint PIN_SEN_RESERVE_40 = 40;//备用
				public const TIOPoint PIN_SEN_RESERVE_41 = 41;//备用
				public const TIOPoint PIN_SEN_RESERVE_42 = 42;//备用
				public const TIOPoint PIN_SEN_RESERVE_43 = 43;//备用
				public const TIOPoint PIN_CY_SIDE_FILM_PUSH_LEFT_REACH_44 = 44;//左侧膜压膜置位
				public const TIOPoint PIN_CY_SIDE_FILM_PUSH_RIGHT_REACH_45 = 45;//右侧膜压膜置位
				public const TIOPoint PIN_CY_SIDE_FILM_PASTE_LEFT_REACH_46 = 46;//左贴膜压膜置位
				public const TIOPoint PIN_CY_SIDE_FILM_PASTE_RIGHT_REACH_47 = 47;//右贴膜压膜置位
			}


			#endregion
			#region IOoutputString
			public static ION[] IOoutputString ={
                                                new ION (IOdefineOut.POUT_CY_SIDE_FILM_LEFT_0,"左侧膜贴膜气缸"),new ION(IOdefineOut.POUT_VACUUM_SIDE_FILM_LEFT_1,"左侧膜真空"),
                                                new ION (IOdefineOut.POUT_CY_WORK_BELT_CLAMP_2,"中段夹紧气缸置位"),new ION(IOdefineOut.POUT_CY_WORK_BELT_CLAMP_RESET_3,"备用"),
                                                new ION (IOdefineOut.POUT_CY_WORK_BELT_JACK_4,"中段升降气缸"),new ION(IOdefineOut.POUT_LAMP_RED_5,"红灯"),
                                                new ION (IOdefineOut.POUT_LAMP_GREEN_6,"绿灯"),new ION(IOdefineOut.POUT_LAMP_YELLOW_7,"黄灯"),

                                                new ION (IOdefineOut.POUT_LAMP_BUZZER_8,"蜂鸣器"),new ION(IOdefineOut.POUT_CY_WORK_BELT_JACK_RESET_9,"中段升降气缸复位"),
                                                new ION (IOdefineOut.POUT_RESERVE_10,"备用"),new ION(IOdefineOut.POUT_RESERVE_11,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_12,"备用"),new ION(IOdefineOut.POUT_RESERVE_13,"备用"),
                                                new ION (IOdefineOut.POUT_CY_PAW_LEFT_BLOW_14,"左移载吹气"),new ION(IOdefineOut.POUT_CY_PAW_RIGHT_BLOW_15,"右移载吹气"),

                                                new ION (IOdefineOut.POUT_CY_SIDE_FILM_RIGHT_16,"右侧膜贴膜气缸"),new ION(IOdefineOut.POUT_VACUUM_SIDE_FILM_RIGHT_17,"右侧膜真空"),
                                                new ION (IOdefineOut.POUT_VACUUM_PAW_LEFT_18,"左移载真空"),new ION(IOdefineOut.POUT_VACUUM_PAW_RIGHT_19,"右移载真空"),
                                                new ION (IOdefineOut.POUT_CY_PAW_LEFT_RESET_20,"左移载升降复位"),new ION(IOdefineOut.POUT_CY_PAW_RIGHT_RESET_21,"右移载升降复位"),
                                                new ION (IOdefineOut.POUT_CY_BOTTOM1_22,"低膜前压滚轮"),new ION(IOdefineOut.POUT_CY_BOTTOM2_23,"低膜后压滚轮"),

                                                new ION (IOdefineOut.POUT_CY_PAW_LEFT_SET_24,"左移载升降置位"),new ION(IOdefineOut.POUT_CY_PAW_RIGHT_SET_25,"右移载升降置位"),
                                                new ION (IOdefineOut.POUT_RESERVE_26,"备用"),new ION(IOdefineOut.POUT_RESERVE_27,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_28,"备用"),new ION(IOdefineOut.POUT_RESERVE_29,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_30,"备用"),new ION(IOdefineOut.POUT_RESERVE_31,"备用"),

                                                new ION (IOdefineOut.POUT_RESERVE_32,"备用"),new ION(IOdefineOut.POUT_RESERVE_33,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_34,"备用"),new ION(IOdefineOut.POUT_RESERVE_35,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_36,"备用"),new ION(IOdefineOut.POUT_RESERVE_37,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_38,"备用"),new ION(IOdefineOut.POUT_RESERVE_39,"备用"),

                                                new ION (IOdefineOut.POUT_RESERVE_40,"备用"),new ION(IOdefineOut.POUT_RESERVE_41,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_42,"备用"),new ION(IOdefineOut.POUT_RESERVE_43,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_44,"备用"),new ION(IOdefineOut.POUT_RESERVE_45,"备用"),
                                                new ION (IOdefineOut.POUT_RESERVE_46,"备用"),new ION(IOdefineOut.POUT_RESERVE_47,"备用"),

												 };
			#endregion
			#region IO output const
			public class IOdefineOut
			{
				public const TIOPoint POUT_CY_SIDE_FILM_LEFT_0 = 0;//左侧膜贴膜气缸
				public const TIOPoint POUT_VACUUM_SIDE_FILM_LEFT_1 = 1;//左侧膜真空
				public const TIOPoint POUT_CY_WORK_BELT_CLAMP_2 = 2;//中段夹紧气缸
				public const TIOPoint POUT_CY_WORK_BELT_CLAMP_RESET_3 = 3;//备用
				public const TIOPoint POUT_CY_WORK_BELT_JACK_4 = 4;//中段升降气缸
				public const TIOPoint POUT_LAMP_RED_5 = 5;//红灯
				public const TIOPoint POUT_LAMP_GREEN_6 = 6;//绿灯
				public const TIOPoint POUT_LAMP_YELLOW_7 = 7;//黄灯
				public const TIOPoint POUT_LAMP_BUZZER_8 = 8;//蜂鸣器
                public const TIOPoint POUT_CY_WORK_BELT_JACK_RESET_9 = 9;//-备用
				public const TIOPoint POUT_RESERVE_10 = 10;//-备用
				public const TIOPoint POUT_RESERVE_11 = 11;//-备用
				public const TIOPoint POUT_RESERVE_12 = 12;//-备用
				public const TIOPoint POUT_RESERVE_13 = 13;//-备用
				public const TIOPoint POUT_CY_PAW_LEFT_BLOW_14 = 14;//-备用
				public const TIOPoint POUT_CY_PAW_RIGHT_BLOW_15 = 15;//-备用

				public const TIOPoint POUT_CY_SIDE_FILM_RIGHT_16 = 16;//右侧膜贴膜气缸
				public const TIOPoint POUT_VACUUM_SIDE_FILM_RIGHT_17 = 17;//右侧膜真空
				public const TIOPoint POUT_VACUUM_PAW_LEFT_18 = 18;//左移载真空
				public const TIOPoint POUT_VACUUM_PAW_RIGHT_19 = 19;//右移载真空
				public const TIOPoint POUT_CY_PAW_LEFT_RESET_20 = 20;//左移载升降
                public const TIOPoint POUT_CY_PAW_RIGHT_RESET_21 = 21;//右移载升降
				public const TIOPoint POUT_CY_BOTTOM1_22 = 22;//低膜气缸1
				public const TIOPoint POUT_CY_BOTTOM2_23 = 23;//低膜气缸2
                public const TIOPoint POUT_CY_PAW_LEFT_SET_24 = 24;//备用
                public const TIOPoint POUT_CY_PAW_RIGHT_SET_25 = 25;//-备用
				public const TIOPoint POUT_RESERVE_26 = 26;//-备用
				public const TIOPoint POUT_RESERVE_27 = 27;//-备用
				public const TIOPoint POUT_RESERVE_28 = 28;//-备用
				public const TIOPoint POUT_RESERVE_29 = 29;//-备用
				public const TIOPoint POUT_RESERVE_30 = 30;//-备用
				public const TIOPoint POUT_RESERVE_31 = 31;//-备用

				public const TIOPoint POUT_RESERVE_32 = 32;//备用
				public const TIOPoint POUT_RESERVE_33 = 33;//备用
				public const TIOPoint POUT_RESERVE_34 = 34;//备用
				public const TIOPoint POUT_RESERVE_35 = 35;//备用
				public const TIOPoint POUT_RESERVE_36 = 36;//备用
				public const TIOPoint POUT_RESERVE_37 = 37;//备用
				public const TIOPoint POUT_RESERVE_38 = 38;//备用1
				public const TIOPoint POUT_RESERVE_39 = 39;//备用2
				public const TIOPoint POUT_RESERVE_40 = 40;//备用
				public const TIOPoint POUT_RESERVE_41 = 41;//-备用
				public const TIOPoint POUT_RESERVE_42 = 42;//-备用
				public const TIOPoint POUT_RESERVE_43 = 43;//-备用
				public const TIOPoint POUT_RESERVE_44 = 44;//-备用
				public const TIOPoint POUT_RESERVE_45 = 45;//-备用
				public const TIOPoint POUT_RESERVE_46 = 46;//-备用
				public const TIOPoint POUT_RESERVE_47 = 47;//-备用
			}


			#endregion
			#endregion

		}
	}


	public class CtrlCardSR:CardBase
	{
		static CtrlCardSR()
		{
			AxisParams = new CAxisParams[BCARD.AXIS_QTY_ALL];
			for (int i = 0; i < BCARD.AXIS_QTY_ALL; i++)
			{
				AxisParams[i] = new CAxisParams((TCard)(i / 8), (TAxis)(i % 8 + 1));
			}

			InitCoordinate();
		}
		#region 全局参数
		private const string LOG_CARD_ERROR = "error_card";
		private const string LOG_HOME = "home";


		//public const TAxis AXIS_QTY = 16;
		//public const TAxis CARD_AXIS_QTY = 8;
		public static bool bCardRunFlg = true;//轴是否正常动作标志

		//public const short CARD_NUM_0 = 0;


		private static string errorMessage;

		 

		public static CAxisParams[] AxisParams;//下标从0开始
		//返回值只读不写
		private static CAxisParams GetAxisSt(TCard card, TAxis axis)
		{
			CAxisParams stAxis = new CAxisParams(0, 0);
			for (int i = 0; i < AxisParams.Length; i++)
			{
				if (card == AxisParams[i].CardNo && axis == AxisParams[i].AxisNo)
				{
					return AxisParams[i];
				}

			}
				MessageBoxLog.Show("不存在给定的轴");
			return stAxis;
		}

		#endregion

		#region 内部函数
		//字节处理：将指定位设0
		private static int SetBit0(params int[] bits)
		{
			int retVal = 0xFFFF;
			for (int i = 0; i < bits.Length; i++)
			{
				retVal &= ~(1 << bits[i]);
			}
			return retVal;
		}
		//write log
        private static void WriteTxtDate(string WriteString, string fileName, string path = Global.CConst. SAVE_DATA_PATH)
		{
            Global.CFile.WriteTxtDate(WriteString, fileName, path);
		}
		//回原点log
		private static void HomeLog(TCard card, TAxis axis, string strLog)
		{
			string message = string.Format("card:{0},axis:{1}:{2}", card, axis, strLog);
			WriteTxtDate(message, LOG_HOME);
		}
		//判断命令执行结果
		private static bool CommandResult(string command, short srtn)
		{
			if (srtn == RET_SUCCESS)
			{
				return true;
			}
			else
			{
				errorMessage = command + "命令出错，返回值：" + srtn.ToString();
				Console.WriteLine(command + " error,return value:" + srtn.ToString());
				WriteTxtDate(errorMessage, LOG_CARD_ERROR);
				return false;
			}
		}
		//电机参数初始化
		private static TReturn AxisInit()
		{
			short sResult;
			mc.TTrapPrm tPra;
			short sRtn;
			TCard car = 0;
			//打开运动控制器
			sRtn = mc.GT_Open(car, 0, 1);
			//sRtn = mc.GT_Open(car, 0, 3);
			if (!CommandResult("GT_Open", sRtn))
			{
				return RET_FAIL;
			}
            //复位控制器
            sRtn = mc.GT_Reset(car);
            if (!CommandResult("GT_Reset", sRtn))
            {
                return RET_FAIL;
            }
			//配置运动控制器
			sRtn = mc.GT_LoadConfig(car, Application.StartupPath + "\\GTS8001.cfg");
			if (!CommandResult("GT_LoadConfig", sRtn))
			{
				return RET_FAIL;
			}
			//清除指定轴的报警和限位
			sRtn = mc.GT_ClrSts(car, 1, 8);
			if (!CommandResult("GT_ClrSts", sRtn))
			{
				return RET_FAIL;
			}
			for (TAxis axis = 1; axis < 9; axis++)
			{
				sResult = mc.GT_GetTrapPrm(car, axis, out tPra);
				if (!CommandResult("GT_GetTrapPrm", sResult))
				{
					return RET_FAIL;
				}
				tPra.acc = BAX.AXIS_ACC;
				tPra.dec = BAX.AXIS_DEC;
				tPra.smoothTime = BAX.SMOOTHTIME;
				sResult = mc.GT_SetTrapPrm(car, axis, ref tPra);
				if (!CommandResult("GT_SetTrapPrm", sResult))
				{
					return RET_FAIL;
				}
			}
            if (CtrlCard.BCARD.CARD_QTY <= 1)
            {
			    return RET_SUCCESS;
            }

			car = 1;
			//打开运动控制器
			sRtn = mc.GT_Open(car, 0, 1);
			//sRtn = mc.GT_Open(car, 0, 3);
			if (!CommandResult("GT_Open", sRtn))
			{
				return RET_FAIL;
			}
            //复位控制器
            sRtn = mc.GT_Reset(car);
            if (!CommandResult("GT_Reset", sRtn))
            {
                return RET_FAIL;
            }
			//配置运动控制器
			sRtn = mc.GT_LoadConfig(car, Application.StartupPath + "\\GTS8002.cfg");
			if (!CommandResult("GT_LoadConfig", sRtn))
			{
				return RET_FAIL;
			}
			//清除指定轴的报警和限位
			sRtn = mc.GT_ClrSts(car, 1, 8);
			if (!CommandResult("GT_ClrSts", sRtn))
			{
				return RET_FAIL;
			}
			for (TAxis axis = 1; axis < 9; axis++)
			{
				sResult = mc.GT_GetTrapPrm(car, axis, out tPra);
				if (!CommandResult("GT_GetTrapPrm", sResult))
				{
					return RET_FAIL;
				}
				tPra.acc = BAX.AXIS_ACC;
				tPra.dec = BAX.AXIS_DEC;
				tPra.smoothTime = BAX.SMOOTHTIME;
				sResult = mc.GT_SetTrapPrm(car, axis, ref tPra);
				if (!CommandResult("GT_SetTrapPrm", sResult))
				{
					return RET_FAIL;
				}
			}
            if (CtrlCard.BCARD.CARD_QTY <= 2)
            {
                return RET_SUCCESS;
            }
            car = 2;
            //打开运动控制器
            sRtn = mc.GT_Open(car, 0, 1);
            //sRtn = mc.GT_Open(car, 0, 3);
            if (!CommandResult("GT_Open", sRtn))
            {
                return RET_FAIL;
            }
            //复位控制器
            sRtn = mc.GT_Reset(car);
            if (!CommandResult("GT_Reset", sRtn))
            {
                return RET_FAIL;
            }
            //配置运动控制器
            sRtn = mc.GT_LoadConfig(car, Application.StartupPath + "\\GTS8003.cfg");
            if (!CommandResult("GT_LoadConfig", sRtn))
            {
                return RET_FAIL;
            }
            //清除指定轴的报警和限位
            sRtn = mc.GT_ClrSts(car, 1, 8);
            if (!CommandResult("GT_ClrSts", sRtn))
            {
                return RET_FAIL;
            }
            for (TAxis axis = 1; axis < 9; axis++)
            {
                sResult = mc.GT_GetTrapPrm(car, axis, out tPra);
                if (!CommandResult("GT_GetTrapPrm", sResult))
                {
                    return RET_FAIL;
                }
                tPra.acc = BAX.AXIS_ACC;
                tPra.dec = BAX.AXIS_DEC;
                tPra.smoothTime = BAX.SMOOTHTIME;
                sResult = mc.GT_SetTrapPrm(car, axis, ref tPra);
                if (!CommandResult("GT_SetTrapPrm", sResult))
                {
                    return RET_FAIL;
                }
            }
            return RET_SUCCESS;
            ////extern IO
            //car = 0;//add by 0809
            //sRtn = mc.GT_OpenExtMdlGts(car, "Gts.dll");
            //if (!CommandResult("GT_OpenExtMdlGts", sRtn))
            //{
            //    return RET_FAIL;
            //}
            //sRtn = mc.GT_LoadExtConfigGts(car, Application.StartupPath + "\\ExtModule1.cfg");
            //if (!CommandResult("GT_LoadExtConfigGts", sRtn))
            //{
            //    return RET_FAIL;
            //}
            //return RET_SUCCESS;

		}
		//控制卡单点输出
		private static void SetDoBit(TCard card, TIOPoint pt, short pOut)
		{//pt start with 0;
			short sResult;
			sResult = mc.GT_SetDoBit(card, mc.MC_GPO, (short)(pt + 1), pOut);
			//GT_SetDoBit pt start with 1, add 1 make SetDoBit start with 0
			if (!CommandResult("GT_SetDoBit", sResult))
			{
				return;
			}
		}

		//单点点位
		private static TReturn AxisPrfTrap(TCard card, short axis, int postion, double vel)
		{
            short sResult;
            int ipos;
            //sResult = mc.GT_SetAxisBand(axis, torlerance, iTime);
            sResult = mc.GT_Stop(card, 1 << (axis - 1), 0);
            if (!CommandResult("GT_Stop", sResult))
            {
                return RET_FAIL;
            }
            //设置为点位模试
            sResult = mc.GT_PrfTrap(card, axis);
            if (!CommandResult("GT_PrfTrap", sResult))
            {
                return RET_FAIL;
            }
            mc.TTrapPrm tprm;
            //读取点位运动参数           
            sResult = mc.GT_GetTrapPrm(card, axis, out tprm);
            if (!CommandResult("GT_GetTrapPrm", sResult))
            {
                return RET_FAIL;
            }
            tprm.acc = BAX.AXIS_ACC;
            tprm.dec = BAX.AXIS_DEC;
            tprm.smoothTime = BAX.SMOOTHTIME;
            //设置点位运动参数
            sResult = mc.GT_SetTrapPrm(card, axis, ref  tprm);
            if (!CommandResult("GT_SetTrapPrm", sResult))
            {
 
                WriteTxtDate("AxisPrfTrap_GT_SetTrapPrm_Eroor_10", LOG_CARD_ERROR);
                //return RET_FAIL;
            }
            sResult = mc.GT_ClrSts(card, axis, 1);
            if (!CommandResult("GT_ClrSts", sResult))
            {
                return RET_FAIL;
            }
            sResult = mc.GT_ClrSts(card, axis, 1);//1001 09
            if (!CommandResult("GT_ClrSts", sResult))
            {
                return RET_FAIL;
            }
            sResult = mc.GT_GetPos(card, axis, out ipos);
            if (!CommandResult("GT_SetPos", sResult))
            {
                return RET_FAIL;
            }
            //设置目标位置
            //ipos = ipos + postion;
            sResult = mc.GT_SetPos(card, axis, postion);
            if (!CommandResult("GT_SetPos", sResult))
            {
                return RET_FAIL;
            }
            //设置轴运动速度
            sResult = mc.GT_SetVel(card, axis, vel);
            if (!CommandResult("GT_SetVel", sResult))
            {
                return RET_FAIL;
            }
            //启动轴运动
            sResult = mc.GT_Update(card, 1 << (axis - 1));
            if (!CommandResult("GT_Update", sResult))
            {
                return RET_FAIL;
            }
            return RET_SUCCESS;




            //short sResult;
            ////设置为点位模试
            //sResult = mc.GT_PrfTrap(card, axis);
            //if (!CommandResult("GT_PrfTrap", sResult))
            //{
            //    return RET_FAIL;
            //}
            ////轴跟随功能
            //sResult = mc.GT_SetAxisBand(card, axis, BAX.torlerance, BAX.iTime);
            //if (!CommandResult("GT_SetAxisBand", sResult))
            //{
            //    return RET_FAIL;
            //}
            //mc.TTrapPrm tprm; 
            ////读取点位运动参数           
            //sResult = mc.GT_GetTrapPrm(card, axis, out tprm);
            //if (!CommandResult("GT_GetTrapPrm", sResult))
            //{
            //    return RET_FAIL;
            //}
            //tprm.acc = BAX.AXIS_ACC;
            //tprm.dec = BAX.AXIS_DEC;
            //tprm.smoothTime = BAX.SMOOTHTIME;
            ////设置点位运动参数
            //sResult = mc.GT_SetTrapPrm(card, axis, ref  tprm);
            //if (!CommandResult("GT_SetTrapPrm", sResult))
            //{
            //    return RET_FAIL;
            //}
            ////设置目标位置
            //sResult = mc.GT_SetPos(card, axis, postion);
            //if (!CommandResult("GT_SetPos", sResult))
            //{
            //    return RET_FAIL;
            //}
            ////设置轴运动速度
            //sResult = mc.GT_SetVel(card, axis, vel);
            //if (!CommandResult("GT_SetVel", sResult))
            //{
            //    return RET_FAIL;
            //}
            ////清除状态
            //sResult = mc.GT_ClrSts(card, axis, 1);
            //if (!CommandResult("GT_ClrSts", sResult))
            //{
            //    return RET_FAIL;
            //}
            ////启动轴运动
            //sResult = mc.GT_Update(card, 1 << (axis - 1));
            //if (!CommandResult("GT_Update", sResult))
            //{
            //    return RET_FAIL;
            //}
            //return RET_SUCCESS;
		}
		//点位模试相对运动
		private static TReturn AxisPrfTrapRel(TCard card, TAxis axis, mc.TTrapPrm tprm, TPusle postion, TSpeed vel)
		{
			short sResult;
			int ipos;
			//sResult = mc.GT_SetAxisBand(axis, torlerance, iTime);
			//设置为点位模试
            if (SR_IsAxisStop(card, axis) !=RET_SUCCESS )
            {
                //MessageBoxLog.Show("未停止");
                Global.CFile.WriteTxtDate("not stop", "not stop");
            }
			sResult = mc.GT_PrfTrap(card, axis);
			if (!CommandResult("GT_PrfTrap", sResult))
			{
				return RET_FAIL;
			}
			//读取点位运动参数           
			sResult = mc.GT_GetTrapPrm(card, axis, out tprm);
			if (!CommandResult("GT_GetTrapPrm", sResult))
			{
				return RET_FAIL;
			}
			tprm.acc = BAX.AXIS_ACC;
			tprm.dec = BAX.AXIS_DEC;
			tprm.smoothTime = BAX.SMOOTHTIME;
			//设置点位运动参数
			sResult = mc.GT_SetTrapPrm(card, axis, ref  tprm);
			if (!CommandResult("GT_SetTrapPrm", sResult))
			{
				return RET_FAIL;
			}
			sResult = mc.GT_ClrSts(card, axis, 1);
			if (!CommandResult("GT_ClrSts", sResult))
			{
				return RET_FAIL;
			}
			sResult = mc.GT_GetPos(card, axis, out ipos);
			if (!CommandResult("GT_SetPos", sResult))
			{
				return RET_FAIL;
			}
			//设置目标位置
			ipos = ipos + postion;
			sResult = mc.GT_SetPos(card, axis, ipos);
			if (!CommandResult("GT_SetPos", sResult))
			{
				return RET_FAIL;
			}
			//设置轴运动速度
			sResult = mc.GT_SetVel(card, axis, vel);
			if (!CommandResult("GT_SetVel", sResult))
			{
				return RET_FAIL;
			}
			//启动轴运动
			sResult = mc.GT_Update(card, 1 << (axis - 1));
			if (!CommandResult("GT_Update", sResult))
			{
				return RET_FAIL;
			}
			return RET_SUCCESS;
		}
		//Jog模试运动   主要用于皮带线
		private static TReturn AxisPrfJog(TCard car, TAxis axis, TSpeed vel)
		{
			mc.TJogPrm jog;
			short sResult;
			sResult = mc.GT_PrfJog(car, axis);
			if (!CommandResult("GT_PrfJog", sResult))
			{
				return RET_FAIL;
			}
            sResult = mc.GT_ClrSts(car , axis, 1);//1001 09
            if (!CommandResult("GT_ClrSts", sResult))
            {
                return RET_FAIL;
            }
			sResult = mc.GT_GetJogPrm(car, axis, out jog);
			if (!CommandResult("GT_GetJogPrm", sResult))
			{
				return RET_FAIL;
			}
			jog.acc = 0.0625;
			jog.dec = 0.0625;

			sResult = mc.GT_SetJogPrm(car, axis, ref  jog);
			if (!CommandResult("GT_SetJogPrm", sResult))
			{
				return RET_FAIL;
			}
			sResult = mc.GT_SetVel(car, axis, vel);
			if (!CommandResult("GT_SetVel", sResult))
			{
				return RET_FAIL;
			}

			sResult = mc.GT_Update(car, 1 << (axis - 1));
			if (!CommandResult("GT_Update", sResult))
			{
				return RET_FAIL;
			}
			return RET_SUCCESS;
		}
		//判断轴返回状态
		private static int AxisStatusResult(short axis, int AxisStartus)
		{
			//if not return ret_fail means stop
			bool bResutl = false;
			if ((AxisStartus & 0x2) > 0)//伺服报警标志
			{
				MessageBoxLog.Show(axis.ToString() + "轴伺服报警\n");
				bResutl = true;
			}
			if ((AxisStartus & 0x10) > 0)//跟随误差越限标记
			{
				MessageBoxLog.Show(axis.ToString() + "轴运动出错\n");
				bResutl = true;
			}
			if ((AxisStartus & 0x20) > 0)//正向限位
			{
				//  MessageBoxLog.Show(axis.ToString()+"正限位触发\n");

			}
			if ((AxisStartus & 0x40) > 0)//负向限位
			{
				//MessageBoxLog.Show(axis.ToString()+"负限位触发\n");

			}
			if ((AxisStartus & 0x80) > 0)//平滑停止
			{
				MessageBoxLog.Show(axis.ToString() + "轴平滑停止\n");

				return 2;
			}
			if ((AxisStartus & 0x100) > 0)//急停标记
			{
				MessageBoxLog.Show(axis.ToString() + "轴急停触发\n");

				return 2;
			}
			if ((AxisStartus & 0x200) > 0)//伺服使能标记
			{
				//MessageBoxLog.Show(axis.ToString() + "伺服未使能\n");
				//bResutl = true;
			}
			if ((AxisStartus & 0x400) < 1)//规划器正在运动标记
			{
				// MessageBoxLog.Show("轴停止运动\n");

			}
			if (bResutl)
			{
				return RET_FAIL;
			}
			else
			{
				return RET_SUCCESS;
			}

		}
		private static TReturn DetectingAxis(short car, short axis)
		{//轴停止 return RET_SUCCESS else return RET_FAIL
			int axisStatus;
			uint sClock;
			short sResult;
			sResult = mc.GT_GetSts(car, axis, out axisStatus, 1, out sClock);
			//轴停止 10(400)   到位 11(800)
			//if ((axisStatus & 0x400) < 1 && (axisStatus & 0x800) > 0)
			//{
			//    return 1;
			//}
			if ((axisStatus & 0x400) < 1)
			{
				return RET_SUCCESS;
			}
			return RET_FAIL;
		}
		// stop axis
		private static TReturn StopAxisgts(TCard card, int mask, int option)
		{//   success return RET_SUCCESS
			short sResult;
			int AxisStatus;
			uint sClock;
			int iResult;
			int i;
			sResult = mc.GT_Stop(card, mask, option);
			if (!CommandResult("GT_Stop", sResult))
			{
				return RET_FAIL;
			}
			for (i = 1; i < 9; i++)
			{
				if (((mask >> (i - 1)) & 1) > 0)
				{
					while (true)//ok
					{
						sResult = mc.GT_GetSts(card, (short)i, out AxisStatus, 1, out sClock);
						if (!CommandResult("GT_GetSts", sResult))
						{
							return RET_FAIL;
						}
						if ((AxisStatus & 0x400) < 1)
						{
							break;
						}
						else
						{
							iResult = AxisStatusResult((short)(i), AxisStatus);
							if (iResult == RET_FAIL)
							{
								return RET_FAIL;
							}
						}
					}
				}
			}
			return RET_SUCCESS;

		}

		#region 回原函数
		//回原时高速查找原点、正限或负限
		//用于回原初段的高速搜索段
		// 可设置搜索方向，搜索的感应器，以及超时时间
		private static TReturn HomeSearchOriHighVel(TCard card, TAxis axis, TMode dir,
							bool bOriStop = true, bool bNegStop = true, bool bPosiStop = false, int timeoutSec = 60)
		{//return: 1 find ori.	2: find positive.  3: find Negative
			//return:-1 fail
			//dirNeg:true负向查找，false正向查找
			//bHighVel: ture high speed search, else low speed
			//bOriStop:是否查找原点退出，bNegStop:是否查找负限位退出，bPosiStop:是否查找正限退出
			short sResult;
			int axisStatus;
			uint pClock;
			short capture = 0;//捕获原点
			int pValue;

			HomeLog(card, axis, "HighVel_SearchOri_start_1！");
			sResult = mc.GT_GetDi(card, mc.MC_HOME, out pValue);
			if (!CommandResult("GT_GetDi", sResult))
			{
                SR_AxisStop(card, axis);
				return RET_FAIL;
			}
			#region 1.在原点
			if ((pValue & (1 << (axis - 1))) > 0)
			{
				HomeLog(card, axis, "HighVel_SearchOri_inOri_2！");
				if (bOriStop)
				{
					SR_AxisStop(card,axis);
					return FIND_ORI;
				}
			}
			else//跳出，2不在原点,找原点或限位
			{//
				HomeLog(card, axis, "SearchOri_not_inOri_3！");
				CAxisParams axParam = GetAxisSt(card, axis);
				if (dir == BAX.DIR_NEGATIVE )
				{
					sResult = AxisPrfTrap(card, axis, axParam.DisHomeNeg, axParam.VelHomeHigh);  //负向搜索
				}
				else
				{
					sResult = AxisPrfTrap(card, axis, axParam.DisHomePositive, axParam.VelHomeHigh);  //正向搜索
				}
				if (sResult == RET_FAIL)
				{
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				//清除轴状态和报警
				sResult = mc.GT_ClrSts(card, axis, 1);
				if (!CommandResult("GT_ClrSts", sResult))
				{
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				//设置HOME capture
				sResult = mc.GT_SetCaptureMode(card, axis, mc.CAPTURE_HOME);
				if (!CommandResult("GT_SetCaptureMode", sResult))
				{
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				//让轴到正、负极限位，原点退出
				DateTime dtForTimeout = DateTime.Now;
				while (bCardRunFlg)//
				{
					if ((DateTime.Now - dtForTimeout).TotalSeconds > timeoutSec)
					{
						HomeLog(card, axis, "HighVel_SearchOri_Timeout！");
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
                    sResult = mc.GT_GetDi(card, mc.MC_HOME, out pValue);
                    if (!CommandResult("GT_GetDi", sResult))
                    {
                        SR_AxisStop(card, axis);
                        return RET_FAIL;
                    }
                    if ((pValue & (1 << (axis - 1))) > 0)
                    {
                        HomeLog(card, axis, "HighVel_SearchOri_FindOri_4！");
                        if (bOriStop)
                        {
                            SR_AxisStop(card, axis);
                            return FIND_ORI;
                        }
                    }

					sResult = mc.GT_GetSts(card, axis, out axisStatus, 1, out pClock);
					if (!CommandResult("GT_GetSts", sResult))
					{
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
                    //sResult = mc.GT_GetCaptureStatus(card, axis, out capture, out pValue, 1, out pClock);
                    //if (!CommandResult("GT_GetCaptureStatus", sResult))
                    //{
                    //    return RET_FAIL;
                    //}
                    //if (capture == 1)
                    //{
                    //    if (bOriStop)
                    //    {
                    //        HomeLog(card, axis, "HighVel_SearchOri_find_ori_4！");
                    //        SR_AxisStop(card, axis);
                    //        return FIND_ORI;
                    //    }
                    //}
					if ((axisStatus & 0x20) == 32)  //正限位
					{
						if (bPosiStop)
						{
							HomeLog(card, axis, "HighVel_SearchOri_find_positive_5！");
							SR_AxisStop(card, axis);
							return FIND_POSITIVE;
						}
					}
					if ((axisStatus & 0x40) == 64) //负限位
					{
						if (bNegStop)
						{
							HomeLog(card, axis, "HighVel_SearchOri_find_negative_6！");
							SR_AxisStop(card, axis);
							return FIND_NEG;
						}
					}
					if ((axisStatus & 0x2) > 0)
					{
						HomeLog(card, axis, "HighVel_SearchOri_axis_alarm_7！");
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
					if ((axisStatus & 0x10) > 0)
					{
						HomeLog(card, axis, "HighVel_SearchOri_axis_error_8！");
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
					if ((axisStatus & 0x80) > 0)
					{
						HomeLog(card, axis, "HighVel_SearchOri_轴平滑停止9！");
						return RET_FAIL;
					}
					if ((axisStatus & 0x100) > 0)
					{
						HomeLog(card, axis, "HighVel_SearchOri_轴急停触发10！");
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
					Thread.Sleep(5);
				}
			}

			#endregion
			return RET_FAIL;
		}
		//回原时慢速查找原点、正限或负限，并回到该点
		private static TReturn HomeSearchOriLowVel(TCard card, TAxis axis, TMode dir,
							int findID, int timeoutSec = 60)
		{
			//return: RET_FAIL失败，RET_SUSS成功
			//dirNeg:true负向查找，false正向查找
			//bOriStop:是否查找原点退出，bNegStop:是否查找负限位退出，bPosiStop:是否查找正限退出

			short sResult;
			int axisStatus;
			uint pClock;
			short capture = 0;
			int pValue = 0;
			CAxisParams axParam = GetAxisSt(card, axis);
			#region 查找
			//设置轴运动速度
            //sResult = mc.GT_SetVel(card, axis, axParam.VelHomeLow);
            //if (!CommandResult("GT_SetVel", sResult))
            //{
            //    return RET_FAIL;
            //}
			if(findID==FIND_POSITIVE)
			{
                //sResult = mc.GT_SetPos(card, axis, axParam.DisHomePositive);//正向找正限
                sResult = AxisPrfTrap(card, axis, axParam.DisHomePositive , axParam.VelHomeLow);  // 搜索
			}
			else if(findID==FIND_NEG)
			{
                //sResult = mc.GT_SetPos(card, axis, axParam.DisHomeNeg );//负向找负限
                sResult = AxisPrfTrap(card, axis, axParam.DisHomeNeg, axParam.VelHomeLow);  // 搜索
			}
			else
			{
				if(dir==BAX.DIR_NEGATIVE )
				{
                    //sResult = mc.GT_SetPos(card, axis, axParam.DisHomeNeg );//正向找正向
                    sResult = AxisPrfTrap(card, axis, axParam.DisHomeNeg, axParam.VelHomeLow);  // 搜索
				}
				else
				{
                    //sResult = mc.GT_SetPos(card, axis, axParam.DisHomePositive);//正向找正向
                    sResult = AxisPrfTrap(card, axis, axParam.DisHomePositive, axParam.VelHomeLow);  // 搜索
				}
			}

			if (!CommandResult("GT_SetPos", sResult))
			{
				return RET_FAIL;
			}
            ////清除轴状态和报警
            //sResult = mc.GT_ClrSts(card, axis, 1);
            //if (!CommandResult("GT_ClrSts", sResult))
            //{
            //    return RET_FAIL;
            //}
            ////启动轴运动
            //sResult = mc.GT_Update(card, 1 << (axis - 1));
            //if (!CommandResult("GT_Update", sResult))
            //{
            //    return RET_FAIL;
            //}
			//启动capture
			if(findID==FIND_ORI )
			{
				sResult = mc.GT_SetCaptureMode(card, axis, mc.CAPTURE_HOME);
				if (!CommandResult("GT_SetCaptureMode", sResult))
				{
					return RET_FAIL;
				}
			}

			//搜索
			while (bCardRunFlg)//!!!!
			{
				sResult = mc.GT_GetSts(card, axis, out axisStatus, 1, out pClock);
				if (!CommandResult("GT_GetSts", sResult))
				{
					return RET_FAIL;
				}
				if (findID==FIND_ORI )
				{
					sResult = mc.GT_GetCaptureStatus(card, axis, out capture, out pValue, 1, out pClock);
                    int aaa = pValue;
                    //if (axis == 3)
                    //{
                        double pV;
                        double dPostValue;
                        mc.GT_GetAxisPrfPos(card, (short)axis, out dPostValue, 1, out pClock);
                        sResult = mc.GT_GetAxisPrfPos(card, axis, out pV, 1, out pClock);

                        pValue = (int)dPostValue;
                        if (!CommandResult("GT_GetEncPos", sResult))
                        {
                            return RET_FAIL;
                        }
                    //}
					if (!CommandResult("GT_GetCaptureStatus", sResult))
					{
						return RET_FAIL;
					}
					if (capture == 1)
					{
                        SR_AxisStop(card, axis);
						HomeLog(card, axis, "lowVelSearch_找到原点位，原点位坐标18：" + pValue.ToString());
						break;
					}
				}

				if (findID==FIND_POSITIVE )
				{
					if ((axisStatus & 0x20) == 32)  //正限位
					{
						double pV = 0;
						sResult = mc.GT_GetAxisPrfPos(card, axis, out pV, 1, out pClock);
						pValue = (int)pV;
						if (!CommandResult("GT_GetAxisPrfPos", sResult))
						{
							return RET_FAIL;
						}
						HomeLog(card, axis, "lowVelSearch_找到正限位，正限位坐标：" + pValue.ToString());
						break;
					}
				}
				if (findID==FIND_NEG )
				{
					if ((axisStatus & 0x40) == 64) //负限位
					{
						double pV = 0;
						sResult = mc.GT_GetAxisPrfPos(card, axis, out pV, 1, out pClock);
						pValue = (int)pV;
						if (!CommandResult("GT_GetAxisPrfPos", sResult))
						{
							return RET_FAIL;
						}
						HomeLog(card, axis, "lowVelSearch_找到负限位，负限位坐标：" + pValue.ToString());
						break;
					}
				}

				if ((axisStatus & 0x2) > 0)
				{
					HomeLog(card, axis, "lowVelSearch_轴伺服报警!");
					return RET_FAIL;
				}
				if ((axisStatus & 0x10) > 0)
				{
					HomeLog(card, axis, "lowVelSearch_轴运动出错!");
					return RET_FAIL;
				}
				if ((axisStatus & 0x80) > 0)
				{
					HomeLog(card, axis, "lowVelSearch_轴平滑停止!");
					return RET_FAIL;
				}
				if ((axisStatus & 0x100) > 0)
				{
					HomeLog(card, axis, "lowVelSearch_轴急停触发!");
					return RET_FAIL;
				}
				if ((axisStatus & 0x400) < 1)
				{
					HomeLog(card, axis, "lowVelSearch_轴停止运动,未找到原点信号!");
					if ((axisStatus & 0x800) > 0)
					{
						HomeLog(card, axis, "lowVelSearch_轴运动到位，未找到信号!");
					}
					return RET_FAIL;
				}
				Thread.Sleep(5);
			}

            //if (axParam.ax.IsServe == false)//步进不捕获
            //{
            //    sResult = mc.GT_ZeroPos(card, axis, 1);
            //    if (!CommandResult("GT_ZeroPos", sResult))
            //    {
            //        return RET_FAIL;
            //    }
            //    return RET_SUCCESS;
            //}

			#endregion
            double pV1;
            sResult = mc.GT_GetAxisPrfPos(card, axis, out pV1, 1, out pClock);
            pValue = (int)pV1;
            CtrlCardSR.SR_AbsoluteMove (card, axis, pValue, axParam.VelHomeLow);
            //sResult = mc.GT_SetPos(card, axis, pValue);
            //if (!CommandResult("GT_SetPos", sResult))
            //{
            //    return RET_FAIL;
            //}
            ////清除轴状态和报警
            //sResult = mc.GT_ClrSts(card, axis, 1);
            //if (!CommandResult("GT_ClrSts", sResult))
            //{
            //    return RET_FAIL;
            //}
            ////启动轴运动
            //sResult = mc.GT_Update(card, 1 << (axis - 1));
            //if (!CommandResult("GT_Update", sResult))
            //{
            //    return RET_FAIL;
            //}
			while (true)//added
			{
				sResult = mc.GT_GetSts(card, axis, out axisStatus, 1, out pClock);
				if (!CommandResult("GT_GetSts", sResult))
				{
					return RET_FAIL;
				}
				if(canRun()==false)
				{
					return RET_FAIL;
				}
				if ((axisStatus & 0x400) < 1)
				{
					HomeLog(card, axis, "lowVelSearch_复位完成!");
					break;
				}
				Thread.Sleep(10);
			}
			Thread.Sleep(100);
			//坐标清0
			sResult = mc.GT_ZeroPos(card, axis, 1);
			if (!CommandResult("GT_ZeroPos", sResult))
			{
				return RET_FAIL;
			}
			return RET_SUCCESS;
		}





		//回原时离开原点 1001 04
		private static TReturn HomeLeaveSensor1(TCard card, TAxis axis, TMode dir, int leaveID, int timeoutSec = 60)
		{// not in ori return RET_SUCCESS
			//bDirNeg: true时负向离开 false正向离开.  只有FIND_ORI有效，其他两种模式自指定方向
			//leaveID : FIND_NEG,FIND_POSITIVE,FIND_ORI
			//timeoutSec 超时时间，单位秒
			//leaveID: FIND_NEG 离开负限位， FIND_POSI 离开正限位，其他 离开原点
			short sResult;
			int axisStatus;
			uint pClock;
			int pValue;
			CAxisParams axParam = GetAxisSt(card, axis);
			HomeLog(card, axis, "LeaveSensor_start_1！");
			if (leaveID == FIND_NEG)
			{
				sResult = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out pValue);
			}
			else if (leaveID == FIND_POSITIVE)
			{
				sResult = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out pValue);
			}
			else
			{
				sResult = mc.GT_GetDi(card, mc.MC_HOME, out pValue);
			}
			if (!CommandResult("GT_GetDi", sResult))
			{
                SR_AxisStop(card, axis);
				return RET_FAIL;
			} 

			if ((pValue & (1 << (axis - 1))) > 0)
			{
				HomeLog(card, axis, "LeaveSensor_inPos_2！");
			}
			else
			{
				HomeLog(card, axis, "HomeLeaveSensor_notinPos_leaved_3！");
                SR_AxisStop(card, axis);
				return RET_SUCCESS; //不在该位，认为已离开
			}

			#region 在原点或找到原点，离开原点
			StopAxis(card, axis, 0);
			//设置轴运动速度
			Thread.Sleep(10);//0809 not delay may can't start move
			sResult = mc.GT_SetVel(card, axis, axParam.VelHomeLow);
			if (!CommandResult("GT_SetVel", sResult))
			{
                SR_AxisStop(card, axis);
                //return RET_FAIL;\\1004出错屏蔽
			}
			if (leaveID == FIND_POSITIVE)
			{
				sResult = mc.GT_SetPos(card, axis, axParam.DisHomeNeg);//离开正限，往负向离开
			}
			else if (leaveID == FIND_NEG)
			{
				sResult = mc.GT_SetPos(card, axis, axParam.DisHomePositive);//往正向离开
			}
			else//离开原点
			{
				if (dir==BAX.DIR_NEGATIVE )
				{
					sResult = mc.GT_SetPos(card, axis, axParam.DisHomeNeg);//往负向离开
				}
				else
				{
					sResult = mc.GT_SetPos(card, axis, axParam.DisHomePositive);//往正向离开
				}
			}
			if (!CommandResult("GT_SetPos", sResult))
			{
                SR_AxisStop(card, axis);
				return RET_FAIL;
			}
			//清除轴状态和报警
			sResult = mc.GT_ClrSts(card, axis, 1);
			if (!CommandResult("GT_ClrSts", sResult))
			{
                SR_AxisStop(card, axis);
				return RET_FAIL;
			}
			//启动轴运动
			sResult = mc.GT_Update(card, 1 << (axis - 1));
			if (!CommandResult("GT_Update", sResult))
			{
                SR_AxisStop(card, axis);
				return RET_FAIL;
			}
			//让轴运动出原点位
			DateTime dtForTimeout = DateTime.Now;
			while (bCardRunFlg)//!!!!
			{
				if ((DateTime.Now - dtForTimeout).TotalSeconds > timeoutSec)
				{
					HomeLog(card, axis, "HomeLeaveSensor_timeout_4！");
				}
				sResult = mc.GT_GetSts(card, axis, out axisStatus, 1, out pClock);
				if (!CommandResult("GT_GetSts", sResult))
				{
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if (leaveID == FIND_NEG)
				{
					sResult = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out pValue);
				}
				else if (leaveID == FIND_POSITIVE)
				{
					sResult = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out pValue);
				}
				else
				{
					sResult = mc.GT_GetDi(card, mc.MC_HOME, out pValue);
				}

				if (!CommandResult("GT_GetCaptureStatus", sResult))
				{
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if ((pValue & (1 << (axis - 1))) < 1)
				{
					HomeLog(card, axis, "HomeLeaveSensor_Leaved_5！");
                    SR_AxisStop(card, axis);
					return RET_SUCCESS;
				}
				if (leaveID != FIND_POSITIVE)
				{
					if ((axisStatus & 0x20) == 32)  //正限位
					{
						HomeLog(card, axis, "HomeLeaveSensor_active_positive_6！");
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
				}
				if (leaveID != FIND_NEG)
				{
					if ((axisStatus & 0x40) == 64) //负限位
					{
						HomeLog(card, axis, "HomeLeaveSensor_active_negative_7！");
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
				}
				if ((axisStatus & 0x2) > 0)
				{
					HomeLog(card, axis, "HomeLeaveSensor_axis_alarm_8！");
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if ((axisStatus & 0x10) > 0)
				{
					HomeLog(card, axis, "HomeLeaveSensor_active_轴运动出错_9！");
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if ((axisStatus & 0x80) > 0)
				{
					HomeLog(card, axis, "HomeLeaveSensor_active_轴平滑停止_10！");
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if ((axisStatus & 0x100) > 0)
				{
					HomeLog(card, axis, "HomeLeaveSensor_active_轴急停触发_11！");
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				Thread.Sleep(5);
			}
			#endregion
            SR_AxisStop(card, axis);
			return RET_FAIL;
		}
		//回原时离开原点
		private static TReturn HomeLeaveSensor(TCard card, TAxis axis, TMode dir, int leaveID, int timeoutSec = 60)
		{// not in ori return RET_SUCCESS
			//bDirNeg: true时负向离开 false正向离开.  只有FIND_ORI有效，其他两种模式自指定方向
			//leaveID : FIND_NEG,FIND_POSITIVE,FIND_ORI
			//timeoutSec 超时时间，单位秒
			//leaveID: FIND_NEG 离开负限位， FIND_POSI 离开正限位，其他 离开原点
			short sResult;
			int axisStatus;
			uint pClock;
			int pValue;
			CAxisParams axParam = GetAxisSt(card, axis);
			HomeLog(card, axis, "LeaveSensor_start_1！");
			if (leaveID == FIND_NEG)
			{
				sResult = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out pValue);
			}
			else if (leaveID == FIND_POSITIVE)
			{
				sResult = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out pValue);
			}
			else
			{
				sResult = mc.GT_GetDi(card, mc.MC_HOME, out pValue);
			}
			if (!CommandResult("GT_GetDi", sResult))
			{
                SR_AxisStop(card, axis);
				return RET_FAIL;
			} 

			if ((pValue & (1 << (axis - 1))) > 0)
			{
				HomeLog(card, axis, "LeaveSensor_inPos_2！");
			}
			else
			{
				HomeLog(card, axis, "HomeLeaveSensor_notinPos_leaved_3！");
                SR_AxisStop(card, axis);
				return RET_SUCCESS; //不在该位，认为已离开
			}

			#region 在原点或找到原点，离开原点
			StopAxis(card, axis, 0);
			//设置轴运动速度
			Thread.Sleep(10);//0809 not delay may can't start move
            //sResult = mc.GT_SetVel(card, axis, axParam.VelHomeLow);
            //if (!CommandResult("GT_SetVel", sResult))
            //{
            //    SR_AxisStop(card, axis);
            //    //return RET_FAIL;\\1004出错屏蔽
            //}
			if (leaveID == FIND_POSITIVE)
			{
                sResult = AxisPrfTrap(card, axis, axParam.DisHomeNeg, axParam.VelHomeLow);  //负向搜索 1001 04
                //sResult = SR_ContinueMove(card, axis, axParam.VelHomeLow, BAX.DIR_NEGATIVE );
                //sResult = mc.GT_SetPos(card, axis, axParam.DisHomeNeg);//离开正限，往负向离开
			}
			else if (leaveID == FIND_NEG)
			{
                sResult = AxisPrfTrap(card, axis, axParam.DisHomePositive , axParam.VelHomeLow);  // 搜索
                //sResult = SR_ContinueMove(card, axis, axParam.VelHomeLow, BAX.DIR_POSITIVE );
                //sResult = mc.GT_SetPos(card, axis, axParam.DisHomePositive);//往正向离开
			}
			else//离开原点
			{
				if (dir==BAX.DIR_NEGATIVE )
				{
                    sResult = AxisPrfTrap(card, axis, axParam.DisHomeNeg, axParam.VelHomeLow);  // 搜索
                    //sResult = SR_ContinueMove(card, axis, axParam.VelHomeLow, BAX.DIR_NEGATIVE);
                    //sResult = mc.GT_SetPos(card, axis, axParam.DisHomeNeg);//往负向离开
				}
				else
				{
                    sResult = AxisPrfTrap(card, axis, axParam.DisHomePositive, axParam.VelHomeLow);  // 搜索
                    //sResult = SR_ContinueMove(card, axis, axParam.VelHomeLow, BAX.DIR_POSITIVE);
                    //sResult = mc.GT_SetPos(card, axis, axParam.DisHomePositive);//往正向离开
				}
			}
            //if (!CommandResult("GT_SetPos", sResult))
            //{
            //    SR_AxisStop(card, axis);
            //    return RET_FAIL;
            //}
			//清除轴状态和报警
            //sResult = mc.GT_ClrSts(card, axis, 1);
            //if (!CommandResult("GT_ClrSts", sResult))
            //{
            //    SR_AxisStop(card, axis);
            //    return RET_FAIL;
            //}
            ////启动轴运动
            //sResult = mc.GT_Update(card, 1 << (axis - 1));
            //if (!CommandResult("GT_Update", sResult))
            //{
            //    SR_AxisStop(card, axis);
            //    return RET_FAIL;
            //}
			//让轴运动出原点位
			DateTime dtForTimeout = DateTime.Now;
			while (bCardRunFlg)//!!!!
			{
				if ((DateTime.Now - dtForTimeout).TotalSeconds > timeoutSec)
				{
					HomeLog(card, axis, "HomeLeaveSensor_timeout_4！");
				}
				sResult = mc.GT_GetSts(card, axis, out axisStatus, 1, out pClock);
				if (!CommandResult("GT_GetSts", sResult))
				{
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if (leaveID == FIND_NEG)
				{
					sResult = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out pValue);
				}
				else if (leaveID == FIND_POSITIVE)
				{
					sResult = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out pValue);
				}
				else
				{
					sResult = mc.GT_GetDi(card, mc.MC_HOME, out pValue);
				}

				if (!CommandResult("GT_GetCaptureStatus", sResult))
				{
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if ((pValue & (1 << (axis - 1))) < 1)
				{
					HomeLog(card, axis, "HomeLeaveSensor_Leaved_5！");
                    SR_AxisStop(card, axis);
					return RET_SUCCESS;
				}
				if (leaveID != FIND_POSITIVE)
				{
					if ((axisStatus & 0x20) == 32)  //正限位
					{
						HomeLog(card, axis, "HomeLeaveSensor_active_positive_6！");
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
				}
				if (leaveID != FIND_NEG)
				{
					if ((axisStatus & 0x40) == 64) //负限位
					{
						HomeLog(card, axis, "HomeLeaveSensor_active_negative_7！");
                        SR_AxisStop(card, axis);
						return RET_FAIL;
					}
				}
				if ((axisStatus & 0x2) > 0)
				{
					HomeLog(card, axis, "HomeLeaveSensor_axis_alarm_8！");
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if ((axisStatus & 0x10) > 0)
				{
					HomeLog(card, axis, "HomeLeaveSensor_active_轴运动出错_9！");
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if ((axisStatus & 0x80) > 0)
				{
					HomeLog(card, axis, "HomeLeaveSensor_active_轴平滑停止_10！");
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				if ((axisStatus & 0x100) > 0)
				{
					HomeLog(card, axis, "HomeLeaveSensor_active_轴急停触发_11！");
                    SR_AxisStop(card, axis);
					return RET_FAIL;
				}
				Thread.Sleep(5);
			}
			#endregion
            SR_AxisStop(card, axis);
			return RET_FAIL;
		}



		//负向+原点回原
		private static TReturn AxisHomeNeg_Ori(TCard card, TAxis axis )
		{//return RET_FAIL:fail RET_SUCCESS:success
			TReturn sResult;
			CAxisParams axParam = GetAxisSt(card, axis);
            //高速查找原点或限位
            HomeLog(card, axis, "负向+原点回原_高速查找感应器 ");
			sResult = HomeSearchOriHighVel(card, axis, BAX.DIR_NEGATIVE , true, true, false );
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "负向+原点回原_高速查找感应器失败");
				return RET_FAIL;
			}
            //高速查找原点
            HomeLog(card, axis, "负向+原点回原_高速查找原点");
			sResult = HomeSearchOriHighVel(card, axis, BAX.DIR_POSITIVE  , true, false , false );
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "负向+原点回原_高速查找原点失败");
				return RET_FAIL;
			}
			//离开原点
            HomeLog(card, axis, "负向+原点回原_离开原点");
			sResult = HomeLeaveSensor(card, axis, BAX.DIR_POSITIVE  , FIND_ORI );
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "负向+原点回原_离开原点失败");
				return RET_FAIL;
			}
            HomeLog(card, axis, "负向+原点回原_低速定位原点");
            //反向低速找原点
			sResult = HomeSearchOriLowVel(card, axis, BAX.DIR_NEGATIVE , FIND_ORI );
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "负向+原点回原_低速定位原点失败");
				return RET_FAIL;
			}
            Global.CFile.WriteTxtDate(string.Format("card{0},axis{1},offStart",card,axis), "homeTrack");
			AxisPMoveAbsoluteToStop(card, axis, axParam.mm_to_pulse( axParam .homeOffset ), axParam.VelAuto/3);//走偏移位
            Global.CFile.WriteTxtDate(string.Format("card{0},axis{1},offStop", card, axis), "homeTrack");
			return RET_SUCCESS;
		}
		//正向+原点回原
		private static TReturn AxisHomePOSI_Ori(TCard card, TAxis axis  )
		{//return RET_FAIL:fail RET_SUCCESS:success
			TReturn sResult;
			CAxisParams axParam = GetAxisSt(card, axis);
            //高速查找原点或正限
			sResult = HomeSearchOriHighVel(card, axis, BAX.DIR_POSITIVE  , true, false , true , 10);
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "正向+原点回原_高速查找感应器失败");
				return RET_FAIL;
			}
            //高速查找原点
            sResult = HomeSearchOriHighVel(card, axis, BAX.DIR_NEGATIVE , true, false, false, 10);
            if (sResult == RET_FAIL)
            {
                HomeLog(card, axis, "负向+原点回原_高速查找原点失败");
                return RET_FAIL;
            }
			//离开原点
			sResult = HomeLeaveSensor(card, axis, BAX.DIR_NEGATIVE , FIND_ORI, 10);
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "正向+原点回原_离开原点失败");
				return RET_FAIL;
			}
			sResult = HomeSearchOriLowVel(card, axis, BAX.DIR_POSITIVE  , FIND_ORI, 10);
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "正向+原点回原_低速定位原点失败");
				return RET_FAIL;
			}
			AxisPMoveAbsoluteToStop(card, axis, axParam.mm_to_pulse( axParam.homeOffset ), axParam.VelAuto/3);//走偏移位
			return RET_SUCCESS;
		}

		//原点回原 
		private static TReturn AxisHomeOri(TCard card, TAxis axis, TMode  nDir  )
		{//return RET_FAIL:fail RET_SUCCESS:success

            TReturn sResult;
            CAxisParams axParam = GetAxisSt(card, axis);
            //高速查找原点或限位
            HomeLog(card, axis, "原点回原_高速查找感应器 ");
            sResult = HomeSearchOriHighVel(card, axis, BAX.DIR_NEGATIVE, true, true, false );
            if (sResult == RET_FAIL)
            {
                HomeLog(card, axis, "原点回原_高速查找感应器失败");
                return RET_FAIL;
            }
            //高速查找原点
            HomeLog(card, axis, "原点回原_高速查找原点");
            sResult = HomeSearchOriHighVel(card, axis, BAX.DIR_POSITIVE, true, false, false );
            if (sResult == RET_FAIL)
            {
                HomeLog(card, axis, "原点回原_高速查找原点失败");
                return RET_FAIL;
            }
            //离开原点
            HomeLog(card, axis, "原点回原_离开原点");
            sResult = HomeLeaveSensor(card, axis, BAX.DIR_POSITIVE, FIND_ORI );
            if (sResult == RET_FAIL)
            {
                HomeLog(card, axis, "原点回原_离开原点失败");
                return RET_FAIL;
            }
            HomeLog(card, axis, "原点回原_低速定位原点");
            //反向低速找原点
            sResult = HomeSearchOriLowVel(card, axis, BAX.DIR_NEGATIVE, FIND_ORI );
            if (sResult == RET_FAIL)
            {
                HomeLog(card, axis, "原点回原_低速定位原点失败");
                return RET_FAIL;
            }
            AxisPMoveAbsoluteToStop(card, axis, axParam.mm_to_pulse(axParam.homeOffset ), axParam.VelAuto / 3);//走偏移位
            return RET_SUCCESS;

            //TReturn sResult;
            //CAxisParams axParam = GetAxisSt(card, axis);
            //TMode dirOppsite=(nDir==BAX.DIR_POSITIVE)?BAX.DIR_NEGATIVE:BAX.DIR_POSITIVE;
            //sResult = HomeSearchOriHighVel(card, axis, nDir, true, true, false, 100);
            //if (sResult == RET_FAIL)
            //{
            //    HomeLog(card, axis, "原点回原_高速查找感应器失败");
            //    return RET_FAIL;
            //}
            ////FIND_ORI
            //sResult = HomeLeaveSensor(card, axis, dirOppsite, FIND_ORI, 100);
            //if (sResult == RET_FAIL)
            //{
            //    HomeLog(card, axis, "原点回原_离开原点失败");
            //    return RET_FAIL;
            //}
            //sResult = HomeSearchOriLowVel(card, axis, nDir, FIND_ORI, 100);
            //if (sResult == RET_FAIL)
            //{
            //    HomeLog(card, axis, "原点回原_低速定位原点失败");
            //    return RET_FAIL;
            //}
            //AxisPMoveAbsoluteToStop(card, axis, offsetpos, axParam.VelAuto/3);//走偏移位
            //return RET_SUCCESS;
		}
		//找负限位回原
		private static TReturn AxisHomeNeg(TCard card, TAxis axis )
		{
			TReturn sResult;
			CAxisParams axParam = GetAxisSt(card, axis);
			sResult = HomeSearchOriHighVel(card, axis, BAX.DIR_NEGATIVE , false, true, false, 10);
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "负限位回原_高速查找负向感应器失败");
				return RET_FAIL;
			}
			//leave
			sResult = HomeLeaveSensor(card, axis, BAX.DIR_POSITIVE  , FIND_NEG , 10);
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "负限位回原_离开负限失败");
				return RET_FAIL;
			}
			AxisPMoveAbsoluteToStop(card, axis, axParam.mm_to_pulse( axParam.homeOffset) , axParam.VelAuto / 3);//走偏移位
			return RET_SUCCESS;
		}
		//找正限位回原
		private static TReturn AxisHomePositive(TCard card, TAxis axis   )
		{
			TReturn sResult;
			CAxisParams axParam = GetAxisSt(card, axis);
			sResult = HomeSearchOriHighVel(card, axis, BAX.DIR_POSITIVE , false, false, true, 10);
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "正限位回原_高速查找正向感应器失败");
				return RET_FAIL;
			}
			//leave
			sResult = HomeLeaveSensor(card, axis, BAX.DIR_NEGATIVE , FIND_POSITIVE  , 10);
			if (sResult == RET_FAIL)
			{
				HomeLog(card, axis, "正限位回原_离开正限失败");
				return RET_FAIL;
			}
			AxisPMoveAbsoluteToStop(card, axis, axParam.mm_to_pulse( axParam.homeOffset) , axParam.VelAuto / 3);//走偏移位
			return RET_SUCCESS;
		}


		#endregion

		//停止单轴
		private static TReturn StopAxis(TCard card, TAxis axis, int option = 0)
		{//card start with 0
			//axis start with 1
			return StopAxisgts(card, 1 << (axis - 1), 0);

		}
		//单轴绝对定位，以及等待运动停止
		private static int AxisPMoveAbsoluteToStop(TCard card, short axis, double postion, double vel, double TimeOut = 100)
		{
			int iResult;
			int nPosition = (int)(postion);
			// vel = Convert.ToDouble(runingVelRYtxt.Text.ToString());
			iResult = AxisPrfTrap(card, axis, nPosition, vel);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}
			DateTime axisStartTime = DateTime.Now;
			Thread.Sleep(10);
			while (canRun())
			{
				iResult = DetectingAxis(card, axis);
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
			return RET_FAIL;
		}

		#endregion



		#region 基本函数
		#region add
		public static TReturn SR_SetPosZero(TCard card, TAxis axis)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_ZeroPos(card, axis, 1);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_ZeroPos", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		#endregion

		//卡初始化 返回卡数量 默认2主卡+1IO扩展
		public static TReturn SR_InitCard()
		{
			return AxisInit();
		}
		//关闭控制卡 默认2主卡+1IO扩展
		public static TReturn SR_CloseCard()
		{//1 success . 0 fail
			TReturn sRtn;
			TReturn nReturn = RET_SUCCESS;
			sRtn = mc.GT_CloseExtMdlGts(0);
			if (!CommandResult("GT_CloseExtMdlGts", sRtn))
			{
				nReturn = RET_FAIL;
			}
			sRtn = mc.GT_Close(0);
			if (!CommandResult("GT_Close", sRtn))
			{
				nReturn = RET_FAIL;
			}
			sRtn = mc.GT_Close(1);
			if (!CommandResult("GT_Close", sRtn))
			{
				nReturn = RET_FAIL;
			}
			return nReturn;
		}
		//限位设置：bEnable:true:有效，false：无效
		public static TReturn SR_SetLimitEnable(TCard card, TAxis axis, bool bEnable)
		{
			TReturn ret = RET_SUCCESS;
			if (bEnable)
			{
				ret = mc.GT_LmtsOn(card, axis, mc.MC_LIMIT_POSITIVE);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_LmtsOn", ret);
					ret = RET_FAIL;
				}
				ret = mc.GT_LmtsOn(card, axis, mc.MC_LIMIT_NEGATIVE);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_LmtsOn", ret);
					ret = RET_FAIL;
				}
			}
			else
			{
				ret = mc.GT_LmtsOff(card, axis, mc.MC_LIMIT_POSITIVE);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_LmtsOff", ret);
					ret = RET_FAIL;
				}
				ret = mc.GT_LmtsOff(card, axis, mc.MC_LIMIT_NEGATIVE);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_LmtsOff", ret);
					ret = RET_FAIL;
				}
			}
			return ret;
		}

		//限位触发电平设置//未完成//bMode:true:低电平，0：高电平
		public static TReturn SR_SetLimitMode(TCard card, TAxis axis, bool bMode)
		{
			TReturn ret = RET_SUCCESS;

			return ret;
		}

		//设置编码器计数方向//没有获取设置函数，只能整卡一起设置，false:正向，true:反向
		public static TReturn SR_SetEncSnsMode(TCard card, TAxis axis, bool bReverse)
		{
			TReturn ret = RET_SUCCESS;
			int retVal = 0xFFFF;
			bool[] bSet = { bReverse, bReverse, bReverse, bReverse, bReverse, bReverse, bReverse, bReverse };
			for (int i = 0; i < bSet.Length; i++)
			{
				if (!bSet[i])//正向设0
				{
					retVal &= ~(1 << i);
				}
			}
			ret = mc.GT_EncSns(card, (ushort)retVal);
			return ret;
		}
		//设置编码器计数方向//同时单独指定各轴，false:正向，true:反向
		public static TReturn SR_SetEncSnsMode(TCard card, bool bReverse0 = true, bool bReverse1 = true, bool bReverse2 = true,
				bool bReverse3 = true, bool bReverse4 = true, bool bReverse5 = true, bool bReverse6 = true, bool bReverse7 = true)
		{
			TReturn ret = RET_SUCCESS;
			int retVal = 0xFFFF;
			bool[] bSet = { bReverse0, bReverse1, bReverse2, bReverse3, bReverse4, bReverse5, bReverse6, bReverse7 };
			for (int i = 0; i < bSet.Length; i++)
			{
				if (!bSet[i])//正向设0
				{
					retVal &= ~(1 << i);
				}
			}
			ret = mc.GT_EncSns(card, (ushort)retVal);
			return ret;
		}
		//设置脉冲输出模式// bMode: true CCW/CW, false 脉冲+方向
		public static TReturn SR_PulseMode(TCard card, TAxis axis, bool bMode)
		{
			TReturn ret = RET_SUCCESS;
			if (bMode)
			{
				ret = mc.GT_StepPulse(card, axis);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_StepPulse", ret);
					ret = RET_FAIL;
				}
			}
			else
			{
				ret = mc.GT_StepDir(card, axis);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_StepDir", ret);
					ret = RET_FAIL;
				}
			}

			return ret;
		}
		//设置编码器计数方式  未完成 暂未找到函数
		public static TReturn SR_SetEncMode(TCard card, TAxis axis, bool bMode)
		{
			TReturn ret = RET_SUCCESS;
			//ret =mc.GT_EncSns(card,
			return ret;
		}
		//清除各轴异常状态 
		// count :读取的轴数 1表示1个轴
		public static TReturn SR_ClrStatus(TCard card, TAxis axis, TMode count = 1)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_ClrSts(card, axis, count);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_ClrSts", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//设置伺服on/off
		// bOn : ture on, false off
		public static TReturn SR_SetServoEnable(TCard card, TAxis axis, bool bOn)
		{
			TReturn ret = RET_SUCCESS;
			if (bOn)
			{
				ret = mc.GT_AxisOn(card, axis);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_AxisOn", ret);
					ret = RET_FAIL;
				}
			}
			else
			{
				ret = mc.GT_AxisOff(card, axis);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_AxisOff", ret);
					ret = RET_FAIL;
				}
			}

			return ret;
		}
		//设置伺服报警使能
		// bOn : ture 有效, false 无效
		public static TReturn SR_AlarmEnable(TCard card, TAxis axis, bool bOn)
		{
			TReturn ret = RET_SUCCESS;
			if (bOn)
			{
				ret = mc.GT_AlarmOn(card, axis);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_AlarmOn", ret);
					ret = RET_FAIL;
				}
			}
			else
			{
				ret = mc.GT_AlarmOff(card, axis);
				if (ret != RET_SUCCESS)
				{
					CommandResult("GT_AlarmOff", ret);
					ret = RET_FAIL;
				}
			}

			return ret;
		}
		//获取轴状态信息
		// axisStatus : 获取的轴状态
		public static TReturn SR_GetAxisStatus(TCard card, TAxis axis, out TPusle axisStatus)
		{
			TReturn ret = RET_SUCCESS;
			uint pClock;

			ret = mc.GT_GetSts(card, axis, out axisStatus, 1, out pClock);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetSts", ret);
				ret = RET_FAIL;
			}
			return ret;
		}

		//获取轴状态信息
		// statusFlag : 指定获取的轴状态
		//bStatus: 输出指定的轴状态结果
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetAxisStatus(TCard card, TAxis axis, int statusFlag, out bool bStatus)
		{
			TReturn ret = RET_SUCCESS;
			TPusle axisStatus;
			bStatus = false;
			ret = SR_GetAxisStatus(card, axis, out axisStatus);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetSts", ret);
				bStatus = false;
				ret = RET_FAIL;
			}
			switch (statusFlag)
			{
				case BAX.STS_FLAG_ALARM:
					bStatus = ((axisStatus & 0x2) < 1) ? false : true;
					break;
				case BAX.STS_FLAG_MERROR:
					bStatus = ((axisStatus & 0x10) < 1) ? false : true;
					break;
				case BAX.STS_FLAG_LIMIT_POSITIVE:
					bStatus = ((axisStatus & 0x20) < 1) ? false : true;
					break;
				case BAX.STS_FLAG_LIMIT_NEGATIVE:
					bStatus = ((axisStatus & 0x40) < 1) ? false : true;
					break;
				case BAX.STS_FLAG_STOP_SMOOTH:
					bStatus = ((axisStatus & 0x80) < 1) ? false : true;
					break;
				case BAX.STS_FLAG_STOP_EMG:
					bStatus = ((axisStatus & 0x100) < 1) ? false : true;
					break;
				case BAX.STS_FLAG_SEVER_ON:
					bStatus = ((axisStatus & 0x200) < 1) ? false : true;
					break;
				case BAX.STS_FLAG_MOTION:
					bStatus = ((axisStatus & 0x400) < 1) ? false : true;
					break;
				case BAX.STS_FLAG_INPOSITION:
					bStatus = ((axisStatus & 0x800) < 1) ? false : true;
					break;
				default:
					ret = RET_FAIL;
					MessageBoxLog.Show("SR_GetAxisStatus指令输入未知的轴状态");
					break;
			}

			return ret;
		}
		//获取轴运动状态  未完成
		// bAxisRunStatus : 运动中ture, else false
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetRunStatus(TCard card, TAxis axis, out bool bAxisRunStatus)
		{
			TReturn ret = RET_SUCCESS;
			ret = SR_GetAxisStatus(card, axis, BAX.STS_FLAG_MOTION, out bAxisRunStatus);
			if (ret != RET_SUCCESS)
			{
				CommandResult("SR_GetAxisStatus", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//设置单轴停止,等待停止
		// bAxisRunStatus : 运动中ture, else false
		public static TReturn SR_AxisStop(TCard card, TAxis axis)
		{
			return StopAxisgts(card, 1 << (axis - 1), 0);
		}
		//设置单轴停止，不等待停止
		// bAxisRunStatus : 运动中ture, else false
		public static TReturn SR_AxisStopNoStop(TCard card, TAxis axis)
		{
			return mc.GT_Stop(card, 1 << (axis - 1), 0);
		}
		//获取原点输入状态 单轴
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetOriginInput(TCard card, TAxis axis, out bool bStatus)
		{
			TReturn ret = RET_SUCCESS;
			int AxisBit;
			ret = mc.GT_GetDi(card, mc.MC_HOME, out AxisBit);//原点输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			bStatus = (AxisBit & (1 << (axis - 1))) > 0 ? true : false;
			return ret;
		}
		//获取原点输入状态 单卡
		//AxisBit 0-7bit 表示轴1-8对应的信号
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的AxisBit结果
		public static TReturn SR_GetOriginInput(TCard card, out int AxisBit)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_GetDi(card, mc.MC_HOME, out AxisBit);//原点输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//获取极限输入状态 获取单轴
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetLimitInput(TCard card, TAxis axis, out bool bStatusPositive, out bool bStatusNegative)
		{
			TReturn ret = RET_SUCCESS;
			int AxisBit;
			ret = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out AxisBit);//原点输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			bStatusPositive = (AxisBit & (1 << (axis - 1))) > 0 ? true : false;
			ret = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out AxisBit);//原点输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			bStatusNegative = (AxisBit & (1 << (axis - 1))) > 0 ? true : false;
			return ret;
		}
		//获取极限输入状态 获取单卡
		//bStatusPositive、bStatusNegative  0-7bit 表示轴1-8对应的信号
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetLimitInput(TCard card, TAxis axis, out int bStatusPositive, out int bStatusNegative)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out bStatusPositive);//原点输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			ret = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out bStatusNegative);//原点输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//获取输入信号 单点
		//point: 0开始， 0-15
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetInput(TCard card, TIOPoint point, out bool bStatus)
		{
			TReturn ret = RET_SUCCESS;
			int IOBit;
			ret = mc.GT_GetDi(card, mc.MC_GPI, out IOBit);//AxisDi输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			if (point < 16)
			{
				bStatus = (IOBit & (1 << point)) <= 0 ? true : false;
			}
			else
			{
				bStatus = false;
				MessageBoxLog.Show("读取IO传入错误的参数");
				System.Diagnostics.Debug.Assert(false);
			}
			return ret;
		}
		//获取输入信号 单卡
		//IOBit  0-15bit 对应1-15点
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetInput(TCard card, TIOPoint point, out int IOBit)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_GetDi(card, mc.MC_GPO, out IOBit);//AxisDi输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//设置输出信号 单点
		//point 0开始， 0-15
		public static TReturn SR_SetOutput(TCard card, TIOPoint point, TReturn bOut)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_SetDoBit(card, mc.MC_GPO, (short)(point + 1), bOut);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_SetDoBit", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//设置输入信号 单卡
		//setValue  0-15bit 对应1-15点
		public static TReturn SR_SetOutput(TCard card, TIOPoint point, int setValue)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_SetDo(card, mc.MC_GPO, setValue);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_SetDo", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//获取输出信号 单点
		//point: 0开始， 0-15
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetOutput(TCard card, TIOPoint point, out bool bStatus)
		{
			TReturn ret = RET_SUCCESS;
			int IOBit;
			ret = mc.GT_GetDi(card, mc.MC_GPO, out IOBit);//AxisDi输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			if (point < 16)
			{
				bStatus = (IOBit & (1 << point)) <= 0 ? true : false;
			}
			else
			{
				bStatus = false;
				MessageBoxLog.Show("读取IO传入错误的参数");
				System.Diagnostics.Debug.Assert(false);
			}
			return ret;
		}
		//获取输出信号 单卡
		//IOBit  0-15bit 对应1-15点
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_GetOutput(TCard card, TIOPoint point, out int IOBit)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_GetDi(card, mc.MC_GPO, out IOBit);//AxisDi输入IO点
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetDi", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//轴是否运动中
		//bMoving:true 运动中 else false
		//IOBit  0-15bit 对应1-15点
		//执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
		public static TReturn SR_IsBusy(TCard card, TAxis axis, out bool bMoving)
		{
			TReturn ret = RET_SUCCESS;
			ret = SR_GetAxisStatus(card, axis, BAX.STS_FLAG_MOTION, out bMoving);
			if (ret != RET_SUCCESS)
			{
				CommandResult("SR_GetAxisStatus", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//stop return RET_SUCCESS else RET_FAIL
		public static TReturn SR_IsAxisStop(TCard card, TAxis axis)
		{
			return DetectingAxis(card, axis);
		}
		////回原 启动回原线程
		//public static TReturn SR_GoHome(TCard card, TAxis axis, int offsetpos = 3)
		//{
		//    if (GetHoming(card, axis))
		//    {
		//        return RET_FAIL; //回原中
		//    }
		//    StartHome(card, axis);
		//    return RET_SUCCESS;
		//}
		//有回原中的轴将导致返回结果为失败
		public static TReturn SR_GoHome(TCard card,params TAxis []axises)
		{
			TReturn ret=RET_SUCCESS ;
			foreach (TAxis axis in axises)
			{
				if(GetHoming(card,axis))
				{
					ret= RET_FAIL;//回原中
				}
			}
			foreach( TAxis axis in axises)
			{
				if(GetHoming(card,axis)==false)
				{
					StartHome(card, axis);
				}		
			}
			return ret;
		}
		//非线程回原
		public static TReturn SR_GoHomeWait(TCard card,params TAxis []axises)
		{
			TReturn ret = RET_SUCCESS;
			foreach (TAxis axis in axises)
			{
				if (GetHoming(card, axis))
				{
					ret= RET_FAIL;//回原中
				}
			}
			foreach( TAxis axis in axises)
			{
				if(GetHoming(card,axis)==false)
				{
					GoHome(card, axis);
				}		
			}
			return ret;
		}

        //回原
		public  static TReturn GoHome(TCard card, TAxis axis )
		{
			if (GetAxisSt(card, axis).HomeMode == HMode.HOME_MODE_NEGATIVE)//负限回原
			{
                //CAxisParams axParam = GetAxisSt(card, axis);
                //SetHomeFlag(
                return AxisHomeNeg(card, axis );
			}
			else if (GetAxisSt(card, axis).HomeMode == HMode.HOME_MODE_NEG_ORI )//负限加原点
			{
                //if (AxisHomeOrLimit(card, axis) != RET_SUCCESS)
                //{
                //    return RET_FAIL;
                //}
                //return RET_SUCCESS; 
                return AxisHomeNeg_Ori(card, axis);
			}
			else if (GetAxisSt(card, axis).HomeMode == HMode.HOME_MODE_ORI )//原点回原
			{
                return AxisHomeOri(card, axis, BAX.DIR_NEGATIVE );
			}
			else if (GetAxisSt(card, axis).HomeMode == HMode.HOME_MODE_POSITIVE )//正限回原
			{
                return AxisHomePositive(card, axis );
			}
			else if (GetAxisSt(card, axis).HomeMode == HMode.HOME_MODE_POSITIVE_ORI  )//正限加原点
			{
                return AxisHomePOSI_Ori(card, axis );
			}
			return RET_FAIL;
		}
		//单轴相对运动
		//dist:距离
		//spd:速度
		public static TReturn SR_RelativeMove(TCard card, TAxis axis, TPusle dist, TSpeed spd)
		{
			mc.TTrapPrm pPrm;
			mc.GT_GetTrapPrm(card, axis, out pPrm);
			return AxisPrfTrapRel(card, axis, pPrm, dist, spd);
		}
		//单轴绝对运动
		//pos:要运动到的点位
		//spd:速度
		public static TReturn SR_AbsoluteMove(TCard card, TAxis axis, TPusle pos, TSpeed spd)
		{
			return AxisPrfTrap(card, axis, pos, spd);
		}
		//单轴连续运动 未完成
		//dir:方向 
		//spd:速度
		/// <summary>
		/// 单轴连续运动
		/// </summary>
		/// <param name="卡号"></卡号>
		/// <param name="axis"></轴号>
		/// <param name="spd"></速度>
		/// <param name="dir"></方向>
		/// <returns></returns>
		public static TReturn SR_ContinueMove(TCard card, TAxis axis, TSpeed spd, TMode dir)
		{
			spd = (dir == BAX .DIR_POSITIVE) ? spd : -spd;
			return AxisPrfJog(card, axis, spd);
		}
		//设置软限位
		public static TReturn SR_SetSoftLimit(TCard card, TAxis axis, TPusle positive, TPusle negative)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_SetSoftLimit(card, axis, positive, negative);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_SetSoftLimit", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		//设置加速度
		public static TReturn SR_SetAcc(CAxisParams stAxis, TPusle acc)
		{
			TReturn ret = RET_SUCCESS;
			try
			{
				stAxis.Acc = acc;
			}
			catch (Exception ex)
			{
				ret = RET_FAIL;
				MessageBoxLog.Show(ex.Message);
				System.Diagnostics.Debug.Assert(false);
			}
			return ret;
		}
		//设置减速度
		public static TReturn SR_SetDec(CAxisParams stAxis, TPusle dec)
		{
			TReturn ret = RET_SUCCESS;
			try
			{
				stAxis.Dec = dec;
			}
			catch (Exception ex)
			{
				ret = RET_FAIL;
				MessageBoxLog.Show(ex.Message);
				System.Diagnostics.Debug.Assert(false);
			}
			return ret;
		}
		//设置驱动速度
		public static TReturn SR_SetSpeed(CAxisParams stAxis, TPusle speed)
		{
			TReturn ret = RET_SUCCESS;
			try
			{
				stAxis.VelTarget = speed;
			}
			catch (Exception ex)
			{
				ret = RET_FAIL;
				MessageBoxLog.Show(ex.Message);
				System.Diagnostics.Debug.Assert(false);
			}
			return ret;
		}
		//设置启动速度
		public static TReturn SR_SetStartV(CAxisParams stAxis, TPusle startV)
		{
			TReturn ret = RET_SUCCESS;
			try
			{
				stAxis.VelStart = startV;
			}
			catch (Exception ex)
			{
				ret = RET_FAIL;
				MessageBoxLog.Show(ex.Message);
				System.Diagnostics.Debug.Assert(false);
			}
			return ret;
		}
		//
		private static string  GetHomeMode(TCard card, TAxis axis, TMode mode)
		{
			return AxisParams[BCARD.AXIS_QTY_SINGLE * card + axis].HomeMode;
		}
		public static TReturn SR_SetHomeMode(TCard card, TAxis axis, string  mode)
		{
			AxisParams[BCARD.AXIS_QTY_SINGLE * card + axis].HomeMode = mode;
			return RET_SUCCESS;
		}
		//设置加减速模式 未完成
		public static TReturn SR_SetAccDelMode(TCard card, TAxis axis, TMode mode)
		{

			TReturn ret = RET_SUCCESS;

			return ret;
		}
		//获取当前速度
		public static TReturn SR_GetSpeed(TCard card, TAxis axis, out TSpeed pos)
		{
			TReturn ret = RET_SUCCESS;
			uint pLock;
			ret = mc.GT_GetPrfVel(card, axis, out pos, 1, out pLock);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_SetSoftLimit", ret);
				ret = RET_FAIL;
			}
			return ret;
		}
		private static void InitCoordinate()
		{

		}

		//crdPrm 坐标结构购体
		//axisID 当前指定的轴号
		//axis：1坐标系X，2坐标系Y，3坐标系Z，4坐标系U。只有三种组合：XY，XYZ，XYZU
		public  static TReturn SR_SetcoordinateAxis(ref mc.TCrdPrm crdPrm,TAxis axisID, TAxis axisOrdinate)
		{
			if(axisID>8)
			{
				MessageBoxLog.Show("错误，赋值的轴号不能大于8");
				return RET_FAIL;
			}
			if (axisOrdinate > 4)
			{
				MessageBoxLog.Show("错误，赋值的坐标系值不能大于4");
				return RET_FAIL;
			}
			switch(axisID)
			{
				case 1:
					crdPrm.profile1 = axisOrdinate;
					break;
				case 2:
					crdPrm.profile2 = axisOrdinate;
					break;
				case 3:
					crdPrm.profile3 = axisOrdinate;
					break;
				case 4:
					crdPrm.profile4 = axisOrdinate;
					break;
				case 5:
					crdPrm.profile5 = axisOrdinate;
					break;
				case 6:
					crdPrm.profile6 = axisOrdinate;
					break;
				case 7:
					crdPrm.profile7 = axisOrdinate;
					break;
				case 8:
					crdPrm.profile8 = axisOrdinate;
					break;
					default :
					break;
			}

			return RET_SUCCESS;
		}

		//axises 要插补的轴，几维传几个轴进来
		//coordinateID要建立的坐标系ID：1或2
		private static TReturn SR_SetCoords(TCard card, TMode coordinateID, params TAxis []axises)
		{
			if(coordinateID!=1 && coordinateID!=2)
			{
				MessageBoxLog.Show("错误，要设置的坐标系参数只能是1或2");
                return RET_FAIL;
			}
			int nDimention=axises.Length;
			mc.TCrdPrm crdPrm = new mc.TCrdPrm();//坐标系结构体
			crdPrm.dimension = (short )nDimention;// 维数赋值
			crdPrm.synVelMax = 500;//最大合成速度 pulse/ms default:500
			crdPrm.synAccMax = 1;//最大加速度
			crdPrm.evenTime = 50;//最小匀速时间
			crdPrm.setOriginFlag = 1;// 需要设置加工坐标系原点位置

			if(nDimention>=2)
			{
				SR_SetcoordinateAxis(ref crdPrm, axises[0], 1);//规划器1
				SR_SetcoordinateAxis(ref crdPrm, axises[1], 2);//规划器2
				crdPrm.originPos1 = 0;//加工坐标系原点位置在(0,0,0)，即与机床坐标系原点重合
				crdPrm.originPos2 = 0;//
			}
			if(nDimention>=3)
			{
				SR_SetcoordinateAxis(ref crdPrm, axises[2], 3);//规划器3
				crdPrm.originPos3 = 0;//
			}
			if(nDimention>=4)
			{
				SR_SetcoordinateAxis(ref crdPrm, axises[3], 4);//规划器4
				crdPrm.originPos4 = 0;//
			}

			mc.GT_SetCrdPrm(card, coordinateID, ref crdPrm);//建立1号坐标系，坐标系编号：1，2

			return RET_SUCCESS;
		}
		//两轴直线插补 未完成
		public static TReturn SR_InpMove2(TCard card, TAxis axis1, TAxis axis2, TPusle pos1, TPusle pos2, TSpeed speed, TSpeed acc,TAxis ordinateID=1)
		{
			TReturn ret = RET_SUCCESS;
			#region 建立坐标系
			mc.TCrdPrm crdPrm = new mc.TCrdPrm();//坐标系结构体
			crdPrm.dimension = 2;// two dimention
			crdPrm.synVelMax = 500 ;//最大合成速度 pulse/ms default:500
			crdPrm.synAccMax = 1;//最大加速度
			crdPrm.evenTime = 50;//最小匀速时间

			SR_SetCoords(card, ordinateID, axis1, axis2);
 
 
			#endregion

			short sRtn;
			sRtn = mc.GT_CrdClear(card, ordinateID, 0);
			//查询坐标系1的FIFO0所剩余的空间

			sRtn = mc.GT_LnXY(
				card,
				ordinateID, // 该插补段的坐标系是坐标系1
				pos1 , pos2, // 该插补段的终点坐标(200000, 0)
				speed , // 该插补段的目标速度：100pulse/ms
				acc, // 插补段的加速度：0.1pulse/ms^2
				0, // 终点速度为0
				0); // 向坐标系1的FIFO0缓存区传递该直线插补数据

			// 坐标系运动状态查询变量
			short run;
			// 坐标系运动完成段查询变量
			int segment;
			//// 坐标系的缓存区剩余空间查询变量
			//int space;
			//sRtn = mc.GT_CrdSpace(1, 1, out space, 0);

			sRtn = mc.GT_CrdStart(card, ordinateID, 0);
			// 等待运动完成
			sRtn = mc.GT_CrdStatus(card, ordinateID, out run, out segment, 0);
            Thread.Sleep(10);
			do
			{
				// 查询坐标系1的FIFO的插补运动状态
				sRtn = mc.GT_CrdStatus(
				card, //1卡
				ordinateID, // 坐标系是坐标系1
				out run, // 读取插补运动状态
				out segment, // 读取当前已经完成的插补段数
				0); // 查询坐标系1的FIFO0缓存区
				// 坐标系在运动, 查询到的run的值为1
			} while (run == 1);
			//pRun:读取插补运动状态。0：该坐标系的该FIFO没有在运动；1：该坐标系的该FIFO正在进行插补运动。
            //sRtn = mc.GT_Stop(card, 255, 255);
            //if (sRtn != 0)
            //{
            //    MessageBoxLog.Show("命令失败！");
            //}
            //sRtn = mc.GT_Stop(card, 511, 511);
            //if (sRtn != 0)
            //{
            //    MessageBoxLog.Show("命令失败！");
            //}
			return ret;
		}
		//两轴直线插补 不等待完成
		public static TReturn SR_InpMove2NotWait(TCard card, TAxis axis1, TAxis axis2, TPusle pos1, TPusle pos2, TSpeed speed, TSpeed acc,TAxis ordinateID=1)
		{
			TReturn ret = RET_SUCCESS;
			#region 建立坐标系
			mc.TCrdPrm crdPrm = new mc.TCrdPrm();//坐标系结构体
			crdPrm.dimension = 2;// two dimention
			crdPrm.synVelMax = 500 ;//最大合成速度 pulse/ms default:500
			crdPrm.synAccMax = 1;//最大加速度
			crdPrm.evenTime = 50;//最小匀速时间

			SR_SetCoords(card, ordinateID, axis1, axis2);
 
 
			#endregion

			short sRtn;
			sRtn = mc.GT_CrdClear(card, ordinateID, 0);
			//查询坐标系1的FIFO0所剩余的空间

			sRtn = mc.GT_LnXY(
				card,
				ordinateID, // 该插补段的坐标系是坐标系1
				pos1 , pos2, // 该插补段的终点坐标(200000, 0)
				speed , // 该插补段的目标速度：100pulse/ms
				acc, // 插补段的加速度：0.1pulse/ms^2
				0, // 终点速度为0
				0); // 向坐标系1的FIFO0缓存区传递该直线插补数据

			// 坐标系运动状态查询变量
            //short run;
			// 坐标系运动完成段查询变量
            //int segment;
			//// 坐标系的缓存区剩余空间查询变量
			//int space;
			//sRtn = mc.GT_CrdSpace(1, 1, out space, 0);

			sRtn = mc.GT_CrdStart(card, ordinateID, 0);

			return ret;
		}
		//两轴直线插补 不等待完成
		public static TReturn SR_InpMove2NotWait22(TCard card, TAxis axis1, TAxis axis2, TPusle pos1, TPusle pos2, 
            TPusle pos11, TPusle pos22,TSpeed speed, TSpeed acc,TAxis ordinateID=1)
		{
			TReturn ret = RET_SUCCESS;
			#region 建立坐标系
			mc.TCrdPrm crdPrm = new mc.TCrdPrm();//坐标系结构体
			crdPrm.dimension = 2;// two dimention
			crdPrm.synVelMax = 500 ;//最大合成速度 pulse/ms default:500
			crdPrm.synAccMax = 1;//最大加速度
			crdPrm.evenTime = 50;//最小匀速时间

			SR_SetCoords(card, ordinateID, axis1, axis2);
 
 
			#endregion

			short sRtn;
			sRtn = mc.GT_CrdClear(card, ordinateID, 0);
			//查询坐标系1的FIFO0所剩余的空间

			sRtn = mc.GT_LnXY(
				card,
				ordinateID, // 该插补段的坐标系是坐标系1
				pos1 , pos2, // 该插补段的终点坐标(200000, 0)
				speed , // 该插补段的目标速度：100pulse/ms
				acc, // 插补段的加速度：0.1pulse/ms^2
                speed, // 终点速度为0
				0); // 向坐标系1的FIFO0缓存区传递该直线插补数据
            Thread.Sleep(10);
			sRtn = mc.GT_LnXY(
				card,
				ordinateID, // 该插补段的坐标系是坐标系1
				pos11 , pos22, // 该插补段的终点坐标(200000, 0)
				speed , // 该插补段的目标速度：100pulse/ms
				acc, // 插补段的加速度：0.1pulse/ms^2
				0, // 终点速度为0
				0); // 向坐标系1的FIFO0缓存区传递该直线插补数据


			// 坐标系运动状态查询变量
            //short run;
			// 坐标系运动完成段查询变量
            //int segment;
			//// 坐标系的缓存区剩余空间查询变量
			//int space;
			//sRtn = mc.GT_CrdSpace(1, 1, out space, 0);

			sRtn = mc.GT_CrdStart(card, ordinateID, 0);

			return ret;
		}

		//3轴直线插补 未完成
		public static TReturn SR_InpMove3(TCard card, TAxis axis1, TAxis axis2,TAxis axis3, 
				TPusle pos1, TPusle pos2,TPusle pos3, TSpeed speed, TSpeed acc,TAxis ordinateID=1)
		{
			TReturn ret = RET_SUCCESS;

			SR_SetCoords(card, ordinateID,axis1, axis2, axis3);
			short sRtn;
			sRtn = mc.GT_CrdClear(card, ordinateID, 0);
			//查询坐标系1的FIFO0所剩余的空间

			sRtn = mc.GT_LnXYZ(
				card,
				ordinateID, // 该插补段的坐标系是坐标系1
				pos1, pos2, pos3,// 该插补段的终点坐标(200000, 0)
				speed, // 该插补段的目标速度：100pulse/ms
				acc, // 插补段的加速度：0.1pulse/ms^2
				0, // 终点速度为0
				0); // 向坐标系1的FIFO0缓存区传递该直线插补数据

			// 坐标系运动状态查询变量
			short run;
			// 坐标系运动完成段查询变量
			int segment;
			//// 坐标系的缓存区剩余空间查询变量
			//int space;
			//sRtn = mc.GT_CrdSpace(1, 1, out space, 0);

			// 启动坐标系1的FIFO0的插补运动
			//double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
			sRtn = mc.GT_CrdStart(card, ordinateID, 0);
			//short mask,short option mask:bit0~bit1表示要启动的坐标系，0不启动，1启动。option:bit0~bit1表示要启动的缓冲区
			// 等待运动完成
			sRtn = mc.GT_CrdStatus(card, ordinateID, out run, out segment, 0);
			do
			{
				// 查询坐标系1的FIFO的插补运动状态
				sRtn = mc.GT_CrdStatus(
				card, //1卡
				ordinateID, // 坐标系是坐标系1
				out run, // 读取插补运动状态
				out segment, // 读取当前已经完成的插补段数
				0); // 查询坐标系1的FIFO0缓存区
				// 坐标系在运动, 查询到的run的值为1
			} while (run == 1);
			//pRun:读取插补运动状态。0：该坐标系的该FIFO没有在运动；1：该坐标系的该FIFO正在进行插补运动。

            //sRtn = mc.GT_Stop(card, 255, 255);
            //if (sRtn != 0)
            //{
            //    MessageBoxLog.Show("命令失败！");
            //}
            //sRtn = mc.GT_Stop(card, 511, 511);
            //if (sRtn != 0)
            //{
            //    MessageBoxLog.Show("命令失败！");
            //}

			return ret;
		}
		//4轴直线插补 未完成
		public static TReturn SR_InpMove4(TCard card, TAxis axis1, TAxis axis2,TAxis axis3,TAxis axis4,
			 TPusle pos1, TPusle pos2, TPusle pos3, TPusle pos4, TSpeed speed, TSpeed acc, TAxis ordinateID=1)
		{
			TReturn ret = RET_SUCCESS;
			SR_SetCoords(card, ordinateID,axis1, axis2, axis3, axis4);

			short sRtn;
			sRtn = mc.GT_CrdClear(card, ordinateID, 0);
			//查询坐标系1的FIFO0所剩余的空间

			sRtn = mc.GT_LnXYZA(
				card,
				ordinateID, // 该插补段的坐标系是坐标系1
				pos1, pos2, pos3,pos4 ,// 该插补段的终点坐标(200000, 0)
				speed, // 该插补段的目标速度：100pulse/ms
				acc, // 插补段的加速度：0.1pulse/ms^2
				0, // 终点速度为0
				0); // 向坐标系1的FIFO0缓存区传递该直线插补数据

			// 坐标系运动状态查询变量
			short run;
			// 坐标系运动完成段查询变量
			int segment;
			//// 坐标系的缓存区剩余空间查询变量
			//int space;
			//sRtn = mc.GT_CrdSpace(1, 1, out space, 0);

			// 启动坐标系1的FIFO0的插补运动
			//double xCenter,double yCenter,short circleDir,double synVel,double synAcc,double velEnd,short fifo);
			sRtn = mc.GT_CrdStart(card, ordinateID, 0);
			//short mask,short option mask:bit0~bit1表示要启动的坐标系，0不启动，1启动。option:bit0~bit1表示要启动的缓冲区
			// 等待运动完成
			sRtn = mc.GT_CrdStatus(card, ordinateID, out run, out segment, 0);
			do
			{
				// 查询坐标系1的FIFO的插补运动状态
				sRtn = mc.GT_CrdStatus(
				card, //1卡
				ordinateID, // 坐标系是坐标系1
				out run, // 读取插补运动状态
				out segment, // 读取当前已经完成的插补段数
				0); // 查询坐标系1的FIFO0缓存区
				// 坐标系在运动, 查询到的run的值为1
			} while (run == 1);
			//pRun:读取插补运动状态。0：该坐标系的该FIFO没有在运动；1：该坐标系的该FIFO正在进行插补运动。

            //sRtn = mc.GT_Stop(card, 255, 255);
            //if (sRtn != 0)
            //{
            //    MessageBoxLog.Show("命令失败！");
            //}
            //sRtn = mc.GT_Stop(card, 511, 511);
            //if (sRtn != 0)
            //{
            //    MessageBoxLog.Show("命令失败！");
            //}

			return ret;
		}
		//两轴圆弧插补 未完成
		//dir 0顺时针，1逆时针
		public static TReturn SR_InpArc(TCard card, TAxis axis1, TAxis axis2, TPusle pos1, TPusle pos2, TSpeed speed, TSpeed acc, double radius, TMode dir, TAxis ordinateID=1)
		{
			TReturn ret = RET_SUCCESS;

			SR_SetCoords(card, ordinateID,axis1, axis2);

			short sRtn;
			sRtn = mc.GT_CrdClear(card, ordinateID, 0);


			sRtn = mc.GT_ArcXYR(
			  card,
			  ordinateID, // 坐标系是坐标系1
			  pos1 , pos2, // 该圆弧的终点坐标(0, 200000)
			  radius, // 半径：200000pulse
			  dir, // 该圆弧是逆时针圆弧
			  speed , // 该插补段的目标速度：100pulse/ms
			  acc, // 该插补段的加速度：0.1pulse/ms^2
			  0, // 终点速度为0
			  0); // 向坐标系1的FIFO0缓存区传递该直线插补数据

			  //handle stop
			return ret;
		}
		//获取版本号
		public static TReturn SR_GetLibVision(TCard card, out string strVersion)
		{
			TReturn ret = RET_SUCCESS;
			ret = mc.GT_GetVersion(card, out strVersion);
			if (ret != RET_SUCCESS)
			{
				CommandResult("GT_GetVersion", ret);
				ret = RET_FAIL;
			}
			return ret;
		}

		//直接停止不等待,需验证
		public static TReturn SR_AllAxisStopNoWait(TCard card)
		{
			TReturn ret = RET_SUCCESS;
			if (card == BCARD.CARD_NUM_0 || card == BCARD.CARD_NUM_1)
			{
				ret = mc.GT_Stop(card, 255, 255);
			}
			else if (card == BCARD.CARD_NUM_2)
			{
				ret = mc.GT_Stop(card, 15, 15);
			}
			//for (int i = 1; i < 9; i++)
			//{
			//    mc.GT_Stop(card, 1 << (i - 1), 0);
			//}
			Thread.Sleep(5);
			if (ret != RET_SUCCESS)
			{
				ret = RET_FAIL;
			}
			return ret;
		}
		//所有轴停止
		public static TReturn SR_AllAxisStop(TCard card)
		{
			TReturn ret = RET_SUCCESS;
			int iAxis=9;
			if(card==BCARD.CARD_NUM_2 )
			{
				iAxis = 5;
			}

			for (TAxis i = 1; i < iAxis; i++)
			{
				if (StopAxis(card, i, 0) != RET_SUCCESS)
				{
					ret = RET_FAIL;
				}
			}
			return ret;
		}


		#endregion

		#region 其他接口
		#region home
		class MCP
		{
			public MCP(TCard Card, TAxis Axis)
			{
				CardNo = Card;
				AxisNo = Axis;
			}
			public TCard CardNo;
			public TAxis AxisNo;
		}
		private static void StartHome(TCard card, TAxis axis)
		{
			MCP mp = new MCP(card, axis);
			Thread threadHomeAxisX1 = new Thread(ThreadStartHome);
			threadHomeAxisX1.IsBackground = true;
			threadHomeAxisX1.Start(mp);
		}
		private static void ThreadStartHome(object ob)
		{
			TCard card = ((MCP)ob).CardNo;
			TAxis axis = ((MCP)ob).AxisNo;

			SetHomeFlag(card, axis, false, false);
            int homeResult = GoHome(card, axis);
			if (homeResult == RET_FAIL)
			{
                Global.CFile.WriteTxtDate(string.Format("card{0},axis{1},set fail", card, axis), "homeTrack");
				SetHomeFlag(card, axis, false, true);
			}
			else if (homeResult == RET_SUCCESS)
			{
                Global.CFile.WriteTxtDate(string.Format("card{0},axis{1},set succeed", card, axis), "homeTrack");
				SetHomeFlag(card, axis, true, false);
			}
		}

		public static void ResetHomeFinish(TCard card)
		{
			for (int i = 0; i < AxisParams.Length; i++)
			{
				if (card == AxisParams[i].CardNo)
				{
					AxisParams[i].ax.HomeFinish = false;
				}
			}
		}
		public static bool GetHomeFinish(TCard card, TAxis axis)
		{
			return GetAxisSt(card, axis).ax.HomeFinish;
		}
		public static bool GetHoming(TCard card, TAxis axis)
		{
			return GetAxisSt(card, axis).ax.Homing;
		}
		public static bool GetHomeFail(TCard card, TAxis axis)
		{
			return GetAxisSt(card, axis).ax.HomeFail;
		}
		public static void SetHomeFlag(TCard card, TAxis axis, bool bSetFinish, bool bSetFail)
		{
			for (int i = 0; i < AxisParams.Length; i++)
			{
				if (card == AxisParams[i].CardNo && axis == AxisParams[i].AxisNo)
				{
					AxisParams[i].ax.HomeFinish = bSetFinish;
					AxisParams[i].ax.HomeFail = bSetFail;
                    Global.CFile.WriteTxtDate(string.Format("card{0},axis{1},setFinish{2}", card, axis,bSetFinish), "homeTrack");
					return;
				}

			}
			MessageBoxLog.Show("不存在给定的轴");
		}
		#endregion

		public static bool bIOEx = false;
		public static TReturn ReadAllInput(TMode portNo = 0)
		{
			short sResult;
			int AxisBit;
			for (TCard card = 0; card < BCARD.CARD_QTY; card++)
			{
				#region  card 0 io
				try
				{
					sResult = mc.GT_GetDi(card, mc.MC_GPI, out AxisBit);//AxisDi输入IO点
				}
				catch (System.Exception ex)
				{

                    if (bIOEx == false)
                    {
                       
                    }

                    bIOEx = true;
					return RET_FAIL;
				}
                bIOEx = false ;
				if (!CommandResult("GT_GetDi", sResult))
				{
					
					return RET_FAIL;
				}
				for (int i = 0; i < 16; i++)
				{
					bool bTemp = (AxisBit & (1 << i)) <= 0 ? true : false;
					if (bTemp == true && CARDSTS.InputRead[16 * card + i] == false)
					{
						CARDSTS.InputPulseUp[16 * card + i] = true;
					}
					else
					{
						CARDSTS.InputPulseUp[16 * card + i] = false;
					}

					if (bTemp == false && CARDSTS.InputRead[16 * card + i] == true)
                    {
						CARDSTS.InputPulseDown[16 * card + i] = true;
                    }
                    else
                    {
						CARDSTS.InputPulseDown[16 * card + i] = false;
                    }

					CARDSTS.InputRead[16 * card + i] = bTemp;

				}

				sResult = mc.GT_GetDo(card, mc.MC_GPO, out AxisBit);//AxisDo输出IO点
				if (!CommandResult("GT_GetDi", sResult))
				{
					
					return RET_FAIL;
				}
				for (int i = 0; i < 16; i++)
				{
					if ((AxisBit & (1 << i)) > 0)//没输出
					{
						CARDSTS.OutputRead[16 * card + i] = false;
					}
					else
					{
						CARDSTS.OutputRead[16 * card + i] = true;
					}
				}
				#endregion
				#region card  limitNegative
				//读限位信号
				sResult = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out AxisBit);//负输入IO点
				if (!CommandResult("GT_GetDi", sResult))
				{
					return RET_FAIL;
				}
				for (int i = 0; i < 8; i++)
				{
					if ((AxisBit & (1 << i)) > 0)
					{
						CARDSTS.InLimitNegative[8 * card + i] = true;
					}
					else
					{
						CARDSTS.InLimitNegative[8 * card + i] = false;
					}
                    if ((8 * card + i) == BCARD.AXIS_QTY_ALL - 1) break;//0927
				}
				#endregion
				#region card  limitPositive
				//读限位信号
				sResult = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out AxisBit);//正限位输入IO点
				if (!CommandResult("GT_GetDi", sResult))
				{
						return RET_FAIL;
				}
				for (int i = 0; i < 8; i++)
				{
					if ((AxisBit & (1 << i)) > 0)
					{
						CARDSTS.InLimitPositive[8 * card + i] = true;
					}
					else
					{
						CARDSTS.InLimitPositive[8 * card + i] = false;
					}
                    if ((8 * card + i) == BCARD.AXIS_QTY_ALL - 1) break;
				}
				#endregion
				#region card alarm
				//读报警信号
				sResult = mc.GT_GetDi(card, mc.MC_ALARM, out AxisBit);//正限位输入IO点
				if (!CommandResult("GT_GetDi", sResult))
				{
					return RET_FAIL;
				}
				for (int i = 0; i < 8; i++)
				{
					if ((AxisBit & (1 << i)) > 0)
					{
						CARDSTS.InServerAlarm[8 * card + i] = true;
					}
					else
					{
						CARDSTS.InServerAlarm[8 * card + i] = false;
					}
                    if ((8 * card + i) == BCARD.AXIS_QTY_ALL - 1) break;
				}
				#endregion

				#region card 0 ori
				//读原点信号
				sResult = mc.GT_GetDi(card, mc.MC_HOME, out AxisBit);//原点输入IO点
				if (!CommandResult("GT_GetDi", sResult))
				{
						return RET_FAIL;
				}
				for (int i = 0; i < 8; i++)
				{
					if ((AxisBit & (1 << i)) > 0)
					{
						CARDSTS.InLimitOri[8 * card + i] = true;
					}
					else
					{
						CARDSTS.InLimitOri[8 * card + i] = false;
					}
                    if ((8 * card + i) == BCARD.AXIS_QTY_ALL - 1) break;
				}
				#endregion
			}
            return RET_SUCCESS;//0726
			if (bIOEx == true)
				return RET_FAIL;
			ushort  uAxisBit = 0;
			try
			{
				//sResult = mc.GT_GetExtIoValueGts(0, 0, uAxisBit);//AxisDi输入IO点
				sResult = mc.GT_GetExtIoValueGts((short)0, (short)0, ref uAxisBit);//AxisDi输入IO点
			}
			catch (System.Exception ex)
			{
					bIOEx = true;
				return RET_FAIL;
			}

			if (!CommandResult("GT_GetDi", sResult))
			{
				return RET_FAIL;
			}
			for (int i = 0; i < 16; i++)
			{
				bool bTemp = (uAxisBit & (1 << i)) <= 0 ? true : false;
				if (bTemp == true && CARDSTS.InputRead[32 + i] == false)
				{
					CARDSTS.InputPulseUp[32 + i] = true;
				}
				else
				{
					CARDSTS.InputPulseUp[32 + i] = false;
				}
				if (bTemp == false && CARDSTS.InputRead[32 + i] == true)
				{
					CARDSTS.InputPulseDown[32 + i] = true;
				}
				else
				{
					CARDSTS.InputPulseDown[32 + i] = false;
				}
				CARDSTS.InputRead[32 + i] = bTemp;

			}
			return RET_SUCCESS;

		}

		public static TReturn WriteOutput(TIOPoint OutPoint, TMode bOut)
		{
            if (Global.WorkVar.ConnectCard == false)
				return RET_SUCCESS;
			TReturn sResult = RET_SUCCESS;

			TCard card = 0;
			if (OutPoint < 32) //控制卡输出点
			{
				if (OutPoint > 15)
				{
					card = 1;
					OutPoint -= 16;
				}
				CtrlCardSR.SR_SetOutput(card, OutPoint, bOut);
			}
			else//扩展卡输出点
			{
				sResult = mc.GT_SetExtIoBitGts(0, 0, (short)(OutPoint - 32), (ushort)bOut);
				if (!CommandResult("GT_SetExtIoBitGts", sResult))
				{
					return RET_FAIL;
				}
				CARDSTS.OutputRead[OutPoint] = (bOut == BCARD.OUT_TRUE) ? true : false;
			}
			return sResult;
		}
		public static TReturn WriteOutput(TCard card, TIOPoint OutPoint, TMode bOut,bool bIsExIO=false)
		{
			//bIsExIO: true 为扩展IO
            if (Global.WorkVar.ConnectCard == false)
				return RET_SUCCESS;

			TReturn sRet = RET_SUCCESS;
			if(bIsExIO)
			{
				sRet = mc.GT_SetExtIoBitGts(0, 0, (short)(OutPoint - 32), (ushort)bOut);
				if (!CommandResult("GT_SetExtIoBitGts", sRet))
				{
					return RET_FAIL;
				}
			}
			else
			{
				return CtrlCardSR.SR_SetOutput(card, OutPoint, bOut);
			}
			return sRet;
		}

		//电机回原点线程函数
		public static TReturn  homeMessage(TCard card, short axis)
		{// success return RET_SUCCESS
			string sMessage;
			sMessage = "回原点启动！";
			mc.TTrapPrm tPra;

			short sResult;

			sResult = mc.GT_GetTrapPrm(card, axis, out tPra);//读取点位运动参数
			if (!CommandResult("GT_GetTrapPrm", sResult))
			{
				sMessage = axis.ToString() + "轴获取加减速命令出错！";
				MessageBoxLog.Show(sMessage);
				return RET_FAIL;
			}
			tPra.acc = BAX.AXIS_ACC;
			tPra.dec = BAX.AXIS_DEC;
			tPra.velStart = 0;
			tPra.smoothTime = BAX.SMOOTHTIME;
			sResult = mc.GT_SetTrapPrm(card, axis, ref tPra);
			if (!CommandResult("GT_SetTrapPrm", sResult))
			{
				sMessage = axis.ToString() + "轴设置加减速命令出错！";
				MessageBoxLog.Show(sMessage);
				return RET_FAIL;
			}
			return RET_SUCCESS;
		}


		public static TReturn ReadAxisPosEnc()
		{
			short sResult;
			double dPostValue;
			uint pClock;
			for (TCard card = 0; card < BCARD.CARD_QTY; card++)
			{
				for (short i = 0; i < 8; i++)
				{
					sResult = mc.GT_GetAxisEncPos(card, (TAxis)(i+1), out dPostValue, 1, out pClock);
					if (!CommandResult("GT_GetEncPos", sResult))
					{
						return RET_FAIL;
					}
					CARDSTS.EncodePos0[8 * card + i  ] = (TPusle)dPostValue;
                    if ((8 * card + i) == BCARD.AXIS_QTY_ALL - 1) break;
				}
			}
			return RET_SUCCESS;
		}

		//读取逻辑位置
		public static TReturn ReadAxisPosLogic()
		{
			short sResult;
			double dPostValue;
			uint pClock;
			for (TCard card = 0; card < BCARD.CARD_QTY; card++)
			{
				for (short i = 0; i < 8; i++)
				{
					sResult = mc.GT_GetAxisPrfPos(card, (short)(i+1), out dPostValue, 1, out pClock);
					if (!CommandResult("GT_GetEncPos", sResult))
					{
						return RET_FAIL;
					}
					CARDSTS.LogicPos0[8 * card + i ] = (TPusle)dPostValue;
                    if ((8 * card + i) == BCARD.AXIS_QTY_ALL - 1) break;
				}
			}
			return RET_SUCCESS;
		}

		//包括更新轴逻辑。实际位置。轴运动状态
        public static TReturn UpdateAxises()
        {
            ReadAxisPosEnc();
            ReadAxisPosLogic();
            for (int i = 0; i < AxisParams.Length ; i++)
            {
                AxisParams[i].UpdateStatus();
            }
            return RET_SUCCESS;
        }




		#endregion


	}
}
