using System;
using AgroPlan.Property.AgroPlan.Core;
using AgroPlan.Property.AgroPlan.Core.Exceptions;
using AgroPlan.Property.AgroPlan.Core.OwnerAggregate;
using Xunit;

namespace AgroPlan.Property.AgroPlan.Property.CoreTest.Models.OwnerTest
{
    
    public class OwnerShould
    {
        [Fact]
        public void BeAnEntity()
        {
            var owner = new Owner("7842239457394");
            Assert.IsAssignableFrom<Entity<string>>(owner);
        }

        [Fact]
        public void Throw_NullArgumentException_FirstName(){
            
            Assert.Throws<ArgumentNullException>(
                () => Owner.Create("7842239457394","","LastName")
            );
        }

        [Fact]
        public void Throw_NullArgumentException_LastName()
        {
            Assert.Throws<ArgumentNullException>(
                () => Owner.Create("7842239457394","FirstName","")
            );
        }

        [Fact]
        public void Throw_InvalidOwnerIdException()
        {
            Assert.Throws<InvalidOwnerIdException>(
                () => Owner.Create("3423", "FirstName", "LastName")
            );
        }   
    }
}