namespace StrongProject
{
	partial class Frm_Main
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
			this.components = new System.ComponentModel.Container();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label_state = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox_RunInfo = new System.Windows.Forms.GroupBox();
			this.groupBox_RunData = new System.Windows.Forms.GroupBox();
			this.panel_NG = new System.Windows.Forms.Panel();
			this.panel_Charts = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 1000;
			this.timer1.Tick += new System.EventHandler(this.timerUI_Tick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label_state);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Location = new System.Drawing.Point(3, 7);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1244, 75);
			this.panel1.TabIndex = 18;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Image = global::StrongProject.Properties.Resources.bigbk_red;
			this.label3.Location = new System.Drawing.Point(8, 6);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(123, 64);
			this.label3.TabIndex = 13;
			this.label3.Text = "Error Code";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Image = global::StrongProject.Properties.Resources.bigbk_red;
			this.label4.Location = new System.Drawing.Point(140, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(123, 64);
			this.label4.TabIndex = 11;
			this.label4.Text = "AlarmTime";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
			this.label2.Location = new System.Drawing.Point(272, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123, 64);
			this.label2.TabIndex = 12;
			this.label2.Text = "   UPH     200";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_state
			// 
			this.label_state.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label_state.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
			this.label_state.Location = new System.Drawing.Point(716, 6);
			this.label_state.Name = "label_state";
			this.label_state.Size = new System.Drawing.Size(123, 64);
			this.label_state.TabIndex = 10;
			this.label_state.Text = "未复位";
			this.label_state.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("幼圆", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label6.Image = global::StrongProject.Properties.Resources.bigbk_sel_;
			this.label6.Location = new System.Drawing.Point(404, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(123, 64);
			this.label6.TabIndex = 10;
			this.label6.Text = "   CT    11.6";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// groupBox_RunInfo
			// 
			this.groupBox_RunInfo.Location = new System.Drawing.Point(623, 464);
			this.groupBox_RunInfo.Name = "groupBox_RunInfo";
			this.groupBox_RunInfo.Size = new System.Drawing.Size(630, 112);
			this.groupBox_RunInfo.TabIndex = 467;
			this.groupBox_RunInfo.TabStop = false;
			this.groupBox_RunInfo.Text = "运行信息";
			// 
			// groupBox_RunData
			// 
			this.groupBox_RunData.Location = new System.Drawing.Point(3, 464);
			this.groupBox_RunData.Name = "groupBox_RunData";
			this.groupBox_RunData.Size = new System.Drawing.Size(614, 112);
			this.groupBox_RunData.TabIndex = 466;
			this.groupBox_RunData.TabStop = false;
			this.groupBox_RunData.Text = "生产数据";
			// 
			// panel_NG
			// 
			this.panel_NG.BackColor = System.Drawing.Color.White;
			this.panel_NG.Location = new System.Drawing.Point(623, 84);
			this.panel_NG.Name = "panel_NG";
			this.panel_NG.Size = new System.Drawing.Size(624, 374);
			this.panel_NG.TabIndex = 465;
			// 
			// panel_Charts
			// 
			this.panel_Charts.Location = new System.Drawing.Point(4, 84);
			this.panel_Charts.Name = "panel_Charts";
			this.panel_Charts.Size = new System.Drawing.Size(613, 374);
			this.panel_Charts.TabIndex = 464;
			// 
			// Frm_Main
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(1254, 579);
			this.Controls.Add(this.groupBox_RunInfo);
			this.Controls.Add(this.groupBox_RunData);
			this.Controls.Add(this.panel_NG);
			this.Controls.Add(this.panel_Charts);
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "Frm_Main";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Frm_Main";
			this.Load += new System.EventHandler(this.Frm_Main_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private StrongProject.MyChart myChartPie;

		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label_state;
		private System.Windows.Forms.GroupBox groupBox_RunInfo;
		private System.Windows.Forms.GroupBox groupBox_RunData;
		private System.Windows.Forms.Panel panel_NG;
		private System.Windows.Forms.Panel panel_Charts;
	}
}