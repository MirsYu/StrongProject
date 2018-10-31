using System;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class IOoutputStatus : UserControl
	{
		short result;
		public delegate void UIdelegate(bool var);
		public UIdelegate tag_UIdelegate;
		public delegate void delegateStationSelect(object var);
		public bool btstatus = false;
		public delegateStationSelect tag_delegateStationSelect;
		IOParameter arrOutputIo; //定义一个AxisConfig的对象
		public IOoutputStatus()
		{
			InitializeComponent();
		}
		public IOoutputStatus(IOParameter arrOutputIo_, delegateStationSelect selectstation) //初始化IOinput参数数组
		{
			arrOutputIo = arrOutputIo_;
			NewCtrlCardIO.OutputIOChange += new EventHandler(NewCtrlCardSR_IOstChange);
			tag_UIdelegate = UIdelegate_;
			tag_delegateStationSelect = selectstation;
			InitializeComponent();
		}

		private void IOoutputStatus_Load(object sender, EventArgs e)
		{
			if (arrOutputIo != null)
			{
				label1.Text = arrOutputIo.StrIoName; //从ArrAxis拿轴名称

			}

		}

		public void UIdelegate_(bool var)
		{
			OutputBT.BackColor = !var ? Color.Gainsboro : Color.LawnGreen;
			OutputBT.Text = !var ? "OFF" : "ON";
		}

		public void IOoutputStatus_Load(IOParameter arrOutputIo_)
		{
			arrOutputIo = arrOutputIo_;
			if (arrOutputIo != null)
			{
				short var = NewCtrlCardV0.GetOutputIoBit(arrOutputIo, 0);

				OutputBT.BackColor = ((var > 0 && arrOutputIo.Logic == 1) ||
				 (var < 1 && arrOutputIo.Logic == 0)) ? Color.Gainsboro : Color.LawnGreen;
				label1.Text = arrOutputIo.StrIoName; //从ArrAxis拿轴名称

				OutputBT.Text = ((var > 0 && arrOutputIo.Logic == 1) ||
				 (var < 1 && arrOutputIo.Logic == 0)) ? "ON" : "OFF";
				label1.Text = arrOutputIo.StrIoName; //从ArrAxis拿轴名称

				btstatus = ((var > 0 && arrOutputIo.Logic == 1) ||
				 (var < 1 && arrOutputIo.Logic == 0)) ? true : false;
				label1.Text = arrOutputIo.StrIoName;


			}

		}
		//输出按钮
		private void OutputBT_Click(object sender, EventArgs e)
		{
			if (arrOutputIo == null)
			{
				return;
			}
			if (arrOutputIo.tagPointAggregate != null && arrOutputIo.tagPointAggregate.tag_AxisSafeManage != null && !arrOutputIo.tagPointAggregate.tag_AxisSafeManage.PointIsSafe(arrOutputIo.tagPointAggregate))
			{
				MessageBoxLog.Show("失败");
				return;
			}
			if (btstatus == false)
			{
				result = NewCtrlCardV0.SetOutputIoBit(arrOutputIo, 1);
				if (result != 0)
				{
					MessageBoxLog.Show("置位IO失败");
					return;
				}
				OutputBT.Text = "ON";
				OutputBT.BackColor = Color.LawnGreen;
				btstatus = true;
			}
			else
			{
				result = NewCtrlCardV0.SetOutputIoBit(arrOutputIo, 0);
				if (result != 0)
				{
					MessageBoxLog.Show("复位IO失败");
					return;
				}
				OutputBT.Text = "OFF";
				OutputBT.BackColor = Color.Gainsboro;
				btstatus = false;
			}

		}


		//IO状态C:\Users\Administrator\Desktop\Manual_Debug\Manual_Debug\UserCtrl\IOoutputStatus.cs
		public void NewCtrlCardSR_IOstChange(object sender, EventArgs e)
		{
			try
			{
				CardInputIOEvengArgs cIOE = e as CardInputIOEvengArgs;
				if (arrOutputIo != null && cIOE.CardNum == arrOutputIo.CardNum && cIOE.type == (int)arrOutputIo.tag_MotionCardManufacturer)
				{
					bool var = (((cIOE.Value & ((ulong)(1 << arrOutputIo.IOBit))) > 0 && arrOutputIo.Logic == 1) || ((cIOE.Value & ((ulong)(1 << arrOutputIo.IOBit))) < 1 && arrOutputIo.Logic == 0));

					btstatus = !var;
					this.Invoke(tag_UIdelegate, btstatus);
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

		private void button1_Click(object sender, EventArgs e)
		{
			if (arrOutputIo.tag_StationModule == null)
				arrOutputIo.tag_StationModule = new StationModule();
			arrOutputIo.tag_StationModule.strStationName = arrOutputIo.StrIoName;
			if (tag_delegateStationSelect != null)
				tag_delegateStationSelect(arrOutputIo.tag_StationModule);

		}
	}
}
