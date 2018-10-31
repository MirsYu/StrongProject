using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace StrongProject
{
     
    public    class NewCtrlCardV0
    {
        /// <summary>
        ///表示不同类型的卡，各有多少卡，比如 tag_CardCount[0] 表示8491的卡数， tag_CardCount[1] 8496的卡数
        /// </summary>
        public static int[] tag_CardCount = new int[(int)(MotionCardManufacturer.MotionCardManufacturer_max)];      
        /// <summary>
        ///    表示不同类型的卡，各有多少卡，比如 tag_CardCount[0] 表示8491的轴数， tag_CardCount[1] 8496的卡数
        /// </summary>
        public static  int[] tag_CardAxisCount = new int[(int)(MotionCardManufacturer.MotionCardManufacturer_max)];

        public static NewCtrlCardBase[] tag_NewCtrlCardBase = new NewCtrlCardBase[(int)(MotionCardManufacturer.MotionCardManufacturer_max)];

        public static short shrGtsSuccess = 0;
        public static short shrFail = -9999;
        public static short shrSuccess = 0;
        public static int tag_initResult = 0;
        public static int tag_isInit = 0;
        public static bool IsExit()
        {
            if (Global.WorkVar.tag_StopState == 1)
            {
                return true;
            }
            if (Global.WorkVar.tag_IsExit == 1)
            {

                return true;
            }
            
            return false;
        }

        public static short initCard(int[] card)
        {
            short ret = 0;
            short ret1 = 0;
            tag_NewCtrlCardBase[(int)MotionCardManufacturer.MotionCardManufacturer_8940] = new NewCtrlCard_ZWX8940();
            tag_NewCtrlCardBase[(int)MotionCardManufacturer.MotionCardManufacturer_8960m] = new NewCtrlCard_ZWX8690m();
            tag_NewCtrlCardBase[(int)MotionCardManufacturer.MotionCardManufacturer_IO3224] = new NewCtrlCard_IO3224();

            tag_NewCtrlCardBase[(int)MotionCardManufacturer.MotionCardManufacturer_DMC3800] = new NewCtrlCard_DMC3000(8,0);
            tag_NewCtrlCardBase[(int)MotionCardManufacturer.MotionCardManufacturer_DMC3600] = new NewCtrlCard_DMC3000(6,1);
            tag_NewCtrlCardBase[(int)MotionCardManufacturer.MotionCardManufacturer_DMC3400] = new NewCtrlCard_DMC3000(4,2);

            tag_NewCtrlCardBase[(int)MotionCardManufacturer.MotionCardManufacturer_DMC1000B] = new NewCtrlCard_DMC1000B(4);

            for (int i = 0; i < (int)(MotionCardManufacturer.MotionCardManufacturer_max); i++)
            {
                if (card[i] == 0)
                {
                    continue;
                }
                NewCtrlCardBase Base_ = tag_NewCtrlCardBase[i];
                ret = Base_.SR_InitCard();

                if (ret <= 0)
                {
                    tag_CardCount[i] = 0;
                    tag_CardAxisCount[i] = 0;
                    tag_initResult = 1;
                    ret1 = -1;
                   // return -1;
                }
                else
                {
                    tag_CardCount[i] = ret;
                    tag_CardAxisCount[i] = Base_.tag_AxisCount;
                }
            }
            tag_isInit = 1;
            return ret1;
        }

        public static short set_io_mode(AxisConfig axisC)
        {
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            short ret = 0;
            if (axisC == null)
            {
                return shrFail;
            }

            short shrResult = Base_.SR_set_io_mode(axisC.CardNum, 0, 0);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }

            return shrResult;  
        }

    
        /// <summary>
        /// 设定正负方向限位输入nLMT信号的模式
        /// </summary>
        /// <param name="axisC"></param>
        /// <returns></returns>
        public static short set_limit_mode(AxisConfig axisC)
        {
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            short ret = 0;
            if (axisC == null)
            {
                return shrFail;
            }
            short shrResult = Base_.SR_set_limit_mode(axisC.CardNum, axisC.AxisNum, axisC.tag_IoLimtPEnable, axisC.tag_IoLimtNEnable, axisC.tag_IoLimtPNHighEnable);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrResult;
        }

        /// <summary>
        /// 设定正负方向限位输入nLMT信号的模式
        /// </summary>
        /// <param name="axisC"></param>
        /// <returns></returns>
        public static short set_pulse_mode(AxisConfig axisC)
        {
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            short ret = 0;
            if (axisC == null)
            {
                return shrFail;
            }
            short shrResult =(short) Base_.SR_set_pulse_mode((int)axisC.CardNum, (int)axisC.AxisNum, axisC.tag_CC_value, axisC.tag_CC_logic, axisC.tag_dir_logic);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrResult;
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
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            short ret = 0;
            if (axisC == null)
            {
                return shrFail;
            }
            short shrResult = -1;

            switch(axisC.GoHomeType)
            {
                case 0:
                    shrResult = Base_.SR_GoHome(axisC);
                    break;
                case 1:
                    shrResult = Base_.SR_GoHome(axisC);
                    break;
                case 2:
                    shrResult = Base_.SR_GoOneHome(axisC);
                    break;

                case 3:
                    shrResult = Base_.SR_GoOneHomeOrg(axisC);
                    break;
                default:
                    shrResult = Base_.SR_GoHome(axisC);
                    break;

            }          
           
            
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrResult;
        }
        /// <summary>
        /// 伺服电机回原函数
        /// </summary>
        /// <param name="stationName"></param>
        /// <param name="axisName"></param>
        /// <returns></returns>
        public static short GoHome(AxisConfig axisC)
        {
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            short ret = 0;
            if (axisC == null)
            {
                return shrFail;
            }
            short shrResult = -1;
            switch (axisC.GoHomeType)
            {
                case 0:
                    shrResult = Base_.SR_GoHome(axisC);
                    break;
                case 1:
                    shrResult = Base_.SR_GoHome(axisC);
                    break;
                case 2:
                    shrResult = Base_.SR_GoOneHome(axisC);
                    break;

                case 3:
                    shrResult = Base_.SR_GoOneHomeOrg(axisC);
                    break;
                default:
                    shrResult = Base_.SR_GoHome(axisC);
                    break;

            }          
            
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            return shrResult;
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
            if (ioName == null)
            {
                status = true;
                return 0;
            }
            IOParameter ioP = StationManage.FindInputIo( ioName);
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)ioP.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)ioP.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            if (ioP == null)
            {
                MessageBoxLog.Show(stationName +"\r\nIO:<" + ioName + ">没有找到，请配置");
                return shrFail;
            }

            shrResult = Base_.SR_GetInputBit(ioP.CardNum, ioP.IOBit, out status);
            if (ioP.Logic == 0)
            {
                if (status)
                    status = false;
                else
                {
                    status = true;
                }          
            }
            if (shrResult 
                == -1)
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
        public static short WaitAxisStop(AxisConfig axisC)
        {
            int intAxisStatus = 0;
            short shrResult =0;

            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            short ret = 0;
            do
            {
                //程序是否正常运行
                if (IsExit())
                {
                    Base_.SR_AxisEmgStop(axisC.CardNum, axisC.AxisNum);
                    return shrFail;
                }
                //读轴状态
                shrResult = Base_.SR_GetAxisStatus(axisC.CardNum, axisC.AxisNum, out intAxisStatus);
                if (shrResult != shrGtsSuccess)
                {
                    return shrFail;
                }
                Thread.Sleep(5);
            } while (intAxisStatus != 0);  //运动中         
            return shrSuccess;
        }

        /// <summary>
        /// 读取板卡规划位置
        /// </summary>
        /// <param name="card"></param>
        /// <param name="axis"></param>
        /// <param name="pos"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static  short SR_GetPrfPos(int Cardtype, short card, short axis, ref double pos)
        {
            short shrResult;
            int posV = 0;
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[Cardtype];
            shrResult = Base_.SR_GetPrfPos(card, axis, ref pos);
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)Cardtype) + "控制卡初始化失败!");
                return shrFail;
            }
            if (shrResult != shrGtsSuccess)
            {
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
        public static short SR_AxisStop(int Cardtype, short card, short axis)
        {
            short shrResult;
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[Cardtype];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)Cardtype) + "控制卡初始化失败!");
                return shrFail;
            }
            shrResult = Base_.SR_AxisStop(card, axis);
            if (shrResult != shrGtsSuccess)
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
        public static short SR_AxisEmgStop(int Cardtype, short card, short axis)
        {
            short shrResult;
            int posV = 0;
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[Cardtype];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)Cardtype) + "控制卡初始化失败!");
                return shrFail;
            }
            shrResult = Base_.SR_AxisEmgStop(card, axis);
            if (shrResult != shrGtsSuccess)
            {
                return -1;
            }
            return 0;
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
            if (io == null)
            {
                return shrFail;
            }
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)io.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)io.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            shrResult = Base_.SR_SetOutputBit(io.CardNum, io.IOBit, ioValue);
            if (shrResult != shrSuccess)
            {
                return shrFail;
            }
            
            return shrSuccess;
        }

        /// <summary>
        /// IO输出
        /// </summary>
        /// <param name="io">IO类对象</param>
        /// <param name="ioValue">输出状态</param>
        /// <returns></returns>
        public static short GetOutputIoBit(IOParameter io, short ioValue)
        {
            short shrResult = 0;
            bool bioValue = false;
            if (io == null)
            {
                return shrFail;
            }
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)io.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)io.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            shrResult = Base_.SR_GetOutputBit(io.CardNum, io.IOBit,out bioValue);
            if (shrResult != shrSuccess)
            {
                shrResult = -1;
                return shrResult;
            }
            if (bioValue)
                shrResult = 1;
            else
            {
                shrResult = 0;
            }
            return shrResult;
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
               
                return shrFail;
            }
            AxisConfig axisC = StationManage.FindAxis(stationM, axisName);
            if (axisC == null)
            {
            
                return shrFail;
            }
            PointAggregate pointA = StationManage.FindPoint(stationM, pointName);
            if (pointA == null)
            {
              
                return shrFail;
            }
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            short shrResult = 0;
            if (pointA.tag_motionType == 0)
            {
                shrResult = Base_.SR_AbsoluteMove(axisC,  pointA.arrPoint[asxisIndex]);
            }
            else
            {
                shrResult = Base_.SR_RelativeMove(axisC,  pointA.arrPoint[asxisIndex]);

            }
            if (shrResult != shrSuccess)
            {
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
        public static short SR_AbsoluteMove(AxisConfig axisC,  PointModule point)
        {
            short shrResult = 0;
            if (point.dblPonitSpeed == 0)
            {
                MessageBoxLog.Show(axisC.AxisName + "速度设置位0,急停请设置");
                return -1;
            }
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            shrResult = Base_.SR_AbsoluteMove(axisC, point);
            if (shrResult != shrGtsSuccess)
            {
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
        public static short SR_continue_move(AxisConfig axisC, PointModule point, int dir)
        {
            short shrResult = 0;
            if (point.dblPonitSpeed == 0)
            {
                MessageBoxLog.Show(axisC.AxisName + "速度设置位0,急停请设置");
                return -1;
            }
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return shrFail;
            }
            shrResult = Base_.SR_continue_move(axisC, point, dir);
            if (shrResult != shrGtsSuccess)
            {
                return shrFail;
            }
            return shrSuccess;
        }
        /// <summary>
        /// 获取轴运动状态 1，运动，0 飞运动 -1异常
        /// </summary>
        /// <param name="axisC"></param>
        /// <returns></returns>
        public static short SR_GetAxisStatus(AxisConfig axisC)
        {
            short shrResult = 0;
            int axisStatus = 0;
            NewCtrlCardBase Base_ = tag_NewCtrlCardBase[(int)axisC.tag_MotionCardManufacturer];
            if (Base_ == null)
            {
                MessageBoxLog.Show(NewCtrlCardBase.GetManufacturerName((int)axisC.tag_MotionCardManufacturer) + "控制卡初始化失败!");
                return -1;
            }
            shrResult = Base_.SR_GetAxisStatus(axisC.CardNum, axisC.AxisNum, out axisStatus);
            if (shrResult == 1)
            {
                return -1;
            }
            return (short)axisStatus;
        }
      
    }
}
