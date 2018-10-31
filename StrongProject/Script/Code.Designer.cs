namespace StrongProject
{
	partial class Code
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
			this.button_Build = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button_Build
			// 
			this.button_Build.Location = new System.Drawing.Point(905, 677);
			this.button_Build.Name = "button_Build";
			this.button_Build.Size = new System.Drawing.Size(75, 23);
			this.button_Build.TabIndex = 0;
			this.button_Build.Text = "保存并编译";
			this.button_Build.UseVisualStyleBackColor = true;
			this.button_Build.Click += new System.EventHandler(this.button_Build_Click);
			// 
			// Code
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(992, 712);
			this.Controls.Add(this.button_Build);
			this.Name = "Code";
			this.Text = "Code";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Code_FormClosing);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button button_Build;
	}
}