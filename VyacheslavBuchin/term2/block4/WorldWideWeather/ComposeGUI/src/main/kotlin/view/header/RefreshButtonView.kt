package view.header

import androidx.compose.foundation.clickable
import androidx.compose.material.Icon
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Refresh
import androidx.compose.runtime.*
import androidx.compose.ui.Modifier
import kotlin.concurrent.thread

@Composable
fun RefreshButtonView(onClick: () -> List<Thread>) {
	var available by remember { mutableStateOf(true) }
	Icon(Icons.Default.Refresh, "",
		modifier = Modifier
			.clickable {
				if (available) {
					thread {
						available = false
						val threads = onClick()
						threads.forEach { it.join() }
						available = true
					}
				}
			}
	)
}