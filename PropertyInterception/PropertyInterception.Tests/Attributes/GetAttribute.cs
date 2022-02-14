using PropertyInterception.Interfaces;
using System;

namespace PropertyInterception.Tests.Attributes
{
    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    internal class GetAttribute : Attribute, IPropertyGetterInterceptor
    {
        public bool OnException(PropertyInterceptionInfo propertyInterceptionInfo,Exception exception)
        {
            return true;
        }

        public void OnExit(PropertyInterceptionInfo propertyInterceptionInfo)
        {
            
        }

        public void OnGet(PropertyInterceptionInfo propertyInterceptionInfo, object currentValue)
        {
            propertyInterceptionInfo.SetValue("GetAttribute");
        }
    }
}
