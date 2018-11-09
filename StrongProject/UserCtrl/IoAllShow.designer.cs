namespace StrongProject
{
	partial class IoAllShow
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
			this.panel_IO = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.panel_out = new System.Windows.Forms.Panel();
			this.button2 = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.comboBox_motiontype = new System.Windows.Forms.ComboBox();
			this.button_save = new System.Windows.Forms.Button();
			this.comboBox_Stat = new System.Windows.Forms.ComboBox();
			this.button_IOSet = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel_IO
			// 
			this.panel_IO.AutoScroll = true;
			this.panel_IO.Location = new System.Drawing.Point(6, 6);
			this.panel_IO.Name = "panel_IO";
			this.panel_IO.Size = new System.Drawing.Size(653, 450);
			this.panel_IO.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(137, 2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "IN查询";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(29, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "卡号";
			// 
			// panel_out
			// 
			this.panel_out.AutoScroll = true;
			this.panel_out.Location = new System.Drawing.Point(665, 6);
			this.panel_out.Name = "panel_out";
			this.panel_out.Size = new System.Drawing.Size(280, 450);
			this.panel_out.TabIndex = 5;
			this.panel_out.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_out_Paint);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(218, 2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 6;
			this.button2.Text = "Out查询";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(3, 29);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(977, 500);
			this.tabControl1.TabIndex = 7;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.panel_IO);
			this.tabPage1.Controls.Add(this.panel_out);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(951, 474);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "IO控制";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(969, 474);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "IO设置";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// tabPage3
			// 
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(1172, 474);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "输入io分配";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// tabPage4
			// 
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(1172, 474);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "输出IO分配";
			this.tabPage4.UseVisualStyleBackColor = true;
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
			"扩1",
			"扩2",
			"扩3",
			"扩4",
			"扩5",
			"扩6",
			"扩7",
			"扩8",
			"扩9",
			"扩10"});
			this.comboBox_motiontype.Location = new System.Drawing.Point(85, 2);
			this.comboBox_motiontype.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBox_motiontype.Name = "comboBox_motiontype";
			this.comboBox_motiontype.Size = new System.Drawing.Size(46, 20);
			this.comboBox_motiontype.TabIndex = 8;
			// 
			// button_save
			// 
			this.button_save.Location = new System.Drawing.Point(299, 1);
			this.button_save.Name = "button_save";
			this.button_save.Size = new System.Drawing.Size(75, 23);
			this.button_save.TabIndex = 9;
			this.button_save.Text = "保存";
			this.button_save.UseVisualStyleBackColor = true;
			this.button_save.Click += new System.EventHandler(this.button_save_Click);
			// 
			// comboBox_Stat
			// 
			this.comboBox_Stat.FormattingEnabled = true;
			this.comboBox_Stat.Location = new System.Drawing.Point(394, 2);
			this.comboBox_Stat.Name = "comboBox_Stat";
			this.comboBox_Stat.Size = new System.Drawing.Size(121, 20);
			this.comboBox_Stat.TabIndex = 18;
			// 
			// button_IOSet
			// 
			this.button_IOSet.Location = new System.Drawing.Point(552, 1);
			this.button_IOSet.Name = "button_IOSet";
			this.button_IOSet.Size = new System.Drawing.Size(75, 23);
			this.button_IOSet.TabIndex = 19;
			this.button_IOSet.Text = "IOSet";
			this.button_IOSet.UseVisualStyleBackColor = true;
			this.button_IOSet.Click += new System.EventHandler(this.button_IOSet_Click);
			// 
			// IoAllShow
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button_IOSet);
			this.Controls.Add(this.comboBox_Stat);
			this.Controls.Add(this.button_save);
			this.Controls.Add(this.comboBox_motiontype);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button1);
			this.Name = "IoAllShow";
			this.Size = new System.Drawing.Size(980, 550);
			this.Load += new System.EventHandler(this.IoAllShow_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.DoubleBuffered = true;this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel_IO;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel_out;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;

		private System.Windows.Forms.ComboBox comboBox_motiontype;
		private System.Windows.Forms.Button button_save;
		private System.Windows.Forms.ComboBox comboBox_Stat;
		private System.Windows.Forms.Button button_IOSet;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;

	}
}
