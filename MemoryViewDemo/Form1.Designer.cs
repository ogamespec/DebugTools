namespace MemoryView
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
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
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.noDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byteIntegerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byteIntegerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.byteIntegerToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.byteIntegerToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bitFloatingPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitFloatingPointToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.hexademicalDispayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signedDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsignedDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bigEndianToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.noTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aNSITextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unicodeTextToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryViewControl1 = new DebugTools.MemoryViewControl();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(676, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.loadFileToolStripMenuItem.Text = "Open file...";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(128, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "bin";
            this.openFileDialog1.Filter = "Binary files|*.bin|All files|*.*";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.memoryViewControl1);
            this.splitContainer1.Size = new System.Drawing.Size(676, 303);
            this.splitContainer1.SplitterDistance = 26;
            this.splitContainer1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.toolStripTextBox1,
            this.toolStripLabel2,
            this.toolStripComboBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(676, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(52, 22);
            this.toolStripLabel1.Text = "Address:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(300, 25);
            this.toolStripTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripTextBox1_KeyDown);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(58, 22);
            this.toolStripLabel2.Text = "Columns:";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Auto",
            "1",
            "2",
            "4",
            "8",
            "16",
            "32",
            "64"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 25);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            this.toolStripComboBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.toolStripComboBox1_KeyDown);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noDataToolStripMenuItem,
            this.byteIntegerToolStripMenuItem,
            this.byteIntegerToolStripMenuItem1,
            this.byteIntegerToolStripMenuItem2,
            this.byteIntegerToolStripMenuItem3,
            this.toolStripSeparator2,
            this.bitFloatingPointToolStripMenuItem,
            this.bitFloatingPointToolStripMenuItem1,
            this.toolStripSeparator3,
            this.hexademicalDispayToolStripMenuItem,
            this.signedDisplayToolStripMenuItem,
            this.unsignedDisplayToolStripMenuItem,
            this.bigEndianToolStripMenuItem,
            this.toolStripSeparator4,
            this.noTextToolStripMenuItem,
            this.aNSITextToolStripMenuItem,
            this.unicodeTextToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 330);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // noDataToolStripMenuItem
            // 
            this.noDataToolStripMenuItem.Name = "noDataToolStripMenuItem";
            this.noDataToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.noDataToolStripMenuItem.Text = "No Data";
            this.noDataToolStripMenuItem.Click += new System.EventHandler(this.noDataToolStripMenuItem_Click);
            // 
            // byteIntegerToolStripMenuItem
            // 
            this.byteIntegerToolStripMenuItem.Name = "byteIntegerToolStripMenuItem";
            this.byteIntegerToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.byteIntegerToolStripMenuItem.Text = "1-byte Integer";
            this.byteIntegerToolStripMenuItem.Click += new System.EventHandler(this.byteIntegerToolStripMenuItem_Click);
            // 
            // byteIntegerToolStripMenuItem1
            // 
            this.byteIntegerToolStripMenuItem1.Name = "byteIntegerToolStripMenuItem1";
            this.byteIntegerToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.byteIntegerToolStripMenuItem1.Text = "2-byte Integer";
            this.byteIntegerToolStripMenuItem1.Click += new System.EventHandler(this.byteIntegerToolStripMenuItem1_Click);
            // 
            // byteIntegerToolStripMenuItem2
            // 
            this.byteIntegerToolStripMenuItem2.Name = "byteIntegerToolStripMenuItem2";
            this.byteIntegerToolStripMenuItem2.Size = new System.Drawing.Size(182, 22);
            this.byteIntegerToolStripMenuItem2.Text = "4-byte Integer";
            this.byteIntegerToolStripMenuItem2.Click += new System.EventHandler(this.byteIntegerToolStripMenuItem2_Click);
            // 
            // byteIntegerToolStripMenuItem3
            // 
            this.byteIntegerToolStripMenuItem3.Name = "byteIntegerToolStripMenuItem3";
            this.byteIntegerToolStripMenuItem3.Size = new System.Drawing.Size(182, 22);
            this.byteIntegerToolStripMenuItem3.Text = "8-byte Integer";
            this.byteIntegerToolStripMenuItem3.Click += new System.EventHandler(this.byteIntegerToolStripMenuItem3_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(179, 6);
            // 
            // bitFloatingPointToolStripMenuItem
            // 
            this.bitFloatingPointToolStripMenuItem.Name = "bitFloatingPointToolStripMenuItem";
            this.bitFloatingPointToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.bitFloatingPointToolStripMenuItem.Text = "32-bit Floating Point";
            this.bitFloatingPointToolStripMenuItem.Click += new System.EventHandler(this.bitFloatingPointToolStripMenuItem_Click);
            // 
            // bitFloatingPointToolStripMenuItem1
            // 
            this.bitFloatingPointToolStripMenuItem1.Name = "bitFloatingPointToolStripMenuItem1";
            this.bitFloatingPointToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
            this.bitFloatingPointToolStripMenuItem1.Text = "64-bit Floating Point";
            this.bitFloatingPointToolStripMenuItem1.Click += new System.EventHandler(this.bitFloatingPointToolStripMenuItem1_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(179, 6);
            // 
            // hexademicalDispayToolStripMenuItem
            // 
            this.hexademicalDispayToolStripMenuItem.Name = "hexademicalDispayToolStripMenuItem";
            this.hexademicalDispayToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.hexademicalDispayToolStripMenuItem.Text = "Hexademical Dispay";
            this.hexademicalDispayToolStripMenuItem.Click += new System.EventHandler(this.hexademicalDispayToolStripMenuItem_Click);
            // 
            // signedDisplayToolStripMenuItem
            // 
            this.signedDisplayToolStripMenuItem.Name = "signedDisplayToolStripMenuItem";
            this.signedDisplayToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.signedDisplayToolStripMenuItem.Text = "Signed Display";
            this.signedDisplayToolStripMenuItem.Click += new System.EventHandler(this.signedDisplayToolStripMenuItem_Click);
            // 
            // unsignedDisplayToolStripMenuItem
            // 
            this.unsignedDisplayToolStripMenuItem.Name = "unsignedDisplayToolStripMenuItem";
            this.unsignedDisplayToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.unsignedDisplayToolStripMenuItem.Text = "Unsigned Display";
            this.unsignedDisplayToolStripMenuItem.Click += new System.EventHandler(this.unsignedDisplayToolStripMenuItem_Click);
            // 
            // bigEndianToolStripMenuItem
            // 
            this.bigEndianToolStripMenuItem.Name = "bigEndianToolStripMenuItem";
            this.bigEndianToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.bigEndianToolStripMenuItem.Text = "Big Endian";
            this.bigEndianToolStripMenuItem.Click += new System.EventHandler(this.bigEndianToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(179, 6);
            // 
            // noTextToolStripMenuItem
            // 
            this.noTextToolStripMenuItem.Name = "noTextToolStripMenuItem";
            this.noTextToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.noTextToolStripMenuItem.Text = "No Text";
            this.noTextToolStripMenuItem.Click += new System.EventHandler(this.noTextToolStripMenuItem_Click);
            // 
            // aNSITextToolStripMenuItem
            // 
            this.aNSITextToolStripMenuItem.Name = "aNSITextToolStripMenuItem";
            this.aNSITextToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.aNSITextToolStripMenuItem.Text = "ANSI Text";
            this.aNSITextToolStripMenuItem.Click += new System.EventHandler(this.aNSITextToolStripMenuItem_Click);
            // 
            // unicodeTextToolStripMenuItem
            // 
            this.unicodeTextToolStripMenuItem.Name = "unicodeTextToolStripMenuItem";
            this.unicodeTextToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.unicodeTextToolStripMenuItem.Text = "Unicode Text";
            this.unicodeTextToolStripMenuItem.Click += new System.EventHandler(this.unicodeTextToolStripMenuItem_Click);
            // 
            // memoryViewControl1
            // 
            this.memoryViewControl1.Address = ((ulong)(0ul));
            this.memoryViewControl1.AddressColor = System.Drawing.Color.Gray;
            this.memoryViewControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.memoryViewControl1.BigEndian = false;
            this.memoryViewControl1.Columns = -1;
            this.memoryViewControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.memoryViewControl1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.memoryViewControl1.Data = null;
            this.memoryViewControl1.DataColor = System.Drawing.Color.Silver;
            this.memoryViewControl1.DataDisplayMode = DebugTools.DataDisplayMode.Hex;
            this.memoryViewControl1.DataMode = DebugTools.DataMode.Byte;
            this.memoryViewControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.memoryViewControl1.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.memoryViewControl1.Location = new System.Drawing.Point(0, 0);
            this.memoryViewControl1.Name = "memoryViewControl1";
            this.memoryViewControl1.ScrollbarArrowColor = System.Drawing.Color.Silver;
            this.memoryViewControl1.ScrollbarColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.memoryViewControl1.ScrollbarHoverArrowColor = System.Drawing.Color.DodgerBlue;
            this.memoryViewControl1.Size = new System.Drawing.Size(676, 273);
            this.memoryViewControl1.TabIndex = 1;
            this.memoryViewControl1.Text = "memoryViewControl1";
            this.memoryViewControl1.TextMode = DebugTools.TextMode.Ansi;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 327);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Memory View Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private DebugTools.MemoryViewControl memoryViewControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem noDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byteIntegerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byteIntegerToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem byteIntegerToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem byteIntegerToolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem bitFloatingPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitFloatingPointToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem hexademicalDispayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signedDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsignedDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bigEndianToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem noTextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aNSITextToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unicodeTextToolStripMenuItem;
    }
}

