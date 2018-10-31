namespace StrongProject
{
	partial class Frm_Alarm
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
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.dateTimePicker_end = new System.Windows.Forms.DateTimePicker();
			this.dateTimePicker_begin = new System.Windows.Forms.DateTimePicker();
			this.button1 = new System.Windows.Forms.Button();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(207, 14);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 34;
			this.label3.Text = "结束时间";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 14);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 33;
			this.label2.Text = "开始时间";
			// 
			// dateTimePicker_end
			// 
			this.dateTimePicker_end.CustomFormat = "yyyy-MM-dd HH:mm";
			this.dateTimePicker_end.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker_end.Location = new System.Drawing.Point(266, 8);
			this.dateTimePicker_end.Name = "dateTimePicker_end";
			this.dateTimePicker_end.Size = new System.Drawing.Size(141, 21);
			this.dateTimePicker_end.TabIndex = 30;
			this.dateTimePicker_end.Value = new System.DateTime(2016, 5, 30, 0, 0, 0, 0);
			// 
			// dateTimePicker_begin
			// 
			this.dateTimePicker_begin.CustomFormat = "yyyy-MM-dd HH:mm";
			this.dateTimePicker_begin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dateTimePicker_begin.Location = new System.Drawing.Point(71, 8);
			this.dateTimePicker_begin.Name = "dateTimePicker_begin";
			this.dateTimePicker_begin.Size = new System.Drawing.Size(130, 21);
			this.dateTimePicker_begin.TabIndex = 29;
			this.dateTimePicker_begin.Value = new System.DateTime(2016, 5, 30, 0, 0, 0, 0);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(455, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(63, 23);
			this.button1.TabIndex = 37;
			this.button1.Text = "查询";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.Column2,
			this.Column3,
			this.Column4});
			this.dataGridView1.Location = new System.Drawing.Point(8, 38);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowTemplate.Height = 23;
			this.dataGridView1.Size = new System.Drawing.Size(1058, 510);
			this.dataGridView1.TabIndex = 38;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Column2";
			this.Column2.Name = "Column2";
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Column3";
			this.Column3.Name = "Column3";
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Column4";
			this.Column4.Name = "Column4";
			this.Column4.Width = 970;
			// 
			// Frm_Alarm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1070, 560);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dateTimePicker_end);
			this.Controls.Add(this.dateTimePicker_begin);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Frm_Alarm";
			this.Text = "Frm_Alarm";
			this.Load += new System.EventHandler(this.Frm_Alarm_Load);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.DateTimePicker dateTimePicker_end;
		private System.Windows.Forms.DateTimePicker dateTimePicker_begin;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
	}
}