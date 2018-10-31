using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading; 
using System.Windows.Forms;
namespace StrongProject
{public  class leftDingWeiCCD:workBase
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
	/// 
	/// </summary>
	public SocketClient tag_SocketClient1;
    public string tag_sendStr = "";
	/// <summary>
	/// 构造函数
	/// </summary>
	/// <param name="ConstructCreate"></param>
	/// <returns></returns>
	public leftDingWeiCCD(Work _Work) 
 	{
 		tag_Work = _Work;
	 	tag_SocketClient1 =_Work.tag_SocketClient[1];
  tag_stationName ="左CCD_9_定位宫格标定";
	  tag_isRestStation =2;
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
    public short runjg(object o)
    {

        StationModule sm = StationManage.FindStation(tag_stationName);
        PointAggregate jg = StationManage.FindPoint(sm, "激光焊接");
        Thread.Sleep(1000);
        short aret = pointMotion.pointRun(jg, tag_stationName, null);
        Thread.Sleep(1000);
        return aret;
    }
	/// <summary>
	/// 工位开始，第0步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step0"></param>
	/// <returns></returns>
	public short Step0(object o)
	{

		tag_isWork = 1;;
		return 0;
	}
	/// <summary>
	/// 标定开始，第1步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step1"></param>
	/// <returns></returns>
	public short Step1(object o)
	{

		return 0;
	}
	/// <summary>
	/// 起始点，第2步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step2"></param>
	/// <returns></returns>
	public short Step2(object o)
	{

		return 0;
	}
	/// <summary>
	/// 激光焊接，第3步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step3"></param>
	/// <returns></returns>
	public short Step3(object o)
	{

		return 0;
	}
	/// <summary>
	/// 0_0点位，第4步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step4"></param>
	/// <returns></returns>
	public short Step4(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = "";
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
        return 0;
		return 0;
	}
	/// <summary>
	/// 0_1点位，第5步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step5"></param>
	/// <returns></returns>
	public short Step5(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
		return 0;
	}
	/// <summary>
	/// 0_2点位，第6步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step6"></param>
	/// <returns></returns>
	public short Step6(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
	
		return 0;
	}
	/// <summary>
	/// 1_0点位，第7步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step7"></param>
	/// <returns></returns>
	public short Step7(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
	
		return 0;
	}
	/// <summary>
	/// 1_1点位，第8步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step8"></param>
	/// <returns></returns>
	public short Step8(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
	
		return 0;
	}
	/// <summary>
	/// 1_2点位，第9步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step9"></param>
	/// <returns></returns>
	public short Step9(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
	
		return 0;
	}
	/// <summary>
	/// 2_0点位，第10步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step10"></param>
	/// <returns></returns>
	public short Step10(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
	
		return 0;
	}
	/// <summary>
	/// 2_1点位，第11步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step11"></param>
	/// <returns></returns>
	public short Step11(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
	
		return 0;
	}
	/// <summary>
	/// 2_2点位，第12步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step12"></param>
	/// <returns></returns>
	public short Step12(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
	
		return 0;
	}
	/// <summary>
	/// 相机拍照位Z位置，第13步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step13"></param>
	/// <returns></returns>
	public short Step13(object o)
	{
      
		return 0;
	}
	/// <summary>
	/// 相机拍照位，第14步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step14"></param>
	/// <returns></returns>
	public short Step14(object o)
	{
        PointAggregate p = (PointAggregate)o;
        tag_sendStr = tag_sendStr + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + ",";
	
		return 0;
	}
	/// <summary>
	/// 标定结束，第15步嵌入的代码，返回0 表示成功
	/// </summary>
	/// <param name="Step15"></param>
	/// <returns></returns>
	public short Step15(object o)
	{
        tag_SocketClient1.send("TC1," + tag_sendStr + "\r\n", 5000);
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
		PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "标定开始",1);
		if (_Step1  == null)
		{
			return -1;
		}
		_Step1 .tag_BeginFun = Step1 ;
		PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "起始点",2);
		if (_Step2  == null)
		{
			return -1;
		}
		_Step2 .tag_BeginFun = Step2 ;
		PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "激光焊接",3);
		if (_Step3  == null)
		{
			return -1;
		}
		_Step3 .tag_BeginFun = Step3 ;
		PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "0_0点位",4);
		if (_Step4  == null)
		{
			return -1;
		}
		_Step4 .tag_BeginFun = Step4 ;
        _Step4.tag_EndFun = runjg;
		PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "0_1点位",5);
		if (_Step5  == null)
		{
			return -1;
		}
		_Step5 .tag_BeginFun = Step5 ;
        _Step5.tag_EndFun = runjg;
		PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "0_2点位",6);
		if (_Step6  == null)
		{
			return -1;
		}
		_Step6 .tag_BeginFun = Step6 ;
        _Step6.tag_EndFun = runjg;
		PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "1_0点位",7);
		if (_Step7  == null)
		{
			return -1;
		}
		_Step7 .tag_BeginFun = Step7 ;
        _Step7.tag_EndFun = runjg;
		PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "1_1点位",8);
		if (_Step8  == null)
		{
			return -1;
		}
		_Step8 .tag_BeginFun = Step8 ;
        _Step8.tag_EndFun = runjg;
		PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "1_2点位",9);
		if (_Step9  == null)
		{
			return -1;
		}
		_Step9 .tag_BeginFun = Step9 ;
        _Step9.tag_EndFun = runjg;
		PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "2_0点位",10);
		if (_Step10  == null)
		{
			return -1;
		}
		_Step10 .tag_BeginFun = Step10 ;
        _Step10.tag_EndFun = runjg;
		PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "2_1点位",11);
		if (_Step11  == null)
		{
			return -1;
		}
		_Step11 .tag_BeginFun = Step11 ;
        _Step11.tag_EndFun = runjg;
		PointAggregate _Step12 = pointMotion.FindPoint(tag_stationName, "2_2点位",12);
		if (_Step12  == null)
		{
			return -1;
		}
		_Step12 .tag_BeginFun = Step12 ;
        _Step12.tag_EndFun = runjg;
		PointAggregate _Step13 = pointMotion.FindPoint(tag_stationName, "相机拍照位Z位置",13);
		if (_Step13  == null)
		{
			return -1;
		}
		_Step13 .tag_BeginFun = Step13 ;
		PointAggregate _Step14 = pointMotion.FindPoint(tag_stationName, "相机拍照位",14);
		if (_Step14  == null)
		{
			return -1;
		}
		_Step14 .tag_BeginFun = Step14 ;
		PointAggregate _Step15 = pointMotion.FindPoint(tag_stationName, "标定结束",15);
		if (_Step15  == null)
		{
			return -1;
		}
		_Step15 .tag_BeginFun = Step15 ;
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
