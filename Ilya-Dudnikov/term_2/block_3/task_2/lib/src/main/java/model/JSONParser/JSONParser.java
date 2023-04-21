package model.JSONParser;

import org.json.simple.parser.ParseException;

import java.util.Map;

public interface JSONParser {
	public Map<String, Double> parse(String json) throws ParseException;
}
