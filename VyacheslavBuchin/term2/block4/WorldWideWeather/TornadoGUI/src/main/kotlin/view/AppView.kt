package view

import controller.ReportController
import controller.ServiceController
import entity.Location
import tornadofx.*
import view.location.LocationView
import view.service.ServicesView

class AppView: View() {
	private val location = Location("Saint-Petersburg", 59.94, 30.313)
	private val reportController = ReportController()
	private val serviceController = ServiceController()
	private val locationView = LocationView(location, reportController)

	init {
		updateData()
	}

	override val root = borderpane {
		left = vbox(10) {
			add(ServicesView(serviceController))
			button("REFRESH") {
				action { updateData() }
			}
		}
		center = vbox(10) {
			add(locationView)
		}
	}

	private fun updateData() {
		reportController.refreshReportsIn(location, serviceController.selectedServices)
	}

}