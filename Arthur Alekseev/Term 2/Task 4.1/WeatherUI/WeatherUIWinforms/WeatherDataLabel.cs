using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherUI.Weather;

namespace WeatherUIWinforms
{
	internal class WeatherDataLabel
	{
		List<Label> labels;
		string innerData = "No data availible";
		Panel backGroundPanel;
		int positionStride = 40;

		public WeatherDataLabel(Panel panel)
		{
			labels = new List<Label>();
			backGroundPanel = panel;
			CreateLabels();
		}

		private void CreateLabels()
		{
			foreach (var data in innerData.Split("\n"))
			{
				labels.Add(new Label());
				labels.Last().Text = data;
				labels.Last().Size = new Size(300, 30);
				labels.Last().Font = new System.Drawing.Font("Tahoma", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
				labels.Last().Location = new Point(labels.Last().Location.X, labels.Last().Location.Y + labels.Count() * positionStride);
				backGroundPanel.Controls.Add(labels.Last());
			}
		}

		public void LoadWeatherData(WeatherData? data) 
		{
			if (data != null)
				innerData = data.ToString();
			else
				innerData = "No Data Available";

			labels.Clear();
			backGroundPanel.Controls.Clear();

			CreateLabels();
		}
	}
}
