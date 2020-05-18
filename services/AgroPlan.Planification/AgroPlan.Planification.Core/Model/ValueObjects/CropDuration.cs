using System.Collections.Generic;

namespace AgroPlan.Planification.Core.Model.ValueObjects
{
    public class CropDuration : ValueObject
    {
        private CropDuration(int? duration)
        {
            this.Value = duration ?? 1;
        }

        protected CropDuration() { }

        public int? Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}