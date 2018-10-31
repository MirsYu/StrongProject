using System.IO;
using System.Text;

namespace StrongProject
{
	public class StationPattern
	{
		public string tag_stationName;
		public string tag_ClassName;
		public string codeUsing = "using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing System.Collections.Generic;\r\nusing System.Text;\r\nusing System.Threading; \r\nusing System.Windows.Forms;\r\n";
		public string strNamespace = "namespace StrongProject\r\n{";
		public string strClassName = "public  class ";
		public string strParameter = ":workBase\r\n{\t/// <summary>\r\n\t///  work\r\n\t/// </summary>\r\n \tpublic Work tag_Work;\r\n\t/// <summary>\r\n\t/// 线程\r\n\t/// </summary>\r\n\tpublic Thread tag_workThread;\r\n\t/// <summary>\r\n\t";



		public StationPattern(string station)
		{
			tag_stationName = station;
		}
		public string SocketParameterCreate(int[] Soc)
		{
			string strParameter_ = null;
			if (Soc == null || Soc.Length == 0)
				return "";
			for (int i = 0; i < Soc.Length; i++)
			{
				strParameter_ = strParameter_ + "\t/// <summary>\r\n\t/// \r\n\t/// </summary>\r\n\tpublic SocketClient tag_SocketClient" + Soc[i] + ";\r\n\t";
			}
			return strParameter_;
		}
		public string PortParameterCreate(int[] port)
		{
			string strParameter_ = null;
			if (port == null || port.Length == 0)
				return "";
			for (int i = 0; i < port.Length; i++)
			{
				strParameter_ = strParameter_ + "\t/// <summary>\r\n\t/// \r\n\t/// </summary>\r\n\tpublic JSerialPort tag_JSerialPort" + port[i] + ";\r\n\t";
			}
			return strParameter_;
		}
		public string PortParameterInit(int[] port)
		{
			string strParameter_ = null;
			if (port == null || port.Length == 0)
				return "";
			for (int i = 0; i < port.Length; i++)
			{
				strParameter_ = strParameter_ + "\ttag_JSerialPort" + port[i] + " =_Work.tag_JSerialPort[" + port[i] + "];\r\n";
			}
			return strParameter_;
		}
		public string SocketParameterInit(int[] soc)
		{
			string strParameter_ = null;
			if (soc == null || soc.Length == 0)
				return "";
			for (int i = 0; i < soc.Length; i++)
			{
				strParameter_ = strParameter_ + "\ttag_SocketClient" + soc[i] + " =_Work.tag_SocketClient[" + soc[i] + "];\r\n";
			}
			return strParameter_;
		}
		public string annotateCreate(string FunName, string annotate)
		{
			string strConstruct0 = "\t/// <summary>\r\n";
			string strConstruct1 = "\t/// " + annotate + "\r\n";
			string strConstruct2 = "\t/// </summary>\r\n";
			string strConstruct3 = "\t/// <param name=\"" + FunName + "\"></param>\r\n";
			string strConstruct4 = "\t/// <returns></returns>\r\n";
			return strConstruct0 + strConstruct1 + strConstruct2 + strConstruct3 + strConstruct4;
		}

		public string PropertiesCreate(string PropertiesName, string annotate)
		{
			string strConstruct0 = "\t/// <summary>\r\n";
			string strConstruct1 = "\t/// " + annotate + "\r\n";
			string strConstruct2 = "\t/// </summary>\r\n";
			string strConstruct3 = "\t/// <param name=\"" + PropertiesName + "\"></param>\r\n";
			string strConstruct4 = "\tpublic " + PropertiesName + "  tag_" + PropertiesName + ";\r\n";
			return strConstruct0 + strConstruct1 + strConstruct2 + strConstruct3 + strConstruct4;

		}
		public string PropertiesCreateAdd(string PropertiesName, string annotate)
		{
			string strConstruct3 = "\r\n\ttag_" + PropertiesName + "= new " + PropertiesName + "(_Work);\r\n";
			string strConstruct4 = "\r\n\tworkObject.Add(  tag_" + PropertiesName + ");\r\n";
			return strConstruct3 + strConstruct4;
		}
		/// <summary>
		/// 构造函数生成，classname 类名
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string ConstructCreate(string classname, int isRest, int[] soc, int[] port)
		{
			string strConstruct1 = null;
			string SocketParameter = SocketParameterInit(soc);
			string portParameter = PortParameterInit(port);
			if (isRest == 0)
			{
				strConstruct1 = "\tpublic " + classname + "(Work _Work) \r\n \t{\r\n \t\ttag_Work = _Work;\r\n\t " + portParameter + SocketParameter + "  tag_stationName =\"" + tag_stationName + "\";\r\n\t  tag_isRestStation =" + "0" + ";\r\n\t }\r\n";
			}
			else
			{
				strConstruct1 = "\tpublic " + classname + "(Work _Work) \r\n \t{\r\n \t\ttag_Work = _Work;\r\n\t " + portParameter + SocketParameter + "   tag_stationName =\"" + tag_stationName + "\";\r\n\t   tag_isRestStation =" + "1" + ";\r\n\t }\r\n";
			}
			return annotateCreate("ConstructCreate", "构造函数") + strConstruct1;
		}
		/// <summary>
		/// 构造开始函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string StartCreate()
		{
			string content0 = "\tpublic bool StartThread()\r\n\t{\r\n";
			string content1 = "\t\tif (tag_workThread != null)\r\n";
			string content2 = "\t\t{\r\n";
			string content3 = "\t\t\ttag_workThread.Abort();\r\n";
			string content4 = "\t\t}\r\n";
			string content5 = "\t\ttag_workThread = new Thread(new ParameterizedThreadStart(workThread));\r\n";
			string content6 = "\t\ttag_manual.tag_stepName = 0;\r\n";
			string content7 = "\t\ttag_workThread.Start();\r\n";
			string content8 = "\t\ttag_workThread.IsBackground = true;\r\n";
			string content9 = "\t\treturn true;\r\n\t}\r\n";
			return annotateCreate("start", "启动函数，主要是线程启动") + content0 + content1 + content2 + content3 + content4 + content5 + content6 + content7 + content8 + content9;
		}
		/// <summary>
		/// 构造退出函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string ExitCreate()
		{
			string content0 = "\tpublic bool exit()\r\n\t{\r\n";
			string content1 = "\t\ttag_manual.tag_stepName = 100000;\r\n";
			string content3 = "\t\ttag_isWork = 0;\r\n";

			string content2 = "\t\treturn true;\r\n\t}\r\n";
			return annotateCreate("exit", "退出函数，调用本函数，流程推出") + content0 + content1 + content3 + content2;
		}
		/// <summary>
		/// 构造第一步函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string FirstStepCreate()
		{
			string content0 = "\tpublic short FirstStep(object o)\r\n\t{\r\n";
			string content1 = "\t\ttag_isWork = 1;;\r\n";
			string content2 = "\t\treturn 0;\r\n\t}\r\n";
			return annotateCreate("FirstStep", "初始化函数，第一步嵌入的代码，返回0 表示成功") + content0 + content1 + content2;
		}
		/// <summary>
		/// 构造退出函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string LastStepCreate()
		{

			string content0 = "\tpublic short LastStep(object o)\r\n\t{\r\n";
			string content00 = "\r\n";
			string content1 = "\t\ttag_isWork = 0;;\r\n";
			string content2 = "\t\treturn 0;\r\n\t}\r\n";
			return annotateCreate("LastStep", "初始化函数，最后一步嵌入的代码，返回0 表示成功") + content0 + content00 + content1 + content2;
		}

		/// <summary>
		/// 构造第N步函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string StepCreateN(int n, string pointName)
		{

			string content0 = "\tpublic short Step" + n + "(object o)\r\n\t{\r\n";
			string content00 = "\r\n";
			string content1 = "";
			if (n == 0)
			{
				content1 = "\t\ttag_isWork = 1;;\r\n";
			}
			string content2 = "\t\treturn 0;\r\n\t}\r\n";
			return annotateCreate("Step" + n, pointName + "，第" + n + "步嵌入的代码，返回0 表示成功") + content0 + content00 + content1 + content2;
		}



		/// <summary>
		/// 构造第N步函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string StepCreateN()
		{
			StationModule sm = StationManage.FindStation(tag_stationName);
			if (sm != null && sm.intUsePointCount > 0)
			{
				int endStep = sm.intUsePointCount - 1;
				string ret = null;


				for (int i = 0; i < endStep + 1; i++)
				{
					ret = ret + StepCreateN(i, sm.arrPoint[i].strName);
				}
				return ret;
			}
			return "";
		}

		/// <summary>
		/// 构造3步函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string StepCreateN3()
		{
			StationModule sm = StationManage.FindStation(tag_stationName);
			if (sm != null && sm.intUsePointCount > 0 && sm.intUsePointCount >= 3)
			{
				int endStep = sm.intUsePointCount - 1;
				string ret = null;

				ret = ret + StepCreateN(0, sm.arrPoint[0].strName);
				ret = ret + StepCreateN(1, sm.arrPoint[1].strName);

				ret = ret + StepCreateN(endStep, sm.arrPoint[endStep].strName);
				return ret;
			}
			return "";
		}

		/// <summary>
		/// 构造初始化函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string InitCreateN()
		{
			string content0 = "\tpublic short Init()\r\n\t{\r\n";
			StationModule sm = StationManage.FindStation(tag_stationName);
			int endStep = sm.intUsePointCount - 1;

			string stepN = null;

			for (int i = 0; i < endStep + 1; i++)
			{
				string content1 = "\t\tPointAggregate _Step" + i + " = pointMotion.FindPoint(tag_stationName, \"" + sm.arrPoint[i].strName + "\"," + i + ");\r\n";
				string content2 = "\t\tif (_Step" + i + "  == null)\r\n";
				string content3 = "\t\t{\r\n";
				string content4 = "\t\t\treturn -1;\r\n\t\t}\r\n";
				string content5 = "\t\t_Step" + i + " .tag_BeginFun = Step" + i + " ;\r\n";
				stepN = stepN + content1 + content2 + content3 + content4 + content5;
			}

			string content11 = "\t\treturn 0;\r\n\t}\r\n";
			return annotateCreate("init", "初始化函数，初始化流程嵌入的代码，返回0 表示成功") + content0 + stepN + content11;
		}
		/// <summary>
		/// 构造初始化函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string InitCreateN3()
		{
			string content0 = "\tpublic short Init()\r\n\t{\r\n";
			StationModule sm = StationManage.FindStation(tag_stationName);
			int endStep = sm.intUsePointCount - 1;

			string stepN = null;

			for (int i = 0; i < endStep + 1; i++)
			{
				string content1 = "\t\tPointAggregate _Step" + i + " = pointMotion.FindPoint(tag_stationName, \"" + sm.arrPoint[i].strName + "\"," + i + ");\r\n";
				string content2 = "\t\tif (_Step" + i + "  == null)\r\n";
				string content3 = "\t\t{\r\n";
				string content4 = "\t\t\treturn -1;\r\n\t\t}\r\n";
				string content5 = null;
				if (i > 0 && i < endStep)
				{
					content5 = "\t\t_Step" + i + " .tag_BeginFun = Step1 ;\r\n";
				}
				else
				{
					content5 = "\t\t_Step" + i + " .tag_BeginFun = Step" + i + " ;\r\n";
				}
				stepN = stepN + content1 + content2 + content3 + content4 + content5;
			}

			string content11 = "\t\treturn 0;\r\n\t}\r\n";
			return annotateCreate("init", "初始化函数，初始化流程嵌入的代码，返回0 表示成功") + content0 + stepN + content11;
		}

		/// <summary>
		/// 构造初始化函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string InitCreate()
		{
			string content0 = "\tpublic short Init()\r\n\t{\r\n";
			StationModule sm = StationManage.FindStation(tag_stationName);
			int endStep = sm.intUsePointCount - 1;
			string content1 = "\t\tPointAggregate startStep = pointMotion.FindPoint(tag_stationName, \"" + sm.arrPoint[0].strName + "\",0);\r\n";
			string content2 = "\t\tif (startStep == null)\r\n";
			string content3 = "\t\t{\r\n";
			string content4 = "\t\t\treturn -1;\r\n\t\t}\r\n";
			string content5 = "\t\tstartStep.tag_BeginFun = FirstStep;\r\n";
			string content6 = "\t\tPointAggregate endStep = pointMotion.FindPoint(tag_stationName, \"" + sm.arrPoint[sm.intUsePointCount - 1].strName + "\");\r\n";
			string content7 = "\t\tif (endStep == null)\r\n";
			string content8 = "\t\t{\r\n";
			string content9 = "\t\t\treturn -1;\r\n\t\t}\r\n";
			string content10 = "\t\tendStep.tag_EndFun = LastStep;\r\n";
			string content11 = "\t\treturn 0;\r\n\t}\r\n";
			return annotateCreate("init", "初始化函数，初始化流程嵌入的代码，返回0 表示成功") + content0 + content1 + content2 + content3 + content4 + content5 + content6 + content7 + content8 + content9 + content10 + content11;
		}

		/// <summary>
		/// 继续函数
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public string ContinueFunCreate()
		{
			string content0 = "\tpublic short Continue(object o)\r\n\t{\r\n";
			string content00 = "\r\n";
			string content2 = "\t\treturn 0;\r\n\t}\r\n";
			return annotateCreate("Continue", "继续函数") + content0 + content00 + content2;

		}
		/// <summary>
		/// 中断函数
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public string SuspendFunCreate()
		{
			string content0 = "\tpublic short Suspend(object o)\r\n\t{\r\n";
			string content00 = "\r\n";
			string content2 = "\t\treturn 0;\r\n\t}\r\n";
			return annotateCreate("Suspend", "中断函数") + content0 + content00 + content2;

		}
		/// <summary>
		/// 构造退出函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string workCreate()
		{

			string content0 = "\tpublic short Start(object o)\r\n\t{\r\n";
			string content1 = "\t\tshort ret = 0;\r\n";
			string content2 = "\t\tif (Init() == 0)\r\n";
			string content3 = "\t\t{\r\n";
			string content4 = "\t\tif (tag_manual.tag_step == 0)\r\n";
			string content5 = "\t\t{\r\n";
			string content6 = "\t\tret = pointMotion.StationRun(tag_stationName, tag_manual);\r\n \t\ttag_manual.tag_stepName = 0;\r\n";
			string content7 = "\t\t}\r\n";
			string content8 = "\t\ttag_isWork = -1;\r\n";
			string content9 = "\t\treturn ret;\r\n";
			string content10 = "\t\t}\r\n";
			string content11 = "\t\telse\r\n";
			string content12 = "\t\t{\r\n";
			string content13 = "\t\treturn -1;\r\n";
			string content14 = "\t\t}\r\n";
			string content15 = "\t\treturn  0;\r\n\t}\r\n";
			return annotateCreate("work", "流程执行的函数 返回0 表示成功") + content0 + content1 + content2 + content3 + content4 + content5 + content6 + content7 + content8 + content9 + content10 + content11 + content12 + content13 + content14 + content15;
		}
		/// <summary>
		/// 构造退出函数
		/// </summary>
		/// <param name="classname"></param>
		/// <returns></returns>
		public string workThreadCreate()
		{
			string content0 = "\tpublic void workThread(object o)\r\n\t{\t\r\n";
			string content1 = "\t\tshort ret = 0;\r\n";
			string content2 = "\t\tif (Init() == 0)\r\n";
			string content3 = "\t\t{\r\n";
			string content4 = "\t\t\tif (tag_manual.tag_step == 0)\r\n";
			string content5 = "\t\t\t{\r\n";
			string content6 = "\t\t\t\ttag_isWork = pointMotion.StationRun(tag_stationName, tag_manual);\r\n\t\ttag_manual.tag_stepName = 0;\r\n";
			string content7 = "\t\t\t}\r\n\t\t}\r\n\t}\r\n}\r\n}\r\n";
			return annotateCreate("workThreadCreate", "流程用线程执行执行的函数 无返回值") + content0 + content1 + content2 + content3 + content4 + content5 + content6 + content7;
		}
		/// <summary>
		/// 生成代码 
		/// </summary>
		/// <returns></returns>
		public string classCodeCreate(int isRest, int[] soc, int[] port)
		{
			string strCode = codeUsing + strNamespace + strClassName + tag_ClassName + strParameter + SocketParameterCreate(soc) + PortParameterCreate(port) + "\r\n" + ConstructCreate(tag_ClassName, isRest, soc, port) + StartCreate()
				+ FirstStepCreate() + LastStepCreate() + InitCreate() + ExitCreate() + SuspendFunCreate() + ContinueFunCreate() + workCreate() + workThreadCreate();
			return strCode;

		}
		/// <summary>
		/// 生成代码 
		/// </summary>
		/// <returns></returns>
		public string classCodeCreateN(int isRest, int[] soc, int[] port)
		{
			string strCode = codeUsing + strNamespace + strClassName + tag_ClassName + strParameter + SocketParameterCreate(soc) + PortParameterCreate(port) + "\r\n" + ConstructCreate(tag_ClassName, isRest, soc, port) + StartCreate()
				+ ExitCreate() + StepCreateN() + InitCreateN() + SuspendFunCreate() + ContinueFunCreate() + workCreate() + workThreadCreate();
			return strCode;

		}
		/// <summary>
		/// 生成代码 
		/// </summary>
		/// <returns></returns>
		public string classCodeCreateN3(int isRest, int[] soc, int[] port)
		{
			string strCode = codeUsing + strNamespace + strClassName + tag_ClassName + strParameter + SocketParameterCreate(soc) + PortParameterCreate(port) + "\r\n" + ConstructCreate(tag_ClassName, isRest, soc, port) + StartCreate()
				+ ExitCreate() + StepCreateN3() + InitCreateN3() + SuspendFunCreate() + ContinueFunCreate() + workCreate() + workThreadCreate();
			return strCode;

		}
		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public void CodeCreateN3(string filename, int isRest, int[] portCount, int[] NetCount)
		{
			int index = filename.LastIndexOf("\\");
			string dir = filename.Substring(0, index - 1);
			string file = filename.Substring(index + 1, filename.Length - index - 1);
			tag_ClassName = file;
			string ret = classCodeCreateN3(isRest, NetCount, portCount);

			if (filename.IndexOf(".cs") < 0)
			{
				filename = filename + ".cs";
			}
			if (!Directory.Exists(filename))
			{

				Directory.CreateDirectory(dir);
			}
			if (!File.Exists(filename))
			{
				StreamWriter sw = File.CreateText(filename);

				sw.Flush();
				sw.Close();
			}
			{
				StreamWriter sw = new StreamWriter(filename, true, Encoding.GetEncoding("GB2312"));
				sw.Write(ret);
				sw.Flush();
				sw.Close();
			}
		}
		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public void CodeCreateN(string filename, int isRest, int[] portCount, int[] NetCount)
		{
			int index = filename.LastIndexOf("\\");
			string dir = filename.Substring(0, index - 1);
			string file = filename.Substring(index + 1, filename.Length - index - 1);
			tag_ClassName = file;
			string ret = classCodeCreateN(isRest, NetCount, portCount);

			if (filename.IndexOf(".cs") < 0)
			{
				filename = filename + ".cs";
			}
			if (!Directory.Exists(filename))
			{

				Directory.CreateDirectory(dir);
			}
			if (!File.Exists(filename))
			{
				StreamWriter sw = File.CreateText(filename);

				sw.Flush();
				sw.Close();
			}
			{
				StreamWriter sw = new StreamWriter(filename, true, Encoding.GetEncoding("GB2312"));
				sw.Write(ret);
				sw.Flush();
				sw.Close();
			}
		}
		/// <summary>
		/// 获取所有步骤按工位保存
		/// </summary>
		/// <returns></returns>
		public void CodeCreate(string filename, int isRest, int[] portCount, int[] NetCount)
		{
			int index = filename.LastIndexOf("\\");
			string dir = filename.Substring(0, index - 1);
			string file = filename.Substring(index + 1, filename.Length - index - 1);
			tag_ClassName = file;
			string ret = classCodeCreate(isRest, NetCount, portCount);

			if (filename.IndexOf(".cs") < 0)
			{
				filename = filename + ".cs";
			}
			if (!Directory.Exists(filename))
			{

				Directory.CreateDirectory(dir);
			}
			if (!File.Exists(filename))
			{

				StreamWriter sw = File.CreateText(filename);

				sw.Flush();
				sw.Close();
			}
			{
				StreamWriter sw = new StreamWriter(filename, true, Encoding.GetEncoding("GB2312"));
				sw.Write(ret);
				sw.Flush();
				sw.Close();
			}
		}
		/// <summary>
		/// 插入到 WorkObjectManage文件代码里面去
		/// </summary>
		/// <returns></returns>
		public void InsertToWorkObjectManage(string filename, int isRest, int[] portCount, int[] NetCount)
		{
			int index = filename.LastIndexOf("\\");
			string dir = filename.Substring(0, index - 1);
			string file = filename.Substring(index + 1, filename.Length - index - 1);

			string ret = classCodeCreateN(isRest, NetCount, portCount);

			if (!File.Exists(filename))
			{
				return;
			}

			StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("UTF-8"), true);
			string content = sr.ReadToEnd();
			int indexOf = content.IndexOf("workObjectManage");
			indexOf = content.IndexOf("{", indexOf);
			if (indexOf < 0)
				return;
			string Properties = PropertiesCreate(tag_ClassName, tag_ClassName);
			content = content.Insert(indexOf + 1, Properties);
			indexOf = content.IndexOf("AddWorkObjectManage", indexOf);
			if (indexOf < 0)
				return;
			Properties = PropertiesCreateAdd(tag_ClassName, tag_ClassName);
			content = content.Insert(indexOf + "AddWorkObjectManage".Length, Properties);
			sr.Close();

			StreamWriter sw = new StreamWriter(filename, false, Encoding.GetEncoding("GB2312"));
			sw.Write(content);
			sw.Flush();
			sw.Close();

		}
		/// <summary>
		/// 把文件到插入到 csproj文件里面去
		/// </summary>
		/// <returns></returns>
		public int InsertTocsproj(string filename, int isRest, int[] portCount, int[] NetCount)
		{
			int index = filename.LastIndexOf("\\");
			string dir = filename.Substring(0, index - 1);
			string file = filename.Substring(index + 1, filename.Length - index - 1);

			string ret = classCodeCreateN(isRest, NetCount, portCount);

			if (!File.Exists(filename))
			{
				return 1;
			}

			StreamReader sr = new StreamReader(filename, Encoding.GetEncoding("UTF-8"), true);
			string content = sr.ReadToEnd();
			int indexOf = content.IndexOf("<Compile Include=\"Work\\Work.cs\" />");
			if (indexOf < 0)
				return 1;


			string Properties = "\r\n<Compile Include=\"Work\\" + tag_ClassName + ".cs\" />\r\n";


			content = content.Insert(indexOf + "<Compile Include=\"Work\\Work.cs\" />".Length, Properties);

			sr.Close();

			StreamWriter sw = new StreamWriter(filename, false, Encoding.GetEncoding("UTF-8"));
			sw.Write(content);
			sw.Flush();
			sw.Close();
			return 0;


		}
	}
}
