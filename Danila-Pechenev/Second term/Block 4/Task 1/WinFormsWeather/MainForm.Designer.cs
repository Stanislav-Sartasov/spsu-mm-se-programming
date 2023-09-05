namespace WinFormsWeather;

partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pictureSpb = new System.Windows.Forms.PictureBox();
            this.usedServiceBox = new System.Windows.Forms.ComboBox();
            this.topPanel = new System.Windows.Forms.Panel();
            this.header = new System.Windows.Forms.Label();
            this.usedServiceLabel = new System.Windows.Forms.Label();
            this.updateWeatherButton = new System.Windows.Forms.Button();
            this.weatherTextBox = new System.Windows.Forms.RichTextBox();
            this.errorTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureSpb)).BeginInit();
            this.topPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureSpb
            // 
            this.pictureSpb.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pictureSpb.Image = global::WinFormsWeather.Properties.Resources.spb;
            this.pictureSpb.Location = new System.Drawing.Point(0, 403);
            this.pictureSpb.Name = "pictureSpb";
            this.pictureSpb.Size = new System.Drawing.Size(1000, 275);
            this.pictureSpb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureSpb.TabIndex = 0;
            this.pictureSpb.TabStop = false;
            // 
            // usedServiceBox
            // 
            this.usedServiceBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.usedServiceBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.usedServiceBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.usedServiceBox.Font = new System.Drawing.Font("Lucida Sans", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.usedServiceBox.Items.AddRange(new object[] {
            "TomorrowIo",
            "OpenWeatherMap"});
            this.usedServiceBox.Location = new System.Drawing.Point(39, 146);
            this.usedServiceBox.Name = "usedServiceBox";
            this.usedServiceBox.Size = new System.Drawing.Size(286, 40);
            this.usedServiceBox.TabIndex = 1;
            this.usedServiceBox.Text = "TomorrowIo";
            this.usedServiceBox.SelectedIndexChanged += new System.EventHandler(this.usedServiceBox_SelectedIndexChanged);
            this.usedServiceBox.TextUpdate += new System.EventHandler(this.usedServiceBox_TextUpdate);
            // 
            // topPanel
            // 
            this.topPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(169)))), ((int)(((byte)(224)))));
            this.topPanel.Controls.Add(this.header);
            this.topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.topPanel.Location = new System.Drawing.Point(0, 0);
            this.topPanel.Name = "topPanel";
            this.topPanel.Size = new System.Drawing.Size(1000, 70);
            this.topPanel.TabIndex = 2;
            // 
            // header
            // 
            this.header.Dock = System.Windows.Forms.DockStyle.Fill;
            this.header.Font = new System.Drawing.Font("Lucida Sans", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.header.ForeColor = System.Drawing.SystemColors.Window;
            this.header.Location = new System.Drawing.Point(0, 0);
            this.header.Name = "header";
            this.header.Size = new System.Drawing.Size(1000, 70);
            this.header.TabIndex = 0;
            this.header.Text = "Weather in St. Petersburg";
            this.header.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // usedServiceLabel
            // 
            this.usedServiceLabel.AutoSize = true;
            this.usedServiceLabel.Font = new System.Drawing.Font("Lucida Sans", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.usedServiceLabel.Location = new System.Drawing.Point(39, 111);
            this.usedServiceLabel.Name = "usedServiceLabel";
            this.usedServiceLabel.Size = new System.Drawing.Size(286, 32);
            this.usedServiceLabel.TabIndex = 3;
            this.usedServiceLabel.Text = "Choose data source:";
            // 
            // updateWeatherButton
            // 
            this.updateWeatherButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(169)))), ((int)(((byte)(224)))));
            this.updateWeatherButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.updateWeatherButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateWeatherButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.updateWeatherButton.FlatAppearance.BorderSize = 2;
            this.updateWeatherButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.updateWeatherButton.Font = new System.Drawing.Font("Lucida Sans", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.updateWeatherButton.ForeColor = System.Drawing.SystemColors.Window;
            this.updateWeatherButton.Location = new System.Drawing.Point(39, 341);
            this.updateWeatherButton.Name = "updateWeatherButton";
            this.updateWeatherButton.Size = new System.Drawing.Size(331, 63);
            this.updateWeatherButton.TabIndex = 4;
            this.updateWeatherButton.Text = "Update data!";
            this.updateWeatherButton.UseVisualStyleBackColor = false;
            this.updateWeatherButton.Click += new System.EventHandler(this.updateWeatherButton_Click);
            // 
            // weatherTextBox
            // 
            this.weatherTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.weatherTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.weatherTextBox.Font = new System.Drawing.Font("Lucida Sans", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.weatherTextBox.Location = new System.Drawing.Point(523, 111);
            this.weatherTextBox.Name = "weatherTextBox";
            this.weatherTextBox.ReadOnly = true;
            this.weatherTextBox.Size = new System.Drawing.Size(422, 328);
            this.weatherTextBox.TabIndex = 5;
            this.weatherTextBox.Text = "";
            // 
            // errorTextBox
            // 
            this.errorTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.errorTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.errorTextBox.Font = new System.Drawing.Font("Lucida Sans", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.errorTextBox.ForeColor = System.Drawing.Color.Red;
            this.errorTextBox.Location = new System.Drawing.Point(39, 261);
            this.errorTextBox.Name = "errorTextBox";
            this.errorTextBox.ReadOnly = true;
            this.errorTextBox.Size = new System.Drawing.Size(331, 74);
            this.errorTextBox.TabIndex = 6;
            this.errorTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(1000, 678);
            this.Controls.Add(this.errorTextBox);
            this.Controls.Add(this.weatherTextBox);
            this.Controls.Add(this.updateWeatherButton);
            this.Controls.Add(this.usedServiceLabel);
            this.Controls.Add(this.topPanel);
            this.Controls.Add(this.usedServiceBox);
            this.Controls.Add(this.pictureSpb);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Weather in St. Petersburg";
            ((System.ComponentModel.ISupportInitialize)(this.pictureSpb)).EndInit();
            this.topPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private PictureBox pictureSpb;
    private ComboBox usedServiceBox;
    private Panel topPanel;
    private Label header;
    private Label usedServiceLabel;
    private Button updateWeatherButton;
    private RichTextBox weatherTextBox;
    private RichTextBox errorTextBox;
}
