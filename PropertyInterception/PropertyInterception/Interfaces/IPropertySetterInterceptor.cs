namespace PropertyInterception.Interfaces
{
    public interface IPropertySetterInterceptor : IExceptionInterceptor, IExitInterceptor
    {
        bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue);
    }
}
