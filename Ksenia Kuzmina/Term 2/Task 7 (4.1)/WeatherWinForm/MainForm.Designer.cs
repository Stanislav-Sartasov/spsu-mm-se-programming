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
			this.leftPanel = new System.Windows.Forms.Panel();
			this.openWeatherWindSpeedLabel = new System.Windows.Forms.Label();
			this.openWeatherWindDirectionLabel = new System.Windows.Forms.Label();
			this.openWeatherPrecipationLabel = new System.Windows.Forms.Label();
			this.openWeatherHumidityLabel = new System.Windows.Forms.Label();
			this.openWeatherCloudCoverLabel = new System.Windows.Forms.Label();
			this.openWeatherFahrenheitLabel = new System.Windows.Forms.Label();
			this.openWeatherCelsiusLabel = new System.Windows.Forms.Label();
			this.WindSpeedLabel = new System.Windows.Forms.Label();
			this.WindDirectionLabel = new System.Windows.Forms.Label();
			this.PrecipationLabel = new System.Windows.Forms.Label();
			this.HumidityLabel = new System.Windows.Forms.Label();
			this.CloudCoverLabel = new System.Windows.Forms.Label();
			this.FahrenheitLabel = new System.Windows.Forms.Label();
			this.CelsiusLabel = new System.Windows.Forms.Label();
			this.rightPanel = new System.Windows.Forms.Panel();
			this.tomorrowIoWindSpeedLabel = new System.Windows.Forms.Label();
			this.tomorrowIoWindDirectionLabel = new System.Windows.Forms.Label();
			this.tomorrowIoPrecipationLabel = new System.Windows.Forms.Label();
			this.tomorrowIoHumidityLabel = new System.Windows.Forms.Label();
			this.tomorrowIoCloudCoverLabel = new System.Windows.Forms.Label();
			this.tomorrowIoFahrenheitLabel = new System.Windows.Forms.Label();
			this.tomorrowIoCelsiusLabel = new System.Windows.Forms.Label();
			this.buttonUpdate = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.pictureBoxOpenWeather = new System.Windows.Forms.PictureBox();
			this.pictureBoxTomorrowIo = new System.Windows.Forms.PictureBox();
			this.leftPanel.SuspendLayout();
			this.rightPanel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxOpenWeather)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTomorrowIo)).BeginInit();
			this.SuspendLayout();
			// 
			// leftPanel
			// 
			this.leftPanel.Controls.Add(this.pictureBoxOpenWeather);
			this.leftPanel.Controls.Add(this.openWeatherWindSpeedLabel);
			this.leftPanel.Controls.Add(this.openWeatherWindDirectionLabel);
			this.leftPanel.Controls.Add(this.openWeatherPrecipationLabel);
			this.leftPanel.Controls.Add(this.openWeatherHumidityLabel);
			this.leftPanel.Controls.Add(this.openWeatherCloudCoverLabel);
			this.leftPanel.Controls.Add(this.openWeatherFahrenheitLabel);
			this.leftPanel.Controls.Add(this.openWeatherCelsiusLabel);
			this.leftPanel.Location = new System.Drawing.Point(12, 12);
			this.leftPanel.Name = "leftPanel";
			this.leftPanel.Size = new System.Drawing.Size(326, 520);
			this.leftPanel.TabIndex = 0;
			// 
			// openWeatherWindSpeedLabel
			// 
			this.openWeatherWindSpeedLabel.AutoSize = true;
			this.openWeatherWindSpeedLabel.Location = new System.Drawing.Point(124, 401);
			this.openWeatherWindSpeedLabel.Name = "openWeatherWindSpeedLabel";
			this.openWeatherWindSpeedLabel.Size = new System.Drawing.Size(69, 20);
			this.openWeatherWindSpeedLabel.TabIndex = 6;
			this.openWeatherWindSpeedLabel.Text = "Waiting...";
			// 
			// openWeatherWindDirectionLabel
			// 
			this.openWeatherWindDirectionLabel.AutoSize = true;
			this.openWeatherWindDirectionLabel.Location = new System.Drawing.Point(124, 348);
			this.openWeatherWindDirectionLabel.Name = "openWeatherWindDirectionLabel";
			this.openWeatherWindDirectionLabel.Size = new System.Drawing.Size(69, 20);
			this.openWeatherWindDirectionLabel.TabIndex = 5;
			this.openWeatherWindDirectionLabel.Text = "Waiting...";
			// 
			// openWeatherPrecipationLabel
			// 
			this.openWeatherPrecipationLabel.AutoSize = true;
			this.openWeatherPrecipationLabel.Location = new System.Drawing.Point(124, 300);
			this.openWeatherPrecipationLabel.Name = "openWeatherPrecipationLabel";
			this.openWeatherPrecipationLabel.Size = new System.Drawing.Size(69, 20);
			this.openWeatherPrecipationLabel.TabIndex = 4;
			this.openWeatherPrecipationLabel.Text = "Waiting...";
			// 
			// openWeatherHumidityLabel
			// 
			this.openWeatherHumidityLabel.AutoSize = true;
			this.openWeatherHumidityLabel.Location = new System.Drawing.Point(124, 251);
			this.openWeatherHumidityLabel.Name = "openWeatherHumidityLabel";
			this.openWeatherHumidityLabel.Size = new System.Drawing.Size(69, 20);
			this.openWeatherHumidityLabel.TabIndex = 3;
			this.openWeatherHumidityLabel.Text = "Waiting...";
			// 
			// openWeatherCloudCoverLabel
			// 
			this.openWeatherCloudCoverLabel.AutoSize = true;
			this.openWeatherCloudCoverLabel.Location = new System.Drawing.Point(124, 197);
			this.openWeatherCloudCoverLabel.Name = "openWeatherCloudCoverLabel";
			this.openWeatherCloudCoverLabel.Size = new System.Drawing.Size(69, 20);
			this.openWeatherCloudCoverLabel.TabIndex = 2;
			this.openWeatherCloudCoverLabel.Text = "Waiting...";
			// 
			// openWeatherFahrenheitLabel
			// 
			this.openWeatherFahrenheitLabel.AutoSize = true;
			this.openWeatherFahrenheitLabel.Location = new System.Drawing.Point(124, 150);
			this.openWeatherFahrenheitLabel.Name = "openWeatherFahrenheitLabel";
			this.openWeatherFahrenheitLabel.Size = new System.Drawing.Size(69, 20);
			this.openWeatherFahrenheitLabel.TabIndex = 1;
			this.openWeatherFahrenheitLabel.Text = "Waiting...";
			// 
			// openWeatherCelsiusLabel
			// 
			this.openWeatherCelsiusLabel.AutoSize = true;
			this.openWeatherCelsiusLabel.Location = new System.Drawing.Point(124, 102);
			this.openWeatherCelsiusLabel.Name = "openWeatherCelsiusLabel";
			this.openWeatherCelsiusLabel.Size = new System.Drawing.Size(69, 20);
			this.openWeatherCelsiusLabel.TabIndex = 0;
			this.openWeatherCelsiusLabel.Text = "Waiting...";
			// 
			// WindSpeedLabel
			// 
			this.WindSpeedLabel.AutoSize = true;
			this.WindSpeedLabel.Location = new System.Drawing.Point(392, 413);
			this.WindSpeedLabel.Name = "WindSpeedLabel";
			this.WindSpeedLabel.Size = new System.Drawing.Size(88, 20);
			this.WindSpeedLabel.TabIndex = 6;
			this.WindSpeedLabel.Text = "Wind speed";
			// 
			// WindDirectionLabel
			// 
			this.WindDirectionLabel.AutoSize = true;
			this.WindDirectionLabel.Location = new System.Drawing.Point(382, 360);
			this.WindDirectionLabel.Name = "WindDirectionLabel";
			this.WindDirectionLabel.Size = new System.Drawing.Size(107, 20);
			this.WindDirectionLabel.TabIndex = 5;
			this.WindDirectionLabel.Text = "Wind direction";
			// 
			// PrecipationLabel
			// 
			this.PrecipationLabel.AutoSize = true;
			this.PrecipationLabel.Location = new System.Drawing.Point(396, 312);
			this.PrecipationLabel.Name = "PrecipationLabel";
			this.PrecipationLabel.Size = new System.Drawing.Size(84, 20);
			this.PrecipationLabel.TabIndex = 4;
			this.PrecipationLabel.Text = "Precipation";
			// 
			// HumidityLabel
			// 
			this.HumidityLabel.AutoSize = true;
			this.HumidityLabel.Location = new System.Drawing.Point(401, 263);
			this.HumidityLabel.Name = "HumidityLabel";
			this.HumidityLabel.Size = new System.Drawing.Size(70, 20);
			this.HumidityLabel.TabIndex = 3;
			this.HumidityLabel.Text = "Humidity";
			// 
			// CloudCoverLabel
			// 
			this.CloudCoverLabel.AutoSize = true;
			this.CloudCoverLabel.Location = new System.Drawing.Point(392, 209);
			this.CloudCoverLabel.Name = "CloudCoverLabel";
			this.CloudCoverLabel.Size = new System.Drawing.Size(88, 20);
			this.CloudCoverLabel.TabIndex = 2;
			this.CloudCoverLabel.Text = "Cloud cover";
			// 
			// FahrenheitLabel
			// 
			this.FahrenheitLabel.AutoSize = true;
			this.FahrenheitLabel.Location = new System.Drawing.Point(358, 162);
			this.FahrenheitLabel.Name = "FahrenheitLabel";
			this.FahrenheitLabel.Size = new System.Drawing.Size(163, 20);
			this.FahrenheitLabel.TabIndex = 1;
			this.FahrenheitLabel.Text = "Fahrenheit temperature";
			// 
			// CelsiusLabel
			// 
			this.CelsiusLabel.AutoSize = true;
			this.CelsiusLabel.Location = new System.Drawing.Point(372, 114);
			this.CelsiusLabel.Name = "CelsiusLabel";
			this.CelsiusLabel.Size = new System.Drawing.Size(140, 20);
			this.CelsiusLabel.TabIndex = 0;
			this.CelsiusLabel.Text = "Celsius temperature";
			this.CelsiusLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// rightPanel
			// 
			this.rightPanel.Controls.Add(this.pictureBoxTomorrowIo);
			this.rightPanel.Controls.Add(this.tomorrowIoWindSpeedLabel);
			this.rightPanel.Controls.Add(this.tomorrowIoWindDirectionLabel);
			this.rightPanel.Controls.Add(this.tomorrowIoPrecipationLabel);
			this.rightPanel.Controls.Add(this.tomorrowIoHumidityLabel);
			this.rightPanel.Controls.Add(this.tomorrowIoCloudCoverLabel);
			this.rightPanel.Controls.Add(this.tomorrowIoFahrenheitLabel);
			this.rightPanel.Controls.Add(this.tomorrowIoCelsiusLabel);
			this.rightPanel.Location = new System.Drawing.Point(543, 12);
			this.rightPanel.Name = "rightPanel";
			this.rightPanel.Size = new System.Drawing.Size(326, 520);
			this.rightPanel.TabIndex = 1;
			// 
			// tomorrowIoWindSpeedLabel
			// 
			this.tomorrowIoWindSpeedLabel.AutoSize = true;
			this.tomorrowIoWindSpeedLabel.Location = new System.Drawing.Point(127, 401);
			this.tomorrowIoWindSpeedLabel.Name = "tomorrowIoWindSpeedLabel";
			this.tomorrowIoWindSpeedLabel.Size = new System.Drawing.Size(69, 20);
			this.tomorrowIoWindSpeedLabel.TabIndex = 6;
			this.tomorrowIoWindSpeedLabel.Text = "Waiting...";
			// 
			// tomorrowIoWindDirectionLabel
			// 
			this.tomorrowIoWindDirectionLabel.AutoSize = true;
			this.tomorrowIoWindDirectionLabel.Location = new System.Drawing.Point(127, 348);
			this.tomorrowIoWindDirectionLabel.Name = "tomorrowIoWindDirectionLabel";
			this.tomorrowIoWindDirectionLabel.Size = new System.Drawing.Size(69, 20);
			this.tomorrowIoWindDirectionLabel.TabIndex = 5;
			this.tomorrowIoWindDirectionLabel.Text = "Waiting...";
			// 
			// tomorrowIoPrecipationLabel
			// 
			this.tomorrowIoPrecipationLabel.AutoSize = true;
			this.tomorrowIoPrecipationLabel.Location = new System.Drawing.Point(127, 300);
			this.tomorrowIoPrecipationLabel.Name = "tomorrowIoPrecipationLabel";
			this.tomorrowIoPrecipationLabel.Size = new System.Drawing.Size(69, 20);
			this.tomorrowIoPrecipationLabel.TabIndex = 4;
			this.tomorrowIoPrecipationLabel.Text = "Waiting...";
			// 
			// tomorrowIoHumidityLabel
			// 
			this.tomorrowIoHumidityLabel.AutoSize = true;
			this.tomorrowIoHumidityLabel.Location = new System.Drawing.Point(127, 251);
			this.tomorrowIoHumidityLabel.Name = "tomorrowIoHumidityLabel";
			this.tomorrowIoHumidityLabel.Size = new System.Drawing.Size(69, 20);
			this.tomorrowIoHumidityLabel.TabIndex = 3;
			this.tomorrowIoHumidityLabel.Text = "Waiting...";
			// 
			// tomorrowIoCloudCoverLabel
			// 
			this.tomorrowIoCloudCoverLabel.AutoSize = true;
			this.tomorrowIoCloudCoverLabel.Location = new System.Drawing.Point(127, 197);
			this.tomorrowIoCloudCoverLabel.Name = "tomorrowIoCloudCoverLabel";
			this.tomorrowIoCloudCoverLabel.Size = new System.Drawing.Size(69, 20);
			this.tomorrowIoCloudCoverLabel.TabIndex = 2;
			this.tomorrowIoCloudCoverLabel.Text = "Waiting...";
			// 
			// tomorrowIoFahrenheitLabel
			// 
			this.tomorrowIoFahrenheitLabel.AutoSize = true;
			this.tomorrowIoFahrenheitLabel.Location = new System.Drawing.Point(127, 150);
			this.tomorrowIoFahrenheitLabel.Name = "tomorrowIoFahrenheitLabel";
			this.tomorrowIoFahrenheitLabel.Size = new System.Drawing.Size(69, 20);
			this.tomorrowIoFahrenheitLabel.TabIndex = 1;
			this.tomorrowIoFahrenheitLabel.Text = "Waiting...";
			// 
			// tomorrowIoCelsiusLabel
			// 
			this.tomorrowIoCelsiusLabel.AutoSize = true;
			this.tomorrowIoCelsiusLabel.Location = new System.Drawing.Point(127, 102);
			this.tomorrowIoCelsiusLabel.Name = "tomorrowIoCelsiusLabel";
			this.tomorrowIoCelsiusLabel.Size = new System.Drawing.Size(69, 20);
			this.tomorrowIoCelsiusLabel.TabIndex = 0;
			this.tomorrowIoCelsiusLabel.Text = "Waiting...";
			// 
			// buttonUpdate
			// 
			this.buttonUpdate.Location = new System.Drawing.Point(344, 12);
			this.buttonUpdate.Name = "buttonUpdate";
			this.buttonUpdate.Size = new System.Drawing.Size(193, 70);
			this.buttonUpdate.TabIndex = 2;
			this.buttonUpdate.Text = "Update";
			this.buttonUpdate.UseVisualStyleBackColor = true;
			this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
			// 
			// buttonExit
			// 
			this.buttonExit.Location = new System.Drawing.Point(344, 462);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(193, 70);
			this.buttonExit.TabIndex = 3;
			this.buttonExit.Text = "Exit";
			this.buttonExit.UseVisualStyleBackColor = true;
			this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
			// 
			// pictureBoxOpenWeather
			// 
			this.pictureBoxOpenWeather.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxOpenWeather.Image")));
			this.pictureBoxOpenWeather.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxOpenWeather.Name = "pictureBoxOpenWeather";
			this.pictureBoxOpenWeather.Size = new System.Drawing.Size(326, 70);
			this.pictureBoxOpenWeather.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxOpenWeather.TabIndex = 7;
			this.pictureBoxOpenWeather.TabStop = false;
			// 
			// pictureBoxTomorrowIo
			// 
			this.pictureBoxTomorrowIo.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxTomorrowIo.Image")));
			this.pictureBoxTomorrowIo.Location = new System.Drawing.Point(0, 0);
			this.pictureBoxTomorrowIo.Name = "pictureBoxTomorrowIo";
			this.pictureBoxTomorrowIo.Size = new System.Drawing.Size(326, 70);
			this.pictureBoxTomorrowIo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBoxTomorrowIo.TabIndex = 8;
			this.pictureBoxTomorrowIo.TabStop = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(881, 544);
			this.Controls.Add(this.WindSpeedLabel);
			this.Controls.Add(this.WindDirectionLabel);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonUpdate);
			this.Controls.Add(this.HumidityLabel);
			this.Controls.Add(this.PrecipationLabel);
			this.Controls.Add(this.rightPanel);
			this.Controls.Add(this.CloudCoverLabel);
			this.Controls.Add(this.leftPanel);
			this.Controls.Add(this.FahrenheitLabel);
			this.Controls.Add(this.CelsiusLabel);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.leftPanel.ResumeLayout(false);
			this.leftPanel.PerformLayout();
			this.rightPanel.ResumeLayout(false);
			this.rightPanel.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxOpenWeather)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxTomorrowIo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Panel leftPanel;
		private Panel rightPanel;
		private Button buttonUpdate;
		private Button buttonExit;
		private Label CelsiusLabel;
		private Label WindSpeedLabel;
		private Label WindDirectionLabel;
		private Label PrecipationLabel;
		private Label HumidityLabel;
		private Label CloudCoverLabel;
		private Label FahrenheitLabel;
		private Label tomorrowIoCelsiusLabel;
		private Label tomorrowIoWindSpeedLabel;
		private Label tomorrowIoWindDirectionLabel;
		private Label tomorrowIoPrecipationLabel;
		private Label tomorrowIoHumidityLabel;
		private Label tomorrowIoCloudCoverLabel;
		private Label tomorrowIoFahrenheitLabel;
		private Label openWeatherWindSpeedLabel;
		private Label openWeatherWindDirectionLabel;
		private Label openWeatherPrecipationLabel;
		private Label openWeatherHumidityLabel;
		private Label openWeatherCloudCoverLabel;
		private Label openWeatherFahrenheitLabel;
		private Label openWeatherCelsiusLabel;
		private PictureBox pictureBoxOpenWeather;
		private PictureBox pictureBoxTomorrowIo;
	}
}