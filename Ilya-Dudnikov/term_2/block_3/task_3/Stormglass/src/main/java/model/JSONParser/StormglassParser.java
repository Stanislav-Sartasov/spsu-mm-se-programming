package model.JSONParser;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.ParseException;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

public class StormglassParser implements JSONParser {
	private static final ArrayList<String> params = new ArrayList<>() {{
		add("airTemperature");
		add("cloudCover");
		add("humidity");
		add("precipitation");
		add("windDirection");
		add("windSpeed");
	}};

	@Override
	public Map<String, Double> parse(String json) throws ParseException {
		JSONObject jsonObject = (JSONObject) new org.json.simple.parser.JSONParser().parse(json);

		var hoursArray = (JSONArray) jsonObject.get("hours");
		JSONObject data = (JSONObject) hoursArray.get(hoursArray.size() - 1);

		return new HashMap<>() {{
			params.forEach((param) -> {
				JSONObject currentElem = (JSONObject) data.get(param);
				if (currentElem == null)
					put(param, null);
				else if (param.equals("airTemperature")) {
					put("airTemperatureC", (Double) currentElem.get("sg"));
					put("airTemperatureF", 1.8 * ((Double) currentElem.get("sg")) + 32);
				} else {
					put(param, (Double) currentElem.get("sg"));
				}
			});
		}};
	}
}
