using System;
using System.Text;

namespace AgroPlan.Planification.Core
{
    public abstract class Entity<T>
    {
        public virtual T Id { get; private set; }

        protected Entity(T id)
        => (Id) = (id);

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;
            if (!(obj is Entity<T> other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return this.Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return (this.GetType().ToString() + this.Id).GetHashCode();
        }

    }
}