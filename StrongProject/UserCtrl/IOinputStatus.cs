using System;
using System.Windows.Forms;
namespace StrongProject
{
	public partial class IOinputStatus : UserControl
	{

		IOParameter arrInputIo; //定义一个IOParameter的对象  
		public delegate void UIdelegate1(bool var);

		public UIdelegate1 tag_UIdelegate;
		public IOinputStatus()
		{
			InitializeComponent();

		}

		public IOinputStatus(IOParameter arrInputIo_) //初始化IOinput参数数组
		{
			arrInputIo = arrInputIo_;
			NewCtrlCardIO.InputIOChange += new EventHandler(NewCtrlCardSR_IOstChange);
			tag_UIdelegate = UIdelegate_;
			InitializeComponent();
		}

		public void UIdelegate_(bool var)
		{
			lblinputLight.Image = var ? StrongProject.Properties.Resources.led_green_on_16 : StrongProject.Properties.Resources.led_off_16;

		}
		private void IOinputStatus_Load(object sender, EventArgs e)
		{
			if (arrInputIo != null)
			{
				lblInputname.Text = arrInputIo.StrIoName; //从ArrAxis拿轴名称

			}

		}
		public void IOinputStatus_Load(IOParameter arrInputIo_)
		{
			arrInputIo = arrInputIo_;
			NewCtrlCardIO.InputIOChange += new EventHandler(NewCtrlCardSR_IOstChange);
			if (arrInputIo != null)
			{
				lblInputname.Text = arrInputIo.StrIoName; //从ArrAxis拿轴名称

			}

		}


		//IO状态C:\Users\Administrator\Desktop\Manual_Debug\Manual_Debug\UserCtrl\IOoutputStatus.cs
		void NewCtrlCardSR_IOstChange(object sender, EventArgs e)
		{
			try
			{
				CardInputIOEvengArgs cIOE = e as CardInputIOEvengArgs;
				if (arrInputIo != null && cIOE.CardNum == arrInputIo.CardNum && cIOE.type == (int)arrInputIo.tag_MotionCardManufacturer)
				{
					ulong uaone = 1;
					if (arrInputIo.StrIoName == "右载具右压紧位")
					{

					}
					ulong a = uaone << arrInputIo.IOBit;
					ulong b = (cIOE.Value & a) >> arrInputIo.IOBit;
					bool var = ((b > 0 && arrInputIo.Logic == 1) || (b < 1 && arrInputIo.Logic == 0));


					//  lblinputLight.Image = (((cIOE.Value & ((ulong)(1 << arrInputIo.IOBit))) > 0 && arrInputIo.Logic == 1) || ((cIOE.Value & ((ulong)(1 << arrInputIo.IOBit))) < 1 && arrInputIo.Logic == 0)) ? StrongProject.Properties.Resources.led_green_on_16 : StrongProject.Properties.Resources.led_off_16;
					this.Invoke(tag_UIdelegate, var);
				}


			}
			catch
			{ }

		}

		//清除事件
		public void ClearEvent()
		{
			NewCtrlCardIO.InputIOChange -= new EventHandler(NewCtrlCardSR_IOstChange);
		}

	}
}
