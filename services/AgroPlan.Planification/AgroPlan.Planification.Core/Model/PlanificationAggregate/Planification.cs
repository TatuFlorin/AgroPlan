using System;
using System.Collections.Generic;
using System.Linq;
using AgroPlan.Planification.Core.Model.ValueObjects;
using AgroPlan.Planification.Core.Model.Exceptions;
using AgroPlan.Planification.Core.Model.Enums;

namespace AgroPlan.Planification.Core.Model.Aggregate
{
    public class Planification : Entity<Guid>
    {
        protected Planification(Guid id) : base(id) { }

        #region NHibernate REQUIRED
        protected Planification() : base(Guid.NewGuid()) { }
        #endregion

        private Planification(Client client, int year, Surface surface)
            : base(Guid.NewGuid())
        {
            this.Client = client;
            this.Year = year;
            this.Surface = surface;
        }

        public virtual Client Client { get; protected set; }
        public virtual int Year { get; protected set; }
        public virtual Surface Surface { get; protected set; }

        private readonly IList<Crop> _crops = new List<Crop>();
        public virtual IEnumerable<Crop> Crops => _crops.ToList();

        public virtual void AddCrop(CropType type, Surface surface
        , Code physicalBlock, Code parcel)
        {
            if (surface.Value <= 0)
                throw new InvalidValueException("Value must be grater then 0!");

            var crop = Crop.Create(type, surface, this, physicalBlock, parcel);
            crop.State = TrackingState.Added;

            _crops.Add(crop);
            this.Surface = this.Surface.Decrease(surface.Value);
        }

        public virtual void RemoveCrop(Guid cropId)
        {
            if (cropId == null && cropId == Guid.Empty)
                throw new ArgumentNullException();

            var crop = _crops.ToList().Find(x => x.Id == cropId)
                ?? throw new KeyNotFoundException();

            crop.State = TrackingState.Removed;

            this.Surface = this.Surface.Increase(crop.Surface.Value);

            _crops.Remove(crop);
        }

        /// <summary>
        /// Factory method.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="year"></param>
        /// <param name="surface"></param>
        /// <returns></returns>
        public static Planification Create(Client client, int year)
        {
            _ = client ?? throw new ArgumentNullException();

            if (year < DateTime.Now.Year)
                throw new InvalidValueException();

            return new Planification(client, year, new Surface(client.UsageSurface.Value));
        }

    }
}