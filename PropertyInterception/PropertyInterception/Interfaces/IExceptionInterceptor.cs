using System;

namespace PropertyInterception.Interfaces
{
    public interface IExceptionInterceptor
    {
        bool OnException(Exception exception);
    }
}
