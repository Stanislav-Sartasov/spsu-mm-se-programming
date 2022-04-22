package model.WeatherService.StormglassWeather;

import model.WeatherService.IWeatherAPIRequest;

import java.net.URI;
import java.net.http.HttpRequest;
import java.text.DateFormat;
import java.time.Instant;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;

public class StormglassWeather implements IWeatherAPIRequest {
	private static final String STORMGLASS_URL = "https://api.stormglass.io/v2/weather/point";

	private final ArrayList<String> params;

	public StormglassWeather(ArrayList<String> params) {
		this.params = params;
	}

	@Override
	public HttpRequest createRequest(double lat, double lon, String APIKey) {
		final int DATE_LENGTH = 10;

		return HttpRequest.newBuilder()
				.GET()
				.header("Authorization", APIKey)
				.uri(URI.create(
						STORMGLASS_URL
						+ "?lat=" + lat
						+ "&lng=" + lon
						+ "&params=" + String.join(",", params)
						+ "&start=" + Instant.now().toString().substring(0, DATE_LENGTH)
						+ "&end=" + Instant.now().toString().substring(0, DATE_LENGTH)
				))
				.build();
	}
}
