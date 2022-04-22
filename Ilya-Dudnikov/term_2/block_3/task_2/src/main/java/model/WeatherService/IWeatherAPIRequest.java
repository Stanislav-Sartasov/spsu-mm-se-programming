package model.WeatherService;

import java.net.http.HttpRequest;

public interface IWeatherAPIRequest {
	public HttpRequest createRequest(double lat, double lon, String APIKey);
}
