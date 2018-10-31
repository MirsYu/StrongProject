using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrongProject
{
   public class workBase
    {
       /// <summary>
       /// 
       /// </summary>
       public manual tag_manual = new manual(0);
       /// <summary>
       /// 是否工作中 0表示没工作，1表示工作
       /// </summary>
       public int tag_isWork;

       /// <summary>
       /// 判断是否是复位工位,0:不是，1:是
       /// </summary>
       public int tag_isRestStation;//
       /// <summary>
       /// 工位名
       /// </summary>
       public string tag_stationName ;

       public bool IsExit()
       {
           if (Global.WorkVar.tag_StopState == 1)
               return true;
           if (Global.WorkVar.tag_IsExit == 1)
               return true;
           return false;
       }
    }
}
