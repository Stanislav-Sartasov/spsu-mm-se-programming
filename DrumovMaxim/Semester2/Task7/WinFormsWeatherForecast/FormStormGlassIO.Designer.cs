namespace WinFormsWeatherForecast
{
    partial class FormStormGlassIO
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormStormGlassIO));
            this.ButtonBack = new System.Windows.Forms.Button();
            this.TemperatureCelsius = new System.Windows.Forms.Label();
            this.CloudCover = new System.Windows.Forms.Label();
            this.Humidity = new System.Windows.Forms.Label();
            this.WindDirection = new System.Windows.Forms.Label();
            this.WeatherLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.TemperatureFahrenheit = new System.Windows.Forms.Label();
            this.Precipitaion = new System.Windows.Forms.Label();
            this.WindSpeed = new System.Windows.Forms.Label();
            this.picTemp = new System.Windows.Forms.PictureBox();
            this.picCloud = new System.Windows.Forms.PictureBox();
            this.picHum = new System.Windows.Forms.PictureBox();
            this.picPrecip = new System.Windows.Forms.PictureBox();
            this.picDir = new System.Windows.Forms.PictureBox();
            this.picSpeed = new System.Windows.Forms.PictureBox();
            this.ExceptionMessage = new System.Windows.Forms.Label();
            this.GetWeather = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.MinimizeButton = new System.Windows.Forms.Button();
            this.WeatherLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCloud)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrecip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpeed)).BeginInit();
            this.SuspendLayout();
            // 
            // ButtonBack
            // 
            this.ButtonBack.BackColor = System.Drawing.Color.NavajoWhite;
            this.ButtonBack.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.ButtonBack.Location = new System.Drawing.Point(423, 293);
            this.ButtonBack.Name = "ButtonBack";
            this.ButtonBack.Size = new System.Drawing.Size(99, 56);
            this.ButtonBack.TabIndex = 1;
            this.ButtonBack.Text = "ButtonBack";
            this.ButtonBack.UseVisualStyleBackColor = false;
            this.ButtonBack.Click += new System.EventHandler(this.ButtonBack_Click);
            // 
            // TemperatureCelsius
            // 
            this.TemperatureCelsius.AutoSize = true;
            this.TemperatureCelsius.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.TemperatureCelsius.ForeColor = System.Drawing.Color.Black;
            this.TemperatureCelsius.Location = new System.Drawing.Point(87, 0);
            this.TemperatureCelsius.Name = "TemperatureCelsius";
            this.TemperatureCelsius.Size = new System.Drawing.Size(74, 30);
            this.TemperatureCelsius.TabIndex = 0;
            this.TemperatureCelsius.Text = "Temperature in Celsius";
            // 
            // CloudCover
            // 
            this.CloudCover.AutoSize = true;
            this.CloudCover.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.CloudCover.Location = new System.Drawing.Point(252, 0);
            this.CloudCover.Name = "CloudCover";
            this.CloudCover.Size = new System.Drawing.Size(41, 30);
            this.CloudCover.TabIndex = 1;
            this.CloudCover.Text = "Cloud Cover";
            // 
            // Humidity
            // 
            this.Humidity.AutoSize = true;
            this.Humidity.Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Humidity.Location = new System.Drawing.Point(410, 0);
            this.Humidity.Name = "Humidity";
            this.Humidity.Size = new System.Drawing.Size(57, 15);
            this.Humidity.TabIndex = 2;
            this.Humidity.Text = "Humidity";
            // 
            // WindDirection
            // 
            this.WindDirection.AutoSize = true;
            this.WindDirection.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WindDirection.Location = new System.Drawing.Point(252, 76);
            this.WindDirection.Name = "WindDirection";
            this.WindDirection.Size = new System.Drawing.Size(56, 30);
            this.WindDirection.TabIndex = 4;
            this.WindDirection.Text = "Wind Direction";
            // 
            // WeatherLayoutPanel
            // 
            this.WeatherLayoutPanel.ColumnCount = 6;
            this.WeatherLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.85214F));
            this.WeatherLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.29572F));
            this.WeatherLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.85214F));
            this.WeatherLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 68F));
            this.WeatherLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.WeatherLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 65F));
            this.WeatherLayoutPanel.Controls.Add(this.TemperatureFahrenheit, 1, 1);
            this.WeatherLayoutPanel.Controls.Add(this.Humidity, 5, 0);
            this.WeatherLayoutPanel.Controls.Add(this.TemperatureCelsius, 1, 0);
            this.WeatherLayoutPanel.Controls.Add(this.Precipitaion, 1, 2);
            this.WeatherLayoutPanel.Controls.Add(this.WindDirection, 3, 2);
            this.WeatherLayoutPanel.Controls.Add(this.WindSpeed, 5, 2);
            this.WeatherLayoutPanel.Controls.Add(this.CloudCover, 3, 0);
            this.WeatherLayoutPanel.Controls.Add(this.picTemp, 0, 0);
            this.WeatherLayoutPanel.Controls.Add(this.picCloud, 2, 0);
            this.WeatherLayoutPanel.Controls.Add(this.picHum, 4, 0);
            this.WeatherLayoutPanel.Controls.Add(this.picPrecip, 0, 2);
            this.WeatherLayoutPanel.Controls.Add(this.picDir, 2, 2);
            this.WeatherLayoutPanel.Controls.Add(this.picSpeed, 4, 2);
            this.WeatherLayoutPanel.Location = new System.Drawing.Point(37, 124);
            this.WeatherLayoutPanel.Name = "WeatherLayoutPanel";
            this.WeatherLayoutPanel.RowCount = 3;
            this.WeatherLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.78947F));
            this.WeatherLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.21053F));
            this.WeatherLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.WeatherLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.WeatherLayoutPanel.Size = new System.Drawing.Size(474, 163);
            this.WeatherLayoutPanel.TabIndex = 5;
            // 
            // TemperatureFahrenheit
            // 
            this.TemperatureFahrenheit.AutoSize = true;
            this.TemperatureFahrenheit.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TemperatureFahrenheit.Location = new System.Drawing.Point(87, 31);
            this.TemperatureFahrenheit.Name = "TemperatureFahrenheit";
            this.TemperatureFahrenheit.Size = new System.Drawing.Size(75, 30);
            this.TemperatureFahrenheit.TabIndex = 8;
            this.TemperatureFahrenheit.Text = "Temperature in Fahrenheit";
            // 
            // Precipitaion
            // 
            this.Precipitaion.AutoSize = true;
            this.Precipitaion.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Precipitaion.Location = new System.Drawing.Point(87, 76);
            this.Precipitaion.Name = "Precipitaion";
            this.Precipitaion.Size = new System.Drawing.Size(70, 15);
            this.Precipitaion.TabIndex = 5;
            this.Precipitaion.Text = "Precipitaion";
            // 
            // WindSpeed
            // 
            this.WindSpeed.AutoSize = true;
            this.WindSpeed.Font = new System.Drawing.Font("Yu Gothic UI Semibold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.WindSpeed.Location = new System.Drawing.Point(410, 76);
            this.WindSpeed.Name = "WindSpeed";
            this.WindSpeed.Size = new System.Drawing.Size(40, 30);
            this.WindSpeed.TabIndex = 6;
            this.WindSpeed.Text = "Wind Speed";
            // 
            // picTemp
            // 
            this.picTemp.Image = ((System.Drawing.Image)(resources.GetObject("picTemp.Image")));
            this.picTemp.Location = new System.Drawing.Point(3, 3);
            this.picTemp.Name = "picTemp";
            this.WeatherLayoutPanel.SetRowSpan(this.picTemp, 2);
            this.picTemp.Size = new System.Drawing.Size(78, 70);
            this.picTemp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picTemp.TabIndex = 9;
            this.picTemp.TabStop = false;
            // 
            // picCloud
            // 
            this.picCloud.Image = ((System.Drawing.Image)(resources.GetObject("picCloud.Image")));
            this.picCloud.Location = new System.Drawing.Point(168, 3);
            this.picCloud.Name = "picCloud";
            this.WeatherLayoutPanel.SetRowSpan(this.picCloud, 2);
            this.picCloud.Size = new System.Drawing.Size(78, 70);
            this.picCloud.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCloud.TabIndex = 10;
            this.picCloud.TabStop = false;
            // 
            // picHum
            // 
            this.picHum.Image = ((System.Drawing.Image)(resources.GetObject("picHum.Image")));
            this.picHum.Location = new System.Drawing.Point(320, 3);
            this.picHum.Name = "picHum";
            this.WeatherLayoutPanel.SetRowSpan(this.picHum, 2);
            this.picHum.Size = new System.Drawing.Size(84, 70);
            this.picHum.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHum.TabIndex = 11;
            this.picHum.TabStop = false;
            // 
            // picPrecip
            // 
            this.picPrecip.Image = ((System.Drawing.Image)(resources.GetObject("picPrecip.Image")));
            this.picPrecip.Location = new System.Drawing.Point(3, 79);
            this.picPrecip.Name = "picPrecip";
            this.picPrecip.Size = new System.Drawing.Size(78, 71);
            this.picPrecip.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPrecip.TabIndex = 12;
            this.picPrecip.TabStop = false;
            // 
            // picDir
            // 
            this.picDir.Image = ((System.Drawing.Image)(resources.GetObject("picDir.Image")));
            this.picDir.Location = new System.Drawing.Point(168, 79);
            this.picDir.Name = "picDir";
            this.picDir.Size = new System.Drawing.Size(78, 71);
            this.picDir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDir.TabIndex = 13;
            this.picDir.TabStop = false;
            // 
            // picSpeed
            // 
            this.picSpeed.Image = ((System.Drawing.Image)(resources.GetObject("picSpeed.Image")));
            this.picSpeed.Location = new System.Drawing.Point(320, 79);
            this.picSpeed.Name = "picSpeed";
            this.picSpeed.Size = new System.Drawing.Size(84, 71);
            this.picSpeed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSpeed.TabIndex = 14;
            this.picSpeed.TabStop = false;
            // 
            // ExceptionMessage
            // 
            this.ExceptionMessage.AutoSize = true;
            this.ExceptionMessage.ForeColor = System.Drawing.Color.OrangeRed;
            this.ExceptionMessage.Location = new System.Drawing.Point(37, 314);
            this.ExceptionMessage.Name = "ExceptionMessage";
            this.ExceptionMessage.Size = new System.Drawing.Size(0, 15);
            this.ExceptionMessage.TabIndex = 6;
            // 
            // GetWeather
            // 
            this.GetWeather.BackColor = System.Drawing.Color.NavajoWhite;
            this.GetWeather.FlatAppearance.BorderSize = 0;
            this.GetWeather.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GetWeather.Font = new System.Drawing.Font("Showcard Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.GetWeather.Location = new System.Drawing.Point(0, 28);
            this.GetWeather.Name = "GetWeather";
            this.GetWeather.Size = new System.Drawing.Size(535, 90);
            this.GetWeather.TabIndex = 7;
            this.GetWeather.Text = "Click to get weather forecast";
            this.GetWeather.UseVisualStyleBackColor = false;
            this.GetWeather.Click += new System.EventHandler(this.GetWeather_Click);
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
            // MinimizeButton
            // 
            this.MinimizeButton.BackColor = System.Drawing.Color.WhiteSmoke;
            this.MinimizeButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MinimizeButton.BackgroundImage")));
            this.MinimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.MinimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MinimizeButton.Location = new System.Drawing.Point(466, 12);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(25, 25);
            this.MinimizeButton.TabIndex = 11;
            this.MinimizeButton.UseVisualStyleBackColor = false;
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // FormStormGlassIO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.NavajoWhite;
            this.ClientSize = new System.Drawing.Size(534, 361);
            this.Controls.Add(this.MinimizeButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.GetWeather);
            this.Controls.Add(this.ButtonBack);
            this.Controls.Add(this.ExceptionMessage);
            this.Controls.Add(this.WeatherLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormStormGlassIO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "FormStormGlassIO";
            this.WeatherLayoutPanel.ResumeLayout(false);
            this.WeatherLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCloud)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPrecip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSpeed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ButtonBack;
        private Label TemperatureCelsius;
        private Label CloudCover;
        private Label Humidity;
        private Label WindDirection;
        private TableLayoutPanel WeatherLayoutPanel;
        private Label WindSpeed;
        private Label Precipitaion;
        private Label ExceptionMessage;
        private Button GetWeather;
        private Label TemperatureFahrenheit;
        private PictureBox picTemp;
        private PictureBox picCloud;
        private PictureBox picHum;
        private PictureBox picPrecip;
        private PictureBox picDir;
        private PictureBox picSpeed;
        private Button CloseButton;
        private Button MinimizeButton;
    }
}