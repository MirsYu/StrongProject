namespace StrongProject
{
	partial class QueryPointInfo
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
			this.lblAxisName = new System.Windows.Forms.Label();
			this.chkEnable = new System.Windows.Forms.CheckBox();
			this.chkSpecial = new System.Windows.Forms.CheckBox();
			this.numValue = new System.Windows.Forms.NumericUpDown();
			this.numSpeed = new System.Windows.Forms.NumericUpDown();
			this.numAcc = new System.Windows.Forms.NumericUpDown();
			this.numDec = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_StartSpeed = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_DccTime = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_ACCTime = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_StopSpeed = new System.Windows.Forms.NumericUpDown();
			this.numericUpDown_S_StopTime = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numValue)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numSpeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numAcc)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numDec)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StartSpeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_DccTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ACCTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StopSpeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_S_StopTime)).BeginInit();
			this.SuspendLayout();
			// 
			// lblAxisName
			// 
			this.lblAxisName.AutoSize = true;
			this.lblAxisName.Location = new System.Drawing.Point(3, 5);
			this.lblAxisName.Name = "lblAxisName";
			this.lblAxisName.Size = new System.Drawing.Size(29, 12);
			this.lblAxisName.TabIndex = 0;
			this.lblAxisName.Text = "轴名";
			// 
			// chkEnable
			// 
			this.chkEnable.AutoSize = true;
			this.chkEnable.Location = new System.Drawing.Point(92, 4);
			this.chkEnable.Name = "chkEnable";
			this.chkEnable.Size = new System.Drawing.Size(15, 14);
			this.chkEnable.TabIndex = 1;
			this.chkEnable.UseVisualStyleBackColor = true;
			this.chkEnable.CheckedChanged += new System.EventHandler(this.chkEnable_CheckedChanged);
			// 
			// chkSpecial
			// 
			this.chkSpecial.AutoSize = true;
			this.chkSpecial.Location = new System.Drawing.Point(148, 4);
			this.chkSpecial.Name = "chkSpecial";
			this.chkSpecial.Size = new System.Drawing.Size(15, 14);
			this.chkSpecial.TabIndex = 2;
			this.chkSpecial.UseVisualStyleBackColor = true;
			// 
			// numValue
			// 
			this.numValue.DecimalPlaces = 3;
			this.numValue.Location = new System.Drawing.Point(200, 0);
			this.numValue.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.numValue.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numValue.Name = "numValue";
			this.numValue.Size = new System.Drawing.Size(58, 21);
			this.numValue.TabIndex = 3;
			// 
			// numSpeed
			// 
			this.numSpeed.DecimalPlaces = 3;
			this.numSpeed.Location = new System.Drawing.Point(264, 0);
			this.numSpeed.Maximum = new decimal(new int[] {
			20000,
			0,
			0,
			0});
			this.numSpeed.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numSpeed.Name = "numSpeed";
			this.numSpeed.Size = new System.Drawing.Size(58, 21);
			this.numSpeed.TabIndex = 4;
			// 
			// numAcc
			// 
			this.numAcc.DecimalPlaces = 3;
			this.numAcc.Location = new System.Drawing.Point(328, 0);
			this.numAcc.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.numAcc.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numAcc.Name = "numAcc";
			this.numAcc.Size = new System.Drawing.Size(58, 21);
			this.numAcc.TabIndex = 5;
			// 
			// numDec
			// 
			this.numDec.DecimalPlaces = 3;
			this.numDec.Location = new System.Drawing.Point(392, 0);
			this.numDec.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.numDec.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numDec.Name = "numDec";
			this.numDec.Size = new System.Drawing.Size(58, 21);
			this.numDec.TabIndex = 6;
			// 
			// numericUpDown_StartSpeed
			// 
			this.numericUpDown_StartSpeed.DecimalPlaces = 3;
			this.numericUpDown_StartSpeed.Location = new System.Drawing.Point(579, -1);
			this.numericUpDown_StartSpeed.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.numericUpDown_StartSpeed.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numericUpDown_StartSpeed.Name = "numericUpDown_StartSpeed";
			this.numericUpDown_StartSpeed.Size = new System.Drawing.Size(58, 21);
			this.numericUpDown_StartSpeed.TabIndex = 9;
			// 
			// numericUpDown_DccTime
			// 
			this.numericUpDown_DccTime.DecimalPlaces = 3;
			this.numericUpDown_DccTime.Location = new System.Drawing.Point(515, -1);
			this.numericUpDown_DccTime.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.numericUpDown_DccTime.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numericUpDown_DccTime.Name = "numericUpDown_DccTime";
			this.numericUpDown_DccTime.Size = new System.Drawing.Size(58, 21);
			this.numericUpDown_DccTime.TabIndex = 8;
			// 
			// numericUpDown_ACCTime
			// 
			this.numericUpDown_ACCTime.DecimalPlaces = 3;
			this.numericUpDown_ACCTime.Location = new System.Drawing.Point(451, -1);
			this.numericUpDown_ACCTime.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.numericUpDown_ACCTime.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numericUpDown_ACCTime.Name = "numericUpDown_ACCTime";
			this.numericUpDown_ACCTime.Size = new System.Drawing.Size(58, 21);
			this.numericUpDown_ACCTime.TabIndex = 7;
			// 
			// numericUpDown_StopSpeed
			// 
			this.numericUpDown_StopSpeed.DecimalPlaces = 3;
			this.numericUpDown_StopSpeed.Location = new System.Drawing.Point(642, 0);
			this.numericUpDown_StopSpeed.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.numericUpDown_StopSpeed.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numericUpDown_StopSpeed.Name = "numericUpDown_StopSpeed";
			this.numericUpDown_StopSpeed.Size = new System.Drawing.Size(58, 21);
			this.numericUpDown_StopSpeed.TabIndex = 10;
			// 
			// numericUpDown_S_StopTime
			// 
			this.numericUpDown_S_StopTime.DecimalPlaces = 3;
			this.numericUpDown_S_StopTime.Location = new System.Drawing.Point(706, 0);
			this.numericUpDown_S_StopTime.Maximum = new decimal(new int[] {
			2000,
			0,
			0,
			0});
			this.numericUpDown_S_StopTime.Minimum = new decimal(new int[] {
			200,
			0,
			0,
			-2147483648});
			this.numericUpDown_S_StopTime.Name = "numericUpDown_S_StopTime";
			this.numericUpDown_S_StopTime.Size = new System.Drawing.Size(58, 21);
			this.numericUpDown_S_StopTime.TabIndex = 11;
			this.numericUpDown_S_StopTime.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
			// 
			// QueryPointInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.numericUpDown_S_StopTime);
			this.Controls.Add(this.numericUpDown_StopSpeed);
			this.Controls.Add(this.numericUpDown_StartSpeed);
			this.Controls.Add(this.numericUpDown_DccTime);
			this.Controls.Add(this.numericUpDown_ACCTime);
			this.Controls.Add(this.numDec);
			this.Controls.Add(this.numAcc);
			this.Controls.Add(this.numSpeed);
			this.Controls.Add(this.numValue);
			this.Controls.Add(this.chkSpecial);
			this.Controls.Add(this.chkEnable);
			this.Controls.Add(this.lblAxisName);
			this.Name = "QueryPointInfo";
			this.Size = new System.Drawing.Size(779, 21);
			((System.ComponentModel.ISupportInitialize)(this.numValue)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numSpeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numAcc)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numDec)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StartSpeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_DccTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_ACCTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_StopSpeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown_S_StopTime)).EndInit();
			this.DoubleBuffered = true;this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label lblAxisName;
		private System.Windows.Forms.CheckBox chkEnable;
		private System.Windows.Forms.CheckBox chkSpecial;
		private System.Windows.Forms.NumericUpDown numValue;
		private System.Windows.Forms.NumericUpDown numSpeed;
		private System.Windows.Forms.NumericUpDown numAcc;
		private System.Windows.Forms.NumericUpDown numDec;
		private System.Windows.Forms.NumericUpDown numericUpDown_StartSpeed;
		private System.Windows.Forms.NumericUpDown numericUpDown_DccTime;
		private System.Windows.Forms.NumericUpDown numericUpDown_ACCTime;
		private System.Windows.Forms.NumericUpDown numericUpDown_StopSpeed;
		private System.Windows.Forms.NumericUpDown numericUpDown_S_StopTime;
	}
}
