package entity

class Temperature private constructor(degreesCelsius: Double) {

	val celsius = degreesCelsius
	val fahrenheit: Double
		get() = celsius * 9 / 5.0 + 32

	companion object {
		fun ofCelsius(degrees: Double) = Temperature(degrees)
		fun ofFahrenheit(degrees: Double) = ofCelsius((degrees - 32) * 5 / 9.0)
	}


}