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

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.huguesjohnson.IPSCreator
{
    public partial class IPSCreatorForm:Form
    {
        public IPSCreatorForm()
        {
            InitializeComponent();
        }

        public IPSCreatorForm(string modifiedFilePath)
        {
            InitializeComponent();
            this.textBoxModifiedFilePath.Text=modifiedFilePath;
            //create a default IPS file path base on the source file
            string ipsPath=modifiedFilePath;
            int indexof=ipsPath.LastIndexOf(".");
            if(indexof>0)
            {
                ipsPath=ipsPath.Substring(0,indexof);           
            }
            ipsPath+=".ips";
            this.textBoxIPSFileToCreatePath.Text=ipsPath;
        }

        private void buttonOpenOriginalFile_Click(object sender,EventArgs e)
        {
            this.openFileDialog.Filter="Sega Genesis ROM Images (*.bin)|*.bin";
            this.openFileDialog.Title="Open original ROM Image"; 
            System.Windows.Forms.DialogResult result=this.openFileDialog.ShowDialog(this);
            if(result.Equals(System.Windows.Forms.DialogResult.OK))
            {
                this.textBoxOriginalFilePath.Text=this.openFileDialog.FileName;
            }
            this.validateCreateButtonEnabled();
        }

        private void validateCreateButtonEnabled()
        {
            this.buttonCreateIPSFile.Enabled=((this.textBoxIPSFileToCreatePath.Text.Length>0)&&(this.textBoxModifiedFilePath.Text.Length>0)&&(this.textBoxOriginalFilePath.Text.Length>0));
        }

        private void buttonOpenModifiedFile_Click(object sender,EventArgs e)
        {
            this.openFileDialog.Filter="Sega Genesis ROM Images (*.bin)|*.bin";
            this.openFileDialog.Title="Open modified ROM Image";
            System.Windows.Forms.DialogResult result=this.openFileDialog.ShowDialog(this);
            if(result.Equals(System.Windows.Forms.DialogResult.OK))
            {
                this.textBoxModifiedFilePath.Text=this.openFileDialog.FileName;
            }
            this.validateCreateButtonEnabled();
        }

        private void buttonOpenIPSFileToCreate_Click(object sender,EventArgs e)
        {
            if(this.textBoxIPSFileToCreatePath.Text.Length>0) 
            {
                this.saveFileDialog.FileName=this.textBoxIPSFileToCreatePath.Text;
            }
            System.Windows.Forms.DialogResult result=this.saveFileDialog.ShowDialog(this);
            if(result.Equals(System.Windows.Forms.DialogResult.OK))
            {
                this.textBoxIPSFileToCreatePath.Text=this.saveFileDialog.FileName;
            }
            this.validateCreateButtonEnabled();
        }

        private void buttonClose_Click(object sender,EventArgs e)
        {
            this.Close();
        }

        private void buttonCreateIPSFile_Click(object sender,EventArgs e)
        {
            //update UI
            this.toolStripProgressBar.Minimum=0;
            this.toolStripProgressBar.Maximum=100;
            this.buttonClose.Enabled=false;
            this.buttonCreateIPSFile.Enabled=false;
            this.Cursor=Cursors.WaitCursor;

            #region read the files
            this.toolStripStatusLabel.Text="Reading files...";
            this.toolStripProgressBar.Value=10;
            int fileLength=-1;
            //arrays to store the two files in memory
            byte[] original=null;
            byte[] modified=null;
            //streams to read the two files
            FileStream sourceStream=null;
            FileStream modifiedStream=null;
            try //compare the original and modified files
            {
                //open the file streams
                sourceStream=File.Open(this.textBoxOriginalFilePath.Text,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                modifiedStream=File.Open(this.textBoxModifiedFilePath.Text,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                //check if the files are the same length, this really won't work if they are not
                fileLength=(int)sourceStream.Length;
                if(modifiedStream.Length!=fileLength)
                {
                    throw(new Exception("File lengths are different, can not create IPS patch."));
                }
                //read the files into memory
                original=new byte[fileLength];
                sourceStream.Read(original,0,fileLength);
                modified=new byte[fileLength];
                modifiedStream.Read(modified,0,fileLength);
                //close the streams
                sourceStream.Close();
                modifiedStream.Close();
                this.toolStripProgressBar.Value=60;
            }
            catch(Exception x)
            {
                //show an error message
                System.Windows.Forms.MessageBox.Show(this,"Error reading the original and modified files:\n\n"+x.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                this.toolStripStatusLabel.Text="Error: "+x.Message;
            }
            finally 
            {
                //check for any open streams that need to be closed
                try{if(sourceStream!=null){sourceStream.Close();}}catch{}
                try{if(modifiedStream!=null){modifiedStream.Close();}}catch{}
            }
            #endregion
            
            #region compare the files
            this.toolStripStatusLabel.Text="Comparing files...";
            //stuff to store all the differences that are found
            ArrayList patchRecords=new ArrayList();
            //compare the two files byte by byte
            if((modified.Length==original.Length)&&(original.Length==fileLength)) 
            {
                bool recordInProgress=false;
                PatchRecord currentRecord=null;
                for(int address=0;address<fileLength;address++) 
                {
                    if(original[address]!=modified[address]) 
                    {
                        if(!recordInProgress)
                        {
                            currentRecord=new PatchRecord(address);
                            recordInProgress=true;
                        }
                    }
                    else if(recordInProgress) 
                    {
                        currentRecord.Data=new byte[address-currentRecord.Address];
                        Array.Copy(modified,currentRecord.Address,currentRecord.Data,0,address-currentRecord.Address);
                        patchRecords.Add(currentRecord);
                        recordInProgress=false;
                    }
                }
                //check if the last changes went up to the end of the file
                if(recordInProgress)
                {
                    currentRecord.Data=new byte[fileLength-currentRecord.Address];
                    Array.Copy(modified,currentRecord.Address,currentRecord.Data,0,fileLength-currentRecord.Address);
                    patchRecords.Add(currentRecord);
                }
            }
            this.toolStripProgressBar.Value=80;
            #endregion

            #region create the ips file
            this.toolStripStatusLabel.Text="Writing IPS file...";
            FileStream patchFileStream=null;
            BinaryWriter writer=null;
            try
            {
                patchFileStream=File.Open(this.textBoxIPSFileToCreatePath.Text,FileMode.Create,FileAccess.ReadWrite,FileShare.Read);
                writer=new BinaryWriter(patchFileStream);
                //write PATCH
                byte[] bytes={(byte)'P',(byte)'A',(byte)'T',(byte)'C',(byte)'H'};
                writer.Write(bytes);
                //write all the changes
                int count=patchRecords.Count;
                for(int index=0;index<count;index++)
                {
                    PatchRecord record=(PatchRecord)patchRecords[index];
                    byte[] address=BitConverter.GetBytes((Int32)record.Address);
                    int dataLength=record.Length;
                    byte[] length=BitConverter.GetBytes((Int16)dataLength);
                    byte[] data=new byte[dataLength];
                    for(int dataIndex=0;dataIndex<dataLength;dataIndex++) 
                    {
                        data[dataIndex]=record.Data[dataIndex];
                    }
                    /* write the record - a couple things to consider: 
                     * -In the IPS format address is only 3 bytes but GetBytes returns a 4 byte array
                     * -Need to write address and length in reverse order
                    */
                    writer.Write(address[2]);
                    writer.Write(address[1]);
                    writer.Write(address[0]);
                    writer.Write(length[1]);
                    writer.Write(length[0]);
                    writer.Write(data);
                }
                //add EOF
                byte[] eof={(byte)'E',(byte)'O',(byte)'F'};
                writer.Write(eof);
                //close the stream
                writer.Close();
                this.toolStripProgressBar.Value=100;
                this.toolStripStatusLabel.Text="Done";
            }
            catch(Exception x)
            {
                //show an error message
                System.Windows.Forms.MessageBox.Show(this,"Error writing the file :\n\n"+x.Message,"Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                this.toolStripStatusLabel.Text="Error: "+x.Message;
            }
            finally 
            {
                //check for any open streams that need to be closed
                try
                {
                    if(writer!=null){writer.Close();}
                    else if(patchFileStream!=null){patchFileStream.Close();}
                }
                catch{ }
            }
            #endregion

            //update UI
            this.toolStripProgressBar.Value=0;
            this.buttonClose.Enabled=true;
            this.Cursor=Cursors.Default;
        }
    }
}
