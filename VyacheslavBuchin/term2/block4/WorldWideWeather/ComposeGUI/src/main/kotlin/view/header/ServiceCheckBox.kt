package view.header

import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.padding
import androidx.compose.material.Checkbox
import androidx.compose.material.Text
import androidx.compose.runtime.*
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import service.weather.WeatherService
import view_model.service.ServiceViewModel


@Composable
fun ServiceCheckBox(service: WeatherService, serviceViewModel: ServiceViewModel) {
	Row(
		modifier = Modifier.padding(5.dp),
		horizontalArrangement = Arrangement.spacedBy(5.dp),
		verticalAlignment = Alignment.CenterVertically
	) {
		var isChecked by remember { mutableStateOf(serviceViewModel.isSelected(service)) }
		Checkbox(
			checked = isChecked,
			onCheckedChange = {
				isChecked = !isChecked
				serviceViewModel.setSelected(service, it)
			}
		)
		Text(service.name)
	}
}