package meteo.app.presentation.views

import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.items
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import meteo.presentation.state.MeteoState

@Composable
fun Content(state: MeteoState) {
    Row(modifier = Modifier.fillMaxSize()) {
        when (state) {
            MeteoState.Uninitialised -> Unit
            is MeteoState.Initialised -> LazyColumn(
                contentPadding = PaddingValues(start = 16.dp, top = 16.dp, end = (16 + 5).dp, bottom = 16.dp),
                verticalArrangement = Arrangement.spacedBy(16.dp),
            ) {
                items(state.weatherList) {
                    ServiceWeatherCard(it)
                }
            }
        }
    }
}
