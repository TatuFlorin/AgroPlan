using Newtonsoft.Json;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class OwnerDto
    {
        public OwnerDto(string id, string firstName, string lastName, float totalSurface)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            TotalSurface = totalSurface;
        }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("total_surface")]
        public float TotalSurface { get; set; }
    }
}