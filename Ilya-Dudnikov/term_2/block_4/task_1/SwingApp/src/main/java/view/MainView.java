package view;

import controller.StateController;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.CompoundBorder;
import javax.swing.border.EmptyBorder;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.HashMap;
import java.util.Map;

public class MainView extends JPanel {
	private static final int SPB_LON = 60;
	private static final int SPB_LAT = 30;

	private Map<String, StateController> controllerMap;
	private Map<String, WeatherView> weatherViewMap;
	private ServiceSelector serviceSelector;

	public MainView(ServiceSelector serviceSelector) {
		setLayout(new BorderLayout());
		controllerMap = new HashMap<>();
		weatherViewMap = new HashMap<>();

		setBackground(new Color(43, 43, 43));

		// MenuBar
		this.serviceSelector = serviceSelector;

		// Refresh button
		JButton refreshButton = new JButton("Update");
		add(refreshButton, BorderLayout.SOUTH);
		refreshButton.addActionListener(e -> {
			if (!serviceSelector.getSelectedService().equals("Stormglass"))
				updateDataFromService(serviceSelector.getSelectedService());
			outputDataFromService(serviceSelector.getSelectedService());
			revalidate();
		});
		refreshButton.setForeground(Color.BLACK);
		refreshButton.setBackground(Color.WHITE);
		Border line = new LineBorder(Color.BLACK);

		Border margin = new EmptyBorder(5, 15, 5, 15);
		Border compound = new CompoundBorder(line, margin);
		refreshButton.setBorder(compound);
		refreshButton.setFocusPainted(false);
		refreshButton.setFont(new Font("Roboto", Font.BOLD, 15));
	}

	public void addService(String name, WeatherView view, StateController controller) {
		weatherViewMap.put(name, view);
		controllerMap.put(name, controller);
		serviceSelector.addService(name);

		if (weatherViewMap.size() == 1) {
			updateDataFromService(name);
			outputDataFromService(name);
		}
	}

	public void updateDataFromService(String service) {
		controllerMap.get(service).updateState(SPB_LON, SPB_LAT);
	}

	public void outputDataFromService(String service) {
		weatherViewMap.get(service).outputData();
		add(weatherViewMap.get(service), BorderLayout.CENTER);
	}
}