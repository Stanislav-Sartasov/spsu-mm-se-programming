import controller.StateController;
import org.springframework.context.annotation.AnnotationConfigApplicationContext;
import view.MainView;
import view.ServiceSelector;
import view.WeatherView;

import javax.swing.*;

public class SwingApp extends JFrame {
	public SwingApp() {
		AnnotationConfigApplicationContext context = new AnnotationConfigApplicationContext(DIConfiguration.class);

		var menuBar = new ServiceSelector();
		MainView mainView = new MainView(menuBar);
		setContentPane(mainView);
		setSize(800, 600);
		setResizable(false);
		setJMenuBar(menuBar);

		StateController openWeatherMapController = (StateController) context.getBean("openWeatherMapStateController");
		WeatherView openWeatherMapView = (WeatherView) context.getBean("openWeatherMapView");

		StateController stormglassController = (StateController) context.getBean("stormglassStateController");
		WeatherView stormglassView = (WeatherView) context.getBean("stormglassView");

		mainView.addService("OpenWeatherMap", openWeatherMapView, openWeatherMapController);
		mainView.addService("Stormglass", stormglassView, stormglassController);

		setVisible(true);

		setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
	}

	public static void main(String[] args) {
		new SwingApp();
	}
}
