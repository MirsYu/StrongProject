using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace StrongProject
{
	public partial class FrmStationMange : Form
	{
		/// <summary>
		/// 工位管理
		/// </summary>
		public Work tag_work;
		public List<CheckBox> tag_portList = new List<CheckBox>();
		public List<CheckBox> tag_NetList = new List<CheckBox>();
		public List<PointAggregate> tag_PointAggregateList;
		public PointAggregate tag_PointAggregate;
		/* /// <summary>
		 /// 左树工位模块
		 /// </summary>
		 public StationModule tag_LeftStationModule;*/
		/* /// <summary>
		 /// 右树工位模块
		 /// </summary>
		 public StationModule tag_RightStationModule;*/
		public FrmStationMange(Work work)
		{
			tag_work = work;
			InitializeComponent();
		}

		private void FrmStationMange_Load(object sender, EventArgs e)
		{
			//  tag_work._Config.arrWorkStation
			trVStation.TopNode.Nodes.Clear();
			int index = 0;
			foreach (StationModule sm in tag_work._Config.arrWorkStation)
			{
				trVStation.TopNode.Nodes.Add(sm.strStationName);

			}
			foreach (StationModule sm in tag_work._Config.arrWorkStation)
			{
				TreeNode tn = new TreeNode(sm.strStationName);
				tn.Tag = sm;
				tn.Name = "Root_" + index;
				treView_station.TopNode.Nodes.Add(tn);
				index++;

			}
			int i = 0;
			groupBox_PORT.Controls.Clear();
			foreach (JSerialPort jsp in tag_work.tag_JSerialPort)
			{

				CheckBox port = new CheckBox();
				port.Text = jsp.tag_PortParameter.tag_name;
				port.Location = new Point(10, i * port.Size.Height + 10);
				groupBox_PORT.Controls.Add(port);
				i++;
				tag_portList.Add(port);
			}
			groupBox_Net.Controls.Clear();
			foreach (SocketClient sc in tag_work.tag_SocketClient)
			{

				CheckBox port = new CheckBox();
				port.Text = sc.tag_IPAdrr.tag_name;
				port.Location = new Point(10, i * port.Size.Height + 10);
				groupBox_Net.Controls.Add(port);
				tag_NetList.Add(port);
				i++;

			}

		}

		private int[] GetPortCount()
		{
			int count = 0;
			foreach (CheckBox sc in tag_portList)
			{

				if (sc.Checked)
				{
					count++;
				}

			}
			int[] ret = new int[count];
			int j = 0;
			int i = 0;
			foreach (CheckBox sc in tag_portList)
			{

				if (sc.Checked)
				{
					ret[i] = j;
					i++;
				}
				j++;

			}
			return ret;
		}
		private int[] GetNetCount()
		{
			int count = 0;
			foreach (CheckBox sc in tag_NetList)
			{

				if (sc.Checked)
				{
					count++;
				}

			}
			int[] ret = new int[count];
			int j = 0;
			int i = 0;
			foreach (CheckBox sc in tag_NetList)
			{

				if (sc.Checked)
				{
					ret[i] = j;
					i++;
				}
				j++;

			}
			return ret;
		}

		private void trVStation_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeView tv = (TreeView)trVStation;
			StationModule sm = StationManage.FindStation(tv.SelectedNode.Text);


		}



		private void 嵌入到工程中ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void 添加CCD九宫格标定点位ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			string strStationName = "CCD_9宫格标定";
			if (tag_work.Config.arrWorkStation.Count > 0)
			{
				foreach (StationModule ms in tag_work.Config.arrWorkStation)
				{
					if (ms.strStationName != "" && ms.strStationName == strStationName)
					{
						{
							MessageBoxLog.Show("已有工位配置");
							return;
						}
					}
				}
			}
			StationModule sm = new StationModule();
			tag_work.Config.arrWorkStation.Add(sm);
			tag_work.Config.arrWorkStation[tag_work.Config.arrWorkStation.Count - 1].strStationName = strStationName;
			PointAggregate pa = new PointAggregate(strStationName, "标定开始");
			sm.arrPoint.Add(pa);
			sm.intUsePointCount++;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					pa = new PointAggregate(strStationName, i + "_" + j + "点位");
					sm.arrPoint.Add(pa);
					sm.intUsePointCount++;
				}
			}

			pa = new PointAggregate(strStationName, "标定结束");
			sm.intUsePointCount++;
			sm.arrPoint.Add(pa);
			tag_work.Config.intUseStationCount++;
			tag_work.Config.Save();
			FrmStationMange_Load(null, null);
		}

		private void 每一步都嵌入代码ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (Global.WorkVar.g_projectDir == "" || !DirIsSubDir(Global.WorkVar.g_projectDir, "Work"))
			{
				MessageBoxLog.Show("请设置工程路径");
				return;
			}

			if (DialogResult.OK == saveFileDialog_Code.ShowDialog())
			{

				string filename = saveFileDialog_Code.FileName;
				StationPattern sp = new StationPattern(trVStation.SelectedNode.Text);
				sp.CodeCreateN(filename, 0, GetPortCount(), GetNetCount());

				string filiNename = Global.WorkVar.g_projectDir + "\\work\\workObjectManage.cs";
				sp.InsertToWorkObjectManage(filiNename, 0, GetPortCount(), GetNetCount());


				filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
				if (sp.InsertTocsproj(filiNename, 0, GetPortCount(), GetNetCount()) != 0)
				{

					filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
					sp.InsertTocsproj(filiNename, 0, GetPortCount(), GetNetCount());
				}

			}
		}

		private void 只收尾两步嵌入代码ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (Global.WorkVar.g_projectDir == "" || !DirIsSubDir(Global.WorkVar.g_projectDir, "Work"))
			{
				MessageBoxLog.Show("请设置工程路径");
				return;
			}

			if (DialogResult.OK == saveFileDialog_Code.ShowDialog())
			{

				string filename = saveFileDialog_Code.FileName;
				StationPattern sp = new StationPattern(trVStation.SelectedNode.Text);
				sp.CodeCreate(filename, 0, GetPortCount(), GetNetCount());


				string filiNename = Global.WorkVar.g_projectDir + "\\work\\workObjectManage.cs";
				sp.InsertToWorkObjectManage(filiNename, 0, GetPortCount(), GetNetCount());


				filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
				if (sp.InsertTocsproj(filiNename, 0, GetPortCount(), GetNetCount()) != 0)
				{

					filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
					sp.InsertTocsproj(filiNename, 0, GetPortCount(), GetNetCount());
				}

			}
		}

		private void 首尾嵌代码中间共用ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (Global.WorkVar.g_projectDir == "" || !DirIsSubDir(Global.WorkVar.g_projectDir, "Work"))
			{
				MessageBoxLog.Show("请设置工程路径");
				return;
			}

			if (DialogResult.OK == saveFileDialog_Code.ShowDialog())
			{

				string filename = saveFileDialog_Code.FileName;
				StationPattern sp = new StationPattern(trVStation.SelectedNode.Text);
				sp.CodeCreateN3(filename, 0, GetPortCount(), GetNetCount());


				string filiNename = Global.WorkVar.g_projectDir + "\\work\\workObjectManage.cs";
				sp.InsertToWorkObjectManage(filiNename, 0, GetPortCount(), GetNetCount());



				filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
				if (sp.InsertTocsproj(filiNename, 0, GetPortCount(), GetNetCount()) != 0)
				{

					filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
					sp.InsertTocsproj(filiNename, 0, GetPortCount(), GetNetCount());
				}

			}
		}

		private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (MessageBoxLog.Show("是否删除?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			StationModule sm = StationManage.FindStation(trVStation.SelectedNode.Text);
			tag_work.Config.arrWorkStation.Remove(sm);
			tag_work.Config.intUseStationCount--;
			tag_work.Config.Save();
			FrmStationMange_Load(null, null);
		}

		private void 添加两个点位ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			string strStationName = "NewStation";
			if (tag_work.Config.arrWorkStation.Count > 0)
			{
				foreach (StationModule ms in tag_work.Config.arrWorkStation)
				{
					if (ms.strStationName != "" && ms.strStationName == strStationName)
					{
						{
							MessageBoxLog.Show("已有工位配置");
							return;
						}
					}
				}
			}
			StationModule sm = new StationModule();
			tag_work.Config.arrWorkStation.Add(sm);
			tag_work.Config.arrWorkStation[tag_work.Config.arrWorkStation.Count - 1].strStationName = strStationName;
			PointAggregate pa = new PointAggregate(strStationName, "工位开始");
			sm.arrPoint.Add(pa);
			sm.intUsePointCount++;
			int pointCount = 0;
			int i = 0;
			try
			{
				pointCount = int.Parse(texB_Count.Text);
			}
			catch (Exception mss)
			{
				UserControl_LogOut.OutLog(mss.Message, 0);
			}
			while (i < pointCount && i < 100)
			{
				i++;
				pa = new PointAggregate(strStationName, "步骤" + i);
				sm.intUsePointCount++;
				sm.arrPoint.Add(pa);

			}

			pa = new PointAggregate(strStationName, "工位结束");
			sm.intUsePointCount++;
			sm.arrPoint.Add(pa);
			tag_work.Config.intUseStationCount++;
			tag_work.Config.Save();
			FrmStationMange_Load(null, null);
		}

		private void trVStation_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			string oldStr = e.Node.Text;
			if (e.Label == null)
			{
				trVStation.SelectedNode.Text = oldStr;
				return;
			}
			string newStr = e.Label.Trim();
			TreeView node = (TreeView)sender;
			StationModule sm = null;
			if (newStr == null || newStr == "")
			{
				trVStation.SelectedNode.Text = oldStr;
				return;
			}
			if (tag_work.Config.arrWorkStation.Count > 0)
			{
				foreach (StationModule ms in tag_work.Config.arrWorkStation)
				{
					if (ms.strStationName != "" && ms.strStationName == newStr)
					{
						e.CancelEdit = true;
						trVStation.SelectedNode.EndEdit(true);
						trVStation.LabelEdit = false;
						//    trVStation.SelectedNode.Text = oldStr;
						//   MessageBoxLog.Show("名称重合");

						return;
					}
				}
			}
			e.CancelEdit = false;
			if (tag_work.Config.arrWorkStation.Count > 0)
			{
				foreach (StationModule ms in tag_work.Config.arrWorkStation)
				{
					if (ms.strStationName != "" && ms.strStationName == oldStr)
					{
						sm = ms;
						break;
					}
				}
			}
			if (sm == null)
			{
				trVStation.SelectedNode.EndEdit(false);
				return;
			}
			sm.strStationName = newStr;
			foreach (PointAggregate pa in sm.arrPoint)
			{
				pa.strStationName = newStr;
			}

			trVStation.SelectedNode.EndEdit(false);
			tag_work.Config.Save();
		}
		public string getUpDir2(string dir)
		{
			string[] temp = dir.Split("\\".ToCharArray());
			string ret = null;
			for (int i = 0; i < temp.Length - 1; i++)
			{
				ret += temp[i];
				ret += "\\";
			}
			return ret;
		}
		public bool DirIsSubDir(string dir, string subdir)
		{
			return System.IO.Directory.Exists(dir + "\\" + subdir);


		}
		private void 修改名称ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			trVStation.LabelEdit = true;
			trVStation.SelectedNode.BeginEdit();
		}

		private void 每一步都嵌入代码ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (Global.WorkVar.g_projectDir == "" || !DirIsSubDir(Global.WorkVar.g_projectDir, "Work"))
			{
				MessageBoxLog.Show("请设置工程路径");
				return;
			}

			if (DialogResult.OK == saveFileDialog_Code.ShowDialog())
			{

				string filename = saveFileDialog_Code.FileName;
				StationPattern sp = new StationPattern(trVStation.SelectedNode.Text);
				sp.CodeCreateN(filename, 1, GetPortCount(), GetNetCount());


				string filiNename = Global.WorkVar.g_projectDir + "\\work\\workObjectManage.cs";
				sp.InsertToWorkObjectManage(filiNename, 1, GetPortCount(), GetNetCount());

				filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
				if (sp.InsertTocsproj(filiNename, 1, GetPortCount(), GetNetCount()) != 0)
				{

					filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
					sp.InsertTocsproj(filiNename, 1, GetPortCount(), GetNetCount());
				}

			}
		}

		private void 只首尾两步嵌入代码ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (Global.WorkVar.g_projectDir == "" || !DirIsSubDir(Global.WorkVar.g_projectDir, "Work"))
			{
				MessageBoxLog.Show("请设置工程路径");
				return;
			}

			if (DialogResult.OK == saveFileDialog_Code.ShowDialog())
			{

				string filename = saveFileDialog_Code.FileName;
				StationPattern sp = new StationPattern(trVStation.SelectedNode.Text);
				sp.CodeCreate(filename, 1, GetPortCount(), GetNetCount());

				string filiNename = Global.WorkVar.g_projectDir + "\\work\\workObjectManage.cs";
				sp.InsertToWorkObjectManage(filiNename, 1, GetPortCount(), GetNetCount());


				filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
				if (sp.InsertTocsproj(filiNename, 1, GetPortCount(), GetNetCount()) != 0)
				{

					filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
					sp.InsertTocsproj(filiNename, 1, GetPortCount(), GetNetCount());
				}

			}
		}

		private void 首尾嵌代码中间共用ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (Global.WorkVar.g_projectDir == "" || !DirIsSubDir(Global.WorkVar.g_projectDir, "Work"))
			{
				MessageBoxLog.Show("请设置工程路径");
				return;
			}

			if (DialogResult.OK == saveFileDialog_Code.ShowDialog())
			{

				string filename = saveFileDialog_Code.FileName;
				StationPattern sp = new StationPattern(trVStation.SelectedNode.Text);
				sp.CodeCreateN3(filename, 1, GetPortCount(), GetNetCount());

				string filiNename = Global.WorkVar.g_projectDir + "\\work\\workObjectManage.cs";
				sp.InsertToWorkObjectManage(filiNename, 1, GetPortCount(), GetNetCount());


				filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
				if (sp.InsertTocsproj(filiNename, 1, GetPortCount(), GetNetCount()) != 0)
				{

					filiNename = Global.WorkVar.g_projectDir + "\\StrongProject.csproj";
					sp.InsertTocsproj(filiNename, 1, GetPortCount(), GetNetCount());
				}

			}
		}

		private void 删除所有ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (MessageBoxLog.Show("是否删除?", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			StationModule sm = StationManage.FindStation(trVStation.SelectedNode.Text);
			tag_work.Config.arrWorkStation.Clear();
			tag_work.Config.intUseStationCount = 0;
			tag_work.Config.Save();
			FrmStationMange_Load(null, null);
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			TextBox t = (TextBox)sender;
			Global.WorkVar.g_projectDir = t.Text;
		}


		private void 更新左列表ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			trVStation_NodeMouseDoubleClick(null, null);
		}

		private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
		{


			if (treView_station.SelectedNode == null || treView_station.SelectedNode.Name == "Root")
			{
				return;
			}
			else
			{
				if (treView_station.SelectedNode.Parent.Name == "Root")
				{

				}
				else
				{
					tag_PointAggregate = (PointAggregate)treView_station.SelectedNode.Tag;

					tag_PointAggregateList = new List<PointAggregate>();

					if (tag_PointAggregate != null)
					{
						tag_PointAggregateList.Add(tag_PointAggregate);
					}

				}
			}
		}

		private void treView_station_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			int index = 0;
			if (treView_station.SelectedNode == null || treView_station.SelectedNode.Name == "Root")
			{
				return;
			}
			else
			{
				if (treView_station.SelectedNode.Parent.Name == "Root")
				{
					treView_station.SelectedNode.Nodes.Clear();
					StationModule lsfm = (StationModule)treView_station.SelectedNode.Tag; ;
					PointAggregate pa = StationManage.FindPoint(lsfm, treView_station.SelectedNode.Text);
					if (lsfm == null || lsfm.arrPoint == null)
						return;
					foreach (object pp in lsfm.arrPoint)
					{
						PointAggregate p = (PointAggregate)pp;
						TreeNode tn = new TreeNode(p.strName);
						tn.Tag = p;
						tn.Name = treView_station.SelectedNode.Name + "_" + index;






						TreeNode tnUp = new TreeNode("在本点之前执行");
						tnUp.Tag = p;
						tn.Nodes.Add(tnUp);

						TreeNode tnDown = new TreeNode("在本点之后执行");
						tnDown.Tag = p;
						tn.Nodes.Add(tnDown);

						treView_station.SelectedNode.Nodes.Add(tn);
						index++;
					}
				}
				else
				{

					if (treView_station.SelectedNode.Text == "在本点之前执行")
					{
						treView_station.SelectedNode.Nodes.Clear();

						PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Tag;
						if (pa == null || pa.tag_BeginPointAggregateList == null)
							return;
						foreach (object pp in pa.tag_BeginPointAggregateList)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Name + "_" + index;
							treView_station.SelectedNode.Nodes.Add(tn);





							index++;
						}
					}
					else
						if (treView_station.SelectedNode.Text == "在本点之后执行")
					{
						treView_station.SelectedNode.Nodes.Clear();

						PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Tag;
						if (pa == null || pa.tag_EndPointAggregateList == null)
							return;
						foreach (object pp in pa.tag_EndPointAggregateList)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Name + "_" + index;
							treView_station.SelectedNode.Nodes.Add(tn);





							index++;
						}
					}
				}
			}
		}

		private void 粘贴到本店前ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int index = 0;
			if (treView_station.SelectedNode == null || treView_station.SelectedNode.Name == "Root")
			{
				return;
			}
			else
			{
				if (treView_station.SelectedNode.Parent.Name == "Root")
				{

				}
				else
				{

					if (treView_station.SelectedNode.Parent.Parent.Name == "Root")
					{
						StationModule sm = (StationModule)treView_station.SelectedNode.Parent.Tag;

						if (tag_PointAggregateList != null)
						{
							int n = 0;
							foreach (PointAggregate ppp in tag_PointAggregateList)
							{
								sm.arrPoint.Insert(treView_station.SelectedNode.Index + n, ppp);
								n++;
							}
						}
						treView_station.SelectedNode = treView_station.SelectedNode.Parent;
						treView_station.SelectedNode.Nodes.Clear();

						foreach (object pp in sm.arrPoint)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;

							TreeNode tnUp = new TreeNode("在本点之前执行");
							tnUp.Tag = p;
							tn.Nodes.Add(tnUp);

							TreeNode tnDown = new TreeNode("在本点之后执行");
							tnDown.Tag = p;
							tn.Nodes.Add(tnDown);

							treView_station.SelectedNode.Nodes.Add(tn);
							index++;
						}
					}
					else
						if (treView_station.SelectedNode.Parent.Text == "在本点之前执行")
					{
						PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Parent.Tag;


						if (tag_PointAggregateList != null)
						{
							int n = 0;
							foreach (PointAggregate ppp in tag_PointAggregateList)
							{
								pa.tag_BeginPointAggregateList.Insert(treView_station.SelectedNode.Index + n, ppp);
								n++;
							}
						}

						treView_station.SelectedNode = treView_station.SelectedNode.Parent;
						treView_station.SelectedNode.Nodes.Clear();

						foreach (object pp in pa.tag_BeginPointAggregateList)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
							treView_station.SelectedNode.Nodes.Add(tn);
							index++;
						}
					}
					else
							if (treView_station.SelectedNode.Parent.Text == "在本点之后执行")
					{
						PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Parent.Tag;



						if (tag_PointAggregateList != null)
						{
							int n = 0;
							foreach (PointAggregate ppp in tag_PointAggregateList)
							{
								pa.tag_EndPointAggregateList.Insert(treView_station.SelectedNode.Index + n, ppp);
								n++;
							}
						}

						treView_station.SelectedNode = treView_station.SelectedNode.Parent;
						treView_station.SelectedNode.Nodes.Clear();

						foreach (object pp in pa.tag_EndPointAggregateList)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
							treView_station.SelectedNode.Nodes.Add(tn);
							index++;
						}
					}
				}
			}
		}

		private void 粘贴到本点后ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int index = 0;
			if (treView_station.SelectedNode == null || treView_station.SelectedNode.Name == "Root")
			{
				return;
			}
			else
			{
				if (treView_station.SelectedNode.Parent.Name == "Root")
				{

				}
				else
				{

					if (treView_station.SelectedNode.Parent.Parent.Name == "Root")
					{
						StationModule sm = (StationModule)treView_station.SelectedNode.Parent.Tag;



						if (tag_PointAggregateList != null)
						{
							int n = 0;
							foreach (PointAggregate ppp in tag_PointAggregateList)
							{
								sm.arrPoint.Insert(treView_station.SelectedNode.Index + n, ppp);
								n++;
							}
						}

						treView_station.SelectedNode = treView_station.SelectedNode.Parent;
						treView_station.SelectedNode.Nodes.Clear();

						foreach (object pp in sm.arrPoint)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;

							TreeNode tnUp = new TreeNode("在本点之前执行");
							tnUp.Tag = p;
							tn.Nodes.Add(tnUp);

							TreeNode tnDown = new TreeNode("在本点之后执行");
							tnDown.Tag = p;
							tn.Nodes.Add(tnDown);

							treView_station.SelectedNode.Nodes.Add(tn);
							index++;
						}
					}
					else
						if (treView_station.SelectedNode.Parent.Text == "在本点之后执行")

					{
						PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Parent.Tag;


						if (tag_PointAggregateList != null)
						{
							int n = 0;
							foreach (PointAggregate ppp in tag_PointAggregateList)
							{
								pa.tag_EndPointAggregateList.Insert(treView_station.SelectedNode.Index + 1 + n, ppp);
								n++;
							}
						}




						treView_station.SelectedNode = treView_station.SelectedNode.Parent;
						treView_station.SelectedNode.Nodes.Clear();

						foreach (object pp in pa.tag_EndPointAggregateList)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
							treView_station.SelectedNode.Nodes.Add(tn);
							index++;
						}
					}
					else
							 if (treView_station.SelectedNode.Parent.Text == "在本点之前执行")

					{
						PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Parent.Tag;


						if (tag_PointAggregateList != null)
						{
							int n = 0;
							foreach (PointAggregate ppp in tag_PointAggregateList)
							{
								pa.tag_EndPointAggregateList.Insert(treView_station.SelectedNode.Index + 1 + n, ppp);
								n++;
							}
						}

						treView_station.SelectedNode = treView_station.SelectedNode.Parent;
						treView_station.SelectedNode.Nodes.Clear();

						foreach (object pp in pa.tag_EndPointAggregateList)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
							treView_station.SelectedNode.Nodes.Add(tn);
							index++;
						}
					}
				}
			}
		}

		private void 粘贴到本点的列表中ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int index = 0;
			if (treView_station.SelectedNode.Text == "在本点之前执行")
			{
				PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Tag;

				if (pa.tag_BeginPointAggregateList == null)
				{
					pa.tag_BeginPointAggregateList = new List<object>();
				}

				if (tag_PointAggregateList != null)
				{
					int n = 0;
					pa.tag_BeginPointAggregateListIsEnable = true;
					foreach (PointAggregate ppp in tag_PointAggregateList)
					{
						pa.tag_BeginPointAggregateList.Insert(0 + n, ppp);
						n++;
					}
				}



				treView_station.SelectedNode.Nodes.Clear();

				foreach (object pp in pa.tag_BeginPointAggregateList)
				{
					PointAggregate p = (PointAggregate)pp;
					TreeNode tn = new TreeNode(p.strName);
					tn.Tag = p;
					tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
					treView_station.SelectedNode.Nodes.Add(tn);
					index++;
				}
				return;
			}
			if (treView_station.SelectedNode.Text == "在本点之后执行")
			{
				PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Tag;
				if (pa.tag_EndPointAggregateList == null)
				{
					pa.tag_EndPointAggregateListIsEnable = true;
					pa.tag_EndPointAggregateList = new List<object>();
				}

				if (tag_PointAggregateList != null)
				{
					int n = 0;
					pa.tag_EndPointAggregateListIsEnable = true;
					foreach (PointAggregate ppp in tag_PointAggregateList)
					{
						pa.tag_EndPointAggregateList.Insert(0 + n, ppp);
						n++;
					}
				}

				treView_station.SelectedNode.Nodes.Clear();

				foreach (object pp in pa.tag_EndPointAggregateList)
				{
					PointAggregate p = (PointAggregate)pp;
					TreeNode tn = new TreeNode(p.strName);
					tn.Tag = p;
					tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
					treView_station.SelectedNode.Nodes.Add(tn);
					index++;
				}
				return;
			}
		}

		private void 移除本点ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			int index = 0;
			if (MessageBoxLog.Show("是否移除" + treView_station.SelectedNode.Text, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			if (treView_station.SelectedNode.Text == "在本点之前执行")
			{
				PointAggregate pppp = (PointAggregate)treView_station.SelectedNode.Parent.Tag;
				if (pppp.tag_BeginPointAggregateList != null)
				{
					pppp.tag_BeginPointAggregateList.Clear();
					treView_station.SelectedNode.Nodes.Clear();
				}
				return;
			}
			else

			 if (treView_station.SelectedNode.Text == "在本点之后执行")
			{
				PointAggregate pppp = (PointAggregate)treView_station.SelectedNode.Parent.Tag;
				if (pppp.tag_EndPointAggregateList != null)
				{
					pppp.tag_EndPointAggregateList.Clear();
					treView_station.SelectedNode.Nodes.Clear();
				}
				return;
			}


			if (treView_station.SelectedNode == null || treView_station.SelectedNode.Name == "Root")
			{
				return;
			}
			else
			{
				if (treView_station.SelectedNode.Parent.Name == "Root")
				{

				}
				else
				{

					if (treView_station.SelectedNode.Parent.Parent.Name == "Root")
					{
						StationModule sm = (StationModule)treView_station.SelectedNode.Parent.Tag;
						sm.arrPoint.RemoveAt(treView_station.SelectedNode.Index);
						if (sm.arrPoint == null)
							return;
						treView_station.SelectedNode = treView_station.SelectedNode.Parent;
						treView_station.SelectedNode.Nodes.Clear();

						foreach (object pp in sm.arrPoint)
						{
							PointAggregate p = (PointAggregate)pp;
							TreeNode tn = new TreeNode(p.strName);
							tn.Tag = p;
							tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
							treView_station.SelectedNode.Nodes.Add(tn);
							index++;
						}
					}
					else
					{
						PointAggregate pa = (PointAggregate)treView_station.SelectedNode.Parent.Tag;
						if (treView_station.SelectedNode.Parent.Text == "在本点之前执行")
						{
							if (pa.tag_BeginPointAggregateList == null)
								return;
							pa.tag_BeginPointAggregateList.RemoveAt(treView_station.SelectedNode.Index);


							treView_station.SelectedNode = treView_station.SelectedNode.Parent;
							treView_station.SelectedNode.Nodes.Clear();

							foreach (object pp in pa.tag_BeginPointAggregateList)
							{
								PointAggregate p = (PointAggregate)pp;
								TreeNode tn = new TreeNode(p.strName);
								tn.Tag = p;
								tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
								treView_station.SelectedNode.Nodes.Add(tn);
								index++;
							}
						}
						else
							if (treView_station.SelectedNode.Parent.Text == "在本点之后执行")
						{
							if (pa.tag_EndPointAggregateList == null)
								return;
							pa.tag_EndPointAggregateList.RemoveAt(treView_station.SelectedNode.Index);


							treView_station.SelectedNode = treView_station.SelectedNode.Parent;
							treView_station.SelectedNode.Nodes.Clear();

							foreach (object pp in pa.tag_EndPointAggregateList)
							{
								PointAggregate p = (PointAggregate)pp;
								TreeNode tn = new TreeNode(p.strName);
								tn.Tag = p;
								tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
								treView_station.SelectedNode.Nodes.Add(tn);
								index++;
							}

						}


					}
				}
			}
		}

		private void 粘贴到本点的列表中后ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void 粘贴到本点的列表中前ToolStripMenuItem_Click(object sender, EventArgs e)
		{


		}

		private void 粘贴到本点的列表中后ToolStripMenuItem1_Click(object sender, EventArgs e)
		{
			int index = 0;

		}

		private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treView_station.SelectedNode == null || treView_station.SelectedNode.Name == "Root")
			{
				return;
			}
			else
			{
				if (treView_station.SelectedNode.Parent.Name == "Root")
				{

				}
				else
				{
					if (tag_PointAggregateList == null)
						tag_PointAggregateList = new List<PointAggregate>();
					tag_PointAggregateList.Clear();
					treeView1_Copy.TopNode.Nodes.Clear();


				}
			}

		}

		private void 粘贴到列表中ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (treView_station.SelectedNode == null || treView_station.SelectedNode.Name == "Root")
			{
				return;
			}
			else
			{
				if (treView_station.SelectedNode.Parent.Name == "Root")
				{

				}
				else
				{
					if (tag_PointAggregateList == null)
						tag_PointAggregateList = new List<PointAggregate>();
					tag_PointAggregateList.Add((PointAggregate)treView_station.SelectedNode.Tag);
					treeView1_Copy.TopNode.Nodes.Clear();
					foreach (PointAggregate pp in tag_PointAggregateList)
					{
						treeView1_Copy.TopNode.Nodes.Add(pp.strName);
					}

				}
			}
		}





#region 拖拽功能

		//拖拽的点
		private Point Position = new Point(0, 0);


		private void treView_station_ItemDrag(object sender, ItemDragEventArgs e)
		{
			//开始拖拽，设定拖拽效果。传递的参数e.Item项目
			//数据data=e.item,拖拽的最终效果为move
			DoDragDrop(e.Item, DragDropEffects.Move);
		}

		private void treView_station_DragEnter(object sender, DragEventArgs e)
		{
			//进入拖拽区间发生,判断是否可以转换成指定的格式来决定是否能够进入此区域。
			if (e.Data.GetDataPresent(typeof(TreeNode)))//是否是真
			{
				e.Effect = DragDropEffects.Move;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void treView_station_DragDrop(object sender, DragEventArgs e)
		{
			//拖拽结束，Drop放下时执行
			TreeNode myNode = null;
			if (e.Data.GetDataPresent(typeof(TreeNode)))
			{
				myNode = (TreeNode)(e.Data.GetData(typeof(TreeNode)));//拖拽源节点dragNode
			}
			else
			{
				MessageBox.Show("errror!");
			}

			///处理拖拽
			Position.X = e.X;
			Position.Y = e.Y;
			Position = treView_station.PointToClient(Position);

			//取得拖拽点的数据
			TreeNode dropNode = this.treView_station.GetNodeAt(Position);

			//1.// 1.目标节点不是空。
			//2.目标节点不是被拖拽接点的子节点。
			//3.目标节点不是被拖拽节点本身

			if (dropNode != null
				//&& dropNode.Parent != myNode.Parent
				&& dropNode != myNode)
			{
				TreeNode dragNode = myNode;
				myNode.Remove();
				dropNode.Nodes.Add(dragNode);

				#region tete
				int index = 0;

				StationModule sm = (StationModule)treView_station.SelectedNode.Parent.Tag;
				sm.arrPoint.RemoveAt(treView_station.SelectedNode.Index);
				if (sm.arrPoint == null)
					return;

				foreach (object pp in sm.arrPoint)
				{
					PointAggregate p = (PointAggregate)pp;
					TreeNode tn = new TreeNode(p.strName);
					tn.Tag = p;
					tn.Name = treView_station.SelectedNode.Parent.Name + "_" + index;
					index++;
				}
				#endregion
			}

			// 如果目标节点不存在，即拖拽的位置不存在节点，那么就将被拖拽节点放在根节点之下
			if (dropNode == null)
			{
				TreeNode DragNode = myNode;
				myNode.Remove();
				treView_station.Nodes.Add(DragNode);
			}
		}
#endregion
	}
}
