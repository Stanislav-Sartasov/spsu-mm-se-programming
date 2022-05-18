package view;

import model.WeatherData.WeatherData;
import model.WeatherModel.WeatherModel;

import javax.swing.*;
import javax.swing.table.JTableHeader;
import java.awt.*;
import java.text.DecimalFormat;

public class WeatherView extends JPanel {
	private WeatherModel weatherModel;
	private JScrollPane table;

	public WeatherView(WeatherModel weatherModel) {
		this.weatherModel = weatherModel;
		this.table = new JScrollPane();
		add(table, BorderLayout.CENTER);
	}

	private String dataOrMessage(Double value) {
		if (value == null) {
			return "Data unavailable";
		}

		return new DecimalFormat("#.##").format(value);
	}

	public void outputData() {
		var data = weatherModel.getData();

		Object[][] rowData = {
				{"Air Temperature, \u00B0C", dataOrMessage(data.airTemperatureC())},
				{"Air Temperature, \u00B0F", dataOrMessage(data.airTemperatureF())},
				{"Cloud Cover, %", dataOrMessage(data.cloudCover())},
				{"Humidity, %", dataOrMessage(data.humidity())},
				{"Precipitation, mm", dataOrMessage(data.precipitation())},
				{"Wind Direction, \u00B0", dataOrMessage(data.windDirection())},
				{"Wind Speed, mm/s", dataOrMessage(data.windSpeed())},
		};
		JTable dataTable = new JTable(rowData, new Object[] {"Metric", "Value"}) {
			@Override
			public Dimension getPreferredScrollableViewportSize() {
				Dimension dim = super.getPreferredScrollableViewportSize();
				dim.height = getPreferredSize().height;
				return dim;
			}

			@Override
			public boolean isCellEditable(int row, int col) {
				return false;
			}
		};

		dataTable.setRowHeight(30);
		dataTable.setPreferredScrollableViewportSize(dataTable.getPreferredScrollableViewportSize());
		dataTable.setFont(new Font("Roboto", Font.PLAIN, 20));

		table.setViewportView(dataTable);
		// add(table, BorderLayout.CENTER);
		// dataTable.setBackground(new Color(43, 43, 43));
//		add(dataTable, BorderLayout.CENTER);
	}
}
