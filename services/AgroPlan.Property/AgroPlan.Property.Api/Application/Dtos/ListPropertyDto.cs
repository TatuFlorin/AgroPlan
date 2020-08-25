using System;
using Newtonsoft.Json;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ListPropertyDto
    {
        public ListPropertyDto() {}

        public ListPropertyDto(Guid propertyId, float surface)
            => (this.Id, this.Surface)
            =  (propertyId, surface);

        [JsonProperty("property_id")]
        public Guid Id { get; set; }

        [JsonProperty("surface")]
        public float Surface { get; set; }
    }
}