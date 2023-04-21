import org.junit.jupiter.api.Test
import weatherApp.WeatherApp

internal class WeatherAppTests {

	@Test
	fun runTest() {
		try {
			WeatherApp.run("xfewfw", "v432ijo", "h", "r", "r", "r", "vrgtr", "h", "r", "h", "q")
		}
		catch (e: Exception) {
			assert(false)
		}
		assert(true)
	}

}