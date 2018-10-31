using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gts;
using System.Windows.Forms;
using System.Threading;

namespace StrongProject
{
    public class NewCtrlCardSR
    {
        public static bool blnRun = false;  //程序启动/退出标志 需在程序初始化时赋值       

        private const int intCardCount = 2; //卡的数量
        private const int intAxisCountForCard = 8;//卡轴的数量
        private const int intAxisCountForCard1 = 4;//卡轴的数量
        private const int intExtendIoCount = 7;   //扩展IO模块数量 
        private const int intExtendStartId = 11; //扩展模块起始号

        private static short shrGtsSuccess = 0;    //固高函数成功
        private static short shrSuccess = 0;    //二次封装函数成功
        private static short shrFail = -1;      //二次封装函数失败
        private static double dblAxisAcc = 7;   //减速度
        private static double dblAxisDec = 7;   //加速度
        private static short shtSmoothTime = 25; //平滑时间
        private static short shtPstMoveDir = 0; //正方向运动
        public const int Torlerance = 50; //最大误差脉冲数        
        public const int DtTime = 20;  //单位微秒，持续保存小于误差数后置位到位信号
        private static double dblMoveOutOriginTime = 4; //移出原点感应器信号超时时间，单位秒
        public const int AxisMotionError = 100;  //轴移动误差，单位脉冲
        public const int IoClose = 1;  //关闭IO
        public const int IoOpen = 0;  //打开IO
        private static int[] arrInputStatus = new int[intCardCount + intExtendIoCount];
        private static int[] arrOutputStatus = new int[intCardCount + intExtendIoCount];
        private static double[] arrAxisEncPos = new double[intCardCount * 8];
        private static double[] arrAxisPrfPos = new double[intCardCount * 8];
        private static int[] arraAxisEnableStatus = new int[intCardCount];
        private static int[] arraAxisAlarmStatus = new int[intCardCount];
        private static int[] arrAxisLimitPStatus = new int[intCardCount];
        private static int[] arrAxisLimitNStatus = new int[intCardCount];
        private static int[] arrAxisOriginStatus = new int[intCardCount];
        public static Int16 tag_initResult = 0;//初始化结果
        static NewCtrlCardSR()
        {
        }
        #region 基本函数
        /// <summary>
        /// 读取板卡实际位置
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="pos"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static short SR_GetEncPos(short card, short axis, ref double[] pos, int count = 8)
        {
            short shrResult;
            uint[] pClock = new uint[count];
            if(card>0)
            {
                count = intAxisCountForCard1;
                shrResult = mc.GT_GetAxisEncPos(card, (short)axis, out pos[8], (short)count, out pClock[0]);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetAxisEncPos", shrResult);
                    return -1;
                }
            }
            else
            {
                shrResult = mc.GT_GetAxisEncPos(card, (short)axis, out pos[0], (short)count, out pClock[0]);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetAxisEncPos", shrResult);
                    return -1;
                }
            }
            
            return 0;
        }
        /// <summary>
        /// 读取板卡规划位置
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="pos"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        /// 
        public static short SR_GetPrfPos(short card, short axis, ref double[] pos, short count = 8)
        {
            uint[] pClock = new uint[count];
            short shrResult;
            if (card > 0)
            {
                count = intAxisCountForCard1;
                shrResult = mc.GT_GetAxisPrfPos(card, (short)axis, out pos[8], (short)count, out pClock[0]);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetAxisPrfPos", shrResult);
                    return -1;
                }
            }
            else
            {
                shrResult = mc.GT_GetAxisPrfPos(card, (short)axis, out pos[0], (short)count, out pClock[0]);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetAxisPrfPos", shrResult);
                    return -1;
                }
            }
           
            return 0;
        }
        /// <summary>
        /// 轴坐标清零
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short SR_SetPosZero(short card, short axis)
        {
            short shrResult;
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 卡初始化
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axisCount"></param>
        /// <param name="configFileName"></param>
        /// <returns></returns>
        public static short SR_InitCard(short card, short axisCount, string configFileName = null)
        {
            short shrResult;
            mc.TTrapPrm tPra;

            //打开运动控制器
            shrResult = mc.GT_Open(card, 0, axisCount);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_Open", shrResult);
                return shrFail;
            }
            //复位控制器
            shrResult = mc.GT_Reset(card);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_Reset", shrResult);
                return shrFail;
            }

            //shrResult = mc.GT_AxisOn(card, 4);
            //if (shrResult != shrGtsSuccess)
            //{
            //    CommandResult("GT_AxisOn", shrResult);
            //    return shrFail;
            //}

            if (!string.IsNullOrEmpty(configFileName))
            {
                //配置运动控制器
                shrResult = mc.GT_LoadConfig(card, configFileName);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_LoadConfig", shrResult);
                    return shrFail;
                }
            }

            //清除指定轴的报警和限位
            shrResult = mc.GT_ClrSts(card, 1, axisCount);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ClrSts", shrResult);
                return shrFail;
            }



            //shrResult = SR_SetAllAxisEnable(card, 1, 8);
            //if (shrResult != shrSuccess)
            //{
            //    return shrFail;
            //}


            for (short axis = 1; axis < axisCount + 1; axis++)
            {
                shrResult = mc.GT_GetTrapPrm(card, axis, out tPra);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetTrapPrm", shrResult);
                    return shrFail;
                }
                tPra.acc = 7;
                tPra.dec = 7;
                tPra.smoothTime = 40;
                shrResult = mc.GT_SetTrapPrm(card, axis, ref tPra);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_SetTrapPrm", shrResult);
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 关闭控制卡
        /// </summary>
        /// <returns></returns>
        public static short SR_CloseCard()
        {
            short shrResult;
            shrResult = mc.GT_CloseExtMdlGts(0);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_CloseExtMdlGts", shrResult);
                return shrFail;
            }
            for (short i = 0; i < intCardCount; i++)
            {
                shrResult = mc.GT_Close(i);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_Close", shrResult);
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        //限位设置：暂时没用
        public static short SR_SetLimitEnable()
        {
            return shrSuccess;
        }
        //未完成 限位触发电平设置  bMode:true:低电平，0：高电平
        public static short SR_SetLimitMode()
        {
            return shrSuccess;
        }
        //未完成 设置编码器计数方向//没有获取设置函数，只能整卡一起设置，false:正向，true:反向
        public static short SR_SetEncSnsMode()
        {
            return shrSuccess;
        }
        //未完成 设置脉冲输出模式// bMode: true CCW/CW, false 脉冲+方向 未完成
        public static short SR_PulseMode()
        {
            return shrSuccess;
        }
        //未完成 设置编码器计数方式   暂未找到函数
        public static short SR_SetEncMode()
        {
            return shrSuccess;
        }
        /// <summary>
        /// 除各轴异常状态 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="axisCount">轴数量，默认为1，清除单轴</param>
        /// <returns></returns>
        public static short SR_ClrStatus(short card, short axis, short axisCount = 1)
        {
            short shrResult;
            //清除指定轴的报警和限位
            shrResult = mc.GT_ClrSts(card, axis, axisCount);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ClrSts", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 清除卡上所有轴状态
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axisCount">轴数量，默认为8</param>
        /// <returns></returns>
        public static short SR_ClrAllStatus(short card, short axisCount = 8)
        {
            short shrResult;
            //清除所有轴的报警和限位
            shrResult = mc.GT_ClrSts(card, 1, axisCount);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ClrSts", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴使能
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="bEanble">使能状态 true 使能  false 使能关闭</param>
        /// <returns></returns>
        public static short SR_GetServoEnable(short card, short axis, out bool bEanble)
        {
            short shrResult;
            int pValue;
            bEanble = false;
            shrResult = mc.GT_GetDo(card, mc.MC_ENABLE, out pValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDo", shrResult);
                return shrFail;
            }
            if ((pValue & (1 << (axis - 1))) > 0)
            {
                bEanble = true;
            }
            else
            {
                bEanble = false;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴使能
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="bEanble">使能状态 true 使能  false 使能关闭</param>
        /// <returns></returns>
        public static short SR_SetServoEnable(short card, short axis, bool bEanble)
        {
            short shrResult;
            if (bEanble)
            {
                shrResult = mc.GT_AxisOn(card, axis);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_AxisOn", shrResult);
                    return shrFail;
                }
            }
            else
            {
                shrResult = mc.GT_AxisOff(card, axis);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_AxisOff", shrResult);
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        public static short SR_SetAllAxisEnable(short card, short startAxis, short axisCount = 8, bool bEanble = true)
        {
            short shrResult;
            short i = 0;
            if (bEanble)
            {
                for (i = startAxis; i < axisCount; i++)
                {
                    shrResult = mc.GT_AxisOn(card, i);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_AxisOn", shrResult);
                        return shrFail;
                    }
                }
            }
            else
            {
                for (i = startAxis; i < axisCount; i++)
                {
                    shrResult = mc.GT_AxisOff(card, 1);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_AxisOff", shrResult);
                        return shrFail;
                    }
                }

            }
            return shrSuccess;
        }
        /// <summary>
        /// 设置伺服报警使能
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="bEanble"> ture 有效, false 无效</param>
        /// <returns></returns>
        public static short SR_AlarmEnable(short card, short axis, bool bEanble)
        {
            short shrResult;
            if (bEanble)
            {
                shrResult = mc.GT_AlarmOn(card, axis);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_AlarmOn", shrResult);
                    return shrFail;
                }
            }
            else
            {
                shrResult = mc.GT_AlarmOff(card, axis);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_AlarmOff", shrResult);
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取轴状态信息
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="axisStatus">获取的轴状态</param>
        /// <returns></returns>
        public static short SR_GetAxisStatus(short card, short axis, out int axisStatus)
        {
            short shrResult;
            uint pClock;
            shrResult = mc.GT_GetSts(card, axis, out axisStatus, 1, out pClock);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetSts", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        //未完成 获取轴运动状态  
        // bAxisRunStatus : 运动中ture, else false
        //执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果
        public static short SR_GetRunStatus(short card, short axis)
        {
            return shrSuccess;
        }
        public static short SR_AxisStop(string stationName, string axisName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            return SR_AxisStop(axisC.CardNum, axisC.AxisNum);
        }
        /// <summary>
        /// 设置单轴停止 减速停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short SR_AxisStop(short card, short axis)
        {
            short shrResult;
            shrResult = mc.GT_Stop(card, 1 << (axis - 1), 0);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_Stop", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 设置单卡上所有轴停止 减速停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axisCount">默认8轴卡，4轴卡参数传 4</param>
        /// <returns></returns>
        public static short SR_AxisAllStop(short card, int axisCount = 8)
        {
            short shrResult;
            if (axisCount >= 8)
            {
                shrResult = mc.GT_Stop(card, 255, 0);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_Stop", shrResult);
                    return shrFail;
                }
            }
            else
            {
                shrResult = mc.GT_Stop(card, 15, 0);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_Stop", shrResult);
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 设置单轴紧急停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short SR_AxisEmgStop(short card, short axis)
        {
            short shrResult;
            shrResult = mc.GT_Stop(card, 1 << (axis - 1), 1 << (axis - 1));
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_Stop", shrResult);
                return shrFail;
            }
            shrResult = WaitAxisStop(card, axis);
            if (shrResult != shrSuccess)
            {               
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        ///  设置单卡所有轴紧急停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axisCount">默认8轴卡，4轴卡参数传 4</param>
        /// <returns></returns>
        public static short SR_AxisAllEmgStop(short card, int axisCount = 8)
        {
            short shrResult;
            if (axisCount == 8)
            {
                shrResult = mc.GT_Stop(card, 255, 255);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_Stop", shrResult);
                    return shrFail;
                }
            }
            else
            {
                shrResult = mc.GT_Stop(card, 15, 15);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_Stop", shrResult);
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 暂时不用 设置单轴停止，不等待停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short SR_AxisStopNoStop(short card, short axis)
        {
            return mc.GT_Stop(card, 1 << (axis - 1), 0);
        }
        /// <summary>
        /// 获取轴原点输入状态 单轴
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="bStatus"></param>
        /// <returns>执行此函数需先对函数返回值判断，若不成功，不要使用获取的bStatus结果</returns>
        public static short SR_GetAxisOriginInput(short card, short axis, out bool bStatus)
        {
            short shrResult;
            int pValue = 0;
            shrResult = mc.GT_GetDi(card, mc.MC_HOME, out pValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);
                bStatus = false;
                return shrFail;
            }
            bStatus = (pValue & (1 << (axis - 1))) > 0 ? true : false;
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 原点输入状态 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pValue">原点状态值，按位取</param>
        /// <returns></returns>        
        public static short SR_GetOriginInput(short card, out int pValue)
        {
            short shrResult;
            shrResult = mc.GT_GetDi(card, mc.MC_HOME, out pValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);

                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 正极限输入状态
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short SR_GetLimitPInput(short card, out int pValue)
        {
            short shrResult;
            shrResult = mc.GT_GetDi(card, mc.MC_LIMIT_POSITIVE, out pValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 负极限输入状态
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short SR_GetLimitNInput(short card, out int pValue)
        {
            short shrResult;
            shrResult = mc.GT_GetDi(card, mc.MC_LIMIT_NEGATIVE, out pValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 电机使能输入状态
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short SR_GetEnableInput(short card, out int pValue)
        {
            short shrResult;
            shrResult = mc.GT_GetDo(card, mc.MC_ENABLE, out pValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDo", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 报警输入状态 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short SR_GetAlarmInput(short card, out int pValue)
        {
            short shrResult;
            shrResult = mc.GT_GetDi(card, mc.MC_ALARM, out pValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取单点 输入信号 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="ioBit">0开始， 0-15</param>
        /// <param name="bStatus"></param>
        /// <returns></returns>
        public static short SR_GetInputBit(short card, short ioBit, out bool bStatus)
        {
            short shrResult;
            int intValue = 0;
            ushort ushortValue = 0;
            bStatus = false;
            if (card >= 11)
            {
                shrResult = mc.GT_GetExtIoValueGts(0, (short)(card - intExtendStartId), ref ushortValue);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetExtIoValueGts", shrResult);
                    return shrFail;
                }
                intValue = (int)ushortValue;
            }
            else
            {
                shrResult = mc.GT_GetDi(card, mc.MC_GPI, out intValue);
                if (shrResult != shrSuccess)
                {
                    CommandResult("GT_GetDi", shrResult);
                    return shrFail;
                }
            }
            //intValue = (int)ushortValue;
            bStatus = (intValue & (1 << ioBit)) > 0 ? true : false;
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 所有输入信号 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pValue">所有输入IO状态，按位取</param>
        /// <returns>执行此函数需先对函数返回值判断，若不成功，不要使用获取的pValue结果</returns>
        public static short SR_GetInput(short card, out int pValue)
        {
            short shrResult;
            ushort pValue1 = 0;
            if (card >= 11)
            {
                shrResult = mc.GT_GetExtIoValueGts(0, (short)(card - intExtendStartId), ref pValue1);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetExtIoValueGts", shrResult);
                    pValue = 0;
                    return shrFail;
                }
                pValue = (int)pValue1;
            }
            else
            {
                shrResult = mc.GT_GetDi(card, mc.MC_GPI, out pValue);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetDi", shrResult);
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 设置单点 输出信号 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="ioBit">0开始， 0-15</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short SR_SetOutputBit(short card, short ioBit, short value)
        {
            short shrResult;
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
            return shrSuccess;
        }
        /// <summary>
        /// 设置单卡 输出信号 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short SR_SetOutput(short card, short value)
        {
            short shrResult;
            if (card >= intExtendStartId)
            {
                shrResult = mc.GT_SetExtIoValueGts(0, (short)(card - intExtendStartId), (ushort)value);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_SetExtIoBitGts", shrResult);
                    return shrFail;
                }
            }
            else
            {
                shrResult = mc.GT_SetDo(card, mc.MC_GPO, value);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_SetDoBit", shrResult);
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 所有输出信号状态 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="outputIoStatus"></param>
        /// <returns>执行此函数需先对函数返回值判断，若不成功，不要使用获取的outputIoStatus结果</returns>
        public static short SR_GetOutput(short card, out int outputIoStatus)
        {
            short shrResult;
            outputIoStatus = 0;
            if (card < intExtendStartId)
            {
                shrResult = mc.GT_GetDo(card, mc.MC_GPO, out outputIoStatus);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetDo", shrResult);
                    return shrFail;
                }
                return shrSuccess;
            }
            else
            {
                return shrFail;
            }

        }
        /// <summary>
        /// 轴是否停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <returns>返回0表示轴停止，非0轴运动或函数异常</returns>
        public static short SR_IsAxisStop(short card, short axis)
        {
            int axisStatus;
            uint sClock;
            short shrResult;
            shrResult = mc.GT_GetSts(card, axis, out axisStatus, 1, out sClock);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetSts", shrResult);
                return shrFail;
            }
            if ((axisStatus & 0x400) < 1)
            {
                return shrSuccess;
            }
            return shrFail;
        }
        /// <summary>
        /// 伺服回原函数
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="hightSpeed">回原高速</param>
        /// <param name="lowSpeed">回原低速</param>
        /// <param name="dir">回原方向 0正方向回原  非0 负方向</param>
        /// <returns>返回0表示回原成功，非0轴回原失败或函数异常</returns>
        public static short SR_GoHome(AxisConfig axisC, short card, short axis, double hightSpeed, double lowSpeed, int dir = 0)
        {
            short shrResult, shrCapture = 0;
            int intAxisStatus = 0, intValue, intFirstFindOriginDis = 0, intSecondFindOriginDis = 0, intThreeFindOriginDis = 0, intResult;
            uint uintClock;
            double orgPos;
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }
            if (dir == shtPstMoveDir)
            {
                intFirstFindOriginDis = (int)axisC.intFirstFindOriginDis;
                intSecondFindOriginDis = (int)axisC.intSecondFindOriginDis;
                intThreeFindOriginDis = (int)axisC.intThreeFindOriginDis;
                if (intFirstFindOriginDis == 0)
                {
                    intFirstFindOriginDis = 999999999;
                }
                if (intSecondFindOriginDis == 0)
                {
                    intSecondFindOriginDis = 400000;
                }
                if (intThreeFindOriginDis == 0)
                {
                    intThreeFindOriginDis = 80000;
                }
            }
            else
            {
                intFirstFindOriginDis =0- (int)axisC.intFirstFindOriginDis;
                intSecondFindOriginDis =0- (int)axisC.intSecondFindOriginDis;
                intThreeFindOriginDis =0- (int)axisC.intThreeFindOriginDis;
                if (intFirstFindOriginDis == 0)
                {
                    intFirstFindOriginDis = -999999999;
                }
                if (intSecondFindOriginDis == 0)
                {
                    intSecondFindOriginDis = -400000;
                }
                if (intThreeFindOriginDis == 0)
                {
                    intThreeFindOriginDis = -80000;
                }
            }
            #region 一次找原
            //读取原点感应器信号
            shrResult = mc.GT_GetDi(card, mc.MC_HOME, out intValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);
                return shrFail;
            }
            if ((intValue & (1 << (axis - 1))) > 0)
            {
                shrCapture = 1; //轴在原点位
            }
            else
            {
                //不在原点位，按照设定回零方向，搜原点感应器
                intResult = SR_AbsoluteMove(card, axis, intFirstFindOriginDis, hightSpeed);  //封装轴运
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }
                intResult = CaptureHomeSignalFirst(card, axis, out shrCapture, out intValue);//捕获原点
                if (intResult != shrSuccess)
                    return shrFail;

                //未找到原点 唯一原因找到极限
                if (shrCapture == 0)
                {
                    shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetSts", shrResult);
                        return shrFail;
                    }
                    if (dir == shtPstMoveDir)//正方向回零
                    {
                        if ((intAxisStatus & 0x20) < 1)//正极限未触发
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴回原中正限位感应器信号异常");
                            return shrFail;
                        }
                    }
                    else
                    {
                        if ((intAxisStatus & 0x40) < 1)//负极限未触发
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴回原中负限位感应器信号异常");
                            return shrFail;
                        }
                    }

                    shrResult = mc.GT_GetAxisPrfPos(card, axis, out orgPos, 1, out uintClock);
                    if (shrResult != shrSuccess)
                    {
                        return shrFail;
                    }

                    Thread.Sleep(10);
                    // 点位运动反向找原，传进二次距离是安全考虑，取反是因为反向运动
                    intResult = SR_AbsoluteMove(card, axis, (int)(orgPos - intSecondFindOriginDis), hightSpeed);  //封装轴运 
                    if (intResult != shrSuccess)
                    {
                        return shrFail;
                    }
                    intResult = CaptureHomeSignal(card, axis, out shrCapture, out intValue);//搜索原点
                    if (intResult != shrSuccess)
                        return shrFail;
                }
            }
            #endregion

            #region 移出原点感应器位
            //在此己找到原点
            //判断的意义在于提示己找原点
            if (shrCapture == 1)
            {
                shrResult =SR_AxisEmgStop(card, axis);
                if (shrResult !=shrSuccess)
                {
                    return shrFail;

                }
                shrResult = mc.GT_GetAxisPrfPos(card, axis, out orgPos, 1, out uintClock);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }

                Thread.Sleep(10);
                // 移出原点，传进三次距离是安全考虑，取反是因为与找原方向相反
                intResult = SR_AbsoluteMove(card, axis, (int)(orgPos - intThreeFindOriginDis), lowSpeed);  //封装轴运                
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }

                Thread.Sleep(300);//延时一会
                DateTime startTime = DateTime.Now;
                DateTime countTime;
                //让轴运动出原点位
                do
                {
                    shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetSts", shrResult);
                        return shrFail;
                    }
                    shrResult = mc.GT_GetDi(card, mc.MC_HOME, out intValue);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetDi", shrResult);
                        return shrFail;
                    }
                    if ((intValue & (1 << (axis - 1))) < 1)
                    {
                        shrCapture = 0;
                        break;
                    }
                    //countTime = DateTime.Now;
                    //if ((countTime - startTime).TotalSeconds > dblMoveOutOriginTime)
                    //{
                    //    SR_AxisEmgStop(card, axis);
                    //    CFile.WriteErrorForDate(card + "卡" + axis + "轴移出原点感应器信号超时，时长：" + dblMoveOutOriginTime);                        
                    //    return shrFail;
                    //}
                } while ((intAxisStatus & 0x400) > 0);
            }
            else
            {
                CFile.WriteErrorForDate(card + "卡" + axis + "轴 一次找原异常");
                return shrFail;
            }
            //移出原点失败
            if (shrCapture != 0)
                return shrFail;
            #endregion

            #region 二次找原
            shrResult = SR_AxisEmgStop(card, axis);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            intResult = SR_AbsoluteMove(card, axis, (int)(orgPos + intThreeFindOriginDis), lowSpeed);  //封装轴运            
            if (intResult != shrSuccess)
            {
                return shrFail;
            }
            //intResult = ChangePostion(card, axis, intThreeFindOriginDis, lowSpeed);
            //if (intResult != shrSuccess)
            //    return shrFail;
            //启动找HOME
            intResult = CaptureHomeSignal(card, axis, out shrCapture, out intValue);
            if (intResult != shrSuccess)
                return shrFail;
            if (shrCapture == 1)
            {
                CFile.WriteErrorForDate(card + "卡" + axis + "轴  运动去原点坐标位" + intValue);
                shrResult = SR_AxisEmgStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;

                }
                intResult = SR_AbsoluteMove(card, axis, intValue, lowSpeed);  //封装轴运
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }
                //让轴运动到捕获的原点位
                do
                {
                    shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetSts", shrResult);
                        return shrFail;
                    }
                    if ((intAxisStatus & 0x20) > 0)//正极限触发
                    {
                        CFile.WriteErrorForDate(card + "卡" + axis + "轴  运动去原点坐标位正限位异常");
                        return shrFail;
                    }
                    if ((intAxisStatus & 0x40) > 0)//负极限触发
                    {
                        CFile.WriteErrorForDate(card + "卡" + axis + "轴 运动去原点坐标位负限位异常");
                        return shrFail;
                    }
                } while ((intAxisStatus & 0x400) > 0);
            }
            else
            {
                return shrFail;
            }
            #endregion

            //在此二次回原点己完成  pValue为原点位置           
            Thread.Sleep(700);
            //坐标清0
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }

        /// 单原点回原函数
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="hightSpeed">回原高速</param>
        /// <param name="lowSpeed">回原低速</param>
        /// <param name="dir">回原方向 0正方向回原  非0 负方向</param>
        /// <returns>返回0表示回原成功，非0轴回原失败或函数异常</returns>
        public static short SR_GoHomeSingel(short card, short axis, double hightSpeed, double lowSpeed, int singel_dis , int singel_max , int singel_min , int intThreeFindOriginDis ,int dir = 0 )
        {
            short shrResult, shrCapture = 0;
            int intAxisStatus = 0, intValue, intFirstFindOriginDis = 0, intSecondFindOriginDis = 0, intResult;
            uint uintClock;
            double orgPos;
            //int singel_dis = 1000;
            //int singel_max = 10000;
            //int singel_min = -10000;
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }

            if (dir == shtPstMoveDir)
            {
                intFirstFindOriginDis = 999999999;
                intSecondFindOriginDis = 400000;
                //intThreeFindOriginDis = 1000;
            }
            else
            {
                intFirstFindOriginDis = -999999999;
                intSecondFindOriginDis = -400000;
                //intThreeFindOriginDis = -1000;
            }
            #region 一次找原
            //读取原点感应器信号
            shrResult = mc.GT_GetDi(card, mc.MC_HOME, out intValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);
                return shrFail;
            }
            if ((intValue & (1 << (axis - 1))) > 0)
            {
                shrCapture = 1; //轴在原点位
            }
            else
            {
                while (true)
                {
                    //不在原点位，按照设定回零方向，搜原点感应器
                    intResult = SR_RelativeMove(card, axis, singel_dis, hightSpeed);  //封装轴运
                    if (intResult != shrSuccess)
                    {
                        return shrFail;
                    }
                    intResult = CaptureHomeSignalFirst(card, axis, out shrCapture, out intValue);//捕获原点
                    if (intResult != shrSuccess)
                        return shrFail;

                    if (shrCapture == 0)
                    {
                        singel_dis = 0 - singel_dis * 2;
                    }
                    else
                    {
                        break;
                    }
                    if (singel_dis > singel_max || singel_dis < singel_min)
                    {
                        CFile.WriteErrorForDate(card + "卡" + axis + "找原点失败");
                        break;
                    }
                }

            }
            #endregion

            #region 移出原点感应器位
            ////在此己找到原点
            ////判断的意义在于提示己找原点
            if (shrCapture == 1)
            {
                shrResult = SR_AxisEmgStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;

                }
            
                shrResult = mc.GT_GetAxisPrfPos(card, axis, out orgPos, 1, out uintClock);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }

                Thread.Sleep(10);
                // 移出原点，传进三次距离是安全考虑，取反是因为与找原方向相反
                intResult = SR_RelativeMove(card, axis, (int)(orgPos - intThreeFindOriginDis), lowSpeed);  //封装轴运                
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }
                Thread.Sleep(20);
                DateTime startTime = DateTime.Now;
                DateTime countTime;

                shrResult = WaitAxisStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
           
            }
            else
            {
                CFile.WriteErrorForDate(card + "卡" + axis + "轴 一次找原异常");
                return shrFail;
            }
  
            #endregion

            #region 二次找原
            shrResult = SR_AxisEmgStop(card, axis);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            intResult = SR_RelativeMove(card, axis, (int)(orgPos + intThreeFindOriginDis), lowSpeed);  //封装轴运            
            if (intResult != shrSuccess)
            {
                return shrFail;
            }
            //intResult = ChangePostion(card, axis, intThreeFindOriginDis, lowSpeed);
            //if (intResult != shrSuccess)
            //    return shrFail;
            //启动找HOME
            intResult = CaptureHomeSignal(card, axis, out shrCapture, out intValue);
            if (intResult != shrSuccess)
                return shrFail;
            if (shrCapture == 1)
            {
                CFile.WriteErrorForDate(card + "卡" + axis + "轴  运动去原点坐标位" + intValue);
                shrResult = SR_AxisEmgStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;

                }
               
            }
            else
            {
                return shrFail;
            }
            #endregion

            //在此二次回原点己完成  pValue为原点位置           
            Thread.Sleep(700);
            //坐标清0
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }


        /// <summary>
        /// 上矽钢片回原函数
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="hightSpeed">回原高速</param>
        /// <param name="lowSpeed">回原低速</param>
        /// <param name="dir">回原方向 0正方向回原  非0 负方向</param>
        /// <returns>返回0表示回原成功，非0轴回原失败或函数异常</returns>
        public static short SR_GoHomeTest(short card, short axis, double hightSpeed, double lowSpeed, int dir = 0)
        {
            short shrResult, shrCapture = 0;
            int intAxisStatus = 0, intValue, intFirstFindOriginDis = 0, intSecondFindOriginDis = 0, intThreeFindOriginDis = 0, intResult;
            uint uintClock;
            double orgPos = 0;
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }
            if (dir == shtPstMoveDir)
            {
                intFirstFindOriginDis = 999999999;
                intSecondFindOriginDis = 400000;
                intThreeFindOriginDis = 70000;
            }
            else
            {
                intFirstFindOriginDis = -999999999;
                intSecondFindOriginDis = -400000;
                intThreeFindOriginDis = -70000;
            }
            #region 一次找原
            //读取原点感应器信号
            shrResult = mc.GT_GetDi(card, mc.MC_HOME, out intValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);
                return shrFail;
            }
            if ((intValue & (1 << (axis - 1))) > 0)
            {
                shrCapture = 1; //轴在原点位
            }
            else
            {
                //不在原点位，按照设定回零方向，搜原点感应器
                intResult = SR_AbsoluteMove(card, axis, intFirstFindOriginDis, hightSpeed);  //封装轴运
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }
                intResult = CaptureHomeSignalForStepMotorFirst(card, axis, out shrCapture, out intValue);//捕获原点
                if (intResult != shrSuccess)
                    return shrFail;

                //未找到原点 唯一原因找到极限
                if (shrCapture == 0)
                {
                    shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetSts", shrResult);
                        return shrFail;
                    }
                    if (dir == shtPstMoveDir)//正方向回零
                    {
                        if ((intAxisStatus & 0x20) < 1)//正极限未触发
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴回原中正限位感应器信号异常");
                            return shrFail;
                        }
                    }
                    else
                    {
                        if ((intAxisStatus & 0x40) < 1)//负极限未触发
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴回原中负限位感应器信号异常");
                            return shrFail;
                        }
                    }


                    intResult = mc.GT_GetAxisPrfPos(card, axis, out orgPos, 1, out uintClock);
                    if (intResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetAxisPrfPos", (short)intResult);
                        return shrFail;
                    }
                    // 点位运动反向找原，传进二次距离是安全考虑，取反是因为反向运动
                    intResult = SR_AbsoluteMove(card, axis, (int)(orgPos - intSecondFindOriginDis), hightSpeed);  //封装轴运 
                    if (intResult != shrSuccess)
                    {
                        return shrFail;
                    }
                    intResult = CaptureHomeSignalForStepMotor(card, axis, out shrCapture, out intValue);//搜索原点
                    if (intResult != shrSuccess)
                        return shrFail;
                }
            }
            #endregion

            #region 移出原点感应器位
            //在此己找到原点
            //判断的意义在于提示己找原点
            if (shrCapture == 1)
            {
                shrResult = SR_AxisEmgStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;

                }

                intResult = mc.GT_GetAxisPrfPos(card, axis, out orgPos, 1, out uintClock);
                if (intResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetAxisPrfPos", (short)intResult);
                    return shrFail;
                }
                // 移出原点，传进三次距离是安全考虑，取反是因为与找原方向相反
                intResult = SR_AbsoluteMove(card, axis, (int)(orgPos - intThreeFindOriginDis), lowSpeed);  //封装轴运
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }
                DateTime startTime = DateTime.Now;
                DateTime countTime;

                shrResult = WaitAxisStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                //让轴运动出原点位
            
            }
            else
            {
                CFile.WriteErrorForDate(card + "卡" + axis + "轴 一次找原异常");
                return shrFail;
            }

            #endregion

            #region 二次找原
            shrResult = SR_AxisEmgStop(card, axis);
            if (shrResult != shrSuccess)
            {
                return shrFail;

            }
            intResult = SR_AbsoluteMove(card, axis, (int)(orgPos + intThreeFindOriginDis), lowSpeed);
            if (intResult != shrSuccess)
                return shrFail;
            //启动找HOME
            intResult = CaptureHomeSignalForStepMotor(card, axis, out shrCapture, out intValue);
            if (intResult != shrSuccess)
                return shrFail;
            if (shrCapture == 1)
            {
                shrResult = SR_AxisEmgStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;

                }
                intResult = SR_AbsoluteMove(card, axis, intValue, lowSpeed);  //封装轴运
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }
                
            }
            else
            {
                return shrFail;
            }
            #endregion

            //在此二次回原点己完成  pValue为原点位置           
            Thread.Sleep(700);
            //坐标清0
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
       


        /// <summary>
        /// 步进回原函数
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="hightSpeed">回原高速</param>
        /// <param name="lowSpeed">回原低速</param>
        /// <param name="dir">回原方向 0正方向回原  非0 负方向</param>
        /// <returns>返回0表示回原成功，非0轴回原失败或函数异常</returns>
        public static short SR_GoHomeWithStepMotor(AxisConfig axisC,short card, short axis, double hightSpeed, double lowSpeed, int dir = 0)
        {
            short shrResult, shrCapture = 0;
            int intAxisStatus = 0, intValue, intFirstFindOriginDis = 0, intSecondFindOriginDis = 0, intThreeFindOriginDis = 0, intResult;
            uint uintClock;
            double orgPos = 0;
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }
       
           

            if (dir == shtPstMoveDir)
            {
                intFirstFindOriginDis = (int)axisC.intFirstFindOriginDis;
                intSecondFindOriginDis = (int)axisC.intSecondFindOriginDis;
                intThreeFindOriginDis = (int)axisC.intThreeFindOriginDis;
                if(intFirstFindOriginDis == 0)
                {
                    intFirstFindOriginDis = 999999999;
                }
                 if(intSecondFindOriginDis == 0)
                {
                    intSecondFindOriginDis = 400000;
                }
                if(intThreeFindOriginDis == 0)
                {
                    intThreeFindOriginDis = 80000;
                }
            }
            else
            {
                intFirstFindOriginDis = 0-(int)axisC.intFirstFindOriginDis;
                intSecondFindOriginDis =0-(int)axisC.intSecondFindOriginDis;
                intThreeFindOriginDis = 0-(int)axisC.intThreeFindOriginDis;
                if (intFirstFindOriginDis == 0)
                {
                    intFirstFindOriginDis = -999999999;
                }
                if (intSecondFindOriginDis == 0)
                {
                    intSecondFindOriginDis = -400000;
                }
                if (intThreeFindOriginDis == 0)
                {
                    intThreeFindOriginDis = -80000;
                }
            }
            #region 一次找原
            //读取原点感应器信号
            shrResult = mc.GT_GetDi(card, mc.MC_HOME, out intValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDi", shrResult);
                return shrFail;
            }
            if ((intValue & (1 << (axis - 1))) > 0)
            {
                shrCapture = 1; //轴在原点位
            }
            else
            {
                //不在原点位，按照设定回零方向，搜原点感应器
                intResult = SR_AbsoluteMove(card, axis, intFirstFindOriginDis, hightSpeed);  //封装轴运
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }
                intResult = CaptureHomeSignalForStepMotorFirst(card, axis, out shrCapture, out intValue);//捕获原点
                if (intResult != shrSuccess)
                    return shrFail;

                //未找到原点 唯一原因找到极限
                if (shrCapture == 0)
                {
                    shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetSts", shrResult);
                        return shrFail;
                    }
                    if (dir == shtPstMoveDir)//正方向回零
                    {
                        if ((intAxisStatus & 0x20) < 1)//正极限未触发
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴回原中正限位感应器信号异常");
                            return shrFail;
                        }
                    }
                    else
                    {
                        if ((intAxisStatus & 0x40) < 1)//负极限未触发
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴回原中负限位感应器信号异常");
                            return shrFail;
                        }
                    }


                    intResult = mc.GT_GetAxisPrfPos(card, axis, out orgPos, 1, out uintClock);
                    if (intResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetAxisPrfPos", (short)intResult);
                        return shrFail;
                    }
                    // 点位运动反向找原，传进二次距离是安全考虑，取反是因为反向运动
                    intResult = SR_AbsoluteMove(card, axis, (int)(orgPos - intSecondFindOriginDis), hightSpeed);  //封装轴运 
                    Thread.Sleep(50);//lrz
                    if (intResult != shrSuccess)
                    {
                        return shrFail;
                    }
                    intResult = CaptureHomeSignalForStepMotor(card, axis, out shrCapture, out intValue);//搜索原点
                    if (intResult != shrSuccess)
                        return shrFail;
                }
            }
            #endregion

            #region 移出原点感应器位
            //在此己找到原点
            //判断的意义在于提示己找原点
            if (shrCapture == 1)
            {
                shrResult = SR_AxisEmgStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;

                }

                intResult = mc.GT_GetAxisPrfPos(card, axis, out orgPos, 1, out uintClock);
                if (intResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetAxisPrfPos", (short)intResult);
                    return shrFail;
                }
                // 移出原点，传进三次距离是安全考虑，取反是因为与找原方向相反
                intResult = SR_AbsoluteMove(card, axis, (int)(orgPos - intThreeFindOriginDis), lowSpeed);  //封装轴运
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }

                Thread.Sleep(200);
                DateTime startTime = DateTime.Now;
                DateTime countTime;
                //让轴运动出原点位
                do
                {
                    shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetSts", shrResult);
                        return shrFail;
                    }
                    shrResult = mc.GT_GetDi(card, mc.MC_HOME, out intValue);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetDi", shrResult);
                        return shrFail;
                    }
                    if ((intValue & (1 << (axis - 1))) < 1)
                    {
                        shrCapture = 0;
                        break;
                    }
                    countTime = DateTime.Now;
                    if ((countTime - startTime).TotalSeconds > dblMoveOutOriginTime)
                    {
                        CFile.WriteErrorForDate(card + "卡" + axis + "轴移出原点感应器信号超时，时长：" + dblMoveOutOriginTime);
                        shrResult = SR_AxisEmgStop(card, axis);
                        if (shrResult != shrSuccess)
                        {
                            return shrFail;

                        }
                        return shrFail;
                    }
                } while ((intAxisStatus & 0x400) > 0);
            }
            else
            {
                CFile.WriteErrorForDate(card + "卡" + axis + "轴 一次找原异常");
                return shrFail;
            }
            //移出原点失败
            if (shrCapture != 0)
                return shrFail;
            #endregion


            #region 二次找原
            shrResult = SR_AxisEmgStop(card, axis);
            if (shrResult != shrSuccess)
            {
                return shrFail;

            }
            intResult = SR_AbsoluteMove(card, axis, (int)(orgPos + intThreeFindOriginDis), lowSpeed);
            if (intResult != shrSuccess)
                return shrFail;
            //启动找HOME
            intResult = CaptureHomeSignalForStepMotor(card, axis, out shrCapture, out intValue);
            if (intResult != shrSuccess)
                return shrFail;
            if (shrCapture == 1)
            {
                shrResult = SR_AxisEmgStop(card, axis);
                if (shrResult != shrSuccess)
                {
                    return shrFail;

                }
                intResult = SR_AbsoluteMove(card, axis, intValue, lowSpeed);  //封装轴运
                if (intResult != shrSuccess)
                {
                    return shrFail;
                }
                //让轴运动到捕获的原点位
                do
                {
                    shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_GetSts", shrResult);
                        return shrFail;
                    }
                    if ((intAxisStatus & 0x20) > 0)//正极限触发
                    {
                        CFile.WriteErrorForDate(card + "卡" + axis + "轴  运动去原点坐标位正限位异常");
                        return shrFail;
                    }
                    if ((intAxisStatus & 0x40) > 0)//负极限触发
                    {
                        CFile.WriteErrorForDate(card + "卡" + axis + "轴 运动去原点坐标位负限位异常");
                        return shrFail;
                    }
                } while ((intAxisStatus & 0x400) > 0);
            }
            else
            {
                return shrFail;
            }
            #endregion

            //在此二次回原点己完成  pValue为原点位置           
            Thread.Sleep(700);
            //坐标清0
            shrResult = mc.GT_ZeroPos(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ZeroPos", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 单轴相对运动
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="postion">目标点位</param>
        /// <param name="speed">速度</param>
        /// <returns></returns>
        public static short SR_RelativeMove(short card, short axis, int postion, double speed)
        {
            short shrResult;
            double dblPos = 0;
            short shrCount = 1;
            uint uintClock = 0;
            //设置为点位模试
            shrResult = mc.GT_PrfTrap(card, axis);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_PrfTrap", shrResult);
                return shrFail;
            }
            ////轴跟随功能
            //shrResult = mc.GT_SetAxisBand(card, axis, Torlerance, DtTime);
            //if (shrResult != shrGtsSuccess)
            //{
            //    CommandResult("GT_SetAxisBand", shrResult);
            //    return shrFail;
            //}

            //mc.TTrapPrm tprm;
            ////读取点位运动参数           
            //shrResult = mc.GT_GetTrapPrm(card, axis, out tprm);
            //if (shrResult != shrGtsSuccess)
            //{
            //    CommandResult("GT_SetPos", shrResult);
            //    return shrFail;
            //}
            //tprm.acc = dblAxisAcc;
            //tprm.dec = dblAxisDec;
            //tprm.smoothTime = shtSmoothTime;
            ////设置点位运动参数
            //shrResult = mc.GT_SetTrapPrm(card, axis, ref  tprm);
            //if (shrResult != shrGtsSuccess)
            //{
            //    CommandResult("GT_SetPos", shrResult);
            //    return shrFail;
            //}

            shrResult = mc.GT_GetPrfPos(card, axis, out dblPos, shrCount, out uintClock);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetPrfPos", shrResult);
                return -1;
            }
            //设置目标位置
            dblPos = dblPos + postion;
            //设置目标位置
            shrResult = mc.GT_SetPos(card, axis, postion);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetPos", shrResult);
                return shrFail;
            }
            //设置轴运动速度
            shrResult = mc.GT_SetVel(card, axis, speed);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetVel", shrResult);
                return shrFail;
            }
            shrResult = mc.GT_ClrSts(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ClrSts", shrResult);
                return shrFail;
            }
            //启动轴运动
            shrResult = mc.GT_Update(card, 1 << (axis - 1));
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_Update", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 单轴绝对运动
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="postion">目标点位</param>
        /// <param name="speed">速度</param>
        /// <returns></returns>
        public static short SR_AbsoluteMove(short card, short axis, int postion, double speed)
        {
            short shrResult;
            //设置为点位模试
            shrResult = mc.GT_PrfTrap(card, axis);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_PrfTrap", shrResult);
                return shrFail;
            }
            ////轴跟随功能
            //shrResult = mc.GT_SetAxisBand(card, axis, Torlerance, DtTime);
            //if (shrResult != shrGtsSuccess)
            //{
            //    CommandResult("GT_SetAxisBand", shrResult);
            //    return shrFail;
            //}

            //mc.TTrapPrm tprm;
            ////读取点位运动参数           
            //shrResult = mc.GT_GetTrapPrm(card, axis, out tprm);
            //if (shrResult != shrGtsSuccess)
            //{
            //    CommandResult("GT_GetTrapPrm", shrResult);
            //    return shrFail;
            //}
            //tprm.acc = dblAxisAcc;
            //tprm.dec = dblAxisDec;
            //tprm.smoothTime = shtSmoothTime;
            ////设置点位运动参数
            //shrResult = mc.GT_SetTrapPrm(card, axis, ref  tprm);
            //if (shrResult != shrGtsSuccess)
            //{
            //    CommandResult("GT_SetTrapPrm", shrResult);
            //    return shrFail;
            //}
            shrResult = mc.GT_ClrSts(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ClrSts", shrResult);
                return shrFail;
            }
            //设置目标位置
            shrResult = mc.GT_SetPos(card, axis, postion);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetPos", shrResult);
                return shrFail;
            }
            //设置轴运动速度
            shrResult = mc.GT_SetVel(card, axis, speed);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetVel", shrResult);
                return shrFail;
            }
            shrResult = mc.GT_ClrSts(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ClrSts", shrResult);
                return shrFail;
            }
            //启动轴运动
            shrResult = mc.GT_Update(card, 1 << (axis - 1));
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_Update", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 单轴连续运动  JOG模试
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="speed"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static short SR_ContinueMove(short card, short axis, double speed, short dir = 0)
        {
            if (dir != shtPstMoveDir)
            {
                speed = -speed;
            }
            short shrResult;
            mc.TJogPrm jog;
            shrResult = mc.GT_PrfJog(card, axis);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_PrfJog", shrResult);
                return shrFail;
            }
            shrResult = mc.GT_GetJogPrm(card, axis, out jog);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetJogPrm", shrResult);
                return shrFail;
            }
            jog.acc = 0.0625;
            jog.dec = 0.0625;

            shrResult = mc.GT_SetJogPrm(card, axis, ref  jog);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetJogPrm", shrResult);
                return shrFail;
            }
            shrResult = mc.GT_SetVel(card, axis, speed);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetVel", shrResult);
                return shrFail;
            }

            shrResult = mc.GT_Update(card, 1 << (axis - 1));
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_Update", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 未完成
        /// </summary>
        /// <returns></returns>
        public static short SR_SetSoftLimit()
        {
            return shrSuccess;
        }
        /// <summary>
        /// 设置加速度
        /// </summary>
        /// <returns></returns>
        public static short SR_SetAcc()
        {
            return shrSuccess;
        }
        /// <summary>
        /// 设置减速度
        /// </summary>
        /// <returns></returns>
        public static short SR_SetDec()
        {
            return shrSuccess;
        }
        /// <summary>
        /// 未实现 设置运动速度
        /// </summary>
        /// <returns></returns>
        public static short SR_SetSpeed()
        {
            return shrSuccess;
        }

        public static short SR_SetAccAndDec(short card,short axis)
        {
            short shrResult;
            //设置为点位模试
            shrResult = mc.GT_PrfTrap(card, axis);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_PrfTrap", shrResult);
                return shrFail;
            }

            mc.TTrapPrm tprm;
            //读取点位运动参数           
            shrResult = mc.GT_GetTrapPrm(card, axis, out tprm);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetTrapPrm", shrResult);
                return shrFail;
            }
            tprm.acc = dblAxisAcc;
            tprm.dec = dblAxisDec;
            tprm.smoothTime = shtSmoothTime;
            //设置点位运动参数
            shrResult = mc.GT_SetTrapPrm(card, axis, ref  tprm);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetTrapPrm", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 未完成
        /// </summary>
        /// <returns></returns>

        public static short SR_SetStartV()
        {
            return shrSuccess;
        }
        /// <summary>
        /// 未完成
        /// </summary>
        /// <returns></returns>
        public static short SR_GetHomeMode()
        {
            return shrSuccess;
        }
        /// <summary>
        /// 未完成
        /// </summary>
        /// <returns></returns>
        public static short SR_SetHomeMode()
        {
            return shrSuccess;
        }
        /// <summary>
        /// 未完成 设置加减速模式 
        /// </summary>
        /// <returns></returns>
        public static short SR_SetAccDelMode()
        {
            return shrSuccess;
        }
        /// <summary>
        /// 获取当前速度
        /// </summary>
        /// <returns></returns>
        public static short SR_GetSpeed()
        {
            return shrSuccess;
        }
        //crdPrm 坐标结构购体
        //axisID 当前指定的轴号
        //axis：1坐标系X，2坐标系Y，3坐标系Z，4坐标系U。只有三种组合：XY，XYZ，XYZU
        public static short SR_SetcoordinateAxis(ref mc.TCrdPrm crdPrm, short axisID, short axisOrdinate)
        {
            if (axisID > 8)
            {
                //MessageBoxLog.Show("错误，赋值的轴号不能大于8");
                return shrFail;
            }
            if (axisOrdinate > 4)
            {
                //MessageBoxLog.Show("错误，赋值的坐标系值不能大于4");
                return shrFail;
            }
            switch (axisID)
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
                default:
                    break;
            }
            return shrSuccess;
        }
        //axises 要插补的轴，几维传几个轴进来
        //coordinateID要建立的坐标系ID：1或2
        private static short SR_SetCoords(short card, short coordinateID, params short[] axises)
        {
            if (coordinateID != 1 && coordinateID != 2)
            {
                MessageBoxLog.Show("错误，要设置的坐标系参数只能是1或2");
                return shrFail;
            }
            int nDimention = axises.Length;
            mc.TCrdPrm crdPrm = new mc.TCrdPrm();//坐标系结构体
            crdPrm.dimension = (short)nDimention;// 维数赋值
            crdPrm.synVelMax = 500;//最大合成速度 pulse/ms default:500
            crdPrm.synAccMax = 1;//最大加速度
            crdPrm.evenTime = 50;//最小匀速时间
            crdPrm.setOriginFlag = 1;// 需要设置加工坐标系原点位置

            if (nDimention >= 2)
            {
                SR_SetcoordinateAxis(ref crdPrm, axises[0], 1);//规划器1
                SR_SetcoordinateAxis(ref crdPrm, axises[1], 2);//规划器2
                crdPrm.originPos1 = 0;//加工坐标系原点位置在(0,0,0)，即与机床坐标系原点重合
                crdPrm.originPos2 = 0;//
            }
            if (nDimention >= 3)
            {
                SR_SetcoordinateAxis(ref crdPrm, axises[2], 3);//规划器3
                crdPrm.originPos3 = 0;//
            }
            if (nDimention >= 4)
            {
                SR_SetcoordinateAxis(ref crdPrm, axises[3], 4);//规划器4
                crdPrm.originPos4 = 0;//
            }

            short shrResult = mc.GT_SetCrdPrm(card, coordinateID, ref crdPrm);//建立1号坐标系，坐标系编号：1，2
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetCrdPrm", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        //两轴直线插补
        public static short SR_InpMove2(short card, short axis1, short axis2, int pos1, int pos2, double speed, double acc, short ordinateID = 1)
        {
            short ret = shrSuccess;
            #region 建立坐标系
            mc.TCrdPrm crdPrm = new mc.TCrdPrm();//坐标系结构体
            crdPrm.dimension = 2;// two dimention
            crdPrm.synVelMax = 500;//最大合成速度 pulse/ms default:500
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
                pos1, pos2, // 该插补段的终点坐标(200000, 0)
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
            sRtn = mc.GT_Stop(card, 255, 255);
            if (sRtn != 0)
            {
                MessageBoxLog.Show("命令失败！");
            }
            sRtn = mc.GT_Stop(card, 511, 511);
            if (sRtn != 0)
            {
                MessageBoxLog.Show("命令失败！");
            }
            return ret;
        }
        //3轴直线插补 未完成
        public static short SR_InpMove3(short card, short axis1, short axis2, short axis3,
                int pos1, int pos2, int pos3, double speed, double acc, short ordinateID = 1)
        {
            short ret = shrSuccess;

            SR_SetCoords(card, ordinateID, axis1, axis2, axis3);
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

            sRtn = mc.GT_Stop(card, 255, 255);
            if (sRtn != 0)
            {
                MessageBoxLog.Show("命令失败！");
            }
            sRtn = mc.GT_Stop(card, 511, 511);
            if (sRtn != 0)
            {
                MessageBoxLog.Show("命令失败！");
            }

            return ret;
        }
        //4轴直线插补 未完成
        public static short SR_InpMove4(short card, short axis1, short axis2, short axis3, short axis4,
             int pos1, int pos2, int pos3, int pos4, double speed, double acc, short ordinateID = 1)
        {
            short ret = shrSuccess;
            SR_SetCoords(card, ordinateID, axis1, axis2, axis3, axis4);

            short sRtn;
            sRtn = mc.GT_CrdClear(card, ordinateID, 0);
            //查询坐标系1的FIFO0所剩余的空间

            sRtn = mc.GT_LnXYZA(
                card,
                ordinateID, // 该插补段的坐标系是坐标系1
                pos1, pos2, pos3, pos4,// 该插补段的终点坐标(200000, 0)
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

            sRtn = mc.GT_Stop(card, 255, 255);
            if (sRtn != 0)
            {
                MessageBoxLog.Show("命令失败！");
            }
            sRtn = mc.GT_Stop(card, 511, 511);
            if (sRtn != 0)
            {
                MessageBoxLog.Show("命令失败！");
            }

            return ret;
        }
        //两轴圆弧插补 未完成
        //dir 0顺时针，1逆时针
        public static short SR_InpArc(short card, short axis1, short axis2, int pos1, int pos2, double speed, double acc, double radius, short dir, short ordinateID = 1)
        {
            short ret = shrSuccess;

            ret = SR_SetCoords(card, ordinateID, axis1, axis2);
            if (ret != shrSuccess)
            {
                return shrFail;
            }            

            short sRtn;
            // 坐标系运动状态查询变量
            short run;
            // 坐标系运动完成段查询变量
            int segment;
            sRtn = mc.GT_CrdClear(card, ordinateID, 0);
            if (sRtn != shrGtsSuccess)
            {
                CommandResult("GT_CrdClear", sRtn);
                return shrFail;
            }

            sRtn = mc.GT_ArcXYR(
              card,
              ordinateID, // 坐标系是坐标系1
              pos1, pos2, // 该圆弧的终点坐标(0, 200000)
              radius, // 半径：200000pulse
              dir, // 该圆弧是逆时针圆弧
              speed, // 该插补段的目标速度：100pulse/ms
              acc, // 该插补段的加速度：0.1pulse/ms^2
              0, // 终点速度为0
              0); // 向坐标系1的FIFO0缓存区传递该直线插补数据
            if (sRtn!=shrGtsSuccess)
            {
                return shrFail;
            }
            

            sRtn = mc.GT_CrdStart(card, ordinateID, 0);  //插补起动
            if (sRtn != shrGtsSuccess)
            {
                CommandResult("GT_CrdStart", sRtn);
                return shrFail;
            }
            // 等待运动完成
            sRtn = mc.GT_CrdStatus(card, ordinateID, out run, out segment, 0); 
            if (sRtn != shrGtsSuccess)
            {
                CommandResult("GT_CrdStatus", sRtn);
                return shrFail;
            }
            Thread.Sleep(10);
            do
            {
                // 查询坐标系1的FIFO的插补运动状态
                sRtn = mc.GT_CrdStatus(
                card,        //卡号
                ordinateID,  // 坐标系是坐标系
                out run,     //读取插补运动状态
                out segment, // 读取当前已经完成的插补段数
                0);          // 查询坐标系1的FIFO0缓存区
                // 坐标系在运动, 查询到的run的值为1
            } while (run == 1);

            sRtn = SR_AxisStop(card, axis1);
            if (sRtn != shrSuccess)
            {                
                return shrFail;
            }
            sRtn = SR_AxisStop(card, axis2);
            if (sRtn != shrSuccess)
            {
                return shrFail;
            }
            sRtn=SR_SetAccAndDec(card, axis1);
            if (sRtn != shrSuccess)
            {
                return shrFail;
            }
            sRtn = SR_SetAccAndDec(card, axis2);
            if (sRtn != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;

        }
        //获取版本号
        public static short SR_GetLibVision(short card, out string strVersion)
        {
            strVersion = "";
            return shrSuccess;
        }
        #endregion

        /// <summary>
        /// 程序初始化卡函数
        /// </summary>
        /// <returns></returns>
        public static short InitCard()
        {
            short shrResult;
            short carNum = 0;
            //按卡配置初始化卡
            for (carNum = 0; carNum < intCardCount; carNum++)
            {
                if (carNum > 0)
                {
                    shrResult = SR_InitCard(carNum, intAxisCountForCard1, Application.StartupPath + "\\GTS800" + carNum + ".cfg");
                    //shrResult = SR_InitCard(carNum, intAxisCountForCard);
                    if (shrResult != shrSuccess)
                    {
                        break;
                    }
                }
                else 
                {
                    shrResult = SR_InitCard(carNum, intAxisCountForCard, Application.StartupPath + "\\GTS800" + carNum + ".cfg");
                    //shrResult = SR_InitCard(carNum, intAxisCountForCard);
                    if (shrResult != shrSuccess)
                    {
                        break;
                    }
                }

                //shrResult = SR_InitCard(carNum, intAxisCountForCard, Application.StartupPath + "\\GTS800" + carNum + ".cfg");
                ////shrResult = SR_InitCard(carNum, intAxisCountForCard);
                //if (shrResult != shrSuccess)
                //{
                //    break;
                //}
            }
            if (carNum > (intCardCount - 1))
            {
                if (intExtendIoCount > 0)
                {
                    shrResult = mc.GT_OpenExtMdlGts(0, "Gts.dll");
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_OpenExtMdlGts", shrResult);
                        return shrFail;
                    }
                    shrResult = mc.GT_LoadExtConfigGts(0, Application.StartupPath + "\\ExtModule.cfg");
                    if (shrResult != shrGtsSuccess)
                    {
                        CommandResult("GT_OpenExtMdlGts", shrResult);
                        return shrFail;
                    }
                    tag_initResult = shrSuccess;
                    return shrSuccess;
                }
                else
                {
                    tag_initResult = shrSuccess;
                    return shrSuccess;
                }
            }
            else
            {
                tag_initResult = shrFail;
                return shrFail;
            }
        }
        /// <summary>
        /// 两轴圆孤插补
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="pointName1"></param>
        /// <param name="pointName2"></param>
        /// <param name="pointName3"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static short InpArc(string stationName, string pointName1, string pointName2, string pointName3, params string[] axisName)
        {
            double radius=0;
            AxisConfig[] axis = new AxisConfig[axisName.Length];
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 查找工位" + stationName + "执行异常，返回空");
                return shrFail;
            }
            for (int i = 0; i < axis.Length;i++ )
            {
               
                axis[i] = StationManage.FindAxis(stationM, axisName[i]);
                if (axis[i] == null)
                {
                    CFile.WriteErrorForDate("函数：FindAxis 查找轴 " + axisName[i] + " 执行异常，返回空");
                    return shrFail;
                }
            }
           
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName1);
            if (pointA == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 查找点" + pointName1 + " 执行异常，返回空");
                return shrFail;
            }

            PointAggregate pointB = StationManage.FindPoint(stationM, pointName2);
            if (pointB == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 查找点" + pointName2 + " 执行异常，返回空");
                return shrFail;
            }

            PointAggregate pointC = StationManage.FindPoint(stationM, pointName3);
            if (pointC == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 查找点" + pointName3 + " 执行异常，返回空");
                return shrFail;
            }
            double X1,Y1,X2,Y2,X3,Y3;
            X1=pointA.arrPoint[axis[0].AxisIndex].dblPonitValue * axis[0].Eucf;
            Y1=pointA.arrPoint[axis[1].AxisIndex].dblPonitValue * axis[1].Eucf;
            X2=pointB.arrPoint[axis[0].AxisIndex].dblPonitValue * axis[0].Eucf;
            Y2=pointB.arrPoint[axis[1].AxisIndex].dblPonitValue * axis[1].Eucf;
            X3=pointC.arrPoint[axis[0].AxisIndex].dblPonitValue * axis[0].Eucf;
            Y3=pointC.arrPoint[axis[1].AxisIndex].dblPonitValue * axis[1].Eucf;
            Circle(X1,Y1,X2,Y2,X3,Y3, out radius);

            short shrResult = SR_InpArc(axis[0].CardNum, axis[0].AxisNum, axis[1].AxisNum, (int)X3, (int)Y3, pointC.arrPoint[axis[0].AxisIndex].dblPonitSpeed * axis[0].Eucf/1000, 0.2, radius, 0);
           
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            else
            {
                return shrSuccess;
            }
            
        }
        //计算圆心和半径
        public static void Circle(double x1, double y1, double x2, double y2, double x3, double y3, out double radius)
        {
            //斜率
            double k;
            //偏移
            //double b1;
            //k = (x2 - x1) / (y1 - y2);
            //double midx = (x1 + x2) / 2;
            //double midy = (y1 + y2) / 2;

            //b1 = midy - k * midx;

            //midy = k * midx + b1;
            //double y3 =60000;
            //double x3 = 150000;
            double a = 2 * (x2 - x1);
            double b = 2 * (y2 - y1);
            double c = x2 * x2 + y2 * y2 - x1 * x1 - y1 * y1;
            double d = 2 * (x3 - x2);
            double e = 2 * (y3 - y2);
            double f = x3 * x3 + y3 * y3 - x2 * x2 - y2 * y2;
            double centerXP = (b * f - e * c) / (b * d - e * a);
            double centerYP = (d * c - a * f) / (b * d - e * a);
            //centerX = (b * f - e * c) / (b * d - e * a);
            //centerY = (d * c - a * f) / (b * d - e * a);
            radius = Math.Sqrt((centerXP - x1) * (centerXP - x1) + (centerYP - y1) * (centerYP - y1));
            //MessageBoxLog.Show(centerX.ToString() + "," + centerY.ToString() + "," + radius.ToString());

        }
        /// <summary>
        /// 判断命令执行结果 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="result"></param>
        /// <returns></returns> 
        public static short CommandResult(string command, short result)
        {
            string strErrorMsg = "";
            if (result == shrSuccess)
            {
                return shrSuccess;
            }
            else
            {
                switch (result)
                {
                    case 1:
                        strErrorMsg = "检查当前指令的执行条件是否满足";
                        break;
                    case 2:
                        strErrorMsg = "无此功能，请与生产商联系";
                        break;
                    case 7:
                        strErrorMsg = "指令参数错误（传入参数取值范围）";
                        break;
                    case -1:
                        strErrorMsg = "主机和运动控制器通讯失败";
                        break;
                    case -6:
                        strErrorMsg = "打开控制器失败";
                        break;
                    case -7:
                        strErrorMsg = "运动控制器没有响应";
                        break;
                    default:
                        strErrorMsg = "其他原因";
                        break;
                }
                CFile.WriteErrorForDate("函数：" + command + " 执行异常，" + strErrorMsg);
                return shrFail;
            }
        }
        /// <summary>
        /// 搜索原点信号
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="capture"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short CaptureHomeSignal(short card, short axis, out short capture, out int pValue)
        {
            int intAxisStatus;
            int intCount = 0;
            uint uintClock;
            pValue = -1;
            capture = 0;
            short shrResult;
            //捕获原点
            shrResult = mc.GT_SetCaptureMode(card, axis, mc.CAPTURE_HOME);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetCaptureMode", shrResult);
                return shrFail;
            }
            Thread.Sleep(5);
            do
            {
                //读取捕获原点状态   pValue为1 捕获到原点信号
                shrResult = mc.GT_GetCaptureStatus(card, axis, out capture, out pValue, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetCaptureStatus", shrResult);
                    return shrFail;
                }
                if (capture == 1)  //捕获到原点
                {
                    SR_AxisEmgStop(card, axis);
                    break;
                }
                //读轴状态
                shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_ClrSts", shrResult);
                    return shrFail;
                }
                if ((intAxisStatus & 0x20) > 0 || (intAxisStatus & 0x40) > 0)//正或负极限触发
                {
                    //防止在限位，限位状态来不急清除 给了两次机会
                    if (intCount < 5)
                    {
                        shrResult = mc.GT_ClrSts(card, axis, 1);
                        if (shrResult != shrGtsSuccess)
                        {
                            CommandResult("GT_ClrSts", shrResult);
                            return shrFail;
                        }
                        intCount++;
                    }
                    if (intCount > 5)  //连续读到限位异常
                    {
                        if ((intAxisStatus & 0x20) > 0)
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴，搜索原点信号时正限位信号异常");
                        }
                        if ((intAxisStatus & 0x40) > 0)
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴，搜索原点信号时负限位信号异常");
                        }
                        SR_AxisEmgStop(card, axis);
                        return shrFail;
                    }
                }
                Thread.Sleep(5);

            } while ((intAxisStatus & 0x400) > 0);

            return shrSuccess;

        }
        /// <summary>
        /// 搜索原点信号
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="capture"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short CaptureHomeSignalFirst(short card, short axis, out short capture, out int pValue)
        {
            int intAxisStatus;
            int intCount = 0;
            uint uintClock;
            pValue = -1;
            capture = 0;
            short shrResult;
            //捕获原点
            shrResult = mc.GT_SetCaptureMode(card, axis, mc.CAPTURE_HOME);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetCaptureMode", shrResult);
                return shrFail;
            }
            Thread.Sleep(5);
            do
            {
                //读取捕获原点状态   pValue为1 捕获到原点信号
                shrResult = mc.GT_GetCaptureStatus(card, axis, out capture, out pValue, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetCaptureStatus", shrResult);
                    return shrFail;
                }
                if (capture == 1)  //捕获到原点
                {
                    SR_AxisEmgStop(card, axis);
                    break;
                }
                //读轴状态
                shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_ClrSts", shrResult);
                    return shrFail;
                }
                //if ((intAxisStatus & 0x20) > 0 || (intAxisStatus & 0x40) > 0)//正或负极限触发
                //{
                //    //防止在限位，限位状态来不急清除 给了两次机会
                //    if (intCount < 2)
                //    {
                //        shrResult = mc.GT_ClrSts(card, axis, 1);
                //        if (shrResult != shrGtsSuccess)
                //        {
                //            CommandResult("GT_ClrSts", shrResult);
                //            return shrFail;
                //        }
                //        intCount++;
                //    }
                //    if (intCount > 1)  //连续读到限位异常
                //    {
                //        if ((intAxisStatus & 0x20) > 0)
                //        {
                //            CFile.WriteErrorForDate(card + "卡" + axis + "轴，搜索原点信号时正限位信号异常");
                //        }
                //        if ((intAxisStatus & 0x40) > 0)
                //        {
                //            CFile.WriteErrorForDate(card + "卡" + axis + "轴，搜索原点信号时负限位信号异常");
                //        }
                //        SR_AxisEmgStop(card, axis);
                //        return shrFail;
                //    }
                //}
                Thread.Sleep(5);

            } while ((intAxisStatus & 0x400) > 0);

            return shrSuccess;

        }
        /// <summary>
        /// 搜索原点信号
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="capture"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short CaptureHomeSignalForStepMotor(short card, short axis, out short capture, out int pValue)
        {
            int intAxisStatus;
            int intCount = 0;
            uint uintClock;
            pValue = -1;
            capture = 0;
            double dblPos = 0;
            short shrResult;
            //捕获原点
            shrResult = mc.GT_SetCaptureMode(card, axis, mc.CAPTURE_HOME);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetCaptureMode", shrResult);
                return shrFail;
            }
            Thread.Sleep(5);
            do
            {
                //读取捕获原点状态   pValue为1 捕获到原点信号
                shrResult = mc.GT_GetCaptureStatus(card, axis, out capture, out pValue, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetCaptureStatus", shrResult);
                    return shrFail;
                }
                if (capture == 1)  //捕获到原点
                {
                    mc.GT_GetAxisPrfPos(card, axis, out dblPos, 1, out uintClock);
                    pValue = (int)dblPos;
                    SR_AxisEmgStop(card, axis);
                    break;
                }
                //读轴状态
                shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_ClrSts", shrResult);
                    return shrFail;
                }
                if ((intAxisStatus & 0x20) > 0 || (intAxisStatus & 0x40) > 0)//正或负极限触发
                {
                    //防止在限位，限位状态来不急清除 给了两次机会
                    if (intCount < 2)
                    {
                        shrResult = mc.GT_ClrSts(card, axis, 1);
                        if (shrResult != shrGtsSuccess)
                        {
                            CommandResult("GT_ClrSts", shrResult);
                            return shrFail;
                        }
                        intCount++;
                    }
                    if (intCount > 1)  //连续读到限位异常
                    {
                        if ((intAxisStatus & 0x20) > 0)
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴，搜索原点信号时正限位信号异常");
                        }
                        if ((intAxisStatus & 0x40) > 0)
                        {
                            CFile.WriteErrorForDate(card + "卡" + axis + "轴，搜索原点信号时负限位信号异常");
                        }
                        SR_AxisEmgStop(card, axis);
                        return shrFail;
                    }
                }
                Thread.Sleep(5);

            } while ((intAxisStatus & 0x400) > 0);

            return shrSuccess;

        }
        /// <summary>
        /// 搜索原点信号
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="capture"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short CaptureHomeSignalForStepMotorFirst(short card, short axis, out short capture, out int pValue)
        {
            int intAxisStatus;
            uint uintClock;
            pValue = -1;
            capture = 0;
            double dblPos = 0;
            short shrResult;
            //捕获原点
            shrResult = mc.GT_SetCaptureMode(card, axis, mc.CAPTURE_HOME);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetCaptureMode", shrResult);
                return shrFail;
            }
            Thread.Sleep(5);
            do
            {
                //读取捕获原点状态   pValue为1 捕获到原点信号
                shrResult = mc.GT_GetCaptureStatus(card, axis, out capture, out pValue, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetCaptureStatus", shrResult);
                    return shrFail;
                }
                if (capture == 1)  //捕获到原点
                {
                    mc.GT_GetAxisPrfPos(card, axis, out dblPos, 1, out uintClock);
                    pValue = (int)dblPos;
                    SR_AxisEmgStop(card, axis);
                    break;
                }
                //读轴状态
                shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_ClrSts", shrResult);
                    return shrFail;
                }
                //if ((intAxisStatus & 0x20) > 0 || (intAxisStatus & 0x40) > 0)//正或负极限触发
                //{
                //    //防止在限位，限位状态来不急清除 给了两次机会
                //    if (intCount < 2)
                //    {
                //        shrResult = mc.GT_ClrSts(card, axis, 1);
                //        if (shrResult != shrGtsSuccess)
                //        {
                //            CommandResult("GT_ClrSts", shrResult);
                //            return shrFail;
                //        }
                //        intCount++;
                //    }
                //    if (intCount > 1)  //连续读到限位异常
                //    {
                //        if ((intAxisStatus & 0x20) > 0)
                //        {
                //            CFile.WriteErrorForDate(card + "卡" + axis + "轴，搜索原点信号时正限位信号异常");
                //        }
                //        if ((intAxisStatus & 0x40) > 0)
                //        {
                //            CFile.WriteErrorForDate(card + "卡" + axis + "轴，搜索原点信号时负限位信号异常");
                //        }
                //        SR_AxisEmgStop(card, axis);
                //        return shrFail;
                //    }
                //}
                Thread.Sleep(5);

            } while ((intAxisStatus & 0x400) > 0);

            return shrSuccess;

        }
        /// <summary>
        /// 改变点位运动中轴的目标位置和速度
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="postion"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static short ChangePostion(short card, short axis, int postion, double speed)
        {
            short shrResult;
            //设置目标位置
            shrResult = mc.GT_SetPos(card, axis, postion);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetPos", shrResult);
                return shrFail;
            }
            //设置轴运动速度
            shrResult = mc.GT_SetVel(card, axis, speed);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_SetVel", shrResult);
                return shrFail;
            }
            //清除轴状态和报警
            shrResult = mc.GT_ClrSts(card, axis, 1);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_ClrSts", shrResult);
                return shrFail;
            }
            //启动轴运动
            shrResult = mc.GT_Update(card, 1 << (axis - 1));
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_Update", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴停止
        /// </summary>
        /// <param name="stationName">工位名称</param>
        /// <param name="axisName">轴名称</param>
        /// <returns></returns>
        public static short IsAxisStop(string stationName, string axisName)
        {
            AxisConfig axisC = StationManage.FindAxis(stationName, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            short shrResult = SR_IsAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }

        /// <summary>
        /// 等待轴停止
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static short AxisdWaitStop(string stationName, string axisName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }

            short shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 等待轴停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short WaitAxisStop(short card, short axis)
        {
            int intAxisStatus;
            uint uintClock;
            short shrResult;
            //读轴状态
            shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
            do
            {
                //程序是否正常运行
                if (!blnRun)
                {
                    SR_AxisEmgStop(card, axis);
                    return shrFail;
                }
                //读轴状态
                shrResult = mc.GT_GetSts(card, axis, out intAxisStatus, 1, out uintClock);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_ClrSts", shrResult);
                    return shrFail;
                }
                Thread.Sleep(5);
            } while ((intAxisStatus & 0x400) > 0);  //运动中         
            return shrSuccess;
        }
        /// <summary>
        /// 轴停止
        /// </summary>
        /// <param name="stationName">工位名字</param>
        /// <param name="axisName">轴名字</param>
        /// <returns></returns>
        public static short WaitAxisStop(string stationName, string axisName)
        {
            AxisConfig axisC = StationManage.FindAxis(stationName, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            short shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }

        public static bool stepMovePointNoWait(string StationName, string pointName)
        {
            StationModule tag_at = StationManage.FindStation(StationName);
            if (tag_at == null)
            {
                if (MessageBoxLog.Show(StationName + "没有找到，请添加工位") == DialogResult.OK)
                {
                   
                }
                return false;
            }
            PointAggregate pointA1 = StationManage.FindPoint(tag_at, pointName);
            if (tag_at == null || pointA1 == null)
            {
                if (MessageBoxLog.Show(StationName + "->" + pointName + "没有找到，请添加点位") == DialogResult.OK)
                {
                   
                }
                return false;
            }
            foreach (AxisConfig af in tag_at.arrAxis)
            {
                AxisConfig axisC = StationManage.FindAxis(StationName, af.AxisName);
                if (axisC == null)
                {
                    if (MessageBoxLog.Show("模块:" + StationName + "->" + af.AxisName + "没有找到，请添加轴") == DialogResult.OK)
                    {
                        
                    }
                    return false;
                }
                if (pointA1.arrPoint[axisC.AxisIndex].dblPonitValue >= 100000 || !pointA1.arrPoint[axisC.AxisIndex].blnPointEnable)
                {
                    //不运行
                }
                else
                {
                    if (NewCtrlCardSR.AxisAbsoluteMove(StationName, af.AxisName, pointName) != 0)
                    {
                        if (MessageBoxLog.Show("模块:" + StationName + "->" + af.AxisName + "移动异常，请检查伺服驱动！") == DialogResult.OK)
                        {
                           
                        }
                        return false;
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// 步进电机相对运动并等待轴停止，检测运动结果 用于相机定校准
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short VerificationAxisPoint(short card, short axis, int postion)
        {
            short shrResult;
            double dblSendValue = 0, dblEncValue = 0;
            uint uintClock;
            //读取规划位置
            shrResult = mc.GT_GetAxisPrfPos(card, axis, out dblSendValue, 1, out uintClock);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetAxisPrfPos", shrResult);
                return shrFail;
            }
            //读取编码器返馈位置
            shrResult = mc.GT_GetAxisEncPos(card, axis, out dblEncValue, 1, out uintClock);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetAxisEncPos", shrResult);
                return shrFail;
            }
            //判断发送脉冲是否出现异常   &&  发送脉冲与实际位置是否偏差超过设定范围
            if ((Math.Abs(dblSendValue - postion) <= 10) && (Math.Abs(dblSendValue - dblEncValue) <= AxisMotionError))
            {
                return shrSuccess;//在误差范围内返回成功
            }
            else
            {
                return shrFail;//否则返回失败
            }
        }
        /// <summary>
        /// 步进轴定位校验
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="postion"></param>
        /// <returns></returns>
        public static short VerificationStepPoint(short card, short axis, int postion)
        {
            short shrResult;
            double dblSendValue;
            uint uintClock;
            //读取规划位置
            shrResult = mc.GT_GetAxisPrfPos(card, axis, out dblSendValue, 1, out uintClock);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetAxisPrfPos", shrResult);
                return shrFail;
            }
            //判断发送脉冲是否出现异常
            if ((Math.Abs(dblSendValue - postion) <= 1))
            {
                return shrSuccess;
            }
            else
            {
                return shrFail;
            }
        }
        /// <summary>
        /// 伺服电机回原函数
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static short GoHome(string stationName, string axisName)
        {
            AxisConfig axisC = StationManage.FindAxis(stationName, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            short shrResult = 0;
            if (axisC.GoHomeType == 0)
            {
                shrResult = SR_GoHome(axisC,axisC.CardNum, axisC.AxisNum, axisC.HomeSpeedHight * axisC.Eucf / 1000, axisC.HomeSpeed * axisC.Eucf / 1000, (int)axisC.HomeDir);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }

            else
                if (axisC.GoHomeType == 1)
                {
                    shrResult = GoHomeWithStepMotor(stationName, axisName);
                    if (shrResult != shrSuccess)
                    {
                        return shrFail;
                    }
                }
                else
                    if (axisC.GoHomeType == 2)
                    {

                    }

            return shrSuccess;
        }
        /// <summary>
        /// 步进电机回原函数
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static short GoHomeWithStepMotor(string stationName, string axisName)
        {
            AxisConfig axisC = StationManage.FindAxis(stationName, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            short shrResult = SR_GoHomeWithStepMotor( axisC ,axisC.CardNum, axisC.AxisNum, axisC.HomeSpeedHight * axisC.Eucf / 1000, axisC.HomeSpeed * axisC.Eucf / 1000, (int)axisC.HomeDir);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }

        /// <summary>
        ///  线程回原
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>        
        public static short GoHomeForThread(string stationName, string axisName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            Thread homeThread = new Thread(new ParameterizedThreadStart(delegate { SR_GoHome(axisC,axisC.CardNum, axisC.AxisNum, axisC.HomeSpeedHight * axisC.Eucf / 1000, axisC.HomeSpeed * axisC.Eucf / 1000, (int)axisC.HomeDir); }));
            homeThread.Start();
            homeThread.IsBackground = true;
            return shrSuccess;
        }
        /// <summary>
        /// 线程回原
        /// </summary>
        /// <param name="axisC"></param>
        /// <returns></returns>
        public static short GoHomeForThread(AxisConfig axisC)
        {
            if (axisC == null)
            {
                return shrFail;
            }
            Thread homeThread = new Thread(new ParameterizedThreadStart(delegate { SR_GoHome(axisC,axisC.CardNum, axisC.AxisNum, axisC.HomeSpeedHight * axisC.Eucf / 1000, axisC.HomeSpeed * axisC.Eucf / 1000, (int)axisC.HomeDir); }));
            homeThread.Start();
            homeThread.IsBackground = true;
            return shrSuccess;
        }
        /// <summary>
        /// 轴绝对运动
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisAbsoluteMove(string stationName, string axisName, string pointName, double pos)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            if (pointA == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }
            short shrResult = SR_AbsoluteMove(axisC.CardNum, axisC.AxisNum, (int)(pos * axisC.Eucf), pointA.arrPoint[axisC.AxisIndex].dblPonitSpeed * axisC.Eucf / 1000);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴绝对运动
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisAbsoluteMove(string stationName, string axisName, string pointName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            if (pointA == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }
            short shrResult = SR_AbsoluteMove(axisC.CardNum, axisC.AxisNum, (int)(pointA.arrPoint[axisC.AxisIndex].dblPonitValue * axisC.Eucf), pointA.arrPoint[axisC.AxisIndex].dblPonitSpeed * axisC.Eucf / 1000);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴绝对运动
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisAbsoluteMove(string stationName, string axisName, string pointName,int asxisIndex)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            if (pointA == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }
            short shrResult = SR_AbsoluteMove(axisC.CardNum, axisC.AxisNum, (int)(pointA.arrPoint[asxisIndex].dblPonitValue * axisC.Eucf), pointA.arrPoint[asxisIndex].dblPonitSpeed * axisC.Eucf / 1000);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴绝对运动并等待轴停止
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisAbsoluteMoveAndWaitStop(string stationName, string axisName, string pointName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            short shrResult = AxisAbsoluteMove(stationName, axisName, pointName);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 伺服电机绝对运动并等待轴停止,加检测收发脉冲数
        /// </summary>
        /// <param name="stationName">工站名字</param>
        /// <param name="axisName">轴名字</param>
        /// <param name="pointName">点位名字</param>
        /// <returns>返数返回0，执行成功 非0执行失败</returns>
        public static short AxisAbsoluteMoveAndVrfStop(string stationName, string axisName, string pointName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            short shrResult = AxisAbsoluteMove(stationName, axisName, pointName);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = VerificationAxisPoint(axisC.CardNum, axisC.AxisNum, (int)(pointA.arrPoint[axisC.AxisIndex].dblPonitValue * axisC.Eucf));
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }

        /// <summary>
        /// 步进电机绝对运动并等待轴停止,加检测发脉冲数
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short StepMotorAbsoluteMoveAndVrfStop(string stationName, string axisName, string pointName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            short shrResult = AxisAbsoluteMove(stationName, axisName, pointName);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = VerificationStepPoint(axisC.CardNum, axisC.AxisNum, (int)(pointA.arrPoint[axisC.AxisIndex].dblPonitValue * axisC.Eucf));
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴相对运动
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisRelativeMove(string stationName, string axisName, string pointName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            if (pointA == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }
            short shrResutl = SR_RelativeMove(axisC.CardNum, axisC.AxisNum, (int)(pointA.arrPoint[axisC.AxisIndex].dblPonitValue * axisC.Eucf), pointA.arrPoint[axisC.AxisIndex].dblPonitSpeed * axisC.Eucf / 1000);
            if (shrResutl != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴相对运动并等待轴停止
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisRelativeMoveAndWaitStop(string stationName, string axisName, string pointName)
        {
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            if (pointA == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }
            short shrResult = SR_RelativeMove(axisC.CardNum, axisC.AxisNum, (int)(pointA.arrPoint[axisC.AxisIndex].dblPonitValue * axisC.Eucf), pointA.arrPoint[axisC.AxisIndex].dblPonitSpeed * axisC.Eucf / 1000);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 伺服电机相对运动并等待轴停止，检测运动结果 用于相机定校准
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisRelativeMoveAndVrfStop(string stationName, string axisName, double offValue, double speed)
        {
            double pos = 0;
            uint pClock = 0;
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            //PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            //if (pointA == null)
            //{
            //    CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
            //    return shrFail;
            //}
            short shrResult = mc.GT_GetAxisPrfPos(axisC.CardNum, axisC.AxisNum, out pos, 1, out pClock);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetAxisPrfPos", shrResult);
                return shrFail;
            }
            shrResult = SR_RelativeMove(axisC.CardNum, axisC.AxisNum, (int)(offValue * axisC.Eucf), speed * axisC.Eucf / 1000);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = VerificationAxisPoint(axisC.CardNum, axisC.AxisNum, (int)((pos + offValue) * axisC.Eucf));
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }/// <summary>
        /// 移动到StationName工位上pointName点位 ： StationName工位名称，pointName点位名称
        /// </summary>
        /// <param name="StationName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
   
        /// <summary>
        /// 步进电机相对运动并等待轴停止，检测运动结果 用于相机定校准
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short StepMotorRelativeMoveAndVrfStop(string stationName, string axisName, double offValue, double speed)
        {
            double pos = 0;
            uint pClock = 0;
            StationModule stationM = StationManage.FindStation(stationName);
            if (stationM == null)
            {
                CFile.WriteErrorForDate("函数：FindStation 执行异常，返回空");
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            //PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            //if (pointA == null)
            //{
            //    CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
            //    return shrFail;
            //}
            short shrResult = mc.GT_GetAxisPrfPos(axisC.CardNum, axisC.AxisNum, out pos, 1, out pClock);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetAxisPrfPos", shrResult);
                return shrFail;
            }
            shrResult = SR_RelativeMove(axisC.CardNum, axisC.AxisNum, (int)(offValue * axisC.Eucf), speed * axisC.Eucf / 1000);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = VerificationStepPoint(axisC.CardNum, axisC.AxisNum, (int)((pos + offValue) * axisC.Eucf));
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴Jog运动到信号停 轴停止
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisJoMove(string stationName, string axisName)
        {
            if (string.IsNullOrEmpty(stationName) || string.IsNullOrEmpty(axisName))
            {
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationName, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
          
            short shrResult;
            if (SR_IsAxisStop(axisC.CardNum, axisC.AxisNum) == shrSuccess)
            {
                shrResult = SR_ContinueMove(axisC.CardNum, axisC.AxisNum, (int)(axisC.Speed * axisC.Eucf / 1000), axisC.JogDir);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 轴Jog运动到信号停 轴停止
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisJoMoveForSignalStop(string stationName, string axisName, string inputName, bool check, short dir = 0, double outTime = 10)
        {
            if (string.IsNullOrEmpty(stationName) || string.IsNullOrEmpty(axisName) || string.IsNullOrEmpty(inputName))
            {
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationName, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            IOParameter input1 = StationManage.FindInputIo(stationName, inputName);
            if (input1 == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }

            short shrResult;
            if (SR_IsAxisStop(axisC.CardNum, axisC.AxisNum) == shrSuccess)
            {
                shrResult = SR_ContinueMove(axisC.CardNum, axisC.AxisNum, (int)(axisC.Speed * axisC.Eucf / 1000), dir);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }
            if (!check)
            {
                Thread.Sleep(2000);
                shrResult = SR_AxisStop(axisC.CardNum, axisC.AxisNum);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                return shrSuccess;
            }
            DateTime dtmStart = DateTime.Now;
            DateTime dtmNow;
            bool blnStatus1 = false;

            while (blnRun)
            {
                dtmNow = DateTime.Now;
                if ((dtmNow - dtmStart).TotalSeconds > outTime)
                {
                    return shrFail;
                }
                shrResult = GetInputIoBitStatus(input1, out blnStatus1);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                if (blnStatus1)
                {
                    shrResult = SR_AxisEmgStop(axisC.CardNum, axisC.AxisNum);
                    if (shrResult == shrSuccess)
                    {
                        return shrSuccess;
                    }
                    else
                    {
                        return shrFail;
                    }
                }
            }
            return shrFail;
        }
        /// <summary>
        /// 轴Jog运动到信号停 轴停止
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisJoMoveOutSignalStop(string stationName, string axisName, string inputName, bool check, short dir = 0, double outTime = 10)
        {
            if (string.IsNullOrEmpty(stationName) || string.IsNullOrEmpty(axisName) || string.IsNullOrEmpty(inputName))
            {
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationName, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            IOParameter input1 = StationManage.FindInputIo(stationName, inputName);
            if (input1 == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }

            short shrResult;
            if (SR_IsAxisStop(axisC.CardNum, axisC.AxisNum) == shrSuccess)
            {
                shrResult = SR_ContinueMove(axisC.CardNum, axisC.AxisNum, (int)(axisC.Speed * axisC.Eucf / 1000), dir);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }
            if (!check)
            {
                Thread.Sleep(2000);
                shrResult = SR_AxisStop(axisC.CardNum, axisC.AxisNum);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                return shrSuccess;
            }
            DateTime dtmStart = DateTime.Now;
            DateTime dtmNow;
            bool blnStatus1 = false;

            while (blnRun)
            {
                dtmNow = DateTime.Now;
                if ((dtmNow - dtmStart).TotalSeconds > outTime)
                {
                    return shrFail;
                }
                shrResult = GetInputIoBitStatus(input1, out blnStatus1);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                if (!blnStatus1)
                {
                    shrResult = SR_AxisEmgStop(axisC.CardNum, axisC.AxisNum);
                    if (shrResult == shrSuccess)
                    {
                        return shrSuccess;
                    }
                    else
                    {
                        return shrFail;
                    }
                }
                Thread.Sleep(5);
            }
            return shrFail;
        }
        /// <summary>
        /// 轴Jog运动到信号停 轴停止
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisJoMoveForSignalStop(string stationName, string axisName, string inputName1, string inputName2, bool check, short dir = 0, double outTime = 10)
        {
            if (string.IsNullOrEmpty(stationName) || string.IsNullOrEmpty(axisName) || string.IsNullOrEmpty(inputName1) || string.IsNullOrEmpty(inputName2))
            {
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationName, axisName);
            if (axisC == null)
            {
                CFile.WriteErrorForDate("函数：FindAxis 执行异常，返回空");
                return shrFail;
            }
            IOParameter input1 = StationManage.FindInputIo(stationName, inputName1);
            if (input1 == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }
            IOParameter input2 = StationManage.FindInputIo(stationName, inputName2);
            if (input1 == null)
            {
                CFile.WriteErrorForDate("函数：FindPoint 执行异常，返回空");
                return shrFail;
            }
            short shrResult;
            if (SR_IsAxisStop(axisC.CardNum, axisC.AxisNum) == shrSuccess)
            {
                shrResult = SR_ContinueMove(axisC.CardNum, axisC.AxisNum, (int)(axisC.Speed * axisC.Eucf / 1000), dir);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }
            if (!check)
            {
                Thread.Sleep(2000);
                shrResult = SR_AxisStop(axisC.CardNum, axisC.AxisNum);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                return shrSuccess;
            }
            DateTime dtmStart = DateTime.Now;
            DateTime dtmNow;
            bool blnStatus1 = false, blnStatus2 = false;

            while (blnRun)
            {
                dtmNow = DateTime.Now;
                if ((dtmNow - dtmStart).TotalSeconds > outTime)
                {
                    return shrFail;
                }
                shrResult = GetInputIoBitStatus(input1, out blnStatus1);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                shrResult = GetInputIoBitStatus(input2, out blnStatus2);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                if (blnStatus1 && blnStatus2)
                {
                    shrResult = SR_AxisEmgStop(axisC.CardNum, axisC.AxisNum);
                    if (shrResult == shrSuccess)
                    {
                        return shrSuccess;
                    }
                    else
                    {
                        return shrFail;
                    }
                }
            }
            return shrFail;
        }
        /// <summary>
        /// 获取IO单位状态
        /// </summary>
        /// <param name="io">io对象</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public static short GetInputIoBitStatus(IOParameter io, out bool status)
        {

            short shrResult = 0;
            status = false;
            try
            {
            shrResult = SR_GetInputBit(io.CardNum, io.IOBit, out status);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            if ((status && io.Logic == 1) || (!status && io.Logic == 0))
            {
                status = true;
            }

            
         
            else
            {
                status = false;
            }
        

          
            }
             catch (Exception)
            {

            }
            return shrSuccess;
        }
   
        /// <summary>
        /// 获取单通道（Bit）输入IO状态，函数执行成功 判断status参数值 TRUE　有信号输入　　FALSE无信号输入 
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="ioName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        //public static short GetInputIoBitStatus(string stationName, string ioName, out bool status)
        //{
        //    short shrResult;
        //    status = false;
        //    IOParameter ioP = StationManage.FindInputIo(stationName, ioName);
        //    if (ioP == null)
        //    {
        //        CFile.WriteErrorForDate("函数：FindInputIo 执行异常，返回空 工站名：" + stationName + " IO名：" + ioName);
        //        return shrFail;
        //    }

        //    shrResult = GetInputIoBitStatus(ioP, out status);
        //    if (shrResult != shrSuccess)
        //    {
        //        return shrFail;
        //    }

        //    return shrSuccess;
        //}
        /// <summary>
        /// 气缸输入IO检测
        /// </summary>
        /// <param name="input1">检测信号1</param>
        /// <param name="input2">检测信号2</param>
        /// <param name="outTime">气缸超时时间</param>
        /// <returns></returns>
        public static short CyInputIoCheck(IOParameter input1 = null, IOParameter input2 = null, double outTime = 6)
        {
            DateTime dtmStart = DateTime.Now;
            DateTime dtmNow = DateTime.Now;
            short shrReuslt = 0;
            bool ioStatus = false;
            if (input2 != null)
            {
                while (blnRun)
                {
                    dtmNow = DateTime.Now;
                    if ((dtmNow - dtmStart).TotalSeconds > outTime)
                    {
                        CFile.WriteErrorForDate("函数：CyInputIoCheck 执行异常，IO信号断开超时");
                        return shrFail;
                    }
                    shrReuslt = GetInputIoBitStatus(input2, out ioStatus);
                    if (shrReuslt != shrSuccess)
                    {
                        return shrFail;
                    }else  if (!ioStatus)
                    {
                        break;
                    }
                    Thread.Sleep(5);
                }
            }
            if (input1 != null)
            {
                while (blnRun)
                {
                    dtmNow = DateTime.Now;
                    if ((dtmNow - dtmStart).TotalSeconds > outTime)
                    {
                        CFile.WriteErrorForDate("函数：CyInputIoCheck 执行异常，IO信号到位超时");
                        return shrFail;
                    }
                    shrReuslt = GetInputIoBitStatus(input1, out ioStatus);
                    if (shrReuslt != shrSuccess)
                    {
                        return shrFail;
                    }
                    else if (ioStatus)
                    {
                        break;
                    }
                    Thread.Sleep(5);
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 气缸单电控输出
        /// </summary>
        /// <param name="stationName">工站名字</param>
        /// <param name="outIoName">气缸输出点名字</param>
        /// <param name="outValue">输出状态</param>
        /// <param name="check">是否启动检查到位</param>
        /// <param name="inputName1">检测信号1</param>
        /// <param name="inputName2">检测信号2</param>
        /// <returns>函数返回 0 气缸输出OK，信号到位，   非0 气缸输出失败，或信号到位异常</returns>
        public static short CyControl(string stationName, string outIoName, short outValue, bool check, string inputName1 = null, string inputName2 = null)
        {
            short shrResult = 0;
            IOParameter outI0 = StationManage.FindOutputIo(stationName, outIoName);
            if (outI0 == null)
            {
                return shrFail;
            }

           
            shrResult = SetOutputIoBit(outI0, outValue);
            if (shrResult == shrFail)
            {
                return shrFail;
            }
            if (check)
            {
                IOParameter input1 = null, input2 = null;
                if (!string.IsNullOrEmpty(inputName1))
                {
                    input1 = StationManage.FindInputIo(stationName, inputName1);
                    if (input1 == null)
                    {
                        return shrFail;
                    }
                }
                if (!string.IsNullOrEmpty(inputName2))
                {
                    input2 = StationManage.FindInputIo(stationName, inputName2);
                    if (input2 == null)
                    {
                        return shrFail;
                    }
                }

                shrResult = CyInputIoCheck(input1, input2);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// 气缸双电控输出
        /// </summary>
        /// <param name="stationName">工站名字</param>
        /// <param name="outIoName">气缸输出点1名字</param>
        /// <param name="outValue">输出状态</param>
        /// <param name="outIoName1">气缸输出点2名字</param>
        /// <param name="outValue1">输出状态</param>
        /// <param name="check">是否启动检查到位</param>
        /// <param name="inputName1">检测信号1</param>
        /// <param name="inputName2">检测信号2</param>
        /// <returns>函数返回 0 气缸输出OK，信号到位，   非0 气缸输出失败，或信号到位异常</returns>
        public static short CyDoubleControl(string stationName, string outIoName, short outValue, string outIoName1, short outValue1, bool check, string inputName1 = null, string inputName2 = null)
        {

            if (string.IsNullOrEmpty(stationName) || string.IsNullOrEmpty(outIoName) || string.IsNullOrEmpty(outIoName1))
            {
                return shrFail;
            }
            IOParameter outI0 = StationManage.FindOutputIo(stationName, outIoName);
            if (outI0 == null)
            {
                return shrFail;
            }
            IOParameter outI01 = StationManage.FindOutputIo(stationName, outIoName1);
            if (outI01 == null)
            {
                return shrFail;
            }
            short shrResult = 0;
            shrResult = SetOutputIoBit(outI0, outValue);
            if (shrResult == shrFail)
            {
                return shrFail;
            }
            shrResult = SetOutputIoBit(outI01, outValue1);
            if (shrResult == shrFail)
            {
                return shrFail;
            }
            if (check)
            {
                IOParameter input1 = null, input2 = null;
                if (!string.IsNullOrEmpty(inputName1))
                {
                    input1 = StationManage.FindInputIo(stationName, inputName1);
                    if (input1 == null)
                    {
                        return shrFail;
                    }
                }
                if (!string.IsNullOrEmpty(inputName2))
                {
                    input2 = StationManage.FindInputIo(stationName, inputName2);
                    if (input2 == null)
                    {
                        return shrFail;
                    }
                }
                shrResult = CyInputIoCheck(input1, input2);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }
            return shrSuccess;
        }
        /// <summary>
        /// IO输出
        /// </summary>
        /// <param name="io">IO类对象</param>
        /// <param name="ioValue">输出状态</param>
        /// <returns></returns>
        public static short SetOutputIoBit(IOParameter io, short ioValue)
        {
            short shrResult = 0;
            shrResult = SR_SetOutputBit(io.CardNum, io.IOBit, ioValue);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }

            return shrSuccess;
        }
        /// <summary>
        /// 单个IO输出
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="ioName"></param>
        /// <returns></returns>
        public static short SetOutputIoBit(string stationName, string ioName, short ioValue)
        {
            IOParameter io = StationManage.FindOutputIo(stationName, ioName);
            if (io == null)
            {
                return shrFail;
            }
            short shrResult = SetOutputIoBit(io, ioValue);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// IO控制 步进电机
        /// </summary>
        /// <param name="stationName">工站名字</param>
        /// <param name="ioDir">方向IO </param>
        /// <param name="ioPluse">脉冲iO名字</param>
        /// <param name="dirValue">输出方向</param>
        /// <param name="pluseValue">是否启动</param>
        /// <returns></returns>
        public static short SetOutputForStepMotor(string stationName, string ioDir, string ioPluse, short dirValue, short pluseValue)
        {
            IOParameter io = StationManage.FindOutputIo(stationName, ioDir);
            if (io == null)
            {
                return shrFail;
            }
            IOParameter io1 = StationManage.FindOutputIo(stationName, ioPluse);
            if (io == null)
            {
                return shrFail;
            }
            short shrResult = SetOutputIoBit(io, dirValue);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = SetOutputIoBit(io1, pluseValue);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        public static event EventHandler ControlCtrlChange;
        protected void OnControlCtrlChange()
        {
            if (ControlCtrlChange != null)
            {
                ControlCtrlChange(this, null);
            }
        }
        public static event EventHandler InputIOChange;
        protected static void OnInputIOChange(int cardNum, int ioStatus)
        {
            if (InputIOChange != null)
                InputIOChange(cardNum, new CardInputIOEvengArgs(cardNum, (ulong)ioStatus));
        }
        public static event EventHandler OutputIOChange;
        protected static void OnOutputIOChange(int cardNum, int ioStatus)
        {
            if (OutputIOChange != null)
                OutputIOChange(cardNum, new CardIOEvengArgs(cardNum, ioStatus));
        }
        public static event EventHandler AxisEnableChange;
        protected static void OnAxisEnableChange(int cardNum, int EnableStatus)
        {
            if (AxisEnableChange != null)
                AxisEnableChange(cardNum, new CardAxisSignalEvengArgs(cardNum, (ushort)EnableStatus));
        }
        public static event EventHandler AxisAlarmChange;
        protected static void OnAxisAlarmChange(int cardNum, ushort alarmValue)
        {
            if (AxisAlarmChange != null)
            {
                AxisAlarmChange(cardNum, new CardAxisSignalEvengArgs(cardNum, alarmValue));
            }
        }
        public static event EventHandler AxisHomeChange;
        protected static void OnAxisHomeChange(int cardNum, int homeValue)
        {
            if (AxisHomeChange != null)
            {
                AxisHomeChange(cardNum, new CardAxisSignalEvengArgs(cardNum, (ushort)homeValue));
            }
        }
        public static event EventHandler AxisLimitPChange;
        protected static void OnAxisLimitPChange(int cardNum, int limitPValue)
        {
            if (AxisLimitPChange != null)
            {
                AxisLimitPChange(cardNum, new CardAxisSignalEvengArgs(cardNum, (ushort)limitPValue));
            }
        }
        public static event EventHandler AxisLimitNChange;
        protected static void OnAxisLimitNChange(int cardNum, int limitNValue)
        {
            if (AxisLimitNChange != null)
            {
                AxisLimitNChange(cardNum, new CardAxisSignalEvengArgs(cardNum, (ushort)limitNValue));
            }
        }
        public static event EventHandler AxisEncPosChange;
        protected static void OnAxisEncPosChange(int cardNum, double[] posValue)
        {
            if (AxisEncPosChange != null)
            {
                AxisEncPosChange(cardNum, new CardAxisPosEvengArgs(cardNum, posValue));
            }
        }
        public static event EventHandler AxisPrfPosChange;
        protected static void OnAxisPrfPosChange(int cardNum, double[] posValue)
        {
            if (AxisPrfPosChange != null)
            {
                AxisPrfPosChange(null, new CardAxisPosEvengArgs(cardNum, posValue));
            }
        }
        //板卡开始读取所有信号
        public static void StartListenSignal()
        {
            Thread thdSignal = new Thread(ReadSignal);
            thdSignal.Start();
            thdSignal.IsBackground = true;
        }
        public static short GetInputIoBitStatus(string stationName, string ioName, out bool status)
        {
            short shrResult;
            status = false;
            IOParameter ioP = StationManage.FindInputIo(stationName, ioName);
            if (ioP == null)
            {
                CFile.WriteErrorForDate("函数：FindInputIo 执行异常，返回空 工站名：" + stationName + " IO名：" + ioName);
                return shrFail;
            }

            shrResult = GetInputIoBitStatus(ioP, out status);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }

            return shrSuccess;
        }

        public static void ReadOneSingnal()
        {
            short shrResult = 0;
            int i = 0;
            double[] arrAxisEnc = new double[intCardCount * 8];
            double[] arrAxisPrf = new double[intCardCount * 8];
            int intOrigin, intAlarm, intLimitP, intLimitN, intEnable, intInput, intOutput;
            short card = 0;
            for (card = 0; card < intCardCount; card++)
            {
                if (!blnRun)
                {
                    return;
                }
                shrResult = SR_GetEnableInput(card, out intEnable);
                if (shrResult == shrSuccess)
                {
                    arraAxisEnableStatus[card] = intEnable;
                }
                OnAxisEnableChange((int)card, arraAxisEnableStatus[card]);

                shrResult = SR_GetInput(card, out intInput);
                if (shrResult == shrSuccess)
                {
                    arrInputStatus[card] = intInput;
                }
                OnInputIOChange((int)card, arrInputStatus[card]);

                shrResult = SR_GetOutput(card, out intOutput);
                if (shrResult == shrSuccess)
                {
                    arrOutputStatus[card] = intOutput;
                }
                OnOutputIOChange((int)card, arrOutputStatus[card]);

                shrResult = SR_GetPrfPos(card, (short)1, ref arrAxisPrf);
                if (shrResult == shrSuccess)
                {
                    for (i = card; i < intAxisCountForCard + card * intAxisCountForCard; i++)
                    {
                        arrAxisPrfPos[i] = arrAxisPrf[i];
                    }
                    OnAxisPrfPosChange((int)card, arrAxisPrfPos);
                }

                shrResult = SR_GetEncPos(card, (short)1, ref arrAxisEnc);
                if (shrResult == shrSuccess)
                {
                    for (i = card; i < intAxisCountForCard + card * intAxisCountForCard; i++)
                    {
                        arrAxisEncPos[i] = arrAxisEnc[i];
                    }
                    OnAxisEncPosChange((int)card, arrAxisEncPos);
                }

                shrResult = SR_GetOriginInput(card, out intOrigin);
                if (shrResult == shrSuccess)
                {
                    arrAxisOriginStatus[card] = intOrigin;
                }
                OnAxisHomeChange((int)card, arrAxisOriginStatus[card]);

                shrResult = SR_GetLimitPInput(card, out intLimitP);
                if (shrResult == shrSuccess)
                {
                    arrAxisLimitPStatus[card] = intLimitP;
                }
                OnAxisLimitPChange((int)card, arrAxisLimitPStatus[card]);

                shrResult = SR_GetLimitNInput(card, out intLimitN);
                if (shrResult == shrSuccess)
                {
                    arrAxisLimitNStatus[card] = intLimitN;
                }
                OnAxisLimitNChange((int)card, arrAxisLimitNStatus[card]);

                shrResult = SR_GetAlarmInput(card, out intAlarm);
                if (shrResult == shrSuccess)
                {
                    arraAxisAlarmStatus[card] = intAlarm;
                }
                OnAxisAlarmChange((int)card, (ushort)arraAxisAlarmStatus[card]);
            }
            for (card = intExtendStartId; card < (intExtendStartId + intExtendIoCount); card++)
            {
                shrResult = SR_GetInput(card, out intInput);
                if (shrResult == shrSuccess)
                {
                    //if (arrInputStatus[intCardCount + card - intExtendStartId] != intInput)
                    {
                        arrInputStatus[intCardCount + card - intExtendStartId] = intInput;
                        OnInputIOChange((int)card, arrInputStatus[intCardCount + card - intExtendStartId]);
                    }
                }

                shrResult = SR_GetOutput(card, out intOutput);
                if (shrResult == shrSuccess)
                {
                    //if (arrOutputStatus[intCardCount + card - 11] != intOutput)
                    {
                        arrOutputStatus[intCardCount + card - intExtendStartId] = intOutput;
                        OnOutputIOChange((int)card, arrOutputStatus[intCardCount + card - intExtendStartId]);
                    }
                }
            }
        }
        private static void ReadSignal()
        {
            bool blnChange = false;
            short shrResult = 0;
            int i = 0;
            double[] arrAxisEnc = new double[intCardCount * 8];
            double[] arrAxisPrf = new double[intCardCount * 8];
            int intOrigin, intAlarm, intLimitP, intLimitN, intEnable, intInput, intOutput;
            short card = 0;
            while (blnRun)
            {
                for (card = 0; card < intCardCount; card++)
                {
                    if (!blnRun)
                    {
                        return;
                    }
                    shrResult = SR_GetEnableInput(card, out intEnable);
                    if (shrResult == shrSuccess)
                    {
                        if (arraAxisEnableStatus[card] != intEnable)
                        {
                            arraAxisEnableStatus[card] = intEnable;
                            OnAxisEnableChange((int)card, arraAxisEnableStatus[card]);
                        }
                    }

                    shrResult = SR_GetInput(card, out intInput);
                    if (shrResult == shrSuccess)
                    {
                        if (arrInputStatus[card] != intInput)
                        {
                            arrInputStatus[card] = intInput;
                            OnInputIOChange((int)card, arrInputStatus[card]);
                        }
                    }

                    shrResult = SR_GetOutput(card, out intOutput);
                    if (shrResult == shrSuccess)
                    {
                        if (arrOutputStatus[card] != intOutput)
                        {
                            arrOutputStatus[card] = intOutput;
                            OnOutputIOChange((int)card, arrOutputStatus[card]);
                        }
                    }

                 
                    
                        shrResult = SR_GetPrfPos(card, (short)1, ref arrAxisPrf);
                        if (shrResult == shrSuccess)
                        {
                            for (i = card; i < intAxisCountForCard + card * intAxisCountForCard; i++)
                            {
                                if (arrAxisPrfPos[i] != arrAxisPrf[i])
                                {
                                    arrAxisPrfPos[i] = arrAxisPrf[i];
                                    blnChange = true;
                                }
                            }
                            if (blnChange)
                            {
                                OnAxisPrfPosChange((int)card, arrAxisPrfPos);
                                blnChange = true;
                            }
                        }
                   
                   

                    shrResult = SR_GetEncPos(card, (short)1, ref arrAxisEnc);
                    if (shrResult == shrSuccess)
                    {
                        for (i = card; i < intAxisCountForCard + card * intAxisCountForCard; i++)
                        {
                            if (arrAxisEncPos[i] != arrAxisEnc[i])
                            {
                                arrAxisEncPos[i] = arrAxisEnc[i];
                                blnChange = true;
                            }
                            arrAxisEncPos[i] = arrAxisEnc[i];
                        }
                        if (blnChange)
                        {
                            OnAxisEncPosChange((int)card, arrAxisEncPos);
                            blnChange = false;
                        }

                    }

                    shrResult = SR_GetOriginInput(card, out intOrigin);
                    if (shrResult == shrSuccess)
                    {
                        if (arrAxisOriginStatus[card] != intOrigin)
                        {
                            arrAxisOriginStatus[card] = intOrigin;
                            OnAxisHomeChange((int)card, arrAxisOriginStatus[card]);
                        }
                    }

                    shrResult = SR_GetLimitPInput(card, out intLimitP);
                    if (shrResult == shrSuccess)
                    {
                        if (arrAxisLimitPStatus[card] != intLimitP)
                        {
                            arrAxisLimitPStatus[card] = intLimitP;
                            OnAxisLimitPChange((int)card, arrAxisLimitPStatus[card]);
                        }
                    }

                    shrResult = SR_GetLimitNInput(card, out intLimitN);
                    if (shrResult == shrSuccess)
                    {
                        if (arrAxisLimitNStatus[card] != intLimitN)
                        {
                            arrAxisLimitNStatus[card] = intLimitN;
                            OnAxisLimitNChange((int)card, arrAxisLimitNStatus[card]);
                        }
                    }

                    shrResult = SR_GetAlarmInput(card, out intAlarm);
                    if (shrResult == shrSuccess)
                    {
                        if (arraAxisAlarmStatus[card] != intAlarm)
                        {
                            arraAxisAlarmStatus[card] = intAlarm;
                            OnAxisAlarmChange((int)card, (ushort)arraAxisAlarmStatus[card]);
                        }
                    }

                }
                for (card = intExtendStartId; card < (intExtendStartId + intExtendIoCount); card++)
                {
                    shrResult = SR_GetInput(card, out intInput);
                    if (shrResult == shrSuccess)
                    {
                        if (arrInputStatus[intCardCount + card - intExtendStartId] != intInput)
                        {
                            arrInputStatus[intCardCount + card - intExtendStartId] = intInput;
                            OnInputIOChange((int)card, arrInputStatus[intCardCount + card - intExtendStartId]);
                        }
                    }

                    shrResult = SR_GetOutput(card, out intOutput);
                    if (shrResult == shrSuccess)
                    {
                        if (arrOutputStatus[intCardCount + card - intExtendStartId] != intOutput)
                        {
                            arrOutputStatus[intCardCount + card - intExtendStartId] = intOutput;
                            OnOutputIOChange((int)card, arrOutputStatus[intCardCount + card - intExtendStartId]);
                        }
                    }
                }
                Thread.Sleep(5);

            }
        }
    }
}
