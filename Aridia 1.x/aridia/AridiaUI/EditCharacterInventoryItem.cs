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

using com.huguesjohnson.aridia.MegaDriveIO;

namespace com.huguesjohnson.aridia.ui
{
	/// <summary>
	/// Summary description for EditCharacterInventoryItem.
	/// </summary>
	public class EditCharacterInventoryItem : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label labelHexValue;
		private System.Windows.Forms.TextBox textBoxHexValue;
		private System.Windows.Forms.Label labelItem;
		private System.Windows.Forms.ComboBox comboBoxItem;
		private System.Windows.Forms.ComboBox comboBoxWhereEquipped;
		private System.Windows.Forms.Label labelWhereEquipped;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.CheckBox checkBoxEquipped;

		private bool cancel;
		/// <summary>
		/// Returns true if the dialog was cancelled or closed via the 'X' button.
		/// </summary>
		public bool Cancel
		{
			get{ return(this.cancel); }
			set{ this.cancel=value; }
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="item">The item to edit, set this to null to start with a blank item.</param>
		public EditCharacterInventoryItem(PS3Item item)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//set cancel to true by default
			this.Cancel=true;
			//load lists
			AridiaUtils.loadLookupValues(this.comboBoxItem,"Item-Codes");
			AridiaUtils.loadLookupValues(this.comboBoxWhereEquipped,"Equip-Codes");
			//set values
			if(item==null)
			{
				this.comboBoxItem.SelectedIndex=0;
				this.comboBoxWhereEquipped.SelectedIndex=0;
				this.comboBoxWhereEquipped.Enabled=false;
				this.checkBoxEquipped.Checked=false;
			}
			else
			{
				this.textBoxHexValue.Text=item.hexString;
				AridiaUtils.setComboBoxSelection(this.comboBoxItem,item.itemLookup.IntValue);
				if(item.isEquipped)
				{
					AridiaUtils.setComboBoxSelection(this.comboBoxWhereEquipped,item.whereEquipped.IntValue);
					this.checkBoxEquipped.Checked=true;
				}
				else
				{
					this.comboBoxWhereEquipped.SelectedIndex=0;
					this.comboBoxWhereEquipped.Enabled=false;
					this.checkBoxEquipped.Checked=false;
				}
			}
		}

		private void buildHexString()
		{
			LookupValue selectedItem;
			char[] hexChars=new char[4];
			//equipped and where equipped
			if(this.checkBoxEquipped.Checked)
			{
				hexChars[0]='8';
				selectedItem=(LookupValue)this.comboBoxWhereEquipped.SelectedItem;
				hexChars[3]=(char)selectedItem.IntValue.ToString("X").ToCharArray().GetValue(0);
			}
			else
			{
				hexChars[0]='0';
				hexChars[3]='0';
			}
			//item code
			selectedItem=(LookupValue)this.comboBoxItem.SelectedItem;
			String hexString=selectedItem.IntValue.ToString("X");
			if(hexString.Length==1)
			{
				hexString="0"+hexString;
			}
			char[] itemArray=hexString.ToCharArray();
			hexChars[1]=itemArray[0];
			hexChars[2]=itemArray[1];
			//set the textbox
			this.textBoxHexValue.Text=new String(hexChars);
		}

		/// <summary>
		/// Returns the PS3Item represented by the values in the dialog.
		/// </summary>
		/// <returns>The PS3Item represented by the values in the dialog.</returns>
		public PS3Item getItem()
		{
			PS3Item item=new PS3Item();
			item.hexString=this.textBoxHexValue.Text;
			item.isEquipped=this.checkBoxEquipped.Checked;
			item.itemLookup=(LookupValue)this.comboBoxItem.SelectedItem;
			item.whereEquipped=(LookupValue)this.comboBoxWhereEquipped.SelectedItem;
			return(item);
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EditCharacterInventoryItem));
			this.labelHexValue = new System.Windows.Forms.Label();
			this.textBoxHexValue = new System.Windows.Forms.TextBox();
			this.labelItem = new System.Windows.Forms.Label();
			this.comboBoxItem = new System.Windows.Forms.ComboBox();
			this.comboBoxWhereEquipped = new System.Windows.Forms.ComboBox();
			this.labelWhereEquipped = new System.Windows.Forms.Label();
			this.buttonOK = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.checkBoxEquipped = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// labelHexValue
			// 
			this.labelHexValue.Location = new System.Drawing.Point(8, 16);
			this.labelHexValue.Name = "labelHexValue";
			this.labelHexValue.Size = new System.Drawing.Size(96, 20);
			this.labelHexValue.TabIndex = 0;
			this.labelHexValue.Text = "Hex Value:";
			this.labelHexValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// textBoxHexValue
			// 
			this.textBoxHexValue.Location = new System.Drawing.Point(112, 16);
			this.textBoxHexValue.Name = "textBoxHexValue";
			this.textBoxHexValue.ReadOnly = true;
			this.textBoxHexValue.Size = new System.Drawing.Size(144, 20);
			this.textBoxHexValue.TabIndex = 1;
			this.textBoxHexValue.Text = "";
			// 
			// labelItem
			// 
			this.labelItem.Location = new System.Drawing.Point(8, 40);
			this.labelItem.Name = "labelItem";
			this.labelItem.Size = new System.Drawing.Size(96, 20);
			this.labelItem.TabIndex = 2;
			this.labelItem.Text = "Item:";
			this.labelItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxItem
			// 
			this.comboBoxItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxItem.Location = new System.Drawing.Point(112, 40);
			this.comboBoxItem.Name = "comboBoxItem";
			this.comboBoxItem.Size = new System.Drawing.Size(144, 21);
			this.comboBoxItem.TabIndex = 3;
			this.comboBoxItem.SelectedValueChanged += new System.EventHandler(this.comboBoxItem_SelectedValueChanged);
			// 
			// comboBoxWhereEquipped
			// 
			this.comboBoxWhereEquipped.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxWhereEquipped.Location = new System.Drawing.Point(112, 80);
			this.comboBoxWhereEquipped.Name = "comboBoxWhereEquipped";
			this.comboBoxWhereEquipped.Size = new System.Drawing.Size(144, 21);
			this.comboBoxWhereEquipped.TabIndex = 5;
			this.comboBoxWhereEquipped.SelectedValueChanged += new System.EventHandler(this.comboBoxWhereEquipped_SelectedValueChanged);
			// 
			// labelWhereEquipped
			// 
			this.labelWhereEquipped.Location = new System.Drawing.Point(8, 80);
			this.labelWhereEquipped.Name = "labelWhereEquipped";
			this.labelWhereEquipped.Size = new System.Drawing.Size(96, 20);
			this.labelWhereEquipped.TabIndex = 4;
			this.labelWhereEquipped.Text = "Where Equipped:";
			this.labelWhereEquipped.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// buttonOK
			// 
			this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonOK.Location = new System.Drawing.Point(136, 120);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(56, 24);
			this.buttonOK.TabIndex = 6;
			this.buttonOK.Text = "&OK";
			this.buttonOK.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.buttonOK_KeyPress);
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonCancel.Location = new System.Drawing.Point(200, 120);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(56, 24);
			this.buttonCancel.TabIndex = 7;
			this.buttonCancel.Text = "&Cancel";
			this.buttonCancel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.buttonCancel_KeyPress);
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// checkBoxEquipped
			// 
			this.checkBoxEquipped.Location = new System.Drawing.Point(112, 64);
			this.checkBoxEquipped.Name = "checkBoxEquipped";
			this.checkBoxEquipped.Size = new System.Drawing.Size(144, 16);
			this.checkBoxEquipped.TabIndex = 4;
			this.checkBoxEquipped.Text = "Equipped";
			this.checkBoxEquipped.CheckedChanged += new System.EventHandler(this.checkBoxEquipped_CheckedChanged);
			// 
			// EditCharacterInventoryItem
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(266, 151);
			this.Controls.Add(this.checkBoxEquipped);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonOK);
			this.Controls.Add(this.comboBoxWhereEquipped);
			this.Controls.Add(this.labelWhereEquipped);
			this.Controls.Add(this.comboBoxItem);
			this.Controls.Add(this.labelItem);
			this.Controls.Add(this.textBoxHexValue);
			this.Controls.Add(this.labelHexValue);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditCharacterInventoryItem";
			this.Text = "Inventory Item";
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonCancel_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.Close();
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			this.buttonCancel_KeyPress(sender,null);
		}

		private void buttonOK_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.Cancel=false;
			this.Close();
		}

		private void buttonOK_Click(object sender, System.EventArgs e)
		{
			this.buttonOK_KeyPress(sender,null);
		}

		private void comboBoxItem_SelectedValueChanged(object sender, System.EventArgs e)
		{
			this.buildHexString();
		}

		private void comboBoxWhereEquipped_SelectedValueChanged(object sender, System.EventArgs e)
		{
			this.buildHexString();
		}

		private void checkBoxEquipped_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBoxEquipped.Checked)
			{
				this.comboBoxWhereEquipped.Enabled=true;
			}
			else
			{
				this.comboBoxWhereEquipped.Enabled=false;
				this.comboBoxWhereEquipped.SelectedIndex=0;
			}
			this.buildHexString();
		}
	}
}
