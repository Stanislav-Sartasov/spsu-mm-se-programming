package meteo.app.presentation

import androidx.compose.material.MaterialTheme
import androidx.compose.material.lightColors
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.unit.DpSize
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.*
import meteo.app.presentation.MeteoComposeMessagesWizard.METEO
import meteo.app.presentation.views.Screen
import meteo.presentation.mvi.MviStore
import meteo.presentation.state.MeteoState
import meteo.presentation.wish.MeteoWish

class MeteoComposeApp(private val store: MviStore<MeteoWish, MeteoState>) {

    fun run() = singleWindowApplication(
        state = WindowState(
            position = WindowPosition(Alignment.Center),
            size = DpSize(width = 800.dp, height = 800.dp)
        ),
        title = METEO,
        resizable = false
    ) {
        val state by store.state.collectAsState()

        MaterialTheme(colors = lightColors()) {
            Screen(state, onLoad = { store.consume(MeteoWish.Load) })
        }

        LaunchedEffect(key1 = Unit) {
            store.consume(MeteoWish.Load)
        }
    }
}
