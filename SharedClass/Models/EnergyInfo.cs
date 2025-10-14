using System.Text.Json.Serialization;

namespace Utility.Models
{
    public class EnergyInfo
    {
        [JsonPropertyName("energy_id")]
        public int EnergyId { get; set; }

        [JsonPropertyName("price_per_unit")]
        public double PricePerUnit { get; set; }

        [JsonPropertyName("quantity_of_units")]
        public double QuantityOfUnits { get; set; }

        [JsonPropertyName("unit_type")]
        public string UnitType { get; set; }
    }
}
