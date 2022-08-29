namespace Xmas_Grotto_Information
{
    partial class frmStartup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStartup));
            this.cmdSettings = new System.Windows.Forms.Button();
            this.cmdGrottoController = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdSettings
            // 
            this.cmdSettings.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdSettings.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdSettings.FlatAppearance.BorderSize = 5;
            this.cmdSettings.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.cmdSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSettings.Font = new System.Drawing.Font("Century", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSettings.Location = new System.Drawing.Point(33, 290);
            this.cmdSettings.Name = "cmdSettings";
            this.cmdSettings.Size = new System.Drawing.Size(192, 65);
            this.cmdSettings.TabIndex = 7;
            this.cmdSettings.Text = "Settings";
            this.cmdSettings.UseVisualStyleBackColor = false;
            this.cmdSettings.Click += new System.EventHandler(this.cmdSettings_Click);
            // 
            // cmdGrottoController
            // 
            this.cmdGrottoController.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdGrottoController.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdGrottoController.FlatAppearance.BorderSize = 5;
            this.cmdGrottoController.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.cmdGrottoController.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdGrottoController.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdGrottoController.Font = new System.Drawing.Font("Century", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdGrottoController.Location = new System.Drawing.Point(33, 210);
            this.cmdGrottoController.Name = "cmdGrottoController";
            this.cmdGrottoController.Size = new System.Drawing.Size(192, 65);
            this.cmdGrottoController.TabIndex = 6;
            this.cmdGrottoController.Text = "Grotto Information";
            this.cmdGrottoController.UseVisualStyleBackColor = false;
            this.cmdGrottoController.Click += new System.EventHandler(this.cmdGrottoController_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.BackColor = System.Drawing.Color.Goldenrod;
            this.cmdClose.FlatAppearance.BorderColor = System.Drawing.Color.DarkGoldenrod;
            this.cmdClose.FlatAppearance.BorderSize = 5;
            this.cmdClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.cmdClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.cmdClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdClose.Font = new System.Drawing.Font("Century", 12.22642F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdClose.Location = new System.Drawing.Point(33, 374);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(192, 65);
            this.cmdClose.TabIndex = 5;
            this.cmdClose.Text = "Close Grotto Information";
            this.cmdClose.UseVisualStyleBackColor = false;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(254, 202);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // frmStartup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(258, 448);
            this.Controls.Add(this.cmdSettings);
            this.Controls.Add(this.cmdGrottoController);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmStartup";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmStartup_FormClosing);
            this.Load += new System.EventHandler(this.frmStartup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button cmdSettings;
        private System.Windows.Forms.Button cmdGrottoController;
        private System.Windows.Forms.Button cmdClose;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

