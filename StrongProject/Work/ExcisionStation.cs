using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace StrongProject
{
	public class ExcisionStation : workBase
	{   /// <summary>
		///  work
		/// </summary>
		public Work tag_Work;
		/// <summary>
		/// �߳�
		/// </summary>
		public Thread tag_workThread;
		/// <summary>
		/// <summary>
		/// 
		/// </summary>
		public SocketClient tag_SocketClient0;

		public bool isLeftStation;

		char[] SpinLock = new char[1];
		string[] retArr;
		IOParameter axisRCoil;

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="ConstructCreate"></param>
		/// <returns></returns>
		public ExcisionStation(Work _Work)
		{
			tag_Work = _Work;
			tag_SocketClient0 = _Work.tag_SocketClient[0];
			tag_stationName = "�е�ع�λ";
			tag_isRestStation = 0;
			SpinLock[0] = ',';
			axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
		}
		/// <summary>
		/// ������������Ҫ���߳�����
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
		/// �˳����������ñ������������Ƴ�
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
		/// ��λ��ʼ����0��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step0"></param>
		/// <returns></returns>
		public short Step0(object o)
		{

			tag_isWork = 1; ;
			return 0;
		}
		/// <summary>
		/// �е�������������1��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step1"></param>
		/// <returns></returns>
		public short Step1(object o)
		{

			return 0;
		}
		/// <summary>
		/// �ȴ�����ҹ�λ��������2��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step2"></param>
		/// <returns></returns>
		public short Step2(object o)
		{

			while (true)
			{
				if (Global.WorkVar.tag_LeftStart == true)
				{
					//Global.WorkVar.tag_LeftStart = false;
					isLeftStation = true;
					return 0;
				}
				else if (Global.WorkVar.tag_RightStart == true)
				{
					//Global.WorkVar.tag_RightStart = false;
					tag_manual.tag_stepName = 3;
					isLeftStation = false;
					return 0;
				}
				else
				{
					Thread.Sleep(10);
				}

				if (IsExit())
				{
					return -1;
				}
			}
			return 0;
		}
		/// <summary>
		/// X�ᵽ����λ����3��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step3"></param>
		/// <returns></returns>
		public short Step3(object o)
		{
			Global.WorkVar.tag_LeftStart = false;
			tag_manual.tag_stepName = 4;
			return 0;
		}
		/// <summary>
		/// X�ᵽ�ҹ���λ����4��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step4"></param>
		/// <returns></returns>
		public short Step4(object o)
		{
			Global.WorkVar.tag_RightStart = false;
			return 0;
		}
		/// <summary>
		/// �ȴ���ز�����ɣ���5��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step5"></param>
		/// <returns></returns>
		public short Step5(object o)
		{
			//return 0;
			while (true)
			{
				if (isLeftStation && Global.WorkVar.bPCBTestFinish_L == true)
				{
					//Global.WorkVar.bPCBTestFinish_L = false;
					return 0;
				}
				else if (!isLeftStation && Global.WorkVar.bPCBTestFinish_R == true)
				{
					//Global.WorkVar.bPCBTestFinish_R = false;
					return 0;
				}
				else
				{
					Thread.Sleep(10);
				}

				if (IsExit())
				{
					return -1;
				}
			}

			return 0;
		}
		/// <summary>
		/// ������գ���6��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step6"></param>
		/// <returns></returns>
		public short Step6(object o)
		{
			if (isLeftStation)
				Global.WorkVar.bPCBTestFinish_L = false;
			else
				Global.WorkVar.bPCBTestFinish_R = false;
			if (Global.WorkVar.bEmptyRun)
			{
				tag_SocketClient0.Send("T1,1\r\n", 1000);
			}
			else
			{
				try
				{
					string strSend;
					PointAggregate p = (PointAggregate)o;
					Global.WorkVar.strCCDPosition_X = p.arrPoint[0].dblPonitValue.ToString();
					Global.WorkVar.strCCDPosition_A = p.arrPoint[1].dblPonitValue.ToString();
					if (isLeftStation)
					{
						strSend = "T1,1,0," + Global.WorkVar.strCCDPosition_X + "," + Global.WorkVar.strCCDPosition_Y_L
							+ "," + Global.WorkVar.strCCDPosition_A + "," + Global.WorkVar.CCDMode + "\r\n";
					}
					else
					{
						strSend = "T1,1,1," + Global.WorkVar.strCCDPosition_X + "," + Global.WorkVar.strCCDPosition_Y_R
							+ "," + Global.WorkVar.strCCDPosition_A + "," + Global.WorkVar.CCDMode + "\r\n";
					}
					CFile.WriteCCDData(DateTime.Now.ToString() + "  Send  " + strSend);
					string strReceive = tag_SocketClient0.Send(strSend, 1000);
					CFile.WriteCCDData(DateTime.Now.ToString() + "  Receive  " + strReceive + "\r\n");

					if (strReceive == "")
					{
						if (MessageBox.Show("���ͨѶ�쳣,ȷ���Ƿ��������գ��������յ��ȷ��", "ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
						{
							tag_manual.tag_stepName = tag_manual.tag_stepName - 1;
							return 0;
						}
						else
						{
							return -1;
						}
					}
					retArr = strReceive.Split(SpinLock);
					int endIndex = retArr[retArr.Count() - 1].IndexOf("\r\n", 0, retArr[retArr.Count() - 1].Length - 1);
					retArr[retArr.Count() - 1] = retArr[retArr.Count() - 1].Substring(0, endIndex);

					if (retArr[2] == "0")
					{
						if (MessageBox.Show("�������ʧ��,ȷ���Ƿ��������գ��������յ��ȷ��", "ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
						{
							tag_manual.tag_stepName = tag_manual.tag_stepName - 1;
							return 0;
						}
						else
						{
							return -1;
						}
					}
					else
					{
						Global.WorkVar.dCCDPosition_X = Convert.ToDouble(retArr[3]);
						if (isLeftStation)
							Global.WorkVar.dCCDPosition_Y_L = Convert.ToDouble(retArr[4]);
						else
							Global.WorkVar.dCCDPosition_Y_R = Convert.ToDouble(retArr[4]);
						Global.WorkVar.dCCDPosition_A = Convert.ToDouble(retArr[5]);
					}

				}
				catch
				{
					MessageBox.Show("������������쳣���������������");
					return -1;
				}
			}
			if (isLeftStation)
			{
				Global.WorkVar.bCCDFinish_L = true;
			}
			else
			{
				Global.WorkVar.bCCDFinish_R = true;
			}
			return 0;
		}
		/// <summary>
		/// X�ᡢR�ᵽ�е��1λ����7��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step7"></param>
		/// <returns></returns>
		public short Step7(object o)
		{
			//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
			Thread.Sleep(100);
			if (isLeftStation)
			{
				if (Global.WorkVar.bEmptyRun)
				{
					NewCtrlCardV0.AxisAbsoluteMove("���ܵ�λ", "X��", "���е��1λ", 2);
					NewCtrlCardV0.AxisAbsoluteMove("���ܵ�λ", "R��", "���е��1λ", 3);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "X��", "���е��1λ", 2) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "R��", "���е��1λ", 3) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
				else
				{
					StationModule stationM = StationManage.FindStation("���ܵ�λ");
					AxisConfig axisX = StationManage.FindAxis(stationM, "X��");
					AxisConfig axisA = StationManage.FindAxis(stationM, "R��");
					PointAggregate pointA = StationManage.FindPoint(stationM, "���е��1λ");
					double speedX = pointA.arrPoint[0].dblPonitSpeed;
					double speedA = pointA.arrPoint[1].dblPonitSpeed;
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX);
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
			}
			else
			{
				if (Global.WorkVar.bEmptyRun)
				{
					NewCtrlCardV0.AxisAbsoluteMove("���ܵ�λ", "X��", "���е��1λ", 2);
					NewCtrlCardV0.AxisAbsoluteMove("���ܵ�λ", "R��", "���е��1λ", 3);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "X��", "���е��1λ", 2) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "R��", "���е��1λ", 3) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
				else
				{
					StationModule stationM = StationManage.FindStation("���ܵ�λ");
					AxisConfig axisX = StationManage.FindAxis(stationM, "X��");
					AxisConfig axisA = StationManage.FindAxis(stationM, "R��");
					PointAggregate pointA = StationManage.FindPoint(stationM, "���е��1λ");
					double speedX = pointA.arrPoint[0].dblPonitSpeed;
					double speedA = pointA.arrPoint[1].dblPonitSpeed;
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX);
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
			}

			return 0;
		}
		/// <summary>
		/// �ȴ�Y�ᵽ�е��1λ����8��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step8"></param>
		/// <returns></returns>
		public short Step8(object o)
		{
			while (true)
			{
				if (isLeftStation && Global.WorkVar.bYAxisArrive_L[0] == true)
				{
					//Global.WorkVar.bYAxisArrive_L[0] = false;
					NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
					return 0;
				}
				else if (!isLeftStation && Global.WorkVar.bYAxisArrive_R[0] == true)
				{
					//Global.WorkVar.bYAxisArrive_R[0] = false;
					NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
					return 0;
				}
				else
				{
					Thread.Sleep(10);
				}

				if (IsExit())
				{
					return -1;
				}
			}
			//return 0;
		}
		/// <summary>
		/// �е������½�1����9��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step9"></param>
		/// <returns></returns>
		public short Step9(object o)
		{
			if (isLeftStation)
			{
				Global.WorkVar.bYAxisArrive_L[0] = false;
			}
			else
			{
				Global.WorkVar.bYAxisArrive_R[0] = false;
			}
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// �е���������1����10��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step10"></param>
		/// <returns></returns>
		public short Step10(object o)
		{
			if (isLeftStation)
			{
				Global.WorkVar.bExcisionFinish_L[0] = true;
			}
			else
			{
				Global.WorkVar.bExcisionFinish_R[0] = true;
			}

			NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
			return 0;
		}
		/// <summary>
		/// X�ᡢR�ᵽ�е��2λ����11��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step11"></param>
		/// <returns></returns>
		public short Step11(object o)
		{
			//IOParameter axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
			//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
			Thread.Sleep(100);
			if (isLeftStation)
			{
				if (Global.WorkVar.bEmptyRun)
				{
					NewCtrlCardV0.AxisAbsoluteMove("���ܵ�λ", "X��", "���е��2λ", 2);
					NewCtrlCardV0.AxisAbsoluteMove("���ܵ�λ", "R��", "���е��2λ", 3);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "X��", "���е��2λ", 2) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "R��", "���е��2λ", 3) != 0)
					{
						return -1;
					}

					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
				else
				{
					StationModule stationM = StationManage.FindStation("���ܵ�λ");
					AxisConfig axisX = StationManage.FindAxis(stationM, "X��");
					AxisConfig axisA = StationManage.FindAxis(stationM, "R��");
					PointAggregate pointA = StationManage.FindPoint(stationM, "���е��2λ");
					double speedX = pointA.arrPoint[0].dblPonitSpeed;
					double speedA = pointA.arrPoint[1].dblPonitSpeed;
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX);
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
			}
			else
			{
				if (Global.WorkVar.bEmptyRun)
				{
					NewCtrlCardV0.AxisAbsoluteMove("���ܵ�λ", "X��", "���е��2λ", 2);
					NewCtrlCardV0.AxisAbsoluteMove("���ܵ�λ", "R��", "���е��2λ", 3);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "X��", "���е��2λ", 2) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "R��", "���е��2λ", 3) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
				else
				{
					StationModule stationM = StationManage.FindStation("���ܵ�λ");
					AxisConfig axisX = StationManage.FindAxis(stationM, "X��");
					AxisConfig axisA = StationManage.FindAxis(stationM, "R��");
					PointAggregate pointA = StationManage.FindPoint(stationM, "���е��2λ");
					double speedX = pointA.arrPoint[0].dblPonitSpeed;
					double speedA = pointA.arrPoint[1].dblPonitSpeed;
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX);
					NewCtrlCardV0.GT_AbsoluteMove(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[2]);
					NewCtrlCardV0.WaitAxisStop(tag_Work._Config.axisArray[3]);
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[2], Global.WorkVar.dCCDPosition_X, speedX) != 0)
					{
						return -1;
					}
					if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[3], Global.WorkVar.dCCDPosition_A, speedA) != 0)
					{
						return -1;
					}
					//IOParameter axisRCoil = StationManage.FindOutputIo("�е�ع�λ", "R��ɲ����Ȧ");
					//NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
				}
			}
			return 0;
		}
		/// <summary>
		/// �ȴ�Y�ᵽ�е��2λ����12��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step12"></param>
		/// <returns></returns>
		public short Step12(object o)
		{
			while (true)
			{
				if (isLeftStation && Global.WorkVar.bYAxisArrive_L[1] == true)
				{
					//Global.WorkVar.bYAxisArrive_L[1] = false;

					NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
					return 0;
				}
				else if (!isLeftStation && Global.WorkVar.bYAxisArrive_R[1] == true)
				{
					//Global.WorkVar.bYAxisArrive_R[1] = false;

					NewCtrlCardV0.SetOutputIoBit(axisRCoil, 1);
					return 0;
				}
				else
				{
					Thread.Sleep(10);
				}

				if (IsExit())
				{
					return -1;
				}
			}
			return 0;
		}
		/// <summary>
		/// �е������½�2����13��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step13"></param>
		/// <returns></returns>
		public short Step13(object o)
		{
			if (isLeftStation)
			{
				Global.WorkVar.bYAxisArrive_L[1] = false;
			}
			else
			{
				Global.WorkVar.bYAxisArrive_R[1] = false;
			}
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fCutterCYTime));
			return 0;
		}
		/// <summary>
		/// �е���������2����14��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step14"></param>
		/// <returns></returns>
		public short Step14(object o)
		{
			NewCtrlCardV0.SetOutputIoBit(axisRCoil, 0);
			if (isLeftStation)
			{
				Global.WorkVar.bExcisionFinish_L[1] = true;
			}
			else
			{
				Global.WorkVar.bExcisionFinish_R[1] = true;
			}
			if (Global.WorkVar.tag_LeftStart == true)
			{
				//Global.WorkVar.tag_LeftStart = false;
				tag_manual.tag_stepName = 2;
				isLeftStation = true;
				return 0;
			}
			else if (Global.WorkVar.tag_RightStart == true)
			{
				//Global.WorkVar.tag_RightStart = false;
				tag_manual.tag_stepName = 3;
				isLeftStation = false;
				return 0;
			}
			return 0;
		}

		/// <summary>
		/// X�ᡢR�ᵽ����λ����15��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step23"></param>
		/// <returns></returns>
		public short Step15(object o)
		{

			return 0;
		}
		/// <summary>
		/// ��λ��������16��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step24"></param>
		/// <returns></returns>
		public short Step16(object o)
		{
			tag_manual.tag_stepName = -1;
			return 0;
		}
		/// <summary>
		/// ��ʼ����������ʼ������Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="init"></param>
		/// <returns></returns>
		public short Init()
		{
			PointAggregate _Step0 = pointMotion.FindPoint(tag_stationName, "��λ��ʼ", 0);
			if (_Step0 == null)
			{
				return -1;
			}
			_Step0.tag_BeginFun = Step0;
			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "�е���������", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;
			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "�ȴ�����ҹ�λ����", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;
			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "X�ᵽ����λ", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_EndFun = Step3;
			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "X�ᵽ�ҹ���λ", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "�ȴ���ز������", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5;
			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "�������", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_BeginFun = Step6;
			PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "X�ᡢR�ᵽ�е��1λ", 7);
			if (_Step7 == null)
			{
				return -1;
			}
			_Step7.tag_BeginFun = Step7;
			PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "�ȴ�Y�ᵽ�е��1λ", 8);
			if (_Step8 == null)
			{
				return -1;
			}
			_Step8.tag_BeginFun = Step8;
			PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "�е������½�1", 9);
			if (_Step9 == null)
			{
				return -1;
			}
			_Step9.tag_EndFun = Step9;
			PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "�е���������1", 10);
			if (_Step10 == null)
			{
				return -1;
			}
			_Step10.tag_EndFun = Step10;
			PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "X�ᡢR�ᵽ�е��2λ", 11);
			if (_Step11 == null)
			{
				return -1;
			}
			_Step11.tag_BeginFun = Step11;
			PointAggregate _Step12 = pointMotion.FindPoint(tag_stationName, "�ȴ�Y�ᵽ�е��2λ", 12);
			if (_Step12 == null)
			{
				return -1;
			}
			_Step12.tag_BeginFun = Step12;
			PointAggregate _Step13 = pointMotion.FindPoint(tag_stationName, "�е������½�2", 13);
			if (_Step13 == null)
			{
				return -1;
			}
			_Step13.tag_EndFun = Step13;
			PointAggregate _Step14 = pointMotion.FindPoint(tag_stationName, "�е���������2", 14);
			if (_Step14 == null)
			{
				return -1;
			}
			_Step14.tag_EndFun = Step14;
			PointAggregate _Step15 = pointMotion.FindPoint(tag_stationName, "X�ᡢR�ᵽ����λ", 15);
			if (_Step15 == null)
			{
				return -1;
			}
			_Step15.tag_BeginFun = Step15;
			PointAggregate _Step16 = pointMotion.FindPoint(tag_stationName, "��λ����", 16);
			if (_Step16 == null)
			{
				return -1;
			}
			_Step16.tag_BeginFun = Step16;
			return 0;
		}
		/// <summary>
		/// �жϺ���
		/// </summary>
		/// <param name="Suspend"></param>
		/// <returns></returns>
		public short Suspend(object o)
		{
			if (tag_Work._Config.tag_PrivateSave.tag_SuspendType == 0)
			{
				if (tag_manual.tag_stepName > 0)
				{
					tag_manual.tag_stepName = tag_manual.tag_stepName - 1;
				}
			}
			return 0;
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="Continue"></param>
		/// <returns></returns>
		public short Continue(object o)
		{

			return 0;
		}
		/// <summary>
		/// ����ִ�еĺ��� ����0 ��ʾ�ɹ�
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
		/// �������߳�ִ��ִ�еĺ��� �޷���ֵ
		/// </summary>
		/// <param name="workThreadCreate"></param>
		/// <returns></returns>
		public void workThread(object o)
		{
			short ret = 0;
			if (Init() == 0)
			{
				//while (true)
				//{
				if (tag_manual.tag_step == 0)
				{
					tag_isWork = pointMotion.StationRun(tag_stationName, tag_manual);
					tag_manual.tag_stepName = 0;
				}
				//    Thread.Sleep(10);
				//}
			}
		}
	}
}
