namespace StrongProject
{
	partial class IOinputStatus
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IOinputStatus));
			this.label1 = new System.Windows.Forms.Label();
			this.lblInputname = new System.Windows.Forms.Label();
			this.lblinputLight = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(18, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(0, 12);
			this.label1.TabIndex = 0;
			// 
			// lblInputname
			// 
			this.lblInputname.AutoSize = true;
			this.lblInputname.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblInputname.Location = new System.Drawing.Point(36, 3);
			this.lblInputname.Name = "lblInputname";
			this.lblInputname.Size = new System.Drawing.Size(70, 17);
			this.lblInputname.TabIndex = 1;
			this.lblInputname.Text = "Inputname";
			// 
			// lblinputLight
			// 
			this.lblinputLight.Image = ((System.Drawing.Image)(resources.GetObject("lblinputLight.Image")));
			this.lblinputLight.Location = new System.Drawing.Point(12, 2);
			this.lblinputLight.Name = "lblinputLight";
			this.lblinputLight.Size = new System.Drawing.Size(20, 20);
			this.lblinputLight.TabIndex = 2;
			// 
			// IOinputStatus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblinputLight);
			this.Controls.Add(this.lblInputname);
			this.Controls.Add(this.label1);
			this.Name = "IOinputStatus";
			this.Size = new System.Drawing.Size(160, 23);
			this.Load += new System.EventHandler(this.IOinputStatus_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblInputname;
		private System.Windows.Forms.Label lblinputLight;
	}
}
