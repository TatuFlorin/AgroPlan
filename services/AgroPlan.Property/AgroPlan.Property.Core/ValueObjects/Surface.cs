using System.Collections.Generic;

namespace AgroPlan.Property.AgroPlan.Property.Core.ValueObjects{
    public class Surface : ValueObject
    {

        protected Surface () {}

        public Surface(float surface){
            Value = surface;
        }

        public float Value { get; protected set; }

        public Surface ChangeSurface(float surface)
            => new Surface(surface);


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Value;
        }
    }
}