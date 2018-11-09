namespace StrongProject
{
	partial class IOOutputLable
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
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
			this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(279, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(31, 12);
			this.label4.TabIndex = 3;
			this.label4.Text = "电平";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(229, 7);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "索引位";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(184, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "卡号";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(46, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 12);
			this.label1.TabIndex = 2;
			this.label1.Text = "输出标签";
			// 
			// lineShape1
			// 
			this.lineShape1.Name = "lineShape1";
			this.lineShape1.X1 = -5;
			this.lineShape1.X2 = 312;
			this.lineShape1.Y1 = 21;
			this.lineShape1.Y2 = 21;
			// 
			// shapeContainer1
			// 
			this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
			this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
			this.shapeContainer1.Name = "shapeContainer1";
			this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
			this.shapeContainer1.Size = new System.Drawing.Size(317, 26);
			this.shapeContainer1.TabIndex = 5;
			this.shapeContainer1.TabStop = false;
			// 
			// IOOutputLable
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.shapeContainer1);
			this.Name = "IOOutputLable";
			this.Size = new System.Drawing.Size(317, 26);
			this.DoubleBuffered = true;this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
		private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
	}
}
