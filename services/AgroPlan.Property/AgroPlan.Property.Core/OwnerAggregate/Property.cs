using System;
using AgroPlan.Property.AgroPlan.Property.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Property.Core.ValueObjects;
using AgroPlan.Property.AgroPlan.Property.Core.Enums;

namespace AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate{
    public class Property : Entity<Guid>
    {
        public Property (Guid id) :base(id){}

        protected Property() : base(Guid.NewGuid()) {}

        protected Property(Owner owner, float surface, int physicalBlockCode
            , int parcelCode, Neighbors neighbors)
            : this(Guid.NewGuid())
        {
            _surface = surface;
            _physicalBlockId = physicalBlockCode;
            _parcelId = parcelCode;
            Neighbors = neighbors;
            Owner = owner;
            EntityState = EntityState.Added;
        }

        //non persistence
        public EntityState EntityState { get; set; }

        //Value Objects
        public virtual Neighbors Neighbors { get; protected set; }
        public virtual Owner Owner {get; protected set;}

        public readonly float _surface;
        public virtual float Surface => _surface;

        private readonly int _physicalBlockId;
        public virtual int PhysicalBlockId => _physicalBlockId;

        private readonly int _parcelId;
        public virtual int ParcelId => _parcelId;

        //public virtual ImageBin Image { get;protected set; }

        public static Property Create(Owner owner, float surface
            , int parcelCode
            , string N_Neighbor, string S_Neighbor
            , string E_Neighbor,string W_Neighbor
            , int physicalBlockCode)
        {

            if(parcelCode <= 0 || physicalBlockCode <= 0)
                throw new InvalidCodeException(
                    "Please provide valid code!"
                );

            if(surface <= 0.0F)
                throw new InvalidSurfaceException(
                    "Must provide a valid surface!"
                );

            if(string.IsNullOrEmpty(N_Neighbor)
            || string.IsNullOrEmpty(S_Neighbor) 
            || string.IsNullOrEmpty(W_Neighbor)
            || string.IsNullOrEmpty(E_Neighbor))
                throw new ArgumentNullException(
                    "Must provide all neighbors for this property!"
                );

            return new Property(owner
                , surface
                , physicalBlockCode
                , parcelCode
                , new Neighbors(N_Neighbor, S_Neighbor, E_Neighbor, W_Neighbor));
        }

    }
}