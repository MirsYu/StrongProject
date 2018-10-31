using System;
using System.Collections.Generic;


namespace StrongProject
{
	[Serializable]

	public class StationModule
	{
		public StationModule()
		{
			strStationName = "";
			//arrAxis = new AxisConfig[StationManage.intStationAxisCount];
			//arrInputIo = new IOParameter[StationManage.intStationIoInputCount];
			//arrOutputIo = new IOParameter[StationManage.intStationIoOutPutCount];
			//arrPoint = new PointAggregate[StationManage.intStationPointCount];

			arrAxis = new List<AxisConfig>(StationManage.intStationAxisCount);
			arrInputIo = new List<IOParameter>(StationManage.intStationIoInputCount);
			arrOutputIo = new List<IOParameter>(StationManage.intStationIoOutPutCount);
			arrPoint = new List<PointAggregate>(StationManage.intStationPointCount);
			//for (int i = 0; i < StationManage.intStationPointCount; i++)
			//{
			//    arrPoint.Add(new PointAggregate(strStationName));
			//}
		}

		public string strStationName;//工位名字
		public int intUseAxisCount = 0;//当前方案轴数量
		public int intUseInputIoCount = 0; //当前方案输入IO数量
		public int intUseOutputIoCount = 0;//当前方案输出IO数量
		public int intUsePointCount = 0;//当前方案点位集合数量
										//public AxisConfig[] arrAxis;//工位轴数组
										//public IOParameter[] arrInputIo;//工位输入IO数组
										//public IOParameter[] arrOutputIo;//工位输出IO数组
										//public PointAggregate[] arrPoint;


		public List<AxisConfig> arrAxis;//工位轴数组
		public List<IOParameter> arrInputIo;//工位输入IO数组
		public List<IOParameter> arrOutputIo;//工位输出IO数组
		public List<PointAggregate> arrPoint;

		public int tag_stepId;//步数ID
							  /// <summary>
							  /// 工位类型0，表示是一般工位1,表示是手动防呆配置工位
							  /// </summary>
		public int tag_type = 0;

		/// <summary>
		/// 是否屏蔽本工位 0,步 屏蔽，1屏蔽，复位工位不能屏蔽
		/// </summary>
		public bool tag_Enable;

	}
	public delegate short delegate_PointModule(object o);
	[Serializable]
	public class PointModule
	{
		public PointModule()
		{
			dblPonitValue = 0;
			dblPonitSpeed = 0;
			blnPointEnable = false;
			blnIsSpecialPoint = false;
			dblAcc = 0.5;
			dblDec = 0.5;
			dblAccTime = 0.01;
			dblDecTime = 0.01;


		}

		public PointModule(bool lnPointEnable,//表示点位是否启用
		 bool lnIsSpecialPoint,//表示此点位是否是特殊点位 方便对应轴速度快速改变        
		 double blPonitValue,//点位数据
		 double blPonitSpeed,//点位速度        
		 double blAcc,//加速度
		 double blDec, //减速度
		 double blAccTime,//加速时间
		 double blDecTime,//减速时间
		 double blPonitStartSpeed,//初始速度
		 double b_S_Time,//停止s时间 0-0.05
		 double b_StopSpeed)//停止速度 0-0.05
		{
			blnPointEnable = lnPointEnable;//表示点位是否启用
			blnIsSpecialPoint = lnIsSpecialPoint;//表示此点位是否是特殊点位 方便对应轴速度快速改变        
			dblPonitValue = blPonitValue;//点位数据
			dblPonitSpeed = blPonitSpeed;//点位速度        
			dblAcc = blAcc; //加速度
			dblDec = blDec;  //减速度
			dblAccTime = blAccTime;//加速时间
			dblDecTime = blDecTime;//减速时间
			dblPonitStartSpeed = blPonitStartSpeed;//初始速度
			db_S_Time = b_S_Time;//s端时间 0-0.05
			db_StopSpeed = b_StopSpeed;//s端时间 0-0.05
		}

		public bool blnPointEnable;//表示点位是否启用
		public bool blnIsSpecialPoint;//表示此点位是否是特殊点位 方便对应轴速度快速改变        
		public double dblPonitValue;//点位数据
		public double dblPonitSpeed;//点位速度        
		public double dblAcc; //加速度
		public double dblDec;  //减速度
		public double dblAccTime;//加速时间
		public double dblDecTime;//减速时间
		public double dblPonitStartSpeed;//初始速度

		public double db_S_Time = 0.01;//s端时间 0-0.05
		public double db_StopSpeed = 0;//s端时间 0-0.05
	}
	[Serializable]
	public class PointAggregate
	{
		public PointAggregate(string stationName)
		{
			strName = "P1";
			strStationName = stationName;
			arrPoint = new PointModule[StationManage.intStationAxisCount];
			for (int i = 0; i < StationManage.intStationAxisCount; i++)
			{
				arrPoint[i] = new PointModule();
			}

		}
		public PointAggregate(string stationName, string pointName)
		{
			strName = pointName;
			strStationName = stationName;
			arrPoint = new PointModule[StationManage.intStationAxisCount];
			for (int i = 0; i < StationManage.intStationAxisCount; i++)
			{
				arrPoint[i] = new PointModule();
			}

		}
		// public Work _Worker = null;
		/// <summary>
		/// 点位名称
		/// </summary>
		public string strName; //
							   /// <summary>
							   /// g工位名称 
							   /// </summary>
		public string strStationName;
		public PointModule[] arrPoint;  //点位     
		public bool blnIsSpecialPoint;//表示此点位是否是特殊点位 方便对应轴速度快速改变

		/// <summary>
		/// 步骤名
		/// </summary>
		public int tag_stepName;
		/// <summary>
		/// 输出IO控制
		/// </summary>
		public OutIOParameterPoint tag_OutIo;


		/// <summary>
		/// 判断轴运动是否需要等待1，不许要，0需要
		/// </summary>
		public int tag_isWait;
		/// <summary>
		/// 点位类型，表示此点再轴运行的时候，是否是回原点操作
		/// </summary>
		public int tag_isRest;

		public bool tag_isEnable;

		[NonSerialized]
		/// <summary>
		/// 
		/// </summary>
		public object tag_bObject;
		[NonSerialized]
		/// <summary>
		/// 
		/// </summary>
		public delegate_PointModule tag_BeginFun;

		[NonSerialized]
		/// <summary>
		/// 
		/// </summary>
		public delegate_PointModule tag_AxisMoveFun;

		/// <summary>
		/// 
		/// </summary>
		public object tag_eObject;
		[NonSerialized]
		/// <summary>
		/// 
		/// </summary>
		public delegate_PointModule tag_EndFun;
		/// <summary>
		/// 轴是否停止
		/// </summary>
		public bool tag_isAxisStop;
		/*
         * 
         */
		/// <summary>
		/// 运动类型0  1 相对，
		/// </summary>
		public int tag_motionType = 0;        //运动类型
											  /// <summary>
											  /// 安全管理
											  /// </summary>
		public AxisSafeManage tag_AxisSafeManage;

		/// <summary>
		/// 延时
		/// </summary>
		public int tag_Sleep;
		/// <summary>
		/// 多个POINT点，
		/// </summary>
		public List<object> tag_BeginPointAggregateList;
		/// <summary>
		/// 多个POINT点，
		/// </summary>
		public List<object> tag_EndPointAggregateList;

		public bool tag_EndPointAggregateListIsEnable = true;

		public bool tag_BeginPointAggregateListIsEnable = true;
		/// <summary>
		/// 运动类型，直线差补，
		/// </summary>
		public int tag_MotionLineType = 0;

	}
	[Serializable]
	public class IOParameter
	{
		private string strIoName = "1#";

		public string StrIoName
		{
			get { return strIoName; }
			set { strIoName = value; }
		}

		private short _CardNum = -1;
		public short CardNum
		{
			get { return _CardNum; }
			set { _CardNum = value; }
		}
		private short _IOBit = -1;
		public short IOBit
		{
			get
			{
				return _IOBit;
			}
			set
			{
				_IOBit = value;
			}
		}

		private int _Logic = 0;
		public int Logic
		{
			get { return _Logic; }
			set { _Logic = value; }
		}
		[NonSerialized]
		private bool _BlnOut = false;
		public bool BlnOut
		{
			get
			{
				return _BlnOut;
			}
			set
			{
				_BlnOut = value;
			}
		}
		public string tag_info;
		/// <summary>
		/// io卡的类型
		/// </summary>
		public int tag_MotionCardManufacturer;

		/// <summary>
		/// 安全列表
		/// </summary>
		public PointAggregate tagPointAggregate;
		//防呆配置
		public StationModule tag_StationModule;
		public IOParameter()
		{
		}
		public IOParameter(string ioname)
		{
			strIoName = ioname;
		}
		public IOParameter(short cardNum, short ioBit, int logic)
		{
			_CardNum = cardNum;
			_IOBit = ioBit;
			_Logic = logic;
		}


	}
	[Serializable]
	public class InIOParameterPoint
	{
		/// <summary>
		/// 工位名称
		/// </summary>
		public string tag_name;
		/// <summary>
		/// 超时时间,或者超时
		/// </summary>
		public long tag_IOParameterOutTime;
		/// <summary>
		///IO
		/// </summary>
		public string tag_IOName;
		/// <summary>
		/// 变量
		/// </summary>
		public bool tag_var = true;

	}

	[Serializable]
	public class OutIOParameterPoint
	{

		/// <summary>
		/// 主要是当 tag_InOut1，tag_InOut2 触发后，tag_IniO1看是否满足条件
		/// </summary>
		public InIOParameterPoint tag_IniO1;
		public InIOParameterPoint tag_InOut1;
		public InIOParameterPoint tag_InOut2;

		/// <summary>
		/// 安全IO判断，主要是在运行IO时候，判断本IO是否触发
		/// </summary>
		public InIOParameterPoint tag_IniO2;
		/// <summary>
		/// 名称
		/// </summary>
		public string tag_name;



		public OutIOParameterPoint()
		{
			tag_IniO1 = new InIOParameterPoint();
			tag_IniO2 = new InIOParameterPoint();
			tag_InOut1 = new InIOParameterPoint();
			tag_InOut2 = new InIOParameterPoint();
		}

	}
}
