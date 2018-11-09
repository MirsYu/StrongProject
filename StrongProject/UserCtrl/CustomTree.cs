﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StrongProject.UserCtrl
{
	public partial class CustomTree : UserControl
	{

		public StationModule tag_StationModule;
		public TreeNode parent;
		public TreeNode chlid;

		private string NodeMap;
		private const int MAPSIZE = 128;
		private StringBuilder NewNodeMap = new StringBuilder(MAPSIZE);


		public CustomTree()
		{
			InitializeComponent();

			ImageList TreeviewIL = new ImageList();
			TreeviewIL.Images.Add(Properties.Resources.folder);
			TreeviewIL.Images.Add(Properties.Resources.node);
			this.treeView_Flow.ImageList = TreeviewIL;
			this.treeView_Flow.HideSelection = false;
			this.treeView_Flow.ItemHeight = this.treeView_Flow.ItemHeight + 3;
			this.treeView_Flow.Indent = this.treeView_Flow.Indent + 3;
			this.treeView_Flow.ExpandAll();
		}


		public string ParentName { get; set; }

		public string ChlidName { get; set; }

		public void Init(string	stationName)
		{
			treeView_Flow.Nodes.Clear();
			tag_StationModule = StationManage.FindStation(stationName);
			if (tag_StationModule != null)
			{
				ParentName = stationName;
				this.button_AddParent_Click(null, EventArgs.Empty);
			}
			for (int i = 0; i < tag_StationModule.arrPoint.Count; i++)
			{
				ChlidName = tag_StationModule.arrPoint[i].strName;
				this.button_AddChlid_Click(null, EventArgs.Empty);
			}
		}

		private void button_AddChlid_Click(object sender, EventArgs e)
		{
			chlid = new TreeNode(ChlidName, 1, 1);
			this.parent.Nodes.Add(chlid);
			parent.ExpandAll();
		}

		private void button_AddParent_Click(object sender, EventArgs e)
		{
			parent = new TreeNode(ParentName, 0, 0);
			this.treeView_Flow.Nodes.Add(parent);
		}

		private void button_Enable_Click(object sender, EventArgs e)
		{

		}

		private void treeView_Flow_MouseDown(object sender, MouseEventArgs e)
		{
			this.treeView_Flow.SelectedNode = this.treeView_Flow.GetNodeAt(e.X, e.Y);
		}

		private void treeView_Flow_ItemDrag(object sender, ItemDragEventArgs e)
		{
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void treeView_Flow_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Move;
		}

		private void treeView_Flow_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode", false) && this.NodeMap != "")
			{
				TreeNode MovingNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
				string[] NodeIndexes = this.NodeMap.Split('|');
				TreeNodeCollection InsertCollection = this.treeView_Flow.Nodes;
				for (int i = 0; i < NodeIndexes.Length - 1; i++)
				{
					InsertCollection = InsertCollection[Int32.Parse(NodeIndexes[i])].Nodes;
				}

				if (InsertCollection != null)
				{
					InsertCollection.Insert(Int32.Parse(NodeIndexes[NodeIndexes.Length - 1]), (TreeNode)MovingNode.Clone());
					this.treeView_Flow.SelectedNode = InsertCollection[Int32.Parse(NodeIndexes[NodeIndexes.Length - 1])];
					MovingNode.Remove();
				}
			}
		}

		private void treeView_Flow_DragOver(object sender, DragEventArgs e)
		{
			TreeNode NodeOver = this.treeView_Flow.GetNodeAt(this.treeView_Flow.PointToClient(Cursor.Position));
			TreeNode NodeMoving = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");


			// A bit long, but to summarize, process the following code only if the nodeover is null
			// and either the nodeover is not the same thing as nodemoving UNLESSS nodeover happens
			// to be the last node in the branch (so we can allow drag & drop below a parent branch)
			if (NodeOver != null && (NodeOver != NodeMoving || (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))))
			{
				int OffsetY = this.treeView_Flow.PointToClient(Cursor.Position).Y - NodeOver.Bounds.Top;
				int NodeOverImageWidth = this.treeView_Flow.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
				Graphics g = this.treeView_Flow.CreateGraphics();

				// Image index of 1 is the non-folder icon
				if (NodeOver.ImageIndex == 1)
				{
					#region Standard Node
					if (OffsetY < (NodeOver.Bounds.Height / 2))
					{
						//this.lblDebug.Text = "top";

						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while (tnParadox.Parent != null)
						{
							if (tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						SetNewNodeMap(NodeOver, false);
						if (SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						this.DrawLeafTopPlaceholders(NodeOver);
						#endregion
					}
					else
					{
						//this.lblDebug.Text = "bottom";

						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while (tnParadox.Parent != null)
						{
							if (tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Allow drag drop to parent branches
						TreeNode ParentDragDrop = null;
						// If the node the mouse is over is the last node of the branch we should allow
						// the ability to drop the "nodemoving" node BELOW the parent node
						if (NodeOver.Parent != null && NodeOver.Index == (NodeOver.Parent.Nodes.Count - 1))
						{
							int XPos = this.treeView_Flow.PointToClient(Cursor.Position).X;
							if (XPos < NodeOver.Bounds.Left)
							{
								ParentDragDrop = NodeOver.Parent;

								if (XPos < (ParentDragDrop.Bounds.Left - this.treeView_Flow.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width))
								{
									if (ParentDragDrop.Parent != null)
										ParentDragDrop = ParentDragDrop.Parent;
								}
							}
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						// Since we are in a special case here, use the ParentDragDrop node as the current "nodeover"
						SetNewNodeMap(ParentDragDrop != null ? ParentDragDrop : NodeOver, true);
						if (SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						DrawLeafBottomPlaceholders(NodeOver, ParentDragDrop);
						#endregion
					}
					#endregion
				}
				else
				{
					#region Folder Node
					if (OffsetY < (NodeOver.Bounds.Height / 3))
					{
						//this.lblDebug.Text = "folder top";

						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while (tnParadox.Parent != null)
						{
							if (tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						SetNewNodeMap(NodeOver, false);
						if (SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						this.DrawFolderTopPlaceholders(NodeOver);
						#endregion
					}
					else if ((NodeOver.Parent != null && NodeOver.Index == 0) && (OffsetY > (NodeOver.Bounds.Height - (NodeOver.Bounds.Height / 3))))
					{
						//this.lblDebug.Text = "folder bottom";

						#region If NodeOver is a child then cancel
						TreeNode tnParadox = NodeOver;
						while (tnParadox.Parent != null)
						{
							if (tnParadox.Parent == NodeMoving)
							{
								this.NodeMap = "";
								return;
							}

							tnParadox = tnParadox.Parent;
						}
						#endregion
						#region Store the placeholder info into a pipe delimited string
						SetNewNodeMap(NodeOver, true);
						if (SetMapsEqual() == true)
							return;
						#endregion
						#region Clear placeholders above and below
						this.Refresh();
						#endregion
						#region Draw the placeholders
						DrawFolderTopPlaceholders(NodeOver);
						#endregion
					}
					else
					{
						//this.lblDebug.Text = "folder over";

						if (NodeOver.Nodes.Count > 0)
						{
							NodeOver.Expand();
							//this.Refresh();
						}
						else
						{
							#region Prevent the node from being dragged onto itself
							if (NodeMoving == NodeOver)
								return;
							#endregion
							#region If NodeOver is a child then cancel
							TreeNode tnParadox = NodeOver;
							while (tnParadox.Parent != null)
							{
								if (tnParadox.Parent == NodeMoving)
								{
									this.NodeMap = "";
									return;
								}

								tnParadox = tnParadox.Parent;
							}
							#endregion
							#region Store the placeholder info into a pipe delimited string
							SetNewNodeMap(NodeOver, false);
							NewNodeMap = NewNodeMap.Insert(NewNodeMap.Length, "|0");

							if (SetMapsEqual() == true)
								return;
							#endregion
							#region Clear placeholders above and below
							this.Refresh();
							#endregion
							#region Draw the "add to folder" placeholder
							DrawAddToFolderPlaceholder(NodeOver);
							#endregion
						}
					}
					#endregion
				}
			}
		}

		#region Helper Methods
		private void DrawLeafTopPlaceholders(TreeNode NodeOver)
		{
			Graphics g = this.treeView_Flow.CreateGraphics();

			int NodeOverImageWidth = this.treeView_Flow.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
			int LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
			int RightPos = this.treeView_Flow.Width - 4;

			Point[] LeftTriangle = new Point[5]{
												   new Point(LeftPos, NodeOver.Bounds.Top - 4),
												   new Point(LeftPos, NodeOver.Bounds.Top + 4),
												   new Point(LeftPos + 4, NodeOver.Bounds.Y),
												   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
												   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

			Point[] RightTriangle = new Point[5]{
													new Point(RightPos, NodeOver.Bounds.Top - 4),
													new Point(RightPos, NodeOver.Bounds.Top + 4),
													new Point(RightPos - 4, NodeOver.Bounds.Y),
													new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
													new Point(RightPos, NodeOver.Bounds.Top - 5)};


			g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
			g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
			g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

		}

		private void DrawLeafBottomPlaceholders(TreeNode NodeOver, TreeNode ParentDragDrop)
		{
			Graphics g = this.treeView_Flow.CreateGraphics();

			int NodeOverImageWidth = this.treeView_Flow.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;
			// Once again, we are not dragging to node over, draw the placeholder using the ParentDragDrop bounds
			int LeftPos, RightPos;
			if (ParentDragDrop != null)
				LeftPos = ParentDragDrop.Bounds.Left - (this.treeView_Flow.ImageList.Images[ParentDragDrop.ImageIndex].Size.Width + 8);
			else
				LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
			RightPos = this.treeView_Flow.Width - 4;

			Point[] LeftTriangle = new Point[5]{
												   new Point(LeftPos, NodeOver.Bounds.Bottom - 4),
												   new Point(LeftPos, NodeOver.Bounds.Bottom + 4),
												   new Point(LeftPos + 4, NodeOver.Bounds.Bottom),
												   new Point(LeftPos + 4, NodeOver.Bounds.Bottom - 1),
												   new Point(LeftPos, NodeOver.Bounds.Bottom - 5)};

			Point[] RightTriangle = new Point[5]{
													new Point(RightPos, NodeOver.Bounds.Bottom - 4),
													new Point(RightPos, NodeOver.Bounds.Bottom + 4),
													new Point(RightPos - 4, NodeOver.Bounds.Bottom),
													new Point(RightPos - 4, NodeOver.Bounds.Bottom - 1),
													new Point(RightPos, NodeOver.Bounds.Bottom - 5)};


			g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
			g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
			g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Bottom), new Point(RightPos, NodeOver.Bounds.Bottom));
		}

		private void DrawFolderTopPlaceholders(TreeNode NodeOver)
		{
			Graphics g = this.treeView_Flow.CreateGraphics();
			int NodeOverImageWidth = this.treeView_Flow.ImageList.Images[NodeOver.ImageIndex].Size.Width + 8;

			int LeftPos, RightPos;
			LeftPos = NodeOver.Bounds.Left - NodeOverImageWidth;
			RightPos = this.treeView_Flow.Width - 4;

			Point[] LeftTriangle = new Point[5]{
												   new Point(LeftPos, NodeOver.Bounds.Top - 4),
												   new Point(LeftPos, NodeOver.Bounds.Top + 4),
												   new Point(LeftPos + 4, NodeOver.Bounds.Y),
												   new Point(LeftPos + 4, NodeOver.Bounds.Top - 1),
												   new Point(LeftPos, NodeOver.Bounds.Top - 5)};

			Point[] RightTriangle = new Point[5]{
													new Point(RightPos, NodeOver.Bounds.Top - 4),
													new Point(RightPos, NodeOver.Bounds.Top + 4),
													new Point(RightPos - 4, NodeOver.Bounds.Y),
													new Point(RightPos - 4, NodeOver.Bounds.Top - 1),
													new Point(RightPos, NodeOver.Bounds.Top - 5)};


			g.FillPolygon(System.Drawing.Brushes.Black, LeftTriangle);
			g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
			g.DrawLine(new System.Drawing.Pen(Color.Black, 2), new Point(LeftPos, NodeOver.Bounds.Top), new Point(RightPos, NodeOver.Bounds.Top));

		}
		private void DrawAddToFolderPlaceholder(TreeNode NodeOver)
		{
			Graphics g = this.treeView_Flow.CreateGraphics();
			int RightPos = NodeOver.Bounds.Right + 6;
			Point[] RightTriangle = new Point[5]{
													new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
													new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) + 4),
													new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2)),
													new Point(RightPos - 4, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 1),
													new Point(RightPos, NodeOver.Bounds.Y + (NodeOver.Bounds.Height / 2) - 5)};

			this.Refresh();
			g.FillPolygon(System.Drawing.Brushes.Black, RightTriangle);
		}

		private void SetNewNodeMap(TreeNode tnNode, bool boolBelowNode)
		{
			NewNodeMap.Length = 0;

			if (boolBelowNode)
				NewNodeMap.Insert(0, (int)tnNode.Index + 1);
			else
				NewNodeMap.Insert(0, (int)tnNode.Index);
			TreeNode tnCurNode = tnNode;

			while (tnCurNode.Parent != null)
			{
				tnCurNode = tnCurNode.Parent;

				if (NewNodeMap.Length == 0 && boolBelowNode == true)
				{
					NewNodeMap.Insert(0, (tnCurNode.Index + 1) + "|");
				}
				else
				{
					NewNodeMap.Insert(0, tnCurNode.Index + "|");
				}
			}
		}

		private bool SetMapsEqual()
		{
			if (this.NewNodeMap.ToString() == this.NodeMap)
				return true;
			else
			{
				this.NodeMap = this.NewNodeMap.ToString();
				return false;
			}
		}
		#endregion
	}
}
