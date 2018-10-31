using System;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class UCL_StationMotion : UserControl
	{
		public Work tag_Work;
		public UCL_StationMotion()
		{
			InitializeComponent();
		}

		private void UCL_StationMotion_Load(object sender, EventArgs e)
		{
			int offsetH = 0;
			stepTrialRun _stepTrialRun = null;
			if (tag_Work == null)
				return;

			foreach (object o in tag_Work.tag_workObject)
			{
				workBase wb = (workBase)o;
				if (o == null)
					return;
				_stepTrialRun = new stepTrialRun(o);
				this.Controls.Add(_stepTrialRun);
				_stepTrialRun.Location = new Point(0, offsetH);
				offsetH = _stepTrialRun.Size.Height + offsetH;

			}



		}
	}
}
