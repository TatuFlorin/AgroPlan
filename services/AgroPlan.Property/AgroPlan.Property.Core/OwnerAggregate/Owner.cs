using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AgroPlan.Property.AgroPlan.Property.Core.Enums;
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
            this.TotalSurface = new Surface(0.0F);
        }

        public virtual Name Name { get; protected set; }
        public virtual Surface TotalSurface { get; protected set; }
        private readonly IList<Property> _properties = new List<Property>();

        public virtual IList<Property> Properties => _properties.ToList();

        public void RegisterProperty(int physicalBlock
            , int parcelCode
            , float surface
            , Neighbors neighbors)
            {

            _properties?.Add(Property.Create(
                this,
                surface,
                parcelCode,
                neighbors.North_Neighbor,
                neighbors.South_Neighbor,
                neighbors.East_Neighbor,
                neighbors.West_Neighbor,
                physicalBlock
            ));

            this.TotalSurface += surface;
        }

        public void UnregisterProperty(Guid id)
        {
            if(id == Guid.Empty)
                throw new ArgumentNullException("Have to provide a valid id.");
            
            var property = _properties.FirstOrDefault(x => x.Id == id);

            _ = property.Equals(null)
                ? throw new PropertyNotExistException("A property with this id doesn't exist!")
                : (property.EntityState = EntityState.Deleted, this.TotalSurface -= property.Surface);
            
            _properties.Remove(property);
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