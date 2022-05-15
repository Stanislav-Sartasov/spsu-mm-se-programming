package view.dataView;

public enum Metrics {
	DEGREES_CELSIUS,
	DEGREES_FAHRENHEIT,
	DEGREES,
	PERCENT,
	MM_S,
	NONE;

	public String toString() {
		String result = "";
		switch (this) {
			case NONE -> result = "";
			case DEGREES_CELSIUS -> result = "\u00B0C";
			case DEGREES_FAHRENHEIT -> result = "\u00B0F";
			case DEGREES -> result = "\u00B0";
			case PERCENT -> result = "%";
			case MM_S -> result = "mm/s";
		}
		return result;
	}
}
