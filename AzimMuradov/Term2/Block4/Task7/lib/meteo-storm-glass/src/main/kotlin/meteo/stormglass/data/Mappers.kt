package meteo.stormglass.data

import meteo.domain.entity.*
import meteo.stormglass.data.StormGlassApi.SG
import meteo.stormglass.data.model.StormGlassModel
import kotlin.math.roundToInt

internal fun StormGlassModel.toWeatherData(): Weather = with(hours.first()) {
    Weather(
        description = null,
        temperature = Temperature.inCelsius(airTemperature.getValue(SG)),
        cloudCoverage = CloudCoverage.inPercent(cloudCover.getValue(SG).roundToInt()),
        humidity = Humidity.inPercent(humidity.getValue(SG).roundToInt()),
        precipitation = Precipitation.inMmPerHour(precipitation.getValue(SG)),
        windDirection = WindDirection.inDegrees(windDirection.getValue(SG).roundToInt()),
        windSpeed = WindSpeed.inMetersPerSecond(windSpeed.getValue(SG)),
    )
}
