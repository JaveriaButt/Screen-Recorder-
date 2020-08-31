using Accord.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Screen_Recorder
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        [Obsolete]
        private void Form2_Load(object sender, EventArgs e)
        {
            timer1 = new Timer();
            timer1.Interval=1;
            timer1.Tick+=Timer1_Tick;
 

        }
        Bitmap bp;
        Graphics gr;
        VideoFileWriter vf; 
        //Audio variables:
        [DllImport("winmm.dll", EntryPoint = "mciSendStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern int record(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);
        private void Timer1_Tick(object sender, EventArgs e)
        {
           
            bp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height);
            gr = Graphics.FromImage(bp);
            gr.CopyFromScreen(0, 0, 0, 0,bp.Size);
            pictureBox1.Image = bp;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; 
            vf.WriteVideoFrame(bp);
        }

       
        private void Button1_Click(object sender, EventArgs e)
        {
            vf = new VideoFileWriter();
            vf.Open(path, Screen.PrimaryScreen.Bounds.Width,Screen.PrimaryScreen.Bounds.Height,10, VideoCodec.MPEG4, 100000);
            record("open new Type waveaudio Alias recsound", "", 0, 0);
            record("record recsound", "", 0, 0);

            timer1.Start();
        }

        //Stop Recording...
        [Obsolete]
        private void Button2_Click(object sender, EventArgs e)
        { 
            timer1.Stop();
            //Save audio;
            string p = "save recsound D:\\Extra\\Screen Recorder\\Screen Recorder\\bin\\Debug\\mic.wav";
            record(p, "", 0, 0);
            record("close recsound", "", 0, 0);
            vf.Close();
            MergeAudVid();
        }
        //merge audio and video
        public void MergeAudVid()
        {
            //Merge audio and video:
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.WorkingDirectory= @"D:\Extra\Screen Recorder\Screen Recorder\bin\Debug\";
                startInfo.Arguments = @"/C ffmpeg  -i   video.mp4  -i   mic.wav  -shortest C:/Users/javer/Documents/Final.mp4"; 
                process.StartInfo = startInfo;
                process.Start();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : "+ex);
            }
        }

        //Pause Option
        private void Button3_Click(object sender, EventArgs e)
        {
            timer1.Interval = 900000000 ;

        }
        string path= @"D:\Extra\Screen Recorder\Screen Recorder\bin\Debug\video.mp4";
        //Browse Location..
        private void Button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
           // SaveFileDialog fd = new SaveFileDialog();
            if(fbd.ShowDialog()==DialogResult.OK)
            {
                //DateTimeKind.Local;
                path = fbd.SelectedPath + "\\"+"video" + ".avi";
            }
            textBox1.Text = path;
        }


        //Resume 
        private void Button5_Click(object sender, EventArgs e)
        {
            timer1.Interval=1;
        }
    }
}
