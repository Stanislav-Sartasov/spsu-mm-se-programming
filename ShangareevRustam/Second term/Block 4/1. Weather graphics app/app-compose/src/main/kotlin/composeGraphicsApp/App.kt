package composeGraphicsApp

import androidx.compose.material.MaterialTheme
import androidx.compose.runtime.*
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.Window
import androidx.compose.ui.window.application
import androidx.compose.ui.window.rememberWindowState
import springIoc.SpringIoc
import weatherDataFormatter.WeatherDataFormatter
import weatherUtilities.Location
import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherDataState
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

fun main() = application {

	val fieldList = listOf(
		WeatherCharacteristics(name = "Temperature"),
		WeatherCharacteristics(name = "Cloud cover"),
		WeatherCharacteristics(name = "Humidity"),
		WeatherCharacteristics(name = "Precipitation type"),
		WeatherCharacteristics(name = "Wind direction"),
		WeatherCharacteristics(name = "Wind speed"),
		WeatherCharacteristics(name = "Precipitation")
	)
	val icon = painterResource("appIcon.png")
	val location = Location(lat = 59.9055, lon = 30.3007)
	val container = SpringIoc()

	var service: Int by remember { mutableStateOf(0) }
	val ww = if (service == 0) container.openweathermapOrg() else container.tomorrowIo()
	val data: MutableList<WeatherDataState> = remember {
		mutableStateListOf(
			WeatherDataState(getTimeFormatted(),
				WeatherDataFormatter.formatWeatherData(container.openweathermapOrg().getWeatherData(
					fieldList,
					location)
				)
			),
			WeatherDataState(getTimeFormatted(),
				WeatherDataFormatter.formatWeatherData(container.tomorrowIo().getWeatherData(
					fieldList,
					location)
				)
			)
		)
	}

	Window(
		onCloseRequest = ::exitApplication,
		title = "Weather application",
		state = rememberWindowState(width = 760.dp, height = 300.dp),
		resizable = false,
		icon = icon
	) {
		MaterialTheme {
			ComposeController.Screen(
				service = service,
				data = data,
				chooseService = { service = it },
				updateData = {
					if (getTimeFormatted() != data[service].time) data[service] =
					WeatherDataState(getTimeFormatted(),
						WeatherDataFormatter.formatWeatherData(ww.getWeatherData(
							fieldList,
							location)
						)
					)
				}
			)
		}
	}
}

private fun getTimeFormatted(): String = LocalDateTime.now().format(DateTimeFormatter.ofPattern("HH:mm"))
