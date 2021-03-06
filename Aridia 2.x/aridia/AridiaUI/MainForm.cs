/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2021 Hugues Johnson

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Reflection;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using com.huguesjohnson.aridia.types;
using com.huguesjohnson.IPSCreator;
using com.huguesjohnson.MegaDriveIO;
using com.huguesjohnson.PaletteEditor;
using com.huguesjohnson.TileEditor;

namespace com.huguesjohnson.aridia.ui.AridiaUI
{
	/// <summary>
	/// Main form for Aridia user interface.
	/// </summary>
	public class MainForm : System.Windows.Forms.Form
	{
		private IMegaDriveIO romIO;
        private const String GOOD_HEADER="SEGA MEGA DRIVE (C)SEGA 1991.APLTOKINO          KEISHOUSHA      PHANTASY STAR 3 PHANTASY STAR 3 GENERATIONS     OF DOOM         GM 1303-01";
        //used to lookup which sprites are available in an area
        private LookupValueCollection npcSprites=null;
        //used to determine if the script tab needs to be reloaded due to a change in the NPC tab
        private bool scriptDirtyFlag=false;
        private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
        private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.TabControl tabControlMainContent;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.TabPage tabPageMain;
		private System.Windows.Forms.TabPage tabPageScript;
		private System.Windows.Forms.TabPage tabPageDialogText;
        private System.Windows.Forms.TabPage tabPageItems;
		private System.Windows.Forms.TabPage tabPageCharacters;
        private System.Windows.Forms.TabPage tabPageEnemies;
		private System.Windows.Forms.StatusBarPanel statusBarPanel;
		private System.Windows.Forms.Label labelRom;
		private System.Windows.Forms.TextBox textBoxRomPath;
		private System.Windows.Forms.Button buttonOpenRom;
		private System.Windows.Forms.ImageList imageListIcons;
		private System.Windows.Forms.Button buttonChecksum;
		private System.Windows.Forms.Label labelWarning;
		private System.Windows.Forms.TextBox textBoxWarningText;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItemOpen;
		private System.Windows.Forms.MenuItem menuItemChecksum;
		private System.Windows.Forms.MenuItem menuItemHomepage;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.PictureBox pictureBoxWarning;
		private System.Windows.Forms.OpenFileDialog openFileRomDialog;
		private System.Windows.Forms.Label labelRomHeader;
		private System.Windows.Forms.TextBox textBoxRomHeader;
		private System.Windows.Forms.Label labelChecksum;
		private System.Windows.Forms.TextBox textBoxChecksum;
		private System.Windows.Forms.ListView listViewDialogText;
		private System.Windows.Forms.ColumnHeader columnDialogTextHeaderCurrentValue;
		private System.Windows.Forms.ColumnHeader columnDialogTextCategory;
		private System.Windows.Forms.ColumnHeader columnDialogTextDescription;
		private System.Windows.Forms.ColumnHeader columnDialogTextAddress;
		private System.Windows.Forms.ColumnHeader columncolumnDialogTextLength;
		private System.ComponentModel.IContainer components;
		private ListViewColumnSorter listViewPalettesSorter;
        private ListViewColumnSorter listViewTreasureSorter;
        private System.Windows.Forms.Label labelSelectItem;
		private System.Windows.Forms.ComboBox comboBoxSelectItem;
		private System.Windows.Forms.Panel panelItem;
		private System.Windows.Forms.Label labelItemCost;
		private System.Windows.Forms.Label labelItemTechnique;
		private System.Windows.Forms.Label labelItemEffectiveness;
		private System.Windows.Forms.TextBox textBoxItemCost;
		private System.Windows.Forms.ComboBox comboBoxItemTechnique;
		private System.Windows.Forms.TextBox textBoxItemEffectiveness;
		private System.Windows.Forms.TextBox textBoxItemAddress;
		private System.Windows.Forms.Label labelItemAddress;
		private System.Windows.Forms.ListView listViewScript;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
        private ListViewColumnSorter listViewDialogTextSorter;
        private ListViewColumnSorter listViewScriptSorter;
		private System.Windows.Forms.ComboBox comboBoxSelectCharacter;
		private System.Windows.Forms.Label labelSelectCharacter;
		private System.Windows.Forms.Panel panelCharacter;
		private System.Windows.Forms.TextBox textBoxCharacterAddress;
		private System.Windows.Forms.Label labelCharacterAddress;
		private System.Windows.Forms.TextBox textBoxCharacterName;
		private System.Windows.Forms.Label labelCharacterName;
		private System.Windows.Forms.Label labelCharacterType;
		private System.Windows.Forms.ComboBox comboBoxCharacterTechniques;
		private System.Windows.Forms.TextBox textBoxCharacterHitPoints;
		private System.Windows.Forms.Label labelCharacterHitPoints;
		private System.Windows.Forms.Label labelCharacterTechPoints;
		private System.Windows.Forms.TextBox textBoxCharacterAttack;
		private System.Windows.Forms.Label labelCharacterAttack;
		private System.Windows.Forms.TextBox textBoxCharacterDefense;
		private System.Windows.Forms.Label labelCharacterDefense;
		private System.Windows.Forms.TextBox textBoxCharacterLuck;
		private System.Windows.Forms.Label labelCharacterLuck;
		private System.Windows.Forms.TextBox textBoxCharacterSkill;
		private System.Windows.Forms.Label labelCharacterSkill;
		private System.Windows.Forms.TextBox textBoxCharacterSpeed;
        private System.Windows.Forms.Label labelCharacterSpeed;
		private System.Windows.Forms.Label labelCharacterItems;
		private System.Windows.Forms.ListView listViewCharacterItems;
		private System.Windows.Forms.ComboBox comboBoxCharacterType;
        private System.Windows.Forms.TextBox textBoxCharacterTechPoints;
		private System.Windows.Forms.ColumnHeader columnHeaderCharacterItemHexString;
		private System.Windows.Forms.ColumnHeader columnHeaderCharacterItemIsEquipped;
		private System.Windows.Forms.ColumnHeader columnHeaderCharacterItemItem;
		private System.Windows.Forms.ColumnHeader columnHeaderCharacterItemWhereEquipped;
		private System.Windows.Forms.ComboBox comboBoxSelectEnemy;
		private System.Windows.Forms.Label labelSelectEnemy;
		private System.Windows.Forms.TextBox textBoxEnemyAddress;
		private System.Windows.Forms.Label labelEnemyAddress;
		private System.Windows.Forms.ComboBox comboBoxEnemySpriteGroup;
		private System.Windows.Forms.Label labelEnemySpriteGroup;
		private System.Windows.Forms.ComboBox comboBoxEnemyAnimation;
		private System.Windows.Forms.Label labelEnemyAnimation;
		private System.Windows.Forms.ComboBox comboBoxEnemyTechnique;
		private System.Windows.Forms.Label labelEnemyTechnique;
		private System.Windows.Forms.TextBox textBoxEnemyHitPoints;
		private System.Windows.Forms.Label labelEnemyHitPoints;
		private System.Windows.Forms.TextBox textBoxEnemyAttack;
		private System.Windows.Forms.Label labelEnemyAttack;
		private System.Windows.Forms.TextBox textBoxEnemyDefense;
		private System.Windows.Forms.Label labelEnemyDefense;
		private System.Windows.Forms.TextBox textBoxEnemySpeed;
		private System.Windows.Forms.Label labelEnemySpeed;
		private System.Windows.Forms.Panel panelEnemy;
		private System.Windows.Forms.TextBox textBoxEnemyExperience;
		private System.Windows.Forms.Label labelEnemyExperience;
		private System.Windows.Forms.TextBox textBoxEnemyMeseta;
		private System.Windows.Forms.Label labelEnemyMeseta;
		private System.Windows.Forms.TextBox textBoxEnemyNameOffset;
		private System.Windows.Forms.Label labelEnemyNameOffset;
		private System.Windows.Forms.TextBox textBoxEnemyName;
		private System.Windows.Forms.Label labelEnemyName;
		private System.Windows.Forms.TextBox textBoxCalculatedChecksum;
		private System.Windows.Forms.Label labelCalculatedChecksum;
        private System.Windows.Forms.TabPage tabPageGraphics;
		private System.Windows.Forms.MenuItem menuItemThanks;
		private System.Windows.Forms.TextBox textBoxFindScript;
		private System.Windows.Forms.Button buttonFindScriptNext;
		private System.Windows.Forms.Button buttonFindScriptPrevious;
		private System.Windows.Forms.Label labelFindScript;
		private ListViewColumnSorter listViewInventoryNamesSorter;
		private System.Windows.Forms.TabPage tabPagePalettes;
		private System.Windows.Forms.ListView listViewPalettes;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.Button buttonLaunchPaletteEditor;
		private System.Windows.Forms.LinkLabel linkLabelItemEditing;
		private System.Windows.Forms.ContextMenu contextMenuCharacterMenu;
		private System.Windows.Forms.MenuItem menuItemEditCharacterItem;
		private System.Windows.Forms.PictureBox pictureBoxPalettePreview;
		private System.Windows.Forms.Label labelPalettePreview;
		private System.Windows.Forms.Label labelCharacterTechPower;
		private System.Windows.Forms.Label labelCharacterTechniqueMelee;
		private System.Windows.Forms.ComboBox comboBoxCharacterTechniqueMelee;
		private System.Windows.Forms.ComboBox comboBoxCharacterTechniqueHeal;
		private System.Windows.Forms.Label labelCharacterTechniqueHeal;
		private System.Windows.Forms.ComboBox comboBoxCharacterTechniqueTime;
		private System.Windows.Forms.Label labelCharacterTechniqueTime;
		private System.Windows.Forms.ComboBox comboBoxCharacterTechniqueOrder;
		private System.Windows.Forms.Label labelCharacterTechniqueOrder;
		private System.Windows.Forms.Label labelPaletteFind;
		private System.Windows.Forms.Button buttonPalettePrevious;
		private System.Windows.Forms.Button buttonPaletteFind;
		private System.Windows.Forms.TextBox textBoxPaletteFind;
		private int scriptFindIndex;
		private System.Windows.Forms.TabPage tabPageShops;
		private System.Windows.Forms.Label labelSelectShop;
		private System.Windows.Forms.TextBox textBoxShopAddress;
		private System.Windows.Forms.Label labelShopAddress;
		private System.Windows.Forms.ComboBox comboBoxItem1;
		private System.Windows.Forms.Label labelItem1;
		private System.Windows.Forms.ComboBox comboBoxItem2;
		private System.Windows.Forms.Label labelItem2;
		private System.Windows.Forms.ComboBox comboBoxItem3;
		private System.Windows.Forms.Label labelItem3;
		private System.Windows.Forms.ComboBox comboBoxItem4;
		private System.Windows.Forms.Label labelItem4;
		private System.Windows.Forms.ComboBox comboBoxItem5;
		private System.Windows.Forms.Label labelItem5;
		private System.Windows.Forms.ComboBox comboBoxSelectShop;
		private System.Windows.Forms.Panel panelShop;
		private System.Windows.Forms.TabPage tabPageLevelTables;
		private System.Windows.Forms.ComboBox comboBoxLevelTable;
		private System.Windows.Forms.Label labelSelectLevelTable;
		private System.Windows.Forms.ListView listViewLevelTable;
		private System.Windows.Forms.ColumnHeader columnHeaderXP;
		private System.Windows.Forms.ColumnHeader columnHeaderLevel;
		private System.Windows.Forms.ColumnHeader columnHeaderLevelTableAddress;
		private System.Windows.Forms.GroupBox groupBoxStatisticGrowth;
		private System.Windows.Forms.ComboBox comboBoxLevelTableSpeed;
		private System.Windows.Forms.Label labelLevelTableSpeed;
		private System.Windows.Forms.ComboBox comboBoxLevelTableSkill;
		private System.Windows.Forms.Label labelLevelTableSkill;
		private System.Windows.Forms.ComboBox comboBoxLevelTableDamage;
		private System.Windows.Forms.Label labelLevelTableDamage;
		private System.Windows.Forms.ComboBox comboBoxLevelTableLuck;
		private System.Windows.Forms.Label labelLevelTableLuck;
		private System.Windows.Forms.ComboBox comboBoxLevelTableDefense;
		private System.Windows.Forms.Label labelLevelTableDefense;
		private System.Windows.Forms.ComboBox comboBoxLevelTableTP;
		private System.Windows.Forms.Label labelLevelTableTP;
		private System.Windows.Forms.ComboBox comboBoxLevelTableHP;
		private System.Windows.Forms.Label labelLevelTableHP;
		private System.Windows.Forms.TextBox textBoxEnemyTechniqueLevel;
		private System.Windows.Forms.Label labelEnemyTechniqueLevel;
		private System.Windows.Forms.TextBox textBoxEnemyTechniqueCastPercent;
		private System.Windows.Forms.Label labelEnemyTechniqueCastPercent;
		private System.Windows.Forms.TextBox textBoxEnemyEscapePercent;
		private System.Windows.Forms.Label labelEnemyEscapePercent;
        private Button buttonCreateIPS;
        private MenuItem menuItemCreateIPSFile;
        private CheckBox checkBoxAutoFixChecksum;
        private TabPage tabPageTreasure;
        private ListView listViewTreasure;
        private ColumnHeader columnHeader9;
        private ColumnHeader columnHeader11;
        private ComboBox comboBoxTreasureItem;
        private Label labelTreasureItem;
        private TextBox textBoxTreasureY;
        private Label labelTreasureY;
        private TextBox textBoxTreasureX;
        private Label labelTreasureX;
        private Label labelItemEquipBy;
        private TextBox textBoxItemSpeed;
        private Label labelItemSpeed;
        private TextBox textBoxItemDefense;
        private Label labelItemDefense;
        private TextBox textBoxItemAttack;
        private Label labelItemAttack;
        private ComboBox comboBoxItemAnimation;
        private Label labelItemAnimation;
        private TextBox textBoxItemNameLength;
        private Label labelItemNameLength;
        private TextBox textBoxItemNameAddress;
        private Label labelItemNameAddress;
        private TextBox textBoxItemName;
        private Label labelItemName;
        private CheckBox checkBoxEquipLayaGwyn;
        private CheckBox checkBoxEquipLenaSari;
        private CheckBox checkBoxEquipLyleRyan;
        private CheckBox checkBoxEquipTheaKara;
        private CheckBox checkBoxEquipWren;
        private CheckBox checkBoxEquipMieu;
        private CheckBox checkBoxEquipAynSeanCrysAdanAron;
        private Label labeltemFlags;
        private CheckBox checkBoxItemFlag1;
        private CheckBox checkBoxItemFlag0;
        private CheckBox checkBoxEquipRhysNial;
        private CheckBox checkBoxItemFlag2;
        private CheckBox checkBoxItemFlag3;
        private ComboBox comboBoxItemEquipWhere;
        private Label labelEquipWhere;
        private TabPage tabPageEnemyGroups;
        private Panel panelEnemyGroups;
        private ListBox listBoxSelectEnemyGroup;
        private Label labelSelectEnemyGroup;
        private Label labelEnemyGroupBetaFeature;
        private TextBox textBoxGroupAddress;
        private Label labelGroupAddress;
        private ComboBox comboBoxBackRowEnemyCount;
        private Label labelBackRowEnemyCount;
        private ComboBox comboBoxBackRowEnemy;
        private Label labelBackRowEnemy;
        private ComboBox comboBoxFrontRowEnemyCount;
        private Label labelFrontRowEnemyCount;
        private ComboBox comboBoxFrontRowEnemy;
        private Label labelFrontRowEnemy;
        private MenuItem menuItemHelp;
        private TabPage tabPageNPCs;
        private ComboBox comboBoxNPCsSelectLocation;
        private Label labelNPCsSelectLocation;
        private Panel panelNPCs;
        private ListBox listBoxNPCs;
        private Label labelNPCs;
        private ComboBox comboBoxNPCSprite;
        private Label labelNPCSprite;
        private TextBox textBoxNPCY;
        private Label labelNPCY;
        private TextBox textBoxNPCX;
        private Label labelNPCX;
        private TextBox textBoxNPCUpdateRate;
        private Label labelNPCUpdateRate;
        private TextBox textBoxNPCScriptAddress;
        private Label labelNPCScriptAddress;
        private TextBox textBoxNPCScriptText;
        private Label labelNPCScriptText;
        private TextBox textBoxNPCAddress;
        private Label labelNPCAddress;
        private Label labelNPCsBetaFeature;
        private TabPage tabPageScriptedEvents;
        private ComboBox comboBoxSelectScriptedEvent;
        private Label labelSelectScriptedEvent;
        private Panel panelScriptedEvents;
        private TextBox textBoxScriptedEventDialogValue;
        private Label labelScriptedEventDialogValue;
        private Label labelScriptedEventDialogAddress;
        private RadioButton radioButtonScriptedEventDialog;
        private Label labelScriptedEventDuration;
        private ComboBox comboBoxScriptedEventDirection;
        private Label labelScriptedEventDirection;
        private RadioButton radioButtonScriptedEventMovement;
        private ListBox listBoxScriptedEventFrames;
        private Label labelScriptedEventFrames;
        private TextBox textBoxScriptedEventDuration;
        private TextBox textBoxScriptedEventDelay;
        private Label labelScriptedEventDelay;
        private RadioButton radioButtonScriptedEventDelay;
        private ComboBox comboBoxScriptedEventDialogOffset;
        private Label labelSelectTileset;
        private ListBox listBoxTileSets;
        private TextBox textBoxTilesetStartAddress;
        private Label labelTilesetStartAddress;
        private Button buttonLaunchTileEditor;
        private TextBox textBoxTilesetEndAddress;
        private Label labelTilesetEndAddress;
        private TextBox textBoxTilesetHeight;
        private Label labelTilesetHeight;
        private TextBox textBoxTilesetWidth;
        private Label labelTilesetWidth;
        private TextBox textBoxTilesetRows;
        private Label labelTilesetRows;
        private TextBox textBoxTilesetColumns;
        private Label labelTilesetColumns;
		private int paletteFindIndex;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//setup listview sorters
			this.listViewDialogTextSorter=new ListViewColumnSorter();
			this.listViewDialogText.ListViewItemSorter=listViewDialogTextSorter;
			this.listViewScriptSorter=new ListViewColumnSorter();
			this.listViewScript.ListViewItemSorter=listViewScriptSorter;
			this.listViewPalettesSorter=new ListViewColumnSorter();
			this.listViewPalettes.ListViewItemSorter=listViewPalettesSorter;
            this.listViewTreasureSorter=new ListViewColumnSorter();
            this.listViewTreasure.ListViewItemSorter=listViewTreasureSorter;
            this.tabControlMainContent.SelectedIndex=0;
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemOpen = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItemCreateIPSFile = new System.Windows.Forms.MenuItem();
            this.menuItemChecksum = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemHomepage = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.menuItemThanks = new System.Windows.Forms.MenuItem();
            this.menuItem8 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.tabControlMainContent = new System.Windows.Forms.TabControl();
            this.tabPageMain = new System.Windows.Forms.TabPage();
            this.checkBoxAutoFixChecksum = new System.Windows.Forms.CheckBox();
            this.buttonCreateIPS = new System.Windows.Forms.Button();
            this.textBoxCalculatedChecksum = new System.Windows.Forms.TextBox();
            this.labelCalculatedChecksum = new System.Windows.Forms.Label();
            this.textBoxChecksum = new System.Windows.Forms.TextBox();
            this.labelChecksum = new System.Windows.Forms.Label();
            this.textBoxRomHeader = new System.Windows.Forms.TextBox();
            this.labelRomHeader = new System.Windows.Forms.Label();
            this.textBoxWarningText = new System.Windows.Forms.TextBox();
            this.labelWarning = new System.Windows.Forms.Label();
            this.pictureBoxWarning = new System.Windows.Forms.PictureBox();
            this.buttonChecksum = new System.Windows.Forms.Button();
            this.imageListIcons = new System.Windows.Forms.ImageList(this.components);
            this.tabPageCharacters = new System.Windows.Forms.TabPage();
            this.panelCharacter = new System.Windows.Forms.Panel();
            this.comboBoxCharacterTechniqueOrder = new System.Windows.Forms.ComboBox();
            this.labelCharacterTechniqueOrder = new System.Windows.Forms.Label();
            this.comboBoxCharacterTechniqueTime = new System.Windows.Forms.ComboBox();
            this.labelCharacterTechniqueTime = new System.Windows.Forms.Label();
            this.comboBoxCharacterTechniqueHeal = new System.Windows.Forms.ComboBox();
            this.labelCharacterTechniqueHeal = new System.Windows.Forms.Label();
            this.comboBoxCharacterTechniqueMelee = new System.Windows.Forms.ComboBox();
            this.labelCharacterTechniqueMelee = new System.Windows.Forms.Label();
            this.linkLabelItemEditing = new System.Windows.Forms.LinkLabel();
            this.listViewCharacterItems = new System.Windows.Forms.ListView();
            this.columnHeaderCharacterItemHexString = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCharacterItemItem = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCharacterItemIsEquipped = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCharacterItemWhereEquipped = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuCharacterMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemEditCharacterItem = new System.Windows.Forms.MenuItem();
            this.labelCharacterItems = new System.Windows.Forms.Label();
            this.textBoxCharacterSpeed = new System.Windows.Forms.TextBox();
            this.labelCharacterSpeed = new System.Windows.Forms.Label();
            this.textBoxCharacterSkill = new System.Windows.Forms.TextBox();
            this.labelCharacterSkill = new System.Windows.Forms.Label();
            this.textBoxCharacterLuck = new System.Windows.Forms.TextBox();
            this.labelCharacterLuck = new System.Windows.Forms.Label();
            this.textBoxCharacterDefense = new System.Windows.Forms.TextBox();
            this.labelCharacterDefense = new System.Windows.Forms.Label();
            this.textBoxCharacterAttack = new System.Windows.Forms.TextBox();
            this.labelCharacterAttack = new System.Windows.Forms.Label();
            this.textBoxCharacterTechPoints = new System.Windows.Forms.TextBox();
            this.labelCharacterTechPoints = new System.Windows.Forms.Label();
            this.textBoxCharacterHitPoints = new System.Windows.Forms.TextBox();
            this.labelCharacterHitPoints = new System.Windows.Forms.Label();
            this.comboBoxCharacterTechniques = new System.Windows.Forms.ComboBox();
            this.labelCharacterTechPower = new System.Windows.Forms.Label();
            this.comboBoxCharacterType = new System.Windows.Forms.ComboBox();
            this.labelCharacterType = new System.Windows.Forms.Label();
            this.textBoxCharacterName = new System.Windows.Forms.TextBox();
            this.labelCharacterName = new System.Windows.Forms.Label();
            this.textBoxCharacterAddress = new System.Windows.Forms.TextBox();
            this.labelCharacterAddress = new System.Windows.Forms.Label();
            this.comboBoxSelectCharacter = new System.Windows.Forms.ComboBox();
            this.labelSelectCharacter = new System.Windows.Forms.Label();
            this.tabPageDialogText = new System.Windows.Forms.TabPage();
            this.listViewDialogText = new System.Windows.Forms.ListView();
            this.columnDialogTextHeaderCurrentValue = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDialogTextCategory = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDialogTextDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDialogTextAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columncolumnDialogTextLength = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageEnemies = new System.Windows.Forms.TabPage();
            this.panelEnemy = new System.Windows.Forms.Panel();
            this.textBoxEnemyEscapePercent = new System.Windows.Forms.TextBox();
            this.labelEnemyEscapePercent = new System.Windows.Forms.Label();
            this.textBoxEnemyTechniqueCastPercent = new System.Windows.Forms.TextBox();
            this.labelEnemyTechniqueCastPercent = new System.Windows.Forms.Label();
            this.textBoxEnemyTechniqueLevel = new System.Windows.Forms.TextBox();
            this.labelEnemyTechniqueLevel = new System.Windows.Forms.Label();
            this.textBoxEnemyName = new System.Windows.Forms.TextBox();
            this.labelEnemyName = new System.Windows.Forms.Label();
            this.textBoxEnemyNameOffset = new System.Windows.Forms.TextBox();
            this.labelEnemyNameOffset = new System.Windows.Forms.Label();
            this.textBoxEnemyMeseta = new System.Windows.Forms.TextBox();
            this.labelEnemyMeseta = new System.Windows.Forms.Label();
            this.textBoxEnemyExperience = new System.Windows.Forms.TextBox();
            this.labelEnemyExperience = new System.Windows.Forms.Label();
            this.textBoxEnemySpeed = new System.Windows.Forms.TextBox();
            this.labelEnemySpeed = new System.Windows.Forms.Label();
            this.textBoxEnemyDefense = new System.Windows.Forms.TextBox();
            this.labelEnemyDefense = new System.Windows.Forms.Label();
            this.textBoxEnemyAttack = new System.Windows.Forms.TextBox();
            this.labelEnemyAttack = new System.Windows.Forms.Label();
            this.textBoxEnemyHitPoints = new System.Windows.Forms.TextBox();
            this.labelEnemyHitPoints = new System.Windows.Forms.Label();
            this.comboBoxEnemyTechnique = new System.Windows.Forms.ComboBox();
            this.labelEnemyTechnique = new System.Windows.Forms.Label();
            this.comboBoxEnemyAnimation = new System.Windows.Forms.ComboBox();
            this.labelEnemyAnimation = new System.Windows.Forms.Label();
            this.comboBoxEnemySpriteGroup = new System.Windows.Forms.ComboBox();
            this.labelEnemySpriteGroup = new System.Windows.Forms.Label();
            this.textBoxEnemyAddress = new System.Windows.Forms.TextBox();
            this.labelEnemyAddress = new System.Windows.Forms.Label();
            this.comboBoxSelectEnemy = new System.Windows.Forms.ComboBox();
            this.labelSelectEnemy = new System.Windows.Forms.Label();
            this.tabPageEnemyGroups = new System.Windows.Forms.TabPage();
            this.panelEnemyGroups = new System.Windows.Forms.Panel();
            this.comboBoxBackRowEnemyCount = new System.Windows.Forms.ComboBox();
            this.labelBackRowEnemyCount = new System.Windows.Forms.Label();
            this.comboBoxBackRowEnemy = new System.Windows.Forms.ComboBox();
            this.labelBackRowEnemy = new System.Windows.Forms.Label();
            this.comboBoxFrontRowEnemyCount = new System.Windows.Forms.ComboBox();
            this.labelFrontRowEnemyCount = new System.Windows.Forms.Label();
            this.comboBoxFrontRowEnemy = new System.Windows.Forms.ComboBox();
            this.labelFrontRowEnemy = new System.Windows.Forms.Label();
            this.labelEnemyGroupBetaFeature = new System.Windows.Forms.Label();
            this.textBoxGroupAddress = new System.Windows.Forms.TextBox();
            this.labelGroupAddress = new System.Windows.Forms.Label();
            this.listBoxSelectEnemyGroup = new System.Windows.Forms.ListBox();
            this.labelSelectEnemyGroup = new System.Windows.Forms.Label();
            this.tabPageGraphics = new System.Windows.Forms.TabPage();
            this.tabPageItems = new System.Windows.Forms.TabPage();
            this.panelItem = new System.Windows.Forms.Panel();
            this.comboBoxItemEquipWhere = new System.Windows.Forms.ComboBox();
            this.labelEquipWhere = new System.Windows.Forms.Label();
            this.checkBoxItemFlag3 = new System.Windows.Forms.CheckBox();
            this.checkBoxItemFlag2 = new System.Windows.Forms.CheckBox();
            this.checkBoxItemFlag1 = new System.Windows.Forms.CheckBox();
            this.checkBoxItemFlag0 = new System.Windows.Forms.CheckBox();
            this.labeltemFlags = new System.Windows.Forms.Label();
            this.checkBoxEquipLayaGwyn = new System.Windows.Forms.CheckBox();
            this.checkBoxEquipLenaSari = new System.Windows.Forms.CheckBox();
            this.checkBoxEquipLyleRyan = new System.Windows.Forms.CheckBox();
            this.checkBoxEquipTheaKara = new System.Windows.Forms.CheckBox();
            this.checkBoxEquipWren = new System.Windows.Forms.CheckBox();
            this.checkBoxEquipMieu = new System.Windows.Forms.CheckBox();
            this.checkBoxEquipAynSeanCrysAdanAron = new System.Windows.Forms.CheckBox();
            this.checkBoxEquipRhysNial = new System.Windows.Forms.CheckBox();
            this.textBoxItemName = new System.Windows.Forms.TextBox();
            this.labelItemName = new System.Windows.Forms.Label();
            this.textBoxItemNameLength = new System.Windows.Forms.TextBox();
            this.labelItemNameLength = new System.Windows.Forms.Label();
            this.textBoxItemNameAddress = new System.Windows.Forms.TextBox();
            this.labelItemNameAddress = new System.Windows.Forms.Label();
            this.labelItemEquipBy = new System.Windows.Forms.Label();
            this.textBoxItemSpeed = new System.Windows.Forms.TextBox();
            this.labelItemSpeed = new System.Windows.Forms.Label();
            this.textBoxItemDefense = new System.Windows.Forms.TextBox();
            this.labelItemDefense = new System.Windows.Forms.Label();
            this.textBoxItemAttack = new System.Windows.Forms.TextBox();
            this.labelItemAttack = new System.Windows.Forms.Label();
            this.comboBoxItemAnimation = new System.Windows.Forms.ComboBox();
            this.labelItemAnimation = new System.Windows.Forms.Label();
            this.textBoxItemAddress = new System.Windows.Forms.TextBox();
            this.labelItemAddress = new System.Windows.Forms.Label();
            this.textBoxItemEffectiveness = new System.Windows.Forms.TextBox();
            this.comboBoxItemTechnique = new System.Windows.Forms.ComboBox();
            this.textBoxItemCost = new System.Windows.Forms.TextBox();
            this.labelItemEffectiveness = new System.Windows.Forms.Label();
            this.labelItemTechnique = new System.Windows.Forms.Label();
            this.labelItemCost = new System.Windows.Forms.Label();
            this.comboBoxSelectItem = new System.Windows.Forms.ComboBox();
            this.labelSelectItem = new System.Windows.Forms.Label();
            this.tabPageLevelTables = new System.Windows.Forms.TabPage();
            this.groupBoxStatisticGrowth = new System.Windows.Forms.GroupBox();
            this.comboBoxLevelTableSpeed = new System.Windows.Forms.ComboBox();
            this.labelLevelTableSpeed = new System.Windows.Forms.Label();
            this.comboBoxLevelTableSkill = new System.Windows.Forms.ComboBox();
            this.labelLevelTableSkill = new System.Windows.Forms.Label();
            this.comboBoxLevelTableDamage = new System.Windows.Forms.ComboBox();
            this.labelLevelTableDamage = new System.Windows.Forms.Label();
            this.comboBoxLevelTableLuck = new System.Windows.Forms.ComboBox();
            this.labelLevelTableLuck = new System.Windows.Forms.Label();
            this.comboBoxLevelTableDefense = new System.Windows.Forms.ComboBox();
            this.labelLevelTableDefense = new System.Windows.Forms.Label();
            this.comboBoxLevelTableTP = new System.Windows.Forms.ComboBox();
            this.labelLevelTableTP = new System.Windows.Forms.Label();
            this.comboBoxLevelTableHP = new System.Windows.Forms.ComboBox();
            this.labelLevelTableHP = new System.Windows.Forms.Label();
            this.listViewLevelTable = new System.Windows.Forms.ListView();
            this.columnHeaderXP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLevelTableAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxLevelTable = new System.Windows.Forms.ComboBox();
            this.labelSelectLevelTable = new System.Windows.Forms.Label();
            this.tabPageNPCs = new System.Windows.Forms.TabPage();
            this.panelNPCs = new System.Windows.Forms.Panel();
            this.labelNPCsBetaFeature = new System.Windows.Forms.Label();
            this.textBoxNPCAddress = new System.Windows.Forms.TextBox();
            this.labelNPCAddress = new System.Windows.Forms.Label();
            this.textBoxNPCScriptText = new System.Windows.Forms.TextBox();
            this.labelNPCScriptText = new System.Windows.Forms.Label();
            this.textBoxNPCScriptAddress = new System.Windows.Forms.TextBox();
            this.labelNPCScriptAddress = new System.Windows.Forms.Label();
            this.textBoxNPCUpdateRate = new System.Windows.Forms.TextBox();
            this.labelNPCUpdateRate = new System.Windows.Forms.Label();
            this.textBoxNPCY = new System.Windows.Forms.TextBox();
            this.labelNPCY = new System.Windows.Forms.Label();
            this.textBoxNPCX = new System.Windows.Forms.TextBox();
            this.labelNPCX = new System.Windows.Forms.Label();
            this.comboBoxNPCSprite = new System.Windows.Forms.ComboBox();
            this.labelNPCSprite = new System.Windows.Forms.Label();
            this.listBoxNPCs = new System.Windows.Forms.ListBox();
            this.labelNPCs = new System.Windows.Forms.Label();
            this.comboBoxNPCsSelectLocation = new System.Windows.Forms.ComboBox();
            this.labelNPCsSelectLocation = new System.Windows.Forms.Label();
            this.tabPagePalettes = new System.Windows.Forms.TabPage();
            this.labelPaletteFind = new System.Windows.Forms.Label();
            this.buttonPalettePrevious = new System.Windows.Forms.Button();
            this.buttonPaletteFind = new System.Windows.Forms.Button();
            this.textBoxPaletteFind = new System.Windows.Forms.TextBox();
            this.labelPalettePreview = new System.Windows.Forms.Label();
            this.pictureBoxPalettePreview = new System.Windows.Forms.PictureBox();
            this.buttonLaunchPaletteEditor = new System.Windows.Forms.Button();
            this.listViewPalettes = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageScript = new System.Windows.Forms.TabPage();
            this.labelFindScript = new System.Windows.Forms.Label();
            this.buttonFindScriptPrevious = new System.Windows.Forms.Button();
            this.buttonFindScriptNext = new System.Windows.Forms.Button();
            this.textBoxFindScript = new System.Windows.Forms.TextBox();
            this.listViewScript = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageScriptedEvents = new System.Windows.Forms.TabPage();
            this.panelScriptedEvents = new System.Windows.Forms.Panel();
            this.comboBoxScriptedEventDialogOffset = new System.Windows.Forms.ComboBox();
            this.textBoxScriptedEventDelay = new System.Windows.Forms.TextBox();
            this.labelScriptedEventDelay = new System.Windows.Forms.Label();
            this.radioButtonScriptedEventDelay = new System.Windows.Forms.RadioButton();
            this.textBoxScriptedEventDuration = new System.Windows.Forms.TextBox();
            this.textBoxScriptedEventDialogValue = new System.Windows.Forms.TextBox();
            this.labelScriptedEventDialogValue = new System.Windows.Forms.Label();
            this.labelScriptedEventDialogAddress = new System.Windows.Forms.Label();
            this.radioButtonScriptedEventDialog = new System.Windows.Forms.RadioButton();
            this.labelScriptedEventDuration = new System.Windows.Forms.Label();
            this.comboBoxScriptedEventDirection = new System.Windows.Forms.ComboBox();
            this.labelScriptedEventDirection = new System.Windows.Forms.Label();
            this.radioButtonScriptedEventMovement = new System.Windows.Forms.RadioButton();
            this.listBoxScriptedEventFrames = new System.Windows.Forms.ListBox();
            this.labelScriptedEventFrames = new System.Windows.Forms.Label();
            this.comboBoxSelectScriptedEvent = new System.Windows.Forms.ComboBox();
            this.labelSelectScriptedEvent = new System.Windows.Forms.Label();
            this.tabPageShops = new System.Windows.Forms.TabPage();
            this.panelShop = new System.Windows.Forms.Panel();
            this.comboBoxItem5 = new System.Windows.Forms.ComboBox();
            this.labelItem5 = new System.Windows.Forms.Label();
            this.comboBoxItem4 = new System.Windows.Forms.ComboBox();
            this.labelItem4 = new System.Windows.Forms.Label();
            this.comboBoxItem3 = new System.Windows.Forms.ComboBox();
            this.labelItem3 = new System.Windows.Forms.Label();
            this.comboBoxItem2 = new System.Windows.Forms.ComboBox();
            this.labelItem2 = new System.Windows.Forms.Label();
            this.textBoxShopAddress = new System.Windows.Forms.TextBox();
            this.labelShopAddress = new System.Windows.Forms.Label();
            this.comboBoxItem1 = new System.Windows.Forms.ComboBox();
            this.labelItem1 = new System.Windows.Forms.Label();
            this.comboBoxSelectShop = new System.Windows.Forms.ComboBox();
            this.labelSelectShop = new System.Windows.Forms.Label();
            this.tabPageTreasure = new System.Windows.Forms.TabPage();
            this.comboBoxTreasureItem = new System.Windows.Forms.ComboBox();
            this.labelTreasureItem = new System.Windows.Forms.Label();
            this.textBoxTreasureY = new System.Windows.Forms.TextBox();
            this.labelTreasureY = new System.Windows.Forms.Label();
            this.textBoxTreasureX = new System.Windows.Forms.TextBox();
            this.labelTreasureX = new System.Windows.Forms.Label();
            this.listViewTreasure = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.buttonOpenRom = new System.Windows.Forms.Button();
            this.textBoxRomPath = new System.Windows.Forms.TextBox();
            this.labelRom = new System.Windows.Forms.Label();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.statusBarPanel = new System.Windows.Forms.StatusBarPanel();
            this.openFileRomDialog = new System.Windows.Forms.OpenFileDialog();
            this.listBoxTileSets = new System.Windows.Forms.ListBox();
            this.labelSelectTileset = new System.Windows.Forms.Label();
            this.buttonLaunchTileEditor = new System.Windows.Forms.Button();
            this.textBoxTilesetStartAddress = new System.Windows.Forms.TextBox();
            this.labelTilesetStartAddress = new System.Windows.Forms.Label();
            this.textBoxTilesetEndAddress = new System.Windows.Forms.TextBox();
            this.labelTilesetEndAddress = new System.Windows.Forms.Label();
            this.textBoxTilesetColumns = new System.Windows.Forms.TextBox();
            this.labelTilesetColumns = new System.Windows.Forms.Label();
            this.textBoxTilesetRows = new System.Windows.Forms.TextBox();
            this.labelTilesetRows = new System.Windows.Forms.Label();
            this.textBoxTilesetWidth = new System.Windows.Forms.TextBox();
            this.labelTilesetWidth = new System.Windows.Forms.Label();
            this.textBoxTilesetHeight = new System.Windows.Forms.TextBox();
            this.labelTilesetHeight = new System.Windows.Forms.Label();
            this.tabControlMainContent.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarning)).BeginInit();
            this.tabPageCharacters.SuspendLayout();
            this.panelCharacter.SuspendLayout();
            this.tabPageDialogText.SuspendLayout();
            this.tabPageEnemies.SuspendLayout();
            this.panelEnemy.SuspendLayout();
            this.tabPageEnemyGroups.SuspendLayout();
            this.panelEnemyGroups.SuspendLayout();
            this.tabPageGraphics.SuspendLayout();
            this.tabPageItems.SuspendLayout();
            this.panelItem.SuspendLayout();
            this.tabPageLevelTables.SuspendLayout();
            this.groupBoxStatisticGrowth.SuspendLayout();
            this.tabPageNPCs.SuspendLayout();
            this.panelNPCs.SuspendLayout();
            this.tabPagePalettes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPalettePreview)).BeginInit();
            this.tabPageScript.SuspendLayout();
            this.tabPageScriptedEvents.SuspendLayout();
            this.panelScriptedEvents.SuspendLayout();
            this.tabPageShops.SuspendLayout();
            this.panelShop.SuspendLayout();
            this.tabPageTreasure.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOpen,
            this.menuItem3,
            this.menuItemCreateIPSFile,
            this.menuItemChecksum,
            this.menuItem5,
            this.menuItemHomepage,
            this.menuItemHelp,
            this.menuItemAbout,
            this.menuItemThanks,
            this.menuItem8,
            this.menuItemExit});
            this.menuItem1.Text = "&Aridia";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index = 0;
            this.menuItemOpen.Text = "&Open ROM..";
            this.menuItemOpen.Click += new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.Text = "-";
            // 
            // menuItemCreateIPSFile
            // 
            this.menuItemCreateIPSFile.Enabled = false;
            this.menuItemCreateIPSFile.Index = 2;
            this.menuItemCreateIPSFile.Text = "Create &IPS File...";
            this.menuItemCreateIPSFile.Click += new System.EventHandler(this.menuItemCreateIPSFile_Click);
            // 
            // menuItemChecksum
            // 
            this.menuItemChecksum.Enabled = false;
            this.menuItemChecksum.Index = 3;
            this.menuItemChecksum.Text = "&Repair Checksum";
            this.menuItemChecksum.Click += new System.EventHandler(this.menuItemChecksum_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 4;
            this.menuItem5.Text = "-";
            // 
            // menuItemHomepage
            // 
            this.menuItemHomepage.Index = 5;
            this.menuItemHomepage.Text = "&Homepage..";
            this.menuItemHomepage.Click += new System.EventHandler(this.menuItemHomepage_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 6;
            this.menuItemHelp.Text = "Help..";
            this.menuItemHelp.Click += new System.EventHandler(this.menuItemHelp_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 7;
            this.menuItemAbout.Text = "&About..";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // menuItemThanks
            // 
            this.menuItemThanks.Index = 8;
            this.menuItemThanks.Text = "&Thanks..";
            this.menuItemThanks.Click += new System.EventHandler(this.menuItemThanks_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index = 9;
            this.menuItem8.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 10;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // tabControlMainContent
            // 
            this.tabControlMainContent.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlMainContent.Controls.Add(this.tabPageMain);
            this.tabControlMainContent.Controls.Add(this.tabPageCharacters);
            this.tabControlMainContent.Controls.Add(this.tabPageDialogText);
            this.tabControlMainContent.Controls.Add(this.tabPageEnemies);
            this.tabControlMainContent.Controls.Add(this.tabPageEnemyGroups);
            this.tabControlMainContent.Controls.Add(this.tabPageGraphics);
            this.tabControlMainContent.Controls.Add(this.tabPageItems);
            this.tabControlMainContent.Controls.Add(this.tabPageLevelTables);
            this.tabControlMainContent.Controls.Add(this.tabPageNPCs);
            this.tabControlMainContent.Controls.Add(this.tabPagePalettes);
            this.tabControlMainContent.Controls.Add(this.tabPageScript);
            this.tabControlMainContent.Controls.Add(this.tabPageScriptedEvents);
            this.tabControlMainContent.Controls.Add(this.tabPageShops);
            this.tabControlMainContent.Controls.Add(this.tabPageTreasure);
            this.tabControlMainContent.Location = new System.Drawing.Point(5, 58);
            this.tabControlMainContent.Multiline = true;
            this.tabControlMainContent.Name = "tabControlMainContent";
            this.tabControlMainContent.SelectedIndex = 0;
            this.tabControlMainContent.Size = new System.Drawing.Size(708, 438);
            this.tabControlMainContent.TabIndex = 1;
            this.tabControlMainContent.SelectedIndexChanged += new System.EventHandler(this.tabControlMainContent_SelectedIndexChanged);
            // 
            // tabPageMain
            // 
            this.tabPageMain.Controls.Add(this.checkBoxAutoFixChecksum);
            this.tabPageMain.Controls.Add(this.buttonCreateIPS);
            this.tabPageMain.Controls.Add(this.textBoxCalculatedChecksum);
            this.tabPageMain.Controls.Add(this.labelCalculatedChecksum);
            this.tabPageMain.Controls.Add(this.textBoxChecksum);
            this.tabPageMain.Controls.Add(this.labelChecksum);
            this.tabPageMain.Controls.Add(this.textBoxRomHeader);
            this.tabPageMain.Controls.Add(this.labelRomHeader);
            this.tabPageMain.Controls.Add(this.textBoxWarningText);
            this.tabPageMain.Controls.Add(this.labelWarning);
            this.tabPageMain.Controls.Add(this.pictureBoxWarning);
            this.tabPageMain.Controls.Add(this.buttonChecksum);
            this.tabPageMain.Location = new System.Drawing.Point(4, 55);
            this.tabPageMain.Name = "tabPageMain";
            this.tabPageMain.Size = new System.Drawing.Size(700, 379);
            this.tabPageMain.TabIndex = 0;
            this.tabPageMain.Text = "Main";
            this.tabPageMain.ToolTipText = "Open a game ROM and regenerate checksum";
            // 
            // checkBoxAutoFixChecksum
            // 
            this.checkBoxAutoFixChecksum.AutoSize = true;
            this.checkBoxAutoFixChecksum.Checked = true;
            this.checkBoxAutoFixChecksum.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAutoFixChecksum.Location = new System.Drawing.Point(374, 213);
            this.checkBoxAutoFixChecksum.Name = "checkBoxAutoFixChecksum";
            this.checkBoxAutoFixChecksum.Size = new System.Drawing.Size(278, 21);
            this.checkBoxAutoFixChecksum.TabIndex = 16;
            this.checkBoxAutoFixChecksum.Text = "Automatically repair checksum on close";
            this.checkBoxAutoFixChecksum.UseVisualStyleBackColor = true;
            // 
            // buttonCreateIPS
            // 
            this.buttonCreateIPS.Enabled = false;
            this.buttonCreateIPS.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreateIPS.Image = ((System.Drawing.Image)(resources.GetObject("buttonCreateIPS.Image")));
            this.buttonCreateIPS.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCreateIPS.Location = new System.Drawing.Point(503, 39);
            this.buttonCreateIPS.Name = "buttonCreateIPS";
            this.buttonCreateIPS.Size = new System.Drawing.Size(149, 37);
            this.buttonCreateIPS.TabIndex = 15;
            this.buttonCreateIPS.Text = "Create IPS File     ";
            this.buttonCreateIPS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCreateIPS.Click += new System.EventHandler(this.buttonCreateIPS_Click);
            // 
            // textBoxCalculatedChecksum
            // 
            this.textBoxCalculatedChecksum.Location = new System.Drawing.Point(503, 134);
            this.textBoxCalculatedChecksum.Name = "textBoxCalculatedChecksum";
            this.textBoxCalculatedChecksum.ReadOnly = true;
            this.textBoxCalculatedChecksum.Size = new System.Drawing.Size(149, 22);
            this.textBoxCalculatedChecksum.TabIndex = 12;
            // 
            // labelCalculatedChecksum
            // 
            this.labelCalculatedChecksum.Location = new System.Drawing.Point(330, 134);
            this.labelCalculatedChecksum.Name = "labelCalculatedChecksum";
            this.labelCalculatedChecksum.Size = new System.Drawing.Size(166, 23);
            this.labelCalculatedChecksum.TabIndex = 11;
            this.labelCalculatedChecksum.Text = "Calculated Checksum: ";
            this.labelCalculatedChecksum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxChecksum
            // 
            this.textBoxChecksum.Location = new System.Drawing.Point(503, 104);
            this.textBoxChecksum.Name = "textBoxChecksum";
            this.textBoxChecksum.ReadOnly = true;
            this.textBoxChecksum.Size = new System.Drawing.Size(149, 22);
            this.textBoxChecksum.TabIndex = 10;
            // 
            // labelChecksum
            // 
            this.labelChecksum.Location = new System.Drawing.Point(371, 104);
            this.labelChecksum.Name = "labelChecksum";
            this.labelChecksum.Size = new System.Drawing.Size(125, 23);
            this.labelChecksum.TabIndex = 9;
            this.labelChecksum.Text = "Stored Checksum: ";
            this.labelChecksum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxRomHeader
            // 
            this.textBoxRomHeader.Location = new System.Drawing.Point(134, 11);
            this.textBoxRomHeader.Name = "textBoxRomHeader";
            this.textBoxRomHeader.ReadOnly = true;
            this.textBoxRomHeader.Size = new System.Drawing.Size(518, 22);
            this.textBoxRomHeader.TabIndex = 8;
            // 
            // labelRomHeader
            // 
            this.labelRomHeader.Location = new System.Drawing.Point(10, 10);
            this.labelRomHeader.Name = "labelRomHeader";
            this.labelRomHeader.Size = new System.Drawing.Size(124, 23);
            this.labelRomHeader.TabIndex = 7;
            this.labelRomHeader.Text = "ROM Header: ";
            this.labelRomHeader.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxWarningText
            // 
            this.textBoxWarningText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxWarningText.Enabled = false;
            this.textBoxWarningText.Location = new System.Drawing.Point(13, 295);
            this.textBoxWarningText.Multiline = true;
            this.textBoxWarningText.Name = "textBoxWarningText";
            this.textBoxWarningText.Size = new System.Drawing.Size(639, 65);
            this.textBoxWarningText.TabIndex = 6;
            this.textBoxWarningText.Text = resources.GetString("textBoxWarningText.Text");
            // 
            // labelWarning
            // 
            this.labelWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWarning.Location = new System.Drawing.Point(116, 255);
            this.labelWarning.Name = "labelWarning";
            this.labelWarning.Size = new System.Drawing.Size(230, 18);
            this.labelWarning.TabIndex = 5;
            this.labelWarning.Text = "Warnings";
            // 
            // pictureBoxWarning
            // 
            this.pictureBoxWarning.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxWarning.Image")));
            this.pictureBoxWarning.Location = new System.Drawing.Point(13, 201);
            this.pictureBoxWarning.Name = "pictureBoxWarning";
            this.pictureBoxWarning.Size = new System.Drawing.Size(96, 92);
            this.pictureBoxWarning.TabIndex = 4;
            this.pictureBoxWarning.TabStop = false;
            // 
            // buttonChecksum
            // 
            this.buttonChecksum.Enabled = false;
            this.buttonChecksum.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonChecksum.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonChecksum.ImageIndex = 2;
            this.buttonChecksum.ImageList = this.imageListIcons;
            this.buttonChecksum.Location = new System.Drawing.Point(503, 170);
            this.buttonChecksum.Name = "buttonChecksum";
            this.buttonChecksum.Size = new System.Drawing.Size(149, 37);
            this.buttonChecksum.TabIndex = 3;
            this.buttonChecksum.Text = "Repair Checksum";
            this.buttonChecksum.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonChecksum.Click += new System.EventHandler(this.buttonChecksum_Click);
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListIcons.Images.SetKeyName(0, "");
            this.imageListIcons.Images.SetKeyName(1, "");
            this.imageListIcons.Images.SetKeyName(2, "");
            this.imageListIcons.Images.SetKeyName(3, "");
            this.imageListIcons.Images.SetKeyName(4, "");
            // 
            // tabPageCharacters
            // 
            this.tabPageCharacters.Controls.Add(this.panelCharacter);
            this.tabPageCharacters.Controls.Add(this.comboBoxSelectCharacter);
            this.tabPageCharacters.Controls.Add(this.labelSelectCharacter);
            this.tabPageCharacters.Location = new System.Drawing.Point(4, 55);
            this.tabPageCharacters.Name = "tabPageCharacters";
            this.tabPageCharacters.Size = new System.Drawing.Size(700, 379);
            this.tabPageCharacters.TabIndex = 5;
            this.tabPageCharacters.Text = "Characters";
            this.tabPageCharacters.ToolTipText = "Edit character names and properties";
            // 
            // panelCharacter
            // 
            this.panelCharacter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCharacter.Controls.Add(this.comboBoxCharacterTechniqueOrder);
            this.panelCharacter.Controls.Add(this.labelCharacterTechniqueOrder);
            this.panelCharacter.Controls.Add(this.comboBoxCharacterTechniqueTime);
            this.panelCharacter.Controls.Add(this.labelCharacterTechniqueTime);
            this.panelCharacter.Controls.Add(this.comboBoxCharacterTechniqueHeal);
            this.panelCharacter.Controls.Add(this.labelCharacterTechniqueHeal);
            this.panelCharacter.Controls.Add(this.comboBoxCharacterTechniqueMelee);
            this.panelCharacter.Controls.Add(this.labelCharacterTechniqueMelee);
            this.panelCharacter.Controls.Add(this.linkLabelItemEditing);
            this.panelCharacter.Controls.Add(this.listViewCharacterItems);
            this.panelCharacter.Controls.Add(this.labelCharacterItems);
            this.panelCharacter.Controls.Add(this.textBoxCharacterSpeed);
            this.panelCharacter.Controls.Add(this.labelCharacterSpeed);
            this.panelCharacter.Controls.Add(this.textBoxCharacterSkill);
            this.panelCharacter.Controls.Add(this.labelCharacterSkill);
            this.panelCharacter.Controls.Add(this.textBoxCharacterLuck);
            this.panelCharacter.Controls.Add(this.labelCharacterLuck);
            this.panelCharacter.Controls.Add(this.textBoxCharacterDefense);
            this.panelCharacter.Controls.Add(this.labelCharacterDefense);
            this.panelCharacter.Controls.Add(this.textBoxCharacterAttack);
            this.panelCharacter.Controls.Add(this.labelCharacterAttack);
            this.panelCharacter.Controls.Add(this.textBoxCharacterTechPoints);
            this.panelCharacter.Controls.Add(this.labelCharacterTechPoints);
            this.panelCharacter.Controls.Add(this.textBoxCharacterHitPoints);
            this.panelCharacter.Controls.Add(this.labelCharacterHitPoints);
            this.panelCharacter.Controls.Add(this.comboBoxCharacterTechniques);
            this.panelCharacter.Controls.Add(this.labelCharacterTechPower);
            this.panelCharacter.Controls.Add(this.comboBoxCharacterType);
            this.panelCharacter.Controls.Add(this.labelCharacterType);
            this.panelCharacter.Controls.Add(this.textBoxCharacterName);
            this.panelCharacter.Controls.Add(this.labelCharacterName);
            this.panelCharacter.Controls.Add(this.textBoxCharacterAddress);
            this.panelCharacter.Controls.Add(this.labelCharacterAddress);
            this.panelCharacter.Location = new System.Drawing.Point(10, 55);
            this.panelCharacter.Name = "panelCharacter";
            this.panelCharacter.Size = new System.Drawing.Size(559, 323);
            this.panelCharacter.TabIndex = 6;
            // 
            // comboBoxCharacterTechniqueOrder
            // 
            this.comboBoxCharacterTechniqueOrder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniqueOrder.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.comboBoxCharacterTechniqueOrder.Location = new System.Drawing.Point(475, 175);
            this.comboBoxCharacterTechniqueOrder.Name = "comboBoxCharacterTechniqueOrder";
            this.comboBoxCharacterTechniqueOrder.Size = new System.Drawing.Size(60, 24);
            this.comboBoxCharacterTechniqueOrder.TabIndex = 40;
            this.comboBoxCharacterTechniqueOrder.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniqueOrder_Validating);
            // 
            // labelCharacterTechniqueOrder
            // 
            this.labelCharacterTechniqueOrder.Location = new System.Drawing.Point(414, 173);
            this.labelCharacterTechniqueOrder.Name = "labelCharacterTechniqueOrder";
            this.labelCharacterTechniqueOrder.Size = new System.Drawing.Size(54, 24);
            this.labelCharacterTechniqueOrder.TabIndex = 48;
            this.labelCharacterTechniqueOrder.Text = "Order:";
            this.labelCharacterTechniqueOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterTechniqueTime
            // 
            this.comboBoxCharacterTechniqueTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniqueTime.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.comboBoxCharacterTechniqueTime.Location = new System.Drawing.Point(348, 173);
            this.comboBoxCharacterTechniqueTime.Name = "comboBoxCharacterTechniqueTime";
            this.comboBoxCharacterTechniqueTime.Size = new System.Drawing.Size(60, 24);
            this.comboBoxCharacterTechniqueTime.TabIndex = 39;
            this.comboBoxCharacterTechniqueTime.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniqueTime_Validating);
            // 
            // labelCharacterTechniqueTime
            // 
            this.labelCharacterTechniqueTime.Location = new System.Drawing.Point(300, 173);
            this.labelCharacterTechniqueTime.Name = "labelCharacterTechniqueTime";
            this.labelCharacterTechniqueTime.Size = new System.Drawing.Size(46, 24);
            this.labelCharacterTechniqueTime.TabIndex = 46;
            this.labelCharacterTechniqueTime.Text = "Time:";
            this.labelCharacterTechniqueTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterTechniqueHeal
            // 
            this.comboBoxCharacterTechniqueHeal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniqueHeal.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.comboBoxCharacterTechniqueHeal.Location = new System.Drawing.Point(228, 174);
            this.comboBoxCharacterTechniqueHeal.Name = "comboBoxCharacterTechniqueHeal";
            this.comboBoxCharacterTechniqueHeal.Size = new System.Drawing.Size(60, 24);
            this.comboBoxCharacterTechniqueHeal.TabIndex = 38;
            this.comboBoxCharacterTechniqueHeal.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniqueHeal_Validating);
            // 
            // labelCharacterTechniqueHeal
            // 
            this.labelCharacterTechniqueHeal.Location = new System.Drawing.Point(172, 174);
            this.labelCharacterTechniqueHeal.Name = "labelCharacterTechniqueHeal";
            this.labelCharacterTechniqueHeal.Size = new System.Drawing.Size(49, 24);
            this.labelCharacterTechniqueHeal.TabIndex = 44;
            this.labelCharacterTechniqueHeal.Text = "Heal:";
            this.labelCharacterTechniqueHeal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterTechniqueMelee
            // 
            this.comboBoxCharacterTechniqueMelee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniqueMelee.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.comboBoxCharacterTechniqueMelee.Location = new System.Drawing.Point(115, 175);
            this.comboBoxCharacterTechniqueMelee.Name = "comboBoxCharacterTechniqueMelee";
            this.comboBoxCharacterTechniqueMelee.Size = new System.Drawing.Size(60, 24);
            this.comboBoxCharacterTechniqueMelee.TabIndex = 37;
            this.comboBoxCharacterTechniqueMelee.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniqueMelee_Validating);
            // 
            // labelCharacterTechniqueMelee
            // 
            this.labelCharacterTechniqueMelee.Location = new System.Drawing.Point(35, 175);
            this.labelCharacterTechniqueMelee.Name = "labelCharacterTechniqueMelee";
            this.labelCharacterTechniqueMelee.Size = new System.Drawing.Size(71, 25);
            this.labelCharacterTechniqueMelee.TabIndex = 42;
            this.labelCharacterTechniqueMelee.Text = "Melee:";
            this.labelCharacterTechniqueMelee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabelItemEditing
            // 
            this.linkLabelItemEditing.LinkColor = System.Drawing.Color.Blue;
            this.linkLabelItemEditing.Location = new System.Drawing.Point(400, 299);
            this.linkLabelItemEditing.Name = "linkLabelItemEditing";
            this.linkLabelItemEditing.Size = new System.Drawing.Size(153, 18);
            this.linkLabelItemEditing.TabIndex = 42;
            this.linkLabelItemEditing.TabStop = true;
            this.linkLabelItemEditing.Text = "Item Editing Limitations";
            this.linkLabelItemEditing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabelItemEditing.VisitedLinkColor = System.Drawing.Color.Blue;
            this.linkLabelItemEditing.Click += new System.EventHandler(this.linkLabelItemEditing_Click);
            // 
            // listViewCharacterItems
            // 
            this.listViewCharacterItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCharacterItemHexString,
            this.columnHeaderCharacterItemItem,
            this.columnHeaderCharacterItemIsEquipped,
            this.columnHeaderCharacterItemWhereEquipped});
            this.listViewCharacterItems.ContextMenu = this.contextMenuCharacterMenu;
            this.listViewCharacterItems.FullRowSelect = true;
            this.listViewCharacterItems.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewCharacterItems.HideSelection = false;
            this.listViewCharacterItems.Location = new System.Drawing.Point(115, 203);
            this.listViewCharacterItems.MultiSelect = false;
            this.listViewCharacterItems.Name = "listViewCharacterItems";
            this.listViewCharacterItems.Size = new System.Drawing.Size(438, 92);
            this.listViewCharacterItems.TabIndex = 41;
            this.listViewCharacterItems.UseCompatibleStateImageBehavior = false;
            this.listViewCharacterItems.View = System.Windows.Forms.View.Details;
            this.listViewCharacterItems.DoubleClick += new System.EventHandler(this.listViewCharacterItems_DoubleClick);
            // 
            // columnHeaderCharacterItemHexString
            // 
            this.columnHeaderCharacterItemHexString.Text = "Hex Value";
            this.columnHeaderCharacterItemHexString.Width = 65;
            // 
            // columnHeaderCharacterItemItem
            // 
            this.columnHeaderCharacterItemItem.Text = "Item";
            this.columnHeaderCharacterItemItem.Width = 72;
            // 
            // columnHeaderCharacterItemIsEquipped
            // 
            this.columnHeaderCharacterItemIsEquipped.Text = "Equipped";
            // 
            // columnHeaderCharacterItemWhereEquipped
            // 
            this.columnHeaderCharacterItemWhereEquipped.Text = "Equipped Where";
            this.columnHeaderCharacterItemWhereEquipped.Width = 94;
            // 
            // contextMenuCharacterMenu
            // 
            this.contextMenuCharacterMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEditCharacterItem});
            this.contextMenuCharacterMenu.Popup += new System.EventHandler(this.contextMenuCharacterMenu_Popup);
            // 
            // menuItemEditCharacterItem
            // 
            this.menuItemEditCharacterItem.Index = 0;
            this.menuItemEditCharacterItem.Text = "&Edit Selected Item";
            this.menuItemEditCharacterItem.Click += new System.EventHandler(this.menuItemEditCharacterItem_Click);
            // 
            // labelCharacterItems
            // 
            this.labelCharacterItems.Location = new System.Drawing.Point(19, 203);
            this.labelCharacterItems.Name = "labelCharacterItems";
            this.labelCharacterItems.Size = new System.Drawing.Size(87, 24);
            this.labelCharacterItems.TabIndex = 36;
            this.labelCharacterItems.Text = "Items:";
            this.labelCharacterItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterSpeed
            // 
            this.textBoxCharacterSpeed.Location = new System.Drawing.Point(115, 148);
            this.textBoxCharacterSpeed.MaxLength = 3;
            this.textBoxCharacterSpeed.Name = "textBoxCharacterSpeed";
            this.textBoxCharacterSpeed.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterSpeed.TabIndex = 33;
            this.textBoxCharacterSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterSpeed_Validating);
            // 
            // labelCharacterSpeed
            // 
            this.labelCharacterSpeed.Location = new System.Drawing.Point(19, 148);
            this.labelCharacterSpeed.Name = "labelCharacterSpeed";
            this.labelCharacterSpeed.Size = new System.Drawing.Size(87, 24);
            this.labelCharacterSpeed.TabIndex = 32;
            this.labelCharacterSpeed.Text = "Speed:";
            this.labelCharacterSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterSkill
            // 
            this.textBoxCharacterSkill.Location = new System.Drawing.Point(379, 120);
            this.textBoxCharacterSkill.MaxLength = 3;
            this.textBoxCharacterSkill.Name = "textBoxCharacterSkill";
            this.textBoxCharacterSkill.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterSkill.TabIndex = 31;
            this.textBoxCharacterSkill.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterSkill_Validating);
            // 
            // labelCharacterSkill
            // 
            this.labelCharacterSkill.Location = new System.Drawing.Point(286, 119);
            this.labelCharacterSkill.Name = "labelCharacterSkill";
            this.labelCharacterSkill.Size = new System.Drawing.Size(86, 24);
            this.labelCharacterSkill.TabIndex = 30;
            this.labelCharacterSkill.Text = "Skill:";
            this.labelCharacterSkill.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterLuck
            // 
            this.textBoxCharacterLuck.Location = new System.Drawing.Point(115, 120);
            this.textBoxCharacterLuck.MaxLength = 3;
            this.textBoxCharacterLuck.Name = "textBoxCharacterLuck";
            this.textBoxCharacterLuck.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterLuck.TabIndex = 29;
            this.textBoxCharacterLuck.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterLuck_Validating);
            // 
            // labelCharacterLuck
            // 
            this.labelCharacterLuck.Location = new System.Drawing.Point(19, 120);
            this.labelCharacterLuck.Name = "labelCharacterLuck";
            this.labelCharacterLuck.Size = new System.Drawing.Size(87, 24);
            this.labelCharacterLuck.TabIndex = 28;
            this.labelCharacterLuck.Text = "Luck:";
            this.labelCharacterLuck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterDefense
            // 
            this.textBoxCharacterDefense.Location = new System.Drawing.Point(379, 92);
            this.textBoxCharacterDefense.MaxLength = 3;
            this.textBoxCharacterDefense.Name = "textBoxCharacterDefense";
            this.textBoxCharacterDefense.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterDefense.TabIndex = 27;
            this.textBoxCharacterDefense.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterDefense_Validating);
            // 
            // labelCharacterDefense
            // 
            this.labelCharacterDefense.Location = new System.Drawing.Point(286, 89);
            this.labelCharacterDefense.Name = "labelCharacterDefense";
            this.labelCharacterDefense.Size = new System.Drawing.Size(86, 24);
            this.labelCharacterDefense.TabIndex = 26;
            this.labelCharacterDefense.Text = "Defense:";
            this.labelCharacterDefense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterAttack
            // 
            this.textBoxCharacterAttack.Location = new System.Drawing.Point(115, 92);
            this.textBoxCharacterAttack.MaxLength = 3;
            this.textBoxCharacterAttack.Name = "textBoxCharacterAttack";
            this.textBoxCharacterAttack.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterAttack.TabIndex = 25;
            this.textBoxCharacterAttack.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterAttack_Validating);
            // 
            // labelCharacterAttack
            // 
            this.labelCharacterAttack.Location = new System.Drawing.Point(19, 92);
            this.labelCharacterAttack.Name = "labelCharacterAttack";
            this.labelCharacterAttack.Size = new System.Drawing.Size(87, 25);
            this.labelCharacterAttack.TabIndex = 24;
            this.labelCharacterAttack.Text = "Attack:";
            this.labelCharacterAttack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterTechPoints
            // 
            this.textBoxCharacterTechPoints.Location = new System.Drawing.Point(379, 66);
            this.textBoxCharacterTechPoints.MaxLength = 3;
            this.textBoxCharacterTechPoints.Name = "textBoxCharacterTechPoints";
            this.textBoxCharacterTechPoints.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterTechPoints.TabIndex = 23;
            this.textBoxCharacterTechPoints.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterTechPoints_Validating);
            // 
            // labelCharacterTechPoints
            // 
            this.labelCharacterTechPoints.Location = new System.Drawing.Point(272, 65);
            this.labelCharacterTechPoints.Name = "labelCharacterTechPoints";
            this.labelCharacterTechPoints.Size = new System.Drawing.Size(100, 24);
            this.labelCharacterTechPoints.TabIndex = 22;
            this.labelCharacterTechPoints.Text = "Tech Points:";
            this.labelCharacterTechPoints.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterHitPoints
            // 
            this.textBoxCharacterHitPoints.Location = new System.Drawing.Point(115, 68);
            this.textBoxCharacterHitPoints.MaxLength = 3;
            this.textBoxCharacterHitPoints.Name = "textBoxCharacterHitPoints";
            this.textBoxCharacterHitPoints.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterHitPoints.TabIndex = 21;
            this.textBoxCharacterHitPoints.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterHitPoints_Validating);
            // 
            // labelCharacterHitPoints
            // 
            this.labelCharacterHitPoints.Location = new System.Drawing.Point(19, 65);
            this.labelCharacterHitPoints.Name = "labelCharacterHitPoints";
            this.labelCharacterHitPoints.Size = new System.Drawing.Size(87, 24);
            this.labelCharacterHitPoints.TabIndex = 20;
            this.labelCharacterHitPoints.Text = "Hit Points:";
            this.labelCharacterHitPoints.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterTechniques
            // 
            this.comboBoxCharacterTechniques.BackColor = System.Drawing.SystemColors.Window;
            this.comboBoxCharacterTechniques.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniques.Location = new System.Drawing.Point(379, 37);
            this.comboBoxCharacterTechniques.Name = "comboBoxCharacterTechniques";
            this.comboBoxCharacterTechniques.Size = new System.Drawing.Size(156, 24);
            this.comboBoxCharacterTechniques.TabIndex = 19;
            this.comboBoxCharacterTechniques.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniques_Validating);
            // 
            // labelCharacterTechPower
            // 
            this.labelCharacterTechPower.Location = new System.Drawing.Point(275, 37);
            this.labelCharacterTechPower.Name = "labelCharacterTechPower";
            this.labelCharacterTechPower.Size = new System.Drawing.Size(97, 24);
            this.labelCharacterTechPower.TabIndex = 18;
            this.labelCharacterTechPower.Text = "Tech Power: ";
            this.labelCharacterTechPower.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterType
            // 
            this.comboBoxCharacterType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterType.Location = new System.Drawing.Point(115, 37);
            this.comboBoxCharacterType.Name = "comboBoxCharacterType";
            this.comboBoxCharacterType.Size = new System.Drawing.Size(156, 24);
            this.comboBoxCharacterType.TabIndex = 17;
            this.comboBoxCharacterType.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterType_Validating);
            // 
            // labelCharacterType
            // 
            this.labelCharacterType.Location = new System.Drawing.Point(19, 37);
            this.labelCharacterType.Name = "labelCharacterType";
            this.labelCharacterType.Size = new System.Drawing.Size(87, 24);
            this.labelCharacterType.TabIndex = 16;
            this.labelCharacterType.Text = "Type: ";
            this.labelCharacterType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterName
            // 
            this.textBoxCharacterName.Location = new System.Drawing.Point(379, 7);
            this.textBoxCharacterName.MaxLength = 4;
            this.textBoxCharacterName.Name = "textBoxCharacterName";
            this.textBoxCharacterName.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterName.TabIndex = 15;
            this.textBoxCharacterName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxCharacterName_Validating);
            // 
            // labelCharacterName
            // 
            this.labelCharacterName.Location = new System.Drawing.Point(286, 9);
            this.labelCharacterName.Name = "labelCharacterName";
            this.labelCharacterName.Size = new System.Drawing.Size(86, 24);
            this.labelCharacterName.TabIndex = 14;
            this.labelCharacterName.Text = "Name:";
            this.labelCharacterName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterAddress
            // 
            this.textBoxCharacterAddress.Location = new System.Drawing.Point(115, 9);
            this.textBoxCharacterAddress.Name = "textBoxCharacterAddress";
            this.textBoxCharacterAddress.ReadOnly = true;
            this.textBoxCharacterAddress.Size = new System.Drawing.Size(156, 22);
            this.textBoxCharacterAddress.TabIndex = 13;
            // 
            // labelCharacterAddress
            // 
            this.labelCharacterAddress.Location = new System.Drawing.Point(19, 9);
            this.labelCharacterAddress.Name = "labelCharacterAddress";
            this.labelCharacterAddress.Size = new System.Drawing.Size(87, 24);
            this.labelCharacterAddress.TabIndex = 12;
            this.labelCharacterAddress.Text = "Address:";
            this.labelCharacterAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectCharacter
            // 
            this.comboBoxSelectCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectCharacter.Location = new System.Drawing.Point(154, 18);
            this.comboBoxSelectCharacter.Name = "comboBoxSelectCharacter";
            this.comboBoxSelectCharacter.Size = new System.Drawing.Size(415, 24);
            this.comboBoxSelectCharacter.TabIndex = 5;
            this.comboBoxSelectCharacter.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectCharacter_SelectedIndexChanged);
            // 
            // labelSelectCharacter
            // 
            this.labelSelectCharacter.Location = new System.Drawing.Point(19, 18);
            this.labelSelectCharacter.Name = "labelSelectCharacter";
            this.labelSelectCharacter.Size = new System.Drawing.Size(125, 25);
            this.labelSelectCharacter.TabIndex = 4;
            this.labelSelectCharacter.Text = "Select Character: ";
            this.labelSelectCharacter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageDialogText
            // 
            this.tabPageDialogText.Controls.Add(this.listViewDialogText);
            this.tabPageDialogText.Location = new System.Drawing.Point(4, 55);
            this.tabPageDialogText.Name = "tabPageDialogText";
            this.tabPageDialogText.Size = new System.Drawing.Size(700, 379);
            this.tabPageDialogText.TabIndex = 2;
            this.tabPageDialogText.Text = "Dialog Text";
            this.tabPageDialogText.ToolTipText = "Edit dialog messages";
            // 
            // listViewDialogText
            // 
            this.listViewDialogText.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDialogTextHeaderCurrentValue,
            this.columnDialogTextCategory,
            this.columnDialogTextDescription,
            this.columnDialogTextAddress,
            this.columncolumnDialogTextLength});
            this.listViewDialogText.LabelEdit = true;
            this.listViewDialogText.Location = new System.Drawing.Point(10, 9);
            this.listViewDialogText.MultiSelect = false;
            this.listViewDialogText.Name = "listViewDialogText";
            this.listViewDialogText.Size = new System.Drawing.Size(681, 367);
            this.listViewDialogText.TabIndex = 5;
            this.listViewDialogText.UseCompatibleStateImageBehavior = false;
            this.listViewDialogText.View = System.Windows.Forms.View.Details;
            this.listViewDialogText.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewDialogText_AfterLabelEdit);
            this.listViewDialogText.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewDialogText_ColumnClick);
            this.listViewDialogText.DoubleClick += new System.EventHandler(this.listViewDialogText_DoubleClick);
            this.listViewDialogText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewDialogText_KeyPress);
            // 
            // columnDialogTextHeaderCurrentValue
            // 
            this.columnDialogTextHeaderCurrentValue.Text = "Current Value";
            this.columnDialogTextHeaderCurrentValue.Width = 174;
            // 
            // columnDialogTextCategory
            // 
            this.columnDialogTextCategory.Text = "Category";
            this.columnDialogTextCategory.Width = 155;
            // 
            // columnDialogTextDescription
            // 
            this.columnDialogTextDescription.Text = "Description";
            this.columnDialogTextDescription.Width = 190;
            // 
            // columnDialogTextAddress
            // 
            this.columnDialogTextAddress.Text = "Address";
            this.columnDialogTextAddress.Width = 51;
            // 
            // columncolumnDialogTextLength
            // 
            this.columncolumnDialogTextLength.Text = "Length";
            this.columncolumnDialogTextLength.Width = 54;
            // 
            // tabPageEnemies
            // 
            this.tabPageEnemies.Controls.Add(this.panelEnemy);
            this.tabPageEnemies.Controls.Add(this.comboBoxSelectEnemy);
            this.tabPageEnemies.Controls.Add(this.labelSelectEnemy);
            this.tabPageEnemies.Location = new System.Drawing.Point(4, 55);
            this.tabPageEnemies.Name = "tabPageEnemies";
            this.tabPageEnemies.Size = new System.Drawing.Size(700, 379);
            this.tabPageEnemies.TabIndex = 6;
            this.tabPageEnemies.Text = "Enemies";
            this.tabPageEnemies.ToolTipText = "Edit enemy names and properties";
            // 
            // panelEnemy
            // 
            this.panelEnemy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEnemy.Controls.Add(this.textBoxEnemyEscapePercent);
            this.panelEnemy.Controls.Add(this.labelEnemyEscapePercent);
            this.panelEnemy.Controls.Add(this.textBoxEnemyTechniqueCastPercent);
            this.panelEnemy.Controls.Add(this.labelEnemyTechniqueCastPercent);
            this.panelEnemy.Controls.Add(this.textBoxEnemyTechniqueLevel);
            this.panelEnemy.Controls.Add(this.labelEnemyTechniqueLevel);
            this.panelEnemy.Controls.Add(this.textBoxEnemyName);
            this.panelEnemy.Controls.Add(this.labelEnemyName);
            this.panelEnemy.Controls.Add(this.textBoxEnemyNameOffset);
            this.panelEnemy.Controls.Add(this.labelEnemyNameOffset);
            this.panelEnemy.Controls.Add(this.textBoxEnemyMeseta);
            this.panelEnemy.Controls.Add(this.labelEnemyMeseta);
            this.panelEnemy.Controls.Add(this.textBoxEnemyExperience);
            this.panelEnemy.Controls.Add(this.labelEnemyExperience);
            this.panelEnemy.Controls.Add(this.textBoxEnemySpeed);
            this.panelEnemy.Controls.Add(this.labelEnemySpeed);
            this.panelEnemy.Controls.Add(this.textBoxEnemyDefense);
            this.panelEnemy.Controls.Add(this.labelEnemyDefense);
            this.panelEnemy.Controls.Add(this.textBoxEnemyAttack);
            this.panelEnemy.Controls.Add(this.labelEnemyAttack);
            this.panelEnemy.Controls.Add(this.textBoxEnemyHitPoints);
            this.panelEnemy.Controls.Add(this.labelEnemyHitPoints);
            this.panelEnemy.Controls.Add(this.comboBoxEnemyTechnique);
            this.panelEnemy.Controls.Add(this.labelEnemyTechnique);
            this.panelEnemy.Controls.Add(this.comboBoxEnemyAnimation);
            this.panelEnemy.Controls.Add(this.labelEnemyAnimation);
            this.panelEnemy.Controls.Add(this.comboBoxEnemySpriteGroup);
            this.panelEnemy.Controls.Add(this.labelEnemySpriteGroup);
            this.panelEnemy.Controls.Add(this.textBoxEnemyAddress);
            this.panelEnemy.Controls.Add(this.labelEnemyAddress);
            this.panelEnemy.Location = new System.Drawing.Point(10, 46);
            this.panelEnemy.Name = "panelEnemy";
            this.panelEnemy.Size = new System.Drawing.Size(559, 240);
            this.panelEnemy.TabIndex = 8;
            // 
            // textBoxEnemyEscapePercent
            // 
            this.textBoxEnemyEscapePercent.Location = new System.Drawing.Point(115, 203);
            this.textBoxEnemyEscapePercent.MaxLength = 2;
            this.textBoxEnemyEscapePercent.Name = "textBoxEnemyEscapePercent";
            this.textBoxEnemyEscapePercent.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyEscapePercent.TabIndex = 34;
            this.textBoxEnemyEscapePercent.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyEscapePercent_Validating);
            // 
            // labelEnemyEscapePercent
            // 
            this.labelEnemyEscapePercent.Location = new System.Drawing.Point(19, 203);
            this.labelEnemyEscapePercent.Name = "labelEnemyEscapePercent";
            this.labelEnemyEscapePercent.Size = new System.Drawing.Size(87, 24);
            this.labelEnemyEscapePercent.TabIndex = 46;
            this.labelEnemyEscapePercent.Text = "Escape %:";
            this.labelEnemyEscapePercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyTechniqueCastPercent
            // 
            this.textBoxEnemyTechniqueCastPercent.Location = new System.Drawing.Point(382, 65);
            this.textBoxEnemyTechniqueCastPercent.MaxLength = 2;
            this.textBoxEnemyTechniqueCastPercent.Name = "textBoxEnemyTechniqueCastPercent";
            this.textBoxEnemyTechniqueCastPercent.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyTechniqueCastPercent.TabIndex = 25;
            this.textBoxEnemyTechniqueCastPercent.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyTechniqueCastPercent_Validating);
            // 
            // labelEnemyTechniqueCastPercent
            // 
            this.labelEnemyTechniqueCastPercent.Location = new System.Drawing.Point(277, 65);
            this.labelEnemyTechniqueCastPercent.Name = "labelEnemyTechniqueCastPercent";
            this.labelEnemyTechniqueCastPercent.Size = new System.Drawing.Size(96, 24);
            this.labelEnemyTechniqueCastPercent.TabIndex = 44;
            this.labelEnemyTechniqueCastPercent.Text = "Tech Cast %:";
            this.labelEnemyTechniqueCastPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyTechniqueLevel
            // 
            this.textBoxEnemyTechniqueLevel.Location = new System.Drawing.Point(115, 65);
            this.textBoxEnemyTechniqueLevel.MaxLength = 2;
            this.textBoxEnemyTechniqueLevel.Name = "textBoxEnemyTechniqueLevel";
            this.textBoxEnemyTechniqueLevel.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyTechniqueLevel.TabIndex = 24;
            this.textBoxEnemyTechniqueLevel.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyTechniqueLevel_Validating);
            // 
            // labelEnemyTechniqueLevel
            // 
            this.labelEnemyTechniqueLevel.Location = new System.Drawing.Point(19, 65);
            this.labelEnemyTechniqueLevel.Name = "labelEnemyTechniqueLevel";
            this.labelEnemyTechniqueLevel.Size = new System.Drawing.Size(87, 24);
            this.labelEnemyTechniqueLevel.TabIndex = 42;
            this.labelEnemyTechniqueLevel.Text = "Tech Level:";
            this.labelEnemyTechniqueLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyName
            // 
            this.textBoxEnemyName.Location = new System.Drawing.Point(382, 175);
            this.textBoxEnemyName.MaxLength = 4;
            this.textBoxEnemyName.Name = "textBoxEnemyName";
            this.textBoxEnemyName.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyName.TabIndex = 33;
            this.textBoxEnemyName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyName_Validating);
            // 
            // labelEnemyName
            // 
            this.labelEnemyName.Location = new System.Drawing.Point(286, 175);
            this.labelEnemyName.Name = "labelEnemyName";
            this.labelEnemyName.Size = new System.Drawing.Size(87, 25);
            this.labelEnemyName.TabIndex = 40;
            this.labelEnemyName.Text = "Name:";
            this.labelEnemyName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyNameOffset
            // 
            this.textBoxEnemyNameOffset.Location = new System.Drawing.Point(115, 175);
            this.textBoxEnemyNameOffset.Name = "textBoxEnemyNameOffset";
            this.textBoxEnemyNameOffset.ReadOnly = true;
            this.textBoxEnemyNameOffset.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyNameOffset.TabIndex = 32;
            // 
            // labelEnemyNameOffset
            // 
            this.labelEnemyNameOffset.Location = new System.Drawing.Point(3, 175);
            this.labelEnemyNameOffset.Name = "labelEnemyNameOffset";
            this.labelEnemyNameOffset.Size = new System.Drawing.Size(103, 25);
            this.labelEnemyNameOffset.TabIndex = 38;
            this.labelEnemyNameOffset.Text = "Name Offset:";
            this.labelEnemyNameOffset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyMeseta
            // 
            this.textBoxEnemyMeseta.Location = new System.Drawing.Point(382, 148);
            this.textBoxEnemyMeseta.MaxLength = 4;
            this.textBoxEnemyMeseta.Name = "textBoxEnemyMeseta";
            this.textBoxEnemyMeseta.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyMeseta.TabIndex = 31;
            this.textBoxEnemyMeseta.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyMeseta_Validating);
            // 
            // labelEnemyMeseta
            // 
            this.labelEnemyMeseta.Location = new System.Drawing.Point(286, 148);
            this.labelEnemyMeseta.Name = "labelEnemyMeseta";
            this.labelEnemyMeseta.Size = new System.Drawing.Size(87, 24);
            this.labelEnemyMeseta.TabIndex = 36;
            this.labelEnemyMeseta.Text = "Meseta:";
            this.labelEnemyMeseta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyExperience
            // 
            this.textBoxEnemyExperience.Location = new System.Drawing.Point(115, 148);
            this.textBoxEnemyExperience.MaxLength = 4;
            this.textBoxEnemyExperience.Name = "textBoxEnemyExperience";
            this.textBoxEnemyExperience.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyExperience.TabIndex = 30;
            this.textBoxEnemyExperience.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyExperience_Validating);
            // 
            // labelEnemyExperience
            // 
            this.labelEnemyExperience.Location = new System.Drawing.Point(19, 148);
            this.labelEnemyExperience.Name = "labelEnemyExperience";
            this.labelEnemyExperience.Size = new System.Drawing.Size(87, 24);
            this.labelEnemyExperience.TabIndex = 34;
            this.labelEnemyExperience.Text = "Experience:";
            this.labelEnemyExperience.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemySpeed
            // 
            this.textBoxEnemySpeed.Location = new System.Drawing.Point(382, 120);
            this.textBoxEnemySpeed.MaxLength = 4;
            this.textBoxEnemySpeed.Name = "textBoxEnemySpeed";
            this.textBoxEnemySpeed.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemySpeed.TabIndex = 29;
            this.textBoxEnemySpeed.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemySpeed_Validating);
            // 
            // labelEnemySpeed
            // 
            this.labelEnemySpeed.Location = new System.Drawing.Point(286, 120);
            this.labelEnemySpeed.Name = "labelEnemySpeed";
            this.labelEnemySpeed.Size = new System.Drawing.Size(87, 24);
            this.labelEnemySpeed.TabIndex = 32;
            this.labelEnemySpeed.Text = "Speed:";
            this.labelEnemySpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyDefense
            // 
            this.textBoxEnemyDefense.Location = new System.Drawing.Point(115, 120);
            this.textBoxEnemyDefense.MaxLength = 4;
            this.textBoxEnemyDefense.Name = "textBoxEnemyDefense";
            this.textBoxEnemyDefense.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyDefense.TabIndex = 28;
            this.textBoxEnemyDefense.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyDefense_Validating);
            // 
            // labelEnemyDefense
            // 
            this.labelEnemyDefense.Location = new System.Drawing.Point(19, 120);
            this.labelEnemyDefense.Name = "labelEnemyDefense";
            this.labelEnemyDefense.Size = new System.Drawing.Size(87, 24);
            this.labelEnemyDefense.TabIndex = 30;
            this.labelEnemyDefense.Text = "Defense:";
            this.labelEnemyDefense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyAttack
            // 
            this.textBoxEnemyAttack.Location = new System.Drawing.Point(382, 92);
            this.textBoxEnemyAttack.MaxLength = 4;
            this.textBoxEnemyAttack.Name = "textBoxEnemyAttack";
            this.textBoxEnemyAttack.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyAttack.TabIndex = 27;
            this.textBoxEnemyAttack.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyAttack_Validating);
            // 
            // labelEnemyAttack
            // 
            this.labelEnemyAttack.Location = new System.Drawing.Point(286, 92);
            this.labelEnemyAttack.Name = "labelEnemyAttack";
            this.labelEnemyAttack.Size = new System.Drawing.Size(87, 25);
            this.labelEnemyAttack.TabIndex = 28;
            this.labelEnemyAttack.Text = "Attack:";
            this.labelEnemyAttack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyHitPoints
            // 
            this.textBoxEnemyHitPoints.Location = new System.Drawing.Point(115, 92);
            this.textBoxEnemyHitPoints.MaxLength = 4;
            this.textBoxEnemyHitPoints.Name = "textBoxEnemyHitPoints";
            this.textBoxEnemyHitPoints.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyHitPoints.TabIndex = 26;
            this.textBoxEnemyHitPoints.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxEnemyHitPoints_Validating);
            // 
            // labelEnemyHitPoints
            // 
            this.labelEnemyHitPoints.Location = new System.Drawing.Point(19, 92);
            this.labelEnemyHitPoints.Name = "labelEnemyHitPoints";
            this.labelEnemyHitPoints.Size = new System.Drawing.Size(87, 25);
            this.labelEnemyHitPoints.TabIndex = 26;
            this.labelEnemyHitPoints.Text = "Hit Points:";
            this.labelEnemyHitPoints.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxEnemyTechnique
            // 
            this.comboBoxEnemyTechnique.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnemyTechnique.Location = new System.Drawing.Point(382, 37);
            this.comboBoxEnemyTechnique.Name = "comboBoxEnemyTechnique";
            this.comboBoxEnemyTechnique.Size = new System.Drawing.Size(156, 24);
            this.comboBoxEnemyTechnique.TabIndex = 23;
            this.comboBoxEnemyTechnique.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxEnemyTechnique_Validating);
            // 
            // labelEnemyTechnique
            // 
            this.labelEnemyTechnique.Location = new System.Drawing.Point(286, 37);
            this.labelEnemyTechnique.Name = "labelEnemyTechnique";
            this.labelEnemyTechnique.Size = new System.Drawing.Size(87, 24);
            this.labelEnemyTechnique.TabIndex = 24;
            this.labelEnemyTechnique.Text = "Technique: ";
            this.labelEnemyTechnique.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxEnemyAnimation
            // 
            this.comboBoxEnemyAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnemyAnimation.Location = new System.Drawing.Point(115, 37);
            this.comboBoxEnemyAnimation.Name = "comboBoxEnemyAnimation";
            this.comboBoxEnemyAnimation.Size = new System.Drawing.Size(156, 24);
            this.comboBoxEnemyAnimation.TabIndex = 22;
            this.comboBoxEnemyAnimation.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxEnemyAnimation_Validating);
            // 
            // labelEnemyAnimation
            // 
            this.labelEnemyAnimation.Location = new System.Drawing.Point(19, 37);
            this.labelEnemyAnimation.Name = "labelEnemyAnimation";
            this.labelEnemyAnimation.Size = new System.Drawing.Size(87, 24);
            this.labelEnemyAnimation.TabIndex = 22;
            this.labelEnemyAnimation.Text = "Animation: ";
            this.labelEnemyAnimation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxEnemySpriteGroup
            // 
            this.comboBoxEnemySpriteGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnemySpriteGroup.Location = new System.Drawing.Point(382, 9);
            this.comboBoxEnemySpriteGroup.Name = "comboBoxEnemySpriteGroup";
            this.comboBoxEnemySpriteGroup.Size = new System.Drawing.Size(156, 24);
            this.comboBoxEnemySpriteGroup.TabIndex = 21;
            this.comboBoxEnemySpriteGroup.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxEnemySpriteGroup_Validating);
            // 
            // labelEnemySpriteGroup
            // 
            this.labelEnemySpriteGroup.Location = new System.Drawing.Point(286, 9);
            this.labelEnemySpriteGroup.Name = "labelEnemySpriteGroup";
            this.labelEnemySpriteGroup.Size = new System.Drawing.Size(87, 24);
            this.labelEnemySpriteGroup.TabIndex = 20;
            this.labelEnemySpriteGroup.Text = "Sprite Group: ";
            this.labelEnemySpriteGroup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyAddress
            // 
            this.textBoxEnemyAddress.Location = new System.Drawing.Point(115, 9);
            this.textBoxEnemyAddress.Name = "textBoxEnemyAddress";
            this.textBoxEnemyAddress.ReadOnly = true;
            this.textBoxEnemyAddress.Size = new System.Drawing.Size(156, 22);
            this.textBoxEnemyAddress.TabIndex = 13;
            // 
            // labelEnemyAddress
            // 
            this.labelEnemyAddress.Location = new System.Drawing.Point(19, 9);
            this.labelEnemyAddress.Name = "labelEnemyAddress";
            this.labelEnemyAddress.Size = new System.Drawing.Size(87, 24);
            this.labelEnemyAddress.TabIndex = 12;
            this.labelEnemyAddress.Text = "Address:";
            this.labelEnemyAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectEnemy
            // 
            this.comboBoxSelectEnemy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectEnemy.Location = new System.Drawing.Point(125, 18);
            this.comboBoxSelectEnemy.Name = "comboBoxSelectEnemy";
            this.comboBoxSelectEnemy.Size = new System.Drawing.Size(444, 24);
            this.comboBoxSelectEnemy.TabIndex = 7;
            this.comboBoxSelectEnemy.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectEnemy_SelectedIndexChanged);
            // 
            // labelSelectEnemy
            // 
            this.labelSelectEnemy.Location = new System.Drawing.Point(3, 18);
            this.labelSelectEnemy.Name = "labelSelectEnemy";
            this.labelSelectEnemy.Size = new System.Drawing.Size(112, 25);
            this.labelSelectEnemy.TabIndex = 6;
            this.labelSelectEnemy.Text = "Select Enemy: ";
            this.labelSelectEnemy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageEnemyGroups
            // 
            this.tabPageEnemyGroups.Controls.Add(this.panelEnemyGroups);
            this.tabPageEnemyGroups.Location = new System.Drawing.Point(4, 55);
            this.tabPageEnemyGroups.Name = "tabPageEnemyGroups";
            this.tabPageEnemyGroups.Size = new System.Drawing.Size(700, 379);
            this.tabPageEnemyGroups.TabIndex = 15;
            this.tabPageEnemyGroups.Text = "Enemy Groups";
            this.tabPageEnemyGroups.UseVisualStyleBackColor = true;
            // 
            // panelEnemyGroups
            // 
            this.panelEnemyGroups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEnemyGroups.Controls.Add(this.comboBoxBackRowEnemyCount);
            this.panelEnemyGroups.Controls.Add(this.labelBackRowEnemyCount);
            this.panelEnemyGroups.Controls.Add(this.comboBoxBackRowEnemy);
            this.panelEnemyGroups.Controls.Add(this.labelBackRowEnemy);
            this.panelEnemyGroups.Controls.Add(this.comboBoxFrontRowEnemyCount);
            this.panelEnemyGroups.Controls.Add(this.labelFrontRowEnemyCount);
            this.panelEnemyGroups.Controls.Add(this.comboBoxFrontRowEnemy);
            this.panelEnemyGroups.Controls.Add(this.labelFrontRowEnemy);
            this.panelEnemyGroups.Controls.Add(this.labelEnemyGroupBetaFeature);
            this.panelEnemyGroups.Controls.Add(this.textBoxGroupAddress);
            this.panelEnemyGroups.Controls.Add(this.labelGroupAddress);
            this.panelEnemyGroups.Controls.Add(this.listBoxSelectEnemyGroup);
            this.panelEnemyGroups.Controls.Add(this.labelSelectEnemyGroup);
            this.panelEnemyGroups.Location = new System.Drawing.Point(3, 3);
            this.panelEnemyGroups.Name = "panelEnemyGroups";
            this.panelEnemyGroups.Size = new System.Drawing.Size(489, 290);
            this.panelEnemyGroups.TabIndex = 10;
            // 
            // comboBoxBackRowEnemyCount
            // 
            this.comboBoxBackRowEnemyCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBackRowEnemyCount.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.comboBoxBackRowEnemyCount.Location = new System.Drawing.Point(313, 161);
            this.comboBoxBackRowEnemyCount.Name = "comboBoxBackRowEnemyCount";
            this.comboBoxBackRowEnemyCount.Size = new System.Drawing.Size(156, 24);
            this.comboBoxBackRowEnemyCount.TabIndex = 29;
            this.comboBoxBackRowEnemyCount.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxBackRowEnemyCount_Validating);
            // 
            // labelBackRowEnemyCount
            // 
            this.labelBackRowEnemyCount.Location = new System.Drawing.Point(130, 161);
            this.labelBackRowEnemyCount.Name = "labelBackRowEnemyCount";
            this.labelBackRowEnemyCount.Size = new System.Drawing.Size(177, 24);
            this.labelBackRowEnemyCount.TabIndex = 30;
            this.labelBackRowEnemyCount.Text = "Back row enemy count:";
            this.labelBackRowEnemyCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxBackRowEnemy
            // 
            this.comboBoxBackRowEnemy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBackRowEnemy.Location = new System.Drawing.Point(313, 131);
            this.comboBoxBackRowEnemy.Name = "comboBoxBackRowEnemy";
            this.comboBoxBackRowEnemy.Size = new System.Drawing.Size(156, 24);
            this.comboBoxBackRowEnemy.TabIndex = 27;
            this.comboBoxBackRowEnemy.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxBackRowEnemy_Validating);
            // 
            // labelBackRowEnemy
            // 
            this.labelBackRowEnemy.Location = new System.Drawing.Point(161, 131);
            this.labelBackRowEnemy.Name = "labelBackRowEnemy";
            this.labelBackRowEnemy.Size = new System.Drawing.Size(146, 24);
            this.labelBackRowEnemy.TabIndex = 28;
            this.labelBackRowEnemy.Text = "Back row enemy:";
            this.labelBackRowEnemy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxFrontRowEnemyCount
            // 
            this.comboBoxFrontRowEnemyCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFrontRowEnemyCount.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBoxFrontRowEnemyCount.Location = new System.Drawing.Point(313, 101);
            this.comboBoxFrontRowEnemyCount.Name = "comboBoxFrontRowEnemyCount";
            this.comboBoxFrontRowEnemyCount.Size = new System.Drawing.Size(156, 24);
            this.comboBoxFrontRowEnemyCount.TabIndex = 25;
            this.comboBoxFrontRowEnemyCount.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxFrontRowEnemyCount_Validating);
            // 
            // labelFrontRowEnemyCount
            // 
            this.labelFrontRowEnemyCount.Location = new System.Drawing.Point(130, 101);
            this.labelFrontRowEnemyCount.Name = "labelFrontRowEnemyCount";
            this.labelFrontRowEnemyCount.Size = new System.Drawing.Size(177, 24);
            this.labelFrontRowEnemyCount.TabIndex = 26;
            this.labelFrontRowEnemyCount.Text = "Front row enemy count:";
            this.labelFrontRowEnemyCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxFrontRowEnemy
            // 
            this.comboBoxFrontRowEnemy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFrontRowEnemy.Location = new System.Drawing.Point(313, 71);
            this.comboBoxFrontRowEnemy.Name = "comboBoxFrontRowEnemy";
            this.comboBoxFrontRowEnemy.Size = new System.Drawing.Size(156, 24);
            this.comboBoxFrontRowEnemy.TabIndex = 23;
            this.comboBoxFrontRowEnemy.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxFrontRowEnemy_Validating);
            // 
            // labelFrontRowEnemy
            // 
            this.labelFrontRowEnemy.Location = new System.Drawing.Point(161, 71);
            this.labelFrontRowEnemy.Name = "labelFrontRowEnemy";
            this.labelFrontRowEnemy.Size = new System.Drawing.Size(146, 24);
            this.labelFrontRowEnemy.TabIndex = 24;
            this.labelFrontRowEnemy.Text = "Front row enemy:";
            this.labelFrontRowEnemy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelEnemyGroupBetaFeature
            // 
            this.labelEnemyGroupBetaFeature.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelEnemyGroupBetaFeature.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelEnemyGroupBetaFeature.Location = new System.Drawing.Point(15, 256);
            this.labelEnemyGroupBetaFeature.Name = "labelEnemyGroupBetaFeature";
            this.labelEnemyGroupBetaFeature.Size = new System.Drawing.Size(454, 24);
            this.labelEnemyGroupBetaFeature.TabIndex = 16;
            this.labelEnemyGroupBetaFeature.Text = "Beta feature - see help file for usage and limitations";
            this.labelEnemyGroupBetaFeature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxGroupAddress
            // 
            this.textBoxGroupAddress.Location = new System.Drawing.Point(313, 43);
            this.textBoxGroupAddress.Name = "textBoxGroupAddress";
            this.textBoxGroupAddress.ReadOnly = true;
            this.textBoxGroupAddress.Size = new System.Drawing.Size(156, 22);
            this.textBoxGroupAddress.TabIndex = 15;
            // 
            // labelGroupAddress
            // 
            this.labelGroupAddress.Location = new System.Drawing.Point(169, 41);
            this.labelGroupAddress.Name = "labelGroupAddress";
            this.labelGroupAddress.Size = new System.Drawing.Size(138, 24);
            this.labelGroupAddress.TabIndex = 14;
            this.labelGroupAddress.Text = "Group Address:";
            this.labelGroupAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listBoxSelectEnemyGroup
            // 
            this.listBoxSelectEnemyGroup.FormattingEnabled = true;
            this.listBoxSelectEnemyGroup.ItemHeight = 16;
            this.listBoxSelectEnemyGroup.Location = new System.Drawing.Point(6, 41);
            this.listBoxSelectEnemyGroup.Name = "listBoxSelectEnemyGroup";
            this.listBoxSelectEnemyGroup.Size = new System.Drawing.Size(112, 212);
            this.listBoxSelectEnemyGroup.TabIndex = 12;
            this.listBoxSelectEnemyGroup.SelectedIndexChanged += new System.EventHandler(this.listBoxSelectEnemyGroup_SelectedIndexChanged);
            // 
            // labelSelectEnemyGroup
            // 
            this.labelSelectEnemyGroup.Location = new System.Drawing.Point(3, 13);
            this.labelSelectEnemyGroup.Name = "labelSelectEnemyGroup";
            this.labelSelectEnemyGroup.Size = new System.Drawing.Size(108, 25);
            this.labelSelectEnemyGroup.TabIndex = 11;
            this.labelSelectEnemyGroup.Text = "Group ID";
            this.labelSelectEnemyGroup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPageGraphics
            // 
            this.tabPageGraphics.Controls.Add(this.textBoxTilesetHeight);
            this.tabPageGraphics.Controls.Add(this.labelTilesetHeight);
            this.tabPageGraphics.Controls.Add(this.textBoxTilesetWidth);
            this.tabPageGraphics.Controls.Add(this.labelTilesetWidth);
            this.tabPageGraphics.Controls.Add(this.textBoxTilesetRows);
            this.tabPageGraphics.Controls.Add(this.labelTilesetRows);
            this.tabPageGraphics.Controls.Add(this.textBoxTilesetColumns);
            this.tabPageGraphics.Controls.Add(this.labelTilesetColumns);
            this.tabPageGraphics.Controls.Add(this.textBoxTilesetEndAddress);
            this.tabPageGraphics.Controls.Add(this.labelTilesetEndAddress);
            this.tabPageGraphics.Controls.Add(this.textBoxTilesetStartAddress);
            this.tabPageGraphics.Controls.Add(this.labelTilesetStartAddress);
            this.tabPageGraphics.Controls.Add(this.buttonLaunchTileEditor);
            this.tabPageGraphics.Controls.Add(this.labelSelectTileset);
            this.tabPageGraphics.Controls.Add(this.listBoxTileSets);
            this.tabPageGraphics.Location = new System.Drawing.Point(4, 55);
            this.tabPageGraphics.Name = "tabPageGraphics";
            this.tabPageGraphics.Size = new System.Drawing.Size(700, 379);
            this.tabPageGraphics.TabIndex = 8;
            this.tabPageGraphics.Text = "Graphics";
            this.tabPageGraphics.ToolTipText = "Edit graphics";
            // 
            // tabPageItems
            // 
            this.tabPageItems.Controls.Add(this.panelItem);
            this.tabPageItems.Controls.Add(this.comboBoxSelectItem);
            this.tabPageItems.Controls.Add(this.labelSelectItem);
            this.tabPageItems.Location = new System.Drawing.Point(4, 55);
            this.tabPageItems.Name = "tabPageItems";
            this.tabPageItems.Size = new System.Drawing.Size(700, 379);
            this.tabPageItems.TabIndex = 3;
            this.tabPageItems.Text = "Items";
            this.tabPageItems.ToolTipText = "Edit item names and properties";
            // 
            // panelItem
            // 
            this.panelItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelItem.Controls.Add(this.comboBoxItemEquipWhere);
            this.panelItem.Controls.Add(this.labelEquipWhere);
            this.panelItem.Controls.Add(this.checkBoxItemFlag3);
            this.panelItem.Controls.Add(this.checkBoxItemFlag2);
            this.panelItem.Controls.Add(this.checkBoxItemFlag1);
            this.panelItem.Controls.Add(this.checkBoxItemFlag0);
            this.panelItem.Controls.Add(this.labeltemFlags);
            this.panelItem.Controls.Add(this.checkBoxEquipLayaGwyn);
            this.panelItem.Controls.Add(this.checkBoxEquipLenaSari);
            this.panelItem.Controls.Add(this.checkBoxEquipLyleRyan);
            this.panelItem.Controls.Add(this.checkBoxEquipTheaKara);
            this.panelItem.Controls.Add(this.checkBoxEquipWren);
            this.panelItem.Controls.Add(this.checkBoxEquipMieu);
            this.panelItem.Controls.Add(this.checkBoxEquipAynSeanCrysAdanAron);
            this.panelItem.Controls.Add(this.checkBoxEquipRhysNial);
            this.panelItem.Controls.Add(this.textBoxItemName);
            this.panelItem.Controls.Add(this.labelItemName);
            this.panelItem.Controls.Add(this.textBoxItemNameLength);
            this.panelItem.Controls.Add(this.labelItemNameLength);
            this.panelItem.Controls.Add(this.textBoxItemNameAddress);
            this.panelItem.Controls.Add(this.labelItemNameAddress);
            this.panelItem.Controls.Add(this.labelItemEquipBy);
            this.panelItem.Controls.Add(this.textBoxItemSpeed);
            this.panelItem.Controls.Add(this.labelItemSpeed);
            this.panelItem.Controls.Add(this.textBoxItemDefense);
            this.panelItem.Controls.Add(this.labelItemDefense);
            this.panelItem.Controls.Add(this.textBoxItemAttack);
            this.panelItem.Controls.Add(this.labelItemAttack);
            this.panelItem.Controls.Add(this.comboBoxItemAnimation);
            this.panelItem.Controls.Add(this.labelItemAnimation);
            this.panelItem.Controls.Add(this.textBoxItemAddress);
            this.panelItem.Controls.Add(this.labelItemAddress);
            this.panelItem.Controls.Add(this.textBoxItemEffectiveness);
            this.panelItem.Controls.Add(this.comboBoxItemTechnique);
            this.panelItem.Controls.Add(this.textBoxItemCost);
            this.panelItem.Controls.Add(this.labelItemEffectiveness);
            this.panelItem.Controls.Add(this.labelItemTechnique);
            this.panelItem.Controls.Add(this.labelItemCost);
            this.panelItem.Location = new System.Drawing.Point(10, 55);
            this.panelItem.Name = "panelItem";
            this.panelItem.Size = new System.Drawing.Size(681, 321);
            this.panelItem.TabIndex = 3;
            // 
            // comboBoxItemEquipWhere
            // 
            this.comboBoxItemEquipWhere.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemEquipWhere.Location = new System.Drawing.Point(306, 266);
            this.comboBoxItemEquipWhere.Name = "comboBoxItemEquipWhere";
            this.comboBoxItemEquipWhere.Size = new System.Drawing.Size(155, 24);
            this.comboBoxItemEquipWhere.TabIndex = 21;
            this.comboBoxItemEquipWhere.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxItemEquipWhere_Validating);
            // 
            // labelEquipWhere
            // 
            this.labelEquipWhere.Location = new System.Drawing.Point(303, 242);
            this.labelEquipWhere.Name = "labelEquipWhere";
            this.labelEquipWhere.Size = new System.Drawing.Size(100, 24);
            this.labelEquipWhere.TabIndex = 34;
            this.labelEquipWhere.Text = "Equip Where:";
            this.labelEquipWhere.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxItemFlag3
            // 
            this.checkBoxItemFlag3.AutoSize = true;
            this.checkBoxItemFlag3.Location = new System.Drawing.Point(518, 115);
            this.checkBoxItemFlag3.Name = "checkBoxItemFlag3";
            this.checkBoxItemFlag3.Size = new System.Drawing.Size(156, 21);
            this.checkBoxItemFlag3.TabIndex = 25;
            this.checkBoxItemFlag3.Text = "[3] - Usable in fights";
            this.checkBoxItemFlag3.UseVisualStyleBackColor = true;
            this.checkBoxItemFlag3.Validating += new System.ComponentModel.CancelEventHandler(this.saveItemFlags);
            // 
            // checkBoxItemFlag2
            // 
            this.checkBoxItemFlag2.AutoSize = true;
            this.checkBoxItemFlag2.Location = new System.Drawing.Point(518, 91);
            this.checkBoxItemFlag2.Name = "checkBoxItemFlag2";
            this.checkBoxItemFlag2.Size = new System.Drawing.Size(125, 21);
            this.checkBoxItemFlag2.TabIndex = 24;
            this.checkBoxItemFlag2.Text = "[2] - Droppable";
            this.checkBoxItemFlag2.UseVisualStyleBackColor = true;
            this.checkBoxItemFlag2.Validating += new System.ComponentModel.CancelEventHandler(this.saveItemFlags);
            // 
            // checkBoxItemFlag1
            // 
            this.checkBoxItemFlag1.AutoSize = true;
            this.checkBoxItemFlag1.Location = new System.Drawing.Point(518, 64);
            this.checkBoxItemFlag1.Name = "checkBoxItemFlag1";
            this.checkBoxItemFlag1.Size = new System.Drawing.Size(125, 21);
            this.checkBoxItemFlag1.TabIndex = 23;
            this.checkBoxItemFlag1.Text = "[1] - Unknown?";
            this.checkBoxItemFlag1.UseVisualStyleBackColor = true;
            this.checkBoxItemFlag1.Validating += new System.ComponentModel.CancelEventHandler(this.saveItemFlags);
            // 
            // checkBoxItemFlag0
            // 
            this.checkBoxItemFlag0.AutoSize = true;
            this.checkBoxItemFlag0.Location = new System.Drawing.Point(518, 37);
            this.checkBoxItemFlag0.Name = "checkBoxItemFlag0";
            this.checkBoxItemFlag0.Size = new System.Drawing.Size(115, 21);
            this.checkBoxItemFlag0.TabIndex = 22;
            this.checkBoxItemFlag0.Text = "[0] - Shot flag";
            this.checkBoxItemFlag0.UseVisualStyleBackColor = true;
            this.checkBoxItemFlag0.Validating += new System.ComponentModel.CancelEventHandler(this.saveItemFlags);
            // 
            // labeltemFlags
            // 
            this.labeltemFlags.Location = new System.Drawing.Point(515, 10);
            this.labeltemFlags.Name = "labeltemFlags";
            this.labeltemFlags.Size = new System.Drawing.Size(71, 24);
            this.labeltemFlags.TabIndex = 33;
            this.labeltemFlags.Text = "Flags:";
            this.labeltemFlags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxEquipLayaGwyn
            // 
            this.checkBoxEquipLayaGwyn.AutoSize = true;
            this.checkBoxEquipLayaGwyn.Location = new System.Drawing.Point(306, 218);
            this.checkBoxEquipLayaGwyn.Name = "checkBoxEquipLayaGwyn";
            this.checkBoxEquipLayaGwyn.Size = new System.Drawing.Size(104, 21);
            this.checkBoxEquipLayaGwyn.TabIndex = 20;
            this.checkBoxEquipLayaGwyn.Text = "Laya, Gwyn";
            this.checkBoxEquipLayaGwyn.UseVisualStyleBackColor = true;
            this.checkBoxEquipLayaGwyn.Validating += new System.ComponentModel.CancelEventHandler(this.saveEquipBy);
            // 
            // checkBoxEquipLenaSari
            // 
            this.checkBoxEquipLenaSari.AutoSize = true;
            this.checkBoxEquipLenaSari.Location = new System.Drawing.Point(306, 194);
            this.checkBoxEquipLenaSari.Name = "checkBoxEquipLenaSari";
            this.checkBoxEquipLenaSari.Size = new System.Drawing.Size(95, 21);
            this.checkBoxEquipLenaSari.TabIndex = 19;
            this.checkBoxEquipLenaSari.Text = "Lena, Sari";
            this.checkBoxEquipLenaSari.UseVisualStyleBackColor = true;
            this.checkBoxEquipLenaSari.Validating += new System.ComponentModel.CancelEventHandler(this.saveEquipBy);
            // 
            // checkBoxEquipLyleRyan
            // 
            this.checkBoxEquipLyleRyan.AutoSize = true;
            this.checkBoxEquipLyleRyan.Location = new System.Drawing.Point(306, 167);
            this.checkBoxEquipLyleRyan.Name = "checkBoxEquipLyleRyan";
            this.checkBoxEquipLyleRyan.Size = new System.Drawing.Size(97, 21);
            this.checkBoxEquipLyleRyan.TabIndex = 18;
            this.checkBoxEquipLyleRyan.Text = "Lyle, Ryan";
            this.checkBoxEquipLyleRyan.UseVisualStyleBackColor = true;
            this.checkBoxEquipLyleRyan.Validating += new System.ComponentModel.CancelEventHandler(this.saveEquipBy);
            // 
            // checkBoxEquipTheaKara
            // 
            this.checkBoxEquipTheaKara.AutoSize = true;
            this.checkBoxEquipTheaKara.Location = new System.Drawing.Point(306, 140);
            this.checkBoxEquipTheaKara.Name = "checkBoxEquipTheaKara";
            this.checkBoxEquipTheaKara.Size = new System.Drawing.Size(101, 21);
            this.checkBoxEquipTheaKara.TabIndex = 17;
            this.checkBoxEquipTheaKara.Text = "Thea, Kara";
            this.checkBoxEquipTheaKara.UseVisualStyleBackColor = true;
            this.checkBoxEquipTheaKara.Validating += new System.ComponentModel.CancelEventHandler(this.saveEquipBy);
            // 
            // checkBoxEquipWren
            // 
            this.checkBoxEquipWren.AutoSize = true;
            this.checkBoxEquipWren.Location = new System.Drawing.Point(306, 115);
            this.checkBoxEquipWren.Name = "checkBoxEquipWren";
            this.checkBoxEquipWren.Size = new System.Drawing.Size(64, 21);
            this.checkBoxEquipWren.TabIndex = 16;
            this.checkBoxEquipWren.Text = "Wren";
            this.checkBoxEquipWren.UseVisualStyleBackColor = true;
            this.checkBoxEquipWren.Validating += new System.ComponentModel.CancelEventHandler(this.saveEquipBy);
            // 
            // checkBoxEquipMieu
            // 
            this.checkBoxEquipMieu.AutoSize = true;
            this.checkBoxEquipMieu.Location = new System.Drawing.Point(306, 91);
            this.checkBoxEquipMieu.Name = "checkBoxEquipMieu";
            this.checkBoxEquipMieu.Size = new System.Drawing.Size(60, 21);
            this.checkBoxEquipMieu.TabIndex = 15;
            this.checkBoxEquipMieu.Text = "Mieu";
            this.checkBoxEquipMieu.UseVisualStyleBackColor = true;
            this.checkBoxEquipMieu.Validating += new System.ComponentModel.CancelEventHandler(this.saveEquipBy);
            // 
            // checkBoxEquipAynSeanCrysAdanAron
            // 
            this.checkBoxEquipAynSeanCrysAdanAron.AutoSize = true;
            this.checkBoxEquipAynSeanCrysAdanAron.Location = new System.Drawing.Point(306, 64);
            this.checkBoxEquipAynSeanCrysAdanAron.Name = "checkBoxEquipAynSeanCrysAdanAron";
            this.checkBoxEquipAynSeanCrysAdanAron.Size = new System.Drawing.Size(210, 21);
            this.checkBoxEquipAynSeanCrysAdanAron.TabIndex = 14;
            this.checkBoxEquipAynSeanCrysAdanAron.Text = "Ayn, Sean, Crys, Adan, Aron";
            this.checkBoxEquipAynSeanCrysAdanAron.UseVisualStyleBackColor = true;
            this.checkBoxEquipAynSeanCrysAdanAron.Validating += new System.ComponentModel.CancelEventHandler(this.saveEquipBy);
            // 
            // checkBoxEquipRhysNial
            // 
            this.checkBoxEquipRhysNial.AutoSize = true;
            this.checkBoxEquipRhysNial.Location = new System.Drawing.Point(306, 37);
            this.checkBoxEquipRhysNial.Name = "checkBoxEquipRhysNial";
            this.checkBoxEquipRhysNial.Size = new System.Drawing.Size(94, 21);
            this.checkBoxEquipRhysNial.TabIndex = 13;
            this.checkBoxEquipRhysNial.Text = "Rhys, Nial";
            this.checkBoxEquipRhysNial.UseVisualStyleBackColor = true;
            this.checkBoxEquipRhysNial.Validating += new System.ComponentModel.CancelEventHandler(this.saveEquipBy);
            // 
            // textBoxItemName
            // 
            this.textBoxItemName.Location = new System.Drawing.Point(132, 98);
            this.textBoxItemName.MaxLength = 5;
            this.textBoxItemName.Name = "textBoxItemName";
            this.textBoxItemName.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemName.TabIndex = 5;
            this.textBoxItemName.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxItemName_Validating);
            // 
            // labelItemName
            // 
            this.labelItemName.Location = new System.Drawing.Point(27, 98);
            this.labelItemName.Name = "labelItemName";
            this.labelItemName.Size = new System.Drawing.Size(96, 24);
            this.labelItemName.TabIndex = 32;
            this.labelItemName.Text = "Name: ";
            this.labelItemName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxItemNameLength
            // 
            this.textBoxItemNameLength.Location = new System.Drawing.Point(132, 68);
            this.textBoxItemNameLength.Name = "textBoxItemNameLength";
            this.textBoxItemNameLength.ReadOnly = true;
            this.textBoxItemNameLength.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemNameLength.TabIndex = 4;
            // 
            // labelItemNameLength
            // 
            this.labelItemNameLength.Location = new System.Drawing.Point(7, 68);
            this.labelItemNameLength.Name = "labelItemNameLength";
            this.labelItemNameLength.Size = new System.Drawing.Size(116, 24);
            this.labelItemNameLength.TabIndex = 31;
            this.labelItemNameLength.Text = "Name Length:";
            this.labelItemNameLength.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxItemNameAddress
            // 
            this.textBoxItemNameAddress.Location = new System.Drawing.Point(132, 40);
            this.textBoxItemNameAddress.Name = "textBoxItemNameAddress";
            this.textBoxItemNameAddress.ReadOnly = true;
            this.textBoxItemNameAddress.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemNameAddress.TabIndex = 3;
            // 
            // labelItemNameAddress
            // 
            this.labelItemNameAddress.Location = new System.Drawing.Point(4, 40);
            this.labelItemNameAddress.Name = "labelItemNameAddress";
            this.labelItemNameAddress.Size = new System.Drawing.Size(119, 24);
            this.labelItemNameAddress.TabIndex = 29;
            this.labelItemNameAddress.Text = "Name Address:";
            this.labelItemNameAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelItemEquipBy
            // 
            this.labelItemEquipBy.Location = new System.Drawing.Point(303, 10);
            this.labelItemEquipBy.Name = "labelItemEquipBy";
            this.labelItemEquipBy.Size = new System.Drawing.Size(80, 24);
            this.labelItemEquipBy.TabIndex = 27;
            this.labelItemEquipBy.Text = "Equip By: ";
            this.labelItemEquipBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxItemSpeed
            // 
            this.textBoxItemSpeed.Location = new System.Drawing.Point(132, 292);
            this.textBoxItemSpeed.MaxLength = 3;
            this.textBoxItemSpeed.Name = "textBoxItemSpeed";
            this.textBoxItemSpeed.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemSpeed.TabIndex = 12;
            this.textBoxItemSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxItemSpeed_Validating);
            // 
            // labelItemSpeed
            // 
            this.labelItemSpeed.Location = new System.Drawing.Point(27, 292);
            this.labelItemSpeed.Name = "labelItemSpeed";
            this.labelItemSpeed.Size = new System.Drawing.Size(96, 25);
            this.labelItemSpeed.TabIndex = 26;
            this.labelItemSpeed.Text = "Speed: ";
            this.labelItemSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxItemDefense
            // 
            this.textBoxItemDefense.Location = new System.Drawing.Point(132, 265);
            this.textBoxItemDefense.MaxLength = 3;
            this.textBoxItemDefense.Name = "textBoxItemDefense";
            this.textBoxItemDefense.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemDefense.TabIndex = 11;
            this.textBoxItemDefense.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxItemDefense_Validating);
            // 
            // labelItemDefense
            // 
            this.labelItemDefense.Location = new System.Drawing.Point(27, 265);
            this.labelItemDefense.Name = "labelItemDefense";
            this.labelItemDefense.Size = new System.Drawing.Size(96, 24);
            this.labelItemDefense.TabIndex = 25;
            this.labelItemDefense.Text = "Defense: ";
            this.labelItemDefense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxItemAttack
            // 
            this.textBoxItemAttack.Location = new System.Drawing.Point(132, 237);
            this.textBoxItemAttack.MaxLength = 3;
            this.textBoxItemAttack.Name = "textBoxItemAttack";
            this.textBoxItemAttack.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemAttack.TabIndex = 10;
            this.textBoxItemAttack.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxItemAttack_Validating);
            // 
            // labelItemAttack
            // 
            this.labelItemAttack.Location = new System.Drawing.Point(27, 237);
            this.labelItemAttack.Name = "labelItemAttack";
            this.labelItemAttack.Size = new System.Drawing.Size(96, 24);
            this.labelItemAttack.TabIndex = 24;
            this.labelItemAttack.Text = "Attack: ";
            this.labelItemAttack.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItemAnimation
            // 
            this.comboBoxItemAnimation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemAnimation.Location = new System.Drawing.Point(132, 209);
            this.comboBoxItemAnimation.Name = "comboBoxItemAnimation";
            this.comboBoxItemAnimation.Size = new System.Drawing.Size(155, 24);
            this.comboBoxItemAnimation.TabIndex = 9;
            this.comboBoxItemAnimation.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxItemAnimation_Validating);
            // 
            // labelItemAnimation
            // 
            this.labelItemAnimation.Location = new System.Drawing.Point(27, 209);
            this.labelItemAnimation.Name = "labelItemAnimation";
            this.labelItemAnimation.Size = new System.Drawing.Size(96, 25);
            this.labelItemAnimation.TabIndex = 22;
            this.labelItemAnimation.Text = "Animation: ";
            this.labelItemAnimation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxItemAddress
            // 
            this.textBoxItemAddress.Location = new System.Drawing.Point(132, 12);
            this.textBoxItemAddress.Name = "textBoxItemAddress";
            this.textBoxItemAddress.ReadOnly = true;
            this.textBoxItemAddress.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemAddress.TabIndex = 2;
            // 
            // labelItemAddress
            // 
            this.labelItemAddress.Location = new System.Drawing.Point(27, 12);
            this.labelItemAddress.Name = "labelItemAddress";
            this.labelItemAddress.Size = new System.Drawing.Size(96, 24);
            this.labelItemAddress.TabIndex = 7;
            this.labelItemAddress.Text = "Address:";
            this.labelItemAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxItemEffectiveness
            // 
            this.textBoxItemEffectiveness.Location = new System.Drawing.Point(132, 181);
            this.textBoxItemEffectiveness.MaxLength = 3;
            this.textBoxItemEffectiveness.Name = "textBoxItemEffectiveness";
            this.textBoxItemEffectiveness.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemEffectiveness.TabIndex = 8;
            this.textBoxItemEffectiveness.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxItemEffectiveness_Validating);
            // 
            // comboBoxItemTechnique
            // 
            this.comboBoxItemTechnique.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemTechnique.Location = new System.Drawing.Point(132, 154);
            this.comboBoxItemTechnique.Name = "comboBoxItemTechnique";
            this.comboBoxItemTechnique.Size = new System.Drawing.Size(155, 24);
            this.comboBoxItemTechnique.TabIndex = 7;
            this.comboBoxItemTechnique.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxItemTechnique_Validating);
            // 
            // textBoxItemCost
            // 
            this.textBoxItemCost.Location = new System.Drawing.Point(132, 126);
            this.textBoxItemCost.MaxLength = 5;
            this.textBoxItemCost.Name = "textBoxItemCost";
            this.textBoxItemCost.Size = new System.Drawing.Size(155, 22);
            this.textBoxItemCost.TabIndex = 6;
            this.textBoxItemCost.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxItemCost_Validating);
            // 
            // labelItemEffectiveness
            // 
            this.labelItemEffectiveness.Location = new System.Drawing.Point(27, 181);
            this.labelItemEffectiveness.Name = "labelItemEffectiveness";
            this.labelItemEffectiveness.Size = new System.Drawing.Size(96, 25);
            this.labelItemEffectiveness.TabIndex = 3;
            this.labelItemEffectiveness.Text = "Effectiveness: ";
            this.labelItemEffectiveness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelItemTechnique
            // 
            this.labelItemTechnique.Location = new System.Drawing.Point(27, 154);
            this.labelItemTechnique.Name = "labelItemTechnique";
            this.labelItemTechnique.Size = new System.Drawing.Size(96, 24);
            this.labelItemTechnique.TabIndex = 2;
            this.labelItemTechnique.Text = "Technique: ";
            this.labelItemTechnique.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelItemCost
            // 
            this.labelItemCost.Location = new System.Drawing.Point(27, 126);
            this.labelItemCost.Name = "labelItemCost";
            this.labelItemCost.Size = new System.Drawing.Size(96, 24);
            this.labelItemCost.TabIndex = 1;
            this.labelItemCost.Text = "Cost: ";
            this.labelItemCost.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectItem
            // 
            this.comboBoxSelectItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectItem.Location = new System.Drawing.Point(115, 18);
            this.comboBoxSelectItem.Name = "comboBoxSelectItem";
            this.comboBoxSelectItem.Size = new System.Drawing.Size(297, 24);
            this.comboBoxSelectItem.TabIndex = 1;
            this.comboBoxSelectItem.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectItem_SelectedIndexChanged);
            // 
            // labelSelectItem
            // 
            this.labelSelectItem.Location = new System.Drawing.Point(10, 18);
            this.labelSelectItem.Name = "labelSelectItem";
            this.labelSelectItem.Size = new System.Drawing.Size(96, 25);
            this.labelSelectItem.TabIndex = 0;
            this.labelSelectItem.Text = "Select Item: ";
            this.labelSelectItem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageLevelTables
            // 
            this.tabPageLevelTables.Controls.Add(this.groupBoxStatisticGrowth);
            this.tabPageLevelTables.Controls.Add(this.listViewLevelTable);
            this.tabPageLevelTables.Controls.Add(this.comboBoxLevelTable);
            this.tabPageLevelTables.Controls.Add(this.labelSelectLevelTable);
            this.tabPageLevelTables.Location = new System.Drawing.Point(4, 55);
            this.tabPageLevelTables.Name = "tabPageLevelTables";
            this.tabPageLevelTables.Size = new System.Drawing.Size(700, 379);
            this.tabPageLevelTables.TabIndex = 13;
            this.tabPageLevelTables.Text = "Level Tables";
            // 
            // groupBoxStatisticGrowth
            // 
            this.groupBoxStatisticGrowth.Controls.Add(this.comboBoxLevelTableSpeed);
            this.groupBoxStatisticGrowth.Controls.Add(this.labelLevelTableSpeed);
            this.groupBoxStatisticGrowth.Controls.Add(this.comboBoxLevelTableSkill);
            this.groupBoxStatisticGrowth.Controls.Add(this.labelLevelTableSkill);
            this.groupBoxStatisticGrowth.Controls.Add(this.comboBoxLevelTableDamage);
            this.groupBoxStatisticGrowth.Controls.Add(this.labelLevelTableDamage);
            this.groupBoxStatisticGrowth.Controls.Add(this.comboBoxLevelTableLuck);
            this.groupBoxStatisticGrowth.Controls.Add(this.labelLevelTableLuck);
            this.groupBoxStatisticGrowth.Controls.Add(this.comboBoxLevelTableDefense);
            this.groupBoxStatisticGrowth.Controls.Add(this.labelLevelTableDefense);
            this.groupBoxStatisticGrowth.Controls.Add(this.comboBoxLevelTableTP);
            this.groupBoxStatisticGrowth.Controls.Add(this.labelLevelTableTP);
            this.groupBoxStatisticGrowth.Controls.Add(this.comboBoxLevelTableHP);
            this.groupBoxStatisticGrowth.Controls.Add(this.labelLevelTableHP);
            this.groupBoxStatisticGrowth.Location = new System.Drawing.Point(19, 55);
            this.groupBoxStatisticGrowth.Name = "groupBoxStatisticGrowth";
            this.groupBoxStatisticGrowth.Size = new System.Drawing.Size(550, 102);
            this.groupBoxStatisticGrowth.TabIndex = 16;
            this.groupBoxStatisticGrowth.TabStop = false;
            this.groupBoxStatisticGrowth.Text = "Statistic Growth";
            // 
            // comboBoxLevelTableSpeed
            // 
            this.comboBoxLevelTableSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableSpeed.Location = new System.Drawing.Point(466, 60);
            this.comboBoxLevelTableSpeed.Name = "comboBoxLevelTableSpeed";
            this.comboBoxLevelTableSpeed.Size = new System.Drawing.Size(76, 24);
            this.comboBoxLevelTableSpeed.TabIndex = 30;
            this.comboBoxLevelTableSpeed.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableSpeed_Validating);
            // 
            // labelLevelTableSpeed
            // 
            this.labelLevelTableSpeed.Location = new System.Drawing.Point(391, 60);
            this.labelLevelTableSpeed.Name = "labelLevelTableSpeed";
            this.labelLevelTableSpeed.Size = new System.Drawing.Size(67, 24);
            this.labelLevelTableSpeed.TabIndex = 35;
            this.labelLevelTableSpeed.Text = "Speed: ";
            this.labelLevelTableSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableSkill
            // 
            this.comboBoxLevelTableSkill.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableSkill.Location = new System.Drawing.Point(466, 22);
            this.comboBoxLevelTableSkill.Name = "comboBoxLevelTableSkill";
            this.comboBoxLevelTableSkill.Size = new System.Drawing.Size(76, 24);
            this.comboBoxLevelTableSkill.TabIndex = 25;
            this.comboBoxLevelTableSkill.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableSkill_Validating);
            // 
            // labelLevelTableSkill
            // 
            this.labelLevelTableSkill.Location = new System.Drawing.Point(420, 22);
            this.labelLevelTableSkill.Name = "labelLevelTableSkill";
            this.labelLevelTableSkill.Size = new System.Drawing.Size(38, 24);
            this.labelLevelTableSkill.TabIndex = 34;
            this.labelLevelTableSkill.Text = "Skill: ";
            this.labelLevelTableSkill.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableDamage
            // 
            this.comboBoxLevelTableDamage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableDamage.Location = new System.Drawing.Point(288, 60);
            this.comboBoxLevelTableDamage.Name = "comboBoxLevelTableDamage";
            this.comboBoxLevelTableDamage.Size = new System.Drawing.Size(77, 24);
            this.comboBoxLevelTableDamage.TabIndex = 29;
            this.comboBoxLevelTableDamage.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableDamage_Validating);
            // 
            // labelLevelTableDamage
            // 
            this.labelLevelTableDamage.Location = new System.Drawing.Point(214, 60);
            this.labelLevelTableDamage.Name = "labelLevelTableDamage";
            this.labelLevelTableDamage.Size = new System.Drawing.Size(67, 24);
            this.labelLevelTableDamage.TabIndex = 33;
            this.labelLevelTableDamage.Text = "Damage: ";
            this.labelLevelTableDamage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableLuck
            // 
            this.comboBoxLevelTableLuck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableLuck.Location = new System.Drawing.Point(323, 22);
            this.comboBoxLevelTableLuck.Name = "comboBoxLevelTableLuck";
            this.comboBoxLevelTableLuck.Size = new System.Drawing.Size(77, 24);
            this.comboBoxLevelTableLuck.TabIndex = 24;
            this.comboBoxLevelTableLuck.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableLuck_Validating);
            // 
            // labelLevelTableLuck
            // 
            this.labelLevelTableLuck.Location = new System.Drawing.Point(268, 22);
            this.labelLevelTableLuck.Name = "labelLevelTableLuck";
            this.labelLevelTableLuck.Size = new System.Drawing.Size(48, 24);
            this.labelLevelTableLuck.TabIndex = 32;
            this.labelLevelTableLuck.Text = "Luck: ";
            this.labelLevelTableLuck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableDefense
            // 
            this.comboBoxLevelTableDefense.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableDefense.Location = new System.Drawing.Point(112, 60);
            this.comboBoxLevelTableDefense.Name = "comboBoxLevelTableDefense";
            this.comboBoxLevelTableDefense.Size = new System.Drawing.Size(76, 24);
            this.comboBoxLevelTableDefense.TabIndex = 27;
            this.comboBoxLevelTableDefense.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableDefense_Validating);
            // 
            // labelLevelTableDefense
            // 
            this.labelLevelTableDefense.Location = new System.Drawing.Point(37, 60);
            this.labelLevelTableDefense.Name = "labelLevelTableDefense";
            this.labelLevelTableDefense.Size = new System.Drawing.Size(67, 24);
            this.labelLevelTableDefense.TabIndex = 31;
            this.labelLevelTableDefense.Text = "Defense: ";
            this.labelLevelTableDefense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableTP
            // 
            this.comboBoxLevelTableTP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableTP.Location = new System.Drawing.Point(172, 22);
            this.comboBoxLevelTableTP.Name = "comboBoxLevelTableTP";
            this.comboBoxLevelTableTP.Size = new System.Drawing.Size(76, 24);
            this.comboBoxLevelTableTP.TabIndex = 23;
            this.comboBoxLevelTableTP.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableTP_Validating);
            // 
            // labelLevelTableTP
            // 
            this.labelLevelTableTP.Location = new System.Drawing.Point(136, 22);
            this.labelLevelTableTP.Name = "labelLevelTableTP";
            this.labelLevelTableTP.Size = new System.Drawing.Size(28, 24);
            this.labelLevelTableTP.TabIndex = 28;
            this.labelLevelTableTP.Text = "TP: ";
            this.labelLevelTableTP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableHP
            // 
            this.comboBoxLevelTableHP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableHP.Location = new System.Drawing.Point(52, 22);
            this.comboBoxLevelTableHP.Name = "comboBoxLevelTableHP";
            this.comboBoxLevelTableHP.Size = new System.Drawing.Size(76, 24);
            this.comboBoxLevelTableHP.TabIndex = 22;
            this.comboBoxLevelTableHP.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableHP_Validating);
            // 
            // labelLevelTableHP
            // 
            this.labelLevelTableHP.Location = new System.Drawing.Point(12, 22);
            this.labelLevelTableHP.Name = "labelLevelTableHP";
            this.labelLevelTableHP.Size = new System.Drawing.Size(38, 24);
            this.labelLevelTableHP.TabIndex = 26;
            this.labelLevelTableHP.Text = "HP: ";
            this.labelLevelTableHP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listViewLevelTable
            // 
            this.listViewLevelTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderXP,
            this.columnHeaderLevel,
            this.columnHeaderLevelTableAddress});
            this.listViewLevelTable.LabelEdit = true;
            this.listViewLevelTable.Location = new System.Drawing.Point(19, 166);
            this.listViewLevelTable.MultiSelect = false;
            this.listViewLevelTable.Name = "listViewLevelTable";
            this.listViewLevelTable.Size = new System.Drawing.Size(550, 203);
            this.listViewLevelTable.TabIndex = 15;
            this.listViewLevelTable.UseCompatibleStateImageBehavior = false;
            this.listViewLevelTable.View = System.Windows.Forms.View.Details;
            this.listViewLevelTable.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewLevelTable_AfterLabelEdit);
            this.listViewLevelTable.DoubleClick += new System.EventHandler(this.listViewLevelTable_DoubleClick);
            this.listViewLevelTable.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewLevelTable_KeyPress);
            // 
            // columnHeaderXP
            // 
            this.columnHeaderXP.Text = "XP";
            this.columnHeaderXP.Width = 275;
            // 
            // columnHeaderLevel
            // 
            this.columnHeaderLevel.Text = "Level";
            this.columnHeaderLevel.Width = 51;
            // 
            // columnHeaderLevelTableAddress
            // 
            this.columnHeaderLevelTableAddress.Text = "Address";
            this.columnHeaderLevelTableAddress.Width = 54;
            // 
            // comboBoxLevelTable
            // 
            this.comboBoxLevelTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTable.Location = new System.Drawing.Point(144, 18);
            this.comboBoxLevelTable.Name = "comboBoxLevelTable";
            this.comboBoxLevelTable.Size = new System.Drawing.Size(425, 24);
            this.comboBoxLevelTable.TabIndex = 5;
            this.comboBoxLevelTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxLevelTable_SelectedIndexChanged);
            // 
            // labelSelectLevelTable
            // 
            this.labelSelectLevelTable.Location = new System.Drawing.Point(10, 18);
            this.labelSelectLevelTable.Name = "labelSelectLevelTable";
            this.labelSelectLevelTable.Size = new System.Drawing.Size(124, 25);
            this.labelSelectLevelTable.TabIndex = 4;
            this.labelSelectLevelTable.Text = "Select Level Table: ";
            this.labelSelectLevelTable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageNPCs
            // 
            this.tabPageNPCs.Controls.Add(this.panelNPCs);
            this.tabPageNPCs.Controls.Add(this.comboBoxNPCsSelectLocation);
            this.tabPageNPCs.Controls.Add(this.labelNPCsSelectLocation);
            this.tabPageNPCs.Location = new System.Drawing.Point(4, 55);
            this.tabPageNPCs.Name = "tabPageNPCs";
            this.tabPageNPCs.Size = new System.Drawing.Size(700, 379);
            this.tabPageNPCs.TabIndex = 16;
            this.tabPageNPCs.Text = "NPCs";
            this.tabPageNPCs.UseVisualStyleBackColor = true;
            // 
            // panelNPCs
            // 
            this.panelNPCs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelNPCs.Controls.Add(this.labelNPCsBetaFeature);
            this.panelNPCs.Controls.Add(this.textBoxNPCAddress);
            this.panelNPCs.Controls.Add(this.labelNPCAddress);
            this.panelNPCs.Controls.Add(this.textBoxNPCScriptText);
            this.panelNPCs.Controls.Add(this.labelNPCScriptText);
            this.panelNPCs.Controls.Add(this.textBoxNPCScriptAddress);
            this.panelNPCs.Controls.Add(this.labelNPCScriptAddress);
            this.panelNPCs.Controls.Add(this.textBoxNPCUpdateRate);
            this.panelNPCs.Controls.Add(this.labelNPCUpdateRate);
            this.panelNPCs.Controls.Add(this.textBoxNPCY);
            this.panelNPCs.Controls.Add(this.labelNPCY);
            this.panelNPCs.Controls.Add(this.textBoxNPCX);
            this.panelNPCs.Controls.Add(this.labelNPCX);
            this.panelNPCs.Controls.Add(this.comboBoxNPCSprite);
            this.panelNPCs.Controls.Add(this.labelNPCSprite);
            this.panelNPCs.Controls.Add(this.listBoxNPCs);
            this.panelNPCs.Controls.Add(this.labelNPCs);
            this.panelNPCs.Location = new System.Drawing.Point(28, 56);
            this.panelNPCs.Name = "panelNPCs";
            this.panelNPCs.Size = new System.Drawing.Size(399, 296);
            this.panelNPCs.TabIndex = 4;
            // 
            // labelNPCsBetaFeature
            // 
            this.labelNPCsBetaFeature.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNPCsBetaFeature.ForeColor = System.Drawing.SystemColors.Highlight;
            this.labelNPCsBetaFeature.Location = new System.Drawing.Point(7, 270);
            this.labelNPCsBetaFeature.Name = "labelNPCsBetaFeature";
            this.labelNPCsBetaFeature.Size = new System.Drawing.Size(387, 24);
            this.labelNPCsBetaFeature.TabIndex = 28;
            this.labelNPCsBetaFeature.Text = "Beta feature - see help file for usage and limitations";
            this.labelNPCsBetaFeature.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxNPCAddress
            // 
            this.textBoxNPCAddress.Enabled = false;
            this.textBoxNPCAddress.Location = new System.Drawing.Point(239, 38);
            this.textBoxNPCAddress.MaxLength = 5;
            this.textBoxNPCAddress.Name = "textBoxNPCAddress";
            this.textBoxNPCAddress.Size = new System.Drawing.Size(131, 22);
            this.textBoxNPCAddress.TabIndex = 5;
            // 
            // labelNPCAddress
            // 
            this.labelNPCAddress.AutoSize = true;
            this.labelNPCAddress.Location = new System.Drawing.Point(130, 41);
            this.labelNPCAddress.Name = "labelNPCAddress";
            this.labelNPCAddress.Size = new System.Drawing.Size(96, 17);
            this.labelNPCAddress.TabIndex = 27;
            this.labelNPCAddress.Text = "NPC Address:";
            this.labelNPCAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNPCScriptText
            // 
            this.textBoxNPCScriptText.Location = new System.Drawing.Point(239, 211);
            this.textBoxNPCScriptText.MaxLength = 5;
            this.textBoxNPCScriptText.Name = "textBoxNPCScriptText";
            this.textBoxNPCScriptText.Size = new System.Drawing.Size(131, 22);
            this.textBoxNPCScriptText.TabIndex = 11;
            this.textBoxNPCScriptText.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNPCScriptText_Validating);
            // 
            // labelNPCScriptText
            // 
            this.labelNPCScriptText.AutoSize = true;
            this.labelNPCScriptText.Location = new System.Drawing.Point(154, 214);
            this.labelNPCScriptText.Name = "labelNPCScriptText";
            this.labelNPCScriptText.Size = new System.Drawing.Size(79, 17);
            this.labelNPCScriptText.TabIndex = 25;
            this.labelNPCScriptText.Text = "Script Text:";
            this.labelNPCScriptText.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNPCScriptAddress
            // 
            this.textBoxNPCScriptAddress.Enabled = false;
            this.textBoxNPCScriptAddress.Location = new System.Drawing.Point(239, 183);
            this.textBoxNPCScriptAddress.MaxLength = 5;
            this.textBoxNPCScriptAddress.Name = "textBoxNPCScriptAddress";
            this.textBoxNPCScriptAddress.Size = new System.Drawing.Size(131, 22);
            this.textBoxNPCScriptAddress.TabIndex = 10;
            // 
            // labelNPCScriptAddress
            // 
            this.labelNPCScriptAddress.AutoSize = true;
            this.labelNPCScriptAddress.Location = new System.Drawing.Point(130, 186);
            this.labelNPCScriptAddress.Name = "labelNPCScriptAddress";
            this.labelNPCScriptAddress.Size = new System.Drawing.Size(104, 17);
            this.labelNPCScriptAddress.TabIndex = 23;
            this.labelNPCScriptAddress.Text = "Script Address:";
            this.labelNPCScriptAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNPCUpdateRate
            // 
            this.textBoxNPCUpdateRate.Location = new System.Drawing.Point(239, 155);
            this.textBoxNPCUpdateRate.MaxLength = 5;
            this.textBoxNPCUpdateRate.Name = "textBoxNPCUpdateRate";
            this.textBoxNPCUpdateRate.Size = new System.Drawing.Size(131, 22);
            this.textBoxNPCUpdateRate.TabIndex = 9;
            this.textBoxNPCUpdateRate.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNPCUpdateRate_Validating);
            // 
            // labelNPCUpdateRate
            // 
            this.labelNPCUpdateRate.AutoSize = true;
            this.labelNPCUpdateRate.Location = new System.Drawing.Point(141, 158);
            this.labelNPCUpdateRate.Name = "labelNPCUpdateRate";
            this.labelNPCUpdateRate.Size = new System.Drawing.Size(92, 17);
            this.labelNPCUpdateRate.TabIndex = 21;
            this.labelNPCUpdateRate.Text = "Update Rate:";
            this.labelNPCUpdateRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNPCY
            // 
            this.textBoxNPCY.Location = new System.Drawing.Point(239, 125);
            this.textBoxNPCY.MaxLength = 5;
            this.textBoxNPCY.Name = "textBoxNPCY";
            this.textBoxNPCY.Size = new System.Drawing.Size(131, 22);
            this.textBoxNPCY.TabIndex = 8;
            this.textBoxNPCY.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNPCY_Validating);
            // 
            // labelNPCY
            // 
            this.labelNPCY.AutoSize = true;
            this.labelNPCY.Location = new System.Drawing.Point(212, 130);
            this.labelNPCY.Name = "labelNPCY";
            this.labelNPCY.Size = new System.Drawing.Size(21, 17);
            this.labelNPCY.TabIndex = 19;
            this.labelNPCY.Text = "Y:";
            this.labelNPCY.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxNPCX
            // 
            this.textBoxNPCX.Location = new System.Drawing.Point(239, 96);
            this.textBoxNPCX.MaxLength = 5;
            this.textBoxNPCX.Name = "textBoxNPCX";
            this.textBoxNPCX.Size = new System.Drawing.Size(131, 22);
            this.textBoxNPCX.TabIndex = 7;
            this.textBoxNPCX.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxNPCX_Validating);
            // 
            // labelNPCX
            // 
            this.labelNPCX.AutoSize = true;
            this.labelNPCX.Location = new System.Drawing.Point(212, 99);
            this.labelNPCX.Name = "labelNPCX";
            this.labelNPCX.Size = new System.Drawing.Size(21, 17);
            this.labelNPCX.TabIndex = 17;
            this.labelNPCX.Text = "X:";
            this.labelNPCX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxNPCSprite
            // 
            this.comboBoxNPCSprite.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNPCSprite.Location = new System.Drawing.Point(239, 65);
            this.comboBoxNPCSprite.Name = "comboBoxNPCSprite";
            this.comboBoxNPCSprite.Size = new System.Drawing.Size(131, 24);
            this.comboBoxNPCSprite.TabIndex = 6;
            this.comboBoxNPCSprite.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxNPCSprite_Validating);
            // 
            // labelNPCSprite
            // 
            this.labelNPCSprite.Location = new System.Drawing.Point(144, 65);
            this.labelNPCSprite.Name = "labelNPCSprite";
            this.labelNPCSprite.Size = new System.Drawing.Size(89, 25);
            this.labelNPCSprite.TabIndex = 15;
            this.labelNPCSprite.Text = "Sprite:";
            this.labelNPCSprite.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listBoxNPCs
            // 
            this.listBoxNPCs.FormattingEnabled = true;
            this.listBoxNPCs.ItemHeight = 16;
            this.listBoxNPCs.Location = new System.Drawing.Point(10, 38);
            this.listBoxNPCs.Name = "listBoxNPCs";
            this.listBoxNPCs.Size = new System.Drawing.Size(112, 212);
            this.listBoxNPCs.TabIndex = 4;
            this.listBoxNPCs.SelectedIndexChanged += new System.EventHandler(this.listBoxNPCs_SelectedIndexChanged);
            // 
            // labelNPCs
            // 
            this.labelNPCs.Location = new System.Drawing.Point(7, 10);
            this.labelNPCs.Name = "labelNPCs";
            this.labelNPCs.Size = new System.Drawing.Size(108, 25);
            this.labelNPCs.TabIndex = 13;
            this.labelNPCs.Text = "Select NPC";
            this.labelNPCs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxNPCsSelectLocation
            // 
            this.comboBoxNPCsSelectLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxNPCsSelectLocation.Location = new System.Drawing.Point(149, 14);
            this.comboBoxNPCsSelectLocation.Name = "comboBoxNPCsSelectLocation";
            this.comboBoxNPCsSelectLocation.Size = new System.Drawing.Size(278, 24);
            this.comboBoxNPCsSelectLocation.Sorted = true;
            this.comboBoxNPCsSelectLocation.TabIndex = 3;
            this.comboBoxNPCsSelectLocation.SelectedIndexChanged += new System.EventHandler(this.comboBoxNPCsSelectLocation_SelectedIndexChanged);
            // 
            // labelNPCsSelectLocation
            // 
            this.labelNPCsSelectLocation.Location = new System.Drawing.Point(7, 14);
            this.labelNPCsSelectLocation.Name = "labelNPCsSelectLocation";
            this.labelNPCsSelectLocation.Size = new System.Drawing.Size(136, 25);
            this.labelNPCsSelectLocation.TabIndex = 2;
            this.labelNPCsSelectLocation.Text = "Select Location: ";
            this.labelNPCsSelectLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPagePalettes
            // 
            this.tabPagePalettes.Controls.Add(this.labelPaletteFind);
            this.tabPagePalettes.Controls.Add(this.buttonPalettePrevious);
            this.tabPagePalettes.Controls.Add(this.buttonPaletteFind);
            this.tabPagePalettes.Controls.Add(this.textBoxPaletteFind);
            this.tabPagePalettes.Controls.Add(this.labelPalettePreview);
            this.tabPagePalettes.Controls.Add(this.pictureBoxPalettePreview);
            this.tabPagePalettes.Controls.Add(this.buttonLaunchPaletteEditor);
            this.tabPagePalettes.Controls.Add(this.listViewPalettes);
            this.tabPagePalettes.Location = new System.Drawing.Point(4, 55);
            this.tabPagePalettes.Name = "tabPagePalettes";
            this.tabPagePalettes.Size = new System.Drawing.Size(700, 379);
            this.tabPagePalettes.TabIndex = 11;
            this.tabPagePalettes.Text = "Palettes";
            // 
            // labelPaletteFind
            // 
            this.labelPaletteFind.Location = new System.Drawing.Point(3, 351);
            this.labelPaletteFind.Name = "labelPaletteFind";
            this.labelPaletteFind.Size = new System.Drawing.Size(45, 23);
            this.labelPaletteFind.TabIndex = 14;
            this.labelPaletteFind.Text = "Find:";
            this.labelPaletteFind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonPalettePrevious
            // 
            this.buttonPalettePrevious.Enabled = false;
            this.buttonPalettePrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPalettePrevious.Location = new System.Drawing.Point(445, 351);
            this.buttonPalettePrevious.Name = "buttonPalettePrevious";
            this.buttonPalettePrevious.Size = new System.Drawing.Size(82, 23);
            this.buttonPalettePrevious.TabIndex = 13;
            this.buttonPalettePrevious.Text = "Previous";
            this.buttonPalettePrevious.Click += new System.EventHandler(this.buttonPalettePrevious_Click);
            // 
            // buttonPaletteFind
            // 
            this.buttonPaletteFind.Enabled = false;
            this.buttonPaletteFind.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonPaletteFind.Location = new System.Drawing.Point(355, 351);
            this.buttonPaletteFind.Name = "buttonPaletteFind";
            this.buttonPaletteFind.Size = new System.Drawing.Size(82, 23);
            this.buttonPaletteFind.TabIndex = 12;
            this.buttonPaletteFind.Text = "Next";
            this.buttonPaletteFind.Click += new System.EventHandler(this.buttonPaletteFind_Click);
            // 
            // textBoxPaletteFind
            // 
            this.textBoxPaletteFind.Enabled = false;
            this.textBoxPaletteFind.Location = new System.Drawing.Point(58, 351);
            this.textBoxPaletteFind.Name = "textBoxPaletteFind";
            this.textBoxPaletteFind.Size = new System.Drawing.Size(288, 22);
            this.textBoxPaletteFind.TabIndex = 11;
            this.textBoxPaletteFind.TextChanged += new System.EventHandler(this.textBoxPaletteFind_TextChanged);
            // 
            // labelPalettePreview
            // 
            this.labelPalettePreview.Location = new System.Drawing.Point(10, 314);
            this.labelPalettePreview.Name = "labelPalettePreview";
            this.labelPalettePreview.Size = new System.Drawing.Size(57, 18);
            this.labelPalettePreview.TabIndex = 10;
            this.labelPalettePreview.Text = "Preview:";
            this.labelPalettePreview.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBoxPalettePreview
            // 
            this.pictureBoxPalettePreview.Location = new System.Drawing.Point(77, 314);
            this.pictureBoxPalettePreview.Name = "pictureBoxPalettePreview";
            this.pictureBoxPalettePreview.Size = new System.Drawing.Size(269, 16);
            this.pictureBoxPalettePreview.TabIndex = 9;
            this.pictureBoxPalettePreview.TabStop = false;
            this.pictureBoxPalettePreview.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxPalettePreview_Paint);
            // 
            // buttonLaunchPaletteEditor
            // 
            this.buttonLaunchPaletteEditor.Enabled = false;
            this.buttonLaunchPaletteEditor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLaunchPaletteEditor.Image = ((System.Drawing.Image)(resources.GetObject("buttonLaunchPaletteEditor.Image")));
            this.buttonLaunchPaletteEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLaunchPaletteEditor.Location = new System.Drawing.Point(355, 305);
            this.buttonLaunchPaletteEditor.Name = "buttonLaunchPaletteEditor";
            this.buttonLaunchPaletteEditor.Size = new System.Drawing.Size(172, 37);
            this.buttonLaunchPaletteEditor.TabIndex = 8;
            this.buttonLaunchPaletteEditor.Text = "Launch Palette Editor";
            this.buttonLaunchPaletteEditor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLaunchPaletteEditor.Click += new System.EventHandler(this.buttonLaunchPaletteEditor_Click);
            // 
            // listViewPalettes
            // 
            this.listViewPalettes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader10});
            this.listViewPalettes.HideSelection = false;
            this.listViewPalettes.Location = new System.Drawing.Point(10, 6);
            this.listViewPalettes.MultiSelect = false;
            this.listViewPalettes.Name = "listViewPalettes";
            this.listViewPalettes.Size = new System.Drawing.Size(559, 289);
            this.listViewPalettes.TabIndex = 7;
            this.listViewPalettes.UseCompatibleStateImageBehavior = false;
            this.listViewPalettes.View = System.Windows.Forms.View.Details;
            this.listViewPalettes.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewPalettes_ColumnClick);
            this.listViewPalettes.SelectedIndexChanged += new System.EventHandler(this.listViewPalettes_SelectedIndexChanged);
            this.listViewPalettes.DoubleClick += new System.EventHandler(this.listViewPalettes_DoubleClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Description";
            this.columnHeader6.Width = 322;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Address";
            this.columnHeader10.Width = 69;
            // 
            // tabPageScript
            // 
            this.tabPageScript.Controls.Add(this.labelFindScript);
            this.tabPageScript.Controls.Add(this.buttonFindScriptPrevious);
            this.tabPageScript.Controls.Add(this.buttonFindScriptNext);
            this.tabPageScript.Controls.Add(this.textBoxFindScript);
            this.tabPageScript.Controls.Add(this.listViewScript);
            this.tabPageScript.Location = new System.Drawing.Point(4, 55);
            this.tabPageScript.Name = "tabPageScript";
            this.tabPageScript.Size = new System.Drawing.Size(700, 379);
            this.tabPageScript.TabIndex = 1;
            this.tabPageScript.Text = "Script";
            this.tabPageScript.ToolTipText = "Edit scripted text";
            // 
            // labelFindScript
            // 
            this.labelFindScript.Location = new System.Drawing.Point(3, 351);
            this.labelFindScript.Name = "labelFindScript";
            this.labelFindScript.Size = new System.Drawing.Size(45, 23);
            this.labelFindScript.TabIndex = 10;
            this.labelFindScript.Text = "Find:";
            this.labelFindScript.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonFindScriptPrevious
            // 
            this.buttonFindScriptPrevious.Enabled = false;
            this.buttonFindScriptPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFindScriptPrevious.Location = new System.Drawing.Point(609, 351);
            this.buttonFindScriptPrevious.Name = "buttonFindScriptPrevious";
            this.buttonFindScriptPrevious.Size = new System.Drawing.Size(82, 23);
            this.buttonFindScriptPrevious.TabIndex = 9;
            this.buttonFindScriptPrevious.Text = "Previous";
            this.buttonFindScriptPrevious.Click += new System.EventHandler(this.buttonFindScriptPrevious_Click);
            // 
            // buttonFindScriptNext
            // 
            this.buttonFindScriptNext.Enabled = false;
            this.buttonFindScriptNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFindScriptNext.Location = new System.Drawing.Point(520, 351);
            this.buttonFindScriptNext.Name = "buttonFindScriptNext";
            this.buttonFindScriptNext.Size = new System.Drawing.Size(82, 23);
            this.buttonFindScriptNext.TabIndex = 8;
            this.buttonFindScriptNext.Text = "Next";
            this.buttonFindScriptNext.Click += new System.EventHandler(this.buttonFindScriptNext_Click);
            // 
            // textBoxFindScript
            // 
            this.textBoxFindScript.Enabled = false;
            this.textBoxFindScript.Location = new System.Drawing.Point(58, 351);
            this.textBoxFindScript.Name = "textBoxFindScript";
            this.textBoxFindScript.Size = new System.Drawing.Size(456, 22);
            this.textBoxFindScript.TabIndex = 7;
            this.textBoxFindScript.TextChanged += new System.EventHandler(this.textBoxFindScript_TextChanged);
            // 
            // listViewScript
            // 
            this.listViewScript.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewScript.LabelEdit = true;
            this.listViewScript.Location = new System.Drawing.Point(10, 9);
            this.listViewScript.MultiSelect = false;
            this.listViewScript.Name = "listViewScript";
            this.listViewScript.Size = new System.Drawing.Size(681, 333);
            this.listViewScript.TabIndex = 6;
            this.listViewScript.UseCompatibleStateImageBehavior = false;
            this.listViewScript.View = System.Windows.Forms.View.Details;
            this.listViewScript.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewScript_AfterLabelEdit);
            this.listViewScript.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewScript_ColumnClick);
            this.listViewScript.DoubleClick += new System.EventHandler(this.listViewScript_DoubleClick);
            this.listViewScript.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.listViewScript_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Current Value";
            this.columnHeader1.Width = 294;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Address";
            this.columnHeader4.Width = 51;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Length";
            this.columnHeader5.Width = 54;
            // 
            // tabPageScriptedEvents
            // 
            this.tabPageScriptedEvents.Controls.Add(this.panelScriptedEvents);
            this.tabPageScriptedEvents.Controls.Add(this.comboBoxSelectScriptedEvent);
            this.tabPageScriptedEvents.Controls.Add(this.labelSelectScriptedEvent);
            this.tabPageScriptedEvents.Location = new System.Drawing.Point(4, 55);
            this.tabPageScriptedEvents.Name = "tabPageScriptedEvents";
            this.tabPageScriptedEvents.Size = new System.Drawing.Size(700, 379);
            this.tabPageScriptedEvents.TabIndex = 17;
            this.tabPageScriptedEvents.Text = "Scripted Events";
            this.tabPageScriptedEvents.UseVisualStyleBackColor = true;
            // 
            // panelScriptedEvents
            // 
            this.panelScriptedEvents.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelScriptedEvents.Controls.Add(this.comboBoxScriptedEventDialogOffset);
            this.panelScriptedEvents.Controls.Add(this.textBoxScriptedEventDelay);
            this.panelScriptedEvents.Controls.Add(this.labelScriptedEventDelay);
            this.panelScriptedEvents.Controls.Add(this.radioButtonScriptedEventDelay);
            this.panelScriptedEvents.Controls.Add(this.textBoxScriptedEventDuration);
            this.panelScriptedEvents.Controls.Add(this.textBoxScriptedEventDialogValue);
            this.panelScriptedEvents.Controls.Add(this.labelScriptedEventDialogValue);
            this.panelScriptedEvents.Controls.Add(this.labelScriptedEventDialogAddress);
            this.panelScriptedEvents.Controls.Add(this.radioButtonScriptedEventDialog);
            this.panelScriptedEvents.Controls.Add(this.labelScriptedEventDuration);
            this.panelScriptedEvents.Controls.Add(this.comboBoxScriptedEventDirection);
            this.panelScriptedEvents.Controls.Add(this.labelScriptedEventDirection);
            this.panelScriptedEvents.Controls.Add(this.radioButtonScriptedEventMovement);
            this.panelScriptedEvents.Controls.Add(this.listBoxScriptedEventFrames);
            this.panelScriptedEvents.Controls.Add(this.labelScriptedEventFrames);
            this.panelScriptedEvents.Location = new System.Drawing.Point(30, 70);
            this.panelScriptedEvents.Name = "panelScriptedEvents";
            this.panelScriptedEvents.Size = new System.Drawing.Size(457, 294);
            this.panelScriptedEvents.TabIndex = 2;
            // 
            // comboBoxScriptedEventDialogOffset
            // 
            this.comboBoxScriptedEventDialogOffset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScriptedEventDialogOffset.FormattingEnabled = true;
            this.comboBoxScriptedEventDialogOffset.Location = new System.Drawing.Point(299, 173);
            this.comboBoxScriptedEventDialogOffset.Name = "comboBoxScriptedEventDialogOffset";
            this.comboBoxScriptedEventDialogOffset.Size = new System.Drawing.Size(146, 24);
            this.comboBoxScriptedEventDialogOffset.TabIndex = 9;
            this.comboBoxScriptedEventDialogOffset.SelectedIndexChanged += new System.EventHandler(this.comboBoxScriptedEventDialogOffset_SelectedIndexChanged);
            this.comboBoxScriptedEventDialogOffset.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxScriptedEventDialogOffset_Validating);
            // 
            // textBoxScriptedEventDelay
            // 
            this.textBoxScriptedEventDelay.Location = new System.Drawing.Point(299, 260);
            this.textBoxScriptedEventDelay.MaxLength = 3;
            this.textBoxScriptedEventDelay.Name = "textBoxScriptedEventDelay";
            this.textBoxScriptedEventDelay.Size = new System.Drawing.Size(142, 22);
            this.textBoxScriptedEventDelay.TabIndex = 14;
            this.textBoxScriptedEventDelay.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxScriptedEventDelay_Validating);
            // 
            // labelScriptedEventDelay
            // 
            this.labelScriptedEventDelay.AutoSize = true;
            this.labelScriptedEventDelay.Location = new System.Drawing.Point(228, 263);
            this.labelScriptedEventDelay.Name = "labelScriptedEventDelay";
            this.labelScriptedEventDelay.Size = new System.Drawing.Size(66, 17);
            this.labelScriptedEventDelay.TabIndex = 13;
            this.labelScriptedEventDelay.Text = "Duration:";
            this.labelScriptedEventDelay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButtonScriptedEventDelay
            // 
            this.radioButtonScriptedEventDelay.AutoSize = true;
            this.radioButtonScriptedEventDelay.Location = new System.Drawing.Point(173, 239);
            this.radioButtonScriptedEventDelay.Name = "radioButtonScriptedEventDelay";
            this.radioButtonScriptedEventDelay.Size = new System.Drawing.Size(65, 21);
            this.radioButtonScriptedEventDelay.TabIndex = 12;
            this.radioButtonScriptedEventDelay.TabStop = true;
            this.radioButtonScriptedEventDelay.Text = "Delay";
            this.radioButtonScriptedEventDelay.UseVisualStyleBackColor = true;
            this.radioButtonScriptedEventDelay.CheckedChanged += new System.EventHandler(this.radioButtonScriptedEventDelay_CheckedChanged);
            // 
            // textBoxScriptedEventDuration
            // 
            this.textBoxScriptedEventDuration.Location = new System.Drawing.Point(299, 98);
            this.textBoxScriptedEventDuration.MaxLength = 3;
            this.textBoxScriptedEventDuration.Name = "textBoxScriptedEventDuration";
            this.textBoxScriptedEventDuration.Size = new System.Drawing.Size(142, 22);
            this.textBoxScriptedEventDuration.TabIndex = 6;
            this.textBoxScriptedEventDuration.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxScriptedEventDuration_Validating);
            // 
            // textBoxScriptedEventDialogValue
            // 
            this.textBoxScriptedEventDialogValue.Location = new System.Drawing.Point(299, 201);
            this.textBoxScriptedEventDialogValue.Name = "textBoxScriptedEventDialogValue";
            this.textBoxScriptedEventDialogValue.Size = new System.Drawing.Size(142, 22);
            this.textBoxScriptedEventDialogValue.TabIndex = 11;
            this.textBoxScriptedEventDialogValue.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxScriptedEventDialogValue_Validating);
            // 
            // labelScriptedEventDialogValue
            // 
            this.labelScriptedEventDialogValue.AutoSize = true;
            this.labelScriptedEventDialogValue.Location = new System.Drawing.Point(246, 204);
            this.labelScriptedEventDialogValue.Name = "labelScriptedEventDialogValue";
            this.labelScriptedEventDialogValue.Size = new System.Drawing.Size(48, 17);
            this.labelScriptedEventDialogValue.TabIndex = 10;
            this.labelScriptedEventDialogValue.Text = "Value:";
            this.labelScriptedEventDialogValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelScriptedEventDialogAddress
            // 
            this.labelScriptedEventDialogAddress.AutoSize = true;
            this.labelScriptedEventDialogAddress.Location = new System.Drawing.Point(188, 176);
            this.labelScriptedEventDialogAddress.Name = "labelScriptedEventDialogAddress";
            this.labelScriptedEventDialogAddress.Size = new System.Drawing.Size(106, 17);
            this.labelScriptedEventDialogAddress.TabIndex = 8;
            this.labelScriptedEventDialogAddress.Text = "Address Offset:";
            this.labelScriptedEventDialogAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButtonScriptedEventDialog
            // 
            this.radioButtonScriptedEventDialog.AutoSize = true;
            this.radioButtonScriptedEventDialog.Location = new System.Drawing.Point(173, 152);
            this.radioButtonScriptedEventDialog.Name = "radioButtonScriptedEventDialog";
            this.radioButtonScriptedEventDialog.Size = new System.Drawing.Size(69, 21);
            this.radioButtonScriptedEventDialog.TabIndex = 7;
            this.radioButtonScriptedEventDialog.TabStop = true;
            this.radioButtonScriptedEventDialog.Text = "Dialog";
            this.radioButtonScriptedEventDialog.UseVisualStyleBackColor = true;
            this.radioButtonScriptedEventDialog.CheckedChanged += new System.EventHandler(this.radioButtonScriptedEventDialog_CheckedChanged);
            // 
            // labelScriptedEventDuration
            // 
            this.labelScriptedEventDuration.AutoSize = true;
            this.labelScriptedEventDuration.Location = new System.Drawing.Point(228, 101);
            this.labelScriptedEventDuration.Name = "labelScriptedEventDuration";
            this.labelScriptedEventDuration.Size = new System.Drawing.Size(66, 17);
            this.labelScriptedEventDuration.TabIndex = 5;
            this.labelScriptedEventDuration.Text = "Duration:";
            this.labelScriptedEventDuration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxScriptedEventDirection
            // 
            this.comboBoxScriptedEventDirection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScriptedEventDirection.FormattingEnabled = true;
            this.comboBoxScriptedEventDirection.Location = new System.Drawing.Point(299, 68);
            this.comboBoxScriptedEventDirection.Name = "comboBoxScriptedEventDirection";
            this.comboBoxScriptedEventDirection.Size = new System.Drawing.Size(142, 24);
            this.comboBoxScriptedEventDirection.TabIndex = 4;
            this.comboBoxScriptedEventDirection.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxScriptedEventDirection_Validating);
            // 
            // labelScriptedEventDirection
            // 
            this.labelScriptedEventDirection.AutoSize = true;
            this.labelScriptedEventDirection.Location = new System.Drawing.Point(226, 71);
            this.labelScriptedEventDirection.Name = "labelScriptedEventDirection";
            this.labelScriptedEventDirection.Size = new System.Drawing.Size(68, 17);
            this.labelScriptedEventDirection.TabIndex = 3;
            this.labelScriptedEventDirection.Text = "Direction:";
            this.labelScriptedEventDirection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // radioButtonScriptedEventMovement
            // 
            this.radioButtonScriptedEventMovement.AutoSize = true;
            this.radioButtonScriptedEventMovement.Location = new System.Drawing.Point(173, 47);
            this.radioButtonScriptedEventMovement.Name = "radioButtonScriptedEventMovement";
            this.radioButtonScriptedEventMovement.Size = new System.Drawing.Size(94, 21);
            this.radioButtonScriptedEventMovement.TabIndex = 2;
            this.radioButtonScriptedEventMovement.TabStop = true;
            this.radioButtonScriptedEventMovement.Text = "Movement";
            this.radioButtonScriptedEventMovement.UseVisualStyleBackColor = true;
            this.radioButtonScriptedEventMovement.CheckedChanged += new System.EventHandler(this.radioButtonScriptedEventMovement_CheckedChanged);
            // 
            // listBoxScriptedEventFrames
            // 
            this.listBoxScriptedEventFrames.FormattingEnabled = true;
            this.listBoxScriptedEventFrames.ItemHeight = 16;
            this.listBoxScriptedEventFrames.Location = new System.Drawing.Point(12, 39);
            this.listBoxScriptedEventFrames.Name = "listBoxScriptedEventFrames";
            this.listBoxScriptedEventFrames.Size = new System.Drawing.Size(147, 244);
            this.listBoxScriptedEventFrames.TabIndex = 1;
            this.listBoxScriptedEventFrames.SelectedIndexChanged += new System.EventHandler(this.listBoxScriptedEventFrames_SelectedIndexChanged);
            // 
            // labelScriptedEventFrames
            // 
            this.labelScriptedEventFrames.AutoSize = true;
            this.labelScriptedEventFrames.Location = new System.Drawing.Point(10, 13);
            this.labelScriptedEventFrames.Name = "labelScriptedEventFrames";
            this.labelScriptedEventFrames.Size = new System.Drawing.Size(91, 17);
            this.labelScriptedEventFrames.TabIndex = 0;
            this.labelScriptedEventFrames.Text = "Select Frame";
            // 
            // comboBoxSelectScriptedEvent
            // 
            this.comboBoxSelectScriptedEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectScriptedEvent.FormattingEnabled = true;
            this.comboBoxSelectScriptedEvent.Location = new System.Drawing.Point(128, 30);
            this.comboBoxSelectScriptedEvent.Name = "comboBoxSelectScriptedEvent";
            this.comboBoxSelectScriptedEvent.Size = new System.Drawing.Size(359, 24);
            this.comboBoxSelectScriptedEvent.TabIndex = 1;
            this.comboBoxSelectScriptedEvent.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectScriptedEvent_SelectedIndexChanged);
            // 
            // labelSelectScriptedEvent
            // 
            this.labelSelectScriptedEvent.AutoSize = true;
            this.labelSelectScriptedEvent.Location = new System.Drawing.Point(27, 33);
            this.labelSelectScriptedEvent.Name = "labelSelectScriptedEvent";
            this.labelSelectScriptedEvent.Size = new System.Drawing.Size(95, 17);
            this.labelSelectScriptedEvent.TabIndex = 0;
            this.labelSelectScriptedEvent.Text = "Select Event: ";
            this.labelSelectScriptedEvent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageShops
            // 
            this.tabPageShops.Controls.Add(this.panelShop);
            this.tabPageShops.Controls.Add(this.comboBoxSelectShop);
            this.tabPageShops.Controls.Add(this.labelSelectShop);
            this.tabPageShops.Location = new System.Drawing.Point(4, 55);
            this.tabPageShops.Name = "tabPageShops";
            this.tabPageShops.Size = new System.Drawing.Size(700, 379);
            this.tabPageShops.TabIndex = 12;
            this.tabPageShops.Text = "Shops";
            // 
            // panelShop
            // 
            this.panelShop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelShop.Controls.Add(this.comboBoxItem5);
            this.panelShop.Controls.Add(this.labelItem5);
            this.panelShop.Controls.Add(this.comboBoxItem4);
            this.panelShop.Controls.Add(this.labelItem4);
            this.panelShop.Controls.Add(this.comboBoxItem3);
            this.panelShop.Controls.Add(this.labelItem3);
            this.panelShop.Controls.Add(this.comboBoxItem2);
            this.panelShop.Controls.Add(this.labelItem2);
            this.panelShop.Controls.Add(this.textBoxShopAddress);
            this.panelShop.Controls.Add(this.labelShopAddress);
            this.panelShop.Controls.Add(this.comboBoxItem1);
            this.panelShop.Controls.Add(this.labelItem1);
            this.panelShop.Location = new System.Drawing.Point(29, 55);
            this.panelShop.Name = "panelShop";
            this.panelShop.Size = new System.Drawing.Size(427, 185);
            this.panelShop.TabIndex = 10;
            // 
            // comboBoxItem5
            // 
            this.comboBoxItem5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem5.Location = new System.Drawing.Point(115, 148);
            this.comboBoxItem5.Name = "comboBoxItem5";
            this.comboBoxItem5.Size = new System.Drawing.Size(292, 24);
            this.comboBoxItem5.TabIndex = 19;
            this.comboBoxItem5.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxItem5_Validating);
            // 
            // labelItem5
            // 
            this.labelItem5.Location = new System.Drawing.Point(10, 148);
            this.labelItem5.Name = "labelItem5";
            this.labelItem5.Size = new System.Drawing.Size(96, 24);
            this.labelItem5.TabIndex = 18;
            this.labelItem5.Text = "Item 5: ";
            this.labelItem5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItem4
            // 
            this.comboBoxItem4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem4.Location = new System.Drawing.Point(115, 120);
            this.comboBoxItem4.Name = "comboBoxItem4";
            this.comboBoxItem4.Size = new System.Drawing.Size(292, 24);
            this.comboBoxItem4.TabIndex = 17;
            this.comboBoxItem4.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxItem4_Validating);
            // 
            // labelItem4
            // 
            this.labelItem4.Location = new System.Drawing.Point(10, 120);
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Size = new System.Drawing.Size(96, 24);
            this.labelItem4.TabIndex = 16;
            this.labelItem4.Text = "Item 4: ";
            this.labelItem4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItem3
            // 
            this.comboBoxItem3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem3.Location = new System.Drawing.Point(115, 92);
            this.comboBoxItem3.Name = "comboBoxItem3";
            this.comboBoxItem3.Size = new System.Drawing.Size(292, 24);
            this.comboBoxItem3.TabIndex = 15;
            this.comboBoxItem3.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxItem3_Validating);
            // 
            // labelItem3
            // 
            this.labelItem3.Location = new System.Drawing.Point(10, 92);
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Size = new System.Drawing.Size(96, 25);
            this.labelItem3.TabIndex = 14;
            this.labelItem3.Text = "Item 3: ";
            this.labelItem3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItem2
            // 
            this.comboBoxItem2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem2.Location = new System.Drawing.Point(115, 65);
            this.comboBoxItem2.Name = "comboBoxItem2";
            this.comboBoxItem2.Size = new System.Drawing.Size(292, 24);
            this.comboBoxItem2.TabIndex = 13;
            this.comboBoxItem2.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxItem2_Validating);
            // 
            // labelItem2
            // 
            this.labelItem2.Location = new System.Drawing.Point(10, 65);
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Size = new System.Drawing.Size(96, 24);
            this.labelItem2.TabIndex = 12;
            this.labelItem2.Text = "Item 2: ";
            this.labelItem2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxShopAddress
            // 
            this.textBoxShopAddress.Location = new System.Drawing.Point(115, 9);
            this.textBoxShopAddress.Name = "textBoxShopAddress";
            this.textBoxShopAddress.ReadOnly = true;
            this.textBoxShopAddress.Size = new System.Drawing.Size(292, 22);
            this.textBoxShopAddress.TabIndex = 11;
            // 
            // labelShopAddress
            // 
            this.labelShopAddress.Location = new System.Drawing.Point(10, 9);
            this.labelShopAddress.Name = "labelShopAddress";
            this.labelShopAddress.Size = new System.Drawing.Size(96, 24);
            this.labelShopAddress.TabIndex = 7;
            this.labelShopAddress.Text = "Address:";
            this.labelShopAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItem1
            // 
            this.comboBoxItem1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem1.Location = new System.Drawing.Point(115, 37);
            this.comboBoxItem1.Name = "comboBoxItem1";
            this.comboBoxItem1.Size = new System.Drawing.Size(292, 24);
            this.comboBoxItem1.TabIndex = 5;
            this.comboBoxItem1.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxItem1_Validating);
            // 
            // labelItem1
            // 
            this.labelItem1.Location = new System.Drawing.Point(10, 37);
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Size = new System.Drawing.Size(96, 24);
            this.labelItem1.TabIndex = 2;
            this.labelItem1.Text = "Item 1: ";
            this.labelItem1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectShop
            // 
            this.comboBoxSelectShop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectShop.Location = new System.Drawing.Point(133, 18);
            this.comboBoxSelectShop.Name = "comboBoxSelectShop";
            this.comboBoxSelectShop.Size = new System.Drawing.Size(323, 24);
            this.comboBoxSelectShop.TabIndex = 9;
            this.comboBoxSelectShop.SelectedIndexChanged += new System.EventHandler(this.comboBoxSelectShop_SelectedIndexChanged);
            // 
            // labelSelectShop
            // 
            this.labelSelectShop.Location = new System.Drawing.Point(19, 18);
            this.labelSelectShop.Name = "labelSelectShop";
            this.labelSelectShop.Size = new System.Drawing.Size(108, 25);
            this.labelSelectShop.TabIndex = 8;
            this.labelSelectShop.Text = "Select Shop: ";
            this.labelSelectShop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageTreasure
            // 
            this.tabPageTreasure.Controls.Add(this.comboBoxTreasureItem);
            this.tabPageTreasure.Controls.Add(this.labelTreasureItem);
            this.tabPageTreasure.Controls.Add(this.textBoxTreasureY);
            this.tabPageTreasure.Controls.Add(this.labelTreasureY);
            this.tabPageTreasure.Controls.Add(this.textBoxTreasureX);
            this.tabPageTreasure.Controls.Add(this.labelTreasureX);
            this.tabPageTreasure.Controls.Add(this.listViewTreasure);
            this.tabPageTreasure.Location = new System.Drawing.Point(4, 55);
            this.tabPageTreasure.Name = "tabPageTreasure";
            this.tabPageTreasure.Size = new System.Drawing.Size(700, 379);
            this.tabPageTreasure.TabIndex = 14;
            this.tabPageTreasure.Text = "Treasure Chests";
            this.tabPageTreasure.UseVisualStyleBackColor = true;
            // 
            // comboBoxTreasureItem
            // 
            this.comboBoxTreasureItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxTreasureItem.Location = new System.Drawing.Point(271, 334);
            this.comboBoxTreasureItem.Name = "comboBoxTreasureItem";
            this.comboBoxTreasureItem.Size = new System.Drawing.Size(250, 24);
            this.comboBoxTreasureItem.TabIndex = 14;
            this.comboBoxTreasureItem.Validating += new System.ComponentModel.CancelEventHandler(this.comboBoxTreasureItem_Validating);
            // 
            // labelTreasureItem
            // 
            this.labelTreasureItem.AutoSize = true;
            this.labelTreasureItem.Location = new System.Drawing.Point(227, 339);
            this.labelTreasureItem.Name = "labelTreasureItem";
            this.labelTreasureItem.Size = new System.Drawing.Size(38, 17);
            this.labelTreasureItem.TabIndex = 13;
            this.labelTreasureItem.Text = "Item:";
            // 
            // textBoxTreasureY
            // 
            this.textBoxTreasureY.Location = new System.Drawing.Point(142, 334);
            this.textBoxTreasureY.MaxLength = 5;
            this.textBoxTreasureY.Name = "textBoxTreasureY";
            this.textBoxTreasureY.Size = new System.Drawing.Size(68, 22);
            this.textBoxTreasureY.TabIndex = 12;
            this.textBoxTreasureY.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxTreasureY_Validating);
            // 
            // labelTreasureY
            // 
            this.labelTreasureY.AutoSize = true;
            this.labelTreasureY.Location = new System.Drawing.Point(115, 337);
            this.labelTreasureY.Name = "labelTreasureY";
            this.labelTreasureY.Size = new System.Drawing.Size(21, 17);
            this.labelTreasureY.TabIndex = 11;
            this.labelTreasureY.Text = "Y:";
            // 
            // textBoxTreasureX
            // 
            this.textBoxTreasureX.Location = new System.Drawing.Point(30, 334);
            this.textBoxTreasureX.MaxLength = 5;
            this.textBoxTreasureX.Name = "textBoxTreasureX";
            this.textBoxTreasureX.Size = new System.Drawing.Size(68, 22);
            this.textBoxTreasureX.TabIndex = 10;
            this.textBoxTreasureX.Validating += new System.ComponentModel.CancelEventHandler(this.textBoxTreasureX_Validating);
            // 
            // labelTreasureX
            // 
            this.labelTreasureX.AutoSize = true;
            this.labelTreasureX.Location = new System.Drawing.Point(3, 336);
            this.labelTreasureX.Name = "labelTreasureX";
            this.labelTreasureX.Size = new System.Drawing.Size(21, 17);
            this.labelTreasureX.TabIndex = 9;
            this.labelTreasureX.Text = "X:";
            // 
            // listViewTreasure
            // 
            this.listViewTreasure.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader11});
            this.listViewTreasure.HideSelection = false;
            this.listViewTreasure.Location = new System.Drawing.Point(3, 3);
            this.listViewTreasure.MultiSelect = false;
            this.listViewTreasure.Name = "listViewTreasure";
            this.listViewTreasure.Size = new System.Drawing.Size(518, 318);
            this.listViewTreasure.TabIndex = 8;
            this.listViewTreasure.UseCompatibleStateImageBehavior = false;
            this.listViewTreasure.View = System.Windows.Forms.View.Details;
            this.listViewTreasure.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listViewTreasure_ColumnClick);
            this.listViewTreasure.SelectedIndexChanged += new System.EventHandler(this.listViewTreasure_SelectedIndexChanged);
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Description";
            this.columnHeader9.Width = 407;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Address";
            this.columnHeader11.Width = 69;
            // 
            // buttonOpenRom
            // 
            this.buttonOpenRom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenRom.ImageIndex = 0;
            this.buttonOpenRom.ImageList = this.imageListIcons;
            this.buttonOpenRom.Location = new System.Drawing.Point(661, 14);
            this.buttonOpenRom.Name = "buttonOpenRom";
            this.buttonOpenRom.Size = new System.Drawing.Size(39, 23);
            this.buttonOpenRom.TabIndex = 2;
            this.buttonOpenRom.Click += new System.EventHandler(this.buttonOpenRom_Click);
            // 
            // textBoxRomPath
            // 
            this.textBoxRomPath.Location = new System.Drawing.Point(104, 14);
            this.textBoxRomPath.Name = "textBoxRomPath";
            this.textBoxRomPath.ReadOnly = true;
            this.textBoxRomPath.Size = new System.Drawing.Size(557, 22);
            this.textBoxRomPath.TabIndex = 1;
            // 
            // labelRom
            // 
            this.labelRom.Location = new System.Drawing.Point(1, 14);
            this.labelRom.Name = "labelRom";
            this.labelRom.Size = new System.Drawing.Size(96, 23);
            this.labelRom.TabIndex = 0;
            this.labelRom.Text = "ROM File:";
            this.labelRom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(0, 500);
            this.statusBar.Name = "statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel});
            this.statusBar.ShowPanels = true;
            this.statusBar.Size = new System.Drawing.Size(730, 18);
            this.statusBar.SizingGrip = false;
            this.statusBar.TabIndex = 2;
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel.Name = "statusBarPanel";
            this.statusBarPanel.Text = "Please open a ROM file to start";
            this.statusBarPanel.ToolTipText = "Please open a ROM file to start";
            this.statusBarPanel.Width = 730;
            // 
            // openFileRomDialog
            // 
            this.openFileRomDialog.DefaultExt = "bin";
            this.openFileRomDialog.Filter = "Phantasy Star III ROM Images (*.bin)|*.bin";
            this.openFileRomDialog.Title = "Open Phantasy Star III ROM";
            // 
            // listBoxTileSets
            // 
            this.listBoxTileSets.FormattingEnabled = true;
            this.listBoxTileSets.ItemHeight = 16;
            this.listBoxTileSets.Location = new System.Drawing.Point(18, 48);
            this.listBoxTileSets.Name = "listBoxTileSets";
            this.listBoxTileSets.Size = new System.Drawing.Size(159, 260);
            this.listBoxTileSets.TabIndex = 0;
            this.listBoxTileSets.SelectedIndexChanged += new System.EventHandler(this.listBoxTileSets_SelectedIndexChanged);
            // 
            // labelSelectTileset
            // 
            this.labelSelectTileset.AutoSize = true;
            this.labelSelectTileset.Location = new System.Drawing.Point(15, 19);
            this.labelSelectTileset.Name = "labelSelectTileset";
            this.labelSelectTileset.Size = new System.Drawing.Size(93, 17);
            this.labelSelectTileset.TabIndex = 1;
            this.labelSelectTileset.Text = "Select Tileset";
            // 
            // buttonLaunchTileEditor
            // 
            this.buttonLaunchTileEditor.Enabled = false;
            this.buttonLaunchTileEditor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLaunchTileEditor.Image = ((System.Drawing.Image)(resources.GetObject("buttonLaunchTileEditor.Image")));
            this.buttonLaunchTileEditor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLaunchTileEditor.Location = new System.Drawing.Point(18, 314);
            this.buttonLaunchTileEditor.Name = "buttonLaunchTileEditor";
            this.buttonLaunchTileEditor.Size = new System.Drawing.Size(159, 37);
            this.buttonLaunchTileEditor.TabIndex = 1;
            this.buttonLaunchTileEditor.Text = "Launch Tile Editor";
            this.buttonLaunchTileEditor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLaunchTileEditor.Click += new System.EventHandler(this.buttonLaunchTileEditor_Click);
            // 
            // textBoxTilesetStartAddress
            // 
            this.textBoxTilesetStartAddress.Location = new System.Drawing.Point(315, 48);
            this.textBoxTilesetStartAddress.Name = "textBoxTilesetStartAddress";
            this.textBoxTilesetStartAddress.ReadOnly = true;
            this.textBoxTilesetStartAddress.Size = new System.Drawing.Size(156, 22);
            this.textBoxTilesetStartAddress.TabIndex = 2;
            // 
            // labelTilesetStartAddress
            // 
            this.labelTilesetStartAddress.Location = new System.Drawing.Point(184, 48);
            this.labelTilesetStartAddress.Name = "labelTilesetStartAddress";
            this.labelTilesetStartAddress.Size = new System.Drawing.Size(125, 25);
            this.labelTilesetStartAddress.TabIndex = 40;
            this.labelTilesetStartAddress.Text = "Start Address:";
            this.labelTilesetStartAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTilesetEndAddress
            // 
            this.textBoxTilesetEndAddress.Location = new System.Drawing.Point(315, 76);
            this.textBoxTilesetEndAddress.Name = "textBoxTilesetEndAddress";
            this.textBoxTilesetEndAddress.ReadOnly = true;
            this.textBoxTilesetEndAddress.Size = new System.Drawing.Size(156, 22);
            this.textBoxTilesetEndAddress.TabIndex = 3;
            // 
            // labelTilesetEndAddress
            // 
            this.labelTilesetEndAddress.Location = new System.Drawing.Point(184, 76);
            this.labelTilesetEndAddress.Name = "labelTilesetEndAddress";
            this.labelTilesetEndAddress.Size = new System.Drawing.Size(125, 25);
            this.labelTilesetEndAddress.TabIndex = 42;
            this.labelTilesetEndAddress.Text = "End Address:";
            this.labelTilesetEndAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTilesetColumns
            // 
            this.textBoxTilesetColumns.Location = new System.Drawing.Point(315, 104);
            this.textBoxTilesetColumns.Name = "textBoxTilesetColumns";
            this.textBoxTilesetColumns.ReadOnly = true;
            this.textBoxTilesetColumns.Size = new System.Drawing.Size(156, 22);
            this.textBoxTilesetColumns.TabIndex = 4;
            // 
            // labelTilesetColumns
            // 
            this.labelTilesetColumns.Location = new System.Drawing.Point(184, 104);
            this.labelTilesetColumns.Name = "labelTilesetColumns";
            this.labelTilesetColumns.Size = new System.Drawing.Size(125, 25);
            this.labelTilesetColumns.TabIndex = 44;
            this.labelTilesetColumns.Text = "Editor Columns:";
            this.labelTilesetColumns.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTilesetRows
            // 
            this.textBoxTilesetRows.Location = new System.Drawing.Point(315, 132);
            this.textBoxTilesetRows.Name = "textBoxTilesetRows";
            this.textBoxTilesetRows.ReadOnly = true;
            this.textBoxTilesetRows.Size = new System.Drawing.Size(156, 22);
            this.textBoxTilesetRows.TabIndex = 5;
            // 
            // labelTilesetRows
            // 
            this.labelTilesetRows.Location = new System.Drawing.Point(184, 132);
            this.labelTilesetRows.Name = "labelTilesetRows";
            this.labelTilesetRows.Size = new System.Drawing.Size(125, 25);
            this.labelTilesetRows.TabIndex = 46;
            this.labelTilesetRows.Text = "Editor Rows:";
            this.labelTilesetRows.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTilesetWidth
            // 
            this.textBoxTilesetWidth.Location = new System.Drawing.Point(315, 160);
            this.textBoxTilesetWidth.Name = "textBoxTilesetWidth";
            this.textBoxTilesetWidth.ReadOnly = true;
            this.textBoxTilesetWidth.Size = new System.Drawing.Size(156, 22);
            this.textBoxTilesetWidth.TabIndex = 6;
            // 
            // labelTilesetWidth
            // 
            this.labelTilesetWidth.Location = new System.Drawing.Point(184, 160);
            this.labelTilesetWidth.Name = "labelTilesetWidth";
            this.labelTilesetWidth.Size = new System.Drawing.Size(125, 25);
            this.labelTilesetWidth.TabIndex = 48;
            this.labelTilesetWidth.Text = "Tile Width:";
            this.labelTilesetWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxTilesetHeight
            // 
            this.textBoxTilesetHeight.Location = new System.Drawing.Point(315, 188);
            this.textBoxTilesetHeight.Name = "textBoxTilesetHeight";
            this.textBoxTilesetHeight.ReadOnly = true;
            this.textBoxTilesetHeight.Size = new System.Drawing.Size(156, 22);
            this.textBoxTilesetHeight.TabIndex = 7;
            // 
            // labelTilesetHeight
            // 
            this.labelTilesetHeight.Location = new System.Drawing.Point(184, 188);
            this.labelTilesetHeight.Name = "labelTilesetHeight";
            this.labelTilesetHeight.Size = new System.Drawing.Size(125, 25);
            this.labelTilesetHeight.TabIndex = 50;
            this.labelTilesetHeight.Text = "Tile Height:";
            this.labelTilesetHeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(730, 518);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.tabControlMainContent);
            this.Controls.Add(this.textBoxRomPath);
            this.Controls.Add(this.labelRom);
            this.Controls.Add(this.buttonOpenRom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Menu = this.mainMenu;
            this.Name = "MainForm";
            this.Text = "Aridia";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControlMainContent.ResumeLayout(false);
            this.tabPageMain.ResumeLayout(false);
            this.tabPageMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarning)).EndInit();
            this.tabPageCharacters.ResumeLayout(false);
            this.panelCharacter.ResumeLayout(false);
            this.panelCharacter.PerformLayout();
            this.tabPageDialogText.ResumeLayout(false);
            this.tabPageEnemies.ResumeLayout(false);
            this.panelEnemy.ResumeLayout(false);
            this.panelEnemy.PerformLayout();
            this.tabPageEnemyGroups.ResumeLayout(false);
            this.panelEnemyGroups.ResumeLayout(false);
            this.panelEnemyGroups.PerformLayout();
            this.tabPageGraphics.ResumeLayout(false);
            this.tabPageGraphics.PerformLayout();
            this.tabPageItems.ResumeLayout(false);
            this.panelItem.ResumeLayout(false);
            this.panelItem.PerformLayout();
            this.tabPageLevelTables.ResumeLayout(false);
            this.groupBoxStatisticGrowth.ResumeLayout(false);
            this.tabPageNPCs.ResumeLayout(false);
            this.panelNPCs.ResumeLayout(false);
            this.panelNPCs.PerformLayout();
            this.tabPagePalettes.ResumeLayout(false);
            this.tabPagePalettes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPalettePreview)).EndInit();
            this.tabPageScript.ResumeLayout(false);
            this.tabPageScript.PerformLayout();
            this.tabPageScriptedEvents.ResumeLayout(false);
            this.tabPageScriptedEvents.PerformLayout();
            this.panelScriptedEvents.ResumeLayout(false);
            this.panelScriptedEvents.PerformLayout();
            this.tabPageShops.ResumeLayout(false);
            this.panelShop.ResumeLayout(false);
            this.panelShop.PerformLayout();
            this.tabPageTreasure.ResumeLayout(false);
            this.tabPageTreasure.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new MainForm());
		}

        private void MainForm_Load(object sender,EventArgs e)
        {
            Version v=Assembly.GetEntryAssembly().GetName().Version;
            String major=v.Major.ToString();
            String minor=v.Minor.ToString();
            this.Text="Aridia "+major+"."+minor;
        }

        private void errorHandler(String action,Exception x)
		{
			ErrorDialog errorDialog=new ErrorDialog(action,x);
			this.Cursor=Cursors.Default;
			errorDialog.ShowDialog(this);
			if(errorDialog.EndApplication)
			{
				this.Close();
			}
		}

		private void validationFailed(MDString mdString)
		{
			MessageBox.Show(this,"The maximum length for this string is "+mdString.NumBytes+".\n\nIt can only contain alphanumeric characters and punctuation.","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
		}

		private void validationFailed(MDInteger mdInt)
		{
			double maxValue=(double)mdInt.MaxValue;
			if(maxValue==0)
			{
				maxValue=Math.Pow((double)2.0,(double)(8*mdInt.NumBytes));
			}
			MessageBox.Show(this,"The value must be numeric, greater than zero, and less than "+((int)maxValue)+".","Validation Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
		}

		private void openRom()
		{
			try
			{
				System.Windows.Forms.DialogResult result=this.openFileRomDialog.ShowDialog(this);
				if(result.Equals(System.Windows.Forms.DialogResult.OK))
				{
                    this.Cursor=Cursors.WaitCursor;
                    //if there's already a file open check if we need to fix the checksum
                    if(this.romIO!=null){
                        if(this.checkBoxAutoFixChecksum.Checked==true)
                        {
                            this.romIO.writeChecksum(this.romIO.generateChecksum());
                        }
                    }
                    String fileName=this.openFileRomDialog.FileName;
					this.statusBarPanel.Text="Opening "+fileName+"..";
					//update romIO
					if(this.romIO!=null){ this.romIO.Dispose(); }
					this.romIO=new MDBinaryRomIO(fileName);
					String romHeader=this.romIO.readString(256,138,AridiaUtils.STRING_TERMINATOR);
                    //check if this is a supported rom header
                    if(!romHeader.Equals(GOOD_HEADER)) 
                    {
                        System.Windows.Forms.MessageBox.Show(this,"The selected ROM does not match a supported Phantasy Star III image. Bad stuff could happen if you edit it in Aridia.\n\nSelected ROM header:\n"+romHeader+"\n\nSupported header:\n"+GOOD_HEADER,"Warning",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Warning);
                    }
					//update UI
					this.textBoxRomPath.Text=fileName;
					this.statusBarPanel.Text="Opened "+fileName;
					this.textBoxRomHeader.Text=romHeader;
					this.tabControlMainContent.Enabled=true;
					this.listViewDialogText.Items.Clear();
					this.listViewScript.Items.Clear();
					this.comboBoxItemTechnique.Items.Clear();
					this.comboBoxSelectItem.Items.Clear();
					this.comboBoxCharacterTechniques.Items.Clear();
					this.comboBoxCharacterType.Items.Clear();
					this.comboBoxSelectCharacter.Items.Clear();
					this.listViewCharacterItems.Items.Clear();
					this.tabControlMainContent_SelectedIndexChanged(null,null);
					this.menuItemChecksum.Enabled=true;
					this.textBoxFindScript.Enabled=true;
					this.textBoxPaletteFind.Enabled=true;
                    this.buttonCreateIPS.Enabled=true;
                    this.menuItemCreateIPSFile.Enabled=true;
					//checksum stuff
					int storedChecksum=this.romIO.readInteger(398,2);
					this.textBoxChecksum.Text=storedChecksum.ToString()+" (0x"+storedChecksum.ToString("X")+")";
					int calculatedChecksum=this.romIO.generateChecksum();
					this.textBoxCalculatedChecksum.Text=calculatedChecksum.ToString()+" (0x"+calculatedChecksum.ToString("X")+")";
					if(storedChecksum!=calculatedChecksum)
					{
						this.buttonChecksum.Enabled=true;
					}
					else
					{
						this.buttonChecksum.Enabled=false;
					}
				}
			}
			catch(Exception x)
			{
				this.errorHandler("open a rom",x);
				this.statusBarPanel.Text="Error opening rom: "+x.Message;
			}
			this.Cursor=Cursors.Default;
		}

		private void generateChecksum()
		{
			this.Cursor=Cursors.WaitCursor;
			this.statusBarPanel.Text="Regenerating checksum..";
			try
			{
				int newChecksum=this.romIO.generateChecksum();
				string newHexChecksum=" (0x"+newChecksum.ToString("X")+")";
				//write new value
				MDInteger mdInt=new MDInteger();
				mdInt.CurrentValue=newChecksum;
				mdInt.NumBytes=2;
				mdInt.Address=398;
				this.romIO.writeInt(mdInt);
				//update UI
				this.textBoxChecksum.Text=this.textBoxCalculatedChecksum.Text=newChecksum.ToString()+newHexChecksum;
				this.buttonChecksum.Enabled=false;
				this.statusBarPanel.Text="New checksum value ["+newChecksum+"] written to rom image";
			}
			catch(Exception x)
			{
				this.errorHandler("generate the checksum",x);
				this.statusBarPanel.Text="Error generating checksum: "+x.Message;
			}
			this.Cursor=Cursors.Default;
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuItemHomepage_Click(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				System.Diagnostics.Process.Start(@"http://www.huguesjohnson.com/aridia/index.html");		
			}
			catch(Exception x)
			{
				this.errorHandler("going to the Aridia homepage",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(this,"Aridia - Phantasy Star III ROM Editor\n\n(c) 2007-2015 Hugues Johnson\nhttp://www.huguesjohnson.com/\n\nChecksum code based off Calculate_Checksum method in Gens\nhttp://sourceforge.net/projects/gens/","About Aridia",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
		}

		private void menuItemOpen_Click(object sender, System.EventArgs e)
		{
			this.openRom();
		}

		private void buttonOpenRom_Click(object sender, System.EventArgs e)
		{
			this.openRom();
		}

		private void menuItemChecksum_Click(object sender, System.EventArgs e)
		{
			this.generateChecksum();
		}

		private void buttonChecksum_Click(object sender, System.EventArgs e)
		{
			this.generateChecksum();
		}

		private void listViewDialogText_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			AridiaUtils.sortListView(e,this.listViewDialogText);
		}

		private void tabControlMainContent_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			string newTabName=this.tabControlMainContent.SelectedTab.Text;
			//move to the selected tab - loading data if needed
			if(this.romIO!=null)
			{
				try
				{
					switch(newTabName)
					{
						case "Main":
							int calculatedChecksum=this.romIO.generateChecksum();
							this.textBoxCalculatedChecksum.Text=calculatedChecksum.ToString()+" (0x"+calculatedChecksum.ToString("X")+")";
							if(this.textBoxCalculatedChecksum.Text.Equals(this.textBoxChecksum.Text))
							{
								this.buttonChecksum.Enabled=false;
							}
							else
							{
								this.buttonChecksum.Enabled=true;
							}
							break;
						case "Script":
                            if(this.scriptDirtyFlag) 
                            { 
                                this.listViewScript.Items.Clear();
                                this.scriptDirtyFlag=false;
                            }
							if(this.listViewScript.Items.Count<1)
							{
								this.listViewScript.Items.AddRange(AridiaUtils.getScriptItems(107326,107857,this.romIO));
								this.listViewScript.Items.AddRange(AridiaUtils.getScriptItems(155406,201013,this.romIO));
							}
							break;
						case "Dialog Text":
							if(this.listViewDialogText.Items.Count<1)
							{
								this.listViewDialogText.Items.AddRange(AridiaUtils.getDialogListItems("Techniques",this.romIO));
								this.listViewDialogText.Items.AddRange(AridiaUtils.getDialogListItems("Marriage",this.romIO));
								this.listViewDialogText.Items.AddRange(AridiaUtils.getDialogListItems("Character-Menu",this.romIO));
								this.listViewDialogText.Items.AddRange(AridiaUtils.getDialogListItems("Battle-Messages",this.romIO));
								this.listViewDialogText.Items.AddRange(AridiaUtils.getDialogListItems("Shops",this.romIO));
								this.listViewDialogText.Items.AddRange(AridiaUtils.getDialogListItems("Save-Load",this.romIO));
								this.listViewDialogText.Items.AddRange(AridiaUtils.getDialogListItems("Title-Screen",this.romIO));
							}
							break;
						case "Characters":
							if(this.comboBoxSelectCharacter.Items.Count<1)
							{
								AridiaUtils.loadLookupValues(this.comboBoxSelectCharacter,"Character-Addresses");
								AridiaUtils.loadLookupValues(this.comboBoxCharacterType,"Character-Types");
								AridiaUtils.loadLookupValues(this.comboBoxCharacterTechniques,"Character-Techniques");
							}
							break;
						case "Enemies":
							if(this.comboBoxSelectEnemy.Items.Count<1)
							{
								AridiaUtils.loadLookupValues(this.comboBoxSelectEnemy,"Enemy-Addresses");
								AridiaUtils.loadLookupValues(this.comboBoxEnemyAnimation,"Enemy-Animations");
								AridiaUtils.loadLookupValues(this.comboBoxEnemySpriteGroup,"Enemy-Sprite-Group");
								AridiaUtils.loadLookupValues(this.comboBoxEnemyTechnique,"Enemy-Techniques");
							}
							break;
						case "Items":
							if(this.comboBoxSelectItem.Items.Count<1)
							{
								AridiaUtils.loadLookupValues(this.comboBoxSelectItem,"Item-Addresses");
								AridiaUtils.loadLookupValues(this.comboBoxItemTechnique,"Item-Techniques");
                                AridiaUtils.loadLookupValues(this.comboBoxItemAnimation,"Weapon-Animations");
                                AridiaUtils.loadLookupValues(this.comboBoxItemEquipWhere,"Item-EquipWhere");
                            }
							break;
						case "Shops":
							if(this.comboBoxSelectShop.Items.Count<1)
							{
								AridiaUtils.loadLookupValues(this.comboBoxSelectShop,"Shop-Addresses");
								AridiaUtils.loadLookupValues(this.comboBoxItem1,"Item-Codes");
								AridiaUtils.loadLookupValues(this.comboBoxItem2,"Item-Codes");
								AridiaUtils.loadLookupValues(this.comboBoxItem3,"Item-Codes");
								AridiaUtils.loadLookupValues(this.comboBoxItem4,"Item-Codes");
								AridiaUtils.loadLookupValues(this.comboBoxItem5,"Item-Codes");
								this.comboBoxItem1.Items.Add(new LookupValue("Empty",255));
								this.comboBoxItem2.Items.Add(new LookupValue("Empty",255));
								this.comboBoxItem3.Items.Add(new LookupValue("Empty",255));
								this.comboBoxItem4.Items.Add(new LookupValue("Empty",255));
								this.comboBoxItem5.Items.Add(new LookupValue("Empty",255));
							}
							break;
						case "Graphics":
                            if(this.listBoxTileSets.Items.Count<1)
                            {
                                TileSetDefinition[] defs=AridiaUtils.TileSetDefinitions.getAll();
                                this.listBoxTileSets.Items.AddRange(defs);
                            }
							break;
						case "Palettes":
							if(this.listViewPalettes.Items.Count<1)
							{
								AridiaUtils.loadLookupValues(this.listViewPalettes,"Palette-Addresses");
								ColumnClickEventArgs args=new ColumnClickEventArgs(0);
								AridiaUtils.sortListView(args,this.listViewPalettes);
							}
							break;
                        case "Treasure Chests":
                            if(this.listViewTreasure.Items.Count<1)
                            {
                                AridiaUtils.loadLookupValues(this.listViewTreasure,"Treasure-Addresses");
                                ColumnClickEventArgs args=new ColumnClickEventArgs(0);
                                AridiaUtils.sortListView(args,this.listViewTreasure);
                                AridiaUtils.loadLookupValues(this.comboBoxTreasureItem,"Item-Codes");
                            }
                            break;
                        case "Level Tables":
							if(this.comboBoxLevelTable.Items.Count<1)
							{
								AridiaUtils.loadLookupValues(this.comboBoxLevelTable,"Level-Table-Addresses");
								AridiaUtils.loadStatGrowthValues(this.comboBoxLevelTableDamage);
								AridiaUtils.loadStatGrowthValues(this.comboBoxLevelTableDefense);
								AridiaUtils.loadStatGrowthValues(this.comboBoxLevelTableHP);
								AridiaUtils.loadStatGrowthValues(this.comboBoxLevelTableLuck);
								AridiaUtils.loadStatGrowthValues(this.comboBoxLevelTableSkill);
								AridiaUtils.loadStatGrowthValues(this.comboBoxLevelTableSpeed);
								AridiaUtils.loadStatGrowthValues(this.comboBoxLevelTableTP);
							}
							break;
                        case "Enemy Groups":
                            if(this.listBoxSelectEnemyGroup.Items.Count<1)
                            { 
                                int address=Constants.EnemyGroupStartAddress;
                                for(int groupIndex=0;groupIndex<Constants.EnemyGroupCount;groupIndex++)
                                {
                                    LookupValue lv=new LookupValue("["+groupIndex.ToString()+"]",address);
                                    this.listBoxSelectEnemyGroup.Items.Add(lv);
                                    address+=4;
                                }
								AridiaUtils.loadLookupValues(this.comboBoxFrontRowEnemy,"Enemy-IDs-FrontRow");
								AridiaUtils.loadLookupValues(this.comboBoxBackRowEnemy,"Enemy-IDs-BackRow");
                                //previous experimental method - save this code in case it's brought back
								//AridiaUtils.loadLookupValues(this.comboBoxEnemyGroupsSelectGeneration,"EnemyGroup-Generations");
                            }
                            break;
                        case "NPCs":
                            if(this.comboBoxNPCsSelectLocation.Items.Count<1)
                            { 
								AridiaUtils.loadLookupValues(this.comboBoxNPCsSelectLocation,"NPC-Addresses");
                                this.npcSprites=AridiaUtils.getLookupValueCollection("NPC-Sprites");
                            }
                            break;
                        case "Scripted Events":
                            if(this.comboBoxSelectScriptedEvent.Items.Count<1)
                            { 
								AridiaUtils.loadLookupValues(this.comboBoxSelectScriptedEvent,"ScriptedEvent-Addresses");
								AridiaUtils.loadLookupValues(this.comboBoxScriptedEventDirection,"ButtonMask-Codes");
                                for(int dialogAddressOffset=0;dialogAddressOffset<16;dialogAddressOffset++) 
                                { 
                                    this.comboBoxScriptedEventDialogOffset.Items.Add(dialogAddressOffset);
                                }
                            }
                            break;
					}
				} 
				catch(Exception x)
				{
					this.errorHandler("changing to tab "+newTabName,x);
				}
			}
			this.Cursor=Cursors.Default;
		}

		private void listViewDialogText_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.listViewDialogText.SelectedItems.Count==1)
			{
				this.listViewDialogText.SelectedItems[0].BeginEdit();
			}
		}

		private void listViewDialogText_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			if(e.Label!=null)
			{
				this.Cursor=Cursors.WaitCursor;
				try
				{
					ListViewItem selectedItem=this.listViewDialogText.SelectedItems[0];
					MDString mdString=new MDString();
					mdString.CurrentValue=e.Label;
					mdString.Address=int.Parse(selectedItem.SubItems[3].Text);
					mdString.NumBytes=int.Parse(selectedItem.SubItems[4].Text);
					if(AridiaUtils.validateMDString(mdString))
					{
                        this.romIO.writeString(mdString,AridiaUtils.STRING_TERMINATOR);
						this.statusBarPanel.Text="Wrote "+mdString.CurrentValue+" to address "+mdString.Address.ToString();
					}
					else
					{
						this.validationFailed(mdString);
						e.CancelEdit=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save a dialog value",x);
				}
				this.Cursor=Cursors.Default;
			}
		}

		private void listViewDialogText_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(this.listViewDialogText.SelectedItems.Count==1)
			{
				this.listViewDialogText.SelectedItems[0].BeginEdit();
			}
		}

		private void comboBoxSelectItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectItem.SelectedItem;
				int address=selectedItem.IntValue;
				//starting address
				this.textBoxItemAddress.Text=address.ToString();
				//item cost
				int cost=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Cost,2);
				this.textBoxItemCost.Text=cost.ToString();
                //attack
                int attack=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Attack,1);
                this.textBoxItemAttack.Text=attack.ToString();
                //defense
                int defense=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Defense,1);
                this.textBoxItemDefense.Text=defense.ToString();
                //speed
                int speed=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Speed,1);
                this.textBoxItemSpeed.Text=speed.ToString();
				//item technique
				int technique=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Technique,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxItemTechnique,technique);
				//item effectiveness
				int effectiveness=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Effectiveness,1);
				this.textBoxItemEffectiveness.Text=effectiveness.ToString();
                //animation
                int animation=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Animation,2);
                AridiaUtils.setComboBoxSelection(this.comboBoxItemAnimation,animation);
                //equip by
                int equipby=this.romIO.readInteger(address+(int)Constants.ItemOffsets.EquipBy,1);
                this.checkBoxEquipRhysNial.Checked=((equipby&(int)Constants.CharacterIdMask.RhysNial)==(int)Constants.CharacterIdMask.RhysNial);
                this.checkBoxEquipAynSeanCrysAdanAron.Checked=((equipby&(int)Constants.CharacterIdMask.AynSeanCrysAdanAron)==(int)Constants.CharacterIdMask.AynSeanCrysAdanAron);
                this.checkBoxEquipMieu.Checked=((equipby&(int)Constants.CharacterIdMask.Mieu)==(int)Constants.CharacterIdMask.Mieu);
                this.checkBoxEquipWren.Checked=((equipby&(int)Constants.CharacterIdMask.Wren)==(int)Constants.CharacterIdMask.Wren);
                this.checkBoxEquipTheaKara.Checked=((equipby&(int)Constants.CharacterIdMask.TheaKara)==(int)Constants.CharacterIdMask.TheaKara);
                this.checkBoxEquipLyleRyan.Checked=((equipby&(int)Constants.CharacterIdMask.LyleRyan)==(int)Constants.CharacterIdMask.LyleRyan);
                this.checkBoxEquipLenaSari.Checked=((equipby&(int)Constants.CharacterIdMask.LenaSari)==(int)Constants.CharacterIdMask.LenaSari);
                this.checkBoxEquipLayaGwyn.Checked=((equipby&(int)Constants.CharacterIdMask.LayaGwyn)==(int)Constants.CharacterIdMask.LayaGwyn);
                //equip where
                int equipWhere=this.romIO.readInteger(address+(int)Constants.ItemOffsets.EquipWhere,1);
                AridiaUtils.setComboBoxSelection(this.comboBoxItemEquipWhere,equipWhere);
                //item flags
                int itemFlags=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Flags,1);
                this.checkBoxItemFlag0.Checked=((itemFlags&16)==16);
                this.checkBoxItemFlag1.Checked=((itemFlags&32)==32);
                this.checkBoxItemFlag2.Checked=((itemFlags&64)==64);
                this.checkBoxItemFlag3.Checked=((itemFlags&128)==128);
                //name
                int nameAddress=this.romIO.readInteger(address+(int)Constants.ItemOffsets.NameAddress,2)+Constants.ItemNameStartAddress;
                this.textBoxItemNameAddress.Text=nameAddress.ToString();
                string itemText=romIO.readString(nameAddress,65535,AridiaUtils.STRING_TERMINATOR);
				int itemNameLength=itemText.Length;
                this.textBoxItemNameLength.Text=itemNameLength.ToString();
                this.textBoxItemName.Text=itemText;
                this.textBoxItemName.MaxLength=itemNameLength;
            }
			catch(Exception x)
			{
				this.errorHandler("selecting an item",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxItemCost_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxItemCost.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.Cost;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the cost for an item",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxItemEffectiveness_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxItemEffectiveness.Text;
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.Effectiveness;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the effectiveness for an item",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxItemTechnique_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxItemTechnique.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.Technique;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the technique for an item",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void listViewScript_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			AridiaUtils.sortListView(e,this.listViewScript);
			this.Cursor=Cursors.Default;
		}

		private void listViewScript_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.listViewScript.SelectedItems.Count==1)
			{
                this.listViewScript.SelectedItems[0].BeginEdit();
			}
		}

		private void listViewScript_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			if(e.Label!=null)
			{
				this.Cursor=Cursors.WaitCursor;
				try
				{
					ListViewItem selectedItem=this.listViewScript.SelectedItems[0];
					MDString mdString=new MDString();
					mdString.CurrentValue=e.Label;
					mdString.Address=int.Parse(selectedItem.SubItems[1].Text);
					mdString.NumBytes=int.Parse(selectedItem.SubItems[2].Text);
					if(AridiaUtils.validateMDString(mdString))
					{
                        this.romIO.writeString(mdString,AridiaUtils.STRING_TERMINATOR);
						this.statusBarPanel.Text="Wrote "+mdString.CurrentValue+" to address "+mdString.Address.ToString();
					}
					else
					{
						this.validationFailed(mdString);
						e.CancelEdit=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save a script value",x);
				}
				this.Cursor=Cursors.Default;
			}
		}

		private void listViewScript_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(this.listViewScript.SelectedItems.Count==1)
			{
				this.listViewScript.SelectedItems[0].BeginEdit();
			}
		}

		private void comboBoxSelectCharacter_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectCharacter.SelectedItem;
				int address=selectedItem.IntValue;
				//starting address
				this.textBoxCharacterAddress.Text=address.ToString();
				//name
                string name=this.romIO.readString(address+(int)Constants.CharacterOffsets.Name,4,AridiaUtils.STRING_TERMINATOR);
				this.textBoxCharacterName.Text=name;
				//hit points
				int hitPoints=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.HitPoints,1);
				this.textBoxCharacterHitPoints.Text=hitPoints.ToString();
				//tech points
				int techPoints=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.TechniquePoints,1);
				this.textBoxCharacterTechPoints.Text=techPoints.ToString();
				//attack
				int attack=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.Attack,1);
				this.textBoxCharacterAttack.Text=attack.ToString();
				//defense
				int defense=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.Defense,1);
				this.textBoxCharacterDefense.Text=defense.ToString();
				//luck
				int luck=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.Luck,1);
				this.textBoxCharacterLuck.Text=luck.ToString();
				//skill
				int skill=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.Skill,1);
				this.textBoxCharacterSkill.Text=skill.ToString();
				//speed
				int speed=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.Speed,1);
				this.textBoxCharacterSpeed.Text=speed.ToString();
				//items
				this.listViewCharacterItems.Items.Clear();
				int itemCount=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.ItemCount,1);
				itemCount/=2;
				if(itemCount>0)
				{
					for(int index=(itemCount-1);index>=0;index--)
					{
						int itemAddress=address+(int)Constants.CharacterOffsets.Items+(2*index);
						PS3Item item=AridiaUtils.readItem(itemAddress,this.romIO);
						if(item.isEquipped)
						{
							this.listViewCharacterItems.Items.Add(new ListViewItem(new string[]{item.hexString,item.itemLookup.Description,item.isEquipped.ToString(),item.whereEquipped.Description}));
						}
						else
						{
							this.listViewCharacterItems.Items.Add(new ListViewItem(new string[]{item.hexString,item.itemLookup.Description,item.isEquipped.ToString(),""}));
						}
					}
					this.listViewCharacterItems.Items[0].Selected=true;
				}
				//type
				int type=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.Type,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxCharacterType,type);
				//technique power
				int techniquePower=this.romIO.readInteger(address+(int)Constants.CharacterOffsets.TechniquePower,2);
				AridiaUtils.setComboBoxSelection(this.comboBoxCharacterTechniques,techniquePower);
				//techniques start immediately after the last item
				int techniqueStartAddress=address+(int)Constants.CharacterOffsets.ItemCount+(itemCount*2)+1;
				int melee=this.romIO.readInteger(techniqueStartAddress+((int)Constants.TechniqueGroupOrder.Melee*8),2);
				this.comboBoxCharacterTechniqueMelee.SelectedIndex=AridiaUtils.findTechniquePowerIndex(melee);
				int heal=this.romIO.readInteger(techniqueStartAddress+((int)Constants.TechniqueGroupOrder.Heal*8),2);
				this.comboBoxCharacterTechniqueHeal.SelectedIndex=AridiaUtils.findTechniquePowerIndex(heal);
				int time=this.romIO.readInteger(techniqueStartAddress+((int)Constants.TechniqueGroupOrder.Time*8),2);
				this.comboBoxCharacterTechniqueTime.SelectedIndex=AridiaUtils.findTechniquePowerIndex(time);
				int order=this.romIO.readInteger(techniqueStartAddress+((int)Constants.TechniqueGroupOrder.Order*8),2);
				this.comboBoxCharacterTechniqueOrder.SelectedIndex=AridiaUtils.findTechniquePowerIndex(order);
			}
			catch(Exception x)
			{
				this.errorHandler("selecting a character to edit",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void editItem(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(this.listViewCharacterItems.SelectedItems.Count<1){ return; }
			if(this.comboBoxSelectCharacter.Text.Length<1){ return; }
			try
			{
				//get the selected item
				ListViewItem selectedItem=this.listViewCharacterItems.SelectedItems[0];
				PS3Item item=new PS3Item();
				item.hexString=selectedItem.SubItems[0].Text;
				item.itemLookup=AridiaUtils.ItemCodes.getByDescription(selectedItem.SubItems[1].Text);
				item.isEquipped=bool.Parse(selectedItem.SubItems[2].Text);
				if(item.isEquipped)
				{
					item.whereEquipped=AridiaUtils.EquipCodes.getByDescription(selectedItem.SubItems[3].Text);
				}
				//show the item dialog
				EditCharacterInventoryItem editDialog=new EditCharacterInventoryItem(item);
				editDialog.ShowDialog(this);
				if(!editDialog.Cancel)
				{
					PS3Item newItem=editDialog.getItem();
					selectedItem.SubItems[0].Text=newItem.hexString;
					selectedItem.SubItems[1].Text=newItem.itemLookup.Description;
					selectedItem.SubItems[2].Text=newItem.isEquipped.ToString();
					if(newItem.isEquipped)
					{
						selectedItem.SubItems[3].Text=newItem.whereEquipped.Description;
					}
					else
					{
						selectedItem.SubItems[3].Text="";
					}
					//save the results
					this.Cursor=Cursors.WaitCursor;
					int baseAddress=int.Parse(this.textBoxCharacterAddress.Text);
					AridiaUtils.saveCharacterEquipment(baseAddress,this.listViewCharacterItems,this.romIO);
				}
			}
			catch(Exception x)
			{
				this.errorHandler("edit an inventory item",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void listViewCharacterItems_DoubleClick(object sender, System.EventArgs e)
		{
			this.editItem(sender,null);
		}

		private void comboBoxCharacterType_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxCharacterType.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.Type;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the type for a character",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxCharacterTechniques_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxCharacterTechniques.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.TechniquePower;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the techniques for a character",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxCharacterName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDString mdString=new MDString();
				mdString.CurrentValue=this.textBoxCharacterName.Text;
				mdString.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.Name;
				mdString.NumBytes=4;
				if(AridiaUtils.validateMDString(mdString))
				{
                    this.romIO.writeString(mdString,AridiaUtils.STRING_TERMINATOR);
					this.statusBarPanel.Text="Wrote "+mdString.CurrentValue+" to address "+mdString.Address.ToString();
				}
				else
				{
					this.validationFailed(mdString);
					e.Cancel=true;
				}
			}
			catch(Exception x)
			{
				this.errorHandler("save the name for a character",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxCharacterHitPoints_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxCharacterHitPoints.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.HitPoints;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the hit points for a character",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxCharacterTechPoints_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxCharacterTechPoints.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.TechniquePoints;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the technique points for a character",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxCharacterAttack_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxCharacterAttack.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.Attack;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the attack for a character",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxCharacterDefense_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxCharacterDefense.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.Defense;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the defense for a character",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxCharacterLuck_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxCharacterLuck.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.Luck;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the luck for a character",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxCharacterSkill_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxCharacterSkill.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.Skill;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the skill for a character",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxCharacterSpeed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxCharacterSpeed.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxCharacterAddress.Text))+(int)Constants.CharacterOffsets.Speed;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the speed for a character",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxSelectEnemy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectEnemy.SelectedItem;
				int address=selectedItem.IntValue;
				//starting address
				this.textBoxEnemyAddress.Text=address.ToString();
				//sprite group
				int spriteGroup=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.SpriteGroup,2);
				AridiaUtils.setComboBoxSelection(this.comboBoxEnemySpriteGroup,spriteGroup);
				//animation
				int animation=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.Animation,2);
				AridiaUtils.setComboBoxSelection(this.comboBoxEnemyAnimation,animation);
				//technique
				int technique=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.Technique,2);
				AridiaUtils.setComboBoxSelection(this.comboBoxEnemyTechnique,technique);
				//hit points
				int hitPoints=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.HitPoints,2);
				this.textBoxEnemyHitPoints.Text=hitPoints.ToString();
				//attack
				int attack=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.Attack,2);
				this.textBoxEnemyAttack.Text=attack.ToString();
				//defense
				int defense=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.Defense,2);
				this.textBoxEnemyDefense.Text=defense.ToString();
				//speed
				int speed=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.Speed,1);
				this.textBoxEnemySpeed.Text=speed.ToString();
				//experience
				int experience=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.Experience,2);
				this.textBoxEnemyExperience.Text=experience.ToString();
				//meseta
				int meseta=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.Meseta,2);
				this.textBoxEnemyMeseta.Text=meseta.ToString();
				//TechniqueLevel
				int techniqueLevel=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.TechniqueLevel,1);
				this.textBoxEnemyTechniqueLevel.Text=techniqueLevel.ToString();
				//TechniqueCastPercent
				int techniqueCastPercent=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.TechniqueCastPercent,1);
				techniqueCastPercent=AridiaUtils.percentFromRomToDecimal(techniqueCastPercent);
				this.textBoxEnemyTechniqueCastPercent.Text=techniqueCastPercent.ToString();
				//EscapePercent
				int escapePercent=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.EscapePercent,1);
				escapePercent=AridiaUtils.percentFromRomToDecimal(escapePercent);
				this.textBoxEnemyEscapePercent.Text=escapePercent.ToString();
				//name
				int nameOffset=this.romIO.readInteger(address+(int)Constants.EnemyOffsets.NameLookup,2);
				int nameLookup=Constants.EnemyNameOffset+nameOffset;
				int nameLength=-1;
				int selectedIndex=this.comboBoxSelectEnemy.SelectedIndex;
				if(selectedIndex<(this.comboBoxSelectEnemy.Items.Count-1))
				{
					LookupValue nextItem=(LookupValue)this.comboBoxSelectEnemy.Items[selectedIndex+1];
					int nextAddress=nextItem.IntValue;
					int nextOffset=this.romIO.readInteger(nextAddress+(int)Constants.EnemyOffsets.NameLookup,2);
					nameLength=nextOffset-nameOffset-1;
				}
				this.textBoxEnemyNameOffset.Text=nameLookup.ToString();
                String currentName=this.romIO.readString(nameLookup,256,AridiaUtils.STRING_TERMINATOR);
				if(nameLength>0)
				{
					this.textBoxEnemyName.MaxLength=nameLength;
				}
				else //this will likely result in some bugs (i.e. once you shrink the name of an enemy it can't be expanded)
				{
					this.textBoxEnemyName.MaxLength=currentName.Length;
				}
				this.textBoxEnemyName.Text=currentName;
			}
			catch(Exception x)
			{
				this.errorHandler("selecting an enemy to edit",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxEnemySpriteGroup_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxEnemySpriteGroup.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.SpriteGroup;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the sprite group for an enemy",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxEnemyAnimation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxEnemyAnimation.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.Animation;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the animation for an enemy",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxEnemyTechnique_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxEnemyTechnique.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.Technique;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the technique for an enemy",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemyHitPoints_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemyHitPoints.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.HitPoints;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the hit points for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemyAttack_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemyAttack.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.Attack;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the attack for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemyDefense_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemyDefense.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.Defense;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the defense for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemySpeed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemySpeed.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.Speed;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the speed for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemyExperience_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemyExperience.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.Experience;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the experience for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemyMeseta_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemyMeseta.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.Meseta;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the meseta for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemyName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDString mdString=new MDString();
				mdString.CurrentValue=this.textBoxEnemyName.Text;
				mdString.Address=Convert.ToInt32(this.textBoxEnemyNameOffset.Text);
				mdString.NumBytes=this.textBoxEnemyName.MaxLength;
				if(AridiaUtils.validateMDString(mdString))
				{
                    this.romIO.writeString(mdString,AridiaUtils.STRING_TERMINATOR);
					this.statusBarPanel.Text="Wrote "+mdString.CurrentValue+" to address "+mdString.Address.ToString();
				}
				else
				{
					this.validationFailed(mdString);
					e.Cancel=true;
				}
			}
			catch(Exception x)
			{
				this.errorHandler("save the name for an enemy",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void menuItemThanks_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(this,"Thanks to mr2 and TheKomrade for finding the enemy and character data. This program would not have started without these invaluable contributions.\n\nThanks to Nic Olas for all the testing and feedback.\n\nThanks to Tony Hedstrom for finding the location of the treasure chest data.\n\n'Sega Programming FAQ October 18, 1995, Sixth Edition - Final' by Henry Rieke was consulted for palette editing.\n\nThanks to lorenzo for creating the Phantasy Star III disassembly which has been incredibly valuable for finding new features.\n\nThanks to Alianger for identifying several palettes.","Thanks!",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
		}

		private void textBoxFindScript_TextChanged(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxFindScript.Text;
			if(searchText.Length<1)
			{
				this.listViewScript.Items[this.scriptFindIndex].SubItems[0].ResetStyle();
				this.listViewScript.Items[this.scriptFindIndex].Selected=false;
				this.buttonFindScriptNext.Enabled=false;
				this.buttonFindScriptPrevious.Enabled=false;
				this.textBoxFindScript.BackColor=System.Drawing.SystemColors.Window;
				this.scriptFindIndex=0;
			}
			else
			{
				int newIndex=AridiaUtils.findNextInListView(searchText,0,0,this.listViewScript);
				if((newIndex>=0)&&(newIndex<this.listViewScript.Items.Count))
				{
					this.listViewScript.Items[this.scriptFindIndex].SubItems[0].ResetStyle();
					this.listViewScript.Items[this.scriptFindIndex].Selected=false;
					this.scriptFindIndex=newIndex;				
					this.listViewScript.Items[this.scriptFindIndex].Selected=true;
					this.listViewScript.Items[this.scriptFindIndex].SubItems[0].BackColor=Color.LightBlue;
					this.listViewScript.EnsureVisible(this.scriptFindIndex);
					this.buttonFindScriptNext.Enabled=true;
					this.buttonFindScriptPrevious.Enabled=true;
					this.statusBarPanel.Text="";
					this.textBoxFindScript.BackColor=System.Drawing.SystemColors.Window;
				}
				else
				{
					this.buttonFindScriptNext.Enabled=false;
					this.buttonFindScriptPrevious.Enabled=false;
					this.listViewScript.Items[this.scriptFindIndex].SubItems[0].ResetStyle();
					this.scriptFindIndex=0;
					this.statusBarPanel.Text=searchText+" not found.";
					this.textBoxFindScript.BackColor=Color.Orange;
				}
			}
		}

		private void buttonFindScriptNext_Click(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxFindScript.Text;
			int newIndex=AridiaUtils.findNextInListView(searchText,0,this.scriptFindIndex+1,this.listViewScript);
			if((newIndex>=0)&&(newIndex<this.listViewScript.Items.Count))
			{
				this.listViewScript.Items[this.scriptFindIndex].SubItems[0].ResetStyle();
				this.listViewScript.Items[this.scriptFindIndex].Selected=false;
				this.scriptFindIndex=newIndex;				
				this.listViewScript.Items[this.scriptFindIndex].Selected=true;
				this.listViewScript.Items[this.scriptFindIndex].SubItems[0].BackColor=Color.LightBlue;
				this.listViewScript.EnsureVisible(this.scriptFindIndex);
				this.statusBarPanel.Text="";
			}
			else
			{
				//search from the beginning
				this.textBoxFindScript_TextChanged(null,null);
				this.statusBarPanel.Text="Reached end of list, continued from the top.";
			}
		}

		private void buttonFindScriptPrevious_Click(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxFindScript.Text;
			int newIndex=AridiaUtils.findPreviousInListView(searchText,0,this.scriptFindIndex-1,this.listViewScript);
			if(newIndex<0)
			{
				newIndex=AridiaUtils.findPreviousInListView(searchText,0,this.listViewScript.Items.Count-1,this.listViewScript);
				this.statusBarPanel.Text="Reached end of list, continued from the bottom.";
			}
			else
			{
				this.statusBarPanel.Text="";
			}
			if((newIndex>=0)&&(newIndex<this.listViewScript.Items.Count))
			{
				this.listViewScript.Items[this.scriptFindIndex].SubItems[0].ResetStyle();
				this.listViewScript.Items[this.scriptFindIndex].Selected=false;
				this.scriptFindIndex=newIndex;				
				this.listViewScript.Items[this.scriptFindIndex].Selected=true;
				this.listViewScript.Items[this.scriptFindIndex].SubItems[0].BackColor=Color.LightBlue;
				this.listViewScript.EnsureVisible(this.scriptFindIndex);
			}
		}

		private void buttonLaunchPaletteEditor_Click(object sender, System.EventArgs e)
		{
			if(listViewPalettes.SelectedItems.Count!=1){ return; }
			try
			{
				this.Cursor=Cursors.WaitCursor;
				PaletteEditorForm paletteEditor=new PaletteEditorForm(this.romIO,int.Parse(listViewPalettes.SelectedItems[0].SubItems[1].Text));
				this.Cursor=Cursors.Default;
				paletteEditor.ShowDialog(this);
				//refresh the preview
				this.pictureBoxPalettePreview.Refresh();
			}
			catch(Exception x)
			{
				this.errorHandler("edit a palette",x);
			}
		}

		private void listViewPalettes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(listViewPalettes.SelectedItems.Count==1)
			{
				this.buttonLaunchPaletteEditor.Enabled=true;
				//update the preview
				this.pictureBoxPalettePreview.Refresh();
			}
			else
			{
				this.buttonLaunchPaletteEditor.Enabled=false;
			}
		}

		private void linkLabelItemEditing_Click(object sender, System.EventArgs e)
		{
			ItemEditingLimitations dialog=new ItemEditingLimitations();
			dialog.ShowDialog(this);
		}

		private void contextMenuCharacterMenu_Popup(object sender, System.EventArgs e)
		{
			if(this.listViewCharacterItems.Items.Count<1)
			{
				this.menuItemEditCharacterItem.Enabled=false;
			}
			else if(this.listViewCharacterItems.SelectedItems.Count!=1)
			{
				this.menuItemEditCharacterItem.Enabled=false;
			}
			else
			{
				this.menuItemEditCharacterItem.Enabled=true;
			}
		}

		private void menuItemEditCharacterItem_Click(object sender, System.EventArgs e)
		{
			this.editItem(sender,null);
		}

		private void listViewPalettes_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			AridiaUtils.sortListView(e,this.listViewPalettes);
			this.Cursor=Cursors.Default;
		}

		private void listViewPalettes_DoubleClick(object sender, System.EventArgs e)
		{
			this.buttonLaunchPaletteEditor_Click(sender,null);
		}

		private void pictureBoxPalettePreview_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if(this.listViewPalettes.SelectedItems.Count!=1){ return; }
			Palette previewPalette=this.romIO.readPalette(int.Parse(this.listViewPalettes.SelectedItems[0].SubItems[1].Text));
			int width=pictureBoxPalettePreview.Width/16;
			int height=pictureBoxPalettePreview.Height;
			for(int x=0;x<16;x++)
			{
				int xc=(x*width);
				SolidBrush brush=new SolidBrush(previewPalette.Entries[x].ToRGB()); 
				e.Graphics.FillRectangle(brush,xc,0,width,height);
			}
		}

		private void comboBoxCharacterTechniqueMelee_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				//get the value
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Constants.TechniquePowers[this.comboBoxCharacterTechniqueMelee.SelectedIndex];
				//find the address - techniques start immediately after the last item
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectCharacter.SelectedItem;
				int address=selectedItem.IntValue;
				int meleeStartAddress=address+(int)Constants.CharacterOffsets.ItemCount+(this.listViewCharacterItems.Items.Count*2)+1;
				meleeStartAddress+=((int)Constants.TechniqueGroupOrder.Melee*8);
				//save the technique group
				for(int index=0;index<Constants.TechniqueGroupSize;index++)
				{
					mdInt.Address=meleeStartAddress+(index*2);
					this.romIO.writeInt(mdInt);
				}
				this.statusBarPanel.Text="Wrote four melee values";
			} 
			catch(Exception x)
			{
				this.errorHandler("save the melee value",x);			
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxCharacterTechniqueHeal_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				//get the value
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Constants.TechniquePowers[this.comboBoxCharacterTechniqueHeal.SelectedIndex];
				//find the address - techniques start immediately after the last item
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectCharacter.SelectedItem;
				int address=selectedItem.IntValue;
				int healStartAddress=address+(int)Constants.CharacterOffsets.ItemCount+(this.listViewCharacterItems.Items.Count*2)+1;
				healStartAddress+=((int)Constants.TechniqueGroupOrder.Heal*8);
				//save the technique group
				for(int index=0;index<Constants.TechniqueGroupSize;index++)
				{
					mdInt.Address=healStartAddress+(index*2);
					this.romIO.writeInt(mdInt);
				}
				this.statusBarPanel.Text="Wrote four heal values";
			} 
			catch(Exception x)
			{
				this.errorHandler("save the heal value",x);			
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxCharacterTechniqueTime_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				//get the value
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Constants.TechniquePowers[this.comboBoxCharacterTechniqueTime.SelectedIndex];
				//find the address - techniques start immediately after the last item
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectCharacter.SelectedItem;
				int address=selectedItem.IntValue;
				int timeStartAddress=address+(int)Constants.CharacterOffsets.ItemCount+(this.listViewCharacterItems.Items.Count*2)+1;
				timeStartAddress+=((int)Constants.TechniqueGroupOrder.Time*8);
				//save the technique group
				for(int index=0;index<Constants.TechniqueGroupSize;index++)
				{
					mdInt.Address=timeStartAddress+(index*2);
					this.romIO.writeInt(mdInt);
				}
				this.statusBarPanel.Text="Wrote four time values";
			} 
			catch(Exception x)
			{
				this.errorHandler("save the time value",x);			
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxCharacterTechniqueOrder_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectCharacter.Text.Length<1)||(this.textBoxCharacterAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				//get the value
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Constants.TechniquePowers[this.comboBoxCharacterTechniqueOrder.SelectedIndex];
				//find the address - techniques start immediately after the last item
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectCharacter.SelectedItem;
				int address=selectedItem.IntValue;
				int orderStartAddress=address+(int)Constants.CharacterOffsets.ItemCount+(this.listViewCharacterItems.Items.Count*2)+1;
				orderStartAddress+=((int)Constants.TechniqueGroupOrder.Order*8);
				//save the technique group
				for(int index=0;index<Constants.TechniqueGroupSize;index++)
				{
					mdInt.Address=orderStartAddress+(index*2);
					this.romIO.writeInt(mdInt);
				}
				this.statusBarPanel.Text="Wrote four order values";
			} 
			catch(Exception x)
			{
				this.errorHandler("save the order value",x);			
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxPaletteFind_TextChanged(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxPaletteFind.Text;
			if(searchText.Length<1)
			{
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=false;
				this.buttonPaletteFind.Enabled=false;
				this.buttonPalettePrevious.Enabled=false;
				this.textBoxPaletteFind.BackColor=System.Drawing.SystemColors.Window;
				this.paletteFindIndex=0;
			}
			else
			{
				int newIndex=AridiaUtils.findNextInListView(searchText,0,0,this.listViewPalettes);
				if((newIndex>=0)&&(newIndex<this.listViewPalettes.Items.Count))
				{
					this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
					this.listViewPalettes.Items[this.paletteFindIndex].Selected=false;
					this.paletteFindIndex=newIndex;				
					this.listViewPalettes.Items[this.paletteFindIndex].Selected=true;
					this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].BackColor=Color.LightBlue;
					this.listViewPalettes.EnsureVisible(this.paletteFindIndex);
					this.buttonPaletteFind.Enabled=true;
					this.buttonPalettePrevious.Enabled=true;
					this.statusBarPanel.Text="";
					this.textBoxPaletteFind.BackColor=System.Drawing.SystemColors.Window;
				}
				else
				{
					this.buttonPaletteFind.Enabled=false;
					this.buttonPalettePrevious.Enabled=false;
					this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
					this.paletteFindIndex=0;
					this.statusBarPanel.Text=searchText+" not found.";
					this.textBoxPaletteFind.BackColor=Color.Orange;
				}
			}
		}

		private void buttonPaletteFind_Click(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxPaletteFind.Text;
			int newIndex=AridiaUtils.findNextInListView(searchText,0,this.paletteFindIndex+1,this.listViewPalettes);
			if((newIndex>=0)&&(newIndex<this.listViewPalettes.Items.Count))
			{
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=false;
				this.paletteFindIndex=newIndex;				
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=true;
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].BackColor=Color.LightBlue;
				this.listViewPalettes.EnsureVisible(this.paletteFindIndex);
				this.statusBarPanel.Text="";
			}
			else
			{
				//search from the beginning
				this.textBoxPaletteFind_TextChanged(null,null);
				this.statusBarPanel.Text="Reached end of list, continued from the top.";
			}
		}

		private void buttonPalettePrevious_Click(object sender, System.EventArgs e)
		{
			//search for the text
			string searchText=this.textBoxPaletteFind.Text;
			int newIndex=AridiaUtils.findPreviousInListView(searchText,0,this.paletteFindIndex-1,this.listViewPalettes);
			if(newIndex<0)
			{
				newIndex=AridiaUtils.findPreviousInListView(searchText,0,this.listViewPalettes.Items.Count-1,this.listViewPalettes);
				this.statusBarPanel.Text="Reached end of list, continued from the bottom.";
			}
			else
			{
				this.statusBarPanel.Text="";
			}
			if((newIndex>=0)&&(newIndex<this.listViewPalettes.Items.Count))
			{
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].ResetStyle();
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=false;
				this.paletteFindIndex=newIndex;				
				this.listViewPalettes.Items[this.paletteFindIndex].Selected=true;
				this.listViewPalettes.Items[this.paletteFindIndex].SubItems[0].BackColor=Color.LightBlue;
				this.listViewPalettes.EnsureVisible(this.paletteFindIndex);
			}
		}

		private void comboBoxSelectShop_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.comboBoxSelectShop.Text.Length<1){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectShop.SelectedItem;
				int address=selectedItem.IntValue;
				//starting address
				this.textBoxShopAddress.Text=address.ToString();
				//item 1
				int itemCode=this.romIO.readInteger(address,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxItem1,itemCode);
				//item 2
				itemCode=this.romIO.readInteger(address+1,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxItem2,itemCode);
				//item 3
				itemCode=this.romIO.readInteger(address+2,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxItem3,itemCode);
				//item 4
				itemCode=this.romIO.readInteger(address+3,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxItem4,itemCode);
				//item 5
				itemCode=this.romIO.readInteger(address+4,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxItem5,itemCode);
			}
			catch(Exception x)
			{
				this.errorHandler("selecting a shop",x);
			}
			this.Cursor=Cursors.Default;		
		}

		private bool saveShopItem(int itemIndex,ComboBox comboBox)
		{
			bool cancel=false;
			if((this.comboBoxSelectShop.Text.Length<1)||(this.textBoxShopAddress.Text.Length<1)){ return(cancel); }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)comboBox.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxShopAddress.Text))+itemIndex;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("save a shop item",x);
				cancel=true;
			}
			this.Cursor=Cursors.Default;
			return(cancel);
		}

		private void comboBoxItem1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel=this.saveShopItem(0,comboBoxItem1);
		}

		private void comboBoxItem2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel=this.saveShopItem(1,comboBoxItem2);
		}

		private void comboBoxItem3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel=this.saveShopItem(2,comboBoxItem3);
		}

		private void comboBoxItem4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel=this.saveShopItem(3,comboBoxItem4);
		}

		private void comboBoxItem5_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel=this.saveShopItem(4,comboBoxItem5);
		}

		private void comboBoxLevelTable_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.comboBoxLevelTable.SelectedItem;
				//set values in the statistic growth combo boxes
				int address=selectedItem.IntValue+((int)Constants.LevelTableOffsets.Damage);
				AridiaUtils.setComboBoxSelection(this.comboBoxLevelTableDamage,this.romIO.readInteger(address,1));
				address=selectedItem.IntValue+((int)Constants.LevelTableOffsets.Defense);
				AridiaUtils.setComboBoxSelection(this.comboBoxLevelTableDefense,this.romIO.readInteger(address,1));
				address=selectedItem.IntValue+((int)Constants.LevelTableOffsets.HP);
				AridiaUtils.setComboBoxSelection(this.comboBoxLevelTableHP,this.romIO.readInteger(address,1));
				address=selectedItem.IntValue+((int)Constants.LevelTableOffsets.Luck);
				AridiaUtils.setComboBoxSelection(this.comboBoxLevelTableLuck,this.romIO.readInteger(address,1));
				address=selectedItem.IntValue+((int)Constants.LevelTableOffsets.Skill);
				AridiaUtils.setComboBoxSelection(this.comboBoxLevelTableSkill,this.romIO.readInteger(address,1));
				address=selectedItem.IntValue+((int)Constants.LevelTableOffsets.Speed);
				AridiaUtils.setComboBoxSelection(this.comboBoxLevelTableSpeed,this.romIO.readInteger(address,1));
				address=selectedItem.IntValue+((int)Constants.LevelTableOffsets.TP);
				AridiaUtils.setComboBoxSelection(this.comboBoxLevelTableTP,this.romIO.readInteger(address,1));
				//load the XP table
				this.listViewLevelTable.Items.Clear();
				this.listViewLevelTable.Items.AddRange(AridiaUtils.getLevelTable(selectedItem.IntValue+((int)Constants.LevelTableOffsets.XPTable),this.romIO));
			}
			catch(Exception x)
			{
				this.errorHandler("selecting a level table",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void listViewLevelTable_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.listViewLevelTable.SelectedItems.Count==1)
			{
				this.listViewLevelTable.SelectedItems[0].BeginEdit();
			}		
		}

		private void listViewLevelTable_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(this.listViewLevelTable.SelectedItems.Count==1)
			{
				this.listViewLevelTable.SelectedItems[0].BeginEdit();
			}		
		}

		private void listViewLevelTable_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			if(e.Label!=null)
			{
				this.Cursor=Cursors.WaitCursor;
				try
				{
					MDInteger mdInt=new MDInteger();
					mdInt.CurrentValue=int.Parse(e.Label);
					ListViewItem selectedItem=this.listViewLevelTable.SelectedItems[0];
					mdInt.Address=int.Parse(selectedItem.SubItems[2].Text);
					mdInt.NumBytes=4;
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.CancelEdit=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save an XP value",x);
					e.CancelEdit=true;
				}
				this.Cursor=Cursors.Default;
			}		
		}

		private void comboBoxLevelTableHP_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxLevelTable.Text.Length<1)||(this.comboBoxLevelTable.SelectedItem==null)){ return; }
			if((this.comboBoxLevelTableHP.Text.Length<1)||(this.comboBoxLevelTableHP.SelectedItem==null)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=((LookupValue)this.comboBoxLevelTable.SelectedItem).IntValue+((int)Constants.LevelTableOffsets.HP);
				mdInt.CurrentValue=((LookupValue)this.comboBoxLevelTableHP.SelectedItem).IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("change the HP growth rate",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxLevelTableTP_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxLevelTable.Text.Length<1)||(this.comboBoxLevelTable.SelectedItem==null)){ return; }
			if((this.comboBoxLevelTableTP.Text.Length<1)||(this.comboBoxLevelTableTP.SelectedItem==null)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=((LookupValue)this.comboBoxLevelTable.SelectedItem).IntValue+((int)Constants.LevelTableOffsets.TP);
				mdInt.CurrentValue=((LookupValue)this.comboBoxLevelTableTP.SelectedItem).IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("change the TP growth rate",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxLevelTableLuck_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxLevelTable.Text.Length<1)||(this.comboBoxLevelTable.SelectedItem==null)){ return; }
			if((this.comboBoxLevelTableLuck.Text.Length<1)||(this.comboBoxLevelTableLuck.SelectedItem==null)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=((LookupValue)this.comboBoxLevelTable.SelectedItem).IntValue+((int)Constants.LevelTableOffsets.Luck);
				mdInt.CurrentValue=((LookupValue)this.comboBoxLevelTableLuck.SelectedItem).IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("change the Luck growth rate",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxLevelTableSkill_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxLevelTable.Text.Length<1)||(this.comboBoxLevelTable.SelectedItem==null)){ return; }
			if((this.comboBoxLevelTableSkill.Text.Length<1)||(this.comboBoxLevelTableSkill.SelectedItem==null)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=((LookupValue)this.comboBoxLevelTable.SelectedItem).IntValue+((int)Constants.LevelTableOffsets.Skill);
				mdInt.CurrentValue=((LookupValue)this.comboBoxLevelTableSkill.SelectedItem).IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("change the Skill growth rate",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxLevelTableDefense_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxLevelTable.Text.Length<1)||(this.comboBoxLevelTable.SelectedItem==null)){ return; }
			if((this.comboBoxLevelTableDefense.Text.Length<1)||(this.comboBoxLevelTableDefense.SelectedItem==null)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=((LookupValue)this.comboBoxLevelTable.SelectedItem).IntValue+((int)Constants.LevelTableOffsets.Defense);
				mdInt.CurrentValue=((LookupValue)this.comboBoxLevelTableDefense.SelectedItem).IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("change the Defense growth rate",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxLevelTableDamage_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxLevelTable.Text.Length<1)||(this.comboBoxLevelTable.SelectedItem==null)){ return; }
			if((this.comboBoxLevelTableDamage.Text.Length<1)||(this.comboBoxLevelTableDamage.SelectedItem==null)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=((LookupValue)this.comboBoxLevelTable.SelectedItem).IntValue+((int)Constants.LevelTableOffsets.Damage);
				mdInt.CurrentValue=((LookupValue)this.comboBoxLevelTableDamage.SelectedItem).IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("change the Damage growth rate",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxLevelTableSpeed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxLevelTable.Text.Length<1)||(this.comboBoxLevelTable.SelectedItem==null)){ return; }
			if((this.comboBoxLevelTableSpeed.Text.Length<1)||(this.comboBoxLevelTableSpeed.SelectedItem==null)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=((LookupValue)this.comboBoxLevelTable.SelectedItem).IntValue+((int)Constants.LevelTableOffsets.Speed);
				mdInt.CurrentValue=((LookupValue)this.comboBoxLevelTableSpeed.SelectedItem).IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}
			catch(Exception x)
			{
				this.errorHandler("change the Speed growth rate",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemyTechniqueLevel_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemyTechniqueLevel.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.TechniqueLevel;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the technique level for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxEnemyTechniqueCastPercent_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemyTechniqueCastPercent.Text;
				mdInt.NumBytes=1;
				int percent=Convert.ToInt32(newValue);
				mdInt.CurrentValue=AridiaUtils.percentFromDecimalToRom(percent);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.TechniqueCastPercent;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the technique cast percent for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;	
		}

		private void textBoxEnemyEscapePercent_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectEnemy.Text.Length<1)||(this.textBoxEnemyAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxEnemyEscapePercent.Text;
				mdInt.NumBytes=1;
				int percent=Convert.ToInt32(newValue);
				mdInt.CurrentValue=AridiaUtils.percentFromDecimalToRom(percent);
				mdInt.Address=(Convert.ToInt32(this.textBoxEnemyAddress.Text))+(int)Constants.EnemyOffsets.EscapePercent;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the escape percent for an enemy",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;	
		}

        private void buttonCreateIPS_Click(object sender,EventArgs e)
        {
            try
            {
                this.Cursor=Cursors.WaitCursor;
                IPSCreatorForm ipsCreatorForm=new IPSCreatorForm(this.textBoxRomPath.Text);
                this.Cursor=Cursors.Default;
                ipsCreatorForm.ShowDialog(this);
            }
            catch(Exception x)
            {
                this.errorHandler("create an IPS file",x);
            }
        }

        private void menuItemCreateIPSFile_Click(object sender,EventArgs e)
        {
            buttonCreateIPS_Click(sender,e);
        }

        private void MainForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            if(this.checkBoxAutoFixChecksum.Checked==true)
            {
                if(this.romIO!=null) 
                {
                    this.romIO.writeChecksum(this.romIO.generateChecksum());
                }
            }
        }

        private void listViewTreasure_ColumnClick(object sender,ColumnClickEventArgs e)
        {
            this.Cursor=Cursors.WaitCursor;
            AridiaUtils.sortListView(e,this.listViewTreasure);
            this.Cursor=Cursors.Default;
        }

        private void listViewTreasure_SelectedIndexChanged(object sender,EventArgs e)
        {
            if(this.listViewTreasure.SelectedItems.Count==1)
            {
                this.Cursor=Cursors.WaitCursor;
                try
                {
                    //starting address
                    int address=int.Parse(listViewTreasure.SelectedItems[0].SubItems[1].Text);
                    //X
                    int x=this.romIO.readInteger(address+(int)Constants.TreasureOffsets.X,2);
                    this.textBoxTreasureX.Text=x.ToString();
                    //Y
                    int y=this.romIO.readInteger(address+(int)Constants.TreasureOffsets.Y,2);
                    this.textBoxTreasureY.Text=y.ToString();
                    //item
                    PS3Item item=AridiaUtils.readItem(address+(int)Constants.TreasureOffsets.ItemCode,this.romIO);
                    AridiaUtils.setComboBoxSelection(this.comboBoxTreasureItem,item.itemLookup.IntValue);
                }
                catch(Exception x)
                {
                    this.errorHandler("selecting a treasure chest",x);
                }
                this.Cursor=Cursors.Default;
            }
        }

        private void comboBoxTreasureItem_Validating(object sender,CancelEventArgs e)
        {
            if(this.listViewTreasure.SelectedItems.Count!=1) { return; }
            this.Cursor=Cursors.WaitCursor;
            try
            {
                int address=int.Parse(this.listViewTreasure.SelectedItems[0].SubItems[1].Text)+(int)Constants.TreasureOffsets.ItemCode;
                LookupValue newValue=(LookupValue)this.comboBoxTreasureItem.SelectedItem;
                String hexString=newValue.IntValue.ToString("X");
                if(hexString.Length==1)
                {
                    hexString="0"+hexString;
                }
                hexString="0"+hexString+"0";
                AridiaUtils.saveInventoryItem(hexString,address,this.romIO);
                this.statusBarPanel.Text="Wrote "+hexString+" to address "+address.ToString();
            }
            catch(Exception x)
            {
                this.errorHandler("saving a treasure chest item",x);
            }
            this.Cursor=Cursors.Default;
        }

        private void textBoxTreasureX_Validating(object sender,CancelEventArgs e)
        {
            if((this.listViewTreasure.SelectedItems.Count!=1)||(this.textBoxTreasureX.Text.Length<1)) { return; }
            this.Cursor=Cursors.WaitCursor;
            MDInteger mdInt=new MDInteger();
            try
            {
                string newValue=this.textBoxTreasureX.Text;
                mdInt.NumBytes=2;
                mdInt.CurrentValue=Convert.ToInt32(newValue);
                mdInt.Address=int.Parse(this.listViewTreasure.SelectedItems[0].SubItems[1].Text)+(int)Constants.TreasureOffsets.X;
                try
                {
                    if(AridiaUtils.validateMDInteger(mdInt))
                    {
                        this.romIO.writeInt(mdInt);
                        this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
                    }
                    else
                    {
                        this.validationFailed(mdInt);
                        e.Cancel=true;
                    }
                }
                catch(Exception x)
                {
                    this.errorHandler("save the x value for a treasure chest",x);
                    e.Cancel=true;
                }
            }
            catch
            {
                //can be thrown from Convert.ToInt call
                this.validationFailed(mdInt);
                e.Cancel=true;
            }
            this.Cursor=Cursors.Default;
        }

        private void textBoxTreasureY_Validating(object sender,CancelEventArgs e)
        {
            if((this.listViewTreasure.SelectedItems.Count!=1)||(this.textBoxTreasureY.Text.Length<1)) { return; }
            this.Cursor=Cursors.WaitCursor;
            MDInteger mdInt=new MDInteger();
            try
            {
                string newValue=this.textBoxTreasureY.Text;
                mdInt.NumBytes=2;
                mdInt.CurrentValue=Convert.ToInt32(newValue);
                mdInt.Address=int.Parse(this.listViewTreasure.SelectedItems[0].SubItems[1].Text)+(int)Constants.TreasureOffsets.Y;
                try
                {
                    if(AridiaUtils.validateMDInteger(mdInt))
                    {
                        this.romIO.writeInt(mdInt);
                        this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
                    }
                    else
                    {
                        this.validationFailed(mdInt);
                        e.Cancel=true;
                    }
                }
                catch(Exception x)
                {
                    this.errorHandler("save the y value for a treasure chest",x);
                    e.Cancel=true;
                }
            }
            catch
            {
                //can be thrown from Convert.ToInt call
                this.validationFailed(mdInt);
                e.Cancel=true;
            }
            this.Cursor=Cursors.Default;
        }

        private void comboBoxItemAnimation_Validating(object sender, CancelEventArgs e)
        {
            if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){return;}
            this.Cursor=Cursors.WaitCursor;
            try
            {
                LookupValue newValue=(LookupValue)this.comboBoxItemAnimation.SelectedItem;
                MDInteger mdInt=new MDInteger();
                mdInt.NumBytes=2;
                mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.Animation;
                mdInt.CurrentValue=newValue.IntValue;
                this.romIO.writeInt(mdInt);
                this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
            }
            catch(Exception x)
            {
                this.errorHandler("save the animation for an item",x);
                e.Cancel=true;
            }
            this.Cursor = Cursors.Default;
        }

        private void textBoxItemAttack_Validating(object sender, CancelEventArgs e)
        {
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxItemAttack.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.Attack;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the attack for a Item",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}		
			this.Cursor=Cursors.Default;
        }

        private void textBoxItemDefense_Validating(object sender, CancelEventArgs e)
        {
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxItemDefense.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.Defense;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the defense for a Item",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}		
			this.Cursor=Cursors.Default;
        }

        private void textBoxItemSpeed_Validating(object sender, CancelEventArgs e)
        {
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxItemSpeed.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.Speed;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the speed for a Item",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void textBoxItemName_Validating(object sender,CancelEventArgs e)
        {
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)||(this.textBoxItemNameAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDString mdString=new MDString();
				mdString.CurrentValue=this.textBoxItemName.Text;
                mdString.Address=int.Parse(this.textBoxItemNameAddress.Text);
                mdString.NumBytes=int.Parse(this.textBoxItemNameLength.Text);
                if(AridiaUtils.validateMDString(mdString))
                {
                    this.romIO.writeString(mdString,AridiaUtils.STRING_TERMINATOR);
                    this.statusBarPanel.Text="Wrote "+mdString.CurrentValue+" to address "+mdString.Address.ToString();
                }
                else
                {
                    this.validationFailed(mdString);
                    e.Cancel=true;
                }
            }
			catch(Exception x)
			{
				this.errorHandler("save the name for a Item",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void saveEquipBy(object sender,CancelEventArgs e)
        {
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                int equipby=0;
                if(this.checkBoxEquipRhysNial.Checked){equipby=equipby|(int)Constants.CharacterIdMask.RhysNial;}
                if(this.checkBoxEquipAynSeanCrysAdanAron.Checked){equipby=equipby|(int)Constants.CharacterIdMask.AynSeanCrysAdanAron;}
                if(this.checkBoxEquipMieu.Checked){equipby=equipby|(int)Constants.CharacterIdMask.Mieu;}
                if(this.checkBoxEquipWren.Checked){equipby=equipby|(int)Constants.CharacterIdMask.Wren;}
                if(this.checkBoxEquipTheaKara.Checked){equipby=equipby|(int)Constants.CharacterIdMask.TheaKara;}
                if(this.checkBoxEquipLyleRyan.Checked){equipby=equipby|(int)Constants.CharacterIdMask.LyleRyan;}
                if(this.checkBoxEquipLenaSari.Checked){equipby=equipby|(int)Constants.CharacterIdMask.LenaSari;}
                if(this.checkBoxEquipLayaGwyn.Checked){equipby=equipby|(int)Constants.CharacterIdMask.LayaGwyn;}
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.EquipBy;
				mdInt.CurrentValue=equipby;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the equip by for a Item",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void saveItemFlags(object sender,CancelEventArgs e)
        {
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                int itemFlags=0;
                if(this.checkBoxItemFlag0.Checked){itemFlags=itemFlags|16;}
                if(this.checkBoxItemFlag1.Checked){itemFlags=itemFlags|32;}
                if(this.checkBoxItemFlag2.Checked){itemFlags=itemFlags|64;}
                if(this.checkBoxItemFlag3.Checked){itemFlags=itemFlags|128;}
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.Flags;
				mdInt.CurrentValue=itemFlags;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the flags by for a Item",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxItemEquipWhere_Validating(object sender,CancelEventArgs e)
        {
			if((this.comboBoxSelectItem.Text.Length<1)||(this.textBoxItemAddress.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxItemEquipWhere.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxItemAddress.Text))+(int)Constants.ItemOffsets.EquipWhere;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save equip where for an item",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void listBoxSelectEnemyGroup_SelectedIndexChanged(object sender,EventArgs e)
        {
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.listBoxSelectEnemyGroup.SelectedItem;
				int address=selectedItem.IntValue;
                this.textBoxGroupAddress.Text=address.ToString();
				//front row enemy
				int frontRowEnemyId=this.romIO.readInteger(address+(int)Constants.EnemyGroupOffsets.FrontRowEnemyId,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxFrontRowEnemy,frontRowEnemyId);
				//front row enemy count
				int frontRowEnemyCount=this.romIO.readInteger(address+(int)Constants.EnemyGroupOffsets.FrontRowEnemyCount,1);
				this.comboBoxFrontRowEnemyCount.SelectedIndex=frontRowEnemyCount;
				//back row enemy
				int backRowEnemyId=this.romIO.readInteger(address+(int)Constants.EnemyGroupOffsets.BackRowEnemyId,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxBackRowEnemy,backRowEnemyId);
				//back row enemy count
				int backRowEnemyCount=this.romIO.readInteger(address+(int)Constants.EnemyGroupOffsets.BackRowEnemyCount,1);
				this.comboBoxBackRowEnemyCount.SelectedIndex=backRowEnemyCount;
            }
			catch(Exception x)
			{
				this.errorHandler("selecting an enemy group",x);
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxFrontRowEnemy_Validating(object sender,CancelEventArgs e)
        {
			if(this.textBoxGroupAddress.Text.Length<1){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxFrontRowEnemy.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxGroupAddress.Text))+(int)Constants.EnemyGroupOffsets.FrontRowEnemyId;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the front row enemy",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxFrontRowEnemyCount_Validating(object sender,CancelEventArgs e)
        {
			if(this.textBoxGroupAddress.Text.Length<1){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxGroupAddress.Text))+(int)Constants.EnemyGroupOffsets.FrontRowEnemyCount;
				mdInt.CurrentValue=this.comboBoxFrontRowEnemyCount.SelectedIndex;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the front row enemy count",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxBackRowEnemy_Validating(object sender,CancelEventArgs e)
        {
			if(this.textBoxGroupAddress.Text.Length<1){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxBackRowEnemy.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxGroupAddress.Text))+(int)Constants.EnemyGroupOffsets.BackRowEnemyId;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the back row enemy",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxBackRowEnemyCount_Validating(object sender,CancelEventArgs e)
        {
			if(this.textBoxGroupAddress.Text.Length<1){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxGroupAddress.Text))+(int)Constants.EnemyGroupOffsets.BackRowEnemyCount;
				mdInt.CurrentValue=this.comboBoxBackRowEnemyCount.SelectedIndex;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the back row enemy count",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void menuItemHelp_Click(object sender,EventArgs e)
        {
            this.Cursor=Cursors.WaitCursor;
			try
			{
				System.Diagnostics.Process.Start(@"https://docs.google.com/document/d/1DR9HdW-avvZ3R7yixsDUbg8neFMypzVjUuQdLnpWJDE/edit?usp=sharing");		
			}
			catch(Exception x)
			{
				this.errorHandler("opening the online help",x);
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxNPCsSelectLocation_SelectedIndexChanged(object sender,EventArgs e)
        {
			if(this.comboBoxNPCsSelectLocation.Text.Length<1){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                this.listBoxNPCs.Items.Clear();
                this.comboBoxNPCSprite.Items.Clear();
				LookupValue selectedItem=(LookupValue)this.comboBoxNPCsSelectLocation.SelectedItem;
				int address=selectedItem.IntValue;
                int npcCount=this.romIO.readInteger(address,2);
                address+=2;
                for(int npcIndex=0;npcIndex<=npcCount;npcIndex++) 
                { 
                    //find the sprite for this NPC
                    int spriteId=this.romIO.readInteger(address+(int)Constants.NPCOffsets.SpriteObjectTableIndex,1);
                    LookupValue spriteLV=this.npcSprites.getByValue(spriteId);
                    if(!this.comboBoxNPCSprite.Items.Contains(spriteLV))
                    {
                        this.comboBoxNPCSprite.Items.Add(spriteLV);
                    }
                    //add the npc to the listview
                    LookupValue lv=new LookupValue("NPC ["+npcIndex+"]: "+spriteLV.Description,address);
                    this.listBoxNPCs.Items.Add(lv);
                    address+=10;
                }
                this.listBoxNPCs.SelectedIndex=0;
			}
			catch(Exception x)
			{
				this.errorHandler("selecting a NPC location",x);
			}
			this.Cursor=Cursors.Default;		
        }

        private void listBoxNPCs_SelectedIndexChanged(object sender,EventArgs e)
        {
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.listBoxNPCs.SelectedItem;
				int address=selectedItem.IntValue;
                this.textBoxNPCAddress.Text=address.ToString();
                //sprite
				int sprite=this.romIO.readInteger(address+(int)Constants.NPCOffsets.SpriteObjectTableIndex,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxNPCSprite,sprite);
                //x
                this.textBoxNPCX.Text=this.romIO.readInteger(address+(int)Constants.NPCOffsets.XCoordinate,2).ToString();
                //y
                this.textBoxNPCY.Text=this.romIO.readInteger(address+(int)Constants.NPCOffsets.YCoordinate,2).ToString();
                //update rate
                this.textBoxNPCUpdateRate.Text=this.romIO.readInteger(address+(int)Constants.NPCOffsets.UpdateRate,2).ToString();
                //script
                int scriptAddress=this.romIO.readInteger(address+(int)Constants.NPCOffsets.GameScriptIndex,2)+Constants.NPCScriptStartPad;
                scriptAddress+=Constants.NPCScriptOffset;
                this.textBoxNPCScriptAddress.Text=scriptAddress.ToString();
                String scriptText=this.romIO.readString(scriptAddress,65535);
                this.textBoxNPCScriptText.Text=scriptText;
                this.textBoxNPCScriptText.MaxLength=scriptText.Length;
            }
			catch(Exception x)
			{
				this.errorHandler("selecting a NPC",x);
			}
			this.Cursor=Cursors.Default;
        }

        private void textBoxNPCX_Validating(object sender,CancelEventArgs e)
        {
			if((this.comboBoxNPCsSelectLocation.Text.Length<1)||(this.textBoxNPCAddress.Text.Length<1)){return;}
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxNPCX.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxNPCAddress.Text))+(int)Constants.NPCOffsets.XCoordinate;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the X value for an NPC",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void textBoxNPCY_Validating(object sender,CancelEventArgs e)
        {
			if((this.comboBoxNPCsSelectLocation.Text.Length<1)||(this.textBoxNPCAddress.Text.Length<1)){return;}
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxNPCY.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxNPCAddress.Text))+(int)Constants.NPCOffsets.YCoordinate;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the Y value for an NPC",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxNPCSprite_Validating(object sender,CancelEventArgs e)
        {
			if((this.comboBoxNPCsSelectLocation.Text.Length<1)||(this.textBoxNPCAddress.Text.Length<1)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxNPCSprite.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxNPCAddress.Text))+(int)Constants.NPCOffsets.SpriteObjectTableIndex;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the sprite for an NPC",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void textBoxNPCUpdateRate_Validating(object sender,CancelEventArgs e)
        {
			if((this.comboBoxNPCsSelectLocation.Text.Length<1)||(this.textBoxNPCAddress.Text.Length<1)){return;}
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=this.textBoxNPCUpdateRate.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxNPCAddress.Text))+(int)Constants.NPCOffsets.UpdateRate;
				try
				{
					if(AridiaUtils.validateMDInteger(mdInt))
					{
						this.romIO.writeInt(mdInt);
						this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
					}
					else
					{
						this.validationFailed(mdInt);
						e.Cancel=true;
					}
				}
				catch(Exception x)
				{
					this.errorHandler("save the update rate for an NPC",x);
					e.Cancel=true;
				}
			}
			catch
			{
				//can be thrown from Convert.ToInt call
				this.validationFailed(mdInt);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void textBoxNPCScriptText_Validating(object sender,CancelEventArgs e)
        {
			if((this.comboBoxNPCsSelectLocation.Text.Length<1)||(this.textBoxNPCScriptAddress.Text.Length<1)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                MDString mdString=new MDString();
                mdString.CurrentValue=this.textBoxNPCScriptText.Text;
                mdString.Address=int.Parse(this.textBoxNPCScriptAddress.Text);
                mdString.NumBytes=this.textBoxNPCScriptText.MaxLength;
                if(AridiaUtils.validateMDString(mdString))
                {
                    this.romIO.writeString(mdString,AridiaUtils.STRING_TERMINATOR);
                    this.statusBarPanel.Text="Wrote "+mdString.CurrentValue+" to address "+mdString.Address.ToString();
                    this.scriptDirtyFlag=true;
                }
                else
                {
                    this.validationFailed(mdString);
                    e.Cancel=true;
                }
			}
			catch(Exception x)
			{
				this.errorHandler("save the script text for an NPC",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxSelectScriptedEvent_SelectedIndexChanged(object sender,EventArgs e)
        {
			this.Cursor=Cursors.WaitCursor;
			try
			{
                this.listBoxScriptedEventFrames.Items.Clear();
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectScriptedEvent.SelectedItem;
				int address=selectedItem.IntValue;
                int frameNumber=0;
                int byte1=this.romIO.readInteger(address,1);
                while(byte1<255)
                { 
                    ScriptedEventFrame frame=new ScriptedEventFrame();
                    frame.frameNumber=frameNumber;
                    frame.byte1=byte1;
                    address++;
                    frame.byte2=this.romIO.readInteger(address,1);
                    this.listBoxScriptedEventFrames.Items.Add(frame);
                    address++;
                    byte1=this.romIO.readInteger(address,1);
                    frameNumber++;
                }
            }
			catch(Exception x)
			{
				this.errorHandler("select a scripted event",x);
			}
			this.Cursor=Cursors.Default;
        }

        private void listBoxScriptedEventFrames_SelectedIndexChanged(object sender,EventArgs e)
        {
            if((this.comboBoxSelectScriptedEvent.SelectedItem==null)||(this.listBoxScriptedEventFrames.SelectedItem==null)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                ScriptedEventFrame frame=(ScriptedEventFrame)this.listBoxScriptedEventFrames.SelectedItem;
                if(frame.byte1==0) //dialog event
                { 
                    this.radioButtonScriptedEventDialog.Select();
                    this.comboBoxScriptedEventDialogOffset.SelectedIndex=frame.byte2;
                }
                else
                { 
                    if(frame.byte2==0) //delay event
                    {
                        this.radioButtonScriptedEventDelay.Select();
                        this.textBoxScriptedEventDelay.Text=frame.byte1.ToString();
                    }
                    else //movement
                    { 
                        this.radioButtonScriptedEventMovement.Select();
                        AridiaUtils.setComboBoxSelection(comboBoxScriptedEventDirection,frame.byte2);
                        this.textBoxScriptedEventDuration.Text=frame.byte1.ToString();
                    }
                
                }
            }
			catch(Exception x)
			{
				this.errorHandler("select a scripted event frame",x);
			}
			this.Cursor=Cursors.Default;
        }

        private void radioButtonScriptedEventMovement_CheckedChanged(object sender,EventArgs e)
        {
            if(radioButtonScriptedEventMovement.Checked) 
            { 
                this.comboBoxScriptedEventDirection.Enabled=true;
                this.textBoxScriptedEventDuration.Enabled=true;
                if(this.textBoxScriptedEventDuration.Text.Length==0) 
                { 
                    this.textBoxScriptedEventDuration.Text="0";
                }
                this.textBoxScriptedEventDialogValue.Enabled=false;
                this.comboBoxScriptedEventDialogOffset.Enabled=false;
                this.textBoxScriptedEventDelay.Enabled=false;
            }
        }

        private void radioButtonScriptedEventDialog_CheckedChanged(object sender,EventArgs e)
        {
            if(radioButtonScriptedEventDialog.Checked) 
            { 
                this.comboBoxScriptedEventDirection.Enabled=false;
                this.textBoxScriptedEventDuration.Enabled=false;
                this.textBoxScriptedEventDialogValue.Enabled=true;
                this.comboBoxScriptedEventDialogOffset.Enabled=true;
                this.textBoxScriptedEventDelay.Enabled=false;
            }
        }

        private void radioButtonScriptedEventDelay_CheckedChanged(object sender,EventArgs e)
        {
            if(radioButtonScriptedEventDelay.Checked) 
            { 
                this.comboBoxScriptedEventDirection.Enabled=false;
                this.textBoxScriptedEventDuration.Enabled=false;
                this.textBoxScriptedEventDialogValue.Enabled=false;
                this.comboBoxScriptedEventDialogOffset.Enabled=false;
                this.textBoxScriptedEventDelay.Enabled=true;
                if(this.textBoxScriptedEventDelay.Text.Length==0) 
                { 
                    this.textBoxScriptedEventDelay.Text="0";
                }
            }
        }

        private bool saveEventFrame(int byte1,int byte2)
        { 
            if((this.comboBoxSelectScriptedEvent.SelectedItem==null)||(this.listBoxScriptedEventFrames.SelectedItem==null)){return(false);}
			try
			{
                MDInteger mdIntByte1=new MDInteger();
                mdIntByte1.MaxValue=255;
                mdIntByte1.MinValue=0;
                mdIntByte1.NumBytes=1;
                mdIntByte1.CurrentValue=byte1;
                MDInteger mdIntByte2=new MDInteger();
                mdIntByte2.MaxValue=255;
                mdIntByte2.MinValue=0;
                mdIntByte2.NumBytes=1;
                mdIntByte2.CurrentValue=byte2;
                //make sure both validate before saving
                if(!AridiaUtils.validateMDInteger(mdIntByte1))
                {
                    this.validationFailed(mdIntByte1);
                    return(false);
                }
                if(!AridiaUtils.validateMDInteger(mdIntByte2))
                {
                    this.validationFailed(mdIntByte2);
                    return(false);
                }
                //figure out the address
                int baseAddress=((LookupValue)this.comboBoxSelectScriptedEvent.SelectedItem).IntValue;
                int frameNumber=((ScriptedEventFrame)this.listBoxScriptedEventFrames.SelectedItem).frameNumber;
                mdIntByte1.Address=baseAddress+(frameNumber*2);
                mdIntByte2.Address=mdIntByte1.Address+1;
                this.romIO.writeInt(mdIntByte1);
                this.romIO.writeInt(mdIntByte2);
                this.statusBarPanel.Text="Wrote "+mdIntByte1.CurrentValue+","+mdIntByte2.CurrentValue+" to address "+mdIntByte1.Address.ToString();
                //update the selected item in the listview
                ((ScriptedEventFrame)this.listBoxScriptedEventFrames.SelectedItem).byte1=byte1;
                ((ScriptedEventFrame)this.listBoxScriptedEventFrames.SelectedItem).byte2=byte2;
                this.listBoxScriptedEventFrames.Items[this.listBoxScriptedEventFrames.SelectedIndex]=this.listBoxScriptedEventFrames.SelectedItem;
            }
			catch(Exception x)
			{
				this.errorHandler("save a scripted event frame",x);
                return(false);
			}
            return(true);
        }

        private void comboBoxScriptedEventDirection_Validating(object sender,CancelEventArgs e)
        {
            if((this.comboBoxSelectScriptedEvent.SelectedItem==null)||(this.listBoxScriptedEventFrames.SelectedItem==null)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                String duration=this.textBoxScriptedEventDuration.Text;
                int byte1=Int32.Parse(duration);
                int byte2=((LookupValue)this.comboBoxScriptedEventDirection.SelectedItem).IntValue;
                e.Cancel=!this.saveEventFrame(byte1,byte2);
            }
			catch(Exception x)
			{
				this.errorHandler("edit the direction for a scripted event frame",x);
                e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void textBoxScriptedEventDuration_Validating(object sender,CancelEventArgs e)
        {
            if((this.comboBoxSelectScriptedEvent.SelectedItem==null)||(this.listBoxScriptedEventFrames.SelectedItem==null)){return;}
			try
			{
                String duration=this.textBoxScriptedEventDuration.Text;
                int byte1=Int32.Parse(duration);
                int byte2=((LookupValue)this.comboBoxScriptedEventDirection.SelectedItem).IntValue;
                e.Cancel=!this.saveEventFrame(byte1,byte2);
            }
			catch(Exception x)
			{
				this.errorHandler("edit the duration for a scripted event frame",x);
                e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void textBoxScriptedEventDialogValue_Validating(object sender,CancelEventArgs e)
        {
            if((this.comboBoxSelectScriptedEvent.SelectedItem==null)||(this.listBoxScriptedEventFrames.SelectedItem==null)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                MDString mdString=new MDString();
                mdString.CurrentValue=this.textBoxScriptedEventDialogValue.Text;
                mdString.NumBytes=this.textBoxScriptedEventDialogValue.MaxLength;
                //figure out the address
                int offsetAddress=Constants.ScriptedEventDialogOffsetsStart+(this.comboBoxScriptedEventDialogOffset.SelectedIndex*2);
                int offset=this.romIO.readInteger(offsetAddress,2);
                int dialogAddress=Constants.ScriptStartAddress+offset+Constants.NPCScriptStartPad;
                mdString.Address=dialogAddress;                
                if(AridiaUtils.validateMDString(mdString))
                {
                    this.romIO.writeString(mdString,AridiaUtils.STRING_TERMINATOR);
                    this.statusBarPanel.Text="Wrote "+mdString.CurrentValue+" to address "+mdString.Address.ToString();
                    this.scriptDirtyFlag=true;
                }
                else
                {
                    this.validationFailed(mdString);
                    e.Cancel=true;
                }
            }
			catch(Exception x)
			{
				this.errorHandler("edit the dialog for a scripted event frame",x);
                e.Cancel=true;
			}
			this.Cursor=Cursors.Default;

        }

        private void textBoxScriptedEventDelay_Validating(object sender,CancelEventArgs e)
        {
            if((this.comboBoxSelectScriptedEvent.SelectedItem==null)||(this.listBoxScriptedEventFrames.SelectedItem==null)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                String duration=this.textBoxScriptedEventDelay.Text;
                int byte1=Int32.Parse(duration);
                int byte2=0;
                e.Cancel=!this.saveEventFrame(byte1,byte2);
            }
			catch(Exception x)
			{
				this.errorHandler("edit the delay for a scripted event frame",x);
                e.Cancel=true;
			}
			this.Cursor=Cursors.Default;

        }

        private void comboBoxScriptedEventDialogOffset_Validating(object sender,CancelEventArgs e)
        {
            if((this.comboBoxSelectScriptedEvent.SelectedItem==null)||(this.listBoxScriptedEventFrames.SelectedItem==null)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                e.Cancel=!this.saveEventFrame(0,comboBoxScriptedEventDialogOffset.SelectedIndex);
            }
			catch(Exception x)
			{
				this.errorHandler("edit the dialog offset for a scripted event frame",x);
                e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
        }

        private void comboBoxScriptedEventDialogOffset_SelectedIndexChanged(object sender,EventArgs e)
        {
            if((this.comboBoxSelectScriptedEvent.SelectedItem==null)||(this.listBoxScriptedEventFrames.SelectedItem==null)){return;}
			this.Cursor=Cursors.WaitCursor;
			try
			{
                int offsetAddress=Constants.ScriptedEventDialogOffsetsStart+(this.comboBoxScriptedEventDialogOffset.SelectedIndex*2);
                int offset=this.romIO.readInteger(offsetAddress,2);
                int dialogAddress=Constants.ScriptStartAddress+offset+Constants.NPCScriptStartPad;
                String dialog=this.romIO.readString(dialogAddress,65535);
                this.textBoxScriptedEventDialogValue.Text=dialog;
                this.textBoxScriptedEventDialogValue.MaxLength=dialog.Length;
            }
			catch(Exception x)
			{
				this.errorHandler("select a scripted event dialog offset",x);
			}
			this.Cursor=Cursors.Default;
        }

        private void buttonLaunchTileEditor_Click(object sender,EventArgs e)
        {
            if(this.listBoxTileSets.SelectedItem==null){return;}
			try
			{
                TileSetDefinition tsd=(TileSetDefinition)this.listBoxTileSets.SelectedItem;
				TileEditorForm tileEditor=new TileEditorForm(this.romIO,tsd.startAddress,tsd.endAddress,tsd.columns,tsd.rows,AridiaUtils.getLookupValueCollection("Palette-Addresses"));
				tileEditor.ShowDialog(this);
			}
			catch(Exception x)
			{
				this.errorHandler("edit a tileset",x);
			}
        }

        private void listBoxTileSets_SelectedIndexChanged(object sender,EventArgs e)
        {
            if(this.listBoxTileSets.SelectedItem==null){return;}
            this.Cursor=Cursors.WaitCursor;
			try
			{
                TileSetDefinition tsd=(TileSetDefinition)this.listBoxTileSets.SelectedItem;
                this.textBoxTilesetColumns.Text=tsd.columns.ToString();
                this.textBoxTilesetRows.Text=tsd.rows.ToString();
                this.textBoxTilesetStartAddress.Text=tsd.startAddress.ToString();
                this.textBoxTilesetEndAddress.Text=tsd.endAddress.ToString();
                this.textBoxTilesetHeight.Text=tsd.tileHeight.ToString();
                this.textBoxTilesetWidth.Text=tsd.tileWidth.ToString();
                this.buttonLaunchTileEditor.Enabled=true;
			}
			catch(Exception x)
			{
				this.errorHandler("select a tileset",x);
			}
            this.Cursor=Cursors.Default;
        }

    }
}
