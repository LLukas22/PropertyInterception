namespace PropertyInterception.Interfaces
{
    public interface IPropertyGetterInterceptor : IExceptionInterceptor, IExitInterceptor
    {
        /// <summary>
        /// Gets called before the value of the backingfield is returned by the Getter.
        /// </summary>
        void OnGet(PropertyInterceptionInfo propertyInterceptionInfo, object currentValue);
    }
}
