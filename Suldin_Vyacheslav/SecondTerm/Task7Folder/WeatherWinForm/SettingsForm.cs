using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UILogicLibrary;

namespace WeatherWinForm
{
    public partial class SettingsForm : Form
    {
        private readonly WeatherModel model = new WeatherModel();
        public SettingsForm(WeatherModel inputModel)
        {
            InitializeComponent();
            model = inputModel;
            foreach (var service in model.services)
            {
                ServiceBox.Items.Add(service.GetType().Name);
            }
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            foreach (var service in model.services)
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
