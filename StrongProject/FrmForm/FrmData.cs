using System;
using System.Windows.Forms;
namespace StrongProject
{
	public partial class FrmData : Form
	{

		public Frm_Frame fmPro = null;//    new FrmFrame();
		public Work tag_Work;


		public FrmData(Frm_Frame frm, Work _Work)
		{
			tag_Work = _Work;
			fmPro = frm;
			InitializeComponent();
		}


		private void FrmData_Load(object sender, EventArgs e)
		{




		}
	}
}
