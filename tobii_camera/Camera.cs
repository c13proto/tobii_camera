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
    public partial class Camera : Form
    {
        private static Camera _instance;
        private Timer timer;
        CvCapture CAPTURE;
        bool REFRESH=true;

        public Camera()
        {
            InitializeComponent();
            CAPTURE = Cv.CreateCameraCapture(0);
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);   
        }

        public static Camera Instance
        {
            get
            {
                //_instanceがnullまたは破棄されているときは、
                //新しくインスタンスを作成する
                if (_instance == null || _instance.IsDisposed)
                    _instance = new Camera();
                return _instance;
            }
        }

        private void Click_Start(object sender, EventArgs e)
        {
            REFRESH = true;
            解像度設定(int.Parse(textBox_resX.Text), int.Parse(textBox_resY.Text));
            FPS設定(int.Parse(textBox_FPS.Text));
            timer.Interval = (int)(1000 / CAPTURE.Fps);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)//タイマ割り込みで行う処理
        {

            if(REFRESH)
            {
                var frame = Cv.QueryFrame(CAPTURE);
                if (frame != null) pictureBoxIpl1.RefreshIplImage(frame);
                else System.Diagnostics.Debug.WriteLine("frame=null"); 
                Cv.ReleaseImage(frame);
            }
        }

        private void Click_Stop(object sender, EventArgs e)
        {
            REFRESH = false;
            timer.Stop();
        }

        void 解像度設定(int x,int y)
        {
            Cv.SetCaptureProperty(CAPTURE, CaptureProperty.FrameWidth, x);
            Cv.SetCaptureProperty(CAPTURE, CaptureProperty.FrameHeight,y);
        }
        void FPS設定(int fps)
        {
            CAPTURE.Fps = fps;
        }

    }
}
