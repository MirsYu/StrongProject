using System;
using System.IO;
using System.Text;

namespace StrongProject
{

	//文件操作
	public static class CFile
	{

		public const string strSaveCardErrorPath = "D:\\Log\\CardError";
		public const string strSaveDataPath = "D:\\DATA";

		/// <summary>
		/// 产品测试数据写入,清零时用
		/// </summary>
		/// <param name="writeString"></param>
		public static void WriteHistoryProductionCount(string writeString, string path = strSaveDataPath)
		{
			//path = Application.StartupPath + "\\DATA\\HistoryProductionCount";
			path = path + "\\HistoryProductionCount";
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string fileName = "History.csv";
			string strFile = path + "\\" + fileName;
			if (!File.Exists(strFile))
			{
				using (StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default))
				{
					swf.Write("清除时间,总产量,测试合格产量,测试不合格产量\r\n");
				}
			}
			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				swf.Write(writeString);
				swf.Flush();
				swf.Close();
			}
			catch
			{

			}
		}

		/// <summary>
		/// 产品测试结果写入
		/// </summary>
		/// <param name="writeString"></param>
		public static void WriteProductResult(string writeString, string path = strSaveDataPath)
		{
			path = path + "\\ProductResult";
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			path = Path.Combine(path, DateTime.Now.ToString("yyyy"));
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			path = Path.Combine(path, DateTime.Now.ToString("MM"));
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
			string strFile = path + "\\" + fileName;
			if (!File.Exists(strFile))
			{
				using (StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default))
				{
					swf.Write("sn,start_time,stop_time ,parameter_1,parameter_2,parameter_3,result,\r\n");
				}
			}
			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				swf.Write(writeString);
				swf.Flush();
				swf.Close();
			}
			catch
			{
				//MessageBox.Show("写入文件失败，请检查文件是否被打开，如是，请关闭！");
			}
		}

		/// <summary>
		/// 上传URL记录
		/// </summary>
		/// <param name="writeString"></param>
		public static void WriteURLResult(string writeString, string path = strSaveDataPath)
		{
			//string path = Application.StartupPath + "\\DATA\\URLResult";
			path = path + "\\URLResult";
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			path = Path.Combine(path, DateTime.Now.ToString("yyyy"));
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			path = Path.Combine(path, DateTime.Now.ToString("MM"));
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
			string strFile = path + "\\" + fileName;
			if (!File.Exists(strFile))
			{
				using (StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default))
				{
					swf.Write("update_time ,url,result,\r\n");
				}
			}
			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				swf.Write(writeString);
				swf.Flush();
				swf.Close();
			}
			catch
			{
				//MessageBox.Show("写入文件失败，请检查文件是否被打开，如是，请关闭！");
			}
		}

		/// <summary>
		/// 发送给CCD数据和返回数据 DateTime.Now + Send/Receive + strData
		/// </summary>
		/// <param name="writeString"></param>
		/// <param name="path"></param>
		public static void WriteCCDData(string writeString, string path = strSaveDataPath)
		{
			path = path + "\\CCDData";
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			path = Path.Combine(path, DateTime.Now.ToString("yyyy"));
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			path = Path.Combine(path, DateTime.Now.ToString("MM"));
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
			string strFile = path + "\\" + fileName;
			if (!File.Exists(strFile))
			{
				using (StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default))
				{
					swf.Write("update_time ,url,result,\r\n");
				}
			}
			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				swf.Write(writeString);
				swf.Flush();
				swf.Close();
			}
			catch
			{
				//MessageBox.Show("写入文件失败，请检查文件是否被打开，如是，请关闭！");
			}
		}



		/// <summary>
		/// 压力记录
		/// </summary>
		/// <param name="writeString"></param>
		/// <param name="path"></param>
		public static void WritePressure(string writeString, string path = strSaveDataPath)
		{
			//string path = Application.StartupPath + "\\DATA\\Pressure";
			path = path + "\\Pressure";
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string fileName = DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
			string strFile = path + "\\" + fileName;
			if (!File.Exists(strFile))
			{
				using (StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default))
				{
					swf.Write("TIME,230g,0ms,50ms,100ms,200ms,300ms,400ms,500ms,600ms,700ms,800ms,900ms,\r\n");
				}
			}
			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				swf.Write(writeString);
				swf.Flush();
				swf.Close();
			}
			catch
			{

			}
		}



		public static void WriteCSVDate(string WriteString, string fileName, bool bAppendEnter = true, string path = strSaveCardErrorPath)
		{
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string strFile = path + "\\" + fileName;
			if (File.Exists(strFile) == false)
			{
				//FileStream fs1 = new FileStream("..\\..\\message\\log.csv", FileMode.Create );
			}

			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				if (bAppendEnter)
				{
					swf.Write(WriteString + "\r\n");
				}
				else
				{
					swf.Write(WriteString);
				}

				swf.Close();
			}
			catch (System.Exception ex)
			{
				//Forms.Msg.MesssageShowOnce("csv文件操作出错:" + ex.Message + " 请不要在运行中打开csv文件", Forms.Msg.MSG.CSV);
				RewrteCSV(WriteString, strFile, bAppendEnter);
			}
		}

		//重写CSV文件
		public static void RewrteCSV(string WriteString, string strFile, bool bAppendEnter)
		{
			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				if (bAppendEnter)
				{
					swf.Write(WriteString + "\r\n");
				}
				else
				{
					swf.Write(WriteString);
				}
				swf.Close();
			}
			catch (System.Exception ex)
			{
				//Forms.Msg.MesssageShowOnce("csv文件操作出错:" + ex.Message + " 请不要在运行中打开csv文件", Forms.Msg.MSG.CSV);
				RewrteCSV(WriteString, strFile, bAppendEnter);
				//MessageBoxLog.Show("csv文件操作出错," +ex.Message+ " 请不要在运行中打开csv文件");
			}
		}



		//Write txt ，log名字前自动加日期
		public static void WriteTxtDate(string WriteString, string fileName, string path = strSaveCardErrorPath)//"..\\..\\log"
		{
			//path = path + CConst.Save_Folder_Name;
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string strFile = path + "\\" + DateTime.Now.ToString("yyMMdd") + fileName + ".txt";
			if (File.Exists(strFile) == false)
			{//可用于第一行的写标题操作
			 //FileStream fs1 = new FileStream("..\\..\\message\\log.csv", FileMode.Create );
			}
			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				swf.Write(DateTime.Now.ToString("yyMMdd-") + DateTime.Now.ToString("HH:mm:ss.fff ") + WriteString + "\r\n");
				swf.Close();
			}
			catch
			{

			}
		}

		//专门记录错误信息，只是默认路径不同
		public static void WriteErrorForDate(string WriteString, string fileName = null, string path = strSaveCardErrorPath)//"..\\..\\log"
		{
			if (Directory.Exists(path) == false)
			{
				Directory.CreateDirectory(path);
			}
			string strFile = path + "\\" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
			if (File.Exists(strFile) == false)
			{//可用于第一行的写标题操作
			 //FileStream fs1 = new FileStream("..\\..\\message\\log.csv", FileMode.Create );
			}
			try
			{
				StreamWriter swf = new StreamWriter(strFile, true, Encoding.Default);
				swf.Write(DateTime.Now.ToString("yyyyMMdd-") + DateTime.Now.ToString("HH:mm:ss.fff ") + WriteString + "\r\n");
				swf.Close();
			}
			catch
			{

			}
		}

		public class MyFile
		{
			//delete folder
			public static void DeleteFolder(string path)
			{
				try
				{
					if (Directory.Exists(path))
					{
						Directory.Delete(path, true);
					}
				}
				catch (Exception ex)
				{

				}
			}
			//create folder
			public static void CreateFolder(string folder)
			{
				if (!Directory.Exists(folder))
				{
					Directory.CreateDirectory(folder);
				}
			}
			//delet folder beyond set days
			public static bool BeyondDays(string compare, int days = 7)
			{//some name may not format for yyMMdd, that will run to catch
				try
				{
					compare = GetNameWithoutSlash(compare);
					int nowMonth = DateTime.Now.Month;
					int nowDay = DateTime.Now.Day;

					int Month = int.Parse(compare.Substring(2, 2));
					int Days = int.Parse(compare.Substring(4, 2));

					//   //HCHEN 0315

					//DateTime creatTime  = File.GetCreationTime("D:\\DataScrewMachine\\" + compare);

					//TimeSpan   t = DateTime.Now - creatTime;
					//if (t.Days >= days)
					//{
					//    return true;
					//}
					//else
					//    return false;

					if ((30 * nowMonth + nowDay) - (30 * Month + Days) > days)
					{
						return true;
					}
					else
					{
						return false;
					}

				}
				catch
				{
					//System.Diagnostics.Debug.Assert(false);
					//MessageBoxLog.Show(ex.Message);
				}
				return false;
			}
			//get name last of slash
			private static string GetNameWithoutSlash(string strName)
			{
				try
				{
					if (strName.IndexOf("\\") > 0)
					{
						int nLastSlash = strName.LastIndexOf("\\");
						string FileName = strName.Substring(nLastSlash + 1);
						return FileName;
					}
					else
					{
						return strName;
					}
				}
				catch
				{
				}
				return "";
			}
			//delete file
			public static void DeleteFile(string path)
			{
				if (System.IO.File.Exists(path))
				{
					System.IO.File.Delete(path);
				}
			}
			//move one file from folder to another
			public static void MoveFile(string oriFolder, string desFolder, string file)
			{
				if (System.IO.File.Exists(oriFolder + "\\" + file))
				{
					CreateFolder(desFolder);
					if (System.IO.Directory.Exists(desFolder))
					{
						System.IO.File.Move(oriFolder + "\\" + file, desFolder + "\\" + file);
					}
				}
			}
			//copy a file from a oriFolder to desFolder
			public static void CopyFile(string oriFolder, string desFolder, string file)
			{
				if (System.IO.File.Exists(oriFolder + "\\" + file))
				{
					CreateFolder(desFolder);
					if (System.IO.Directory.Exists(desFolder))
					{
						System.IO.File.Copy(oriFolder + "\\" + file, desFolder + "\\" + file, true);
						//System.IO.File.Copy(oriFolder + "\\" + file, desFolder + "\\" + file );
					}
				}
			}
			//delete folder beyond setting days
			public static void DeleteOutDayFolder(string Folder, int days)
			{
				if (days < 5)
				{
					days = 5;
				}
				if (Directory.Exists(Folder))
				{
					string[] FoldersName = Directory.GetDirectories(Folder);
					for (int i = 0; i < FoldersName.Length; i++)
					{
						string FileName = GetNameWithoutSlash(FoldersName[i]);
						if (BeyondDays(FileName, days))
						{
							DeleteFolder(FoldersName[i]);
						}
					}
				}
			}
			//delete sub folder have DelIdentifier beyond setting days within Folder
			// need check
			public static void DeleteSubfolderFolder(string Folder, int days, string DelIdentifier)
			{
				if (days < 2)
				{
					days = 2;
				}
				if (Directory.Exists(Folder))
				{
					string[] FoldersName = Directory.GetDirectories(Folder);
					for (int i = 0; i < FoldersName.Length; i++)
					{
						if (BeyondDays(FoldersName[i], days))
						{

							//0315  HCHEN


							//string DelName = Path.Combine(FoldersName[i], DelIdentifier);
							//if (Directory.Exists(DelName))
							//{
							DeleteFolder(FoldersName[i]);
							//}
						}
					}
				}
			}
			//move all files of folder to another
			public static void MoveFolderFiles(string OriFolder, string DesFolder)
			{
				try
				{
					if (Directory.Exists(OriFolder))
					{
						string[] FilesName = System.IO.Directory.GetFiles(OriFolder);
						for (int i = 0; i < FilesName.Length; i++)
						{
							string FileName = GetNameWithoutSlash(FilesName[i]);
							MoveFile(OriFolder, DesFolder, FileName);
						}
					}
				}
				catch
				{
				}


			}
			//copy all files of folder to another
			public static void CopyFolderFiles(string OriFolder, string DesFolder)
			{
				try
				{
					if (Directory.Exists(OriFolder))
					{
						string[] FilesName = System.IO.Directory.GetFiles(OriFolder);
						CreateFolder(DesFolder);
						for (int i = 0; i < FilesName.Length; i++)
						{
							string FileName = GetNameWithoutSlash(FilesName[i]);
							CopyFile(OriFolder, DesFolder, FileName);
						}
					}
				}
				catch
				{
				}


			}

		}
	}
}
