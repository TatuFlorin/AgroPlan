using System;
using System.Collections.Generic;
using System.Linq;

namespace AgroPlan.Planification.Core.Model.ValueObjects
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<Object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj is null || obj.GetType() != GetType())
                return false;

            ValueObject valueObject = (ValueObject)obj;

            IEnumerator<object> thisObj = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherObj = valueObject.GetAtomicValues().GetEnumerator();

            while (thisObj.MoveNext() && otherObj.MoveNext())
            {
                if (ReferenceEquals(thisObj.Current, null) ^
                ReferenceEquals(otherObj.Current, null))
                    return false;

                if (thisObj.Current != null &&
                    !thisObj.Current.Equals(otherObj.Current))
                    return false;
            }
            return !thisObj.MoveNext() && !otherObj.MoveNext();
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}