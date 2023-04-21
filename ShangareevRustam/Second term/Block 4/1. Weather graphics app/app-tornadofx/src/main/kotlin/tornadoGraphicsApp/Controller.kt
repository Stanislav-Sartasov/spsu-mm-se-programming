package tornadoGraphicsApp

import javafx.fxml.FXML
import javafx.scene.control.Button
import javafx.scene.image.Image
import javafx.scene.image.ImageView
import javafx.scene.text.Text
import springIoc.SpringIoc
import weatherDataFormatter.WeatherDataFormatter
import weatherUtilities.Location
import weatherUtilities.WeatherCharacteristics
import weatherUtilities.WeatherDataField
import java.time.LocalDateTime
import java.time.format.DateTimeFormatter

internal class Controller {

	private companion object {

		private const val ST_PETERSBURG_LAT = 59.9055

		private const val ST_PETERSBURG_LON = 30.3007

		private const val SERVICE_UNAVAILABLE_MESSAGE =
			"Service is unavailable!"

		private val container = SpringIoc()

		private val openweathermapOrg = container.openweathermapOrg()

		private val tomorrowIo = container.tomorrowIo()

		private val fieldList = listOf(
			WeatherCharacteristics("Temperature"),
			WeatherCharacteristics("Cloud cover"),
			WeatherCharacteristics("Humidity"),
			WeatherCharacteristics("Precipitation type"),
			WeatherCharacteristics("Wind direction"),
			WeatherCharacteristics("Wind speed"),
			WeatherCharacteristics("Precipitation")
		)

		private fun convertPrecipitationCodeToImage(code: String): Image {
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
				Image(pairOfImages.first)
			} else {
				Image(pairOfImages.second)
			}
		}


		private fun fillWeatherData(
			dataToFill: Map<WeatherCharacteristics, WeatherDataField>,
			serviceWarningMessage: Text,
			serviceWeatherImage: ImageView,
			serviceTemperature: Text,
			serviceTime: Text,
			serviceDataFields: List<Pair<ImageView, Text>>,
		) {

			val data = dataToFill.toMutableMap()
			val fields = serviceDataFields.toMutableList()

			serviceTime.text = LocalDateTime.now().format(DateTimeFormatter.ofPattern("HH:mm"))

			if (data[WeatherCharacteristics("Temperature")]?.str in listOf("NO DATA", null)) {
				serviceWarningMessage.text = SERVICE_UNAVAILABLE_MESSAGE
				return
			}

			serviceTemperature.text = data[WeatherCharacteristics("Temperature")]!!.str
			data.remove(WeatherCharacteristics("Temperature"))

			val precipitationType = data[WeatherCharacteristics("Precipitation type")]?.str
			data.remove(WeatherCharacteristics("Precipitation type"))

			if (precipitationType !in listOf("NO DATA", null)) {
				serviceWeatherImage.image = convertPrecipitationCodeToImage(precipitationType!!)
			}

			for ((characteristics, field) in data) {
				if (field.str != "NO DATA") {
					val sceneField = fields.removeFirst()

					sceneField.first.image = Image(
						when (characteristics.name) {
							"Precipitation" -> "/weatherDataIcons/precipitationImage.png"
							"Wind" -> "/weatherDataIcons/windImage.png"
							"Humidity" -> "/weatherDataIcons/humidityImage.png"
							"Cloud cover" -> "/weatherDataIcons/cloudCoverImage.png"
							else -> ""
						}
					)
					sceneField.second.text = field.str
				}
			}
		}
	}

	@FXML
	private lateinit var firstServiceData1: Text

	@FXML
	private lateinit var firstServiceData2: Text

	@FXML
	private lateinit var firstServiceData3: Text

	@FXML
	private lateinit var firstServiceData4: Text

	@FXML
	private lateinit var firstServiceImage1: ImageView

	@FXML
	private lateinit var firstServiceImage2: ImageView

	@FXML
	private lateinit var firstServiceImage3: ImageView

	@FXML
	private lateinit var firstServiceImage4: ImageView

	@FXML
	private lateinit var firstServiceName: Text

	@FXML
	private lateinit var firstServiceRefreshButton: Button

	@FXML
	private lateinit var firstServiceTemperature: Text

	@FXML
	private lateinit var firstServiceTime: Text

	@FXML
	private lateinit var firstServiceWarningMessage: Text

	@FXML
	private lateinit var firstServiceWeatherImage: ImageView

	@FXML
	private lateinit var secondServiceData1: Text

	@FXML
	private lateinit var secondServiceData2: Text

	@FXML
	private lateinit var secondServiceData3: Text

	@FXML
	private lateinit var secondServiceData4: Text

	@FXML
	private lateinit var secondServiceImage1: ImageView

	@FXML
	private lateinit var secondServiceImage2: ImageView

	@FXML
	private lateinit var secondServiceImage3: ImageView

	@FXML
	private lateinit var secondServiceImage4: ImageView

	@FXML
	private lateinit var secondServiceName: Text

	@FXML
	private lateinit var secondServiceRefreshButton: Button

	@FXML
	private lateinit var secondServiceTemperature: Text

	@FXML
	private lateinit var secondServiceTime: Text

	@FXML
	private lateinit var secondServiceWarningMessage: Text

	@FXML
	private lateinit var secondServiceWeatherImage: ImageView

	@FXML
	private fun clickOnSecondServiceRefreshButton() {

		if (secondServiceTime.text == LocalDateTime.now().format(DateTimeFormatter.ofPattern("HH:mm"))) {
			return
		}

		secondServiceWarningMessage.text = ""

		val secondServiceData = WeatherDataFormatter.formatWeatherData(
			tomorrowIo.getWeatherData(
				fieldList,
				Location(ST_PETERSBURG_LAT, ST_PETERSBURG_LON)
			)
		)

		fillWeatherData(
			secondServiceData,
			secondServiceWarningMessage,
			secondServiceWeatherImage,
			secondServiceTemperature,
			secondServiceTime,
			listOf(
				Pair(secondServiceImage1, secondServiceData1),
				Pair(secondServiceImage2, secondServiceData2),
				Pair(secondServiceImage3, secondServiceData3),
				Pair(secondServiceImage4, secondServiceData4),
			)
		)
	}

	@FXML
	private fun clickOnFirstServiceRefreshButton() {

		if (firstServiceTime.text == LocalDateTime.now().format(DateTimeFormatter.ofPattern("HH:mm"))) {
			return
		}

		firstServiceWarningMessage.text = ""

		val firstServiceData = WeatherDataFormatter.formatWeatherData(
			openweathermapOrg.getWeatherData(
				fieldList,
				Location(ST_PETERSBURG_LAT, ST_PETERSBURG_LON)
			)
		)

		fillWeatherData(
			firstServiceData,
			firstServiceWarningMessage,
			firstServiceWeatherImage,
			firstServiceTemperature,
			firstServiceTime,
			listOf(
				Pair(firstServiceImage1, firstServiceData1),
				Pair(firstServiceImage2, firstServiceData2),
				Pair(firstServiceImage3, firstServiceData3),
				Pair(firstServiceImage4, firstServiceData4)
			)
		)
	}
}