package meteo.app.presentation.views

import androidx.compose.foundation.layout.*
import androidx.compose.runtime.Composable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import meteo.app.presentation.MeteoComposeMessagesWizard.isLoading
import meteo.presentation.state.MeteoState

@Composable
fun Screen(state: MeteoState, onLoad: () -> Unit) {
    Column(modifier = Modifier.fillMaxSize()) {
        Spacer(Modifier.height(64.dp))
        Content(state)
    }
    Header(isLoading = state.isLoading, onLoad = onLoad)
}
