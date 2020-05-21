using System;
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
        , int physicalBlock, int parcel, string N_Neighbor, string S_Neighbor
        ,string E_Neighbor,string W_Neighbor)
        {
            //Guard

            return new Property(new Surface(surface)
                , new Code(physicalBlock)
                , new Code(parcel)
                , new Neighbors(N_Neighbor, S_Neighbor, E_Neighbor, W_Neighbor));
        }

    }
}