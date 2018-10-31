using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Windows.Forms;
using System.Reflection;
using StrongProject;
namespace StrongProject
{
/// <summary>
/// 手动调试参数
/// </summary>
public class manual
{
        /// <summary>
        /// 屏蔽作用
        /// </summary>
        public bool tag_induction;
        /// <summary>
        /// 单步运行，
        /// </summary>
        public int tag_stepName;
        /// <summary>
        /// 单步调试开关
        /// </summary>
        public int tag_step;
        /// <summary>
        /// 是否暂停
        /// </summary>
        public bool tag_isSuspend;
       /// <summary>
       /// 暂停函数
       /// </summary>
        public delegate_PointModule tag_SuspendFun;
        /// <summary>
        /// 暂停继续函数
        /// </summary>
        public delegate_PointModule tag_ContinueFun;

       
    
        /// <summary>
        /// 构造函数，初始化的时候，表示从第几步开始 
        /// </summary>
        /// <param name="ndStep"></param>
        public manual(int ndStep)
        {
            tag_stepName = ndStep;
        }

}
public class Work
{
    /*
    * 点位信息，配置
    * 
    */
    public Config _Config;
    /// <summary>
    /// 复位工位
    /// </summary>
    public Rest tag_Rest;
    /// <summary>
    /// 工位管理，主要用于添加，初始化 
    /// </summary>
    public workObjectManage tag_workObjectManage;
    /// <summary>
    /// 工作工位列表，集中管理
    /// </summary>
    public List<object> tag_workObject = new List<object>();

    public List<JSerialPort> tag_JSerialPort = new List<JSerialPort>();

    public List<SocketClient> tag_SocketClient = new List<SocketClient>();
    /// <summary>
    /// 整个工作流程
    /// </summary>
    public Work()
    {
        _Config = Config.Load() as Config;

        NewCtrlCardSR.blnRun = true;
        StationManage._Config = _Config;
 
        foreach(PortParameter pp in _Config.tag_PortParameterList)
        {
            tag_JSerialPort.Add(new JSerialPort(null,pp));
        }
       
        foreach (IPAdrr pp in _Config.tag_IPAdrrList)
        {
            tag_SocketClient.Add(new SocketClient(pp));
        }
      
        tag_workObjectManage = new workObjectManage(this,tag_workObject);
        Init();
        IoCheckThreadStart();
        return;
    }
    /// <summary>
    /// 急停所有
    /// </summary>
    public void Stop()
    {
        StationManage.StopAllAxis();
        Global.WorkVar.tag_StopState = 1;
        Global.WorkVar.tag_SuspendState = 0;
        Global.WorkVar.tag_ResetState = 0;
        Global.WorkVar.tag_workState = 0;
    }
    /// <summary>
    /// 暂停
    /// </summary>
    /// <param name="o1"></param>
    /// <returns></returns>
    public short Suspend(object o1) 
    {
        
        Global.WorkVar.tag_SuspendState = 1;
        object[] po = new object[1];
        po[0] = o1;
        foreach (object o in tag_workObject)
        {
            Type t = o.GetType();
            System.Reflection.MethodInfo[] methods = t.GetMethods();
            System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
            System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].Name == "Suspend")
                {
                    methods[i].Invoke(o, po);
                }
            }
        }
        return 0;
    }
    /// <summary>
    /// 继续函数
    /// </summary>
    /// <param name="o"></param>
    /// <returns></returns>
    public short Continue(object o1)
    {
        if (Global.WorkVar.tag_ResetState == 0)
        {
            UserControl_LogOut.OutLog("没有复位，请复位",0);
            return -1;
        }
        if (Global.WorkVar.tag_SuspendState == 1)
        {

            object[] po = new object[1];
            po[0] = o1;
            foreach (object o in tag_workObject)
            {
                Type t = o.GetType();
                System.Reflection.MethodInfo[] methods = t.GetMethods();
                System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
                System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
                for (int i = 0; i < methods.Length; i++)
                {
                    if (methods[i].Name == "Continue")
                    {
                        methods[i].Invoke(o, po);
                    }
                }
            }
        }
        Global.WorkVar.tag_SuspendState = 0;
        return 0;
    }
    /// <summary>
    /// 工位初始化
    /// </summary>
    /// <returns></returns>
    public short Init()
    {
        foreach (object o in tag_workObject)
        {
            if (o == null)
                return 1;
            Type t =   o.GetType();
            System.Reflection.MethodInfo[] methods = t.GetMethods();
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].Name == "Init")
                {
                    methods[i].Invoke(o, null);
                }
            }
        }
        return 0;
    }
    public bool IsWork()
    {
        foreach (object o in tag_workObject)
        {

            Type t = o.GetType();
            System.Reflection.MethodInfo[] methods = t.GetMethods();
            System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
            System.Reflection.MemberInfo[] MemberInfos = t.GetMembers();
            workBase wb = (workBase)o;
            if (wb.tag_isWork == 1)
            {
                UserControl_LogOut.OutLog(wb.tag_stationName + "：工位正在工作",0);
                return true;
            }
        }
        return false;
    }
    /// <summary>
    ///启动列表
    /// </summary>
    public void startList()
    {
        foreach(object o in tag_workObject)
        {
          
            Type t = o.GetType();
            System.Reflection.MethodInfo[] methods = t.GetMethods();
            System.Reflection.PropertyInfo[] PropertyInfo = t.GetProperties();
            System.Reflection.MemberInfo[] MemberInfos =  t.GetMembers();
            workBase wb = (workBase)o;
            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].Name == "StartThread")
                {
                    if (wb.tag_isRestStation == 0)
                    {
                        wb.tag_isWork = 0;
                        wb.tag_manual.tag_stepName = 0;
                        methods[i].Invoke(o, null);
                    }
                }

            }
        }
    }
    /// <summary>
    /// 开始启动
    /// </summary>
    public bool start()
    {
        if (Global.WorkVar.tag_ResetState == 1)
        {
            UserControl_LogOut.OutLog("复位中，请等待", 0);
            return false;
        }
      if (Global.WorkVar.tag_ResetState == 0)
        {
            UserControl_LogOut.OutLog("没有复位，请复位", 0);
            return false;
        }
       
        if (Global.WorkVar.tag_IsExit == 1)
        {
            UserControl_LogOut.OutLog("请到主界面按启动键", 0);
            return false;
        }
        if (Global.WorkVar.tag_StopState == 1)
        {
      
            UserControl_LogOut.OutLog("急停中",0);
            return false;
        }
        if (IsWork())
        {
            UserControl_LogOut.OutLog("当前有工位正在工作，请停止正在工作的工位", 0);
            return false;
        }
        if (Global.WorkVar.tag_workState == 0)
        {
            Global.WorkVar.tag_workState = 1;
            startList();
            return true;
        }
        else
        {
            if (Global.WorkVar.tag_ResetState == 1)
            {
                UserControl_LogOut.OutLog("工作中", 0);
                
            }
        }
        return false;
    }
    public static bool IsMove()
    {
        if (Global.WorkVar.tag_ResetState == 1)
        {

            MessageBoxLog.Show("复位中，请等待");
            return false;
        }
        if (Global.WorkVar.tag_ResetState == 0)
        {
            MessageBoxLog.Show("没有复位，请复位");
            return false;
        }


        if (Global.WorkVar.tag_StopState == 1)
        {
            MessageBoxLog.Show("急停中");
            return false;
        }
        if (Global.WorkVar.tag_workState == 1)
        {
            MessageBoxLog.Show("工作中，请调为自动状态");
        }
        return true;
    }
    /// <summary>
    /// 复位 动作
    /// </summary>
    public void Rest()
    {

        if (tag_Rest == null)
        {
            tag_Rest = new Rest(this);
        }
        tag_Rest.start();
    }          
    public void CheckIoThread(object o)
    {
        while (true)
        {
            bool suspendIo = false;
            bool stopIoS = false;
            bool RestIoS = false;
            bool AxisAlarm = false;
            bool AxisLimitAlarm = false;
            
            IOParameter SuspendIo = StationManage.FindInputIo("暂停");
            IOParameter StopIo = StationManage.FindInputIo("急停");
            IOParameter RestIo = StationManage.FindInputIo("复位");

           AxisAlarm =  NewCtrlCardSR.IsAxisAlarm();
           AxisLimitAlarm = NewCtrlCardSR.IsAxisLimitAlarm();
            if (SuspendIo == null)
            {
                UserControl_LogOut.OutLog("请配置<暂停IO>", 0);
                Thread.Sleep(100);
                continue;
               
            }
            if (StopIo == null)
            {
                UserControl_LogOut.OutLog("请配置<急停IO>", 0);
                Thread.Sleep(100);
                continue;
              
            }
            if (RestIo == null)
            {
                UserControl_LogOut.OutLog("请配置<复位IO>", 0);
                Thread.Sleep(100);
                continue;
               
            }

            NewCtrlCardSR.GetInputIoBitStatus(StationManage.FindInputIo("暂停"),out suspendIo);
            NewCtrlCardSR.GetInputIoBitStatus(StationManage.FindInputIo("复位"), out RestIoS);
            NewCtrlCardSR.GetInputIoBitStatus(StationManage.FindInputIo("急停"), out stopIoS);
           
         //   NewCtrlCardSR.GetInputIoBitStatus(StationManage.FindInputIo("启动"), out LeftIoS);
            if (!stopIoS || Global.WorkVar.tag_StopState == 2 || AxisAlarm || (AxisLimitAlarm && Global.WorkVar.tag_workState == 1) )
            {
                Stop();
                Thread.Sleep(100);
                continue;
            }
            else
                if (suspendIo && Global.WorkVar.tag_SuspendState  == 0)
                {
                    Suspend(null);
                    Thread.Sleep(500);
                }
            else
               if (RestIoS)
                {
                    Global.WorkVar.tag_StopState = 0; 
                    if (Global.WorkVar.tag_ResetState != 1 && Global.WorkVar.tag_workState == 0)
                    {
                        Rest();
                    }
                    else
                    {
                  
                    }
                    continue;
                }
                if (suspendIo && Global.WorkVar.tag_SuspendState  == 1)
                {
                     Continue(null);
                     Thread.Sleep(500);
                     continue;
                }
                if ( Global.WorkVar.bcanRunFalg == true && Global.WorkVar.tag_ResetState == 2 && Global.WorkVar.tag_workState == 0)
                {
                  Global.WorkVar.bcanRunFalg = false;
                  if (start())
                  {
                      Global.WorkVar.bcanRunFalg = true;
                  }
                  continue;
                }
            Thread.Sleep(100);
        }
    }  
    /// <summary>
    /// io检查，
    /// </summary>
    public void IoCheckThreadStart()
    {
        Thread tag_IoCheckThread = new Thread(new ParameterizedThreadStart(CheckIoThread));
        tag_IoCheckThread.IsBackground = true;
        tag_IoCheckThread.Start(null);
    }
    /// <summary>
    /// 
    /// </summary>
    public Config Config
    {
        get { return _Config; }
        set { _Config = value; }
    }
 
}
    
}
