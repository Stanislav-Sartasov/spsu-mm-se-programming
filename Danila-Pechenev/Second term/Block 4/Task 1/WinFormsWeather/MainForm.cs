namespace WinFormsWeather;
using General;

public partial class MainForm : Form
{
    private Model model;

    public MainForm()
    {
        model = new Model();
        InitializeComponent();
    }

    private void usedServiceBox_TextUpdate(object sender, EventArgs e)
    {
        if (!usedServiceBox.Items.Contains(usedServiceBox.Text))
        {
            errorTextBox.Text = "Please select one of the suggested data sources!";
        }
        else
        {
            errorTextBox.Text = "";
        }
    }

    private void usedServiceBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        errorTextBox.Text = "";
    }

    private void updateWeatherButton_Click(object sender, EventArgs e)
    {
        if (usedServiceBox.Items.Contains(usedServiceBox.Text))
        {
            weatherTextBox.Text = model.GetData(usedServiceBox.Text);
        }
    }
}
