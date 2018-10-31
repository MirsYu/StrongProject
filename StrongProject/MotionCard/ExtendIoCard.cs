using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StrongProject
{
    class ExtendIoCard
    {       
        private static short shrSuccess = 0;    //二次封装函数成功
        private static short shrFail = -1;      //二次封装函数失败  
        private static short shrFail2 = -2;      //二次封装函数失败
        public static bool blnInputIoChange = false;
        public static short InitCard()
        {
            Int32 cardNum;           
            cardNum = adtIO3224.adtIO3224_initial();
            if (cardNum < 0)
            {
                MessageBoxLog.Show("IO3224没有正确安装");
                return shrFail;
            }
            return shrSuccess;
        }

        public static short GetInputIoBit(short card, short ioBit, out bool bStatus)
        {
            short shrResult = (short)adtIO3224.adtIO3224_read_bit(card, ioBit);
            bStatus = false;
            if (shrResult < shrSuccess)
            {
                CommandResult("adtIO3224_read_bit", shrResult);
                return shrFail;
            }
            if (shrResult == 0)
            {
                bStatus = false;
            }
            else
                bStatus = true;
            return shrSuccess;
        }

        public static short GetIpnutIoAll(short card, out ulong pValue)
        {
            int intValue = 0;
            short shrResult = (short)adtIO3224.adtIO3224_read_in(card, out intValue);
            pValue = 0;
            if (shrResult < shrSuccess)
            {
                CommandResult("adtIO3224_read_in", shrResult);
                return shrFail;
            }
            pValue = (ulong)intValue;
            return shrSuccess;
        }

        public static short SetOutputAll(short card,int value)
        {
            short shrResult = (short)adtIO3224.adtIO3224_write_out(card, out value);

            if (shrResult < shrSuccess)
            {
                CommandResult("adtIO3224_write_bit", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        public static short SetOutputIoBit(short card, short ioBit, short value)
        {
            short shrResult = (short)adtIO3224.adtIO3224_write_bit(card, ioBit,value);
            
            if (shrResult < shrSuccess)
            {
                CommandResult("adtIO3224_write_bit", shrResult);
                return shrFail;
            }
            return shrSuccess;
        }
        public static short GetOutputIoBit(short card, short ioBit, out bool bStatus)
        {
            short shrResult = (short)adtIO3224.adtIO3224_get_out(card, ioBit);
            bStatus = false;
            if (shrResult < shrSuccess)
            {
                CommandResult("adtIO3224_get_out", shrResult);
                return shrFail;
            }
            if (shrResult == 0)
            {
                bStatus = false;
            }
            else
                bStatus = true;
            return shrSuccess;
        }
        public static short GetOutputIoAll(short card, out int value)
        {
            short shrResult = (short)adtIO3224.adtIO3224_read_out((int)card,out value);
            
            if (shrResult < shrSuccess)
            {
                CommandResult("adtIO3224_read_out", shrResult);
                return shrFail;
            }
           
            return shrSuccess;
        }

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
    }
}
