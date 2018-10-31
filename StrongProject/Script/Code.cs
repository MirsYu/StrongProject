using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Folding;
using ICSharpCode.AvalonEdit.Highlighting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Threading;

namespace StrongProject
{
	public partial class Code : Form
	{
		FoldingManager foldingManager;
		object foldingStrategy;
		TextEditor textEditor;
		CompletionWindow completionWindow;

		public Code()
		{
			InitializeComponent();
			// Add WPF Control
			CodeEditor control = new CodeEditor();
			ElementHost host = new ElementHost();
			host.Top = 10;
			host.Left = 10;
			host.Width = this.Width - 30;
			host.Height = this.Height - 80;
			host.Child = control;
			this.Controls.Add(host);

			textEditor = control.codeEditor;

			DispatcherTimer foldingUpdateTimer = new DispatcherTimer();

			foldingUpdateTimer.Interval = TimeSpan.FromSeconds(2);
			foldingUpdateTimer.Tick += delegate { UpdateFoldings(); };
			foldingUpdateTimer.Start();
			textEditor.TextArea.TextEntered += textEditor_TextArea_TextEntered;

			textEditor.Load(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\StrongProject\\ScrewTest.cs");
			textEditor.SyntaxHighlighting =
				HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(AppDomain.CurrentDomain.BaseDirectory + "..\\..\\StrongProject\\ScrewTest.cs"));
			textEditor.TextArea.IndentationStrategy = new ICSharpCode.AvalonEdit.Indentation.CSharp.CSharpIndentationStrategy(textEditor.Options);
			textEditor.Options.ShowSpaces = true;
			textEditor.Options.ShowTabs = true;
			foldingStrategy = new BraceFoldingStrategy();
			foldingManager = FoldingManager.Install(textEditor.TextArea);
			UpdateFoldings();
		}

		private void Code_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.Hide();
			e.Cancel = true;
		}

		void UpdateFoldings()
		{
			if (foldingStrategy is BraceFoldingStrategy)
			{
				((BraceFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, textEditor.Document);
			}
			if (foldingStrategy is XmlFoldingStrategy)
			{
				((XmlFoldingStrategy)foldingStrategy).UpdateFoldings(foldingManager, textEditor.Document);
			}
		}

		void textEditor_TextArea_TextEntered(object sender, TextCompositionEventArgs e)
		{
			if (e.Text == ".")
			{
				completionWindow = new CompletionWindow(textEditor.TextArea);
				IList<ICompletionData> data = completionWindow.CompletionList.CompletionData;
				data.Add(new MyCompletionData("Test"));
				completionWindow.Show();
				completionWindow.Closed += delegate
				{
					completionWindow = null;
				};
			}
		}

		private void button_Build_Click(object sender, EventArgs e)
		{
			// 将要被编译和执行的代码读入并以字符串方式保存
			// 声明CSharpCodeProvider对象实例
			// 调用CSharpCodeProvider实例的CompileAssemblyFromSource方法编译
			// roslyn

			// CSScript

			//https://www.cnblogs.com/foreachlife/p/ciiproslyn.html
		}
	}
}
