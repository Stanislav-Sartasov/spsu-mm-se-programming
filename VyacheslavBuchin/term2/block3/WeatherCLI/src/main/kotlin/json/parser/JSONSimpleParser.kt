package json.parser

import org.json.simple.JSONObject

abstract class JSONSimpleParser<T> : JSONParser<T> {
	protected fun <P> getByParameterOrNull(parameters: List<String>, jsonObject: JSONObject, factory: (String) -> P): P? {
		var json = jsonObject
		for (i in 0..(parameters.size - 2)) {
			json = json[parameters[i]] as JSONObject? ?: return null
		}
		val value = json[parameters.last()] ?: return null
		return factory(value.toString())
	}
}