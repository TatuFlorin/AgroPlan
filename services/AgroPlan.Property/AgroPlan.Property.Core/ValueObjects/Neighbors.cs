using System.Collections.Generic;
using System.Linq;

namespace AgroPlan.Property.AgroPlan.Core.ValueObjects{
    public class Neighbors : ValueObject
    {

        protected Neighbors() {}

        public Neighbors(string N_Neighbor
        , string S_Neighbor
        , string W_Neighbor
        , string E_Neighbor)
        {
            North_Neighbor = N_Neighbor;
            South_NeighBor = S_Neighbor;
            West_Neighbor = W_Neighbor;
            East_Neighbor = E_Neighbor;
        }

        public string North_Neighbor { get; protected set; }
        public string South_NeighBor { get; protected set; }
        public string West_Neighbor { get; protected set; }
        public string  East_Neighbor { get; protected set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.North_Neighbor;
            yield return this.South_NeighBor;
            yield return this.West_Neighbor;
            yield return this.East_Neighbor;
        }
    }
}