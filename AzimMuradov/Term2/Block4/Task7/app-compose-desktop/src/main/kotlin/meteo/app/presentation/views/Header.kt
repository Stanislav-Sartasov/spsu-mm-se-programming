package meteo.app.presentation.views

import androidx.compose.animation.core.*
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.*
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Refresh
import androidx.compose.runtime.Composable
import androidx.compose.runtime.getValue
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.rotate
import androidx.compose.ui.draw.shadow
import androidx.compose.ui.unit.dp
import meteo.app.presentation.MeteoComposeMessagesWizard

@Composable
fun Header(isLoading: Boolean, onLoad: () -> Unit) {
    Row(
        modifier = Modifier
            .fillMaxWidth()
            .height(64.dp)
            .shadow(elevation = 8.dp)
            .background(MaterialTheme.colors.primary)
            .padding(horizontal = 16.dp),
        horizontalArrangement = Arrangement.SpaceBetween,
        verticalAlignment = Alignment.CenterVertically
    ) {
        Text(text = MeteoComposeMessagesWizard.METEO, color = MaterialTheme.colors.onPrimary)

        val transition = rememberInfiniteTransition()

        val degrees by transition.animateFloat(
            initialValue = 0.0f,
            targetValue = if (isLoading) 360.0f else 0.0f,
            animationSpec = infiniteRepeatable(
                animation = tween(durationMillis = 1000, easing = FastOutSlowInEasing),
                repeatMode = RepeatMode.Restart
            )
        )

        IconButton(onClick = onLoad, enabled = !isLoading) {
            Icon(
                imageVector = Icons.Default.Refresh,
                contentDescription = null,
                modifier = Modifier.height(32.dp).rotate(degrees),
                tint = MaterialTheme.colors.onPrimary
            )
        }
    }
}
