using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class FrmInIoList : Form
	{
		/// <summary>
		/// 
		/// </summary>
		public PointAggregate tag_PointModule;
		/// <summary>
		/// 
		/// </summary>
		public OutIOParameterPoint tag_OutIo;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="_PoitModule"></param>
		public FrmInIoList(PointAggregate _PoitModule)
		{
			tag_PointModule = _PoitModule;
			if (tag_PointModule.tag_AxisSafeManage == null)
			{
				tag_PointModule.tag_AxisSafeManage = new AxisSafeManage(_PoitModule);
			}
			if (tag_PointModule.tag_AxisSafeManage.tag_InIoList == null)
			{
				tag_PointModule.tag_AxisSafeManage.tag_InIoList = new List<OutIOParameterPoint>();
				tag_PointModule.tag_OutIo = new OutIOParameterPoint();
				tag_PointModule.tag_AxisSafeManage.tag_InIoList.Add(tag_PointModule.tag_OutIo);
			}
			if (tag_PointModule.tag_AxisSafeManage.tag_InIoList.Count == 0)
			{
				tag_PointModule.tag_OutIo = new OutIOParameterPoint();
				tag_PointModule.tag_AxisSafeManage.tag_InIoList.Add(tag_PointModule.tag_OutIo);
			}
			if (tag_PointModule.tag_AxisSafeManage.tag_InIoList.Count > 0)
			{
				tag_OutIo = tag_PointModule.tag_AxisSafeManage.tag_InIoList[0];

			}
			InitializeComponent();
		}
		private void Tree_Load(object sender, EventArgs e)
		{
			treeView_IO.TopNode.Nodes.Clear();
			foreach (OutIOParameterPoint io in tag_PointModule.tag_AxisSafeManage.tag_InIoList)
			{
				if (io.tag_name == null)
				{
					io.tag_name = "io输入输出控制";
				}
				if (io.tag_name != null)
				{
					treeView_IO.TopNode.Nodes.Add(io.tag_name);
					continue;
				}
			}
			treeView_IO.TopNode.Text = tag_PointModule.strName + "->" + tag_OutIo.tag_name;
		}
		private void FrmInIoList_Load(object sender, EventArgs e)
		{
			if (sender != null)
				Tree_Load(sender, e);
			comboBox_Stat.Items.Clear();
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				comboBox_Stat.Items.Add(sm.strStationName);
			}
			comboBox_Stat.Items.Add("ALL");
			string strStationName = null;
			if (tag_OutIo != null)
			{
				strStationName = tag_OutIo.tag_IniO1.tag_name;
				if (strStationName == null)
				{
					strStationName = tag_OutIo.tag_InOut1.tag_name;
				}
				if (strStationName == null)
				{
					strStationName = tag_OutIo.tag_InOut2.tag_name;
				}
				if (strStationName == null)
				{
					strStationName = tag_PointModule.strStationName;
				}
			}
			listBox1.Items.Clear();
			listBox2.Items.Clear();
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{
				if (sm.strStationName == strStationName)
				{
					foreach (IOParameter ioP in sm.arrOutputIo)
					{
						listBox1.Items.Add(ioP.StrIoName);
					}
					foreach (IOParameter ioP in sm.arrInputIo)
					{
						listBox2.Items.Add(ioP.StrIoName);
					}
				}
			}


			if (tag_OutIo != null && tag_OutIo.tag_InOut1 != null)
			{

				ucl_InIoName1.show(tag_OutIo.tag_InOut1, 1);

			}
			else
			{
				ucl_InIoName1.show(null, 1);
			}
			if (tag_OutIo != null && tag_OutIo.tag_InOut2 != null)
			{
				ucl_InIoName2.show(tag_OutIo.tag_InOut2, 1);
			}
			if (tag_OutIo != null && tag_OutIo.tag_IniO1 != null)
			{
				ucl_InIo1.show(tag_OutIo.tag_IniO1);
			}
			else
			{
				ucl_InIo1.show(null);
			}
			if (tag_OutIo != null && tag_OutIo.tag_IniO2 != null)
			{
				ucl_InIo2.show(tag_OutIo.tag_IniO2);
			}
			else
			{
				ucl_InIo2.show(null);
			}
			if (tag_PointModule.tag_AxisSafeManage == null)
			{
				checkBox_andCheck.Checked = true;
			}
			else
			{
				checkBox_andCheck.Checked = tag_PointModule.tag_AxisSafeManage.tag_isAndCheck;
			}
			this.Text = tag_PointModule.strName;
		}

		private void button_Add_Click(object sender, EventArgs e)
		{
			try
			{
				if (tag_OutIo.tag_InOut1 == null)
				{
					tag_OutIo.tag_InOut1 = new InIOParameterPoint();
				}
				tag_OutIo.tag_InOut1.tag_IOName = listBox1.SelectedItem.ToString(); ;
				if (comboBox_Stat.SelectedItem != null)
					tag_OutIo.tag_InOut1.tag_name = comboBox_Stat.SelectedItem.ToString();
				ucl_InIoName1.show(tag_OutIo.tag_InOut1, 1);
			}
			catch
			{ }
		}

		private void button1_Click(object sender, EventArgs e)
		{

			if (tag_OutIo.tag_InOut2 == null)
			{

				tag_OutIo.tag_InOut2 = new InIOParameterPoint();
			}
			if (listBox1.SelectedItem != null)
				tag_OutIo.tag_InOut2.tag_IOName = listBox1.SelectedItem.ToString(); ;
			if (comboBox_Stat.SelectedItem != null)
				tag_OutIo.tag_InOut2.tag_name = comboBox_Stat.SelectedItem.ToString();
			ucl_InIoName2.show(tag_OutIo.tag_InOut2, 2);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			try
			{

				if (tag_OutIo.tag_IniO1 == null)
				{
					tag_OutIo.tag_IniO1 = new InIOParameterPoint();
				}
				tag_OutIo.tag_IniO1.tag_IOName = listBox2.SelectedItem.ToString();
				if (comboBox_Stat.SelectedItem != null)
					tag_OutIo.tag_IniO1.tag_name = comboBox_Stat.SelectedItem.ToString();
				ucl_InIo1.show(tag_OutIo.tag_IniO1);
			}
			catch
			{

			}
		}

		private void button2_Click(object sender, EventArgs e)
		{


		}

		private void comboBox_Stat_SelectedIndexChanged(object sender, EventArgs e)
		{
			listBox1.Items.Clear();
			listBox2.Items.Clear();
			foreach (StationModule sm in StationManage._Config.arrWorkStation)
			{

				if (sm.strStationName == comboBox_Stat.SelectedItem.ToString() || comboBox_Stat.SelectedItem.ToString() == "ALL")
				{
					foreach (IOParameter ioP in sm.arrOutputIo)
					{
						listBox1.Items.Add(ioP.StrIoName);
					}
					foreach (IOParameter ioP in sm.arrInputIo)
					{
						listBox2.Items.Add(ioP.StrIoName);
					}
					if (comboBox_Stat.SelectedItem.ToString() != "ALL")
					{
						break;
					}
				}
			}

		}
		private void button4_AddIn_Click(object sender, EventArgs e)
		{


			tag_OutIo.tag_InOut1.tag_IOName = listBox1.SelectedItem.ToString(); ;
			tag_OutIo.tag_InOut1.tag_name = comboBox_Stat.SelectedItem.ToString();
			ucl_InIoName1.show(tag_OutIo.tag_InOut1, 1);
		}

		private void button2_Click_1(object sender, EventArgs e)
		{
			try
			{

				if (tag_OutIo.tag_IniO2 == null)
				{
					tag_OutIo.tag_IniO2 = new InIOParameterPoint();
				}
				tag_OutIo.tag_IniO2.tag_IOName = listBox2.SelectedItem.ToString();
				if (comboBox_Stat.SelectedItem != null)
					tag_OutIo.tag_IniO2.tag_name = comboBox_Stat.SelectedItem.ToString();
				ucl_InIo2.show(tag_OutIo.tag_IniO2);
			}
			catch
			{ }
		}

		private void button2_ADD_Out_Click(object sender, EventArgs e)
		{

		}

		private void 添加ToolStripMenuItem_Click(object sender, EventArgs e)
		{

			tag_PointModule.tag_OutIo = new OutIOParameterPoint();
			tag_PointModule.tag_OutIo.tag_name = "io输入输出控制" + tag_PointModule.tag_AxisSafeManage.tag_InIoList.Count;
			tag_PointModule.tag_AxisSafeManage.tag_InIoList.Add(tag_PointModule.tag_OutIo);
			tag_OutIo = tag_PointModule.tag_OutIo;
			Tree_Load(null, null);
		}

		private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			treeView_IO.LabelEdit = true;
			treeView_IO.SelectedNode.BeginEdit();
		}

		private void treeView_IO_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		{
			string oldStr = e.Node.Text;
			if (e.Label == null)
			{
				treeView_IO.SelectedNode.Text = oldStr;
				return;
			}
			string newStr = e.Label.Trim();
			TreeView node = (TreeView)sender;

			if (newStr == null || newStr == "")
			{
				treeView_IO.SelectedNode.Text = oldStr;
				return;
			}
			tag_OutIo.tag_name = newStr;

			e.CancelEdit = false;


			treeView_IO.SelectedNode.EndEdit(false);

		}

		private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				for (int i = 0; i < tag_PointModule.tag_AxisSafeManage.tag_InIoList.Count; i++)
				{
					if (tag_PointModule.tag_AxisSafeManage.tag_InIoList[i].tag_name == treeView_IO.SelectedNode.Text)
					{
						tag_PointModule.tag_AxisSafeManage.tag_InIoList.RemoveAt(i);
					}
				}

				Tree_Load(null, null);
			}
			catch (Exception mess)
			{
				UserControl_LogOut.OutLog(mess.Message, 0);
			}
		}

		private void treeView_IO_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			TreeView tv = (TreeView)sender;

			for (int i = 0; i < tag_PointModule.tag_AxisSafeManage.tag_InIoList.Count; i++)
			{
				if (treeView_IO.SelectedNode != null && tag_PointModule.tag_AxisSafeManage.tag_InIoList[i].tag_name == treeView_IO.SelectedNode.Text)
				{
					// tag_PointModule.tag_AxisSafeManage.tag_InIoList.RemoveAt(i);
					tag_OutIo = tag_PointModule.tag_AxisSafeManage.tag_InIoList[i];
					break;
				}
			}
			FrmInIoList_Load(null, null);
		}

		private void checkBox_andCheck_CheckedChanged(object sender, EventArgs e)
		{
			tag_PointModule.tag_AxisSafeManage.tag_isAndCheck = checkBox_andCheck.Checked;
		}
	}
}
