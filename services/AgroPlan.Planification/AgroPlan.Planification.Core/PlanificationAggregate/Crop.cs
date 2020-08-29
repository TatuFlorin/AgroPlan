using System;
using AgroPlan.Planification.Core.ValueObjects;
using AgroPlan.Planification.Core.Enums;

namespace AgroPlan.Planification.Core.Aggregate
{
    public class Crop : Entity<Guid>
    {
        public Crop(Guid id) : base(id) { }

        protected Crop() : this(Guid.NewGuid()) { }

        protected Crop(CropType type, Surface surface, Planification planification
        , Code physicalBlock, Code parcel)
        : this(Guid.NewGuid())
        {
            this.Type = type;
            this.Surface = surface;
            this.Planification = planification;
            this.Parcel = parcel;
            this.PhysicalBlock = physicalBlock;
        }

        public virtual Planification Planification { get; protected set; }
        public virtual CropType Type { get; protected set; }
        public virtual Surface Surface { get; protected set; }
        public virtual CropDuration Duration { get; private set; }

        public virtual Code PhysicalBlock { get; private set; }
        public virtual Code Parcel { get; private set; }

        #region NOT PERSISTENCE
        public TrackingState State { get; set; }
        #endregion

        /// <summary>
        /// Factory method
        /// </summary>
        /// <param name="type"></param>
        /// <param name="surface"></param>
        /// <returns>A new instance of Crop.</returns>
        public static Crop Create(CropType type, Surface surface, Planification planification
            , Code physicalBlock, Code parcel)
        {
            _ = type ?? throw new ArgumentNullException();
            _ = surface ?? throw new ArgumentNullException();
            _ = planification ?? throw new ArgumentNullException();
            _ = physicalBlock ?? throw new ArgumentNullException();
            _ = parcel ?? throw new ArgumentNullException();

            return new Crop(type, surface, planification, physicalBlock, parcel);
        }

    }
}