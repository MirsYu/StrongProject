using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading; 
using System.Windows.Forms;
namespace StrongProject
{public  class LeftStation:workBase
{	/// <summary>
	///  work
	/// </summary>
 	public Work tag_Work;
	/// <summary>
	/// 线程
	/// </summary>
	public Thread tag_workThread;
	/// <summary>
	
	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="ConstructCreate"></param>
	/// <returns></returns>
	public LeftStation(Work _Work) 
 	{
 		tag_Work = _Work;
	   tag_stationName ="左工位";
	  tag_isRestStation =0;
	 }
	/// <summary>
	/// 启动函数，主要是线程启动
	/// </summary>
	/// <param name="start"></param>
	/// <returns></returns>
	public bool StartThread()
	{
		if (tag_workThread != null)
		{
			tag_workThread.Abort();
		}
		tag_workThread = new Thread(new ParameterizedThreadStart(workThread));
		tag_manual.tag_stepName = 0;
		tag_workThread.Start();
		tag_workThread.IsBackground = true;
		return true;
	}
	/// <summary>
	/// 退出函数，调用本函数，流程推出
	/// </summary>
	/// <param name="exit"></param>
	/// <returns></returns>
	public bool exit()
	{
		tag_manual.tag_stepName = 100000;
		tag_isWork = 0;
		return true;
	}
	/// <summary>
	/// 工位开始，第0步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step0"></param>
	/// <returns></returns>
	public short Step0(object o)
	{

        if (Global.WorkVar.bemptyRun)
        {
            tag_manual.tag_stepName = 1;
        }
		return 0;
	}
	/// <summary>
	/// 等待左工位启动信号，第1步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step1"></param>
	/// <returns></returns>
	public short Step1(object o)
	{

		return 0;
	}
	/// <summary>
	/// 顶升气缸置位，第2步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step2"></param>
	/// <returns></returns>
	public short Step2(object o)
	{
        tag_isWork = 1;
        while (true)
        {
            if (tag_Work.tag_workObjectManage.tag_RightStation.tag_isWork == 0)
            {
                Thread.Sleep(1000);

                if (tag_Work.tag_workObjectManage.tag_RightStation.tag_isWork == 0)
                {
                    tag_isWork = 1;
                    return 0;
                }
            }
            if (IsExit())
            {
                return 1;
            }
        }
        
        return 0;
	}
	/// <summary>
	/// 伸出气缸置位，第3步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step3"></param>
	/// <returns></returns>
	public short Step3(object o)
	{

		return 0;
	}
	/// <summary>
	/// Y1轴运到到拍照起始位，第4步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step4"></param>
	/// <returns></returns>
	public short Step4(object o)
	{
        while (true)
        {
            if (tag_Work.tag_workObjectManage.tag_RightStation.tag_isWork == 0)
            {
                break;
            }
            if (IsExit())
                return 1;
        }
		return 0;
	}
	/// <summary>
	/// 激光开始，第5步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step5"></param>
	/// <returns></returns>
	public short Step5(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第一个点位，第6步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step6"></param>
	/// <returns></returns>
	public short Step6(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第一个焊接开启，第7步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step7"></param>
	/// <returns></returns>
	public short Step7(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第一个焊接关闭，第8步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step8"></param>
	/// <returns></returns>
	public short Step8(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第二个点位，第9步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step9"></param>
	/// <returns></returns>
	public short Step9(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第二个焊接，第10步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step10"></param>
	/// <returns></returns>
	public short Step10(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第三个点位，第11步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step11"></param>
	/// <returns></returns>
	public short Step11(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第三个焊接，第12步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step12"></param>
	/// <returns></returns>
	public short Step12(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第四个点位，第13步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step13"></param>
	/// <returns></returns>
	public short Step13(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第四个焊接，第14步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step14"></param>
	/// <returns></returns>
	public short Step14(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第五个点位，第15步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step15"></param>
	/// <returns></returns>
	public short Step15(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第五个焊接，第16步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step16"></param>
	/// <returns></returns>
	public short Step16(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第六个点位，第17步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step17"></param>
	/// <returns></returns>
	public short Step17(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第六个焊接，第18步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step18"></param>
	/// <returns></returns>
	public short Step18(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第七个点位，第19步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step19"></param>
	/// <returns></returns>
	public short Step19(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第七个焊接，第20步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step20"></param>
	/// <returns></returns>
	public short Step20(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第八个点位，第21步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step21"></param>
	/// <returns></returns>
	public short Step21(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光第八个焊接，第22步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step22"></param>
	/// <returns></returns>
	public short Step22(object o)
	{

		return 0;
	}
	/// <summary>
	/// Y1轴运动到下料点位，第23步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step23"></param>
	/// <returns></returns>
	public short Step23(object o)
	{
        tag_isWork = 0;
		return 0;
	}
	/// <summary>
	/// 伸出气缸复位，第24步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step24"></param>
	/// <returns></returns>
	public short Step24(object o)
	{

		return 0;
	}
	/// <summary>
	/// 顶升气缸复位，第25步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step25"></param>
	/// <returns></returns>
	public short Step25(object o)
	{

		return 0;
	}
	/// <summary>
	/// 工位结束，第26步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step26"></param>
	/// <returns></returns>
	public short Step26(object o)
	{
        tag_manual.tag_stepName = -1;
        tag_isWork = 0;
        Thread.Sleep(2000);
		return 0;
	}
	/// <summary>
	/// 初始化函数，初始化流程嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="init"></param>
	/// <returns></returns>
	public short Init()
	{
		PointAggregate _Step0 = pointMotion.FindPoint(tag_stationName, "工位开始",0);
		if (_Step0  == null)
		{
			return -1;
		}
		_Step0 .tag_BeginFun = Step0 ;
		PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "等待左工位启动信号",1);
		if (_Step1  == null)
		{
			return -1;
		}
		_Step1 .tag_BeginFun = Step1 ;
		PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "顶升气缸置位",2);
		if (_Step2  == null)
		{
			return -1;
		}
		_Step2 .tag_BeginFun = Step2 ;
		PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "伸出气缸置位",3);
		if (_Step3  == null)
		{
			return -1;
		}
		_Step3 .tag_BeginFun = Step3 ;
		PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "Y1轴运到到拍照起始位",4);
		if (_Step4  == null)
		{
			return -1;
		}
		_Step4 .tag_BeginFun = Step4 ;
		PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "激光开始",5);
		if (_Step5  == null)
		{
			return -1;
		}
		_Step5 .tag_BeginFun = Step5 ;
		PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "激光第一个点位",6);
		if (_Step6  == null)
		{
			return -1;
		}
		_Step6 .tag_BeginFun = Step6 ;
		PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "激光第一个焊接开启",7);
		if (_Step7  == null)
		{
			return -1;
		}
		_Step7 .tag_BeginFun = Step7 ;
		PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "激光第一个焊接关闭",8);
		if (_Step8  == null)
		{
			return -1;
		}
		_Step8 .tag_BeginFun = Step8 ;
		PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "激光第二个点位",9);
		if (_Step9  == null)
		{
			return -1;
		}
		_Step9 .tag_BeginFun = Step9 ;
		PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "激光第二个焊接",10);
		if (_Step10  == null)
		{
			return -1;
		}
		_Step10 .tag_BeginFun = Step10 ;
		PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "激光第三个点位",11);
		if (_Step11  == null)
		{
			return -1;
		}
		_Step11 .tag_BeginFun = Step11 ;
		PointAggregate _Step12 = pointMotion.FindPoint(tag_stationName, "激光第三个焊接",12);
		if (_Step12  == null)
		{
			return -1;
		}
		_Step12 .tag_BeginFun = Step12 ;
		PointAggregate _Step13 = pointMotion.FindPoint(tag_stationName, "激光第四个点位",13);
		if (_Step13  == null)
		{
			return -1;
		}
		_Step13 .tag_BeginFun = Step13 ;
		PointAggregate _Step14 = pointMotion.FindPoint(tag_stationName, "激光第四个焊接",14);
		if (_Step14  == null)
		{
			return -1;
		}
		_Step14 .tag_BeginFun = Step14 ;
		PointAggregate _Step15 = pointMotion.FindPoint(tag_stationName, "激光第五个点位",15);
		if (_Step15  == null)
		{
			return -1;
		}
		_Step15 .tag_BeginFun = Step15 ;
		PointAggregate _Step16 = pointMotion.FindPoint(tag_stationName, "激光第五个焊接",16);
		if (_Step16  == null)
		{
			return -1;
		}
		_Step16 .tag_BeginFun = Step16 ;
		PointAggregate _Step17 = pointMotion.FindPoint(tag_stationName, "激光第六个点位",17);
		if (_Step17  == null)
		{
			return -1;
		}
		_Step17 .tag_BeginFun = Step17 ;
		PointAggregate _Step18 = pointMotion.FindPoint(tag_stationName, "激光第六个焊接",18);
		if (_Step18  == null)
		{
			return -1;
		}
		_Step18 .tag_BeginFun = Step18 ;
		PointAggregate _Step19 = pointMotion.FindPoint(tag_stationName, "激光第七个点位",19);
		if (_Step19  == null)
		{
			return -1;
		}
		_Step19 .tag_BeginFun = Step19 ;
		PointAggregate _Step20 = pointMotion.FindPoint(tag_stationName, "激光第七个焊接",20);
		if (_Step20  == null)
		{
			return -1;
		}
		_Step20 .tag_BeginFun = Step20 ;
		PointAggregate _Step21 = pointMotion.FindPoint(tag_stationName, "激光第八个点位",21);
		if (_Step21  == null)
		{
			return -1;
		}
		_Step21 .tag_BeginFun = Step21 ;
		PointAggregate _Step22 = pointMotion.FindPoint(tag_stationName, "激光第八个焊接",22);
		if (_Step22  == null)
		{
			return -1;
		}
		_Step22 .tag_BeginFun = Step22 ;
		PointAggregate _Step23 = pointMotion.FindPoint(tag_stationName, "Y1轴运动到下料点位",23);
		if (_Step23  == null)
		{
			return -1;
		}
		_Step23 .tag_BeginFun = Step23 ;
		PointAggregate _Step24 = pointMotion.FindPoint(tag_stationName, "伸出气缸复位",24);
		if (_Step24  == null)
		{
			return -1;
		}
		_Step24 .tag_BeginFun = Step24 ;
		PointAggregate _Step25 = pointMotion.FindPoint(tag_stationName, "顶升气缸复位",25);
		if (_Step25  == null)
		{
			return -1;
		}
		_Step25 .tag_BeginFun = Step25 ;
		PointAggregate _Step26 = pointMotion.FindPoint(tag_stationName, "工位结束",26);
		if (_Step26  == null)
		{
			return -1;
		}
		_Step26 .tag_BeginFun = Step26 ;
		return 0;
	}
	/// <summary>
	/// 中断函数
	/// </summary>
	/// <param name="Suspend"></param>
	/// <returns></returns>
	public short Suspend(object o)
	{

		return 0;
	}
	/// <summary>
	/// 继续函数
	/// </summary>
	/// <param name="Continue"></param>
	/// <returns></returns>
	public short Continue(object o)
	{

		return 0;
	}
	/// <summary>
	/// 流程执行的函数 返回0 表示成功
	/// </summary>
	/// <param name="work"></param>
	/// <returns></returns>
	public short Start(object o)
	{
		short ret = 0;
		if (Init() == 0)
		{
		if (tag_manual.tag_step == 0)
		{
		ret = pointMotion.StationRun(tag_stationName, tag_manual);
 		tag_manual.tag_stepName = 0;
		}
		tag_isWork = -1;
		return ret;
		}
		else
		{
		return -1;
		}
		return  0;
	}
	/// <summary>
	/// 流程用线程执行执行的函数 无返回值
	/// </summary>
	/// <param name="workThreadCreate"></param>
	/// <returns></returns>
	public void workThread(object o)
	{	
		short ret = 0;
		if (Init() == 0)
		{
			if (tag_manual.tag_step == 0)
			{
				tag_isWork = pointMotion.StationRun(tag_stationName, tag_manual);
		tag_manual.tag_stepName = 0;
			}
		}
	}
}
}
