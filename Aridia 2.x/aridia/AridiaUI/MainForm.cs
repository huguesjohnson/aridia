/*
Aridia: Phantasy Star III ROM Editor
Copyright (c) 2007-2011 Hugues Johnson

Aridia is free software; you can redistribute it and/or modify
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
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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
		private MDBinaryRomIO romIO;
        private const String GOOD_HEADER="SEGA MEGA DRIVE (C)SEGA 1991.APLTOKINO          KEISHOUSHA      PHANTASY STAR 3 PHANTASY STAR 3 GENERATIONS     OF DOOM         GM 1303-01";
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.ListView listViewNavigate;
		private System.Windows.Forms.TabControl tabControlMainContent;
		private System.Windows.Forms.StatusBar statusBar;
		private System.Windows.Forms.TabPage tabPageMain;
		private System.Windows.Forms.TabPage tabPageScript;
		private System.Windows.Forms.TabPage tabPageDialogText;
		private System.Windows.Forms.TabPage tabPageItems;
		private System.Windows.Forms.TabPage tabPageWeapons;
		private System.Windows.Forms.TabPage tabPageCharacters;
		private System.Windows.Forms.TabPage tabPageEnemies;
		private System.Windows.Forms.ColumnHeader columnHeader;
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
		private ListViewColumnSorter listViewNavigateSorter;
		private ListViewColumnSorter listViewPalettesSorter;
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
		private System.Windows.Forms.TabPage tabPageInventoryNames;
		private System.Windows.Forms.ListView listViewInventoryNames;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private ListViewColumnSorter listViewScriptSorter;
		private System.Windows.Forms.ComboBox comboBoxSelectWeapon;
		private System.Windows.Forms.Label labelSelectWeapon;
		private System.Windows.Forms.Panel panelWeapon;
		private System.Windows.Forms.TextBox textBoxWeaponAddress;
		private System.Windows.Forms.Label labelWeaponAddress;
		private System.Windows.Forms.ComboBox comboBoxWeaponTechnique;
		private System.Windows.Forms.TextBox textBoxWeaponCost;
		private System.Windows.Forms.Label labelWeaponTechnique;
		private System.Windows.Forms.Label labelWeaponCost;
		private System.Windows.Forms.ComboBox comboBoxWeaponAnimation;
		private System.Windows.Forms.Label labelWeaponAnimation;
		private System.Windows.Forms.TextBox textBoxWeaponAttack;
		private System.Windows.Forms.Label labelWeaponAttack;
		private System.Windows.Forms.TextBox textBoxWeaponDefense;
		private System.Windows.Forms.Label labelWeaponDefense;
		private System.Windows.Forms.TextBox textBoxWeaponSpeed;
		private System.Windows.Forms.Label labelWeaponSpeed;
		private System.Windows.Forms.ComboBox comboBoxWeaponEquipBy;
		private System.Windows.Forms.Label labelWeaponEquipBy;
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
		private System.Windows.Forms.Button buttonEditTitleLogo;
		private System.Windows.Forms.Button buttonEditFont;
        private System.Windows.Forms.Button buttonEditBorders;
		private System.Windows.Forms.Button buttonCreditFont;
		private System.Windows.Forms.Button buttonEditStatusFont;
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
		private int paletteFindIndex;

		public MainForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			//setup listview sorters
			this.listViewNavigateSorter=new ListViewColumnSorter();
			this.listViewNavigate.ListViewItemSorter=listViewNavigateSorter;
			this.listViewDialogTextSorter=new ListViewColumnSorter();
			this.listViewDialogText.ListViewItemSorter=listViewDialogTextSorter;
			this.listViewScriptSorter=new ListViewColumnSorter();
			this.listViewScript.ListViewItemSorter=listViewScriptSorter;
			this.listViewInventoryNamesSorter=new ListViewColumnSorter();
			this.listViewInventoryNames.ListViewItemSorter=listViewScriptSorter;
			this.listViewPalettesSorter=new ListViewColumnSorter();
			this.listViewPalettes.ListViewItemSorter=listViewPalettesSorter;
			this.tabControlMainContent.SelectedIndex=0;
			this.listViewNavigate.Items[0].Selected=true;
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
            this.components=new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem13=new System.Windows.Forms.ListViewItem("Main");
            System.Windows.Forms.ListViewItem listViewItem14=new System.Windows.Forms.ListViewItem("Characters");
            System.Windows.Forms.ListViewItem listViewItem15=new System.Windows.Forms.ListViewItem("Dialog Text");
            System.Windows.Forms.ListViewItem listViewItem16=new System.Windows.Forms.ListViewItem("Enemies");
            System.Windows.Forms.ListViewItem listViewItem17=new System.Windows.Forms.ListViewItem("Graphics");
            System.Windows.Forms.ListViewItem listViewItem18=new System.Windows.Forms.ListViewItem("Inventory Names");
            System.Windows.Forms.ListViewItem listViewItem19=new System.Windows.Forms.ListViewItem("Items");
            System.Windows.Forms.ListViewItem listViewItem20=new System.Windows.Forms.ListViewItem("Level Tables");
            System.Windows.Forms.ListViewItem listViewItem21=new System.Windows.Forms.ListViewItem("Palettes");
            System.Windows.Forms.ListViewItem listViewItem22=new System.Windows.Forms.ListViewItem("Script");
            System.Windows.Forms.ListViewItem listViewItem23=new System.Windows.Forms.ListViewItem("Shops");
            System.Windows.Forms.ListViewItem listViewItem24=new System.Windows.Forms.ListViewItem("Weapons");
            System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMenu=new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1=new System.Windows.Forms.MenuItem();
            this.menuItemOpen=new System.Windows.Forms.MenuItem();
            this.menuItem3=new System.Windows.Forms.MenuItem();
            this.menuItemCreateIPSFile=new System.Windows.Forms.MenuItem();
            this.menuItemChecksum=new System.Windows.Forms.MenuItem();
            this.menuItem5=new System.Windows.Forms.MenuItem();
            this.menuItemHomepage=new System.Windows.Forms.MenuItem();
            this.menuItemAbout=new System.Windows.Forms.MenuItem();
            this.menuItemThanks=new System.Windows.Forms.MenuItem();
            this.menuItem8=new System.Windows.Forms.MenuItem();
            this.menuItemExit=new System.Windows.Forms.MenuItem();
            this.listViewNavigate=new System.Windows.Forms.ListView();
            this.columnHeader=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControlMainContent=new System.Windows.Forms.TabControl();
            this.tabPageMain=new System.Windows.Forms.TabPage();
            this.buttonCreateIPS=new System.Windows.Forms.Button();
            this.textBoxCalculatedChecksum=new System.Windows.Forms.TextBox();
            this.labelCalculatedChecksum=new System.Windows.Forms.Label();
            this.textBoxChecksum=new System.Windows.Forms.TextBox();
            this.labelChecksum=new System.Windows.Forms.Label();
            this.textBoxRomHeader=new System.Windows.Forms.TextBox();
            this.labelRomHeader=new System.Windows.Forms.Label();
            this.textBoxWarningText=new System.Windows.Forms.TextBox();
            this.labelWarning=new System.Windows.Forms.Label();
            this.pictureBoxWarning=new System.Windows.Forms.PictureBox();
            this.buttonChecksum=new System.Windows.Forms.Button();
            this.imageListIcons=new System.Windows.Forms.ImageList(this.components);
            this.buttonOpenRom=new System.Windows.Forms.Button();
            this.textBoxRomPath=new System.Windows.Forms.TextBox();
            this.labelRom=new System.Windows.Forms.Label();
            this.tabPageCharacters=new System.Windows.Forms.TabPage();
            this.panelCharacter=new System.Windows.Forms.Panel();
            this.comboBoxCharacterTechniqueOrder=new System.Windows.Forms.ComboBox();
            this.labelCharacterTechniqueOrder=new System.Windows.Forms.Label();
            this.comboBoxCharacterTechniqueTime=new System.Windows.Forms.ComboBox();
            this.labelCharacterTechniqueTime=new System.Windows.Forms.Label();
            this.comboBoxCharacterTechniqueHeal=new System.Windows.Forms.ComboBox();
            this.labelCharacterTechniqueHeal=new System.Windows.Forms.Label();
            this.comboBoxCharacterTechniqueMelee=new System.Windows.Forms.ComboBox();
            this.labelCharacterTechniqueMelee=new System.Windows.Forms.Label();
            this.linkLabelItemEditing=new System.Windows.Forms.LinkLabel();
            this.listViewCharacterItems=new System.Windows.Forms.ListView();
            this.columnHeaderCharacterItemHexString=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCharacterItemItem=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCharacterItemIsEquipped=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderCharacterItemWhereEquipped=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuCharacterMenu=new System.Windows.Forms.ContextMenu();
            this.menuItemEditCharacterItem=new System.Windows.Forms.MenuItem();
            this.labelCharacterItems=new System.Windows.Forms.Label();
            this.textBoxCharacterSpeed=new System.Windows.Forms.TextBox();
            this.labelCharacterSpeed=new System.Windows.Forms.Label();
            this.textBoxCharacterSkill=new System.Windows.Forms.TextBox();
            this.labelCharacterSkill=new System.Windows.Forms.Label();
            this.textBoxCharacterLuck=new System.Windows.Forms.TextBox();
            this.labelCharacterLuck=new System.Windows.Forms.Label();
            this.textBoxCharacterDefense=new System.Windows.Forms.TextBox();
            this.labelCharacterDefense=new System.Windows.Forms.Label();
            this.textBoxCharacterAttack=new System.Windows.Forms.TextBox();
            this.labelCharacterAttack=new System.Windows.Forms.Label();
            this.textBoxCharacterTechPoints=new System.Windows.Forms.TextBox();
            this.labelCharacterTechPoints=new System.Windows.Forms.Label();
            this.textBoxCharacterHitPoints=new System.Windows.Forms.TextBox();
            this.labelCharacterHitPoints=new System.Windows.Forms.Label();
            this.comboBoxCharacterTechniques=new System.Windows.Forms.ComboBox();
            this.labelCharacterTechPower=new System.Windows.Forms.Label();
            this.comboBoxCharacterType=new System.Windows.Forms.ComboBox();
            this.labelCharacterType=new System.Windows.Forms.Label();
            this.textBoxCharacterName=new System.Windows.Forms.TextBox();
            this.labelCharacterName=new System.Windows.Forms.Label();
            this.textBoxCharacterAddress=new System.Windows.Forms.TextBox();
            this.labelCharacterAddress=new System.Windows.Forms.Label();
            this.comboBoxSelectCharacter=new System.Windows.Forms.ComboBox();
            this.labelSelectCharacter=new System.Windows.Forms.Label();
            this.tabPageDialogText=new System.Windows.Forms.TabPage();
            this.listViewDialogText=new System.Windows.Forms.ListView();
            this.columnDialogTextHeaderCurrentValue=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDialogTextCategory=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDialogTextDescription=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnDialogTextAddress=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columncolumnDialogTextLength=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageEnemies=new System.Windows.Forms.TabPage();
            this.panelEnemy=new System.Windows.Forms.Panel();
            this.textBoxEnemyEscapePercent=new System.Windows.Forms.TextBox();
            this.labelEnemyEscapePercent=new System.Windows.Forms.Label();
            this.textBoxEnemyTechniqueCastPercent=new System.Windows.Forms.TextBox();
            this.labelEnemyTechniqueCastPercent=new System.Windows.Forms.Label();
            this.textBoxEnemyTechniqueLevel=new System.Windows.Forms.TextBox();
            this.labelEnemyTechniqueLevel=new System.Windows.Forms.Label();
            this.textBoxEnemyName=new System.Windows.Forms.TextBox();
            this.labelEnemyName=new System.Windows.Forms.Label();
            this.textBoxEnemyNameOffset=new System.Windows.Forms.TextBox();
            this.labelEnemyNameOffset=new System.Windows.Forms.Label();
            this.textBoxEnemyMeseta=new System.Windows.Forms.TextBox();
            this.labelEnemyMeseta=new System.Windows.Forms.Label();
            this.textBoxEnemyExperience=new System.Windows.Forms.TextBox();
            this.labelEnemyExperience=new System.Windows.Forms.Label();
            this.textBoxEnemySpeed=new System.Windows.Forms.TextBox();
            this.labelEnemySpeed=new System.Windows.Forms.Label();
            this.textBoxEnemyDefense=new System.Windows.Forms.TextBox();
            this.labelEnemyDefense=new System.Windows.Forms.Label();
            this.textBoxEnemyAttack=new System.Windows.Forms.TextBox();
            this.labelEnemyAttack=new System.Windows.Forms.Label();
            this.textBoxEnemyHitPoints=new System.Windows.Forms.TextBox();
            this.labelEnemyHitPoints=new System.Windows.Forms.Label();
            this.comboBoxEnemyTechnique=new System.Windows.Forms.ComboBox();
            this.labelEnemyTechnique=new System.Windows.Forms.Label();
            this.comboBoxEnemyAnimation=new System.Windows.Forms.ComboBox();
            this.labelEnemyAnimation=new System.Windows.Forms.Label();
            this.comboBoxEnemySpriteGroup=new System.Windows.Forms.ComboBox();
            this.labelEnemySpriteGroup=new System.Windows.Forms.Label();
            this.textBoxEnemyAddress=new System.Windows.Forms.TextBox();
            this.labelEnemyAddress=new System.Windows.Forms.Label();
            this.comboBoxSelectEnemy=new System.Windows.Forms.ComboBox();
            this.labelSelectEnemy=new System.Windows.Forms.Label();
            this.tabPageGraphics=new System.Windows.Forms.TabPage();
            this.buttonEditStatusFont=new System.Windows.Forms.Button();
            this.buttonCreditFont=new System.Windows.Forms.Button();
            this.buttonEditBorders=new System.Windows.Forms.Button();
            this.buttonEditFont=new System.Windows.Forms.Button();
            this.buttonEditTitleLogo=new System.Windows.Forms.Button();
            this.tabPageInventoryNames=new System.Windows.Forms.TabPage();
            this.listViewInventoryNames=new System.Windows.Forms.ListView();
            this.columnHeader2=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageItems=new System.Windows.Forms.TabPage();
            this.panelItem=new System.Windows.Forms.Panel();
            this.textBoxItemAddress=new System.Windows.Forms.TextBox();
            this.labelItemAddress=new System.Windows.Forms.Label();
            this.textBoxItemEffectiveness=new System.Windows.Forms.TextBox();
            this.comboBoxItemTechnique=new System.Windows.Forms.ComboBox();
            this.textBoxItemCost=new System.Windows.Forms.TextBox();
            this.labelItemEffectiveness=new System.Windows.Forms.Label();
            this.labelItemTechnique=new System.Windows.Forms.Label();
            this.labelItemCost=new System.Windows.Forms.Label();
            this.comboBoxSelectItem=new System.Windows.Forms.ComboBox();
            this.labelSelectItem=new System.Windows.Forms.Label();
            this.tabPageLevelTables=new System.Windows.Forms.TabPage();
            this.groupBoxStatisticGrowth=new System.Windows.Forms.GroupBox();
            this.comboBoxLevelTableSpeed=new System.Windows.Forms.ComboBox();
            this.labelLevelTableSpeed=new System.Windows.Forms.Label();
            this.comboBoxLevelTableSkill=new System.Windows.Forms.ComboBox();
            this.labelLevelTableSkill=new System.Windows.Forms.Label();
            this.comboBoxLevelTableDamage=new System.Windows.Forms.ComboBox();
            this.labelLevelTableDamage=new System.Windows.Forms.Label();
            this.comboBoxLevelTableLuck=new System.Windows.Forms.ComboBox();
            this.labelLevelTableLuck=new System.Windows.Forms.Label();
            this.comboBoxLevelTableDefense=new System.Windows.Forms.ComboBox();
            this.labelLevelTableDefense=new System.Windows.Forms.Label();
            this.comboBoxLevelTableTP=new System.Windows.Forms.ComboBox();
            this.labelLevelTableTP=new System.Windows.Forms.Label();
            this.comboBoxLevelTableHP=new System.Windows.Forms.ComboBox();
            this.labelLevelTableHP=new System.Windows.Forms.Label();
            this.listViewLevelTable=new System.Windows.Forms.ListView();
            this.columnHeaderXP=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLevel=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderLevelTableAddress=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBoxLevelTable=new System.Windows.Forms.ComboBox();
            this.labelSelectLevelTable=new System.Windows.Forms.Label();
            this.tabPagePalettes=new System.Windows.Forms.TabPage();
            this.labelPaletteFind=new System.Windows.Forms.Label();
            this.buttonPalettePrevious=new System.Windows.Forms.Button();
            this.buttonPaletteFind=new System.Windows.Forms.Button();
            this.textBoxPaletteFind=new System.Windows.Forms.TextBox();
            this.labelPalettePreview=new System.Windows.Forms.Label();
            this.pictureBoxPalettePreview=new System.Windows.Forms.PictureBox();
            this.buttonLaunchPaletteEditor=new System.Windows.Forms.Button();
            this.listViewPalettes=new System.Windows.Forms.ListView();
            this.columnHeader6=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageScript=new System.Windows.Forms.TabPage();
            this.labelFindScript=new System.Windows.Forms.Label();
            this.buttonFindScriptPrevious=new System.Windows.Forms.Button();
            this.buttonFindScriptNext=new System.Windows.Forms.Button();
            this.textBoxFindScript=new System.Windows.Forms.TextBox();
            this.listViewScript=new System.Windows.Forms.ListView();
            this.columnHeader1=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5=((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageShops=new System.Windows.Forms.TabPage();
            this.panelShop=new System.Windows.Forms.Panel();
            this.comboBoxItem5=new System.Windows.Forms.ComboBox();
            this.labelItem5=new System.Windows.Forms.Label();
            this.comboBoxItem4=new System.Windows.Forms.ComboBox();
            this.labelItem4=new System.Windows.Forms.Label();
            this.comboBoxItem3=new System.Windows.Forms.ComboBox();
            this.labelItem3=new System.Windows.Forms.Label();
            this.comboBoxItem2=new System.Windows.Forms.ComboBox();
            this.labelItem2=new System.Windows.Forms.Label();
            this.textBoxShopAddress=new System.Windows.Forms.TextBox();
            this.labelShopAddress=new System.Windows.Forms.Label();
            this.comboBoxItem1=new System.Windows.Forms.ComboBox();
            this.labelItem1=new System.Windows.Forms.Label();
            this.comboBoxSelectShop=new System.Windows.Forms.ComboBox();
            this.labelSelectShop=new System.Windows.Forms.Label();
            this.tabPageWeapons=new System.Windows.Forms.TabPage();
            this.panelWeapon=new System.Windows.Forms.Panel();
            this.comboBoxWeaponEquipBy=new System.Windows.Forms.ComboBox();
            this.labelWeaponEquipBy=new System.Windows.Forms.Label();
            this.textBoxWeaponSpeed=new System.Windows.Forms.TextBox();
            this.labelWeaponSpeed=new System.Windows.Forms.Label();
            this.textBoxWeaponDefense=new System.Windows.Forms.TextBox();
            this.labelWeaponDefense=new System.Windows.Forms.Label();
            this.textBoxWeaponAttack=new System.Windows.Forms.TextBox();
            this.labelWeaponAttack=new System.Windows.Forms.Label();
            this.comboBoxWeaponAnimation=new System.Windows.Forms.ComboBox();
            this.labelWeaponAnimation=new System.Windows.Forms.Label();
            this.textBoxWeaponAddress=new System.Windows.Forms.TextBox();
            this.labelWeaponAddress=new System.Windows.Forms.Label();
            this.comboBoxWeaponTechnique=new System.Windows.Forms.ComboBox();
            this.textBoxWeaponCost=new System.Windows.Forms.TextBox();
            this.labelWeaponTechnique=new System.Windows.Forms.Label();
            this.labelWeaponCost=new System.Windows.Forms.Label();
            this.comboBoxSelectWeapon=new System.Windows.Forms.ComboBox();
            this.labelSelectWeapon=new System.Windows.Forms.Label();
            this.statusBar=new System.Windows.Forms.StatusBar();
            this.statusBarPanel=new System.Windows.Forms.StatusBarPanel();
            this.openFileRomDialog=new System.Windows.Forms.OpenFileDialog();
            this.tabControlMainContent.SuspendLayout();
            this.tabPageMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWarning)).BeginInit();
            this.tabPageCharacters.SuspendLayout();
            this.panelCharacter.SuspendLayout();
            this.tabPageDialogText.SuspendLayout();
            this.tabPageEnemies.SuspendLayout();
            this.panelEnemy.SuspendLayout();
            this.tabPageGraphics.SuspendLayout();
            this.tabPageInventoryNames.SuspendLayout();
            this.tabPageItems.SuspendLayout();
            this.panelItem.SuspendLayout();
            this.tabPageLevelTables.SuspendLayout();
            this.groupBoxStatisticGrowth.SuspendLayout();
            this.tabPagePalettes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPalettePreview)).BeginInit();
            this.tabPageScript.SuspendLayout();
            this.tabPageShops.SuspendLayout();
            this.panelShop.SuspendLayout();
            this.tabPageWeapons.SuspendLayout();
            this.panelWeapon.SuspendLayout();
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
            this.menuItem1.Index=0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOpen,
            this.menuItem3,
            this.menuItemCreateIPSFile,
            this.menuItemChecksum,
            this.menuItem5,
            this.menuItemHomepage,
            this.menuItemAbout,
            this.menuItemThanks,
            this.menuItem8,
            this.menuItemExit});
            this.menuItem1.Text="&Aridia";
            // 
            // menuItemOpen
            // 
            this.menuItemOpen.Index=0;
            this.menuItemOpen.Text="&Open ROM..";
            this.menuItemOpen.Click+=new System.EventHandler(this.menuItemOpen_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index=1;
            this.menuItem3.Text="-";
            // 
            // menuItemCreateIPSFile
            // 
            this.menuItemCreateIPSFile.Enabled=false;
            this.menuItemCreateIPSFile.Index=2;
            this.menuItemCreateIPSFile.Text="Create &IPS File...";
            this.menuItemCreateIPSFile.Click+=new System.EventHandler(this.menuItemCreateIPSFile_Click);
            // 
            // menuItemChecksum
            // 
            this.menuItemChecksum.Enabled=false;
            this.menuItemChecksum.Index=3;
            this.menuItemChecksum.Text="&Repair Checksum";
            this.menuItemChecksum.Click+=new System.EventHandler(this.menuItemChecksum_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index=4;
            this.menuItem5.Text="-";
            // 
            // menuItemHomepage
            // 
            this.menuItemHomepage.Index=5;
            this.menuItemHomepage.Text="&Homepage..";
            this.menuItemHomepage.Click+=new System.EventHandler(this.menuItemHomepage_Click);
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index=6;
            this.menuItemAbout.Text="&About..";
            this.menuItemAbout.Click+=new System.EventHandler(this.menuItemAbout_Click);
            // 
            // menuItemThanks
            // 
            this.menuItemThanks.Index=7;
            this.menuItemThanks.Text="&Thanks..";
            this.menuItemThanks.Click+=new System.EventHandler(this.menuItemThanks_Click);
            // 
            // menuItem8
            // 
            this.menuItem8.Index=8;
            this.menuItem8.Text="-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index=9;
            this.menuItemExit.Text="E&xit";
            this.menuItemExit.Click+=new System.EventHandler(this.menuItemExit_Click);
            // 
            // listViewNavigate
            // 
            this.listViewNavigate.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
            this.listViewNavigate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader});
            this.listViewNavigate.FullRowSelect=true;
            this.listViewNavigate.HideSelection=false;
            this.listViewNavigate.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem13,
            listViewItem14,
            listViewItem15,
            listViewItem16,
            listViewItem17,
            listViewItem18,
            listViewItem19,
            listViewItem20,
            listViewItem21,
            listViewItem22,
            listViewItem23,
            listViewItem24});
            this.listViewNavigate.Location=new System.Drawing.Point(0,0);
            this.listViewNavigate.MultiSelect=false;
            this.listViewNavigate.Name="listViewNavigate";
            this.listViewNavigate.Scrollable=false;
            this.listViewNavigate.Size=new System.Drawing.Size(104,386);
            this.listViewNavigate.TabIndex=0;
            this.listViewNavigate.UseCompatibleStateImageBehavior=false;
            this.listViewNavigate.View=System.Windows.Forms.View.Details;
            this.listViewNavigate.ColumnClick+=new System.Windows.Forms.ColumnClickEventHandler(this.listViewNavigate_ColumnClick);
            this.listViewNavigate.SelectedIndexChanged+=new System.EventHandler(this.listViewNavigate_SelectedIndexChanged);
            // 
            // columnHeader
            // 
            this.columnHeader.Text="Pages";
            this.columnHeader.Width=203;
            // 
            // tabControlMainContent
            // 
            this.tabControlMainContent.Appearance=System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControlMainContent.Controls.Add(this.tabPageMain);
            this.tabControlMainContent.Controls.Add(this.tabPageCharacters);
            this.tabControlMainContent.Controls.Add(this.tabPageDialogText);
            this.tabControlMainContent.Controls.Add(this.tabPageEnemies);
            this.tabControlMainContent.Controls.Add(this.tabPageGraphics);
            this.tabControlMainContent.Controls.Add(this.tabPageInventoryNames);
            this.tabControlMainContent.Controls.Add(this.tabPageItems);
            this.tabControlMainContent.Controls.Add(this.tabPageLevelTables);
            this.tabControlMainContent.Controls.Add(this.tabPagePalettes);
            this.tabControlMainContent.Controls.Add(this.tabPageScript);
            this.tabControlMainContent.Controls.Add(this.tabPageShops);
            this.tabControlMainContent.Controls.Add(this.tabPageWeapons);
            this.tabControlMainContent.Location=new System.Drawing.Point(104,0);
            this.tabControlMainContent.Multiline=true;
            this.tabControlMainContent.Name="tabControlMainContent";
            this.tabControlMainContent.SelectedIndex=0;
            this.tabControlMainContent.Size=new System.Drawing.Size(490,390);
            this.tabControlMainContent.TabIndex=1;
            this.tabControlMainContent.SelectedIndexChanged+=new System.EventHandler(this.tabControlMainContent_SelectedIndexChanged);
            // 
            // tabPageMain
            // 
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
            this.tabPageMain.Controls.Add(this.buttonOpenRom);
            this.tabPageMain.Controls.Add(this.textBoxRomPath);
            this.tabPageMain.Controls.Add(this.labelRom);
            this.tabPageMain.Location=new System.Drawing.Point(4,49);
            this.tabPageMain.Name="tabPageMain";
            this.tabPageMain.Size=new System.Drawing.Size(482,337);
            this.tabPageMain.TabIndex=0;
            this.tabPageMain.Text="Main";
            this.tabPageMain.ToolTipText="Open a game ROM and regenerate checksum";
            // 
            // buttonCreateIPS
            // 
            this.buttonCreateIPS.Enabled=false;
            this.buttonCreateIPS.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreateIPS.Image=((System.Drawing.Image)(resources.GetObject("buttonCreateIPS.Image")));
            this.buttonCreateIPS.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCreateIPS.Location=new System.Drawing.Point(340,71);
            this.buttonCreateIPS.Name="buttonCreateIPS";
            this.buttonCreateIPS.Size=new System.Drawing.Size(124,32);
            this.buttonCreateIPS.TabIndex=15;
            this.buttonCreateIPS.Text="Create IPS File     ";
            this.buttonCreateIPS.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            this.buttonCreateIPS.Click+=new System.EventHandler(this.buttonCreateIPS_Click);
            // 
            // textBoxCalculatedChecksum
            // 
            this.textBoxCalculatedChecksum.Location=new System.Drawing.Point(340,137);
            this.textBoxCalculatedChecksum.Name="textBoxCalculatedChecksum";
            this.textBoxCalculatedChecksum.ReadOnly=true;
            this.textBoxCalculatedChecksum.Size=new System.Drawing.Size(124,20);
            this.textBoxCalculatedChecksum.TabIndex=12;
            // 
            // labelCalculatedChecksum
            // 
            this.labelCalculatedChecksum.Location=new System.Drawing.Point(214,137);
            this.labelCalculatedChecksum.Name="labelCalculatedChecksum";
            this.labelCalculatedChecksum.Size=new System.Drawing.Size(120,20);
            this.labelCalculatedChecksum.TabIndex=11;
            this.labelCalculatedChecksum.Text="Calculated Checksum: ";
            this.labelCalculatedChecksum.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxChecksum
            // 
            this.textBoxChecksum.Location=new System.Drawing.Point(340,111);
            this.textBoxChecksum.Name="textBoxChecksum";
            this.textBoxChecksum.ReadOnly=true;
            this.textBoxChecksum.Size=new System.Drawing.Size(124,20);
            this.textBoxChecksum.TabIndex=10;
            // 
            // labelChecksum
            // 
            this.labelChecksum.Location=new System.Drawing.Point(230,111);
            this.labelChecksum.Name="labelChecksum";
            this.labelChecksum.Size=new System.Drawing.Size(104,20);
            this.labelChecksum.TabIndex=9;
            this.labelChecksum.Text="Stored Checksum: ";
            this.labelChecksum.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxRomHeader
            // 
            this.textBoxRomHeader.Location=new System.Drawing.Point(112,45);
            this.textBoxRomHeader.Name="textBoxRomHeader";
            this.textBoxRomHeader.ReadOnly=true;
            this.textBoxRomHeader.Size=new System.Drawing.Size(352,20);
            this.textBoxRomHeader.TabIndex=8;
            // 
            // labelRomHeader
            // 
            this.labelRomHeader.Location=new System.Drawing.Point(8,44);
            this.labelRomHeader.Name="labelRomHeader";
            this.labelRomHeader.Size=new System.Drawing.Size(104,20);
            this.labelRomHeader.TabIndex=7;
            this.labelRomHeader.Text="ROM Header: ";
            this.labelRomHeader.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxWarningText
            // 
            this.textBoxWarningText.BorderStyle=System.Windows.Forms.BorderStyle.None;
            this.textBoxWarningText.Enabled=false;
            this.textBoxWarningText.Location=new System.Drawing.Point(8,277);
            this.textBoxWarningText.Multiline=true;
            this.textBoxWarningText.Name="textBoxWarningText";
            this.textBoxWarningText.Size=new System.Drawing.Size(456,56);
            this.textBoxWarningText.TabIndex=6;
            this.textBoxWarningText.Text=resources.GetString("textBoxWarningText.Text");
            // 
            // labelWarning
            // 
            this.labelWarning.Font=new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.labelWarning.Location=new System.Drawing.Point(97,255);
            this.labelWarning.Name="labelWarning";
            this.labelWarning.Size=new System.Drawing.Size(192,16);
            this.labelWarning.TabIndex=5;
            this.labelWarning.Text="Warnings";
            // 
            // pictureBoxWarning
            // 
            this.pictureBoxWarning.Image=((System.Drawing.Image)(resources.GetObject("pictureBoxWarning.Image")));
            this.pictureBoxWarning.Location=new System.Drawing.Point(11,191);
            this.pictureBoxWarning.Name="pictureBoxWarning";
            this.pictureBoxWarning.Size=new System.Drawing.Size(80,80);
            this.pictureBoxWarning.TabIndex=4;
            this.pictureBoxWarning.TabStop=false;
            // 
            // buttonChecksum
            // 
            this.buttonChecksum.Enabled=false;
            this.buttonChecksum.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonChecksum.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonChecksum.ImageIndex=2;
            this.buttonChecksum.ImageList=this.imageListIcons;
            this.buttonChecksum.Location=new System.Drawing.Point(340,163);
            this.buttonChecksum.Name="buttonChecksum";
            this.buttonChecksum.Size=new System.Drawing.Size(124,32);
            this.buttonChecksum.TabIndex=3;
            this.buttonChecksum.Text="Repair Checksum";
            this.buttonChecksum.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            this.buttonChecksum.Click+=new System.EventHandler(this.buttonChecksum_Click);
            // 
            // imageListIcons
            // 
            this.imageListIcons.ImageStream=((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListIcons.ImageStream")));
            this.imageListIcons.TransparentColor=System.Drawing.Color.Transparent;
            this.imageListIcons.Images.SetKeyName(0,"");
            this.imageListIcons.Images.SetKeyName(1,"");
            this.imageListIcons.Images.SetKeyName(2,"");
            this.imageListIcons.Images.SetKeyName(3,"");
            this.imageListIcons.Images.SetKeyName(4,"");
            // 
            // buttonOpenRom
            // 
            this.buttonOpenRom.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenRom.ImageIndex=0;
            this.buttonOpenRom.ImageList=this.imageListIcons;
            this.buttonOpenRom.Location=new System.Drawing.Point(432,16);
            this.buttonOpenRom.Name="buttonOpenRom";
            this.buttonOpenRom.Size=new System.Drawing.Size(32,20);
            this.buttonOpenRom.TabIndex=2;
            this.buttonOpenRom.Click+=new System.EventHandler(this.buttonOpenRom_Click);
            // 
            // textBoxRomPath
            // 
            this.textBoxRomPath.Location=new System.Drawing.Point(112,16);
            this.textBoxRomPath.Name="textBoxRomPath";
            this.textBoxRomPath.ReadOnly=true;
            this.textBoxRomPath.Size=new System.Drawing.Size(320,20);
            this.textBoxRomPath.TabIndex=1;
            // 
            // labelRom
            // 
            this.labelRom.Location=new System.Drawing.Point(8,15);
            this.labelRom.Name="labelRom";
            this.labelRom.Size=new System.Drawing.Size(104,20);
            this.labelRom.TabIndex=0;
            this.labelRom.Text="Editing ROM: ";
            this.labelRom.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageCharacters
            // 
            this.tabPageCharacters.Controls.Add(this.panelCharacter);
            this.tabPageCharacters.Controls.Add(this.comboBoxSelectCharacter);
            this.tabPageCharacters.Controls.Add(this.labelSelectCharacter);
            this.tabPageCharacters.Location=new System.Drawing.Point(4,49);
            this.tabPageCharacters.Name="tabPageCharacters";
            this.tabPageCharacters.Size=new System.Drawing.Size(482,337);
            this.tabPageCharacters.TabIndex=5;
            this.tabPageCharacters.Text="Characters";
            this.tabPageCharacters.ToolTipText="Edit character names and properties";
            // 
            // panelCharacter
            // 
            this.panelCharacter.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panelCharacter.Location=new System.Drawing.Point(8,48);
            this.panelCharacter.Name="panelCharacter";
            this.panelCharacter.Size=new System.Drawing.Size(466,280);
            this.panelCharacter.TabIndex=6;
            // 
            // comboBoxCharacterTechniqueOrder
            // 
            this.comboBoxCharacterTechniqueOrder.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniqueOrder.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.comboBoxCharacterTechniqueOrder.Location=new System.Drawing.Point(396,152);
            this.comboBoxCharacterTechniqueOrder.Name="comboBoxCharacterTechniqueOrder";
            this.comboBoxCharacterTechniqueOrder.Size=new System.Drawing.Size(50,21);
            this.comboBoxCharacterTechniqueOrder.TabIndex=40;
            this.comboBoxCharacterTechniqueOrder.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniqueOrder_Validating);
            // 
            // labelCharacterTechniqueOrder
            // 
            this.labelCharacterTechniqueOrder.Location=new System.Drawing.Point(350,150);
            this.labelCharacterTechniqueOrder.Name="labelCharacterTechniqueOrder";
            this.labelCharacterTechniqueOrder.Size=new System.Drawing.Size(40,21);
            this.labelCharacterTechniqueOrder.TabIndex=48;
            this.labelCharacterTechniqueOrder.Text="Order:";
            this.labelCharacterTechniqueOrder.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterTechniqueTime
            // 
            this.comboBoxCharacterTechniqueTime.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniqueTime.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.comboBoxCharacterTechniqueTime.Location=new System.Drawing.Point(290,150);
            this.comboBoxCharacterTechniqueTime.Name="comboBoxCharacterTechniqueTime";
            this.comboBoxCharacterTechniqueTime.Size=new System.Drawing.Size(50,21);
            this.comboBoxCharacterTechniqueTime.TabIndex=39;
            this.comboBoxCharacterTechniqueTime.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniqueTime_Validating);
            // 
            // labelCharacterTechniqueTime
            // 
            this.labelCharacterTechniqueTime.Location=new System.Drawing.Point(250,150);
            this.labelCharacterTechniqueTime.Name="labelCharacterTechniqueTime";
            this.labelCharacterTechniqueTime.Size=new System.Drawing.Size(38,21);
            this.labelCharacterTechniqueTime.TabIndex=46;
            this.labelCharacterTechniqueTime.Text="Time:";
            this.labelCharacterTechniqueTime.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterTechniqueHeal
            // 
            this.comboBoxCharacterTechniqueHeal.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniqueHeal.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.comboBoxCharacterTechniqueHeal.Location=new System.Drawing.Point(190,151);
            this.comboBoxCharacterTechniqueHeal.Name="comboBoxCharacterTechniqueHeal";
            this.comboBoxCharacterTechniqueHeal.Size=new System.Drawing.Size(50,21);
            this.comboBoxCharacterTechniqueHeal.TabIndex=38;
            this.comboBoxCharacterTechniqueHeal.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniqueHeal_Validating);
            // 
            // labelCharacterTechniqueHeal
            // 
            this.labelCharacterTechniqueHeal.Location=new System.Drawing.Point(152,151);
            this.labelCharacterTechniqueHeal.Name="labelCharacterTechniqueHeal";
            this.labelCharacterTechniqueHeal.Size=new System.Drawing.Size(32,21);
            this.labelCharacterTechniqueHeal.TabIndex=44;
            this.labelCharacterTechniqueHeal.Text="Heal:";
            this.labelCharacterTechniqueHeal.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterTechniqueMelee
            // 
            this.comboBoxCharacterTechniqueMelee.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniqueMelee.Items.AddRange(new object[] {
            "0",
            "3",
            "5",
            "7",
            "9",
            "11"});
            this.comboBoxCharacterTechniqueMelee.Location=new System.Drawing.Point(96,152);
            this.comboBoxCharacterTechniqueMelee.Name="comboBoxCharacterTechniqueMelee";
            this.comboBoxCharacterTechniqueMelee.Size=new System.Drawing.Size(50,21);
            this.comboBoxCharacterTechniqueMelee.TabIndex=37;
            this.comboBoxCharacterTechniqueMelee.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniqueMelee_Validating);
            // 
            // labelCharacterTechniqueMelee
            // 
            this.labelCharacterTechniqueMelee.Location=new System.Drawing.Point(48,152);
            this.labelCharacterTechniqueMelee.Name="labelCharacterTechniqueMelee";
            this.labelCharacterTechniqueMelee.Size=new System.Drawing.Size(40,21);
            this.labelCharacterTechniqueMelee.TabIndex=42;
            this.labelCharacterTechniqueMelee.Text="Melee:";
            this.labelCharacterTechniqueMelee.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // linkLabelItemEditing
            // 
            this.linkLabelItemEditing.LinkColor=System.Drawing.Color.Blue;
            this.linkLabelItemEditing.Location=new System.Drawing.Point(333,259);
            this.linkLabelItemEditing.Name="linkLabelItemEditing";
            this.linkLabelItemEditing.Size=new System.Drawing.Size(128,16);
            this.linkLabelItemEditing.TabIndex=42;
            this.linkLabelItemEditing.TabStop=true;
            this.linkLabelItemEditing.Text="Item Editing Limitations";
            this.linkLabelItemEditing.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            this.linkLabelItemEditing.VisitedLinkColor=System.Drawing.Color.Blue;
            this.linkLabelItemEditing.Click+=new System.EventHandler(this.linkLabelItemEditing_Click);
            // 
            // listViewCharacterItems
            // 
            this.listViewCharacterItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderCharacterItemHexString,
            this.columnHeaderCharacterItemItem,
            this.columnHeaderCharacterItemIsEquipped,
            this.columnHeaderCharacterItemWhereEquipped});
            this.listViewCharacterItems.ContextMenu=this.contextMenuCharacterMenu;
            this.listViewCharacterItems.FullRowSelect=true;
            this.listViewCharacterItems.HeaderStyle=System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listViewCharacterItems.HideSelection=false;
            this.listViewCharacterItems.Location=new System.Drawing.Point(96,176);
            this.listViewCharacterItems.MultiSelect=false;
            this.listViewCharacterItems.Name="listViewCharacterItems";
            this.listViewCharacterItems.Size=new System.Drawing.Size(365,80);
            this.listViewCharacterItems.TabIndex=41;
            this.listViewCharacterItems.UseCompatibleStateImageBehavior=false;
            this.listViewCharacterItems.View=System.Windows.Forms.View.Details;
            this.listViewCharacterItems.DoubleClick+=new System.EventHandler(this.listViewCharacterItems_DoubleClick);
            // 
            // columnHeaderCharacterItemHexString
            // 
            this.columnHeaderCharacterItemHexString.Text="Hex Value";
            this.columnHeaderCharacterItemHexString.Width=65;
            // 
            // columnHeaderCharacterItemItem
            // 
            this.columnHeaderCharacterItemItem.Text="Item";
            this.columnHeaderCharacterItemItem.Width=72;
            // 
            // columnHeaderCharacterItemIsEquipped
            // 
            this.columnHeaderCharacterItemIsEquipped.Text="Equipped";
            // 
            // columnHeaderCharacterItemWhereEquipped
            // 
            this.columnHeaderCharacterItemWhereEquipped.Text="Equipped Where";
            this.columnHeaderCharacterItemWhereEquipped.Width=94;
            // 
            // contextMenuCharacterMenu
            // 
            this.contextMenuCharacterMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemEditCharacterItem});
            this.contextMenuCharacterMenu.Popup+=new System.EventHandler(this.contextMenuCharacterMenu_Popup);
            // 
            // menuItemEditCharacterItem
            // 
            this.menuItemEditCharacterItem.Index=0;
            this.menuItemEditCharacterItem.Text="&Edit Selected Item";
            this.menuItemEditCharacterItem.Click+=new System.EventHandler(this.menuItemEditCharacterItem_Click);
            // 
            // labelCharacterItems
            // 
            this.labelCharacterItems.Location=new System.Drawing.Point(16,176);
            this.labelCharacterItems.Name="labelCharacterItems";
            this.labelCharacterItems.Size=new System.Drawing.Size(72,21);
            this.labelCharacterItems.TabIndex=36;
            this.labelCharacterItems.Text="Items:";
            this.labelCharacterItems.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterSpeed
            // 
            this.textBoxCharacterSpeed.Location=new System.Drawing.Point(96,128);
            this.textBoxCharacterSpeed.MaxLength=3;
            this.textBoxCharacterSpeed.Name="textBoxCharacterSpeed";
            this.textBoxCharacterSpeed.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterSpeed.TabIndex=33;
            this.textBoxCharacterSpeed.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxCharacterSpeed_Validating);
            // 
            // labelCharacterSpeed
            // 
            this.labelCharacterSpeed.Location=new System.Drawing.Point(16,128);
            this.labelCharacterSpeed.Name="labelCharacterSpeed";
            this.labelCharacterSpeed.Size=new System.Drawing.Size(72,21);
            this.labelCharacterSpeed.TabIndex=32;
            this.labelCharacterSpeed.Text="Speed:";
            this.labelCharacterSpeed.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterSkill
            // 
            this.textBoxCharacterSkill.Location=new System.Drawing.Point(316,104);
            this.textBoxCharacterSkill.MaxLength=3;
            this.textBoxCharacterSkill.Name="textBoxCharacterSkill";
            this.textBoxCharacterSkill.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterSkill.TabIndex=31;
            this.textBoxCharacterSkill.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxCharacterSkill_Validating);
            // 
            // labelCharacterSkill
            // 
            this.labelCharacterSkill.Location=new System.Drawing.Point(238,103);
            this.labelCharacterSkill.Name="labelCharacterSkill";
            this.labelCharacterSkill.Size=new System.Drawing.Size(72,21);
            this.labelCharacterSkill.TabIndex=30;
            this.labelCharacterSkill.Text="Skill:";
            this.labelCharacterSkill.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterLuck
            // 
            this.textBoxCharacterLuck.Location=new System.Drawing.Point(96,104);
            this.textBoxCharacterLuck.MaxLength=3;
            this.textBoxCharacterLuck.Name="textBoxCharacterLuck";
            this.textBoxCharacterLuck.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterLuck.TabIndex=29;
            this.textBoxCharacterLuck.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxCharacterLuck_Validating);
            // 
            // labelCharacterLuck
            // 
            this.labelCharacterLuck.Location=new System.Drawing.Point(16,104);
            this.labelCharacterLuck.Name="labelCharacterLuck";
            this.labelCharacterLuck.Size=new System.Drawing.Size(72,21);
            this.labelCharacterLuck.TabIndex=28;
            this.labelCharacterLuck.Text="Luck:";
            this.labelCharacterLuck.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterDefense
            // 
            this.textBoxCharacterDefense.Location=new System.Drawing.Point(316,80);
            this.textBoxCharacterDefense.MaxLength=3;
            this.textBoxCharacterDefense.Name="textBoxCharacterDefense";
            this.textBoxCharacterDefense.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterDefense.TabIndex=27;
            this.textBoxCharacterDefense.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxCharacterDefense_Validating);
            // 
            // labelCharacterDefense
            // 
            this.labelCharacterDefense.Location=new System.Drawing.Point(238,77);
            this.labelCharacterDefense.Name="labelCharacterDefense";
            this.labelCharacterDefense.Size=new System.Drawing.Size(72,21);
            this.labelCharacterDefense.TabIndex=26;
            this.labelCharacterDefense.Text="Defense:";
            this.labelCharacterDefense.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterAttack
            // 
            this.textBoxCharacterAttack.Location=new System.Drawing.Point(96,80);
            this.textBoxCharacterAttack.MaxLength=3;
            this.textBoxCharacterAttack.Name="textBoxCharacterAttack";
            this.textBoxCharacterAttack.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterAttack.TabIndex=25;
            this.textBoxCharacterAttack.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxCharacterAttack_Validating);
            // 
            // labelCharacterAttack
            // 
            this.labelCharacterAttack.Location=new System.Drawing.Point(16,80);
            this.labelCharacterAttack.Name="labelCharacterAttack";
            this.labelCharacterAttack.Size=new System.Drawing.Size(72,21);
            this.labelCharacterAttack.TabIndex=24;
            this.labelCharacterAttack.Text="Attack:";
            this.labelCharacterAttack.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterTechPoints
            // 
            this.textBoxCharacterTechPoints.Location=new System.Drawing.Point(316,57);
            this.textBoxCharacterTechPoints.MaxLength=3;
            this.textBoxCharacterTechPoints.Name="textBoxCharacterTechPoints";
            this.textBoxCharacterTechPoints.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterTechPoints.TabIndex=23;
            this.textBoxCharacterTechPoints.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxCharacterTechPoints_Validating);
            // 
            // labelCharacterTechPoints
            // 
            this.labelCharacterTechPoints.Location=new System.Drawing.Point(238,56);
            this.labelCharacterTechPoints.Name="labelCharacterTechPoints";
            this.labelCharacterTechPoints.Size=new System.Drawing.Size(72,21);
            this.labelCharacterTechPoints.TabIndex=22;
            this.labelCharacterTechPoints.Text="Tech Points:";
            this.labelCharacterTechPoints.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterHitPoints
            // 
            this.textBoxCharacterHitPoints.Location=new System.Drawing.Point(96,59);
            this.textBoxCharacterHitPoints.MaxLength=3;
            this.textBoxCharacterHitPoints.Name="textBoxCharacterHitPoints";
            this.textBoxCharacterHitPoints.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterHitPoints.TabIndex=21;
            this.textBoxCharacterHitPoints.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxCharacterHitPoints_Validating);
            // 
            // labelCharacterHitPoints
            // 
            this.labelCharacterHitPoints.Location=new System.Drawing.Point(7,56);
            this.labelCharacterHitPoints.Name="labelCharacterHitPoints";
            this.labelCharacterHitPoints.Size=new System.Drawing.Size(72,21);
            this.labelCharacterHitPoints.TabIndex=20;
            this.labelCharacterHitPoints.Text="Hit Points:";
            this.labelCharacterHitPoints.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterTechniques
            // 
            this.comboBoxCharacterTechniques.BackColor=System.Drawing.SystemColors.Window;
            this.comboBoxCharacterTechniques.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterTechniques.Location=new System.Drawing.Point(316,32);
            this.comboBoxCharacterTechniques.Name="comboBoxCharacterTechniques";
            this.comboBoxCharacterTechniques.Size=new System.Drawing.Size(130,21);
            this.comboBoxCharacterTechniques.TabIndex=19;
            this.comboBoxCharacterTechniques.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterTechniques_Validating);
            // 
            // labelCharacterTechPower
            // 
            this.labelCharacterTechPower.Location=new System.Drawing.Point(238,32);
            this.labelCharacterTechPower.Name="labelCharacterTechPower";
            this.labelCharacterTechPower.Size=new System.Drawing.Size(72,21);
            this.labelCharacterTechPower.TabIndex=18;
            this.labelCharacterTechPower.Text="Tech Power: ";
            this.labelCharacterTechPower.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxCharacterType
            // 
            this.comboBoxCharacterType.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCharacterType.Location=new System.Drawing.Point(96,32);
            this.comboBoxCharacterType.Name="comboBoxCharacterType";
            this.comboBoxCharacterType.Size=new System.Drawing.Size(130,21);
            this.comboBoxCharacterType.TabIndex=17;
            this.comboBoxCharacterType.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxCharacterType_Validating);
            // 
            // labelCharacterType
            // 
            this.labelCharacterType.Location=new System.Drawing.Point(16,32);
            this.labelCharacterType.Name="labelCharacterType";
            this.labelCharacterType.Size=new System.Drawing.Size(72,21);
            this.labelCharacterType.TabIndex=16;
            this.labelCharacterType.Text="Type: ";
            this.labelCharacterType.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterName
            // 
            this.textBoxCharacterName.Location=new System.Drawing.Point(316,6);
            this.textBoxCharacterName.MaxLength=4;
            this.textBoxCharacterName.Name="textBoxCharacterName";
            this.textBoxCharacterName.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterName.TabIndex=15;
            this.textBoxCharacterName.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxCharacterName_Validating);
            // 
            // labelCharacterName
            // 
            this.labelCharacterName.Location=new System.Drawing.Point(238,8);
            this.labelCharacterName.Name="labelCharacterName";
            this.labelCharacterName.Size=new System.Drawing.Size(72,21);
            this.labelCharacterName.TabIndex=14;
            this.labelCharacterName.Text="Name:";
            this.labelCharacterName.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxCharacterAddress
            // 
            this.textBoxCharacterAddress.Location=new System.Drawing.Point(96,8);
            this.textBoxCharacterAddress.Name="textBoxCharacterAddress";
            this.textBoxCharacterAddress.ReadOnly=true;
            this.textBoxCharacterAddress.Size=new System.Drawing.Size(130,20);
            this.textBoxCharacterAddress.TabIndex=13;
            // 
            // labelCharacterAddress
            // 
            this.labelCharacterAddress.Location=new System.Drawing.Point(16,8);
            this.labelCharacterAddress.Name="labelCharacterAddress";
            this.labelCharacterAddress.Size=new System.Drawing.Size(72,21);
            this.labelCharacterAddress.TabIndex=12;
            this.labelCharacterAddress.Text="Address:";
            this.labelCharacterAddress.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectCharacter
            // 
            this.comboBoxSelectCharacter.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectCharacter.Location=new System.Drawing.Point(128,16);
            this.comboBoxSelectCharacter.Name="comboBoxSelectCharacter";
            this.comboBoxSelectCharacter.Size=new System.Drawing.Size(346,21);
            this.comboBoxSelectCharacter.TabIndex=5;
            this.comboBoxSelectCharacter.SelectedIndexChanged+=new System.EventHandler(this.comboBoxSelectCharacter_SelectedIndexChanged);
            // 
            // labelSelectCharacter
            // 
            this.labelSelectCharacter.Location=new System.Drawing.Point(16,16);
            this.labelSelectCharacter.Name="labelSelectCharacter";
            this.labelSelectCharacter.Size=new System.Drawing.Size(104,21);
            this.labelSelectCharacter.TabIndex=4;
            this.labelSelectCharacter.Text="Select Character: ";
            this.labelSelectCharacter.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageDialogText
            // 
            this.tabPageDialogText.Controls.Add(this.listViewDialogText);
            this.tabPageDialogText.Location=new System.Drawing.Point(4,49);
            this.tabPageDialogText.Name="tabPageDialogText";
            this.tabPageDialogText.Size=new System.Drawing.Size(482,337);
            this.tabPageDialogText.TabIndex=2;
            this.tabPageDialogText.Text="Dialog Text";
            this.tabPageDialogText.ToolTipText="Edit dialog messages";
            // 
            // listViewDialogText
            // 
            this.listViewDialogText.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnDialogTextHeaderCurrentValue,
            this.columnDialogTextCategory,
            this.columnDialogTextDescription,
            this.columnDialogTextAddress,
            this.columncolumnDialogTextLength});
            this.listViewDialogText.LabelEdit=true;
            this.listViewDialogText.Location=new System.Drawing.Point(8,8);
            this.listViewDialogText.MultiSelect=false;
            this.listViewDialogText.Name="listViewDialogText";
            this.listViewDialogText.Size=new System.Drawing.Size(466,325);
            this.listViewDialogText.TabIndex=5;
            this.listViewDialogText.UseCompatibleStateImageBehavior=false;
            this.listViewDialogText.View=System.Windows.Forms.View.Details;
            this.listViewDialogText.AfterLabelEdit+=new System.Windows.Forms.LabelEditEventHandler(this.listViewDialogText_AfterLabelEdit);
            this.listViewDialogText.ColumnClick+=new System.Windows.Forms.ColumnClickEventHandler(this.listViewDialogText_ColumnClick);
            this.listViewDialogText.DoubleClick+=new System.EventHandler(this.listViewDialogText_DoubleClick);
            this.listViewDialogText.KeyPress+=new System.Windows.Forms.KeyPressEventHandler(this.listViewDialogText_KeyPress);
            // 
            // columnDialogTextHeaderCurrentValue
            // 
            this.columnDialogTextHeaderCurrentValue.Text="Current Value";
            this.columnDialogTextHeaderCurrentValue.Width=135;
            // 
            // columnDialogTextCategory
            // 
            this.columnDialogTextCategory.Text="Category";
            this.columnDialogTextCategory.Width=73;
            // 
            // columnDialogTextDescription
            // 
            this.columnDialogTextDescription.Text="Description";
            this.columnDialogTextDescription.Width=100;
            // 
            // columnDialogTextAddress
            // 
            this.columnDialogTextAddress.Text="Address";
            this.columnDialogTextAddress.Width=51;
            // 
            // columncolumnDialogTextLength
            // 
            this.columncolumnDialogTextLength.Text="Length";
            this.columncolumnDialogTextLength.Width=54;
            // 
            // tabPageEnemies
            // 
            this.tabPageEnemies.Controls.Add(this.panelEnemy);
            this.tabPageEnemies.Controls.Add(this.comboBoxSelectEnemy);
            this.tabPageEnemies.Controls.Add(this.labelSelectEnemy);
            this.tabPageEnemies.Location=new System.Drawing.Point(4,49);
            this.tabPageEnemies.Name="tabPageEnemies";
            this.tabPageEnemies.Size=new System.Drawing.Size(482,337);
            this.tabPageEnemies.TabIndex=6;
            this.tabPageEnemies.Text="Enemies";
            this.tabPageEnemies.ToolTipText="Edit enemy names and properties";
            // 
            // panelEnemy
            // 
            this.panelEnemy.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panelEnemy.Location=new System.Drawing.Point(8,40);
            this.panelEnemy.Name="panelEnemy";
            this.panelEnemy.Size=new System.Drawing.Size(466,208);
            this.panelEnemy.TabIndex=8;
            // 
            // textBoxEnemyEscapePercent
            // 
            this.textBoxEnemyEscapePercent.Location=new System.Drawing.Point(96,176);
            this.textBoxEnemyEscapePercent.MaxLength=2;
            this.textBoxEnemyEscapePercent.Name="textBoxEnemyEscapePercent";
            this.textBoxEnemyEscapePercent.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyEscapePercent.TabIndex=34;
            this.textBoxEnemyEscapePercent.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyEscapePercent_Validating);
            // 
            // labelEnemyEscapePercent
            // 
            this.labelEnemyEscapePercent.Location=new System.Drawing.Point(16,176);
            this.labelEnemyEscapePercent.Name="labelEnemyEscapePercent";
            this.labelEnemyEscapePercent.Size=new System.Drawing.Size(72,21);
            this.labelEnemyEscapePercent.TabIndex=46;
            this.labelEnemyEscapePercent.Text="Escape %:";
            this.labelEnemyEscapePercent.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyTechniqueCastPercent
            // 
            this.textBoxEnemyTechniqueCastPercent.Location=new System.Drawing.Point(318,56);
            this.textBoxEnemyTechniqueCastPercent.MaxLength=2;
            this.textBoxEnemyTechniqueCastPercent.Name="textBoxEnemyTechniqueCastPercent";
            this.textBoxEnemyTechniqueCastPercent.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyTechniqueCastPercent.TabIndex=25;
            this.textBoxEnemyTechniqueCastPercent.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyTechniqueCastPercent_Validating);
            // 
            // labelEnemyTechniqueCastPercent
            // 
            this.labelEnemyTechniqueCastPercent.Location=new System.Drawing.Point(238,56);
            this.labelEnemyTechniqueCastPercent.Name="labelEnemyTechniqueCastPercent";
            this.labelEnemyTechniqueCastPercent.Size=new System.Drawing.Size(73,21);
            this.labelEnemyTechniqueCastPercent.TabIndex=44;
            this.labelEnemyTechniqueCastPercent.Text="Tech Cast %:";
            this.labelEnemyTechniqueCastPercent.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyTechniqueLevel
            // 
            this.textBoxEnemyTechniqueLevel.Location=new System.Drawing.Point(96,56);
            this.textBoxEnemyTechniqueLevel.MaxLength=2;
            this.textBoxEnemyTechniqueLevel.Name="textBoxEnemyTechniqueLevel";
            this.textBoxEnemyTechniqueLevel.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyTechniqueLevel.TabIndex=24;
            this.textBoxEnemyTechniqueLevel.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyTechniqueLevel_Validating);
            // 
            // labelEnemyTechniqueLevel
            // 
            this.labelEnemyTechniqueLevel.Location=new System.Drawing.Point(16,56);
            this.labelEnemyTechniqueLevel.Name="labelEnemyTechniqueLevel";
            this.labelEnemyTechniqueLevel.Size=new System.Drawing.Size(72,21);
            this.labelEnemyTechniqueLevel.TabIndex=42;
            this.labelEnemyTechniqueLevel.Text="Tech Level:";
            this.labelEnemyTechniqueLevel.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyName
            // 
            this.textBoxEnemyName.Location=new System.Drawing.Point(318,152);
            this.textBoxEnemyName.MaxLength=4;
            this.textBoxEnemyName.Name="textBoxEnemyName";
            this.textBoxEnemyName.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyName.TabIndex=33;
            this.textBoxEnemyName.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyName_Validating);
            // 
            // labelEnemyName
            // 
            this.labelEnemyName.Location=new System.Drawing.Point(238,152);
            this.labelEnemyName.Name="labelEnemyName";
            this.labelEnemyName.Size=new System.Drawing.Size(73,21);
            this.labelEnemyName.TabIndex=40;
            this.labelEnemyName.Text="Name:";
            this.labelEnemyName.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyNameOffset
            // 
            this.textBoxEnemyNameOffset.Location=new System.Drawing.Point(96,152);
            this.textBoxEnemyNameOffset.Name="textBoxEnemyNameOffset";
            this.textBoxEnemyNameOffset.ReadOnly=true;
            this.textBoxEnemyNameOffset.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyNameOffset.TabIndex=32;
            // 
            // labelEnemyNameOffset
            // 
            this.labelEnemyNameOffset.Location=new System.Drawing.Point(16,152);
            this.labelEnemyNameOffset.Name="labelEnemyNameOffset";
            this.labelEnemyNameOffset.Size=new System.Drawing.Size(72,21);
            this.labelEnemyNameOffset.TabIndex=38;
            this.labelEnemyNameOffset.Text="Name Offset:";
            this.labelEnemyNameOffset.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyMeseta
            // 
            this.textBoxEnemyMeseta.Location=new System.Drawing.Point(318,128);
            this.textBoxEnemyMeseta.MaxLength=4;
            this.textBoxEnemyMeseta.Name="textBoxEnemyMeseta";
            this.textBoxEnemyMeseta.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyMeseta.TabIndex=31;
            this.textBoxEnemyMeseta.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyMeseta_Validating);
            // 
            // labelEnemyMeseta
            // 
            this.labelEnemyMeseta.Location=new System.Drawing.Point(238,128);
            this.labelEnemyMeseta.Name="labelEnemyMeseta";
            this.labelEnemyMeseta.Size=new System.Drawing.Size(73,21);
            this.labelEnemyMeseta.TabIndex=36;
            this.labelEnemyMeseta.Text="Meseta:";
            this.labelEnemyMeseta.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyExperience
            // 
            this.textBoxEnemyExperience.Location=new System.Drawing.Point(96,128);
            this.textBoxEnemyExperience.MaxLength=4;
            this.textBoxEnemyExperience.Name="textBoxEnemyExperience";
            this.textBoxEnemyExperience.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyExperience.TabIndex=30;
            this.textBoxEnemyExperience.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyExperience_Validating);
            // 
            // labelEnemyExperience
            // 
            this.labelEnemyExperience.Location=new System.Drawing.Point(16,128);
            this.labelEnemyExperience.Name="labelEnemyExperience";
            this.labelEnemyExperience.Size=new System.Drawing.Size(72,21);
            this.labelEnemyExperience.TabIndex=34;
            this.labelEnemyExperience.Text="Experience:";
            this.labelEnemyExperience.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemySpeed
            // 
            this.textBoxEnemySpeed.Location=new System.Drawing.Point(318,104);
            this.textBoxEnemySpeed.MaxLength=4;
            this.textBoxEnemySpeed.Name="textBoxEnemySpeed";
            this.textBoxEnemySpeed.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemySpeed.TabIndex=29;
            this.textBoxEnemySpeed.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemySpeed_Validating);
            // 
            // labelEnemySpeed
            // 
            this.labelEnemySpeed.Location=new System.Drawing.Point(238,104);
            this.labelEnemySpeed.Name="labelEnemySpeed";
            this.labelEnemySpeed.Size=new System.Drawing.Size(73,21);
            this.labelEnemySpeed.TabIndex=32;
            this.labelEnemySpeed.Text="Speed:";
            this.labelEnemySpeed.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyDefense
            // 
            this.textBoxEnemyDefense.Location=new System.Drawing.Point(96,104);
            this.textBoxEnemyDefense.MaxLength=4;
            this.textBoxEnemyDefense.Name="textBoxEnemyDefense";
            this.textBoxEnemyDefense.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyDefense.TabIndex=28;
            this.textBoxEnemyDefense.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyDefense_Validating);
            // 
            // labelEnemyDefense
            // 
            this.labelEnemyDefense.Location=new System.Drawing.Point(16,104);
            this.labelEnemyDefense.Name="labelEnemyDefense";
            this.labelEnemyDefense.Size=new System.Drawing.Size(72,21);
            this.labelEnemyDefense.TabIndex=30;
            this.labelEnemyDefense.Text="Defense:";
            this.labelEnemyDefense.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyAttack
            // 
            this.textBoxEnemyAttack.Location=new System.Drawing.Point(318,80);
            this.textBoxEnemyAttack.MaxLength=4;
            this.textBoxEnemyAttack.Name="textBoxEnemyAttack";
            this.textBoxEnemyAttack.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyAttack.TabIndex=27;
            this.textBoxEnemyAttack.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyAttack_Validating);
            // 
            // labelEnemyAttack
            // 
            this.labelEnemyAttack.Location=new System.Drawing.Point(238,80);
            this.labelEnemyAttack.Name="labelEnemyAttack";
            this.labelEnemyAttack.Size=new System.Drawing.Size(73,21);
            this.labelEnemyAttack.TabIndex=28;
            this.labelEnemyAttack.Text="Attack:";
            this.labelEnemyAttack.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyHitPoints
            // 
            this.textBoxEnemyHitPoints.Location=new System.Drawing.Point(96,80);
            this.textBoxEnemyHitPoints.MaxLength=4;
            this.textBoxEnemyHitPoints.Name="textBoxEnemyHitPoints";
            this.textBoxEnemyHitPoints.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyHitPoints.TabIndex=26;
            this.textBoxEnemyHitPoints.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxEnemyHitPoints_Validating);
            // 
            // labelEnemyHitPoints
            // 
            this.labelEnemyHitPoints.Location=new System.Drawing.Point(16,80);
            this.labelEnemyHitPoints.Name="labelEnemyHitPoints";
            this.labelEnemyHitPoints.Size=new System.Drawing.Size(72,21);
            this.labelEnemyHitPoints.TabIndex=26;
            this.labelEnemyHitPoints.Text="Hit Points:";
            this.labelEnemyHitPoints.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxEnemyTechnique
            // 
            this.comboBoxEnemyTechnique.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnemyTechnique.Location=new System.Drawing.Point(318,32);
            this.comboBoxEnemyTechnique.Name="comboBoxEnemyTechnique";
            this.comboBoxEnemyTechnique.Size=new System.Drawing.Size(130,21);
            this.comboBoxEnemyTechnique.TabIndex=23;
            this.comboBoxEnemyTechnique.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxEnemyTechnique_Validating);
            // 
            // labelEnemyTechnique
            // 
            this.labelEnemyTechnique.Location=new System.Drawing.Point(238,32);
            this.labelEnemyTechnique.Name="labelEnemyTechnique";
            this.labelEnemyTechnique.Size=new System.Drawing.Size(73,21);
            this.labelEnemyTechnique.TabIndex=24;
            this.labelEnemyTechnique.Text="Technique: ";
            this.labelEnemyTechnique.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxEnemyAnimation
            // 
            this.comboBoxEnemyAnimation.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnemyAnimation.Location=new System.Drawing.Point(96,32);
            this.comboBoxEnemyAnimation.Name="comboBoxEnemyAnimation";
            this.comboBoxEnemyAnimation.Size=new System.Drawing.Size(130,21);
            this.comboBoxEnemyAnimation.TabIndex=22;
            this.comboBoxEnemyAnimation.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxEnemyAnimation_Validating);
            // 
            // labelEnemyAnimation
            // 
            this.labelEnemyAnimation.Location=new System.Drawing.Point(16,32);
            this.labelEnemyAnimation.Name="labelEnemyAnimation";
            this.labelEnemyAnimation.Size=new System.Drawing.Size(72,21);
            this.labelEnemyAnimation.TabIndex=22;
            this.labelEnemyAnimation.Text="Animation: ";
            this.labelEnemyAnimation.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxEnemySpriteGroup
            // 
            this.comboBoxEnemySpriteGroup.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEnemySpriteGroup.Location=new System.Drawing.Point(318,8);
            this.comboBoxEnemySpriteGroup.Name="comboBoxEnemySpriteGroup";
            this.comboBoxEnemySpriteGroup.Size=new System.Drawing.Size(130,21);
            this.comboBoxEnemySpriteGroup.TabIndex=21;
            this.comboBoxEnemySpriteGroup.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxEnemySpriteGroup_Validating);
            // 
            // labelEnemySpriteGroup
            // 
            this.labelEnemySpriteGroup.Location=new System.Drawing.Point(238,8);
            this.labelEnemySpriteGroup.Name="labelEnemySpriteGroup";
            this.labelEnemySpriteGroup.Size=new System.Drawing.Size(73,21);
            this.labelEnemySpriteGroup.TabIndex=20;
            this.labelEnemySpriteGroup.Text="Sprite Group: ";
            this.labelEnemySpriteGroup.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxEnemyAddress
            // 
            this.textBoxEnemyAddress.Location=new System.Drawing.Point(96,8);
            this.textBoxEnemyAddress.Name="textBoxEnemyAddress";
            this.textBoxEnemyAddress.ReadOnly=true;
            this.textBoxEnemyAddress.Size=new System.Drawing.Size(130,20);
            this.textBoxEnemyAddress.TabIndex=13;
            // 
            // labelEnemyAddress
            // 
            this.labelEnemyAddress.Location=new System.Drawing.Point(16,8);
            this.labelEnemyAddress.Name="labelEnemyAddress";
            this.labelEnemyAddress.Size=new System.Drawing.Size(72,21);
            this.labelEnemyAddress.TabIndex=12;
            this.labelEnemyAddress.Text="Address:";
            this.labelEnemyAddress.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectEnemy
            // 
            this.comboBoxSelectEnemy.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectEnemy.Location=new System.Drawing.Point(104,16);
            this.comboBoxSelectEnemy.Name="comboBoxSelectEnemy";
            this.comboBoxSelectEnemy.Size=new System.Drawing.Size(370,21);
            this.comboBoxSelectEnemy.TabIndex=7;
            this.comboBoxSelectEnemy.SelectedIndexChanged+=new System.EventHandler(this.comboBoxSelectEnemy_SelectedIndexChanged);
            // 
            // labelSelectEnemy
            // 
            this.labelSelectEnemy.Location=new System.Drawing.Point(16,16);
            this.labelSelectEnemy.Name="labelSelectEnemy";
            this.labelSelectEnemy.Size=new System.Drawing.Size(80,21);
            this.labelSelectEnemy.TabIndex=6;
            this.labelSelectEnemy.Text="Select Enemy: ";
            this.labelSelectEnemy.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageGraphics
            // 
            this.tabPageGraphics.Controls.Add(this.buttonEditStatusFont);
            this.tabPageGraphics.Controls.Add(this.buttonCreditFont);
            this.tabPageGraphics.Controls.Add(this.buttonEditBorders);
            this.tabPageGraphics.Controls.Add(this.buttonEditFont);
            this.tabPageGraphics.Controls.Add(this.buttonEditTitleLogo);
            this.tabPageGraphics.Location=new System.Drawing.Point(4,49);
            this.tabPageGraphics.Name="tabPageGraphics";
            this.tabPageGraphics.Size=new System.Drawing.Size(482,337);
            this.tabPageGraphics.TabIndex=8;
            this.tabPageGraphics.Text="Graphics";
            this.tabPageGraphics.ToolTipText="Edit graphics";
            // 
            // buttonEditStatusFont
            // 
            this.buttonEditStatusFont.Enabled=false;
            this.buttonEditStatusFont.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonEditStatusFont.Image=((System.Drawing.Image)(resources.GetObject("buttonEditStatusFont.Image")));
            this.buttonEditStatusFont.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditStatusFont.Location=new System.Drawing.Point(24,144);
            this.buttonEditStatusFont.Name="buttonEditStatusFont";
            this.buttonEditStatusFont.Size=new System.Drawing.Size(152,48);
            this.buttonEditStatusFont.TabIndex=4;
            this.buttonEditStatusFont.Text="Status Font";
            this.buttonEditStatusFont.Click+=new System.EventHandler(this.buttonEditStatusFont_Click);
            // 
            // buttonCreditFont
            // 
            this.buttonCreditFont.Enabled=false;
            this.buttonCreditFont.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreditFont.Image=((System.Drawing.Image)(resources.GetObject("buttonCreditFont.Image")));
            this.buttonCreditFont.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCreditFont.Location=new System.Drawing.Point(184,88);
            this.buttonCreditFont.Name="buttonCreditFont";
            this.buttonCreditFont.Size=new System.Drawing.Size(152,48);
            this.buttonCreditFont.TabIndex=3;
            this.buttonCreditFont.Text="Credit Font";
            this.buttonCreditFont.Click+=new System.EventHandler(this.buttonCreditFont_Click);
            // 
            // buttonEditBorders
            // 
            this.buttonEditBorders.Enabled=false;
            this.buttonEditBorders.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonEditBorders.Image=((System.Drawing.Image)(resources.GetObject("buttonEditBorders.Image")));
            this.buttonEditBorders.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditBorders.Location=new System.Drawing.Point(24,88);
            this.buttonEditBorders.Name="buttonEditBorders";
            this.buttonEditBorders.Size=new System.Drawing.Size(152,48);
            this.buttonEditBorders.TabIndex=2;
            this.buttonEditBorders.Text="Borders";
            this.buttonEditBorders.Click+=new System.EventHandler(this.buttonEditBorders_Click);
            // 
            // buttonEditFont
            // 
            this.buttonEditFont.Enabled=false;
            this.buttonEditFont.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonEditFont.Image=((System.Drawing.Image)(resources.GetObject("buttonEditFont.Image")));
            this.buttonEditFont.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditFont.Location=new System.Drawing.Point(184,32);
            this.buttonEditFont.Name="buttonEditFont";
            this.buttonEditFont.Size=new System.Drawing.Size(152,48);
            this.buttonEditFont.TabIndex=1;
            this.buttonEditFont.Text="Font";
            this.buttonEditFont.Click+=new System.EventHandler(this.buttonEditFont_Click);
            // 
            // buttonEditTitleLogo
            // 
            this.buttonEditTitleLogo.Enabled=false;
            this.buttonEditTitleLogo.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonEditTitleLogo.Image=((System.Drawing.Image)(resources.GetObject("buttonEditTitleLogo.Image")));
            this.buttonEditTitleLogo.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonEditTitleLogo.Location=new System.Drawing.Point(24,32);
            this.buttonEditTitleLogo.Name="buttonEditTitleLogo";
            this.buttonEditTitleLogo.Size=new System.Drawing.Size(152,48);
            this.buttonEditTitleLogo.TabIndex=0;
            this.buttonEditTitleLogo.Text="Title Logo";
            this.buttonEditTitleLogo.Click+=new System.EventHandler(this.buttonEditTitleLogo_Click);
            // 
            // tabPageInventoryNames
            // 
            this.tabPageInventoryNames.Controls.Add(this.listViewInventoryNames);
            this.tabPageInventoryNames.Location=new System.Drawing.Point(4,49);
            this.tabPageInventoryNames.Name="tabPageInventoryNames";
            this.tabPageInventoryNames.Size=new System.Drawing.Size(482,337);
            this.tabPageInventoryNames.TabIndex=10;
            this.tabPageInventoryNames.Text="Inventory Names";
            // 
            // listViewInventoryNames
            // 
            this.listViewInventoryNames.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader7,
            this.columnHeader8});
            this.listViewInventoryNames.LabelEdit=true;
            this.listViewInventoryNames.Location=new System.Drawing.Point(8,5);
            this.listViewInventoryNames.MultiSelect=false;
            this.listViewInventoryNames.Name="listViewInventoryNames";
            this.listViewInventoryNames.Size=new System.Drawing.Size(466,323);
            this.listViewInventoryNames.TabIndex=6;
            this.listViewInventoryNames.UseCompatibleStateImageBehavior=false;
            this.listViewInventoryNames.View=System.Windows.Forms.View.Details;
            this.listViewInventoryNames.AfterLabelEdit+=new System.Windows.Forms.LabelEditEventHandler(this.listViewInventoryNames_AfterLabelEdit);
            this.listViewInventoryNames.ColumnClick+=new System.Windows.Forms.ColumnClickEventHandler(this.listViewInventoryNames_ColumnClick);
            this.listViewInventoryNames.DoubleClick+=new System.EventHandler(this.listViewInventoryNames_DoubleClick);
            this.listViewInventoryNames.KeyPress+=new System.Windows.Forms.KeyPressEventHandler(this.listViewInventoryNames_KeyPress);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text="Current Value";
            this.columnHeader2.Width=206;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text="Category";
            this.columnHeader3.Width=73;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text="Address";
            this.columnHeader7.Width=69;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text="Length";
            this.columnHeader8.Width=54;
            // 
            // tabPageItems
            // 
            this.tabPageItems.Controls.Add(this.panelItem);
            this.tabPageItems.Controls.Add(this.comboBoxSelectItem);
            this.tabPageItems.Controls.Add(this.labelSelectItem);
            this.tabPageItems.Location=new System.Drawing.Point(4,49);
            this.tabPageItems.Name="tabPageItems";
            this.tabPageItems.Size=new System.Drawing.Size(482,337);
            this.tabPageItems.TabIndex=3;
            this.tabPageItems.Text="Items";
            this.tabPageItems.ToolTipText="Edit item names and properties";
            // 
            // panelItem
            // 
            this.panelItem.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelItem.Controls.Add(this.textBoxItemAddress);
            this.panelItem.Controls.Add(this.labelItemAddress);
            this.panelItem.Controls.Add(this.textBoxItemEffectiveness);
            this.panelItem.Controls.Add(this.comboBoxItemTechnique);
            this.panelItem.Controls.Add(this.textBoxItemCost);
            this.panelItem.Controls.Add(this.labelItemEffectiveness);
            this.panelItem.Controls.Add(this.labelItemTechnique);
            this.panelItem.Controls.Add(this.labelItemCost);
            this.panelItem.Location=new System.Drawing.Point(8,48);
            this.panelItem.Name="panelItem";
            this.panelItem.Size=new System.Drawing.Size(335,112);
            this.panelItem.TabIndex=3;
            // 
            // textBoxItemAddress
            // 
            this.textBoxItemAddress.Location=new System.Drawing.Point(96,8);
            this.textBoxItemAddress.Name="textBoxItemAddress";
            this.textBoxItemAddress.ReadOnly=true;
            this.textBoxItemAddress.Size=new System.Drawing.Size(234,20);
            this.textBoxItemAddress.TabIndex=8;
            // 
            // labelItemAddress
            // 
            this.labelItemAddress.Location=new System.Drawing.Point(8,8);
            this.labelItemAddress.Name="labelItemAddress";
            this.labelItemAddress.Size=new System.Drawing.Size(80,21);
            this.labelItemAddress.TabIndex=7;
            this.labelItemAddress.Text="Address:";
            this.labelItemAddress.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxItemEffectiveness
            // 
            this.textBoxItemEffectiveness.Location=new System.Drawing.Point(96,80);
            this.textBoxItemEffectiveness.MaxLength=3;
            this.textBoxItemEffectiveness.Name="textBoxItemEffectiveness";
            this.textBoxItemEffectiveness.Size=new System.Drawing.Size(234,20);
            this.textBoxItemEffectiveness.TabIndex=6;
            this.textBoxItemEffectiveness.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxItemEffectiveness_Validating);
            // 
            // comboBoxItemTechnique
            // 
            this.comboBoxItemTechnique.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemTechnique.Location=new System.Drawing.Point(96,56);
            this.comboBoxItemTechnique.Name="comboBoxItemTechnique";
            this.comboBoxItemTechnique.Size=new System.Drawing.Size(234,21);
            this.comboBoxItemTechnique.TabIndex=5;
            this.comboBoxItemTechnique.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxItemTechnique_Validating);
            // 
            // textBoxItemCost
            // 
            this.textBoxItemCost.Location=new System.Drawing.Point(96,32);
            this.textBoxItemCost.MaxLength=5;
            this.textBoxItemCost.Name="textBoxItemCost";
            this.textBoxItemCost.Size=new System.Drawing.Size(234,20);
            this.textBoxItemCost.TabIndex=4;
            this.textBoxItemCost.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxItemCost_Validating);
            // 
            // labelItemEffectiveness
            // 
            this.labelItemEffectiveness.Location=new System.Drawing.Point(8,80);
            this.labelItemEffectiveness.Name="labelItemEffectiveness";
            this.labelItemEffectiveness.Size=new System.Drawing.Size(80,21);
            this.labelItemEffectiveness.TabIndex=3;
            this.labelItemEffectiveness.Text="Effectiveness: ";
            this.labelItemEffectiveness.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelItemTechnique
            // 
            this.labelItemTechnique.Location=new System.Drawing.Point(8,56);
            this.labelItemTechnique.Name="labelItemTechnique";
            this.labelItemTechnique.Size=new System.Drawing.Size(80,21);
            this.labelItemTechnique.TabIndex=2;
            this.labelItemTechnique.Text="Technique: ";
            this.labelItemTechnique.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelItemCost
            // 
            this.labelItemCost.Location=new System.Drawing.Point(8,32);
            this.labelItemCost.Name="labelItemCost";
            this.labelItemCost.Size=new System.Drawing.Size(80,21);
            this.labelItemCost.TabIndex=1;
            this.labelItemCost.Text="Cost: ";
            this.labelItemCost.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectItem
            // 
            this.comboBoxSelectItem.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectItem.Location=new System.Drawing.Point(96,16);
            this.comboBoxSelectItem.Name="comboBoxSelectItem";
            this.comboBoxSelectItem.Size=new System.Drawing.Size(247,21);
            this.comboBoxSelectItem.TabIndex=1;
            this.comboBoxSelectItem.SelectedIndexChanged+=new System.EventHandler(this.comboBoxSelectItem_SelectedIndexChanged);
            // 
            // labelSelectItem
            // 
            this.labelSelectItem.Location=new System.Drawing.Point(8,16);
            this.labelSelectItem.Name="labelSelectItem";
            this.labelSelectItem.Size=new System.Drawing.Size(80,21);
            this.labelSelectItem.TabIndex=0;
            this.labelSelectItem.Text="Select Item: ";
            this.labelSelectItem.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageLevelTables
            // 
            this.tabPageLevelTables.Controls.Add(this.groupBoxStatisticGrowth);
            this.tabPageLevelTables.Controls.Add(this.listViewLevelTable);
            this.tabPageLevelTables.Controls.Add(this.comboBoxLevelTable);
            this.tabPageLevelTables.Controls.Add(this.labelSelectLevelTable);
            this.tabPageLevelTables.Location=new System.Drawing.Point(4,49);
            this.tabPageLevelTables.Name="tabPageLevelTables";
            this.tabPageLevelTables.Size=new System.Drawing.Size(482,337);
            this.tabPageLevelTables.TabIndex=13;
            this.tabPageLevelTables.Text="Level Tables";
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
            this.groupBoxStatisticGrowth.Location=new System.Drawing.Point(16,48);
            this.groupBoxStatisticGrowth.Name="groupBoxStatisticGrowth";
            this.groupBoxStatisticGrowth.Size=new System.Drawing.Size(458,88);
            this.groupBoxStatisticGrowth.TabIndex=16;
            this.groupBoxStatisticGrowth.TabStop=false;
            this.groupBoxStatisticGrowth.Text="Statistic Growth";
            // 
            // comboBoxLevelTableSpeed
            // 
            this.comboBoxLevelTableSpeed.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableSpeed.Location=new System.Drawing.Point(388,52);
            this.comboBoxLevelTableSpeed.Name="comboBoxLevelTableSpeed";
            this.comboBoxLevelTableSpeed.Size=new System.Drawing.Size(64,21);
            this.comboBoxLevelTableSpeed.TabIndex=30;
            this.comboBoxLevelTableSpeed.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableSpeed_Validating);
            // 
            // labelLevelTableSpeed
            // 
            this.labelLevelTableSpeed.Location=new System.Drawing.Point(326,52);
            this.labelLevelTableSpeed.Name="labelLevelTableSpeed";
            this.labelLevelTableSpeed.Size=new System.Drawing.Size(56,21);
            this.labelLevelTableSpeed.TabIndex=35;
            this.labelLevelTableSpeed.Text="Speed: ";
            this.labelLevelTableSpeed.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableSkill
            // 
            this.comboBoxLevelTableSkill.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableSkill.Location=new System.Drawing.Point(388,19);
            this.comboBoxLevelTableSkill.Name="comboBoxLevelTableSkill";
            this.comboBoxLevelTableSkill.Size=new System.Drawing.Size(64,21);
            this.comboBoxLevelTableSkill.TabIndex=25;
            this.comboBoxLevelTableSkill.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableSkill_Validating);
            // 
            // labelLevelTableSkill
            // 
            this.labelLevelTableSkill.Location=new System.Drawing.Point(350,19);
            this.labelLevelTableSkill.Name="labelLevelTableSkill";
            this.labelLevelTableSkill.Size=new System.Drawing.Size(32,21);
            this.labelLevelTableSkill.TabIndex=34;
            this.labelLevelTableSkill.Text="Skill: ";
            this.labelLevelTableSkill.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableDamage
            // 
            this.comboBoxLevelTableDamage.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableDamage.Location=new System.Drawing.Point(240,52);
            this.comboBoxLevelTableDamage.Name="comboBoxLevelTableDamage";
            this.comboBoxLevelTableDamage.Size=new System.Drawing.Size(64,21);
            this.comboBoxLevelTableDamage.TabIndex=29;
            this.comboBoxLevelTableDamage.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableDamage_Validating);
            // 
            // labelLevelTableDamage
            // 
            this.labelLevelTableDamage.Location=new System.Drawing.Point(178,52);
            this.labelLevelTableDamage.Name="labelLevelTableDamage";
            this.labelLevelTableDamage.Size=new System.Drawing.Size(56,21);
            this.labelLevelTableDamage.TabIndex=33;
            this.labelLevelTableDamage.Text="Damage: ";
            this.labelLevelTableDamage.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableLuck
            // 
            this.comboBoxLevelTableLuck.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableLuck.Location=new System.Drawing.Point(269,19);
            this.comboBoxLevelTableLuck.Name="comboBoxLevelTableLuck";
            this.comboBoxLevelTableLuck.Size=new System.Drawing.Size(64,21);
            this.comboBoxLevelTableLuck.TabIndex=24;
            this.comboBoxLevelTableLuck.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableLuck_Validating);
            // 
            // labelLevelTableLuck
            // 
            this.labelLevelTableLuck.Location=new System.Drawing.Point(223,19);
            this.labelLevelTableLuck.Name="labelLevelTableLuck";
            this.labelLevelTableLuck.Size=new System.Drawing.Size(40,21);
            this.labelLevelTableLuck.TabIndex=32;
            this.labelLevelTableLuck.Text="Luck: ";
            this.labelLevelTableLuck.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableDefense
            // 
            this.comboBoxLevelTableDefense.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableDefense.Location=new System.Drawing.Point(93,52);
            this.comboBoxLevelTableDefense.Name="comboBoxLevelTableDefense";
            this.comboBoxLevelTableDefense.Size=new System.Drawing.Size(64,21);
            this.comboBoxLevelTableDefense.TabIndex=27;
            this.comboBoxLevelTableDefense.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableDefense_Validating);
            // 
            // labelLevelTableDefense
            // 
            this.labelLevelTableDefense.Location=new System.Drawing.Point(31,52);
            this.labelLevelTableDefense.Name="labelLevelTableDefense";
            this.labelLevelTableDefense.Size=new System.Drawing.Size(56,21);
            this.labelLevelTableDefense.TabIndex=31;
            this.labelLevelTableDefense.Text="Defense: ";
            this.labelLevelTableDefense.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableTP
            // 
            this.comboBoxLevelTableTP.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableTP.Location=new System.Drawing.Point(143,19);
            this.comboBoxLevelTableTP.Name="comboBoxLevelTableTP";
            this.comboBoxLevelTableTP.Size=new System.Drawing.Size(64,21);
            this.comboBoxLevelTableTP.TabIndex=23;
            this.comboBoxLevelTableTP.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableTP_Validating);
            // 
            // labelLevelTableTP
            // 
            this.labelLevelTableTP.Location=new System.Drawing.Point(113,19);
            this.labelLevelTableTP.Name="labelLevelTableTP";
            this.labelLevelTableTP.Size=new System.Drawing.Size(24,21);
            this.labelLevelTableTP.TabIndex=28;
            this.labelLevelTableTP.Text="TP: ";
            this.labelLevelTableTP.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxLevelTableHP
            // 
            this.comboBoxLevelTableHP.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTableHP.Location=new System.Drawing.Point(43,19);
            this.comboBoxLevelTableHP.Name="comboBoxLevelTableHP";
            this.comboBoxLevelTableHP.Size=new System.Drawing.Size(64,21);
            this.comboBoxLevelTableHP.TabIndex=22;
            this.comboBoxLevelTableHP.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxLevelTableHP_Validating);
            // 
            // labelLevelTableHP
            // 
            this.labelLevelTableHP.Location=new System.Drawing.Point(10,19);
            this.labelLevelTableHP.Name="labelLevelTableHP";
            this.labelLevelTableHP.Size=new System.Drawing.Size(32,21);
            this.labelLevelTableHP.TabIndex=26;
            this.labelLevelTableHP.Text="HP: ";
            this.labelLevelTableHP.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listViewLevelTable
            // 
            this.listViewLevelTable.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderXP,
            this.columnHeaderLevel,
            this.columnHeaderLevelTableAddress});
            this.listViewLevelTable.LabelEdit=true;
            this.listViewLevelTable.Location=new System.Drawing.Point(16,144);
            this.listViewLevelTable.MultiSelect=false;
            this.listViewLevelTable.Name="listViewLevelTable";
            this.listViewLevelTable.Size=new System.Drawing.Size(458,176);
            this.listViewLevelTable.TabIndex=15;
            this.listViewLevelTable.UseCompatibleStateImageBehavior=false;
            this.listViewLevelTable.View=System.Windows.Forms.View.Details;
            this.listViewLevelTable.AfterLabelEdit+=new System.Windows.Forms.LabelEditEventHandler(this.listViewLevelTable_AfterLabelEdit);
            this.listViewLevelTable.DoubleClick+=new System.EventHandler(this.listViewLevelTable_DoubleClick);
            this.listViewLevelTable.KeyPress+=new System.Windows.Forms.KeyPressEventHandler(this.listViewLevelTable_KeyPress);
            // 
            // columnHeaderXP
            // 
            this.columnHeaderXP.Text="XP";
            this.columnHeaderXP.Width=275;
            // 
            // columnHeaderLevel
            // 
            this.columnHeaderLevel.Text="Level";
            this.columnHeaderLevel.Width=51;
            // 
            // columnHeaderLevelTableAddress
            // 
            this.columnHeaderLevelTableAddress.Text="Address";
            this.columnHeaderLevelTableAddress.Width=54;
            // 
            // comboBoxLevelTable
            // 
            this.comboBoxLevelTable.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevelTable.Location=new System.Drawing.Point(120,16);
            this.comboBoxLevelTable.Name="comboBoxLevelTable";
            this.comboBoxLevelTable.Size=new System.Drawing.Size(354,21);
            this.comboBoxLevelTable.TabIndex=5;
            this.comboBoxLevelTable.SelectedIndexChanged+=new System.EventHandler(this.comboBoxLevelTable_SelectedIndexChanged);
            // 
            // labelSelectLevelTable
            // 
            this.labelSelectLevelTable.Location=new System.Drawing.Point(8,16);
            this.labelSelectLevelTable.Name="labelSelectLevelTable";
            this.labelSelectLevelTable.Size=new System.Drawing.Size(104,21);
            this.labelSelectLevelTable.TabIndex=4;
            this.labelSelectLevelTable.Text="Select Level Table: ";
            this.labelSelectLevelTable.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
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
            this.tabPagePalettes.Location=new System.Drawing.Point(4,49);
            this.tabPagePalettes.Name="tabPagePalettes";
            this.tabPagePalettes.Size=new System.Drawing.Size(482,337);
            this.tabPagePalettes.TabIndex=11;
            this.tabPagePalettes.Text="Palettes";
            // 
            // labelPaletteFind
            // 
            this.labelPaletteFind.Location=new System.Drawing.Point(8,304);
            this.labelPaletteFind.Name="labelPaletteFind";
            this.labelPaletteFind.Size=new System.Drawing.Size(32,20);
            this.labelPaletteFind.TabIndex=14;
            this.labelPaletteFind.Text="Find:";
            this.labelPaletteFind.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonPalettePrevious
            // 
            this.buttonPalettePrevious.Enabled=false;
            this.buttonPalettePrevious.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonPalettePrevious.Location=new System.Drawing.Point(371,304);
            this.buttonPalettePrevious.Name="buttonPalettePrevious";
            this.buttonPalettePrevious.Size=new System.Drawing.Size(68,20);
            this.buttonPalettePrevious.TabIndex=13;
            this.buttonPalettePrevious.Text="Previous";
            this.buttonPalettePrevious.Click+=new System.EventHandler(this.buttonPalettePrevious_Click);
            // 
            // buttonPaletteFind
            // 
            this.buttonPaletteFind.Enabled=false;
            this.buttonPaletteFind.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonPaletteFind.Location=new System.Drawing.Point(296,304);
            this.buttonPaletteFind.Name="buttonPaletteFind";
            this.buttonPaletteFind.Size=new System.Drawing.Size(68,20);
            this.buttonPaletteFind.TabIndex=12;
            this.buttonPaletteFind.Text="Next";
            this.buttonPaletteFind.Click+=new System.EventHandler(this.buttonPaletteFind_Click);
            // 
            // textBoxPaletteFind
            // 
            this.textBoxPaletteFind.Enabled=false;
            this.textBoxPaletteFind.Location=new System.Drawing.Point(48,304);
            this.textBoxPaletteFind.Name="textBoxPaletteFind";
            this.textBoxPaletteFind.Size=new System.Drawing.Size(240,20);
            this.textBoxPaletteFind.TabIndex=11;
            this.textBoxPaletteFind.TextChanged+=new System.EventHandler(this.textBoxPaletteFind_TextChanged);
            // 
            // labelPalettePreview
            // 
            this.labelPalettePreview.Location=new System.Drawing.Point(8,272);
            this.labelPalettePreview.Name="labelPalettePreview";
            this.labelPalettePreview.Size=new System.Drawing.Size(48,16);
            this.labelPalettePreview.TabIndex=10;
            this.labelPalettePreview.Text="Preview:";
            this.labelPalettePreview.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pictureBoxPalettePreview
            // 
            this.pictureBoxPalettePreview.Location=new System.Drawing.Point(64,272);
            this.pictureBoxPalettePreview.Name="pictureBoxPalettePreview";
            this.pictureBoxPalettePreview.Size=new System.Drawing.Size(224,14);
            this.pictureBoxPalettePreview.TabIndex=9;
            this.pictureBoxPalettePreview.TabStop=false;
            this.pictureBoxPalettePreview.Paint+=new System.Windows.Forms.PaintEventHandler(this.pictureBoxPalettePreview_Paint);
            // 
            // buttonLaunchPaletteEditor
            // 
            this.buttonLaunchPaletteEditor.Enabled=false;
            this.buttonLaunchPaletteEditor.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonLaunchPaletteEditor.Image=((System.Drawing.Image)(resources.GetObject("buttonLaunchPaletteEditor.Image")));
            this.buttonLaunchPaletteEditor.ImageAlign=System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLaunchPaletteEditor.Location=new System.Drawing.Point(296,264);
            this.buttonLaunchPaletteEditor.Name="buttonLaunchPaletteEditor";
            this.buttonLaunchPaletteEditor.Size=new System.Drawing.Size(143,32);
            this.buttonLaunchPaletteEditor.TabIndex=8;
            this.buttonLaunchPaletteEditor.Text="Launch Palette Editor";
            this.buttonLaunchPaletteEditor.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLaunchPaletteEditor.Click+=new System.EventHandler(this.buttonLaunchPaletteEditor_Click);
            // 
            // listViewPalettes
            // 
            this.listViewPalettes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader10});
            this.listViewPalettes.HideSelection=false;
            this.listViewPalettes.Location=new System.Drawing.Point(8,5);
            this.listViewPalettes.MultiSelect=false;
            this.listViewPalettes.Name="listViewPalettes";
            this.listViewPalettes.Size=new System.Drawing.Size(466,251);
            this.listViewPalettes.TabIndex=7;
            this.listViewPalettes.UseCompatibleStateImageBehavior=false;
            this.listViewPalettes.View=System.Windows.Forms.View.Details;
            this.listViewPalettes.ColumnClick+=new System.Windows.Forms.ColumnClickEventHandler(this.listViewPalettes_ColumnClick);
            this.listViewPalettes.SelectedIndexChanged+=new System.EventHandler(this.listViewPalettes_SelectedIndexChanged);
            this.listViewPalettes.DoubleClick+=new System.EventHandler(this.listViewPalettes_DoubleClick);
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text="Description";
            this.columnHeader6.Width=322;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text="Address";
            this.columnHeader10.Width=69;
            // 
            // tabPageScript
            // 
            this.tabPageScript.Controls.Add(this.labelFindScript);
            this.tabPageScript.Controls.Add(this.buttonFindScriptPrevious);
            this.tabPageScript.Controls.Add(this.buttonFindScriptNext);
            this.tabPageScript.Controls.Add(this.textBoxFindScript);
            this.tabPageScript.Controls.Add(this.listViewScript);
            this.tabPageScript.Location=new System.Drawing.Point(4,49);
            this.tabPageScript.Name="tabPageScript";
            this.tabPageScript.Size=new System.Drawing.Size(482,337);
            this.tabPageScript.TabIndex=1;
            this.tabPageScript.Text="Script";
            this.tabPageScript.ToolTipText="Edit scripted text";
            // 
            // labelFindScript
            // 
            this.labelFindScript.Location=new System.Drawing.Point(8,304);
            this.labelFindScript.Name="labelFindScript";
            this.labelFindScript.Size=new System.Drawing.Size(32,20);
            this.labelFindScript.TabIndex=10;
            this.labelFindScript.Text="Find:";
            this.labelFindScript.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonFindScriptPrevious
            // 
            this.buttonFindScriptPrevious.Enabled=false;
            this.buttonFindScriptPrevious.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonFindScriptPrevious.Location=new System.Drawing.Point(406,304);
            this.buttonFindScriptPrevious.Name="buttonFindScriptPrevious";
            this.buttonFindScriptPrevious.Size=new System.Drawing.Size(68,20);
            this.buttonFindScriptPrevious.TabIndex=9;
            this.buttonFindScriptPrevious.Text="Previous";
            this.buttonFindScriptPrevious.Click+=new System.EventHandler(this.buttonFindScriptPrevious_Click);
            // 
            // buttonFindScriptNext
            // 
            this.buttonFindScriptNext.Enabled=false;
            this.buttonFindScriptNext.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonFindScriptNext.Location=new System.Drawing.Point(332,304);
            this.buttonFindScriptNext.Name="buttonFindScriptNext";
            this.buttonFindScriptNext.Size=new System.Drawing.Size(68,20);
            this.buttonFindScriptNext.TabIndex=8;
            this.buttonFindScriptNext.Text="Next";
            this.buttonFindScriptNext.Click+=new System.EventHandler(this.buttonFindScriptNext_Click);
            // 
            // textBoxFindScript
            // 
            this.textBoxFindScript.Enabled=false;
            this.textBoxFindScript.Location=new System.Drawing.Point(48,304);
            this.textBoxFindScript.Name="textBoxFindScript";
            this.textBoxFindScript.Size=new System.Drawing.Size(278,20);
            this.textBoxFindScript.TabIndex=7;
            this.textBoxFindScript.TextChanged+=new System.EventHandler(this.textBoxFindScript_TextChanged);
            // 
            // listViewScript
            // 
            this.listViewScript.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader5});
            this.listViewScript.LabelEdit=true;
            this.listViewScript.Location=new System.Drawing.Point(8,8);
            this.listViewScript.MultiSelect=false;
            this.listViewScript.Name="listViewScript";
            this.listViewScript.Size=new System.Drawing.Size(466,288);
            this.listViewScript.TabIndex=6;
            this.listViewScript.UseCompatibleStateImageBehavior=false;
            this.listViewScript.View=System.Windows.Forms.View.Details;
            this.listViewScript.AfterLabelEdit+=new System.Windows.Forms.LabelEditEventHandler(this.listViewScript_AfterLabelEdit);
            this.listViewScript.ColumnClick+=new System.Windows.Forms.ColumnClickEventHandler(this.listViewScript_ColumnClick);
            this.listViewScript.DoubleClick+=new System.EventHandler(this.listViewScript_DoubleClick);
            this.listViewScript.KeyPress+=new System.Windows.Forms.KeyPressEventHandler(this.listViewScript_KeyPress);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text="Current Value";
            this.columnHeader1.Width=294;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text="Address";
            this.columnHeader4.Width=51;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text="Length";
            this.columnHeader5.Width=54;
            // 
            // tabPageShops
            // 
            this.tabPageShops.Controls.Add(this.panelShop);
            this.tabPageShops.Controls.Add(this.comboBoxSelectShop);
            this.tabPageShops.Controls.Add(this.labelSelectShop);
            this.tabPageShops.Location=new System.Drawing.Point(4,49);
            this.tabPageShops.Name="tabPageShops";
            this.tabPageShops.Size=new System.Drawing.Size(482,337);
            this.tabPageShops.TabIndex=12;
            this.tabPageShops.Text="Shops";
            // 
            // panelShop
            // 
            this.panelShop.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
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
            this.panelShop.Location=new System.Drawing.Point(24,48);
            this.panelShop.Name="panelShop";
            this.panelShop.Size=new System.Drawing.Size(356,160);
            this.panelShop.TabIndex=10;
            // 
            // comboBoxItem5
            // 
            this.comboBoxItem5.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem5.Location=new System.Drawing.Point(96,128);
            this.comboBoxItem5.Name="comboBoxItem5";
            this.comboBoxItem5.Size=new System.Drawing.Size(243,21);
            this.comboBoxItem5.TabIndex=19;
            this.comboBoxItem5.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxItem5_Validating);
            // 
            // labelItem5
            // 
            this.labelItem5.Location=new System.Drawing.Point(8,128);
            this.labelItem5.Name="labelItem5";
            this.labelItem5.Size=new System.Drawing.Size(80,21);
            this.labelItem5.TabIndex=18;
            this.labelItem5.Text="Item 5: ";
            this.labelItem5.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItem4
            // 
            this.comboBoxItem4.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem4.Location=new System.Drawing.Point(96,104);
            this.comboBoxItem4.Name="comboBoxItem4";
            this.comboBoxItem4.Size=new System.Drawing.Size(243,21);
            this.comboBoxItem4.TabIndex=17;
            this.comboBoxItem4.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxItem4_Validating);
            // 
            // labelItem4
            // 
            this.labelItem4.Location=new System.Drawing.Point(8,104);
            this.labelItem4.Name="labelItem4";
            this.labelItem4.Size=new System.Drawing.Size(80,21);
            this.labelItem4.TabIndex=16;
            this.labelItem4.Text="Item 4: ";
            this.labelItem4.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItem3
            // 
            this.comboBoxItem3.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem3.Location=new System.Drawing.Point(96,80);
            this.comboBoxItem3.Name="comboBoxItem3";
            this.comboBoxItem3.Size=new System.Drawing.Size(243,21);
            this.comboBoxItem3.TabIndex=15;
            this.comboBoxItem3.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxItem3_Validating);
            // 
            // labelItem3
            // 
            this.labelItem3.Location=new System.Drawing.Point(8,80);
            this.labelItem3.Name="labelItem3";
            this.labelItem3.Size=new System.Drawing.Size(80,21);
            this.labelItem3.TabIndex=14;
            this.labelItem3.Text="Item 3: ";
            this.labelItem3.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItem2
            // 
            this.comboBoxItem2.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem2.Location=new System.Drawing.Point(96,56);
            this.comboBoxItem2.Name="comboBoxItem2";
            this.comboBoxItem2.Size=new System.Drawing.Size(243,21);
            this.comboBoxItem2.TabIndex=13;
            this.comboBoxItem2.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxItem2_Validating);
            // 
            // labelItem2
            // 
            this.labelItem2.Location=new System.Drawing.Point(8,56);
            this.labelItem2.Name="labelItem2";
            this.labelItem2.Size=new System.Drawing.Size(80,21);
            this.labelItem2.TabIndex=12;
            this.labelItem2.Text="Item 2: ";
            this.labelItem2.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxShopAddress
            // 
            this.textBoxShopAddress.Location=new System.Drawing.Point(96,8);
            this.textBoxShopAddress.Name="textBoxShopAddress";
            this.textBoxShopAddress.ReadOnly=true;
            this.textBoxShopAddress.Size=new System.Drawing.Size(243,20);
            this.textBoxShopAddress.TabIndex=11;
            // 
            // labelShopAddress
            // 
            this.labelShopAddress.Location=new System.Drawing.Point(8,8);
            this.labelShopAddress.Name="labelShopAddress";
            this.labelShopAddress.Size=new System.Drawing.Size(80,21);
            this.labelShopAddress.TabIndex=7;
            this.labelShopAddress.Text="Address:";
            this.labelShopAddress.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxItem1
            // 
            this.comboBoxItem1.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItem1.Location=new System.Drawing.Point(96,32);
            this.comboBoxItem1.Name="comboBoxItem1";
            this.comboBoxItem1.Size=new System.Drawing.Size(243,21);
            this.comboBoxItem1.TabIndex=5;
            this.comboBoxItem1.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxItem1_Validating);
            // 
            // labelItem1
            // 
            this.labelItem1.Location=new System.Drawing.Point(8,32);
            this.labelItem1.Name="labelItem1";
            this.labelItem1.Size=new System.Drawing.Size(80,21);
            this.labelItem1.TabIndex=2;
            this.labelItem1.Text="Item 1: ";
            this.labelItem1.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectShop
            // 
            this.comboBoxSelectShop.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectShop.Location=new System.Drawing.Point(96,16);
            this.comboBoxSelectShop.Name="comboBoxSelectShop";
            this.comboBoxSelectShop.Size=new System.Drawing.Size(284,21);
            this.comboBoxSelectShop.TabIndex=9;
            this.comboBoxSelectShop.SelectedIndexChanged+=new System.EventHandler(this.comboBoxSelectShop_SelectedIndexChanged);
            // 
            // labelSelectShop
            // 
            this.labelSelectShop.Location=new System.Drawing.Point(16,16);
            this.labelSelectShop.Name="labelSelectShop";
            this.labelSelectShop.Size=new System.Drawing.Size(72,21);
            this.labelSelectShop.TabIndex=8;
            this.labelSelectShop.Text="Select Shop: ";
            this.labelSelectShop.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPageWeapons
            // 
            this.tabPageWeapons.Controls.Add(this.panelWeapon);
            this.tabPageWeapons.Controls.Add(this.comboBoxSelectWeapon);
            this.tabPageWeapons.Controls.Add(this.labelSelectWeapon);
            this.tabPageWeapons.Location=new System.Drawing.Point(4,49);
            this.tabPageWeapons.Name="tabPageWeapons";
            this.tabPageWeapons.Size=new System.Drawing.Size(482,337);
            this.tabPageWeapons.TabIndex=4;
            this.tabPageWeapons.Text="Weapons";
            this.tabPageWeapons.ToolTipText="Edit weapon names and properties";
            // 
            // panelWeapon
            // 
            this.panelWeapon.BorderStyle=System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWeapon.Controls.Add(this.comboBoxWeaponEquipBy);
            this.panelWeapon.Controls.Add(this.labelWeaponEquipBy);
            this.panelWeapon.Controls.Add(this.textBoxWeaponSpeed);
            this.panelWeapon.Controls.Add(this.labelWeaponSpeed);
            this.panelWeapon.Controls.Add(this.textBoxWeaponDefense);
            this.panelWeapon.Controls.Add(this.labelWeaponDefense);
            this.panelWeapon.Controls.Add(this.textBoxWeaponAttack);
            this.panelWeapon.Controls.Add(this.labelWeaponAttack);
            this.panelWeapon.Controls.Add(this.comboBoxWeaponAnimation);
            this.panelWeapon.Controls.Add(this.labelWeaponAnimation);
            this.panelWeapon.Controls.Add(this.textBoxWeaponAddress);
            this.panelWeapon.Controls.Add(this.labelWeaponAddress);
            this.panelWeapon.Controls.Add(this.comboBoxWeaponTechnique);
            this.panelWeapon.Controls.Add(this.textBoxWeaponCost);
            this.panelWeapon.Controls.Add(this.labelWeaponTechnique);
            this.panelWeapon.Controls.Add(this.labelWeaponCost);
            this.panelWeapon.Location=new System.Drawing.Point(8,48);
            this.panelWeapon.Name="panelWeapon";
            this.panelWeapon.Size=new System.Drawing.Size(330,208);
            this.panelWeapon.TabIndex=4;
            // 
            // comboBoxWeaponEquipBy
            // 
            this.comboBoxWeaponEquipBy.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWeaponEquipBy.Location=new System.Drawing.Point(96,176);
            this.comboBoxWeaponEquipBy.Name="comboBoxWeaponEquipBy";
            this.comboBoxWeaponEquipBy.Size=new System.Drawing.Size(229,21);
            this.comboBoxWeaponEquipBy.TabIndex=10;
            this.comboBoxWeaponEquipBy.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxWeaponEquipBy_Validating);
            // 
            // labelWeaponEquipBy
            // 
            this.labelWeaponEquipBy.Location=new System.Drawing.Point(8,176);
            this.labelWeaponEquipBy.Name="labelWeaponEquipBy";
            this.labelWeaponEquipBy.Size=new System.Drawing.Size(80,21);
            this.labelWeaponEquipBy.TabIndex=17;
            this.labelWeaponEquipBy.Text="Equip By: ";
            this.labelWeaponEquipBy.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxWeaponSpeed
            // 
            this.textBoxWeaponSpeed.Location=new System.Drawing.Point(96,152);
            this.textBoxWeaponSpeed.MaxLength=3;
            this.textBoxWeaponSpeed.Name="textBoxWeaponSpeed";
            this.textBoxWeaponSpeed.Size=new System.Drawing.Size(229,20);
            this.textBoxWeaponSpeed.TabIndex=9;
            this.textBoxWeaponSpeed.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxWeaponSpeed_Validating);
            // 
            // labelWeaponSpeed
            // 
            this.labelWeaponSpeed.Location=new System.Drawing.Point(8,152);
            this.labelWeaponSpeed.Name="labelWeaponSpeed";
            this.labelWeaponSpeed.Size=new System.Drawing.Size(80,21);
            this.labelWeaponSpeed.TabIndex=15;
            this.labelWeaponSpeed.Text="Speed: ";
            this.labelWeaponSpeed.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxWeaponDefense
            // 
            this.textBoxWeaponDefense.Location=new System.Drawing.Point(96,128);
            this.textBoxWeaponDefense.MaxLength=3;
            this.textBoxWeaponDefense.Name="textBoxWeaponDefense";
            this.textBoxWeaponDefense.Size=new System.Drawing.Size(229,20);
            this.textBoxWeaponDefense.TabIndex=8;
            this.textBoxWeaponDefense.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxWeaponDefense_Validating);
            // 
            // labelWeaponDefense
            // 
            this.labelWeaponDefense.Location=new System.Drawing.Point(8,128);
            this.labelWeaponDefense.Name="labelWeaponDefense";
            this.labelWeaponDefense.Size=new System.Drawing.Size(80,21);
            this.labelWeaponDefense.TabIndex=13;
            this.labelWeaponDefense.Text="Defense: ";
            this.labelWeaponDefense.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxWeaponAttack
            // 
            this.textBoxWeaponAttack.Location=new System.Drawing.Point(96,104);
            this.textBoxWeaponAttack.MaxLength=3;
            this.textBoxWeaponAttack.Name="textBoxWeaponAttack";
            this.textBoxWeaponAttack.Size=new System.Drawing.Size(229,20);
            this.textBoxWeaponAttack.TabIndex=7;
            this.textBoxWeaponAttack.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxWeaponAttack_Validating);
            // 
            // labelWeaponAttack
            // 
            this.labelWeaponAttack.Location=new System.Drawing.Point(8,104);
            this.labelWeaponAttack.Name="labelWeaponAttack";
            this.labelWeaponAttack.Size=new System.Drawing.Size(80,21);
            this.labelWeaponAttack.TabIndex=11;
            this.labelWeaponAttack.Text="Attack: ";
            this.labelWeaponAttack.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxWeaponAnimation
            // 
            this.comboBoxWeaponAnimation.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWeaponAnimation.Location=new System.Drawing.Point(96,80);
            this.comboBoxWeaponAnimation.Name="comboBoxWeaponAnimation";
            this.comboBoxWeaponAnimation.Size=new System.Drawing.Size(229,21);
            this.comboBoxWeaponAnimation.TabIndex=6;
            this.comboBoxWeaponAnimation.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxWeaponAnimation_Validating);
            // 
            // labelWeaponAnimation
            // 
            this.labelWeaponAnimation.Location=new System.Drawing.Point(8,80);
            this.labelWeaponAnimation.Name="labelWeaponAnimation";
            this.labelWeaponAnimation.Size=new System.Drawing.Size(80,21);
            this.labelWeaponAnimation.TabIndex=9;
            this.labelWeaponAnimation.Text="Animation: ";
            this.labelWeaponAnimation.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBoxWeaponAddress
            // 
            this.textBoxWeaponAddress.Location=new System.Drawing.Point(96,8);
            this.textBoxWeaponAddress.Name="textBoxWeaponAddress";
            this.textBoxWeaponAddress.ReadOnly=true;
            this.textBoxWeaponAddress.Size=new System.Drawing.Size(229,20);
            this.textBoxWeaponAddress.TabIndex=11;
            // 
            // labelWeaponAddress
            // 
            this.labelWeaponAddress.Location=new System.Drawing.Point(8,8);
            this.labelWeaponAddress.Name="labelWeaponAddress";
            this.labelWeaponAddress.Size=new System.Drawing.Size(80,21);
            this.labelWeaponAddress.TabIndex=7;
            this.labelWeaponAddress.Text="Address:";
            this.labelWeaponAddress.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxWeaponTechnique
            // 
            this.comboBoxWeaponTechnique.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxWeaponTechnique.Location=new System.Drawing.Point(96,56);
            this.comboBoxWeaponTechnique.Name="comboBoxWeaponTechnique";
            this.comboBoxWeaponTechnique.Size=new System.Drawing.Size(229,21);
            this.comboBoxWeaponTechnique.TabIndex=5;
            this.comboBoxWeaponTechnique.Validating+=new System.ComponentModel.CancelEventHandler(this.comboBoxWeaponTechnique_Validating);
            // 
            // textBoxWeaponCost
            // 
            this.textBoxWeaponCost.Location=new System.Drawing.Point(96,32);
            this.textBoxWeaponCost.MaxLength=5;
            this.textBoxWeaponCost.Name="textBoxWeaponCost";
            this.textBoxWeaponCost.Size=new System.Drawing.Size(229,20);
            this.textBoxWeaponCost.TabIndex=4;
            this.textBoxWeaponCost.Validating+=new System.ComponentModel.CancelEventHandler(this.textBoxWeaponCost_Validating);
            // 
            // labelWeaponTechnique
            // 
            this.labelWeaponTechnique.Location=new System.Drawing.Point(8,56);
            this.labelWeaponTechnique.Name="labelWeaponTechnique";
            this.labelWeaponTechnique.Size=new System.Drawing.Size(80,21);
            this.labelWeaponTechnique.TabIndex=2;
            this.labelWeaponTechnique.Text="Technique: ";
            this.labelWeaponTechnique.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labelWeaponCost
            // 
            this.labelWeaponCost.Location=new System.Drawing.Point(8,32);
            this.labelWeaponCost.Name="labelWeaponCost";
            this.labelWeaponCost.Size=new System.Drawing.Size(80,21);
            this.labelWeaponCost.TabIndex=1;
            this.labelWeaponCost.Text="Cost: ";
            this.labelWeaponCost.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBoxSelectWeapon
            // 
            this.comboBoxSelectWeapon.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectWeapon.Location=new System.Drawing.Point(96,16);
            this.comboBoxSelectWeapon.Name="comboBoxSelectWeapon";
            this.comboBoxSelectWeapon.Size=new System.Drawing.Size(242,21);
            this.comboBoxSelectWeapon.TabIndex=3;
            this.comboBoxSelectWeapon.SelectedIndexChanged+=new System.EventHandler(this.comboBoxSelectWeapon_SelectedIndexChanged);
            // 
            // labelSelectWeapon
            // 
            this.labelSelectWeapon.Location=new System.Drawing.Point(8,16);
            this.labelSelectWeapon.Name="labelSelectWeapon";
            this.labelSelectWeapon.Size=new System.Drawing.Size(80,21);
            this.labelSelectWeapon.TabIndex=2;
            this.labelSelectWeapon.Text="Select Item: ";
            this.labelSelectWeapon.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusBar
            // 
            this.statusBar.Location=new System.Drawing.Point(0,388);
            this.statusBar.Name="statusBar";
            this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.statusBarPanel});
            this.statusBar.ShowPanels=true;
            this.statusBar.Size=new System.Drawing.Size(594,16);
            this.statusBar.SizingGrip=false;
            this.statusBar.TabIndex=2;
            // 
            // statusBarPanel
            // 
            this.statusBarPanel.AutoSize=System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.statusBarPanel.Name="statusBarPanel";
            this.statusBarPanel.Text="No ROM open, please open a ROM from the Main tab or menu";
            this.statusBarPanel.ToolTipText="No ROM open, please open a ROM from the Main tab or menu";
            this.statusBarPanel.Width=594;
            // 
            // openFileRomDialog
            // 
            this.openFileRomDialog.DefaultExt="bin";
            this.openFileRomDialog.Filter="Phantasy Star III ROM Images (*.bin)|*.bin";
            this.openFileRomDialog.Title="Open Phantasy Star III ROM";
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize=new System.Drawing.Size(5,13);
            this.ClientSize=new System.Drawing.Size(594,404);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.tabControlMainContent);
            this.Controls.Add(this.listViewNavigate);
            this.FormBorderStyle=System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox=false;
            this.Menu=this.mainMenu;
            this.Name="MainForm";
            this.Text="Aridia 2.1";
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
            this.tabPageGraphics.ResumeLayout(false);
            this.tabPageInventoryNames.ResumeLayout(false);
            this.tabPageItems.ResumeLayout(false);
            this.panelItem.ResumeLayout(false);
            this.panelItem.PerformLayout();
            this.tabPageLevelTables.ResumeLayout(false);
            this.groupBoxStatisticGrowth.ResumeLayout(false);
            this.tabPagePalettes.ResumeLayout(false);
            this.tabPagePalettes.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPalettePreview)).EndInit();
            this.tabPageScript.ResumeLayout(false);
            this.tabPageScript.PerformLayout();
            this.tabPageShops.ResumeLayout(false);
            this.panelShop.ResumeLayout(false);
            this.panelShop.PerformLayout();
            this.tabPageWeapons.ResumeLayout(false);
            this.panelWeapon.ResumeLayout(false);
            this.panelWeapon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.statusBarPanel)).EndInit();
            this.ResumeLayout(false);

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
                    String fileName=this.openFileRomDialog.FileName;
					this.statusBarPanel.Text="Opening "+fileName+"..";
					//update romIO
					if(this.romIO!=null){ this.romIO.Dispose(); }
					this.romIO=new MDBinaryRomIO(fileName);
					String romHeader=this.romIO.readString(256,138);
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
					this.listViewNavigate.Enabled=true;
					this.listViewDialogText.Items.Clear();
					this.listViewInventoryNames.Items.Clear();
					this.listViewScript.Items.Clear();
					this.comboBoxItemTechnique.Items.Clear();
					this.comboBoxSelectItem.Items.Clear();
					this.comboBoxSelectWeapon.Items.Clear();
					this.comboBoxWeaponAnimation.Items.Clear();
					this.comboBoxWeaponEquipBy.Items.Clear();
					this.comboBoxWeaponTechnique.Items.Clear();
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

		private void listViewNavigate_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				//find tab that with name that matches selection
				if((listViewNavigate.SelectedItems!=null)&&(listViewNavigate.SelectedItems.Count>0))
				{
					ListViewItem item=listViewNavigate.SelectedItems[0];
					int tabIndex=-1;
					int counter=0;
					int size=this.tabControlMainContent.TabPages.Count;
					while((tabIndex<0)&&(counter<size))
					{
						if(this.tabControlMainContent.TabPages[counter].Text.Equals(item.Text))
						{
							tabIndex=counter;					
						}
						else
						{
							counter++;
						}
					}
					if((tabIndex>-1)&&(tabIndex<size)&&(tabIndex!=this.tabControlMainContent.SelectedIndex))
					{
						this.tabControlMainContent.SelectedIndex=tabIndex;
						this.listViewNavigate.Focus();
					}
				}
			}
			catch(Exception x)
			{
				this.errorHandler("changing the tab",x);
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
			System.Windows.Forms.MessageBox.Show(this,"Aridia - Phantasy Star III ROM Editor\n\n(c) 2007-2011 Hugues Johnson\nhttp://www.huguesjohnson.com/\n\nChecksum code based off Calculate_Checksum method in Gens\nhttp://sourceforge.net/projects/gens/","About Aridia",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
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

		private void listViewNavigate_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			AridiaUtils.sortListView(e,this.listViewNavigate);
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
						case "Inventory Names":
							if(this.listViewInventoryNames.Items.Count<1)
							{
								this.listViewInventoryNames.Items.AddRange(AridiaUtils.getInventoryListItems("Items",234050,234118,this.romIO));
								this.listViewInventoryNames.Items.AddRange(AridiaUtils.getInventoryListItems("Weapons",234119,234715,this.romIO));
								this.listViewInventoryNames.Items.AddRange(AridiaUtils.getInventoryListItems("Weapons",235504,235580,this.romIO));
								this.listViewInventoryNames.Items.AddRange(AridiaUtils.getInventoryListItems("Armor",234726,235503,this.romIO));
								this.listViewInventoryNames.Items.AddRange(AridiaUtils.getInventoryListItems("Gems",235581,235654,this.romIO));
								this.listViewInventoryNames.Items.AddRange(AridiaUtils.getInventoryListItems("Transport",235655,235694,this.romIO));
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
						case "Weapons":
							if(this.comboBoxSelectWeapon.Items.Count<1)
							{
								AridiaUtils.loadLookupValues(this.comboBoxSelectWeapon,"Weapon-Addresses");
								AridiaUtils.loadLookupValues(this.comboBoxWeaponAnimation,"Weapon-Animations");
								AridiaUtils.loadLookupValues(this.comboBoxWeaponEquipBy,"Weapon-EquipBy");
								AridiaUtils.loadLookupValues(this.comboBoxWeaponTechnique,"Weapon-Techniques");
							}
							break;
						case "Graphics":
							this.buttonEditBorders.Enabled=true;
							this.buttonEditFont.Enabled=true;
							this.buttonCreditFont.Enabled=true;
							this.buttonEditTitleLogo.Enabled=true;
							this.buttonEditStatusFont.Enabled=true;
							break;
						case "Palettes":
							if(this.listViewPalettes.Items.Count<1)
							{
								AridiaUtils.loadLookupValues(this.listViewPalettes,"Palette-Addresses");
								ColumnClickEventArgs args=new ColumnClickEventArgs(0);
								AridiaUtils.sortListView(args,this.listViewPalettes);
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
					}
				} 
				catch(Exception x)
				{
					this.errorHandler("changing to tab "+newTabName,x);
				}
			}
			//set the list view selection to the tab title
			int selectedIndex=-1;
			int count=this.listViewNavigate.Items.Count;
			int index=0;
			while((index<count)&&(selectedIndex==-1))
			{
				if(this.listViewNavigate.Items[index].Text.Equals(newTabName))
				{
					selectedIndex=index;
				}
				else
				{
					index++;
				}
			}
			if((selectedIndex>-1)&&(selectedIndex<count)&&(selectedIndex!=this.listViewNavigate.SelectedIndices[0]))
			{
				this.listViewNavigate.Items[selectedIndex].Selected=true;
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
						this.romIO.writeString(mdString);
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
				//item technique
				int technique=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Technique,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxItemTechnique,technique);
				//item effectiveness
				int effectiveness=this.romIO.readInteger(address+(int)Constants.ItemOffsets.Effectiveness,1);
				this.textBoxItemEffectiveness.Text=effectiveness.ToString();
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
						this.romIO.writeString(mdString);
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

		private void listViewInventoryNames_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			AridiaUtils.sortListView(e,this.listViewInventoryNames);
			this.Cursor=Cursors.Default;
		}

		private void listViewInventoryNames_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.listViewInventoryNames.SelectedItems.Count==1)
			{
				this.listViewInventoryNames.SelectedItems[0].BeginEdit();
			}
		}

		private void listViewInventoryNames_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if(this.listViewInventoryNames.SelectedItems.Count==1)
			{
				this.listViewInventoryNames.SelectedItems[0].BeginEdit();
			}
		}

		private void listViewInventoryNames_AfterLabelEdit(object sender, System.Windows.Forms.LabelEditEventArgs e)
		{
			if(e.Label!=null)
			{
				this.Cursor=Cursors.WaitCursor;
				try
				{
					ListViewItem selectedItem=this.listViewInventoryNames.SelectedItems[0];
					MDString mdString=new MDString();
					mdString.CurrentValue=e.Label;
					mdString.Address=int.Parse(selectedItem.SubItems[2].Text);
					mdString.NumBytes=int.Parse(selectedItem.SubItems[3].Text);
					if(AridiaUtils.validateMDString(mdString))
					{
						this.romIO.writeString(mdString);
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
					this.errorHandler("save an inventory item name",x);
					e.CancelEdit=true;
				}
				this.Cursor=Cursors.Default;
			}
		}

		private void comboBoxSelectWeapon_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue selectedItem=(LookupValue)this.comboBoxSelectWeapon.SelectedItem;
				int address=selectedItem.IntValue;
				//starting address
				this.textBoxWeaponAddress.Text=address.ToString();
				//cost
				int cost=this.romIO.readInteger(address+(int)Constants.WeaponOffsets.Cost,2);
				this.textBoxWeaponCost.Text=cost.ToString();
				//attack
				int attack=this.romIO.readInteger(address+(int)Constants.WeaponOffsets.Attack,1);
				this.textBoxWeaponAttack.Text=attack.ToString();
				//defense
				int defense=this.romIO.readInteger(address+(int)Constants.WeaponOffsets.Defense,1);
				this.textBoxWeaponDefense.Text=defense.ToString();
				//speed
				int speed=this.romIO.readInteger(address+(int)Constants.WeaponOffsets.Speed,1);
				this.textBoxWeaponSpeed.Text=speed.ToString();
				//technique
				int technique=this.romIO.readInteger(address+(int)Constants.WeaponOffsets.Technique,2);
				AridiaUtils.setComboBoxSelection(this.comboBoxWeaponTechnique,technique);
				//animation
				int animation=this.romIO.readInteger(address+(int)Constants.WeaponOffsets.Animation,2);
				AridiaUtils.setComboBoxSelection(this.comboBoxWeaponAnimation,animation);
				//equip by
				int equipby=this.romIO.readInteger(address+(int)Constants.WeaponOffsets.EquipBy,1);
				AridiaUtils.setComboBoxSelection(this.comboBoxWeaponEquipBy,equipby);
			}
			catch(Exception x)
			{
				this.errorHandler("selecting a weapon",x);
			}
			this.Cursor=Cursors.Default;
		}

		private void textBoxWeaponCost_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectWeapon.Text.Length<1)||(this.comboBoxSelectWeapon.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxWeaponCost.Text;
				mdInt.NumBytes=2;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxWeaponAddress.Text))+(int)Constants.WeaponOffsets.Cost;
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
					this.errorHandler("save the cost for a weapon",x);
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

		private void textBoxWeaponAttack_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectWeapon.Text.Length<1)||(this.comboBoxSelectWeapon.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxWeaponAttack.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxWeaponAddress.Text))+(int)Constants.WeaponOffsets.Attack;
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
					this.errorHandler("save the attack for a weapon",x);
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

		private void textBoxWeaponDefense_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectWeapon.Text.Length<1)||(this.comboBoxSelectWeapon.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxWeaponDefense.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxWeaponAddress.Text))+(int)Constants.WeaponOffsets.Defense;
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
					this.errorHandler("save the defense for a weapon",x);
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

		private void textBoxWeaponSpeed_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectWeapon.Text.Length<1)||(this.comboBoxSelectWeapon.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			MDInteger mdInt=new MDInteger();
			try
			{
				string newValue=textBoxWeaponSpeed.Text;
				mdInt.NumBytes=1;
				mdInt.CurrentValue=Convert.ToInt32(newValue);
				mdInt.Address=(Convert.ToInt32(this.textBoxWeaponAddress.Text))+(int)Constants.WeaponOffsets.Speed;
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
					this.errorHandler("save the speed for a weapon",x);
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

		private void comboBoxWeaponTechnique_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectWeapon.Text.Length<1)||(this.comboBoxSelectWeapon.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxWeaponTechnique.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.Address=(Convert.ToInt32(this.textBoxWeaponAddress.Text))+(int)Constants.WeaponOffsets.Technique;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the technique for a weapon",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxWeaponAnimation_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectWeapon.Text.Length<1)||(this.comboBoxSelectWeapon.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxWeaponAnimation.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=2;
				mdInt.Address=(Convert.ToInt32(this.textBoxWeaponAddress.Text))+(int)Constants.WeaponOffsets.Animation;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the animation for a weapon",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
		}

		private void comboBoxWeaponEquipBy_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if((this.comboBoxSelectWeapon.Text.Length<1)||(this.comboBoxSelectWeapon.Text.Length<1)){ return; }
			this.Cursor=Cursors.WaitCursor;
			try
			{
				LookupValue newValue=(LookupValue)this.comboBoxWeaponEquipBy.SelectedItem;
				MDInteger mdInt=new MDInteger();
				mdInt.NumBytes=1;
				mdInt.Address=(Convert.ToInt32(this.textBoxWeaponAddress.Text))+(int)Constants.WeaponOffsets.EquipBy;
				mdInt.CurrentValue=newValue.IntValue;
				this.romIO.writeInt(mdInt);
				this.statusBarPanel.Text="Wrote "+mdInt.CurrentValue+" to address "+mdInt.Address.ToString();
			}				
			catch(Exception x)
			{
				this.errorHandler("save the equip by for a weapon",x);
				e.Cancel=true;
			}
			this.Cursor=Cursors.Default;
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
				string name=this.romIO.readString(address+(int)Constants.CharacterOffsets.Name,4);
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
					this.romIO.writeString(mdString);
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
				String currentName=this.romIO.readString(nameLookup,256);
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
					this.romIO.writeString(mdString);
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

		private void buttonEditTitleLogo_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				TileEditorForm tileEditor=new TileEditorForm(this.romIO,(int)Constants.TileAddresses.LogoStart,(int)Constants.TileAddresses.LogoEnd,AridiaUtils.getLookupValueCollection("Palette-Addresses"));
				this.Cursor=Cursors.Default;
				tileEditor.ShowDialog(this);
			}
			catch(Exception x)
			{
				this.errorHandler("edit the title logo",x);
			}
		}

		private void buttonEditFont_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				TileEditorForm tileEditor=new TileEditorForm(this.romIO,(int)Constants.TileAddresses.FontStart,(int)Constants.TileAddresses.FontEnd,AridiaUtils.getLookupValueCollection("Palette-Addresses"));
				this.Cursor=Cursors.Default;
				tileEditor.ShowDialog(this);
			}
			catch(Exception x)
			{
				this.errorHandler("edit the font",x);
			}
		}

		private void buttonEditBorders_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				TileEditorForm tileEditor=new TileEditorForm(this.romIO,(int)Constants.TileAddresses.BorderStart,(int)Constants.TileAddresses.BorderEnd,AridiaUtils.getLookupValueCollection("Palette-Addresses"));
				this.Cursor=Cursors.Default;
				tileEditor.ShowDialog(this);
			}
			catch(Exception x)
			{
				this.errorHandler("edit the borders",x);
			}
		}

		private void buttonCreditFont_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				TileEditorForm tileEditor=new TileEditorForm(this.romIO,(int)Constants.TileAddresses.CreditFontStart,(int)Constants.TileAddresses.CreditFontEnd,AridiaUtils.getLookupValueCollection("Palette-Addresses"));
				this.Cursor=Cursors.Default;
				tileEditor.ShowDialog(this);
			}
			catch(Exception x)
			{
				this.errorHandler("edit the credit font",x);
			}
		}

		private void buttonEditStatusFont_Click(object sender, System.EventArgs e)
		{
			try
			{
				this.Cursor=Cursors.WaitCursor;
				TileEditorForm tileEditor=new TileEditorForm(this.romIO,(int)Constants.TileAddresses.StatusFontStart,(int)Constants.TileAddresses.StatusFontEnd,AridiaUtils.getLookupValueCollection("Palette-Addresses"));
				this.Cursor=Cursors.Default;
				tileEditor.ShowDialog(this);
			}
			catch(Exception x)
			{
				this.errorHandler("edit the status font",x);
			}
		}

		private void menuItemThanks_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(this,"Thanks to mr2 and TheKomrade for providing the hex values used in the application.\nThis program would not be possible without their invaluable contributions.\n\nThanks to Nic Olas for all the testing and feedback.\n\n'Sega Programming FAQ October 18, 1995, Sixth Edition - Final' by Henry Rieke was consulted for palette editing.","Thanks!",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Information);
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
	}
}
