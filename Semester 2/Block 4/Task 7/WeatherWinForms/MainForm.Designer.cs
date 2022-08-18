
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
			this.headerLabel = new System.Windows.Forms.Label();
			this.temperatureGroupBox = new System.Windows.Forms.GroupBox();
			this.temperatureRightLabel = new System.Windows.Forms.Label();
			this.temperatureLeftLabel = new System.Windows.Forms.Label();
			this.cloudCoverGroupBox = new System.Windows.Forms.GroupBox();
			this.cloudCoverRightLabel = new System.Windows.Forms.Label();
			this.cloudCoverLeftLabel = new System.Windows.Forms.Label();
			this.humidityGroupBox = new System.Windows.Forms.GroupBox();
			this.humidityRightLabel = new System.Windows.Forms.Label();
			this.humidityLeftLabel = new System.Windows.Forms.Label();
			this.precipitationRightLabel = new System.Windows.Forms.Label();
			this.precipitationLeftLabel = new System.Windows.Forms.Label();
			this.precipitationGroupBox = new System.Windows.Forms.GroupBox();
			this.windSpeedRightLabel = new System.Windows.Forms.Label();
			this.windSpeedLeftLabel = new System.Windows.Forms.Label();
			this.windSpeedGroupBox = new System.Windows.Forms.GroupBox();
			this.windDirectionRightLabel = new System.Windows.Forms.Label();
			this.windDirectionLeftLabel = new System.Windows.Forms.Label();
			this.windDirectionGroupBox = new System.Windows.Forms.GroupBox();
			this.refreshPictureBox = new System.Windows.Forms.PictureBox();
			this.cellarLeftLabel = new System.Windows.Forms.Label();
			this.cellarRightLabel = new System.Windows.Forms.Label();
			this.temperatureGroupBox.SuspendLayout();
			this.cloudCoverGroupBox.SuspendLayout();
			this.humidityGroupBox.SuspendLayout();
			this.precipitationGroupBox.SuspendLayout();
			this.windSpeedGroupBox.SuspendLayout();
			this.windDirectionGroupBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.refreshPictureBox)).BeginInit();
			this.SuspendLayout();
			// 
			// headerLabel
			// 
			this.headerLabel.AutoSize = true;
			this.headerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
			this.headerLabel.Location = new System.Drawing.Point(40, 31);
			this.headerLabel.Name = "headerLabel";
			this.headerLabel.Size = new System.Drawing.Size(322, 25);
			this.headerLabel.TabIndex = 0;
			this.headerLabel.Text = "Actual weather in St. Petersburg";
			// 
			// temperatureGroupBox
			// 
			this.temperatureGroupBox.Controls.Add(this.temperatureRightLabel);
			this.temperatureGroupBox.Controls.Add(this.temperatureLeftLabel);
			this.temperatureGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.temperatureGroupBox.Location = new System.Drawing.Point(37, 72);
			this.temperatureGroupBox.Name = "temperatureGroupBox";
			this.temperatureGroupBox.Size = new System.Drawing.Size(722, 52);
			this.temperatureGroupBox.TabIndex = 1;
			this.temperatureGroupBox.TabStop = false;
			this.temperatureGroupBox.Text = "Temperature";
			// 
			// temperatureRightLabel
			// 
			this.temperatureRightLabel.AutoSize = true;
			this.temperatureRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.temperatureRightLabel.Location = new System.Drawing.Point(470, 20);
			this.temperatureRightLabel.Name = "temperatureRightLabel";
			this.temperatureRightLabel.Size = new System.Drawing.Size(20, 18);
			this.temperatureRightLabel.TabIndex = 1;
			this.temperatureRightLabel.Text = "...";
			// 
			// temperatureLeftLabel
			// 
			this.temperatureLeftLabel.AutoSize = true;
			this.temperatureLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.temperatureLeftLabel.Location = new System.Drawing.Point(241, 20);
			this.temperatureLeftLabel.Name = "temperatureLeftLabel";
			this.temperatureLeftLabel.Size = new System.Drawing.Size(20, 18);
			this.temperatureLeftLabel.TabIndex = 0;
			this.temperatureLeftLabel.Text = "...";
			// 
			// cloudCoverGroupBox
			// 
			this.cloudCoverGroupBox.Controls.Add(this.cloudCoverRightLabel);
			this.cloudCoverGroupBox.Controls.Add(this.cloudCoverLeftLabel);
			this.cloudCoverGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cloudCoverGroupBox.Location = new System.Drawing.Point(37, 130);
			this.cloudCoverGroupBox.Name = "cloudCoverGroupBox";
			this.cloudCoverGroupBox.Size = new System.Drawing.Size(722, 52);
			this.cloudCoverGroupBox.TabIndex = 2;
			this.cloudCoverGroupBox.TabStop = false;
			this.cloudCoverGroupBox.Text = "Cloud Cover";
			// 
			// cloudCoverRightLabel
			// 
			this.cloudCoverRightLabel.AutoSize = true;
			this.cloudCoverRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cloudCoverRightLabel.Location = new System.Drawing.Point(470, 20);
			this.cloudCoverRightLabel.Name = "cloudCoverRightLabel";
			this.cloudCoverRightLabel.Size = new System.Drawing.Size(20, 18);
			this.cloudCoverRightLabel.TabIndex = 1;
			this.cloudCoverRightLabel.Text = "...";
			// 
			// cloudCoverLeftLabel
			// 
			this.cloudCoverLeftLabel.AutoSize = true;
			this.cloudCoverLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cloudCoverLeftLabel.Location = new System.Drawing.Point(241, 20);
			this.cloudCoverLeftLabel.Name = "cloudCoverLeftLabel";
			this.cloudCoverLeftLabel.Size = new System.Drawing.Size(20, 18);
			this.cloudCoverLeftLabel.TabIndex = 0;
			this.cloudCoverLeftLabel.Text = "...";
			// 
			// humidityGroupBox
			// 
			this.humidityGroupBox.Controls.Add(this.humidityRightLabel);
			this.humidityGroupBox.Controls.Add(this.humidityLeftLabel);
			this.humidityGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.humidityGroupBox.Location = new System.Drawing.Point(37, 188);
			this.humidityGroupBox.Name = "humidityGroupBox";
			this.humidityGroupBox.Size = new System.Drawing.Size(722, 52);
			this.humidityGroupBox.TabIndex = 2;
			this.humidityGroupBox.TabStop = false;
			this.humidityGroupBox.Text = "Humidity";
			// 
			// humidityRightLabel
			// 
			this.humidityRightLabel.AutoSize = true;
			this.humidityRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.humidityRightLabel.Location = new System.Drawing.Point(470, 20);
			this.humidityRightLabel.Name = "humidityRightLabel";
			this.humidityRightLabel.Size = new System.Drawing.Size(20, 18);
			this.humidityRightLabel.TabIndex = 1;
			this.humidityRightLabel.Text = "...";
			// 
			// humidityLeftLabel
			// 
			this.humidityLeftLabel.AutoSize = true;
			this.humidityLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.humidityLeftLabel.Location = new System.Drawing.Point(241, 20);
			this.humidityLeftLabel.Name = "humidityLeftLabel";
			this.humidityLeftLabel.Size = new System.Drawing.Size(20, 18);
			this.humidityLeftLabel.TabIndex = 0;
			this.humidityLeftLabel.Text = "...";
			// 
			// precipitationRightLabel
			// 
			this.precipitationRightLabel.AutoSize = true;
			this.precipitationRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.precipitationRightLabel.Location = new System.Drawing.Point(470, 20);
			this.precipitationRightLabel.Name = "precipitationRightLabel";
			this.precipitationRightLabel.Size = new System.Drawing.Size(20, 18);
			this.precipitationRightLabel.TabIndex = 1;
			this.precipitationRightLabel.Text = "...";
			// 
			// precipitationLeftLabel
			// 
			this.precipitationLeftLabel.AutoSize = true;
			this.precipitationLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.precipitationLeftLabel.Location = new System.Drawing.Point(241, 20);
			this.precipitationLeftLabel.Name = "precipitationLeftLabel";
			this.precipitationLeftLabel.Size = new System.Drawing.Size(20, 18);
			this.precipitationLeftLabel.TabIndex = 0;
			this.precipitationLeftLabel.Text = "...";
			// 
			// precipitationGroupBox
			// 
			this.precipitationGroupBox.Controls.Add(this.precipitationRightLabel);
			this.precipitationGroupBox.Controls.Add(this.precipitationLeftLabel);
			this.precipitationGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.precipitationGroupBox.Location = new System.Drawing.Point(37, 246);
			this.precipitationGroupBox.Name = "precipitationGroupBox";
			this.precipitationGroupBox.Size = new System.Drawing.Size(722, 52);
			this.precipitationGroupBox.TabIndex = 3;
			this.precipitationGroupBox.TabStop = false;
			this.precipitationGroupBox.Text = "Precipitation";
			// 
			// windSpeedRightLabel
			// 
			this.windSpeedRightLabel.AutoSize = true;
			this.windSpeedRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.windSpeedRightLabel.Location = new System.Drawing.Point(470, 20);
			this.windSpeedRightLabel.Name = "windSpeedRightLabel";
			this.windSpeedRightLabel.Size = new System.Drawing.Size(20, 18);
			this.windSpeedRightLabel.TabIndex = 1;
			this.windSpeedRightLabel.Text = "...";
			// 
			// windSpeedLeftLabel
			// 
			this.windSpeedLeftLabel.AutoSize = true;
			this.windSpeedLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.windSpeedLeftLabel.Location = new System.Drawing.Point(241, 20);
			this.windSpeedLeftLabel.Name = "windSpeedLeftLabel";
			this.windSpeedLeftLabel.Size = new System.Drawing.Size(20, 18);
			this.windSpeedLeftLabel.TabIndex = 0;
			this.windSpeedLeftLabel.Text = "...";
			// 
			// windSpeedGroupBox
			// 
			this.windSpeedGroupBox.Controls.Add(this.windSpeedRightLabel);
			this.windSpeedGroupBox.Controls.Add(this.windSpeedLeftLabel);
			this.windSpeedGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.windSpeedGroupBox.Location = new System.Drawing.Point(37, 304);
			this.windSpeedGroupBox.Name = "windSpeedGroupBox";
			this.windSpeedGroupBox.Size = new System.Drawing.Size(722, 52);
			this.windSpeedGroupBox.TabIndex = 4;
			this.windSpeedGroupBox.TabStop = false;
			this.windSpeedGroupBox.Text = "Wind Speed";
			// 
			// windDirectionRightLabel
			// 
			this.windDirectionRightLabel.AutoSize = true;
			this.windDirectionRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.windDirectionRightLabel.Location = new System.Drawing.Point(470, 20);
			this.windDirectionRightLabel.Name = "windDirectionRightLabel";
			this.windDirectionRightLabel.Size = new System.Drawing.Size(20, 18);
			this.windDirectionRightLabel.TabIndex = 1;
			this.windDirectionRightLabel.Text = "...";
			// 
			// windDirectionLeftLabel
			// 
			this.windDirectionLeftLabel.AutoSize = true;
			this.windDirectionLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.windDirectionLeftLabel.Location = new System.Drawing.Point(241, 20);
			this.windDirectionLeftLabel.Name = "windDirectionLeftLabel";
			this.windDirectionLeftLabel.Size = new System.Drawing.Size(20, 18);
			this.windDirectionLeftLabel.TabIndex = 0;
			this.windDirectionLeftLabel.Text = "...";
			// 
			// windDirectionGroupBox
			// 
			this.windDirectionGroupBox.Controls.Add(this.windDirectionRightLabel);
			this.windDirectionGroupBox.Controls.Add(this.windDirectionLeftLabel);
			this.windDirectionGroupBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.windDirectionGroupBox.Location = new System.Drawing.Point(37, 362);
			this.windDirectionGroupBox.Name = "windDirectionGroupBox";
			this.windDirectionGroupBox.Size = new System.Drawing.Size(722, 52);
			this.windDirectionGroupBox.TabIndex = 5;
			this.windDirectionGroupBox.TabStop = false;
			this.windDirectionGroupBox.Text = "Wind Direction";
			// 
			// refreshPictureBox
			// 
			this.refreshPictureBox.Image = global::WeatherWinForms.Properties.Resources.refresh;
			this.refreshPictureBox.Location = new System.Drawing.Point(368, 31);
			this.refreshPictureBox.Name = "refreshPictureBox";
			this.refreshPictureBox.Size = new System.Drawing.Size(25, 25);
			this.refreshPictureBox.TabIndex = 6;
			this.refreshPictureBox.TabStop = false;
			this.refreshPictureBox.Click += new System.EventHandler(this.RefreshDataAfterPictureBoxClick);
			// 
			// cellarLeftLabel
			// 
			this.cellarLeftLabel.AutoSize = true;
			this.cellarLeftLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cellarLeftLabel.Location = new System.Drawing.Point(280, 417);
			this.cellarLeftLabel.Name = "cellarLeftLabel";
			this.cellarLeftLabel.Size = new System.Drawing.Size(16, 13);
			this.cellarLeftLabel.TabIndex = 7;
			this.cellarLeftLabel.Text = "...";
			this.cellarLeftLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cellarRightLabel
			// 
			this.cellarRightLabel.AutoSize = true;
			this.cellarRightLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.cellarRightLabel.Location = new System.Drawing.Point(509, 417);
			this.cellarRightLabel.Name = "cellarRightLabel";
			this.cellarRightLabel.Size = new System.Drawing.Size(16, 13);
			this.cellarRightLabel.TabIndex = 8;
			this.cellarRightLabel.Text = "...";
			this.cellarRightLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 466);
			this.Controls.Add(this.cellarRightLabel);
			this.Controls.Add(this.cellarLeftLabel);
			this.Controls.Add(this.refreshPictureBox);
			this.Controls.Add(this.windDirectionGroupBox);
			this.Controls.Add(this.windSpeedGroupBox);
			this.Controls.Add(this.precipitationGroupBox);
			this.Controls.Add(this.humidityGroupBox);
			this.Controls.Add(this.cloudCoverGroupBox);
			this.Controls.Add(this.temperatureGroupBox);
			this.Controls.Add(this.headerLabel);
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.temperatureGroupBox.ResumeLayout(false);
			this.temperatureGroupBox.PerformLayout();
			this.cloudCoverGroupBox.ResumeLayout(false);
			this.cloudCoverGroupBox.PerformLayout();
			this.humidityGroupBox.ResumeLayout(false);
			this.humidityGroupBox.PerformLayout();
			this.precipitationGroupBox.ResumeLayout(false);
			this.precipitationGroupBox.PerformLayout();
			this.windSpeedGroupBox.ResumeLayout(false);
			this.windSpeedGroupBox.PerformLayout();
			this.windDirectionGroupBox.ResumeLayout(false);
			this.windDirectionGroupBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.refreshPictureBox)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label headerLabel;
		private System.Windows.Forms.GroupBox temperatureGroupBox;
		private System.Windows.Forms.Label temperatureRightLabel;
		private System.Windows.Forms.Label temperatureLeftLabel;
		private System.Windows.Forms.GroupBox cloudCoverGroupBox;
		private System.Windows.Forms.Label cloudCoverRightLabel;
		private System.Windows.Forms.Label cloudCoverLeftLabel;
		private System.Windows.Forms.GroupBox humidityGroupBox;
		private System.Windows.Forms.Label humidityRightLabel;
		private System.Windows.Forms.Label humidityLeftLabel;
		private System.Windows.Forms.Label precipitationRightLabel;
		private System.Windows.Forms.Label precipitationLeftLabel;
		private System.Windows.Forms.GroupBox precipitationGroupBox;
		private System.Windows.Forms.Label windSpeedRightLabel;
		private System.Windows.Forms.Label windSpeedLeftLabel;
		private System.Windows.Forms.GroupBox windSpeedGroupBox;
		private System.Windows.Forms.Label windDirectionRightLabel;
		private System.Windows.Forms.Label windDirectionLeftLabel;
		private System.Windows.Forms.GroupBox windDirectionGroupBox;
		private System.Windows.Forms.PictureBox refreshPictureBox;
		private System.Windows.Forms.Label cellarLeftLabel;
		private System.Windows.Forms.Label cellarRightLabel;
	}
}

