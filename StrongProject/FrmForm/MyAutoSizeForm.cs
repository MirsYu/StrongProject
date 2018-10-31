using Microsoft.VisualBasic.PowerPacks;
using System;
using System.Windows.Forms;

namespace StrongProject
{
	public class MyAutoSizeForm
	{
		private int OldWidth;
		private int OldHeight;
		public double Px;
		private double Py;
		//记录原窗体大小
		public void RecordOldSizeScale(Control ctrl)
		{
			OldWidth = ctrl.Width;
			OldHeight = ctrl.Height;
			RecordOldSize(ctrl);

		}
		//记录原控件大小
		public void RecordOldSize(Control ctl)
		{
			foreach (Control cl in ctl.Controls)
			{
				//记录矩形以及线条大小
				if (cl.GetType().ToString() == "Microsoft.VisualBasic.PowerPacks.ShapeContainer")
				{
					ShapeContainer shape = ((ShapeContainer)(cl));
					for (int i = 0; i < shape.Shapes.Count; i++)
					{
						object control = shape.Shapes.get_Item(i);
						if (control is RectangleShape)
						{
							RectangleShape rectangleShape1 = (RectangleShape)control;
							if (rectangleShape1.Tag == null)
							{
								rectangleShape1.Tag = rectangleShape1.Left + "," + rectangleShape1.Top + "," + rectangleShape1.Width + "," + rectangleShape1.Height;
							}
						}
						else if (control is LineShape)
						{
							LineShape lineSharp1 = (LineShape)control;
							if (lineSharp1.Tag == null)
							{
								lineSharp1.Tag = lineSharp1.X1 + "," + lineSharp1.X2 + "," + lineSharp1.Y1 + "," + lineSharp1.Y2;
							}
						}
					}
					continue;
				}

				if (cl is ListView)
				{
					ListView listV = (ListView)cl;
					for (int i = 0; i < listV.Columns.Count; i++)
					{
						if (listV.Columns[i].Tag == null)
						{
							listV.Columns[i].Tag = listV.Columns[i].Width;
						}
					}
				}

				if (cl.Tag == null)
				{
					//cl.Tag = cl.Left + "," + cl.Top + "," + cl.Width + "," + cl.Height + "," + cl.Font.Size; //放大字体
					cl.Tag = cl.Left + "," + cl.Top + "," + cl.Width + "," + cl.Height;
				}
				if (cl.Controls.Count > 0)  //判断控件里面是否还有控件
					RecordOldSize(cl);


			}

		}
		//计算缩放比例
		public void AutoSizeFormsContrl(Control ctl)
		{
			Px = (double)ctl.Width / OldWidth;
			Py = (double)ctl.Height / OldHeight;
			AutoScaleControl(ctl);
		}

		//自动放大窗体
		public void AutoScaleControl(Control ctl)
		{
			foreach (Control cotrl in ctl.Controls)
			{
				//放大矩形以及线条
				if (cotrl.GetType().ToString() == "Microsoft.VisualBasic.PowerPacks.ShapeContainer")
				{
					ShapeContainer shape = ((ShapeContainer)(cotrl));
					Shape[] shapeln = new Shape[shape.Shapes.Count];
					for (int i = 0; i < shape.Shapes.Count; i++)
					{
						object control = shape.Shapes.get_Item(i);
						if (control is RectangleShape)
						{
							RectangleShape rectangleShape1 = (RectangleShape)control;
							shapeln[i] = rectangleShape1;
							if (rectangleShape1.Tag != null)
							{
								string[] pls1 = (rectangleShape1.Tag.ToString()).Split(',');
								rectangleShape1.Left = (int)(Px * int.Parse(pls1[0]));
								rectangleShape1.Top = (int)(Py * int.Parse(pls1[1]));
								rectangleShape1.Width = (int)(Px * int.Parse(pls1[2]));
								rectangleShape1.Height = (int)(Py * int.Parse(pls1[3]));
							}
						}
						else if (control is LineShape)
						{
							LineShape lineSharp1 = (LineShape)control;
							shapeln[i] = lineSharp1;
							if (lineSharp1.Tag != null)
							{
								string[] pls1 = (lineSharp1.Tag.ToString()).Split(',');
								lineSharp1.X1 = (int)(Px * int.Parse(pls1[0]));
								lineSharp1.X2 = (int)(Px * int.Parse(pls1[1]));

								lineSharp1.Y1 = (int)(Py * int.Parse(pls1[2]));
								lineSharp1.Y2 = (int)(Py * int.Parse(pls1[3]));
							}
						}

					}
					//shape.Shapes.AddRange(shapeln);
					continue;
				}

				//将Panel的滚动条置顶
				if (cotrl is Panel)
				{
					Panel panl = (Panel)cotrl;
					SetPanelScoll(panl);
				}

				if (cotrl is ListView)
				{
					ListView listV = (ListView)cotrl;
					for (int i = 0; i < listV.Columns.Count; i++)
					{
						if (listV.Columns[i].Tag != null)
						{
							listV.Columns[i].Width = (int)(Convert.ToInt16(listV.Columns[i].Tag) * Px);

						}
					}
				}
				if (cotrl.Tag == null)
				{
					continue;
				}
				string p = cotrl.Tag.ToString();
				string[] pdata = p.Split(',');
				cotrl.Left = (int)(Px * int.Parse(pdata[0]));
				cotrl.Top = (int)(Py * int.Parse(pdata[1]));
				cotrl.Width = (int)(Px * int.Parse(pdata[2]));
				cotrl.Height = (int)(Py * int.Parse(pdata[3]));
				//cotrl.Font = new Font(cotrl.Font.Name, float.Parse((float.Parse(pdata[4]) * Math.Min(Px, Py)).ToString()));//放大字体
				if (cotrl.Controls.Count > 0)
					AutoScaleControl(cotrl);

			}


		}

		private void SetPanelScoll(Panel pl)
		{
			if (pl.VerticalScroll.Value > 0)
			{
				pl.VerticalScroll.Value = pl.VerticalScroll.Minimum;

			}
			if (pl.HorizontalScroll.Value > 0)
			{
				pl.HorizontalScroll.Value = pl.HorizontalScroll.Minimum;

			}
		}
	}
}
