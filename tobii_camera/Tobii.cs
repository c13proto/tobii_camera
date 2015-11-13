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
        EyePositionDataStream PosDataStream;
        

        //眼球位置
        List<int> POS_L=new List<int>();//平均化のためlist型(x座標のみ
        List<int> POS_R=new List<int>();
        int l_counter,r_counter;//ノイズ除去のためのカウンタ

        //視点座標
        List<int> POINT_X=new List<int>();
        List<int> POINT_Y=new List<int>();

        public static bool 目がない = false;
        public static string debug = "";
        public static Point 視点座標;
        public static int[] 眼球位置=new int[2];
        public static int pos_max = 1000;
        public Tobii()
        {
            InitializeComponent();
            timer = new Timer();
            timer.Tick += new EventHandler(timer_Tick);   
            eyexhost.Start();
            lightlyFilteredGazeDataStream = eyexhost.CreateGazePointDataStream(GazePointDataMode.LightlyFiltered);
            lightlyFilteredGazeDataStream.Next += (s, e) => 視点情報格納(s, e);
            PosDataStream=eyexhost.CreateEyePositionDataStream();
            PosDataStream.Next += (s, e) => 眼球位置情報格納(s, e);//何かしらフィルタかけるべきかも


            timer.Interval = int.Parse(textBox_interval.Text);
            timer.Start();
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
            POINT_X.Add((int)e.X);
            POINT_Y.Add((int)e.Y);

        }
        void 眼球位置情報格納(object s, EyePositionEventArgs e)
        {
            //double[] left = { e.LeftEyeNormalized.X, e.LeftEyeNormalized.Y, e.LeftEyeNormalized.Z};
            //double[] right = { e.RightEyeNormalized.X, e.RightEyeNormalized.Y, e.RightEyeNormalized.Z };
            const int limit = 3;
            if (e.LeftEyeNormalized.X == 0)
            {
                l_counter++;
                if (l_counter > limit) POS_L.Add(0);//limit回連続で0が来たら
            }
            else
            {
                POS_L.Add((int)(pos_max - e.LeftEyeNormalized.X * pos_max));//x座標だけでいい気がしてきた
                l_counter = 0;
            }
            if (e.RightEyeNormalized.X == 0)
            {
                r_counter++;
                if (r_counter > limit) POS_R.Add(pos_max);
            }
            else
            {
                POS_R.Add((int)(pos_max - e.RightEyeNormalized.X * pos_max));
                r_counter = 0;
            }
        }
        private void timer_Tick(object sender, EventArgs e)//タイマ割り込みで行う処理
        {
            眼球位置[0] = 平均計算(POS_L);//何故かAverage()が使えない
            眼球位置[1] = 平均計算(POS_R);
            視点座標.X = (int)平均計算(POINT_X);
            視点座標.Y = (int)平均計算(POINT_Y);

            if (眼球位置[0] == 0 && 眼球位置[1] == 0) 目がない = true;
            else 目がない = false;

            if(!目がない&&checkBox_mouse.Checked)System.Windows.Forms.Cursor.Position = 視点座標;

            label_point.Text = "Point=(" + 視点座標.X + "," + 視点座標.Y + ")";
            label_L.Text = "L=" + 眼球位置[0];
            label_R.Text = "R=" + 眼球位置[1];

            debug = "count(pos,point)=(" + POS_R.Count +","+POINT_X.Count+")"+ "\n" +
                    "Position=" + ((眼球位置[1] + 眼球位置[0]) / 2 - pos_max/2) + "\n" +
                    "diff=" + (眼球位置[1] - 眼球位置[0]) + "\n" +
                    "Point=(" + 視点座標.X + "," + 視点座標.Y + ")";

            POS_L.Clear();
            POS_R.Clear();
            POINT_X.Clear();
            POINT_Y.Clear();
                        
        }

        private void Click_start(object sender, EventArgs e)
        {
            timer.Interval = int.Parse(textBox_interval.Text);
            timer.Start();
        }

        private void Click_stop(object sender, EventArgs e)
        {
            timer.Stop();
        }
        int 平均計算(List<int> data)
        {
            double ave=0;
            for (int i = 0; i < data.Count; i++) ave+=data[i];
            ave /= data.Count;
            return (int)ave;
        }


    }
}
