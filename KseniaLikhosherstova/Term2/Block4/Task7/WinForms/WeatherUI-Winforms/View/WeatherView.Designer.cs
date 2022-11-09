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
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // isOpenWeather
            // 
            this.isOpenWeather.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.isOpenWeather.AutoSize = true;
            this.isOpenWeather.Location = new System.Drawing.Point(39, 52);
            this.isOpenWeather.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.isOpenWeather.Name = "isOpenWeather";
            this.isOpenWeather.Size = new System.Drawing.Size(152, 24);
            this.isOpenWeather.TabIndex = 0;
            this.isOpenWeather.Text = "OpenWeatherMap";
            this.isOpenWeather.UseVisualStyleBackColor = true;
            this.isOpenWeather.CheckedChanged += new System.EventHandler(this.isOpenWeather_CheckedChanged);
            // 
            // isTomorrowIO
            // 
            this.isTomorrowIO.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.isTomorrowIO.AutoSize = true;
            this.isTomorrowIO.Location = new System.Drawing.Point(39, 88);
            this.isTomorrowIO.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.isTomorrowIO.Name = "isTomorrowIO";
            this.isTomorrowIO.Size = new System.Drawing.Size(114, 24);
            this.isTomorrowIO.TabIndex = 1;
            this.isTomorrowIO.Text = "TomorrowIO";
            this.isTomorrowIO.UseVisualStyleBackColor = true;
            this.isTomorrowIO.CheckedChanged += new System.EventHandler(this.isTomorrowIO_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Location = new System.Drawing.Point(347, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Temperature Celcius:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 88);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Temperature Farenheit:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(347, 124);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Humidity:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(347, 160);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Clouds Percent:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(347, 196);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Precipitation:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(347, 232);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 7;
            this.label6.Text = "Wind Speed:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(347, 268);
            this.label7.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Wind Direction:";
            // 
            // TempC
            // 
            this.TempC.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TempC.AutoSize = true;
            this.TempC.Location = new System.Drawing.Point(507, 52);
            this.TempC.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.TempC.Name = "TempC";
            this.TempC.Size = new System.Drawing.Size(42, 20);
            this.TempC.TabIndex = 9;
            this.TempC.Text = "INFO";
            // 
            // TempF
            // 
            this.TempF.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.TempF.AutoSize = true;
            this.TempF.Location = new System.Drawing.Point(507, 88);
            this.TempF.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.TempF.Name = "TempF";
            this.TempF.Size = new System.Drawing.Size(42, 20);
            this.TempF.TabIndex = 10;
            this.TempF.Text = "INFO";
            // 
            // CloudsPercent
            // 
            this.CloudsPercent.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.CloudsPercent.AutoSize = true;
            this.CloudsPercent.Location = new System.Drawing.Point(507, 160);
            this.CloudsPercent.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.CloudsPercent.Name = "CloudsPercent";
            this.CloudsPercent.Size = new System.Drawing.Size(42, 20);
            this.CloudsPercent.TabIndex = 11;
            this.CloudsPercent.Text = "INFO";
            // 
            // Humidity
            // 
            this.Humidity.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Humidity.AutoSize = true;
            this.Humidity.Location = new System.Drawing.Point(507, 124);
            this.Humidity.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Humidity.Name = "Humidity";
            this.Humidity.Size = new System.Drawing.Size(42, 20);
            this.Humidity.TabIndex = 12;
            this.Humidity.Text = "INFO";
            // 
            // Precipitation
            // 
            this.Precipitation.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Precipitation.AutoSize = true;
            this.Precipitation.Location = new System.Drawing.Point(507, 196);
            this.Precipitation.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.Precipitation.Name = "Precipitation";
            this.Precipitation.Size = new System.Drawing.Size(42, 20);
            this.Precipitation.TabIndex = 13;
            this.Precipitation.Text = "INFO";
            // 
            // WindSpeed
            // 
            this.WindSpeed.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.WindSpeed.AutoSize = true;
            this.WindSpeed.Location = new System.Drawing.Point(507, 232);
            this.WindSpeed.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.WindSpeed.Name = "WindSpeed";
            this.WindSpeed.Size = new System.Drawing.Size(42, 20);
            this.WindSpeed.TabIndex = 14;
            this.WindSpeed.Text = "INFO";
            // 
            // WindDirection
            // 
            this.WindDirection.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.WindDirection.AutoSize = true;
            this.WindDirection.Location = new System.Drawing.Point(507, 268);
            this.WindDirection.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.WindDirection.Name = "WindDirection";
            this.WindDirection.Size = new System.Drawing.Size(42, 20);
            this.WindDirection.TabIndex = 15;
            this.WindDirection.Text = "INFO";
            // 
            // UpdateButton
            // 
            this.UpdateButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.UpdateButton.Location = new System.Drawing.Point(268, 300);
            this.UpdateButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(86, 31);
            this.UpdateButton.TabIndex = 1;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label8.Location = new System.Drawing.Point(19, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(329, 38);
            this.label8.TabIndex = 16;
            this.label8.Text = "Select one or more weather services:";
            // 
            // WeatherView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 343);
            this.Controls.Add(this.label8);
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
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.MinimumSize = new System.Drawing.Size(620, 387);
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
        private System.Windows.Forms.Label label8;
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