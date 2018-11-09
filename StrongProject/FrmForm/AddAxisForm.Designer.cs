namespace StrongProject
{
	partial class AddAxisForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panel_Axis = new System.Windows.Forms.Panel();
			this.button9 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button_RemoveAxis = new System.Windows.Forms.Button();
			this.button_AddAxis = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// panel_Axis
			// 
			this.panel_Axis.AutoScroll = true;
			this.panel_Axis.BackColor = System.Drawing.SystemColors.Control;
			this.panel_Axis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_Axis.Location = new System.Drawing.Point(37, 27);
			this.panel_Axis.Name = "panel_Axis";
			this.panel_Axis.Size = new System.Drawing.Size(776, 368);
			this.panel_Axis.TabIndex = 3;
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(71, 422);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(75, 32);
			this.button9.TabIndex = 58;
			this.button9.Text = "保存";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(522, 419);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(89, 32);
			this.button6.TabIndex = 57;
			this.button6.Text = "轴导入";
			this.button6.UseVisualStyleBackColor = true;
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(406, 419);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(89, 32);
			this.button7.TabIndex = 56;
			this.button7.Text = "轴导出";
			this.button7.UseVisualStyleBackColor = true;
			// 
			// button_RemoveAxis
			// 
			this.button_RemoveAxis.Location = new System.Drawing.Point(305, 416);
			this.button_RemoveAxis.Name = "button_RemoveAxis";
			this.button_RemoveAxis.Size = new System.Drawing.Size(82, 38);
			this.button_RemoveAxis.TabIndex = 55;
			this.button_RemoveAxis.Text = "删除轴";
			this.button_RemoveAxis.UseVisualStyleBackColor = true;
			this.button_RemoveAxis.Click += new System.EventHandler(this.button_RemoveAxis_Click);
			// 
			// button_AddAxis
			// 
			this.button_AddAxis.Location = new System.Drawing.Point(175, 416);
			this.button_AddAxis.Name = "button_AddAxis";
			this.button_AddAxis.Size = new System.Drawing.Size(82, 38);
			this.button_AddAxis.TabIndex = 54;
			this.button_AddAxis.Text = "添加轴";
			this.button_AddAxis.UseVisualStyleBackColor = true;
			this.button_AddAxis.Click += new System.EventHandler(this.button_AddAxis_Click);
			// 
			// AddAxisForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(862, 481);
			this.Controls.Add(this.button9);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button_RemoveAxis);
			this.Controls.Add(this.button_AddAxis);
			this.Controls.Add(this.panel_Axis);
			this.Name = "AddAxisForm";
			this.Text = "AddAxisForm";
			this.Load += new System.EventHandler(this.AddAxisForm_Load);
			this.DoubleBuffered = true;this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel_Axis;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button_RemoveAxis;
		private System.Windows.Forms.Button button_AddAxis;
	}
}