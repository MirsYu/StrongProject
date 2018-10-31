namespace StrongProject
{
	partial class SetP_Distance
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
			this.PointDistance = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.PointDistance)).BeginInit();
			this.SuspendLayout();
			// 
			// PointDistance
			// 
			this.PointDistance.DecimalPlaces = 3;
			this.PointDistance.Location = new System.Drawing.Point(3, 1);
			this.PointDistance.Maximum = new decimal(new int[] {
			360,
			0,
			0,
			0});
			this.PointDistance.Name = "PointDistance";
			this.PointDistance.Size = new System.Drawing.Size(122, 21);
			this.PointDistance.TabIndex = 27;
			this.PointDistance.Value = new decimal(new int[] {
			1,
			0,
			0,
			0});
			this.PointDistance.ValueChanged += new System.EventHandler(this.PointDistance_ValueChanged);
			// 
			// SetP_Distance
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PointDistance);
			this.Name = "SetP_Distance";
			this.Size = new System.Drawing.Size(129, 23);
			this.Load += new System.EventHandler(this.SetP_Distance_Load);
			((System.ComponentModel.ISupportInitialize)(this.PointDistance)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NumericUpDown PointDistance;
	}
}
