namespace StrongProject
{
	partial class UserControl_AxisSafe
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
			this.label1 = new System.Windows.Forms.Label();
			this.textBox_Max = new System.Windows.Forms.TextBox();
			this.textBox_Min = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.button_Save = new System.Windows.Forms.Button();
			this.label_Asix = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(98, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "最大极限";
			// 
			// textBox_Max
			// 
			this.textBox_Max.Location = new System.Drawing.Point(157, 3);
			this.textBox_Max.Name = "textBox_Max";
			this.textBox_Max.Size = new System.Drawing.Size(100, 21);
			this.textBox_Max.TabIndex = 1;
			// 
			// textBox_Min
			// 
			this.textBox_Min.Location = new System.Drawing.Point(322, 3);
			this.textBox_Min.Name = "textBox_Min";
			this.textBox_Min.Size = new System.Drawing.Size(100, 21);
			this.textBox_Min.TabIndex = 3;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(263, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 2;
			this.label2.Text = "最小极限";
			// 
			// button_Save
			// 
			this.button_Save.Location = new System.Drawing.Point(428, 3);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(75, 23);
			this.button_Save.TabIndex = 4;
			this.button_Save.Text = "保存";
			this.button_Save.UseVisualStyleBackColor = true;
			this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
			// 
			// label_Asix
			// 
			this.label_Asix.AutoSize = true;
			this.label_Asix.Location = new System.Drawing.Point(12, 7);
			this.label_Asix.Name = "label_Asix";
			this.label_Asix.Size = new System.Drawing.Size(41, 12);
			this.label_Asix.TabIndex = 5;
			this.label_Asix.Text = "轴名称";
			// 
			// UserControl_AxisSafe
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.label_Asix);
			this.Controls.Add(this.button_Save);
			this.Controls.Add(this.textBox_Min);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox_Max);
			this.Controls.Add(this.label1);
			this.Name = "UserControl_AxisSafe";
			this.Size = new System.Drawing.Size(508, 27);
			this.Load += new System.EventHandler(this.UserControl_AxisSafe_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox_Max;
		private System.Windows.Forms.TextBox textBox_Min;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button_Save;
		private System.Windows.Forms.Label label_Asix;
	}
}
