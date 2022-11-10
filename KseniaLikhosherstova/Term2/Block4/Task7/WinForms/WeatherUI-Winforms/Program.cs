using WeatherUI_Winforms.Controller;
using WeatherUI_Winforms.View;

namespace WeatherUI_Winforms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            WeatherController controller = new WeatherController(new WeatherView());
            Application.Run(controller.View);
        }
    }
}