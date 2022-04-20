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
using UILogicLibrary;

namespace WeatherWinForm
{
    public partial class MainForm : Form
    {
        private readonly WeatherModel model = new WeatherModel();

        private SettingsForm settings;

        public MainForm()
        {
            InitializeComponent();
        }
        private void Settings_Click(object sender, EventArgs e)
        {
            if (settings == null || settings.IsDisposed)
            {
                settings = new SettingsForm(model);
                settings.Show();
            }
            else settings.Activate();
        }

        private void ShowButton_Click(object sender, EventArgs e)
        {
            List<PictureBox> oldPic = this.Controls.OfType<PictureBox>().Where(x =>x.ImageLocation != null && x.ImageLocation.Contains("https://http.cat")).ToList();
            while (oldPic.Count != 0)
            {
                oldPic[0].Dispose();
                oldPic.Remove(oldPic[0]);
            }

            View.Height = 30;
            View.Items.Clear();
            View.Update();

            List<List<string>> toShow = model.GetWeather();

            foreach (var list in toShow)
            {
                ListViewItem content = new ListViewItem("");

                foreach (var term in list)
                {
                    if (content.Text == "")
                    {
                        content.Text = term;
                        string error = model.GetError(term);
                        if (error != "")
                        {
                            PictureBox errorPic = new PictureBox();
                            errorPic.Size = new Size(16, 16);
                            errorPic.Location = new Point(View.Columns[0].Width + View.Location.X - errorPic.Width
                                , View.Height - 2 + View.Items.Count + View.Location.Y);
                            errorPic.ImageLocation = $"https://http.cat/{error}.jpg";
                            this.Controls.Add(errorPic);
                            errorPic.Click += new EventHandler(ShowError);
                            errorPic.BringToFront();
                        }
                    }
                    else
                    {
                        content.SubItems.Add(term);
                    }
                    
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
                errorPic.Location = new Point(0,220);
            }
            else
            {
                errorPic.Dispose();
            }
        }
    }
}
