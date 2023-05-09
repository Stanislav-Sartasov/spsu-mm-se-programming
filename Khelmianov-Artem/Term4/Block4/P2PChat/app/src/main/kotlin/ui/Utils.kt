package ui

import androidx.compose.foundation.border
import androidx.compose.material.darkColors
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.RectangleShape
import androidx.compose.ui.unit.dp
import net.utils.localAddress


internal fun Modifier.thinBorder() = this.border(0.dp, darkColors().primary, RectangleShape)
internal val pad = 4.dp

internal val DEFAULT_IP = localAddress
internal const val DEFAULT_PORT = 42069
internal const val DEFAULT_USERNAME = "User"

