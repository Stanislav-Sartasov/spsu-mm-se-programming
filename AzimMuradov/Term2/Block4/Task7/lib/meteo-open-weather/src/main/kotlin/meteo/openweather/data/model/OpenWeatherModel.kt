package meteo.openweather.data.model

import kotlinx.serialization.Serializable

@Serializable
internal data class OpenWeatherModel(
    val weather: List<WeatherModel>?,
    val main: MainModel?,
    val wind: WindModel?,
    val clouds: CloudsModel?,
    val rain: RainModel?,
    val snow: SnowModel?,
)
