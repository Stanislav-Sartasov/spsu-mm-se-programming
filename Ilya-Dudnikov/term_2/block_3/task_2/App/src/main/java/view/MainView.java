package view;

import controller.StateController;
import model.WeatherData.WeatherData;

import javax.swing.plaf.nimbus.State;
import java.util.ArrayList;
import java.util.InputMismatchException;
import java.util.Objects;
import java.util.Scanner;

public class MainView {
	private final double SPB_LAT = 60;
	private final double SPB_LON = 30;

	ArrayList<WeatherView> servicesList;
	ArrayList<StateController> controllerList;

	public MainView() {
		this.servicesList = new ArrayList<>();
		this.controllerList = new ArrayList<>();
	}

	public void addService(WeatherView view, StateController controller) {
		servicesList.add(view);
		controllerList.add(controller);
	}

	public void refreshAction() throws IllegalArgumentException {
		System.out.println("Please enter the desired latitude and longitude or type \"default\" to proceed with default settings.");
		System.out.println("Default coordinates are: 60\u00B0 latitude, 30\u00B0 longitude (those are the coordinates of Saint-Petersburg");

		Scanner scanner = new Scanner(System.in);

		Double lat = null;
		Double lon = null;
		while (lat == null || lon == null) {
			try {
				lat = scanner.nextDouble();
				lon = scanner.nextDouble();
			} catch (InputMismatchException e) {
				if (scanner.nextLine().equals("default")) {
					lat = SPB_LAT;
					lon = SPB_LON;
					return;
				}

				System.out.println("Invalid input: two floating-point numbers are required. Please, try again:");
			}
		}

		updateData(lon, lat);
	}

	public void updateData(double lon, double lat) {
		for (var controller : controllerList) {
			controller.updateState(lon, lat);
		}
	}

	public void outputData() {
		for (var service : servicesList) {
			service.outputData();
		}
	}
}
