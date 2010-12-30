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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ItemEditingLimitations));
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelPleaseAllow = new System.Windows.Forms.Label();
			this.textBoxLimitations = new System.Windows.Forms.TextBox();
			this.buttonOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(8, 8);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 64);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// labelPleaseAllow
			// 
			this.labelPleaseAllow.Location = new System.Drawing.Point(64, 32);
			this.labelPleaseAllow.Name = "labelPleaseAllow";
			this.labelPleaseAllow.Size = new System.Drawing.Size(208, 32);
			this.labelPleaseAllow.TabIndex = 1;
			this.labelPleaseAllow.Text = "Please allow me to explain some known issues and limitations of item editing:";
			// 
			// textBoxLimitations
			// 
			this.textBoxLimitations.Location = new System.Drawing.Point(8, 72);
			this.textBoxLimitations.Multiline = true;
			this.textBoxLimitations.Name = "textBoxLimitations";
			this.textBoxLimitations.ReadOnly = true;
			this.textBoxLimitations.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxLimitations.Size = new System.Drawing.Size(264, 176);
			this.textBoxLimitations.TabIndex = 2;
			this.textBoxLimitations.Text = @"Adding events items, such as gems or parts for Wren, does not make them usable. There are event triggers in the game that activate the item. Although those events can be edited in save state file it has not been determined how to edit them in the ROM image.

There is a pretty signficant bug that is triggered when items are added to a character's initial inventory. Editing items is fine, increasing the total number of items is not. Techniques are stored immediately after items in the ROM adding items causes their values to overflow into techniques. This results in techniques being wildly altered.

There is nothing in this editor to prevent you from doing something like equipping a Monomate on a character's head. Don't be shocked if doing something like that results in a bug.";
			// 
			// buttonOK
			// 
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonOK.Location = new System.Drawing.Point(192, 256);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(80, 32);
			this.buttonOK.TabIndex = 3;
			this.buttonOK.Text = "OK, I Got It";
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// ItemEditingLimitations
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
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
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
