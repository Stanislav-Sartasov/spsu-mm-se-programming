namespace P2PChat.Core.Json;

/// <summary>
///     Interface for a json object
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IJsonObject<out T>
{
	/// <summary>
	///     Gets object from json
	/// </summary>
	/// <param name="json">json string</param>
	/// <returns>object of type T</returns>
	public static abstract T? FromJson(string json);

	/// <summary>
	///     Converts object to json with a format
	///     Type name\n
	///     Serialized Json
	/// </summary>
	/// <returns>json string with specific format</returns>
	public string ToJson();
}