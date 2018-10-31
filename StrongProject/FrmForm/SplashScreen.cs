using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class SplashScreen : Form
	{
		//public SplashScreen()
		//{
		//    InitializeComponent();
		//}
		/// <summary>  
		/// 启动画面本身  
		/// </summary>  
		static SplashScreen instance;

		/// <summary>  
		/// 显示的图片  
		/// </summary>  
		Bitmap bitmap;

		public static SplashScreen Instance
		{
			get
			{
				return instance;
			}
			set
			{
				instance = value;
			}
		}
		public SplashScreen()
		{
			InitializeComponent();

			// 设置窗体的类型  
			const string showInfo = "启动画面：我们正在努力的加载程序，请稍后...";
			FormBorderStyle = FormBorderStyle.None;
			StartPosition = FormStartPosition.CenterScreen;
			ShowInTaskbar = false;
			bitmap = new Bitmap(global::StrongProject.Properties.Resources.stronglogo);
			ClientSize = bitmap.Size;

			using (Font font = new Font("Consoles", 15))
			{
				using (Graphics g = Graphics.FromImage(bitmap))
				{
					g.DrawString(showInfo, font, Brushes.Black, 330, 400);
				}
			}

			BackgroundImage = bitmap;
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				if (bitmap != null)
				{
					bitmap.Dispose();
					bitmap = null;
				}
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public static void ShowSplashScreen()
		{
			instance = new SplashScreen();
			instance.Show();
		}
	}
}
