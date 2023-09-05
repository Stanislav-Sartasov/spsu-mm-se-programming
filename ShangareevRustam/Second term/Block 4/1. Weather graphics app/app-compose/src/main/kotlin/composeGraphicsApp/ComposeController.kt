package composeGraphicsApp

import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.material.Button
import androidx.compose.material.Icon
import androidx.compose.material.Text
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Refresh
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.text.font.FontWeight
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherDataField
import weatherUtilities.WeatherDataState
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

object ComposeController {

	private const val PADDING_LENGTH = 5

	private const val SPACING_LENGTH = 7

	private const val SPACING_BETWEEN_IMAGES_LENGTH = 20

	private const val TEMPERATURE_FONT_SIZE = 30

	private const val WEATHER_DATA_TITLE =
		"Current weather conditions in Saint Petersburg according to %s"

	private const val WEATHER_DATA_CLOCK =
		"Data for %s"

	private val OPENWEATHERMAP_ORG_COLOR = Color(235, 110, 75, 255)

	private val TOMORROW_IO_COLOR = Color(0, 114, 245, 255)

	@Composable
	fun Screen(
		service: Int,
		data: List<WeatherDataState>,
		chooseService: (Int) -> Unit,
		updateData: () -> Unit,
	) {
		Column {
			Row(modifier = Modifier.background(Color.LightGray).fillMaxWidth().padding(all = PADDING_LENGTH.dp)) {
				Button(onClick = { chooseService(0) }) {
					Text("openweathermap.org", fontWeight = FontWeight.Bold)
				}
				Spacer(modifier = Modifier.width(PADDING_LENGTH.dp))
				Button(onClick = { chooseService(1) }) {
					Text("tomorrow.io", fontWeight = FontWeight.Bold)
				}
			}

			Title(service, data[service].time, updateData)
			ServiceWeather(service, data[service].data)
		}
	}

	@Composable
	fun ServiceWeather(service: Int, data: Map<WeatherCharacteristics, WeatherDataField>) {

		Column(
			modifier = Modifier
				.fillMaxSize()
				.background(if (service == 0) OPENWEATHERMAP_ORG_COLOR else TOMORROW_IO_COLOR),
			verticalArrangement = Arrangement.SpaceEvenly,
			horizontalAlignment = Alignment.CenterHorizontally
		) {

			Row(
				modifier = Modifier.fillMaxWidth(),
				verticalAlignment = Alignment.CenterVertically,
				horizontalArrangement = Arrangement.Center
			) {
				if (data[WeatherCharacteristics("Precipitation type")]?.str !in listOf("NO DATA", null)) {
					Image(
						painter = painterResource(
							convertPrecipitationCodeToImagePath(
								data[WeatherCharacteristics("Precipitation type")]!!.str
							)
						),
						contentDescription = null
					)
					Spacer(modifier = Modifier.width(SPACING_LENGTH.dp))
				}
				Text(
					data[WeatherCharacteristics("Temperature")]?.str ?: "",
					color = Color.White,
					fontSize = TEMPERATURE_FONT_SIZE.sp
				)
			}

			Row(
				modifier = Modifier.fillMaxSize(),
				verticalAlignment = Alignment.CenterVertically,
				horizontalArrangement = Arrangement.Center
			) {
				val dataToFill = data.toMutableMap()
				dataToFill.remove(WeatherCharacteristics("Temperature"))
				dataToFill.remove(WeatherCharacteristics("Precipitation type"))

				for ((characteristics, field) in dataToFill) {
					if (field.str != "NO DATA") {

						if (characteristics.name in listOf("Precipitation", "Wind", "Humidity", "Cloud cover")) Image(
							painter = painterResource(
								when (characteristics.name) {
									"Precipitation" -> "/weatherDataIcons/precipitationImage.png"
									"Wind" -> "/weatherDataIcons/windImage.png"
									"Humidity" -> "/weatherDataIcons/humidityImage.png"
									"Cloud cover" -> "/weatherDataIcons/cloudCoverImage.png"
									else -> ""
								}
							),
							contentDescription = null
						)
						else {
							Text("${characteristics.name}: ", fontWeight = FontWeight.Bold)
						}

						Spacer(modifier = Modifier.width(SPACING_LENGTH.dp))
						Text(field.str, color = Color.White)
						Spacer(modifier = Modifier.width(SPACING_BETWEEN_IMAGES_LENGTH.dp))
					}
				}
			}
		}
	}

	@Composable
	fun Title(service: Int, updateTime: String, updateData: () -> Unit) {
		Row(modifier = Modifier
			.fillMaxWidth()
			.background(if (service == 0) OPENWEATHERMAP_ORG_COLOR else TOMORROW_IO_COLOR)
			.padding(all = PADDING_LENGTH.dp), horizontalArrangement = Arrangement.SpaceBetween
		) {
			Column {
				val serviceName = if (service == 0) "openweathermap.org" else "tomorrow.io"
				Text(String.format(WEATHER_DATA_TITLE, serviceName), fontWeight = FontWeight.Bold)
				Text(String.format(WEATHER_DATA_CLOCK, updateTime), fontWeight = FontWeight.Bold)
			}
			Button(onClick = updateData) {
				Icon(Icons.Default.Refresh, null)
			}
		}
	}

	private fun convertPrecipitationCodeToImagePath(code: String): String {
		val time = LocalDateTime.now().format(DateTimeFormatter.ofPattern("HH")).toInt()

		val pairOfImages = when (code) {
			"800" -> Pair("/precipitationTypeIcons/clearImage.png", "/precipitationTypeIcons/clearImageNight.png")
			"801" -> Pair("/precipitationTypeIcons/cloudyImage.png", "/precipitationTypeIcons/cloudyImageNight.png")
			"802" -> Pair("/precipitationTypeIcons/cloudyImage.png", "/precipitationTypeIcons/cloudyImageNight.png")
			"803" -> Pair("/precipitationTypeIcons/cloudyImage.png", "/precipitationTypeIcons/cloudyImageNight.png")
			"804" -> Pair("/precipitationTypeIcons/fullCloudyImage.png",
				"/precipitationTypeIcons/fullCloudyImage.png")
			else -> {
				when (code.first()) {
					'2' -> Pair("/precipitationTypeIcons/thunderstormImage.png",
						"/precipitationTypeIcons/thunderstormImage.png")
					'3' -> Pair("/precipitationTypeIcons/rainImage.png", "/precipitationTypeIcons/rainImage.png")
					'4' -> Pair("/precipitationTypeIcons/rainImage.png", "/precipitationTypeIcons/rainImage.png")
					'5' -> Pair("/precipitationTypeIcons/snowImage.png", "/precipitationTypeIcons/snowImage.png")
					else -> Pair("/precipitationTypeIcons/fogImage.png", "/precipitationTypeIcons/fogImage.png")
				}
			}
		}

		return if (time in 6..21) {
			pairOfImages.first
		} else {
			pairOfImages.second
		}
	}

}