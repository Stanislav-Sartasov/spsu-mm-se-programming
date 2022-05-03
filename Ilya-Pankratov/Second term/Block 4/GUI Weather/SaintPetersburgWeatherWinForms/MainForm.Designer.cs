namespace SaintPetersburgWeatherWinForms
{
    partial class MainForm
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
            this.forecastIntervalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.daily = new System.Windows.Forms.ToolStripMenuItem();
            this.weekly = new System.Windows.Forms.ToolStripMenuItem();
            this.connectedSitesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWeather = new System.Windows.Forms.ToolStripMenuItem();
            this.tomorrowIO = new System.Windows.Forms.ToolStripMenuItem();
            this.stormGlass = new System.Windows.Forms.ToolStripMenuItem();
            this.updateButton = new System.Windows.Forms.Button();
            this.weatherSourceLabel = new System.Windows.Forms.Label();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.titleLabel = new System.Windows.Forms.Label();
            this.openWeatherButton = new System.Windows.Forms.Button();
            this.tomorrowIOButton = new System.Windows.Forms.Button();
            this.stormGlassButton = new System.Windows.Forms.Button();
            this.buttonPanel = new System.Windows.Forms.Panel();
            this.exitButton = new System.Windows.Forms.Button();
            this.menu.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.buttonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // forecastIntervalToolStripMenuItem
            // 
            this.forecastIntervalToolStripMenuItem.Name = "forecastIntervalToolStripMenuItem";
            this.forecastIntervalToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.forecastIntervalToolStripMenuItem.Text = "Forecast Interval";
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.connectedSitesToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1370, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "Settings ";
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.daily,
            this.weekly});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(105, 20);
            this.settingsToolStripMenuItem.Text = "Forecast Interval";
            // 
            // daily
            // 
            this.daily.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.daily.Name = "daily";
            this.daily.Size = new System.Drawing.Size(112, 22);
            this.daily.Text = "Daily";
            this.daily.Click += new System.EventHandler(this.daily_Click);
            // 
            // weekly
            // 
            this.weekly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.weekly.Name = "weekly";
            this.weekly.Size = new System.Drawing.Size(112, 22);
            this.weekly.Text = "Weekly";
            this.weekly.Click += new System.EventHandler(this.weekly_Click);
            // 
            // connectedSitesToolStripMenuItem
            // 
            this.connectedSitesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWeather,
            this.tomorrowIO,
            this.stormGlass});
            this.connectedSitesToolStripMenuItem.Name = "connectedSitesToolStripMenuItem";
            this.connectedSitesToolStripMenuItem.Size = new System.Drawing.Size(104, 20);
            this.connectedSitesToolStripMenuItem.Text = "Connected Sites";
            // 
            // openWeather
            // 
            this.openWeather.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.openWeather.Name = "openWeather";
            this.openWeather.Size = new System.Drawing.Size(147, 22);
            this.openWeather.Text = "OpenWeather";
            this.openWeather.Click += new System.EventHandler(this.openWeather_Click);
            // 
            // tomorrowIO
            // 
            this.tomorrowIO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.tomorrowIO.Name = "tomorrowIO";
            this.tomorrowIO.Size = new System.Drawing.Size(147, 22);
            this.tomorrowIO.Text = "TomorrowIO";
            this.tomorrowIO.Click += new System.EventHandler(this.tomorrowIO_Click);
            // 
            // stormGlass
            // 
            this.stormGlass.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(223)))), ((int)(((byte)(230)))));
            this.stormGlass.Name = "stormGlass";
            this.stormGlass.Size = new System.Drawing.Size(147, 22);
            this.stormGlass.Text = "StormGlass";
            this.stormGlass.Click += new System.EventHandler(this.stormGlass_Click);
            // 
            // updateButton
            // 
            this.updateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.updateButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.updateButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(165)))), ((int)(((byte)(173)))));
            this.updateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.updateButton.Location = new System.Drawing.Point(33, 475);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(215, 65);
            this.updateButton.TabIndex = 1;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // weatherSourceLabel
            // 
            this.weatherSourceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(87)))), ((int)(((byte)(91)))));
            this.weatherSourceLabel.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.weatherSourceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.weatherSourceLabel.Location = new System.Drawing.Point(0, 0);
            this.weatherSourceLabel.Name = "weatherSourceLabel";
            this.weatherSourceLabel.Size = new System.Drawing.Size(281, 80);
            this.weatherSourceLabel.TabIndex = 16;
            this.weatherSourceLabel.Text = "Weather Sources";
            this.weatherSourceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // titlePanel
            // 
            this.titlePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.titlePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.titlePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(165)))), ((int)(((byte)(173)))));
            this.titlePanel.Controls.Add(this.titleLabel);
            this.titlePanel.Location = new System.Drawing.Point(281, 24);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(1089, 706);
            this.titlePanel.TabIndex = 17;
            // 
            // titleLabel
            // 
            this.titleLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.titleLabel.Enabled = false;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.titleLabel.Location = new System.Drawing.Point(0, 0);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(1089, 706);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "Saint Petersburg Weather Forecast on every day";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openWeatherButton
            // 
            this.openWeatherButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(165)))), ((int)(((byte)(173)))));
            this.openWeatherButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openWeatherButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.openWeatherButton.Location = new System.Drawing.Point(33, 100);
            this.openWeatherButton.Name = "openWeatherButton";
            this.openWeatherButton.Size = new System.Drawing.Size(215, 65);
            this.openWeatherButton.TabIndex = 18;
            this.openWeatherButton.Text = "OpenWeather";
            this.openWeatherButton.UseVisualStyleBackColor = false;
            this.openWeatherButton.Click += new System.EventHandler(this.openWeatherButton_Click);
            // 
            // tomorrowIOButton
            // 
            this.tomorrowIOButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(165)))), ((int)(((byte)(173)))));
            this.tomorrowIOButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tomorrowIOButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.tomorrowIOButton.Location = new System.Drawing.Point(33, 225);
            this.tomorrowIOButton.Name = "tomorrowIOButton";
            this.tomorrowIOButton.Size = new System.Drawing.Size(215, 65);
            this.tomorrowIOButton.TabIndex = 19;
            this.tomorrowIOButton.Text = "TomorrowIO";
            this.tomorrowIOButton.UseVisualStyleBackColor = false;
            this.tomorrowIOButton.Click += new System.EventHandler(this.tomorrowIOButton_Click);
            // 
            // stormGlassButton
            // 
            this.stormGlassButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(165)))), ((int)(((byte)(173)))));
            this.stormGlassButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.stormGlassButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(39)))), ((int)(((byte)(46)))));
            this.stormGlassButton.Location = new System.Drawing.Point(33, 350);
            this.stormGlassButton.Name = "stormGlassButton";
            this.stormGlassButton.Size = new System.Drawing.Size(215, 65);
            this.stormGlassButton.TabIndex = 20;
            this.stormGlassButton.Text = "StormGlass";
            this.stormGlassButton.UseVisualStyleBackColor = false;
            this.stormGlassButton.Click += new System.EventHandler(this.stormGlassButton_Click);
            // 
            // buttonPanel
            // 
            this.buttonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(87)))), ((int)(((byte)(91)))));
            this.buttonPanel.Controls.Add(this.exitButton);
            this.buttonPanel.Controls.Add(this.updateButton);
            this.buttonPanel.Controls.Add(this.stormGlassButton);
            this.buttonPanel.Controls.Add(this.weatherSourceLabel);
            this.buttonPanel.Controls.Add(this.openWeatherButton);
            this.buttonPanel.Controls.Add(this.tomorrowIOButton);
            this.buttonPanel.Location = new System.Drawing.Point(0, 24);
            this.buttonPanel.Name = "buttonPanel";
            this.buttonPanel.Size = new System.Drawing.Size(281, 706);
            this.buttonPanel.TabIndex = 18;
            // 
            // exitButton
            // 
            this.exitButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.exitButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(165)))), ((int)(((byte)(173)))));
            this.exitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(70)))));
            this.exitButton.Location = new System.Drawing.Point(33, 600);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(215, 65);
            this.exitButton.TabIndex = 21;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1370, 729);
            this.Controls.Add(this.buttonPanel);
            this.Controls.Add(this.titlePanel);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(1386, 768);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Saint Petersburg Weather Forecast";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.titlePanel.ResumeLayout(false);
            this.buttonPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ToolStripMenuItem forecastIntervalToolStripMenuItem;
        private MenuStrip menu;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private Button updateButton;
        private Label weatherSourceLabel;
        private Panel titlePanel;
        private Button openWeatherButton;
        private Button tomorrowIOButton;
        private Button stormGlassButton;
        private Label titleLabel;
        private Panel buttonPanel;
        private Button exitButton;
        private ToolStripMenuItem connectedSitesToolStripMenuItem;
        private ToolStripMenuItem daily;
        private ToolStripMenuItem weekly;
        private ToolStripMenuItem tomorrowIO;
        private ToolStripMenuItem openWeather;
        private ToolStripMenuItem stormGlass;
    }
}