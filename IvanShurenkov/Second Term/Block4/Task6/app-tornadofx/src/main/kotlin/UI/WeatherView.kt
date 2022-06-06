package UI

import KodeinInjactions
import javafx.beans.property.SimpleStringProperty
import javafx.collections.ObservableList
import lib.weather.IWeatherApi
import lib.weather.date.Location
import lib.weather.date.Weather
import org.kodein.di.instance
import tornadofx.*
import kotlin.concurrent.thread

class WeatherView : View() {
    private var services: ObservableList<String> = observableListOf()
    private val selectedService = SimpleStringProperty()

    private var tempreture = SimpleStringProperty()
    private var cloudCoverage = SimpleStringProperty()
    private var humidity = SimpleStringProperty()
    private var precipitation = SimpleStringProperty()
    private var windSpeed = SimpleStringProperty()
    private var windDirection = SimpleStringProperty()

    private val noData = "No data available"
    private val hasNotService = "Service had been not selected"

    init {
        for (i in KodeinInjactions.servicesNames) {
            services += i
        }
        tempreture.value = hasNotService
        cloudCoverage.value = hasNotService
        humidity.value = hasNotService
        precipitation.value = hasNotService
        windSpeed.value = hasNotService
        windDirection.value = hasNotService
    }

    override val root = vbox {
        combobox(selectedService, services) {
            setMinSize(300.0, 10.0)
            setOnAction {
                println(selectedService.value)
                val service: IWeatherApi by KodeinInjactions.service.instance(selectedService.value)
                updateLabel(service.getWeather())
            }
        }
        hbox {
            vbox {
                setMinSize(110.0, 0.0)
                label("Temperature")
                label("Cloud coverage")
                label("Humidity")
                label("Precipitation")
                label("Wind speed")
                label("Wind directiom")
            }
            vbox {
                label(tempreture)
                label(cloudCoverage)
                label(humidity)
                label(precipitation)
                label(windSpeed)
                label(windDirection)
            }
        }
        button("Update") {
            setMinSize(300.0, 10.0)
            action {
                if (selectedService.value != null) {
                    val service: IWeatherApi by KodeinInjactions.service.instance(selectedService.value)
                    val location: Location by KodeinInjactions.location.instance("spb")
                    val api: String by KodeinInjactions.apikey.instance(selectedService.value)
                    if (api != null)
                        service.updateWeather(location, api)
                    updateLabel(service.getWeather())
                }
            }
        }
    }

    fun updateLabel(weather: Weather) {
        tempreture.value =
            if (weather.temperature != null) "${weather.temperature!!.celsius}°C | ${weather.temperature!!.fahrenheit}°F"
            else noData
        cloudCoverage.value = if (weather.cloudCoverage != null) "${weather.cloudCoverage!!.percent}%"
        else noData
        humidity.value = if (weather.humidity != null) "${weather.humidity!!.percent}%"
        else noData
        precipitation.value = if (weather.precipitation != null) "${weather.precipitation!!.mmPerHour} mm/h"
        else noData
        windSpeed.value = if (weather.windSpeed != null) "${weather.windSpeed!!.speed} m/s"
        else noData
        windDirection.value = if (weather.windDirection != null) "${weather.windDirection!!.degree}°"
        else noData
    }
}
