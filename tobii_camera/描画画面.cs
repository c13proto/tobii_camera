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
            background.Zero();
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
            background.Zero();//背景の初期化
            
            //視点位置と□
            int point_x, point_y;
            point_x = Tobii.視点座標.X-window_size.Width/2;
            point_y = Tobii.視点座標.Y - window_size.Height / 2;
            if (point_x < 0) point_x = 0;
            if (point_y < 0) point_y = 0;
            if (point_x > dis_width - window_size.Width) point_x = dis_width - window_size.Width;
            if (point_y > dis_height - window_size.Height) point_y = dis_height - window_size.Height;
            CvPoint point1=new CvPoint(point_x,point_y);
            CvPoint point2=new CvPoint(point_x+window_size.Width,point_y+window_size.Height);
            Cv.Rectangle(background,point1,point2, new CvScalar(0, 0, 255),3);
            Cv.Circle(background, new CvPoint(Tobii.視点座標.X, Tobii.視点座標.Y), 5, new CvScalar(255, 255, 255));

            // ||の表示(向いてる角度)
            double max_deg=30.0;
            double angle=Tobii.顔の角度;
            if(angle>max_deg)angle=max_deg;
            if(angle<-max_deg)angle=-max_deg;
            int line_pos = (int)((double)dis_width / 2.0 + ((double)dis_width / 2.0) * (angle / max_deg));
            if (line_pos < メイン画面.間隔 / 2) line_pos = メイン画面.間隔 / 2;
            if (line_pos > dis_width - メイン画面.間隔 / 2) line_pos = dis_width - メイン画面.間隔 / 2;
            Cv.Line(background, new CvPoint(line_pos - メイン画面.間隔 / 2, 0), new CvPoint(line_pos - メイン画面.間隔 / 2, dis_height), new CvScalar(0, 255, 0), 1);
            Cv.Line(background, new CvPoint(line_pos + メイン画面.間隔 / 2, 0), new CvPoint(line_pos + メイン画面.間隔 / 2, dis_height), new CvScalar(0, 255, 0), 1);
            

            pictureBoxIpl1.RefreshIplImage(background);

        }
    }
}
