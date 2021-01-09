/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2021 Hugues Johnson

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
	/// Summary description for ItemEditingLimitations.
	/// </summary>
	public class ItemEditingLimitations : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label labelPleaseAllow;
		private System.Windows.Forms.TextBox textBoxLimitations;
		private System.Windows.Forms.Button buttonOK;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ItemEditingLimitations()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.buttonOK.Focus();
			this.textBoxLimitations.SelectionLength=0;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemEditingLimitations));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.labelPleaseAllow = new System.Windows.Forms.Label();
            this.textBoxLimitations = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(10, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(57, 74);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // labelPleaseAllow
            // 
            this.labelPleaseAllow.Location = new System.Drawing.Point(77, 37);
            this.labelPleaseAllow.Name = "labelPleaseAllow";
            this.labelPleaseAllow.Size = new System.Drawing.Size(249, 37);
            this.labelPleaseAllow.TabIndex = 1;
            this.labelPleaseAllow.Text = "Please allow me to explain some known issues and limitations of item editing:";
            // 
            // textBoxLimitations
            // 
            this.textBoxLimitations.Location = new System.Drawing.Point(10, 83);
            this.textBoxLimitations.Multiline = true;
            this.textBoxLimitations.Name = "textBoxLimitations";
            this.textBoxLimitations.ReadOnly = true;
            this.textBoxLimitations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLimitations.Size = new System.Drawing.Size(316, 203);
            this.textBoxLimitations.TabIndex = 2;
            this.textBoxLimitations.Text = resources.GetString("textBoxLimitations.Text");
            // 
            // buttonOK
            // 
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(230, 295);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(96, 37);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK, I Got It";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // ItemEditingLimitations
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(282, 295);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.textBoxLimitations);
            this.Controls.Add(this.labelPleaseAllow);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ItemEditingLimitations";
            this.Text = "Item Editing Limitations";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
