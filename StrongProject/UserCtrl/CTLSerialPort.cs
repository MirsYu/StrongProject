using System;
using System.ComponentModel;
using System.Drawing;
using System.IO.Ports;
using System.Runtime.InteropServices;//for INI
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class CTLSerialPort : UserControl
	{
		public const string errRet = "err";
		public bool HasLoadPara = false;
		private bool Listening = false;//接收事件是否在监听
		private bool Closing = false;//串口是否在关闭中
		private string dataRecive;//接收的数据
		private bool IsReadedDate = false;//是否接收到数据

		public PortParameter tag_PortParameter;
		public CTLSerialPort()
		{
			InitializeComponent();
		}
		public CTLSerialPort(PortParameter _PortParameter)
		{
			tag_PortParameter = _PortParameter;
			InitializeComponent();
		}
		//端口是否打开
		public bool IsOpen()
		{
			return spTest1.IsOpen;
		}
		//是否有接收到数据
		public bool IsReceivedData()
		{
			return IsReadedDate;
		}
		//clear receive data
		public void clearRead()
		{
			dataRecive = "";
			IsReadedDate = false;
		}
		//get received data
		public string GetReadData()
		{
			string strTempReceive = dataRecive;
			clearRead();
			return strTempReceive;
		}

		//串口打开，关闭
		public void ctrlOpenCom()
		{
			if (btnSPopen.Text == "打开串口")
			{
				try
				{
					if (!HasLoadPara)
					{
						LoadPara();
						HasLoadPara = true;
					}

					spTest1.PortName = cmbPortName.Text.ToString(); ;
					spTest1.BaudRate = Convert.ToInt32(cmbBaudRate.Text);
					spTest1.DataBits = Convert.ToInt16(cmbDataBits.Text);

					switch (cmbParity.Text)
					{
						case "EVEN":
							spTest1.Parity = Parity.Even; break;
						case "ODD":
							spTest1.Parity = Parity.Odd; break;
						case "NONE":
							spTest1.Parity = Parity.None; break;
					}
					switch (cmbStopBits.Text)
					{
						case "NONE":
							spTest1.StopBits = System.IO.Ports.StopBits.None; break;
						case "One":
							spTest1.StopBits = StopBits.One; break;
						case "Two":
							spTest1.StopBits = StopBits.Two; break;
					}


					spTest1.Open();
				}
				catch (Exception)
				{
					MessageBoxLog.Show("串口打开失败");
					lblPortInd.BackColor = Color.Red;
					return;
				}

			}
			else// close port
			{

				btnSPopen.Text = "打开串口";
				if (spTest1.IsOpen)
				{
					Closing = true;
					while (Listening) Application.DoEvents();
					spTest1.Close();
					Closing = false;
					lblPortInd.BackColor = Color.Red;
				}
			}
			if (!spTest1.IsOpen)
			{
				bool bSetCtrl = true;
				cmbPortName.Enabled = bSetCtrl;
				cmbBaudRate.Enabled = bSetCtrl;
				cmbDataBits.Enabled = bSetCtrl;
				cmbParity.Enabled = bSetCtrl;
				cmbStopBits.Enabled = bSetCtrl;
				btnSend.Enabled = !bSetCtrl;
				btnClear.Enabled = !bSetCtrl;
				lblPortInd.BackColor = Color.Red;
				btnSPopen.Text = "打开串口";

				//this.Text = strCtrlName ;

			}
			else
			{
				bool bSetCtrl = false;
				cmbPortName.Enabled = bSetCtrl;
				cmbBaudRate.Enabled = bSetCtrl;
				cmbDataBits.Enabled = bSetCtrl;
				cmbParity.Enabled = bSetCtrl;
				cmbStopBits.Enabled = bSetCtrl;
				btnSend.Enabled = !bSetCtrl;
				btnClear.Enabled = !bSetCtrl;
				lblPortInd.BackColor = Color.Green;
				btnSPopen.Text = "关闭串口";

			}
		}
		//open serial
		private void btnSPopen_Click(object sender, EventArgs e)
		{
			SavePara();
			ctrlOpenCom();
		}
		//get exist ports
		private void getSpName()
		{
			string[] sp = SerialPort.GetPortNames();
			for (int i = 0; i < sp.Length; i++)
			{
				if (cmbPortName.Items.Contains(sp[i]) == false)
				{
					cmbPortName.Items.Add(sp[i]);
				}

			}
			cmbPortName.Sorted = true;
			//cmbPortName.SelectedIndex = 0;//no nees set default if read para
		}
		//load parameter
		private void LoadPara()
		{
			getSpName();
			ReadWritePara(true);
			//if (errRet == cmbPortName.Text)
			//    cmbPortName.SelectedIndex = 0;
			if (errRet == cmbBaudRate.Text)
				cmbBaudRate.SelectedIndex = 3;
			if (errRet == cmbDataBits.Text)
				cmbDataBits.SelectedIndex = 3;
			if (errRet == cmbParity.Text)
				cmbParity.SelectedIndex = 0;
			if (errRet == cmbStopBits.Text)
				cmbStopBits.SelectedIndex = 0;
		}
		private void CTLSerialPort_Load(object sender, EventArgs e)
		{
			LoadPara();
			if (tag_PortParameter != null)
			{
				groupBox1.Text = tag_PortParameter.tag_name;
				textBox_name.Text = tag_PortParameter.tag_name;
			}
		}
		//send by string
		public bool ctrlSend(string strSend)
		{
			try
			{
				if (IsOpen() == false)
				{
					ctrlOpenCom();
				}
				spTest1.WriteLine(strSend);
			}
			catch
			{
				return false;
			}
			return true;
		}
		//send by byte
		public bool ctrlSend(byte[] by)
		{
			try
			{
				if (IsOpen() == false)
				{
					ctrlOpenCom();
				}
				spTest1.Write(by, 0, by.Length);
			}
			catch
			{
				return false;
			}
			return true;
		}
		//Send button
		private void btnSendPort1_Click(object sender, EventArgs e)
		{
			string strSend = txtSendPort1.Text;
			//this.ctrlSend(strSend);
			this.ctrlSend(HexStringToByteArray(strSend));
		}
		delegate void DelSetTxt(TextBox paTxtBox, object write);
		void SetTxt(TextBox paTxtBox, object write)
		{
			paTxtBox.Text += write.ToString() + "\r\n";
			if (paTxtBox.TextLength > 1000)
			{
				paTxtBox.Clear();
			}

		}
		private void spTest1_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			if (Closing) return;//正在关闭时，忽略
			Listening = true;
			string strShow = "";
			try
			{
				if (spTest1.BytesToRead > 0)
				{
					Thread.Sleep(100);
					strShow = spTest1.ReadExisting();
					dataRecive = strShow;
					spTest1.DiscardInBuffer();
					IsReadedDate = true;
				}
				DelSetTxt del = new DelSetTxt(SetTxt);
				Invoke(del, txtRecPort1, strShow);
				Listening = false;
			}
			catch (Exception)
			{
				Listening = false;
			}
		}
		private void btnClear_Click(object sender, EventArgs e)
		{
			txtRecPort1.Clear();
		}

		#region SaveINI
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		private void writeINI(string section, string key, string val, string path)
		{
			WritePrivateProfileString(section, key, val, path);
		}
		private string GetINI(string section, string key, string errDefault, string path)
		{
			StringBuilder get = new StringBuilder(500);
			GetPrivateProfileString(section, key, errDefault, get, 500, path);
			return get.ToString();
		}
		private string strSavePath = Application.StartupPath + "\\Setting232.ini";
		private void ReadWriteINIcmb(string section, string key, ComboBox cmb, bool IsRead = true)
		{
			if (IsRead)//read
			{
				cmb.Text = GetINI(section, key, "err", strSavePath);
			}
			else//write
			{
				writeINI(section, key, cmb.Text, strSavePath);
			}
		}


		private void ReadWritePara(bool IsRead)
		{

			if (tag_PortParameter != null)
			{
				if (IsRead == true)
				{
					cmbPortName.Text = tag_PortParameter.tag_portName;
					if (cmbPortName.Text == null || cmbPortName.Text.Length < 2)
					{
						cmbPortName.Text = "COM1";
					}
					cmbBaudRate.Text = tag_PortParameter.tag_baudRate.ToString();
					cmbDataBits.Text = tag_PortParameter.tag_databits.ToString();
					cmbParity.SelectedIndex = (int)tag_PortParameter.tag_Parity;
					cmbStopBits.SelectedIndex = (int)tag_PortParameter.tag_stopBits;
					checkBox_enable.Checked = tag_PortParameter.tag_enable;
					textBox_name.Text = tag_PortParameter.tag_name;
				}
				else
				{
					tag_PortParameter.tag_portName = cmbPortName.Text;
					tag_PortParameter.tag_baudRate = Int32.Parse(cmbBaudRate.Text);
					tag_PortParameter.tag_databits = Int32.Parse(cmbDataBits.Text);
					tag_PortParameter.tag_Parity = (Parity)cmbParity.SelectedIndex;
					tag_PortParameter.tag_stopBits = (StopBits)cmbStopBits.SelectedIndex;
					tag_PortParameter.tag_DefintRet = txtSendPort1.Text;
					tag_PortParameter.tag_enable = checkBox_enable.Checked;
					tag_PortParameter.tag_name = textBox_name.Text;

				}
			}

		}
		public void SavePara()
		{
			if (IsOpen() == false)
			{
				ReadWritePara(false);
			}
			else
			{
				ReadWritePara(true);
			}
		}
		#endregion

		#region communicate para property
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override string Text
		{
			get
			{
				return groupBox1.Text;
			}
			set
			{
				groupBox1.Text = value;
			}
		}


		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string pPortName
		{
			get
			{
				return cmbPortName.Text;
			}
			set
			{
				cmbPortName.Text = value;
			}
		}
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string pBaudRate
		{
			get
			{
				return cmbBaudRate.Text;
			}
			set
			{
				cmbBaudRate.Text = value;
			}
		}
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string pDataBits
		{
			get
			{
				return cmbDataBits.Text;
			}
			set
			{
				cmbDataBits.Text = value;
			}
		}
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string pParity
		{
			get
			{
				return cmbParity.Text;
			}
			set
			{
				cmbParity.Text = value;
			}
		}
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public string pStopBits
		{
			get
			{
				return cmbStopBits.Text;
			}
			set
			{
				cmbStopBits.Text = value;
			}
		}
		#endregion
		private static byte[] HexStringToByteArray(string s)
		{
			s = s.Replace(" ", "");
			byte[] buffer = new byte[s.Length / 2];
			for (int i = 0; i < s.Length; i += 2)
				buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
			return buffer;
		}

	}
}
