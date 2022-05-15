namespace WinForms
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
            this.updateButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.tomorrowIoPanel = new System.Windows.Forms.Panel();
            this.openWeatherPanel = new System.Windows.Forms.Panel();
            this.tomorrowIoLabel = new System.Windows.Forms.Label();
            this.openWeatherLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // updateButton
            // 
            this.updateButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.updateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.updateButton.FlatAppearance.BorderSize = 2;
            this.updateButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.updateButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.updateButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateButton.Font = new System.Drawing.Font("Bahnschrift Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updateButton.Location = new System.Drawing.Point(217, 245);
            this.updateButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(111, 39);
            this.updateButton.TabIndex = 0;
            this.updateButton.Text = "Update";
            this.updateButton.UseVisualStyleBackColor = false;
            this.updateButton.Click += new System.EventHandler(this.UpdateButtonClick);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.exitButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exitButton.FlatAppearance.BorderColor = System.Drawing.Color.Gray;
            this.exitButton.FlatAppearance.BorderSize = 2;
            this.exitButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.DimGray;
            this.exitButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Bahnschrift Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.exitButton.Location = new System.Drawing.Point(354, 245);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(104, 39);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.ExitClick);
            // 
            // tomorrowIoPanel
            // 
            this.tomorrowIoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tomorrowIoPanel.Font = new System.Drawing.Font("Bahnschrift Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tomorrowIoPanel.Location = new System.Drawing.Point(17, 54);
            this.tomorrowIoPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tomorrowIoPanel.Name = "tomorrowIoPanel";
            this.tomorrowIoPanel.Size = new System.Drawing.Size(311, 181);
            this.tomorrowIoPanel.TabIndex = 2;
            // 
            // openWeatherPanel
            // 
            this.openWeatherPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.openWeatherPanel.Font = new System.Drawing.Font("Bahnschrift Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.openWeatherPanel.Location = new System.Drawing.Point(354, 54);
            this.openWeatherPanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.openWeatherPanel.Name = "openWeatherPanel";
            this.openWeatherPanel.Size = new System.Drawing.Size(321, 181);
            this.openWeatherPanel.TabIndex = 3;
            // 
            // tomorrowIoLabel
            // 
            this.tomorrowIoLabel.AutoSize = true;
            this.tomorrowIoLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.tomorrowIoLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tomorrowIoLabel.Font = new System.Drawing.Font("Bahnschrift Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.tomorrowIoLabel.Location = new System.Drawing.Point(93, 9);
            this.tomorrowIoLabel.Name = "tomorrowIoLabel";
            this.tomorrowIoLabel.Size = new System.Drawing.Size(145, 31);
            this.tomorrowIoLabel.TabIndex = 4;
            this.tomorrowIoLabel.Text = "TomorrowIo";
            // 
            // openWeatherLabel
            // 
            this.openWeatherLabel.AutoSize = true;
            this.openWeatherLabel.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.openWeatherLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.openWeatherLabel.Font = new System.Drawing.Font("Bahnschrift Light", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.openWeatherLabel.Location = new System.Drawing.Point(435, 9);
            this.openWeatherLabel.Name = "openWeatherLabel";
            this.openWeatherLabel.Size = new System.Drawing.Size(157, 31);
            this.openWeatherLabel.TabIndex = 5;
            this.openWeatherLabel.Text = "OpenWeather";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(692, 294);
            this.Controls.Add(this.openWeatherLabel);
            this.Controls.Add(this.tomorrowIoLabel);
            this.Controls.Add(this.openWeatherPanel);
            this.Controls.Add(this.tomorrowIoPanel);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.updateButton);
            this.Font = new System.Drawing.Font("JetBrains Mono", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Weather";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Button updateButton;
		private Button exitButton;
		private Panel tomorrowIoPanel;
		private Panel openWeatherPanel;
		private Label tomorrowIoLabel;
		private Label openWeatherLabel;
	}
}