using P2PChat.Core.Json;

namespace P2PChat.Core.Message.Fallback;

/// <summary>
///     Message to request a fallback address
///     It is sent from child to parent
///     Parent gets the Request and sends the response
/// </summary>
public class FallbackListenerRequest : AJsonObject<FallbackListenerRequest>
{
}