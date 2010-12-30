/*
TileEditor: Dialog to edit a 16 color 9-bit RGB palette
Originally created for Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2010 Hugues Johnson

TileEditor is free software; you can redistribute it and/or modify
it under the terms of the GNU General Public License version 2 
as published by the Free Software Foundation.

Aridia is distributed in the hope that it will be useful,
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

namespace com.huguesjohnson.TileEditor
{
	/// <summary>
	/// Summary description for TileEditorForm.
	/// </summary>
	public class TileEditorForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBoxRomBrowser;
		private System.Windows.Forms.Label labelStartingAddress;
		private System.Windows.Forms.Label labelEndAddress;
		private System.Windows.Forms.PictureBox pictureBoxBrowser;
		private System.Windows.Forms.GroupBox groupBoxSelectedTile;
		private System.Windows.Forms.PictureBox pictureBoxSelectedTile;
		private System.Windows.Forms.TextBox textBoxStartAddress;
		private System.Windows.Forms.TextBox textBoxEndAddress;
		private System.Windows.Forms.GroupBox groupBoxPalette;
		private System.Windows.Forms.Label labelPalette;
		private System.Windows.Forms.Button buttonPalette1;
		private System.Windows.Forms.Button buttonPalette2;
		private System.Windows.Forms.Button buttonPalette3;
		private System.Windows.Forms.Button buttonPalette4;
		private System.Windows.Forms.Button buttonPalette8;
		private System.Windows.Forms.Button buttonPalette7;
		private System.Windows.Forms.Button buttonPalette6;
		private System.Windows.Forms.Button buttonPalette5;
		private System.Windows.Forms.Button buttonPalette15;
		private System.Windows.Forms.Button buttonPalette14;
		private System.Windows.Forms.Button buttonPalette13;
		private System.Windows.Forms.Button buttonPalette12;
		private System.Windows.Forms.Button buttonPalette11;
		private System.Windows.Forms.Button buttonPalette10;
		private System.Windows.Forms.Button buttonPalette9;

		private int startAddress;
		private int endAddress;
		private MDBinaryRomIO romIO;
		private MDTile[] tiles;
		private int selectedTileIndex;
		private int selectedPixelX;
		private int selectedPixelY;
		private bool saveOrCancelButtonClicked;
		private System.Windows.Forms.Button buttonGridLines;
		private System.Windows.Forms.ColorDialog colorDialog;
		private const int BROWSER_COLUMNS=24;
		private const int BROWSER_TILE_WIDTH=16;
		private const int BROWSER_TILE_HEIGHT=16;
		private const int SELECTED_TILE_PIXEL_WIDTH=16;
		private const int SELECTED_TILE_PIXEL_HEIGHT=16;
		private System.Windows.Forms.ContextMenu contextMenuSelectedTile;
		private System.Windows.Forms.Label labelPixel;
		private System.Windows.Forms.Label labelPixelXY;
		private System.Windows.Forms.Label labelPaletteEntry;
		private System.Windows.Forms.ComboBox comboBoxPaletteEntry;
		private System.Windows.Forms.Button buttonExit;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonPalette0;
		private System.Windows.Forms.Label labelPalettes;
		private System.Windows.Forms.ComboBox comboBoxPalettes;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Create a new tile editor instance.
		/// </summary>
		/// <param name="romIO">The Mega Drive ROM IO to read/write.</param>
		/// <param name="startAddress">The starting address of the tiles to edit.</param>
		/// <param name="endAddress">The end address of the tiles to edit.</param>
		public TileEditorForm(MDBinaryRomIO romIO,int startAddress,int endAddress)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.romIO=romIO;
			this.startAddress=startAddress;
			this.endAddress=endAddress;
			this.textBoxStartAddress.Text=startAddress.ToString();
			this.textBoxEndAddress.Text=endAddress.ToString();
			this.saveOrCancelButtonClicked=false;
			//get the tiles
			int tileCount=((this.endAddress-this.startAddress)/32)+1;
			if((this.tiles==null)||(this.tiles.Length<1))
			{
				this.tiles=new MDTile[tileCount];
				int tileIndex=0;
				for(int offset=this.startAddress;offset<this.endAddress;offset+=32)
				{
					this.tiles[tileIndex]=this.romIO.readTile(offset,8,8);
					tileIndex++;
				}
				this.selectedTileIndex=0;
				this.selectedPixelX=0;
				this.selectedPixelY=0;
				this.updateSelectedPixel();
			}
			this.comboBoxPalettes.Items.Add(new LookupValue("<custom>",-1));
		}

		/// <summary>
		/// Create a new tile editor instance.
		/// </summary>
		/// <param name="romIO">The Mega Drive ROM IO to read/write.</param>
		/// <param name="startAddress">The starting address of the tiles to edit.</param>
		/// <param name="endAddress">The end address of the tiles to edit.</param>
		/// <param name="romPalettes">The collection of ROM palettes to load into the combo box.</param>
		public TileEditorForm(MDBinaryRomIO romIO,int startAddress,int endAddress,LookupValueCollection romPalettes) : this(romIO,startAddress,endAddress)
		{
			LookupValue[] allItems=romPalettes.getAll();
			int size=allItems.Length;
			for(int index=0;index<size;index++)
			{
				this.comboBoxPalettes.Items.Add(allItems[index]);
			}
			this.comboBoxPalettes.SelectedIndex=0;
			this.comboBoxPalettes.Enabled=true;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TileEditorForm));
			this.groupBoxRomBrowser = new System.Windows.Forms.GroupBox();
			this.textBoxEndAddress = new System.Windows.Forms.TextBox();
			this.textBoxStartAddress = new System.Windows.Forms.TextBox();
			this.pictureBoxBrowser = new System.Windows.Forms.PictureBox();
			this.labelEndAddress = new System.Windows.Forms.Label();
			this.labelStartingAddress = new System.Windows.Forms.Label();
			this.groupBoxSelectedTile = new System.Windows.Forms.GroupBox();
			this.comboBoxPaletteEntry = new System.Windows.Forms.ComboBox();
			this.labelPaletteEntry = new System.Windows.Forms.Label();
			this.labelPixelXY = new System.Windows.Forms.Label();
			this.labelPixel = new System.Windows.Forms.Label();
			this.pictureBoxSelectedTile = new System.Windows.Forms.PictureBox();
			this.groupBoxPalette = new System.Windows.Forms.GroupBox();
			this.buttonPalette0 = new System.Windows.Forms.Button();
			this.buttonGridLines = new System.Windows.Forms.Button();
			this.buttonPalette15 = new System.Windows.Forms.Button();
			this.buttonPalette14 = new System.Windows.Forms.Button();
			this.buttonPalette13 = new System.Windows.Forms.Button();
			this.buttonPalette12 = new System.Windows.Forms.Button();
			this.buttonPalette11 = new System.Windows.Forms.Button();
			this.buttonPalette10 = new System.Windows.Forms.Button();
			this.buttonPalette9 = new System.Windows.Forms.Button();
			this.buttonPalette8 = new System.Windows.Forms.Button();
			this.buttonPalette7 = new System.Windows.Forms.Button();
			this.buttonPalette6 = new System.Windows.Forms.Button();
			this.buttonPalette5 = new System.Windows.Forms.Button();
			this.buttonPalette4 = new System.Windows.Forms.Button();
			this.buttonPalette3 = new System.Windows.Forms.Button();
			this.buttonPalette2 = new System.Windows.Forms.Button();
			this.buttonPalette1 = new System.Windows.Forms.Button();
			this.labelPalette = new System.Windows.Forms.Label();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.contextMenuSelectedTile = new System.Windows.Forms.ContextMenu();
			this.buttonExit = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelPalettes = new System.Windows.Forms.Label();
			this.comboBoxPalettes = new System.Windows.Forms.ComboBox();
			this.groupBoxRomBrowser.SuspendLayout();
			this.groupBoxSelectedTile.SuspendLayout();
			this.groupBoxPalette.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBoxRomBrowser
			// 
			this.groupBoxRomBrowser.Controls.Add(this.textBoxEndAddress);
			this.groupBoxRomBrowser.Controls.Add(this.textBoxStartAddress);
			this.groupBoxRomBrowser.Controls.Add(this.pictureBoxBrowser);
			this.groupBoxRomBrowser.Controls.Add(this.labelEndAddress);
			this.groupBoxRomBrowser.Controls.Add(this.labelStartingAddress);
			this.groupBoxRomBrowser.Location = new System.Drawing.Point(8, 16);
			this.groupBoxRomBrowser.Name = "groupBoxRomBrowser";
			this.groupBoxRomBrowser.Size = new System.Drawing.Size(416, 184);
			this.groupBoxRomBrowser.TabIndex = 0;
			this.groupBoxRomBrowser.TabStop = false;
			this.groupBoxRomBrowser.Text = "ROM Browser";
			// 
			// textBoxEndAddress
			// 
			this.textBoxEndAddress.Location = new System.Drawing.Point(304, 24);
			this.textBoxEndAddress.Name = "textBoxEndAddress";
			this.textBoxEndAddress.ReadOnly = true;
			this.textBoxEndAddress.Size = new System.Drawing.Size(96, 20);
			this.textBoxEndAddress.TabIndex = 4;
			this.textBoxEndAddress.Text = "";
			// 
			// textBoxStartAddress
			// 
			this.textBoxStartAddress.Location = new System.Drawing.Point(104, 24);
			this.textBoxStartAddress.Name = "textBoxStartAddress";
			this.textBoxStartAddress.ReadOnly = true;
			this.textBoxStartAddress.Size = new System.Drawing.Size(96, 20);
			this.textBoxStartAddress.TabIndex = 3;
			this.textBoxStartAddress.Text = "";
			// 
			// pictureBoxBrowser
			// 
			this.pictureBoxBrowser.BackColor = System.Drawing.Color.White;
			this.pictureBoxBrowser.Location = new System.Drawing.Point(16, 48);
			this.pictureBoxBrowser.Name = "pictureBoxBrowser";
			this.pictureBoxBrowser.Size = new System.Drawing.Size(384, 128);
			this.pictureBoxBrowser.TabIndex = 2;
			this.pictureBoxBrowser.TabStop = false;
			this.pictureBoxBrowser.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxBrowser_Paint);
			this.pictureBoxBrowser.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxBrowser_MouseUp);
			// 
			// labelEndAddress
			// 
			this.labelEndAddress.Location = new System.Drawing.Point(216, 24);
			this.labelEndAddress.Name = "labelEndAddress";
			this.labelEndAddress.Size = new System.Drawing.Size(80, 20);
			this.labelEndAddress.TabIndex = 1;
			this.labelEndAddress.Text = "End Address:";
			this.labelEndAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// labelStartingAddress
			// 
			this.labelStartingAddress.Location = new System.Drawing.Point(16, 24);
			this.labelStartingAddress.Name = "labelStartingAddress";
			this.labelStartingAddress.Size = new System.Drawing.Size(80, 20);
			this.labelStartingAddress.TabIndex = 0;
			this.labelStartingAddress.Text = "Start Address:";
			this.labelStartingAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBoxSelectedTile
			// 
			this.groupBoxSelectedTile.Controls.Add(this.comboBoxPaletteEntry);
			this.groupBoxSelectedTile.Controls.Add(this.labelPaletteEntry);
			this.groupBoxSelectedTile.Controls.Add(this.labelPixelXY);
			this.groupBoxSelectedTile.Controls.Add(this.labelPixel);
			this.groupBoxSelectedTile.Controls.Add(this.pictureBoxSelectedTile);
			this.groupBoxSelectedTile.Location = new System.Drawing.Point(432, 16);
			this.groupBoxSelectedTile.Name = "groupBoxSelectedTile";
			this.groupBoxSelectedTile.Size = new System.Drawing.Size(160, 216);
			this.groupBoxSelectedTile.TabIndex = 1;
			this.groupBoxSelectedTile.TabStop = false;
			this.groupBoxSelectedTile.Text = "Selected Tile";
			// 
			// comboBoxPaletteEntry
			// 
			this.comboBoxPaletteEntry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPaletteEntry.Items.AddRange(new object[] {
																	  "0",
																	  "1",
																	  "2",
																	  "3",
																	  "4",
																	  "5",
																	  "6",
																	  "7",
																	  "8",
																	  "9",
																	  "10",
																	  "11",
																	  "12",
																	  "13",
																	  "14",
																	  "15"});
			this.comboBoxPaletteEntry.Location = new System.Drawing.Point(72, 184);
			this.comboBoxPaletteEntry.Name = "comboBoxPaletteEntry";
			this.comboBoxPaletteEntry.Size = new System.Drawing.Size(72, 21);
			this.comboBoxPaletteEntry.TabIndex = 19;
			// 
			// labelPaletteEntry
			// 
			this.labelPaletteEntry.Location = new System.Drawing.Point(16, 184);
			this.labelPaletteEntry.Name = "labelPaletteEntry";
			this.labelPaletteEntry.Size = new System.Drawing.Size(64, 20);
			this.labelPaletteEntry.TabIndex = 3;
			this.labelPaletteEntry.Text = "Pen Color:";
			this.labelPaletteEntry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelPixelXY
			// 
			this.labelPixelXY.Location = new System.Drawing.Point(72, 160);
			this.labelPixelXY.Name = "labelPixelXY";
			this.labelPixelXY.Size = new System.Drawing.Size(72, 16);
			this.labelPixelXY.TabIndex = 2;
			// 
			// labelPixel
			// 
			this.labelPixel.Location = new System.Drawing.Point(16, 160);
			this.labelPixel.Name = "labelPixel";
			this.labelPixel.Size = new System.Drawing.Size(64, 16);
			this.labelPixel.TabIndex = 1;
			this.labelPixel.Text = "Pixel Data:";
			this.labelPixel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pictureBoxSelectedTile
			// 
			this.pictureBoxSelectedTile.BackColor = System.Drawing.Color.White;
			this.pictureBoxSelectedTile.Cursor = System.Windows.Forms.Cursors.Cross;
			this.pictureBoxSelectedTile.Location = new System.Drawing.Point(16, 24);
			this.pictureBoxSelectedTile.Name = "pictureBoxSelectedTile";
			this.pictureBoxSelectedTile.Size = new System.Drawing.Size(128, 128);
			this.pictureBoxSelectedTile.TabIndex = 0;
			this.pictureBoxSelectedTile.TabStop = false;
			this.pictureBoxSelectedTile.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxSelectedTile_Paint);
			this.pictureBoxSelectedTile.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSelectedTile_MouseUp);
			this.pictureBoxSelectedTile.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxSelectedTile_MouseMove);
			// 
			// groupBoxPalette
			// 
			this.groupBoxPalette.Controls.Add(this.comboBoxPalettes);
			this.groupBoxPalette.Controls.Add(this.labelPalettes);
			this.groupBoxPalette.Controls.Add(this.buttonPalette0);
			this.groupBoxPalette.Controls.Add(this.buttonGridLines);
			this.groupBoxPalette.Controls.Add(this.buttonPalette15);
			this.groupBoxPalette.Controls.Add(this.buttonPalette14);
			this.groupBoxPalette.Controls.Add(this.buttonPalette13);
			this.groupBoxPalette.Controls.Add(this.buttonPalette12);
			this.groupBoxPalette.Controls.Add(this.buttonPalette11);
			this.groupBoxPalette.Controls.Add(this.buttonPalette10);
			this.groupBoxPalette.Controls.Add(this.buttonPalette9);
			this.groupBoxPalette.Controls.Add(this.buttonPalette8);
			this.groupBoxPalette.Controls.Add(this.buttonPalette7);
			this.groupBoxPalette.Controls.Add(this.buttonPalette6);
			this.groupBoxPalette.Controls.Add(this.buttonPalette5);
			this.groupBoxPalette.Controls.Add(this.buttonPalette4);
			this.groupBoxPalette.Controls.Add(this.buttonPalette3);
			this.groupBoxPalette.Controls.Add(this.buttonPalette2);
			this.groupBoxPalette.Controls.Add(this.buttonPalette1);
			this.groupBoxPalette.Controls.Add(this.labelPalette);
			this.groupBoxPalette.Location = new System.Drawing.Point(8, 208);
			this.groupBoxPalette.Name = "groupBoxPalette";
			this.groupBoxPalette.Size = new System.Drawing.Size(416, 144);
			this.groupBoxPalette.TabIndex = 2;
			this.groupBoxPalette.TabStop = false;
			this.groupBoxPalette.Text = "Palette";
			// 
			// buttonPalette0
			// 
			this.buttonPalette0.BackColor = System.Drawing.Color.Black;
			this.buttonPalette0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette0.ForeColor = System.Drawing.Color.White;
			this.buttonPalette0.Location = new System.Drawing.Point(16, 16);
			this.buttonPalette0.Name = "buttonPalette0";
			this.buttonPalette0.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette0.TabIndex = 1;
			this.buttonPalette0.Text = "0";
			this.buttonPalette0.Click += new System.EventHandler(this.buttonPalette0_Click);
			this.buttonPalette0.BackColorChanged += new System.EventHandler(this.buttonPalette0_BackColorChanged);
			// 
			// buttonGridLines
			// 
			this.buttonGridLines.BackColor = System.Drawing.Color.White;
			this.buttonGridLines.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonGridLines.ForeColor = System.Drawing.Color.Black;
			this.buttonGridLines.Location = new System.Drawing.Point(280, 48);
			this.buttonGridLines.Name = "buttonGridLines";
			this.buttonGridLines.Size = new System.Drawing.Size(120, 32);
			this.buttonGridLines.TabIndex = 18;
			this.buttonGridLines.Text = "Gridlines";
			this.buttonGridLines.Click += new System.EventHandler(this.buttonGridLines_Click);
			// 
			// buttonPalette15
			// 
			this.buttonPalette15.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.buttonPalette15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette15.Location = new System.Drawing.Point(240, 48);
			this.buttonPalette15.Name = "buttonPalette15";
			this.buttonPalette15.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette15.TabIndex = 16;
			this.buttonPalette15.Text = "15";
			this.buttonPalette15.Click += new System.EventHandler(this.buttonPalette15_Click);
			this.buttonPalette15.BackColorChanged += new System.EventHandler(this.buttonPalette15_BackColorChanged);
			// 
			// buttonPalette14
			// 
			this.buttonPalette14.BackColor = System.Drawing.Color.Blue;
			this.buttonPalette14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette14.Location = new System.Drawing.Point(208, 48);
			this.buttonPalette14.Name = "buttonPalette14";
			this.buttonPalette14.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette14.TabIndex = 15;
			this.buttonPalette14.Text = "14";
			this.buttonPalette14.Click += new System.EventHandler(this.buttonPalette14_Click);
			this.buttonPalette14.BackColorChanged += new System.EventHandler(this.buttonPalette14_BackColorChanged);
			// 
			// buttonPalette13
			// 
			this.buttonPalette13.BackColor = System.Drawing.Color.Teal;
			this.buttonPalette13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette13.Location = new System.Drawing.Point(176, 48);
			this.buttonPalette13.Name = "buttonPalette13";
			this.buttonPalette13.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette13.TabIndex = 14;
			this.buttonPalette13.Text = "13";
			this.buttonPalette13.Click += new System.EventHandler(this.buttonPalette13_Click);
			this.buttonPalette13.BackColorChanged += new System.EventHandler(this.buttonPalette13_BackColorChanged);
			// 
			// buttonPalette12
			// 
			this.buttonPalette12.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(192)), ((System.Byte)(0)));
			this.buttonPalette12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette12.Location = new System.Drawing.Point(144, 48);
			this.buttonPalette12.Name = "buttonPalette12";
			this.buttonPalette12.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette12.TabIndex = 13;
			this.buttonPalette12.Text = "12";
			this.buttonPalette12.Click += new System.EventHandler(this.buttonPalette12_Click);
			this.buttonPalette12.BackColorChanged += new System.EventHandler(this.buttonPalette12_BackColorChanged);
			// 
			// buttonPalette11
			// 
			this.buttonPalette11.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(0)));
			this.buttonPalette11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette11.Location = new System.Drawing.Point(112, 48);
			this.buttonPalette11.Name = "buttonPalette11";
			this.buttonPalette11.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette11.TabIndex = 12;
			this.buttonPalette11.Text = "11";
			this.buttonPalette11.Click += new System.EventHandler(this.buttonPalette11_Click);
			this.buttonPalette11.BackColorChanged += new System.EventHandler(this.buttonPalette11_BackColorChanged);
			// 
			// buttonPalette10
			// 
			this.buttonPalette10.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(128)), ((System.Byte)(0)));
			this.buttonPalette10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette10.Location = new System.Drawing.Point(80, 48);
			this.buttonPalette10.Name = "buttonPalette10";
			this.buttonPalette10.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette10.TabIndex = 11;
			this.buttonPalette10.Text = "10";
			this.buttonPalette10.Click += new System.EventHandler(this.buttonPalette10_Click);
			this.buttonPalette10.BackColorChanged += new System.EventHandler(this.buttonPalette10_BackColorChanged);
			// 
			// buttonPalette9
			// 
			this.buttonPalette9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.buttonPalette9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette9.Location = new System.Drawing.Point(48, 48);
			this.buttonPalette9.Name = "buttonPalette9";
			this.buttonPalette9.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette9.TabIndex = 10;
			this.buttonPalette9.Text = "9";
			this.buttonPalette9.Click += new System.EventHandler(this.buttonPalette9_Click);
			this.buttonPalette9.BackColorChanged += new System.EventHandler(this.buttonPalette9_BackColorChanged);
			// 
			// buttonPalette8
			// 
			this.buttonPalette8.BackColor = System.Drawing.Color.Silver;
			this.buttonPalette8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette8.Location = new System.Drawing.Point(16, 48);
			this.buttonPalette8.Name = "buttonPalette8";
			this.buttonPalette8.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette8.TabIndex = 9;
			this.buttonPalette8.Text = "8";
			this.buttonPalette8.Click += new System.EventHandler(this.buttonPalette8_Click);
			this.buttonPalette8.BackColorChanged += new System.EventHandler(this.buttonPalette8_BackColorChanged);
			// 
			// buttonPalette7
			// 
			this.buttonPalette7.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.buttonPalette7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette7.Location = new System.Drawing.Point(240, 16);
			this.buttonPalette7.Name = "buttonPalette7";
			this.buttonPalette7.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette7.TabIndex = 8;
			this.buttonPalette7.Text = "7";
			this.buttonPalette7.Click += new System.EventHandler(this.buttonPalette7_Click);
			this.buttonPalette7.BackColorChanged += new System.EventHandler(this.buttonPalette7_BackColorChanged);
			// 
			// buttonPalette6
			// 
			this.buttonPalette6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			this.buttonPalette6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette6.Location = new System.Drawing.Point(208, 16);
			this.buttonPalette6.Name = "buttonPalette6";
			this.buttonPalette6.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette6.TabIndex = 7;
			this.buttonPalette6.Text = "6";
			this.buttonPalette6.Click += new System.EventHandler(this.buttonPalette6_Click);
			this.buttonPalette6.BackColorChanged += new System.EventHandler(this.buttonPalette6_BackColorChanged);
			// 
			// buttonPalette5
			// 
			this.buttonPalette5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.buttonPalette5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette5.Location = new System.Drawing.Point(176, 16);
			this.buttonPalette5.Name = "buttonPalette5";
			this.buttonPalette5.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette5.TabIndex = 6;
			this.buttonPalette5.Text = "5";
			this.buttonPalette5.Click += new System.EventHandler(this.buttonPalette5_Click);
			this.buttonPalette5.BackColorChanged += new System.EventHandler(this.buttonPalette5_BackColorChanged);
			// 
			// buttonPalette4
			// 
			this.buttonPalette4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.buttonPalette4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette4.Location = new System.Drawing.Point(144, 16);
			this.buttonPalette4.Name = "buttonPalette4";
			this.buttonPalette4.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette4.TabIndex = 5;
			this.buttonPalette4.Text = "4";
			this.buttonPalette4.Click += new System.EventHandler(this.buttonPalette4_Click);
			this.buttonPalette4.BackColorChanged += new System.EventHandler(this.buttonPalette4_BackColorChanged);
			// 
			// buttonPalette3
			// 
			this.buttonPalette3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.buttonPalette3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette3.Location = new System.Drawing.Point(112, 16);
			this.buttonPalette3.Name = "buttonPalette3";
			this.buttonPalette3.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette3.TabIndex = 4;
			this.buttonPalette3.Text = "3";
			this.buttonPalette3.Click += new System.EventHandler(this.buttonPalette3_Click);
			this.buttonPalette3.BackColorChanged += new System.EventHandler(this.buttonPalette3_BackColorChanged);
			// 
			// buttonPalette2
			// 
			this.buttonPalette2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(224)), ((System.Byte)(192)));
			this.buttonPalette2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette2.Location = new System.Drawing.Point(80, 16);
			this.buttonPalette2.Name = "buttonPalette2";
			this.buttonPalette2.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette2.TabIndex = 3;
			this.buttonPalette2.Text = "2";
			this.buttonPalette2.Click += new System.EventHandler(this.buttonPalette2_Click);
			this.buttonPalette2.BackColorChanged += new System.EventHandler(this.buttonPalette2_BackColorChanged);
			// 
			// buttonPalette1
			// 
			this.buttonPalette1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(192)), ((System.Byte)(192)));
			this.buttonPalette1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonPalette1.Location = new System.Drawing.Point(48, 16);
			this.buttonPalette1.Name = "buttonPalette1";
			this.buttonPalette1.Size = new System.Drawing.Size(32, 32);
			this.buttonPalette1.TabIndex = 2;
			this.buttonPalette1.Text = "1";
			this.buttonPalette1.Click += new System.EventHandler(this.buttonPalette1_Click);
			this.buttonPalette1.BackColorChanged += new System.EventHandler(this.buttonPalette1_BackColorChanged);
			// 
			// labelPalette
			// 
			this.labelPalette.Location = new System.Drawing.Point(8, 120);
			this.labelPalette.Name = "labelPalette";
			this.labelPalette.Size = new System.Drawing.Size(392, 16);
			this.labelPalette.TabIndex = 0;
			this.labelPalette.Text = "Palette is for preview purposes, this does not affect what is saved to the ROM";
			// 
			// colorDialog
			// 
			this.colorDialog.FullOpen = true;
			// 
			// buttonExit
			// 
			this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonExit.Location = new System.Drawing.Point(432, 280);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(168, 32);
			this.buttonExit.TabIndex = 20;
			this.buttonExit.Text = "Save All Changes and Close";
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.buttonCancel.Location = new System.Drawing.Point(432, 320);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(168, 32);
			this.buttonCancel.TabIndex = 21;
			this.buttonCancel.Text = "Cancel All Changes and Close";
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// labelPalettes
			// 
			this.labelPalettes.Location = new System.Drawing.Point(16, 88);
			this.labelPalettes.Name = "labelPalettes";
			this.labelPalettes.Size = new System.Drawing.Size(112, 21);
			this.labelPalettes.TabIndex = 19;
			this.labelPalettes.Text = "Use palette in ROM:";
			this.labelPalettes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// comboBoxPalettes
			// 
			this.comboBoxPalettes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxPalettes.Enabled = false;
			this.comboBoxPalettes.Location = new System.Drawing.Point(136, 88);
			this.comboBoxPalettes.Name = "comboBoxPalettes";
			this.comboBoxPalettes.Size = new System.Drawing.Size(264, 21);
			this.comboBoxPalettes.TabIndex = 20;
			this.comboBoxPalettes.SelectedIndexChanged += new System.EventHandler(this.comboBoxPalettes_SelectedIndexChanged);
			// 
			// TileEditorForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(602, 359);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.groupBoxPalette);
			this.Controls.Add(this.groupBoxSelectedTile);
			this.Controls.Add(this.groupBoxRomBrowser);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "TileEditorForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Tile Editor 1.1";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.TileEditorForm_Closing);
			this.Load += new System.EventHandler(this.TileEditorForm_Load);
			this.groupBoxRomBrowser.ResumeLayout(false);
			this.groupBoxSelectedTile.ResumeLayout(false);
			this.groupBoxPalette.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void TileEditorForm_Load(object sender, System.EventArgs e)
		{
			this.comboBoxPaletteEntry.SelectedIndex=0;
		}

		private void pictureBoxBrowser_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g=e.Graphics;
			//draw the tiles
			Color[] palette=this.getPalette();
			int xCounter=0;
			int yCounter=0;
			int tileCount=this.tiles.Length;
			for(int index=0;index<tileCount;index++)
			{
				for(int x=0;x<8;x++)
				{
					for(int y=0;y<8;y++)
					{
						int pixel=this.tiles[index].PixelData[x][y];
						int xc=(xCounter*BROWSER_TILE_WIDTH)+(x*2);
						int yc=(yCounter*BROWSER_TILE_HEIGHT)+(y*2);
						SolidBrush brush=new SolidBrush(palette[pixel]); 
						e.Graphics.FillRectangle(brush,xc,yc,2,2);
					}
				}
				//increment counters
				if(xCounter==(BROWSER_COLUMNS-1))
				{ 
					xCounter=0; 
					yCounter++;
				}
				else
				{
					xCounter++;
				}
			}
			//draw gridlines
			Pen pen=new Pen(this.buttonGridLines.BackColor);
			int width=this.pictureBoxBrowser.Width;
			int height=this.pictureBoxBrowser.Height;
			for(int index=BROWSER_TILE_WIDTH;index<width;index+=BROWSER_TILE_WIDTH)
			{
				g.DrawLine(pen,index,0,index,height);
			}
			for(int index=BROWSER_TILE_HEIGHT;index<height;index+=BROWSER_TILE_HEIGHT)
			{
				g.DrawLine(pen,0,index,width,index);
			}
			base.OnPaint(e);
		}

		private void pictureBoxSelectedTile_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics g=e.Graphics;
			//set the background
			this.pictureBoxSelectedTile.BackColor=this.pictureBoxSelectedTile.BackColor;
			//draw the selected tile
			if(this.selectedTileIndex>=0)
			{
				MDTile selectedTile=this.tiles[this.selectedTileIndex];
				Color[] palette=this.getPalette();
				int xCounter=0;
				int yCounter=0;
				for(int y=0;y<8;y++)
				{
					for(int x=0;x<8;x++)
					{
						int pixel=selectedTile.PixelData[x][y];
						int xc=(xCounter*SELECTED_TILE_PIXEL_WIDTH);
						int yc=(yCounter*SELECTED_TILE_PIXEL_HEIGHT);
						SolidBrush brush=new SolidBrush(palette[pixel]); 
						e.Graphics.FillRectangle(brush,xc,yc,SELECTED_TILE_PIXEL_WIDTH,SELECTED_TILE_PIXEL_HEIGHT);
						//increment counters
						if(xCounter==7)
						{ 
							xCounter=0; 
							yCounter++;
						}
						else
						{
							xCounter++;
						}
					}
				}
			}
			//draw gridlines
			Pen pen=new Pen(this.buttonGridLines.BackColor);
			int width=this.pictureBoxSelectedTile.Width;
			int height=this.pictureBoxSelectedTile.Height;
			for(int index=SELECTED_TILE_PIXEL_WIDTH;index<width;index+=SELECTED_TILE_PIXEL_WIDTH)
			{
				g.DrawLine(pen,index,0,index,height);
			}
			for(int index=SELECTED_TILE_PIXEL_HEIGHT;index<height;index+=SELECTED_TILE_PIXEL_HEIGHT)
			{
				g.DrawLine(pen,0,index,width,index);
			}
			base.OnPaint(e);
		}

		private Color[] getPalette()
		{
			Color[] palette=new Color[16];
			palette[0]=this.buttonPalette0.BackColor;
			palette[1]=this.buttonPalette1.BackColor;
			palette[2]=this.buttonPalette2.BackColor;
			palette[3]=this.buttonPalette3.BackColor;
			palette[4]=this.buttonPalette4.BackColor;
			palette[5]=this.buttonPalette5.BackColor;
			palette[6]=this.buttonPalette6.BackColor;
			palette[7]=this.buttonPalette7.BackColor;
			palette[8]=this.buttonPalette8.BackColor;
			palette[9]=this.buttonPalette9.BackColor;
			palette[10]=this.buttonPalette10.BackColor;
			palette[11]=this.buttonPalette11.BackColor;
			palette[12]=this.buttonPalette12.BackColor;
			palette[13]=this.buttonPalette13.BackColor;
			palette[14]=this.buttonPalette14.BackColor;
			palette[15]=this.buttonPalette15.BackColor;
			return(palette);
		}

		private void buttonPaletteClick(System.Windows.Forms.Button button)
		{
			this.colorDialog.Color=button.BackColor;
			DialogResult result=this.colorDialog.ShowDialog(this);
			if(!result.Equals(DialogResult.Cancel))
			{
				button.BackColor=this.colorDialog.Color;
				this.pictureBoxBrowser.Refresh();
				this.pictureBoxSelectedTile.Refresh();
				this.comboBoxPalettes.SelectedIndex=0;
			}
		}

		private void buttonPalette1_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette1);
		}

		private void buttonPalette2_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette2);
		}

		private void buttonPalette3_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette3);
		}

		private void buttonPalette4_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette4);
		}

		private void buttonPalette5_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette5);
		}

		private void buttonPalette6_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette6);
		}

		private void buttonPalette7_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette7);
		}

		private void buttonPalette8_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette8);
		}

		private void buttonPalette9_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette9);
		}

		private void buttonPalette10_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette10);
		}

		private void buttonPalette11_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette11);
		}

		private void buttonPalette12_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette12);
		}

		private void buttonPalette13_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette13);
		}

		private void buttonPalette14_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette14);
		}

		private void buttonPalette15_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette15);
		}

		private void buttonGridLines_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonGridLines);
		}

		private void pictureBoxBrowser_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//figure out which tile was clicked
            int tileColumn=e.X/BROWSER_TILE_WIDTH;
			int tileRow=e.Y/BROWSER_TILE_HEIGHT;
			int selectedTile=tileColumn+(BROWSER_COLUMNS*tileRow);
			if((selectedTile>=0)&&(selectedTile<this.tiles.Length))
			{
				this.selectedTileIndex=selectedTile;
				this.pictureBoxSelectedTile.Refresh();
				this.selectedPixelX=0;
				this.selectedPixelY=0;
				this.updateSelectedPixel();
			}
		}

		private void pictureBoxSelectedTile_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			//figure out which tile was clicked
			int pixelColumn=e.X/SELECTED_TILE_PIXEL_WIDTH;
			int pixelRow=e.Y/SELECTED_TILE_PIXEL_HEIGHT;
			int selectedPixel=pixelColumn+(8*pixelRow);
			if((selectedPixel>=0)&&(selectedPixel<64)) //this check *should* never fail
			{
				if((pixelColumn<0)||(pixelColumn>7)||(pixelRow<0)||(pixelRow>7)){ return; }
				this.selectedPixelX=pixelColumn;
				this.selectedPixelY=pixelRow;
				//check for right-click
				if(e.Button.Equals(MouseButtons.Right))
				{
					this.showContextMenuSelectedTile(e);
				}
				else if(e.Button.Equals(MouseButtons.Left))
				{
					this.tiles[this.selectedTileIndex].PixelData[this.selectedPixelX][this.selectedPixelY]=Byte.Parse(this.comboBoxPaletteEntry.Text);
					this.pictureBoxBrowser.Refresh();
					this.pictureBoxSelectedTile.Refresh();
				}
			}
		}

		private void showContextMenuSelectedTile(System.Windows.Forms.MouseEventArgs e)
		{
			int currentPaletteEntry=Int32.Parse(this.comboBoxPaletteEntry.Text);
			//create the subitems
			Color[] colors=this.getPalette();
			int length=colors.Length;
			MenuItem[] subItems=new MenuItem[length];
			for(int index=0;index<length;index++)
			{
				subItems[index]=new MenuItem(index.ToString(),new System.EventHandler(onContextMenuClick));
				subItems[index].RadioCheck=true;
				if(index==currentPaletteEntry)
				{
					subItems[index].DefaultItem=true;
					subItems[index].Checked=true;
				}
			}
			//show the context menu
			this.contextMenuSelectedTile=new ContextMenu(subItems);
			this.contextMenuSelectedTile.Show(this.pictureBoxSelectedTile,new Point(e.X,e.Y));
		}

		private void updateSelectedPixel()
		{
			//dragging the mouse in and out of the selected tile picture box can cause these to fail badly
			if((this.selectedTileIndex<0)||(this.selectedTileIndex>=this.tiles.Length)){ return; }
			MDTile selectedTile=this.tiles[this.selectedTileIndex];
			if((this.selectedPixelX<0)||(this.selectedPixelX>=selectedTile.PixelData.Length)){ return; }
			if((this.selectedPixelY<0)||(this.selectedPixelY>=selectedTile.PixelData[selectedPixelX].Length)){ return; }
			this.labelPixelXY.Text="("+this.selectedPixelX+","+this.selectedPixelY+") "+"["+selectedTile.PixelData[this.selectedPixelX][this.selectedPixelY].ToString()+"]";
		}

		private void buttonExit_Click(object sender, System.EventArgs e)
		{
			//save tile data and close
			this.Cursor=Cursors.WaitCursor;
			this.romIO.writeTiles(this.tiles);
			this.Cursor=Cursors.Default;
			this.saveOrCancelButtonClicked=true;
			this.Close();
		}

		private void onContextMenuClick(object sender,System.EventArgs e)
		{
			this.comboBoxPaletteEntry.Text=((MenuItem)sender).Text;
		}

		private void buttonCancel_Click(object sender, System.EventArgs e)
		{
			if(System.Windows.Forms.MessageBox.Show(this,"Changes will not be saved, continue?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning).Equals(DialogResult.Yes))
			{
				this.saveOrCancelButtonClicked=true;
				this.Close();
			}
		}

		private void TileEditorForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(!this.saveOrCancelButtonClicked)
			{
				if(System.Windows.Forms.MessageBox.Show(this,"Changes will not be saved, continue?","Warning",MessageBoxButtons.YesNo,MessageBoxIcon.Warning).Equals(DialogResult.No))
				{
					e.Cancel=true;
				}
			}
		}

		private void pictureBoxSelectedTile_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int pixelColumn=e.X/SELECTED_TILE_PIXEL_WIDTH;
			int pixelRow=e.Y/SELECTED_TILE_PIXEL_HEIGHT;
			int selectedPixel=pixelColumn+(8*pixelRow);
			if((selectedPixel>=0)&&(selectedPixel<64)) //this check *should* never fail
			{
				if((pixelColumn<0)||(pixelColumn>7)||(pixelRow<0)||(pixelRow>7)){ return; }
				this.selectedPixelX=pixelColumn;
				this.selectedPixelY=pixelRow;
				this.updateSelectedPixel();
				if(e.Button.Equals(MouseButtons.Left))
				{
					this.tiles[this.selectedTileIndex].PixelData[this.selectedPixelX][this.selectedPixelY]=Byte.Parse(this.comboBoxPaletteEntry.Text);
					this.pictureBoxBrowser.Refresh();
					this.pictureBoxSelectedTile.Refresh();
				}			
			}
		}

		private void buttonPalette0_Click(object sender, System.EventArgs e)
		{
			this.buttonPaletteClick(buttonPalette0);
		}

		private void comboBoxPalettes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			LookupValue lookupValue=(LookupValue)comboBoxPalettes.SelectedItem;
			if(lookupValue.IntValue>-1)
			{
				Palette palette=this.romIO.readPalette(lookupValue.IntValue);
				this.buttonPalette0.BackColor=palette.Entries[0].ToRGB();
				this.buttonPalette1.BackColor=palette.Entries[1].ToRGB();
				this.buttonPalette2.BackColor=palette.Entries[2].ToRGB();
				this.buttonPalette3.BackColor=palette.Entries[3].ToRGB();
				this.buttonPalette4.BackColor=palette.Entries[4].ToRGB();
				this.buttonPalette5.BackColor=palette.Entries[5].ToRGB();
				this.buttonPalette6.BackColor=palette.Entries[6].ToRGB();
				this.buttonPalette7.BackColor=palette.Entries[7].ToRGB();
				this.buttonPalette8.BackColor=palette.Entries[8].ToRGB();
				this.buttonPalette9.BackColor=palette.Entries[9].ToRGB();
				this.buttonPalette10.BackColor=palette.Entries[10].ToRGB();
				this.buttonPalette11.BackColor=palette.Entries[11].ToRGB();
				this.buttonPalette12.BackColor=palette.Entries[12].ToRGB();
				this.buttonPalette13.BackColor=palette.Entries[13].ToRGB();
				this.buttonPalette14.BackColor=palette.Entries[14].ToRGB();
				this.buttonPalette15.BackColor=palette.Entries[15].ToRGB();
				this.pictureBoxBrowser.Refresh();
				this.pictureBoxSelectedTile.Refresh();
			}
		}

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

		private void buttonBackColorChanged(Button button)
		{
			Color c=button.BackColor;
			if((c.R>200)&&(c.G>200)&&(c.B>200))
			{
				button.ForeColor=Color.Black;
			}
			else
			{
				button.ForeColor=Color.White;
			}
		}

		private void buttonPalette0_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette0);
		}

		private void buttonPalette1_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette1);
		}

		private void buttonPalette2_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette2);
		}

		private void buttonPalette3_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette3);
		}

		private void buttonPalette4_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette4);
		}

		private void buttonPalette5_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette5);
		}

		private void buttonPalette6_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette6);
		}

		private void buttonPalette7_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette7);
		}

		private void buttonPalette8_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette8);
		}

		private void buttonPalette9_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette9);
		}

		private void buttonPalette10_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette10);
		}

		private void buttonPalette11_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette11);
		}

		private void buttonPalette12_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette12);
		}

		private void buttonPalette13_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette13);
		}

		private void buttonPalette14_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette14);
		}

		private void buttonPalette15_BackColorChanged(object sender, System.EventArgs e)
		{
			this.buttonBackColorChanged(buttonPalette15);
		}
	}
}
