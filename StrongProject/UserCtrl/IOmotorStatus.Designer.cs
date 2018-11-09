namespace StrongProject
{
	partial class IOmotorStatus
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IOmotorStatus));
			this.lblmotorname = new System.Windows.Forms.Label();
			this.OutputBT = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblmotorname
			// 
			this.lblmotorname.AutoSize = true;
			this.lblmotorname.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblmotorname.Location = new System.Drawing.Point(90, 6);
			this.lblmotorname.Name = "lblmotorname";
			this.lblmotorname.Size = new System.Drawing.Size(59, 17);
			this.lblmotorname.TabIndex = 3;
			this.lblmotorname.Text = "outname";
			// 
			// OutputBT
			// 
			this.OutputBT.Image = ((System.Drawing.Image)(resources.GetObject("OutputBT.Image")));
			this.OutputBT.Location = new System.Drawing.Point(13, 0);
			this.OutputBT.Name = "OutputBT";
			this.OutputBT.Size = new System.Drawing.Size(65, 30);
			this.OutputBT.TabIndex = 2;
			this.OutputBT.UseVisualStyleBackColor = true;
			// 
			// IOmotorStatus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblmotorname);
			this.Controls.Add(this.OutputBT);
			this.Name = "IOmotorStatus";
			this.Size = new System.Drawing.Size(182, 30);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblmotorname;
		private System.Windows.Forms.Button OutputBT;
	}
}
