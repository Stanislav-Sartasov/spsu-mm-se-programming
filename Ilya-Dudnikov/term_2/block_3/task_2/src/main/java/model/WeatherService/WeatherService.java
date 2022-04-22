package model.WeatherService;

import java.io.IOException;
import java.net.http.HttpClient;
import java.net.http.HttpResponse;
import java.time.Duration;

public class WeatherService {
	private IWeatherAPIRequest requestCreator;
	private String APIKey;

	public WeatherService(IWeatherAPIRequest requestCreator, String APIKey) {
		this.requestCreator = requestCreator;
		this.APIKey = APIKey;
	}

	public String requestData(double lat, double lon, HttpClient client) {
		try {
			HttpResponse<String> response = client.send(
					requestCreator.createRequest(lat, lon, APIKey),
					HttpResponse.BodyHandlers.ofString()
			);

			return response.body();
		} catch (IOException e) {
			e.printStackTrace();
		} catch (InterruptedException e) {
			e.printStackTrace();
		}

		return null;
	}
}
