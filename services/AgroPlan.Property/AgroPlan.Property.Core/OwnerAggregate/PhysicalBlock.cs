using System;
using AgroPlan.Property.AgroPlan.Property.Core.ValueObjects;
using AgroPlan.Property.AgroPlan.Property.Core.Exceptions;

namespace AgroPlan.Property.AgroPlan.Property.Core.OwnerAggregate{
    public class PhysicalBlock : Entity<Guid>
    {
        public PhysicalBlock(Guid id) : base(id) {}

        //Required by EntityFramework    
        protected PhysicalBlock() : base(Guid.NewGuid()) {}

        protected PhysicalBlock(Code code, string name) : this()
        {
            this.Code = code;
            this._name = name;
        }

        public virtual Code Code { get; protected set; }

        private readonly string _name;
        public string Name => _name;

        #region Factory
            public static PhysicalBlock Create(int code, string name){
                if(code <= 0)
                    throw new InvalidCodeException("The code must a positive number!");

                return new PhysicalBlock(
                    new Code(code),
                    name
                );
            }

            public static PhysicalBlock Create(int code){
                if(code <= 0)
                    throw new InvalidCodeException("The code must a positive number!");

                return new PhysicalBlock(
                    new Code(code),
                    ""
                );
            }
        #endregion
    }
}