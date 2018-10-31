using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace StrongProject
{
	[Serializable]
	public class Config : BinarySerializer
	{
		public Config()

		{

			if (axisArray == null)
			{
				axisArray = new List<AxisConfig>();

			}
			if (arrWorkStation == null)
			{
				arrWorkStation = new List<StationModule>(StationManage.intStationCount); //工站配置最大数
																						 //arrWorkStation = new StationModule[StationManage.intStationCount];
																						 //for (int i = 0; i < StationManage.intStationCount; i++)
																						 //{
																						 //    arrWorkStation.Add( new StationModule());
																						 //}
			}
			//foreach (AxisConfig axis in axisArray)
			//{
			//    axis = new AxisConfig();
			//}


		}


		#region 点位参数

		private StationManage _Station = null;

		public StationManage Station
		{
			get { return _Station; }
			set { _Station = value; }
		}


		public List<AxisConfig> axisArray; //轴配置（lei）
										   //public StationModule[] arrWorkStation;
		public List<StationModule> arrWorkStation; //工位模块（lei）

		public int intUseStationCount = 0; //使用工站数量（lei）

		/// <summary>
		/// 串口保存的列表
		/// </summary>
		public List<PortParameter> tag_PortParameterList;

		/// <summary>
		/// IP列表保存的列表 
		/// </summary>
		public List<IPAdrr> tag_IPAdrrList;

		/// <summary>
		/// 每个项目独自保存的数据
		/// </summary>
		public PrivateSave tag_PrivateSave;

		/// <summary>
		/// 防呆工位设置
		/// </summary>
		public StationModule tag_safeStationModule;

		/// <summary>
		/// 左工位日志索引ID
		/// </summary>
		public int tag_LeftStationLogIndex;

		/// <summary>
		/// 右工位日志索引ID
		/// </summary>
		public int tag_RightStationLogIndex;

		protected static string GetSetPath(string filename)
		{
			string dir = Path.Combine(Application.StartupPath, "set");
			if (Directory.Exists(dir) == false) //如果没有找到目录
			{
				Directory.CreateDirectory(dir);
			}
			return Path.Combine(dir, filename);
		}

		public bool Save()
		{
			string saveFileLog = "set_" + DateTime.Now.Year + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Day.ToString("00") + "_" + DateTime.Now.Hour.ToString("00") + "_" + DateTime.Now.Minute.ToString("00") + "_" + DateTime.Now.Second.ToString("00") + ".config";
			bool ret = this.Save(GetSetPath(saveFileLog));

			return this.Save(GetSetPath("set.config"));
		}

		public static Config Load()
		{

			Config _Config = Load(GetSetPath("set.config")) as Config;
			if (_Config == null)
			{
				_Config = new Config();
			}
			if (_Config.tag_safeStationModule == null)
			{
				_Config.tag_safeStationModule = new StationModule();
				_Config.tag_safeStationModule.tag_type = 1;
				_Config.tag_safeStationModule.strStationName = "轴手动防呆配置工位";
				_Config.tag_safeStationModule.intUsePointCount = 1;
				_Config.tag_safeStationModule.arrPoint.Add(new PointAggregate(_Config.tag_safeStationModule.strStationName, "p1"));
			}
			if (_Config.axisArray == null)
			{
				_Config.axisArray = new List<AxisConfig>();

			}
			if (_Config.tag_PrivateSave == null)
			{
				_Config.tag_PrivateSave = new PrivateSave();
			}
			if (_Config.tag_IPAdrrList == null)
			{
				_Config.tag_IPAdrrList = new List<IPAdrr>();
				for (int j = 0; j < 2; j++)
				{
					IPAdrr pp = new IPAdrr();
					_Config.tag_IPAdrrList.Add(pp);
				}
			}
			if (_Config.arrWorkStation == null)
			{
				_Config.arrWorkStation = new List<StationModule>(StationManage.intStationCount);
				for (int i = 0; i < StationManage.intStationCount; i++)
				{
					_Config.arrWorkStation[i] = new StationModule();
				}
			}
			if (_Config.tag_PortParameterList == null)
			{
				_Config.tag_PortParameterList = new List<PortParameter>();
				for (int j = 0; j < 4; j++)
				{
					PortParameter pp = new PortParameter();
					_Config.tag_PortParameterList.Add(pp);
				}
				UserControl_LogOut.OutLog("请配置端口", 0);
			}
			return _Config;
		}
		public static Config Load(string fileName, string p)
		{
			Config _Config = Load(fileName) as Config;
			if (_Config == null)
			{
				_Config = new Config();
			}
			if (_Config.tag_safeStationModule == null)
			{
				_Config.tag_safeStationModule = new StationModule();
				_Config.tag_safeStationModule.tag_type = 1;
				_Config.tag_safeStationModule.strStationName = "轴手动防呆配置工位";
				_Config.tag_safeStationModule.intUsePointCount = 1;
				_Config.tag_safeStationModule.arrPoint.Add(new PointAggregate(_Config.tag_safeStationModule.strStationName, "p1"));
			}
			if (_Config.axisArray == null)
			{
				_Config.axisArray = new List<AxisConfig>();

			}
			if (_Config.tag_PrivateSave == null)
			{
				_Config.tag_PrivateSave = new PrivateSave();
			}
			if (_Config.tag_IPAdrrList == null)
			{
				_Config.tag_IPAdrrList = new List<IPAdrr>();
				for (int j = 0; j < 2; j++)
				{
					IPAdrr pp = new IPAdrr();
					_Config.tag_IPAdrrList.Add(pp);
				}
			}
			if (_Config.arrWorkStation == null)
			{
				_Config.arrWorkStation = new List<StationModule>(StationManage.intStationCount);
				for (int i = 0; i < StationManage.intStationCount; i++)
				{
					_Config.arrWorkStation[i] = new StationModule();
				}
			}
			if (_Config.tag_PortParameterList == null)
			{
				_Config.tag_PortParameterList = new List<PortParameter>();
				for (int j = 0; j < 4; j++)
				{
					PortParameter pp = new PortParameter();
					_Config.tag_PortParameterList.Add(pp);
				}
				UserControl_LogOut.OutLog("请配置端口", 0);
			}
			return _Config;

		}
		#endregion
	}



}
