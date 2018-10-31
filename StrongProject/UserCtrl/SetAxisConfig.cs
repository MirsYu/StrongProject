using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace StrongProject
{
	public partial class SetAxisConfig : UserControl
	{
		//private bool _TitleVisible = true;
		//[DefaultValue(true)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		//public bool TitleVisible
		//{
		//    get
		//    {
		//        return _TitleVisible;
		//    }
		//    set
		//    {
		//        _TitleVisible = value;
		//        panel1.Visible = _TitleVisible;
		//    }
		//}
		//[Browsable(true)]
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		//public override string Text
		//{
		//    get
		//    {
		//        return label13.Text;
		//    }
		//    set
		//    {
		//        label13.Text = value;
		//    }
		//}
		//private AxisParaSet _AxisSet = null;
		//[DefaultValue(null)]
		//public AxisParaSet AxisSet1
		//{
		//    get { return _AxisSet; }
		//    set 
		//    {
		//        _AxisSet = value;
		//        Init();
		//    }
		//}

		//public bool blnSelect=false;
		public bool CheckStatus
		{
			get { return checkBox_Select.Checked; }
			set
			{
				if (value != checkBox_Select.Checked)
				{
					checkBox_Select.Checked = value;
				}
			}

		}
		private AxisConfig _AxisSet = null;
		[DefaultValue(null)]
		public AxisConfig AxisSet
		{
			get { return _AxisSet; }
			set
			{
				_AxisSet = value;
				Init();
			}
		}
		public SetAxisConfig()
		{
			InitializeComponent();
		}
		public void Init()
		{
			if (_AxisSet != null)
			{
				textBox_AxisName.Text = _AxisSet.AxisName;
				numericUpDown_CardNumber.Value = new decimal(_AxisSet.CardNum);
				numericUpDown_AxisNumber.Value = new decimal(_AxisSet.AxisNum);
				numericUpDown_Lead.Value = new decimal(_AxisSet.UnitPerLap);
				numericUpDown_pulse.Value = new decimal(_AxisSet.PulsePerLap);
				//comboBox_motiondir.Items.Clear();
				//comboBox_motiondir.Items.Add("正方向");
				//comboBox_motiondir.Items.Add("负方向");
				//switch (_AxisSet.MotionDir)
				//{
				//    case MoveDirections.Positive:
				//        comboBox_motiondir.SelectedIndex = 0;
				//        break;
				//    case MoveDirections.Negative:
				//        comboBox_motiondir.SelectedIndex = 1;
				//        break;
				//}
				comboBox_homedir.Items.Clear();
				comboBox_sofeEnable.Items.Clear();
				comboBox_homedir.Items.Add("正方向");
				comboBox_homedir.Items.Add("负方向");
				comboBox_sofeEnable.Items.Add("启用");
				comboBox_sofeEnable.Items.Add("禁用");
				switch ((int)_AxisSet.HomeDir)
				{
					case 0:
						comboBox_homedir.SelectedIndex = 0;
						break;
					case 1:
						comboBox_homedir.SelectedIndex = 1;
						break;
				}

				switch ((int)_AxisSet.SoftLimitEnablel)
				{
					case 0:
						comboBox_sofeEnable.SelectedIndex = 0;
						break;
					case 1:
						comboBox_sofeEnable.SelectedIndex = 1;
						break;
				}

				switch (_AxisSet.tag_homeIoHighLow)
				{
					case true:
						comboBox1_hlIoHome.SelectedIndex = 1;
						break;
					case false:
						comboBox1_hlIoHome.SelectedIndex = 0;
						break;
				}
				comboBox_ResetType.SelectedIndex = _AxisSet.GoHomeType;

				comboBox_motionType.SelectedIndex = _AxisSet.tag_XYZU_Type;

				comboBox_JogDir.SelectedIndex = _AxisSet.JogDir;
				numericUpDown_startspeed.Value = new decimal(_AxisSet.StartSpeed / _AxisSet.Eucf);
				numericUpDown_acc.Value = new decimal(_AxisSet.Acc);
				numericUpDown_homespeed.Value = new decimal(_AxisSet.HomeSpeed / _AxisSet.Eucf);
				numericUpDown_homespeedhight.Value = new decimal(_AxisSet.HomeSpeedHight / _AxisSet.Eucf);
				numericUpDown_movespeed.Value = new decimal(_AxisSet.Speed / _AxisSet.Eucf);
				numericUpDown_manualspeedlow.Value = new decimal(_AxisSet.ManualSpeedLow / _AxisSet.Eucf);
				numericUpDown_manualspeednormal.Value = new decimal(_AxisSet.ManualSpeedNormal / _AxisSet.Eucf);
				numericUpDown_manualspeedhigh.Value = new decimal(_AxisSet.ManualSpeedHigh / _AxisSet.Eucf);
				numericUpDown_eucf.Value = new decimal(_AxisSet.Eucf);
				numericUpDown_orgpos.Value = new decimal(_AxisSet.OrgPos);
				numericUpDown_softlimitmin.Value = new decimal(_AxisSet.SoftLimitMinValue);
				numericUpDown_softlimitmax.Value = new decimal(_AxisSet.SoftLimitMaxValue);

				numericUpDown_AccTime.Value = new decimal(_AxisSet.tag_accTime);


				comboBox_MauType.SelectedIndex = (int)_AxisSet.tag_MotionCardManufacturer;

				numericUpDown_S_StopTime.Value = new decimal(_AxisSet.tag_S_Time);

				numericUpDown_StopSpeed.Value = new decimal(_AxisSet.tag_StopSpeed);

				numericUpDown_decTime.Value = new decimal(_AxisSet.tag_delTime);


			}
		}
		public void Save()
		{
			if (_AxisSet != null)
			{
				_AxisSet.AxisName = textBox_AxisName.Text;
				_AxisSet.CardNum = decimal.ToInt16(numericUpDown_CardNumber.Value);
				_AxisSet.AxisNum = decimal.ToInt16(numericUpDown_AxisNumber.Value);
				_AxisSet.UnitPerLap = decimal.ToDouble(numericUpDown_Lead.Value);
				_AxisSet.PulsePerLap = decimal.ToInt32(numericUpDown_pulse.Value);
				//_AxisSet.MotionDir = comboBox_motiondir.SelectedIndex == 0 ? MoveDirections.Positive : MoveDirections.Negative;
				_AxisSet.HomeDir = comboBox_homedir.SelectedIndex == 0 ? 0 : 1;
				_AxisSet.SoftLimitEnablel = comboBox_sofeEnable.SelectedIndex == 0 ? 0 : 1;
				_AxisSet.StartSpeed = decimal.ToDouble(numericUpDown_startspeed.Value) * _AxisSet.Eucf;
				_AxisSet.Acc = decimal.ToDouble(numericUpDown_acc.Value);
				_AxisSet.HomeSpeed = decimal.ToDouble(numericUpDown_homespeed.Value) * _AxisSet.Eucf;
				_AxisSet.HomeSpeedHight = decimal.ToDouble(numericUpDown_homespeedhight.Value) * _AxisSet.Eucf;
				_AxisSet.Speed = decimal.ToDouble(numericUpDown_movespeed.Value) * _AxisSet.Eucf;
				_AxisSet.ManualSpeedLow = decimal.ToDouble(numericUpDown_manualspeedlow.Value) * _AxisSet.Eucf;
				_AxisSet.ManualSpeedNormal = decimal.ToDouble(numericUpDown_manualspeednormal.Value) * _AxisSet.Eucf;
				_AxisSet.ManualSpeedHigh = decimal.ToDouble(numericUpDown_manualspeedhigh.Value) * _AxisSet.Eucf;
				_AxisSet.Eucf = decimal.ToInt32(numericUpDown_pulse.Value) / decimal.ToDouble(numericUpDown_Lead.Value);
				_AxisSet.OrgPos = decimal.ToDouble(numericUpDown_orgpos.Value);
				_AxisSet.SoftLimitMinValue = decimal.ToDouble(numericUpDown_softlimitmin.Value);
				_AxisSet.SoftLimitMaxValue = decimal.ToDouble(numericUpDown_softlimitmax.Value);

				_AxisSet.tag_accTime = decimal.ToDouble(numericUpDown_AccTime.Value);

				_AxisSet.tag_XYZU_Type = comboBox_motionType.SelectedIndex;
				_AxisSet.GoHomeType = comboBox_ResetType.SelectedIndex;






				_AxisSet.tag_homeIoHighLow = comboBox1_hlIoHome.SelectedIndex == 0 ? false : true;

				//  _AxisSet.JogDir = (short)comboBox_JogDir.SelectedIndex;
				if (comboBox_JogDir.SelectedIndex == 0)
					_AxisSet.JogDir = 0;
				else
					_AxisSet.JogDir = 1;

				_AxisSet.tag_MotionCardManufacturer = (MotionCardManufacturer)comboBox_MauType.SelectedIndex;
				_AxisSet.tag_S_Time = decimal.ToDouble(numericUpDown_S_StopTime.Value);

				_AxisSet.tag_StopSpeed = decimal.ToDouble(numericUpDown_StopSpeed.Value);
				_AxisSet.tag_delTime = decimal.ToDouble(numericUpDown_decTime.Value);



			}
		}

		private void comboBox_sofeEnable_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboBox_sofeEnable.Text == "启用")
			{
				numericUpDown_softlimitmin.Enabled = true;
				numericUpDown_softlimitmax.Enabled = true;
			}
			else
			{
				numericUpDown_softlimitmin.Enabled = false;
				numericUpDown_softlimitmax.Enabled = false;

			}

		}

		private void button1_Click(object sender, EventArgs e)
		{
			FrmGoHomeParameter gh = new FrmGoHomeParameter(_AxisSet);
			gh.Show();
		}


	}
}
