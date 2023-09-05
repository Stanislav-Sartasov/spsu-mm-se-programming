using P2PChat.Core.Json;

namespace P2PChat.Core.Message.Fallback;

/// <summary>
///     Empty message just to skip receiving message in a thread loop
/// </summary>
public class SkipMessage : AJsonObject<SkipMessage>
{
}