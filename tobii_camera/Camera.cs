using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;
using System.Net;
using System.IO;
using OpenCvSharp.Extensions;

namespace tobii_camera
{
    public partial class Camera : Form
    {
        bool IPcamera = false;

        //USBカメラ用変数
        private static Camera _instance;
        CvCapture CAPTURE;

        //IPカメラ用変数
        byte[] CAPTURE_IP;
        string URL = "http://192.168.2.1/jpg/image.jpg";

        private Timer timer;
        public static IplImage camera;

        public Camera()
        {
            InitializeComponent();
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
            IPcamera = false;
            CAPTURE = Cv.CreateCameraCapture(0);
            解像度設定(int.Parse(textBox_resX.Text), int.Parse(textBox_resY.Text));
            FPS設定(int.Parse(textBox_FPS.Text));
            timer.Interval = (int)(1000 / CAPTURE.Fps);
            timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)//タイマ割り込みで行う処理
        {
            if (!IPcamera)//usbカメラの処理
            {
                    var frame = Cv.QueryFrame(CAPTURE);
                    if (frame != null)
                    {
                        pictureBoxIpl1.RefreshIplImage(frame);
                        camera = frame.Clone();
                    }
                    else System.Diagnostics.Debug.WriteLine("frame=null");
                    Cv.ReleaseImage(frame);
            }
            else//webカメラの処理
            {
                int read, total = 0;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                WebResponse resp = request.GetResponse();
                Stream stream = resp.GetResponseStream();
                while ((read = stream.Read(CAPTURE_IP, total, 1000)) != 0) total += read;
                Bitmap bmp = (Bitmap)Bitmap.FromStream(new MemoryStream(CAPTURE_IP, 0, total));

                camera=BitmapConverter.ToIplImage(bmp);
                pictureBoxIpl1.RefreshIplImage(camera);

                bmp.Dispose();
 
            }
        }

        private void Click_Stop(object sender, EventArgs e)
        {
            Cv.ReleaseImage(camera);
            camera = null;
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

        private void Camera_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer.Stop();
            Cv.ReleaseImage(camera);
            camera = null;
        }

        private void Click_IP(object sender, EventArgs e)
        {
            IPcamera = true;            
            CAPTURE_IP = new byte[int.Parse(textBox_resX.Text) * int.Parse(textBox_resY.Text)];

            timer.Interval = (int)(1000 / int.Parse(textBox_FPS.Text));
            timer.Start();
        }

    }
}
