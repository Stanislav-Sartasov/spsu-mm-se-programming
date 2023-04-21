namespace WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.OwmTempC = new System.Windows.Forms.Label();
            this.OpenWeatherMap = new System.Windows.Forms.GroupBox();
            this.OwmPrecipitationValue = new System.Windows.Forms.Label();
            this.OwmWindDirectionValue = new System.Windows.Forms.Label();
            this.OwmWindSpeedValue = new System.Windows.Forms.Label();
            this.OwmCloudinessValue = new System.Windows.Forms.Label();
            this.OwmHumidityValue = new System.Windows.Forms.Label();
            this.OwmTempFValue = new System.Windows.Forms.Label();
            this.OwmTempCValue = new System.Windows.Forms.Label();
            this.OwmPrecipitation = new System.Windows.Forms.Label();
            this.OwmWindSpeed = new System.Windows.Forms.Label();
            this.OwmWindDirection = new System.Windows.Forms.Label();
            this.OwmCloudiness = new System.Windows.Forms.Label();
            this.OwmHumidity = new System.Windows.Forms.Label();
            this.OwmTempF = new System.Windows.Forms.Label();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.TomorrowIo = new System.Windows.Forms.GroupBox();
            this.TioPrecipitationValue = new System.Windows.Forms.Label();
            this.TioWindDirectionValue = new System.Windows.Forms.Label();
            this.TioWindSpeedValue = new System.Windows.Forms.Label();
            this.TioCloudinessValue = new System.Windows.Forms.Label();
            this.TioHumidityValue = new System.Windows.Forms.Label();
            this.TioTempFValue = new System.Windows.Forms.Label();
            this.TioTempCValue = new System.Windows.Forms.Label();
            this.TioTempC = new System.Windows.Forms.Label();
            this.TioPrecipitation = new System.Windows.Forms.Label();
            this.TioWindSpeed = new System.Windows.Forms.Label();
            this.TioWindDirection = new System.Windows.Forms.Label();
            this.TioCloudiness = new System.Windows.Forms.Label();
            this.TioHumidity = new System.Windows.Forms.Label();
            this.TioTempF = new System.Windows.Forms.Label();
            this.Time = new System.Windows.Forms.Label();
            this.OpenWeatherMap.SuspendLayout();
            this.TomorrowIo.SuspendLayout();
            this.SuspendLayout();
            // 
            // OwmTempC
            // 
            this.OwmTempC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmTempC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmTempC.Location = new System.Drawing.Point(0, 40);
            this.OwmTempC.Name = "OwmTempC";
            this.OwmTempC.Size = new System.Drawing.Size(180, 32);
            this.OwmTempC.TabIndex = 1;
            this.OwmTempC.Text = "Temperature(°C)";
            this.OwmTempC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OpenWeatherMap
            // 
            this.OpenWeatherMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenWeatherMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.OpenWeatherMap.Controls.Add(this.OwmPrecipitationValue);
            this.OpenWeatherMap.Controls.Add(this.OwmWindDirectionValue);
            this.OpenWeatherMap.Controls.Add(this.OwmWindSpeedValue);
            this.OpenWeatherMap.Controls.Add(this.OwmCloudinessValue);
            this.OpenWeatherMap.Controls.Add(this.OwmHumidityValue);
            this.OpenWeatherMap.Controls.Add(this.OwmTempFValue);
            this.OpenWeatherMap.Controls.Add(this.OwmTempCValue);
            this.OpenWeatherMap.Controls.Add(this.OwmTempC);
            this.OpenWeatherMap.Controls.Add(this.OwmPrecipitation);
            this.OpenWeatherMap.Controls.Add(this.OwmWindSpeed);
            this.OpenWeatherMap.Controls.Add(this.OwmWindDirection);
            this.OpenWeatherMap.Controls.Add(this.OwmCloudiness);
            this.OpenWeatherMap.Controls.Add(this.OwmHumidity);
            this.OpenWeatherMap.Controls.Add(this.OwmTempF);
            this.OpenWeatherMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.OpenWeatherMap.Location = new System.Drawing.Point(430, 166);
            this.OpenWeatherMap.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OpenWeatherMap.Name = "OpenWeatherMap";
            this.OpenWeatherMap.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OpenWeatherMap.Size = new System.Drawing.Size(318, 281);
            this.OpenWeatherMap.TabIndex = 1;
            this.OpenWeatherMap.TabStop = false;
            this.OpenWeatherMap.Text = "OpenWeatherMap";
            // 
            // OwmPrecipitationValue
            // 
            this.OwmPrecipitationValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmPrecipitationValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmPrecipitationValue.Location = new System.Drawing.Point(180, 232);
            this.OwmPrecipitationValue.Name = "OwmPrecipitationValue";
            this.OwmPrecipitationValue.Size = new System.Drawing.Size(132, 32);
            this.OwmPrecipitationValue.TabIndex = 19;
            this.OwmPrecipitationValue.Text = "---";
            this.OwmPrecipitationValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmWindDirectionValue
            // 
            this.OwmWindDirectionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmWindDirectionValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmWindDirectionValue.Location = new System.Drawing.Point(180, 200);
            this.OwmWindDirectionValue.Name = "OwmWindDirectionValue";
            this.OwmWindDirectionValue.Size = new System.Drawing.Size(132, 32);
            this.OwmWindDirectionValue.TabIndex = 18;
            this.OwmWindDirectionValue.Text = "---";
            this.OwmWindDirectionValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmWindSpeedValue
            // 
            this.OwmWindSpeedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmWindSpeedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmWindSpeedValue.Location = new System.Drawing.Point(180, 168);
            this.OwmWindSpeedValue.Name = "OwmWindSpeedValue";
            this.OwmWindSpeedValue.Size = new System.Drawing.Size(132, 32);
            this.OwmWindSpeedValue.TabIndex = 17;
            this.OwmWindSpeedValue.Text = "---";
            this.OwmWindSpeedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmCloudinessValue
            // 
            this.OwmCloudinessValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmCloudinessValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmCloudinessValue.Location = new System.Drawing.Point(180, 136);
            this.OwmCloudinessValue.Name = "OwmCloudinessValue";
            this.OwmCloudinessValue.Size = new System.Drawing.Size(132, 32);
            this.OwmCloudinessValue.TabIndex = 16;
            this.OwmCloudinessValue.Text = "---";
            this.OwmCloudinessValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmHumidityValue
            // 
            this.OwmHumidityValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmHumidityValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmHumidityValue.Location = new System.Drawing.Point(180, 104);
            this.OwmHumidityValue.Name = "OwmHumidityValue";
            this.OwmHumidityValue.Size = new System.Drawing.Size(132, 32);
            this.OwmHumidityValue.TabIndex = 15;
            this.OwmHumidityValue.Text = "---";
            this.OwmHumidityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmTempFValue
            // 
            this.OwmTempFValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmTempFValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmTempFValue.Location = new System.Drawing.Point(180, 72);
            this.OwmTempFValue.Name = "OwmTempFValue";
            this.OwmTempFValue.Size = new System.Drawing.Size(132, 32);
            this.OwmTempFValue.TabIndex = 14;
            this.OwmTempFValue.Text = "---";
            this.OwmTempFValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmTempCValue
            // 
            this.OwmTempCValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmTempCValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmTempCValue.Location = new System.Drawing.Point(180, 40);
            this.OwmTempCValue.Name = "OwmTempCValue";
            this.OwmTempCValue.Size = new System.Drawing.Size(132, 32);
            this.OwmTempCValue.TabIndex = 13;
            this.OwmTempCValue.Text = "---";
            this.OwmTempCValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmPrecipitation
            // 
            this.OwmPrecipitation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmPrecipitation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmPrecipitation.Location = new System.Drawing.Point(0, 232);
            this.OwmPrecipitation.Name = "OwmPrecipitation";
            this.OwmPrecipitation.Size = new System.Drawing.Size(180, 32);
            this.OwmPrecipitation.TabIndex = 12;
            this.OwmPrecipitation.Text = "Precipitation";
            this.OwmPrecipitation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmWindSpeed
            // 
            this.OwmWindSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmWindSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmWindSpeed.Location = new System.Drawing.Point(0, 168);
            this.OwmWindSpeed.Name = "OwmWindSpeed";
            this.OwmWindSpeed.Size = new System.Drawing.Size(180, 32);
            this.OwmWindSpeed.TabIndex = 11;
            this.OwmWindSpeed.Text = "Wind speed(m/s)";
            this.OwmWindSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmWindDirection
            // 
            this.OwmWindDirection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmWindDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmWindDirection.Location = new System.Drawing.Point(0, 200);
            this.OwmWindDirection.Name = "OwmWindDirection";
            this.OwmWindDirection.Size = new System.Drawing.Size(180, 32);
            this.OwmWindDirection.TabIndex = 10;
            this.OwmWindDirection.Text = "Wind direction";
            this.OwmWindDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmCloudiness
            // 
            this.OwmCloudiness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmCloudiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmCloudiness.Location = new System.Drawing.Point(0, 136);
            this.OwmCloudiness.Name = "OwmCloudiness";
            this.OwmCloudiness.Size = new System.Drawing.Size(180, 32);
            this.OwmCloudiness.TabIndex = 9;
            this.OwmCloudiness.Text = "Cloudiness(%)";
            this.OwmCloudiness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmHumidity
            // 
            this.OwmHumidity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmHumidity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmHumidity.Location = new System.Drawing.Point(0, 104);
            this.OwmHumidity.Name = "OwmHumidity";
            this.OwmHumidity.Size = new System.Drawing.Size(180, 32);
            this.OwmHumidity.TabIndex = 8;
            this.OwmHumidity.Text = "Humidity(%)";
            this.OwmHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OwmTempF
            // 
            this.OwmTempF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OwmTempF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.OwmTempF.Location = new System.Drawing.Point(0, 72);
            this.OwmTempF.Name = "OwmTempF";
            this.OwmTempF.Size = new System.Drawing.Size(180, 32);
            this.OwmTempF.TabIndex = 7;
            this.OwmTempF.Text = "Temperature(°F)";
            this.OwmTempF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.UpdateButton.Font = new System.Drawing.Font("DejaVu Serif Condensed", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.UpdateButton.Location = new System.Drawing.Point(305, 120);
            this.UpdateButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(177, 39);
            this.UpdateButton.TabIndex = 3;
            this.UpdateButton.Text = "Get weather data";
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButtonClick);
            // 
            // TomorrowIo
            // 
            this.TomorrowIo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TomorrowIo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TomorrowIo.Controls.Add(this.TioPrecipitationValue);
            this.TomorrowIo.Controls.Add(this.TioWindDirectionValue);
            this.TomorrowIo.Controls.Add(this.TioWindSpeedValue);
            this.TomorrowIo.Controls.Add(this.TioCloudinessValue);
            this.TomorrowIo.Controls.Add(this.TioHumidityValue);
            this.TomorrowIo.Controls.Add(this.TioTempFValue);
            this.TomorrowIo.Controls.Add(this.TioTempCValue);
            this.TomorrowIo.Controls.Add(this.TioTempC);
            this.TomorrowIo.Controls.Add(this.TioPrecipitation);
            this.TomorrowIo.Controls.Add(this.TioWindSpeed);
            this.TomorrowIo.Controls.Add(this.TioWindDirection);
            this.TomorrowIo.Controls.Add(this.TioCloudiness);
            this.TomorrowIo.Controls.Add(this.TioHumidity);
            this.TomorrowIo.Controls.Add(this.TioTempF);
            this.TomorrowIo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point);
            this.TomorrowIo.Location = new System.Drawing.Point(53, 166);
            this.TomorrowIo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TomorrowIo.Name = "TomorrowIo";
            this.TomorrowIo.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TomorrowIo.Size = new System.Drawing.Size(318, 281);
            this.TomorrowIo.TabIndex = 4;
            this.TomorrowIo.TabStop = false;
            this.TomorrowIo.Text = "TomorrowIo";
            // 
            // TioPrecipitationValue
            // 
            this.TioPrecipitationValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioPrecipitationValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioPrecipitationValue.Location = new System.Drawing.Point(180, 232);
            this.TioPrecipitationValue.Name = "TioPrecipitationValue";
            this.TioPrecipitationValue.Size = new System.Drawing.Size(132, 32);
            this.TioPrecipitationValue.TabIndex = 19;
            this.TioPrecipitationValue.Text = "---";
            this.TioPrecipitationValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioWindDirectionValue
            // 
            this.TioWindDirectionValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioWindDirectionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioWindDirectionValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioWindDirectionValue.Location = new System.Drawing.Point(180, 200);
            this.TioWindDirectionValue.Name = "TioWindDirectionValue";
            this.TioWindDirectionValue.Size = new System.Drawing.Size(132, 32);
            this.TioWindDirectionValue.TabIndex = 18;
            this.TioWindDirectionValue.Text = "---";
            this.TioWindDirectionValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioWindSpeedValue
            // 
            this.TioWindSpeedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioWindSpeedValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioWindSpeedValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioWindSpeedValue.Location = new System.Drawing.Point(180, 168);
            this.TioWindSpeedValue.Name = "TioWindSpeedValue";
            this.TioWindSpeedValue.Size = new System.Drawing.Size(132, 32);
            this.TioWindSpeedValue.TabIndex = 17;
            this.TioWindSpeedValue.Text = "---";
            this.TioWindSpeedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioCloudinessValue
            // 
            this.TioCloudinessValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioCloudinessValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioCloudinessValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioCloudinessValue.Location = new System.Drawing.Point(180, 136);
            this.TioCloudinessValue.Name = "TioCloudinessValue";
            this.TioCloudinessValue.Size = new System.Drawing.Size(132, 32);
            this.TioCloudinessValue.TabIndex = 16;
            this.TioCloudinessValue.Text = "---";
            this.TioCloudinessValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioHumidityValue
            // 
            this.TioHumidityValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioHumidityValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioHumidityValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioHumidityValue.Location = new System.Drawing.Point(180, 104);
            this.TioHumidityValue.Name = "TioHumidityValue";
            this.TioHumidityValue.Size = new System.Drawing.Size(132, 32);
            this.TioHumidityValue.TabIndex = 15;
            this.TioHumidityValue.Text = "---";
            this.TioHumidityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioTempFValue
            // 
            this.TioTempFValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioTempFValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioTempFValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioTempFValue.Location = new System.Drawing.Point(180, 72);
            this.TioTempFValue.Name = "TioTempFValue";
            this.TioTempFValue.Size = new System.Drawing.Size(132, 32);
            this.TioTempFValue.TabIndex = 14;
            this.TioTempFValue.Text = "---";
            this.TioTempFValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioTempCValue
            // 
            this.TioTempCValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioTempCValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioTempCValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioTempCValue.Location = new System.Drawing.Point(180, 40);
            this.TioTempCValue.Name = "TioTempCValue";
            this.TioTempCValue.Size = new System.Drawing.Size(132, 32);
            this.TioTempCValue.TabIndex = 13;
            this.TioTempCValue.Text = "---";
            this.TioTempCValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioTempC
            // 
            this.TioTempC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.TioTempC.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioTempC.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioTempC.Location = new System.Drawing.Point(0, 40);
            this.TioTempC.Name = "TioTempC";
            this.TioTempC.Size = new System.Drawing.Size(180, 32);
            this.TioTempC.TabIndex = 1;
            this.TioTempC.Text = "Temperature(°C)";
            this.TioTempC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioPrecipitation
            // 
            this.TioPrecipitation.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioPrecipitation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioPrecipitation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioPrecipitation.Location = new System.Drawing.Point(0, 232);
            this.TioPrecipitation.Name = "TioPrecipitation";
            this.TioPrecipitation.Size = new System.Drawing.Size(180, 32);
            this.TioPrecipitation.TabIndex = 12;
            this.TioPrecipitation.Text = "Precipitation";
            this.TioPrecipitation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioWindSpeed
            // 
            this.TioWindSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioWindSpeed.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioWindSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioWindSpeed.Location = new System.Drawing.Point(0, 168);
            this.TioWindSpeed.Name = "TioWindSpeed";
            this.TioWindSpeed.Size = new System.Drawing.Size(180, 32);
            this.TioWindSpeed.TabIndex = 11;
            this.TioWindSpeed.Text = "Wind speed(m/s)";
            this.TioWindSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioWindDirection
            // 
            this.TioWindDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioWindDirection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioWindDirection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioWindDirection.Location = new System.Drawing.Point(0, 200);
            this.TioWindDirection.Name = "TioWindDirection";
            this.TioWindDirection.Size = new System.Drawing.Size(180, 32);
            this.TioWindDirection.TabIndex = 10;
            this.TioWindDirection.Text = "Wind direction";
            this.TioWindDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioCloudiness
            // 
            this.TioCloudiness.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioCloudiness.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioCloudiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioCloudiness.Location = new System.Drawing.Point(0, 136);
            this.TioCloudiness.Name = "TioCloudiness";
            this.TioCloudiness.Size = new System.Drawing.Size(180, 32);
            this.TioCloudiness.TabIndex = 9;
            this.TioCloudiness.Text = "Cloudiness(%)";
            this.TioCloudiness.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioHumidity
            // 
            this.TioHumidity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioHumidity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioHumidity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioHumidity.Location = new System.Drawing.Point(0, 104);
            this.TioHumidity.Name = "TioHumidity";
            this.TioHumidity.Size = new System.Drawing.Size(180, 32);
            this.TioHumidity.TabIndex = 8;
            this.TioHumidity.Text = "Humidity(%)";
            this.TioHumidity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TioTempF
            // 
            this.TioTempF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TioTempF.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TioTempF.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.TioTempF.Location = new System.Drawing.Point(0, 72);
            this.TioTempF.Name = "TioTempF";
            this.TioTempF.Size = new System.Drawing.Size(180, 32);
            this.TioTempF.TabIndex = 7;
            this.TioTempF.Text = "Temperature(°F)";
            this.TioTempF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Time
            // 
            this.Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Time.Location = new System.Drawing.Point(291, 89);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(211, 29);
            this.Time.TabIndex = 5;
            this.Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(796, 554);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.TomorrowIo);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.OpenWeatherMap);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(814, 601);
            this.MinimumSize = new System.Drawing.Size(814, 601);
            this.Name = "MainForm";
            this.Text = "Weather";
            this.OpenWeatherMap.ResumeLayout(false);
            this.TomorrowIo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label OwmTempC;
        private System.Windows.Forms.GroupBox OpenWeatherMap;
        private System.Windows.Forms.Label OwmPrecipitation;
        private System.Windows.Forms.Label OwmWindSpeed;
        private System.Windows.Forms.Label OwmWindDirection;
        private System.Windows.Forms.Label OwmCloudiness;
        private System.Windows.Forms.Label OwmHumidity;
        private System.Windows.Forms.Label OwmTempF;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Label OwmPrecipitationValue;
        private System.Windows.Forms.Label OwmWindDirectionValue;
        private System.Windows.Forms.Label OwmWindSpeedValue;
        private System.Windows.Forms.Label OwmCloudinessValue;
        private System.Windows.Forms.Label OwmHumidityValue;
        private System.Windows.Forms.Label OwmTempFValue;
        private System.Windows.Forms.Label OwmTempCValue;
        private System.Windows.Forms.GroupBox TomorrowIo;
        private System.Windows.Forms.Label TioPrecipitationValue;
        private System.Windows.Forms.Label TioWindDirectionValue;
        private System.Windows.Forms.Label TioWindSpeedValue;
        private System.Windows.Forms.Label TioCloudinessValue;
        private System.Windows.Forms.Label TioHumidityValue;
        private System.Windows.Forms.Label TioTempFValue;
        private System.Windows.Forms.Label TioTempCValue;
        private System.Windows.Forms.Label TioTempC;
        private System.Windows.Forms.Label TioPrecipitation;
        private System.Windows.Forms.Label TioWindSpeed;
        private System.Windows.Forms.Label TioWindDirection;
        private System.Windows.Forms.Label TioCloudiness;
        private System.Windows.Forms.Label TioHumidity;
        private System.Windows.Forms.Label TioTempF;
        private System.Windows.Forms.Label Time;
    }
}