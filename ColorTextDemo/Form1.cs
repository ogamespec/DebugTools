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

namespace TextDemo
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        Random rnd = new Random();

        private Color RandomColor()
        {
            return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if ( openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string[] lines = File.ReadAllLines(openFileDialog1.FileName);

                colorTextControl1.ClearText();

                foreach (string str in lines)
                {
                    colorTextControl1.AddString(str + "\n", RandomColor());
                }

                colorTextControl1.Invalidate();
            }

        }
    }
}
