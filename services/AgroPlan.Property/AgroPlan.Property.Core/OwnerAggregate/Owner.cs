using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AgroPlan.Property.AgroPlan.Property.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Property.Core.ValueObjects;

namespace AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate{
    public class Owner : Entity<string>
    {

        public Owner(string id) : base(id) { }

        //Required by EntityFramework
        protected Owner() : base("") {}

        protected Owner(string id, Name name)
            : this(id)
        {
            this.Name = name;
            _properties = _properties ?? new List<Property>();    
        }

        public virtual Name Name { get; protected set; }
        public virtual Surface TotalSurface { get; protected set; }
        private readonly List<Property> _properties;

        public virtual IReadOnlyList<Property> Properties => _properties.AsReadOnly();

        public void RegisterProperty(int physicalBlock
            , int parcelCode
            , Surface surface
            , Neighbors neighbors)
            {
            
            var taken = _properties.Any(x => x.PhysicalBlock.Equals(physicalBlock)
                    && x.Parcel.Equals(parcelCode));

            if(taken)
                throw new TakenParcelException("This parcel is already taken!");

            _properties.Add(Property.Create(
                this,
                surface.Value,
                physicalBlock,
                parcelCode,
                neighbors.North_Neighbor,
                neighbors.South_NeighBor,
                neighbors.East_Neighbor,
                neighbors.West_Neighbor
            ));

            this.TotalSurface.ChangeSurface(
                TotalSurface.Value + surface.Value
            );
        }

        public void UnregisterProperty(Guid id){

            if(id == Guid.Empty || id.GetType().Equals(typeof(Guid)))
                throw new ArgumentNullException("Have to provide a valid id.");
            
            _ = _properties ?? throw new NullReferenceException();

            var property = _properties.First(x => x.Id == id);
                _ = object.Equals(property, null) 
                ? throw new PropertyNotExistException("A property with this is doesn't exist!")
                : _properties.Remove(property);
            
            this.TotalSurface.ChangeSurface(
                TotalSurface.Value - property.Surface.Value
            );
        }

        //Factory method
        public static Owner Create(string id, string firstName, string lastName)
        {
            var reGex = new Regex("[0-9]{13}");
            
            if(!reGex.IsMatch(id))
                throw new InvalidOwnerIdException();
                
            if(string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException();
             
            if(string.IsNullOrEmpty(lastName))
                throw new ArgumentNullException();

            return new Owner(id, new Name(firstName, lastName));
        }
    }
}