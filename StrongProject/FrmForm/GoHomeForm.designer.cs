namespace StrongProject
{
	partial class GoHomeForm
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
			this.GoHomeGroupBox = new System.Windows.Forms.GroupBox();
			this.button_Cancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.GoHomeGroupBox.SuspendLayout();
			this.SuspendLayout();
			// 
			// GoHomeGroupBox
			// 
			this.GoHomeGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GoHomeGroupBox.Controls.Add(this.button_Cancel);
			this.GoHomeGroupBox.Controls.Add(this.label1);
			this.GoHomeGroupBox.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.GoHomeGroupBox.Location = new System.Drawing.Point(1, -7);
			this.GoHomeGroupBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.GoHomeGroupBox.Name = "GoHomeGroupBox";
			this.GoHomeGroupBox.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.GoHomeGroupBox.Size = new System.Drawing.Size(367, 104);
			this.GoHomeGroupBox.TabIndex = 0;
			this.GoHomeGroupBox.TabStop = false;
			// 
			// button_Cancel
			// 
			this.button_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_Cancel.Location = new System.Drawing.Point(138, 59);
			this.button_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.button_Cancel.Name = "button_Cancel";
			this.button_Cancel.Size = new System.Drawing.Size(87, 33);
			this.button_Cancel.TabIndex = 1;
			this.button_Cancel.Text = "取消";
			this.button_Cancel.UseVisualStyleBackColor = true;
			this.button_Cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("幼圆", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(87, 28);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "设备正在回零,请等待...";
			// 
			// GoHomeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.button_Cancel;
			this.ClientSize = new System.Drawing.Size(369, 98);
			this.Controls.Add(this.GoHomeGroupBox);
			this.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "GoHomeForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "GoHomeForm";
			this.Load += new System.EventHandler(this.GoHomeForm_Load);
			this.GoHomeGroupBox.ResumeLayout(false);
			this.GoHomeGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox GoHomeGroupBox;
		private System.Windows.Forms.Button button_Cancel;
		private System.Windows.Forms.Label label1;
	}
}