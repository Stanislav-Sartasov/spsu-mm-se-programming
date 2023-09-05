namespace Task_4_WinForms
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
            this.siteName = new System.Windows.Forms.Label();
            this.temp = new System.Windows.Forms.Label();
            this.clouds = new System.Windows.Forms.Label();
            this.humidity = new System.Windows.Forms.Label();
            this.windSpd = new System.Windows.Forms.Label();
            this.windDeg = new System.Windows.Forms.Label();
            this.refresh = new System.Windows.Forms.Button();
            this.sw = new System.Windows.Forms.Button();
            this.tempValue = new System.Windows.Forms.Label();
            this.cloudsValue = new System.Windows.Forms.Label();
            this.humidityValue = new System.Windows.Forms.Label();
            this.windSpdValue = new System.Windows.Forms.Label();
            this.windDegValue = new System.Windows.Forms.Label();
            this.fallout = new System.Windows.Forms.Label();
            this.falloutValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // siteName
            // 
            this.siteName.BackColor = System.Drawing.Color.Transparent;
            this.siteName.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.siteName.Location = new System.Drawing.Point(0, 0);
            this.siteName.Margin = new System.Windows.Forms.Padding(0);
            this.siteName.Name = "siteName";
            this.siteName.Size = new System.Drawing.Size(300, 60);
            this.siteName.TabIndex = 0;
            this.siteName.Text = "Sitename";
            this.siteName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // temp
            // 
            this.temp.BackColor = System.Drawing.Color.Transparent;
            this.temp.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.temp.Location = new System.Drawing.Point(0, 60);
            this.temp.Name = "temp";
            this.temp.Size = new System.Drawing.Size(184, 60);
            this.temp.TabIndex = 1;
            this.temp.Text = "Temperature";
            this.temp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clouds
            // 
            this.clouds.BackColor = System.Drawing.Color.Transparent;
            this.clouds.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.clouds.Location = new System.Drawing.Point(0, 120);
            this.clouds.Name = "clouds";
            this.clouds.Size = new System.Drawing.Size(184, 60);
            this.clouds.TabIndex = 2;
            this.clouds.Text = "Clouds";
            this.clouds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // humidity
            // 
            this.humidity.BackColor = System.Drawing.Color.Transparent;
            this.humidity.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.humidity.Location = new System.Drawing.Point(0, 180);
            this.humidity.Name = "humidity";
            this.humidity.Size = new System.Drawing.Size(184, 60);
            this.humidity.TabIndex = 3;
            this.humidity.Text = "Humidity";
            this.humidity.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // windSpd
            // 
            this.windSpd.BackColor = System.Drawing.Color.Transparent;
            this.windSpd.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.windSpd.Location = new System.Drawing.Point(0, 240);
            this.windSpd.Name = "windSpd";
            this.windSpd.Size = new System.Drawing.Size(188, 60);
            this.windSpd.TabIndex = 4;
            this.windSpd.Text = "Wind speed";
            this.windSpd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // windDeg
            // 
            this.windDeg.BackColor = System.Drawing.Color.Transparent;
            this.windDeg.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.windDeg.Location = new System.Drawing.Point(0, 300);
            this.windDeg.Name = "windDeg";
            this.windDeg.Size = new System.Drawing.Size(188, 60);
            this.windDeg.TabIndex = 5;
            this.windDeg.Text = "Wind degree";
            this.windDeg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // refresh
            // 
            this.refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refresh.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.refresh.Location = new System.Drawing.Point(0, 423);
            this.refresh.Name = "refresh";
            this.refresh.Size = new System.Drawing.Size(184, 40);
            this.refresh.TabIndex = 6;
            this.refresh.Text = "refresh";
            this.refresh.UseVisualStyleBackColor = true;
            this.refresh.Click += new System.EventHandler(this.refreshClick);
            // 
            // sw
            // 
            this.sw.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sw.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.sw.Location = new System.Drawing.Point(185, 423);
            this.sw.Name = "sw";
            this.sw.Size = new System.Drawing.Size(115, 40);
            this.sw.TabIndex = 7;
            this.sw.Text = "switch";
            this.sw.UseVisualStyleBackColor = true;
            this.sw.Click += new System.EventHandler(this.swClick);
            // 
            // tempValue
            // 
            this.tempValue.BackColor = System.Drawing.Color.Transparent;
            this.tempValue.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.tempValue.Location = new System.Drawing.Point(185, 60);
            this.tempValue.Name = "tempValue";
            this.tempValue.Size = new System.Drawing.Size(116, 60);
            this.tempValue.TabIndex = 8;
            this.tempValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cloudsValue
            // 
            this.cloudsValue.BackColor = System.Drawing.Color.Transparent;
            this.cloudsValue.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.cloudsValue.Location = new System.Drawing.Point(185, 120);
            this.cloudsValue.Name = "cloudsValue";
            this.cloudsValue.Size = new System.Drawing.Size(116, 60);
            this.cloudsValue.TabIndex = 9;
            this.cloudsValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // humidityValue
            // 
            this.humidityValue.BackColor = System.Drawing.Color.Transparent;
            this.humidityValue.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.humidityValue.Location = new System.Drawing.Point(185, 180);
            this.humidityValue.Name = "humidityValue";
            this.humidityValue.Size = new System.Drawing.Size(116, 60);
            this.humidityValue.TabIndex = 10;
            this.humidityValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // windSpdValue
            // 
            this.windSpdValue.BackColor = System.Drawing.Color.Transparent;
            this.windSpdValue.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.windSpdValue.Location = new System.Drawing.Point(185, 240);
            this.windSpdValue.Name = "windSpdValue";
            this.windSpdValue.Size = new System.Drawing.Size(116, 60);
            this.windSpdValue.TabIndex = 11;
            this.windSpdValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.windSpdValue.UseMnemonic = false;
            // 
            // windDegValue
            // 
            this.windDegValue.BackColor = System.Drawing.Color.Transparent;
            this.windDegValue.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.windDegValue.Location = new System.Drawing.Point(185, 300);
            this.windDegValue.Name = "windDegValue";
            this.windDegValue.Size = new System.Drawing.Size(116, 60);
            this.windDegValue.TabIndex = 12;
            this.windDegValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fallout
            // 
            this.fallout.BackColor = System.Drawing.Color.Transparent;
            this.fallout.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.fallout.Location = new System.Drawing.Point(0, 360);
            this.fallout.Name = "fallout";
            this.fallout.Size = new System.Drawing.Size(188, 60);
            this.fallout.TabIndex = 13;
            this.fallout.Text = "Fallout";
            this.fallout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // falloutValue
            // 
            this.falloutValue.BackColor = System.Drawing.Color.Transparent;
            this.falloutValue.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.falloutValue.Location = new System.Drawing.Point(185, 360);
            this.falloutValue.Name = "falloutValue";
            this.falloutValue.Size = new System.Drawing.Size(116, 60);
            this.falloutValue.TabIndex = 14;
            this.falloutValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Task_4_WinForms.Properties.Resources.backround;
            this.ClientSize = new System.Drawing.Size(300, 463);
            this.Controls.Add(this.falloutValue);
            this.Controls.Add(this.fallout);
            this.Controls.Add(this.windDegValue);
            this.Controls.Add(this.windSpdValue);
            this.Controls.Add(this.humidityValue);
            this.Controls.Add(this.cloudsValue);
            this.Controls.Add(this.tempValue);
            this.Controls.Add(this.sw);
            this.Controls.Add(this.refresh);
            this.Controls.Add(this.windDeg);
            this.Controls.Add(this.windSpd);
            this.Controls.Add(this.humidity);
            this.Controls.Add(this.clouds);
            this.Controls.Add(this.temp);
            this.Controls.Add(this.siteName);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(318, 510);
            this.Name = "MainForm";
            this.Text = "Weather";
            this.ResumeLayout(false);

        }

        #endregion

        public Label siteName;
        private Label temp;
        private Label clouds;
        private Label humidity;
        private Label windSpd;
        private Label windDeg;
        public Button refresh;
        public Button sw;
        public Label tempValue;
        public Label cloudsValue;
        public Label humidityValue;
        public Label windSpdValue;
        public Label windDegValue;
        private Label fallout;
        public Label falloutValue;
    }
}