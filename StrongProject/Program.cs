using System;
using System.Threading;
using System.Windows.Forms;

namespace StrongProject
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			bool createNew;
			using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))
			{
				try//0323
				{
					if (createNew)
					{
						Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
						Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
						AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						Application.Run(new Frm_Frame());
					}
					else
					{
						MessageBox.Show("程序已经在运行中,不能重复运行");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
					Application.Exit();
				}
			}
		}


		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			String str = "";
			String strDateInfo = "出现应用程序未处理异常：" + DateTime.Now.ToString() + "\r\n";
			Exception error = e.Exception as Exception;
			if (error != null)
			{
				str = string.Format(strDateInfo + "异常类型：{0}\r\n异常消息：{1}\r\n异常信息：{2}\r\n", error.GetType().Name, error.Message, error.StackTrace);
			}
			else
			{
				str = string.Format("应用程序线程错误：{0}", e);
			}
			//Cgl.ExceptionLog(str);
			Application.Exit();
		}

		static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			string str = "";
			Exception error = e.ExceptionObject as Exception;
			string strDateInfo = "出现应用程序未处理异常：" + DateTime.Now.ToString() + "\r\n";
			if (error != null)
			{
				str = string.Format(strDateInfo + "Application UnhandledException:{0};\n\r堆栈信息：{1}", error.Message, error.StackTrace);
			}
			else
			{
				str = string.Format("Application UnhandledError：{0}", e);
			}
			//Cgl.ExceptionLog(str);
			MessageBox.Show(str, "系统错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
