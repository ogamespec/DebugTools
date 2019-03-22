using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.InteropServices;

using DebugTools;

namespace MemoryView
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32")]
        static extern bool AllocConsole();

        public Form1()
        {
            InitializeComponent();

#if DEBUG
            AllocConsole();
#endif
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            memoryViewControl1.Data = new byte[256];

            for (int i = 0; i < memoryViewControl1.Data.Length; i++)
            {
                memoryViewControl1.Data[i] = (byte)i;
            }

            memoryViewControl1.Invalidate();

            SetAddress(0);
            SetColumns(-1);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetAddress (UInt64 address)
        {
            memoryViewControl1.Address = address;
            toolStripTextBox1.Text = "0x" + address.ToString("X16");
            memoryViewControl1.Invalidate();
        }

        private void SetColumns (int num)
        {
            if (num < 0)
            {
                toolStripComboBox1.Text = "Auto";
            }
            else
            {
                toolStripComboBox1.Text = num.ToString();
            }
            memoryViewControl1.Columns = num;
            memoryViewControl1.Invalidate();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConvertColumnsAndSet();
        }

        private void toolStripComboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ( e.KeyCode == Keys.Enter)
            {
                ConvertColumnsAndSet();
            }
        }

        private void ConvertColumnsAndSet()
        {
            try
            {
                int num = Convert.ToInt32(toolStripComboBox1.Text);
                SetColumns(num);
            }
            catch
            {
                SetColumns(-1);
            }
        }

        private void ConvertAddressAndSet()
        {
            try
            {
                UInt64 address = Convert.ToUInt64(toolStripTextBox1.Text, 16);
                SetAddress(address);
            }
            catch
            {
                SetAddress(0);
            }
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ConvertAddressAndSet();
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            noDataToolStripMenuItem.Checked = false;
            byteIntegerToolStripMenuItem.Checked = false;
            byteIntegerToolStripMenuItem1.Checked = false;
            byteIntegerToolStripMenuItem2.Checked = false;
            byteIntegerToolStripMenuItem3.Checked = false;
            bitFloatingPointToolStripMenuItem.Checked = false;
            bitFloatingPointToolStripMenuItem1.Checked = false;

            bigEndianToolStripMenuItem.Enabled = true;
            hexademicalDispayToolStripMenuItem.Enabled = true;
            signedDisplayToolStripMenuItem.Enabled = true;
            unsignedDisplayToolStripMenuItem.Enabled = true;

            switch (memoryViewControl1.DataMode)
            {
                case DataMode.NoData:
                    noDataToolStripMenuItem.Checked = true;

                    bigEndianToolStripMenuItem.Enabled = false;
                    hexademicalDispayToolStripMenuItem.Enabled = false;
                    signedDisplayToolStripMenuItem.Enabled = false;
                    unsignedDisplayToolStripMenuItem.Enabled = false;
                    break;
                case DataMode.Byte:
                    byteIntegerToolStripMenuItem.Checked = true;

                    bigEndianToolStripMenuItem.Enabled = false;
                    break;
                case DataMode.Bytes2:
                    byteIntegerToolStripMenuItem1.Checked = true;
                    break;
                case DataMode.Bytes4:
                    byteIntegerToolStripMenuItem2.Checked = true;
                    break;
                case DataMode.Bytes8:
                    byteIntegerToolStripMenuItem3.Checked = true;
                    break;
                case DataMode.Float:
                    bitFloatingPointToolStripMenuItem.Checked = true;

                    hexademicalDispayToolStripMenuItem.Enabled = false;
                    signedDisplayToolStripMenuItem.Enabled = false;
                    unsignedDisplayToolStripMenuItem.Enabled = false;
                    break;
                case DataMode.Double:
                    bitFloatingPointToolStripMenuItem1.Checked = true;

                    hexademicalDispayToolStripMenuItem.Enabled = false;
                    signedDisplayToolStripMenuItem.Enabled = false;
                    unsignedDisplayToolStripMenuItem.Enabled = false;
                    break;
            }

            hexademicalDispayToolStripMenuItem.Checked = false;
            signedDisplayToolStripMenuItem.Checked = false;
            unsignedDisplayToolStripMenuItem.Checked = false;

            switch (memoryViewControl1.DataDisplayMode)
            {
                case DataDisplayMode.Hex:
                    hexademicalDispayToolStripMenuItem.Checked = true;
                    break;
                case DataDisplayMode.Signed:
                    signedDisplayToolStripMenuItem.Checked = true;
                    break;
                case DataDisplayMode.Unsigned:
                    unsignedDisplayToolStripMenuItem.Checked = true;
                    break;
            }

            bigEndianToolStripMenuItem.Checked = memoryViewControl1.BigEndian;

            noTextToolStripMenuItem.Checked = false;
            aNSITextToolStripMenuItem.Checked = false;
            unicodeTextToolStripMenuItem.Checked = false;

            switch (memoryViewControl1.TextMode)
            {
                case TextMode.NoText:
                    noTextToolStripMenuItem.Checked = true;
                    break;
                case TextMode.Ansi:
                    aNSITextToolStripMenuItem.Checked = true;
                    break;
                case TextMode.Unicode:
                    unicodeTextToolStripMenuItem.Checked = true;
                    break;
            }

        }

        private void noDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataMode = DataMode.NoData;
            memoryViewControl1.Invalidate();
        }

        private void byteIntegerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataMode = DataMode.Byte;
            memoryViewControl1.Invalidate();
        }

        private void byteIntegerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataMode = DataMode.Bytes2;
            memoryViewControl1.Invalidate();
        }

        private void byteIntegerToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataMode = DataMode.Bytes4;
            memoryViewControl1.Invalidate();
        }

        private void byteIntegerToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataMode = DataMode.Bytes8;
            memoryViewControl1.Invalidate();
        }

        private void bitFloatingPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataMode = DataMode.Float;
            memoryViewControl1.Invalidate();
        }

        private void bitFloatingPointToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataMode = DataMode.Double;
            memoryViewControl1.Invalidate();
        }

        private void bigEndianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.BigEndian = !bigEndianToolStripMenuItem.Checked;
            memoryViewControl1.Invalidate();
        }

        private void noTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.TextMode = TextMode.NoText;
            memoryViewControl1.Invalidate();
        }

        private void aNSITextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.TextMode = TextMode.Ansi;
            memoryViewControl1.Invalidate();
        }

        private void unicodeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.TextMode = TextMode.Unicode;
            memoryViewControl1.Invalidate();
        }

        private void hexademicalDispayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataDisplayMode = DataDisplayMode.Hex;
            memoryViewControl1.Invalidate();
        }

        private void signedDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataDisplayMode = DataDisplayMode.Signed;
            memoryViewControl1.Invalidate();
        }

        private void unsignedDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            memoryViewControl1.DataDisplayMode = DataDisplayMode.Unsigned;
            memoryViewControl1.Invalidate();
        }

        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ( openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                memoryViewControl1.Data = File.ReadAllBytes(openFileDialog1.FileName);
                memoryViewControl1.Invalidate();
            }
        }
    }
}
