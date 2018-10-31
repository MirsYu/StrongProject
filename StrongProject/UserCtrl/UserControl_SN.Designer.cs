namespace StrongProject.UserCtrl
{
	partial class UserControl_SN
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
			this.checkBox_PDCA = new System.Windows.Forms.CheckBox();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.checkBox_SnEm = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// checkBox_PDCA
			// 
			this.checkBox_PDCA.AutoSize = true;
			this.checkBox_PDCA.Location = new System.Drawing.Point(3, 3);
			this.checkBox_PDCA.Name = "checkBox_PDCA";
			this.checkBox_PDCA.Size = new System.Drawing.Size(48, 16);
			this.checkBox_PDCA.TabIndex = 0;
			this.checkBox_PDCA.Text = "PDCA";
			this.checkBox_PDCA.UseVisualStyleBackColor = true;
			this.checkBox_PDCA.CheckedChanged += new System.EventHandler(this.checkBox_PDCA_CheckedChanged);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// checkBox_SnEm
			// 
			this.checkBox_SnEm.AutoSize = true;
			this.checkBox_SnEm.Location = new System.Drawing.Point(57, 3);
			this.checkBox_SnEm.Name = "checkBox_SnEm";
			this.checkBox_SnEm.Size = new System.Drawing.Size(84, 16);
			this.checkBox_SnEm.TabIndex = 1;
			this.checkBox_SnEm.Text = "SN可以为空";
			this.checkBox_SnEm.UseVisualStyleBackColor = true;
			this.checkBox_SnEm.CheckedChanged += new System.EventHandler(this.checkBox_SnEm_CheckedChanged);
			// 
			// UserControl_SN
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.checkBox_SnEm);
			this.Controls.Add(this.checkBox_PDCA);
			this.Name = "UserControl_SN";
			this.Size = new System.Drawing.Size(496, 30);
			this.Load += new System.EventHandler(this.UserControl_SN_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer timer1;
		public System.Windows.Forms.CheckBox checkBox_PDCA;
		public System.Windows.Forms.CheckBox checkBox_SnEm;
	}
}
