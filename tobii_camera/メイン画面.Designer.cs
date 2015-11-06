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
            // メイン画面
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.button_Tobii);
            this.Controls.Add(this.button_Camera);
            this.Name = "メイン画面";
            this.Text = "main";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Camera;
        private System.Windows.Forms.Button button_Tobii;
    }
}

