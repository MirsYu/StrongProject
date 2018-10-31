using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
namespace StrongProject
{
	public partial class UserControl_configIni : UserControl
	{
		public FileInfo[] tag_FileInfo = null;
		public Work tag_Work;
		public UserControl_configIni()
		{
			InitializeComponent();
		}

		private void UserControl_configIni_Load(object sender, EventArgs e)
		{
			dataGridView1.Location = new Point(0, 0);
			int i = 0;
			dataGridView1.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);
			dataGridView1.Columns[0].Width = dataGridView1.Size.Width;
			string dir = Path.Combine(Application.StartupPath, "set");
			if (Directory.Exists(dir) == false)
			{
				Directory.CreateDirectory(dir);
			}
			DirectoryInfo TheFolder = new DirectoryInfo(dir);
			tag_FileInfo = TheFolder.GetFiles("*.config");
			if (tag_FileInfo == null || tag_FileInfo.Length == 0)
			{
				return;
			}
			dataGridView1.RowCount = 1;
			foreach (FileInfo fo in tag_FileInfo)
			{
				dataGridView1.RowCount = tag_FileInfo.Length;
				dataGridView1[0, i].Value = fo.Name.Substring(0, fo.Name.LastIndexOf(".config"));
				i++;
			}
		}

		private void UserControl_configIni_SizeChanged(object sender, EventArgs e)
		{
			dataGridView1.Location = new Point(0, 0);
			dataGridView1.Size = new Size(this.Size.Width - 2, this.Size.Height - 2);
		}

		private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UserControl_configIni_Load(null, null);
		}

		private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// dataGridView1.rom
			//   dataGridView1.CurrentRow
			// string a = dataGridView1[]; ;
		}

		private void 设置为空跑ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Global.WorkVar.tag_StopState = 1;
			Thread.Sleep(1000);
		}

		private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string var = dataGridView1.CurrentRow.Cells[0].Value.ToString();
			if (var == "set")
			{
				MessageBoxLog.Show("此配置文件不是备份文件，不能删除");
				return;
			}
			else
			{
				if (tag_FileInfo != null && tag_FileInfo.Length > 0)
				{
					string name = tag_FileInfo[0].DirectoryName + "\\" + var + ".config";
					if (MessageBoxLog.Show("是否删除备份配置文件" + name, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
					{
						return;
					}
					File.Delete(name);
					UserControl_configIni_Load(null, null);
				}
			}
		}

		private void 设置为临时系统配置文件ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string var = dataGridView1.CurrentRow.Cells[0].Value.ToString();
			if (tag_FileInfo != null && tag_FileInfo.Length > 0)
			{
				string name = tag_FileInfo[0].DirectoryName + "\\" + var + ".config";
				if (MessageBoxLog.Show("是否设置" + name + "零时运行文件", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
				{
					return;
				}
				tag_Work._Config = Config.Load(name, null) as Config;
				StationManage._Config = tag_Work._Config;
			}
		}

		private void 设置为空跑ToolStripMenuItem_Click_1(object sender, EventArgs e)
		{

		}
	}
}
