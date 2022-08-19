namespace WinFormsWeatherForecast
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StormGlassIO = new System.Windows.Forms.Button();
            this.TomorrowIO = new System.Windows.Forms.Button();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MainMenuText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // StormGlassIO
            // 
            this.StormGlassIO.BackColor = System.Drawing.Color.NavajoWhite;
            this.StormGlassIO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("StormGlassIO.BackgroundImage")));
            this.StormGlassIO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.StormGlassIO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StormGlassIO.FlatAppearance.BorderSize = 0;
            this.StormGlassIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StormGlassIO.Location = new System.Drawing.Point(285, 211);
            this.StormGlassIO.Margin = new System.Windows.Forms.Padding(0);
            this.StormGlassIO.Name = "StormGlassIO";
            this.StormGlassIO.Size = new System.Drawing.Size(250, 151);
            this.StormGlassIO.TabIndex = 1;
            this.StormGlassIO.UseVisualStyleBackColor = false;
            this.StormGlassIO.Click += new System.EventHandler(this.StormGlassIO_Click);
            // 
            // TomorrowIO
            // 
            this.TomorrowIO.BackColor = System.Drawing.Color.NavajoWhite;
            this.TomorrowIO.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("TomorrowIO.BackgroundImage")));
            this.TomorrowIO.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.TomorrowIO.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TomorrowIO.FlatAppearance.BorderSize = 0;
            this.TomorrowIO.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TomorrowIO.Location = new System.Drawing.Point(-4, 211);
            this.TomorrowIO.Margin = new System.Windows.Forms.Padding(0);
            this.TomorrowIO.Name = "TomorrowIO";
            this.TomorrowIO.Size = new System.Drawing.Size(289, 151);
            this.TomorrowIO.TabIndex = 0;
            this.TomorrowIO.UseVisualStyleBackColor = false;
            this.TomorrowIO.Click += new System.EventHandler(this.TomorrowIO_Click);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MinimizeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MinimizeButton.BackgroundImage")));
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Location = new System.Drawing.Point(466, 12);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(25, 25);
            this.MinimizeButton.TabIndex = 10;
            this.MinimizeButton.UseVisualStyleBackColor = false;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.Tomato;
            this.CloseButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("CloseButton.BackgroundImage")));
            this.CloseButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseButton.Location = new System.Drawing.Point(497, 12);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(25, 25);
            this.CloseButton.TabIndex = 9;
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MainMenuText
            // 
            this.MainMenuText.AutoSize = true;
            this.MainMenuText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainMenuText.Font = new System.Drawing.Font("Impact", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.MainMenuText.Location = new System.Drawing.Point(114, 78);
            this.MainMenuText.Name = "MainMenuText";
            this.MainMenuText.Size = new System.Drawing.Size(320, 156);
            this.MainMenuText.TabIndex = 2;
            this.MainMenuText.Text = "Choose one of the sites\r\nto get the weather in \r\nSt. Petersburg\r\n\r\n";
            this.MainMenuText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(534, 361);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.MinimizeButton);
            this.Controls.Add(this.StormGlassIO);
            this.Controls.Add(this.TomorrowIO);
            this.Controls.Add(this.MainMenuText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label MainMenuText;
        private Button TomorrowIO;
        private Button CloseButton;
        private Button MinimizeButton;
        private Button StormGlassIO;
    }
}