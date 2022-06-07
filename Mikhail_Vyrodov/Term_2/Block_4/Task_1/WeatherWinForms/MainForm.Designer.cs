
namespace WeatherWinForms
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.tomorrowTemperatureTextBox = new System.Windows.Forms.TextBox();
            this.tomorrowWindDirectionTextBox = new System.Windows.Forms.TextBox();
            this.tomorrowWindSpeedTextBox = new System.Windows.Forms.TextBox();
            this.tomorrowHumidityTextBox = new System.Windows.Forms.TextBox();
            this.tomorrowPrecipitationTextBox = new System.Windows.Forms.TextBox();
            this.tomorrowCloudCoverTextBox = new System.Windows.Forms.TextBox();
            this.tomorrowSiteTextBox = new System.Windows.Forms.TextBox();
            this.stormglassSiteTextBox = new System.Windows.Forms.TextBox();
            this.stormglassTemperatureTextBox = new System.Windows.Forms.TextBox();
            this.stormglassWindDirectionTextBox = new System.Windows.Forms.TextBox();
            this.stormglassWindSpeedTextBox = new System.Windows.Forms.TextBox();
            this.stormglassHumidityTextBox = new System.Windows.Forms.TextBox();
            this.stormglassPrecipitationTextBox = new System.Windows.Forms.TextBox();
            this.stormglassCloudCoverTextBox = new System.Windows.Forms.TextBox();
            this.siteText = new System.Windows.Forms.TextBox();
            this.temperatureText = new System.Windows.Forms.TextBox();
            this.windDirectionText = new System.Windows.Forms.TextBox();
            this.windSpeedText = new System.Windows.Forms.TextBox();
            this.humidityText = new System.Windows.Forms.TextBox();
            this.precipitationText = new System.Windows.Forms.TextBox();
            this.cloudCoverText = new System.Windows.Forms.TextBox();
            this.RefreshButton = new System.Windows.Forms.Button();
            this.weatherBinding = new System.Windows.Forms.BindingSource(this.components);
            this.tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weatherBinding)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel.ColumnCount = 3;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.66667F));
            this.tableLayoutPanel.Controls.Add(this.tomorrowTemperatureTextBox, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.tomorrowWindDirectionTextBox, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.tomorrowWindSpeedTextBox, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.tomorrowHumidityTextBox, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.tomorrowPrecipitationTextBox, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.tomorrowCloudCoverTextBox, 1, 7);
            this.tableLayoutPanel.Controls.Add(this.tomorrowSiteTextBox, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.stormglassSiteTextBox, 2, 1);
            this.tableLayoutPanel.Controls.Add(this.stormglassTemperatureTextBox, 2, 2);
            this.tableLayoutPanel.Controls.Add(this.stormglassWindDirectionTextBox, 2, 3);
            this.tableLayoutPanel.Controls.Add(this.stormglassWindSpeedTextBox, 2, 4);
            this.tableLayoutPanel.Controls.Add(this.stormglassHumidityTextBox, 2, 5);
            this.tableLayoutPanel.Controls.Add(this.stormglassPrecipitationTextBox, 2, 6);
            this.tableLayoutPanel.Controls.Add(this.stormglassCloudCoverTextBox, 2, 7);
            this.tableLayoutPanel.Controls.Add(this.siteText, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.temperatureText, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.windDirectionText, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.windSpeedText, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.humidityText, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.precipitationText, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.cloudCoverText, 0, 7);
            this.tableLayoutPanel.Location = new System.Drawing.Point(12, 46);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 7;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(733, 284);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // tomorrowTemperatureTextBox
            // 
            this.tomorrowTemperatureTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tomorrowTemperatureTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tomorrowTemperatureTextBox.Location = new System.Drawing.Point(125, 29);
            this.tomorrowTemperatureTextBox.Name = "tomorrowTemperatureTextBox";
            this.tomorrowTemperatureTextBox.ReadOnly = true;
            this.tomorrowTemperatureTextBox.Size = new System.Drawing.Size(299, 20);
            this.tomorrowTemperatureTextBox.TabIndex = 6;
            // 
            // tomorrowWindDirectionTextBox
            // 
            this.tomorrowWindDirectionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tomorrowWindDirectionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tomorrowWindDirectionTextBox.Location = new System.Drawing.Point(125, 55);
            this.tomorrowWindDirectionTextBox.Name = "tomorrowWindDirectionTextBox";
            this.tomorrowWindDirectionTextBox.ReadOnly = true;
            this.tomorrowWindDirectionTextBox.Size = new System.Drawing.Size(299, 20);
            this.tomorrowWindDirectionTextBox.TabIndex = 7;
            // 
            // tomorrowWindSpeedTextBox
            // 
            this.tomorrowWindSpeedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tomorrowWindSpeedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tomorrowWindSpeedTextBox.Location = new System.Drawing.Point(125, 81);
            this.tomorrowWindSpeedTextBox.Name = "tomorrowWindSpeedTextBox";
            this.tomorrowWindSpeedTextBox.ReadOnly = true;
            this.tomorrowWindSpeedTextBox.Size = new System.Drawing.Size(299, 20);
            this.tomorrowWindSpeedTextBox.TabIndex = 8;
            // 
            // tomorrowHumidityTextBox
            // 
            this.tomorrowHumidityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tomorrowHumidityTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tomorrowHumidityTextBox.Location = new System.Drawing.Point(125, 107);
            this.tomorrowHumidityTextBox.Name = "tomorrowHumidityTextBox";
            this.tomorrowHumidityTextBox.ReadOnly = true;
            this.tomorrowHumidityTextBox.Size = new System.Drawing.Size(299, 20);
            this.tomorrowHumidityTextBox.TabIndex = 9;
            // 
            // tomorrowPrecipitationTextBox
            // 
            this.tomorrowPrecipitationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tomorrowPrecipitationTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tomorrowPrecipitationTextBox.Location = new System.Drawing.Point(125, 133);
            this.tomorrowPrecipitationTextBox.Name = "tomorrowPrecipitationTextBox";
            this.tomorrowPrecipitationTextBox.ReadOnly = true;
            this.tomorrowPrecipitationTextBox.Size = new System.Drawing.Size(299, 20);
            this.tomorrowPrecipitationTextBox.TabIndex = 10;
            // 
            // tomorrowCloudCoverTextBox
            // 
            this.tomorrowCloudCoverTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tomorrowCloudCoverTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tomorrowCloudCoverTextBox.Location = new System.Drawing.Point(125, 159);
            this.tomorrowCloudCoverTextBox.Name = "tomorrowCloudCoverTextBox";
            this.tomorrowCloudCoverTextBox.ReadOnly = true;
            this.tomorrowCloudCoverTextBox.Size = new System.Drawing.Size(299, 20);
            this.tomorrowCloudCoverTextBox.TabIndex = 11;
            // 
            // tomorrowSiteTextBox
            // 
            this.tomorrowSiteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tomorrowSiteTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tomorrowSiteTextBox.Location = new System.Drawing.Point(125, 3);
            this.tomorrowSiteTextBox.Name = "tomorrowSiteTextBox";
            this.tomorrowSiteTextBox.ReadOnly = true;
            this.tomorrowSiteTextBox.Size = new System.Drawing.Size(299, 20);
            this.tomorrowSiteTextBox.TabIndex = 12;
            this.tomorrowSiteTextBox.Text = "tomorrow.io";
            // 
            // stormglassSiteTextBox
            // 
            this.stormglassSiteTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stormglassSiteTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stormglassSiteTextBox.Location = new System.Drawing.Point(430, 3);
            this.stormglassSiteTextBox.Name = "stormglassSiteTextBox";
            this.stormglassSiteTextBox.ReadOnly = true;
            this.stormglassSiteTextBox.Size = new System.Drawing.Size(300, 20);
            this.stormglassSiteTextBox.TabIndex = 13;
            this.stormglassSiteTextBox.Text = "stormglass.io";
            // 
            // stormglassTemperatureTextBox
            // 
            this.stormglassTemperatureTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stormglassTemperatureTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stormglassTemperatureTextBox.Location = new System.Drawing.Point(430, 29);
            this.stormglassTemperatureTextBox.Name = "stormglassTemperatureTextBox";
            this.stormglassTemperatureTextBox.ReadOnly = true;
            this.stormglassTemperatureTextBox.Size = new System.Drawing.Size(300, 20);
            this.stormglassTemperatureTextBox.TabIndex = 14;
            // 
            // stormglassWindDirectionTextBox
            // 
            this.stormglassWindDirectionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stormglassWindDirectionTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stormglassWindDirectionTextBox.Location = new System.Drawing.Point(430, 55);
            this.stormglassWindDirectionTextBox.Name = "stormglassWindDirectionTextBox";
            this.stormglassWindDirectionTextBox.ReadOnly = true;
            this.stormglassWindDirectionTextBox.Size = new System.Drawing.Size(300, 20);
            this.stormglassWindDirectionTextBox.TabIndex = 15;
            // 
            // stormglassWindSpeedTextBox
            // 
            this.stormglassWindSpeedTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stormglassWindSpeedTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stormglassWindSpeedTextBox.Location = new System.Drawing.Point(430, 81);
            this.stormglassWindSpeedTextBox.Name = "stormglassWindSpeedTextBox";
            this.stormglassWindSpeedTextBox.ReadOnly = true;
            this.stormglassWindSpeedTextBox.Size = new System.Drawing.Size(300, 20);
            this.stormglassWindSpeedTextBox.TabIndex = 16;
            // 
            // stormglassHumidityTextBox
            // 
            this.stormglassHumidityTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stormglassHumidityTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stormglassHumidityTextBox.Location = new System.Drawing.Point(430, 107);
            this.stormglassHumidityTextBox.Name = "stormglassHumidityTextBox";
            this.stormglassHumidityTextBox.ReadOnly = true;
            this.stormglassHumidityTextBox.Size = new System.Drawing.Size(300, 20);
            this.stormglassHumidityTextBox.TabIndex = 17;
            // 
            // stormglassPrecipitationTextBox
            // 
            this.stormglassPrecipitationTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stormglassPrecipitationTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stormglassPrecipitationTextBox.Location = new System.Drawing.Point(430, 133);
            this.stormglassPrecipitationTextBox.Name = "stormglassPrecipitationTextBox";
            this.stormglassPrecipitationTextBox.ReadOnly = true;
            this.stormglassPrecipitationTextBox.Size = new System.Drawing.Size(300, 20);
            this.stormglassPrecipitationTextBox.TabIndex = 18;
            // 
            // stormglassCloudCoverTextBox
            // 
            this.stormglassCloudCoverTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.stormglassCloudCoverTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stormglassCloudCoverTextBox.Location = new System.Drawing.Point(430, 159);
            this.stormglassCloudCoverTextBox.Name = "stormglassCloudCoverTextBox";
            this.stormglassCloudCoverTextBox.ReadOnly = true;
            this.stormglassCloudCoverTextBox.Size = new System.Drawing.Size(300, 20);
            this.stormglassCloudCoverTextBox.TabIndex = 19;
            // 
            // siteText
            // 
            this.siteText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.siteText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.siteText.Location = new System.Drawing.Point(3, 3);
            this.siteText.Name = "siteText";
            this.siteText.ReadOnly = true;
            this.siteText.Size = new System.Drawing.Size(116, 20);
            this.siteText.TabIndex = 20;
            this.siteText.Text = "Site";
            // 
            // temperatureText
            // 
            this.temperatureText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.temperatureText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.temperatureText.Location = new System.Drawing.Point(3, 29);
            this.temperatureText.Name = "temperatureText";
            this.temperatureText.ReadOnly = true;
            this.temperatureText.Size = new System.Drawing.Size(116, 20);
            this.temperatureText.TabIndex = 21;
            this.temperatureText.Text = "Temperature";
            // 
            // windDirectionText
            // 
            this.windDirectionText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.windDirectionText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windDirectionText.Location = new System.Drawing.Point(3, 55);
            this.windDirectionText.Name = "windDirectionText";
            this.windDirectionText.ReadOnly = true;
            this.windDirectionText.Size = new System.Drawing.Size(116, 20);
            this.windDirectionText.TabIndex = 22;
            this.windDirectionText.Text = "Wind Direction";
            // 
            // windSpeedText
            // 
            this.windSpeedText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.windSpeedText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.windSpeedText.Location = new System.Drawing.Point(3, 81);
            this.windSpeedText.Name = "windSpeedText";
            this.windSpeedText.ReadOnly = true;
            this.windSpeedText.Size = new System.Drawing.Size(116, 20);
            this.windSpeedText.TabIndex = 23;
            this.windSpeedText.Text = "Wind Speed";
            // 
            // humidityText
            // 
            this.humidityText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.humidityText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.humidityText.Location = new System.Drawing.Point(3, 107);
            this.humidityText.Name = "humidityText";
            this.humidityText.ReadOnly = true;
            this.humidityText.Size = new System.Drawing.Size(116, 20);
            this.humidityText.TabIndex = 24;
            this.humidityText.Text = "Humidity";
            // 
            // precipitationText
            // 
            this.precipitationText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.precipitationText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.precipitationText.Location = new System.Drawing.Point(3, 133);
            this.precipitationText.Name = "precipitationText";
            this.precipitationText.ReadOnly = true;
            this.precipitationText.Size = new System.Drawing.Size(116, 20);
            this.precipitationText.TabIndex = 25;
            this.precipitationText.Text = "Precipitation";
            // 
            // cloudCoverText
            // 
            this.cloudCoverText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.cloudCoverText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cloudCoverText.Location = new System.Drawing.Point(3, 159);
            this.cloudCoverText.Name = "cloudCoverText";
            this.cloudCoverText.ReadOnly = true;
            this.cloudCoverText.Size = new System.Drawing.Size(116, 20);
            this.cloudCoverText.TabIndex = 26;
            this.cloudCoverText.Text = "Cloud Cover";
            // 
            // RefreshButton
            // 
            this.RefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RefreshButton.Location = new System.Drawing.Point(12, 348);
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.Size = new System.Drawing.Size(150, 60);
            this.RefreshButton.TabIndex = 2;
            this.RefreshButton.Text = "Refresh";
            this.RefreshButton.UseVisualStyleBackColor = true;
            this.RefreshButton.Click += new System.EventHandler(this.RefreshButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 443);
            this.Controls.Add(this.RefreshButton);
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "MainForm";
            this.Text = "Current Weather";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.weatherBinding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Button RefreshButton;
        private System.Windows.Forms.TextBox tomorrowTemperatureTextBox;
        private System.Windows.Forms.TextBox tomorrowWindDirectionTextBox;
        private System.Windows.Forms.TextBox tomorrowWindSpeedTextBox;
        private System.Windows.Forms.TextBox tomorrowHumidityTextBox;
        private System.Windows.Forms.TextBox tomorrowPrecipitationTextBox;
        private System.Windows.Forms.TextBox tomorrowCloudCoverTextBox;
        private System.Windows.Forms.BindingSource weatherBinding;
        private System.Windows.Forms.TextBox tomorrowSiteTextBox;
        private System.Windows.Forms.TextBox stormglassSiteTextBox;
        private System.Windows.Forms.TextBox stormglassTemperatureTextBox;
        private System.Windows.Forms.TextBox stormglassWindDirectionTextBox;
        private System.Windows.Forms.TextBox stormglassWindSpeedTextBox;
        private System.Windows.Forms.TextBox stormglassHumidityTextBox;
        private System.Windows.Forms.TextBox stormglassPrecipitationTextBox;
        private System.Windows.Forms.TextBox stormglassCloudCoverTextBox;
        private System.Windows.Forms.TextBox siteText;
        private System.Windows.Forms.TextBox temperatureText;
        private System.Windows.Forms.TextBox windDirectionText;
        private System.Windows.Forms.TextBox windSpeedText;
        private System.Windows.Forms.TextBox humidityText;
        private System.Windows.Forms.TextBox precipitationText;
        private System.Windows.Forms.TextBox cloudCoverText;
    }
}

