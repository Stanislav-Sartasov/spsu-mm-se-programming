package model.JSONParser;

import org.json.simple.JSONObject;
import org.json.simple.parser.ParseException;

import java.util.HashMap;
import java.util.Map;

public class OpenWeatherMapParser implements JSONParser {
	@Override
	public Map<String, Double> parse(String json) throws ParseException {
		JSONObject jsonObject = (JSONObject) new org.json.simple.parser.JSONParser().parse(json);

		return new HashMap<>() {{
			put("airTemperature", valueOrNull("main.temp", jsonObject));
			put("cloudCover", valueOrNull("clouds.all", jsonObject));
			put("humidity", valueOrNull("main.humidity", jsonObject));
			put("precipitation", valueOrNull("rain.1h", jsonObject));
			put("windDirection", valueOrNull("wind.deg", jsonObject));
			put("windSpeed", valueOrNull("wind.speed", jsonObject));
		}};
	}

	private Double valueOrNull(String parameter, JSONObject jsonObject) {
		String[] splittedParameters = parameter.split("\\.");
		JSONObject jsonParameter = (JSONObject) jsonObject.get(splittedParameters[0]);

		if (jsonParameter == null)
			return null;
		return Double.valueOf(jsonParameter.get(splittedParameters[1]).toString());
	}
}
