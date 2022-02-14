namespace PropertyInterception.Interfaces
{
    public interface IPropertySetterInterceptor : IExceptionInterceptor, IExitInterceptor
    {
        /// <summary>
        /// Gets called before the value of the backingfield is set by the Setter. Return true to set the backingfield to the newValue or false to ignore the newValue.
        /// </summary>
        bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue);
    }
}
