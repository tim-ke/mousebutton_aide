using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp32
{
    public partial class Form1 : Form
    {

        private BackgroundWorker bw ;
        private int time1 = 0;
        private int time2 = 0;
        Thread tt;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //忽略跨執行緒錯誤
            timer1.Interval = 1000;
            timer1.Enabled = false;         
            button2.Enabled = false;
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            MessageBox.Show("OK");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            time1 = (int)numericUpDown1.Value;
            numericUpDown1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = true;
            timer1.Start();
            button_start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            numericUpDown1.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = false;
            timer1.Stop();
            label5.Text = "停止~";
            label6.Text = "0";
            tt.Abort();

        }



        public void button_start()
        {
           tt = new Thread(btn_th);
            tt.Start();
        }

        private void btn_th()
        {
            while (true)
            {
                if (radioButton_left.Checked == true)
                {
                    DoMouseClick_left_down(0, 0);
                    DoMouseClick_left_up(0, 0);
                    label5.Text = "滑鼠左鍵";
                }else if(radioButton_right.Checked == true)
                {
                    //SetCursorPos(0, 0);
                    DoMouseClick_right_down(0, 0);
                    DoMouseClick_right_up(0, 0);
                    //DoMouseClick_up(0, 0);
                    label5.Text = "滑鼠右鍵";
                }

                time2 = time1 -1;
                Thread.Sleep(time1 * 1000);

                //Thread.Sleep(500);


            }
        }

        [DllImport("user32")]
        static extern bool SetCursorPos(int X, int Y);
        //private JoyKeys.Core.Joystick joystick;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(long dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private const int MOUSEEVENTF_MOVE = 0x0001; //移動滑鼠
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模擬滑鼠中鍵按下
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模擬滑鼠中鍵抬起
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000; //標示是否採用絕對座標

        private const int MOUSEEVENTF_LEFTDOWN = 0x02; //模擬滑鼠左鍵按下
        private const int MOUSEEVENTF_LEFTUP = 0x04; //模擬滑鼠左鍵抬起
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08; //模擬滑鼠右鍵按下
        private const int MOUSEEVENTF_RIGHTUP = 0x10; //類比滑鼠右鍵抬起

        /// <summary>
        /// 滑鼠右鍵down
        /// </summary>
        public void DoMouseClick_right_down(int X1, int Y1)
        {
            //Call the imported function with the cursor's current position
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 100, 100);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, X, Y, X1, Y1);
        }


        /// <summary>
        /// 滑鼠右鍵up
        /// </summary>
        public void DoMouseClick_right_up(int X1, int Y1)
        {
            //Call the imported function with the cursor's current position
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 100, 100);
            mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, X1, Y1);
        }


        /// <summary>
        /// 滑鼠左鍵down
        /// </summary>
        public void DoMouseClick_left_down(int X1, int Y1)
        {
            //Call the imported function with the cursor's current position
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 100, 100);
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, X1, Y1);
        }


        /// <summary>
        /// 滑鼠左鍵up
        /// </summary>
        public void DoMouseClick_left_up(int X1, int Y1)
        {
            //Call the imported function with the cursor's current position
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 100, 100);
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y, X1, Y1);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label6.Text = time2.ToString();
            time2--;         
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }



        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    // 按快捷鍵Ctrl+S執行按鈕的點選事件方法
        //    if (keyData == (Keys)Shortcut.ShiftF2)
        //    {
        //        //button1.PerformClick();
        //        MessageBox.Show("OK");
        //        return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData); // 其他鍵按預設處理　
        //}

    }
}
