namespace StrongProject
{
	partial class UserControl_configIni
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
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.info = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.设置为空跑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.设置为临时系统配置文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.名称,
			this.info});
			this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
			this.dataGridView1.Location = new System.Drawing.Point(3, 3);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(880, 366);
			this.dataGridView1.TabIndex = 0;
			// 
			// 名称
			// 
			this.名称.HeaderText = "配置文件名";
			this.名称.Name = "名称";
			// 
			// info
			// 
			this.info.HeaderText = "说明";
			this.info.Name = "info";
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.删除ToolStripMenuItem,
			this.重命名ToolStripMenuItem,
			this.刷新ToolStripMenuItem,
			this.设置为空跑ToolStripMenuItem,
			this.保存ToolStripMenuItem,
			this.设置为临时系统配置文件ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(203, 158);
			// 
			// 删除ToolStripMenuItem
			// 
			this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
			this.删除ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.删除ToolStripMenuItem.Text = "删除";
			this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
			// 
			// 重命名ToolStripMenuItem
			// 
			this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
			this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.重命名ToolStripMenuItem.Text = "重命名";
			this.重命名ToolStripMenuItem.Click += new System.EventHandler(this.重命名ToolStripMenuItem_Click);
			// 
			// 刷新ToolStripMenuItem
			// 
			this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
			this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.刷新ToolStripMenuItem.Text = "刷新";
			this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
			// 
			// 设置为空跑ToolStripMenuItem
			// 
			this.设置为空跑ToolStripMenuItem.Name = "设置为空跑ToolStripMenuItem";
			this.设置为空跑ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.设置为空跑ToolStripMenuItem.Text = "设置为系统配置文件";
			this.设置为空跑ToolStripMenuItem.Click += new System.EventHandler(this.设置为空跑ToolStripMenuItem_Click_1);
			// 
			// 保存ToolStripMenuItem
			// 
			this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
			this.保存ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.保存ToolStripMenuItem.Text = "保存";
			// 
			// 设置为临时系统配置文件ToolStripMenuItem
			// 
			this.设置为临时系统配置文件ToolStripMenuItem.Name = "设置为临时系统配置文件ToolStripMenuItem";
			this.设置为临时系统配置文件ToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
			this.设置为临时系统配置文件ToolStripMenuItem.Text = "设置为临时系统配置文件";
			this.设置为临时系统配置文件ToolStripMenuItem.Click += new System.EventHandler(this.设置为临时系统配置文件ToolStripMenuItem_Click);
			// 
			// UserControl_configIni
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.dataGridView1);
			this.Name = "UserControl_configIni";
			this.Size = new System.Drawing.Size(905, 372);
			this.Load += new System.EventHandler(this.UserControl_configIni_Load);
			this.SizeChanged += new System.EventHandler(this.UserControl_configIni_SizeChanged);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn 名称;
		private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 设置为空跑ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
		private System.Windows.Forms.DataGridViewTextBoxColumn info;
		private System.Windows.Forms.ToolStripMenuItem 设置为临时系统配置文件ToolStripMenuItem;
	}
}
