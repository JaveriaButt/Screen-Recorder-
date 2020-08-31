using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screen_Recorder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
             Graphics g = Graphics.FromImage(bitmap);
             g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
            panel1.BackgroundImage = bitmap;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread trd = new Thread(cap);
            trd.Start();
        }
        public void cap()
        {
            while (true)
            {
                Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
                Graphics g = Graphics.FromImage(bitmap);
                g.CopyFromScreen(0, 0, 0, 0, bitmap.Size);
                panel1.BackgroundImage = bitmap;
                Thread.Sleep(1000);
            }

        }
    }
}
