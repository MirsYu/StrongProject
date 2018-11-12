using System.Linq;
using System.Threading;
namespace StrongProject
{
	public class RightStation : workBase
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
		public JSerialPort tag_JSerialPort1;

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="ConstructCreate"></param>
		/// <returns></returns>
		public RightStation(Work _Work)
		{
			tag_Work = _Work;
			tag_JSerialPort1 = _Work.tag_JSerialPort[1];
			tag_stationName = "�ҹ�λ";
			tag_isRestStation = 0;
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
		/// �ȴ��ҹ�λ˫������1��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step1"></param>
		/// <returns></returns>
		public short Step1(object o)
		{
			bool start_R;
			bool start_M;
			if (Global.WorkVar.bEmptyRun && !Global.WorkVar.bEmptyRunWithBattery)
			{
				Global.WorkVar.tag_RightStart = true;
				Global.WorkVar.bWork_R = true;
				return 0;
			}
			while (true)
			{
				if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "������", out start_R) != 0)
				{
					UserControl_LogOut.OutLog("��ȡ��������ť����״̬ʧ��!", 0);
					return -1;
				}
				if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "������", out start_M) != 0)
				{
					UserControl_LogOut.OutLog("��ȡ��������ť����״̬ʧ��!", 0);
					return -1;
				}
				if ((start_R && start_M))
				{
					Global.WorkVar.tag_RightStart = true;
					Global.WorkVar.bWork_R = true;
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
		}
		/// <summary>
		/// ����ղ�������������2��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step2"></param>
		/// <returns></returns>
		public short Step2(object o)
		{

			return 0;
		}
		/// <summary>
		/// ��Y������λ����3��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step3"></param>
		/// <returns></returns>
		public short Step3(object o)
		{

			return 0;
		}
		/// <summary>
		/// �Ҳ�����������½�����4��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step4"></param>
		/// <returns></returns>
		public short Step4(object o)
		{

			return 0;
		}
		/// <summary>
		/// �ȴ���·����ԣ���5��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step5"></param>
		/// <returns></returns>
		public short Step5(object o)
		{
			Thread.Sleep((int)(1000 * tag_Work._Config.tag_PrivateSave.fTestCYTime));
			string strRec;
			int startIndex, endIndex;
			strRec = tag_JSerialPort1.sendCommand("Read Battery SN\r\n", 2000);
			if (strRec == "Read Battery SN Fail\r\n")
			{

			}
			else if (strRec.Contains("Battery SN:"))
			{
				startIndex = strRec.IndexOf(":", 0, strRec.Length - 1);
				endIndex = strRec.IndexOf("\r\n", 0, strRec.Length - 1);
				Global.WorkVar.strBatterySN_R = strRec.Substring(startIndex + 1, endIndex - startIndex - 1);
			}
			else
			{

			}
			strRec = tag_JSerialPort1.sendCommand("Read Battery Voltage\r\n", 2000);
			if (strRec == "Read Battery Voltage Fail\r\n")
			{

			}
			else if (strRec.Contains("Battery Voltage:"))
			{
				startIndex = strRec.IndexOf(":", 0, strRec.Length - 1);
				endIndex = strRec.IndexOf("mV\r\n", 0, strRec.Length - 1);
				Global.WorkVar.strBatteryVoltage_R = strRec.Substring(startIndex + 1, endIndex - startIndex - 1);
			}
			else
			{

			}
			return 0;
		}
		/// <summary>
		/// �Ҳ������������������6��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step6"></param>
		/// <returns></returns>
		public short Step6(object o)
		{

			return 0;
		}
		/// <summary>
		/// ��Y������λ���ȴ����գ���7��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step7"></param>
		/// <returns></returns>
		public short Step7(object o)
		{
			PointAggregate p = (PointAggregate)o;
			Global.WorkVar.strCCDPosition_Y_R = p.arrPoint[0].dblPonitValue.ToString();
			Global.WorkVar.bPCBTestFinish_R = true;
			while (true)
			{
				if (Global.WorkVar.bCCDFinish_R == true)
				{
					//Global.WorkVar.bCCDFinish_R = false;
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
		/// ��Y���е��1λ����8��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step8"></param>
		/// <returns></returns>
		public short Step8(object o)
		{
			Global.WorkVar.bCCDFinish_R = false;
			if (Global.WorkVar.bEmptyRun)
			{
				if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "��Y��", "���е��1λ", 1) != 0)
				{
					return -1;
				}
			}
			else
			{
				StationModule stationM = StationManage.FindStation("���ܵ�λ");
				AxisConfig axisC = StationManage.FindAxis(stationM, "��Y��");
				PointAggregate pointA = StationManage.FindPoint(stationM, "���е��1λ");
				double speed = pointA.arrPoint[1].dblPonitSpeed;
				if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[1], Global.WorkVar.dCCDPosition_Y_R, speed) != 0)
				{
					return -1;
				}
			}
			Global.WorkVar.bYAxisArrive_R[0] = true;
			return 0;
		}
		/// <summary>
		/// �ȴ��е�����1����9��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step9"></param>
		/// <returns></returns>
		public short Step9(object o)
		{
			//return 0;
			while (true)
			{
				if (Global.WorkVar.bExcisionFinish_R[0] == true)
				{
					//Global.WorkVar.bExcisionFinish_R[0] = false;
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
		}
		/// <summary>
		/// ��Y���е��2λ����10��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step10"></param>
		/// <returns></returns>
		public short Step10(object o)
		{
			Global.WorkVar.bExcisionFinish_R[0] = false;
			if (Global.WorkVar.bEmptyRun)
			{
				if (NewCtrlCardV0.AxisAbsoluteMoveAndWaitStopVrf("���ܵ�λ", "��Y��", "���е��2λ", 1) != 0)
				{
					return -1;
				}
			}
			else
			{
				StationModule stationM = StationManage.FindStation("���ܵ�λ");
				AxisConfig axisC = StationManage.FindAxis(stationM, "��Y��");
				PointAggregate pointA = StationManage.FindPoint(stationM, "���е��2λ");
				double speed = pointA.arrPoint[1].dblPonitSpeed;
				if (NewCtrlCardV0.GT_AxisAbsoluteMoveAndWaitStopVrf(tag_Work._Config.axisArray[1], Global.WorkVar.dCCDPosition_Y_R, speed) != 0)
				{
					return -1;
				}
			}
			Global.WorkVar.bYAxisArrive_R[1] = true;
			return 0;
		}
		/// <summary>
		/// �ȴ��е�����2����11��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step11"></param>
		/// <returns></returns>
		public short Step11(object o)
		{
			//return 0;
			while (true)
			{
				if (Global.WorkVar.bExcisionFinish_R[1] == true)
				{
					//Global.WorkVar.bExcisionFinish_R[1] = false;
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
		/// ��Y������λ����12��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step16"></param>
		/// <returns></returns>
		public short Step12(object o)
		{
			Global.WorkVar.bExcisionFinish_R[1] = false;
			return 0;
		}
		/// <summary>
		/// ����գ���13��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step17"></param>
		/// <returns></returns>
		public short Step13(object o)
		{

			return 0;
		}
		/// <summary>
		/// ��λ��������14��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step18"></param>
		/// <returns></returns>
		public short Step14(object o)
		{
			tag_manual.tag_stepName = -1;
			Global.WorkVar.bWork_R = false;
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
			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "�ȴ��ҹ�λ˫��", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;
			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "����ղ���������", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;
			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "��Y������λ", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_BeginFun = Step3;
			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "�Ҳ�����������½�", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "�ȴ���·�����", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5;
			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "�Ҳ��������������", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_EndFun = Step6;
			PointAggregate _Step7 = pointMotion.FindPoint(tag_stationName, "��Y������λ���ȴ�����", 7);
			if (_Step7 == null)
			{
				return -1;
			}
			_Step7.tag_EndFun = Step7;
			PointAggregate _Step8 = pointMotion.FindPoint(tag_stationName, "��Y���е��1λ", 8);
			if (_Step8 == null)
			{
				return -1;
			}
			_Step8.tag_EndFun = Step8;
			PointAggregate _Step9 = pointMotion.FindPoint(tag_stationName, "�ȴ��е�����1", 9);
			if (_Step9 == null)
			{
				return -1;
			}
			_Step9.tag_BeginFun = Step9;
			PointAggregate _Step10 = pointMotion.FindPoint(tag_stationName, "��Y���е��2λ", 10);
			if (_Step10 == null)
			{
				return -1;
			}
			_Step10.tag_EndFun = Step10;
			PointAggregate _Step11 = pointMotion.FindPoint(tag_stationName, "�ȴ��е�����2", 11);
			if (_Step11 == null)
			{
				return -1;
			}
			_Step11.tag_BeginFun = Step11;
			PointAggregate _Step12 = pointMotion.FindPoint(tag_stationName, "��Y������λ", 12);
			if (_Step12 == null)
			{
				return -1;
			}
			_Step12.tag_BeginFun = Step12;
			PointAggregate _Step13 = pointMotion.FindPoint(tag_stationName, "�����", 13);
			if (_Step13 == null)
			{
				return -1;
			}
			_Step13.tag_BeginFun = Step13;
			PointAggregate _Step14 = pointMotion.FindPoint(tag_stationName, "��λ����", 14);
			if (_Step14 == null)
			{
				return -1;
			}
			_Step14.tag_BeginFun = Step14;
			return 0;
		}
		/// <summary>
		/// �жϺ���
		/// </summary>
		/// <param name="Suspend"></param>
		/// <returns></returns>
		public short Suspend(object o)
		{
			StationModule stationM = StationManage.FindStation(tag_stationName);
			for (int i = 0; i < stationM.arrAxis.Count(); i++)
			{
				if (NewCtrlCardV0.SR_AxisStop((int)stationM.arrAxis[i].tag_MotionCardManufacturer, stationM.arrAxis[i].CardNum, stationM.arrAxis[i].AxisNum) == 0)
				{

				}
				else
				{
					return -1;
				}
			}
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
