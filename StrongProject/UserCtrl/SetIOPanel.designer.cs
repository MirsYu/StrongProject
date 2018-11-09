namespace StrongProject
{
	partial class SetIOPanel
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
			this.comboBox_motiontype = new System.Windows.Forms.ComboBox();
			this.comboBox_io = new System.Windows.Forms.ComboBox();
			this.comboBox_status = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox_Type = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// comboBox_motiontype
			// 
			this.comboBox_motiontype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_motiontype.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.comboBox_motiontype.FormattingEnabled = true;
			this.comboBox_motiontype.Items.AddRange(new object[] {
			"无",
			"1卡",
			"2卡",
			"3卡",
			"4卡",
			"5卡",
			"6卡",
			"7卡",
			"8卡",
			"9卡",
			"10卡",
			"11卡",
			"12卡",
			"13卡"});
			this.comboBox_motiontype.Location = new System.Drawing.Point(115, 1);
			this.comboBox_motiontype.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBox_motiontype.Name = "comboBox_motiontype";
			this.comboBox_motiontype.Size = new System.Drawing.Size(30, 20);
			this.comboBox_motiontype.TabIndex = 1;
			// 
			// comboBox_io
			// 
			this.comboBox_io.DisplayMember = "323";
			this.comboBox_io.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_io.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.comboBox_io.FormattingEnabled = true;
			this.comboBox_io.Items.AddRange(new object[] {
			"无"});
			this.comboBox_io.Location = new System.Drawing.Point(146, 1);
			this.comboBox_io.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBox_io.Name = "comboBox_io";
			this.comboBox_io.Size = new System.Drawing.Size(40, 20);
			this.comboBox_io.TabIndex = 1;
			// 
			// comboBox_status
			// 
			this.comboBox_status.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_status.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.comboBox_status.FormattingEnabled = true;
			this.comboBox_status.Items.AddRange(new object[] {
			"无"});
			this.comboBox_status.Location = new System.Drawing.Point(185, 1);
			this.comboBox_status.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBox_status.Name = "comboBox_status";
			this.comboBox_status.Size = new System.Drawing.Size(35, 20);
			this.comboBox_status.TabIndex = 1;
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textBox1.Location = new System.Drawing.Point(3, 2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(109, 21);
			this.textBox1.TabIndex = 2;
			// 
			// comboBox_Type
			// 
			this.comboBox_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox_Type.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.comboBox_Type.FormattingEnabled = true;
			this.comboBox_Type.Items.AddRange(new object[] {
			"C_8940",
			"C_8960m",
			"DMC3800",
			"DMC3600",
			"DMC3400",
			"DMC1000B",
			"GTS400",
			"IO_3224"});
			this.comboBox_Type.Location = new System.Drawing.Point(262, 1);
			this.comboBox_Type.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBox_Type.Name = "comboBox_Type";
			this.comboBox_Type.Size = new System.Drawing.Size(62, 20);
			this.comboBox_Type.TabIndex = 3;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(221, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(41, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "防呆";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// SetIOPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.comboBox_Type);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.comboBox_motiontype);
			this.Controls.Add(this.comboBox_io);
			this.Controls.Add(this.comboBox_status);
			this.Name = "SetIOPanel";
			this.Size = new System.Drawing.Size(335, 23);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comboBox_motiontype;
		private System.Windows.Forms.ComboBox comboBox_io;
		private System.Windows.Forms.ComboBox comboBox_status;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboBox_Type;
		private System.Windows.Forms.Button button1;


	}
}
