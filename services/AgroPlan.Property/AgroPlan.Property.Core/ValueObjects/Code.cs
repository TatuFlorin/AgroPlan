using System.Collections.Generic;

namespace AgroPlan.Property.AgroPlan.Core.ValueObjects{
    public class Code : ValueObject
    {
        protected Code(){}

        public Code(int code){
            Value = code;
        }

        public int Value { get; protected set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return this.Value;
        }
    }
}