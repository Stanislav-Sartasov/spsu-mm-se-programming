package kodein

import WeatherAppCLI
import entity.Location
import org.kodein.di.DI
import org.kodein.di.*
import service.weather.WeatherService
import service.weather.open_weather_map.OpenWeatherMapService
import service.weather.open_weather_map.parser.OpenWeatherMapJSONParser
import service.weather.tomorrow_io.TomorrowIOService
import service.weather.tomorrow_io.parser.TomorrowIOJSONParser

object DIConfiguration {

	val default = DI.Module("defaultConfiguration") {
		import(openWeatherMap)
		import(tomorrowIO)
		import(app)
	}

	private val openWeatherMap = DI.Module("openweathermap.org") {
		bindProvider {
			OpenWeatherMapService(
				instance()
			)
		}

		bindProvider {
			OpenWeatherMapJSONParser()
		}
	}

	private val tomorrowIO = DI.Module("tomorrow.io") {
		bindProvider {
			TomorrowIOService(
				instance()
			)
		}

		bindProvider {
			TomorrowIOJSONParser()
		}
	}

	private val app = DI.Module("app") {
		bindFactory<List<WeatherService>, WeatherAppCLI> { services ->
			WeatherAppCLI(
				location = instance(),
				services
			)
		}

		bindFactory<Array<String>, WeatherAppCLI> { serviceNames ->
			WeatherAppCLI(
				location = instance(),
				weatherServices = instance(arg = serviceNames)
			)
		}

		bindFactory<Array<String>, List<WeatherService>> { arguments ->
			val availableServices = mapOf(
				"--openweathermap" to instance<OpenWeatherMapService>(),
				"--tomorrowio" to instance<TomorrowIOService>()
			)
			arguments
				.mapNotNull { availableServices[it] }
		}

		bindProvider {
			Location("Saint-Petersburg", 59.94, 30.313)
		}
	}

}