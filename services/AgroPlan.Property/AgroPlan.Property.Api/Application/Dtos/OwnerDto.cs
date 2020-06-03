using Newtonsoft.Json;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class OwnerDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }

        [JsonProperty("total_surface")]
        public float TotalSurface { get; set; }
    }
}