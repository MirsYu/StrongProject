using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
namespace StrongProject
{
	public class Global
	{
		#region 信息弹窗相关变量
		public static class ShowMesgeInfon
		{
			public static Frm_Frame Fram = null;
			public static bool HasShowForm = false;
			public static bool bConfirmNo = false;  //点击返回否
			public static bool bConfirmYes = false;  //点击返回是
			public static bool bInitialized = false;//窗体初始化加载完成

			public static void Init(Frm_Frame form)
			{
				Fram = form;
			}

			public static void MessageTopMost(string mes, bool showYesNO = false, string title = "提示(hint)")
			{

				while (HasShowForm)
				{
					Thread.Sleep(500);
				}
				HasShowForm = true;
				ShowMesg(mes, showYesNO, title);

			}
			private static void ShowMesg(string mes, bool showYesNO, string title)
			{
				if (bInitialized)
				{
					Fram.ShowFormMesg(mes, showYesNO, title);
				}
				else
				{
					Global.Forms.Msg.MessageTopMost(mes, false, false, false);
				}

			}

		}

		#endregion
		#region Const
		//全局常量
		public static class CConst
		{
			//窗口变量         
			public const int FRM_MAIN = 1;
			public const int FRM_DEBUG = 2;
			public const int FRM_DATA = 3;
			public const int FORM_ALARM = 4;

			//用户权限变量
			public const int USER_OPERATOR = 1;//作业员
			public const int USER_ADMINISTOR = 2;//管理员
			public const int USER_SUPERADMIN = 3;//超级管理员
			public static int UserLevel = USER_OPERATOR;
			public static int Form_Var = 0;//窗口变量

			//log保存路径
			public const string LOG_ID_EXCEPTION = "Exception";//异常文件名
			public const string SAVE_DATA_PATH = "D:\\WorkcellMaskdata";
			public const string SAVE_DATA_PATH_ERROR = "D:\\WorkcellMaskdata\\Error";
			public static string Save_Folder_Name = "";
			public const string DTS = "strongscrew";

			//TCP通讯
			public const string TEST_IP_1 = "127.0.0.1";
			public const string TEST_PORT_1 = "8888";
			public const string TEST_ID = "client1";

			public const string TEST_IP_SERVER = "127.0.0.1";
			public const string TEST_PORT_SERVER = "8888";
			public const string TEST_ID_SERVER = "server2";

			//ohter
			public const string SOCK_ID_PDCA = "扫码器";
			public const string LOG_RUN_INFO = "runInfo";
			public static Color COLOR_BACKGROUND = Color.FromArgb(64, 64, 64);
			public static short OUTPUTON = 0;//IO 输出 置位
			public static short OUTPUTOFF = 1; //IO 输出 复位

			public static string LOW = "1";
			public static string HEIGHT = "2";
			public static int WORK_STATE_BEGIN = 99999999; //IO 输出 复位


		}
		#endregion

		#region Var
		//全局变量
		public static class WorkVar
		{
			public static bool bIsAddPhoto = false;//是否开启3#相机多飞拍一次二维码
			public static bool TopMostFormChooseYes = false;
			public static bool TopMostFormShowing = false;

			public static bool BuzzerActive = false;//启动蜂鸣器
			public static bool bBeltWorkSpeedUp = true;

			public static bool ConnectCard = true;//是否连接卡

			public static bool[] bHomeFinish_card0 = new bool[8] { false, false, false, false, false, false, false, false };//0卡的回零状态
			public static bool[] bHomeFinish_card1 = new bool[4] { false, false, false, false };//1卡的回零状态


			//new
			public static bool SafeDoorActive = false;
			public static bool CardIoReadError = false;//1030;IO
			public static bool CardIoReadErrorEx = false;//扩展IO
			public static bool PowerOn = false;



			public static bool bInitialized = false;//程序初始化完成标志   
			public static bool bcanRunFalg = true;//系统是否允许运动  
			public static bool tcpPDCAconnect = true;//是否上传PDCA标志，设true连接PDCA，false不连接

			public static bool ProgramStart = false;//程序启动
			public static bool ProgramStop = false;//程序停止
			public static bool ProgramReset = false;//程序复位
			public static bool ProgramEmg = false;//程序急停
			public static bool tag_GuanLian = true;//模块关联调试
			public static bool bEmptyRun = false;//空跑
			public static bool bEmptyRunWithBattery = false;//带料空跑


			public static bool BuzzerActiveByMsgForms = false;

			public static string g_projectDir = "";
			public static int Form_Var = 0;//窗口界面

			public static SolderConfig _config = new SolderConfig();
			public static string NowPopUpMsg = null;

			/// <summary>
			/// 0，没有复位，1 复位中，2 复位完成 
			/// </summary>
			public static int tag_ResetState = 0;
			/// <summary>
			/// 轴是否可以移动
			/// </summary>
			public static bool tag_isAxisMove = false;

			/// <summary>
			/// 0，没有工作，1 工作中
			/// </summary>
			public static int tag_workState = 0;

			/// <summary>
			/// 0 no stop，，1  
			/// </summary>
			public static int tag_StopState = 0;

			public static int tag_ButtonStopState = 0;

			/// <summary>
			/// 0 no Suspend，，1  
			/// </summary>
			public static int tag_SuspendState = 0;

			/// <summary>
			/// 是否退出，不需要急停  1，退出
			/// </summary>
			public static int tag_IsExit = 0;
			/// <summary>
			/// CCD CL 是否检查 默认检查 
			/// </summary>
			public static bool tag_ccdCLIsCheck = true;

			/// <summary>
			/// CCD HT 是否检查 默认检查 
			/// </summary>
			public static bool tag_ccdHTIsCheck = true;

			/// <summary>
			/// 屏蔽上料模块 0，屏蔽，1，不屏蔽
			/// </summary>
			public static bool tag_putThiing = true;


			/// <summary>
			/// 屏蔽PDCA 0，屏蔽，1，不屏蔽
			/// </summary>
			public static bool tag_PACD = true;

			///// <summary>
			/////线1的工作状态
			///// </summary>
			//public static int tag_line1Work = 1;


			public static string tag_MessageoxStr = null;
			public static bool tag_SnIsbempty = false;
			/// <summary>
			/// 当前正在运行的点位
			/// </summary>
			public static PointAggregate tag_ExePointAggregate;

			public static bool tag_isFangDaiJieChu = false;


			public static Color SRGreen = Color.FromArgb(175, 216, 153);
			public static Color SRRed = Color.FromArgb(227, 146, 137);
			public static Color SRBlue = Color.FromArgb(132, 192, 251);



			/// <summary>
			/// 左工位工作状态 
			/// </summary>
			public static int tag_LeftStationState;

			/// <summary>
			/// 右工位工作状态
			/// </summary>
			public static int tag_RightStationState;

			/// <summary>
			/// 切电池工位工作状态
			/// </summary>
			public static int tag_BatteryStationState;

			public static string strBatterySN_L;//左工位电池SN
			public static string strBatterySN_R;//右工位电池SN

			public static string strBatteryVoltage_L;//左工位电池电压
			public static string strBatteryVoltage_R;//右工位电池电压



			public static bool bWork_L;//左工位工作中
			public static bool bWork_R;//右工位工作中

			public static bool bSuspendState_L;//左工位暂停
			public static bool bSuspendState_R;//右工位暂停

			/// <summary>
			/// 左工位按下双启
			/// </summary>
			public static bool tag_LeftStart;

			/// <summary>
			/// 右工位按下双启
			/// </summary>
			public static bool tag_RightStart;

			public static bool bPCBTestFinish_L;//左工位电路板测试完成
			public static bool bPCBTestFinish_R;//右工位电路板测试完成

			public static bool bCCDFinish_L;//CCD左工位拍照OK
			public static bool bCCDFinish_R;//CCD右工位拍照OK

			public static bool[] bYAxisArrive_L = new bool[2];//左工位Y轴到切电池位
			public static bool[] bYAxisArrive_R = new bool[2];//右工位Y轴到切电池位

			public static bool[] bExcisionFinish_L = new bool[2];//切除左工位电池完成
			public static bool[] bExcisionFinish_R = new bool[2];//切除右工位电池完成

			public static string CCDMode;

			//发送给CCD点位
			public static string strCCDPosition_X;
			public static string strCCDPosition_Y_L;
			public static string strCCDPosition_Y_R;
			public static string strCCDPosition_A;

			//CCD返回数据
			public static double dCCDPosition_X;
			public static double dCCDPosition_Y_L;
			public static double dCCDPosition_Y_R;
			public static double dCCDPosition_A;

			public static void InitCommunicateStatus()
			{
				tag_LeftStart = false;
				tag_RightStart = false;
				bPCBTestFinish_L = false;
				bPCBTestFinish_R = false;
				bCCDFinish_L = false;
				bCCDFinish_R = false;
				bYAxisArrive_L = new bool[2];
				bYAxisArrive_R = new bool[2];
				bExcisionFinish_L = new bool[2];
				bExcisionFinish_R = new bool[2];
			}
		}
		#endregion

		#region File
		//文件操作
		public static class CFile
		{
			//write csv log
			public static void WriteCSVDate(string WriteString, string fileName, bool bAppendEnter = true, string path = CConst.SAVE_DATA_PATH)
			{
				path = path + CConst.Save_Folder_Name;
				if (Directory.Exists(path) == false)
				{
					Directory.CreateDirectory(path);
				}
				string strFile = path + "\\" + fileName;
				if (File.Exists(strFile) == false)
				{
					//FileStream fs1 = new FileStream("..\\..\\message\\log.csv", FileMode.Create );
				}

				try
				{
					StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
					if (bAppendEnter)
					{
						swf.Write(WriteString + "\r\n");
					}
					else
					{
						swf.Write(WriteString);
					}

					swf.Close();
				}
				catch (System.Exception ex)
				{

					RewrteCSV(WriteString, strFile, bAppendEnter);
				}
			}
			//重写CSV文件
			public static void RewrteCSV(string WriteString, string strFile, bool bAppendEnter)
			{
				try
				{
					StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
					if (bAppendEnter)
					{
						swf.Write(WriteString + "\r\n");
					}
					else
					{
						swf.Write(WriteString);
					}

					swf.Close();
				}
				catch (System.Exception ex)
				{

					RewrteCSV(WriteString, strFile, bAppendEnter);
					//MessageBoxLog.Show("csv文件操作出错," +ex.Message+ " 请不要在运行中打开csv文件");
				}
			}
			//Write txt ，log名字前自动加日期
			public static void WriteTxtDate(string WriteString, string fileName, string path = CConst.SAVE_DATA_PATH)//"..\\..\\log"
			{
				path = path + CConst.Save_Folder_Name;
				if (Directory.Exists(path) == false)
				{
					Directory.CreateDirectory(path);
				}
				string strFile = path + "\\" + DateTime.Now.ToString("yyMMdd") + fileName + ".txt";
				if (File.Exists(strFile) == false)
				{//可用于第一行的写标题操作
				 //FileStream fs1 = new FileStream("..\\..\\message\\log.csv", FileMode.Create );
				}
				try
				{
					StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
					swf.Write(DateTime.Now.ToString("yyMMdd-") + DateTime.Now.ToString("HH:mm:ss.fff ") + WriteString + "\r\n");
					swf.Close();
				}
				catch
				{

				}
			}
			//专门记录错误信息，只是默认路径不同
			public static void WriteTxtDateError(string WriteString, string fileName, string path = CConst.SAVE_DATA_PATH_ERROR)//"..\\..\\log"
			{
				//path = path + Global.CConst.Save_Folder_Name;
				if (Directory.Exists(path) == false)
				{
					Directory.CreateDirectory(path);
				}
				string strFile = path + "\\" + fileName + ".txt";
				if (File.Exists(strFile) == false)
				{//可用于第一行的写标题操作
				 //FileStream fs1 = new FileStream("..\\..\\message\\log.csv", FileMode.Create );
				}
				try
				{
					StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
					swf.Write(DateTime.Now.ToString("yyMMdd-") + DateTime.Now.ToString("HH:mm:ss.fff ") + WriteString + "\r\n");
					swf.Close();
				}
				catch
				{

				}
			}

			public class MyFile
			{
				//delete folder

				//create folder
				public static void CreateFolder(string folder)
				{
					if (!Directory.Exists(folder))
					{
						Directory.CreateDirectory(folder);
					}
				}
				//delet folder beyond set days
				public static bool BeyondDays(string compare, int days = 7)
				{//some name may not format for yyMMdd, that will run to catch
					try
					{
						compare = GetNameWithoutSlash(compare);
						int nowMonth = DateTime.Now.Month;
						int nowDay = DateTime.Now.Day;

						int Month = int.Parse(compare.Substring(2, 2));
						int Days = int.Parse(compare.Substring(4, 2));

						//   //HCHEN 0315

						//DateTime creatTime  = File.GetCreationTime("D:\\DataScrewMachine\\" + compare);

						//TimeSpan   t = DateTime.Now - creatTime;
						//if (t.Days >= days)
						//{
						//    return true;
						//}
						//else
						//    return false;

						if ((30 * nowMonth + nowDay) - (30 * Month + Days) > days)
						{
							return true;
						}
						else
						{
							return false;
						}

					}
					catch
					{
						//System.Diagnostics.Debug.Assert(false);
						//MessageBoxLog.Show(ex.Message);
					}
					return false;
				}
				//get name last of slash
				private static string GetNameWithoutSlash(string strName)
				{
					try
					{
						if (strName.IndexOf("\\") > 0)
						{
							int nLastSlash = strName.LastIndexOf("\\");
							string FileName = strName.Substring(nLastSlash + 1);
							return FileName;
						}
						else
						{
							return strName;
						}
					}
					catch
					{
					}
					return "";
				}
				//delete file
				public static void DeleteFile(string path)
				{
					if (System.IO.File.Exists(path))
					{
						System.IO.File.Delete(path);
					}
				}
				//move one file from folder to another
				public static void MoveFile(string oriFolder, string desFolder, string file)
				{
					if (System.IO.File.Exists(oriFolder + "\\" + file))
					{
						CreateFolder(desFolder);
						if (System.IO.Directory.Exists(desFolder))
						{
							System.IO.File.Move(oriFolder + "\\" + file, desFolder + "\\" + file);
						}
					}
				}
				//copy a file from a oriFolder to desFolder
				public static void CopyFile(string oriFolder, string desFolder, string file)
				{
					if (System.IO.File.Exists(oriFolder + "\\" + file))
					{
						CreateFolder(desFolder);
						if (System.IO.Directory.Exists(desFolder))
						{
							System.IO.File.Copy(oriFolder + "\\" + file, desFolder + "\\" + file + DateTime.Now.ToString("HHmm"), true);
							//System.IO.File.Copy(oriFolder + "\\" + file, desFolder + "\\" + file );
						}
					}
				}

				//move all files of folder to another
				public static void MoveFolderFiles(string OriFolder, string DesFolder)
				{
					try
					{
						if (Directory.Exists(OriFolder))
						{
							string[] FilesName = System.IO.Directory.GetFiles(OriFolder);
							for (int i = 0; i < FilesName.Length; i++)
							{
								string FileName = GetNameWithoutSlash(FilesName[i]);
								MoveFile(OriFolder, DesFolder, FileName);
							}
						}
					}
					catch
					{
					}


				}
				//copy all files of folder to another
				public static void CopyFolderFiles(string OriFolder, string DesFolder)
				{
					try
					{
						if (Directory.Exists(OriFolder))
						{
							string[] FilesName = System.IO.Directory.GetFiles(OriFolder);
							CreateFolder(DesFolder);
							for (int i = 0; i < FilesName.Length; i++)
							{
								string FileName = GetNameWithoutSlash(FilesName[i]);
								CopyFile(OriFolder, DesFolder, FileName);
							}
						}
					}
					catch
					{
					}


				}

			}
		}

		#endregion



		public static class FN
		{
			public static bool IsPermitRun()
			{
				return Global.WorkVar.bcanRunFalg;
			}
			public static bool IsAppInitialized()
			{
				return WorkVar.bInitialized;
			}


		}

		public static class FormFlash
		{
			public const int CONFIRM_YES_ON = 1;
			public const int CONFIRM_YES = 2;
			public static bool bMsgShowOnce = false;
			public static bool bConfirmYesNo = false;
			public static bool bConfirmYes = false;
			public static bool bError = false;
		}
		public static class Forms
		{
			public static class Msg
			{
				//记录所有的弹框内容
				public static void LogPopUpMsg(string str)
				{
					//Global.CFile.WriteTxtDate(DateTime.Now.ToString() + str, "PopUpMsg");
				}

				// 将弹框置于所有窗体上面,无需选择
				public static void MessageTopMost(string str, bool showAll, bool activeAlarm, bool setSuspend, int ErrCode = 0, string hint = "提示")
				{
					if (showAll)
					{

					}
					while (Global.WorkVar.TopMostFormShowing)
					{
						Thread.Sleep(30);
					}
					FormFlash.bConfirmYes = true;
					FrmShowMsg fm = new FrmShowMsg(str, hint, false, FormFlash.CONFIRM_YES, activeAlarm, ErrCode);
					fm.ShowDialog();

				}

				//弹框确认是否有取消动作，并将弹框置于所有窗体上面//cancel return true. 
				//showall TRUE时同时报警
				public static bool MessageCancelTopMostYesNo(string str, bool activeAlarm, bool setSuspend, int ErrCode = 0, string hint = "提示")
				{
					//if (activeAlarm)
					//{
					//    RunA.showInfoAll(str.Replace("\n", ""));
					//}
					//if (setSuspend && Global.CVar.staRun.WI.stVar.bToRun)
					//{
					//    RunA.SetSuspend("弹框");
					//}
					while (Global.WorkVar.TopMostFormShowing)
					{
						Thread.Sleep(30);
					}
					FormFlash.bConfirmYesNo = true;
					LogPopUpMsg("YesNo" + str);
					FrmShowMsg fm = new FrmShowMsg(str, hint, true, FormFlash.CONFIRM_YES_ON, activeAlarm, ErrCode);
					fm.ShowDialog();
					if (Global.WorkVar.TopMostFormShowing == false && Global.WorkVar.TopMostFormChooseYes == false)
					{
						LogPopUpMsg("choose cancel");
						return true;
					}
					else
					{
						LogPopUpMsg("choose yes");
						return false;
					}
				}
			}


		}

		public struct Point6D
		{
			public double X;
			public double Y;
			public double Z1;
			public double Z2;
			public double R1;
			public double R2;
		}

	}

}

