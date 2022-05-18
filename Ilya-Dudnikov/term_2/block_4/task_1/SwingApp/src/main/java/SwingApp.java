import view.MainView;
import view.ServiceSelector;

import javax.swing.*;

public class SwingApp extends JFrame {
	public SwingApp() {
		var menuBar = new ServiceSelector();
		MainView mainView = new MainView(menuBar);
		setContentPane(mainView);
		setSize(800, 600);
		setResizable(false);

		setVisible(true);

		setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
	}

	public static void main(String[] args) {
		new SwingApp();
	}
}
