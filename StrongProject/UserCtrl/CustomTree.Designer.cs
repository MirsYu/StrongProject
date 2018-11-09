namespace StrongProject.UserCtrl
{
	partial class CustomTree
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
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.treeView_Flow = new System.Windows.Forms.TreeView();
			this.button_AddChlid = new System.Windows.Forms.Button();
			this.button_AddParent = new System.Windows.Forms.Button();
			this.button_Enable = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// treeView_Flow
			// 
			this.treeView_Flow.AllowDrop = true;
			this.treeView_Flow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.treeView_Flow.Location = new System.Drawing.Point(0, 0);
			this.treeView_Flow.Name = "treeView_Flow";
			this.treeView_Flow.Size = new System.Drawing.Size(458, 480);
			this.treeView_Flow.TabIndex = 0;
			this.treeView_Flow.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_Flow_ItemDrag);
			this.treeView_Flow.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView_Flow_DragDrop);
			this.treeView_Flow.DragEnter += new System.Windows.Forms.DragEventHandler(this.treeView_Flow_DragEnter);
			this.treeView_Flow.DragOver += new System.Windows.Forms.DragEventHandler(this.treeView_Flow_DragOver);
			this.treeView_Flow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_Flow_MouseDown);
			// 
			// button_AddChlid
			// 
			this.button_AddChlid.Location = new System.Drawing.Point(179, 412);
			this.button_AddChlid.Name = "button_AddChlid";
			this.button_AddChlid.Size = new System.Drawing.Size(75, 23);
			this.button_AddChlid.TabIndex = 1;
			this.button_AddChlid.Text = "添加子节点";
			this.button_AddChlid.UseVisualStyleBackColor = true;
			this.button_AddChlid.Visible = false;
			this.button_AddChlid.Click += new System.EventHandler(this.button_AddChlid_Click);
			// 
			// button_AddParent
			// 
			this.button_AddParent.Location = new System.Drawing.Point(273, 412);
			this.button_AddParent.Name = "button_AddParent";
			this.button_AddParent.Size = new System.Drawing.Size(75, 23);
			this.button_AddParent.TabIndex = 2;
			this.button_AddParent.Text = "添加父节点";
			this.button_AddParent.UseVisualStyleBackColor = true;
			this.button_AddParent.Visible = false;
			this.button_AddParent.Click += new System.EventHandler(this.button_AddParent_Click);
			// 
			// button_Enable
			// 
			this.button_Enable.Location = new System.Drawing.Point(364, 412);
			this.button_Enable.Name = "button_Enable";
			this.button_Enable.Size = new System.Drawing.Size(75, 23);
			this.button_Enable.TabIndex = 3;
			this.button_Enable.Text = "打开关闭";
			this.button_Enable.UseVisualStyleBackColor = true;
			this.button_Enable.Visible = false;
			this.button_Enable.Click += new System.EventHandler(this.button_Enable_Click);
			// 
			// CustomTree
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button_Enable);
			this.Controls.Add(this.button_AddParent);
			this.Controls.Add(this.button_AddChlid);
			this.Controls.Add(this.treeView_Flow);
			this.Name = "CustomTree";
			this.Size = new System.Drawing.Size(458, 480);
			this.DoubleBuffered = true;this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TreeView treeView_Flow;
		private System.Windows.Forms.Button button_AddChlid;
		private System.Windows.Forms.Button button_AddParent;
		private System.Windows.Forms.Button button_Enable;
	}
}
