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
        
        
        private const int intAxisCountForCard = 4;//卡轴的数量
        private const int intExtendIoCount = 0;   //扩展IO模块数量 
        private const int intExtendStartId = 11; //扩展模块起始号

        private const int intInputIoCount = 40;
        

        private static int intCardCount = 1; //卡的数量
       
        private static short shrGtsSuccess = 0;    //厂家函数返回成功
        private static short shrSuccess = 0;    //二次封装函数成功
        private static short shrFail = -1;      //二次封装函数失败
        private static short shrFail2 = -2;      //二次封装函数失败
        private static double dblAxisAcc = 3;   //减速度    7 0.06
        private static double dblAxisDec = 3;   //加速度
        private static short shtSmoothTime = 50; //平滑时间
        private static short shtPstMoveDir = 0; //正方向运动
        public const int Torlerance = 50; //最大误差脉冲数        
        public const int DtTime = 20;  //单位微秒，持续保存小于误差数后置位到位信号
        private static double dblMoveOutOriginTime = 4; //移出原点感应器信号超时时间，单位秒
        public const int AxisMotionError = 300;  //轴移动误差，单位脉冲
        public const int IoClose = 1;  //关闭IO
        public const int IoOpen = 0;  //打开IO
        public const int startSpeed = 200; //起始速度 200脉冲/秒
        public const int accSpeed = 200; //起始速度 200脉冲/秒
        public const double accTime = 0.1;  //加减速时间
        public const int accEcfu = 300; //加减速倍率

        private static ulong[] arrInputStatus = new ulong[10];
        private static int[] arrOutputStatus = new int[10];
        private static double[] arrAxisEncPos = new double[intCardCount * intAxisCountForCard];
        private static double[] arrAxisPrfPos = new double[intCardCount * intAxisCountForCard];
        private static int[] arraAxisEnableStatus = new int[intCardCount];
        private static int[] arraAxisAlarmStatus = new int[intCardCount];
        private static int[] arrAxisLimitPStatus = new int[intCardCount];
        private static int[] arrAxisLimitNStatus = new int[intCardCount];
        private static int[] arrAxisOriginStatus = new int[intCardCount];
        public static short tag_initResult =1;
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
        public static short SR_GetEncPos(short card, short axis, ref double[] pos, int count = intAxisCountForCard)
        {
            short shrResult;
            int posV = 0;
            for (int i = 1; i < count+1;i++ )
            {
                shrResult = (short)adt8940a1m.adt8940a1_get_actual_pos(card, (short)axis, out posV);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("SR_GetEncPos", shrResult);
                    return -1;
                }
                pos[i-1] = posV;
            }
            return 0;
        }

        public static short SR_GetEncPos(short card, short axis, ref double pos)
        {
            short shrResult;
            int posV = 0;
            
                shrResult = (short)adt8940a1m.adt8940a1_get_actual_pos(card, (short)axis, out posV);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("SR_GetEncPos", shrResult);
                    return -1;
                }
                pos = posV;
            
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
        public static short SR_GetPrfPos(short card, short axis, ref double pos, short count = 1)
        {
            short shrResult;
            int posV = 0;
            shrResult = (short)adt8940a1m.adt8940a1_get_command_pos(card, (short)axis, out posV);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("SR_GetPrfPos", shrResult);
                return -1;
            }
            pos = posV;
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
        public static short SR_GetPrfPos(short card, short axis, ref double[] pos, short count = intAxisCountForCard)
        {
            uint[] pClock = new uint[count];
            short shrResult;
            int posV;
            for (int i = axis; i < count + 1; i++)
            {
                shrResult = (short)adt8940a1m.adt8940a1_get_command_pos(card, (short)i, out posV);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("adt8940a1_get_command_pos", shrResult);
                    return -1;
                }
                pos[i - 1] = posV;
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
            shrResult = (short)adt8940a1m.adt8940a1_set_command_pos(card, axis, 0);
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
            short returnValue;
            returnValue = (short)adt8940a1m.adt8940a1_initial();
            if (returnValue <= 0)
            {
                MessageBoxLog.Show("控制卡初始化失败!");
                if (returnValue == 0) MessageBoxLog.Show("没有安装ADT8960卡！");
                if (returnValue == -1) MessageBoxLog.Show("没有安装端口驱动程序！");
                if (returnValue == -2) MessageBoxLog.Show("PCI桥故障！");
                return -1;
            }
            intCardCount = returnValue;
            for (Int32 i = 1; i < intAxisCountForCard + 1; i++)
                for (Int32 j = 0; j < returnValue; j++)
                {
                    adt8940a1m.adt8940a1_set_limit_mode(j, i, 0, 0, 0); //设定限位模式，设正负限位有效，低电平有效

                    adt8940a1m.adt8940a1_set_command_pos(j, i, 0);      //清逻辑计数器

                    adt8940a1m.adt8940a1_set_actual_pos(j, i, 0);       //清实位计数器

                    adt8940a1m.adt8940a1_set_startv(j, i, 1000);        //设定起始速度

                    adt8940a1m.adt8940a1_set_speed(j, i, 1000);         //设定驱动速度

                    adt8940a1m.adt8940a1_set_acc(j, i, 625);            //设定加速度  
                }           
            return shrSuccess;
        }
        /// <summary>
        /// 关闭控制卡  暂时没找到
        /// </summary>
        /// <returns></returns>
        public static short SR_CloseCard()
        {
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
        public static short SR_PulseMode(short card, short axis,int mode=0)
        {
            short shrResult = (short)adt8940a1m.adt8940a1_set_pulse_mode(card, axis, mode, 0, 0);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_set_pulse_mode", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        //未完成 设置编码器计数方式   暂未找到函数
        public static short SR_SetEncMode()
        {
            return shrSuccess;
        }

        /// <summary>
        /// 除各轴异常状态 暂时没找到
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="axisCount">轴数量，默认为1，清除单轴</param>
        /// <returns></returns>
        public static short SR_ClrStatus(short card, short axis, short axisCount = 1)
        {
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
           
            return shrSuccess;
        }
        /// <summary>
        /// 轴使能 未找到
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="bEanble">使能状态 true 使能  false 使能关闭</param>
        /// <returns></returns>
        public static short SR_GetServoEnable(short card, short axis, out bool bEanble)
        {
            bEanble = false;
            return shrSuccess;
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
        /// 轴使能  未找到
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="bEanble">使能状态 true 使能  false 使能关闭</param>
        /// <returns></returns>
        public static short SR_SetServoEnable(short card, short axis, bool bEanble)
        {
            return shrSuccess;
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
        /// <summary>
        /// 未找到
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axisCount"></param>
        /// <param name="bEanble"></param>
        /// <returns></returns>
        public static short SR_SetAllAxisEnable(short card, short axisCount = intAxisCountForCard, bool bEanble = true)
        {
            return shrSuccess;
            short shrResult;
            short i = 0;
            if (bEanble)
            {
                for (i = 1; i < axisCount+1; i++)
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
                for (i = 1; i < axisCount+1; i++)
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
            return shrSuccess;
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
        /// <returns>返回0与非0   0代表轴停止， 非0轴在运动中</returns>
        public static short SR_GetAxisStatus(short card, short axis, out int axisStatus)
        {
            short shrResult;            
            shrResult = (short)adt8940a1m.adt8940a1_get_status(card, axis, out axisStatus);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_get_status", shrResult);
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
        /// <summary>
        /// 设置单轴停止 减速停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short SR_AxisStop(short card, short axis)
        {
            short shrResult;
            shrResult = (short)adt8940a1m.adt8940a1_dec_stop(card, axis);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_dec_stop", shrResult);
                return shrFail;
            }
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
        /// 设置单卡上所有轴停止 减速停止
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axisCount">默认6轴卡，4轴卡参数传4</param>
        /// <returns></returns>
        public static short SR_AxisAllStop(short card, int axisCount = intAxisCountForCard)
        {
            short shrResult;
            for (int i = 1; i < axisCount + 1;i++ )
            {
                shrResult = SR_AxisStop( card, (short) i);
                if (shrResult != shrGtsSuccess)
                {                   
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
            shrResult = (short)adt8940a1m.adt8940a1_sudden_stop(card, axis);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_sudden_stop", shrResult);
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
        public static short SR_AxisAllEmgStop(short card, int axisCount = intAxisCountForCard)
        {
            short shrResult;
            for (short i = 1; i < axisCount + 1;i++ )
            {
                shrResult = SR_AxisEmgStop(card, i);
                if (shrResult != shrGtsSuccess)
                {                    
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
            return shrSuccess;
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
            int intOriginBit = 2 + (axis - 1) * 6;           
            shrResult = SR_GetInputBit(card, (short)intOriginBit,out bStatus);
            if (shrResult != shrGtsSuccess)
            {
                return shrFail;
            }          
            bStatus = !bStatus;
             
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
            pValue = 0;
            shrResult = PubReadInput(card, 2, intAxisCountForCard, 6, ref pValue);
            if (shrResult != shrGtsSuccess)
            {
                return shrFail;
            }
            return  shrSuccess;
           
        }


        /// <summary>
        /// 读取IO输入信号，主要用于众为兴板卡
        /// </summary>
        /// <param name="card"></param>
        /// <param name="startId">起始IO编号</param>
        /// <param name="count">读取IO数量</param>
        /// <param name="step">IO之间间隔位数</param>
        /// <param name="value">存储读取IO的值</param>
        /// <returns></returns>
        private static short PubReadInput(short card, short startId,short count,short step,ref int value)
        {
            short shrReadId=startId;
            short shrResult; bool blnStatus = false;
            for(short i=0;i<count;i++)
            {
                shrReadId = (short)(shrReadId + i * step);
                shrResult = SR_GetInputBit(card, shrReadId, out blnStatus);
                if (shrResult != shrGtsSuccess)
                {
                    return shrFail;
                }
                if (blnStatus)
                    value = value + (1 << i);
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
            pValue = 0;
            shrResult = PubReadInput(card, 0, intAxisCountForCard, 6, ref pValue);
            if (shrResult != shrGtsSuccess)
            {
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
            pValue = 0;
            shrResult = PubReadInput(card, 1, intAxisCountForCard, 6, ref pValue);
            if (shrResult != shrGtsSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 电机使能输入状态  暂时不用
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pValue"></param>
        /// <returns></returns>
        public static short SR_GetEnableInput(short card, out int pValue)
        {
            
            pValue = 0;
            return shrSuccess;
            short shrResult;
            shrResult = mc.GT_GetDo(card, mc.MC_ENABLE, out pValue);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetDo", shrResult);
                return shrFail;
            }
           
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
            pValue = 0;
            shrResult = PubReadInput(card, 3, intAxisCountForCard, 6, ref pValue);
            if (shrResult != shrGtsSuccess)
            {
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
                shrResult = (short)adt8940a1m.adt8940a1_read_bit(card, ioBit);
                if (shrResult < shrSuccess)
                {
                    CommandResult("adt8940a1_read_bit", shrResult);
                    return shrFail;
                }
                if (shrResult==0)
                {
                    bStatus = false;
                }
                else
                    bStatus = true;
                
            }         
            return shrSuccess;
        }
        /// <summary>
        /// 获取单卡 所有输入信号 
        /// </summary>
        /// <param name="card"></param>
        /// <param name="pValue">所有输入IO状态，按位取</param>
        /// <returns>执行此函数需先对函数返回值判断，若不成功，不要使用获取的pValue结果</returns>
        public static short SR_GetInput(short card, out ulong pValue)
        {
            short shrResult;
           
          
            bool blnStatus = false;
            pValue = 0;
            if (card >= intExtendStartId)
            {
                shrResult = ExtendIoCard.GetIpnutIoAll(card, out pValue);
                if (shrResult != shrGtsSuccess)
                {
                    return shrFail;
                }
            }
            else
            {
                for (short i = 24; i < intInputIoCount; i++)
                {
                    shrResult = SR_GetInputBit(card, i, out blnStatus);
                    if (shrResult != shrGtsSuccess)
                    {
                        return shrFail;
                    }
                    if (blnStatus)
                    {
                        pValue = pValue + (ulong)(1 << i);
                    }
                  
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
            if (card >= intExtendStartId && intExtendIoCount>0)
            {
                shrResult = ExtendIoCard.SetOutputIoBit( (short)(card - intExtendStartId), ioBit, value);
                if (shrResult != shrGtsSuccess)
                {                   
                    return shrFail;
                }
            }
            else
            {
                shrResult =(short) adt8940a1m.adt8940a1_write_bit(card, ioBit,value);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("adt8940a1_write_bit", shrResult);
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
        public static short SR_SetOutput(short card, int value)
        {
            short shrResult;
            if (card >= intExtendStartId && intExtendIoCount > 0)
            {
                shrResult = ExtendIoCard.SetOutputAll((short)(card - intExtendStartId), (int)value);
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
            int ioStatus=0;
            if (card < intExtendStartId)
            {
                for (short i = 0; i < 16;i++ )
                {
                    shrResult = SR_GetOutputBit(card, i, out ioStatus);
                    if (shrResult != shrGtsSuccess)
                    {
                        return shrFail;
                    }
                    if (ioStatus>0)
                    {
                        outputIoStatus = outputIoStatus + (1 << i);
                    }
                }               
                return shrSuccess;
            }
            else
            {
                shrResult = ExtendIoCard.GetOutputIoAll((short)(card - intExtendStartId), out outputIoStatus);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_SetExtIoBitGts", shrResult);
                    return shrFail;
                }
                return shrSuccess;
            }
            return shrFail; 

        }
        /// <summary>
        /// 获取单个输出IO状态
        /// </summary>
        /// <param name="card"></param>
        /// <param name="ioBit"></param>
        /// <param name="outputIoStatus"></param>
        /// <returns></returns>
        public static short SR_GetOutputBit(short card, short ioBit,out int outputIoStatus)
        {            
            short shrResult;
            outputIoStatus = 0;
            if (card < intExtendStartId)
            {
                shrResult = (short)adt8940a1m.adt8940a1_get_out(card,ioBit);
                if (shrResult == -1)
                {
                    CommandResult("adt8940a1_get_out", shrResult);
                    return shrFail;
                }
                outputIoStatus = shrResult;
                return shrSuccess;
            }
            else
            {
                bool blnOutStatus;
                shrResult = ExtendIoCard.GetOutputIoBit((short)(card - intExtendStartId),ioBit, out blnOutStatus);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_SetExtIoBitGts", shrResult);
                    return shrFail;
                }
                if (blnOutStatus)
                {
                    outputIoStatus = 1;
                }
                return shrSuccess;
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
            short shrResult;
            shrResult = SR_GetAxisStatus(card, axis, out axisStatus);
            if (shrResult != shrGtsSuccess)
            {
                return shrFail;
            }
            if (axisStatus == 0)
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
        public static short SR_GoHome(short card, short axis, double hightSpeed, double lowSpeed, int dir = 0)
        {
            short shrResult;
            shrResult=(short)adt8940a1m.adt8940a1_SetHomeMode_Ex(card, axis, dir, 0, 0, -1, 50000, 1, 0);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_SetHomeMode_Ex", shrResult);
                return shrFail;
            }
            shrResult = (short)adt8940a1m.adt8940a1_SetHomeSpeed_Ex(card, axis, startSpeed, (int)(hightSpeed * 1000), (int)(lowSpeed * 1000), startSpeed, startSpeed);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_SetHomeSpeed_Ex", shrResult);
                return shrFail;
            }
            shrResult = (short)adt8940a1m.adt8940a1_HomeProcess_Ex(card, axis);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_HomeProcess_Ex", shrResult);
                return shrFail;
            }
            while (blnRun)
            {
                shrResult = (short)adt8940a1m.adt8940a1_GetHomeStatus_Ex(card, axis);
                if (shrResult<0 || shrResult>20)
                {
                    return shrFail;
                }
                if (shrResult==0)
                {
                    return shrSuccess;
                }
                Thread.Sleep(5);
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
           return SR_GoHome(card, axis, hightSpeed, lowSpeed, dir);
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
        public static short SR_GoHomeWithStepMotor(short card, short axis, double hightSpeed, double lowSpeed, int dir = 0)
        {            
            short shrResult;
            shrResult=SR_GoHome(card, axis, hightSpeed, lowSpeed, dir);
            if (shrResult != shrSuccess)
            {
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
            short shrResult = 0;
            shrResult = (short)adt8940a1m.adt8940a1_symmetry_relative_move(card, axis, postion, startSpeed, (int)(speed*1000), accTime);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_symmetry_relative_move", shrResult);
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
            short shrResult = 0;
            shrResult = (short)adt8940a1m.adt8940a1_symmetry_absolute_move(card, axis, postion, startSpeed, (int)(speed*1000), accTime);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_symmetry_absolute_move", shrResult);
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
            short shrResult = 0;
           
            
            shrResult = (short)adt8940a1m.adt8940a1_set_startv(0, axis, startSpeed); // 300000
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_set_startv", shrResult);
                return shrFail;
            }
            shrResult =(short) adt8940a1m.adt8940a1_set_speed(0, axis, (int)speed);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_set_speed", shrResult);
                return shrFail;
            }
            shrResult = (short)adt8940a1m.adt8940a1_continue_move(0, axis, dir);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_continue_move", shrResult);
                return shrFail;
            }
         
            return shrSuccess;
        }
        public static short SR_SetLimitMode(short card, short axis, int limitPMode=0, int limitNMode=0,int logic=0)
        {
            short shrResult = 0;
            shrResult = (short)adt8940a1m.adt8940a1_set_limit_mode(card, axis, limitPMode, limitNMode, logic);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_set_limit_mode", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }

        public static short SR_SetOriginMode(short card, short axis, int mode=0, int logic=0)
        {
            short shrResult = 0;
            shrResult = (short)adt8940a1m.adt8940a1_set_stop0_mode(card, axis, mode, 0);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_set_stop0_mode", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }


        public static short SR_SetPulseMode(short card, short axis, int pulseMode=1, int ModeLogic = 0,int DirLogic=0)
        {
            short shrResult = 0;
            shrResult = (short)adt8940a1m.adt8940a1_set_pulse_mode(card, axis, pulseMode,ModeLogic,DirLogic);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_set_pulse_mode", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }

        public static short SR_SetZMode(short card, short axis, int mode=0, int logic = 0)
        {
            short shrResult = 0;
            shrResult = (short)adt8940a1m.adt8940a1_set_stop1_mode(card, axis, mode, 0);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_set_stop0_mode", shrResult);
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
            shrResult=(short)adt8940a1m.adt8940a1_set_acc(card, axis, 2000);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("adt8940a1_set_acc", shrResult);
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
           
                
            shrResult = SR_InitCard(carNum, intAxisCountForCard);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
           
            //扩展卡
            if (intExtendIoCount > 0)
            {
                shrResult = ExtendIoCard.InitCard();
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                tag_initResult = 0;
                return shrSuccess;
            }
            else
            {
                tag_initResult = 0;
                return shrSuccess;
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
            capture = 0; pValue = 0;
            return shrSuccess;
            int intAxisStatus;
            int intCount = 0;
            uint uintClock;
            pValue = -1;
           
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
            capture = 0; pValue = 0;
            return shrSuccess;
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
            capture = 0; pValue = 0;
            return shrSuccess;
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
            capture = 0; pValue = 0;
            return shrSuccess;
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
            
            return shrSuccess;
            short shrResult;
            //adt8940a1m.adt8940a1_change_pmove_pos()
                
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
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static short WaitAxisStop(short card, short axis)
        {
            int intAxisStatus;           
            short shrResult;           
            do
            {
                //程序是否正常运行
                if (!blnRun)
                {
                    SR_AxisEmgStop(card, axis);
                    return shrFail;
                }                
                //读轴状态
                shrResult = SR_GetAxisStatus(card, axis, out intAxisStatus);                
                if (shrResult != shrGtsSuccess)
                {                    
                    return shrFail;
                }
                Thread.Sleep(5);
            } while (intAxisStatus != 0);  //运动中         
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
        /// <summary>
        ///  轴定位校验
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="postion"></param>
        /// <returns></returns>
        public static short VerificationAxisPoint(short card, short axis, int postion)
        {
            short shrResult;
            double dblSendValue = 0, dblEncValue = 0;
            uint uintClock;
            //读取规划位置
            shrResult = SR_GetPrfPos(card, axis, ref dblSendValue);
            if (shrResult != shrGtsSuccess)
            {
                return shrFail;
            }
            //读取编码器返馈位置
            shrResult = SR_GetEncPos(card, axis, ref dblEncValue);
            if (shrResult != shrGtsSuccess)
            {
                return shrFail;
            }
            //判断发送脉冲是否出现异常   &&  发送脉冲与实际位置是否偏差超过设定范围
            if ((Math.Abs(dblSendValue - postion) <= 10) && (Math.Abs(dblSendValue - dblEncValue) <= AxisMotionError))
            {
               
                return shrSuccess;//在误差范围内返回成功
            }
            else
            {
                CFile.WriteErrorForDate(card + "卡" + axis + "轴  运动去坐标位" + postion + " 命令位置：" + dblSendValue + " 实际位置：" + dblEncValue);
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
            double dblSendValue=0;
           
            //读取规划位置
            shrResult = SR_GetPrfPos(card, axis, ref dblSendValue);
            if (shrResult != shrGtsSuccess)
            {
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
            short shrResult = SR_GoHome(axisC.CardNum, axisC.AxisNum, axisC.HomeSpeedHight * axisC.Eucf / 1000, axisC.HomeSpeed * axisC.Eucf / 1000, (int)axisC.HomeDir);
            if (shrResult != shrSuccess)
            {
                return shrFail;
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
            short shrResult = SR_GoHomeWithStepMotor(axisC.CardNum, axisC.AxisNum, axisC.HomeSpeedHight * axisC.Eucf / 1000, axisC.HomeSpeed * axisC.Eucf / 1000, (int)axisC.HomeDir);
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
            Thread homeThread = new Thread(new ParameterizedThreadStart(delegate { SR_GoHome(axisC.CardNum, axisC.AxisNum, axisC.HomeSpeedHight * axisC.Eucf / 1000, axisC.HomeSpeed * axisC.Eucf / 1000, (int)axisC.HomeDir); }));
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
            Thread homeThread = new Thread(new ParameterizedThreadStart(delegate { SR_GoHome(axisC.CardNum, axisC.AxisNum, axisC.HomeSpeedHight * axisC.Eucf / 1000, axisC.HomeSpeed * axisC.Eucf / 1000, (int)axisC.HomeDir); }));
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
            if (axisC.SoftLimitEnablel == 0)
            {
                if (pointA.arrPoint[axisC.AxisIndex].dblPonitValue > axisC.SoftLimitMaxValue)
                {
                    CFile.WriteErrorForDate(axisC.CardNum+"卡"+axisC.AxisNum+"轴"+"运动去点 "+pointName+"  "+pointA.arrPoint[axisC.AxisIndex].dblPonitValue+"  大于该轴软正限位" );
                    return shrFail2;
                }
                if (pointA.arrPoint[axisC.AxisIndex].dblPonitValue < axisC.SoftLimitMinValue)
                {
                    CFile.WriteErrorForDate(axisC.CardNum + "卡" + axisC.AxisNum + "轴" + "运动去点 " + pointName + "  " + pointA.arrPoint[axisC.AxisIndex].dblPonitValue + "  小于该轴软负限位");
                    return shrFail2;
                }
            }
            short shrResult = SR_AbsoluteMove(axisC.CardNum, axisC.AxisNum, (int)(pointA.arrPoint[axisC.AxisIndex].dblPonitValue * axisC.Eucf), pointA.arrPoint[axisC.AxisIndex].dblPonitSpeed * axisC.Eucf / 1000);
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
        /// 轴绝对运动
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short AxisAbsoluteMove(string stationName, string axisName, string pointName, int asxisIndex)
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
            short shrResult;
            if (axisC.SoftLimitEnablel == 0)
            {
                double dblPos = 0;
                uint uintClock;
                shrResult = SR_GetPrfPos(axisC.CardNum, axisC.AxisNum, ref dblPos);
                if (shrResult != shrGtsSuccess)
                {
                    CommandResult("GT_GetPrfPos", shrResult);
                    return -1;
                }
                dblPos = dblPos / axisC.Eucf;
                if (dblPos + pointA.arrPoint[axisC.AxisIndex].dblPonitValue > axisC.SoftLimitMaxValue)
                {
                    CFile.WriteErrorForDate(axisC.CardNum + "卡" + axisC.AxisNum + "轴" + "运动去点 " + pointName + "  " + pointA.arrPoint[axisC.AxisIndex].dblPonitValue + "  大于该轴软正限位");
                    return shrFail2;
                }
                if (dblPos + pointA.arrPoint[axisC.AxisIndex].dblPonitValue < axisC.SoftLimitMinValue)
                {
                    CFile.WriteErrorForDate(axisC.CardNum + "卡" + axisC.AxisNum + "轴" + "运动去点 " + pointName + "  " + pointA.arrPoint[axisC.AxisIndex].dblPonitValue + "  小于该轴软负限位");
                    return shrFail2;
                }
            }
            shrResult = SR_RelativeMove(axisC.CardNum, axisC.AxisNum, (int)(pointA.arrPoint[axisC.AxisIndex].dblPonitValue * axisC.Eucf), pointA.arrPoint[axisC.AxisIndex].dblPonitSpeed * axisC.Eucf / 1000);
            if (shrResult != shrSuccess)
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
            short shrResult = AxisRelativeMove(stationName,axisName,pointName);
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
            double dblPos = 0;
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
            short shrResult = SR_GetPrfPos(axisC.CardNum, axisC.AxisNum, ref dblPos);
            if (shrResult != shrGtsSuccess)
            {
                CommandResult("GT_GetPrfPos", shrResult);
                return -1;
            }
            if (axisC.SoftLimitEnablel == 0)
            {               
                dblPos = dblPos / axisC.Eucf;
                if (dblPos + offValue > axisC.SoftLimitMaxValue)
                {
                    CFile.WriteErrorForDate(axisC.CardNum + "卡" + axisC.AxisNum + "轴" + "运动去点 " + (dblPos + offValue).ToString() + "  大于该轴软正限位");
                    return shrFail2;
                }
                if (dblPos + offValue < axisC.SoftLimitMinValue)
                {
                    CFile.WriteErrorForDate(axisC.CardNum + "卡" + axisC.AxisNum + "轴" + "运动去点 " + (dblPos + offValue).ToString() + "  小于该轴软负限位");
                    return shrFail2;
                }
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
            shrResult = VerificationAxisPoint(axisC.CardNum, axisC.AxisNum, (int)(dblPos + offValue * axisC.Eucf));
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }

        /// <summary>
        /// 步进电机相对运动并等待轴停止，检测运动结果 用于相机定校准
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <param name="pointName"></param>
        /// <returns></returns>
        public static short StepMotorRelativeMoveAndVrfStop(string stationName, string axisName, double offValue, double speed)
        {
            double dblPos = 0;
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
            short shrResult = SR_GetPrfPos(axisC.CardNum, axisC.AxisNum, ref dblPos);
            if (shrResult != shrGtsSuccess)
            {                
                return -1;
            }
            if (axisC.SoftLimitEnablel == 0)
            {
                dblPos = dblPos / axisC.Eucf;
                if (dblPos + offValue > axisC.SoftLimitMaxValue)
                {
                    CFile.WriteErrorForDate(axisC.CardNum + "卡" + axisC.AxisNum + "轴" + "运动去点 " + (dblPos + offValue).ToString() + "  大于该轴软正限位");
                    return shrFail2;
                }
                if (dblPos + offValue < axisC.SoftLimitMinValue)
                {
                    CFile.WriteErrorForDate(axisC.CardNum + "卡" + axisC.AxisNum + "轴" + "运动去点 " + (dblPos + offValue).ToString() + "  小于该轴软负限位");
                    return shrFail2;
                }
            }
            shrResult = SR_RelativeMove(axisC.CardNum, axisC.AxisNum,(int) (offValue * axisC.Eucf), speed * axisC.Eucf / 1000);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = WaitAxisStop(axisC.CardNum, axisC.AxisNum);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            shrResult = VerificationStepPoint(axisC.CardNum, axisC.AxisNum, (int)(dblPos + offValue * axisC.Eucf));
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
                shrResult = SR_ContinueMove(axisC.CardNum, axisC.AxisNum, (axisC.Speed * axisC.Eucf / 1000), dir);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }
            if (!check)
            {
                Thread.Sleep(1000);
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
                    SR_AxisEmgStop(axisC.CardNum, axisC.AxisNum);
                    return shrFail;
                }
                shrResult = GetInputIoBitStatus(input1, out blnStatus1);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
                if (blnStatus1)
                {
                    
                    //if(inputName=="载具回流等待感应" || inputName == "上料扫码定位感应")
                    //{
                    //    Thread.Sleep(1300);
                    //}                   
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
                shrResult = SR_ContinueMove(axisC.CardNum, axisC.AxisNum, (axisC.Speed * axisC.Eucf / 1000), dir);
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
                    SR_AxisEmgStop(axisC.CardNum, axisC.AxisNum);
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
                shrResult = SR_ContinueMove(axisC.CardNum, axisC.AxisNum, (axisC.Speed * axisC.Eucf / 1000), dir);
                if (shrResult != shrSuccess)
                {
                    return shrFail;
                }
            }
            if (!check)
            {
                Thread.Sleep(600);
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
            int i = 0;
            while (blnRun)
            {
                dtmNow = DateTime.Now;
                if ((dtmNow - dtmStart).TotalSeconds > outTime)
                {
                    shrResult = SR_AxisEmgStop(axisC.CardNum, axisC.AxisNum);
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
                    if (i < 2)
                    {
                        shrResult = SR_AxisEmgStop(axisC.CardNum, axisC.AxisNum);
                        if (shrResult != shrSuccess)
                        {
                            return shrFail;
                        }
                        if (SR_IsAxisStop(axisC.CardNum, axisC.AxisNum) == shrSuccess)
                        {
                            shrResult = SR_ContinueMove(axisC.CardNum, axisC.AxisNum, (axisC.Speed * axisC.Eucf / 1000), dir);
                            if (shrResult != shrSuccess)
                            {
                                return shrFail;
                            }
                        }
                        i++;
                        Thread.Sleep(50);
                        continue;
                    }
                    
                    
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
            return shrSuccess;
        }
        /// <summary>
        /// 获取单通道（Bit）输入IO状态，函数执行成功 判断status参数值 TRUE　有信号输入　　FALSE无信号输入 
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="ioName"></param>
        /// <param name="status"></param>
        /// <returns></returns>
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
        /// 气缸输入IO检测
        /// </summary>
        /// <param name="input1">检测信号1</param>
        /// <param name="input2">检测信号2</param>
        /// <param name="outTime">气缸超时时间</param>
        /// <returns></returns>
        public static short CyInputIoAndSignalCheck(IOParameter check,IOParameter input1 = null, IOParameter input2 = null, double outTime = 6)
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
                    shrReuslt = GetInputIoBitStatus(check, out ioStatus);
                    if (shrReuslt != shrSuccess)
                    {
                        return shrFail;
                    }
                    if (ioStatus)
                    {
                        return 1;
                    }
                    shrReuslt = GetInputIoBitStatus(input2, out ioStatus);
                    if (shrReuslt != shrSuccess)
                    {
                        return shrFail;
                    }                    
                    else if (!ioStatus)
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
                    shrReuslt = GetInputIoBitStatus(check, out ioStatus);
                    if (shrReuslt != shrSuccess)
                    {
                        return shrFail;
                    }
                    if (ioStatus)
                    {
                        return 1;
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
        /// 气缸双电控输出 信号触发复位
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
        public static short CyDoubleReControl(string stationName, string outIoName, short outValue, string outIoName1, short outValue1,string checkIoName, bool check, string inputName1 = null, string inputName2 = null)
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
            IOParameter checkIO = StationManage.FindInputIo(stationName, checkIoName);
            if (checkIO == null)
            {
                return shrFail;
            }
            short shrResult = 0;
            bool blnStatus=false;
            shrResult = GetInputIoBitStatus(checkIO, out blnStatus );
            if (shrResult == shrFail)
            {
                return shrFail;
            }
            if (blnStatus)
            {
                return 1;
            }
            
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
                shrResult = CyInputIoAndSignalCheck(checkIO,input1, input2);
                if (shrResult == 1)  //中间需要判断的信号触发，气缸还原动作
                {
                    shrResult = SetOutputIoBit(outI0, outValue1);
                    if (shrResult == shrFail)
                    {
                        return shrFail;
                    }
                    shrResult = SetOutputIoBit(outI01, outValue);
                    if (shrResult == shrFail)
                    {
                        return shrFail;
                    }
                    return 1;
                }else if (shrResult != shrSuccess)
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
            if (ioValue==IoClose)
            {
                io.BlnOut = false;
            }
            else
            {
                io.BlnOut = true;
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
        protected static void OnInputIOChange(int cardNum, ulong ioStatus)
        {
            if (InputIOChange != null)
                InputIOChange(cardNum, new CardInputIOEvengArgs(cardNum, ioStatus));
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

        public static void ReadOneSingnal()
        {
            short shrResult = 0;
            int i = 0;
            double[] arrAxisEnc = new double[8];
            double[] arrAxisPrf = new double[8];
            int intOrigin, intAlarm, intLimitP, intLimitN, intEnable, intInput, intOutput;
            short card = 0;
            for (card = 0; card < intCardCount; card++)
            {
                if (!blnRun)
                {
                    return;
                }
               
                OnAxisEnableChange((int)card, arraAxisEnableStatus[card]);

               
                OnInputIOChange((int)card, arrInputStatus[card]);

               
                OnOutputIOChange((int)card, arrOutputStatus[card]);

               
                    OnAxisPrfPosChange((int)card, arrAxisPrfPos);
                

               
                    OnAxisEncPosChange((int)card, arrAxisEncPos);
                

               
                OnAxisHomeChange((int)card, arrAxisOriginStatus[card]);

               
                OnAxisLimitPChange((int)card, arrAxisLimitPStatus[card]);

                
                OnAxisLimitNChange((int)card, arrAxisLimitNStatus[card]);

               
                OnAxisAlarmChange((int)card, (ushort)arraAxisAlarmStatus[card]);
            }
            for (card = intExtendStartId; card < (intExtendStartId + intExtendIoCount); card++)
            {
                
                        OnInputIOChange((int)card, arrInputStatus[intCardCount + card - intExtendStartId]);              
                        
                        OnOutputIOChange((int)card, arrOutputStatus[intCardCount + card - intExtendStartId]);
              
            }
        }
        private static void ReadSignal()
        {
            bool blnChange = false;
            short shrResult = 0;
            int i = 0;
            double[] arrAxisEnc = new double[8];
            double[] arrAxisPrf = new double[8];
            int intOrigin, intAlarm, intLimitP, intLimitN, intEnable,  intOutput;
            ulong intInput;
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
                        for (i = 0; i < intAxisCountForCard; i++)
                        {
                            if (arrAxisPrfPos[card * intAxisCountForCard + i] != arrAxisPrf[i])
                            {
                                arrAxisPrfPos[card * intAxisCountForCard + i] = arrAxisPrf[i];
                                blnChange = true;
                            }
                        }
                        if (blnChange)
                        {
                            OnAxisPrfPosChange((int)card, arrAxisPrfPos);
                            blnChange = false;
                        }
                    }

                    shrResult = SR_GetEncPos(card, (short)1, ref arrAxisEnc);
                    if (shrResult == shrSuccess)
                    {
                        for (i = 0; i < intAxisCountForCard; i++)
                        {
                            if (arrAxisEncPos[card * intAxisCountForCard + i] != arrAxisEnc[i])
                            {
                                arrAxisEncPos[card * intAxisCountForCard + i] = arrAxisEnc[i];
                                blnChange = true;
                            }                            
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
