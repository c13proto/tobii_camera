using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tobii.EyeX.Client;
using Tobii.EyeX.Framework;
using EyeXFramework;

namespace tobii_camera
{

    public partial class Tobii : Form
    {
        private static Tobii _instance;
        private Timer timer;
        EyeXHost eyexhost = new EyeXHost();
        GazePointDataStream lightlyFilteredGazeDataStream;
        EyePositionDataStream lightFilteredPosDataStream;
        Point 視点座標;
        double[] 左目初期 = new double[3];//x,y,z
        double[] 右目初期 = new double[3];
        double 顔の角度初期;

        double[] 左目 = new double[3];//x,y,z
        double[] 右目 = new double[3];
        double 顔の角度;

        bool 視線でカーソル操作 = false;


        public Tobii()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);   
            eyexhost.Start();
            //
            lightlyFilteredGazeDataStream = eyexhost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            lightlyFilteredGazeDataStream.Next += (s, e) => 視点情報格納(s, e);
            lightFilteredPosDataStream=eyexhost.CreateEyePositionDataStream();
            lightFilteredPosDataStream.Next += (s, e) => 眼球位置情報格納(s, e);
            
            //System.Diagnostics.Debug.WriteLine(gaze_data.ToString()); 
        }
        public static Tobii Instance
        {
            get
            {
                //_instanceがnullまたは破棄されているときは、
                //新しくインスタンスを作成する
                if (_instance == null || _instance.IsDisposed)
                    _instance = new Tobii();
                return _instance;
            }
        }
        void 視点情報格納(object s, GazePointEventArgs e)
        {
            //Console.WriteLine("Gaze point at ({0:0.0}, {1:0.0}) @{2:0}", e.X, e.Y, e.Timestamp);
            視点座標.X = (int)e.X;
            視点座標.Y = (int)e.Y;
            

        }
        void 眼球位置情報格納(object s, EyePositionEventArgs e)
        {
            左目[0] = e.LeftEye.X; 右目[0] = e.RightEye.X;
            左目[1] = e.LeftEye.Y; 右目[1] = e.RightEye.Y;
            左目[2] = e.LeftEye.Z; 右目[2] = e.RightEye.Z;

                      
        }
        private void timer_Tick(object sender, EventArgs e)//タイマ割り込みで行う処理
        {
            if (視線でカーソル操作)
            {
                //マウスポインタの位置を設定する
                System.Windows.Forms.Cursor.Position = 視点座標;
            }
            label_point.Text = "Point=(" + 視点座標.X + "," + 視点座標.Y + ")";
            label_L.Text = "L=(" + (int)左目[0] + "," + (int)左目[1] + "," + (int)左目[2] + ")";
            label_R.Text = "R=(" + (int)右目[0] + "," + (int)右目[1] + "," + (int)右目[2] + ")";
            顔の角度 = 顔の角度計算();
            label_angle.Text = "顔角度=(" + (int)顔の角度 + "," + (int)(顔の角度 - 顔の角度初期) + ")";
        }
        double 顔の角度計算()
        {
            double[] x軸 = { 1, 0, 0 };
            double[] ベクトル= new double[3];
            double angle;
            for (int i = 0; i < 3;i++ ) ベクトル[i] = 左目[i] - 右目[i];

            
            if (右目[0] == 0 && 左目[0] == 0) angle = 0;
            else if (右目[0] == 0) angle = 60;
            else if (左目[0] == 0) angle = -60;
            else
            {
                angle = AngleOf2Vector(x軸, ベクトル);
                if (右目[2] < 左目[2]) angle *= -1;
            }

            return angle;

        }
        private void Click_start(object sender, EventArgs e)
        {
            視線でカーソル操作 = true;
            左目初期 = 左目;//初期位置を格納
            右目初期 = 右目;
            顔の角度初期 = 顔の角度計算();
            timer.Interval = int.Parse(textBox_interval.Text);
            timer.Start();
        }

        private void Click_stop(object sender, EventArgs e)
        {
            視線でカーソル操作 = false;
            timer.Stop();
        }


        //ベクトルの長さを計算する
        double get_vector_length(double[] v)//3次元
        {
            return Math.Pow((v[0] * v[0]) + (v[1] * v[1]) + (v[2] * v[2]), 0.5);
        }

        //ベクトル内積
        double dot_product(double[] vl, double[] vr)
        {
            return vl[0] * vr[0] + vl[1] * vr[1] + vl[2] * vr[2];
        }

        //２つのベクトルABのなす角度θを求める
        double AngleOf2Vector(double[] A, double[] B)
        {
            //　※ベクトルの長さが0だと答えが出ませんので注意してください。

            //ベクトルAとBの長さを計算する
            double length_A = get_vector_length(A);
            double length_B = get_vector_length(B);

            //内積とベクトル長さを使ってcosθを求める
            double cos_sita = dot_product(A, B) / (length_A * length_B);

            //cosθからθを求める
            double sita = Math.Acos(cos_sita);

            //ラジアンでなく0～180の角度でほしい場合はコメント外す
            sita = sita * 180.0 / Math.PI;

            return sita;
        }

        private void KeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.A)
            {
                視線でカーソル操作 = false;
                timer.Stop();
            }
        }
    }
}
