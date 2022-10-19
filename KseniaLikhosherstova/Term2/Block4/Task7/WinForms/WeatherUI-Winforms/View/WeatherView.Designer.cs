namespace WeatherUI_Winforms.View
{
    partial class WeatherView
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
            this.isOpenWeather = new System.Windows.Forms.CheckBox();
            this.isTomorrowIO = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TempC = new System.Windows.Forms.Label();
            this.TempF = new System.Windows.Forms.Label();
            this.CloudsPercent = new System.Windows.Forms.Label();
            this.Humidity = new System.Windows.Forms.Label();
            this.Precipitation = new System.Windows.Forms.Label();
            this.WindSpeed = new System.Windows.Forms.Label();
            this.WindDirection = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // isOpenWeather
            // 
            this.isOpenWeather.AutoSize = true;
            this.isOpenWeather.Location = new System.Drawing.Point(15, 15);
            this.isOpenWeather.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.isOpenWeather.Name = "isOpenWeather";
            this.isOpenWeather.Size = new System.Drawing.Size(123, 19);
            this.isOpenWeather.TabIndex = 0;
            this.isOpenWeather.Text = "OpenWeatherMap";
            this.isOpenWeather.UseVisualStyleBackColor = true;
            this.isOpenWeather.CheckedChanged += new System.EventHandler(this.isOpenWeather_CheckedChanged);
            // 
            // isTomorrowIO
            // 
            this.isTomorrowIO.AutoSize = true;
            this.isTomorrowIO.Location = new System.Drawing.Point(15, 42);
            this.isTomorrowIO.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.isTomorrowIO.Name = "isTomorrowIO";
            this.isTomorrowIO.Size = new System.Drawing.Size(92, 19);
            this.isTomorrowIO.TabIndex = 1;
            this.isTomorrowIO.Text = "TomorrowIO";
            this.isTomorrowIO.UseVisualStyleBackColor = true;
            this.isTomorrowIO.CheckedChanged += new System.EventHandler(this.isTomorrowIO_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(288, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Temperature Celcius:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(288, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Temperature Farenheit:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "Humidity:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(288, 95);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 15);
            this.label4.TabIndex = 5;
            this.label4.Text = "Clouds Percent:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Precipitation:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(288, 148);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Wind Speed:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(288, 174);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "Wind Direction:";
            // 
            // TempC
            // 
            this.TempC.AutoSize = true;
            this.TempC.Location = new System.Drawing.Point(428, 15);
            this.TempC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TempC.Name = "TempC";
            this.TempC.Size = new System.Drawing.Size(34, 15);
            this.TempC.TabIndex = 9;
            this.TempC.Text = "INFO";
            // 
            // TempF
            // 
            this.TempF.AutoSize = true;
            this.TempF.Location = new System.Drawing.Point(428, 42);
            this.TempF.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.TempF.Name = "TempF";
            this.TempF.Size = new System.Drawing.Size(34, 15);
            this.TempF.TabIndex = 10;
            this.TempF.Text = "INFO";
            // 
            // CloudsPercent
            // 
            this.CloudsPercent.AutoSize = true;
            this.CloudsPercent.Location = new System.Drawing.Point(428, 95);
            this.CloudsPercent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CloudsPercent.Name = "CloudsPercent";
            this.CloudsPercent.Size = new System.Drawing.Size(34, 15);
            this.CloudsPercent.TabIndex = 11;
            this.CloudsPercent.Text = "INFO";
            // 
            // Humidity
            // 
            this.Humidity.AutoSize = true;
            this.Humidity.Location = new System.Drawing.Point(428, 68);
            this.Humidity.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Humidity.Name = "Humidity";
            this.Humidity.Size = new System.Drawing.Size(34, 15);
            this.Humidity.TabIndex = 12;
            this.Humidity.Text = "INFO";
            // 
            // Precipitation
            // 
            this.Precipitation.AutoSize = true;
            this.Precipitation.Location = new System.Drawing.Point(428, 121);
            this.Precipitation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Precipitation.Name = "Precipitation";
            this.Precipitation.Size = new System.Drawing.Size(34, 15);
            this.Precipitation.TabIndex = 13;
            this.Precipitation.Text = "INFO";
            // 
            // WindSpeed
            // 
            this.WindSpeed.AutoSize = true;
            this.WindSpeed.Location = new System.Drawing.Point(428, 148);
            this.WindSpeed.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WindSpeed.Name = "WindSpeed";
            this.WindSpeed.Size = new System.Drawing.Size(34, 15);
            this.WindSpeed.TabIndex = 14;
            this.WindSpeed.Text = "INFO";
            // 
            // WindDirection
            // 
            this.WindDirection.AutoSize = true;
            this.WindDirection.Location = new System.Drawing.Point(428, 174);
            this.WindDirection.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WindDirection.Name = "WindDirection";
            this.WindDirection.Size = new System.Drawing.Size(34, 15);
            this.WindDirection.TabIndex = 15;
            this.WindDirection.Text = "INFO";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Location = new System.Drawing.Point(217, 224);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(75, 23);
            this.UpdateButton.TabIndex = 1;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // WeatherView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 259);
            this.Controls.Add(this.WindDirection);
            this.Controls.Add(this.WindSpeed);
            this.Controls.Add(this.Precipitation);
            this.Controls.Add(this.Humidity);
            this.Controls.Add(this.CloudsPercent);
            this.Controls.Add(this.TempF);
            this.Controls.Add(this.TempC);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.isTomorrowIO);
            this.Controls.Add(this.isOpenWeather);
            this.Controls.Add(this.UpdateButton);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "WeatherView";
            this.Text = "WeatherView";
            this.Load += new System.EventHandler(this.WeatherView_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.CheckBox isOpenWeather;
        private System.Windows.Forms.CheckBox isTomorrowIO;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label TempC;
        private System.Windows.Forms.Label TempF;
        private System.Windows.Forms.Label CloudsPercent;
        private System.Windows.Forms.Label Humidity;
        private System.Windows.Forms.Label Precipitation;
        private System.Windows.Forms.Label WindSpeed;
        private System.Windows.Forms.Label WindDirection;
        private System.Windows.Forms.Button UpdateButton;
    }
}