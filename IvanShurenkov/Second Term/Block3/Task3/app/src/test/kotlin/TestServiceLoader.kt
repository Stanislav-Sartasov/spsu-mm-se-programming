import lib.weather.IWeatherApi
import lib.weather.tomorrow.WeatherTomorrow
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertNotNull
import kotlin.test.assertNull

class TestServiceLoader {
    @Test
    fun `Test load tomorrow`() {
        val service: IWeatherApi? = ServiceLoader.loadService("src/test/resources/jars/tomorrow.jar")
        assertNotNull(service)
        assertEquals(WeatherTomorrow, service)
    }

    @Test
    fun `Jar file doesn't exist`() {
        val service =
            ServiceLoader.loadService("src/test/resources/jars/some.jar")
        assertNull(service)
    }

    @Test
    fun `Jar file hasn't IWeatherApi`() {
        val service =
            ServiceLoader.loadService("src/test/resources/jars/some2.jar")
        assertNull(service)
    }
}
