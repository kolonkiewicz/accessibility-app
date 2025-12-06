using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace inzynierka.Models.ViewModels
{
    public class AxeResult
    {
        [JsonPropertyName("violations")]
        public List<AxeViolation> Violations { get; set; } = new();
    }

    public class AxeViolation
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("impact")]
        public string Impact { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("helpUrl")]
        public string HelpUrl { get; set; }

        [JsonPropertyName("nodes")]
        public List<AxeNode> Nodes { get; set; } = new();
    }

    public class AxeNode
    {
        [JsonPropertyName("html")]
        public string Html { get; set; }

        [JsonPropertyName("target")]
        public List<string> Target { get; set; } = new();
    }
}
