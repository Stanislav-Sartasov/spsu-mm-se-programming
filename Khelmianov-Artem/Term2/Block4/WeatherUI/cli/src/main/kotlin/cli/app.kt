package cli

import lib.weather.*
import io.ktor.client.*
import io.ktor.client.plugins.*
import io.ktor.client.plugins.contentnegotiation.*
import io.ktor.serialization.kotlinx.json.*
import kotlinx.coroutines.runBlocking
import kotlinx.serialization.json.Json
import org.koin.core.component.KoinComponent
import org.koin.core.component.inject

class App : KoinComponent {
    private var location = Coordinates(59.9F, 30.3F)
    private val apis by inject<List<IWeatherAPI>>()
    private lateinit var currentApi: IWeatherAPI
    private var data = WeatherData()
    private val client = HttpClient {
        expectSuccess = true
        install(HttpTimeout) {
            requestTimeoutMillis = 3000
        }
        install(ContentNegotiation) {
            json(Json {
                prettyPrint = true
                ignoreUnknownKeys = true
            })
        }
    }

    private fun printHelp() = println(
        """
        | h - print this message
        | u - fetch weather info
        | a - change api
        | l - change location
        | e - exit
    """.trimMargin()
    )

    private fun info() =
        println("Current settings: source - ${currentApi.name}, location - ${location.lat}, ${location.lon}")

    private fun setLocation() {
        try {
            println("Enter new location (2 floats, space separated)")
            val (lat, lon) = readln().split(" ").map { it.toFloat() }
            location = Coordinates(lat, lon)
            info()
        } catch (e: Exception) {
            println(e)
        }
    }

    private fun changeApi() {
        try {
            println("Available APIs:")
            apis.forEachIndexed { index, iWeatherAPI -> println("$index: ${iWeatherAPI.name}") }
            currentApi = apis[readln().toInt().coerceIn(0..apis.lastIndex)]
            info()
        } catch (e: NumberFormatException) {
            println(e)
        }
    }

    private fun update() {
        try {
            runBlocking {
                data = currentApi.get(location, client)
            }
            println(data.format())
        } catch (e: ClientRequestException) {
            System.err.println("Http response status code isn't successful (>=300)")
            System.err.println(e.message)
        } catch (e: Exception) {
            println(e)
        }
    }

    fun run() {
        currentApi = apis[0]
        printHelp()
        info()
        while (true) {
            print(">>> ")
            when (readln()) {
                "u" -> update()
                "a" -> changeApi()
                "l" -> setLocation()
                "h" -> printHelp()
                "e" -> break
            }
        }
    }
}