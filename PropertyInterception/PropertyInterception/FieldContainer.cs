using Microsoft.CodeAnalysis;
using PropertyInterception.Interfaces;
using System.Linq;

namespace PropertyInterception
{
    internal class FieldContainer
    {
        public IFieldSymbol FieldSymbol { get; }
        public AttributeData AttributeData { get; }

        public bool ShouldGenerateSetter { get; }

        public bool ShouldGenerateGetter { get; }

        public FieldContainer(IFieldSymbol fieldSymbol,AttributeData attributeData)
        {
            FieldSymbol=fieldSymbol;
            AttributeData=attributeData;
            var interfaces = AttributeData.AttributeClass.AllInterfaces;
            ShouldGenerateSetter = interfaces.Any(x => x.MetadataName == typeof(IPropertySetterInterceptor).Name);
            ShouldGenerateGetter = interfaces.Any(x => x.MetadataName == typeof(IPropertyGetterInterceptor).Name);
        }
    }
}
