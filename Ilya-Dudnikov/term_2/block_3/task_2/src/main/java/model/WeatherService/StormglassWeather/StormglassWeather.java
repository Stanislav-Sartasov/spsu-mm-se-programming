package model.WeatherService.StormglassWeather;

import model.WeatherService.IWeatherAPIRequest;

import java.net.URI;
import java.net.http.HttpRequest;
import java.util.ArrayList;

public class StormglassWeather implements IWeatherAPIRequest {
	private static final String STORMGLASS_URL = "https://api.stormglass.io/v2/weather/point";
	private String APIKey;

	private ArrayList<String> params;

	public StormglassWeather(ArrayList<String> params) {
		this.params = params;
	}

	public void setAPIKey(String key) {
		this.APIKey = key;
	}

	@Override
	public HttpRequest createRequest(double lat, double lon) {
		HttpRequest request = HttpRequest.newBuilder()
				.GET()
				.header("Authorization", APIKey)
				.uri(URI.create(
						STORMGLASS_URL
						+ "?lat=" + lat
						+ "&lng=" + lon
						+ "&params=" + String.join(",", params)
				))
				.build();
		return request;
	}
}
