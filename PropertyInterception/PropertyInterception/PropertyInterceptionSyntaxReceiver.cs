using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PropertyInterception.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace PropertyInterception
{
    /// <summary>
    /// Created on demand before each generation pass
    /// </summary>
    internal class PropertyInterceptionSyntaxReceiver : ISyntaxContextReceiver
    {
        public List<FieldContainer> Fields { get; } = new List<FieldContainer>();

        public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
        {
            // any field with at least one attribute is a candidate for property generation
            if (context.Node is FieldDeclarationSyntax fieldDeclarationSyntax && fieldDeclarationSyntax.AttributeLists.Count > 0)
            {
                foreach (VariableDeclaratorSyntax variable in fieldDeclarationSyntax.Declaration.Variables)
                {
                    // Get the symbol being declared by the field, and keep it if its annotated
                    IFieldSymbol fieldSymbol = context.SemanticModel.GetDeclaredSymbol(variable) as IFieldSymbol;
                    var attributes = fieldSymbol.GetAttributes();

                    if (attributes.Length > 1)
                        continue;

                    //Check if the Attribute implements IPropertyGetterInterceptor or IPropertySetterInterceptor
                    if (attributes[0].AttributeClass.AllInterfaces.Any(i => (i.MetadataName == typeof(IPropertyGetterInterceptor).Name || i.MetadataName == typeof(IPropertySetterInterceptor).Name))){
                        Fields.Add(new(fieldSymbol, attributes[0]));
                    }
                }
            }
        }
    }
}
