import org.apache.http.impl.client.CloseableHttpClient
import weatherUtilities.Location
import weatherUtilities.UnassembledWeatherDataField
import weatherUtilities.WeatherCharacteristics
import weatherWeb.WeatherWeb

internal class TestWeatherWeb(
	override val httpClient: CloseableHttpClient,
	override val appId: String,
	override val url: String,
	override val dataJSONInformation: Map<WeatherCharacteristics, UnassembledWeatherDataField>
	) : WeatherWeb() {

	override fun getRequestUrl(location: Location) = ""

}