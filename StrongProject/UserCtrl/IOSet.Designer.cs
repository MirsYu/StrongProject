namespace StrongProject
{
	partial class IOSet
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
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.panel_out = new System.Windows.Forms.Panel();
			this.panel_IO = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// panel_out
			// 
			this.panel_out.AutoScroll = true;
			this.panel_out.Location = new System.Drawing.Point(713, 3);
			this.panel_out.Name = "panel_out";
			this.panel_out.Size = new System.Drawing.Size(394, 607);
			this.panel_out.TabIndex = 7;
			// 
			// panel_IO
			// 
			this.panel_IO.AutoScroll = true;
			this.panel_IO.Location = new System.Drawing.Point(3, 3);
			this.panel_IO.Name = "panel_IO";
			this.panel_IO.Size = new System.Drawing.Size(704, 607);
			this.panel_IO.TabIndex = 6;
			// 
			// IOSet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel_out);
			this.Controls.Add(this.panel_IO);
			this.DoubleBuffered = true;
			this.Name = "IOSet";
			this.Size = new System.Drawing.Size(1110, 613);
			this.Load += new System.EventHandler(this.IOSet_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel_out;
		private System.Windows.Forms.Panel panel_IO;
	}
}
