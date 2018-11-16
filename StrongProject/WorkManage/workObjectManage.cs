using System.Collections.Generic;

namespace StrongProject
{
	public class workObjectManage
	{
		/// <summary>
		/// ExcisionStation
		/// </summary>
		/// <param name="totalRest"></param>
		public ExcisionStation tag_ExcisionStation;

		/// <summary>
		/// RightStation
		/// </summary>
		/// <param name="totalRest"></param>
		public RightStation tag_RightStation;

		/// <summary>
		/// LeftStation
		/// </summary>
		/// <param name="totalRest"></param>
		public LeftStation tag_LeftStation;

		/// <summary>
		/// ResetStation
		/// </summary>
		/// <param name="totalRest"></param>
		public ResetStation tag_ResetStation;


		/// <summary>
		/// LeftCCD
		/// </summary>
		/// <param name="totalRest"></param>
		public LeftCCD tag_LeftCCD;



		public AxisCheck axisCheck;

		/// <summary>
		/// tag_workObject
		/// </summary>
		public List<object> tag_workObject;

		public workObjectManage(Work _Work, List<object> workObject)
		{

			/*���δ˴�������
			 * ���ֶ�����ʱ�����˴����Σ����ⲻС�ĵ㵽��λ��ť������������λ������ײ�������
			 * �������ᶼ�ܵ������㣬����û����������ʱ�����Խ��ע�ͣ�ʹ�����帴λ����
			*/


			//tag_ExcisionStation = new ExcisionStation(_Work);
			//workObject.Add(tag_ExcisionStation);

			//tag_RightStation = new RightStation(_Work);
			//workObject.Add(tag_RightStation);

			//tag_LeftStation = new LeftStation(_Work);
			//workObject.Add(tag_LeftStation);




			//tag_LeftCCD = new LeftCCD(_Work);
			//workObject.Add(tag_LeftCCD);

			tag_ResetStation = new ResetStation(_Work);
			workObject.Add(tag_ResetStation);

			axisCheck = new AxisCheck(_Work);
			workObject.Add(axisCheck);
		}

	}
}
