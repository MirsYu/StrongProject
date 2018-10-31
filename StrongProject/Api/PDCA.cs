using System;
using System.IO;
namespace StrongProject
{
	class DataType
	{
		public String Name;
		public String Unit;
		public String Lower;
		public String Upper;
		public String Value;
	}
	class AllData
	{
		public AllData()
		{ }

		public String SN = "";
		public String Result;
		public String StarTime;
		public String EndTime;
		public String SW_Version;
		public String Reserved;

		public System.Collections.ObjectModel.Collection<DataType> DataItems = new System.Collections.ObjectModel.Collection<DataType>();
	}
	class WPDCA
	{

		public WPDCA()
		{

		}

		public void WritData(AllData data, String path)
		{
			StreamWriter writer = new StreamWriter(path);
			String strdata = "";
			String passItems = "";
			String failItems = "";

			foreach (DataType each in data.DataItems)
			{
				passItems += "," + each.Name + "," + each.Unit + "," + each.Lower + "," + each.Upper + "," + each.Value;

				if (each.Lower != "NA")
				{
					if (double.Parse(each.Value) <= double.Parse(each.Lower) || double.Parse(each.Value) >= double.Parse(each.Upper))
					{
						failItems += each.Name + ";";
					}
				}
			}

			strdata = data.SN + "," + data.Result + "," + failItems + ",," + data.StarTime + "," + data.EndTime + ","
				+ data.SW_Version + "," + data.Reserved + passItems;

			writer.Write(strdata);
			writer.Flush();
			writer.Close();
		}
		public String GetSnFromStart(String path)
		{
			StreamReader reader;
			String SN = "";

			try
			{
				if (!File.Exists(path))

				{
					return SN;
				}
				reader = new StreamReader(path);
				SN = reader.ReadToEnd();
				reader.Close();
				File.Delete(path);
			}
			catch
			{
				return SN;
			}
			return SN;
		}
	}
	public class PDCA
	{
		private static string path;
		private static WPDCA log;
		private string tag_index = "";
		public PDCA(string index)
		{

			path = "D:\\DropBox\\Start" + index + ".txt";
			tag_index = index;
			;
			log = new WPDCA();
		}

		public string GetSn()
		{
			return log.GetSnFromStart(path);
		}

		//     public  void WriteData(string sn, double Adc, int var)
		public void WriteData(string sn, int var)
		{
			AllData allData = new AllData();

			/*   DataType powerType = new DataType();
                 powerType.Name = "Adc";
                 powerType.Unit = "V";
                 powerType.Lower = "0";
                 powerType.Upper = "5.0";
                 powerType.Value = Adc.ToString();
                 allData.DataItems.Add(powerType);
             */

			DataType distanceType = new DataType();
			distanceType.Name = "var";
			distanceType.Unit = "bool";
			distanceType.Lower = "NA";
			distanceType.Upper = "NA";
			distanceType.Value = var.ToString();
			allData.DataItems.Add(distanceType);

			allData.SN = sn;
			allData.Reserved = "Reserved";
			allData.SW_Version = "1.0.0.1";
			allData.Result = (var == 1) ? "PASS" : "FAIL";

			string path = "D:\\DropBox\\Done" + tag_index + ".txt";
			log.WritData(allData, path);
		}
	}
}
