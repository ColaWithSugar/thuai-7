using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace GameServer.Recorder;

public record CompetitionUpdate
{
    [JsonPropertyName("messageType")]
    public string messageType => "COMPETITION_UPDATE";

    [JsonPropertyName("tick")]
    public int Tick { get; init; }

    [JsonIgnore]
    public JsonNode Json => JsonNode.Parse(JsonSerializer.Serialize(this))!;

    [JsonPropertyName("info")]
    public infoType? info { get; init; }

    [JsonPropertyName("players")]
    public List<playersType>? players { get; init; }

    [JsonPropertyName("events")]
    public List<IRecord>? events { get; init; }

    public record infoType
    {
        [JsonPropertyName("elapsedTicks")]
        public int? elapsedTicks { get; init; }

        [JsonPropertyName("stage")]
        public string? stage { get; init; }
    }

    public record playersType
    {
        [JsonPropertyName("playerId")]
        public int? playerId { get; init; }

        [JsonPropertyName("armor")]
        public string? armor { get; init; }

        [JsonPropertyName("health")]
        public int? health { get; init; }

        [JsonPropertyName("speed")]
        public double? speed { get; init; }

        [JsonPropertyName("firearm")]
        public firearmType? firearm { get; init; }

        [JsonPropertyName("inventory")]
        public List<inventoryType>? inventory { get; init; }

        [JsonPropertyName("position")]
        public positionType? position { get; init; }
    }

    public record firearmType
    {
        [JsonPropertyName("name")]
        public string? speed { get; init; }

        [JsonPropertyName("windup")]
        public double? windup { get; init; }

        [JsonPropertyName("distance")]
        public double? distance { get; init; }
    }

    public record positionType
    {
        [JsonPropertyName("x")]
        public double x { get; init; }

        [JsonPropertyName("y")]
        public double y { get; init; }
    }

    public record inventoryType
    {
        [JsonPropertyName("name")]
        public string? name { get; init; }

        [JsonPropertyName("num")]
        public int? num { get; init; }
    }
}
