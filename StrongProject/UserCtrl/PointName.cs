using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class PointName : UserControl
	{

		AxisConfig arrAxis; //定义一个AxisConfig的对象
		public PointName()
		{
			InitializeComponent();
		}

		public PointName(AxisConfig arrAxis_) //初始化轴参数数组
		{
			arrAxis = arrAxis_;
			InitializeComponent();
		}

		//PointName控件加载
		private void PointName_Load(object sender, EventArgs e)
		{
			if (arrAxis != null)
			{
				lblname.Text = arrAxis.AxisName;//从ArrAxis拿轴名称

			}
		}
	}
}
