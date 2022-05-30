package view_model.location

import androidx.compose.runtime.mutableStateListOf
import entity.Location
import view_model.report.ReportsViewModel

class LocationViewModel {

	val locations = mutableStateListOf(Location("Saint-Petersburg", 59.94, 30.313) to ReportsViewModel())

	fun add(name: String, latitude: String, longitude: String): Boolean {
		val lon = longitude.toDoubleOrNull()
		val lat = latitude.toDoubleOrNull()
		if (lon == null || lat == null)
			return false
		locations.add(Location(name, lat, lon) to ReportsViewModel())
		return true
	}

	fun remove(location: Location) {
		locations.removeIf { it.first == location }
	}
}