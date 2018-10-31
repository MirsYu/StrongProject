namespace StrongProject
{
	partial class IOStatusCtrl
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
			this.OutputConfigList = new System.Windows.Forms.DataGridView();
			this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewButtonColumn();
			this.label4 = new System.Windows.Forms.Label();
			this.InputConfigList = new System.Windows.Forms.DataGridView();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewLinkColumn();
			this.label3 = new System.Windows.Forms.Label();
			this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
			((System.ComponentModel.ISupportInitialize)(this.OutputConfigList)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.InputConfigList)).BeginInit();
			this.SuspendLayout();
			// 
			// OutputConfigList
			// 
			this.OutputConfigList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
			this.OutputConfigList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.OutputConfigList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.Column7,
			this.Column8,
			this.Column6});
			this.OutputConfigList.Location = new System.Drawing.Point(314, 17);
			this.OutputConfigList.Name = "OutputConfigList";
			this.OutputConfigList.RowTemplate.Height = 23;
			this.OutputConfigList.Size = new System.Drawing.Size(299, 486);
			this.OutputConfigList.TabIndex = 16;
			// 
			// Column7
			// 
			this.Column7.HeaderText = "工站名称";
			this.Column7.Name = "Column7";
			// 
			// Column8
			// 
			this.Column8.HeaderText = "标签";
			this.Column8.Name = "Column8";
			// 
			// Column6
			// 
			this.Column6.HeaderText = "开关";
			this.Column6.Name = "Column6";
			this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.Column6.Width = 55;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(313, 1);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(88, 17);
			this.label4.TabIndex = 15;
			this.label4.Text = "I/O输出配置表";
			// 
			// InputConfigList
			// 
			this.InputConfigList.AllowUserToOrderColumns = true;
			this.InputConfigList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
			this.InputConfigList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.InputConfigList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.Column2,
			this.Column3,
			this.Column4});
			this.InputConfigList.Location = new System.Drawing.Point(3, 17);
			this.InputConfigList.Name = "InputConfigList";
			this.InputConfigList.RowTemplate.Height = 23;
			this.InputConfigList.Size = new System.Drawing.Size(304, 486);
			this.InputConfigList.TabIndex = 14;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "工站名称";
			this.Column2.Name = "Column2";
			// 
			// Column3
			// 
			this.Column3.HeaderText = "标签";
			this.Column3.Name = "Column3";
			// 
			// Column4
			// 
			this.Column4.HeaderText = "状态";
			this.Column4.Name = "Column4";
			this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.Column4.Width = 60;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(2, -1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 17);
			this.label3.TabIndex = 13;
			this.label3.Text = "I/O输入状态表";
			// 
			// IOStatusCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.OutputConfigList);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.InputConfigList);
			this.Controls.Add(this.label3);
			this.Name = "IOStatusCtrl";
			this.Size = new System.Drawing.Size(616, 506);
			((System.ComponentModel.ISupportInitialize)(this.OutputConfigList)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.InputConfigList)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView OutputConfigList;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
		private System.Windows.Forms.DataGridViewButtonColumn Column6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.DataGridView InputConfigList;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewLinkColumn Column4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
	}
}
