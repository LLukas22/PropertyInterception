using PropertyInterception.Examples.Attributes;
using System;

namespace PropertyInterception.Examples.Models
{
    public partial class Animal
    {
        [Exception]
        string type;
    }
}
