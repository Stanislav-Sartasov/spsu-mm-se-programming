package meteo.openweather.data

import meteo.domain.entity.*
import meteo.openweather.data.model.OpenWeatherModel
import meteo.openweather.data.model.WeatherModel

internal fun OpenWeatherModel.toWeatherData(): Weather = Weather(
    description = weather?.mapNotNull(WeatherModel::description)?.joinToString(),
    temperature = main?.temp?.let(Temperature::inKelvin),
    cloudCoverage = clouds?.all?.let(CloudCoverage::inPercent),
    humidity = main?.humidity?.let(Humidity::inPercent),
    precipitation = Precipitation.inMmPerHour(value = (rain?.`1h` ?: 0.0) + (snow?.`1h` ?: 0.0)),
    windDirection = wind?.deg?.let(WindDirection::inDegrees),
    windSpeed = wind?.speed?.let(WindSpeed::inMetersPerSecond),
)
