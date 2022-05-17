using WeatherForecastModelGUI;


namespace WeatherForecastGUIWinForms
{
    public partial class WeatherForecastForm : Form
    {
        WeatherForecastModel model;

        public WeatherForecastForm()
        {
            InitializeComponent();

            model = new WeatherForecastModel();

            modelSource.Add(model);
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            model.UpdateData();
        }

        private void SwitchButton_Click(object sender, EventArgs e)
        {
            model.SwitchService();
        }

        private void ActivityButton_Click(object sender, EventArgs e)
        {
            model.UpdateServiceActivityStatus();
        }
    }
}