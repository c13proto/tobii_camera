using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;

namespace tobii_camera
{
    public partial class メイン画面 : Form
    {
        private Timer timer;
        public static int[] window=new int[2];
        public static int fps;
        public static IplImage background;
        public static int average_num;

        public メイン画面()
        {
            InitializeComponent();

            background = Cv.CreateImage(new CvSize(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height), BitDepth.U8, 3);
            background.Zero();

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
            fps = int.Parse(textBox_fps.Text);
            average_num = int.Parse(textBox_average.Text);
            描画画面.Instance.Show();
        }

        private void Click_back(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog()
            {
                Multiselect = false,  // 複数選択の可否
                Filter =  // フィルタ
                "画像ファイル|*.bmp;*.gif;*.jpg;*.png|全てのファイル|*.*",
            };
            //ダイアログを表示
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // ファイル名をタイトルバーに設定
                this.Text = dialog.SafeFileName;
                //OpenCV処理
                background.Zero();
                var buff = new IplImage(dialog.FileName, LoadMode.Color);
                buff.Resize(background);
            }
        }
    }
}
