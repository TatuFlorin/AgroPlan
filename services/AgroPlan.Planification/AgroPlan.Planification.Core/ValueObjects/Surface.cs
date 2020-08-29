using System;
using System.Collections.Generic;

namespace AgroPlan.Planification.Core.ValueObjects
{
    public class Surface : ValueObject
    {
        public Surface()
        {

        }
        public Surface(float value)
        {
            this.Value = value;
        }

        public float Value { get; private set; }

        public Surface Decrease(float surface)
        {
            var result = this.Value - surface;
            if (result < 0) throw new ArgumentOutOfRangeException();
            return new Surface(result);
        }

        public Surface Increase(float surface)
        {
            var result = this.Value + surface;
            return new Surface(result);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Value;
        }
    }
}