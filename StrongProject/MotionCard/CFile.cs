using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace StrongProject
{
       
        //文件操作
        public static class CFile1
        {
            public  const string strSaveCardErrorPath = "D:\\Log\\CardError";
            
            public static void WriteCSVDate(string WriteString, string fileName, bool bAppendEnter = true, string path = strSaveCardErrorPath )
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
            public static void WriteErrorForDate(string WriteString, string fileName=null, string path = strSaveCardErrorPath)//"..\\..\\log"
            {
                UserControl_LogOut.OutLog(WriteString, 1);
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
