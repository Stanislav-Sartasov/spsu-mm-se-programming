
namespace WeatherWinForm
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.Set = new System.Windows.Forms.Button();
            this.ServiceBox = new System.Windows.Forms.ComboBox();
            this.KeyBox = new System.Windows.Forms.TextBox();
            this.KeyLabel = new System.Windows.Forms.Label();
            this.ChooseLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Set
            // 
            this.Set.Location = new System.Drawing.Point(4, 165);
            this.Set.Name = "Set";
            this.Set.Size = new System.Drawing.Size(302, 44);
            this.Set.TabIndex = 0;
            this.Set.Text = "Save Settings";
            this.Set.UseVisualStyleBackColor = true;
            this.Set.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // ServiceBox
            // 
            this.ServiceBox.FormattingEnabled = true;
            this.ServiceBox.Location = new System.Drawing.Point(12, 12);
            this.ServiceBox.Name = "ServiceBox";
            this.ServiceBox.Size = new System.Drawing.Size(133, 28);
            this.ServiceBox.TabIndex = 1;
            // 
            // KeyBox
            // 
            this.KeyBox.Location = new System.Drawing.Point(173, 13);
            this.KeyBox.Name = "KeyBox";
            this.KeyBox.Size = new System.Drawing.Size(125, 27);
            this.KeyBox.TabIndex = 2;
            // 
            // KeyLabel
            // 
            this.KeyLabel.AutoSize = true;
            this.KeyLabel.Location = new System.Drawing.Point(173, 43);
            this.KeyLabel.Name = "KeyLabel";
            this.KeyLabel.Size = new System.Drawing.Size(107, 20);
            this.KeyLabel.TabIndex = 3;
            this.KeyLabel.Text = "Enter Key Here";
            // 
            // ChooseLabel
            // 
            this.ChooseLabel.AutoSize = true;
            this.ChooseLabel.Location = new System.Drawing.Point(12, 43);
            this.ChooseLabel.Name = "ChooseLabel";
            this.ChooseLabel.Size = new System.Drawing.Size(107, 20);
            this.ChooseLabel.TabIndex = 4;
            this.ChooseLabel.Text = "Choose service";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 218);
            this.Controls.Add(this.ChooseLabel);
            this.Controls.Add(this.KeyLabel);
            this.Controls.Add(this.KeyBox);
            this.Controls.Add(this.ServiceBox);
            this.Controls.Add(this.Set);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(328, 265);
            this.MinimumSize = new System.Drawing.Size(328, 265);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Set;
        private System.Windows.Forms.ComboBox ServiceBox;
        private System.Windows.Forms.TextBox KeyBox;
        private System.Windows.Forms.Label KeyLabel;
        private System.Windows.Forms.Label ChooseLabel;
    }
}