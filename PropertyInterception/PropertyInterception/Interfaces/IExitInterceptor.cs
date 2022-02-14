using System;
using System.Collections.Generic;
using System.Text;

namespace PropertyInterception.Interfaces
{
    public interface IExitInterceptor
    {
        /// <summary>
        /// Gets triggered after a Setter/Getter is exited.
        /// </summary>
        void OnExit(PropertyInterceptionInfo propertyInterceptionInfo);
    }
}
