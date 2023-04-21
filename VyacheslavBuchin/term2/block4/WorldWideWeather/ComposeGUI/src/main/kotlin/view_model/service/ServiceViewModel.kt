package view_model.service

import service.weather.WeatherService
import service.weather.open_weather_map.OpenWeatherMapService
import service.weather.tomorrow_io.TomorrowIOService

class ServiceViewModel {
	private val _services = mutableMapOf(
		OpenWeatherMapService() to true,
		TomorrowIOService() to true
	)

	val availableServices: List<WeatherService>
		get() = _services.keys.toList()
	val selectedServices: List<WeatherService>
		get() = _services.toList().filter { it.second }.map { it.first }

	fun setSelected(service: WeatherService, value: Boolean) {
		_services[service] = value
	}

	fun isSelected(service: WeatherService) = _services[service] ?: false
}