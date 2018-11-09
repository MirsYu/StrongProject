namespace StrongProject
{
	partial class LogIn
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogIn));
			this.button_cancel = new System.Windows.Forms.Button();
			this.button_ok = new System.Windows.Forms.Button();
			this.textBox_password = new System.Windows.Forms.TextBox();
			this.comboBox_type = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.BtnToOp = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// button_cancel
			// 
			this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button_cancel.Font = new System.Drawing.Font("Consolas", 9F);
			this.button_cancel.Location = new System.Drawing.Point(167, 203);
			this.button_cancel.Name = "button_cancel";
			this.button_cancel.Size = new System.Drawing.Size(198, 33);
			this.button_cancel.TabIndex = 8;
			this.button_cancel.Text = "修改密码";
			this.button_cancel.UseVisualStyleBackColor = true;
			this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
			// 
			// button_ok
			// 
			this.button_ok.Font = new System.Drawing.Font("Consolas", 9F);
			this.button_ok.Location = new System.Drawing.Point(167, 158);
			this.button_ok.Name = "button_ok";
			this.button_ok.Size = new System.Drawing.Size(103, 33);
			this.button_ok.TabIndex = 9;
			this.button_ok.Text = "登陆";
			this.button_ok.UseVisualStyleBackColor = true;
			this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
			// 
			// textBox_password
			// 
			this.textBox_password.Location = new System.Drawing.Point(230, 112);
			this.textBox_password.Name = "textBox_password";
			this.textBox_password.PasswordChar = '*';
			this.textBox_password.Size = new System.Drawing.Size(135, 21);
			this.textBox_password.TabIndex = 7;
			this.textBox_password.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_password_KeyDown);
			// 
			// comboBox_type
			// 
			this.comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_type.FormattingEnabled = true;
			this.comboBox_type.Location = new System.Drawing.Point(230, 79);
			this.comboBox_type.Name = "comboBox_type";
			this.comboBox_type.Size = new System.Drawing.Size(135, 20);
			this.comboBox_type.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(164, 114);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(59, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "用户密码:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(164, 81);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "用户类型:";
			// 
			// BtnToOp
			// 
			this.BtnToOp.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.BtnToOp.Font = new System.Drawing.Font("Consolas", 9F);
			this.BtnToOp.Location = new System.Drawing.Point(276, 158);
			this.BtnToOp.Name = "BtnToOp";
			this.BtnToOp.Size = new System.Drawing.Size(89, 33);
			this.BtnToOp.TabIndex = 8;
			this.BtnToOp.Text = "切换到操作员";
			this.BtnToOp.UseVisualStyleBackColor = true;
			this.BtnToOp.Click += new System.EventHandler(this.BtnToOp_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Consolas", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(111, 9);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(158, 31);
			this.label3.TabIndex = 16;
			this.label3.Text = "用户登录系统";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(31, 96);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(104, 109);
			this.pictureBox1.TabIndex = 17;
			this.pictureBox1.TabStop = false;
			// 
			// LogIn
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(396, 258);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.BtnToOp);
			this.Controls.Add(this.button_cancel);
			this.Controls.Add(this.button_ok);
			this.Controls.Add(this.textBox_password);
			this.Controls.Add(this.comboBox_type);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "LogIn";
			this.Text = "LogIn";
			this.Load += new System.EventHandler(this.LogIn_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LogIn_KeyDown);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.DoubleBuffered = true;this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Button button_ok;
		private System.Windows.Forms.TextBox textBox_password;
		private System.Windows.Forms.ComboBox comboBox_type;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BtnToOp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}