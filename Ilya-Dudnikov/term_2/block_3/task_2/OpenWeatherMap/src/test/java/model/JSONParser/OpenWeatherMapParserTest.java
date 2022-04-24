package model.JSONParser;

import org.json.simple.parser.ParseException;
import org.junit.jupiter.api.Test;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.HashMap;

import static org.junit.jupiter.api.Assertions.*;

class OpenWeatherMapParserTest {
	public static final String RESOURCES_PATH = "src/test/resources/";

	@Test
	void parse() throws IOException, ParseException {
		JSONParser jsonParser = new OpenWeatherMapParser();

		HashMap<String, Double> expected = new HashMap<>() {{
			put("airTemperature", 282.55);
			put("precipitation", 0.0);
			put("cloudCover", 1.0);
			put("humidity", 100.0);
			put("windDirection", 350.0);
			put("windSpeed", 1.5);
		}};

		var actual = jsonParser.parse(Files.readString(Path.of(RESOURCES_PATH + "goodOpenWeatherMapJson.json")));

		assertEquals(expected, actual);
	}

	@Test
	void parseJSONWithNullValues() throws IOException, ParseException {
		JSONParser jsonParser = new OpenWeatherMapParser();

		HashMap<String, Double> expected = new HashMap<>() {{
			put("airTemperature", 282.55);
			put("precipitation", null);
			put("cloudCover", 1.0);
			put("humidity", 100.0);
			put("windDirection", 350.0);
			put("windSpeed", 1.5);
		}};

		var actual = jsonParser.parse(Files.readString(Path.of(RESOURCES_PATH + "OpenWeatherMapJsonWithNulls.json")));

		assertEquals(expected, actual);
	}

	@Test
	void parseBrokenJsonExpectParseException() throws IOException, ParseException {
		JSONParser jsonParser = new OpenWeatherMapParser();

		assertThrows(ParseException.class, () -> jsonParser.parse(Files.readString(Path.of(RESOURCES_PATH + "brokenJson.json"))));
	}
}