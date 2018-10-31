using System;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class Form_MessageBos : Form
	{
		/// <summary>
		/// 标题
		/// </summary>
		public static string tag_formTitle;
		/// <summary>
		/// 内容
		/// </summary>
		public static string tag_formContent;

		public MessageBoxButtons tag_buttons;

		public DialogResult tag_DialogResult;

		public Form_MessageBos()
		{
			InitializeComponent();
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="text"></param>
		/// <param name="caption"></param>
		/// <param name="buttons"></param>
		/// <param name="icon"></param>
		public Form_MessageBos(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			tag_buttons = buttons;
			tag_formTitle = caption;
			tag_formContent = text;
			InitializeComponent();

		}
		public void AbortEventHandler(object sender, EventArgs e)
		{

			this.Close();

		}
		/// <summary>
		///  
		// 摘要:
		//     消息框包含“中止”、“重试”和“忽略”按钮。
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form_MessageBos_AbortRetryIgnoreShow(object sender, EventArgs e)
		{
			Button bt1 = new Button();
			Button bt2 = new Button();
			Button bt3 = new Button();
			bt1.Click += AbortEventHandler;
			bt2.Click += AbortEventHandler;
			bt3.Click += AbortEventHandler;
			bt2.Size = new Size(122, 46);
			bt1.Size = new Size(122, 46);
			bt3.Size = new Size(122, 46);
			label_content.Text = tag_formContent;
			label_content.Location = new Point((this.Size.Width - label_content.Size.Width) / 2, (this.Size.Height - label_content.Size.Height) / 2 - 50);

			this.Text = tag_formTitle;
			switch (tag_buttons)

			{
				case MessageBoxButtons.AbortRetryIgnore:
					{
						bt1.Location = new Point((this.Size.Width - (bt1.Size.Width) * 3 - 100) / 2, this.Size.Height * 3 / 4);
						bt2.Location = new Point(bt1.Location.X + bt1.Size.Width + 50, this.Size.Height * 3 / 4);
						bt3.Location = new Point(bt2.Location.X + bt2.Size.Width + 50, this.Size.Height * 3 / 4);

						bt1.Text = "中止";
						bt1.DialogResult = DialogResult.Abort;


						bt2.DialogResult = DialogResult.Retry;
						bt2.Text = "重试";


						bt3.Text = "忽略";
						bt3.DialogResult = DialogResult.Ignore;
						pictureBox1.Controls.Add(bt1);
						pictureBox1.Controls.Add(bt2);
						pictureBox1.Controls.Add(bt3);

					}
					break;
				case MessageBoxButtons.OK:
					{

						bt1.Location = new Point((this.Size.Width - (bt1.Size.Width)) / 2, this.Size.Height * 3 / 4);
						bt1.Text = "确定";
						bt1.DialogResult = DialogResult.OK;
						pictureBox1.Controls.Add(bt1);

					}
					break;

				case MessageBoxButtons.OKCancel:
					{
						bt1.Location = new Point((this.Size.Width - (bt1.Size.Width) * 2 - 50) / 2, this.Size.Height * 3 / 4);
						bt2.Location = new Point(bt1.Location.X + bt1.Size.Width + 50, this.Size.Height * 3 / 4);


						bt1.Text = "确定";
						bt1.DialogResult = DialogResult.OK;


						bt2.DialogResult = DialogResult.Cancel;
						bt2.Text = "取消";
						pictureBox1.Controls.Add(bt1);
						pictureBox1.Controls.Add(bt2);
					}
					break;

				case MessageBoxButtons.YesNo:
					{
						bt1.Location = new Point((this.Size.Width - (bt1.Size.Width) * 2 - 50) / 2, this.Size.Height * 3 / 4);
						bt2.Location = new Point(bt1.Location.X + bt1.Size.Width + 50, this.Size.Height * 3 / 4);
						bt1.Text = "是";
						bt1.DialogResult = DialogResult.Yes;


						bt2.DialogResult = DialogResult.No;
						bt2.Text = "否";

						pictureBox1.Controls.Add(bt1);
						pictureBox1.Controls.Add(bt2);

					}
					break;
				case MessageBoxButtons.YesNoCancel:
					{
						bt1.Location = new Point((this.Size.Width - (bt1.Size.Width) * 3 - 100) / 2, this.Size.Height * 3 / 4);
						bt2.Location = new Point(bt1.Location.X + bt1.Size.Width + 50, this.Size.Height * 3 / 4);
						bt3.Location = new Point(bt2.Location.X + bt2.Size.Width + 50, this.Size.Height * 3 / 4);

						bt1.Text = "是";
						bt1.DialogResult = DialogResult.Yes;


						bt2.DialogResult = DialogResult.No;
						bt2.Text = "否";


						bt3.Text = "取消";
						bt3.DialogResult = DialogResult.Cancel;
						pictureBox1.Controls.Add(bt1);
						pictureBox1.Controls.Add(bt2);
						pictureBox1.Controls.Add(bt3);
					}
					break;
			}



		}
		private void Form_MessageBos_Load(object sender, EventArgs e)
		{

			Form_MessageBos_AbortRetryIgnoreShow(null, null);
		}

	}
}
