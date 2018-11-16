using log4net;
using System.Threading;
using System.Windows.Forms;

namespace StrongProject
{
	public class ResetStation : workBase
	{
		private static readonly ILog log = LogManager.GetLogger("ResetStation.cs");

		public Work tag_Work;
		public JSerialPort tag_Assemblyline;

		public ResetStation(Work _Work)
		{
			tag_Work = _Work;
			tag_stationName = "�ܸ�λ";
			tag_Assemblyline = _Work.tag_JSerialPort[0];
			tag_isRestStation = 1;
		}
		
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

		public short Init()
		{

			PointAggregate _Step0 = pointMotion.FindPoint(tag_stationName, "��λ��ʼ", 0);
			if (_Step0 == null)
			{
				return -1;
			}
			_Step0.tag_BeginFun = Step0;
			PointAggregate _Step1 = pointMotion.FindPoint(tag_stationName, "���ϼ��", 1);
			if (_Step1 == null)
			{
				return -1;
			}
			_Step1.tag_BeginFun = Step1;
			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "Ƥ��,������ֹͣ", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;
			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "Z,R���ԭ", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_BeginFun = Step3;
			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "�����,����ԭλ", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			PointAggregate _Step5 = pointMotion.FindPoint(tag_stationName, "X,Y���ԭ", 5);
			if (_Step5 == null)
			{
				return -1;
			}
			_Step5.tag_BeginFun = Step5;
			PointAggregate _Step6 = pointMotion.FindPoint(tag_stationName, "��λ����", 6);
			if (_Step6 == null)
			{
				return -1;
			}
			_Step6.tag_BeginFun = Step6;

			#region ��⴮�ڴ���
			if (tag_Assemblyline.tag_PortParameter.tag_name != "Ƥ����")
			{
				return -1;
			}
			#endregion
			return 0;
		}

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
		}

		public short Suspend(object o)
		{
			return 0;
		}

		public short Continue(object o)
		{

			return 0;
		}

		public bool exit()
		{
			tag_manual.tag_stepName = 100000;
			tag_isWork = 0;
			return true;
		}
		
		public short Step0(object o)
		{
			tag_isWork = 1;
			return 0;
		}

		public short Step1(object o)
		{
			// ���ϼ��
			// 1.�����ͷ���Ƿ�����
			// 2.���Ƥ�������Ƿ�����
			if (!CheckLineProduce())
			{
				if (MessageBoxLog.Show("Ƥ����������,��ȡ��", "ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				{
					// ������һ��
					--tag_manual.tag_stepName;
					return 0;
				}
				else
					return -1;
			}
			return 0;
		}

		public short Step2(object o)
		{
			// Ƥ��,������ֹͣ
			// 1.Ƥ����(Assembly line)ֹͣ
			for (int i = 0; i < 6; i++)
			{
				if (!StopLine(i + 1))
				{
					if (MessageBoxLog.Show("��ˮ��" + i + "ֹͣʧ��", "ȷ��", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
					{
						// ������һ��
						--tag_manual.tag_stepName;
						return 0;
					}
					else
						return -1;
				}
			}
			// 2.������ֹͣ
			#region ����������ֹͣ���ҷ����ʼ��
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "��������", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "���Ϸ���", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "��������", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "���Ϸ���", 0);
			#endregion
			return 0;
		}

		public short Step3(object o)
		{
			// Z,R���ԭ
			return 0;
		}

		public short Step4(object o)
		{
			// �����,����ԭλ
			// 1.�����
			#region �����
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "����ǰ���", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "���Ϻ����", 0);

			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "������", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "�շ���", 0);

			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "��������", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "��������", 0);

			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "��������", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "��������", 0);
			#endregion
			// 2.���׻�ԭλ,������Ƿ���Ļص�λ
			#region ���׻�ԭ
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "��������", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "��������", 0);

			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "������������", 0);
			NewCtrlCardV0.SetOutputIoBitStatus(tag_stationName, "������������", 0);
			#endregion
			return 0;
		}

		public short Step5(object o)
		{
			// X,Y���ԭ
			return 0;
		}

		public short Step6(object o)
		{
			// ��λ����
			return 0;
		}

		/// <summary>
		/// �����ˮ�����Ƿ��в�Ʒ
		/// </summary>
		/// <returns></returns>
		private bool CheckLineProduce()
		{
			bool bTemp = false;
			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "���Ƥ������", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("��ȡ���Ƥ������״̬ʧ��!", 0);
				log.Warn("��ȡ���Ƥ������״̬ʧ��");
				return false;
			}
			if (bTemp)	return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "���Ƥ������", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("��ȡ���Ƥ������״̬ʧ��!", 0);
				log.Warn("��ȡ���Ƥ������״̬ʧ��");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "�ж�Ƥ������", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("��ȡ�ж�Ƥ������״̬ʧ��!", 0);
				log.Warn("��ȡ�ж�Ƥ������״̬ʧ��");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "�ж�Ƥ������", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("��ȡ�ж�Ƥ������״̬ʧ��!", 0);
				log.Warn("��ȡ�ж�Ƥ������״̬ʧ��");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "�ж�Ƥ������", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("��ȡ�ж�Ƥ������״̬ʧ��!", 0);
				log.Warn("��ȡ�ж�Ƥ������״̬ʧ��");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "�Ҷ�Ƥ������", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("��ȡ�Ҷ�Ƥ������״̬ʧ��!", 0);
				log.Warn("��ȡ�Ҷ�Ƥ������״̬ʧ��");
				return false;
			}
			if (bTemp) return false;

			if (NewCtrlCardV0.GetInputIoBitStatus(tag_stationName, "�Ҷ�Ƥ������", out bTemp) != 0)
			{
				UserControl_LogOut.OutLog("��ȡ�Ҷ�Ƥ������״̬ʧ��!", 0);
				log.Warn("��ȡ�Ҷ�Ƥ������״̬ʧ��");
				return false;
			}
			if (bTemp) return false;

			return true;
		}

		/// <summary>
		/// ֹͣ������ˮ��
		/// </summary>
		/// <param name="index">��ˮ������</param>
		/// <returns></returns>
		private bool StopLine(int index)
		{
			string lineStopCmd = "06 00 28 00 00";
			byte[] bCmd;
			byte[] bResult;
			bCmd = JSerialPort.CreateLineCode(lineStopCmd, index);
			bResult = tag_Assemblyline.sendCommand(bCmd, bCmd.Length, 20000);
			for (int i = 0; i < bCmd.Length; i++)
			{
				if (bCmd[i] != bResult[i])
				{
					log.Warn("ֹͣ" + index + "��ˮ��ʧ��,�������:" + bResult[2].ToString());
					return false;
				}
			}
			return true;
		}
	}
}
