using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace StrongProject
{
	public partial class WebControl : UserControl
	{
		public WebControl()
		{
			InitializeComponent();
			this.webBrowser_NG.ScriptErrorsSuppressed = true;
		}

		public void RefreshURL(Uri URL)
		{
			this.webBrowser_NG.Navigate(URL);
			this.webBrowser_NG.Refresh();
		}
	}
}
