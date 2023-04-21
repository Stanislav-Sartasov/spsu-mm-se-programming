package util;

import bmp.BMPColor;

public final class BMPColors {
	private BMPColors() {
		throw new RuntimeException("BMPColors class is not supposed to be instanced");
	}

	public static BMPColor copy(BMPColor other) {
		return new BMPColor(
				other.red(),
				other.green(),
				other.blue(),
				other.alpha()
		);
	}
}
