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

        public void RegisterProperty(int physicalBlock
            , int parcelCode
            , Surface surface
            , Neighbors neighbors)
            {
                // _ = surface ?? throw new ArgumentNullException(
                //     "Must provide a surface!"
                // );
                // //TODO: add check for each neighbor
                // _ = neighbors ?? throw new ArgumentNullException(
                //     "Must provide all neighbors for this property!"
                // );

                // if(parcelCode <= 0 || physicalBlock <= 0)
                //     throw new InvalidCodeException();

            var taken = _properties.Any(x => x.PhysicalBlock.Equals(physicalBlock) 
                    && x.ParcelCode.Equals(parcelCode));

            if(taken)
                throw new BusyParcelException("This parcel is already taken!");

            _properties.Add(Property.Create(
                surface.Value,
                physicalBlock,
                parcelCode,
                neighbors.North_Neighbor,
                neighbors.South_NeighBor,
                neighbors.East_Neighbor,
                neighbors.West_Neighbor
            ));

            //surface ++
        }

        public void UnregisterProperty(Guid id){

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
            if(string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException();
             
            if(string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException();

            return new Owner(id, new Name(firstName, lastName));
        }
    }
}