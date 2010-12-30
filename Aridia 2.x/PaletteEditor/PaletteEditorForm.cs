/*
PaletteEditor: Dialog to edit a 16 color 9-bit RGB palette
Originally created for Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2010 Hugues Johnson

PaletteEditor is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License version 2 
as published by the Free Software Foundation.

MegaDriveIO is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using com.huguesjohnson.MegaDriveIO;

namespace com.huguesjohnson.PaletteEditor
{
	/// <summary>
	/// Form used to edit a 16-color palette.
	/// </summary>
	public class PaletteEditorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBoxPalette;
		private System.Windows.Forms.RadioButton radioButtonPalette0;
		private System.Windows.Forms.RadioButton radioButtonPalette1;
		private System.Windows.Forms.RadioButton radioButtonPalette2;
		private System.Windows.Forms.RadioButton radioButtonPalette3;
		private System.Windows.Forms.RadioButton radioButtonPalette4;
		private System.Windows.Forms.RadioButton radioButtonPalette5;
		private System.Windows.Forms.RadioButton radioButtonPalette6;
		private System.Windows.Forms.RadioButton radioButtonPalette7;
		private System.Windows.Forms.RadioButton radioButtonPalette15;
		private System.Windows.Forms.RadioButton radioButtonPalette14;
		private System.Windows.Forms.RadioButton radioButtonPalette13;
		private System.Windows.Forms.RadioButton radioButtonPalette12;
		private System.Windows.Forms.RadioButton radioButtonPalette11;
		private System.Windows.Forms.RadioButton radioButtonPalette10;
		private System.Windows.Forms.RadioButton radioButtonPalette9;
		private System.Windows.Forms.RadioButton radioButtonPalette8;
		private System.Windows.Forms.GroupBox groupBoxSelectedEntry;
		private System.Windows.Forms.TrackBar trackBarR;
		private System.Windows.Forms.Label labelR;
		private System.Windows.Forms.Label labelG;
		private System.Windows.Forms.TrackBar trackBarG;
		private System.Windows.Forms.Label labelB;
		private System.Windows.Forms.TrackBar trackBarB;
		private bool saveOrCancelButtonClicked;
		private System.Windows.Forms.Button buttonCancelAndClose;
		private System.Windows.Forms.Button buttonSaveAndClose;
		private int address;
		private Palette palette;
		private MDBinaryRomIO romIO;
		private System.Windows.Forms.RadioButton selectedButton;
		private int selectedPaletteIndex;
		private bool cancelValidation;
		private System.Windows.Forms.Label labelPaletteEntry0IsThe;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Create a new instance of PaletteEditorForm.
		/// </summary>
		/// <param name="romIO">The Mega Drive rom IO to read/write.</param>
		/// <param name="address">The starting address of the palette.</param>
		public PaletteEditorForm(MDBinaryRomIO romIO,int address)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.saveOrCancelButtonClicked=false;
			this.romIO=romIO;
			this.address=address;
			this.palette=romIO.readPalette(address);
			//color the buttons
			this.radioButtonPalette0.BackColor=palette.Entries[0].ToRGB();
			this.radioButtonPalette0.ForeColor=this.getInverseColor(this.radioButtonPalette0.BackColor); 
			this.radioButtonPalette1.BackColor=palette.Entries[1].ToRGB();
			this.radioButtonPalette1.ForeColor=this.getInverseColor(this.radioButtonPalette1.BackColor); 
			this.radioButtonPalette2.BackColor=palette.Entries[2].ToRGB();
			this.radioButtonPalette2.ForeColor=this.getInverseColor(this.radioButtonPalette2.BackColor); 
			this.radioButtonPalette3.BackColor=palette.Entries[3].ToRGB();
			this.radioButtonPalette3.ForeColor=this.getInverseColor(this.radioButtonPalette3.BackColor); 
			this.radioButtonPalette4.BackColor=palette.Entries[4].ToRGB();
			this.radioButtonPalette4.ForeColor=this.getInverseColor(this.radioButtonPalette4.BackColor); 
			this.radioButtonPalette5.BackColor=palette.Entries[5].ToRGB();
			this.radioButtonPalette5.ForeColor=this.getInverseColor(this.radioButtonPalette5.BackColor); 
			this.radioButtonPalette6.BackColor=palette.Entries[6].ToRGB();
			this.radioButtonPalette6.ForeColor=this.getInverseColor(this.radioButtonPalette6.BackColor); 
			this.radioButtonPalette7.BackColor=palette.Entries[7].ToRGB();
			this.radioButtonPalette7.ForeColor=this.getInverseColor(this.radioButtonPalette7.BackColor); 
			this.radioButtonPalette8.BackColor=palette.Entries[8].ToRGB();
			this.radioButtonPalette8.ForeColor=this.getInverseColor(this.radioButtonPalette8.BackColor); 
			this.radioButtonPalette9.BackColor=palette.Entries[9].ToRGB();
			this.radioButtonPalette9.ForeColor=this.getInverseColor(this.radioButtonPalette9.BackColor); 
			this.radioButtonPalette10.BackColor=palette.Entries[10].ToRGB();
			this.radioButtonPalette10.ForeColor=this.getInverseColor(this.radioButtonPalette10.BackColor); 
			this.radioButtonPalette11.BackColor=palette.Entries[11].ToRGB();
			this.radioButtonPalette11.ForeColor=this.getInverseColor(this.radioButtonPalette11.BackColor); 
			this.radioButtonPalette12.BackColor=palette.Entries[12].ToRGB();
			this.radioButtonPalette12.ForeColor=this.getInverseColor(this.radioButtonPalette12.BackColor); 
			this.radioButtonPalette13.BackColor=palette.Entries[13].ToRGB();
			this.radioButtonPalette13.ForeColor=this.getInverseColor(this.radioButtonPalette13.BackColor); 
			this.radioButtonPalette14.BackColor=palette.Entries[14].ToRGB();
			this.radioButtonPalette14.ForeColor=this.getInverseColor(this.radioButtonPalette14.BackColor); 
			this.radioButtonPalette15.BackColor=palette.Entries[15].ToRGB();
			this.radioButtonPalette15.ForeColor=this.getInverseColor(this.radioButtonPalette15.BackColor); 
			//select the first button
			this.radioButtonPalette0.Checked=true;
			radioButtonPalette0_CheckedChanged(null,null);
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PaletteEditorForm));
			this.groupBoxPalette = new System.Windows.Forms.GroupBox();
			this.labelPaletteEntry0IsThe = new System.Windows.Forms.Label();
			this.radioButtonPalette15 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette14 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette13 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette12 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette11 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette10 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette9 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette8 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette7 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette6 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette5 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette4 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette3 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette2 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette1 = new System.Windows.Forms.RadioButton();
			this.radioButtonPalette0 = new System.Windows.Forms.RadioButton();
			this.groupBoxSelectedEntry = new System.Windows.Forms.GroupBox();
			this.labelB = new System.Windows.Forms.Label();
			this.trackBarB = new System.Windows.Forms.TrackBar();
			this.labelG = new System.Windows.Forms.Label();
			this.trackBarG = new System.Windows.Forms.TrackBar();
			this.labelR = new System.Windows.Forms.Label();
			this.trackBarR = new System.Windows.Forms.TrackBar();
			this.buttonCancelAndClose = new System.Windows.Forms.Button();
			this.buttonSaveAndClose = new System.Windows.Forms.Button();
			this.groupBoxPalette.SuspendLayout();
			this.groupBoxSelectedEntry.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackBarB)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarG)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarR)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBoxPalette
			// 
			this.groupBoxPalette.Controls.Add(this.labelPaletteEntry0IsThe);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette15);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette14);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette13);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette12);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette11);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette10);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette9);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette8);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette7);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette6);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette5);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette4);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette3);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette2);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette1);
			this.groupBoxPalette.Controls.Add(this.radioButtonPalette0);
			this.groupBoxPalette.Location = new System.Drawing.Point(8, 8);
			this.groupBoxPalette.Name = "groupBoxPalette";
			this.groupBoxPalette.Size = new System.Drawing.Size(296, 120);
			this.groupBoxPalette.TabIndex = 0;
			this.groupBoxPalette.TabStop = false;
			this.groupBoxPalette.Text = "Palette";
			// 
			// labelPaletteEntry0IsThe
			// 
			this.labelPaletteEntry0IsThe.Location = new System.Drawing.Point(16, 96);
			this.labelPaletteEntry0IsThe.Name = "labelPaletteEntry0IsThe";
			this.labelPaletteEntry0IsThe.Size = new System.Drawing.Size(240, 16);
			this.labelPaletteEntry0IsThe.TabIndex = 17;
			this.labelPaletteEntry0IsThe.Text = "Palette entry 0 is the transparency color";
			// 
			// radioButtonPalette15
			// 
			this.radioButtonPalette15.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette15.Location = new System.Drawing.Point(240, 56);
			this.radioButtonPalette15.Name = "radioButtonPalette15";
			this.radioButtonPalette15.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette15.TabIndex = 16;
			this.radioButtonPalette15.Text = "15";
			this.radioButtonPalette15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette15.CheckedChanged += new System.EventHandler(this.radioButtonPalette15_CheckedChanged);
			// 
			// radioButtonPalette14
			// 
			this.radioButtonPalette14.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette14.Location = new System.Drawing.Point(208, 56);
			this.radioButtonPalette14.Name = "radioButtonPalette14";
			this.radioButtonPalette14.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette14.TabIndex = 15;
			this.radioButtonPalette14.Text = "14";
			this.radioButtonPalette14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette14.CheckedChanged += new System.EventHandler(this.radioButtonPalette14_CheckedChanged);
			// 
			// radioButtonPalette13
			// 
			this.radioButtonPalette13.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette13.Location = new System.Drawing.Point(176, 56);
			this.radioButtonPalette13.Name = "radioButtonPalette13";
			this.radioButtonPalette13.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette13.TabIndex = 14;
			this.radioButtonPalette13.Text = "13";
			this.radioButtonPalette13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette13.CheckedChanged += new System.EventHandler(this.radioButtonPalette13_CheckedChanged);
			// 
			// radioButtonPalette12
			// 
			this.radioButtonPalette12.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette12.Location = new System.Drawing.Point(144, 56);
			this.radioButtonPalette12.Name = "radioButtonPalette12";
			this.radioButtonPalette12.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette12.TabIndex = 13;
			this.radioButtonPalette12.Text = "12";
			this.radioButtonPalette12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette12.CheckedChanged += new System.EventHandler(this.radioButtonPalette12_CheckedChanged);
			// 
			// radioButtonPalette11
			// 
			this.radioButtonPalette11.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette11.Location = new System.Drawing.Point(112, 56);
			this.radioButtonPalette11.Name = "radioButtonPalette11";
			this.radioButtonPalette11.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette11.TabIndex = 12;
			this.radioButtonPalette11.Text = "11";
			this.radioButtonPalette11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette11.CheckedChanged += new System.EventHandler(this.radioButtonPalette11_CheckedChanged);
			// 
			// radioButtonPalette10
			// 
			this.radioButtonPalette10.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette10.Location = new System.Drawing.Point(80, 56);
			this.radioButtonPalette10.Name = "radioButtonPalette10";
			this.radioButtonPalette10.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette10.TabIndex = 11;
			this.radioButtonPalette10.Text = "10";
			this.radioButtonPalette10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette10.CheckedChanged += new System.EventHandler(this.radioButtonPalette10_CheckedChanged);
			// 
			// radioButtonPalette9
			// 
			this.radioButtonPalette9.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette9.Location = new System.Drawing.Point(48, 56);
			this.radioButtonPalette9.Name = "radioButtonPalette9";
			this.radioButtonPalette9.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette9.TabIndex = 10;
			this.radioButtonPalette9.Text = "9";
			this.radioButtonPalette9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette9.CheckedChanged += new System.EventHandler(this.radioButtonPalette9_CheckedChanged);
			// 
			// radioButtonPalette8
			// 
			this.radioButtonPalette8.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette8.Location = new System.Drawing.Point(16, 56);
			this.radioButtonPalette8.Name = "radioButtonPalette8";
			this.radioButtonPalette8.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette8.TabIndex = 9;
			this.radioButtonPalette8.Text = "8";
			this.radioButtonPalette8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette8.CheckedChanged += new System.EventHandler(this.radioButtonPalette8_CheckedChanged);
			// 
			// radioButtonPalette7
			// 
			this.radioButtonPalette7.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette7.Location = new System.Drawing.Point(240, 24);
			this.radioButtonPalette7.Name = "radioButtonPalette7";
			this.radioButtonPalette7.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette7.TabIndex = 8;
			this.radioButtonPalette7.Text = "7";
			this.radioButtonPalette7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette7.CheckedChanged += new System.EventHandler(this.radioButtonPalette7_CheckedChanged);
			// 
			// radioButtonPalette6
			// 
			this.radioButtonPalette6.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette6.Location = new System.Drawing.Point(208, 24);
			this.radioButtonPalette6.Name = "radioButtonPalette6";
			this.radioButtonPalette6.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette6.TabIndex = 7;
			this.radioButtonPalette6.Text = "6";
			this.radioButtonPalette6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette6.CheckedChanged += new System.EventHandler(this.radioButtonPalette6_CheckedChanged);
			// 
			// radioButtonPalette5
			// 
			this.radioButtonPalette5.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette5.Location = new System.Drawing.Point(176, 24);
			this.radioButtonPalette5.Name = "radioButtonPalette5";
			this.radioButtonPalette5.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette5.TabIndex = 6;
			this.radioButtonPalette5.Text = "5";
			this.radioButtonPalette5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette5.CheckedChanged += new System.EventHandler(this.radioButtonPalette5_CheckedChanged);
			// 
			// radioButtonPalette4
			// 
			this.radioButtonPalette4.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette4.Location = new System.Drawing.Point(144, 24);
			this.radioButtonPalette4.Name = "radioButtonPalette4";
			this.radioButtonPalette4.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette4.TabIndex = 5;
			this.radioButtonPalette4.Text = "4";
			this.radioButtonPalette4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette4.CheckedChanged += new System.EventHandler(this.radioButtonPalette4_CheckedChanged);
			// 
			// radioButtonPalette3
			// 
			this.radioButtonPalette3.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette3.Location = new System.Drawing.Point(112, 24);
			this.radioButtonPalette3.Name = "radioButtonPalette3";
			this.radioButtonPalette3.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette3.TabIndex = 4;
			this.radioButtonPalette3.Text = "3";
			this.radioButtonPalette3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette3.CheckedChanged += new System.EventHandler(this.radioButtonPalette3_CheckedChanged);
			// 
			// radioButtonPalette2
			// 
			this.radioButtonPalette2.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette2.Location = new System.Drawing.Point(80, 24);
			this.radioButtonPalette2.Name = "radioButtonPalette2";
			this.radioButtonPalette2.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette2.TabIndex = 3;
			this.radioButtonPalette2.Text = "2";
			this.radioButtonPalette2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette2.CheckedChanged += new System.EventHandler(this.radioButtonPalette2_CheckedChanged);
			// 
			// radioButtonPalette1
			// 
			this.radioButtonPalette1.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette1.Location = new System.Drawing.Point(48, 24);
			this.radioButtonPalette1.Name = "radioButtonPalette1";
			this.radioButtonPalette1.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette1.TabIndex = 2;
			this.radioButtonPalette1.Text = "1";
			this.radioButtonPalette1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette1.CheckedChanged += new System.EventHandler(this.radioButtonPalette1_CheckedChanged);
			// 
			// radioButtonPalette0
			// 
			this.radioButtonPalette0.Appearance = System.Windows.Forms.Appearance.Button;
			this.radioButtonPalette0.Location = new System.Drawing.Point(16, 24);
			this.radioButtonPalette0.Name = "radioButtonPalette0";
			this.radioButtonPalette0.Size = new System.Drawing.Size(32, 32);
			this.radioButtonPalette0.TabIndex = 1;
			this.radioButtonPalette0.Text = "0";
			this.radioButtonPalette0.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.radioButtonPalette0.CheckedChanged += new System.EventHandler(this.radioButtonPalette0_CheckedChanged);
			// 
			// groupBoxSelectedEntry
			// 
			this.groupBoxSelectedEntry.Controls.Add(this.labelB);
			this.groupBoxSelectedEntry.Controls.Add(this.trackBarB);
			this.groupBoxSelectedEntry.Controls.Add(this.labelG);
			this.groupBoxSelectedEntry.Controls.Add(this.trackBarG);
			this.groupBoxSelectedEntry.Controls.Add(this.labelR);
			this.groupBoxSelectedEntry.Controls.Add(this.trackBarR);
			this.groupBoxSelectedEntry.Location = new System.Drawing.Point(8, 136);
			this.groupBoxSelectedEntry.Name = "groupBoxSelectedEntry";
			this.groupBoxSelectedEntry.Size = new System.Drawing.Size(296, 168);
			this.groupBoxSelectedEntry.TabIndex = 1;
			this.groupBoxSelectedEntry.TabStop = false;
			this.groupBoxSelectedEntry.Text = "Selected Entry";
			// 
			// labelB
			// 
			this.labelB.Location = new System.Drawing.Point(16, 120);
			this.labelB.Name = "labelB";
			this.labelB.Size = new System.Drawing.Size(40, 24);
			this.labelB.TabIndex = 5;
			this.labelB.Text = "Blue";
			this.labelB.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarB
			// 
			this.trackBarB.LargeChange = 1;
			this.trackBarB.Location = new System.Drawing.Point(72, 112);
			this.trackBarB.Maximum = 7;
			this.trackBarB.Name = "trackBarB";
			this.trackBarB.Size = new System.Drawing.Size(208, 42);
			this.trackBarB.TabIndex = 4;
			this.trackBarB.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
			// 
			// labelG
			// 
			this.labelG.Location = new System.Drawing.Point(16, 72);
			this.labelG.Name = "labelG";
			this.labelG.Size = new System.Drawing.Size(40, 24);
			this.labelG.TabIndex = 3;
			this.labelG.Text = "Green";
			this.labelG.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarG
			// 
			this.trackBarG.LargeChange = 1;
			this.trackBarG.Location = new System.Drawing.Point(72, 64);
			this.trackBarG.Maximum = 7;
			this.trackBarG.Name = "trackBarG";
			this.trackBarG.Size = new System.Drawing.Size(208, 42);
			this.trackBarG.TabIndex = 2;
			this.trackBarG.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
			// 
			// labelR
			// 
			this.labelR.Location = new System.Drawing.Point(16, 32);
			this.labelR.Name = "labelR";
			this.labelR.Size = new System.Drawing.Size(40, 24);
			this.labelR.TabIndex = 1;
			this.labelR.Text = "Red";
			this.labelR.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// trackBarR
			// 
			this.trackBarR.LargeChange = 1;
			this.trackBarR.Location = new System.Drawing.Point(72, 24);
			this.trackBarR.Maximum = 7;
			this.trackBarR.Name = "trackBarR";
			this.trackBarR.Size = new System.Drawing.Size(208, 42);
			this.trackBarR.TabIndex = 0;
			this.trackBarR.ValueChanged += new System.EventHandler(this.trackBar_ValueChanged);
			// 
			// buttonCancelAndClose
			// 
			this.buttonCancelAndClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonCancelAndClose.Location = new System.Drawing.Point(136, 352);
			this.buttonCancelAndClose.Name = "buttonCancelAndClose";
			this.buttonCancelAndClose.Size = new System.Drawing.Size(168, 32);
			this.buttonCancelAndClose.TabIndex = 23;
			this.buttonCancelAndClose.Text = "Cancel All Changes and Close";
			this.buttonCancelAndClose.Click += new System.EventHandler(this.buttonCancelAndClose_Click);
			// 
			// buttonSaveAndClose
			// 
			this.buttonSaveAndClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonSaveAndClose.Location = new System.Drawing.Point(136, 312);
			this.buttonSaveAndClose.Name = "buttonSaveAndClose";
			this.buttonSaveAndClose.Size = new System.Drawing.Size(168, 32);
			this.buttonSaveAndClose.TabIndex = 22;
			this.buttonSaveAndClose.Text = "Save All Changes and Close";
			this.buttonSaveAndClose.Click += new System.EventHandler(this.buttonSaveAndClose_Click);
			// 
			// PaletteEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(314, 391);
			this.Controls.Add(this.buttonCancelAndClose);
			this.Controls.Add(this.buttonSaveAndClose);
			this.Controls.Add(this.groupBoxSelectedEntry);
			this.Controls.Add(this.groupBoxPalette);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "PaletteEditorForm";
			this.Text = "Palette Editor";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.PaletteEditorForm_Closing);
			this.groupBoxPalette.ResumeLayout(false);
			this.groupBoxSelectedEntry.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.trackBarB)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarG)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trackBarR)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		//this doesn't really do what the name implies
		private Color getInverseColor(Color c)
		{
			if((c.R>200)&&(c.G>200)&&(c.B>200))
			{
				return(Color.Black);
			}
			else
			{
				return(Color.White);
			}
		}

		private void PaletteEditorForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(!this.saveOrCancelButtonClicked)
			{
				if(System.Windows.Forms.MessageBox.Show(this,"Changes will not be saved, continue?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning).Equals(DialogResult.No))
				{
					e.Cancel=true;
				}
			}
		}

		private void buttonSaveAndClose_Click(object sender, System.EventArgs e)
		{
			//save palette and close
			this.Cursor=Cursors.WaitCursor;
			//save palette
			this.romIO.writePalette(this.palette,this.address);
			this.Cursor=Cursors.Default;
			this.saveOrCancelButtonClicked=true;
			this.Close();
		}

		private void buttonCancelAndClose_Click(object sender, System.EventArgs e)
		{
			if(System.Windows.Forms.MessageBox.Show(this,"Changes will not be saved, continue?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning).Equals(DialogResult.Yes))
			{
				this.saveOrCancelButtonClicked=true;
				this.Close();
			}
		}

		private void onButtonClick(int newPaletteIndex)
		{
			this.cancelValidation=true;
			this.selectedPaletteIndex=newPaletteIndex;
			this.trackBarR.Value=this.palette.Entries[newPaletteIndex].R;
			this.trackBarG.Value=this.palette.Entries[newPaletteIndex].G;
			this.trackBarB.Value=this.palette.Entries[newPaletteIndex].B;
			this.cancelValidation=false;
		}

		private void radioButtonPalette0_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette0;
			this.onButtonClick(0);
		}

		private void radioButtonPalette1_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette1;
			this.onButtonClick(1);
		}

		private void radioButtonPalette2_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette2;
			this.onButtonClick(2);
		}

		private void radioButtonPalette3_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette3;
			this.onButtonClick(3);
		}

		private void radioButtonPalette4_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette4;
			this.onButtonClick(4);
		}

		private void radioButtonPalette5_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette5;
			this.onButtonClick(5);
		}

		private void radioButtonPalette6_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette6;
			this.onButtonClick(6);
		}

		private void radioButtonPalette7_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette7;
			this.onButtonClick(7);
		}

		private void radioButtonPalette8_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette8;
			this.onButtonClick(8);
		}

		private void radioButtonPalette9_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette9;
			this.onButtonClick(9);
		}

		private void radioButtonPalette10_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette10;
			this.onButtonClick(10);
		}

		private void radioButtonPalette11_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette11;
			this.onButtonClick(11);
		}

		private void radioButtonPalette12_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette12;
			this.onButtonClick(12);
		}

		private void radioButtonPalette13_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette13;
			this.onButtonClick(13);
		}

		private void radioButtonPalette14_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette14;
			this.onButtonClick(14);
		}

		private void radioButtonPalette15_CheckedChanged(object sender, System.EventArgs e)
		{
			this.selectedButton=this.radioButtonPalette15;
			this.onButtonClick(15);
		}

		private void trackBar_ValueChanged(object sender, System.EventArgs e)
		{
			if(this.cancelValidation){ return; }
			this.palette.Entries[this.selectedPaletteIndex].R=(ushort)this.trackBarR.Value;
			this.palette.Entries[this.selectedPaletteIndex].G=(ushort)this.trackBarG.Value;
			this.palette.Entries[this.selectedPaletteIndex].B=(ushort)this.trackBarB.Value;
			this.selectedButton.BackColor=palette.Entries[this.selectedPaletteIndex].ToRGB();
			this.selectedButton.ForeColor=this.getInverseColor(this.selectedButton.BackColor); 
		}
	}
}
