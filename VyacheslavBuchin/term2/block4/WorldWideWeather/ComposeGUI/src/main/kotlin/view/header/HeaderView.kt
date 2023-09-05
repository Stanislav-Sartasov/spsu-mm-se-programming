package view.header

import androidx.compose.foundation.Image
import androidx.compose.foundation.background
import androidx.compose.foundation.layout.Arrangement
import androidx.compose.foundation.layout.Row
import androidx.compose.foundation.layout.fillMaxWidth
import androidx.compose.foundation.layout.height
import androidx.compose.material.Text
import androidx.compose.material.TopAppBar
import androidx.compose.runtime.Composable
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.res.loadImageBitmap
import androidx.compose.ui.text.style.TextAlign
import androidx.compose.ui.unit.dp
import service.weather.WeatherService
import view_model.service.ServiceViewModel
import java.io.File

@Composable
fun HeaderView(services: List<WeatherService>, serviceViewModel: ServiceViewModel, onRefreshClick: () -> List<Thread>) {
	TopAppBar(
		modifier = Modifier
			.fillMaxWidth()
			.height(50.dp)
			.background(Color.Blue),
	) {
		Row(
			horizontalArrangement = Arrangement.End
		) {
			Image(bitmap = loadImageBitmap(File("ComposeGUI/src/main/resources/icon/beagle.png").inputStream()),
				"cute beagle"
			)
			Row(
				horizontalArrangement = Arrangement.SpaceBetween,
				verticalAlignment = Alignment.CenterVertically,
				modifier = Modifier.fillMaxWidth()
			) {
				Text("World Wide Weather", textAlign = TextAlign.Left)
				Row(
					verticalAlignment = Alignment.CenterVertically
				) {
					RefreshButtonView(onRefreshClick)
					SettingsView(services, serviceViewModel)
				}
			}
		}
	}
}
