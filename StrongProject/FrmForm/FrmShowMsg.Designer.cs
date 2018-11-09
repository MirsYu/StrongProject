namespace StrongProject
{
	partial class FrmShowMsg
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmShowMsg));
			this.btnYes = new System.Windows.Forms.Button();
			this.btnNo = new System.Windows.Forms.Button();
			this.lblMsg = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnYes
			// 
			this.btnYes.Location = new System.Drawing.Point(87, 143);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(122, 46);
			this.btnYes.TabIndex = 0;
			this.btnYes.Text = "是";
			this.btnYes.UseVisualStyleBackColor = true;
			this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
			// 
			// btnNo
			// 
			this.btnNo.Location = new System.Drawing.Point(343, 143);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(122, 46);
			this.btnNo.TabIndex = 0;
			this.btnNo.Text = "否";
			this.btnNo.UseVisualStyleBackColor = true;
			this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
			// 
			// lblMsg
			// 
			this.lblMsg.Font = new System.Drawing.Font("Consolas", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblMsg.ForeColor = System.Drawing.Color.Red;
			this.lblMsg.Location = new System.Drawing.Point(3, 8);
			this.lblMsg.Name = "lblMsg";
			this.lblMsg.Size = new System.Drawing.Size(563, 125);
			this.lblMsg.TabIndex = 1;
			this.lblMsg.Text = "label1";
			this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblMsg.Click += new System.EventHandler(this.lblMsg_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.btnYes);
			this.panel1.Controls.Add(this.lblMsg);
			this.panel1.Controls.Add(this.btnNo);
			this.panel1.Location = new System.Drawing.Point(6, 7);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(581, 205);
			this.panel1.TabIndex = 2;
			// 
			// FrmShowMsg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Red;
			this.ClientSize = new System.Drawing.Size(593, 219);
			this.ControlBox = false;
			this.Controls.Add(this.panel1);
			this.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmShowMsg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmShowMsg";
			this.TopMost = true;
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmShowMsg_FormClosing);
			this.Load += new System.EventHandler(this.FrmShowMsg_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btnYes;
		private System.Windows.Forms.Button btnNo;
		private System.Windows.Forms.Label lblMsg;
		private System.Windows.Forms.Panel panel1;
	}
}