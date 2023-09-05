package view.location

import WeatherReportView
import androidx.compose.animation.*
import androidx.compose.foundation.background
import androidx.compose.foundation.clickable
import androidx.compose.foundation.gestures.*
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyRow
import androidx.compose.foundation.lazy.itemsIndexed
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.material.*
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.*
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.graphics.Color
import entity.Location
import kotlinx.coroutines.launch
import report.WeatherReport

@Composable
fun LocationView(location: Location, reports: List<Pair<String, WeatherReport>>, onCloseButtonClick: (Location) -> Unit) {
	val expanded = remember { mutableStateOf(false) }
	Box(Modifier.fillMaxWidth()) {
		Column(Modifier
			.fillMaxWidth(),
			Arrangement.spacedBy(5.dp),
			Alignment.Start
		) {
			Row(
				Modifier
					.fillMaxWidth()
					.clickable {
						expanded.value = !expanded.value
					}
					.background(Color.LightGray)
					.padding(10.dp),
				verticalAlignment = Alignment.CenterVertically,
				horizontalArrangement = Arrangement.SpaceBetween
			) {
				Text(location.name)
				Row(
					verticalAlignment = Alignment.CenterVertically
				) {
					Icon(
						if (expanded.value) Icons.Filled.KeyboardArrowDown else Icons.Filled.KeyboardArrowRight,
						contentDescription = ""
					)
					Icon(
						Icons.Filled.Close, "",
						modifier = Modifier.width(20.dp).height(20.dp)
							.clickable {
								onCloseButtonClick(location)
							}
					)
				}
			}
			AnimatedVisibility(
				visible = expanded.value,
				enter = expandVertically(expandFrom = Alignment.Top) + fadeIn(),
				exit = shrinkVertically() + fadeOut()
			) {
				if (reports.isNotEmpty()) {
					val scrollState = rememberLazyListState()
					val coroutineScope = rememberCoroutineScope()
					LazyRow(
						state = scrollState,
						modifier = Modifier
							.draggable(
								orientation = Orientation.Horizontal,
								state = rememberDraggableState { delta ->
									coroutineScope.launch {
										scrollState.scrollBy(-delta)
									}
								},
							)
					) {
						itemsIndexed(reports) { index, item ->
							WeatherReportView(item.first, item.second)
							if (index < reports.lastIndex)
								Divider(thickness = 1.dp)
						}
					}
				} else
					Box(
						Modifier.fillMaxWidth(),
						Alignment.Center
					) {
						LoadingView(Modifier.padding(60.dp))
					}
			}

		}
	}
}