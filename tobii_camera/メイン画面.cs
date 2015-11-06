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
        public メイン画面()
        {
            InitializeComponent();
            Camera.Instance.Show();
            Tobii.Instance.Show();
        }

        private void Click_Camera(object sender, EventArgs e)
        {
            Camera.Instance.Show();
        }

        private void Click_Tobii(object sender, EventArgs e)
        {
            Tobii.Instance.Show();
        }
    }
}
