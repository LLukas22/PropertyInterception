using System;

namespace PropertyInterception.Interfaces
{
    public interface IExceptionInterceptor
    {
        /// <summary>
        /// Gets triggered when a Exception in a Get/Set-Block gets thrown. Return true to throw the Exception or false to ignore it.
        /// </summary>
        bool OnException(PropertyInterceptionInfo propertyInterceptionInfo,Exception exception);
    }
}
