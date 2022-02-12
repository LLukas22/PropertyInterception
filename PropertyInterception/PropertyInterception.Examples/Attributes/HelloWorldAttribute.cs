using PropertyInterception.Interfaces;
using System;

namespace PropertyInterception.Examples.Attributes
{
    [AttributeUsage(System.AttributeTargets.Field, AllowMultiple = false)]
    public class HelloWorldAttribute : Attribute, IPropertyGetterInterceptor,IPropertySetterInterceptor
    {
        public HelloWorldAttribute()
        {

        }
        public bool OnException(Exception exception)
        {
            return false;
        }

        public void OnExit()
        {
            Console.WriteLine("[OnExit] Hello World from Attribute!");
        }

        public void OnGet(PropertyInterceptionInfo propertyInterceptionInfo, object currentValue)
        {
            Console.WriteLine("[OnGet] Hello World from Attribute!");
        }

        public bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue)
        {
            Console.WriteLine("[OnSet] Hello World from Attribute!");
            return true;
        }
    }
}
