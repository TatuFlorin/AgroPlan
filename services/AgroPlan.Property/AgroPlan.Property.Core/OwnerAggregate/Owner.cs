using System;
using System.Collections.Generic;
using System.Linq;
using AgroPlan.Property.AgroPlan.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Core.ValueObjects;

namespace AgroPlan.Property.AgroPlan.Core.OwnerAggregate{
    public class Owner : Entity<string>
    {

        public Owner(string id) : base(id) { }

        protected Owner(string id, Name name)
            : this(id)
        {
            this.Name = name;
        }

        public virtual Name Name { get; protected set; }
        public virtual Surface TotalSurface { get; protected set; }
        private List<Property> _properties;

        public IReadOnlyList<Property> Properties => _properties.AsReadOnly();

        public void NewProperty(int physicalBlock, int parcelCode){
            //Guard

            var taken = _properties.Any(x => x.PhysicalBlock.Equals(physicalBlock) 
                    && x.ParcelCode.Equals(parcelCode));

            if(taken)
                throw new BusyParcelException("This parcel is already taken!");

            _properties.Add(Property.Create());

            //surface ++
        }

        public void RemoveProperty(Guid id){

            if(id == Guid.Empty || id.GetType().Equals(typeof(Guid)))
                throw new ArgumentNullException("Have to provide an valid id.");
            
            _ = _properties ?? throw new NullReferenceException();

            var property = _properties.First(x => x.Id == id);
                _ = object.Equals(property, null) 
                ? throw new PropertyNotExistException("A property with this is doesn't exist!")
                : _properties.Remove(property);
            
            //surface --
        }

        public static Owner Create(string id, string firstName, string lastName)
        {
            if(firstName == String.Empty)
                throw new ArgumentNullException();
             
            if(lastName == String.Empty)
                throw new ArgumentNullException();

            return new Client(id, new Name(firstName, lastName));
        }
    }
}