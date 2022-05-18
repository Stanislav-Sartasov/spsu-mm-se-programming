package view;

import controller.StateController;

import javax.swing.*;
import java.awt.*;
import java.util.Map;

public class MainView extends JPanel {
	private static final int SPB_LON = 60;
	private static final int SPB_LAT = 30;

	private Map<String, StateController> controllerMap;
	private Map<String, WeatherView> weatherViewMap;
	private ServiceSelector serviceSelector;

	public MainView(ServiceSelector serviceSelector) {
		setBackground(new Color(43, 43, 43));

		// MenuBar
		this.serviceSelector = serviceSelector;
	}

	public void addService(String name, WeatherView view, StateController controller) {
		weatherViewMap.put(name, view);
		controllerMap.put(name, controller);
		serviceSelector.addService(name);
	}

	public void updateDataFromService(String service) {
		controllerMap.get(service).updateState(SPB_LON, SPB_LAT);
	}

	public void outputDataFromService(String service) {
		weatherViewMap.get(service).outputData();
	}
}