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

		public EmptyRun tag_EmptyRun;


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

			/*屏蔽此处的作用
			 * 当手动调试时，将此处屏蔽，以免不小心点到复位按钮，导致整机复位，出现撞机的情况
			 * 待所有轴都能单独回零，并且没有其他干扰时，可以解除注释，使用整体复位功能
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

			tag_EmptyRun = new EmptyRun(_Work);
			workObject.Add(tag_EmptyRun);

			axisCheck = new AxisCheck(_Work);
			workObject.Add(axisCheck);
		}

	}
}
