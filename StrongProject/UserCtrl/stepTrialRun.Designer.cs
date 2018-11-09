namespace StrongProject
{
	partial class stepTrialRun
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
			this.button_LineCheck = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// button_LineCheck
			// 
			this.button_LineCheck.BackColor = System.Drawing.SystemColors.Control;
			this.button_LineCheck.Location = new System.Drawing.Point(208, 1);
			this.button_LineCheck.Name = "button_LineCheck";
			this.button_LineCheck.Size = new System.Drawing.Size(88, 23);
			this.button_LineCheck.TabIndex = 4;
			this.button_LineCheck.Text = "皮带线1";
			this.button_LineCheck.UseVisualStyleBackColor = false;
			this.button_LineCheck.Click += new System.EventHandler(this.button_LineCheck_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(12, 8);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(84, 16);
			this.checkBox1.TabIndex = 9;
			this.checkBox1.Text = "本工位暂停";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(118, 7);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(84, 16);
			this.checkBox2.TabIndex = 10;
			this.checkBox2.Text = "本工位屏蔽";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
			// 
			// stepTrialRun
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.button_LineCheck);
			this.Name = "stepTrialRun";
			this.Size = new System.Drawing.Size(320, 31);
			this.Load += new System.EventHandler(this.stepTrialRun_Load);
			this.DoubleBuffered = true;this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button_LineCheck;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
	}
}
