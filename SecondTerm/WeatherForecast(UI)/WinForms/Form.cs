using Sites;
using System;
using System.Windows.Forms;

namespace WinForms
{
	public partial class Form : System.Windows.Forms.Form
	{

		private (bool, bool) sites;

		public Form()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			MessageBox.Show("This program output weather forecast\nSelect a site and click on the \"Refresh\" button to get up-to-date information");
		}

		private void refresh_Click(object sender, EventArgs e)
		{
			if (sites.Item1)
			{
				MessageBox.Show($"Tomorrow.io:\n\n{new TomorrowIo().ShowWeather()}");
			}
			if (sites.Item2)
			{
				MessageBox.Show($"Stomglass.io:\n\n{new StormglassIo().ShowWeather()}");
			}
			if (!(sites.Item1 || sites.Item2))
			{
				MessageBox.Show("Сhoose at least 1 site");
			}
		}

		private void TomorrowIoCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (sites.Item1)
				sites.Item1 = false;
			else sites.Item1 = true;
		}

		private void StormglassIoCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if (sites.Item2)
				sites.Item2 = false;
			else sites.Item2 = true;
		}
	}
}
