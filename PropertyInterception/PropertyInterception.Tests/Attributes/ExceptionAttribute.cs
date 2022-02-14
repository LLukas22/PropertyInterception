using PropertyInterception.Interfaces;
using PropertyInterception.Tests.GeneratorTargets;
using System;

namespace PropertyInterception.Tests.Attributes
{
    internal class ExceptionAttribute : Attribute, IPropertyGetterInterceptor, IPropertySetterInterceptor
    {
        public bool OnException(PropertyInterceptionInfo propertyInterceptionInfo,Exception exception)
        {
            (propertyInterceptionInfo.Instance as DummyObject).TriggeredException = true;
            //Void the Exception
            return false;
        }

        public void OnExit(PropertyInterceptionInfo propertyInterceptionInfo)
        {
           
        }

        public void OnGet(PropertyInterceptionInfo propertyInterceptionInfo, object currentValue)
        {
            throw new Exception();
        }

        public bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue)
        {
            throw new Exception();
        }
    }
}
