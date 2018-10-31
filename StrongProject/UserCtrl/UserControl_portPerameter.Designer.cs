namespace StrongProject.UserCtrl
{
	partial class UserControl_portPerameter
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.删除一个ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加一个ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.ContextMenuStrip = this.contextMenuStrip1;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(535, 320);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.删除一个ToolStripMenuItem,
			this.添加一个ToolStripMenuItem,
			this.保存ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(119, 70);
			// 
			// 删除一个ToolStripMenuItem
			// 
			this.删除一个ToolStripMenuItem.Name = "删除一个ToolStripMenuItem";
			this.删除一个ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.删除一个ToolStripMenuItem.Text = "删除一个";
			this.删除一个ToolStripMenuItem.Click += new System.EventHandler(this.删除一个ToolStripMenuItem_Click);
			// 
			// 添加一个ToolStripMenuItem
			// 
			this.添加一个ToolStripMenuItem.Name = "添加一个ToolStripMenuItem";
			this.添加一个ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.添加一个ToolStripMenuItem.Text = "添加一个";
			this.添加一个ToolStripMenuItem.Click += new System.EventHandler(this.添加一个ToolStripMenuItem_Click);
			// 
			// 保存ToolStripMenuItem
			// 
			this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
			this.保存ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.保存ToolStripMenuItem.Text = "保存";
			this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click);
			// 
			// UserControl_portPerameter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "UserControl_portPerameter";
			this.Size = new System.Drawing.Size(575, 454);
			this.Load += new System.EventHandler(this.UserControl_portPerameter_Load);
			this.SizeChanged += new System.EventHandler(this.UserControl_portPerameter_SizeChanged);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 删除一个ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 添加一个ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
	}
}
