package model.JSONParser;

import org.json.simple.parser.ParseException;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.HashMap;

import static org.junit.jupiter.api.Assertions.*;

class StormglassParserTest {
	private static final String RESOURCES_PATH = "src/test/resources/";

	@Test
	void parse() throws IOException, ParseException {
		JSONParser jsonParser = new StormglassParser();

		HashMap<String, Double> expected = new HashMap<>() {{
			put("airTemperatureC", 2.23);
			put("airTemperatureF", 36.014);
			put("precipitation", 0.0);
			put("cloudCover", 0.0);
			put("humidity", 79.44);
			put("windDirection", 78.11);
			put("windSpeed", 1.94);
		}};

		var actual = jsonParser.parse(Files.readString(Path.of(RESOURCES_PATH + "goodStormglassJson.json")));

		assertEquals(expected, actual);
	}

	@Test
	void parseJSONWithNullValues() throws IOException, ParseException {
		JSONParser jsonParser = new StormglassParser();

		HashMap<String, Double> expected = new HashMap<>() {{
			put("airTemperatureC", 2.23);
			put("airTemperatureF", 36.014);
			put("precipitation", 0.0);
			put("cloudCover", 0.0);
			put("humidity", null);
			put("windDirection", 78.11);
			put("windSpeed", null);
		}};

		var actual = jsonParser.parse(Files.readString(Path.of(RESOURCES_PATH + "StormglassJsonWithNulls.json")));

		assertEquals(expected, actual);
	}

	@Test
	void parseBrokenJsonExpectParseException() throws IOException, ParseException {
		JSONParser jsonParser = new StormglassParser();

		assertThrows(ParseException.class, () -> jsonParser.parse(Files.readString(Path.of(RESOURCES_PATH + "brokenJson.json"))));
	}
}