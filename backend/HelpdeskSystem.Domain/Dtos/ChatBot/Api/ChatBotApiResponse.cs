using System.Text.Json.Serialization;

namespace HelpdeskSystem.Domain.Dtos.ChatBot.Api;

public sealed record ChatBotApiResponse
{
    [JsonPropertyName("reply")]
    public required string Reply { get; init; }
}