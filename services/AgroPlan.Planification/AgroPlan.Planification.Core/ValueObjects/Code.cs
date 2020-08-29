using System.Collections.Generic;

namespace AgroPlan.Planification.Core.ValueObjects
{
    public class Code : ValueObject
    {
        public Code(int value)
        {
            this.Value = value;
        }
        public Code()
        {

        }
        public int Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Value;
        }
    }
}