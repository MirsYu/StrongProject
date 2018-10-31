using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class UserControl_ShowAllPoint : UserControl
	{
		public StationModule tag_StationModule;
		public bool tag_statioOpen;
		public Work tag_Work;
		public List<PointVlaue> tag_PointVlaueList = new List<PointVlaue>();

		public List<Label> tag_LabelAxisTitleList = new List<Label>();


		public List<Button> tag_ButtonPageList = new List<Button>();
		/// <summary>
		/// 总页数
		/// </summary>
		public int tag_pageTotal = 0;
		public int tag_page_index = 0;
		public int tag_page_Count = 15;
		public UserControl_ShowAllPoint()
		{

			InitializeComponent();
			for (int i = 0; i < tag_page_Count; i++)
			{
				PointVlaue pvale = new PointVlaue(null, null, 0, null);
				pvale.Location = new Point(0, 20 + i * pvale.Height + i * 2);
				tag_PointVlaueList.Add(pvale);
				pvale.Visible = false;
				plpointMessage.Controls.Add(pvale);
			}
			for (int i = 0; i < 100; i++)
			{
				Button pvale = new Button();
				pvale.Size = new Size(40, 20);
				pvale.Text = i.ToString();
				pvale.Location = new Point(i * pvale.Width + i * 5, this.Size.Height - 60);
				pvale.Visible = false;
				pvale.Click += new System.EventHandler(this.Button_Page_Click);

				tag_ButtonPageList.Add(pvale);
				CBpointMessage.Controls.Add(pvale);
			}
			for (int i = 0; i < 40; i++)
			{
				Label pvale = new Label();
				pvale.Size = new Size(40, 20);
				pvale.Location = new Point(245 + i * pvale.Width + i * 5, 0);
				pvale.Visible = false;
				tag_LabelAxisTitleList.Add(pvale);
				plpointMessage.Controls.Add(pvale);
			}

		}
		private void Button_Page_Click(object sender, EventArgs e)
		{
			Button pvale = (Button)sender;
			tag_page_index = Int32.Parse(pvale.Text);
			Show();
		}
		public int getPageTotal()
		{
			int m = 0;
			int n = 0;
			for (int i = 0; i < tag_StationModule.arrPoint.Count; i++)
			{

				if (tag_statioOpen == true && tag_StationModule.arrPoint[i].tag_BeginPointAggregateList != null && tag_StationModule.arrPoint[i].tag_BeginPointAggregateList.Count > 0)
				{
					for (n = 0; n < tag_StationModule.arrPoint[i].tag_BeginPointAggregateList.Count; n++)
					{
						m++;
					}
				}

				m++;
				if (tag_statioOpen == true && tag_StationModule.arrPoint[i].tag_EndPointAggregateList != null && tag_StationModule.arrPoint[i].tag_EndPointAggregateList.Count > 0)
				{

					for (n = 0; n < tag_StationModule.arrPoint[i].tag_EndPointAggregateList.Count; n++)
					{
						m++;
					}
				}

			}
			if (m < tag_page_Count)
			{
				tag_pageTotal = 1;
			}
			else
			{
				if (m % tag_page_Count == 0)
				{
					tag_pageTotal = m / tag_page_Count;
				}
				else
				{
					tag_pageTotal = m / tag_page_Count + 1;
				}
			}
			return tag_pageTotal;
		}


		private void AddClick(object sender, EventArgs e)
		{
			PointAggregate pvale = (PointAggregate)sender;
			int j = 0;
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (MessageBoxLog.Show("是否添加", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			tag_StationModule.intUsePointCount = tag_StationModule.arrPoint.Count;
			for (int i = 0; i < tag_StationModule.intUsePointCount; i++)
			{
				if (pvale == tag_StationModule.arrPoint[i])
				{
					tag_StationModule.arrPoint.Insert(i + 1, new PointAggregate(pvale.strStationName, "p1"));
					tag_StationModule.intUsePointCount++;
					break;
				}
			}
			Show();

		}
		private void EnterFun_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < tag_PointVlaueList.Count; i++)
			{
				if (tag_PointVlaueList[i] == sender)
				{
					tag_StationModule.tag_stepId = tag_page_index * tag_page_Count + i;
					return;
				}
			}
		}
		private void Delete_Click(object sender, EventArgs e)
		{
			PointAggregate pvale = (PointAggregate)sender;
			int j = 0;
			if (Global.CConst.UserLevel != Global.CConst.USER_SUPERADMIN)
			{
				MessageBoxLog.Show("无权限");
				return;
			}
			if (MessageBoxLog.Show("是否删除", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
			{
				return;
			}
			tag_StationModule.intUsePointCount = tag_StationModule.arrPoint.Count;
			for (int i = 0; i < tag_StationModule.intUsePointCount; i++)
			{
				if (pvale == tag_StationModule.arrPoint[i])
				{
					tag_StationModule.arrPoint.Remove(pvale);
					tag_StationModule.intUsePointCount--;
					break;
				}
			}
			Show();
		}


		public void Show()
		{
			int m = 0;
			int n = 0;
			PointVlaue pvale = null;
			if (tag_StationModule == null)
				return;

			plpointMessage.AutoScrollPosition = new Point(0, 0);
			for (int i = 0; i < tag_StationModule.arrAxis.Count; i++)
			{
				tag_LabelAxisTitleList[i].Text = tag_StationModule.arrAxis[i].AxisName;
				tag_LabelAxisTitleList[i].Visible = true; ;
				m = i;
			}
			m++;
			while (m < tag_LabelAxisTitleList.Count)
			{
				tag_LabelAxisTitleList[m].Visible = false; ;
				m++;
			}
			getPageTotal();
			m = 0;
			int count = 0;
			for (int i = 0; i < tag_StationModule.arrPoint.Count; i++)
			{

				if (tag_statioOpen == true && tag_StationModule.arrPoint[i].tag_BeginPointAggregateList != null && tag_StationModule.arrPoint[i].tag_BeginPointAggregateList.Count > 0)
				{
					for (n = 0; n < tag_StationModule.arrPoint[i].tag_BeginPointAggregateList.Count; n++)
					{

						if (m < (tag_page_index + 1) * tag_page_Count && m >= (tag_page_index) * tag_page_Count)
						{
							pvale = tag_PointVlaueList[m % tag_page_Count];
							pvale.tag_stationM = tag_StationModule;
							pvale.tag_stepName = m;
							pvale.PointSet = (PointAggregate)tag_StationModule.arrPoint[i].tag_BeginPointAggregateList[n];
							pvale._Worker = tag_Work;
							pvale.tag_DelFun = this.Delete_Click;
							pvale.tag_InsterFun = this.AddClick;
							pvale.tag_EnterFun = this.EnterFun_Click;

							pvale.Visible = true;

							count++;
						}
						m++;
					}
				}

				if (m < (tag_page_index + 1) * tag_page_Count && m >= (tag_page_index) * tag_page_Count)
				{
					{
						pvale = tag_PointVlaueList[m % tag_page_Count];
						pvale._Worker = tag_Work;
						pvale.tag_stationM = tag_StationModule;
						pvale.tag_stepName = m;
						pvale.PointSet = (PointAggregate)(PointAggregate)tag_StationModule.arrPoint[i];
					}

					pvale.tag_stationM = tag_StationModule;

					pvale.tag_DelFun = this.Delete_Click;
					pvale.tag_InsterFun = this.AddClick;
					pvale.tag_EnterFun = this.EnterFun_Click;
					pvale.Visible = true;
					count++;
				}

				m++;


				if (tag_statioOpen == true && tag_StationModule.arrPoint[i].tag_EndPointAggregateList != null && tag_StationModule.arrPoint[i].tag_EndPointAggregateList.Count > 0)
				{

					for (n = 0; n < tag_StationModule.arrPoint[i].tag_EndPointAggregateList.Count; n++)
					{
						if (m < (tag_page_index + 1) * tag_page_Count && m >= (tag_page_index) * tag_page_Count)
						{
							{
								pvale = tag_PointVlaueList[m % tag_page_Count];
								pvale.tag_stationM = tag_StationModule;
								pvale.tag_stepName = m;
								pvale.PointSet = (PointAggregate)tag_StationModule.arrPoint[i].tag_EndPointAggregateList[n];
							}


							pvale._Worker = tag_Work;
							pvale.tag_DelFun = this.Delete_Click;
							pvale.tag_InsterFun = this.AddClick;
							pvale.tag_EnterFun = this.EnterFun_Click;
							pvale.Visible = true;
							count++;
						}
						m++;

					}
				}

			}
			while (count < tag_PointVlaueList.Count)
			{
				tag_PointVlaueList[count % tag_page_Count].Visible = false;
				count++;
			}

			for (int i = 0; i < tag_ButtonPageList.Count; i++)
			{
				if (i < tag_pageTotal)
				{
					tag_ButtonPageList[i].Visible = true;
				}
				else
				{
					tag_ButtonPageList[i].Visible = false;
				}


			}
		}
		public void UserControl_ShowAllPoint_HeightLight(object sender, EventArgs e)
		{
			int i = 0;

			int pageInex = 0;

			if (tag_StationModule.tag_stepId < tag_page_Count)
			{
				pageInex = 0;

			}
			else
			{
				if (tag_StationModule.tag_stepId % tag_page_Count == 0)
				{
					pageInex = tag_StationModule.tag_stepId / tag_page_Count;
				}
				else
				{
					pageInex = tag_StationModule.tag_stepId / tag_page_Count;
				}
			}

			if (pageInex != tag_page_index)
			{
				tag_page_index = pageInex;
				Show();
			}

			foreach (PointVlaue pv in tag_PointVlaueList)
			{
				if (i + tag_page_index * tag_page_Count == tag_StationModule.tag_stepId)
				{
					pv.BackColor = Color.Blue; ;
				}
				else
				{
					pv.BackColor = Color.White; ;

				}
				i++;


			}
		}


		private void UserControl_ShowAllPoint_Load(object sender, EventArgs e)
		{
			Show();
		}

		private void 运动ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Global.CConst.UserLevel == Global.CConst.USER_SUPERADMIN || Global.CConst.UserLevel == Global.CConst.USER_ADMINISTOR)
			{

			}
			else
			{
				MessageBoxLog.Show("无权限");
				return;
			}

			if (MessageBoxLog.Show("是否保存", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
			{
				return;
			}
			foreach (PointVlaue pv in tag_PointVlaueList)
			{
				pv.button_save_Click();
			}
			if (!StationManage._Config.Save())
			{
				MessageBoxLog.Show("保存参数异常");
			}
			else
			{
				MessageBoxLog.Show("保存成功");
			}
		}

		private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void iO配置ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void UserControl_ShowAllPoint_SizeChanged(object sender, EventArgs e)
		{

			CBpointMessage.Size = new Size(this.Size.Width, this.Size.Height - 100); ;
			plpointMessage.Size = new Size(CBpointMessage.Size.Width - 10, this.CBpointMessage.Height - 40);
			plpointMessage.Location = new Point(5, 15);
			CBpointMessage.Location = new Point(0, 0);


			for (int i = 0; i < 20; i++)
			{

				tag_ButtonPageList[i].Location = new Point(i * tag_ButtonPageList[i].Width + i * 5, CBpointMessage.Size.Height - 25);



			}
		}
	}
}
