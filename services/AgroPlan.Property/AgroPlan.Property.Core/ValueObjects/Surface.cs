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

        public static Surface operator -(Surface surface1, float surface2)
            => new Surface(surface1.Value - surface2);

        public static Surface operator -(Surface surface1, Surface surface2)
            => new Surface(surface1.Value - surface2.Value);

        public static Surface operator +(Surface surface1, Surface surface2)
            => new Surface(surface1.Value + surface2.Value); 

        public static Surface operator +(Surface surface1, float surface2)
            => new Surface(surface1.Value + surface2);
    }
}