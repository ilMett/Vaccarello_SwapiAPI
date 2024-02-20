using System.Text.Json.Serialization;

public class SwapiResponse
{
    public int Count { get; set; }
    [JsonPropertyName("next")]
    public string? Next { get; set; }
    public string? Previous { get; set; }
    [JsonPropertyName("results")]
    public Person[] People { get; set; }
}

public class Person
{
    public string? Name { get; set; }
    public string? Height { get; set; }
    public string? Mass { get; set; }
    [JsonPropertyName("hair_color")]
    public string? HairColor { get; set; }
    [JsonPropertyName("skin_color")]
    public string? SkinColor { get; set; }
    [JsonPropertyName("eye_color")]
    public string? EyeColor { get; set; }
    public string? Gender { get; set; }

}

