using System.Collections.Generic;

namespace AgroPlan.Planification.Core.ValueObjects
{
    public class Name : ValueObject
    {
        public Name()
        {

        }
        public Name(string value)
        {
            this.Value = value;
        }
        public string Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Value;
        }
    }
}