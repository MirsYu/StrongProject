using System.Windows.Forms;
namespace StrongProject
{
	public class MessageBoxLog
	{
		public static Form_MessageBos log;
		// 摘要:
		//     显示具有指定文本的消息框。
		//
		// 参数:
		//   text:
		//     要在消息框中显示的文本。
		//
		// 返回结果:
		//     System.Windows.Forms.DialogResult 值之一。
		public static DialogResult Show(string text)
		{
			UserControl_LogOut.OutLog(text, 0);
			log = new Form_MessageBos(text, "", MessageBoxButtons.OK, MessageBoxIcon.Question);
			DialogResult tag_buttons = log.ShowDialog();
			log = null;
			return tag_buttons;



		}

		//
		// 摘要:
		//     显示具有指定文本和标题的消息框。
		//
		// 参数:
		//   text:
		//     要在消息框中显示的文本。
		//
		//   caption:
		//     要在消息框的标题栏中显示的文本。
		//
		// 返回结果:
		//     System.Windows.Forms.DialogResult 值之一。

		public static DialogResult Show(string text, string caption)
		{
			UserControl_LogOut.OutLog(text, 0);
			Form_MessageBos box = new Form_MessageBos(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Question);
			DialogResult tag_buttons = box.ShowDialog();
			return tag_buttons;
		}


		//
		// 摘要:
		//     显示具有指定文本、标题、按钮和图标的消息框。
		//
		// 参数:
		//   text:
		//     要在消息框中显示的文本。
		//
		//   caption:
		//     要在消息框的标题栏中显示的文本。
		//
		//   buttons:
		//     System.Windows.Forms.MessageBoxButtons 值之一，可指定在消息框中显示哪些按钮。
		//
		//   icon:
		//     System.Windows.Forms.MessageBoxLog 值之一，它指定在消息框中显示哪个图标。
		//
		// 返回结果:
		//     System.Windows.Forms.DialogResult 值之一。
		//
		// 异常:
		//   System.ComponentModel.InvalidEnumArgumentException:
		//     指定的 buttons 参数不是 System.Windows.Forms.MessageBoxButtons 的成员。- 或 -指定的 icon
		//     参数不是 System.Windows.Forms.MessageBoxIcon 的成员。
		//
		//   System.InvalidOperationException:
		//     尝试在运行模式不是用户交互模式的进程中显示 System.Windows.Forms.MessageBoxLog。这是由 System.Windows.Forms.SystemInformation.UserInteractive
		//     属性指定的。
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			UserControl_LogOut.OutLog(text, 0);
			Form_MessageBos box = new Form_MessageBos(text, caption, buttons, icon);
			DialogResult tag_buttons = box.ShowDialog();
			return tag_buttons;
			//   return MessageBox.Show(text, caption, buttons, icon);

		}





	}
}
