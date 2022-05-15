package view.dataView;

import javafx.geometry.Insets;
import javafx.geometry.Pos;
import javafx.scene.control.Label;
import javafx.scene.control.Tooltip;
import javafx.scene.image.ImageView;
import javafx.scene.layout.BorderPane;
import javafx.scene.paint.Color;
import model.WeatherData.WeatherData;

public class DataView extends BorderPane {
	private static final String TEXT_STYLE = "" +
			"-fx-font-size: 15pt;" +
			"-fx-font-family: 'Roboto Light';" +
			"-fx-font-weight: bold;";

	private static final String PATH_TO_FALLBACK_ICON = "src/main/resources/info.png";

	private Double value;
	private ImageView icon;
	private String name;
	private Metrics metric;

	public String getName() {
		return name;
	}

	public DataView(String name, Double value) {
		this(name, value, PATH_TO_FALLBACK_ICON);
	}

	public DataView(String name, Double value, String pathToIcon) {
		this(name, value, pathToIcon, Metrics.NONE);
	}

	public DataView(String name, Double value, Metrics metric) {
		this(name, value, PATH_TO_FALLBACK_ICON, metric);
	}

	public DataView(String name, Double value, String pathToIcon, Metrics metric) {
		this.name = name;
		this.value = value;
		this.icon = new ImageView("file:" + pathToIcon);
		this.metric = metric;
	}

	public void outputData() {
		if (value == null)
			return;
		Label label = new Label(value + metric.toString());
		icon.setPreserveRatio(true);
		label.setGraphic(icon);
		label.setTextFill(Color.WHITE);
		label.setStyle(TEXT_STYLE);

		label.setTooltip(new Tooltip(name + ", " + metric.toString()));
		setMargin(label, new Insets(5., 0., 0., 10.));
		setTop(label);
		setAlignment(label, Pos.TOP_LEFT);
	}
}
