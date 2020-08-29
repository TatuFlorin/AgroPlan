using System;
using AgroPlan.Planification.Core.ValueObjects;

namespace AgroPlan.Planification.Core.Aggregate
{
    public class Client : Entity<string>
    {
        public Client(string Id) : base(Id) { }

        protected Client() : base(string.Empty) { }

        public Client(string id, ClientName name, PhoneNumber phone)
            : this(id)
        {
            this.Name = name;
            this.Phone = phone;
        }

        public virtual ClientName Name { get; protected set; }
        public virtual PhoneNumber Phone { get; protected set; }
        public virtual Surface UsageSurface { get; private set; }

    }
}