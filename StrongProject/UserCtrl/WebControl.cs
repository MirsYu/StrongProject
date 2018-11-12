using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CefSharp;
using CefSharp.WinForms;

namespace StrongProject
{
	[System.Runtime.InteropServices.ComVisibleAttribute(true)]
	public partial class WebControl : UserControl
	{
		public WebControl()
		{
			InitializeComponent();
		}

		public ChromiumWebBrowser chromeBrowser;

		public void InitializeChromium(string url)
		{
			chromeBrowser = new ChromiumWebBrowser(url);
			this.Controls.Add(chromeBrowser);
			chromeBrowser.Dock = DockStyle.Fill;
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}
