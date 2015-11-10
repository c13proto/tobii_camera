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
        Point 視線座標;
        bool 視線でカーソル操作 = false;


        public Tobii()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);   
            eyexhost.Start();
            lightlyFilteredGazeDataStream = eyexhost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            lightlyFilteredGazeDataStream.Next += (s, e) => 視線情報格納(s, e);

            
            //gaze_data=eyexhost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
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
        void 視線情報格納(object s, GazePointEventArgs e)
        {
            Console.WriteLine("Gaze point at ({0:0.0}, {1:0.0}) @{2:0}", e.X, e.Y, e.Timestamp);
            視線座標.X = (int)e.X;
            視線座標.Y = (int)e.Y;

        }
        private void timer_Tick(object sender, EventArgs e)//タイマ割り込みで行う処理
        {
            if (視線でカーソル操作)
            {
                System.Drawing.Point mp = new Point(視線座標.X, 視線座標.Y);
                //マウスポインタの位置を設定する
                System.Windows.Forms.Cursor.Position = mp;
            }
        }

        private void Click_start(object sender, EventArgs e)
        {
            視線でカーソル操作 = true;
            timer.Interval = int.Parse(textBox_interval.Text);
            timer.Start();
        }

        private void Click_stop(object sender, EventArgs e)
        {
            視線でカーソル操作 = false;
            timer.Stop();
        }
    }
}
