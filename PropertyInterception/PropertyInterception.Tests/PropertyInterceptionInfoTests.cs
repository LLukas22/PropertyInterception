using NUnit.Framework;
using System;

namespace PropertyInterception.Tests
{
    [TestFixture]
    public class PropertyInterceptionInfoTests
    {

        class Child
        {
            public Guid Id;
            public Child()
            {
                Id = Guid.NewGuid();
            }
        }

        class Person
        {
            public string Name => name;
            private string name;

            public Child Child => child;
            private Child child;
        }

        [Test]
        public void PropertyInterceptionInfo_Can_Set_Value()
        {
            var person = new Person();
            var info = new PropertyInterceptionInfo(person,"name");
            ;

            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => info.SetValue("foobar"));
                Assert.AreEqual("foobar", person.Name);
            });
        }

        [Test]
        public void PropertyInterceptionInfo_Can_Get_Value()
        {
            var person = new Person();
            var info = new PropertyInterceptionInfo(person, "name");

            Assert.Multiple(() =>
            {
                string value = string.Empty;
                Assert.DoesNotThrow(() => info.SetValue("foobar"));
                Assert.DoesNotThrow(() => value =  info.GetValue<string>());
                Assert.AreEqual(person.Name,value);
            });
        }


        [Test]
        public void PropertyInterceptionInfo_Can_Set_Object_Reference()
        {
            var person = new Person();
            var info = new PropertyInterceptionInfo(person, "child");
            var child = new Child();
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => info.SetValue(child));
                Assert.AreEqual(child.Id, person.Child.Id);
                Assert.AreSame(child, person.Child);
            });
        }


        [Test]
        public void PropertyInterceptionInfo_Set_Throws_When_Type_Missmatches()
        {
            var person = new Person();
            var info = new PropertyInterceptionInfo(person, "name");
            Assert.Throws<ArgumentException>(() => info.SetValue(true));
        }


        [Test]
        public void PropertyInterceptionInfo_Throws_When_Instance_Is_Null()
        {
            Assert.Throws<ArgumentException>(() => new PropertyInterceptionInfo(null, "name"));
        }

        [Test]
        public void PropertyInterceptionInfo_Throws_When_PropertyName_Is_Empty()
        {
            Assert.Throws<ArgumentException>(() => new PropertyInterceptionInfo(new Person(), ""));
        }

        [Test]
        public void PropertyInterceptionInfo_Throws_When_Instance_Doesnt_Contain_Property()
        {
            Assert.Throws<ArgumentException>(() => new PropertyInterceptionInfo(new Person(), "notintheclass"));
        }
    }
}
