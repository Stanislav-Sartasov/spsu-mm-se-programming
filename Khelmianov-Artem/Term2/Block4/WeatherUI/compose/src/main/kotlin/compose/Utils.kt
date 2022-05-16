package compose

import androidx.compose.foundation.layout.height
import androidx.compose.foundation.layout.width
import androidx.compose.material.TextField
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Modifier
import androidx.compose.ui.draw.scale
import androidx.compose.ui.unit.dp


@Composable
fun FloatField(
    value: Float?,
    onNumberChange: (Float) -> Unit,
    func: Float.() -> Float = { this }
) {
    val number = remember { mutableStateOf(value) }
    val textValue = remember(value != number.value) {
        number.value = value
        mutableStateOf(value.toString())
    }

    val numberRegex = remember { "[-]?[\\d]*[.]?[\\d]{0,3}".toRegex() }

    TextField(
        value = textValue.value,
        onValueChange = {
            if (numberRegex.matches(it)) {
                number.value = it.toFloatOrNull()?.func() ?: 0f
                textValue.value = number.value.toString()
                onNumberChange(number.value!!)
            }
        },
        modifier = Modifier.height(45.dp).scale(0.9F, 0.9F).width(100.dp),
        singleLine = true,
    )
}




