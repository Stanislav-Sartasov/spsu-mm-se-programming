package view.location

import androidx.compose.foundation.layout.Column
import androidx.compose.runtime.Composable
import entity.Location
import view_model.report.ReportsViewModel

@Composable
fun LocationsView(locationsWithReports: List<Pair<Location, ReportsViewModel>>, onCloseButtonClick: (Location) -> Unit) {
	Column {
		locationsWithReports.forEach {
			LocationView(it.first, it.second.reports, onCloseButtonClick)
		}
	}
}