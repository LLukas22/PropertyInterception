namespace PropertyInterception.Interfaces
{
    public interface IPropertyGetterInterceptor : IExceptionInterceptor, IExitInterceptor
    {
        void OnGet(PropertyInterceptionInfo propertyInterceptionInfo, object currentValue);
    }
}
