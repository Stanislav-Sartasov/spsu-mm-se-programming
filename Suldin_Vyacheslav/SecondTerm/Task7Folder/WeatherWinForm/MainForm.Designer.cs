
namespace WeatherWinForm
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
            this.ShowButton = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.PictureBox();
            this.View = new System.Windows.Forms.ListView();
            this.WebService = new System.Windows.Forms.ColumnHeader();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)(this.Settings)).BeginInit();
            this.SuspendLayout();
            // 
            // ShowButton
            // 
            this.ShowButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ShowButton.BackgroundImage")));
            this.ShowButton.Font = new System.Drawing.Font("MV Boli", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ShowButton.ForeColor = System.Drawing.Color.DarkSlateBlue;
            this.ShowButton.Location = new System.Drawing.Point(0, 0);
            this.ShowButton.Name = "ShowButton";
            this.ShowButton.Size = new System.Drawing.Size(237, 57);
            this.ShowButton.TabIndex = 0;
            this.ShowButton.Text = "Show Weather";
            this.ShowButton.UseVisualStyleBackColor = true;
            this.ShowButton.Click += new System.EventHandler(this.ShowButton_Click);
            // 
            // Settings
            // 
            this.Settings.BackColor = System.Drawing.Color.Transparent;
            this.Settings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Settings.BackgroundImage")));
            this.Settings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Settings.Location = new System.Drawing.Point(1104, 0);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(58, 57);
            this.Settings.TabIndex = 2;
            this.Settings.TabStop = false;
            this.Settings.Click += new System.EventHandler(this.Settings_Click);
            // 
            // View
            // 
            this.View.AutoArrange = false;
            this.View.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("View.BackgroundImage")));
            this.View.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.WebService,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.View.Cursor = System.Windows.Forms.Cursors.Default;
            this.View.Enabled = false;
            this.View.Font = new System.Drawing.Font("MV Boli", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.View.ForeColor = System.Drawing.SystemColors.WindowText;
            this.View.GridLines = true;
            this.View.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.View.HideSelection = false;
            this.View.LabelWrap = false;
            this.View.Location = new System.Drawing.Point(0, 94);
            this.View.Name = "View";
            this.View.Scrollable = false;
            this.View.Size = new System.Drawing.Size(1162, 28);
            this.View.TabIndex = 3;
            this.View.UseCompatibleStateImageBehavior = false;
            this.View.View = System.Windows.Forms.View.Details;
            // 
            // WebService
            // 
            this.WebService.Text = "WebService";
            this.WebService.Width = 225;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Temperature (C)";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Temperature (F)";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Cloud Cover (%)";
            this.columnHeader4.Width = 120;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Humidity (%)";
            this.columnHeader5.Width = 120;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Precipipation(type+mm)";
            this.columnHeader6.Width = 180;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "WindSpeed (m/s)";
            this.columnHeader7.Width = 130;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Wind Direction () ";
            this.columnHeader8.Width = 150;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1161, 791);
            this.Controls.Add(this.View);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.ShowButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1179, 838);
            this.MinimumSize = new System.Drawing.Size(1179, 838);
            this.Name = "MainForm";
            this.Text = "WeatherApp";
            ((System.ComponentModel.ISupportInitialize)(this.Settings)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ShowButton;
        private System.Windows.Forms.PictureBox Settings;
        private System.Windows.Forms.ListView View;
        private System.Windows.Forms.ColumnHeader WebService;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
    }
}

