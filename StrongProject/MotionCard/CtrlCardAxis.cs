using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
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
using StrongProject;
namespace StrongProject
{
	public class CtrlCardAxis : CardBase
	{
		public static CAxisParams[] AxisParams;//下标从0开始
		static CtrlCardAxis()
		{
			AxisParams=CtrlCardSR.AxisParams;
			//AxisParams = new CAxisParams[BCARD.AXIS_QTY_ALL];
			//for (int i = 0; i <BCARD. AXIS_QTY_ALL ; i++)
			//{
			//    AxisParams[i] = new CAxisParams((TCard)(i / 8), (TAxis)(i % 8 + 1));
			//}
		}
		//轴是否禁止运动
		private static  bool IsAxisForbidmove(CAxisParams axis)
		{
            if (Global.WorkVar.ProgramEmg )
            {
                return true;
            }
			return false ;
		}
		//单轴回原
		public static TReturn AxisHome(CAxisParams axis)
		{
			if (axis == null ||IsAxisForbidmove(axis))
				return RET_FAIL;
            return CtrlCard.AxisHome(axis.CardNo ,axis.AxisNo );
            //return CtrlCardSR.SR_GoHome(axis.CardNo ,axis.AxisNo );
		}
		//单轴回原
		public static TReturn AxissHome(params  CAxisParams[] axiss)
		{
			TReturn ret=RET_SUCCESS;
			TReturn retTemp=RET_SUCCESS;
			foreach (CAxisParams   axis in axiss)
			{
				if (axis == null ||IsAxisForbidmove(axis))
				{
					return  RET_FAIL ;
				}
 				retTemp= CtrlCard.AxisHome(axis.CardNo ,axis.AxisNo );	
				if(retTemp==RET_FAIL) ret=retTemp ;
			}
            Thread.Sleep(100);
			return  ret ;

		}
		//轴当前是否停止运动
		public static bool IsAxisStop(CAxisParams axis)
		{
			if( CtrlCardSR.SR_IsAxisStop(axis.CardNo, axis.AxisNo)==CtrlCardSR.RET_SUCCESS)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
        //public static bool AxisWaitStop(CAxisParams axis)
        //{
        //    while (true)
        //    {
        //        if (CtrlCardSR.SR_IsAxisStop(axis.CardNo, axis.AxisNo) == CtrlCardSR.RET_SUCCESS)
        //        {
        //            return true;
        //        }
        //        Thread.Sleep(10);
        //    }
        //}
        public static bool AxissWaitStop(params  CAxisParams [] axiss)
        {
            bool stopFlag = true ;
            while (true)
            {
                stopFlag = true;
                foreach (CAxisParams  axis in axiss )
                {
                    if (CtrlCardSR.SR_IsAxisStop(axis.CardNo, axis.AxisNo) != CtrlCardSR.RET_SUCCESS )
                    {
                        stopFlag = false; 

                    }                    
                }
                if (stopFlag)
                {
                    return true; 
                }
                Thread.Sleep(10);
            }
        }

		//单轴停止
		public static TReturn StopAxis(CAxisParams axis)
		{
			if(axis==null) return RET_FAIL;
			if(cardNoActive()) return RET_SUCCESS;
			return CtrlCardSR.SR_AxisStop(axis.CardNo, axis.AxisNo);				
		}
		//停止指定的轴
		public static TReturn StopAxiss(bool bWaitStop,params  CAxisParams [] axiss )
		{
			//bWaitStop true时等待轴停止，false时不等待轴停止
			TReturn retTemp=RET_SUCCESS ;
			TReturn ret=RET_SUCCESS ;
			if(axiss==null) return RET_FAIL;
			foreach (CAxisParams   axis in axiss)
			{
				if (axis == null)
				{
					ret =RET_FAIL ;
					continue ;
				}
				retTemp = CtrlCardSR.SR_AxisStopNoStop (axis.CardNo, axis.AxisNo);					
				if(retTemp==RET_FAIL ) ret=retTemp ;
			}
			if(bWaitStop)
			{
				foreach (CAxisParams   axis in axiss)
				{
					if (axis == null)
					{
						ret =RET_FAIL ;
						continue ;
					}
					retTemp = CtrlCardSR.SR_AxisStop  (axis.CardNo, axis.AxisNo);					
					if(retTemp==RET_FAIL ) ret=retTemp ;
				}				
			}
			return ret;
 
		}
		//卡功能是否已屏蔽
		private static bool cardNoActive()
		{
            return Global.WorkVar.ConnectCard ? false : true;
		}
		//停止指定的轴
		public static TReturn StopAll(  )
		{ 
			TReturn ret=RET_SUCCESS ;
			if (cardNoActive())
				return ret;
			CtrlCardSR.SR_AllAxisStopNoWait(BCARD.CARD_NUM_0);
			CtrlCardSR.SR_AllAxisStopNoWait(BCARD.CARD_NUM_1);
			CtrlCardSR.SR_AllAxisStopNoWait(BCARD.CARD_NUM_2);	
			CtrlCardSR.SR_AllAxisStop (BCARD.CARD_NUM_0);
			CtrlCardSR.SR_AllAxisStop(BCARD.CARD_NUM_1);
			CtrlCardSR.SR_AllAxisStop(BCARD.CARD_NUM_2);						
			return ret;
 
		}


		//连续运动
		public static TReturn AxisContinueMove(CAxisParams axis, TSpeed spd, TMode dir)
		{

			if (axis == null || IsAxisForbidmove(axis))
				return RET_FAIL;
            //spd = axis.mmV_to_pulseV(spd);
            //return CtrlCard.AxisContinueMoveWithLimit(axis.CardNo, axis.AxisNo, spd, dir);
            return CtrlCardSR.SR_ContinueMove(axis.CardNo, axis.AxisNo, spd, dir);
		}

		//<轴点动/相对运动>
		public static TReturn AxisPMoveRelative(CAxisParams axis, TPusle dist, TSpeed spd)
		{
			if (axis == null || IsAxisForbidmove(axis))
				return RET_FAIL;

            return CtrlCardSR.SR_RelativeMove(axis.CardNo, axis.AxisNo, dist, spd);
		}
		//<轴点动/相对运动>
		public static TReturn AxisPMoveRelativeToStop(CAxisParams axis, TPusle dist, TSpeed spd)
		{
			if (axis == null || IsAxisForbidmove(axis))
				return RET_FAIL;


            int iResult; 
            iResult = CtrlCardSR.SR_RelativeMove(axis.CardNo, axis.AxisNo, dist, spd);
            if (iResult != RET_SUCCESS)
            {
                return RET_FAIL;
            }
            DateTime axisStartTime = DateTime.Now;
            Thread.Sleep(10);
            while (canRun())
            {
                iResult = CtrlCardSR.SR_IsAxisStop(axis.CardNo, axis.AxisNo);
                if (iResult == RET_SUCCESS)
                {
                    return RET_SUCCESS;
                }
                if ((DateTime.Now - axisStartTime).TotalSeconds > 100)
                {
                    return RET_FAIL;
                }
                Application.DoEvents();
                Thread.Sleep(10);
            }
            return RET_FAIL;
		}
 

		//单轴绝对定位，以及等待运动停止
		public static TReturn AxisPMoveAbsoluteToStop(CAxisParams axis, TPusle  postion, TSpeed  spd, bool waitstop=true,double TimeOut = 100)
		{//success return RET_SUCCESS
			if (axis == null || IsAxisForbidmove(axis))
				return RET_FAIL;

			//fail return RET_FAIL

			int iResult; 
            iResult = CtrlCardSR.SR_AbsoluteMove(axis.CardNo, axis.AxisNo, postion, spd);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}
            if (waitstop == false)//不等停止
            {
                return RET_SUCCESS;
            }
			DateTime axisStartTime = DateTime.Now;
			Thread.Sleep(10);
			while (canRun())
			{
				iResult = CtrlCardSR.SR_IsAxisStop(axis.CardNo, axis.AxisNo);
				if (iResult == RET_SUCCESS)
				{
					return RET_SUCCESS;
				}
				if ((DateTime.Now - axisStartTime).TotalSeconds > TimeOut)
				{
					return RET_FAIL;
				}
                Application.DoEvents();
				Thread.Sleep(10);
			}
			return  RET_FAIL;
		}
		//双轴绝对定位，以及等待运动停止
		public static TReturn AxisPPMoveAbsoluteToStop(CAxisParams axis1, CAxisParams axis2,
			TPusle pos1, TPusle pos2, TSpeed vel1, TSpeed vel2,bool waitstop=true, double TimeOut = 20)
		{//success return RET_SUCCESS
			//超时 return RET_FAIL
			if (axis1 == null||axis2==null )
				return RET_FAIL;
			if (IsAxisForbidmove(axis1) || IsAxisForbidmove(axis2))
				return RET_FAIL;

			int iResult;
			int iResult2;
			iResult = CtrlCardSR.SR_AbsoluteMove(axis1.CardNo, axis1.AxisNo, pos1, vel1);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}
			iResult = CtrlCardSR.SR_AbsoluteMove(axis2.CardNo, axis2.AxisNo, pos2, vel2);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}
            if (waitstop == false)//不等停止
            {
                return RET_SUCCESS;
            }
			DateTime axisStartTime = DateTime.Now;
			Thread.Sleep(10);
			while (canRun())
			{
				iResult = CtrlCardSR.SR_IsAxisStop(axis1.CardNo, axis1.AxisNo);
				iResult2 = CtrlCardSR.SR_IsAxisStop(axis2.CardNo, axis2.AxisNo);
				if (iResult == RET_SUCCESS && iResult2 == RET_SUCCESS)
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

		//三轴绝对定位 
		public static TReturn AxisPPPMoveAbsoluteToStop(CAxisParams axis1, CAxisParams axis2,CAxisParams axis3,
			TPusle pos1, TPusle pos2, TPusle pos3,TSpeed vel1, TSpeed vel2, TSpeed vel3, bool waitstop=true,double TimeOut = 20)
		{//success return RET_SUCCESS
			//超时 return RET_FAIL
			if (axis1 == null||axis2==null ||axis3==null)
				return RET_FAIL;
			if (IsAxisForbidmove(axis1) || IsAxisForbidmove(axis2)|| IsAxisForbidmove(axis3))
				return RET_FAIL;

 
			int iResult;
			int iResult2;
			int iResult3;
			iResult = CtrlCardSR.SR_AbsoluteMove(axis1.CardNo, axis1.AxisNo, pos1, vel1);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}
			iResult = CtrlCardSR.SR_AbsoluteMove(axis2.CardNo, axis2.AxisNo, pos2, vel2);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}
			iResult = CtrlCardSR.SR_AbsoluteMove(axis3.CardNo, axis3.AxisNo, pos3, vel3);
			if (iResult != RET_SUCCESS)
			{
				return RET_FAIL;
			}
            if (waitstop == false)//不等停止
            {
                return RET_SUCCESS;
            }
			DateTime axisStartTime = DateTime.Now;
			Thread.Sleep(100);
			while (canRun())
			{
				iResult = CtrlCardSR.SR_IsAxisStop(axis1.CardNo, axis1.AxisNo);
				iResult2 = CtrlCardSR.SR_IsAxisStop(axis2.CardNo, axis2.AxisNo);
				iResult3 = CtrlCardSR.SR_IsAxisStop(axis3.CardNo, axis3.AxisNo);
				if (iResult == RET_SUCCESS && iResult2 == RET_SUCCESS && iResult3 == RET_SUCCESS)
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



	}


}
