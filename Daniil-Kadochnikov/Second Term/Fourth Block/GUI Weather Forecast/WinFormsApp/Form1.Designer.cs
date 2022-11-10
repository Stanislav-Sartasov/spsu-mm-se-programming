namespace WinFormsApp
{
	partial class Form1
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.label1 = new System.Windows.Forms.Label();
			this.TiltleLabel = new System.Windows.Forms.Label();
			this.UpdateBothButton = new System.Windows.Forms.Button();
			this.ExitButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.tomorrowIoInfoLabel = new System.Windows.Forms.Label();
			this.stormGlassIoInfoLabel = new System.Windows.Forms.Label();
			this.tomorrowIoLabel = new System.Windows.Forms.Label();
			this.UpdateTomorrowIoButton = new System.Windows.Forms.Button();
			this.stormGlassIoLabel = new System.Windows.Forms.Label();
			this.UpdateStormGlassIoButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// TiltleLabel
			// 
			resources.ApplyResources(this.TiltleLabel, "TiltleLabel");
			this.TiltleLabel.Name = "TiltleLabel";
			// 
			// UpdateBothButton
			// 
			resources.ApplyResources(this.UpdateBothButton, "UpdateBothButton");
			this.UpdateBothButton.Name = "UpdateBothButton";
			this.UpdateBothButton.UseVisualStyleBackColor = true;
			this.UpdateBothButton.Click += new System.EventHandler(this.UpdateBothClick);
			// 
			// ExitButton
			// 
			resources.ApplyResources(this.ExitButton, "ExitButton");
			this.ExitButton.Name = "ExitButton";
			this.ExitButton.UseVisualStyleBackColor = true;
			this.ExitButton.Click += new System.EventHandler(this.ExitButtonClick);
			// 
			// tableLayoutPanel1
			// 
			resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
			this.tableLayoutPanel1.Controls.Add(this.tomorrowIoInfoLabel, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.stormGlassIoInfoLabel, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.tomorrowIoLabel, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.UpdateTomorrowIoButton, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.stormGlassIoLabel, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.UpdateStormGlassIoButton, 2, 2);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			// 
			// tomorrowIoInfoLabel
			// 
			resources.ApplyResources(this.tomorrowIoInfoLabel, "tomorrowIoInfoLabel");
			this.tomorrowIoInfoLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(214)))));
			this.tomorrowIoInfoLabel.Name = "tomorrowIoInfoLabel";
			// 
			// stormGlassIoInfoLabel
			// 
			resources.ApplyResources(this.stormGlassIoInfoLabel, "stormGlassIoInfoLabel");
			this.stormGlassIoInfoLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(214)))));
			this.stormGlassIoInfoLabel.Name = "stormGlassIoInfoLabel";
			// 
			// tomorrowIoLabel
			// 
			resources.ApplyResources(this.tomorrowIoLabel, "tomorrowIoLabel");
			this.tomorrowIoLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(189)))));
			this.tomorrowIoLabel.Name = "tomorrowIoLabel";
			// 
			// UpdateTomorrowIoButton
			// 
			resources.ApplyResources(this.UpdateTomorrowIoButton, "UpdateTomorrowIoButton");
			this.UpdateTomorrowIoButton.Name = "UpdateTomorrowIoButton";
			this.UpdateTomorrowIoButton.UseVisualStyleBackColor = true;
			this.UpdateTomorrowIoButton.Click += new System.EventHandler(this.UpdateTomorrowIoClickAsync);
			// 
			// stormGlassIoLabel
			// 
			resources.ApplyResources(this.stormGlassIoLabel, "stormGlassIoLabel");
			this.stormGlassIoLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(189)))));
			this.stormGlassIoLabel.Name = "stormGlassIoLabel";
			// 
			// UpdateStormGlassIoButton
			// 
			resources.ApplyResources(this.UpdateStormGlassIoButton, "UpdateStormGlassIoButton");
			this.UpdateStormGlassIoButton.Name = "UpdateStormGlassIoButton";
			this.UpdateStormGlassIoButton.UseVisualStyleBackColor = true;
			this.UpdateStormGlassIoButton.Click += new System.EventHandler(this.UpdateStormGlassIoClickAsync);
			// 
			// Form1
			// 
			this.AcceptButton = this.UpdateBothButton;
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.ExitButton;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.ExitButton);
			this.Controls.Add(this.UpdateBothButton);
			this.Controls.Add(this.TiltleLabel);
			this.Name = "Form1";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private Label label1;
		private Label TiltleLabel;
		private Button UpdateBothButton;
		private Button ExitButton;
		private TableLayoutPanel tableLayoutPanel1;
		private Label stormGlassIoLabel;
		private Label tomorrowIoInfoLabel;
		private Label stormGlassIoInfoLabel;
		private Label tomorrowIoLabel;
		private Button UpdateTomorrowIoButton;
		private Button UpdateStormGlassIoButton;
	}
}