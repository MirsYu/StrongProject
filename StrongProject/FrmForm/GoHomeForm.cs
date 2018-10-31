
using System;
using System.Threading;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class GoHomeForm : Form
	{
		private AxisConfig _Axis = null;
		public GoHomeForm(AxisConfig axis)
		{

			InitializeComponent();
			_Axis = axis;
		}

		private void GoHomeForm_Load(object sender, EventArgs e)
		{
			ThreadPool.QueueUserWorkItem(
				delegate
				{
					int iResult = NewCtrlCardV0.GoHome(_Axis);
					try
					{
						this.Invoke(
							(MethodInvoker)delegate
							{
								if (iResult != 0 && _Cancelled == false)
								{
									MessageBoxLog.Show("回零失败,请重新回零.");
								}
								this.Close();
							}
							);
					}
					catch (System.Exception ex)
					{

					}
				}
				);
		}
		private bool _Cancelled = false;
		private void button_cancel_Click(object sender, EventArgs e)
		{
			_Cancelled = true;
			NewCtrlCardV0.SR_AxisEmgStop((int)_Axis.tag_MotionCardManufacturer, _Axis.CardNum, (short)_Axis.AxisNum);
		}
	}
}
