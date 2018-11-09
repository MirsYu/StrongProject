namespace StrongProject
{
	partial class UserControl_LogOut
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.tabControl_msg = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.dataGridView_Alarm = new System.Windows.Forms.DataGridView();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.dataGridView_Step = new System.Windows.Forms.DataGridView();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.textBox_Log = new System.Windows.Forms.TextBox();
			this.s = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.时间 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabControl_msg.SuspendLayout();
			this.tabPage1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Alarm)).BeginInit();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Step)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl_msg
			// 
			this.tabControl_msg.Controls.Add(this.tabPage1);
			this.tabControl_msg.Controls.Add(this.tabPage4);
			this.tabControl_msg.Controls.Add(this.tabPage2);
			this.tabControl_msg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl_msg.Location = new System.Drawing.Point(0, 0);
			this.tabControl_msg.Margin = new System.Windows.Forms.Padding(0);
			this.tabControl_msg.Name = "tabControl_msg";
			this.tabControl_msg.SelectedIndex = 0;
			this.tabControl_msg.Size = new System.Drawing.Size(540, 274);
			this.tabControl_msg.TabIndex = 6;
			// 
			// tabPage1
			// 
			this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			this.tabPage1.Controls.Add(this.dataGridView_Alarm);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(532, 248);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "报警信息";
			// 
			// dataGridView_Alarm
			// 
			this.dataGridView_Alarm.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			this.dataGridView_Alarm.BorderStyle = System.Windows.Forms.BorderStyle.None;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.dataGridView_Alarm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.dataGridView_Alarm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_Alarm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.s,
            this.Column1});
			this.dataGridView_Alarm.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView_Alarm.Location = new System.Drawing.Point(3, 3);
			this.dataGridView_Alarm.Name = "dataGridView_Alarm";
			this.dataGridView_Alarm.RowHeadersVisible = false;
			this.dataGridView_Alarm.RowTemplate.Height = 23;
			this.dataGridView_Alarm.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridView_Alarm.Size = new System.Drawing.Size(526, 242);
			this.dataGridView_Alarm.TabIndex = 3;
			// 
			// tabPage4
			// 
			this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			this.tabPage4.Controls.Add(this.dataGridView_Step);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(532, 248);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "工位步骤";
			// 
			// dataGridView_Step
			// 
			this.dataGridView_Step.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			this.dataGridView_Step.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dataGridView_Step.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView_Step.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.时间,
            this.Column5});
			this.dataGridView_Step.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridView_Step.Location = new System.Drawing.Point(3, 3);
			this.dataGridView_Step.Name = "dataGridView_Step";
			this.dataGridView_Step.RowHeadersVisible = false;
			this.dataGridView_Step.RowTemplate.Height = 23;
			this.dataGridView_Step.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.dataGridView_Step.Size = new System.Drawing.Size(526, 242);
			this.dataGridView_Step.TabIndex = 6;
			// 
			// tabPage2
			// 
			this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			this.tabPage2.Controls.Add(this.textBox_Log);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(532, 248);
			this.tabPage2.TabIndex = 4;
			this.tabPage2.Text = "Log";
			// 
			// textBox_Log
			// 
			this.textBox_Log.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			this.textBox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBox_Log.ForeColor = System.Drawing.Color.Black;
			this.textBox_Log.Location = new System.Drawing.Point(0, 0);
			this.textBox_Log.Multiline = true;
			this.textBox_Log.Name = "textBox_Log";
			this.textBox_Log.Size = new System.Drawing.Size(532, 248);
			this.textBox_Log.TabIndex = 0;
			// 
			// s
			// 
			this.s.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle2.Font = new System.Drawing.Font("Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
			this.s.DefaultCellStyle = dataGridViewCellStyle2;
			this.s.FillWeight = 253.8071F;
			this.s.HeaderText = "时间";
			this.s.Name = "s";
			// 
			// Column1
			// 
			this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle3.Font = new System.Drawing.Font("Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
			this.Column1.DefaultCellStyle = dataGridViewCellStyle3;
			this.Column1.FillWeight = 246.1929F;
			this.Column1.HeaderText = "信息";
			this.Column1.Name = "Column1";
			// 
			// 时间
			// 
			this.时间.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle4.Font = new System.Drawing.Font("Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
			this.时间.DefaultCellStyle = dataGridViewCellStyle4;
			this.时间.FillWeight = 101.5228F;
			this.时间.HeaderText = "时间";
			this.时间.Name = "时间";
			// 
			// Column5
			// 
			this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Console", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
			this.Column5.DefaultCellStyle = dataGridViewCellStyle5;
			this.Column5.FillWeight = 98.47716F;
			this.Column5.HeaderText = "信息";
			this.Column5.Name = "Column5";
			// 
			// UserControl_LogOut
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(248)))), ((int)(((byte)(239)))));
			this.Controls.Add(this.tabControl_msg);
			this.Name = "UserControl_LogOut";
			this.Size = new System.Drawing.Size(540, 274);
			this.Load += new System.EventHandler(this.UserControl_LogOut_Load);
			this.SizeChanged += new System.EventHandler(this.UserControl_LogOut_SizeChanged);
			this.tabControl_msg.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Alarm)).EndInit();
			this.tabPage4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView_Step)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.tabPage2.PerformLayout();
			this.DoubleBuffered = true;this.ResumeLayout(false);

		}




		#endregion

		private System.Windows.Forms.TabControl tabControl_msg;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.DataGridView dataGridView_Alarm;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.DataGridView dataGridView_Step;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox textBox_Log;
		private System.Windows.Forms.DataGridViewTextBoxColumn s;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn 时间;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
	}
}
