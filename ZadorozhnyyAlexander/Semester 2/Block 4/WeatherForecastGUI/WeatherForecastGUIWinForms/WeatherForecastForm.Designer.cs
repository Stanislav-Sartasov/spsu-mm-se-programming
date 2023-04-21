namespace WeatherForecastGUIWinForms
{
    partial class WeatherForecastForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WeatherForecastForm));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.modelSource = new System.Windows.Forms.BindingSource(this.components);
            this.UpdateButton = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.SwitchButton = new System.Windows.Forms.Button();
            this.DataLabel = new System.Windows.Forms.Label();
            this.ActivityButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.modelSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.BackColor = System.Drawing.Color.DeepPink;
            this.DescriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DescriptionLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modelSource, "Description", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DescriptionLabel.Font = new System.Drawing.Font("Lucida Handwriting", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.DescriptionLabel.ForeColor = System.Drawing.Color.GhostWhite;
            this.DescriptionLabel.Location = new System.Drawing.Point(113, 30);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(363, 45);
            this.DescriptionLabel.TabIndex = 0;
            this.DescriptionLabel.Text = "Weather from site";
            // 
            // modelSource
            // 
            this.modelSource.DataSource = typeof(WeatherForecastModelGUI.WeatherForecastModel);
            // 
            // UpdateButton
            // 
            this.UpdateButton.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.UpdateButton.BackgroundImage = global::WeatherForecastGUIWinForms.Properties.Resources.button2;
            this.UpdateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.UpdateButton.DataBindings.Add(new System.Windows.Forms.Binding("Enabled", this.modelSource, "IsUpdateEnabled", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.UpdateButton.Font = new System.Drawing.Font("Script MT Bold", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UpdateButton.Location = new System.Drawing.Point(694, 105);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(298, 65);
            this.UpdateButton.TabIndex = 1;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.UpdateButton.UseVisualStyleBackColor = false;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // SwitchButton
            // 
            this.SwitchButton.BackgroundImage = global::WeatherForecastGUIWinForms.Properties.Resources.button2;
            this.SwitchButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.SwitchButton.Font = new System.Drawing.Font("Script MT Bold", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SwitchButton.Location = new System.Drawing.Point(694, 195);
            this.SwitchButton.Name = "SwitchButton";
            this.SwitchButton.Size = new System.Drawing.Size(298, 65);
            this.SwitchButton.TabIndex = 2;
            this.SwitchButton.Text = "Switch";
            this.SwitchButton.UseVisualStyleBackColor = true;
            this.SwitchButton.Click += new System.EventHandler(this.SwitchButton_Click);
            // 
            // DataLabel
            // 
            this.DataLabel.AutoSize = true;
            this.DataLabel.BackColor = System.Drawing.Color.DeepPink;
            this.DataLabel.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modelSource, "data", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.DataLabel.Font = new System.Drawing.Font("Lucida Handwriting", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.DataLabel.ForeColor = System.Drawing.Color.GhostWhite;
            this.DataLabel.Location = new System.Drawing.Point(39, 105);
            this.DataLabel.Name = "DataLabel";
            this.DataLabel.Size = new System.Drawing.Size(126, 31);
            this.DataLabel.TabIndex = 3;
            this.DataLabel.Text = "No data";
            // 
            // ActivityButton
            // 
            this.ActivityButton.BackgroundImage = global::WeatherForecastGUIWinForms.Properties.Resources.button2;
            this.ActivityButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ActivityButton.Font = new System.Drawing.Font("Script MT Bold", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ActivityButton.Location = new System.Drawing.Point(694, 285);
            this.ActivityButton.Name = "ActivityButton";
            this.ActivityButton.Size = new System.Drawing.Size(298, 65);
            this.ActivityButton.TabIndex = 17;
            this.ActivityButton.Text = "Turn off/on";
            this.ActivityButton.UseVisualStyleBackColor = true;
            this.ActivityButton.Click += new System.EventHandler(this.ActivityButton_Click);
            // 
            // WeatherForecastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.BackgroundImage = global::WeatherForecastGUIWinForms.Properties.Resources.background1;
            this.ClientSize = new System.Drawing.Size(1020, 574);
            this.Controls.Add(this.ActivityButton);
            this.Controls.Add(this.DataLabel);
            this.Controls.Add(this.SwitchButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.DescriptionLabel);
            this.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.modelSource, "ErrorMessage", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "WeatherForecastForm";
            this.Text = "Weather Forecast";
            ((System.ComponentModel.ISupportInitialize)(this.modelSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Label DescriptionLabel;
        private Button UpdateButton;
        private ErrorProvider ErrorProvider;
        private Button SwitchButton;
        private Label DataLabel;
        private Button ActivityButton;
        private BindingSource modelSource;
    }
}