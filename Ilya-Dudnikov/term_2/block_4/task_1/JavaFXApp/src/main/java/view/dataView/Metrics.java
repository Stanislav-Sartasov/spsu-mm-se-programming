package view.dataView;

public enum Metrics {
	DEGREES_CELSIUS,
	DEGREES_FAHRENHEIT,
	DEGREES,
	PERCENT,
	M_S,
	MM,
	NONE;

	public String toString() {
		String result = "";
		switch (this) {
			case NONE -> result = "";
			case DEGREES_CELSIUS -> result = "\u00B0C";
			case DEGREES_FAHRENHEIT -> result = "\u00B0F";
			case DEGREES -> result = "\u00B0";
			case PERCENT -> result = "%";
			case M_S -> result = "m/s";
			case MM -> result = "mm";
		}
		return result;
	}
}
