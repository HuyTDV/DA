namespace QLNhaHang
{
    partial class frmDangNhap
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDangNhap));
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            ptClose = new PictureBox();
            pictureBox4 = new PictureBox();
            txtPassword = new TextBox();
            label1 = new Label();
            btnLogin = new Button();
            pictureBox5 = new PictureBox();
            txtName = new TextBox();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ptClose).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(428, 450);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(538, 38);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(172, 117);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // ptClose
            // 
            ptClose.Image = (Image)resources.GetObject("ptClose.Image");
            ptClose.Location = new Point(755, 7);
            ptClose.Name = "ptClose";
            ptClose.Size = new Size(40, 42);
            ptClose.SizeMode = PictureBoxSizeMode.Zoom;
            ptClose.TabIndex = 2;
            ptClose.TabStop = false;
            ptClose.Click += ptClose_Click;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = (Image)resources.GetObject("pictureBox4.Image");
            pictureBox4.Location = new Point(482, 297);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(48, 27);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(467, 286);
            txtPassword.MaximumSize = new Size(300, 50);
            txtPassword.MinimumSize = new Size(300, 50);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(300, 50);
            txtPassword.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(467, 268);
            label1.Name = "label1";
            label1.Size = new Size(57, 15);
            label1.TabIndex = 5;
            label1.Text = "Mật khẩu";
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(533, 366);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(173, 53);
            btnLogin.TabIndex = 6;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = true;
            
            // 
            // pictureBox5
            // 
            pictureBox5.Image = (Image)resources.GetObject("pictureBox5.Image");
            pictureBox5.Location = new Point(476, 208);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(48, 27);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 3;
            pictureBox5.TabStop = false;
            // 
            // txtName
            // 
            txtName.Location = new Point(467, 196);
            txtName.MaximumSize = new Size(300, 50);
            txtName.MinimumSize = new Size(300, 50);
            txtName.Name = "txtName";
            txtName.Size = new Size(300, 50);
            txtName.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(467, 178);
            label2.Name = "label2";
            label2.Size = new Size(58, 15);
            label2.TabIndex = 5;
            label2.Text = "Tài khoản";
            // 
            // frmDangNhap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(ptClose);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(txtName);
            Controls.Add(txtPassword);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmDangNhap";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)ptClose).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox ptClose;
        private PictureBox pictureBox4;
        private TextBox txtPassword;
        private Label label1;
        private Button btnLogin;
        private PictureBox pictureBox5;
        private TextBox txtName;
        private Label label2;
    }
}
