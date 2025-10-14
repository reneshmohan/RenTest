using System.Text.Json.Serialization;

namespace Utility.Models
{
    public class Energy()
    {
        [JsonPropertyName("electric")]
        public EnergyInfo Electric { get; set; }

        [JsonPropertyName("gas")]
        public EnergyInfo Gas { get; set; }

        [JsonPropertyName("nuclear")]
        public EnergyInfo Nuclear { get; set; }

        [JsonPropertyName("oil")]
        public EnergyInfo Oil { get; set; }
    }
}