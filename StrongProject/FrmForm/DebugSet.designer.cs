namespace StrongProject
{
	partial class DebugFrmSet
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.button9 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.button_RemoveAxis = new System.Windows.Forms.Button();
			this.panel_Axis = new System.Windows.Forms.Panel();
			this.button_AddAxis = new System.Windows.Forms.Button();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.button10 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.button_RemoveInputIo = new System.Windows.Forms.Button();
			this.button_AddInputIo = new System.Windows.Forms.Button();
			this.panel_InputIo = new System.Windows.Forms.Panel();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.button11 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.button_RemoveOutputIo = new System.Windows.Forms.Button();
			this.button_AddOutputIo = new System.Windows.Forms.Button();
			this.panel_OutputIo = new System.Windows.Forms.Panel();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.button12 = new System.Windows.Forms.Button();
			this.comboBox_SetType = new System.Windows.Forms.ComboBox();
			this.button5 = new System.Windows.Forms.Button();
			this.button_out = new System.Windows.Forms.Button();
			this.btnSetSpeed = new System.Windows.Forms.Button();
			this.numSpeed = new System.Windows.Forms.NumericUpDown();
			this.cboAxisName = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button_RemovePoint = new System.Windows.Forms.Button();
			this.button_AddPoint = new System.Windows.Forms.Button();
			this.panel_Point = new System.Windows.Forms.Panel();
			this.button_delAll = new System.Windows.Forms.Button();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.button_Save = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(-4, -1);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(801, 468);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage1.Controls.Add(this.button9);
			this.tabPage1.Controls.Add(this.button8);
			this.tabPage1.Controls.Add(this.button6);
			this.tabPage1.Controls.Add(this.button7);
			this.tabPage1.Controls.Add(this.button_RemoveAxis);
			this.tabPage1.Controls.Add(this.panel_Axis);
			this.tabPage1.Controls.Add(this.button_AddAxis);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(793, 442);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "轴";
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(23, 391);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(75, 32);
			this.button9.TabIndex = 53;
			this.button9.Text = "刷新";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new System.EventHandler(this.button9_Click);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(137, 388);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(82, 38);
			this.button8.TabIndex = 52;
			this.button8.Text = "引用轴";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.button8_Click);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(594, 391);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(89, 32);
			this.button6.TabIndex = 51;
			this.button6.Text = "轴导入";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(485, 391);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(89, 32);
			this.button7.TabIndex = 50;
			this.button7.Text = "轴导出";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// button_RemoveAxis
			// 
			this.button_RemoveAxis.Location = new System.Drawing.Point(383, 388);
			this.button_RemoveAxis.Name = "button_RemoveAxis";
			this.button_RemoveAxis.Size = new System.Drawing.Size(82, 38);
			this.button_RemoveAxis.TabIndex = 4;
			this.button_RemoveAxis.Text = "删除轴";
			this.button_RemoveAxis.UseVisualStyleBackColor = true;
			this.button_RemoveAxis.Click += new System.EventHandler(this.button_RemoveAxis_Click);
			// 
			// panel_Axis
			// 
			this.panel_Axis.AutoScroll = true;
			this.panel_Axis.BackColor = System.Drawing.SystemColors.Control;
			this.panel_Axis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_Axis.Location = new System.Drawing.Point(8, 10);
			this.panel_Axis.Name = "panel_Axis";
			this.panel_Axis.Size = new System.Drawing.Size(776, 368);
			this.panel_Axis.TabIndex = 2;
			// 
			// button_AddAxis
			// 
			this.button_AddAxis.Location = new System.Drawing.Point(251, 388);
			this.button_AddAxis.Name = "button_AddAxis";
			this.button_AddAxis.Size = new System.Drawing.Size(82, 38);
			this.button_AddAxis.TabIndex = 3;
			this.button_AddAxis.Text = "添加轴";
			this.button_AddAxis.UseVisualStyleBackColor = true;
			this.button_AddAxis.Click += new System.EventHandler(this.button_AddAxis_Click);
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage2.Controls.Add(this.button10);
			this.tabPage2.Controls.Add(this.button2);
			this.tabPage2.Controls.Add(this.button1);
			this.tabPage2.Controls.Add(this.button_RemoveInputIo);
			this.tabPage2.Controls.Add(this.button_AddInputIo);
			this.tabPage2.Controls.Add(this.panel_InputIo);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(793, 442);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "输入IO";
			// 
			// button10
			// 
			this.button10.Location = new System.Drawing.Point(458, 385);
			this.button10.Name = "button10";
			this.button10.Size = new System.Drawing.Size(148, 32);
			this.button10.TabIndex = 50;
			this.button10.Text = "IO导入(卡号）";
			this.button10.UseVisualStyleBackColor = true;
			this.button10.Click += new System.EventHandler(this.button10_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(304, 384);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(148, 32);
			this.button2.TabIndex = 49;
			this.button2.Text = "IO导入(不导入卡号）";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(209, 385);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(89, 32);
			this.button1.TabIndex = 48;
			this.button1.Text = "IO导出";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button_RemoveInputIo
			// 
			this.button_RemoveInputIo.Location = new System.Drawing.Point(110, 382);
			this.button_RemoveInputIo.Name = "button_RemoveInputIo";
			this.button_RemoveInputIo.Size = new System.Drawing.Size(82, 38);
			this.button_RemoveInputIo.TabIndex = 6;
			this.button_RemoveInputIo.Text = "删除";
			this.button_RemoveInputIo.UseVisualStyleBackColor = true;
			this.button_RemoveInputIo.Click += new System.EventHandler(this.button_RemoveInputIo_Click);
			// 
			// button_AddInputIo
			// 
			this.button_AddInputIo.Location = new System.Drawing.Point(12, 382);
			this.button_AddInputIo.Name = "button_AddInputIo";
			this.button_AddInputIo.Size = new System.Drawing.Size(82, 38);
			this.button_AddInputIo.TabIndex = 5;
			this.button_AddInputIo.Text = "添加";
			this.button_AddInputIo.UseVisualStyleBackColor = true;
			this.button_AddInputIo.Click += new System.EventHandler(this.button_AddInputIo_Click);
			// 
			// panel_InputIo
			// 
			this.panel_InputIo.AutoScroll = true;
			this.panel_InputIo.BackColor = System.Drawing.SystemColors.Control;
			this.panel_InputIo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_InputIo.Location = new System.Drawing.Point(8, 10);
			this.panel_InputIo.Name = "panel_InputIo";
			this.panel_InputIo.Size = new System.Drawing.Size(779, 368);
			this.panel_InputIo.TabIndex = 1;
			// 
			// tabPage3
			// 
			this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage3.Controls.Add(this.button11);
			this.tabPage3.Controls.Add(this.button3);
			this.tabPage3.Controls.Add(this.button4);
			this.tabPage3.Controls.Add(this.button_RemoveOutputIo);
			this.tabPage3.Controls.Add(this.button_AddOutputIo);
			this.tabPage3.Controls.Add(this.panel_OutputIo);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(793, 442);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "输出IO";
			// 
			// button11
			// 
			this.button11.Location = new System.Drawing.Point(433, 391);
			this.button11.Name = "button11";
			this.button11.Size = new System.Drawing.Size(208, 32);
			this.button11.TabIndex = 52;
			this.button11.Text = "IO导入(卡号）";
			this.button11.UseVisualStyleBackColor = true;
			this.button11.Click += new System.EventHandler(this.button11_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(320, 391);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(94, 35);
			this.button3.TabIndex = 51;
			this.button3.Text = "IO导入";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(215, 391);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(89, 32);
			this.button4.TabIndex = 50;
			this.button4.Text = "IO导出";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button_RemoveOutputIo
			// 
			this.button_RemoveOutputIo.Location = new System.Drawing.Point(110, 388);
			this.button_RemoveOutputIo.Name = "button_RemoveOutputIo";
			this.button_RemoveOutputIo.Size = new System.Drawing.Size(82, 38);
			this.button_RemoveOutputIo.TabIndex = 8;
			this.button_RemoveOutputIo.Text = "删除";
			this.button_RemoveOutputIo.UseVisualStyleBackColor = true;
			this.button_RemoveOutputIo.Click += new System.EventHandler(this.button_RemoveOutputIo_Click);
			// 
			// button_AddOutputIo
			// 
			this.button_AddOutputIo.Location = new System.Drawing.Point(12, 388);
			this.button_AddOutputIo.Name = "button_AddOutputIo";
			this.button_AddOutputIo.Size = new System.Drawing.Size(82, 38);
			this.button_AddOutputIo.TabIndex = 7;
			this.button_AddOutputIo.Text = "添加";
			this.button_AddOutputIo.UseVisualStyleBackColor = true;
			this.button_AddOutputIo.Click += new System.EventHandler(this.button_AddOutputIo_Click);
			// 
			// panel_OutputIo
			// 
			this.panel_OutputIo.AutoScroll = true;
			this.panel_OutputIo.BackColor = System.Drawing.SystemColors.Control;
			this.panel_OutputIo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_OutputIo.Location = new System.Drawing.Point(8, 10);
			this.panel_OutputIo.Name = "panel_OutputIo";
			this.panel_OutputIo.Size = new System.Drawing.Size(776, 368);
			this.panel_OutputIo.TabIndex = 0;
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
			this.tabPage4.Controls.Add(this.button12);
			this.tabPage4.Controls.Add(this.comboBox_SetType);
			this.tabPage4.Controls.Add(this.button5);
			this.tabPage4.Controls.Add(this.button_out);
			this.tabPage4.Controls.Add(this.btnSetSpeed);
			this.tabPage4.Controls.Add(this.numSpeed);
			this.tabPage4.Controls.Add(this.cboAxisName);
			this.tabPage4.Controls.Add(this.label1);
			this.tabPage4.Controls.Add(this.button_RemovePoint);
			this.tabPage4.Controls.Add(this.button_AddPoint);
			this.tabPage4.Controls.Add(this.panel_Point);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(793, 442);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "点位";
			// 
			// button12
			// 
			this.button12.Location = new System.Drawing.Point(336, 414);
			this.button12.Name = "button12";
			this.button12.Size = new System.Drawing.Size(82, 26);
			this.button12.TabIndex = 50;
			this.button12.Text = "一键按比例设置";
			this.button12.UseVisualStyleBackColor = true;
			this.button12.Click += new System.EventHandler(this.button12_Click);
			// 
			// comboBox_SetType
			// 
			this.comboBox_SetType.FormattingEnabled = true;
			this.comboBox_SetType.Items.AddRange(new object[] {
			"启始速度",
			"加速度",
			"加速时间",
			"运行速度",
			"减速度",
			"减速时间"});
			this.comboBox_SetType.Location = new System.Drawing.Point(154, 398);
			this.comboBox_SetType.Name = "comboBox_SetType";
			this.comboBox_SetType.Size = new System.Drawing.Size(90, 20);
			this.comboBox_SetType.TabIndex = 49;
			// 
			// button5
			// 
			this.button5.Location = new System.Drawing.Point(695, 392);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(89, 32);
			this.button5.TabIndex = 48;
			this.button5.Text = "工位步骤导入";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button_out
			// 
			this.button_out.Location = new System.Drawing.Point(600, 392);
			this.button_out.Name = "button_out";
			this.button_out.Size = new System.Drawing.Size(89, 35);
			this.button_out.TabIndex = 47;
			this.button_out.Text = "工位步骤导出";
			this.button_out.UseVisualStyleBackColor = true;
			this.button_out.Click += new System.EventHandler(this.button_out_Click);
			// 
			// btnSetSpeed
			// 
			this.btnSetSpeed.Location = new System.Drawing.Point(336, 386);
			this.btnSetSpeed.Name = "btnSetSpeed";
			this.btnSetSpeed.Size = new System.Drawing.Size(82, 26);
			this.btnSetSpeed.TabIndex = 15;
			this.btnSetSpeed.Text = "一键设置";
			this.btnSetSpeed.UseVisualStyleBackColor = true;
			this.btnSetSpeed.Click += new System.EventHandler(this.btnSetSpeed_Click);
			// 
			// numSpeed
			// 
			this.numSpeed.DecimalPlaces = 3;
			this.numSpeed.Location = new System.Drawing.Point(250, 398);
			this.numSpeed.Maximum = new decimal(new int[] {
			1000000,
			0,
			0,
			0});
			this.numSpeed.Minimum = new decimal(new int[] {
			1000000,
			0,
			0,
			-2147483648});
			this.numSpeed.Name = "numSpeed";
			this.numSpeed.Size = new System.Drawing.Size(80, 21);
			this.numSpeed.TabIndex = 14;
			this.numSpeed.Value = new decimal(new int[] {
			10,
			0,
			0,
			0});
			// 
			// cboAxisName
			// 
			this.cboAxisName.FormattingEnabled = true;
			this.cboAxisName.Location = new System.Drawing.Point(58, 396);
			this.cboAxisName.Name = "cboAxisName";
			this.cboAxisName.Size = new System.Drawing.Size(80, 20);
			this.cboAxisName.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(11, 400);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 11;
			this.label1.Text = "轴名称";
			// 
			// button_RemovePoint
			// 
			this.button_RemovePoint.Location = new System.Drawing.Point(512, 390);
			this.button_RemovePoint.Name = "button_RemovePoint";
			this.button_RemovePoint.Size = new System.Drawing.Size(82, 38);
			this.button_RemovePoint.TabIndex = 10;
			this.button_RemovePoint.Text = "刷新";
			this.button_RemovePoint.UseVisualStyleBackColor = true;
			this.button_RemovePoint.Click += new System.EventHandler(this.button_RemovePoint_Click);
			// 
			// button_AddPoint
			// 
			this.button_AddPoint.Location = new System.Drawing.Point(424, 392);
			this.button_AddPoint.Name = "button_AddPoint";
			this.button_AddPoint.Size = new System.Drawing.Size(82, 38);
			this.button_AddPoint.TabIndex = 9;
			this.button_AddPoint.Text = "添加点";
			this.button_AddPoint.UseVisualStyleBackColor = true;
			this.button_AddPoint.Click += new System.EventHandler(this.button_AddPoint_Click);
			// 
			// panel_Point
			// 
			this.panel_Point.AutoScroll = true;
			this.panel_Point.BackColor = System.Drawing.SystemColors.Control;
			this.panel_Point.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel_Point.Location = new System.Drawing.Point(8, 10);
			this.panel_Point.Name = "panel_Point";
			this.panel_Point.Size = new System.Drawing.Size(776, 368);
			this.panel_Point.TabIndex = 2;
			// 
			// button_delAll
			// 
			this.button_delAll.Location = new System.Drawing.Point(814, 83);
			this.button_delAll.Name = "button_delAll";
			this.button_delAll.Size = new System.Drawing.Size(94, 38);
			this.button_delAll.TabIndex = 49;
			this.button_delAll.Text = "删除所有点位";
			this.button_delAll.UseVisualStyleBackColor = true;
			this.button_delAll.Click += new System.EventHandler(this.button_delAll_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// button_Save
			// 
			this.button_Save.Location = new System.Drawing.Point(814, 20);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(95, 38);
			this.button_Save.TabIndex = 1;
			this.button_Save.Text = "保存工位配置";
			this.button_Save.UseVisualStyleBackColor = true;
			this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
			// 
			// DebugFrmSet
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(951, 481);
			this.Controls.Add(this.button_delAll);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.button_Save);
			this.Name = "DebugFrmSet";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "DebugSet";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DebugFrmSet_FormClosing);
			this.Load += new System.EventHandler(this.DebugSet_Load);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
			this.DoubleBuffered = true;this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.Panel panel_Point;
		private System.Windows.Forms.Panel panel_Axis;
		private System.Windows.Forms.Panel panel_InputIo;
		private System.Windows.Forms.Panel panel_OutputIo;
		private System.Windows.Forms.Button button_RemoveAxis;
		private System.Windows.Forms.Button button_AddAxis;
		private System.Windows.Forms.Button button_RemoveInputIo;
		private System.Windows.Forms.Button button_AddInputIo;
		private System.Windows.Forms.Button button_RemoveOutputIo;
		private System.Windows.Forms.Button button_AddOutputIo;
		private System.Windows.Forms.Button button_AddPoint;
		private System.Windows.Forms.Button btnSetSpeed;
		private System.Windows.Forms.NumericUpDown numSpeed;
		private System.Windows.Forms.ComboBox cboAxisName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.Button button_out;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.Button button_delAll;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.Button button_Save;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.Button button11;
		private System.Windows.Forms.ComboBox comboBox_SetType;
		private System.Windows.Forms.Button button12;
		private System.Windows.Forms.Button button_RemovePoint;
	}
}