using System;
using System.IO;
using System.Text;
namespace StrongProject
{
	[Serializable]
	public class StationManage
	{
		public StationManage()
		{
		}
		public static Config _Config = null;
		public static int intStationCount = 10;//工站配置最大数
		public static int intStationAxisCount = 10;//单工站运动轴最大配置数
		public static int intStationIoInputCount = 50;//单工站输入IO最大配置数
		public static int intStationIoOutPutCount = 50;//单工站输出IO最大数配置数
		public static int intStationPointCount = 100;//单工站点位最大配置数  



		#region 静态变量
		//速度等级
		private static string speedlevel = "";
		public static string Speedlevel
		{
			get { return StationManage.speedlevel; }
			set { StationManage.speedlevel = value; }
		}
		//速度变量
		private static double speedvalue = 0;
		public static double Speedvalue
		{
			get { return StationManage.speedvalue; }
			set { StationManage.speedvalue = value; }
		}

		//距离模式
		private static string distancemode = "";

		public static string Distancemode
		{
			get { return StationManage.distancemode; }
			set { StationManage.distancemode = value; }
		}



		//第一轴短距离设置
		private static double shortdistanceset = 1;

		public static double Shortdistanceset
		{
			get { return StationManage.shortdistanceset; }
			set { StationManage.shortdistanceset = value; }
		}





		//第一轴长距离设置
		private static double longdistanceset = 10;

		public static double Longdistanceset
		{
			get { return StationManage.longdistanceset; }
			set { StationManage.longdistanceset = value; }
		}





		#region 示教点位值
		private static double movepoint_1 = 0;

		public static double Movepoint_1
		{
			get { return StationManage.movepoint_1; }
			set { StationManage.movepoint_1 = value; }
		}

		private static double movepoint_2 = 0;

		public static double Movepoint_2
		{
			get { return StationManage.movepoint_2; }
			set { StationManage.movepoint_2 = value; }
		}

		private static double movepoint_3 = 0;

		public static double Movepoint_3
		{
			get { return StationManage.movepoint_3; }
			set { StationManage.movepoint_3 = value; }
		}

		private static double movepoint_4 = 0;

		public static double Movepoint_4
		{
			get { return StationManage.movepoint_4; }
			set { StationManage.movepoint_4 = value; }
		}

		private static double movepoint_5 = 0;

		public static double Movepoint_5
		{
			get { return StationManage.movepoint_5; }
			set { StationManage.movepoint_5 = value; }
		}

		private static double movepoint_6 = 0;

		public static double Movepoint_6
		{
			get { return StationManage.movepoint_6; }
			set { StationManage.movepoint_6 = value; }
		}
		#endregion
		#endregion

		/// <summary>
		/// 获取工位的索引号
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public static int getStationIndex(string name)
		{
			int index = 0;
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				if (sm.strStationName == name)
				{
					return index;
				}
				index++;
			}
			return index;
		}
		/// <summary>
		/// 查找工位 返回null 函数异常
		/// </summary>
		/// <param name="stationName"></param>
		/// <returns></returns>
		public static StationModule FindStation(string stationName)
		{
			if (_Config == null)
			{
				return null;
			}
			if (string.IsNullOrEmpty(stationName))
			{
				return null;
			}
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				if (sm.strStationName == stationName)
				{
					return sm;
				}
			}
			return null;
		}

		/// <summary>
		/// 查找轴 返回null 函数异常
		/// </summary>
		/// <param name="stationName"></param>
		/// <param name="axisName"></param>
		/// <returns></returns>
		public static AxisConfig FindAxis(string stationName, string axisName)
		{
			if (string.IsNullOrEmpty(axisName))
			{
				return null;
			}
			if (string.IsNullOrEmpty(axisName))
			{
				return null;
			}
			StationModule stationM = FindStation(stationName);
			if (stationM == null)
			{
				return null;
			}
			foreach (AxisConfig axisC in stationM.arrAxis)
			{
				if (axisC.AxisName == axisName)
				{
					return axisC;
				}
			}
			return null;
		}
		/// <summary>
		/// 查找轴 返回null 函数异常
		/// </summary>
		/// <param name="stationM"></param>
		/// <param name="axisName"></param>
		/// <returns></returns>
		public static AxisConfig FindAxis(StationModule stationM, string axisName)
		{
			if (string.IsNullOrEmpty(axisName))
			{
				return null;
			}
			if (stationM == null)
			{
				return null;
			}
			foreach (AxisConfig axisC in stationM.arrAxis)
			{
				if (axisC.AxisName == axisName)
				{
					return axisC;
				}
			}
			return null;
		}
		/// <summary>
		/// 查找工站输入IO 返回null 函数异常
		/// </summary>
		/// <param name="stationName"></param>
		/// <param name="inputName"></param>
		/// <returns></returns>
		public static IOParameter FindInputIo(string stationName, string inputName)
		{
			if (string.IsNullOrEmpty(inputName))
			{
				return null;
			}

			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (IOParameter ioP in sm.arrInputIo)
				{
					if (ioP.StrIoName == inputName)
					{
						return ioP;
					}
				}
			}
			return null;
		}
		/// <summary>
		/// 查找工站输入IO 返回null 函数异常
		/// </summary>
		/// <param name="stationName"></param>
		/// <param name="inputName"></param>
		/// <returns></returns>
		public static IOParameter FindInputIo(string inputName)
		{


			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (IOParameter ioP in sm.arrInputIo)
				{
					if (ioP.StrIoName == inputName)
					{
						return ioP;
					}
				}
			}
			return null;
		}
		/// <summary>
		///  查找工站输入IO 返回null 函数异常
		/// </summary>
		/// <param name="stationM"></param>
		/// <param name="inputName"></param>
		/// <returns></returns>
		public static IOParameter FindInputIo(StationModule stationM, string inputName)
		{
			if (string.IsNullOrEmpty(inputName))
			{
				return null;
			}
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (IOParameter ioP in sm.arrInputIo)
				{
					if (ioP.StrIoName == inputName)
					{
						return ioP;
					}
				}
			}
			return null;
		}
		/// <summary>
		///  查找工站输出IO 返回null 函数异常
		/// </summary>
		/// <param name="stationName"></param>
		/// <param name="outputName"></param>
		/// <returns></returns>
		public static IOParameter FindOutputIo(string stationName, string outputName)
		{
			if (string.IsNullOrEmpty(outputName))
			{
				return null;
			}
			if (string.IsNullOrEmpty(stationName))
			{
				return null;
			}
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (IOParameter ioP in sm.arrOutputIo)
				{
					if (ioP.StrIoName == outputName)
					{
						return ioP;
					}
				}
			}
			return null;
		}
		/// <summary>
		///  查找工站输出IO 返回null 函数异常
		/// </summary>
		/// <param name="stationM"></param>
		/// <param name="outputName"></param>
		/// <returns></returns>
		public static IOParameter FindOutputIo(StationModule stationM, string outputName)
		{
			if (string.IsNullOrEmpty(outputName))
			{
				return null;
			}


			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (IOParameter ioP in sm.arrOutputIo)
				{
					if (ioP.StrIoName == outputName)
					{
						return ioP;
					}
				}
			}
			return null;
		}
		/// <summary>
		///  查找工站输出IO 返回null 函数异常
		/// </summary>
		/// <param name="stationM"></param>
		/// <param name="outputName"></param>
		/// <returns></returns>
		public static IOParameter FindOutputIo(string outputName)
		{
			if (string.IsNullOrEmpty(outputName))
			{
				return null;
			}

			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (IOParameter ioP in sm.arrOutputIo)
				{
					if (ioP.StrIoName == outputName)
					{
						return ioP;
					}
				}
			}
			return null;
		}
		/// <summary>
		///  查找工站点位 返回null 函数异常
		/// </summary>
		/// <param name="stationM"></param>
		/// <param name="pointName"></param>
		/// <returns></returns>
		public static PointAggregate FindPoint(StationModule stationM, string pointName)
		{

			return pointMotion.FindPoint(stationM.strStationName, pointName, 0);
			if (string.IsNullOrEmpty(pointName))
			{
				return null;
			}
			if (stationM == null)
			{
				return null;
			}
			int i = 0;
			foreach (PointAggregate pointA in stationM.arrPoint)
			{
				if (pointA.strName == pointName && i < stationM.intUsePointCount)
				{
					return pointA;
				}
				i++;
			}
			return null;
		}
		public static bool OpenSevro(string stationName, string axisName, bool OnOff)
		{

			AxisConfig acg;
			return true;

		}

		public static bool AxisStop(string stationName, string axisName)
		{

			AxisConfig acg;
			return true;
		}
		/// <summary>
		/// 急停所有轴
		/// </summary>
		/// <returns></returns>
		public static short StopAllAxis()
		{
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (AxisConfig af in sm.arrAxis)
				{

					if (NewCtrlCardV0.SR_AxisStop((int)af.tag_MotionCardManufacturer, af.CardNum, af.AxisNum) == 0)
					{

					}
					else
					{
						return -1;
					}

				}
			}
			return 0;
		}
		/// <summary>
		/// 使能所有轴
		/// </summary>
		/// <returns></returns>
		public static short OpenSevroAllAxis()
		{
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (AxisConfig af in sm.arrAxis)
				{
					if (!StationManage.OpenSevro(sm.strStationName, af.AxisName, true))
					{
						return -1;
					}

				}
			}
			return 0;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="OldStation"></param>
		/// <param name="newStation"></param>
		public static void StationCopy(string OldStation, string newStation)
		{
			StationModule stationOld = FindStation(OldStation);
			StationModule stationNew = FindStation(newStation);
			foreach (PointAggregate p in stationOld.arrPoint)
			{

				stationNew.arrPoint.Add(new PointAggregate(newStation, p.strName));
				stationNew.intUsePointCount++;
			}
		}
		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public static void GetAllStep(string filename)
		{
			string ret = "";
			int max = 0;
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				ret = ret + sm.strStationName + ",";
				if (max < sm.arrPoint.Count)
					max = sm.arrPoint.Count;
			}
			ret = ret + "\r\n";
			int j = 0;
			for (int m = 0; m < max; m++)
			{
				for (int i = 0; i < _Config.arrWorkStation.Count; i++)
				{
					if (j < _Config.arrWorkStation[i].arrPoint.Count)
					{
						ret = ret + _Config.arrWorkStation[i].arrPoint[j].strName + ",";
					}
					else
					{
						ret = ret + ",";
					}

				}
				ret = ret + "\r\n";
				j++;
			}
			if (!Directory.Exists(filename + ".csv"))
			{
				int index = filename.LastIndexOf("\\");
				string dir = filename.Substring(0, index - 1);
				Directory.CreateDirectory(dir);
			}
			if (!File.Exists(filename))
			{
				StreamWriter sw = File.CreateText(filename + ".csv");

				sw.Flush();
				sw.Close();
			}

			{
				StreamWriter sw = new StreamWriter(filename + ".csv", true, Encoding.GetEncoding("GB2312"));
				sw.Write(ret);
				sw.Flush();
				sw.Close();
			}

		}
		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public static void GetAllOutIO(string filename)
		{
			string ret = "";
			int max = 0;
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				ret = ret + sm.strStationName + ",";
				if (max < sm.arrOutputIo.Count)
					max = sm.arrOutputIo.Count;
			}
			ret = ret + "\r\n";
			int j = 0;
			for (int m = 0; m < max; m++)
			{
				for (int i = 0; i < _Config.arrWorkStation.Count; i++)
				{
					if (j < _Config.arrWorkStation[i].arrOutputIo.Count)
					{
						ret = ret + _Config.arrWorkStation[i].arrOutputIo[j].StrIoName + ",";
					}
					else
					{
						ret = ret + ",";
					}

				}
				ret = ret + "\r\n";
				j++;
			}
			if (!Directory.Exists(filename + ".csv"))
			{
				int index = filename.LastIndexOf("\\");
				string dir = filename.Substring(0, index - 1);
				Directory.CreateDirectory(dir);
			}
			if (!File.Exists(filename))
			{
				StreamWriter sw = File.CreateText(filename + ".csv");

				sw.Flush();
				sw.Close();
			}

			{
				StreamWriter sw = new StreamWriter(filename + ".csv", true, Encoding.GetEncoding("GB2312"));
				sw.Write(ret);
				sw.Flush();
				sw.Close();
			}

		}
		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public static void GetAllAxis(string filename)
		{
			string ret = "";
			int max = 0;
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				foreach (AxisConfig axis in sm.arrAxis)
				{
					ret = ret + sm.strStationName + ",";
					foreach (System.Reflection.PropertyInfo p in axis.GetType().GetProperties())
					{
						ret = ret + p.GetValue((object)axis, null) + ",";
					}
					ret = ret + "\r\n";

				}
			}

			if (!Directory.Exists(filename + ".csv"))
			{
				int index = filename.LastIndexOf("\\");
				string dir = filename.Substring(0, index - 1);
				Directory.CreateDirectory(dir);
			}
			if (!File.Exists(filename))
			{
				StreamWriter sw = File.CreateText(filename + ".csv");

				sw.Flush();
				sw.Close();
			}

			{
				StreamWriter sw = new StreamWriter(filename + ".csv", true, Encoding.GetEncoding("GB2312"));
				sw.Write(ret);
				sw.Flush();
				sw.Close();
			}

		}


		public static void OutIoImport(string strIo)
		{
			try
			{
				string[] str2 = System.Text.RegularExpressions.Regex.Split(strIo, "\r\n");
				string[] title = System.Text.RegularExpressions.Regex.Split(str2[0], ",");
				for (int i = 0; i < title.Length; i++)
				{
					StationModule stationOld = FindStation(title[i]);
					for (int j = 1; j < str2.Length; j++)
					{
						string[] IO = System.Text.RegularExpressions.Regex.Split(str2[j], ",");
						if (stationOld != null)
						{
							if (i < IO.Length && FindOutputIo(IO[i]) == null && IO[i] != "")
							{
								stationOld.arrOutputIo.Add(new IOParameter(IO[i]));
								stationOld.intUseOutputIoCount++;
							}
						}
					}

				}
			}
			catch (Exception ex)
			{
				MessageBoxLog.Show(ex.Message);
			}

		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="strIo"></param>
		public static void AxisImport(string strIo)
		{
			try
			{
				string[] str2 = System.Text.RegularExpressions.Regex.Split(strIo, "\r\n");



				for (int i = 0; i < str2.Length; i++)
				{
					string[] axis = System.Text.RegularExpressions.Regex.Split(str2[i], ",");
					for (int j = 1; j < axis.Length; j++)
					{
						StationModule stationOld = FindStation(axis[0]);
						if (stationOld == null && axis[j] != "")
						{
							_Config.arrWorkStation.Add(new StationModule());
							_Config.arrWorkStation[_Config.arrWorkStation.Count - 1].strStationName = axis[0];
							_Config.intUseStationCount++;
						}
						if (stationOld != null)
						{
							AxisConfig ax = FindAxis(stationOld.strStationName, axis[2]);
							if (ax == null && axis[j] != "")
							{
								ax = new AxisConfig(axis[j]);
								stationOld.arrAxis.Add(ax);
								stationOld.intUseAxisCount++;
							}
							int n = 1;
							foreach (System.Reflection.PropertyInfo p in ax.GetType().GetProperties())
							{

								switch (p.PropertyType.Name)
								{
									case "Int32":
										p.SetValue((object)ax, Int32.Parse(axis[n]), null);
										break;
									case "Int16":
										p.SetValue((object)ax, Int16.Parse(axis[n]), null);
										break;
									case "String":
										p.SetValue((object)ax, axis[n], null);
										break;
									case "Double":
										p.SetValue((object)ax, Double.Parse(axis[n]), null);
										break;
								}
								//  
								n++;
							}

						}
					}

				}
			}
			catch (Exception ex)
			{
				MessageBoxLog.Show(ex.Message);
			}

		}

		/// <summary>
		/// 
		/// </summary>
		public static void StepDellAll()
		{
			try
			{
				foreach (StationModule stationOld in _Config.arrWorkStation)
				{
					if (stationOld != null)
					{

						stationOld.arrPoint.RemoveRange(0, stationOld.arrPoint.Count); ;
						stationOld.intUsePointCount = 0;

					}
				}

			}
			catch (Exception ex)
			{
				MessageBoxLog.Show(ex.Message);
			}

		}
		public static void StepImport(string strIo)
		{
			try
			{
				string[] str2 = System.Text.RegularExpressions.Regex.Split(strIo, "\r\n");
				string[] title = System.Text.RegularExpressions.Regex.Split(str2[0], ",");

				for (int i = 0; i < title.Length; i++)
				{
					StationModule stationOld = FindStation(title[i]);
					if (stationOld == null && title[i] != "")
					{
						_Config.arrWorkStation.Add(new StationModule());
						_Config.arrWorkStation[_Config.arrWorkStation.Count - 1].strStationName = title[i];
						_Config.intUseStationCount++;
					}
					for (int n = 1; n < str2.Length; n++)
					{
						string[] IO = System.Text.RegularExpressions.Regex.Split(str2[n], ",");
						if (stationOld != null)
						{
							if (i < IO.Length && FindPoint(stationOld, IO[i]) == null && IO[i] != "")
							{
								stationOld.arrPoint.Add(new PointAggregate(stationOld.strStationName, IO[i]));
								stationOld.intUsePointCount++;
							}
						}


					}





				}
			}
			catch (Exception ex)
			{
				MessageBoxLog.Show(ex.Message);
			}

		}
		/// <summary>
		/// IO dao ru
		/// </summary>
		/// <param name="strIo"></param>
		public static void InIoImport(string strIo)
		{
			try
			{
				string[] str2 = System.Text.RegularExpressions.Regex.Split(strIo, "\r\n");
				string[] title = System.Text.RegularExpressions.Regex.Split(str2[0], ",");
				for (int i = 0; i < title.Length; i++)
				{
					StationModule stationOld = FindStation(title[i]);
					for (int j = 1; j < str2.Length; j++)
					{
						string[] IO = System.Text.RegularExpressions.Regex.Split(str2[j], ",");
						if (stationOld != null)
						{
							if (i < IO.Length && FindInputIo(IO[i]) == null && IO[i] != "")
							{
								stationOld.arrInputIo.Add(new IOParameter(IO[i]));
								stationOld.intUseInputIoCount++;
							}
						}
					}

				}
			}
			catch (Exception ex)
			{
				MessageBoxLog.Show(ex.Message);
			}

		}
		/// <summary>
		/// IO dao ru 
		/// </summary>
		/// <param name="strIo"></param>
		public static void InIoCardImport(string strIo)
		{
			try
			{
				string[] str2 = System.Text.RegularExpressions.Regex.Split(strIo, "\r\n");
				string[] title = System.Text.RegularExpressions.Regex.Split(str2[0], ",");
				StationModule stationOld = FindStation(title[0]);
				if (stationOld == null && title[0] != "")
				{
					stationOld = new StationModule();
					_Config.arrWorkStation.Add(stationOld);
					_Config.arrWorkStation[_Config.arrWorkStation.Count - 1].strStationName = title[0];
					_Config.intUseStationCount++;
				}
				for (int j = 1; j < str2.Length; j++)
				{
					string[] IO = System.Text.RegularExpressions.Regex.Split(str2[j], ",");
					if (FindInputIo(IO[0]) == null && IO[0] != "")
					{
						IOParameter newIo = new IOParameter(IO[0]);
						newIo.CardNum = short.Parse(IO[1]);
						newIo.IOBit = short.Parse(IO[2]);
						stationOld.arrInputIo.Add(newIo);
						stationOld.intUseInputIoCount++;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBoxLog.Show(ex.Message);
			}

		}
		/// <summary>
		/// IO dao ru 
		/// </summary>
		/// <param name="strIo"></param>
		public static void OutIoCardImport(string strIo)
		{
			try
			{
				string[] str2 = System.Text.RegularExpressions.Regex.Split(strIo, "\r\n");
				string[] title = System.Text.RegularExpressions.Regex.Split(str2[0], ",");
				StationModule stationOld = FindStation(title[0]);
				if (stationOld == null && title[0] != "")
				{
					stationOld = new StationModule();
					_Config.arrWorkStation.Add(stationOld);
					_Config.arrWorkStation[_Config.arrWorkStation.Count - 1].strStationName = title[0];
					_Config.intUseStationCount++;
				}
				for (int j = 1; j < str2.Length; j++)
				{
					string[] IO = System.Text.RegularExpressions.Regex.Split(str2[j], ",");
					if (FindOutputIo(IO[0]) == null && IO[0] != "")
					{
						IOParameter newIo = new IOParameter(IO[0]);
						newIo.CardNum = short.Parse(IO[1]);
						newIo.IOBit = short.Parse(IO[2]);
						stationOld.arrOutputIo.Add(newIo);
						stationOld.intUseOutputIoCount++;
					}
				}
			}
			catch (Exception ex)
			{
				MessageBoxLog.Show(ex.Message);
			}

		}
		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public static void GetAllInIO(string filename)
		{
			string ret = "";
			int max = 0;
			foreach (StationModule sm in _Config.arrWorkStation)
			{
				ret = ret + sm.strStationName + ",";
				if (max < sm.arrInputIo.Count)
					max = sm.arrInputIo.Count;
			}
			ret = ret + "\r\n";
			int j = 0;
			for (int m = 0; m < max; m++)
			{
				for (int i = 0; i < _Config.arrWorkStation.Count; i++)
				{
					if (j < _Config.arrWorkStation[i].arrInputIo.Count)
					{
						ret = ret + _Config.arrWorkStation[i].arrInputIo[j].StrIoName + ",";
					}
					else
					{
						ret = ret + ",";
					}

				}
				ret = ret + "\r\n";
				j++;
			}
			if (!Directory.Exists(filename + ".csv"))
			{
				int index = filename.LastIndexOf("\\");
				string dir = filename.Substring(0, index - 1);
				Directory.CreateDirectory(dir);
			}
			if (!File.Exists(filename))
			{
				StreamWriter sw = File.CreateText(filename + ".csv");

				sw.Flush();
				sw.Close();
			}

			{
				StreamWriter sw = new StreamWriter(filename + ".csv", true, Encoding.GetEncoding("GB2312"));
				sw.Write(ret);
				sw.Flush();
				sw.Close();
			}

		}


		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public static void saveFile(string filename, string content)
		{
			if (!Directory.Exists(filename + ".csv"))
			{
				int index = filename.LastIndexOf("\\");
				string dir = filename.Substring(0, index - 1);
				Directory.CreateDirectory(dir);
			}
			if (!File.Exists(filename))
			{
				StreamWriter sw = File.CreateText(filename + ".csv");
				sw.Write(content);
				sw.Flush();
				sw.Close();
			}
			else
			{
				StreamWriter sw = new StreamWriter(filename + ".csv", true, Encoding.GetEncoding("GB2312"));
				sw.Write(content);
				sw.Flush();
				sw.Close();
			}
		}
		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public static string ReadFile(string filename)
		{
			StreamReader sw = new StreamReader(filename, Encoding.GetEncoding("GB2312"), true);
			string ret = sw.ReadToEnd();
			sw.Close();
			return ret;
		}


	}
}
