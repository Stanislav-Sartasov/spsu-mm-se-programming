package view.header

import androidx.compose.foundation.layout.Box
import androidx.compose.material.*
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.MoreVert
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.setValue
import androidx.compose.runtime.getValue
import service.weather.WeatherService
import view_model.service.ServiceViewModel

@Composable
fun SettingsView(services: List<WeatherService>, serviceViewModel: ServiceViewModel) {
	var isExpanded by remember { mutableStateOf(false) }
	Box {
		IconButton(onClick = { isExpanded = !isExpanded }) {
			Icon(Icons.Default.MoreVert, contentDescription = "")
		}
		DropdownMenu(
			expanded = isExpanded,
			onDismissRequest = { isExpanded = false }
		) {
			services.forEach {
				ServiceCheckBox(it, serviceViewModel)
			}
		}
	}
}