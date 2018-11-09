namespace StrongProject
{
	partial class IOoutputStatus
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
			this.OutputBT = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// OutputBT
			// 
			this.OutputBT.Location = new System.Drawing.Point(3, 0);
			this.OutputBT.Name = "OutputBT";
			this.OutputBT.Size = new System.Drawing.Size(42, 30);
			this.OutputBT.TabIndex = 0;
			this.OutputBT.Text = "OFF";
			this.OutputBT.UseVisualStyleBackColor = true;
			this.OutputBT.Click += new System.EventHandler(this.OutputBT_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(112, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(59, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "outname";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(51, 1);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(64, 28);
			this.button1.TabIndex = 2;
			this.button1.Text = "防呆配置";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// IOoutputStatus
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.OutputBT);
			this.Name = "IOoutputStatus";
			this.Size = new System.Drawing.Size(224, 31);
			this.Load += new System.EventHandler(this.IOoutputStatus_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button OutputBT;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button1;
	}
}
