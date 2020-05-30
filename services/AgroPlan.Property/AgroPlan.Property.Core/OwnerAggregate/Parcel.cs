using System;
using AgroPlan.Property.AgroPlan.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Core.ValueObjects;

namespace AgroPlan.Property.AgroPlan.Core.OwnerAggregate{
    public class Parcel : Entity<Guid>
    {
        public Parcel(Guid id) : base(id) {}

        //Required by EntityFramework
        protected Parcel() :base(Guid.NewGuid()) {}

        private Parcel(Code parcelCode, string name) : this()
        {
            this.ParcelCode = parcelCode;
            this._name = Name;
        }

        public virtual Code ParcelCode { get; protected set; }

        private readonly string _name;
        public string Name => _name;
        
 
        #region Factory
            public static Parcel Create(int code, string name){
                if(code <= 0)
                    throw new InvalidCodeException("The code must be a positive number!");

                return new Parcel(
                    new Code(code)
                    , name);
            }
            
            public static Parcel Create(int code){
                if(code <= 0)
                    throw new InvalidCodeException("The code must be a positive number!");

                return new Parcel(
                    new Code(code)
                    , "");
            }
        #endregion
    }
}