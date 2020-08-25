using Newtonsoft.Json;

namespace AgroPlan.Property.AgroPlan.Property.Api.Application.Dtos
{
    [JsonObject(MemberSerialization.OptIn)]
    public class PropertyDto
    {

            public PropertyDto(){}

            public PropertyDto(float surface, int physicalBlockId, int parcelCode,
                string n_Neighbor, string e_neighbot, string w_neighbor, string s_neighbor)
                =>(this.Surface, this.PhysicalBlockId, this.ParcelId, this.NorthNeighbor, this.EastNeighbor
                    ,this.WestNeighbor, this.SouthNeighbor)
                = (surface, physicalBlockId, parcelCode, n_Neighbor, e_neighbot, w_neighbor, s_neighbor);

            
            [JsonProperty("surface")]
            public float Surface { get; set; }

            [JsonProperty("physical_block_code")]
            public int PhysicalBlockId { get; set; }

            [JsonProperty("parcel_code")]
            public int ParcelId { get; set; }

            [JsonProperty("n_neighbor")]
            public string NorthNeighbor { get; set; }
            
            [JsonProperty("e_neighbor")]
            public string EastNeighbor { get; set; }
        
            [JsonProperty("w_neighbor")]
            public string WestNeighbor { get; set; }
           
            [JsonProperty("s_neighbor")]
            public string SouthNeighbor { get; set; }
    }
}