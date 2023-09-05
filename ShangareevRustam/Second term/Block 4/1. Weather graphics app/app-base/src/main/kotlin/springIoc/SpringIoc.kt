package springIoc

import openweathermapOrg.OpenweathermapOrg
import org.apache.http.client.config.RequestConfig
import org.apache.http.impl.client.CloseableHttpClient
import org.apache.http.impl.client.HttpClientBuilder
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
	open fun client(): CloseableHttpClient {
		val timeout = 10
		val config = RequestConfig.custom()
			.setConnectTimeout(timeout * 1000)
			.setConnectionRequestTimeout(timeout * 1000)
			.setSocketTimeout(timeout * 1000).build()
		return HttpClientBuilder.create().setDefaultRequestConfig(config).build()
	}

	private companion object {
		private const val OPEN_WEATHER_MAP_API = "ad50b975ff7fc9b646f54287fb5c9033"

		private const val TOMORROW_IO_API = "9v8kjBEVTpjOfrBHtW2rwo6RtLI4SfH4"
	}
}