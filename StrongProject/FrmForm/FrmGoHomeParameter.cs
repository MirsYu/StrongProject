using System;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class FrmGoHomeParameter : Form
	{
		public AxisConfig tag_AxisConfig;
		public FrmGoHomeParameter()
		{
			InitializeComponent();
		}
		public FrmGoHomeParameter(AxisConfig ax)
		{
			tag_AxisConfig = ax;
			InitializeComponent();
		}

		private void FrmGoHomeParameter_Load(object sender, EventArgs e)
		{
			if (tag_AxisConfig != null)
			{
				goHomeParameter gohome = new goHomeParameter(tag_AxisConfig);
				this.Controls.Add(gohome);
			}

		}
	}
}
