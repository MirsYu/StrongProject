namespace StrongProject
{
	partial class LineCtrl
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
			this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
			this.SuspendLayout();
			// 
			// lineShape1
			// 
			this.lineShape1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
			this.lineShape1.Name = "lineShape1";
			this.lineShape1.X1 = 2;
			this.lineShape1.X2 = 1100;
			this.lineShape1.Y1 = 6;
			this.lineShape1.Y2 = 6;
			// 
			// shapeContainer1
			// 
			this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
			this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.shapeContainer1.Name = "shapeContainer1";
			this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
			this.lineShape1});
			this.shapeContainer1.Size = new System.Drawing.Size(1102, 12);
			this.shapeContainer1.TabIndex = 0;
			this.shapeContainer1.TabStop = false;
			// 
			// LineCtrl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.shapeContainer1);
			this.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.Name = "LineCtrl";
			this.Size = new System.Drawing.Size(1102, 12);
			this.DoubleBuffered = true;this.ResumeLayout(false);

		}

		#endregion

		private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
		private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
	}
}
