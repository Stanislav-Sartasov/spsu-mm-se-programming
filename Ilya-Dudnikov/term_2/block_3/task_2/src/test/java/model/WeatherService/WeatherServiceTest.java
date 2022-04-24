package model.WeatherService;

import model.WeatherService.OpenWeatherMap.OpenWeatherMap;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;

import java.io.IOException;
import java.net.http.HttpClient;
import java.net.http.HttpResponse;
import java.nio.file.Files;
import java.nio.file.Path;

import static org.junit.jupiter.api.Assertions.*;

class WeatherServiceTest {
	private String json;
	private double lat = 30;
	private double lon = 60;
	private String APIKey = "hahaNotPutinAnymore";

	@BeforeEach
	void setUp() throws IOException {
		json = Files.readString(Path.of( "src/test/resources/OpenWeatherMapJSONs/goodOpenWeatherMapJson.json"));
	}

	@Test
	void requestData() throws IOException, InterruptedException {
		HttpResponse<String> response = Mockito.mock(HttpResponse.class);
		Mockito.when(response.body()).thenReturn(json);

		HttpClient client = Mockito.mock(HttpClient.class);
		OpenWeatherMap requestCreator = new OpenWeatherMap();
		Mockito.when(client.send(requestCreator.createRequest(lat, lon), HttpResponse.BodyHandlers.ofString())).thenReturn(response);

		WeatherService weatherService = new WeatherService(requestCreator);
		assertEquals(json, weatherService.requestData(lat, lon, client));
	}
}