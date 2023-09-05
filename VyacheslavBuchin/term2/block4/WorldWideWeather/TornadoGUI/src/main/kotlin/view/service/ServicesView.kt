package view.service

import controller.ServiceController
import tornadofx.*

class ServicesView(private val serviceController: ServiceController) : View() {
	override val root = vbox {
		serviceController.availableServices.forEach {
			checkbox(it.name) {
				isSelected = serviceController.isSelected(it)
				action { serviceController.setSelected(it, isSelected) }
			}
		}
	}
}