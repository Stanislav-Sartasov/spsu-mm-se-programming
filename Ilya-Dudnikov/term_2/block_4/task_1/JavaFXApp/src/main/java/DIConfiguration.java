import controller.StateController;
import model.JSONParser.OpenWeatherMapParser;
import model.JSONParser.StormglassParser;
import model.WeatherData.WeatherData;
import model.WeatherModel.OpenWeatherMapModel;
import model.WeatherModel.StormglassModel;
import model.WeatherService.OpenWeatherMap;
import model.WeatherService.StormglassWeather;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import view.WeatherView;

import java.util.ArrayList;

@Configuration
public class DIConfiguration {
	@Bean
	public StormglassParser stormglassParser() {
		return new StormglassParser();
	}

	@Bean
	public OpenWeatherMapParser openWeatherMapParser() {
		return new OpenWeatherMapParser();
	}

	@Bean
	public WeatherData weatherData() {
		return new WeatherData(
				null,
				null,
				null,
				null,
				null,
				null,
				null
		);
	}

	@Bean
	public StormglassModel stormglassModel() {
		return new StormglassModel(weatherData());
	}

	@Bean
	public OpenWeatherMapModel openWeatherMapModel() {
		return new OpenWeatherMapModel(weatherData());
	}

	@Bean
	public StormglassWeather stormglassWeather() {
		return new StormglassWeather(new ArrayList<>() {{
			add("airTemperature");
			add("cloudCover");
			add("humidity");
			add("precipitation");
			add("windDirection");
			add("windSpeed");
		}});
	}

	@Bean
	public OpenWeatherMap openWeatherMap() {
		return new OpenWeatherMap();
	}

	@Bean
	public StateController stormglassStateController() {
		return new StateController(stormglassModel(), stormglassWeather(), stormglassParser());
	}

	@Bean
	public StateController openWeatherMapStateController() {
		return new StateController(openWeatherMapModel(), openWeatherMap(), openWeatherMapParser());
	}

	@Bean
	public WeatherView stormglassView() {
		return new WeatherView(stormglassModel());
	}

	@Bean
	public WeatherView openWeatherMapView() {
		return new WeatherView(openWeatherMapModel());
	}
}
