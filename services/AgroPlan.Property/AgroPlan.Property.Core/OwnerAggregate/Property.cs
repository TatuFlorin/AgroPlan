using System;
using AgroPlan.Property.AgroPlan.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Core.ValueObjects;

namespace AgroPlan.Property.AgroPlan.Core.OwnerAggregate{
    public class Property : Entity<Guid>
    {
        public Property (Guid id) :base(id){}

        protected Property() : base(Guid.NewGuid()) {}

        protected Property(Owner owner, Surface surface, PhysicalBlock physicalBlock
            , Parcel parcel, Neighbors neighbors)
            : this(Guid.NewGuid())
        {
            Surface = surface;
            _physicalBlock = physicalBlock;
            _parcel = parcel;
            Neighbors = neighbors;
            Owner = owner;
        }

        //Value Objects
        public virtual Surface Surface { get; protected set; }
        public virtual Neighbors Neighbors { get; protected set; }
        public virtual Owner Owner {get; protected set;}

        private readonly PhysicalBlock _physicalBlock;
        public virtual PhysicalBlock PhysicalBlock => _physicalBlock;

        private readonly Parcel _parcel;
        public virtual Parcel Parcel => _parcel;

        //public virtual ImageBin Image { get;protected set; }

        public static Property Create(Owner owner, float surface
            , int physicalBlock, int parcelCode
            , string N_Neighbor, string S_Neighbor
            , string E_Neighbor,string W_Neighbor)
        {

            _ = owner ?? throw new ArgumentNullException(
                "Have to provide an owner for this property!"
                );

            if(surface <= 0f)
                throw new InvalidSurfaceException(
                    "Must provide a valid surface!"
                );

            if(string.IsNullOrEmpty(N_Neighbor)
            || string.IsNullOrEmpty(S_Neighbor) 
            || string.IsNullOrEmpty(W_Neighbor)
            || string.IsNullOrEmpty(E_Neighbor))
                throw new ArgumentNullException(
                    "Must provide all beighbors for this property!"
                );

            if(parcelCode <= 0 || physicalBlock <= 0)
                throw new InvalidCodeException(
                    "Must provide a valid parcel/physical bloc code!"
                );

            return new Property(owner
                , new Surface(surface)
                , PhysicalBlock.Create(physicalBlock)
                , Parcel.Create(parcelCode)
                , new Neighbors(N_Neighbor, S_Neighbor, E_Neighbor, W_Neighbor));
        }

    }
}