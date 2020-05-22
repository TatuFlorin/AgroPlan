using System;
using AgroPlan.Property.AgroPlan.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Core.ValueObjects;

namespace AgroPlan.Property.AgroPlan.Core.OwnerAggregate{
    public class Property : Entity<Guid>
    {
        public Property (Guid id) :base(id){}
        protected Property(Surface surface, Code physicalBlock, Code parcelCode, Neighbors neighbors)
            :this (Guid.NewGuid())
        {
            Surface = surface;
            PhysicalBlock = physicalBlock;
            ParcelCode = parcelCode;
            Neighbors = neighbors;
        }

        public virtual Surface Surface { get; protected set; }
        public virtual Code PhysicalBlock { get; protected set; }
        public virtual Code ParcelCode { get; protected set; }
        public virtual Neighbors Neighbors { get; protected set; }

        //public virtual ImageBin Image { get;protected set; }

        public static Property Create(float surface
            , int physicalBlock, int parcelCode
            , string N_Neighbor, string S_Neighbor
            , string E_Neighbor,string W_Neighbor)
        {
               if(surface <= 0f)
                    throw new InvalidSurfaceException(
                        "Must provide a valid surface!"
                    );

               if(string.IsNullOrEmpty(N_Neighbor)
                || string.IsNullOrEmpty(S_Neighbor) 
                || string.IsNullOrEmpty(W_Neighbor)
                || string.IsNullOrEmpty(E_Neighbor)
               )
                    throw new ArgumentNullException(
                        "Must provide all beighbors for this property!"
                    );

                if(parcelCode <= 0 || physicalBlock <= 0)
                    throw new InvalidCodeException(
                        "Must provide a valid parcel/physical bloc code!"
                    );


            return new Property(new Surface(surface)
                , new Code(physicalBlock)
                , new Code(parcelCode)
                , new Neighbors(N_Neighbor, S_Neighbor, E_Neighbor, W_Neighbor));
        }

    }
}