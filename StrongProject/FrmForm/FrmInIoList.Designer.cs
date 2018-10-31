namespace StrongProject
{
	partial class FrmInIoList
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("根");
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.button_Add = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.button3 = new System.Windows.Forms.Button();
			this.comboBox_Stat = new System.Windows.Forms.ComboBox();
			this.button4_AddIn = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.treeView_IO = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.checkBox_andCheck = new System.Windows.Forms.CheckBox();
			this.ucl_InIo2 = new StrongProject.ucl_InIo();
			this.ucl_InIo1 = new StrongProject.ucl_InIo();
			this.ucl_InIoName2 = new StrongProject.Ucl_InIoName();
			this.ucl_InIoName1 = new StrongProject.Ucl_InIoName();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// listBox1
			// 
			this.listBox1.FormattingEnabled = true;
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(273, 128);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(292, 340);
			this.listBox1.TabIndex = 2;
			// 
			// button_Add
			// 
			this.button_Add.Location = new System.Drawing.Point(273, 99);
			this.button_Add.Name = "button_Add";
			this.button_Add.Size = new System.Drawing.Size(100, 23);
			this.button_Add.TabIndex = 3;
			this.button_Add.Text = "配置为输出IO1";
			this.button_Add.UseVisualStyleBackColor = true;
			this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(376, 99);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(92, 23);
			this.button1.TabIndex = 11;
			this.button1.Text = "配置为输出IO2";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// listBox2
			// 
			this.listBox2.FormattingEnabled = true;
			this.listBox2.ItemHeight = 12;
			this.listBox2.Location = new System.Drawing.Point(757, 128);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(238, 340);
			this.listBox2.TabIndex = 12;
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(757, 99);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(75, 23);
			this.button3.TabIndex = 15;
			this.button3.Text = "配置为IO1";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// comboBox_Stat
			// 
			this.comboBox_Stat.FormattingEnabled = true;
			this.comboBox_Stat.Location = new System.Drawing.Point(571, 102);
			this.comboBox_Stat.Name = "comboBox_Stat";
			this.comboBox_Stat.Size = new System.Drawing.Size(157, 20);
			this.comboBox_Stat.TabIndex = 17;
			this.comboBox_Stat.SelectedIndexChanged += new System.EventHandler(this.comboBox_Stat_SelectedIndexChanged);
			// 
			// button4_AddIn
			// 
			this.button4_AddIn.Location = new System.Drawing.Point(481, 99);
			this.button4_AddIn.Name = "button4_AddIn";
			this.button4_AddIn.Size = new System.Drawing.Size(84, 23);
			this.button4_AddIn.TabIndex = 18;
			this.button4_AddIn.Text = "添加到工位";
			this.button4_AddIn.UseVisualStyleBackColor = true;
			this.button4_AddIn.Click += new System.EventHandler(this.button4_AddIn_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(241, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 12);
			this.label1.TabIndex = 20;
			this.label1.Text = "输出IO控制1";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(855, 99);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(102, 23);
			this.button2.TabIndex = 22;
			this.button2.Text = "配置为安全IO";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click_1);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(734, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 23;
			this.label2.Text = "到位io设置";
			// 
			// treeView_IO
			// 
			this.treeView_IO.ContextMenuStrip = this.contextMenuStrip1;
			this.treeView_IO.Location = new System.Drawing.Point(5, 24);
			this.treeView_IO.Name = "treeView_IO";
			treeNode1.Name = "节点0";
			treeNode1.Text = "根";
			this.treeView_IO.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
			treeNode1});
			this.treeView_IO.Size = new System.Drawing.Size(223, 419);
			this.treeView_IO.TabIndex = 24;
			this.treeView_IO.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView_IO_AfterLabelEdit);
			this.treeView_IO.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView_IO_NodeMouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.删除ToolStripMenuItem,
			this.添加ToolStripMenuItem,
			this.重命名ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(113, 70);
			// 
			// 删除ToolStripMenuItem
			// 
			this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
			this.删除ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.删除ToolStripMenuItem.Text = "删除";
			this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
			// 
			// 添加ToolStripMenuItem
			// 
			this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
			this.添加ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.添加ToolStripMenuItem.Text = "添加";
			this.添加ToolStripMenuItem.Click += new System.EventHandler(this.添加ToolStripMenuItem_Click);
			// 
			// 重命名ToolStripMenuItem
			// 
			this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
			this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.重命名ToolStripMenuItem.Text = "重命名";
			this.重命名ToolStripMenuItem.Click += new System.EventHandler(this.重命名ToolStripMenuItem_Click);
			// 
			// checkBox_andCheck
			// 
			this.checkBox_andCheck.AutoSize = true;
			this.checkBox_andCheck.Location = new System.Drawing.Point(992, 102);
			this.checkBox_andCheck.Name = "checkBox_andCheck";
			this.checkBox_andCheck.Size = new System.Drawing.Size(72, 16);
			this.checkBox_andCheck.TabIndex = 25;
			this.checkBox_andCheck.Text = "并行执行";
			this.checkBox_andCheck.UseVisualStyleBackColor = true;
			this.checkBox_andCheck.CheckedChanged += new System.EventHandler(this.checkBox_andCheck_CheckedChanged);
			// 
			// ucl_InIo2
			// 
			this.ucl_InIo2.Location = new System.Drawing.Point(806, 59);
			this.ucl_InIo2.Margin = new System.Windows.Forms.Padding(4);
			this.ucl_InIo2.Name = "ucl_InIo2";
			this.ucl_InIo2.Size = new System.Drawing.Size(366, 33);
			this.ucl_InIo2.TabIndex = 21;
			// 
			// ucl_InIo1
			// 
			this.ucl_InIo1.Location = new System.Drawing.Point(806, 24);
			this.ucl_InIo1.Margin = new System.Windows.Forms.Padding(4);
			this.ucl_InIo1.Name = "ucl_InIo1";
			this.ucl_InIo1.Size = new System.Drawing.Size(366, 33);
			this.ucl_InIo1.TabIndex = 13;
			// 
			// ucl_InIoName2
			// 
			this.ucl_InIoName2.Location = new System.Drawing.Point(315, 60);
			this.ucl_InIoName2.Margin = new System.Windows.Forms.Padding(4);
			this.ucl_InIoName2.Name = "ucl_InIoName2";
			this.ucl_InIoName2.Size = new System.Drawing.Size(322, 33);
			this.ucl_InIoName2.TabIndex = 10;
			// 
			// ucl_InIoName1
			// 
			this.ucl_InIoName1.Location = new System.Drawing.Point(313, 24);
			this.ucl_InIoName1.Margin = new System.Windows.Forms.Padding(4);
			this.ucl_InIoName1.Name = "ucl_InIoName1";
			this.ucl_InIoName1.Size = new System.Drawing.Size(347, 33);
			this.ucl_InIoName1.TabIndex = 9;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(734, 69);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 12);
			this.label3.TabIndex = 26;
			this.label3.Text = "安全io设置";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(241, 69);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(71, 12);
			this.label4.TabIndex = 27;
			this.label4.Text = "输出IO控制2";
			// 
			// FrmInIoList
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1180, 524);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.checkBox_andCheck);
			this.Controls.Add(this.treeView_IO);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.ucl_InIo2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.button4_AddIn);
			this.Controls.Add(this.comboBox_Stat);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.ucl_InIo1);
			this.Controls.Add(this.listBox2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.ucl_InIoName2);
			this.Controls.Add(this.ucl_InIoName1);
			this.Controls.Add(this.button_Add);
			this.Controls.Add(this.listBox1);
			this.Name = "FrmInIoList";
			this.Text = "IO输入列表";
			this.Load += new System.EventHandler(this.FrmInIoList_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.Button button_Add;
		private Ucl_InIoName ucl_InIoName1;
		private Ucl_InIoName ucl_InIoName2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ListBox listBox2;
		private StrongProject.ucl_InIo ucl_InIo1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ComboBox comboBox_Stat;
		private System.Windows.Forms.Button button4_AddIn;
		private System.Windows.Forms.Label label1;
		private StrongProject.ucl_InIo ucl_InIo2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TreeView treeView_IO;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 重命名ToolStripMenuItem;
		private System.Windows.Forms.CheckBox checkBox_andCheck;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;

	}
}