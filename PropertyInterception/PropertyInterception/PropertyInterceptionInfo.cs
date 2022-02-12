using System;
using System.Reflection;

namespace PropertyInterception
{
    /// <summary>
    /// Contains Property Information 
    /// </summary>
    public class PropertyInterceptionInfo
    {
        /// <summary>
        /// Object Instance of the Parent
        /// </summary>
        public object Instance { get; private set; }
        /// <summary>
        /// Type of the Parent
        /// </summary>
        public Type DeclaringType => Instance.GetType();
        /// <summary>
        /// Name of the Property
        /// </summary>
        public string PropertyName { get; private set; }
        /// <summary>
        /// Type of the Property
        /// </summary>
        public Type PropertyType => field?.FieldType;

        private FieldInfo field;
        public PropertyInterceptionInfo(object instace,string propertyName)
        {
            this.Instance = instace;
            this.PropertyName = propertyName;

            field = DeclaringType.GetField(propertyName, BindingFlags.NonPublic | BindingFlags.Instance);
        }

        /// <summary>
        /// Gets the current value of the Property
        /// </summary>
        public object GetValue()
        {
            return field?.GetValue(Instance);
        }

        /// <summary>
        /// Sets the value of the Property
        /// </summary>
        public void SetValue(object value)
        {
            field.SetValue(Instance, value);
        }
    }
}