using System;
using System.Threading;
namespace StrongProject
{
	public class LeftCCD : workBase
	{   /// <summary>
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
		public SocketClient tag_SocketClient0;


		public string tag_sendStr;

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="ConstructCreate"></param>
		/// <returns></returns>
		public LeftCCD(Work _Work)
		{
			tag_Work = _Work;
			tag_SocketClient0 = _Work.tag_SocketClient[0];
			tag_stationName = "左工位标定流程";
			tag_isRestStation = 0;
		}
		/// <summary>
		/// 启动函数，主要是线程启动
		/// </summary>
		/// <param name="start"></param>
		/// <returns></returns>
		public bool StartThread1()
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

			tag_isWork = 1; ;
			return 0;
		}
		/// <summary>
		/// 开真空，第1步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step1"></param>
		/// <returns></returns>
		public short Step1(object o)
		{

			return 0;
		}
		/// <summary>
		/// 到拍照位，第2步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step2"></param>
		/// <returns></returns>
		public short Step2(object o)
		{

			return 0;
		}
		/// <summary>
		/// 拍照1，第3步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step3"></param>
		/// <returns></returns>
		public short Step3(object o)
		{
			CFile.WriteCCDData(DateTime.Now.ToString() + "  Send  " + "CB,0,1\r\n");
			string strReceive = tag_SocketClient0.Send("CB,0,1\r\n", 1000);
			CFile.WriteCCDData(DateTime.Now.ToString() + "  Receive  " + strReceive + "\r\n");
			return 0;
		}
		/// <summary>
		/// 标定1位A，第4步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step4"></param>
		/// <returns></returns>
		public short Step4(object o)
		{
			tag_sendStr = "HC,0,14";
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降1A，第5步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step5"></param>
		/// <returns></returns>
		public short Step5(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定1位B，第6步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step6"></param>
		/// <returns></returns>
		public short Step6(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升1B，第7步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step7"></param>
		/// <returns></returns>
		public short Step7(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定1位C，第8步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step8"></param>
		/// <returns></returns>
		public short Step8(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降1C，第9步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step9"></param>
		/// <returns></returns>
		public short Step9(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定1位D，第10步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step10"></param>
		/// <returns></returns>
		public short Step10(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升1D，第11步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step11"></param>
		/// <returns></returns>
		public short Step11(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定2位A，第12步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step12"></param>
		/// <returns></returns>
		public short Step12(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降2A，第13步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step13"></param>
		/// <returns></returns>
		public short Step13(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定2位B，第14步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step14"></param>
		/// <returns></returns>
		public short Step14(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升2B，第15步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step15"></param>
		/// <returns></returns>
		public short Step15(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定2位C，第16步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step16"></param>
		/// <returns></returns>
		public short Step16(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降2C，第17步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step17"></param>
		/// <returns></returns>
		public short Step17(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定2位D，第18步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step18"></param>
		/// <returns></returns>
		public short Step18(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升2D，第19步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step19"></param>
		/// <returns></returns>
		public short Step19(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定3位A，第20步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step20"></param>
		/// <returns></returns>
		public short Step20(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降3A，第21步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step21"></param>
		/// <returns></returns>
		public short Step21(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定3位B，第22步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step22"></param>
		/// <returns></returns>
		public short Step22(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升3B，第23步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step23"></param>
		/// <returns></returns>
		public short Step23(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定3位C，第24步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step24"></param>
		/// <returns></returns>
		public short Step24(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降3C，第25步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step25"></param>
		/// <returns></returns>
		public short Step25(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定3位D，第26步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step26"></param>
		/// <returns></returns>
		public short Step26(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升3D，第27步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step27"></param>
		/// <returns></returns>
		public short Step27(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定4位A，第28步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step28"></param>
		/// <returns></returns>
		public short Step28(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降4A，第29步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step29"></param>
		/// <returns></returns>
		public short Step29(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定4位B，第30步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step30"></param>
		/// <returns></returns>
		public short Step30(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升4B，第31步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step31"></param>
		/// <returns></returns>
		public short Step31(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定4位C，第32步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step32"></param>
		/// <returns></returns>
		public short Step32(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降4C，第33步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step33"></param>
		/// <returns></returns>
		public short Step33(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定4位D，第34步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step34"></param>
		/// <returns></returns>
		public short Step34(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升4D，第35步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step35"></param>
		/// <returns></returns>
		public short Step35(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定5位A，第36步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step36"></param>
		/// <returns></returns>
		public short Step36(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降5A，第37步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step37"></param>
		/// <returns></returns>
		public short Step37(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定5位B，第38步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step38"></param>
		/// <returns></returns>
		public short Step38(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升5B，第39步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step39"></param>
		/// <returns></returns>
		public short Step39(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定5位C，第40步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step40"></param>
		/// <returns></returns>
		public short Step40(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降5C，第41步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step41"></param>
		/// <returns></returns>
		public short Step41(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定5位D，第42步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step42"></param>
		/// <returns></returns>
		public short Step42(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升5D，第43步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step43"></param>
		/// <returns></returns>
		public short Step43(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定6位A，第44步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step44"></param>
		/// <returns></returns>
		public short Step44(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降6A，第45步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step45"></param>
		/// <returns></returns>
		public short Step45(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定6位B，第46步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step46"></param>
		/// <returns></returns>
		public short Step46(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升6B，第47步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step47"></param>
		/// <returns></returns>
		public short Step47(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定6位C，第48步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step48"></param>
		/// <returns></returns>
		public short Step48(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降6C，第49步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step49"></param>
		/// <returns></returns>
		public short Step49(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定6位D，第50步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step50"></param>
		/// <returns></returns>
		public short Step50(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升6D，第51步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step51"></param>
		/// <returns></returns>
		public short Step51(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定7位A，第52步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step52"></param>
		/// <returns></returns>
		public short Step52(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降7A，第53步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step53"></param>
		/// <returns></returns>
		public short Step53(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定7位B，第54步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step54"></param>
		/// <returns></returns>
		public short Step54(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升7B，第55步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step55"></param>
		/// <returns></returns>
		public short Step55(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定7位C，第56步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step56"></param>
		/// <returns></returns>
		public short Step56(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降7C，第57步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step57"></param>
		/// <returns></returns>
		public short Step57(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定7位D，第58步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step58"></param>
		/// <returns></returns>
		public short Step58(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升7D，第59步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step59"></param>
		/// <returns></returns>
		public short Step59(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定8位A，第60步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step60"></param>
		/// <returns></returns>
		public short Step60(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降8A，第61步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step61"></param>
		/// <returns></returns>
		public short Step61(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定8位B，第62步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step62"></param>
		/// <returns></returns>
		public short Step62(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升8B，第63步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step63"></param>
		/// <returns></returns>
		public short Step63(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定8位C，第64步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step64"></param>
		/// <returns></returns>
		public short Step64(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降8C，第65步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step65"></param>
		/// <returns></returns>
		public short Step65(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定8位D，第66步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step66"></param>
		/// <returns></returns>
		public short Step66(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升8D，第67步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step67"></param>
		/// <returns></returns>
		public short Step67(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定9位A，第68步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step68"></param>
		/// <returns></returns>
		public short Step68(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降9A，第69步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step69"></param>
		/// <returns></returns>
		public short Step69(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定9位B，第70步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step70"></param>
		/// <returns></returns>
		public short Step70(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升9B，第71步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step71"></param>
		/// <returns></returns>
		public short Step71(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定9位C，第72步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step72"></param>
		/// <returns></returns>
		public short Step72(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降9C，第73步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step73"></param>
		/// <returns></returns>
		public short Step73(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定9位D，第74步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step74"></param>
		/// <returns></returns>
		public short Step74(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升9D，第75步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step75"></param>
		/// <returns></returns>
		public short Step75(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定10位A，第76步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step76"></param>
		/// <returns></returns>
		public short Step76(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降10A，第77步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step77"></param>
		/// <returns></returns>
		public short Step77(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定10位B，第78步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step78"></param>
		/// <returns></returns>
		public short Step78(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升10B，第79步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step79"></param>
		/// <returns></returns>
		public short Step79(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定10位C，第80步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step80"></param>
		/// <returns></returns>
		public short Step80(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降10C，第81步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step81"></param>
		/// <returns></returns>
		public short Step81(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定10位D，第82步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step82"></param>
		/// <returns></returns>
		public short Step82(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升10D，第83步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step83"></param>
		/// <returns></returns>
		public short Step83(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定11位A，第84步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step84"></param>
		/// <returns></returns>
		public short Step84(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降11A，第85步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step85"></param>
		/// <returns></returns>
		public short Step85(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定11位B，第86步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step86"></param>
		/// <returns></returns>
		public short Step86(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升11B，第87步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step87"></param>
		/// <returns></returns>
		public short Step87(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定11位C，第88步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step88"></param>
		/// <returns></returns>
		public short Step88(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降11C，第89步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step89"></param>
		/// <returns></returns>
		public short Step89(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定11位D，第90步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step90"></param>
		/// <returns></returns>
		public short Step90(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升11D，第91步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step91"></param>
		/// <returns></returns>
		public short Step91(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定12位A，第92步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step92"></param>
		/// <returns></returns>
		public short Step92(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降12A，第93步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step93"></param>
		/// <returns></returns>
		public short Step93(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定12位B，第94步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step94"></param>
		/// <returns></returns>
		public short Step94(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升12B，第95步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step95"></param>
		/// <returns></returns>
		public short Step95(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定12位C，第96步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step96"></param>
		/// <returns></returns>
		public short Step96(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降12C，第97步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step97"></param>
		/// <returns></returns>
		public short Step97(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定12位D，第98步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step98"></param>
		/// <returns></returns>
		public short Step98(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升12D，第99步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step99"></param>
		/// <returns></returns>
		public short Step99(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定13位A，第100步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step100"></param>
		/// <returns></returns>
		public short Step100(object o)
		{
			CCD(o);
			return 0;
		}
		/// <summary>
		/// 切刀气缸下降13A，第101步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step101"></param>
		/// <returns></returns>
		public short Step101(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定13位B，第102步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step102"></param>
		/// <returns></returns>
		public short Step102(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升13B，第103步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step103"></param>
		/// <returns></returns>
		public short Step103(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 标定13位C，第104步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step104"></param>
		/// <returns></returns>
		public short Step104(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸下降13C，第105步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step105"></param>
		/// <returns></returns>
		public short Step105(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 标定13位D，第106步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step106"></param>
		/// <returns></returns>
		public short Step106(object o)
		{

			return 0;
		}
		/// <summary>
		/// 切刀气缸上升13D，第107步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step107"></param>
		/// <returns></returns>
		public short Step107(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}

		/// <summary>
		/// 0A，第108步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step108"></param>
		/// <returns></returns>
		public short Step108(object o)
		{
			CCD(o, true);
			return 0;
		}
		/// <summary>
		/// 气缸下降，第109步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step108"></param>
		/// <returns></returns>
		public short Step109(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 气缸上升，第110步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step108"></param>
		/// <returns></returns>
		public short Step110(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}
		/// <summary>
		/// 0B，第111步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step108"></param>
		/// <returns></returns>
		public short Step111(object o)
		{

			return 0;
		}
		/// <summary>
		/// 气缸下降，第112步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step108"></param>
		/// <returns></returns>
		public short Step112(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// 气缸上升，第113步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step108"></param>
		/// <returns></returns>
		public short Step113(object o)
		{
			//Thread.Sleep(1000);
			return 0;
		}





		/// <summary>
		/// 回拍照位，第114步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step108"></param>
		/// <returns></returns>
		public short Step114(object o)
		{

			return 0;
		}
		/// <summary>
		/// 拍照2，第115步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step109"></param>
		/// <returns></returns>
		public short Step115(object o)
		{
			tag_sendStr = tag_sendStr + "\r\n";
			CFile.WriteCCDData(DateTime.Now.ToString() + "  Send  " + tag_sendStr);
			string strReceive = tag_SocketClient0.Send(tag_sendStr, 1000);
			CFile.WriteCCDData(DateTime.Now.ToString() + "  Receive  " + strReceive + "\r\n");

			UserControl_LogOut.OutLog(tag_sendStr, 0);
			return 0;
		}
		/// <summary>
		/// 回待料位，第116步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step110"></param>
		/// <returns></returns>
		public short Step116(object o)
		{

			return 0;
		}
		/// <summary>
		/// 关真空，第117步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step111"></param>
		/// <returns></returns>
		public short Step117(object o)
		{

			return 0;
		}
		/// <summary>
		/// 工位结束，第118步嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="Step112"></param>
		/// <returns></returns>
		public short Step118(object o)
		{

			return 0;
		}
		/// <summary>
		/// 初始化函数，初始化流程嵌入的代码，返回0 表示成功
		/// </summary>
		/// <param name="init"></param>
		/// <returns></returns>
		public short Init()
		{
			PointAggregate _Step0 = pointMotion.FindPoint(tag_stationName, "工位开始", 0);
			if (_Step0 == null)
			{
				return -1;
			}
			_Step0.tag_BeginFun = Step0;
			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "开真空", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;
			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "到拍照位", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;
			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "拍照1", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_BeginFun = Step3;
			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "标定1位A", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降1A", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_EndFun = Step5;
			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "标定1位B", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_BeginFun = Step6;
			PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升1B", 7);
			if (_Step7 == null)
			{
				return -1;
			}
			_Step7.tag_BeginFun = Step7;
			PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "标定1位C", 8);
			if (_Step8 == null)
			{
				return -1;
			}
			_Step8.tag_BeginFun = Step8;
			PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降1C", 9);
			if (_Step9 == null)
			{
				return -1;
			}
			_Step9.tag_EndFun = Step9;
			PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "标定1位D", 10);
			if (_Step10 == null)
			{
				return -1;
			}
			_Step10.tag_BeginFun = Step10;
			PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升1D", 11);
			if (_Step11 == null)
			{
				return -1;
			}
			_Step11.tag_BeginFun = Step11;
			PointAggregate _Step12 = pointMotion.FindPoint(tag_stationName, "标定2位A", 12);
			if (_Step12 == null)
			{
				return -1;
			}
			_Step12.tag_BeginFun = Step12;
			PointAggregate _Step13 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降2A", 13);
			if (_Step13 == null)
			{
				return -1;
			}
			_Step13.tag_EndFun = Step13;
			PointAggregate _Step14 = pointMotion.FindPoint(tag_stationName, "标定2位B", 14);
			if (_Step14 == null)
			{
				return -1;
			}
			_Step14.tag_BeginFun = Step14;
			PointAggregate _Step15 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升2B", 15);
			if (_Step15 == null)
			{
				return -1;
			}
			_Step15.tag_BeginFun = Step15;
			PointAggregate _Step16 = pointMotion.FindPoint(tag_stationName, "标定2位C", 16);
			if (_Step16 == null)
			{
				return -1;
			}
			_Step16.tag_BeginFun = Step16;
			PointAggregate _Step17 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降2C", 17);
			if (_Step17 == null)
			{
				return -1;
			}
			_Step17.tag_EndFun = Step17;
			PointAggregate _Step18 = pointMotion.FindPoint(tag_stationName, "标定2位D", 18);
			if (_Step18 == null)
			{
				return -1;
			}
			_Step18.tag_BeginFun = Step18;
			PointAggregate _Step19 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升2D", 19);
			if (_Step19 == null)
			{
				return -1;
			}
			_Step19.tag_BeginFun = Step19;
			PointAggregate _Step20 = pointMotion.FindPoint(tag_stationName, "标定3位A", 20);
			if (_Step20 == null)
			{
				return -1;
			}
			_Step20.tag_BeginFun = Step20;
			PointAggregate _Step21 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降3A", 21);
			if (_Step21 == null)
			{
				return -1;
			}
			_Step21.tag_EndFun = Step21;
			PointAggregate _Step22 = pointMotion.FindPoint(tag_stationName, "标定3位B", 22);
			if (_Step22 == null)
			{
				return -1;
			}
			_Step22.tag_BeginFun = Step22;
			PointAggregate _Step23 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升3B", 23);
			if (_Step23 == null)
			{
				return -1;
			}
			_Step23.tag_BeginFun = Step23;
			PointAggregate _Step24 = pointMotion.FindPoint(tag_stationName, "标定3位C", 24);
			if (_Step24 == null)
			{
				return -1;
			}
			_Step24.tag_BeginFun = Step24;
			PointAggregate _Step25 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降3C", 25);
			if (_Step25 == null)
			{
				return -1;
			}
			_Step25.tag_EndFun = Step25;
			PointAggregate _Step26 = pointMotion.FindPoint(tag_stationName, "标定3位D", 26);
			if (_Step26 == null)
			{
				return -1;
			}
			_Step26.tag_BeginFun = Step26;
			PointAggregate _Step27 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升3D", 27);
			if (_Step27 == null)
			{
				return -1;
			}
			_Step27.tag_BeginFun = Step27;
			PointAggregate _Step28 = pointMotion.FindPoint(tag_stationName, "标定4位A", 28);
			if (_Step28 == null)
			{
				return -1;
			}
			_Step28.tag_BeginFun = Step28;
			PointAggregate _Step29 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降4A", 29);
			if (_Step29 == null)
			{
				return -1;
			}
			_Step29.tag_EndFun = Step29;
			PointAggregate _Step30 = pointMotion.FindPoint(tag_stationName, "标定4位B", 30);
			if (_Step30 == null)
			{
				return -1;
			}
			_Step30.tag_BeginFun = Step30;
			PointAggregate _Step31 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升4B", 31);
			if (_Step31 == null)
			{
				return -1;
			}
			_Step31.tag_BeginFun = Step31;
			PointAggregate _Step32 = pointMotion.FindPoint(tag_stationName, "标定4位C", 32);
			if (_Step32 == null)
			{
				return -1;
			}
			_Step32.tag_BeginFun = Step32;
			PointAggregate _Step33 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降4C", 33);
			if (_Step33 == null)
			{
				return -1;
			}
			_Step33.tag_EndFun = Step33;
			PointAggregate _Step34 = pointMotion.FindPoint(tag_stationName, "标定4位D", 34);
			if (_Step34 == null)
			{
				return -1;
			}
			_Step34.tag_BeginFun = Step34;
			PointAggregate _Step35 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升4D", 35);
			if (_Step35 == null)
			{
				return -1;
			}
			_Step35.tag_BeginFun = Step35;
			PointAggregate _Step36 = pointMotion.FindPoint(tag_stationName, "标定5位A", 36);
			if (_Step36 == null)
			{
				return -1;
			}
			_Step36.tag_BeginFun = Step36;
			PointAggregate _Step37 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降5A", 37);
			if (_Step37 == null)
			{
				return -1;
			}
			_Step37.tag_EndFun = Step37;
			PointAggregate _Step38 = pointMotion.FindPoint(tag_stationName, "标定5位B", 38);
			if (_Step38 == null)
			{
				return -1;
			}
			_Step38.tag_BeginFun = Step38;
			PointAggregate _Step39 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升5B", 39);
			if (_Step39 == null)
			{
				return -1;
			}
			_Step39.tag_BeginFun = Step39;
			PointAggregate _Step40 = pointMotion.FindPoint(tag_stationName, "标定5位C", 40);
			if (_Step40 == null)
			{
				return -1;
			}
			_Step40.tag_BeginFun = Step40;
			PointAggregate _Step41 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降5C", 41);
			if (_Step41 == null)
			{
				return -1;
			}
			_Step41.tag_EndFun = Step41;
			PointAggregate _Step42 = pointMotion.FindPoint(tag_stationName, "标定5位D", 42);
			if (_Step42 == null)
			{
				return -1;
			}
			_Step42.tag_BeginFun = Step42;
			PointAggregate _Step43 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升5D", 43);
			if (_Step43 == null)
			{
				return -1;
			}
			_Step43.tag_BeginFun = Step43;
			PointAggregate _Step44 = pointMotion.FindPoint(tag_stationName, "标定6位A", 44);
			if (_Step44 == null)
			{
				return -1;
			}
			_Step44.tag_BeginFun = Step44;
			PointAggregate _Step45 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降6A", 45);
			if (_Step45 == null)
			{
				return -1;
			}
			_Step45.tag_EndFun = Step45;
			PointAggregate _Step46 = pointMotion.FindPoint(tag_stationName, "标定6位B", 46);
			if (_Step46 == null)
			{
				return -1;
			}
			_Step46.tag_BeginFun = Step46;
			PointAggregate _Step47 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升6B", 47);
			if (_Step47 == null)
			{
				return -1;
			}
			_Step47.tag_BeginFun = Step47;
			PointAggregate _Step48 = pointMotion.FindPoint(tag_stationName, "标定6位C", 48);
			if (_Step48 == null)
			{
				return -1;
			}
			_Step48.tag_BeginFun = Step48;
			PointAggregate _Step49 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降6C", 49);
			if (_Step49 == null)
			{
				return -1;
			}
			_Step49.tag_EndFun = Step49;
			PointAggregate _Step50 = pointMotion.FindPoint(tag_stationName, "标定6位D", 50);
			if (_Step50 == null)
			{
				return -1;
			}
			_Step50.tag_BeginFun = Step50;
			PointAggregate _Step51 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升6D", 51);
			if (_Step51 == null)
			{
				return -1;
			}
			_Step51.tag_BeginFun = Step51;
			PointAggregate _Step52 = pointMotion.FindPoint(tag_stationName, "标定7位A", 52);
			if (_Step52 == null)
			{
				return -1;
			}
			_Step52.tag_BeginFun = Step52;
			PointAggregate _Step53 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降7A", 53);
			if (_Step53 == null)
			{
				return -1;
			}
			_Step53.tag_EndFun = Step53;
			PointAggregate _Step54 = pointMotion.FindPoint(tag_stationName, "标定7位B", 54);
			if (_Step54 == null)
			{
				return -1;
			}
			_Step54.tag_BeginFun = Step54;
			PointAggregate _Step55 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升7B", 55);
			if (_Step55 == null)
			{
				return -1;
			}
			_Step55.tag_BeginFun = Step55;
			PointAggregate _Step56 = pointMotion.FindPoint(tag_stationName, "标定7位C", 56);
			if (_Step56 == null)
			{
				return -1;
			}
			_Step56.tag_BeginFun = Step56;
			PointAggregate _Step57 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降7C", 57);
			if (_Step57 == null)
			{
				return -1;
			}
			_Step57.tag_EndFun = Step57;
			PointAggregate _Step58 = pointMotion.FindPoint(tag_stationName, "标定7位D", 58);
			if (_Step58 == null)
			{
				return -1;
			}
			_Step58.tag_BeginFun = Step58;
			PointAggregate _Step59 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升7D", 59);
			if (_Step59 == null)
			{
				return -1;
			}
			_Step59.tag_BeginFun = Step59;
			PointAggregate _Step60 = pointMotion.FindPoint(tag_stationName, "标定8位A", 60);
			if (_Step60 == null)
			{
				return -1;
			}
			_Step60.tag_BeginFun = Step60;
			PointAggregate _Step61 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降8A", 61);
			if (_Step61 == null)
			{
				return -1;
			}
			_Step61.tag_EndFun = Step61;
			PointAggregate _Step62 = pointMotion.FindPoint(tag_stationName, "标定8位B", 62);
			if (_Step62 == null)
			{
				return -1;
			}
			_Step62.tag_BeginFun = Step62;
			PointAggregate _Step63 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升8B", 63);
			if (_Step63 == null)
			{
				return -1;
			}
			_Step63.tag_BeginFun = Step63;
			PointAggregate _Step64 = pointMotion.FindPoint(tag_stationName, "标定8位C", 64);
			if (_Step64 == null)
			{
				return -1;
			}
			_Step64.tag_BeginFun = Step64;
			PointAggregate _Step65 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降8C", 65);
			if (_Step65 == null)
			{
				return -1;
			}
			_Step65.tag_EndFun = Step65;
			PointAggregate _Step66 = pointMotion.FindPoint(tag_stationName, "标定8位D", 66);
			if (_Step66 == null)
			{
				return -1;
			}
			_Step66.tag_BeginFun = Step66;
			PointAggregate _Step67 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升8D", 67);
			if (_Step67 == null)
			{
				return -1;
			}
			_Step67.tag_BeginFun = Step67;
			PointAggregate _Step68 = pointMotion.FindPoint(tag_stationName, "标定9位A", 68);
			if (_Step68 == null)
			{
				return -1;
			}
			_Step68.tag_BeginFun = Step68;
			PointAggregate _Step69 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降9A", 69);
			if (_Step69 == null)
			{
				return -1;
			}
			_Step69.tag_EndFun = Step69;
			PointAggregate _Step70 = pointMotion.FindPoint(tag_stationName, "标定9位B", 70);
			if (_Step70 == null)
			{
				return -1;
			}
			_Step70.tag_BeginFun = Step70;
			PointAggregate _Step71 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升9B", 71);
			if (_Step71 == null)
			{
				return -1;
			}
			_Step71.tag_BeginFun = Step71;
			PointAggregate _Step72 = pointMotion.FindPoint(tag_stationName, "标定9位C", 72);
			if (_Step72 == null)
			{
				return -1;
			}
			_Step72.tag_BeginFun = Step72;
			PointAggregate _Step73 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降9C", 73);
			if (_Step73 == null)
			{
				return -1;
			}
			_Step73.tag_EndFun = Step73;
			PointAggregate _Step74 = pointMotion.FindPoint(tag_stationName, "标定9位D", 74);
			if (_Step74 == null)
			{
				return -1;
			}
			_Step74.tag_BeginFun = Step74;
			PointAggregate _Step75 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升9D", 75);
			if (_Step75 == null)
			{
				return -1;
			}
			_Step75.tag_BeginFun = Step75;
			PointAggregate _Step76 = pointMotion.FindPoint(tag_stationName, "标定10位A", 76);
			if (_Step76 == null)
			{
				return -1;
			}
			_Step76.tag_BeginFun = Step76;
			PointAggregate _Step77 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降10A", 77);
			if (_Step77 == null)
			{
				return -1;
			}
			_Step77.tag_EndFun = Step77;
			PointAggregate _Step78 = pointMotion.FindPoint(tag_stationName, "标定10位B", 78);
			if (_Step78 == null)
			{
				return -1;
			}
			_Step78.tag_BeginFun = Step78;
			PointAggregate _Step79 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升10B", 79);
			if (_Step79 == null)
			{
				return -1;
			}
			_Step79.tag_BeginFun = Step79;
			PointAggregate _Step80 = pointMotion.FindPoint(tag_stationName, "标定10位C", 80);
			if (_Step80 == null)
			{
				return -1;
			}
			_Step80.tag_BeginFun = Step80;
			PointAggregate _Step81 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降10C", 81);
			if (_Step81 == null)
			{
				return -1;
			}
			_Step81.tag_EndFun = Step81;
			PointAggregate _Step82 = pointMotion.FindPoint(tag_stationName, "标定10位D", 82);
			if (_Step82 == null)
			{
				return -1;
			}
			_Step82.tag_BeginFun = Step82;
			PointAggregate _Step83 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升10D", 83);
			if (_Step83 == null)
			{
				return -1;
			}
			_Step83.tag_BeginFun = Step83;
			PointAggregate _Step84 = pointMotion.FindPoint(tag_stationName, "标定11位A", 84);
			if (_Step84 == null)
			{
				return -1;
			}
			_Step84.tag_BeginFun = Step84;
			PointAggregate _Step85 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降11A", 85);
			if (_Step85 == null)
			{
				return -1;
			}
			_Step85.tag_EndFun = Step85;
			PointAggregate _Step86 = pointMotion.FindPoint(tag_stationName, "标定11位B", 86);
			if (_Step86 == null)
			{
				return -1;
			}
			_Step86.tag_BeginFun = Step86;
			PointAggregate _Step87 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升11B", 87);
			if (_Step87 == null)
			{
				return -1;
			}
			_Step87.tag_BeginFun = Step87;
			PointAggregate _Step88 = pointMotion.FindPoint(tag_stationName, "标定11位C", 88);
			if (_Step88 == null)
			{
				return -1;
			}
			_Step88.tag_BeginFun = Step88;
			PointAggregate _Step89 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降11C", 89);
			if (_Step89 == null)
			{
				return -1;
			}
			_Step89.tag_EndFun = Step89;
			PointAggregate _Step90 = pointMotion.FindPoint(tag_stationName, "标定11位D", 90);
			if (_Step90 == null)
			{
				return -1;
			}
			_Step90.tag_BeginFun = Step90;
			PointAggregate _Step91 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升11D", 91);
			if (_Step91 == null)
			{
				return -1;
			}
			_Step91.tag_BeginFun = Step91;
			PointAggregate _Step92 = pointMotion.FindPoint(tag_stationName, "标定12位A", 92);
			if (_Step92 == null)
			{
				return -1;
			}
			_Step92.tag_BeginFun = Step92;
			PointAggregate _Step93 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降12A", 93);
			if (_Step93 == null)
			{
				return -1;
			}
			_Step93.tag_EndFun = Step93;
			PointAggregate _Step94 = pointMotion.FindPoint(tag_stationName, "标定12位B", 94);
			if (_Step94 == null)
			{
				return -1;
			}
			_Step94.tag_BeginFun = Step94;
			PointAggregate _Step95 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升12B", 95);
			if (_Step95 == null)
			{
				return -1;
			}
			_Step95.tag_BeginFun = Step95;
			PointAggregate _Step96 = pointMotion.FindPoint(tag_stationName, "标定12位C", 96);
			if (_Step96 == null)
			{
				return -1;
			}
			_Step96.tag_BeginFun = Step96;
			PointAggregate _Step97 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降12C", 97);
			if (_Step97 == null)
			{
				return -1;
			}
			_Step97.tag_EndFun = Step97;
			PointAggregate _Step98 = pointMotion.FindPoint(tag_stationName, "标定12位D", 98);
			if (_Step98 == null)
			{
				return -1;
			}
			_Step98.tag_BeginFun = Step98;
			PointAggregate _Step99 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升12D", 99);
			if (_Step99 == null)
			{
				return -1;
			}
			_Step99.tag_BeginFun = Step99;
			PointAggregate _Step100 = pointMotion.FindPoint(tag_stationName, "标定13位A", 100);
			if (_Step100 == null)
			{
				return -1;
			}
			_Step100.tag_BeginFun = Step100;
			PointAggregate _Step101 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降13A", 101);
			if (_Step101 == null)
			{
				return -1;
			}
			_Step101.tag_EndFun = Step101;
			PointAggregate _Step102 = pointMotion.FindPoint(tag_stationName, "标定13位B", 102);
			if (_Step102 == null)
			{
				return -1;
			}
			_Step102.tag_BeginFun = Step102;
			PointAggregate _Step103 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升13B", 103);
			if (_Step103 == null)
			{
				return -1;
			}
			_Step103.tag_BeginFun = Step103;
			PointAggregate _Step104 = pointMotion.FindPoint(tag_stationName, "标定13位C", 104);
			if (_Step104 == null)
			{
				return -1;
			}
			_Step104.tag_BeginFun = Step104;
			PointAggregate _Step105 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降13C", 105);
			if (_Step105 == null)
			{
				return -1;
			}
			_Step105.tag_EndFun = Step105;
			PointAggregate _Step106 = pointMotion.FindPoint(tag_stationName, "标定13位D", 106);
			if (_Step106 == null)
			{
				return -1;
			}
			_Step106.tag_BeginFun = Step106;
			PointAggregate _Step107 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升13D", 107);
			if (_Step107 == null)
			{
				return -1;
			}
			_Step107.tag_BeginFun = Step107;
			PointAggregate _Step108 = pointMotion.FindPoint(tag_stationName, "标定0位A", 108);
			if (_Step108 == null)
			{
				return -1;
			}
			_Step108.tag_BeginFun = Step108;
			PointAggregate _Step109 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降0A", 109);
			if (_Step109 == null)
			{
				return -1;
			}
			_Step109.tag_EndFun = Step109;
			PointAggregate _Step110 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升0A", 110);
			if (_Step110 == null)
			{
				return -1;
			}
			_Step110.tag_BeginFun = Step110;
			PointAggregate _Step111 = pointMotion.FindPoint(tag_stationName, "标定0位B", 111);
			if (_Step111 == null)
			{
				return -1;
			}
			_Step111.tag_BeginFun = Step111;
			PointAggregate _Step112 = pointMotion.FindPoint(tag_stationName, "切刀气缸下降0B", 112);
			if (_Step112 == null)
			{
				return -1;
			}
			_Step112.tag_EndFun = Step112;
			PointAggregate _Step113 = pointMotion.FindPoint(tag_stationName, "切刀气缸上升0B", 113);
			if (_Step113 == null)
			{
				return -1;
			}
			_Step113.tag_BeginFun = Step113;
			PointAggregate _Step114 = pointMotion.FindPoint(tag_stationName, "回拍照位", 114);
			if (_Step114 == null)
			{
				return -1;
			}
			_Step114.tag_BeginFun = Step114;
			PointAggregate _Step115 = pointMotion.FindPoint(tag_stationName, "拍照2", 115);
			if (_Step115 == null)
			{
				return -1;
			}
			_Step115.tag_BeginFun = Step115;
			PointAggregate _Step116 = pointMotion.FindPoint(tag_stationName, "回待料位", 116);
			if (_Step116 == null)
			{
				return -1;
			}
			_Step116.tag_BeginFun = Step116;
			PointAggregate _Step117 = pointMotion.FindPoint(tag_stationName, "关真空", 117);
			if (_Step117 == null)
			{
				return -1;
			}
			_Step117.tag_BeginFun = Step117;
			PointAggregate _Step118 = pointMotion.FindPoint(tag_stationName, "工位结束", 118);
			if (_Step118 == null)
			{
				return -1;
			}
			_Step118.tag_BeginFun = Step118;
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
			return 0;
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

		public short CCD(object o, bool isZero = false)
		{
			PointAggregate p = (PointAggregate)o;
			if (!isZero)
			{
				tag_sendStr = tag_sendStr + "," + p.arrPoint[1].dblPonitValue + "," + p.arrPoint[0].dblPonitValue + "," + p.arrPoint[2].dblPonitValue;
			}
			else
			{
				tag_sendStr = tag_sendStr + "," + (p.arrPoint[1].dblPonitValue + 4.8) + "," + p.arrPoint[0].dblPonitValue + "," + p.arrPoint[2].dblPonitValue;
			}
			return 0;
		}
	}
}
