using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace tobii_camera
{
    public partial class 描画画面 : Form
    {
        private static 描画画面 _instance;
        IplImage background;
        CvSize window_size;

        int dis_height;//ディスプレイの高さ
        int dis_width;//ディスプレイの幅

        private Timer timer;


        public 描画画面()
        {
            InitializeComponent();
            dis_height= System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            dis_width=System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            pictureBoxIpl1.Width = dis_width;
            pictureBoxIpl1.Height = dis_height;
            background = Cv.CreateImage(new CvSize(dis_width, dis_height), BitDepth.U8, 3);
            background=メイン画面.background;
            pictureBoxIpl1.ImageIpl = background;
            window_size = new CvSize(メイン画面.window[0], メイン画面.window[1]);

            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = (int)(1000 / メイン画面.fps);
            timer.Start();

        }

        public static 描画画面 Instance
        {
            get
            {
                //_instanceがnullまたは破棄されているときは、
                //新しくインスタンスを作成する
                if (_instance == null || _instance.IsDisposed)
                    _instance = new 描画画面();
                return _instance;
            }
        }
        private void timer_Tick(object sender, EventArgs e)//タイマ割り込みで行う処理
        {
            var sample = background.Clone();

            視点描画(ref sample);
            顔の向き描画(ref sample);
            debug描画(ref sample);
            

            pictureBoxIpl1.RefreshIplImage(sample);
            sample.Dispose();
        }

        void 視点描画(ref IplImage src)
        {
            //視点位置と□
            int point_x, point_y;
            point_x = Tobii.視点座標.X - window_size.Width / 2;
            point_y = Tobii.視点座標.Y - window_size.Height / 2;
            if (point_x < 0) point_x = 0;
            if (point_y < 0) point_y = 0;
            if (point_x > dis_width - window_size.Width) point_x = dis_width - window_size.Width;
            if (point_y > dis_height - window_size.Height) point_y = dis_height - window_size.Height;
            CvPoint point1 = new CvPoint(point_x, point_y);
            CvPoint point2 = new CvPoint(point_x + window_size.Width, point_y + window_size.Height);
            //Cv.Rectangle(src, point1, point2, new CvScalar(0, 0, 255), 3);
            枠外を塗りつぶす(ref src, new CvRect(point1, window_size));
            Cv.Circle(src, new CvPoint(Tobii.視点座標.X, Tobii.視点座標.Y), 3, new CvScalar(255, 255, 255));
        }
        void 枠外を塗りつぶす(ref IplImage src,CvRect rect)
        {
            var mask = src.Clone();
            mask.Zero();
            Cv.Rectangle(mask, rect, new CvScalar(255, 255, 255), -1);
            mask.Not(mask);
            Cv.Add(src, mask, src);

            mask.Dispose();
        }
        void 顔の向き描画(ref IplImage src)
        {
            // ||の表示
            double[] eyes = Tobii.眼球位置;
            double center=(eyes[0]+eyes[1])/2;
            int line_pos = dis_width/2;

            if (eyes[0] == 0 && eyes[1] == 1000) { } //両目が検知できなかったら
            else if (eyes[0] > 510) line_pos = dis_width;
            else if (eyes[1] < 490) line_pos=0;
            else if (eyes[0] > 490) line_pos = dis_width * 5 / 6;
            else if (eyes[1] < 510) line_pos = dis_width / 6;
            else if (eyes[0] > 430) line_pos = dis_width * 4 / 6;
            else if (eyes[1] < 530) line_pos = dis_width * 2 / 6;
            else line_pos = Width / 2;

            Cv.Line(src, new CvPoint(line_pos, 0), new CvPoint(line_pos, dis_height), new CvScalar(0, 255, 0), 5);
        }
        void debug描画(ref IplImage src)
        {
            string[] debug=Tobii.debug.Split('\n');
            CvFont フォント = new CvFont(FontFace.HersheyComplex, 0.5, 0.5);
            Cv.PutText(src, debug[0], new CvPoint(10, 20), フォント, new CvColor(0, 0, 255));
            Cv.PutText(src, debug[1], new CvPoint(10, 40), フォント, new CvColor(0, 0, 255));
            Cv.PutText(src, debug[2], new CvPoint(10, 60), フォント, new CvColor(0, 0, 255));
        }
    }
}
