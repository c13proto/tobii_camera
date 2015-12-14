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
using System.Timers;

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
        string URL = "http://192.168.2.1/?action=snapshot";

        private System.Timers.Timer timer_back;
        private System.Windows.Forms.Timer timer_form;
        public static IplImage camera;

        public Camera()
        {
            InitializeComponent();
            timer_back = new System.Timers.Timer();
            timer_back.Elapsed += new ElapsedEventHandler(background_ctrl);
            timer_form = new System.Windows.Forms.Timer();
            timer_form.Tick += new EventHandler(form_ctrl);
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
            CAPTURE.Fps = int.Parse(textBox_FPS.Text);
            timer_back.Interval = (int)(1000 / int.Parse(textBox_FPS.Text));
            timer_form.Interval = (int)(1000 / int.Parse(textBox_FPS.Text));
            timer_form.Start();
            timer_back.Start();
        }
        private void form_ctrl(object sender, EventArgs e)//タイマ割り込みで行う処理
        {
                pictureBoxIpl1.RefreshIplImage(camera);
        }
        void background_ctrl(object sender, ElapsedEventArgs e)
        {            
            if (!IPcamera)//usbカメラの処理
            {
                var frame = Cv.QueryFrame(CAPTURE);
                if (frame != null)
                {
                    camera = frame.Clone();
                }
                else System.Diagnostics.Debug.WriteLine("frame=null");
                Cv.ReleaseImage(frame);
            }
            else//wifiカメラの処理
            {
                WebClient wc = new WebClient();
                Stream stream = wc.OpenRead(URL);
                Bitmap bmp = new Bitmap(stream);
                stream.Close();
                camera = BitmapConverter.ToIplImage(bmp);
                bmp.Dispose();
            }
        }
        private void Click_Stop(object sender, EventArgs e)
        {
            Cv.ReleaseImage(camera);
            camera = null;
            timer_form.Stop();
            timer_back.Stop();
        }

        void 解像度設定(int x,int y)
        {
            Cv.SetCaptureProperty(CAPTURE, CaptureProperty.FrameWidth, x);
            Cv.SetCaptureProperty(CAPTURE, CaptureProperty.FrameHeight,y);
        }

        private void Camera_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_form.Stop();
            timer_back.Stop();
            Cv.ReleaseImage(camera);
            camera = null;
        }

        private void Click_IP(object sender, EventArgs e)
        {
            IPcamera = true;            
            CAPTURE_IP = new byte[int.Parse(textBox_resX.Text) * int.Parse(textBox_resY.Text)];

            timer_back.Interval = (int)(1000 / int.Parse(textBox_FPS.Text));
            timer_form.Interval = (int)(1000 / int.Parse(textBox_FPS.Text));
            timer_form.Start();
            timer_back.Start();

        }

    }
}
