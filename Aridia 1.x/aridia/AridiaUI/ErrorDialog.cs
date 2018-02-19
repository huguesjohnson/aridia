/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2018 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.huguesjohnson.aridia.ui
{
	/// <summary>
	/// Summary description for ErrorDialog.
	/// </summary>
	public class ErrorDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label labelStackTrace;
		private System.Windows.Forms.Label labelYouTriedTo;
		private System.Windows.Forms.Label labelAction;
		private System.Windows.Forms.Label labelNormallyASmartMove;
		private System.Windows.Forms.TextBox textBoxStackTrace;
		private System.Windows.Forms.Button buttonReturn;
		private System.Windows.Forms.Button buttonTerminate;
		private bool endApplication;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ErrorDialog(String action,Exception x)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.endApplication=false;
			this.labelAction.Text=action;
			this.textBoxStackTrace.Text=x.Message+"\n"+x.StackTrace;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ErrorDialog));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelStackTrace = new System.Windows.Forms.Label();
			this.labelYouTriedTo = new System.Windows.Forms.Label();
			this.labelAction = new System.Windows.Forms.Label();
			this.labelNormallyASmartMove = new System.Windows.Forms.Label();
			this.textBoxStackTrace = new System.Windows.Forms.TextBox();
			this.buttonReturn = new System.Windows.Forms.Button();
			this.buttonTerminate = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(128, 64);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// labelStackTrace
			// 
			this.labelStackTrace.Location = new System.Drawing.Point(8, 72);
			this.labelStackTrace.Name = "labelStackTrace";
			this.labelStackTrace.Size = new System.Drawing.Size(104, 24);
			this.labelStackTrace.TabIndex = 1;
			this.labelStackTrace.Text = "Error Stack Trace:";
			// 
			// labelYouTriedTo
			// 
			this.labelYouTriedTo.Location = new System.Drawing.Point(136, 16);
			this.labelYouTriedTo.Name = "labelYouTriedTo";
			this.labelYouTriedTo.Size = new System.Drawing.Size(64, 24);
			this.labelYouTriedTo.TabIndex = 2;
			this.labelYouTriedTo.Text = "You tried to ";
			// 
			// labelAction
			// 
			this.labelAction.Location = new System.Drawing.Point(192, 16);
			this.labelAction.Name = "labelAction";
			this.labelAction.Size = new System.Drawing.Size(280, 24);
			this.labelAction.TabIndex = 3;
			this.labelAction.Text = "<action>";
			// 
			// labelNormallyASmartMove
			// 
			this.labelNormallyASmartMove.Location = new System.Drawing.Point(136, 40);
			this.labelNormallyASmartMove.Name = "labelNormallyASmartMove";
			this.labelNormallyASmartMove.Size = new System.Drawing.Size(344, 24);
			this.labelNormallyASmartMove.TabIndex = 4;
			this.labelNormallyASmartMove.Text = "Normally a smart move, but I\'m afraid an error occured.";
			// 
			// textBoxStackTrace
			// 
			this.textBoxStackTrace.Location = new System.Drawing.Point(8, 96);
			this.textBoxStackTrace.Multiline = true;
			this.textBoxStackTrace.Name = "textBoxStackTrace";
			this.textBoxStackTrace.ReadOnly = true;
			this.textBoxStackTrace.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxStackTrace.Size = new System.Drawing.Size(512, 88);
			this.textBoxStackTrace.TabIndex = 5;
			this.textBoxStackTrace.Text = "";
			// 
			// buttonReturn
			// 
			this.buttonReturn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonReturn.Location = new System.Drawing.Point(264, 192);
			this.buttonReturn.Name = "buttonReturn";
			this.buttonReturn.Size = new System.Drawing.Size(120, 32);
			this.buttonReturn.TabIndex = 7;
			this.buttonReturn.Text = "Return to Aridia";
			this.buttonReturn.Click += new System.EventHandler(this.buttonReturn_Click);
			// 
			// buttonTerminate
			// 
			this.buttonTerminate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonTerminate.Location = new System.Drawing.Point(392, 192);
			this.buttonTerminate.Name = "buttonTerminate";
			this.buttonTerminate.Size = new System.Drawing.Size(120, 32);
			this.buttonTerminate.TabIndex = 8;
			this.buttonTerminate.Text = "End the application";
			this.buttonTerminate.Click += new System.EventHandler(this.buttonTerminate_Click);
			// 
			// ErrorDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(522, 231);
			this.Controls.Add(this.buttonTerminate);
			this.Controls.Add(this.buttonReturn);
			this.Controls.Add(this.textBoxStackTrace);
			this.Controls.Add(this.labelNormallyASmartMove);
			this.Controls.Add(this.labelAction);
			this.Controls.Add(this.labelYouTriedTo);
			this.Controls.Add(this.labelStackTrace);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ErrorDialog";
			this.Text = "Error";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonReturn_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonTerminate_Click(object sender, System.EventArgs e)
		{
			this.endApplication=true;
			this.Close();
		}

		public bool EndApplication
		{
			get
			{
				return(this.endApplication);
			}
		}
	}
}
