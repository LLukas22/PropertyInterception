using NUnit.Framework;
using PropertyInterception.Tests.GeneratorTargets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInterception.Tests
{
    [TestFixture]
    public class PropertyInterceptionGeneratorTests
    {
        [Test]
        public void Generated_Get_Set_Should_Work()
        {
            var dummyObject = new DummyObject();

            Assert.DoesNotThrow(() =>
            
                dummyObject.NormalProperty = "foobar"
            );
            Assert.AreEqual("foobar", dummyObject.NormalProperty);
        }


        [Test]
        public void Generated_Get_Should_Intercept_Getter()
        {
            var dummyObject = new DummyObject();

            Assert.DoesNotThrow(() =>

                dummyObject.GetProperty = "foobar"
            );
            Assert.AreEqual("GetAttribute", dummyObject.GetProperty);
        }


        [Test]
        public void Generated_Set_Should_Intercept_Setter()
        {
            var dummyObject = new DummyObject();

            Assert.DoesNotThrow(() =>

                dummyObject.SetProperty = "foobar"
            );
            Assert.AreEqual("SetAttribute", dummyObject.SetProperty);
        }


        [Test]
        public void OnExit_Is_Triggered_OnSet()
        {
            var dummyObject = new DummyObject();

            Assert.DoesNotThrow(() =>

                dummyObject.OnExitProperty = "foobar"
            );
            Assert.IsTrue(dummyObject.TriggeredOnExit);
        }

        [Test]
        public void OnExit_Is_Triggered_OnGet()
        {
            var dummyObject = new DummyObject();

            Assert.DoesNotThrow(() =>
                { var test = dummyObject.OnExitProperty; }
            );
            Assert.IsTrue(dummyObject.TriggeredOnExit);
        }


        [Test]
        public void OnException_Is_Triggered_OnGetException()
        {
            var dummyObject = new DummyObject();

            Assert.DoesNotThrow(() =>
            { var test = dummyObject.OnExceptionProperty; }
            );
            Assert.IsTrue(dummyObject.TriggeredException);
        }

        [Test]
        public void OnException_Is_Triggered_OnSetException()
        {
            var dummyObject = new DummyObject();

            Assert.DoesNotThrow(() =>
            { dummyObject.OnExceptionProperty = "foobar"; }
            );
            Assert.IsTrue(dummyObject.TriggeredException);
        }
    }
}
