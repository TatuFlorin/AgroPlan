using System.Collections.Generic;

namespace AgroPlan.Property.AgroPlan.Core.ValueObjects{
    public class Name : ValueObject
    {

        protected Name()
        {        }

        public Name(string firstName, string lastName){
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; protected set; }
        public string LastName {get; protected set;}

        public static explicit operator string(Name name)
            => name.FirstName + " " + name.LastName;

        protected override IEnumerable<object> GetAtomicValues()
        {
         yield return this.FirstName;
         yield return this.LastName;
        }
    }
}