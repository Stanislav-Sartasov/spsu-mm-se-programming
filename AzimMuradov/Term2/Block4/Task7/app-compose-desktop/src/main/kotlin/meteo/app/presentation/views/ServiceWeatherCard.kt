package meteo.app.presentation.views

import androidx.compose.animation.core.*
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Check
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.rotate
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.painterResource
import androidx.compose.ui.unit.dp
import meteo.app.presentation.MeteoComposeMessagesWizard.loadingErrorMessage
import meteo.app.presentation.MeteoComposeMessagesWizard.toHumanReadable
import meteo.domain.entity.Weather
import meteo.presentation.state.LoadingState
import meteo.presentation.state.NamedValue

@Composable
fun ServiceWeatherCard(serviceWeatherInfo: NamedValue<LoadingState<Weather>>) {
    val (name, loadingState) = serviceWeatherInfo
    Card(Modifier.fillMaxWidth().height(320.dp), elevation = 4.dp) {
        Column(Modifier.fillMaxSize()) {
            Row(
                modifier = Modifier.fillMaxWidth().height(48.dp).background(Color.LightGray).padding(all = 8.dp),
                horizontalArrangement = Arrangement.SpaceBetween,
                verticalAlignment = Alignment.CenterVertically
            ) {
                Text(text = name)

                when (loadingState) {
                    LoadingState.Loading -> {
                        val transition = rememberInfiniteTransition()

                        val degrees by transition.animateFloat(
                            initialValue = 0.0f,
                            targetValue = 360.0f,
                            animationSpec = infiniteRepeatable(
                                animation = tween(durationMillis = 1000, easing = FastOutSlowInEasing),
                                repeatMode = RepeatMode.Restart
                            )
                        )

                        Icon(
                            painter = painterResource(resourcePath = "icons/loop_black_36dp.svg"),
                            contentDescription = null,
                            modifier = Modifier.height(24.dp).rotate(degrees),
                        )
                    }
                    is LoadingState.Success -> Icon(
                        imageVector = Icons.Default.Check,
                        contentDescription = null,
                        modifier = Modifier.height(24.dp),
                        tint = Color(color = 0xFF009900)
                    )
                    is LoadingState.Error -> Icon(
                        painter = painterResource(resourcePath = "icons/error_black_36dp.svg"),
                        contentDescription = null,
                        modifier = Modifier.height(24.dp),
                        tint = Color(color = 0xFF990000)
                    )
                }
            }

            Box(modifier = Modifier.fillMaxSize().padding(all = 8.dp)) {
                when (loadingState) {
                    LoadingState.Loading -> Unit
                    is LoadingState.Success -> Column(
                        modifier = Modifier.fillMaxSize(),
                        verticalArrangement = Arrangement.spacedBy(4.dp)
                    ) {
                        val rows = loadingState.value.toHumanReadable()

                        if (rows.isNotEmpty()) {
                            DataRow(rows.first(), modifier = Modifier.weight(weight = 1f))
                            rows.subList(1, rows.size).forEach {
                                Divider()
                                DataRow(it, modifier = Modifier.weight(weight = 1f))
                            }
                        }
                    }
                    is LoadingState.Error -> Text(text = loadingErrorMessage(loadingState))
                }
            }
        }
    }
}
