

using PropertyInterception.Examples.Attributes;
using System;

namespace PropertyInterception.Examples.Models
{
    public partial class Person
    {
        [HelloWorld]
        private string firstname;

        [HelloWorld]
        private string lastname;
    }
}
