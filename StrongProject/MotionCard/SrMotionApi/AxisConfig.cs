using System;


namespace StrongProject
{
	[Serializable]
	public class AxisConfig
	{
		public AxisConfig()
		{

		}
		public AxisConfig(string name)
		{
			axisName = name;
		}
		private int axisIndex = 0;
		/// <summary>
		/// 轴索引号
		/// </summary>
		public int AxisIndex
		{
			get { return axisIndex; }
			set { axisIndex = value; }
		}


		private string axisName = "X";
		/// <summary>
		/// 轴名字
		/// </summary>
		public string AxisName
		{
			get { return axisName; }
			set { axisName = value; }
		}

		private short _CardNum = -1;
		/// <summary>
		/// 卡号
		/// </summary>
		public short CardNum
		{
			get { return _CardNum; }
			set { _CardNum = value; }
		}

		private short _AxisNum = -1;
		/// <summary>
		/// 轴号
		/// </summary>
		public short AxisNum
		{
			get { return _AxisNum; }
			set { _AxisNum = value; }
		}
		//private AxisTypes _AxisType = AxisTypes.None;
		/// <summary>
		/// 轴号
		/// </summary>
		//public AxisTypes AxisType
		//{
		//    get { return _AxisType; }
		//    set { _AxisType = value; }
		//}
		//private ControlCardBase _Motion = null;
		///// <summary>
		///// 轴
		///// </summary>
		//public ControlCardBase Motion
		//{
		//    get { return _Motion; }
		//    set
		//    {
		//        _Motion = value;               
		//    }
		//}




		private double acc = 0.01;            //加速度
		public double Acc
		{
			get { return acc; }
			set { acc = value; }
		}

		private double dec = 0.01;              //减速度
		public double Dec
		{
			get { return dec; }
			set { dec = value; }
		}

		private double speed = 10;           //运行速度
											 /// <summary>
											 /// 运行速度
											 /// </summary>
		public double Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		private double homeSpeedHight = 50;     //回零高速  
		public double HomeSpeedHight
		{
			get { return homeSpeedHight; }
			set { homeSpeedHight = value; }
		}

		private double homeSpeed = 5;          //回零低速度
		public double HomeSpeed
		{
			get { return homeSpeed; }
			set { homeSpeed = value; }
		}

		private double startSpeed = 5;        //起始速度
		public double StartSpeed
		{
			get { return startSpeed; }
			set { startSpeed = value; }
		}

		private double homeMode = 2;           //回零模试
		public double HomeMode
		{
			get { return homeMode; }
			set { homeMode = value; }
		}

		private int homeDir = 0;            //回零方向
		public int HomeDir
		{
			get { return homeDir; }
			set { homeDir = value; }
		}

		private int softLimitEnablel = 1;   //软限位使能
		public int SoftLimitEnablel
		{
			get { return softLimitEnablel; }
			set { softLimitEnablel = value; }
		}

		private double softLimitMinValue;    //软限位最小值
		public double SoftLimitMinValue
		{
			get { return softLimitMinValue; }
			set { softLimitMinValue = value; }
		}

		private double softLimitMaxValue;    //软限位最大值
		public double SoftLimitMaxValue
		{
			get { return softLimitMaxValue; }
			set { softLimitMaxValue = value; }
		}

		private double manualSpeedHigh = 10;   //手动高速
		public double ManualSpeedHigh
		{
			get { return manualSpeedHigh; }
			set { manualSpeedHigh = value; }
		}

		private double manualSpeedNormal = 5; //手动常速
		public double ManualSpeedNormal
		{
			get { return manualSpeedNormal; }
			set { manualSpeedNormal = value; }
		}

		private double manualSpeedLow = 1;    //手动低速
		public double ManualSpeedLow
		{
			get { return manualSpeedLow; }
			set { manualSpeedLow = value; }
		}

		private double orgPos = 0;            //设备电气原点
		public double OrgPos
		{
			get { return orgPos; }
			set { orgPos = value; }
		}

		private double pulsePerLap = 1000;   //驱动器脉冲数
		public double PulsePerLap
		{
			get { return pulsePerLap; }
			set { pulsePerLap = value; }
		}

		private double unitPerLap = 1;      //硬件导程（一圈多少MM）
		public double UnitPerLap
		{
			get { return unitPerLap; }
			set { unitPerLap = value; }
		}

		private double eucf = 1000;        //单位系数（PLUSE-MM 转换关系）
										   /// <summary>
										   /// 单位系数（PLUSE-MM 转换关系）
										   /// </summary>
		public double Eucf
		{
			get { return eucf; }
			set { eucf = value; }
		}

		private int goHomeType = 0;        //回原类型
										   /// <summary>
										   /// 回原类型 0 伺服 1 步进 2 单原点
										   /// </summary>
		public int GoHomeType
		{
			get { return goHomeType; }
			set { goHomeType = value; }
		}

		private int motionType = 0;        //运动类型
										   /// <summary>
										   /// 回原类型 0 JOG 1，
										   /// </summary>
		public int MotionType
		{
			get { return motionType; }
			set { motionType = value; }
		}

		private short jogDir = 0;            //JOG方向
		public short JogDir
		{
			get { return jogDir; }
			set { jogDir = value; }
		}

		[NonSerialized]
		public double dblEncPos = 0;  //编码器位置
		[NonSerialized]
		public double dblPrfPos = 0;  //规划位置
		[NonSerialized]
		public bool blnHomeFinish = false;  //回原成功

		public double intFirstFindOriginDis = 10000;
		public double intSecondFindOriginDis = 10000;
		public double intThreeFindOriginDis = 10000;
		public double tag_accTime = 1;

		/// <summary>
		/// 原点是否高低电平有效
		/// </summary>
		public bool tag_homeIoHighLow = false;

		/// <summary>
		/// 表示属于什么卡，固高，众为兴
		/// </summary>
		public MotionCardManufacturer tag_MotionCardManufacturer;




		/// <summary>
		/// 0：脉冲+脉冲方式		1：脉冲+方向方式
		/// </summary>
		public int tag_CC_value;    //   

		/// <summary>
		/// //0：	正逻辑脉冲			1：	负逻辑脉冲
		/// </summary>
		public int tag_CC_logic;

		/// <summary>
		/// 0：方向输出信号正逻辑	1：方向输出信号负逻辑
		/// </summary>
		public int tag_dir_logic;

		/// <summary>
		/// 正限位有效
		/// </summary>
		public int tag_IoLimtPEnable;

		/// <summary>
		/// 负限位有效
		/// </summary>
		public int tag_IoLimtNEnable;

		/// <summary>
		/// 正负限位高电平有效
		/// </summary>
		public int tag_IoLimtPNHighEnable;

		/// <summary>
		///减速时间
		/// </summary>
		public double tag_delTime = 0.01;

		/// <summary>
		/// 平滑时间
		/// </summary>
		public double tag_S_Time = 0.01;

		/// <summary>
		/// 停止速度
		/// </summary>
		public double tag_StopSpeed;

		/// <summary>
		/// 报警高电平有效
		/// </summary>
		public int tag_IoAlarmNHighEnable;

		/// <summary>
		/// 0 -xy轴， 1表示 Z轴
		/// </summary>
		public int tag_XYZU_Type;



	}
}
