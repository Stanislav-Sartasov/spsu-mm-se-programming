package compose

import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.runtime.getValue
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.DpSize
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.WindowState
import androidx.compose.ui.window.singleWindowApplication
import io.ktor.client.*
import io.ktor.client.plugins.*
import io.ktor.client.plugins.contentnegotiation.*
import io.ktor.serialization.kotlinx.json.*
import kotlinx.coroutines.launch
import kotlinx.coroutines.runBlocking
import kotlinx.serialization.json.Json
import lib.weather.Coordinates
import lib.weather.IWeatherAPI
import lib.weather.WeatherData
import lib.weather.format
import org.koin.core.component.KoinComponent
import org.koin.core.component.inject


class Compose : KoinComponent {
    private val apis by inject<List<IWeatherAPI>>()
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

    fun run() = runBlocking {
        singleWindowApplication(
            state = WindowState(size = DpSize(500.dp, 300.dp))
        ) {
            Box{
                var data by remember { mutableStateOf(Result.success(WeatherData())) }
                var currentApi by remember { mutableStateOf(apis[0]) }

                var lat by remember { mutableStateOf(59.9f) }
                var lon by remember { mutableStateOf(30.3f) }

                fun onSelectedChange(api: IWeatherAPI) {
                    currentApi = api
                }

                suspend fun get() {
                    try {
                        data = Result.success(currentApi.get(Coordinates(lat, lon), client))
                    } catch (e: Exception) {
                        data = Result.failure(e)
                    }
                }

                MaterialTheme() {
                    Column(modifier = Modifier.padding(10.dp)) {
                        Row(
                            verticalAlignment = Alignment.CenterVertically,
                        ) {
                            // Get
                            Button(
                                onClick = { launch { get() } },
                                modifier = Modifier.height(60.dp),
                            ) {
                                Text("Get")
                            }

                            // APIs
                            Column {
                                apis.forEach { api ->
                                    Row(
                                        modifier = Modifier
                                            .height(30.dp)
                                            .padding(horizontal = 5.dp),
                                        verticalAlignment = Alignment.CenterVertically
                                    ) {
                                        RadioButton(
                                            selected = (api == currentApi),
                                            onClick = { onSelectedChange(api) }
                                        )
                                        Text(
                                            text = api.name,
                                            style = MaterialTheme.typography.body1.merge(),
                                            modifier = Modifier.padding(start = 5.dp),
                                        )
                                    }
                                }
                            }

                            // Coordinates
                            Text("Lat:")
                            FloatField(lat, { lat = it }, { coerceIn(-90f..90f) })
                            Text("Lon:")
                            FloatField(lon, { lon = it }, { coerceIn(-180f..180f) })
                        }

                        Divider(thickness = 5.dp, modifier = Modifier.padding(10.dp))

                        if (data.isSuccess) {
                            Card(elevation = 5.dp) {
                                Column(modifier = Modifier.padding(5.dp)) {
                                    for (x in data.getOrDefault(WeatherData()).format().split("\n")) {
                                        Row(
                                            modifier = Modifier.fillMaxWidth(),
                                            horizontalArrangement = Arrangement.SpaceBetween,
                                            verticalAlignment = Alignment.CenterVertically
                                        ) {
                                            Text(x.split(":")[0])
                                            Text(x.split(":")[1])
                                        }
                                        Divider()
                                    }
                                }
                            }
                        } else {
                            Card(elevation = 5.dp) {
                                    Text("Error occurred:\n${data.exceptionOrNull()!!.message}")
                            }
                        }
                    }
                }
            }
        }
    }
}