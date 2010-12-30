namespace IPSCreator
{
    partial class IPSCreatorForm
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
            this.buttonOpenRom=new System.Windows.Forms.Button();
            this.textBoxRomPath=new System.Windows.Forms.TextBox();
            this.labelRom=new System.Windows.Forms.Label();
            this.button1=new System.Windows.Forms.Button();
            this.textBox1=new System.Windows.Forms.TextBox();
            this.label1=new System.Windows.Forms.Label();
            this.button2=new System.Windows.Forms.Button();
            this.textBox2=new System.Windows.Forms.TextBox();
            this.label2=new System.Windows.Forms.Label();
            this.button3=new System.Windows.Forms.Button();
            this.button4=new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpenRom
            // 
            this.buttonOpenRom.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.buttonOpenRom.Image=((System.Drawing.Image)(resources.GetObject("buttonOpenRom.Image")));
            this.buttonOpenRom.Location=new System.Drawing.Point(426,10);
            this.buttonOpenRom.Name="buttonOpenRom";
            this.buttonOpenRom.Size=new System.Drawing.Size(32,20);
            this.buttonOpenRom.TabIndex=5;
            // 
            // textBoxRomPath
            // 
            this.textBoxRomPath.Location=new System.Drawing.Point(106,10);
            this.textBoxRomPath.Name="textBoxRomPath";
            this.textBoxRomPath.ReadOnly=true;
            this.textBoxRomPath.Size=new System.Drawing.Size(320,20);
            this.textBoxRomPath.TabIndex=4;
            // 
            // labelRom
            // 
            this.labelRom.Location=new System.Drawing.Point(2,9);
            this.labelRom.Name="labelRom";
            this.labelRom.Size=new System.Drawing.Size(104,20);
            this.labelRom.TabIndex=3;
            this.labelRom.Text="Original File: ";
            this.labelRom.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button1
            // 
            this.button1.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.button1.Image=((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location=new System.Drawing.Point(426,36);
            this.button1.Name="button1";
            this.button1.Size=new System.Drawing.Size(32,20);
            this.button1.TabIndex=8;
            // 
            // textBox1
            // 
            this.textBox1.Location=new System.Drawing.Point(106,36);
            this.textBox1.Name="textBox1";
            this.textBox1.ReadOnly=true;
            this.textBox1.Size=new System.Drawing.Size(320,20);
            this.textBox1.TabIndex=7;
            // 
            // label1
            // 
            this.label1.Location=new System.Drawing.Point(2,35);
            this.label1.Name="label1";
            this.label1.Size=new System.Drawing.Size(104,20);
            this.label1.TabIndex=6;
            this.label1.Text="Modified File: ";
            this.label1.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button2
            // 
            this.button2.FlatStyle=System.Windows.Forms.FlatStyle.Popup;
            this.button2.Image=((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location=new System.Drawing.Point(426,62);
            this.button2.Name="button2";
            this.button2.Size=new System.Drawing.Size(32,20);
            this.button2.TabIndex=11;
            // 
            // textBox2
            // 
            this.textBox2.Location=new System.Drawing.Point(106,62);
            this.textBox2.Name="textBox2";
            this.textBox2.ReadOnly=true;
            this.textBox2.Size=new System.Drawing.Size(320,20);
            this.textBox2.TabIndex=10;
            // 
            // label2
            // 
            this.label2.Location=new System.Drawing.Point(2,61);
            this.label2.Name="label2";
            this.label2.Size=new System.Drawing.Size(104,20);
            this.label2.TabIndex=9;
            this.label2.Text="IPS File to Create: ";
            this.label2.TextAlign=System.Drawing.ContentAlignment.MiddleRight;
            // 
            // button3
            // 
            this.button3.Location=new System.Drawing.Point(339,88);
            this.button3.Name="button3";
            this.button3.Size=new System.Drawing.Size(118,39);
            this.button3.TabIndex=12;
            this.button3.Text="Create IPS File";
            this.button3.UseVisualStyleBackColor=true;
            // 
            // button4
            // 
            this.button4.Location=new System.Drawing.Point(340,133);
            this.button4.Name="button4";
            this.button4.Size=new System.Drawing.Size(118,39);
            this.button4.TabIndex=13;
            this.button4.Text="Close";
            this.button4.UseVisualStyleBackColor=true;
            // 
            // IPSCreatorForm
            // 
            this.AutoScaleDimensions=new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode=System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize=new System.Drawing.Size(469,180);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOpenRom);
            this.Controls.Add(this.textBoxRomPath);
            this.Controls.Add(this.labelRom);
            this.Icon=((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox=false;
            this.MinimizeBox=false;
            this.Name="IPSCreatorForm";
            this.SizeGripStyle=System.Windows.Forms.SizeGripStyle.Hide;
            this.Text="Really Simple IPS Creator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpenRom;
        private System.Windows.Forms.TextBox textBoxRomPath;
        private System.Windows.Forms.Label labelRom;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

