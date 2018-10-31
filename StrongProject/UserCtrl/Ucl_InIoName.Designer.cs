namespace StrongProject
{
	partial class Ucl_InIoName
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
			this.label_name = new System.Windows.Forms.Label();
			this.checkBox_en = new System.Windows.Forms.CheckBox();
			this.comboBox_IO = new System.Windows.Forms.ComboBox();
			this.textBox_WaitTime = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// label_name
			// 
			this.label_name.AutoSize = true;
			this.label_name.Location = new System.Drawing.Point(202, 8);
			this.label_name.Name = "label_name";
			this.label_name.Size = new System.Drawing.Size(77, 12);
			this.label_name.TabIndex = 0;
			this.label_name.Text = "请选择输出IO";
			// 
			// checkBox_en
			// 
			this.checkBox_en.AutoSize = true;
			this.checkBox_en.Location = new System.Drawing.Point(3, 7);
			this.checkBox_en.Name = "checkBox_en";
			this.checkBox_en.Size = new System.Drawing.Size(48, 16);
			this.checkBox_en.TabIndex = 1;
			this.checkBox_en.Text = "启用";
			this.checkBox_en.UseVisualStyleBackColor = true;
			this.checkBox_en.CheckedChanged += new System.EventHandler(this.checkBox_en_CheckedChanged);
			// 
			// comboBox_IO
			// 
			this.comboBox_IO.FormattingEnabled = true;
			this.comboBox_IO.Items.AddRange(new object[] {
			"高电平",
			"低电平"});
			this.comboBox_IO.Location = new System.Drawing.Point(117, 5);
			this.comboBox_IO.Name = "comboBox_IO";
			this.comboBox_IO.Size = new System.Drawing.Size(73, 20);
			this.comboBox_IO.TabIndex = 2;
			this.comboBox_IO.Text = "高电平";
			this.comboBox_IO.SelectedIndexChanged += new System.EventHandler(this.comboBox_IO_SelectedIndexChanged);
			// 
			// textBox_WaitTime
			// 
			this.textBox_WaitTime.Location = new System.Drawing.Point(57, 4);
			this.textBox_WaitTime.Name = "textBox_WaitTime";
			this.textBox_WaitTime.Size = new System.Drawing.Size(54, 21);
			this.textBox_WaitTime.TabIndex = 6;
			this.textBox_WaitTime.Text = "0";
			this.textBox_WaitTime.TextChanged += new System.EventHandler(this.textBox_WaitTime_TextChanged);
			// 
			// Ucl_InIoName
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.textBox_WaitTime);
			this.Controls.Add(this.comboBox_IO);
			this.Controls.Add(this.checkBox_en);
			this.Controls.Add(this.label_name);
			this.Name = "Ucl_InIoName";
			this.Size = new System.Drawing.Size(344, 33);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_name;
		private System.Windows.Forms.CheckBox checkBox_en;
		private System.Windows.Forms.ComboBox comboBox_IO;
		private System.Windows.Forms.TextBox textBox_WaitTime;
	}
}
