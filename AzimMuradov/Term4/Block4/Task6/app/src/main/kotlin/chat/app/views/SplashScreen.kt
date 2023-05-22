package chat.app.views

import androidx.compose.animation.core.Animatable
import androidx.compose.animation.core.spring
import androidx.compose.foundation.layout.Box
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.foundation.layout.size
import androidx.compose.material.Icon
import androidx.compose.material.MaterialTheme
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.rounded.MailOutline
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.alpha
import androidx.compose.ui.unit.dp
import chat.app.views.IconAnimationState.APPEARING
import chat.app.views.IconAnimationState.DISAPPEARING


@Composable
fun SplashScreen(onSplashScreenEnd: () -> Unit) {
    var iconAnimationState by remember { mutableStateOf(APPEARING) }
    val alpha = remember { Animatable(ICON_MIN_TRANSPARENCY) }

    LaunchedEffect(iconAnimationState) {
        alpha.animateTo(
            targetValue = when (iconAnimationState) {
                APPEARING -> ICON_MAX_TRANSPARENCY
                DISAPPEARING -> ICON_MIN_TRANSPARENCY
            },
            animationSpec = spring(ICON_ANIMATION_STIFFNESS)
        ) {
            when (iconAnimationState) {
                APPEARING -> if (alpha.value == ICON_MAX_TRANSPARENCY) {
                    iconAnimationState = DISAPPEARING
                }

                DISAPPEARING -> if (alpha.value == ICON_MIN_TRANSPARENCY) {
                    onSplashScreenEnd()
                }
            }
        }
    }

    Box(
        modifier = Modifier.fillMaxSize(),
        contentAlignment = Alignment.Center,
    ) {
        Icon(
            imageVector = Icons.Rounded.MailOutline,
            contentDescription = null,
            modifier = Modifier.size(200.dp).alpha(alpha.value),
            tint = MaterialTheme.colors.onBackground
        )
    }
}


private enum class IconAnimationState { APPEARING, DISAPPEARING }

private const val ICON_MIN_TRANSPARENCY = 0.0f
private const val ICON_MAX_TRANSPARENCY = 1.0f
private const val ICON_ANIMATION_STIFFNESS = 2f
