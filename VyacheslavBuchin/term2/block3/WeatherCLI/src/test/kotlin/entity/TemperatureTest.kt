package entity

import org.junit.jupiter.api.*
import org.junit.jupiter.api.Assertions.*

internal class TemperatureTest {

	@Test
	fun `temperature should stay same after translating it into fahrenheit and back in celsius`() {
		val temperature1 = Temperature.ofCelsius(42.0)
		val temperature2 = Temperature.ofFahrenheit(temperature1.fahrenheit)
		assertEquals(temperature1.celsius, temperature2.celsius)
	}

	@Test
	fun `temperature of 0 degrees Celsius should be 32 degrees Fahrenheit`() {
		val temperatureCelsius = Temperature.ofCelsius(0.0)
		assertEquals(temperatureCelsius.fahrenheit, 32.0)
	}
}