namespace WinForms
{
	partial class Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form));
			this.refreshButton = new System.Windows.Forms.Button();
			this.tICheckBox = new System.Windows.Forms.CheckBox();
			this.sICheckBox = new System.Windows.Forms.CheckBox();
			this.warningTextBox = new System.Windows.Forms.TextBox();
			this.tITempC = new System.Windows.Forms.TextBox();
			this.tIPrecipitationIntensity = new System.Windows.Forms.TextBox();
			this.tIHumidity = new System.Windows.Forms.TextBox();
			this.tICloudcover = new System.Windows.Forms.TextBox();
			this.tITempF = new System.Windows.Forms.TextBox();
			this.tIWindDirection = new System.Windows.Forms.TextBox();
			this.tIWindSpeed = new System.Windows.Forms.TextBox();
			this.tIName = new System.Windows.Forms.TextBox();
			this.sIName = new System.Windows.Forms.TextBox();
			this.sIWindSpeed = new System.Windows.Forms.TextBox();
			this.sIWindDirection = new System.Windows.Forms.TextBox();
			this.sITempF = new System.Windows.Forms.TextBox();
			this.sICloudcover = new System.Windows.Forms.TextBox();
			this.sIHumidity = new System.Windows.Forms.TextBox();
			this.sIPrecipitationIntensity = new System.Windows.Forms.TextBox();
			this.sITempC = new System.Windows.Forms.TextBox();
			this.clearButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// refreshButton
			// 
			this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.refreshButton.BackColor = System.Drawing.Color.Azure;
			this.refreshButton.Location = new System.Drawing.Point(485, 332);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(85, 33);
			this.refreshButton.TabIndex = 1;
			this.refreshButton.Text = "Refresh";
			this.refreshButton.UseVisualStyleBackColor = false;
			this.refreshButton.Click += new System.EventHandler(this.RefreshClick);
			// 
			// tICheckBox
			// 
			this.tICheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.tICheckBox.AutoSize = true;
			this.tICheckBox.Location = new System.Drawing.Point(8, 321);
			this.tICheckBox.Name = "tICheckBox";
			this.tICheckBox.Size = new System.Drawing.Size(93, 19);
			this.tICheckBox.TabIndex = 2;
			this.tICheckBox.Text = "Tomorrow.io";
			this.tICheckBox.UseVisualStyleBackColor = true;
			this.tICheckBox.CheckedChanged += new System.EventHandler(this.TomorrowIoCheckBoxCheckedChanged);
			// 
			// sICheckBox
			// 
			this.sICheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.sICheckBox.AutoSize = true;
			this.sICheckBox.Location = new System.Drawing.Point(8, 346);
			this.sICheckBox.Name = "sICheckBox";
			this.sICheckBox.Size = new System.Drawing.Size(97, 19);
			this.sICheckBox.TabIndex = 3;
			this.sICheckBox.Text = "Stormglass.io";
			this.sICheckBox.UseVisualStyleBackColor = true;
			this.sICheckBox.CheckedChanged += new System.EventHandler(this.StormglassIoCheckBoxCheckedChanged);
			// 
			// warningTextBox
			// 
			this.warningTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.warningTextBox.BackColor = System.Drawing.Color.Azure;
			this.warningTextBox.Location = new System.Drawing.Point(135, 12);
			this.warningTextBox.Multiline = true;
			this.warningTextBox.Name = "warningTextBox";
			this.warningTextBox.ReadOnly = true;
			this.warningTextBox.Size = new System.Drawing.Size(316, 20);
			this.warningTextBox.TabIndex = 4;
			this.warningTextBox.Text = "Warning: stormglass.io has a limit of 10 requests per day";
			this.warningTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tITempC
			// 
			this.tITempC.BackColor = System.Drawing.Color.AliceBlue;
			this.tITempC.Location = new System.Drawing.Point(12, 93);
			this.tITempC.Name = "tITempC";
			this.tITempC.ReadOnly = true;
			this.tITempC.Size = new System.Drawing.Size(178, 23);
			this.tITempC.TabIndex = 5;
			this.tITempC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tIPrecipitationIntensity
			// 
			this.tIPrecipitationIntensity.BackColor = System.Drawing.Color.AliceBlue;
			this.tIPrecipitationIntensity.Location = new System.Drawing.Point(12, 209);
			this.tIPrecipitationIntensity.Name = "tIPrecipitationIntensity";
			this.tIPrecipitationIntensity.ReadOnly = true;
			this.tIPrecipitationIntensity.Size = new System.Drawing.Size(178, 23);
			this.tIPrecipitationIntensity.TabIndex = 6;
			this.tIPrecipitationIntensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tIHumidity
			// 
			this.tIHumidity.BackColor = System.Drawing.Color.AliceBlue;
			this.tIHumidity.Location = new System.Drawing.Point(12, 180);
			this.tIHumidity.Name = "tIHumidity";
			this.tIHumidity.ReadOnly = true;
			this.tIHumidity.Size = new System.Drawing.Size(178, 23);
			this.tIHumidity.TabIndex = 7;
			this.tIHumidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tICloudcover
			// 
			this.tICloudcover.BackColor = System.Drawing.Color.AliceBlue;
			this.tICloudcover.Location = new System.Drawing.Point(12, 151);
			this.tICloudcover.Name = "tICloudcover";
			this.tICloudcover.ReadOnly = true;
			this.tICloudcover.Size = new System.Drawing.Size(178, 23);
			this.tICloudcover.TabIndex = 8;
			this.tICloudcover.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tITempF
			// 
			this.tITempF.BackColor = System.Drawing.Color.AliceBlue;
			this.tITempF.Location = new System.Drawing.Point(12, 122);
			this.tITempF.Name = "tITempF";
			this.tITempF.ReadOnly = true;
			this.tITempF.Size = new System.Drawing.Size(178, 23);
			this.tITempF.TabIndex = 9;
			this.tITempF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tIWindDirection
			// 
			this.tIWindDirection.BackColor = System.Drawing.Color.AliceBlue;
			this.tIWindDirection.Location = new System.Drawing.Point(12, 238);
			this.tIWindDirection.Name = "tIWindDirection";
			this.tIWindDirection.ReadOnly = true;
			this.tIWindDirection.Size = new System.Drawing.Size(178, 23);
			this.tIWindDirection.TabIndex = 10;
			this.tIWindDirection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tIWindSpeed
			// 
			this.tIWindSpeed.BackColor = System.Drawing.Color.AliceBlue;
			this.tIWindSpeed.Location = new System.Drawing.Point(12, 267);
			this.tIWindSpeed.Name = "tIWindSpeed";
			this.tIWindSpeed.ReadOnly = true;
			this.tIWindSpeed.Size = new System.Drawing.Size(178, 23);
			this.tIWindSpeed.TabIndex = 11;
			this.tIWindSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tIName
			// 
			this.tIName.BackColor = System.Drawing.Color.AliceBlue;
			this.tIName.Location = new System.Drawing.Point(12, 64);
			this.tIName.Name = "tIName";
			this.tIName.ReadOnly = true;
			this.tIName.Size = new System.Drawing.Size(178, 23);
			this.tIName.TabIndex = 19;
			this.tIName.Text = "Tomorrow.io:";
			this.tIName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sIName
			// 
			this.sIName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sIName.BackColor = System.Drawing.Color.AliceBlue;
			this.sIName.Location = new System.Drawing.Point(392, 64);
			this.sIName.Name = "sIName";
			this.sIName.ReadOnly = true;
			this.sIName.Size = new System.Drawing.Size(178, 23);
			this.sIName.TabIndex = 27;
			this.sIName.Text = "Stormglass.io";
			this.sIName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sIWindSpeed
			// 
			this.sIWindSpeed.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sIWindSpeed.BackColor = System.Drawing.Color.AliceBlue;
			this.sIWindSpeed.Location = new System.Drawing.Point(392, 267);
			this.sIWindSpeed.Name = "sIWindSpeed";
			this.sIWindSpeed.ReadOnly = true;
			this.sIWindSpeed.Size = new System.Drawing.Size(178, 23);
			this.sIWindSpeed.TabIndex = 26;
			this.sIWindSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sIWindDirection
			// 
			this.sIWindDirection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sIWindDirection.BackColor = System.Drawing.Color.AliceBlue;
			this.sIWindDirection.Location = new System.Drawing.Point(392, 238);
			this.sIWindDirection.Name = "sIWindDirection";
			this.sIWindDirection.ReadOnly = true;
			this.sIWindDirection.Size = new System.Drawing.Size(178, 23);
			this.sIWindDirection.TabIndex = 25;
			this.sIWindDirection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sITempF
			// 
			this.sITempF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sITempF.BackColor = System.Drawing.Color.AliceBlue;
			this.sITempF.Location = new System.Drawing.Point(392, 122);
			this.sITempF.Name = "sITempF";
			this.sITempF.ReadOnly = true;
			this.sITempF.Size = new System.Drawing.Size(178, 23);
			this.sITempF.TabIndex = 24;
			this.sITempF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sICloudcover
			// 
			this.sICloudcover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sICloudcover.BackColor = System.Drawing.Color.AliceBlue;
			this.sICloudcover.Location = new System.Drawing.Point(392, 151);
			this.sICloudcover.Name = "sICloudcover";
			this.sICloudcover.ReadOnly = true;
			this.sICloudcover.Size = new System.Drawing.Size(178, 23);
			this.sICloudcover.TabIndex = 23;
			this.sICloudcover.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sIHumidity
			// 
			this.sIHumidity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sIHumidity.BackColor = System.Drawing.Color.AliceBlue;
			this.sIHumidity.Location = new System.Drawing.Point(392, 180);
			this.sIHumidity.Name = "sIHumidity";
			this.sIHumidity.ReadOnly = true;
			this.sIHumidity.Size = new System.Drawing.Size(178, 23);
			this.sIHumidity.TabIndex = 22;
			this.sIHumidity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sIPrecipitationIntensity
			// 
			this.sIPrecipitationIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sIPrecipitationIntensity.BackColor = System.Drawing.Color.AliceBlue;
			this.sIPrecipitationIntensity.Location = new System.Drawing.Point(392, 209);
			this.sIPrecipitationIntensity.Name = "sIPrecipitationIntensity";
			this.sIPrecipitationIntensity.ReadOnly = true;
			this.sIPrecipitationIntensity.Size = new System.Drawing.Size(178, 23);
			this.sIPrecipitationIntensity.TabIndex = 21;
			this.sIPrecipitationIntensity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// sITempC
			// 
			this.sITempC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.sITempC.BackColor = System.Drawing.Color.AliceBlue;
			this.sITempC.Location = new System.Drawing.Point(392, 93);
			this.sITempC.Name = "sITempC";
			this.sITempC.ReadOnly = true;
			this.sITempC.Size = new System.Drawing.Size(178, 23);
			this.sITempC.TabIndex = 20;
			this.sITempC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// clearButton
			// 
			this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.clearButton.BackColor = System.Drawing.Color.Azure;
			this.clearButton.Location = new System.Drawing.Point(392, 332);
			this.clearButton.Name = "clearButton";
			this.clearButton.Size = new System.Drawing.Size(85, 33);
			this.clearButton.TabIndex = 28;
			this.clearButton.Text = "Clear";
			this.clearButton.UseVisualStyleBackColor = false;
			this.clearButton.Click += new System.EventHandler(this.ClearButtonClick);
			// 
			// Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Azure;
			this.ClientSize = new System.Drawing.Size(582, 375);
			this.Controls.Add(this.clearButton);
			this.Controls.Add(this.sIName);
			this.Controls.Add(this.sIWindSpeed);
			this.Controls.Add(this.sIWindDirection);
			this.Controls.Add(this.sITempF);
			this.Controls.Add(this.sICloudcover);
			this.Controls.Add(this.sIHumidity);
			this.Controls.Add(this.sIPrecipitationIntensity);
			this.Controls.Add(this.sITempC);
			this.Controls.Add(this.tIName);
			this.Controls.Add(this.tIWindSpeed);
			this.Controls.Add(this.tIWindDirection);
			this.Controls.Add(this.tITempF);
			this.Controls.Add(this.tICloudcover);
			this.Controls.Add(this.tIHumidity);
			this.Controls.Add(this.tIPrecipitationIntensity);
			this.Controls.Add(this.tITempC);
			this.Controls.Add(this.warningTextBox);
			this.Controls.Add(this.sICheckBox);
			this.Controls.Add(this.tICheckBox);
			this.Controls.Add(this.refreshButton);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Weather Forecast";
			this.Load += new System.EventHandler(this.FormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button refreshButton;
		private System.Windows.Forms.CheckBox tICheckBox;
		private System.Windows.Forms.CheckBox sICheckBox;
		private System.Windows.Forms.TextBox warningTextBox;
		private System.Windows.Forms.TextBox tITempC;
		private System.Windows.Forms.TextBox tIPrecipitationIntensity;
		private System.Windows.Forms.TextBox tIHumidity;
		private System.Windows.Forms.TextBox tICloudcover;
		private System.Windows.Forms.TextBox tITempF;
		private System.Windows.Forms.TextBox tIWindDirection;
		private System.Windows.Forms.TextBox tIWindSpeed;
		private System.Windows.Forms.TextBox tIName;
		private System.Windows.Forms.TextBox sIName;
		private System.Windows.Forms.TextBox sIWindSpeed;
		private System.Windows.Forms.TextBox sIWindDirection;
		private System.Windows.Forms.TextBox sITempF;
		private System.Windows.Forms.TextBox sICloudcover;
		private System.Windows.Forms.TextBox sIHumidity;
		private System.Windows.Forms.TextBox sIPrecipitationIntensity;
		private System.Windows.Forms.TextBox sITempC;
		private System.Windows.Forms.Button clearButton;
	}
}

