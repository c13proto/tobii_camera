namespace tobii_camera
{
    partial class メイン画面
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Camera = new System.Windows.Forms.Button();
            this.button_Tobii = new System.Windows.Forms.Button();
            this.button_start = new System.Windows.Forms.Button();
            this.label_debug = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_fps = new System.Windows.Forms.TextBox();
            this.textBox_window_y = new System.Windows.Forms.TextBox();
            this.textBox_window_x = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_space = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_Camera
            // 
            this.button_Camera.Location = new System.Drawing.Point(12, 12);
            this.button_Camera.Name = "button_Camera";
            this.button_Camera.Size = new System.Drawing.Size(75, 23);
            this.button_Camera.TabIndex = 0;
            this.button_Camera.Text = "Camera";
            this.button_Camera.UseVisualStyleBackColor = true;
            this.button_Camera.Click += new System.EventHandler(this.Click_Camera);
            // 
            // button_Tobii
            // 
            this.button_Tobii.Location = new System.Drawing.Point(12, 41);
            this.button_Tobii.Name = "button_Tobii";
            this.button_Tobii.Size = new System.Drawing.Size(75, 23);
            this.button_Tobii.TabIndex = 1;
            this.button_Tobii.Text = "Tobii";
            this.button_Tobii.UseVisualStyleBackColor = true;
            this.button_Tobii.Click += new System.EventHandler(this.Click_Tobii);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(11, 214);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(75, 23);
            this.button_start.TabIndex = 2;
            this.button_start.Text = "start";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.Click_start);
            // 
            // label_debug
            // 
            this.label_debug.AutoSize = true;
            this.label_debug.Location = new System.Drawing.Point(93, 12);
            this.label_debug.Name = "label_debug";
            this.label_debug.Size = new System.Drawing.Size(35, 12);
            this.label_debug.TabIndex = 3;
            this.label_debug.Text = "debug";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 84);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "更新間隔(fps)";
            // 
            // textBox_fps
            // 
            this.textBox_fps.Location = new System.Drawing.Point(61, 100);
            this.textBox_fps.Name = "textBox_fps";
            this.textBox_fps.Size = new System.Drawing.Size(25, 19);
            this.textBox_fps.TabIndex = 5;
            this.textBox_fps.Text = "50";
            // 
            // textBox_window_y
            // 
            this.textBox_window_y.Location = new System.Drawing.Point(61, 136);
            this.textBox_window_y.Name = "textBox_window_y";
            this.textBox_window_y.Size = new System.Drawing.Size(25, 19);
            this.textBox_window_y.TabIndex = 6;
            this.textBox_window_y.Text = "240";
            // 
            // textBox_window_x
            // 
            this.textBox_window_x.Location = new System.Drawing.Point(30, 136);
            this.textBox_window_x.Name = "textBox_window_x";
            this.textBox_window_x.Size = new System.Drawing.Size(25, 19);
            this.textBox_window_x.TabIndex = 7;
            this.textBox_window_x.Text = "320";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 158);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "|| の間隔";
            // 
            // textBox_space
            // 
            this.textBox_space.Location = new System.Drawing.Point(61, 172);
            this.textBox_space.Name = "textBox_space";
            this.textBox_space.Size = new System.Drawing.Size(25, 19);
            this.textBox_space.TabIndex = 10;
            this.textBox_space.Text = "320";
            // 
            // メイン画面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(316, 267);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_space);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_window_x);
            this.Controls.Add(this.textBox_window_y);
            this.Controls.Add(this.textBox_fps);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_debug);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.button_Tobii);
            this.Controls.Add(this.button_Camera);
            this.Name = "メイン画面";
            this.Text = "main";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Camera;
        private System.Windows.Forms.Button button_Tobii;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_fps;
        private System.Windows.Forms.Label label_debug;
        private System.Windows.Forms.TextBox textBox_window_y;
        private System.Windows.Forms.TextBox textBox_window_x;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_space;
    }
}

