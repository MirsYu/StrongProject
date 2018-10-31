namespace StrongProject
{
	partial class FrmStationMange
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
			System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("工位");
			System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("名称");
			System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ROOT");
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmStationMange));
			this.trVStation = new System.Windows.Forms.TreeView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.删除所有ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加两个点位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加CCD九宫格标定点位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.添加CCD11宫格标定工位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.修改名称ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.创建模板代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.嵌入到工程中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.每一步都嵌入代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.只收尾两步嵌入代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.首尾嵌代码中间共用ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.不嵌入到工程中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.创建复位工位模板代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.每一步都嵌入代码ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.只首尾两步嵌入代码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.首尾嵌代码中间共用ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.更新右列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.treView_station = new System.Windows.Forms.TreeView();
			this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.粘贴到本店前ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.粘贴到本点后ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.粘贴到本点的列表中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.移除本点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.粘贴到列表中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFileDialog_Code = new System.Windows.Forms.SaveFileDialog();
			this.checkBox_count = new System.Windows.Forms.CheckBox();
			this.texB_Count = new System.Windows.Forms.TextBox();
			this.groupBox_PORT = new System.Windows.Forms.GroupBox();
			this.groupBox_Net = new System.Windows.Forms.GroupBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.treeView1_Copy = new System.Windows.Forms.TreeView();
			this.contextMenuStrip3 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.清除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
			this.contextMenuStrip2.SuspendLayout();
			this.contextMenuStrip3.SuspendLayout();
			this.SuspendLayout();
			// 
			// trVStation
			// 
			this.trVStation.ContextMenuStrip = this.contextMenuStrip1;
			this.trVStation.Location = new System.Drawing.Point(12, 46);
			this.trVStation.Name = "trVStation";
			treeNode1.Name = "节点0";
			treeNode1.Text = "工位";
			this.trVStation.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
			this.trVStation.Size = new System.Drawing.Size(190, 457);
			this.trVStation.TabIndex = 0;
			this.trVStation.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.trVStation_AfterLabelEdit);
			this.trVStation.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.trVStation_NodeMouseDoubleClick);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除所有ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.添加ToolStripMenuItem,
            this.修改名称ToolStripMenuItem,
            this.创建模板代码ToolStripMenuItem,
            this.创建复位工位模板代码ToolStripMenuItem,
            this.更新右列表ToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(197, 158);
			// 
			// 删除所有ToolStripMenuItem
			// 
			this.删除所有ToolStripMenuItem.Name = "删除所有ToolStripMenuItem";
			this.删除所有ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.删除所有ToolStripMenuItem.Text = "删除所有";
			this.删除所有ToolStripMenuItem.Click += new System.EventHandler(this.删除所有ToolStripMenuItem_Click);
			// 
			// 删除ToolStripMenuItem
			// 
			this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
			this.删除ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.删除ToolStripMenuItem.Text = "删除";
			this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
			// 
			// 添加ToolStripMenuItem
			// 
			this.添加ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加两个点位ToolStripMenuItem,
            this.添加CCD九宫格标定点位ToolStripMenuItem,
            this.添加CCD11宫格标定工位ToolStripMenuItem});
			this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
			this.添加ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.添加ToolStripMenuItem.Text = "添加";
			// 
			// 添加两个点位ToolStripMenuItem
			// 
			this.添加两个点位ToolStripMenuItem.Name = "添加两个点位ToolStripMenuItem";
			this.添加两个点位ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.添加两个点位ToolStripMenuItem.Text = "添加一般工位";
			this.添加两个点位ToolStripMenuItem.Click += new System.EventHandler(this.添加两个点位ToolStripMenuItem_Click);
			// 
			// 添加CCD九宫格标定点位ToolStripMenuItem
			// 
			this.添加CCD九宫格标定点位ToolStripMenuItem.Name = "添加CCD九宫格标定点位ToolStripMenuItem";
			this.添加CCD九宫格标定点位ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.添加CCD九宫格标定点位ToolStripMenuItem.Text = "添加CCD九宫格标定工位";
			this.添加CCD九宫格标定点位ToolStripMenuItem.Click += new System.EventHandler(this.添加CCD九宫格标定点位ToolStripMenuItem_Click);
			// 
			// 添加CCD11宫格标定工位ToolStripMenuItem
			// 
			this.添加CCD11宫格标定工位ToolStripMenuItem.Name = "添加CCD11宫格标定工位ToolStripMenuItem";
			this.添加CCD11宫格标定工位ToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
			this.添加CCD11宫格标定工位ToolStripMenuItem.Text = "添加CCD11宫格标定工位";
			// 
			// 修改名称ToolStripMenuItem
			// 
			this.修改名称ToolStripMenuItem.Name = "修改名称ToolStripMenuItem";
			this.修改名称ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.修改名称ToolStripMenuItem.Text = "修改名称";
			this.修改名称ToolStripMenuItem.Click += new System.EventHandler(this.修改名称ToolStripMenuItem_Click);
			// 
			// 创建模板代码ToolStripMenuItem
			// 
			this.创建模板代码ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.嵌入到工程中ToolStripMenuItem,
            this.不嵌入到工程中ToolStripMenuItem});
			this.创建模板代码ToolStripMenuItem.Name = "创建模板代码ToolStripMenuItem";
			this.创建模板代码ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.创建模板代码ToolStripMenuItem.Text = "创建工位模板代码";
			// 
			// 嵌入到工程中ToolStripMenuItem
			// 
			this.嵌入到工程中ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.每一步都嵌入代码ToolStripMenuItem,
            this.只收尾两步嵌入代码ToolStripMenuItem,
            this.首尾嵌代码中间共用ToolStripMenuItem});
			this.嵌入到工程中ToolStripMenuItem.Name = "嵌入到工程中ToolStripMenuItem";
			this.嵌入到工程中ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.嵌入到工程中ToolStripMenuItem.Text = "启动就执行的工位";
			this.嵌入到工程中ToolStripMenuItem.Click += new System.EventHandler(this.嵌入到工程中ToolStripMenuItem_Click);
			// 
			// 每一步都嵌入代码ToolStripMenuItem
			// 
			this.每一步都嵌入代码ToolStripMenuItem.Name = "每一步都嵌入代码ToolStripMenuItem";
			this.每一步都嵌入代码ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.每一步都嵌入代码ToolStripMenuItem.Text = "每一步都嵌入代码";
			this.每一步都嵌入代码ToolStripMenuItem.Click += new System.EventHandler(this.每一步都嵌入代码ToolStripMenuItem_Click);
			// 
			// 只收尾两步嵌入代码ToolStripMenuItem
			// 
			this.只收尾两步嵌入代码ToolStripMenuItem.Name = "只收尾两步嵌入代码ToolStripMenuItem";
			this.只收尾两步嵌入代码ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.只收尾两步嵌入代码ToolStripMenuItem.Text = "只首尾两步嵌入代码";
			this.只收尾两步嵌入代码ToolStripMenuItem.Click += new System.EventHandler(this.只收尾两步嵌入代码ToolStripMenuItem_Click);
			// 
			// 首尾嵌代码中间共用ToolStripMenuItem
			// 
			this.首尾嵌代码中间共用ToolStripMenuItem.Name = "首尾嵌代码中间共用ToolStripMenuItem";
			this.首尾嵌代码中间共用ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.首尾嵌代码中间共用ToolStripMenuItem.Text = "首尾嵌代码中间共用";
			this.首尾嵌代码中间共用ToolStripMenuItem.Click += new System.EventHandler(this.首尾嵌代码中间共用ToolStripMenuItem_Click);
			// 
			// 不嵌入到工程中ToolStripMenuItem
			// 
			this.不嵌入到工程中ToolStripMenuItem.Name = "不嵌入到工程中ToolStripMenuItem";
			this.不嵌入到工程中ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.不嵌入到工程中ToolStripMenuItem.Text = "启动不执行的工位";
			// 
			// 创建复位工位模板代码ToolStripMenuItem
			// 
			this.创建复位工位模板代码ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.每一步都嵌入代码ToolStripMenuItem1,
            this.只首尾两步嵌入代码ToolStripMenuItem,
            this.首尾嵌代码中间共用ToolStripMenuItem1});
			this.创建复位工位模板代码ToolStripMenuItem.Name = "创建复位工位模板代码ToolStripMenuItem";
			this.创建复位工位模板代码ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.创建复位工位模板代码ToolStripMenuItem.Text = "创建复位工位模板代码";
			// 
			// 每一步都嵌入代码ToolStripMenuItem1
			// 
			this.每一步都嵌入代码ToolStripMenuItem1.Name = "每一步都嵌入代码ToolStripMenuItem1";
			this.每一步都嵌入代码ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
			this.每一步都嵌入代码ToolStripMenuItem1.Text = "每一步都嵌入代码";
			this.每一步都嵌入代码ToolStripMenuItem1.Click += new System.EventHandler(this.每一步都嵌入代码ToolStripMenuItem1_Click);
			// 
			// 只首尾两步嵌入代码ToolStripMenuItem
			// 
			this.只首尾两步嵌入代码ToolStripMenuItem.Name = "只首尾两步嵌入代码ToolStripMenuItem";
			this.只首尾两步嵌入代码ToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
			this.只首尾两步嵌入代码ToolStripMenuItem.Text = "只首尾两步嵌入代码";
			this.只首尾两步嵌入代码ToolStripMenuItem.Click += new System.EventHandler(this.只首尾两步嵌入代码ToolStripMenuItem_Click);
			// 
			// 首尾嵌代码中间共用ToolStripMenuItem1
			// 
			this.首尾嵌代码中间共用ToolStripMenuItem1.Name = "首尾嵌代码中间共用ToolStripMenuItem1";
			this.首尾嵌代码中间共用ToolStripMenuItem1.Size = new System.Drawing.Size(184, 22);
			this.首尾嵌代码中间共用ToolStripMenuItem1.Text = "首尾嵌代码中间共用";
			this.首尾嵌代码中间共用ToolStripMenuItem1.Click += new System.EventHandler(this.首尾嵌代码中间共用ToolStripMenuItem1_Click);
			// 
			// 更新右列表ToolStripMenuItem
			// 
			this.更新右列表ToolStripMenuItem.Name = "更新右列表ToolStripMenuItem";
			this.更新右列表ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.更新右列表ToolStripMenuItem.Text = "更新右列表";
			// 
			// treView_station
			// 
			this.treView_station.ContextMenuStrip = this.contextMenuStrip2;
			this.treView_station.Location = new System.Drawing.Point(208, 46);
			this.treView_station.Name = "treView_station";
			treeNode2.Name = "Root";
			treeNode2.Text = "名称";
			this.treView_station.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
			this.treView_station.Size = new System.Drawing.Size(356, 457);
			this.treView_station.TabIndex = 1;
			this.treView_station.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treView_station_NodeMouseDoubleClick);
			// 
			// contextMenuStrip2
			// 
			this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.粘贴ToolStripMenuItem,
            this.粘贴到本店前ToolStripMenuItem,
            this.粘贴到本点后ToolStripMenuItem,
            this.粘贴到本点的列表中ToolStripMenuItem,
            this.移除本点ToolStripMenuItem,
            this.粘贴到列表中ToolStripMenuItem});
			this.contextMenuStrip2.Name = "contextMenuStrip2";
			this.contextMenuStrip2.Size = new System.Drawing.Size(197, 136);
			// 
			// 粘贴ToolStripMenuItem
			// 
			this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
			this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.粘贴ToolStripMenuItem.Text = "复制";
			this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
			// 
			// 粘贴到本店前ToolStripMenuItem
			// 
			this.粘贴到本店前ToolStripMenuItem.Name = "粘贴到本店前ToolStripMenuItem";
			this.粘贴到本店前ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.粘贴到本店前ToolStripMenuItem.Text = "粘贴到本点前";
			this.粘贴到本店前ToolStripMenuItem.Click += new System.EventHandler(this.粘贴到本店前ToolStripMenuItem_Click);
			// 
			// 粘贴到本点后ToolStripMenuItem
			// 
			this.粘贴到本点后ToolStripMenuItem.Name = "粘贴到本点后ToolStripMenuItem";
			this.粘贴到本点后ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.粘贴到本点后ToolStripMenuItem.Text = "粘贴到本点后";
			this.粘贴到本点后ToolStripMenuItem.Click += new System.EventHandler(this.粘贴到本点后ToolStripMenuItem_Click);
			// 
			// 粘贴到本点的列表中ToolStripMenuItem
			// 
			this.粘贴到本点的列表中ToolStripMenuItem.Name = "粘贴到本点的列表中ToolStripMenuItem";
			this.粘贴到本点的列表中ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.粘贴到本点的列表中ToolStripMenuItem.Text = "粘贴到本点的列表中前";
			this.粘贴到本点的列表中ToolStripMenuItem.Click += new System.EventHandler(this.粘贴到本点的列表中ToolStripMenuItem_Click);
			// 
			// 移除本点ToolStripMenuItem
			// 
			this.移除本点ToolStripMenuItem.Name = "移除本点ToolStripMenuItem";
			this.移除本点ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.移除本点ToolStripMenuItem.Text = "移除本点";
			this.移除本点ToolStripMenuItem.Click += new System.EventHandler(this.移除本点ToolStripMenuItem_Click);
			// 
			// 粘贴到列表中ToolStripMenuItem
			// 
			this.粘贴到列表中ToolStripMenuItem.Name = "粘贴到列表中ToolStripMenuItem";
			this.粘贴到列表中ToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
			this.粘贴到列表中ToolStripMenuItem.Text = "粘贴到列表中";
			this.粘贴到列表中ToolStripMenuItem.Click += new System.EventHandler(this.粘贴到列表中ToolStripMenuItem_Click);
			// 
			// checkBox_count
			// 
			this.checkBox_count.AutoSize = true;
			this.checkBox_count.Location = new System.Drawing.Point(692, 21);
			this.checkBox_count.Name = "checkBox_count";
			this.checkBox_count.Size = new System.Drawing.Size(60, 16);
			this.checkBox_count.TabIndex = 2;
			this.checkBox_count.Text = "点位数";
			this.checkBox_count.UseVisualStyleBackColor = true;
			// 
			// texB_Count
			// 
			this.texB_Count.Location = new System.Drawing.Point(772, 16);
			this.texB_Count.Name = "texB_Count";
			this.texB_Count.Size = new System.Drawing.Size(100, 21);
			this.texB_Count.TabIndex = 3;
			this.texB_Count.Text = "2";
			// 
			// groupBox_PORT
			// 
			this.groupBox_PORT.Location = new System.Drawing.Point(717, 46);
			this.groupBox_PORT.Name = "groupBox_PORT";
			this.groupBox_PORT.Size = new System.Drawing.Size(200, 235);
			this.groupBox_PORT.TabIndex = 4;
			this.groupBox_PORT.TabStop = false;
			this.groupBox_PORT.Text = "选择串口";
			// 
			// groupBox_Net
			// 
			this.groupBox_Net.Location = new System.Drawing.Point(717, 287);
			this.groupBox_Net.Name = "groupBox_Net";
			this.groupBox_Net.Size = new System.Drawing.Size(200, 216);
			this.groupBox_Net.TabIndex = 5;
			this.groupBox_Net.TabStop = false;
			this.groupBox_Net.Text = "选择网口";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(128, 19);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(525, 21);
			this.textBox1.TabIndex = 6;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(25, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 7;
			this.label1.Text = "工程路径";
			// 
			// treeView1_Copy
			// 
			this.treeView1_Copy.ContextMenuStrip = this.contextMenuStrip3;
			this.treeView1_Copy.Location = new System.Drawing.Point(581, 46);
			this.treeView1_Copy.Name = "treeView1_Copy";
			treeNode3.Name = "节点0";
			treeNode3.Text = "ROOT";
			this.treeView1_Copy.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
			this.treeView1_Copy.Size = new System.Drawing.Size(121, 457);
			this.treeView1_Copy.TabIndex = 8;
			// 
			// contextMenuStrip3
			// 
			this.contextMenuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.清除ToolStripMenuItem});
			this.contextMenuStrip3.Name = "contextMenuStrip3";
			this.contextMenuStrip3.Size = new System.Drawing.Size(101, 26);
			// 
			// 清除ToolStripMenuItem
			// 
			this.清除ToolStripMenuItem.Name = "清除ToolStripMenuItem";
			this.清除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
			this.清除ToolStripMenuItem.Text = "清除";
			this.清除ToolStripMenuItem.Click += new System.EventHandler(this.清除ToolStripMenuItem_Click);
			// 
			// FrmStationMange
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(933, 515);
			this.Controls.Add(this.treeView1_Copy);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.treView_station);
			this.Controls.Add(this.groupBox_PORT);
			this.Controls.Add(this.texB_Count);
			this.Controls.Add(this.groupBox_Net);
			this.Controls.Add(this.trVStation);
			this.Controls.Add(this.checkBox_count);
			this.Font = new System.Drawing.Font("幼圆", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmStationMange";
			this.Text = "工位管理";
			this.Load += new System.EventHandler(this.FrmStationMange_Load);
			this.contextMenuStrip1.ResumeLayout(false);
			this.contextMenuStrip2.ResumeLayout(false);
			this.contextMenuStrip3.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TreeView trVStation;
		private System.Windows.Forms.TreeView treView_station;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 删除所有ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 修改名称ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 添加两个点位ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 添加CCD九宫格标定点位ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 添加CCD11宫格标定工位ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 创建模板代码ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 嵌入到工程中ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 不嵌入到工程中ToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog saveFileDialog_Code;
		private System.Windows.Forms.ToolStripMenuItem 每一步都嵌入代码ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 只收尾两步嵌入代码ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 首尾嵌代码中间共用ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 创建复位工位模板代码ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 每一步都嵌入代码ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 只首尾两步嵌入代码ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 首尾嵌代码中间共用ToolStripMenuItem1;
		private System.Windows.Forms.CheckBox checkBox_count;
		private System.Windows.Forms.TextBox texB_Count;
		private System.Windows.Forms.GroupBox groupBox_PORT;
		private System.Windows.Forms.GroupBox groupBox_Net;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.ToolStripMenuItem 更新右列表ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 粘贴到本店前ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 粘贴到本点后ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 粘贴到本点的列表中ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 移除本点ToolStripMenuItem;
		private System.Windows.Forms.TreeView treeView1_Copy;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip3;
		private System.Windows.Forms.ToolStripMenuItem 清除ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 粘贴到列表中ToolStripMenuItem;
	}
}