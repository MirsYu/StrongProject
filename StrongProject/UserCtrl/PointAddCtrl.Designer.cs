namespace StrongProject
{
	partial class PointAddCtrl
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
			this.button15 = new System.Windows.Forms.Button();
			this.label14 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.button16 = new System.Windows.Forms.Button();
			this.comboBox6 = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// button15
			// 
			this.button15.Location = new System.Drawing.Point(249, -1);
			this.button15.Name = "button15";
			this.button15.Size = new System.Drawing.Size(32, 22);
			this.button15.TabIndex = 22;
			this.button15.Text = "+";
			this.button15.UseVisualStyleBackColor = true;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(285, 24);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(53, 12);
			this.label14.TabIndex = 25;
			this.label14.Text = "点位删除";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label12.Location = new System.Drawing.Point(4, 8);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(83, 17);
			this.label12.TabIndex = 20;
			this.label12.Text = "新增点位名称:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(285, 4);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(53, 12);
			this.label13.TabIndex = 24;
			this.label13.Text = "点位增加";
			// 
			// button16
			// 
			this.button16.Location = new System.Drawing.Point(249, 18);
			this.button16.Name = "button16";
			this.button16.Size = new System.Drawing.Size(32, 22);
			this.button16.TabIndex = 21;
			this.button16.Text = "-";
			this.button16.UseVisualStyleBackColor = true;
			// 
			// comboBox6
			// 
			this.comboBox6.FormattingEnabled = true;
			this.comboBox6.Location = new System.Drawing.Point(96, 8);
			this.comboBox6.Name = "comboBox6";
			this.comboBox6.Size = new System.Drawing.Size(143, 20);
			this.comboBox6.TabIndex = 23;
			// 
			// PointAddCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.button16);
			this.Controls.Add(this.comboBox6);
			this.Name = "PointAddCtrl";
			this.Size = new System.Drawing.Size(340, 42);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button15;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Button button16;
		private System.Windows.Forms.ComboBox comboBox6;
	}
}
