package service.weather

import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

abstract class WeatherServiceTest {
	protected abstract val service: WeatherService
	protected abstract val referenceName: String

	@Test
	fun `name of weather service should be the same as the name of service`() {
		assertEquals(referenceName, service.name)
	}


}