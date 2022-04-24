package meteo.app.presentation.views

import androidx.compose.foundation.layout.*
import androidx.compose.material.Text
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import meteo.presentation.state.NamedValue

@Composable
fun DataRow(namedValue: NamedValue<String>, modifier: Modifier = Modifier) {
    val (name, value) = namedValue
    Row(
        modifier = modifier.fillMaxWidth(),
        horizontalArrangement = Arrangement.SpaceBetween,
        verticalAlignment = Alignment.CenterVertically
    ) {
        Text(name)
        Text(value)
    }
}
