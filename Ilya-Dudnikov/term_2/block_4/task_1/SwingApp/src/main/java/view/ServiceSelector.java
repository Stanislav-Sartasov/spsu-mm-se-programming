package view;

import javax.swing.*;

public class ServiceSelector extends JMenuBar {
	private JMenu serviceMenu;
	private ButtonGroup buttonGroup;

	public ServiceSelector() {
		this("Services");
	}

	public ServiceSelector(String name) {
		serviceMenu = new JMenu(name);
		add(serviceMenu);

		buttonGroup = new ButtonGroup();
	}

	public void addService(String service) {
		var newItem = new JRadioButtonMenuItem(service);
		serviceMenu.add(newItem);
		if (serviceMenu.getItemCount() == 1) {
			serviceMenu.getItem(0).setSelected(true);
		}

		buttonGroup.add(newItem);
	}

	public void removeService(String service) {
		var newItem = new JRadioButtonMenuItem(service);
		serviceMenu.remove(newItem);
		buttonGroup.remove(newItem);
	}

	public String getSelectedService() {
		return (String) serviceMenu.getSelectedObjects()[0];
	}
}
