package weatherApp

import openweathermapOrg.OpenweathermapOrg
import org.apache.http.impl.client.CloseableHttpClient
import org.apache.http.impl.client.HttpClients
import org.springframework.context.annotation.Bean
import org.springframework.context.annotation.Configuration
import tomorrowIo.TomorrowIo
import weatherWeb.WeatherWeb

@Configuration
open class SpringIoc {

	@Bean
	open fun tomorrowIo(): WeatherWeb = TomorrowIo(TOMORROW_IO_API, client())

	@Bean
	open fun openweathermapOrg(): WeatherWeb = OpenweathermapOrg(OPEN_WEATHER_MAP_API, client())

	@Bean
	open fun client(): CloseableHttpClient = HttpClients.createDefault()

	private companion object {
		private const val OPEN_WEATHER_MAP_API = "ad50b975ff7fc9b646f54287fb5c9033"

		private const val TOMORROW_IO_API = "9v8kjBEVTpjOfrBHtW2rwo6RtLI4SfH4"
	}
}