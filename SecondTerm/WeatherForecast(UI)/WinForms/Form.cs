using Sites;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinForms
{
	public partial class Form : System.Windows.Forms.Form
	{
		private (bool, bool) sites;
		Dictionary<int, TextBox> tITextBoxes = new Dictionary<int, TextBox>();
		Dictionary<int, TextBox> sITextBoxes = new Dictionary<int, TextBox>();

		public Form()
		{
			InitializeComponent();
		}

		private void FormLoad(object sender, EventArgs e)
		{
			tITextBoxes.Add(0, tITempC);
			tITextBoxes.Add(1, tITempF);
			tITextBoxes.Add(2, tIPrecipitationIntensity);
			tITextBoxes.Add(3, tIHumidity);
			tITextBoxes.Add(4, tICloudcover);
			tITextBoxes.Add(5, tIWindDirection);
			tITextBoxes.Add(6, tIWindSpeed);
			sITextBoxes.Add(0, sITempC);
			sITextBoxes.Add(1, sITempF);
			sITextBoxes.Add(2, sIPrecipitationIntensity);
			sITextBoxes.Add(3, sIHumidity);
			sITextBoxes.Add(4, sICloudcover);
			sITextBoxes.Add(5, sIWindDirection);
			sITextBoxes.Add(6, sIWindSpeed);
			MessageBox.Show("This program output weather forecast\nSelect a site and click on the \"Refresh\" button to get update information");
		}

		private void RefreshClick(object sender, EventArgs e)
		{
			if (sites.Item1)
			{
				string[] weather = new TomorrowIo().ShowWeather().Split("\n") ;
				if (weather[0] != "Inactive")
				{
					for (int i = 0; i < 7; i++)
					{
						tITextBoxes[i].Clear();
						tITextBoxes[i].AppendText(weather[i]);
					}
				}
				else
				{
					MessageBox.Show("Stromglass.io is inactive");
				}
			}
			if (sites.Item2)
			{
				string[] weather = new StormglassIo().ShowWeather().Split("\n");
				if (weather[0] != "Inactive")
				{
					for (int i = 0; i < 7; i++)
					{
						sITextBoxes[i].Clear();
						sITextBoxes[i].AppendText(weather[i]);
					}
				}
				else
				{
					MessageBox.Show("Stromglass.io is inactive");
				}
			}
			if (!(sites.Item1 || sites.Item2))
			{
				MessageBox.Show("Сhoose at least 1 site");
			}
		}

		private void TomorrowIoCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			sites.Item1 = !sites.Item1;
		}

		private void StormglassIoCheckBoxCheckedChanged(object sender, EventArgs e)
		{
			sites.Item2 = !sites.Item2;
		}

		private void ClearButtonClick(object sender, EventArgs e)
		{
			if (sites.Item1)
			{
				for (int i = 0; i < 7; i++)
				{
					tITextBoxes[i].Clear();
				}
			}
			if (sites.Item2)
			{
				for (int i = 0; i < 7; i++)
				{
					sITextBoxes[i].Clear();
				}
			}
		}
	}
}
