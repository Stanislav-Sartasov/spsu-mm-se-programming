package view;

import javafx.scene.control.Menu;
import javafx.scene.control.MenuBar;
import javafx.scene.control.RadioMenuItem;
import javafx.scene.control.ToggleGroup;

public class ServiceSelector extends MenuBar {
	private	ToggleGroup toggleGroup;
	private Menu menu;

	public ServiceSelector() {
		this("Services");
	}

	public ServiceSelector(String name) {
		toggleGroup = new ToggleGroup();
		menu = new Menu(name);
		getMenus().add(menu);
	}

	public void addService(String name) {
		var newItem = new RadioMenuItem(name);
		toggleGroup.getToggles().add(newItem);
		menu.getItems().add(newItem);

		if (toggleGroup.getToggles().size() == 1) {
			toggleGroup.selectToggle(toggleGroup.getToggles().get(0));
		}
	}

	public void removeService(String name) {
		var newItem = new RadioMenuItem(name);
		toggleGroup.getToggles().remove(newItem);
		menu.getItems().remove(newItem);
	}

	public String getSelectedService() {
		return ((RadioMenuItem)toggleGroup.getSelectedToggle()).getText();
	}
}
