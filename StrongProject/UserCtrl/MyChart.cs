using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StrongProject
{
	public partial class MyChart : UserControl
	{
		public MyChart()
		{
			InitializeComponent();
		}

		#region property

		public class LegendsPosition
		{
			public LegendsPosition()
			{
			}
			public LegendsPosition(bool bAuto, int iHeight, int iWidth, int iX, int iY)
			{
				auto = bAuto;
				height = iHeight;
				width = iWidth;
				x = iX;
				y = iY;
			}
			private bool auto = true;
			[Description("元素自动定位标志"), Category("用户新增属性"), Browsable(true)]
			public bool Auto
			{
				get
				{
					return auto;
				}
				set
				{
					auto = value;
				}
			}

			private int height = 15;
			[Description("元素高度。如果希望设置此属性，请将“Auto”属性设置为False"), Category("用户新增属性"), Browsable(true)]
			public int Height
			{
				get
				{
					return height;
				}
				set
				{
					height = value;
				}
			}

			private int width = 25;
			[Description("元素宽度。如果希望设置此属性，请将“Auto”属性设置为False"), Category("用户新增属性"), Browsable(true)]
			public int Width
			{
				get
				{
					return width;
				}
				set
				{
					width = value;
				}
			}

			private int x = 3;
			[Description("元素左侧位置。如果希望设置此属性，请将“Auto”属性设置为False"), Category("用户新增属性"), Browsable(true)]
			public int X
			{
				get
				{
					return x;
				}
				set
				{
					x = value;
				}
			}

			private int y = 3;
			[Description("元素顶部位置。如果希望设置此属性，请将“Auto”属性设置为False"), Category("用户新增属性"), Browsable(true)]
			public int Y
			{
				get
				{
					return y;
				}
				set
				{
					y = value;
				}
			}
		}

		private LegendsPosition legendsPosition = new LegendsPosition();
		[Description("Legends图表图例矩形位置"), Category("用户新增属性"), Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content), TypeConverter(typeof(ExpandableObjectConverter))]
		public LegendsPosition LegendPosition
		{
			get
			{
				return legendsPosition;
			}
			set
			{
				legendsPosition = value;
			}
		}


		public class ChartAreasPosition
		{
			private bool auto = true;
			[Description("元素自动定位标志"), Category("用户新增属性"), Browsable(true)]
			public bool Auto
			{
				get
				{
					return auto;
				}
				set
				{
					auto = value;
				}
			}

			private int height = 92;
			[Description("元素高度。如果希望设置此属性，请将“Auto”属性设置为False"), Category("用户新增属性"), Browsable(true)]
			public int Height
			{
				get
				{
					return height;
				}
				set
				{
					height = value;
				}
			}

			private int width = 72;
			[Description("元素宽度。如果希望设置此属性，请将“Auto”属性设置为False"), Category("用户新增属性"), Browsable(true)]
			public int Width
			{
				get
				{
					return width;
				}
				set
				{
					width = value;
				}
			}

			private int x = 3;
			[Description("元素左侧位置。如果希望设置此属性，请将“Auto”属性设置为False"), Category("用户新增属性"), Browsable(true)]
			public int X
			{
				get
				{
					return x;
				}
				set
				{
					x = value;
				}
			}

			private int y = 3;
			[Description("元素顶部位置。如果希望设置此属性，请将“Auto”属性设置为False"), Category("用户新增属性"), Browsable(true)]
			public int Y
			{
				get
				{
					return y;
				}
				set
				{
					y = value;
				}
			}
		}

		private ChartAreasPosition areasPosition = new ChartAreasPosition();
		[Description("ChartAreas图表区矩形位置"), Category("用户新增属性"), Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Content), TypeConverter(typeof(ExpandableObjectConverter))]
		public ChartAreasPosition ChartAreaPosition
		{
			get
			{
				return areasPosition;
			}
			set
			{
				areasPosition = value;
			}
		}


		private Font legFont = new Font("幼圆", 9F, FontStyle.Regular);
		[Description("Legends图表图例项的文本字体"), Category("用户新增属性"), Browsable(true)]
		public Font LegendsFont
		{
			get
			{
				return legFont;
			}
			set
			{
				legFont = value;
			}
		}

		private Font serFont = new Font("幼圆", 9F, FontStyle.Regular);
		[Description("Series图表序列的文本字体"), Category("用户新增属性"), Browsable(true)]
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

		private string titles;
		[Description("图表标题"), Category("用户新增属性"), Browsable(true)]
		public string Titles
		{
			get
			{
				return titles;
			}
			set
			{
				titles = value;
			}
		}

		private Color legBackColor = Color.White;
		[Description("获取或设置chart控件的Legends区域的背景色"), Category("用户新增属性"), Browsable(true)]
		public Color LegendsBackColor
		{
			get
			{
				return legBackColor;
			}
			set
			{
				legBackColor = value;
			}
		}

		private Color backColor = Color.White;
		[Description("获取或设置chart控件的背景色"), Category("用户新增属性"), Browsable(true)]
		public Color ChartBackColor
		{
			get
			{
				return backColor;
			}
			set
			{
				backColor = value;
			}
		}

		private Color areaBackColor = Color.White;
		[Description("获取或设置chart控件的图表区的背景色"), Category("用户新增属性"), Browsable(true)]
		public Color ChartAreaBackColor
		{
			get
			{
				return areaBackColor;
			}
			set
			{
				areaBackColor = value;
			}
		}

		private int decimals = 2;
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

		private bool isNeedText = false;
		[Description("图表区是否需要数值显示"), Category("用户新增属性"), Browsable(true)]
		public bool IsNeedText
		{
			get
			{
				return isNeedText;
			}
			set
			{
				isNeedText = value;
			}
		}


		private int intAngle = 0;
		[Description("X轴标签角度"), Category("用户新增属性"), Browsable(true)]
		public int IntAngle
		{
			get
			{
				return intAngle;
			}
			set
			{
				intAngle = value;
			}
		}
		#endregion

		private void MyChart_Load(object sender, EventArgs e)
		{
			chart1.Size = this.Size;
			chart1.Location = new Point(0, 0);
			chart1.Legends[0].Position.Auto = legendsPosition.Auto;
			if (!legendsPosition.Auto)
			{
				chart1.Legends[0].Position.Height = legendsPosition.Height;
				chart1.Legends[0].Position.Width = legendsPosition.Width;
				chart1.Legends[0].Position.X = legendsPosition.X;
				chart1.Legends[0].Position.Y = legendsPosition.Y;
			}
			chart1.ChartAreas[0].Position.Auto = areasPosition.Auto;
			if (!areasPosition.Auto)
			{
				chart1.ChartAreas[0].Position.Height = areasPosition.Height;
				chart1.ChartAreas[0].Position.Width = areasPosition.Width;
				chart1.ChartAreas[0].Position.X = areasPosition.X;
				chart1.ChartAreas[0].Position.Y = areasPosition.Y;
			}
			chart1.Titles.Add(titles);
			chart1.Titles[0].Text = titles;
			chart1.BackColor = backColor;
			chart1.ChartAreas[0].BackColor = areaBackColor;
			chart1.Legends[0].BackColor = legBackColor;
			chart1.ChartAreas[0].AxisX.LabelStyle.Angle = intAngle;
			for (int i = 0; i < chart1.ChartAreas[0].Axes.Count(); i++)
			{
				chart1.ChartAreas[0].Axes[i].LabelStyle.Font = serFont;
			}
		}

		/// <summary>
		/// 绘制饼状图，xName饼状图各块名称，yValue各块值，color各块颜色
		/// </summary>
		public void DrawMyChart(string[] xName, int[] yValue, Color[] color)
		{
			chart1.Series[0].ChartType = SeriesChartType.Pie;
			string[] vX = new string[xName.Length];
			if (isNeedText)
			{
				for (int i = 0; i < vX.Length; i++)
				{
					vX[i] = xName[i] + "\r\n" + GetPercent(yValue[i], yValue);
				}
			}
			chart1.Series[0].Points.DataBindXY(vX, yValue);
			for (int i = 0; i < vX.Length; i++)
			{
				chart1.Series[0].Points[i].Color = color[i];
				chart1.Series[0].Points[i].Font = serFont;
				chart1.Series[0].Points[i].LegendText = xName[i];
			}
			chart1.Legends[0].Font = legFont;
		}

		/// <summary>
		/// 绘制柱状图或折线图等，seriesName图表序列名称，type图表序列类型，xName,X轴自定义标签名字，yValue各项的Y值,多维数组，color各图列颜色，isNeedY2是否启用第二个Y轴(仅在有折线图时有用)
		/// </summary>
		public void DrawMyChart(string[] seriesName, SeriesChartType[] type, string[] xName, double[,] yValue, Color[] color, bool isNeedY2 = false)
		{
			double Max = 0;
			chart1.Series.Clear();
			chart1.Legends[0].Font = legFont;
			chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
			chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
			for (int i = 0; i < seriesName.Count(); i++)
			{
				chart1.Series.Add(seriesName[i]);
				chart1.Series[i].ChartType = type[i];
				if (isNeedText)
				{
					chart1.Series[i].IsValueShownAsLabel = true;
					//chart1.Series[i].Label = "#VAL";
				}
				chart1.Series[i].ToolTip = "#VAL";
				chart1.Series[i].Color = color[i];
				chart1.Series[i].Font = serFont;
			}
			for (int i = 0; i < xName.Count(); i++)
			{
				for (int j = 0; j < seriesName.Count(); j++)
				{
					if (chart1.Series[j].ChartType == SeriesChartType.Line)
					{
						if (isNeedY2)
						{
							continue;
						}
					}
					Max = yValue[i, j] > Max ? yValue[i, j] : Max;
				}
			}
			for (int i = 0; i < seriesName.Count(); i++)
			{
				if (chart1.Series[i].ChartType == SeriesChartType.Line)
				{
					if (isNeedY2)
					{
						chart1.Series[i].YAxisType = AxisType.Secondary;
						chart1.ChartAreas[0].AxisY2.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
						chart1.ChartAreas[0].AxisY2.Maximum = 100;
						if (isNeedText)
						{
							chart1.ChartAreas[0].AxisY.Maximum = 1.8 * Max;
						}
						else
						{
							chart1.ChartAreas[0].AxisY.Maximum = 1.2 * Max;
						}
					}
					chart1.Series[i].MarkerStyle = MarkerStyle.Square;
				}
			}
			chart1.ChartAreas[0].Axes[0].CustomLabels.Clear();
			for (int i = 0; i < xName.Count(); i++)
			{
				CustomLabel cLab = new CustomLabel();
				cLab.Text = xName[i];
				cLab.ToPosition = 2 + i * 2;
				chart1.ChartAreas[0].Axes[0].CustomLabels.Add(cLab);//x坐标轴刻度标签
			}
			for (int i = 0; i < yValue.GetLength(0); i++)
			{
				for (int j = 0; j < seriesName.Count(); j++)
				{
					if (yValue[i, j].ToString().Contains("."))
					{
						chart1.Series[j].Points.Add(new DataPoint(i + 1, yValue[i, j].ToString(string.Format("f{0}", decimals))));
					}
					else
					{
						chart1.Series[j].Points.Add(new DataPoint(i + 1, yValue[i, j]));
					}
				}
			}
			//chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
		}

		/// <summary>
		/// 绘制柱状图或折线图等，seriesName图表序列名称，type图表序列类型，xName,X轴自定义标签名字，yValue各项的Y值,多维数组，color各图列颜色，isNeedY2是否启用第二个Y轴(仅在有折线图时有用)
		/// </summary>
		public void DrawMyChart(string seriesName, SeriesChartType type, string[] xName, double[] yValue, Color color)
		{
			double Max = 0;
			chart1.Series.Clear();
			chart1.Legends[0].Font = legFont;
			chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.NotSet;
			chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;

			chart1.Series.Add(seriesName);
			chart1.Series[0].ChartType = type;
			if (isNeedText)
			{
				chart1.Series[0].IsValueShownAsLabel = true;
				//chart1.Series[i].Label = "#VAL";
			}
			chart1.Series[0].ToolTip = "#VAL";
			chart1.Series[0].Color = color;
			chart1.Series[0].Font = serFont;
			if (type == SeriesChartType.Line)
			{
				chart1.Series[0].MarkerStyle = MarkerStyle.Square;
			}
			for (int i = 0; i < xName.Count(); i++)
			{
				Max = yValue[i] > Max ? yValue[i] : Max;
			}
			chart1.ChartAreas[0].Axes[0].CustomLabels.Clear();
			for (int i = 0; i < xName.Count(); i++)
			{
				CustomLabel cLab = new CustomLabel();
				cLab.Text = xName[i];
				cLab.ToPosition = 2 + 2 * i;
				chart1.ChartAreas[0].Axes[0].CustomLabels.Add(cLab);//x坐标轴刻度标签
			}
			for (int i = 0; i < yValue.Count(); i++)
			{
				if (yValue[i].ToString().Contains("."))
				{
					chart1.Series[0].Points.Add(new DataPoint(i + 1, yValue[i].ToString(string.Format("f{0}", decimals))));
				}
				else
				{
					chart1.Series[0].Points.Add(new DataPoint(i + 1, yValue[i]));
				}

			}
			//chart1.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
		}


		/// <summary>
		/// 获取当前项在数组中所占百分比，val当前值，arVal数组
		/// </summary>
		private string GetPercent(double val, int[] arVal)
		{
			double sum = 0;
			foreach (var item in arVal)
			{
				sum += item;
			}
			return string.Format("({0}%)", (100 * val / sum).ToString(string.Format("f{0}", decimals)));
		}



	}
}
