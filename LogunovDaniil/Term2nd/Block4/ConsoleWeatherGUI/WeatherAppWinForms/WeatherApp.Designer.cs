namespace WeatherAppWinForms
{
	partial class WeatherApp
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
			this.tomorrowBox = new System.Windows.Forms.GroupBox();
			this.stormglassBox = new System.Windows.Forms.GroupBox();
			this.temperatureCLabel = new System.Windows.Forms.Label();
			this.temperatureFLabel = new System.Windows.Forms.Label();
			this.humidityLabel = new System.Windows.Forms.Label();
			this.cloudCoverLabel = new System.Windows.Forms.Label();
			this.windSpeedLabel = new System.Windows.Forms.Label();
			this.windDirectionLabel = new System.Windows.Forms.Label();
			this.precipitationLabel = new System.Windows.Forms.Label();
			this.updateButton = new System.Windows.Forms.Button();
			this.fetchStatusLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// tomorrowBox
			// 
			this.tomorrowBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.tomorrowBox.Location = new System.Drawing.Point(232, 90);
			this.tomorrowBox.Name = "tomorrowBox";
			this.tomorrowBox.Size = new System.Drawing.Size(250, 427);
			this.tomorrowBox.TabIndex = 0;
			this.tomorrowBox.TabStop = false;
			this.tomorrowBox.Text = "TomorrowIO";
			// 
			// stormglassBox
			// 
			this.stormglassBox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.stormglassBox.Location = new System.Drawing.Point(488, 90);
			this.stormglassBox.Name = "stormglassBox";
			this.stormglassBox.Size = new System.Drawing.Size(250, 427);
			this.stormglassBox.TabIndex = 1;
			this.stormglassBox.TabStop = false;
			this.stormglassBox.Text = "StormGlassIO";
			// 
			// temperatureCLabel
			// 
			this.temperatureCLabel.AutoSize = true;
			this.temperatureCLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.temperatureCLabel.Location = new System.Drawing.Point(22, 135);
			this.temperatureCLabel.Name = "temperatureCLabel";
			this.temperatureCLabel.Size = new System.Drawing.Size(173, 23);
			this.temperatureCLabel.TabIndex = 2;
			this.temperatureCLabel.Text = "Temperature (Celsius)";
			// 
			// temperatureFLabel
			// 
			this.temperatureFLabel.AutoSize = true;
			this.temperatureFLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.temperatureFLabel.Location = new System.Drawing.Point(22, 180);
			this.temperatureFLabel.Name = "temperatureFLabel";
			this.temperatureFLabel.Size = new System.Drawing.Size(201, 23);
			this.temperatureFLabel.TabIndex = 3;
			this.temperatureFLabel.Text = "Temperature (Fahrenheit)";
			// 
			// humidityLabel
			// 
			this.humidityLabel.AutoSize = true;
			this.humidityLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.humidityLabel.Location = new System.Drawing.Point(22, 225);
			this.humidityLabel.Name = "humidityLabel";
			this.humidityLabel.Size = new System.Drawing.Size(79, 23);
			this.humidityLabel.TabIndex = 4;
			this.humidityLabel.Text = "Humidity";
			// 
			// cloudCoverLabel
			// 
			this.cloudCoverLabel.AutoSize = true;
			this.cloudCoverLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cloudCoverLabel.Location = new System.Drawing.Point(22, 270);
			this.cloudCoverLabel.Name = "cloudCoverLabel";
			this.cloudCoverLabel.Size = new System.Drawing.Size(101, 23);
			this.cloudCoverLabel.TabIndex = 5;
			this.cloudCoverLabel.Text = "Cloud cover";
			// 
			// windSpeedLabel
			// 
			this.windSpeedLabel.AutoSize = true;
			this.windSpeedLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.windSpeedLabel.Location = new System.Drawing.Point(22, 315);
			this.windSpeedLabel.Name = "windSpeedLabel";
			this.windSpeedLabel.Size = new System.Drawing.Size(100, 23);
			this.windSpeedLabel.TabIndex = 6;
			this.windSpeedLabel.Text = "Wind speed";
			// 
			// windDirectionLabel
			// 
			this.windDirectionLabel.AutoSize = true;
			this.windDirectionLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.windDirectionLabel.Location = new System.Drawing.Point(22, 360);
			this.windDirectionLabel.Name = "windDirectionLabel";
			this.windDirectionLabel.Size = new System.Drawing.Size(122, 23);
			this.windDirectionLabel.TabIndex = 7;
			this.windDirectionLabel.Text = "Wind direction";
			// 
			// precipitationLabel
			// 
			this.precipitationLabel.AutoSize = true;
			this.precipitationLabel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.precipitationLabel.Location = new System.Drawing.Point(22, 405);
			this.precipitationLabel.Name = "precipitationLabel";
			this.precipitationLabel.Size = new System.Drawing.Size(106, 23);
			this.precipitationLabel.TabIndex = 8;
			this.precipitationLabel.Text = "Precipitation";
			// 
			// updateButton
			// 
			this.updateButton.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.updateButton.Location = new System.Drawing.Point(22, 22);
			this.updateButton.Name = "updateButton";
			this.updateButton.Size = new System.Drawing.Size(162, 53);
			this.updateButton.TabIndex = 9;
			this.updateButton.Text = "Update";
			this.updateButton.UseVisualStyleBackColor = true;
			this.updateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			// 
			// fetchStatusLabel
			// 
			this.fetchStatusLabel.AutoSize = true;
			this.fetchStatusLabel.Location = new System.Drawing.Point(105, 485);
			this.fetchStatusLabel.Name = "fetchStatusLabel";
			this.fetchStatusLabel.Size = new System.Drawing.Size(86, 20);
			this.fetchStatusLabel.TabIndex = 10;
			this.fetchStatusLabel.Text = "Fetch status";
			// 
			// WeatherApp
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(755, 529);
			this.Controls.Add(this.fetchStatusLabel);
			this.Controls.Add(this.updateButton);
			this.Controls.Add(this.precipitationLabel);
			this.Controls.Add(this.windDirectionLabel);
			this.Controls.Add(this.windSpeedLabel);
			this.Controls.Add(this.cloudCoverLabel);
			this.Controls.Add(this.humidityLabel);
			this.Controls.Add(this.temperatureFLabel);
			this.Controls.Add(this.temperatureCLabel);
			this.Controls.Add(this.stormglassBox);
			this.Controls.Add(this.tomorrowBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "WeatherApp";
			this.Text = "Current Weather";
			this.Load += new System.EventHandler(this.WeatherApp_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private GroupBox tomorrowBox;
		private GroupBox stormglassBox;
		private Label temperatureCLabel;
		private Label temperatureFLabel;
		private Label humidityLabel;
		private Label cloudCoverLabel;
		private Label windSpeedLabel;
		private Label windDirectionLabel;
		private Label precipitationLabel;
		private Button updateButton;
		private Label fetchStatusLabel;
	}
}