using PropertyInterception.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInterception.Examples.Attributes
{
    internal class ExceptionAttribute : Attribute, IPropertyGetterInterceptor, IPropertySetterInterceptor
    {
        public bool OnException(Exception exception)
        {
            if (exception.Message == "foobar")
                return false;
            return true;
        }

        public void OnExit()
        {
            
        }

        public void OnGet(PropertyInterceptionInfo propertyInterceptionInfo, object currentValue)
        {
            
        }

        public bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue)
        {
            throw new Exception("foobar");
        }
    }
}
