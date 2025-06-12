using System.Text.Json.Serialization;

namespace HelpdeskSystem.Domain.Dtos.ChatBot.Api;

public sealed record ChatBotApiRequest
{
    [JsonPropertyName("role")]
    public required string Role { get; init; }
    
    [JsonPropertyName("content")]
    public required string Content { get; init; }
}