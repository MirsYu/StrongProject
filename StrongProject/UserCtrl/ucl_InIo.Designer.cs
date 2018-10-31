namespace StrongProject
{
	partial class ucl_InIo
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
			this.tbxTime = new System.Windows.Forms.TextBox();
			this.comboBox_poe = new System.Windows.Forms.ComboBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label_name
			// 
			this.label_name.AutoSize = true;
			this.label_name.Location = new System.Drawing.Point(153, 9);
			this.label_name.Name = "label_name";
			this.label_name.Size = new System.Drawing.Size(77, 12);
			this.label_name.TabIndex = 0;
			this.label_name.Text = "请选择控制IO";
			// 
			// tbxTime
			// 
			this.tbxTime.Location = new System.Drawing.Point(25, 3);
			this.tbxTime.Name = "tbxTime";
			this.tbxTime.Size = new System.Drawing.Size(54, 21);
			this.tbxTime.TabIndex = 2;
			this.tbxTime.Text = "1000";
			this.tbxTime.TextChanged += new System.EventHandler(this.tbxTime_TextChanged);
			// 
			// comboBox_poe
			// 
			this.comboBox_poe.FormattingEnabled = true;
			this.comboBox_poe.Items.AddRange(new object[] {
			"高电平",
			"低电平"});
			this.comboBox_poe.Location = new System.Drawing.Point(85, 4);
			this.comboBox_poe.Name = "comboBox_poe";
			this.comboBox_poe.Size = new System.Drawing.Size(62, 20);
			this.comboBox_poe.TabIndex = 3;
			this.comboBox_poe.Text = "高电平";
			this.comboBox_poe.SelectedIndexChanged += new System.EventHandler(this.comboBox_poe_SelectedIndexChanged);
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(4, 7);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(15, 14);
			this.checkBox1.TabIndex = 4;
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
			// 
			// ucl_InIo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.comboBox_poe);
			this.Controls.Add(this.tbxTime);
			this.Controls.Add(this.label_name);
			this.Name = "ucl_InIo";
			this.Size = new System.Drawing.Size(271, 33);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label_name;
		private System.Windows.Forms.TextBox tbxTime;
		private System.Windows.Forms.ComboBox comboBox_poe;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}
