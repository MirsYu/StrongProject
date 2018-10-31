namespace StrongProject
{
	partial class CTLSerialPort
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
			this.components = new System.ComponentModel.Container();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.checkBox_enable = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cmbPortName = new System.Windows.Forms.ComboBox();
			this.label18 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.cmbBaudRate = new System.Windows.Forms.ComboBox();
			this.cmbDataBits = new System.Windows.Forms.ComboBox();
			this.cmbParity = new System.Windows.Forms.ComboBox();
			this.label21 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.cmbStopBits = new System.Windows.Forms.ComboBox();
			this.btnClear = new System.Windows.Forms.Button();
			this.txtRecPort1 = new System.Windows.Forms.TextBox();
			this.txtSendPort1 = new System.Windows.Forms.TextBox();
			this.btnSend = new System.Windows.Forms.Button();
			this.lblPortInd = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnSPopen = new System.Windows.Forms.Button();
			this.spTest1 = new System.IO.Ports.SerialPort(this.components);
			this.label3 = new System.Windows.Forms.Label();
			this.textBox_name = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox_name);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.checkBox_enable);
			this.groupBox1.Controls.Add(this.panel1);
			this.groupBox1.Controls.Add(this.btnClear);
			this.groupBox1.Controls.Add(this.txtRecPort1);
			this.groupBox1.Controls.Add(this.txtSendPort1);
			this.groupBox1.Controls.Add(this.btnSend);
			this.groupBox1.Controls.Add(this.lblPortInd);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.btnSPopen);
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(311, 236);
			this.groupBox1.TabIndex = 19;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Port";
			// 
			// checkBox_enable
			// 
			this.checkBox_enable.AutoSize = true;
			this.checkBox_enable.Location = new System.Drawing.Point(230, 199);
			this.checkBox_enable.Name = "checkBox_enable";
			this.checkBox_enable.Size = new System.Drawing.Size(72, 16);
			this.checkBox_enable.TabIndex = 28;
			this.checkBox_enable.Text = "屏蔽发送";
			this.checkBox_enable.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cmbPortName);
			this.panel1.Controls.Add(this.label18);
			this.panel1.Controls.Add(this.label19);
			this.panel1.Controls.Add(this.cmbBaudRate);
			this.panel1.Controls.Add(this.cmbDataBits);
			this.panel1.Controls.Add(this.cmbParity);
			this.panel1.Controls.Add(this.label21);
			this.panel1.Controls.Add(this.label22);
			this.panel1.Controls.Add(this.label20);
			this.panel1.Controls.Add(this.cmbStopBits);
			this.panel1.Location = new System.Drawing.Point(8, 55);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(138, 131);
			this.panel1.TabIndex = 27;
			// 
			// cmbPortName
			// 
			this.cmbPortName.FormattingEnabled = true;
			this.cmbPortName.Location = new System.Drawing.Point(75, 3);
			this.cmbPortName.Name = "cmbPortName";
			this.cmbPortName.Size = new System.Drawing.Size(60, 20);
			this.cmbPortName.TabIndex = 14;
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(24, 5);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(41, 12);
			this.label18.TabIndex = 15;
			this.label18.Text = "端口：";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(12, 30);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(53, 12);
			this.label19.TabIndex = 16;
			this.label19.Text = "波特率：";
			// 
			// cmbBaudRate
			// 
			this.cmbBaudRate.FormattingEnabled = true;
			this.cmbBaudRate.Items.AddRange(new object[] {
			"1200",
			"2400",
			"4800",
			"9600",
			"14400",
			"19200",
			"38400",
			"56000",
			"57600",
			"115200",
			"194000"});
			this.cmbBaudRate.Location = new System.Drawing.Point(75, 28);
			this.cmbBaudRate.Name = "cmbBaudRate";
			this.cmbBaudRate.Size = new System.Drawing.Size(60, 20);
			this.cmbBaudRate.TabIndex = 17;
			// 
			// cmbDataBits
			// 
			this.cmbDataBits.FormattingEnabled = true;
			this.cmbDataBits.Items.AddRange(new object[] {
			"5",
			"6",
			"7",
			"8"});
			this.cmbDataBits.Location = new System.Drawing.Point(75, 53);
			this.cmbDataBits.Name = "cmbDataBits";
			this.cmbDataBits.Size = new System.Drawing.Size(60, 20);
			this.cmbDataBits.TabIndex = 18;
			// 
			// cmbParity
			// 
			this.cmbParity.FormattingEnabled = true;
			this.cmbParity.Items.AddRange(new object[] {
			"NONE",
			"ODD",
			"EVEN"});
			this.cmbParity.Location = new System.Drawing.Point(75, 78);
			this.cmbParity.Name = "cmbParity";
			this.cmbParity.Size = new System.Drawing.Size(60, 20);
			this.cmbParity.TabIndex = 23;
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(10, 55);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(53, 12);
			this.label21.TabIndex = 19;
			this.label21.Text = "数据位：";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(20, 80);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(41, 12);
			this.label22.TabIndex = 22;
			this.label22.Text = "校验：";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(10, 106);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(53, 12);
			this.label20.TabIndex = 20;
			this.label20.Text = "停止位：";
			// 
			// cmbStopBits
			// 
			this.cmbStopBits.FormattingEnabled = true;
			this.cmbStopBits.Items.AddRange(new object[] {
			"NONE",
			"One",
			"Two"});
			this.cmbStopBits.Location = new System.Drawing.Point(75, 104);
			this.cmbStopBits.Name = "cmbStopBits";
			this.cmbStopBits.Size = new System.Drawing.Size(60, 20);
			this.cmbStopBits.TabIndex = 21;
			// 
			// btnClear
			// 
			this.btnClear.Enabled = false;
			this.btnClear.Location = new System.Drawing.Point(125, 194);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(82, 25);
			this.btnClear.TabIndex = 26;
			this.btnClear.Text = "clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			// 
			// txtRecPort1
			// 
			this.txtRecPort1.Location = new System.Drawing.Point(150, 111);
			this.txtRecPort1.Multiline = true;
			this.txtRecPort1.Name = "txtRecPort1";
			this.txtRecPort1.Size = new System.Drawing.Size(153, 77);
			this.txtRecPort1.TabIndex = 24;
			// 
			// txtSendPort1
			// 
			this.txtSendPort1.Location = new System.Drawing.Point(194, 52);
			this.txtSendPort1.Multiline = true;
			this.txtSendPort1.Name = "txtSendPort1";
			this.txtSendPort1.Size = new System.Drawing.Size(111, 18);
			this.txtSendPort1.TabIndex = 24;
			// 
			// btnSend
			// 
			this.btnSend.Enabled = false;
			this.btnSend.Location = new System.Drawing.Point(200, 79);
			this.btnSend.Name = "btnSend";
			this.btnSend.Size = new System.Drawing.Size(95, 25);
			this.btnSend.TabIndex = 19;
			this.btnSend.Text = "send";
			this.btnSend.UseVisualStyleBackColor = true;
			this.btnSend.Click += new System.EventHandler(this.btnSendPort1_Click);
			// 
			// lblPortInd
			// 
			this.lblPortInd.BackColor = System.Drawing.Color.Maroon;
			this.lblPortInd.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblPortInd.Location = new System.Drawing.Point(5, 194);
			this.lblPortInd.Name = "lblPortInd";
			this.lblPortInd.Size = new System.Drawing.Size(26, 25);
			this.lblPortInd.TabIndex = 13;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(153, 92);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(41, 12);
			this.label2.TabIndex = 15;
			this.label2.Text = "接收：";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(152, 58);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(41, 12);
			this.label1.TabIndex = 15;
			this.label1.Text = "发送：";
			// 
			// btnSPopen
			// 
			this.btnSPopen.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSPopen.Location = new System.Drawing.Point(37, 194);
			this.btnSPopen.Name = "btnSPopen";
			this.btnSPopen.Size = new System.Drawing.Size(82, 25);
			this.btnSPopen.TabIndex = 12;
			this.btnSPopen.Text = "打开串口";
			this.btnSPopen.UseVisualStyleBackColor = true;
			this.btnSPopen.Click += new System.EventHandler(this.btnSPopen_Click);
			// 
			// spTest1
			// 
			this.spTest1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.spTest1_DataReceived);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(9, 21);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 29;
			this.label3.Text = "名称";
			// 
			// textBox_name
			// 
			this.textBox_name.Location = new System.Drawing.Point(69, 18);
			this.textBox_name.Name = "textBox_name";
			this.textBox_name.Size = new System.Drawing.Size(233, 21);
			this.textBox_name.TabIndex = 30;
			// 
			// CTLSerialPort
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "CTLSerialPort";
			this.Size = new System.Drawing.Size(322, 247);
			this.Load += new System.EventHandler(this.CTLSerialPort_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtRecPort1;
		private System.Windows.Forms.TextBox txtSendPort1;
		private System.Windows.Forms.Button btnSend;
		private System.Windows.Forms.ComboBox cmbParity;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.ComboBox cmbStopBits;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label lblPortInd;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.ComboBox cmbDataBits;
		private System.Windows.Forms.ComboBox cmbBaudRate;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.ComboBox cmbPortName;
		private System.Windows.Forms.Button btnSPopen;
		private System.IO.Ports.SerialPort spTest1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox checkBox_enable;
		private System.Windows.Forms.TextBox textBox_name;
		private System.Windows.Forms.Label label3;
	}
}
