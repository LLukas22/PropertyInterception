using PropertyInterception.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInterception.Tests.Attributes
{
    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    internal class NormalAttribute : Attribute, IPropertyGetterInterceptor, IPropertySetterInterceptor
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
           
        }

        public bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue)
        {
            return true;
        }
    }
}
