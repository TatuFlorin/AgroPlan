namespace AgroPlan.Property.AgroPlan.Property.Core{
    public abstract class Entity<T> {

        public T Id {get; protected set;}

        public Entity(T id)
        { this.Id = id; }

        public Entity() {}

        //override        
    }
}