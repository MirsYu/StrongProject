namespace StrongProject
{
	partial class UserControl_socketList
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
			this.components = new System.ComponentModel.Container();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.删除ToolStripMenuItem,
			this.添加ToolStripMenuItem,
			this.保存ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(95, 70);
			// 
			// 删除ToolStripMenuItem
			// 
			this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
			this.删除ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
			this.删除ToolStripMenuItem.Text = "删除";
			this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
			// 
			// 添加ToolStripMenuItem
			// 
			this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
			this.添加ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
			this.添加ToolStripMenuItem.Text = "添加";
			this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
			// 
			// 保存ToolStripMenuItem
			// 
			this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
			this.保存ToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
			this.保存ToolStripMenuItem.Text = "保存";
			this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click_1);
			// 
			// UserControl_socketList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.contextMenuStrip1;
			this.Name = "UserControl_socketList";
			this.Size = new System.Drawing.Size(945, 150);
			this.Load += new System.EventHandler(this.UserControl_socketList_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.DoubleBuffered = true;this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
	}
}
