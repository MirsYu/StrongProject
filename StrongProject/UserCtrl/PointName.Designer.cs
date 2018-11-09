namespace StrongProject
{
	partial class PointName
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
			this.lblname = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblname
			// 
			this.lblname.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblname.ForeColor = System.Drawing.Color.Navy;
			this.lblname.Location = new System.Drawing.Point(4, 0);
			this.lblname.Name = "lblname";
			this.lblname.Size = new System.Drawing.Size(40, 17);
			this.lblname.TabIndex = 0;
			this.lblname.Text = "lblaxisname";
			this.lblname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PointName
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblname);
			this.Name = "PointName";
			this.Size = new System.Drawing.Size(40, 17);
			this.Load += new System.EventHandler(this.PointName_Load);
			this.DoubleBuffered = true;this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lblname;
	}
}
