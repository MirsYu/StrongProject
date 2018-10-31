using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace StrongProject
{
	public partial class PointVlaue : UserControl
	{
		public delegate void delegate_Select(object sender, EventArgs e);
		public Work _Worker;
		public delegate_Select tag_upFun;
		public delegate_Select tag_DownFun;
		public delegate_Select tag_DelFun;
		public delegate_Select tag_InsterFun;
		public delegate_Select tag_IndexFun;
		public delegate_Select tag_EnterFun;

		public string tag_StationName;
		public int tag_stepName;
		public StationModule tag_stationM;
		private PointAggregate _PointSet = null;
		[DefaultValue(null)]
		public PointAggregate PointSet
		{
			get { return _PointSet; }
			set
			{
				_PointSet = value;
				Init();
			}
		}
		public object[] tag_textobj = new object[60];
		public PointVlaue(PointAggregate PointSet_, Work _sWorker, int step, StationModule sm) //初始化点位参数数组
		{
			tag_stepName = step;
			_Worker = _sWorker;
			tag_stationM = sm;
			InitializeComponent();
			if (PointSet_ != null)
				PointSet = PointSet_;
		}

		//PointVlue加载
		private void PointVlaue_Load(object sender, EventArgs e)
		{

			if (_PointSet != null)
			{

				textBox_name.Text = _PointSet.strName;
				label_Step.Text = tag_stepName.ToString();
			}
		}

		//初始化组件
		public void Init()
		{
			try
			{
				if (_PointSet != null)
				{

					label_Step.Text = tag_stepName.ToString();

					if (_PointSet.tag_isWait == 0)
					{
						checkBox_wait.Checked = false;
					}
					else
					{
						checkBox_wait.Checked = true;
					}
					textBox_name.Text = _PointSet.strName;
					checkBox_Enable.Checked = _PointSet.tag_isEnable;

					checkBox_AxisStop.Checked = _PointSet.tag_isAxisStop;
					if (_PointSet.tag_motionType == 0)
					{
						comboBox2.SelectedIndex = 0;
					}
					else
					{
						comboBox2.SelectedIndex = 1;
					}
					if (_PointSet.tag_MotionLineType == 0)
					{
						comboBox_Line.SelectedIndex = 0;
					}
					else
					{
						comboBox_Line.SelectedIndex = 1;
					}


					if (_PointSet.tag_isRest == 1)
					{
						checkBox_AxisRest.Checked = true;
					}
					else
					{
						checkBox_AxisRest.Checked = false;
					}
					textBox_Sleep.Text = _PointSet.tag_Sleep.ToString();
					int ww = 5;
					comboBox_delectAdd.Location = new Point(30, (this.Size.Height - textBox_name.Size.Height) / 2);

					textBox_name.Location = new Point(comboBox_delectAdd.Location.X + comboBox_delectAdd.Size.Width + ww, (this.Size.Height - textBox_name.Size.Height) / 2);
					int x = textBox_name.Location.X + textBox_name.Size.Width + ww;



					int x2 = x;
					if (_PointSet.arrPoint.Length < tag_stationM.arrAxis.Count)
					{
						PointModule[] arrPoint = new PointModule[tag_stationM.arrAxis.Count];

						int mm = 0;
						for (mm = 0; mm < _PointSet.arrPoint.Length; mm++)
						{
							arrPoint[mm] = _PointSet.arrPoint[mm];
						}
						for (mm = 0; mm < tag_stationM.arrAxis.Count; mm++)
						{
							arrPoint[mm] = new PointModule(); ;
						}
						_PointSet.arrPoint = arrPoint;
					}
					int i = 0;
					for (i = 0; i < tag_stationM.arrAxis.Count; i++)
					{
						// if (_PointSet.arrPoint[i].blnPointEnable)
						{
							TextBox tex = null;
							if (tag_textobj[i] == null)
							{
								tex = new TextBox();
								this.Controls.Add(tex);
							}
							else
							{
								tex = (TextBox)tag_textobj[i];
							}
							tex.Text = _PointSet.arrPoint[i].dblPonitValue.ToString();
							tex.Size = new Size(40, 20);
							tex.Location = new Point(x2 + ww * i + tex.Size.Width * i,
								(this.Size.Height - tex.Size.Height) / 2);
							x = tex.Location.X;
							tag_textobj[i] = tex;

							if (!_PointSet.arrPoint[i].blnPointEnable)
							{
								tex.BackColor = Color.White;
								tex.ForeColor = Color.Black;
								tex.Enabled = false;
							}
							else
							{
								tex.BackColor = Color.Green;
								tex.ForeColor = Color.White;
								tex.Enabled = true;
							}
							tex.Visible = true;
						}

					}

					while (i < tag_textobj.Length)
					{
						TextBox tex2 = (TextBox)tag_textobj[i];
						if (tex2 != null)
						{
							tex2.Visible = false;
						}
						i++;
					}
					button_Exe.Location = new Point(x + 50, (this.Size.Height - button_Exe.Size.Height) / 2);
					butIoSet.Location = new Point(button_Exe.Location.X + button_Exe.Size.Width + ww, (this.Size.Height - butIoSet.Size.Height) / 2);
					button_save.Location = new Point(butIoSet.Location.X + butIoSet.Size.Width + ww, (this.Size.Height - button_save.Size.Height) / 2);
					checkBox_Enable.Location = new Point(button_save.Location.X + button_save.Size.Width + ww, (this.Size.Height - checkBox_Enable.Size.Height) / 2);
					//  button1.Location = new Point(checkBox_Enable.Location.X + button_save.Size.Width + ww, (this.Size.Height - checkBox_Enable.Size.Height) / 2);




					checkBox_wait.Location = new Point(checkBox_Enable.Location.X + checkBox_Enable.Size.Width + ww, (this.Size.Height - checkBox_wait.Size.Height) / 2);
					checkBox_AxisRest.Location = new Point(checkBox_wait.Location.X + checkBox_wait.Size.Width + ww, (this.Size.Height - checkBox_AxisRest.Size.Height) / 2);
					checkBox_AxisStop.Location = new Point(checkBox_AxisRest.Location.X + checkBox_AxisRest.Size.Width + ww, (this.Size.Height - checkBox_AxisStop.Size.Height) / 2);
					comboBox2.Location = new Point(checkBox_AxisStop.Location.X + checkBox_AxisStop.Size.Width + ww, (this.Size.Height - comboBox2.Size.Height) / 2);
					comboBox_Line.Location = new Point(comboBox2.Location.X + comboBox2.Size.Width + ww, (this.Size.Height - comboBox_Line.Size.Height) / 2);
					button_axisSafe.Location = new Point(comboBox_Line.Location.X + comboBox_Line.Size.Width + ww, (this.Size.Height - button_axisSafe.Size.Height) / 2);
					textBox_Sleep.Location = new Point(button_axisSafe.Location.X + button_axisSafe.Size.Width + ww, (this.Size.Height - textBox_Sleep.Size.Height) / 2);

					if ((_PointSet.tag_BeginFun != null || _PointSet.tag_EndFun != null) && Global.WorkVar.tag_GuanLian == true)
					{
						textBox_name.Enabled = false;
					}
					else
					{
						textBox_name.Enabled = true;
					}
				}
			}
			catch
			{ }

		}


		//刷新
		public void refresh()
		{
			try
			{
				if (_PointSet != null)
				{

					label_Step.Text = tag_stepName.ToString();

					if (_PointSet.tag_isWait == 0)
					{
						checkBox_wait.Checked = false;
					}
					else
					{
						checkBox_wait.Checked = true;
					}
					textBox_name.Text = _PointSet.strName;
					checkBox_Enable.Checked = _PointSet.tag_isEnable;

					checkBox_AxisStop.Checked = _PointSet.tag_isAxisStop;
					if (_PointSet.tag_motionType == 0)
					{
						comboBox2.SelectedIndex = 0;
					}
					else
					{
						comboBox2.SelectedIndex = 1;
					}

					if (_PointSet.tag_isRest == 1)
					{
						checkBox_AxisRest.Checked = true;
					}
					else
					{
						checkBox_AxisRest.Checked = false;
					}
					textBox_Sleep.Text = _PointSet.tag_Sleep.ToString();
					int ww = 5;
					comboBox_delectAdd.Location = new Point(30, (this.Size.Height - textBox_name.Size.Height) / 2);

					textBox_name.Location = new Point(comboBox_delectAdd.Location.X + comboBox_delectAdd.Size.Width + ww, (this.Size.Height - textBox_name.Size.Height) / 2);
					int x = textBox_name.Location.X + textBox_name.Size.Width + ww;



					int x2 = x;
					if (_PointSet.arrPoint.Length < tag_stationM.arrAxis.Count)
					{
						PointModule[] arrPoint = new PointModule[tag_stationM.arrAxis.Count];

						int mm = 0;
						for (mm = 0; mm < _PointSet.arrPoint.Length; mm++)
						{
							arrPoint[mm] = _PointSet.arrPoint[mm];
						}
						for (mm = 0; mm < tag_stationM.arrAxis.Count; mm++)
						{
							arrPoint[mm] = new PointModule(); ;
						}
						_PointSet.arrPoint = arrPoint;
					}
					int i = 0;
					for (i = 0; i < tag_stationM.arrAxis.Count; i++)
					{
						// if (_PointSet.arrPoint[i].blnPointEnable)
						{
							TextBox tex = null;
							if (tag_textobj[i] == null)
							{
								tex = new TextBox();
								this.Controls.Add(tex);
							}
							else
							{
								tex = (TextBox)tag_textobj[i];
							}
							tex.Text = _PointSet.arrPoint[i].dblPonitValue.ToString();
							tex.Size = new Size(40, 20);
							tex.Location = new Point(x2 + ww * i + tex.Size.Width * i,
								(this.Size.Height - tex.Size.Height) / 2);
							x = tex.Location.X;
							tag_textobj[i] = tex;

							if (!_PointSet.arrPoint[i].blnPointEnable)
							{
								tex.BackColor = Color.White;
								tex.ForeColor = Color.Black;
								tex.Enabled = false;
							}
							else
							{
								tex.BackColor = Color.Green;
								tex.ForeColor = Color.White;
								tex.Enabled = true;
							}
							tex.Visible = true;
						}

					}

					while (i < tag_textobj.Length)
					{
						TextBox tex2 = (TextBox)tag_textobj[i];
						if (tex2 != null)
						{
							tex2.Visible = false;
						}
						i++;
					}
					button_Exe.Location = new Point(x + 50, (this.Size.Height - button_Exe.Size.Height) / 2);
					butIoSet.Location = new Point(button_Exe.Location.X + button_Exe.Size.Width + ww, (this.Size.Height - butIoSet.Size.Height) / 2);
					button_save.Location = new Point(butIoSet.Location.X + butIoSet.Size.Width + ww, (this.Size.Height - button_save.Size.Height) / 2);
					checkBox_Enable.Location = new Point(button_save.Location.X + button_save.Size.Width + ww, (this.Size.Height - checkBox_Enable.Size.Height) / 2);
					//  button1.Location = new Point(checkBox_Enable.Location.X + button_save.Size.Width + ww, (this.Size.Height - checkBox_Enable.Size.Height) / 2);




					checkBox_wait.Location = new Point(checkBox_Enable.Location.X + checkBox_Enable.Size.Width + ww, (this.Size.Height - checkBox_wait.Size.Height) / 2);
					checkBox_AxisRest.Location = new Point(checkBox_wait.Location.X + checkBox_wait.Size.Width + ww, (this.Size.Height - checkBox_AxisRest.Size.Height) / 2);
					checkBox_AxisStop.Location = new Point(checkBox_AxisRest.Location.X + checkBox_AxisRest.Size.Width + ww, (this.Size.Height - checkBox_AxisStop.Size.Height) / 2);
					comboBox2.Location = new Point(checkBox_AxisStop.Location.X + checkBox_AxisStop.Size.Width + ww, (this.Size.Height - comboBox2.Size.Height) / 2);
					comboBox_Line.Location = new Point(comboBox2.Location.X + comboBox2.Size.Width + ww, (this.Size.Height - comboBox_Line.Size.Height) / 2);
					button_axisSafe.Location = new Point(comboBox_Line.Location.X + comboBox_Line.Size.Width + ww, (this.Size.Height - button_axisSafe.Size.Height) / 2);
					textBox_Sleep.Location = new Point(button_axisSafe.Location.X + button_axisSafe.Size.Width + ww, (this.Size.Height - textBox_Sleep.Size.Height) / 2);

					if ((_PointSet.tag_BeginFun != null || _PointSet.tag_EndFun != null) && Global.WorkVar.tag_GuanLian == true)
					{
						textBox_name.Enabled = false;
					}
				}
			}
			catch
			{ }

		}


		//保存方法
		public void Save()
		{
			if (_PointSet != null)
			{
			}
		}
		private void butIoSet_Click(object sender, EventArgs e)
		{
			FrmInIoList frmIo = new FrmInIoList(_PointSet);
			frmIo.Show();
		}

		public void button_save_Click(object sender, EventArgs e)
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
			try
			{
				if (Global.CConst.UserLevel == Global.CConst.USER_SUPERADMIN)
				{
					if (textBox_name.Text.Length > 0)
						_PointSet.strName = textBox_name.Text;
				}

				for (int i = 0; i < tag_stationM.arrAxis.Count; i++)
				{
					if (_PointSet.arrPoint[i].blnPointEnable)
					{
						TextBox tex = (TextBox)tag_textobj[i];
						_PointSet.arrPoint[i].dblPonitValue = double.Parse(tex.Text);

					}
					else
					{
						tag_textobj[i] = null;
					}
					if (checkBox_wait.Checked)
					{
						_PointSet.tag_isWait = 1;
					}
					else
					{
						_PointSet.tag_isWait = 0;
					}
					if (checkBox_AxisRest.Checked)
					{
						_PointSet.tag_isRest = 1;
					}
					else
					{
						_PointSet.tag_isRest = 0;
					}




				}
				if (comboBox2.SelectedIndex == 0)
				{
					_PointSet.tag_motionType = 0;
				}
				else
				{
					_PointSet.tag_motionType = 1;
				}


				_PointSet.tag_isAxisStop = checkBox_AxisStop.Checked;

				_PointSet.tag_Sleep = int.Parse(textBox_Sleep.Text);

				_PointSet.tag_isEnable = checkBox_Enable.Checked;
				if (!_Worker.Config.Save())
				{
					MessageBoxLog.Show("保存参数异常");
				}
				else
				{
					// MessageBoxLog.Show("保存成功");

					//RefreshStation();
				}
			}
			catch (Exception ex)
			{
				//gloalOnLineShare.showBox(ex.Message);
			}
		}

		public void button_save_Click()
		{
			try
			{
				if (Global.CConst.UserLevel == Global.CConst.USER_SUPERADMIN)
				{
					if (textBox_name.Text.Length > 0)
						_PointSet.strName = textBox_name.Text;
				}

				for (int i = 0; i < tag_stationM.arrAxis.Count; i++)
				{
					if (_PointSet.arrPoint[i].blnPointEnable)
					{
						TextBox tex = (TextBox)tag_textobj[i];
						_PointSet.arrPoint[i].dblPonitValue = double.Parse(tex.Text);

					}
					else
					{

					}
					if (checkBox_wait.Checked)
					{
						_PointSet.tag_isWait = 1;
					}
					else
					{
						_PointSet.tag_isWait = 0;
					}
					if (checkBox_AxisRest.Checked)
					{
						_PointSet.tag_isRest = 1;
					}
					else
					{
						_PointSet.tag_isRest = 0;
					}
				}
				if (comboBox2.SelectedIndex == 0)
				{
					_PointSet.tag_motionType = 0;
				}
				else
				{
					_PointSet.tag_motionType = 1;
				}


				_PointSet.tag_isAxisStop = checkBox_AxisStop.Checked;

				_PointSet.tag_Sleep = int.Parse(textBox_Sleep.Text);

				_PointSet.tag_isEnable = checkBox_Enable.Checked;
				_PointSet.tag_MotionLineType = comboBox_Line.SelectedIndex;
			}
			catch (Exception ex)
			{
				//gloalOnLineShare.showBox(ex.Message);
			}
		}

		private short pointRun(object o)
		{
			if (tag_stationM.tag_type == 1)
			{
				return 0;
			}
			short ret = 0;
			if ((ret = pointMotion.pointRun(_PointSet, tag_StationName, null)) != 0)
			{
				return ret;
			}
			return ret;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!Work.IsMove(0))
			{
				return;
			}
			Global.WorkVar.tag_IsExit = 0;
			Global.WorkVar.tag_isFangDaiJieChu = false;
			FrmWait wait = new FrmWait(pointRun, _PointSet);
			wait.ShowDialog();

		}

		private void textBox_name_Enter(object sender, EventArgs e)
		{

			int i = 0;
			if (tag_EnterFun != null)
				tag_EnterFun(this, e);
			this.BackColor = Color.Blue;
		}

		private void textBox_name_Leave(object sender, EventArgs e)
		{
			this.BackColor = Color.White;
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (comboBox_delectAdd.SelectedIndex)
			{
				case 0:
					if (tag_InsterFun != null)
						tag_InsterFun(_PointSet, null);
					break;
				case 1:
					if (tag_DelFun != null)
					{
						tag_DelFun(_PointSet, null);
					}
					break;
				case 2:
					break;
				case 3:

					break;
			}
		}

		private void button_Exe_Enter(object sender, EventArgs e)
		{
			this.BackColor = Color.Green;
			tag_stationM.tag_stepId = tag_stepName;
		}

		private void button_Exe_Leave(object sender, EventArgs e)
		{
			this.BackColor = Color.White;
		}

		private void button_axisSafe_Click(object sender, EventArgs e)
		{
			if (_PointSet.tag_AxisSafeManage == null)
			{
				_PointSet.tag_AxisSafeManage = new AxisSafeManage(_PointSet);
			}
			FrmAxisSafeManage frmAsm = new FrmAxisSafeManage(_PointSet);
			frmAsm.Show();
		}
	}
}
