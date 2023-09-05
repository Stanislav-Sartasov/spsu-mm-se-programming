namespace WeatherUIWinforms
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.buttonPanel = new System.Windows.Forms.Panel();
			this.refreshButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.tioPanel = new System.Windows.Forms.Panel();
			this.owmPanel = new System.Windows.Forms.Panel();
			this.buttonPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonPanel
			// 
			this.buttonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(48)))));
			this.buttonPanel.Controls.Add(this.refreshButton);
			this.buttonPanel.Controls.Add(this.exitButton);
			this.buttonPanel.Location = new System.Drawing.Point(-1, 0);
			this.buttonPanel.Name = "buttonPanel";
			this.buttonPanel.Size = new System.Drawing.Size(80, 451);
			this.buttonPanel.TabIndex = 0;
			// 
			// refreshButton
			// 
			this.refreshButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(78)))));
			this.refreshButton.FlatAppearance.BorderSize = 0;
			this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.refreshButton.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.refreshButton.ForeColor = System.Drawing.Color.Azure;
			this.refreshButton.Image = ((System.Drawing.Image)(resources.GetObject("refreshButton.Image")));
			this.refreshButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.refreshButton.Location = new System.Drawing.Point(3, 3);
			this.refreshButton.Name = "refreshButton";
			this.refreshButton.Size = new System.Drawing.Size(74, 82);
			this.refreshButton.TabIndex = 2;
			this.refreshButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.refreshButton.UseVisualStyleBackColor = false;
			this.refreshButton.Click += new System.EventHandler(this.RefreshButtonClick);
			// 
			// exitButton
			// 
			this.exitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(78)))));
			this.exitButton.FlatAppearance.BorderSize = 0;
			this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.exitButton.Font = new System.Drawing.Font("Tahoma", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.exitButton.ForeColor = System.Drawing.Color.Azure;
			this.exitButton.Image = ((System.Drawing.Image)(resources.GetObject("exitButton.Image")));
			this.exitButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.exitButton.Location = new System.Drawing.Point(3, 91);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(74, 75);
			this.exitButton.TabIndex = 1;
			this.exitButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.exitButton.UseVisualStyleBackColor = false;
			this.exitButton.Click += new System.EventHandler(this.ExitButtonClick);
			// 
			// tioPanel
			// 
			this.tioPanel.BackColor = System.Drawing.Color.Snow;
			this.tioPanel.Location = new System.Drawing.Point(79, 0);
			this.tioPanel.Margin = new System.Windows.Forms.Padding(0);
			this.tioPanel.Name = "tioPanel";
			this.tioPanel.Size = new System.Drawing.Size(318, 451);
			this.tioPanel.TabIndex = 1;
			// 
			// owmPanel
			// 
			this.owmPanel.Location = new System.Drawing.Point(397, 0);
			this.owmPanel.Margin = new System.Windows.Forms.Padding(0);
			this.owmPanel.Name = "owmPanel";
			this.owmPanel.Size = new System.Drawing.Size(326, 451);
			this.owmPanel.TabIndex = 2;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(722, 450);
			this.Controls.Add(this.owmPanel);
			this.Controls.Add(this.tioPanel);
			this.Controls.Add(this.buttonPanel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "Weather";
			this.buttonPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Panel buttonPanel;
		private Button exitButton;
		private Button refreshButton;
		private Panel tioPanel;
		private Panel owmPanel;
	}
}