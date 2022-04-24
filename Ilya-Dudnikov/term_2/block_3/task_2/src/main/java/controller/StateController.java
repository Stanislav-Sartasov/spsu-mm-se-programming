package controller;

import model.JSONParser.JSONParser;
import model.WeatherModel.WeatherModel;
import model.WeatherService.IWeatherAPIRequest;
import model.WeatherService.WeatherService;
import org.json.simple.parser.ParseException;

import java.net.http.HttpClient;

public class StateController {
	private static final double MIN_LAT = 0;
	private static final double MIN_LON = 0;
	private static final double MAX_LAT = 90;
	private static final double MAX_LON = 180;

	protected WeatherModel weatherModel;

	public void updateState(
			double lon,
			double lat,
			IWeatherAPIRequest requestCreator,
			JSONParser jsonParser
	) {
		if (lat < MIN_LAT || lat > MAX_LAT || lon < MIN_LON || lon > MAX_LON) {
			throw new IllegalArgumentException("Given longitude and/or latitude are invalid");
		}

		WeatherService weatherService = new WeatherService(requestCreator);
		HttpClient client = HttpClient.newBuilder().build();
		try {
			weatherModel.updateState(jsonParser.parse(weatherService.requestData(lat, lon, client)));
		} catch (ParseException e) {
			weatherModel.updateState(null);
		}
	}
}
