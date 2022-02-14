# PropertyInterception [![NuGet](https://img.shields.io/nuget/v/PropertyInterception.svg)](https://www.nuget.org/packages/PropertyInterception/)

Cauldron like [Property-Interception](https://github.com/Capgemini/Cauldron/wiki/Property-interception) using C# Source Generators.
This Package allows you to control the Getter- and Setter-Behaviour of your Properties via an Attribute. 

## Installation
---
Via Nuget: <code>Install-Package PropertyInterception</code> 
<br>
Or download a [Release](https://github.com/LLukas22/PropertyInterception/releases).

## How it works
---
Your code:
```C#
internal class InterceptionAttribute : Attribute, IPropertyGetterInterceptor, IPropertySetterInterceptor
{
    public bool OnException(PropertyInterceptionInfo propertyInterceptionInfo, Exception exception)
    {
        //Do Something on Error
        return false;
    }

    public void OnExit(PropertyInterceptionInfo propertyInterceptionInfo)
    {
        //Do Something on Exit
    }

    public void OnGet(PropertyInterceptionInfo propertyInterceptionInfo, object currentValue)
    {
        //Do Something on Get
    }

    public bool OnSet(PropertyInterceptionInfo propertyInterceptionInfo, object oldValue, object newValue)
    {
        //Do Something on Set
        return true;
    }
}
```
```C#
public partial class Person
{
    [Interception]
    private string name;
}
```

What gets generated:

```C#
public partial class Person
{
    [NonSerialized]
    private NugetTest.InterceptionAttribute name_propertyInterceptionAttribute;
    [NonSerialized]
    private PropertyInterceptionInfo name_propertyInterceptionInfo;

        
    public string Name 
    {
        get
        {
            
            if(this.name_propertyInterceptionInfo == null)
            {
                this.name_propertyInterceptionInfo = new PropertyInterceptionInfo(this,"name");
            }

            if(this.name_propertyInterceptionAttribute == null)
            {
                this.name_propertyInterceptionAttribute = new NugetTest.InterceptionAttribute();
            }

            try
            {
                name_propertyInterceptionAttribute.OnGet(this.name_propertyInterceptionInfo, this.name);
                return this.name;
            }
            catch (Exception e)
            {
                if(name_propertyInterceptionAttribute.OnException(this.name_propertyInterceptionInfo,e))
                    throw;
                else
                    return this.name;
            }
            finally
            {
                name_propertyInterceptionAttribute.OnExit(this.name_propertyInterceptionInfo);
            }
    
        }

        set
        {
            
            if(this.name_propertyInterceptionInfo == null)
            {
                this.name_propertyInterceptionInfo = new PropertyInterceptionInfo(this,"name");
            }

            if(this.name_propertyInterceptionAttribute == null)
            {
                this.name_propertyInterceptionAttribute = new NugetTest.InterceptionAttribute();
            }

            try
            {
                if(name_propertyInterceptionAttribute.OnSet(this.name_propertyInterceptionInfo, this.name, value))
                {
                    this.name = value;
                }
            }
            catch (Exception e)
            {
                if(name_propertyInterceptionAttribute.OnException(this.name_propertyInterceptionInfo,e))
                    throw;
            }
            finally
            {
                name_propertyInterceptionAttribute.OnExit(this.name_propertyInterceptionInfo);
            }
    
        }      
    }

}
```

See PropertyInterception.Tests for more examples.