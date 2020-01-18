using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using SpeechLib;
using System.Drawing.Imaging;
using System.Threading;



namespace xiaoyuan1._0
{
    public partial class Formmain : Form
    {
        [DllImport("USER32.DLL")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);//导入模拟键盘的方法
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetForegroundWindow();   //WINAPI 获取当前活动窗体的句柄
        SpVoice voice = new SpVoice();//SAPI 5.4

        public Formmain()
        {
            InitializeComponent();
            this.Location = new System.Drawing.Point(Screen.PrimaryScreen.Bounds.Width - 410, Screen.PrimaryScreen.Bounds.Height - 450);
            label3.BackColor = Color.FromArgb(255, 74, 167, 255);
            this.start.Enabled = true;
            //getchengxu.cx();
            
        }
        Point mouseOff;//鼠标移动位置变量
        bool leftFlag;//标记是否为左键
        int kd = 0;
        private void Formmain_Load(object sender, EventArgs e)
        {
            this.MouseDown += Formmain_MouseDown;
            this.MouseMove += Formmain_MouseMove;
            this.MouseUp += Formmain_MouseUp;
            if (Clipboard.GetText() != "")
            {
                Clipboard.Clear();//清空剪切板内容
            }
            
            
            
        }
        private void Formmain_MouseUp(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                leftFlag = false;//释放鼠标后标注为false;
            }
            
        }
        private void Formmain_MouseMove(object sender, MouseEventArgs e)
        {
            if (leftFlag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(mouseOff.X, mouseOff.Y);  //设置移动后的位置
                Location = mouseSet;

                //这个是要box随鼠标移动的代码：this.pictureBox1.Location = mouseSet;
            }
        }
        private void Formmain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseOff = new Point(-e.X, -e.Y); //得到变量的值
                leftFlag = true;                  //点击左键按下时标注为true;
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Right)
            //{
            //    this.pictureBox2.Visible = false;
            //}
            
            
        }

        private void Formmain_MouseLeave(object sender, EventArgs e)
        {


        }

        private void Formmain_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                
            }
            if (e.Button == MouseButtons.Right)
            {
                pictureBox2.Show();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //textBox2.Text = Clipboard.GetText();
            SetWindow SetWin = new xiaoyuan1._0.SetWindow(this.Handle);
            SetWin.Star();
            textBox2.Text = Clipboard.GetText();
            //if (xydo.textbox2 == 0)
            //{
            //    textBox2.Visible = false;
            //    pictureBox3.Focus();
            //    xydo.textbox2 = 1;

            //}
            //if (xydo.textbox2 == 2)
            //{
            //    textBox2.Visible = true;
            //    pictureBox3.Focus();
            //    xydo.textbox2 = 1;

            //}
            if (xylt.open == 1)
            {
                textBox3.Visible = true;
                textBox3.Focus();

            }
            if (xylt.open == 0)
            {
                textBox3.Visible = false;

            }
            if (xydo.textbox == 0)
            {
                textBox1.Visible = false;
                pictureBox3.Focus();
                xydo.textbox = 1;

            }
            if (xydo.textbox == 2)
            {
                textBox1.Visible = true;
                textBox1.Focus();
                label4.Visible = false;
                xydo.textbox = 1;
            }
            if (textBox1.Text == "")
            {
                this.timer2.Enabled = false;
            }
            if (textBox3.Text == "")
            {
                this.timer5.Enabled = false;
            }

            if (textBox1.Text.Contains("哦") || textBox1.Text.Contains("哦!"))
            {

                textBox1.Text = textBox1.Text.Replace("哦!", "");
                textBox1.Text = textBox1.Text.Replace("哦", "");
                textBox1.Text = textBox1.Text.Replace("!", "");
            }
            if (cyk.cymlzt == 1)
            {
                if (Clipboard.GetText() == "不执行" || Clipboard.GetText().Contains("不执行") || Clipboard.GetText().Contains("不"))
                {
                    xyzt.open = 2;
                    if (xyzt.open == 2)
                    {
                        xyzt.open = 0;
                        cyk.cymlzt = 0;
                        if (Clipboard.GetText() != "")
                        {
                            Clipboard.Clear();//清空剪切板内容
                        }
                        keybd_event(0x71, 0, 0, 0);//按F2
                        Random shuiji = new Random();
                        Image image = pictureBox3.BackgroundImage;
                        pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                        
                        string[] word = File.ReadAllLines(@".\\yuyinku\7.txt", System.Text.Encoding.Default);
                        label1.Text = word[shuiji.Next(0, word.Length)];
                        voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                        voice.Speak(word[shuiji.Next(0, word.Length)]);
                        keybd_event(0x71, 0, 0, 0);//按F2
                        //pictureBox3.BackgroundImage = image;
                        
                    }
                }
                else 
                {
                    if (Clipboard.GetText() == "执行" || Clipboard.GetText() == "好" || Clipboard.GetText() == "可以")//听到用户说确定
                    {
                        xyzt.open = 2;
                        if (xyzt.open == 2)
                        {
                            xyzt.open = 0;
                            cyk.cymlzt = 0;
                            Process.Start(cyk.cyml(cyk.cymlnr));
                            keybd_event(0x71, 0, 0, 0);//按F2
                            Random shuiji = new Random();
                            Image image = pictureBox3.BackgroundImage;
                            pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                            string[] word = File.ReadAllLines(@".\\yuyinku\4.txt", System.Text.Encoding.Default);
                            label1.Text = word[shuiji.Next(0, word.Length)];
                            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                            voice.Speak(word[shuiji.Next(0, word.Length)]);
                            keybd_event(0x71, 0, 0, 0);//按F2
                            if (Clipboard.GetText() != "")
                            {
                                Clipboard.Clear();//清空剪切板内容
                            }
                            //pictureBox3.BackgroundImage = image;

                        }
                    }
                }
               
                

            }
            if (cyk.cymlzt == 2)
            {
                
                if (Clipboard.GetText() == "不创建" || Clipboard.GetText().Contains("不创建") || Clipboard.GetText().Contains("不"))
                {
                    xyzt.open2 = 2;
                    if (xyzt.open2 == 2)
                    {
                        xyzt.open2 = 0;
                        cyk.cymlzt = 0;
                        
                        keybd_event(0x71, 0, 0, 0);//按F2
                        Random shuiji = new Random();
                        Image image = pictureBox3.BackgroundImage;
                        pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                        string[] word = File.ReadAllLines(@".\\yuyinku\7.txt", System.Text.Encoding.Default);
                        label1.Text = word[shuiji.Next(0, word.Length)];
                        voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                        voice.Speak(label1.Text);
                        keybd_event(0x71, 0, 0, 0);//按F2
                        //pictureBox3.BackgroundImage = image;
                    }

                }
                else
                {
                    if (Clipboard.GetText() == "创建" || Clipboard.GetText().Contains("创建") || Clipboard.GetText().Contains("好") || Clipboard.GetText().Contains("是"))//听到用户说创建
                    {
                        xyzt.open2 = 1;
                        if (xyzt.open2 == 1)
                        {
                            
                            xyzt.open2 = 0;
                            cyk.cymlzt = 0;
                            System.Threading.Thread.Sleep(1000);
                            if (Clipboard.GetText() != "")
                            {
                                Clipboard.Clear();//清空剪切板内容
                            }
                            OpenFileDialog fileDialog = new OpenFileDialog();
                            fileDialog.Multiselect = true;
                            fileDialog.Title = "请选择文件";
                            fileDialog.Filter = "所有文件(*.*)|*.*";
                            if (fileDialog.ShowDialog() == DialogResult.OK)
                            {
                                string file = fileDialog.FileName;
                                System.IO.Directory.CreateDirectory(@".\\");
                                DirectoryInfo dir = new DirectoryInfo(@".\\cyml");
                                dir.Create();
                                File.WriteAllText(@".//cyml/" + xydo.gettext + ".txt", file);

                                string[] line = File.ReadAllLines(@".//cyml/mulu.txt", System.Text.Encoding.Default);
                                string mulu = "";
                                foreach (string c in line)
                                {
                                    mulu = c + "\r\n" + mulu;
                                }
                                mulu = mulu + xydo.gettext + "\r\n";
                                File.WriteAllText(@".//cyml/mulu.txt", mulu, System.Text.Encoding.Default);
                                keybd_event(0x71, 0, 0, 0);//按F2
                                Random shuiji = new Random();
                                Image image = pictureBox3.BackgroundImage;
                                pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                                string[] word = File.ReadAllLines(@".\\yuyinku\9.txt", System.Text.Encoding.Default);
                                label1.Text = word[shuiji.Next(0, word.Length)];
                                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                                voice.Speak(word[shuiji.Next(0, word.Length)]);
                                keybd_event(0x71, 0, 0, 0);//按F2
                                //pictureBox3.BackgroundImage = image;
                                
                            }
                            else
                            {
                                keybd_event(0x71, 0, 0, 0);//按F2
                                Random shuiji = new Random();
                                Image image = pictureBox3.BackgroundImage;
                                pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                                string[] word = File.ReadAllLines(@".\\yuyinku\7.txt", System.Text.Encoding.Default);
                                label1.Text = word[shuiji.Next(0, word.Length)];
                                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                                voice.Speak(label1.Text);
                                keybd_event(0x71, 0, 0, 0);//按F2
                                //pictureBox3.BackgroundImage = image;
                            }



                        }


                    }
                }
            }
        }
        

          
        private void timer2_Tick(object sender, EventArgs e)
        {
            
            
            if (textBox1.Visible == true)
            {
                //textBox1.Text = Clipboard.GetText();//粘贴识别到的文字
                xydo.gettext = textBox1.Text;
                if (xyzt.open3 == 0)
                {
                    xyzt.open3 = 1;
                    xydo.neirong(xydo.gettext);
                    if (Int32.Parse(think.chongfu(xydo.gettext)) == 0)
                    {
                        xydo.doing(xydo.gettext);
                    }
                }
                
                
                

                textBox1.Text = "";
                xydo.textbox = 0;
            }
            

            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //Random shuiji = new Random();
            //string path = @".\\yuyinku\5\" + shuiji.Next(1, 5) + ".wav";
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
            //player.Play();
            //WaveInfo wav = new WaveInfo(path);
            //xydo.alltime = (int)wav.Second * 1000;
            //System.Threading.Thread.Sleep(xydo.alltime);
            keybd_event(0x71, 0, 0, 0);//按F2
            Random shuiji = new Random(); 
            Image image = pictureBox3.BackgroundImage;
            pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");

            string[] word = File.ReadAllLines(@".\\yuyinku\5.txt", System.Text.Encoding.Default);
            label1.Text = word[shuiji.Next(0, word.Length)];
            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
            voice.Speak(word[shuiji.Next(0, word.Length)]);
            keybd_event(0x71, 0, 0, 0);//按F2
            this.Close();
        }
        Boolean isTrue = true;
        Boolean isTrue2 = true;
        Boolean isTrue3 = true;
        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            if (link.net() == true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (isTrue)
                    {
                        if (textBox1.Visible == false)
                        {
                            keybd_event(0x71, 0, 0, 0);//按F2

                            //textBox1.Enter += new EventHandler(textBox1_Enter);  //获得焦点事件
                            //Random shuiji = new Random();

                            //string path = @".\\yuyinku\2\" + shuiji.Next(1, 7) + ".wav";
                            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                            //player.Play();
                            //WaveInfo wav = new WaveInfo(path);
                            //xydo.alltime = (int)wav.Second * 1000;
                            //System.Threading.Thread.Sleep(xydo.alltime);
                            Random shuiji = new Random();
                            Image image = pictureBox3.BackgroundImage;
                            pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                            string[] word = File.ReadAllLines(@".\\yuyinku\2.txt", System.Text.Encoding.Default);
                            label1.Text = word[shuiji.Next(0, word.Length)];
                            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                            voice.Speak(word[shuiji.Next(0, word.Length)]);
                            keybd_event(0x71, 0, 0, 0);//按F2
                            //pictureBox3.BackgroundImage = image;
                            xydo.textbox = 2;
                            textBox1.Focus();

                        }
                    }
                    else
                    {
                        xydo.textbox = 0;
                        //textBox1.Leave += new EventHandler(textBox1_Leave);


                    }
                    isTrue = !isTrue;
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (isTrue2)
                    {
                        pictureBox2.Visible = true;
                        pictureBox4.Visible = true;
                    }
                    else
                    {
                        pictureBox2.Visible = false;
                        pictureBox4.Visible = false;
                    }
                    isTrue2 = !isTrue2;
                }
            }
            else
            {
                MessageBox.Show("你还没联网哦~");
            
            }
        }

        private void Formmain_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Process[] process;//创建一个PROCESS类数组
            process = System.Diagnostics.Process.GetProcesses();//获取当前任务管理器所有运行中程序
            foreach (Process p in process)//遍历
            {

                if (p.ProcessName == "VoiceInput")
                {
                    p.Kill();

                }
            }

            File.WriteAllText(".//VoiceInput/SpeechInput.ini", "[Setting]\r\nlastX=" + this.Location.X + this.Location.X + "\r\nlastY=" + this.Location.Y);
            //
            System.Environment.Exit(0);
        }

        private void Formmain_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if (Clipboard.GetText() != "")
            //{
            //    Clipboard.Clear();//清空剪切板内容
            //}
            
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            
            this.timer2.Enabled = false;
            //设置时间间隔（毫秒为单位）
            this.timer2.Interval = 3000;
            this.timer2.Enabled = true;//设置Timer控件可用

        }
        
        private void Formmain_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        private void textBoxSend_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.KeyValue == 84)
            {
                
                kd = 0;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (textBox1.Visible == false)
            {
                if (Clipboard.GetText() != "")
                {
                    string c = Clipboard.GetText();
                    if (hxxy.xyout(c) == 1)
                    {
                        Clipboard.Clear();
                        //Random shuiji = new Random();
                        //string path = @".\\yuyinku\5\" + shuiji.Next(1, 5) + ".wav";
                        //System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                        //player.Play();
                        //WaveInfo wav = new WaveInfo(path);
                        //xydo.alltime = (int)wav.Second * 1000;
                        //System.Threading.Thread.Sleep(xydo.alltime);
                        //label1.Text = "小圆不舍得离开主人,主人再见~！";
                        keybd_event(0x71, 0, 0, 0);//按F2
                        Random shuiji = new Random();
                        Image image = pictureBox3.BackgroundImage;
                        pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                        string[] word = File.ReadAllLines(@".\\yuyinku\5.txt", System.Text.Encoding.Default);
                        label1.Text = word[shuiji.Next(0, word.Length)];
                        voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                        voice.Speak(word[shuiji.Next(0, word.Length)]);
                        //pictureBox3.BackgroundImage = image;
                        this.Close();
                    }
                    if (hxxy.check(c) == 1)
                    {
                        keybd_event(0x71, 0, 0, 0);//按F2

                        //label1.Text = "你好主人,需要什么帮助吗？";
                        //Random shuiji = new Random();
                        //string path = @".\\yuyinku\2\" + shuiji.Next(1, 7) + ".wav";
                        //System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                        //player.Play();
                        //WaveInfo wav = new WaveInfo(path);
                        //xydo.alltime = (int)wav.Second * 1000;
                        //System.Threading.Thread.Sleep(xydo.alltime);
                        keybd_event(0x71, 0, 0, 0);//按F2
                        Random shuiji = new Random();
                        Image image = pictureBox3.BackgroundImage;
                        pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                        string[] word = File.ReadAllLines(@".\\yuyinku\2.txt", System.Text.Encoding.Default);
                        label1.Text = word[shuiji.Next(0, word.Length)];
                        voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                        voice.Speak(word[shuiji.Next(0, word.Length)]);
                        keybd_event(0x71, 0, 0, 0);//按F2
                        //pictureBox3.BackgroundImage = image;
                        if (Clipboard.GetText() != "")
                        {
                            Clipboard.Clear();//清空剪切板内容
                        }

                        textBox1.Text = "";
                        textBox1.Focus();
                        xydo.textbox = 2;



                    }
                }



                textBox1.Focus();
            }
            else
            {
                string c = Clipboard.GetText();
                if (hxxy.xyout(c) == 1)
                {
                    keybd_event(0x71, 0, 0, 0);//按F2
                    Random shuiji = new Random();
                    Image image = pictureBox3.BackgroundImage;
                    pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                    string[] word = File.ReadAllLines(@".\\yuyinku\5.txt", System.Text.Encoding.Default);
                    label1.Text = word[shuiji.Next(0, word.Length)];
                    voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                    voice.Speak(word[shuiji.Next(0, word.Length)]);
                    //System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                    //player.Play();
                    //WaveInfo wav = new WaveInfo(path);
                    //xydo.alltime = (int)wav.Second * 1000;
                    //System.Threading.Thread.Sleep(xydo.alltime);
                    if (Clipboard.GetText() != "")
                    {
                        Clipboard.Clear();//清空剪切板内容
                    }
                    //pictureBox3.BackgroundImage = image;



                    this.Close();
                }
            }
            
        }
        void textBox1_Enter(object sender, EventArgs e)
        {
            
        }
        void textBox1_Leave(object sender, EventArgs e)
        {
            Process[] process;//创建一个PROCESS类数组
            process = System.Diagnostics.Process.GetProcesses();//获取当前任务管理器所有运行中程序
            foreach (Process p in process)//遍历
            {

                if (p.ProcessName == "VoiceInput")
                {
                    p.Kill();

                }
            }
        }

        private void Formmain_EnterFocus(object sender, EventArgs e)
        {
        }

        private void Formmain_LostFocus(object sender, EventArgs e)
        {

        }

        private void Formmain_Leave(object sender, EventArgs e)
        {
            
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != "")
            { 
                Clipboard.Clear();//清空剪切板内容
            }
            
            
            
        }

        private void Formmain_Activated(object sender, EventArgs e)
        {
            
            //if (xyzt.dqzt == 1)
            //{
            //    keybd_event(0x71, 0, 0, 0);//按F2
            //    xyzt.dqzt = 1;
            //}
        }

        private void cymltimer_Tick(object sender, EventArgs e)
        {
            if (Clipboard.GetText() == "")
            {
                
                Random shuiji = new Random();
                Image image = pictureBox3.BackgroundImage;
                pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                string[] word = File.ReadAllLines(@".\\yuyinku\10.txt", System.Text.Encoding.Default);
                label1.Text = xylt.talktl(word[shuiji.Next(0, word.Length)]);
                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                voice.Speak(label1.Text);
                textBox1.Visible = false;
                keybd_event(0x71, 0, 0, 0);//按F2
                xydo.blxy++;
                label4.Visible = true;
                //pictureBox3.BackgroundImage = image;
            }
            if (xydo.blxy == 5)//改变量
            {
                this.Close();
            }
        }

        private void pictureBox1_Shown(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Process.Start("mmsys.cpl");
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (textBox3.Visible == true)
            {
                if (textBox3.Text != "")
                {
                    if (textBox3.Text.Contains("结束聊天") || textBox3.Text.Contains("退出聊天") || textBox3.Text.Contains("不聊天"))
                    {
                        textBox3.Visible = false;
                        
                        keybd_event(0x71, 0, 0, 0);//按F2

                        SpVoice voice4 = new SpVoice();//SAPI 5.4
                        voice4.Voice = voice4.GetVoices(string.Empty, string.Empty).Item(0);
                        voice4.Speak("下次再来和小圆聊天哦！");
                        keybd_event(0x71, 0, 0, 0);//按F2
                        xylt.open = 0;
                    }
                    else
                    {

                        keybd_event(0x71, 0, 0, 0);
                        label1.Text = xylt.talktl(textBox3.Text);
                        voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                        voice.Speak(xylt.talktl(textBox3.Text));
                        keybd_event(0x71, 0, 0, 0);//按F2
                        textBox3.Text = "";
                    }
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            this.timer5.Enabled = false;
            //设置时间间隔（毫秒为单位）
            this.timer5.Interval = 3000;
            this.timer5.Enabled = true;//设置Timer控件可用
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            if (Clipboard.GetText() != "")
            {
                cymltimer.Enabled = false;
            }
            if (Clipboard.GetText() == "")
            {
                cymltimer.Enabled = true;
            }
        }

        private void timer7_Tick(object sender, EventArgs e)
        {
            xydo.xybj = pictureBox3.BackgroundImage;
            this.timer8.Enabled = true;
            
            pictureBox3.BackgroundImage=Image.FromFile(".//UI/小圆/小圆.png");
            label3.Visible = true;
            Random shuiji = new Random();
            this.timer7.Interval = shuiji.Next(1500, 5500);
            //
        }

        private void timer8_Tick(object sender, EventArgs e)
        {
            label3.Visible = false;
            pictureBox3.BackgroundImage = xydo.xybj;
        }

        private void label3_MouseClick(object sender, MouseEventArgs e)
        {
            if (link.net() == true)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (isTrue)
                    {
                        if (textBox1.Visible == false)
                        {
                            keybd_event(0x71, 0, 0, 0);//按F2

                            //textBox1.Enter += new EventHandler(textBox1_Enter);  //获得焦点事件
                            //Random shuiji = new Random();

                            //string path = @".\\yuyinku\2\" + shuiji.Next(1, 7) + ".wav";
                            //System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                            //player.Play();
                            //WaveInfo wav = new WaveInfo(path);
                            //xydo.alltime = (int)wav.Second * 1000;
                            //System.Threading.Thread.Sleep(xydo.alltime);
                            Random shuiji = new Random();
                            Image image = pictureBox3.BackgroundImage;
                            pictureBox3.BackgroundImage = Image.FromFile(@".\\UI\表情\" + shuiji.Next(1, 6) + ".png");
                            string[] word = File.ReadAllLines(@".\\yuyinku\2.txt", System.Text.Encoding.Default);
                            label1.Text = word[shuiji.Next(0, word.Length)];
                            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                            voice.Speak(word[shuiji.Next(0, word.Length)]);
                            keybd_event(0x71, 0, 0, 0);//按F2
                            //pictureBox3.BackgroundImage = image;
                            xydo.textbox = 2;
                            textBox1.Focus();

                        }
                    }
                    else
                    {
                        xydo.textbox = 0;
                        //textBox1.Leave += new EventHandler(textBox1_Leave);


                    }
                    isTrue = !isTrue;
                }
                if (e.Button == MouseButtons.Right)
                {
                    if (isTrue2)
                    {
                        pictureBox2.Visible = true;
                        pictureBox4.Visible = true;
                    }
                    else
                    {
                        pictureBox2.Visible = false;
                        pictureBox4.Visible = false;
                    }
                    isTrue2 = !isTrue2;
                }
            }
            else
            {
                MessageBox.Show("你还没联网哦~");

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process[] process;//创建一个PROCESS类数组
            process = System.Diagnostics.Process.GetProcesses();//获取当前任务管理器所有运行中程序
            foreach (Process p in process)//遍历
            {

                if (p.ProcessName == "VoiceInput")
                {
                    p.Kill();

                }
            }
            voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
            voice.Speak("小圆重置完毕");
            System.Diagnostics.Process.Start(@".\\VoiceInput\VoiceInput.exe");

        }

        private void start_Tick(object sender, EventArgs e)
        {
            if (xyzt.wlzt == true)
            {
                //Random shuiji = new Random();

                //WaveInfo wav = new WaveInfo(path);
                //xydo.alltime = (int)wav.Second * 1000;
                Random shuiji = new Random();



                SpVoice voice = new SpVoice();//SAPI 5.4
                string[] word = File.ReadAllLines(@".\\yuyinku\1.txt", System.Text.Encoding.Default);

                voice.Voice = voice.GetVoices(string.Empty, string.Empty).Item(0);
                voice.Speak(word[shuiji.Next(0, word.Length)]);
                string path = @".\\bgm\111345.2.0.wav";
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);
                player.Play();

            }
            System.Diagnostics.Process.Start(@".\\VoiceInput\VoiceInput.exe");
            this.start.Enabled = false;
        }



    }
}
