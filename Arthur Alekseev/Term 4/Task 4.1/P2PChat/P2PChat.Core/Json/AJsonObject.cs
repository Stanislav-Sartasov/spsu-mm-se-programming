using Newtonsoft.Json;

namespace P2PChat.Core.Json;

/// <summary>
///     Abstract class for creating json objects
/// </summary>
/// <typeparam name="T"></typeparam>
public class AJsonObject<T> : IJsonObject<T>
{
	/// <summary>
	///     Returns object from Json
	/// </summary>
	/// <param name="json">Json string containing type string and json for object</param>
	/// <returns>Deserialized object of type T</returns>
	public static T? FromJson(string json)
	{
		var cropped = string.Join("\n", json.Split("\n").Skip(1));
		var obj = JsonConvert.DeserializeObject<T>(cropped);
		return obj;
	}

	/// <summary>
	///     Converts object to json with a format
	///     Type name\n
	///     Serialized Json
	/// </summary>
	/// <returns>json string with specific format</returns>
	public string ToJson()
	{
		var serialized = JsonConvert.SerializeObject(this);
		return $"{typeof(T)}\n{serialized}";
	}

	/// <summary>
	///     Checks if Json is in fact an instance of an object
	/// </summary>
	/// <param name="json">Json to check</param>
	/// <returns>true if json is matched with type and false otherwise</returns>
	public static bool IsValid(string json)
	{
		try
		{
			var objectType = json.Split("\n")[0].Trim();
			return objectType == typeof(T).ToString();
		}
		catch
		{
			return false;
		}
	}
}