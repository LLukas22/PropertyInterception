using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PropertyInterception
{

    [Generator]
    public class PropertyInterceptionGenerator : ISourceGenerator
    {
        private static readonly DiagnosticDescriptor TopLevelError = new DiagnosticDescriptor(id: "PIGEN001",
                                                                                              title: "Must be Top-Level",
                                                                                              messageFormat: "'{0}' must be on the Top-Level of the Namespace",
                                                                                              category: "PIGenerator",
                                                                                              DiagnosticSeverity.Error,
                                                                                              isEnabledByDefault: true);



        private static readonly DiagnosticDescriptor NameError = new DiagnosticDescriptor(id: "PIGEN002",
                                                                                              title: "Invalide Property Name",
                                                                                              messageFormat: "Can't generate Propertyname for Field '{0}'",
                                                                                              category: "PIGenerator",
                                                                                              DiagnosticSeverity.Error,
                                                                                              isEnabledByDefault: true); 
        public void Initialize(GeneratorInitializationContext context)
        {
            // Register a syntax receiver that will be created for each generation pass
            context.RegisterForSyntaxNotifications(() => new PropertyInterceptionSyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // retrieve the populated receiver 
            if (!(context.SyntaxContextReceiver is PropertyInterceptionSyntaxReceiver receiver))
                return;

            //Check if we need to generate Something
            if (receiver.Fields.Count < 1)
                return;

            // group the fields by class, and generate the source
            foreach (IGrouping<INamedTypeSymbol, FieldContainer> group in receiver.Fields.GroupBy(f => f.FieldSymbol.ContainingType))
            {
                string classSource = ProcessClass(group.Key, group.ToList(), context);
                context.AddSource($"{group.Key.Name}_PropertyInterception.cs", SourceText.From(classSource, Encoding.UTF8));
            }
        }

        private string ProcessClass(INamedTypeSymbol classSymbol, List<FieldContainer> fields, GeneratorExecutionContext context)
        {
            if (!classSymbol.ContainingSymbol.Equals(classSymbol.ContainingNamespace, SymbolEqualityComparer.Default))
            {
                context.ReportDiagnostic(Diagnostic.Create(TopLevelError, Location.None, classSymbol.ContainingSymbol));
                return null; //TODO: issue a diagnostic that it must be top level
            }

            string namespaceName = classSymbol.ContainingNamespace.ToDisplayString();

            // begin building the generated source
            StringBuilder source = new StringBuilder($@"
using System;
using PropertyInterception;

namespace {namespaceName}
{{
    public partial class {classSymbol.Name}
    {{
");

            // create properties for each field 
            foreach (FieldContainer fieldContainer in fields)
            {
                ProcessField(source, fieldContainer, context);
            }

            source.Append("\t}\n}");
            return source.ToString();
        }

        private void ProcessField(StringBuilder source, FieldContainer fieldContainer, GeneratorExecutionContext context)
        {
            // get the name and type of the field
            string fieldName = fieldContainer.FieldSymbol.Name;
            ITypeSymbol fieldType = fieldContainer.FieldSymbol.Type;

            string propertyName = chooseName(fieldName);
            if (propertyName.Length == 0 || propertyName == fieldName)
            {
                context.ReportDiagnostic(Diagnostic.Create(NameError, Location.None, $"{fieldContainer.FieldSymbol.ContainingType}.{fieldName}"));
                return;
            }

            string attributeName = $"{fieldName}_propertyInterceptionAttribute";
            string propertyInterceptionInfo = $"{fieldName}_propertyInterceptionInfo";
            source.AppendLine($"\t\t[NonSerialized]\n\t\tprivate {fieldContainer.AttributeData.AttributeClass} {attributeName};");
            source.AppendLine($"\t\t[NonSerialized]\n\t\tprivate {nameof(PropertyInterceptionInfo)} {propertyInterceptionInfo};");
            source.AppendLine($@"
         
        public {fieldType} {propertyName} 
        {{
            get
            {{
                {GenerateGetter(fieldContainer, attributeName, propertyInterceptionInfo)}
            }}

            set
            {{
                {GenerateSetter(fieldContainer, attributeName, propertyInterceptionInfo)}
            }}      
        }}
");

        }

        private string GenerateSetter(FieldContainer fieldContainer, string attribute_name, string propertyInterceptionInfo)
        {
            if (!fieldContainer.ShouldGenerateSetter)
                return $"this.{fieldContainer.FieldSymbol.Name} = value;";
            return $@"
                {EnsurePropertyInterceptionInfo(fieldContainer, propertyInterceptionInfo)}

                {EnsureInterceptionAttribute(fieldContainer, attribute_name)}

                try
                {{
                    if({attribute_name}.OnSet(this.{propertyInterceptionInfo}, this.{fieldContainer.FieldSymbol.Name}, value))
                    {{
                        this.{fieldContainer.FieldSymbol.Name} = value;
                    }}
                }}
                catch (Exception e)
                {{
                    if({attribute_name}.OnException(e))
                        throw;
                }}
                finally
                {{
                    {attribute_name}.OnExit();
                }}
        ";
        }

        private string GenerateGetter(FieldContainer fieldContainer, string attribute_name, string propertyInterceptionInfo)
        {
            if (!fieldContainer.ShouldGenerateGetter)
                return $"return this.{fieldContainer.FieldSymbol.Name};";

            return $@"
                {EnsurePropertyInterceptionInfo(fieldContainer, propertyInterceptionInfo)}

                {EnsureInterceptionAttribute(fieldContainer, attribute_name)}

                try
                {{
                    {attribute_name}.OnGet(this.{propertyInterceptionInfo}, this.{fieldContainer.FieldSymbol.Name});
                    return this.{fieldContainer.FieldSymbol.Name};
                }}
                catch (Exception e)
                {{
                    if({attribute_name}.OnException(e))
                        throw;
                    else
                        return this.{fieldContainer.FieldSymbol.Name};
                }}
                finally
                {{
                    {attribute_name}.OnExit();
                }}
        ";
        }

        private object EnsureInterceptionAttribute(FieldContainer fieldContainer, string attribute_name)
        {
            return $@"if(this.{attribute_name} == null)
                {{
                    this.{attribute_name} = new {fieldContainer.AttributeData.AttributeClass}();
                }}";
        }

        private string EnsurePropertyInterceptionInfo(FieldContainer fieldContainer, string propertyInterceptionInfo)
        {
            return $@"if(this.{propertyInterceptionInfo} == null)
                {{
                    this.{propertyInterceptionInfo} = new PropertyInterceptionInfo(this,""{fieldContainer.FieldSymbol.Name}"");
                }}";
        }
        string chooseName(string fieldName)
        {
            fieldName = fieldName.TrimStart('_');
            if (fieldName.Length == 0)
                return string.Empty;

            if (fieldName.Length == 1)
                return fieldName.ToUpper();

            return fieldName.Substring(0, 1).ToUpper() + fieldName.Substring(1);
        }
    }
}
