namespace WinFormsUI
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.headingSourceName = new System.Windows.Forms.Label();
            this.sourceNameTomorrowIO = new System.Windows.Forms.Label();
            this.bindingSourceTomorrowIO = new System.Windows.Forms.BindingSource(this.components);
            this.sourceNameStormGlass = new System.Windows.Forms.Label();
            this.bindingSourceStormGlass = new System.Windows.Forms.BindingSource(this.components);
            this.headingTemperatureCelsius = new System.Windows.Forms.Label();
            this.temperatureCelsiusTomorrowIO = new System.Windows.Forms.Label();
            this.temperatureCelsiusStormGlass = new System.Windows.Forms.Label();
            this.headingTemperatureFahrenheit = new System.Windows.Forms.Label();
            this.temperatureFahrenheitTomorrowIO = new System.Windows.Forms.Label();
            this.temperatureFahrenheitStormGlass = new System.Windows.Forms.Label();
            this.headingCloudCoverage = new System.Windows.Forms.Label();
            this.cloudCoverageTomorrowIO = new System.Windows.Forms.Label();
            this.cloudCoverageStormGlass = new System.Windows.Forms.Label();
            this.headingHumidity = new System.Windows.Forms.Label();
            this.humidityTomorrowIO = new System.Windows.Forms.Label();
            this.humidityStormGlass = new System.Windows.Forms.Label();
            this.headingPrecipitation = new System.Windows.Forms.Label();
            this.precipitationTomorrowIO = new System.Windows.Forms.Label();
            this.precipitationStormGlass = new System.Windows.Forms.Label();
            this.headingWindSpeed = new System.Windows.Forms.Label();
            this.windSpeedTomorrowIO = new System.Windows.Forms.Label();
            this.windSpeedStormGlass = new System.Windows.Forms.Label();
            this.headingWindDirection = new System.Windows.Forms.Label();
            this.windDirectionTomorrowIO = new System.Windows.Forms.Label();
            this.windDirectionStormGlass = new System.Windows.Forms.Label();
            this.updateWeatherButton = new System.Windows.Forms.Button();
            this.time = new System.Windows.Forms.Label();
            this.bindingSourceDate = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTomorrowIO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceStormGlass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDate)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.headingSourceName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.sourceNameTomorrowIO, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.sourceNameStormGlass, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.headingTemperatureCelsius, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.temperatureCelsiusTomorrowIO, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.temperatureCelsiusStormGlass, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.headingTemperatureFahrenheit, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.temperatureFahrenheitTomorrowIO, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.temperatureFahrenheitStormGlass, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.headingCloudCoverage, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.cloudCoverageTomorrowIO, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.cloudCoverageStormGlass, 2, 3);
            this.tableLayoutPanel1.Controls.Add(this.headingHumidity, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.humidityTomorrowIO, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.humidityStormGlass, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.headingPrecipitation, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.precipitationTomorrowIO, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.precipitationStormGlass, 2, 5);
            this.tableLayoutPanel1.Controls.Add(this.headingWindSpeed, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.windSpeedTomorrowIO, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.windSpeedStormGlass, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.headingWindDirection, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.windDirectionTomorrowIO, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.windDirectionStormGlass, 2, 7);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 84);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(664, 431);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // headingSourceName
            // 
            this.headingSourceName.AutoSize = true;
            this.headingSourceName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headingSourceName.Location = new System.Drawing.Point(3, 0);
            this.headingSourceName.Name = "headingSourceName";
            this.headingSourceName.Size = new System.Drawing.Size(138, 28);
            this.headingSourceName.TabIndex = 0;
            this.headingSourceName.Text = "Source Name";
            // 
            // sourceNameTomorrowIO
            // 
            this.sourceNameTomorrowIO.AutoSize = true;
            this.sourceNameTomorrowIO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTomorrowIO, "SourceName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.sourceNameTomorrowIO.Location = new System.Drawing.Point(335, 0);
            this.sourceNameTomorrowIO.Name = "sourceNameTomorrowIO";
            this.sourceNameTomorrowIO.Size = new System.Drawing.Size(99, 28);
            this.sourceNameTomorrowIO.TabIndex = 1;
            this.sourceNameTomorrowIO.Text = "Loading....";
            // 
            // bindingSourceTomorrowIO
            // 
            this.bindingSourceTomorrowIO.DataSource = typeof(WeatherLibrary.Weather);
            // 
            // sourceNameStormGlass
            // 
            this.sourceNameStormGlass.AutoSize = true;
            this.sourceNameStormGlass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceStormGlass, "SourceName", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.sourceNameStormGlass.Location = new System.Drawing.Point(501, 0);
            this.sourceNameStormGlass.Name = "sourceNameStormGlass";
            this.sourceNameStormGlass.Size = new System.Drawing.Size(99, 28);
            this.sourceNameStormGlass.TabIndex = 2;
            this.sourceNameStormGlass.Text = "Loading....";
            // 
            // bindingSourceStormGlass
            // 
            this.bindingSourceStormGlass.DataSource = typeof(WeatherLibrary.Weather);
            // 
            // headingTemperatureCelsius
            // 
            this.headingTemperatureCelsius.AutoSize = true;
            this.headingTemperatureCelsius.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headingTemperatureCelsius.Location = new System.Drawing.Point(3, 53);
            this.headingTemperatureCelsius.Name = "headingTemperatureCelsius";
            this.headingTemperatureCelsius.Size = new System.Drawing.Size(158, 28);
            this.headingTemperatureCelsius.TabIndex = 3;
            this.headingTemperatureCelsius.Text = "Temperature °C";
            // 
            // temperatureCelsiusTomorrowIO
            // 
            this.temperatureCelsiusTomorrowIO.AutoSize = true;
            this.temperatureCelsiusTomorrowIO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTomorrowIO, "TemperatureCelsius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.temperatureCelsiusTomorrowIO.Location = new System.Drawing.Point(335, 53);
            this.temperatureCelsiusTomorrowIO.Name = "temperatureCelsiusTomorrowIO";
            this.temperatureCelsiusTomorrowIO.Size = new System.Drawing.Size(99, 28);
            this.temperatureCelsiusTomorrowIO.TabIndex = 4;
            this.temperatureCelsiusTomorrowIO.Text = "Loading....";
            // 
            // temperatureCelsiusStormGlass
            // 
            this.temperatureCelsiusStormGlass.AutoSize = true;
            this.temperatureCelsiusStormGlass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceStormGlass, "TemperatureCelsius", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.temperatureCelsiusStormGlass.Location = new System.Drawing.Point(501, 53);
            this.temperatureCelsiusStormGlass.Name = "temperatureCelsiusStormGlass";
            this.temperatureCelsiusStormGlass.Size = new System.Drawing.Size(99, 28);
            this.temperatureCelsiusStormGlass.TabIndex = 5;
            this.temperatureCelsiusStormGlass.Text = "Loading....";
            // 
            // headingTemperatureFahrenheit
            // 
            this.headingTemperatureFahrenheit.AutoSize = true;
            this.headingTemperatureFahrenheit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headingTemperatureFahrenheit.Location = new System.Drawing.Point(3, 106);
            this.headingTemperatureFahrenheit.Name = "headingTemperatureFahrenheit";
            this.headingTemperatureFahrenheit.Size = new System.Drawing.Size(156, 28);
            this.headingTemperatureFahrenheit.TabIndex = 6;
            this.headingTemperatureFahrenheit.Text = "Temperature °F";
            // 
            // temperatureFahrenheitTomorrowIO
            // 
            this.temperatureFahrenheitTomorrowIO.AutoSize = true;
            this.temperatureFahrenheitTomorrowIO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTomorrowIO, "TemperatureFahrenheit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.temperatureFahrenheitTomorrowIO.Location = new System.Drawing.Point(335, 106);
            this.temperatureFahrenheitTomorrowIO.Name = "temperatureFahrenheitTomorrowIO";
            this.temperatureFahrenheitTomorrowIO.Size = new System.Drawing.Size(99, 28);
            this.temperatureFahrenheitTomorrowIO.TabIndex = 7;
            this.temperatureFahrenheitTomorrowIO.Text = "Loading....";
            // 
            // temperatureFahrenheitStormGlass
            // 
            this.temperatureFahrenheitStormGlass.AutoSize = true;
            this.temperatureFahrenheitStormGlass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceStormGlass, "TemperatureFahrenheit", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.temperatureFahrenheitStormGlass.Location = new System.Drawing.Point(501, 106);
            this.temperatureFahrenheitStormGlass.Name = "temperatureFahrenheitStormGlass";
            this.temperatureFahrenheitStormGlass.Size = new System.Drawing.Size(99, 28);
            this.temperatureFahrenheitStormGlass.TabIndex = 8;
            this.temperatureFahrenheitStormGlass.Text = "Loading....";
            // 
            // headingCloudCoverage
            // 
            this.headingCloudCoverage.AutoSize = true;
            this.headingCloudCoverage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headingCloudCoverage.Location = new System.Drawing.Point(3, 159);
            this.headingCloudCoverage.Name = "headingCloudCoverage";
            this.headingCloudCoverage.Size = new System.Drawing.Size(160, 28);
            this.headingCloudCoverage.TabIndex = 9;
            this.headingCloudCoverage.Text = "Cloud Coverage";
            // 
            // cloudCoverageTomorrowIO
            // 
            this.cloudCoverageTomorrowIO.AutoSize = true;
            this.cloudCoverageTomorrowIO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTomorrowIO, "CloudCoverage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cloudCoverageTomorrowIO.Location = new System.Drawing.Point(335, 159);
            this.cloudCoverageTomorrowIO.Name = "cloudCoverageTomorrowIO";
            this.cloudCoverageTomorrowIO.Size = new System.Drawing.Size(99, 28);
            this.cloudCoverageTomorrowIO.TabIndex = 10;
            this.cloudCoverageTomorrowIO.Text = "Loading....";
            // 
            // cloudCoverageStormGlass
            // 
            this.cloudCoverageStormGlass.AutoSize = true;
            this.cloudCoverageStormGlass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceStormGlass, "CloudCoverage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.cloudCoverageStormGlass.Location = new System.Drawing.Point(501, 159);
            this.cloudCoverageStormGlass.Name = "cloudCoverageStormGlass";
            this.cloudCoverageStormGlass.Size = new System.Drawing.Size(99, 28);
            this.cloudCoverageStormGlass.TabIndex = 11;
            this.cloudCoverageStormGlass.Text = "Loading....";
            // 
            // headingHumidity
            // 
            this.headingHumidity.AutoSize = true;
            this.headingHumidity.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headingHumidity.Location = new System.Drawing.Point(3, 212);
            this.headingHumidity.Name = "headingHumidity";
            this.headingHumidity.Size = new System.Drawing.Size(100, 28);
            this.headingHumidity.TabIndex = 12;
            this.headingHumidity.Text = "Humidity";
            // 
            // humidityTomorrowIO
            // 
            this.humidityTomorrowIO.AutoSize = true;
            this.humidityTomorrowIO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTomorrowIO, "Humidity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.humidityTomorrowIO.Location = new System.Drawing.Point(335, 212);
            this.humidityTomorrowIO.Name = "humidityTomorrowIO";
            this.humidityTomorrowIO.Size = new System.Drawing.Size(99, 28);
            this.humidityTomorrowIO.TabIndex = 13;
            this.humidityTomorrowIO.Text = "Loading....";
            // 
            // humidityStormGlass
            // 
            this.humidityStormGlass.AutoSize = true;
            this.humidityStormGlass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceStormGlass, "Humidity", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.humidityStormGlass.Location = new System.Drawing.Point(501, 212);
            this.humidityStormGlass.Name = "humidityStormGlass";
            this.humidityStormGlass.Size = new System.Drawing.Size(99, 28);
            this.humidityStormGlass.TabIndex = 14;
            this.humidityStormGlass.Text = "Loading....";
            // 
            // headingPrecipitation
            // 
            this.headingPrecipitation.AutoSize = true;
            this.headingPrecipitation.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headingPrecipitation.Location = new System.Drawing.Point(3, 265);
            this.headingPrecipitation.Name = "headingPrecipitation";
            this.headingPrecipitation.Size = new System.Drawing.Size(134, 28);
            this.headingPrecipitation.TabIndex = 15;
            this.headingPrecipitation.Text = "Precipitation";
            // 
            // precipitationTomorrowIO
            // 
            this.precipitationTomorrowIO.AutoSize = true;
            this.precipitationTomorrowIO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTomorrowIO, "Precipitation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.precipitationTomorrowIO.Location = new System.Drawing.Point(335, 265);
            this.precipitationTomorrowIO.Name = "precipitationTomorrowIO";
            this.precipitationTomorrowIO.Size = new System.Drawing.Size(99, 28);
            this.precipitationTomorrowIO.TabIndex = 16;
            this.precipitationTomorrowIO.Text = "Loading....";
            // 
            // precipitationStormGlass
            // 
            this.precipitationStormGlass.AutoSize = true;
            this.precipitationStormGlass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceStormGlass, "Precipitation", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.precipitationStormGlass.Location = new System.Drawing.Point(501, 265);
            this.precipitationStormGlass.Name = "precipitationStormGlass";
            this.precipitationStormGlass.Size = new System.Drawing.Size(99, 28);
            this.precipitationStormGlass.TabIndex = 17;
            this.precipitationStormGlass.Text = "Loading....";
            // 
            // headingWindSpeed
            // 
            this.headingWindSpeed.AutoSize = true;
            this.headingWindSpeed.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headingWindSpeed.Location = new System.Drawing.Point(3, 318);
            this.headingWindSpeed.Name = "headingWindSpeed";
            this.headingWindSpeed.Size = new System.Drawing.Size(125, 28);
            this.headingWindSpeed.TabIndex = 18;
            this.headingWindSpeed.Text = "Wind Speed";
            // 
            // windSpeedTomorrowIO
            // 
            this.windSpeedTomorrowIO.AutoSize = true;
            this.windSpeedTomorrowIO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTomorrowIO, "WindSpeed", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.windSpeedTomorrowIO.Location = new System.Drawing.Point(335, 318);
            this.windSpeedTomorrowIO.Name = "windSpeedTomorrowIO";
            this.windSpeedTomorrowIO.Size = new System.Drawing.Size(99, 28);
            this.windSpeedTomorrowIO.TabIndex = 19;
            this.windSpeedTomorrowIO.Text = "Loading....";
            // 
            // windSpeedStormGlass
            // 
            this.windSpeedStormGlass.AutoSize = true;
            this.windSpeedStormGlass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceStormGlass, "WindSpeed", true));
            this.windSpeedStormGlass.Location = new System.Drawing.Point(501, 318);
            this.windSpeedStormGlass.Name = "windSpeedStormGlass";
            this.windSpeedStormGlass.Size = new System.Drawing.Size(99, 28);
            this.windSpeedStormGlass.TabIndex = 20;
            this.windSpeedStormGlass.Text = "Loading....";
            // 
            // headingWindDirection
            // 
            this.headingWindDirection.AutoSize = true;
            this.headingWindDirection.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.headingWindDirection.Location = new System.Drawing.Point(3, 371);
            this.headingWindDirection.Name = "headingWindDirection";
            this.headingWindDirection.Size = new System.Drawing.Size(156, 28);
            this.headingWindDirection.TabIndex = 21;
            this.headingWindDirection.Text = "Wind Direction";
            // 
            // windDirectionTomorrowIO
            // 
            this.windDirectionTomorrowIO.AutoSize = true;
            this.windDirectionTomorrowIO.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceTomorrowIO, "WindDirection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.windDirectionTomorrowIO.Location = new System.Drawing.Point(335, 371);
            this.windDirectionTomorrowIO.Name = "windDirectionTomorrowIO";
            this.windDirectionTomorrowIO.Size = new System.Drawing.Size(99, 28);
            this.windDirectionTomorrowIO.TabIndex = 22;
            this.windDirectionTomorrowIO.Text = "Loading....";
            // 
            // windDirectionStormGlass
            // 
            this.windDirectionStormGlass.AutoSize = true;
            this.windDirectionStormGlass.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceStormGlass, "WindDirection", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.windDirectionStormGlass.Location = new System.Drawing.Point(501, 371);
            this.windDirectionStormGlass.Name = "windDirectionStormGlass";
            this.windDirectionStormGlass.Size = new System.Drawing.Size(99, 28);
            this.windDirectionStormGlass.TabIndex = 23;
            this.windDirectionStormGlass.Text = "Loading....";
            // 
            // updateWeatherButton
            // 
            this.updateWeatherButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.updateWeatherButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateWeatherButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updateWeatherButton.Location = new System.Drawing.Point(501, 551);
            this.updateWeatherButton.Name = "updateWeatherButton";
            this.updateWeatherButton.Size = new System.Drawing.Size(163, 42);
            this.updateWeatherButton.TabIndex = 1;
            this.updateWeatherButton.Text = "Update Weather";
            this.updateWeatherButton.UseVisualStyleBackColor = true;
            this.updateWeatherButton.Click += new System.EventHandler(this.updateWeatherButton_Click);
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bindingSourceDate, "Date", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.time.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.time.Location = new System.Drawing.Point(12, 23);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(125, 31);
            this.time.TabIndex = 2;
            this.time.Text = "Loading....";
            // 
            // bindingSourceDate
            // 
            this.bindingSourceDate.DataSource = typeof(DateTimeManager.DateAndTime);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 616);
            this.Controls.Add(this.time);
            this.Controls.Add(this.updateWeatherButton);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(902, 663);
            this.Name = "MainForm";
            this.Text = "Weather";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceTomorrowIO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceStormGlass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSourceDate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label headingSourceName;
        private Label sourceNameTomorrowIO;
        private Label sourceNameStormGlass;
        private Label headingTemperatureCelsius;
        private Label temperatureCelsiusTomorrowIO;
        private Label temperatureCelsiusStormGlass;
        private Label headingTemperatureFahrenheit;
        private Label temperatureFahrenheitTomorrowIO;
        private Label temperatureFahrenheitStormGlass;
        private Label headingCloudCoverage;
        private Label cloudCoverageTomorrowIO;
        private Label cloudCoverageStormGlass;
        private Label headingHumidity;
        private Label humidityTomorrowIO;
        private Label humidityStormGlass;
        private Label headingPrecipitation;
        private Label precipitationTomorrowIO;
        private Label precipitationStormGlass;
        private Label headingWindSpeed;
        private Label windSpeedTomorrowIO;
        private Label windSpeedStormGlass;
        private Label headingWindDirection;
        private Label windDirectionTomorrowIO;
        private Label windDirectionStormGlass;
        private BindingSource bindingSourceTomorrowIO;
        private Button updateWeatherButton;
        private BindingSource bindingSourceStormGlass;
        private Label time;
        private BindingSource bindingSourceDate;
    }
}