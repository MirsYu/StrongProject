using System;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Document;
using ICSharpCode.AvalonEdit.Editing;
namespace StrongProject
{
	public class MyCompletionData : ICompletionData
	{
		public MyCompletionData(string text)
		{
			this.Text = text;
		}

		public System.Windows.Media.ImageSource Image
		{
			get { return null; }
		}

		public string Text { get; private set; }

		public object Content
		{
			get { return this.Text; }
		}

		public object Description
		{
			get { return "Description for " + this.Text; }
		}

		public double Priority { get { return 0; } }

		public void Complete(TextArea textArea, ISegment completionSegment, EventArgs insertionRequestEventArgs)
		{
			textArea.Document.Replace(completionSegment, this.Text);
		}
	}
}
