using System;
using System.Collections.Generic;

namespace StrongProject
{
	[Serializable]
	/// <summary>
	/// 
	/// </summary>
	public class AxisSafe
	{
		/// <summary>
		/// 轴ID
		/// </summary>
		public int tag_AxisId = 0;
		/// <summary>
		/// 最大值 
		/// </summary>
		public double tag_max = 99999999999;
		/// <summary>
		/// 最小值
		/// </summary>
		public double tag_min = -99999999999;
		public short tag_type = 0;
		/// <summary>
		/// card 卡号，axisId 轴号
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axisId"></param>
		public AxisSafe(short type, short card, short axisId)
		{
			tag_AxisId = type * 1000 + card * 100 + axisId;
		}
		/// <summary>
		///设置安全位   max z最大安全位，min 最小安全位
		/// </summary>
		/// <param name="max"></param>
		/// <param name="min"></param>
		public void SetSafe(double max, double min)
		{
			tag_max = max;
			tag_min = min;
		}
		/// <summary>
		/// 判断是否在安全位
		/// </summary>
		/// <returns></returns>
		public bool IsSafe(double coordinate)
		{
			bool ret = false;
			if (coordinate <= tag_max && coordinate >= tag_min)
			{
				ret = true;
			}
			else
			{

			}
			return ret;
		}
	}
	[Serializable]
	public class AxisSafeManage
	{
		/// <summary>
		/// 安全列表
		/// </summary>
		public List<AxisSafe> tag_AxisSafeList;

		/// <summary>
		/// io安全控制
		/// </summary>
		public List<OutIOParameterPoint> tag_InIoList;

		/// <summary>
		/// 防呆名
		/// </summary>
		public string tag_name;

		/// <summary>
		/// 是否是并行执行
		/// </summary>
		public bool tag_isAndCheck = false;
		/// <summary>
		/// 
		/// </summary>
		public AxisSafeManage()
		{
			tag_AxisSafeList = new List<AxisSafe>();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="point"></param>
		public AxisSafeManage(PointAggregate point)
		{
			tag_AxisSafeList = new List<AxisSafe>();
			StationModule sm = StationManage.FindStation(point.strStationName);
			if (sm == null)
			{
				UserControl_LogOut.OutLog("无" + point.strStationName, 0);
				return;
			}
			foreach (AxisConfig ac in sm.arrAxis)
			{
				Add((short)ac.tag_MotionCardManufacturer, ac.CardNum, ac.AxisNum);
			}
		}
		/// <summary>
		/// 添加一个轴安全区
		/// </summary>
		/// <param name="asf"></param>
		/// <returns></returns>
		public bool Add(AxisSafe asf)
		{
			foreach (AxisSafe af in tag_AxisSafeList)
			{
				if (af.tag_AxisId == asf.tag_AxisId)
				{
					return false;
				}
			}
			tag_AxisSafeList.Add(asf);
			return false;
		}
		/// <summary>
		/// 添加一个轴安全区
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axisId"></param>
		/// <returns></returns>
		public AxisSafe Add(short type, short card, short axisId)
		{
			AxisSafe asf = new AxisSafe(type, card, axisId);
			Add(asf);
			return asf;
		}
		/// <summary>
		/// 设置一card 的axisId 轴的安全区
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axisId"></param>
		/// <param name="max"></param>
		/// <param name="min"></param>
		/// <returns></returns>
		public bool SetAxisSafe(short type, short card, short axisId, double max, double min)
		{
			Add(type, card, axisId);
			AxisSafe asf = GetAxisSafe(type, card, axisId);
			asf.tag_max = max;
			asf.tag_min = min;
			return true;
		}
		/// <summary>
		/// 获取一个安全区 card 卡 axisId  轴
		/// </summary>
		/// <param name="card"></param>
		/// <param name="axisId"></param>
		/// <returns></returns>
		public AxisSafe GetAxisSafe(short type, short card, short axisId)
		{
			foreach (AxisSafe asf in tag_AxisSafeList)
			{
				int aid = type * 1000 + card * 100 + axisId;
				if (asf.tag_AxisId == aid)
				{
					return asf;
				}
			}
			return null;
		}
		/// <summary>
		/// 添加一个控制
		/// </summary>
		/// <param name="ac"></param>
		public void AddIo(OutIOParameterPoint io)
		{
			foreach (OutIOParameterPoint _io in tag_InIoList)
			{
				if (_io.tag_IniO2.tag_IOName == io.tag_IniO2.tag_IOName)
				{
					return;
				}
			}

			tag_InIoList.Add(io);
		}
		/// <summary>
		/// 移除一个控制
		/// </summary>
		/// <param name="ac"></param>
		public void MoveIo(OutIOParameterPoint io)
		{
			foreach (OutIOParameterPoint _io in tag_InIoList)
			{
				if (_io.tag_IniO2.tag_IOName == io.tag_IniO2.tag_IOName)
				{
					tag_InIoList.Remove(_io);
					return;
				}
			}
		}
		/// <summary>
		/// 判断当前位置是否是本点的安全区 安全返回TRUE
		/// </summary>
		/// <returns></returns>
		public bool PointIsSafe(PointAggregate point)
		{
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				foreach (AxisConfig ac in sm.arrAxis)
				{
					AxisSafe asf = GetAxisSafe((short)ac.tag_MotionCardManufacturer, ac.CardNum, ac.AxisNum);
					if (asf != null)
					{
						double poen_ac = 0;
						NewCtrlCardV0.SR_GetPrfPos((int)ac.tag_MotionCardManufacturer, ac.CardNum, ac.AxisNum, ref poen_ac);
						poen_ac = poen_ac / ac.Eucf;

						if (!asf.IsSafe(poen_ac))
						//if (!asf.IsSafe(ac.dblEncPos))
						{
							return false;
						}
					}
				}
			}
			return true;
		}



		/// <summary>
		/// 判断当前轴运动是否是在PA 的安全 配置列表里面(pa.tag_AxisSafeManage.tag_AxisSafeList)  在返回TRUE
		/// </summary>
		/// <param name="_ac"></param>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static bool isAxisHavePoint(AxisConfig _ac, PointAggregate pa, double pos)
		{
			int i = 0;
			if (pa == null || pa.tag_AxisSafeManage == null || pa.tag_AxisSafeManage.tag_AxisSafeList == null)
			{
				return false;
			}
			while (i < pa.tag_AxisSafeManage.tag_AxisSafeList.Count)
			{
				AxisSafe currac = pa.tag_AxisSafeManage.tag_AxisSafeList[i];
				if ((int)_ac.tag_MotionCardManufacturer * 1000 + _ac.CardNum * 100 + _ac.AxisNum == currac.tag_AxisId)
				{
					double poen_ac = 0.0;
					NewCtrlCardV0.SR_GetPrfPos((int)_ac.tag_MotionCardManufacturer, _ac.CardNum, _ac.AxisNum, ref poen_ac);
					poen_ac = poen_ac / _ac.Eucf;
					if (i == 0)
					{
						if (poen_ac <= currac.tag_max && poen_ac >= currac.tag_min)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				}
				i++;

			}
			return false;
		}

		/// <summary>
		/// 判断当前轴运动是否是在PA 的安全 配置列表里面(pa.tag_AxisSafeManage.tag_AxisSafeList)  在返回TRUE
		/// </summary>
		/// <param name="_ac"></param>
		/// <param name="pa"></param>
		/// <returns></returns>
		public static bool isIOAxisHavePoint(AxisConfig _ac, PointAggregate pa, double pos)
		{
			int i = 0;
			if (pa == null || pa.tag_AxisSafeManage == null || pa.tag_AxisSafeManage.tag_AxisSafeList == null)
			{
				return false;
			}
			while (i < pa.tag_AxisSafeManage.tag_AxisSafeList.Count)
			{
				AxisSafe currac = pa.tag_AxisSafeManage.tag_AxisSafeList[i];
				if ((int)_ac.tag_MotionCardManufacturer * 1000 + _ac.CardNum * 100 + _ac.AxisNum == currac.tag_AxisId)
				{
					double poen_ac = 0.0;
					NewCtrlCardV0.SR_GetPrfPos((int)_ac.tag_MotionCardManufacturer, _ac.CardNum, _ac.AxisNum, ref poen_ac);
					//  if (i == 0)
					{
						poen_ac = poen_ac / _ac.Eucf;
						if (poen_ac <= currac.tag_max && poen_ac >= currac.tag_min)
						{
							return true;
						}
						else
						{
							return false;
						}
					}
				}
				i++;

			}
			return false;
		}
		/// <summary>
		/// 判断当前轴运动是否是本点的安全区 安全返回TRUE
		/// </summary>
		/// <returns></returns>
		public static bool AxisIsSafe(StationModule sm, AxisConfig _ac, double offset, double pos)
		{
			bool ret = true;
			int i = 0;
			int f = 0;
			foreach (PointAggregate pa in sm.arrPoint)
			{
				if (isAxisHavePoint(_ac, pa, pos))
				{
					if (pa.tag_AxisSafeManage.PointIsSafe(pa))
					{
						return false;
					}
					f = 1;

				}

			}

			if (ret == false)
			{

				UserControl_LogOut.OutLog(_ac.AxisName + "运动不安全", 0);

			}
			return ret;

		}


		/// <summary>
		/// 判断当前轴运动是否是本点的安全区 安全返回TRUE
		/// </summary>
		/// <returns></returns>
		public static bool IoAxisIsSafe(StationModule sm, AxisConfig _ac, double offset, double pos)
		{
			bool ret = true;
			int i = 0;
			int f = 0;
			if (sm.arrPoint.Count == 0)
			{
				return true;
			}
			foreach (PointAggregate pa in sm.arrPoint)
			{
				if (isIOAxisHavePoint(_ac, pa, pos))
				{
					if (pa.tag_AxisSafeManage.PointIsSafe(pa))
					{
						if (IOIsSafe(pa))
						{
							ret = true;
							// break;
						}
						else
						{
							ret = false;
							break;
						}
					}
					f = 1;

				}

			}

			if (ret == false)
			{

				UserControl_LogOut.OutLog(_ac.AxisName + "运动不安全", 0);

			}
			return ret;

		}

		/// <summary>
		/// 判断当前位置是否是所在按区域的时候，对应IO是否安全，安全返回TRUE,否则FLASE
		/// </summary>
		/// <returns></returns>
		public static bool IOIsSafe(PointAggregate pa)
		{
			if (pa == null || pa.tag_AxisSafeManage == null || pa.tag_AxisSafeManage.tag_InIoList == null || pa.tag_AxisSafeManage.tag_InIoList.Count == 0)
			{
				return true;
			}
			foreach (OutIOParameterPoint io in pa.tag_AxisSafeManage.tag_InIoList)
			{
				bool var = false;
				// io.tag_IniO1.tag_name
				if (io != null && io.tag_IniO2 != null && NewCtrlCardV0.GetInputIoBitStatus(io.tag_IniO2.tag_name, io.tag_IniO2.tag_IOName, out var) == 0)
				{
					if (var == io.tag_IniO2.tag_var)
					{

					}
					else
					{
						UserControl_LogOut.OutLog("IO:<" + io.tag_IniO2.tag_IOName + "  >有安全隐患，可能撞机，请注意", 0);
						return false;
					}
				}

			}

			return true;
		}


	}
}
