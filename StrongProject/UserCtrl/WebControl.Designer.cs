namespace StrongProject
{
	partial class WebControl
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.webBrowser_NG = new System.Windows.Forms.WebBrowser();
			this.SuspendLayout();
			// 
			// webBrowser_NG
			// 
			this.webBrowser_NG.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser_NG.Location = new System.Drawing.Point(0, 0);
			this.webBrowser_NG.MinimumSize = new System.Drawing.Size(20, 20);
			this.webBrowser_NG.Name = "webBrowser_NG";
			this.webBrowser_NG.ScrollBarsEnabled = false;
			this.webBrowser_NG.Size = new System.Drawing.Size(521, 388);
			this.webBrowser_NG.TabIndex = 0;
			this.webBrowser_NG.Url = new System.Uri("http://127.0.0.1:8080/HTML", System.UriKind.Absolute);
			this.webBrowser_NG.WebBrowserShortcutsEnabled = false;
			// 
			// WebControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.webBrowser_NG);
			this.DoubleBuffered = true;
			this.Name = "WebControl";
			this.Size = new System.Drawing.Size(521, 388);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.WebBrowser webBrowser_NG;
	}
}
