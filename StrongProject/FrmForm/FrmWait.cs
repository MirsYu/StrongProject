using System;
using System.Threading;
using System.Windows.Forms;
namespace StrongProject
{
	public partial class FrmWait : Form
	{
		public delegate short delegate_Exe(object o);

		public delegate_Exe tag_delegate_Exe;
		public delegate_Exe tag_delegate_End;
		public object tag_o;
		public FrmWait(delegate_Exe exe, object o)
		{
			tag_delegate_Exe = exe;
			tag_o = o;
			InitializeComponent();
		}
		public FrmWait(delegate_Exe exe, delegate_Exe end, object o)
		{
			tag_delegate_Exe = exe;
			tag_o = o;
			tag_delegate_End = end;
			InitializeComponent();
		}
		private void FrmWait_Load(object sender, EventArgs e)
		{
			ThreadPool.QueueUserWorkItem(
				 delegate
				 {

					 try
					 {
						 short ret = tag_delegate_Exe(tag_o);
						 this.Invoke(
							 (MethodInvoker)delegate
							 {

								 if (tag_delegate_End != null)
								 {

									 tag_delegate_End(tag_o);
								 }
								 else
								 {
									 if (ret != 0)
									 {
										 MessageBoxLog.Show("操作失败");
									 }
								 }
								 this.Close();

							 }
							 );
					 }
					 catch (System.Exception ex)
					 {
						 UserControl_LogOut.OutLog(ex.Message, 0);
					 }
				 }
				 );
		}
		private bool _Cancelled = false;
		private void button_cancel_Click(object sender, EventArgs e)
		{


		}

		private void button_cancel_Click_1(object sender, EventArgs e)
		{
			StationManage.StopAllAxis();
			Global.WorkVar.tag_IsExit = 1;
			_Cancelled = true;
		}
	}
}
