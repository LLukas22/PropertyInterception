using PropertyInterception.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyInterception.Tests.Attributes
{
    internal class SetAttribute : Attribute, IPropertySetterInterceptor
    {
        public bool OnException(PropertyInterceptionInfo propertyInterceptionInfo,Exception exception)
        {
            return true;
        }

        public void OnExit(PropertyInterceptionInfo propertyInterceptionInfo)
        {
           
        }

        public bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue)
        {
            propertyInterceptionInfo.SetValue("SetAttribute");
            return false;
        }
    }
}
