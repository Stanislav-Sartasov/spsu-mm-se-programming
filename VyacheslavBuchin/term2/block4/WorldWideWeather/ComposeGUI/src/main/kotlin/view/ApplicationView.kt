package view

import androidx.compose.foundation.gestures.Orientation
import androidx.compose.foundation.gestures.draggable
import androidx.compose.foundation.gestures.rememberDraggableState
import androidx.compose.foundation.gestures.scrollBy
import androidx.compose.foundation.layout.Column
import androidx.compose.foundation.lazy.LazyColumn
import androidx.compose.foundation.lazy.rememberLazyListState
import androidx.compose.material.MaterialTheme
import androidx.compose.runtime.rememberCoroutineScope
import androidx.compose.runtime.saveable.rememberSaveable
import androidx.compose.ui.Modifier
import androidx.compose.ui.unit.dp
import androidx.compose.ui.window.Window
import androidx.compose.ui.window.application
import androidx.compose.ui.window.rememberWindowState
import entity.Location
import kotlinx.coroutines.launch
import view.header.HeaderView
import view.location.LocationEditorView
import view.location.LocationsView
import view_model.location.LocationViewModel
import view_model.report.ReportsViewModel
import view_model.service.ServiceViewModel

fun ApplicationView() {
	val locationViewModel = LocationViewModel()
	val serviceViewModel = ServiceViewModel()
	updateLocationsWithServices(locationViewModel.locations, serviceViewModel)
	application {
		Window(
			onCloseRequest = ::exitApplication,
			title = "World Wide Weather",
			state = rememberWindowState(width = 600.dp, height = 600.dp),
		) {
			MaterialTheme {
				Column {
					val scrollState = rememberLazyListState()
					val coroutineScope = rememberCoroutineScope()
					val locationsWithReports = rememberSaveable { locationViewModel.locations }
					val availableServices = serviceViewModel.availableServices

					HeaderView(availableServices, serviceViewModel) {
						updateLocationsWithServices(locationsWithReports, serviceViewModel)
					}

					LazyColumn(
						modifier = Modifier
							.draggable(
								orientation = Orientation.Vertical,
								state = rememberDraggableState { delta ->
									coroutineScope.launch {
										scrollState.scrollBy(-delta)
									}
								},
							)
					) {
						item {
							LocationsView(locationsWithReports, locationViewModel::remove)
						}
						item {
							LocationEditorView { name, lat, lon ->
								val isSuccessful = locationViewModel.add(name, lat, lon)
								if (isSuccessful) {
									val (location, reportViewModel) = locationsWithReports.last()
									reportViewModel.updateReports(location, serviceViewModel.selectedServices)
								}
								isSuccessful
							}
						}
					}
				}
			}
		}
	}
}

private fun updateLocationsWithServices(
	locationsWithReports: List<Pair<Location, ReportsViewModel>>,
	serviceViewModel: ServiceViewModel
) : List<Thread> {
	return locationsWithReports.map { (location, reportsViewModel) ->
		reportsViewModel.updateReports(location, serviceViewModel.selectedServices)
	}
}
