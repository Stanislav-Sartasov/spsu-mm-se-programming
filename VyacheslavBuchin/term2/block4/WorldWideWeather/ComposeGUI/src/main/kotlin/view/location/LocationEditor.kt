package view.location

import androidx.compose.animation.*
import androidx.compose.foundation.clickable
import androidx.compose.foundation.gestures.Orientation
import androidx.compose.foundation.gestures.draggable
import androidx.compose.foundation.gestures.rememberDraggableState
import androidx.compose.foundation.gestures.scrollBy
import androidx.compose.foundation.layout.*
import androidx.compose.foundation.lazy.LazyRow
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.material.Icon
import androidx.compose.material.Text
import androidx.compose.material.TextField
import androidx.compose.material.icons.Icons
import androidx.compose.material.icons.filled.Add
import androidx.compose.material.icons.filled.Close
import androidx.compose.material.icons.rounded.Add
import androidx.compose.runtime.Composable
import androidx.compose.runtime.mutableStateOf
import androidx.compose.runtime.remember
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.ui.Alignment
import androidx.compose.ui.Modifier
import androidx.compose.ui.graphics.Color
import androidx.compose.ui.text.input.TextFieldValue
import androidx.compose.ui.unit.dp
import androidx.compose.ui.unit.sp
import kotlinx.coroutines.launch
import androidx.compose.runtime.setValue
import androidx.compose.runtime.getValue

@Composable
fun LocationEditorView(onLocationAdd: (String, String, String) -> Boolean) {
	val expanded = remember { mutableStateOf(false) }
	Column(
		modifier = Modifier
			.fillMaxWidth(),
		horizontalAlignment = Alignment.CenterHorizontally
	) {
		AnimatedVisibility(
			visible = expanded.value,
			enter = expandVertically(expandFrom = Alignment.Top) + fadeIn(),
			exit = shrinkVertically() + fadeOut()
		) {
			val scrollState = rememberLazyListState()
			val coroutineScope = rememberCoroutineScope()
			Row(
				horizontalArrangement = Arrangement.SpaceBetween,
				verticalAlignment = Alignment.CenterVertically,
				modifier = Modifier
					.fillMaxWidth()
					.draggable(
						orientation = Orientation.Horizontal,
						state = rememberDraggableState { delta ->
							coroutineScope.launch {
								scrollState.scrollBy(-delta)
							}
						},
					)
			) {
				val locationName = remember { mutableStateOf(TextFieldValue()) }
				val latitude = remember { mutableStateOf(TextFieldValue()) }
				val longitude = remember { mutableStateOf(TextFieldValue()) }
				var isError by remember { mutableStateOf(false) }
				Column {
					LazyRow(
						modifier = Modifier
							.height(70.dp)
							.padding(5.dp),
						horizontalArrangement = Arrangement.spacedBy(5.dp),
						verticalAlignment = Alignment.CenterVertically
					) {
						item {
							TextField(
								value = locationName.value,
								onValueChange = { locationName.value = it },
								singleLine = true,
								label = { Text("Location name") },
								modifier = Modifier
									.width(200.dp)
							)
						}
						item {
							TextField(
								value = latitude.value,
								onValueChange = { latitude.value = it },
								singleLine = true,
								label = { Text("Latitude") },
								modifier = Modifier
									.width(130.dp)
							)
						}
						item {
							TextField(
								value = longitude.value,
								onValueChange = { longitude.value = it },
								singleLine = true,
								label = { Text("Longitude") },
								modifier = Modifier
									.width(130.dp)
							)
						}
					}
					if (isError)
						ErrorMessage()
				}
				Row(
					verticalAlignment = Alignment.CenterVertically,
				) {
					val buttonModifier = Modifier.height(40.dp).width(40.dp)
					Icon(
						Icons.Filled.Add, "",
						modifier = buttonModifier
							.clickable {
								val isSuccess = onLocationAdd(
									locationName.value.text,
									latitude.value.text,
									longitude.value.text
								)
								if (isSuccess) {
									expanded.value = !expanded.value
									isError = false
								} else
									isError = true
							}
					)
					Icon(
						Icons.Filled.Close, "",
						modifier = buttonModifier
							.clickable {
								expanded.value = !expanded.value
							}
					)
				}
			}
		}
		if (!expanded.value) {
			Icon(Icons.Rounded.Add, "",
				modifier = Modifier
					.padding(5.dp)
					.clickable {
						expanded.value = !expanded.value
					}
					.height(40.dp)
					.width(40.dp)
			)
		}
	}
}

@Composable
fun ErrorMessage() {
	Text("Latitude and longitude fields should contain only numbers",
		fontSize = 15.sp,
		color = Color.Red
	)
}