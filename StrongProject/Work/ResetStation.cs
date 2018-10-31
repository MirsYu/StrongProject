using System.Threading;
namespace StrongProject
{
	public class ResetStation : workBase
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
		/// ���캯��
		/// </summary>
		/// <param name="ConstructCreate"></param>
		/// <returns></returns>
		public ResetStation(Work _Work)
		{
			tag_Work = _Work;
			tag_stationName = "�ܸ�λ";
			tag_isRestStation = 1;
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
		/// �ص�����λ����2��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step2"></param>
		/// <returns></returns>
		public short Step2(object o)
		{

			return 0;
		}
		/// <summary>
		/// ����գ���3��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step3"></param>
		/// <returns></returns>
		public short Step3(object o)
		{

			return 0;
		}
		/// <summary>
		/// ��λ��������4��Ƕ��Ĵ��룬����0 ��ʾ�ɹ�
		/// </summary>
		/// <param name="Step4"></param>
		/// <returns></returns>
		public short Step4(object o)
		{

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
			PointAggregate _Step2 = pointMotion.FindPoint(tag_stationName, "�ص�����λ", 2);
			if (_Step2 == null)
			{
				return -1;
			}
			_Step2.tag_BeginFun = Step2;
			PointAggregate _Step3 = pointMotion.FindPoint(tag_stationName, "�����", 3);
			if (_Step3 == null)
			{
				return -1;
			}
			_Step3.tag_BeginFun = Step3;
			PointAggregate _Step4 = pointMotion.FindPoint(tag_stationName, "��λ����", 4);
			if (_Step4 == null)
			{
				return -1;
			}
			_Step4.tag_BeginFun = Step4;
			return 0;
		}
		/// <summary>
		/// �жϺ���
		/// </summary>
		/// <param name="Suspend"></param>
		/// <returns></returns>
		public short Suspend(object o)
		{
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
				if (tag_manual.tag_step == 0)
				{
					tag_isWork = pointMotion.StationRun(tag_stationName, tag_manual);
					tag_manual.tag_stepName = 0;
				}
			}
		}
	}
}
