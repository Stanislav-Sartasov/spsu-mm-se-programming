package controller;

import model.JSONParser.JSONParser;
import model.WeatherData.WeatherData;
import model.WeatherModel.WeatherModel;
import model.WeatherService.IWeatherAPIRequest;
import model.WeatherService.WeatherService;
import org.json.simple.parser.ParseException;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.Mockito;

import javax.swing.plaf.nimbus.State;

import java.net.http.HttpClient;
import java.util.HashMap;

import static org.junit.jupiter.api.Assertions.*;

class StateControllerTest {
	private IWeatherAPIRequest requestCreator;
	private JSONParser jsonParser;
	private StateController controller;
	private WeatherModel model;

	@BeforeEach
	void setUp() {
		requestCreator = Mockito.mock(IWeatherAPIRequest.class);
		jsonParser = Mockito.mock(JSONParser.class);
		model = Mockito.mock(WeatherModel.class);

		controller = new StateController(model, requestCreator, jsonParser);
	}

	@Test
	void updateStateIllegalArgumentsExpectThrows() {
		assertThrows(IllegalArgumentException.class, () -> controller.updateState(-10, 10));
		assertThrows(IllegalArgumentException.class, () -> controller.updateState(10, -10));
		assertThrows(IllegalArgumentException.class, () -> controller.updateState(200, 10));
		assertThrows(IllegalArgumentException.class, () -> controller.updateState(10, 100));
	}
}