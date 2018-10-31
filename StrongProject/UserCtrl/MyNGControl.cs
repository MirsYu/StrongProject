using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace StrongProject
{
	public partial class MyNGControl : UserControl
	{
		public MyNGControl()
		{
			InitializeComponent();
		}
		Brush brush;//中间区域画刷
		float R;//圆环最外半径
		float r;//圆环最内半径
		float interval;//圆环间隔
		string[] XName;
		double[] YPercent;
		Color[] YColor;
		string[] YValue;

		#region property

		private Font serFont = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular);
		[Description("图表图例项的文本字体"), Category("用户新增属性"), Browsable(true)]
		public Font SeriesFont
		{
			get
			{
				return serFont;
			}
			set
			{
				serFont = value;
			}
		}

		private int decimals = 1;
		[Description("百分比的小数点位数"), Category("用户新增属性"), Browsable(true)]
		public int Decimals
		{
			get
			{
				return decimals;
			}
			set
			{
				decimals = value;
			}
		}

		#endregion

		private void MyNGControl_Load(object sender, EventArgs e)
		{
			xxx();
		}
		private void xxx()
		{
			this.Size = new Size(this.Size.Width, 2 * this.Size.Width / 3);
			brush = new SolidBrush(Color.White);
		}
		/// <summary>
		/// 绘制NG环形图，xName扇形名称数组，yPercent扇形值数组，color扇形颜色数组
		/// </summary>
		public void DrawRing(string[] xName, double[] yPercent, string[] yValue, Color[] color)
		{
			XName = xName;
			YPercent = yPercent;
			YValue = yValue;
			YColor = color;
			DrawRing();
		}

		private void DrawRing()
		{
			if (XName == null || YPercent == null || YColor == null)
			{
				return;
			}
			try
			{
				R = this.Width / 2;
				r = R / 3;
				interval = (float)(R - r) / (10 * XName.Count());
				BufferedGraphicsContext current = BufferedGraphicsManager.Current; //(1)
				BufferedGraphics bg;
				bg = current.Allocate(this.CreateGraphics(), this.DisplayRectangle); //(2)
				Graphics gra = bg.Graphics;//(3)
				gra.Clear(this.BackColor);
				Brush[] brush1 = new Brush[XName.Count()];//画刷数组，用于填充扇形  
				for (int i = 0; i < XName.Count(); i++)
				{
					brush1[i] = new SolidBrush(YColor[i]);
					gra.FillPie(brush1[i], this.Width / 2 - R + 10 * i * interval, this.Height - r - R + 10 * i * interval, 2 * (R - 10 * i * interval), 2 * (R - 10 * i * interval), 180, Convert.ToInt32(1.8 * YPercent[i]));
					brush = new SolidBrush(Global.WorkVar.SRGreen);
					gra.FillPie(brush, this.Width / 2 - R + 10 * i * interval, this.Height - r - R + 10 * i * interval, 2 * (R - 10 * i * interval), 2 * (R - 10 * i * interval), 180 + Convert.ToInt32(1.8 * YPercent[i]), 180 - Convert.ToInt32(1.8 * YPercent[i]));
					gra.FillPie(new SolidBrush(this.BackColor), this.Width / 2 - R + (10 * i + 9) * interval, this.Height - r - R + (10 * i + 9) * interval, 2 * (R - (10 * i + 9) * interval), 2 * (R - (10 * i + 9) * interval), 180, 180);
					string drawString = XName[i] + "\r\n" + (YPercent[i]).ToString(string.Format("f{0}", decimals)) + "%";
					gra.DrawString(drawString, serFont, new SolidBrush(Color.Black), this.Width / 2 - R + 10 * i * interval, this.Height - r);
					drawString = YValue[i];
					gra.DrawString(drawString, serFont, new SolidBrush(Color.Black), this.Width - (9 + 10 * i) * interval, this.Height - r);
				}
				brush = new SolidBrush(Global.WorkVar.SRRed);
				gra.FillPie(brush, this.Width / 2 - r, this.Height - 2 * r, 2 * r, 2 * r, 0, 360);
				gra.DrawString("NG", new Font("幼圆", 14F, FontStyle.Bold), new SolidBrush(Color.Black), this.Width / 2 - 20, this.Height - r - 12);

				bg.Render();//(4)
				bg.Dispose();//(5)
			}
			catch
			{ }
		}

		private void MyNGControl_Paint(object sender, PaintEventArgs e)
		{
			xxx();
			DrawRing();
		}
	}
}
