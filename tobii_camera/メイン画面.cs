using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace tobii_camera
{
    public partial class メイン画面 : Form
    {
        private Timer timer;
        public static int[] window=new int[2];
        public static int 間隔;
        public static int fps;

        public メイン画面()
        {
            InitializeComponent();
            Camera.Instance.Show();
            Tobii.Instance.Show();
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = (int)(1000 / int.Parse(textBox_fps.Text));
            timer.Start();

        }

        private void Click_Camera(object sender, EventArgs e)
        {
            Camera.Instance.Show();
        }

        private void Click_Tobii(object sender, EventArgs e)
        {
            Tobii.Instance.Show();
        }

        private void timer_Tick(object sender, EventArgs e)//タイマ割り込みで行う処理
        {
            
            label_debug.Text = Tobii.debug;

        }

        private void Click_start(object sender, EventArgs e)
        {
            //描画に使うデータ格納
            window[0] = int.Parse(textBox_window_x.Text);
            window[1] = int.Parse(textBox_window_y.Text);
            間隔 = int.Parse(textBox_space.Text);
            fps = int.Parse(textBox_fps.Text);

            描画画面.Instance.Show();
        }
    }
}
