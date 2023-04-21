import androidx.compose.desktop.ui.tooling.preview.Preview
import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Refresh
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import lib.weather.IWeatherApi
import lib.weather.date.Location
import lib.weather.date.Weather
import org.kodein.di.instance

class AppWindow {
    private val noData = "No data available"
    private val hasNotService = "Service had been not selected"

    @Composable
    @Preview
    fun App() {
        var text by remember { mutableStateOf("Update") }
        var selectedItem by remember { mutableStateOf(0) }
        var items = listOf<String>()
        for (i in KodeinInjactions.servicesNames) {
            items += i
        }

        var weather = remember {
            mutableStateListOf<String>(
                hasNotService,
                hasNotService,
                hasNotService,
                hasNotService,
                hasNotService,
                hasNotService
            )
        }

        MaterialTheme {
            Column(Modifier.fillMaxSize(), Arrangement.spacedBy(5.dp)) {
                BottomNavigation {
                    items.forEachIndexed { index, item ->
                        BottomNavigationItem(
                            icon = { null },
                            label = { Text(item) },
                            selected = selectedItem == index,
                            onClick = {
                                selectedItem = index
                                //print(items[selectedItem])
                                val service: IWeatherApi by KodeinInjactions.service.instance(items[selectedItem])

                                val weatherList = updateLabel(service.getWeather())
                                for (i in weatherList.indices) {
                                    weather[i] = weatherList[i]
                                }
                            }
                        )
                    }
                }

                PairRow("Tempreture", weather[0], Modifier)
                PairRow("Cloud coverage",  weather[1], Modifier)
                PairRow("Humidity",  weather[2], Modifier)
                PairRow("Precipitation",  weather[3], Modifier)
                PairRow("Wind speed",  weather[4], Modifier)
                PairRow("Wind direction",  weather[5], Modifier)

                IconButton(modifier = Modifier.align(Alignment.End), onClick = {
                    //print(text)
                    val service: IWeatherApi by KodeinInjactions.service.instance(items[selectedItem])
                    val location: Location by KodeinInjactions.location.instance("spb")
                    val api: String by KodeinInjactions.apikey.instance(items[selectedItem])
                    if (api != null)
                        service.updateWeather(location, api)

                    val weatherList = updateLabel(service.getWeather())
                    for (i in weatherList.indices) {
                        weather[i] = weatherList[i]
                    }
                }) {
                    Icon(
                        Icons.Default.Refresh,
                        contentDescription = null
                    )
                }
            }
        }
    }

    @Composable
    fun PairRow(name: String, value: String, modifier: Modifier) {
        Row(
            modifier = modifier.fillMaxWidth(),
            horizontalArrangement = Arrangement.SpaceBetween,
            verticalAlignment = Alignment.CenterVertically
        ) {
            Text(name)
            Text(value)
        }
        Divider()
    }

    fun updateLabel(weather: Weather): List<String> {
        var weatherList = emptyList<String>()
        weatherList +=
            if (weather.temperature != null) "${weather.temperature!!.celsius}°C | ${weather.temperature!!.fahrenheit}°F"
            else noData
        weatherList += if (weather.cloudCoverage != null) "${weather.cloudCoverage!!.percent}%"
        else noData
        weatherList += if (weather.humidity != null) "${weather.humidity!!.percent}%"
        else noData
        weatherList += if (weather.precipitation != null) "${weather.precipitation!!.mmPerHour} mm/h"
        else noData
        weatherList += if (weather.windSpeed != null) "${weather.windSpeed!!.speed} m/s"
        else noData
        weatherList += if (weather.windDirection != null) "${weather.windDirection!!.degree}°"
        else noData
        return weatherList
    }
}
