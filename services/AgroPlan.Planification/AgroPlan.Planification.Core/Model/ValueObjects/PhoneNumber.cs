using System.Collections.Generic;

namespace AgroPlan.Planification.Core.Model.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber()
        {

        }
        public PhoneNumber(string value)
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