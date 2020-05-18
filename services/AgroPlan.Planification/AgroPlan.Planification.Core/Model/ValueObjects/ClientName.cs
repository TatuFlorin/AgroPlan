using System.Collections.Generic;
using System;

namespace AgroPlan.Planification.Core.Model.ValueObjects
{
    public class ClientName : ValueObject
    {

        public ClientName(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;

        }

        public ClientName() { }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            var fields = this.GetType().GetFields();
            foreach (var field in fields)
            {
                yield return field;
            }
        }
    }
}