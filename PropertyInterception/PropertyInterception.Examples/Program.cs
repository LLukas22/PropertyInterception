using PropertyInterception.Examples.Models;
using System;


namespace PropertyInterception.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var person = new Person();
            //Hello World from Attribute will be set at person.Firstname on Get
            Console.WriteLine(person.Firstname);

            var animal = new Animal();
            //Throws an exeption on Set and catches it 
            animal.Type = "asdasd";
        }
    }
}
