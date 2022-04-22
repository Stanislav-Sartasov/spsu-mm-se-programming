using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Resources;
using System.Reflection;
using Parsers;
using StormGlass;
using WebLibrary;
using OpenWeather;
using GisMeteo;
using TomorrowIO;

namespace WeatherWinForm
{
    public partial class MainForm : Form
    {
        private ResourceManager rm = new ResourceManager("WeatherWinForm.BaseKeysSet", Assembly.GetExecutingAssembly());
        public List<JSONParser> services = new List<JSONParser>();
        private SettingsForm settings;

        public MainForm()
        {
            services = new List<JSONParser>() { new GisMeteoParser(rm.GetString("GisMeteoAPI")),
            new OpenWeatherParser(rm.GetString("OpenWeatherAPI")),
            new TomorrowIOParser(rm.GetString("TomorrowAPI")),
            new StormGlassParser(rm.GetString("StormGlassAPI"))};
            InitializeComponent();
        }
        private void Settings_Click(object sender, EventArgs e)
        {
            if (settings == null || settings.IsDisposed)
            {
                settings = new SettingsForm(services);
                settings.Show();
            }
            else settings.Activate();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            List<PictureBox> oldPic = this.Controls.OfType<PictureBox>().Where(x => x.ImageLocation != null && x.ImageLocation.Contains("https://http.cat")).ToList();
            while (oldPic.Count != 0)
            {
                oldPic[0].Dispose();
                oldPic.Remove(oldPic[0]);
            }

            View.Height = 30;
            View.Items.Clear();
            View.Update();


            foreach (var service in services)
            {
                var subList = new List<string>();
                var gr = new GetRequest(service.Link, service.Headers);
                var jg = new JsonGetter(gr);
                var json = jg.GetJSON();
                var information = service.Parse(json);

                ListViewItem content = new ListViewItem("");


                content.Text = information.Name;
                if (information.Error != null)
                {
                    string error = Regex.Replace(information.Error, @"[^\d]+", "");
                    content.Text += ": " + error;
                    PictureBox errorPic = new PictureBox();
                    errorPic.Size = new Size(16, 16);
                    errorPic.Location = new Point(View.Columns[0].Width + View.Location.X - errorPic.Width
                        , View.Height - 2 + View.Items.Count + View.Location.Y);
                    errorPic.ImageLocation = $"https://http.cat/{error}.jpg";
                    this.Controls.Add(errorPic);
                    errorPic.Click += new EventHandler(ShowError);
                    errorPic.BringToFront();
                }
                else
                {
                    content.SubItems.Add(information.MetricTemp);
                    content.SubItems.Add(information.ImperialTemp);
                    content.SubItems.Add(information.CloudCover);
                    content.SubItems.Add(information.Humidity);
                    content.SubItems.Add(information.Precipipations);
                    content.SubItems.Add(information.WindSpeed);
                    content.SubItems.Add(information.WindDegree);
                }
                View.Height += 20;
                View.Items.Add(content);
                View.Update();
            }
        }
        private void ShowError(object sender, EventArgs e)
        {
            PictureBox errorPic = (sender as PictureBox);
            if (errorPic.Size.Width != 750)
            {
                var tmp = errorPic.ImageLocation;
                errorPic.Size = new Size(750, 600);
                errorPic.ImageLocation = tmp;
                errorPic.Location = new Point(0, 220);
            }
            else
            {
                errorPic.Dispose();
            }
        }
    }
}
