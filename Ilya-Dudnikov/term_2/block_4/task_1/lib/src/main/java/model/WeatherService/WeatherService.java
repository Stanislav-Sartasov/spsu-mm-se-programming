package model.WeatherService;

import java.io.IOException;
import java.net.http.HttpClient;
import java.net.http.HttpResponse;

public class WeatherService {
	private IWeatherAPIRequest requestCreator;

	public WeatherService(IWeatherAPIRequest requestCreator) {
		this.requestCreator = requestCreator;
	}

	public String requestData(double lat, double lon, HttpClient client) {
		try {
			HttpResponse<String> response = client.send(
					requestCreator.createRequest(lat, lon),
					HttpResponse.BodyHandlers.ofString()
			);

			return response.body();
		} catch (IOException | InterruptedException e) {
			return null;
		}
	}
}
