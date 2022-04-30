package view;

import controller.StateController;
import model.WeatherData.WeatherData;
import model.WeatherModel.StormglassModel;
import model.WeatherService.StormglassWeather;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.Mockito;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.PrintStream;
import java.nio.charset.StandardCharsets;

import static org.junit.jupiter.api.Assertions.*;

class MainViewTest {
	private MainView view;

	@BeforeEach
	void setUp() {
		view = new MainView();
	}

	@Test
	void addService() {
		StormglassView stormglassView = Mockito.mock(StormglassView.class);
		OpenWeatherMapView openWeatherMapView = Mockito.mock(OpenWeatherMapView.class);

		StateController stormglassController = Mockito.mock(StateController.class);
		StateController openWeatherMapController = Mockito.mock(StateController.class);

		view.addService(stormglassView, stormglassController);
		view.addService(openWeatherMapView, openWeatherMapController);

		assertTrue(view.servicesList.contains(stormglassView));
		assertTrue(view.servicesList.contains(openWeatherMapView));
		assertTrue(view.controllerList.contains(stormglassController));
		assertTrue(view.controllerList.contains(openWeatherMapController));
	}

	@Test
	void inputDataTwoDoubles() {
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		PrintStream newPrintStream = new PrintStream(byteArrayOutputStream);
		System.setOut(newPrintStream);

		ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream("30 30".getBytes(StandardCharsets.UTF_8));
		System.setIn(byteArrayInputStream);
		view.refreshAction();

		assertEquals(
				"Please enter the desired latitude and longitude or type \"default\" to proceed with default settings." + System.lineSeparator()
				+ "Default coordinates are: 60\u00B0 latitude, 30\u00B0 longitude (those are the coordinates of Saint-Petersburg" + System.lineSeparator(),
				byteArrayOutputStream.toString().replaceAll("\\n|\\r\\n", System.lineSeparator())
		);
	}

	@Test
	void inputDataInvalidInputExpectErrorMessage() {
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		PrintStream newPrintStream = new PrintStream(byteArrayOutputStream);
		System.setOut(newPrintStream);

		ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(
				("ss ss" +System.lineSeparator() + "30 30").getBytes(StandardCharsets.UTF_8)
		);
		System.setIn(byteArrayInputStream);
		view.refreshAction();

		assertEquals(
				"Please enter the desired latitude and longitude or type \"default\" to proceed with default settings." + System.lineSeparator()
						+ "Default coordinates are: 60\u00B0 latitude, 30\u00B0 longitude (those are the coordinates of Saint-Petersburg" + System.lineSeparator()
						+ "Invalid input: two floating-point numbers are required. Please, try again:" + System.lineSeparator(),
				byteArrayOutputStream.toString().replaceAll("\\n|\\r\\n", System.lineSeparator())
		);
	}

	@Test
	void inputDataDefault() {
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		PrintStream newPrintStream = new PrintStream(byteArrayOutputStream);
		System.setOut(newPrintStream);

		ByteArrayInputStream byteArrayInputStream = new ByteArrayInputStream(
				("default").getBytes(StandardCharsets.UTF_8)
		);
		System.setIn(byteArrayInputStream);
		view.refreshAction();

		assertEquals(
				"Please enter the desired latitude and longitude or type \"default\" to proceed with default settings." + System.lineSeparator()
						+ "Default coordinates are: 60\u00B0 latitude, 30\u00B0 longitude (those are the coordinates of Saint-Petersburg" + System.lineSeparator(),
				byteArrayOutputStream.toString().replaceAll("\\n|\\r\\n", System.lineSeparator())
		);
	}
}