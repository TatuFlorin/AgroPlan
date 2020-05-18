using System;
using AgroPlan.Planification.Core.Model.ValueObjects;

namespace AgroPlan.Planification.Core.Model.Aggregate
{
    public class CropType : Entity<Guid>
    {
        protected CropType(Guid id) : base(Guid.NewGuid()) { }

        protected CropType() : this(Guid.NewGuid()) { }

        public virtual Name CropName { get; protected set; }
        public virtual Code CropCode { get; protected set; }

    }
}