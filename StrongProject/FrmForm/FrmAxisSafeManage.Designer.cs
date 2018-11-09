namespace StrongProject
{
	partial class FrmAxisSafeManage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAxisSafeManage));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(559, 36);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(555, 344);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "配置";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(10, 9);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "全部保存";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listBox2
			// 
			this.listBox2.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.listBox2.FormattingEnabled = true;
			this.listBox2.ItemHeight = 19;
			this.listBox2.Location = new System.Drawing.Point(10, 38);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(244, 346);
			this.listBox2.TabIndex = 11;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(260, 84);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(43, 23);
			this.button2.TabIndex = 10;
			this.button2.Text = "移除";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(260, 55);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(43, 23);
			this.button3.TabIndex = 9;
			this.button3.Text = "->";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// listBox1
			// 
			this.listBox1.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 19;
			this.listBox1.Location = new System.Drawing.Point(309, 38);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(244, 346);
			this.listBox1.TabIndex = 12;
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(298, 6);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(75, 23);
			this.button4.TabIndex = 13;
			this.button4.Text = "刷新";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// FrmAxisSafeManage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1126, 403);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.listBox1);
			this.Controls.Add(this.listBox2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmAxisSafeManage";
			this.Text = "点位轴防呆配置";
			this.Load += new System.EventHandler(this.FrmAxisSafeManage_Load);
			this.DoubleBuffered = true;this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button4;
	}
}