using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace StrongProject
{
	public partial class SetIOPanel : UserControl
	{
		private IOParameter _IO = null;
		[DefaultValue(null)]
		public IOParameter IO
		{
			get
			{
				return _IO;
			}
			set
			{
				_IO = value;
				Init();
			}
		}
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get
			{
				return textBox1.Text;
			}
			set
			{
				textBox1.Text = value;
			}
		}
		public SetIOPanel()
		{
			InitializeComponent();
		}
		public SetIOPanel(IOParameter IO)
		{
			_IO = IO;
			InitializeComponent();
			Init();
		}
		public void Init()
		{
			if (_IO != null)
			{
				textBox1.Text = _IO.StrIoName;

				comboBox_motiontype.SelectedIndex = _IO.CardNum + 1;

				comboBox_io.Items.Clear();
				comboBox_io.Items.Add("无");
				for (int i = 0; i < 60; i++)
				{
					comboBox_io.Items.Add(i.ToString());
				}
				comboBox_Type.SelectedIndex = (int)(_IO.tag_MotionCardManufacturer);
				comboBox_io.SelectedIndex = _IO.IOBit + 1;
				comboBox_status.Items.Clear();
				comboBox_status.Items.Add("低");
				comboBox_status.Items.Add("高");
				switch (_IO.Logic)
				{
					case 1:
						comboBox_status.SelectedIndex = 1;
						break;
					case 0:
						comboBox_status.SelectedIndex = 0;
						break;
				}
				//numericUpDown_delay.Value = new decimal(_IO.Delay);
			}
		}
		public void Save()
		{
			if (_IO != null)
			{
				_IO.StrIoName = textBox1.Text;
				_IO.CardNum = (short)(comboBox_motiontype.SelectedIndex - 1);
				_IO.tag_MotionCardManufacturer = (comboBox_Type.SelectedIndex);
				_IO.IOBit = (short)(comboBox_io.SelectedIndex - 1);

				_IO.Logic = comboBox_status.SelectedIndex;
				// _IO.Logic = comboBox_status.SelectedIndex == 0 ? 0 : 1;
			}
		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (_IO.tagPointAggregate == null)
			{
				_IO.tagPointAggregate = new PointAggregate("IO防呆", _IO.StrIoName);
			}

			FrmAxisSafeManage ax = new FrmAxisSafeManage(_IO.tagPointAggregate);
			ax.Show();
		}
	}
}
