namespace WinForms
{
	partial class Form
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
			this.refresh = new System.Windows.Forms.Button();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// refresh
			// 
			this.refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.refresh.Location = new System.Drawing.Point(332, 214);
			this.refresh.Name = "refresh";
			this.refresh.Size = new System.Drawing.Size(85, 33);
			this.refresh.TabIndex = 1;
			this.refresh.Text = "Refresh";
			this.refresh.UseVisualStyleBackColor = true;
			this.refresh.Click += new System.EventHandler(this.refresh_Click);
			// 
			// checkBox1
			// 
			this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(8, 203);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(93, 19);
			this.checkBox1.TabIndex = 2;
			this.checkBox1.Text = "Tomorrow.io";
			this.checkBox1.UseVisualStyleBackColor = true;
			this.checkBox1.CheckedChanged += new System.EventHandler(this.TomorrowIoCheckBox_CheckedChanged);
			// 
			// checkBox2
			// 
			this.checkBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(8, 228);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(97, 19);
			this.checkBox2.TabIndex = 3;
			this.checkBox2.Text = "Stormglass.io";
			this.checkBox2.UseVisualStyleBackColor = true;
			this.checkBox2.CheckedChanged += new System.EventHandler(this.StormglassIoCheckBox_CheckedChanged);
			// 
			// textBox1
			// 
			this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.Location = new System.Drawing.Point(58, 12);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(316, 20);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "Warning: stormglass.io has a limit of 10 requests per day";
			// 
			// Form
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(429, 257);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.checkBox2);
			this.Controls.Add(this.checkBox1);
			this.Controls.Add(this.refresh);
			this.Name = "Form";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Weather Forecast";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button refresh;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.TextBox textBox1;
	}
}

