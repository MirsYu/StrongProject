namespace StrongProject
{
	partial class UserControl_ShowAllPoint
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
			this.CBpointMessage = new System.Windows.Forms.GroupBox();
			this.plpointMessage = new System.Windows.Forms.Panel();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.运动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CBpointMessage.SuspendLayout();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CBpointMessage
			// 
			this.CBpointMessage.Controls.Add(this.plpointMessage);
			this.CBpointMessage.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.CBpointMessage.Location = new System.Drawing.Point(18, 3);
			this.CBpointMessage.Name = "CBpointMessage";
			this.CBpointMessage.Size = new System.Drawing.Size(564, 400);
			this.CBpointMessage.TabIndex = 44;
			this.CBpointMessage.TabStop = false;
			this.CBpointMessage.Text = "点位信息栏";
			// 
			// plpointMessage
			// 
			this.plpointMessage.AutoScroll = true;
			this.plpointMessage.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.plpointMessage.ContextMenuStrip = this.contextMenuStrip1;
			this.plpointMessage.Location = new System.Drawing.Point(4, 20);
			this.plpointMessage.Name = "plpointMessage";
			this.plpointMessage.Size = new System.Drawing.Size(554, 350);
			this.plpointMessage.TabIndex = 38;
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.运动ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
			// 
			// 运动ToolStripMenuItem
			// 
			this.运动ToolStripMenuItem.Name = "运动ToolStripMenuItem";
			this.运动ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.运动ToolStripMenuItem.Text = "保存所有";
			this.运动ToolStripMenuItem.Click += new System.EventHandler(this.运动ToolStripMenuItem_Click);
			// 
			// UserControl_ShowAllPoint
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CBpointMessage);
			this.Name = "UserControl_ShowAllPoint";
			this.Size = new System.Drawing.Size(592, 400);
			this.Load += new System.EventHandler(this.UserControl_ShowAllPoint_Load);
			this.SizeChanged += new System.EventHandler(this.UserControl_ShowAllPoint_SizeChanged);
			this.CBpointMessage.ResumeLayout(false);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox CBpointMessage;
		private System.Windows.Forms.Panel plpointMessage;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 运动ToolStripMenuItem;
	}
}
