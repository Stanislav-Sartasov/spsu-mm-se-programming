namespace WeatherWinForms;

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
			this.refreshButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
			this.tomorrowIoTitle = new System.Windows.Forms.Label();
			this.openWeatherMapTitle = new System.Windows.Forms.Label();
			this.openWeatherMapLabel = new System.Windows.Forms.Label();
			this.tomorrowIoLabel = new System.Windows.Forms.Label();
			this.title = new System.Windows.Forms.Label();
			this.tableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// refreshButton
			// 
			this.refreshButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.refreshButton.Location = new System.Drawing.Point(0, 512);
			this.refreshButton.Margin = new System.Windows.Forms.Padding(16);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(631, 29);
			this.refreshButton.TabIndex = 0;
			this.refreshButton.Text = "Обновить данные";
			this.refreshButton.UseVisualStyleBackColor = true;
			this.refreshButton.Click += new System.EventHandler(this.OnClick);
			// 
			// tableLayoutPanel
			// 
			this.tableLayoutPanel.ColumnCount = 2;
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel.Controls.Add(this.tomorrowIoTitle, 0, 0);
			this.tableLayoutPanel.Controls.Add(this.openWeatherMapTitle, 1, 0);
			this.tableLayoutPanel.Controls.Add(this.openWeatherMapLabel, 1, 1);
			this.tableLayoutPanel.Controls.Add(this.tomorrowIoLabel, 0, 1);
			this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tableLayoutPanel.Name = "tableLayoutPanel";
			this.tableLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 41, 0, 0);
			this.tableLayoutPanel.RowCount = 2;
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel.Size = new System.Drawing.Size(631, 512);
			this.tableLayoutPanel.TabIndex = 1;
			// 
			// tomorrowIoTitle
			// 
			this.tomorrowIoTitle.AutoSize = true;
			this.tomorrowIoTitle.Location = new System.Drawing.Point(3, 41);
			this.tomorrowIoTitle.Name = "tomorrowIoTitle";
			this.tomorrowIoTitle.Padding = new System.Windows.Forms.Padding(8, 8, 8, 4);
			this.tomorrowIoTitle.Size = new System.Drawing.Size(106, 32);
			this.tomorrowIoTitle.TabIndex = 0;
			this.tomorrowIoTitle.Text = "TomorrowIo";
			// 
			// openWeatherMapTitle
			// 
			this.openWeatherMapTitle.AutoSize = true;
			this.openWeatherMapTitle.Location = new System.Drawing.Point(318, 41);
			this.openWeatherMapTitle.Name = "openWeatherMapTitle";
			this.openWeatherMapTitle.Padding = new System.Windows.Forms.Padding(8, 8, 8, 4);
			this.openWeatherMapTitle.Size = new System.Drawing.Size(146, 32);
			this.openWeatherMapTitle.TabIndex = 1;
			this.openWeatherMapTitle.Text = "OpenWeatherMap";
			// 
			// openWeatherMapLabel
			// 
			this.openWeatherMapLabel.AutoSize = true;
			this.openWeatherMapLabel.Location = new System.Drawing.Point(318, 73);
			this.openWeatherMapLabel.Name = "openWeatherMapLabel";
			this.openWeatherMapLabel.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.openWeatherMapLabel.Size = new System.Drawing.Size(94, 32);
			this.openWeatherMapLabel.TabIndex = 3;
			this.openWeatherMapLabel.Text = "Загрузка...";
			// 
			// tomorrowIoLabel
			// 
			this.tomorrowIoLabel.AutoSize = true;
			this.tomorrowIoLabel.Location = new System.Drawing.Point(3, 73);
			this.tomorrowIoLabel.Name = "tomorrowIoLabel";
			this.tomorrowIoLabel.Padding = new System.Windows.Forms.Padding(8, 4, 8, 8);
			this.tomorrowIoLabel.Size = new System.Drawing.Size(94, 32);
			this.tomorrowIoLabel.TabIndex = 4;
			this.tomorrowIoLabel.Text = "Загрузка...";
			// 
			// title
			// 
			this.title.AutoSize = true;
			this.title.Dock = System.Windows.Forms.DockStyle.Top;
			this.title.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.title.Location = new System.Drawing.Point(0, 0);
			this.title.Margin = new System.Windows.Forms.Padding(0);
			this.title.Name = "title";
			this.title.Padding = new System.Windows.Forms.Padding(11, 8, 11, 8);
			this.title.Size = new System.Drawing.Size(352, 41);
			this.title.TabIndex = 2;
			this.title.Text = "Текущая погода в Санкт-Петербурге";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(631, 541);
			this.Controls.Add(this.title);
			this.Controls.Add(this.tableLayoutPanel);
			this.Controls.Add(this.refreshButton);
			this.Name = "MainForm";
			this.Text = "Погода";
			this.tableLayoutPanel.ResumeLayout(false);
			this.tableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	#endregion

	private Button refreshButton;
	private TableLayoutPanel tableLayoutPanel;
	private Label tomorrowIoTitle;
	private Label openWeatherMapTitle;
	private Label openWeatherMapLabel;
	private Label tomorrowIoLabel;
	private Label title;
}
