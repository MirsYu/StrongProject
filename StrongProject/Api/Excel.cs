using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace StrongProject
{
	public class Excel
	{
		public static string GetSetPath(string filename)
		{
			string dir = Path.Combine(Application.StartupPath, "Data");
			string titleTop = "SN,时间,工位,返回类型,返回值,X1,Y1,X2,Y2,X3,Y3,X4,Y4,X5,Y5,X6,Y6,X7,Y7,X8,Y8,Angle\r\n";
			//string datename = DateTime.Now.Year + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Day.ToString("00") + ".csv";
			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
			}
			string csvPath = Path.Combine(dir, filename);
			if (File.Exists(csvPath) == false)
			{
				WritData(titleTop, csvPath);
			}

			return Path.Combine(dir, filename);
		}
		public static void WritData(string data, String path)
		{
			StreamWriter writer = new StreamWriter(path, true, Encoding.GetEncoding("GB2312"));
			writer.Write(data);
			writer.Flush();
			writer.Close();
		}
		public static void writeLog(string log)
		{
			string datename = DateTime.Now.Year + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Day.ToString("00") + ".csv";//+ "_" + DateTime.Now.Hour.ToString("00") + "_" + DateTime.Now.Minute.ToString("00") + "_" + DateTime.Now.Second.ToString("00") + ".config";
			string fileName = GetSetPath(datename);
			WritData(log, fileName);
		}

		public static void writeErrorLog(string log)
		{
			string datename = DateTime.Now.Year + "_" + DateTime.Now.Month.ToString("00") + "_" + DateTime.Now.Day.ToString("00") + ".csv";//+ "_" + DateTime.Now.Hour.ToString("00") + "_" + DateTime.Now.Minute.ToString("00") + "_" + DateTime.Now.Second.ToString("00") + ".config";
			string fileName = GetSetPath(datename + "\\ErrorLog");
			WritData(log, fileName);
		}
	}
}
