using System;

namespace StrongProject
{

	[Serializable]
	public class PrivateSave
	{
		/// <summary>
		/// 
		/// </summary>
		public double tag_SN = 1;
		/// <summary>
		/// 左工位公差
		/// </summary>
		public double tag_LeftoffsetX;
		/// <summary>
		/// 左工位公差
		/// </summary>
		public double tag_LeftoffsetY;
		/// <summary>
		/// 右工位公差
		/// </summary>
		public double tag_RightoffsetX;
		/// <summary>
		/// 右工位公差
		/// </summary>
		public double tag_RightoffsetY;

		public double tag_LeftrightoffsetX;
		public double tag_LeftrightoffsetY;
		public double tag_RightrightoffsetX;
		public double tag_RightrightoffsetY;

		public double tag_LeftJMax;
		public double tag_LeftJMin;
		public double tag_RightJMax;
		public double tag_RightJMin;

		public double tag_LeftDisTo1;
		public double tag_LeftDisTo2;
		public double tag_LeftDisTo3;
		public double tag_LeftDisTo4;

		public double tag_RightDisTo1;
		public double tag_RightDisTo2;
		public double tag_RightDisTo3;
		public double tag_RightDisTo4;
		/// <summary>
		/// 安全光栅
		/// </summary>
		public bool tag_safeLightOffOn = true;

		/// <summary>
		/// 门限
		/// </summary>
		public bool tag_safeGateOffOn = true;

		/// <summary>
		/// 暂停是否立即停止，1，不立即停止，0立即停止
		/// </summary>
		public int tag_SuspendType = 0;

		/// <summary>
		/// 只左吸嘴工作，0，只右吸嘴工作，1.左右都工作位2
		/// </summary>
		public int tag_RightOrLeftNozzleWork;


		public int tag_Cam4_x2;
		public int tag_Cam4_y2;
		public int tag_Cam4_a2;
		/// <summary>
		/// ccD_cam23失败拍照的次数
		/// </summary>
		public int tag_cam23_ReFailCount = 3;

		public int tag_Cam4_x1;
		public int tag_Cam4_y1;
		public int tag_Cam4_a1;



		public double fCutterCYTime;
		public double fTestCYTime;
	}
}
