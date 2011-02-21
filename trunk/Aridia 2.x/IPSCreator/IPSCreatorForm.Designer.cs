/*
IPSCreator: Utility to create an IPS file
Originally created for Aridia: Phantasy Star III ROM Editor
Copyright (c) 2011 Hugues Johnson

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

namespace com.huguesjohnson.IPSCreator
{
    public partial class IPSCreatorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components=null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if(disposing&&(components!=null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources=new System.ComponentModel.ComponentResourceManager(typeof(IPSCreatorForm));
            this.buttonOpenOriginalFile=new System.Windows.Forms.Button();
            this.textBoxOriginalFilePath=new System.Windows.Forms.TextBox();
            this.labelOriginalFile=new System.Windows.Forms.Label();
            this.buttonOpenModifiedFile=new System.Windows.Forms.Button();
            this.textBoxModifiedFilePath=new System.Windows.Forms.TextBox();
            this.labelModifiedFile=new System.Windows.Forms.Label();
            this.buttonOpenIPSFileToCreate=new System.Windows.Forms.Button();
            this.textBoxIPSFileToCreatePath=new System.Windows.Forms.TextBox();
            this.labelIPSFileToCreate=new System.Windows.Forms.Label();
            this.buttonCreateIPSFile=new System.Windows.Forms.Button();
            this.buttonClose=new System.Windows.Forms.Button();
            this.statusStrip=new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar=new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel=new System.Windows.Forms.ToolStripStatusLabel();
            this.openFileDialog=new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog=new System.Windows.Forms.SaveFileDialog();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpenOriginalFile
            // 
            this.buttonOpenOriginalFile.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenOriginalFile.Image=((System.Drawing.Image)(resources.GetObject("buttonOpenOriginalFile.Image")));
            this.buttonOpenOriginalFile.Location=new System.Drawing.Point(426,10);
            this.buttonOpenOriginalFile.Name="buttonOpenOriginalFile";
            this.buttonOpenOriginalFile.Size=new System.Drawing.Size(32,20);
            this.buttonOpenOriginalFile.TabIndex=5;
            this.buttonOpenOriginalFile.Click+=new System.EventHandler(this.buttonOpenOriginalFile_Click);
            // 
            // textBoxOriginalFilePath
            // 
            this.textBoxOriginalFilePath.Location=new System.Drawing.Point(106,10);
            this.textBoxOriginalFilePath.Name="textBoxOriginalFilePath";
            this.textBoxOriginalFilePath.ReadOnly=true;
            this.textBoxOriginalFilePath.Size=new System.Drawing.Size(320,20);
            this.textBoxOriginalFilePath.TabIndex=4;
            // 
            // labelOriginalFile
            // 
            this.labelOriginalFile.Location=new System.Drawing.Point(2,9);
            this.labelOriginalFile.Name="labelOriginalFile";
            this.labelOriginalFile.Size=new System.Drawing.Size(104,20);
            this.labelOriginalFile.TabIndex=3;
            this.labelOriginalFile.Text="Original File: ";
            this.labelOriginalFile.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonOpenModifiedFile
            // 
            this.buttonOpenModifiedFile.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenModifiedFile.Image=((System.Drawing.Image)(resources.GetObject("buttonOpenModifiedFile.Image")));
            this.buttonOpenModifiedFile.Location=new System.Drawing.Point(426,36);
            this.buttonOpenModifiedFile.Name="buttonOpenModifiedFile";
            this.buttonOpenModifiedFile.Size=new System.Drawing.Size(32,20);
            this.buttonOpenModifiedFile.TabIndex=8;
            this.buttonOpenModifiedFile.Click+=new System.EventHandler(this.buttonOpenModifiedFile_Click);
            // 
            // textBoxModifiedFilePath
            // 
            this.textBoxModifiedFilePath.Location=new System.Drawing.Point(106,36);
            this.textBoxModifiedFilePath.Name="textBoxModifiedFilePath";
            this.textBoxModifiedFilePath.ReadOnly=true;
            this.textBoxModifiedFilePath.Size=new System.Drawing.Size(320,20);
            this.textBoxModifiedFilePath.TabIndex=7;
            // 
            // labelModifiedFile
            // 
            this.labelModifiedFile.Location=new System.Drawing.Point(2,35);
            this.labelModifiedFile.Name="labelModifiedFile";
            this.labelModifiedFile.Size=new System.Drawing.Size(104,20);
            this.labelModifiedFile.TabIndex=6;
            this.labelModifiedFile.Text="Modified File: ";
            this.labelModifiedFile.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonOpenIPSFileToCreate
            // 
            this.buttonOpenIPSFileToCreate.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenIPSFileToCreate.Image=((System.Drawing.Image)(resources.GetObject("buttonOpenIPSFileToCreate.Image")));
            this.buttonOpenIPSFileToCreate.Location=new System.Drawing.Point(426,62);
            this.buttonOpenIPSFileToCreate.Name="buttonOpenIPSFileToCreate";
            this.buttonOpenIPSFileToCreate.Size=new System.Drawing.Size(32,20);
            this.buttonOpenIPSFileToCreate.TabIndex=11;
            this.buttonOpenIPSFileToCreate.Click+=new System.EventHandler(this.buttonOpenIPSFileToCreate_Click);
            // 
            // textBoxIPSFileToCreatePath
            // 
            this.textBoxIPSFileToCreatePath.Location=new System.Drawing.Point(106,62);
            this.textBoxIPSFileToCreatePath.Name="textBoxIPSFileToCreatePath";
            this.textBoxIPSFileToCreatePath.ReadOnly=true;
            this.textBoxIPSFileToCreatePath.Size=new System.Drawing.Size(320,20);
            this.textBoxIPSFileToCreatePath.TabIndex=10;
            // 
            // labelIPSFileToCreate
            // 
            this.labelIPSFileToCreate.Location=new System.Drawing.Point(2,61);
            this.labelIPSFileToCreate.Name="labelIPSFileToCreate";
            this.labelIPSFileToCreate.Size=new System.Drawing.Size(104,20);
            this.labelIPSFileToCreate.TabIndex=9;
            this.labelIPSFileToCreate.Text="IPS File to Create: ";
            this.labelIPSFileToCreate.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // buttonCreateIPSFile
            // 
            this.buttonCreateIPSFile.Enabled=false;
            this.buttonCreateIPSFile.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreateIPSFile.Location=new System.Drawing.Point(339,88);
            this.buttonCreateIPSFile.Name="buttonCreateIPSFile";
            this.buttonCreateIPSFile.Size=new System.Drawing.Size(118,39);
            this.buttonCreateIPSFile.TabIndex=12;
            this.buttonCreateIPSFile.Text="Create IPS File";
            this.buttonCreateIPSFile.UseVisualStyleBackColor=true;
            this.buttonCreateIPSFile.Click+=new System.EventHandler(this.buttonCreateIPSFile_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonClose.Location=new System.Drawing.Point(340,133);
            this.buttonClose.Name="buttonClose";
            this.buttonClose.Size=new System.Drawing.Size(118,39);
            this.buttonClose.TabIndex=13;
            this.buttonClose.Text="Close";
            this.buttonClose.UseVisualStyleBackColor=true;
            this.buttonClose.Click+=new System.EventHandler(this.buttonClose_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar,
            this.toolStripStatusLabel});
            this.statusStrip.Location=new System.Drawing.Point(0,183);
            this.statusStrip.Name="statusStrip";
            this.statusStrip.Size=new System.Drawing.Size(469,22);
            this.statusStrip.SizingGrip=false;
            this.statusStrip.TabIndex=14;
            this.statusStrip.Text="statusStrip1";
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.Name="toolStripProgressBar";
            this.toolStripProgressBar.Size=new System.Drawing.Size(100,16);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name="toolStripStatusLabel";
            this.toolStripStatusLabel.Size=new System.Drawing.Size(0,17);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter="IPS File (*.ips)|*.ips";
            this.saveFileDialog.Title="Destination IPS File to Create";
            // 
            // IPSCreatorForm
            // 
            this.AutoScaleDimensions=new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize=new System.Drawing.Size(469,205);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonCreateIPSFile);
            this.Controls.Add(this.buttonOpenIPSFileToCreate);
            this.Controls.Add(this.textBoxIPSFileToCreatePath);
            this.Controls.Add(this.labelIPSFileToCreate);
            this.Controls.Add(this.buttonOpenModifiedFile);
            this.Controls.Add(this.textBoxModifiedFilePath);
            this.Controls.Add(this.labelModifiedFile);
            this.Controls.Add(this.buttonOpenOriginalFile);
            this.Controls.Add(this.textBoxOriginalFilePath);
            this.Controls.Add(this.labelOriginalFile);
            this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox=false;
            this.MinimizeBox=false;
            this.Name="IPSCreatorForm";
            this.SizeGripStyle=System.Windows.Forms.SizeGripStyle.Show;
            this.Text="Really Simple IPS Creator";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenOriginalFile;
        private System.Windows.Forms.TextBox textBoxOriginalFilePath;
        private System.Windows.Forms.Label labelOriginalFile;
        private System.Windows.Forms.Button buttonOpenModifiedFile;
        private System.Windows.Forms.TextBox textBoxModifiedFilePath;
        private System.Windows.Forms.Label labelModifiedFile;
        private System.Windows.Forms.Button buttonOpenIPSFileToCreate;
        private System.Windows.Forms.TextBox textBoxIPSFileToCreatePath;
        private System.Windows.Forms.Label labelIPSFileToCreate;
        private System.Windows.Forms.Button buttonCreateIPSFile;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

