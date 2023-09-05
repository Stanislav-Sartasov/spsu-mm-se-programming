using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Parsers;

namespace WeatherWinForm
{
    public partial class SettingsForm : Form
    {
        public List<JSONParser> services;
        public SettingsForm(List<JSONParser> services)
        {
            this.services = services;
            InitializeComponent();
            foreach (var service in services)
            {
                ServiceBox.Items.Add(service.GetType().Name);
            }

        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            foreach (var service in services)
            {
                if (service.GetType().Name == ServiceBox.Text)
                {
                    service.SetKey(KeyBox.Text);
                    KeyLabel.Text = "Success!";
                    return;
                }
            }
            KeyLabel.Text = "Wrong service";
        }
    }
}
