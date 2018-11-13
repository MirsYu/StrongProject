using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace StrongProject
{

	[Serializable]
	public class LogManage : BinarySerializer
	{
		public List<Log> tag_logList;
		public LogManage()
		{

		}
		protected static string GetSetPath(string filename)
		{
			string dir = Path.Combine(Application.StartupPath, "Data");
			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
			}
			return Path.Combine(dir, filename);
		}
		public bool Save()
		{
			return this.Save(GetSetPath("log.config"));
		}
		public static LogManage Load()
		{
			LogManage log = Load(GetSetPath("log.config")) as LogManage;
			if (log == null)
			{
				log = new LogManage();
				log.tag_logList = new List<Log>();
			}
			return log;
		}
	}
	[Serializable]
	public class Log
	{
		public int tag_id;
		public string tag_info;
		public DateTime tag_dateTime;
		public string tag_OP;
		public Log(int id, string info, DateTime dateTime)
		{
			tag_id = id;
			tag_info = info;
			tag_dateTime = dateTime;
		}
	}
	public class LogDataBase
	{
		public LogManage tag_Log;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		/// <param name="info"></param>
		/// <param name="dateTime"></param>
		public void addLog(int id, string info, DateTime dateTime)
		{
			
		}
		public List<Log> Get(int id, int count)
		{
			List<Log> ret = new List<Log>();
			
			return ret;
		}
		public List<Log> Get(DateTime BegindateTime, DateTime EndindateTime)
		{
			List<Log> ret = new List<Log>();
			
			return ret;
		}
	}
}
