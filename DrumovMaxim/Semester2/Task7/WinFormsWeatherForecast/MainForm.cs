namespace WinFormsWeatherForecast
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x84)
            {
                base.WndProc(ref m);
                if ((int)m.Result == 0x1)
                    m.Result = (IntPtr)0x2;
                return;
            }

            base.WndProc(ref m);
        }

        private void TomorrowIO_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormTomorrowIO formTomorrowIO = new FormTomorrowIO();
            formTomorrowIO.Left = this.Left;
            formTomorrowIO.Top = this.Top;
            formTomorrowIO.Show();
        }

        private void StormGlassIO_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormStormGlassIO formStormGlassIO = new FormStormGlassIO();
            formStormGlassIO.Left = this.Left;
            formStormGlassIO.Top = this.Top;
            formStormGlassIO.Show();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}