package model.WeatherService;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.Mockito;

import java.io.IOException;
import java.net.URI;
import java.net.http.HttpClient;
import java.net.http.HttpRequest;
import java.net.http.HttpResponse;
import java.nio.file.Files;
import java.nio.file.Path;

import static org.junit.jupiter.api.Assertions.assertEquals;

class WeatherServiceTest {
	private String json;
	private double lat = 30;
	private double lon = 60;

	@BeforeEach
	void setUp() throws IOException {
		json = Files.readString(Path.of( "src/test/resources/goodJson.json"));
	}

	@Test
	void requestData() throws IOException, InterruptedException {
		HttpResponse<String> response = Mockito.mock(HttpResponse.class);
		Mockito.when(response.body()).thenReturn(json);

		HttpClient client = Mockito.mock(HttpClient.class);
		IWeatherAPIRequest requestCreator = Mockito.mock(IWeatherAPIRequest.class);

		Mockito.when(requestCreator.createRequest(lat, lon)).thenReturn(HttpRequest.newBuilder().uri(URI.create("https://funnyURI.org")).build());
		Mockito.when(client.send(requestCreator.createRequest(lat, lon), HttpResponse.BodyHandlers.ofString())).thenReturn(response);

		WeatherService weatherService = new WeatherService(requestCreator);
		assertEquals(json, weatherService.requestData(lat, lon, client));
	}
}