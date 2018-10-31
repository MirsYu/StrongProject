namespace StrongProject
{
	partial class UserControl_NetSocket
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
			this.textBox_Ip = new System.Windows.Forms.TextBox();
			this.textBox_port = new System.Windows.Forms.TextBox();
			this.label_ip = new System.Windows.Forms.Label();
			this.label_port = new System.Windows.Forms.Label();
			this.button_Save = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.button_Send = new System.Windows.Forms.Button();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.button_Connect = new System.Windows.Forms.Button();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.checkBox_Enable = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// textBox_Ip
			// 
			this.textBox_Ip.Location = new System.Drawing.Point(141, 5);
			this.textBox_Ip.Name = "textBox_Ip";
			this.textBox_Ip.Size = new System.Drawing.Size(107, 21);
			this.textBox_Ip.TabIndex = 0;
			// 
			// textBox_port
			// 
			this.textBox_port.Location = new System.Drawing.Point(285, 5);
			this.textBox_port.Name = "textBox_port";
			this.textBox_port.Size = new System.Drawing.Size(50, 21);
			this.textBox_port.TabIndex = 1;
			// 
			// label_ip
			// 
			this.label_ip.AutoSize = true;
			this.label_ip.Location = new System.Drawing.Point(100, 8);
			this.label_ip.Name = "label_ip";
			this.label_ip.Size = new System.Drawing.Size(41, 12);
			this.label_ip.TabIndex = 3;
			this.label_ip.Text = "IP地址";
			// 
			// label_port
			// 
			this.label_port.AutoSize = true;
			this.label_port.Location = new System.Drawing.Point(252, 9);
			this.label_port.Name = "label_port";
			this.label_port.Size = new System.Drawing.Size(29, 12);
			this.label_port.TabIndex = 4;
			this.label_port.Text = "端口";
			// 
			// button_Save
			// 
			this.button_Save.Location = new System.Drawing.Point(341, 5);
			this.button_Save.Name = "button_Save";
			this.button_Save.Size = new System.Drawing.Size(75, 23);
			this.button_Save.TabIndex = 6;
			this.button_Save.Text = "修改";
			this.button_Save.UseVisualStyleBackColor = true;
			this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(62, 81);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(273, 65);
			this.textBox1.TabIndex = 9;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 41);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 12);
			this.label1.TabIndex = 10;
			this.label1.Text = "屏蔽后默认返回值";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(5, 3);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(89, 21);
			this.txtName.TabIndex = 11;
			// 
			// button_Send
			// 
			this.button_Send.Location = new System.Drawing.Point(341, 123);
			this.button_Send.Name = "button_Send";
			this.button_Send.Size = new System.Drawing.Size(75, 23);
			this.button_Send.TabIndex = 12;
			this.button_Send.Text = "发送";
			this.button_Send.UseVisualStyleBackColor = true;
			this.button_Send.Click += new System.EventHandler(this.button_Send_Click);
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(3, 152);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(332, 75);
			this.textBox2.TabIndex = 13;
			// 
			// button_Connect
			// 
			this.button_Connect.Location = new System.Drawing.Point(341, 204);
			this.button_Connect.Name = "button_Connect";
			this.button_Connect.Size = new System.Drawing.Size(75, 23);
			this.button_Connect.TabIndex = 14;
			this.button_Connect.Text = "连接";
			this.button_Connect.UseVisualStyleBackColor = true;
			this.button_Connect.Click += new System.EventHandler(this.button_Connect_Click);
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(105, 32);
			this.textBox3.Multiline = true;
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(230, 43);
			this.textBox3.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 116);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(53, 12);
			this.label2.TabIndex = 16;
			this.label2.Text = "发送内容";
			// 
			// checkBox_Enable
			// 
			this.checkBox_Enable.AutoSize = true;
			this.checkBox_Enable.Location = new System.Drawing.Point(358, 38);
			this.checkBox_Enable.Name = "checkBox_Enable";
			this.checkBox_Enable.Size = new System.Drawing.Size(48, 16);
			this.checkBox_Enable.TabIndex = 17;
			this.checkBox_Enable.Text = "屏蔽";
			this.checkBox_Enable.UseVisualStyleBackColor = true;
			// 
			// UserControl_NetSocket
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.checkBox_Enable);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.button_Connect);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.button_Send);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.button_Save);
			this.Controls.Add(this.label_port);
			this.Controls.Add(this.label_ip);
			this.Controls.Add(this.textBox_port);
			this.Controls.Add(this.textBox_Ip);
			this.Name = "UserControl_NetSocket";
			this.Size = new System.Drawing.Size(427, 243);
			this.Load += new System.EventHandler(this.UserControl_NetSocket_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox_Ip;
		private System.Windows.Forms.TextBox textBox_port;
		private System.Windows.Forms.Label label_ip;
		private System.Windows.Forms.Label label_port;
		private System.Windows.Forms.Button button_Save;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Button button_Send;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button button_Connect;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox_Enable;
	}
}
