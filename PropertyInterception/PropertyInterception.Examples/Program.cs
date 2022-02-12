using PropertyInterception.Examples.Models;
using System;


namespace PropertyInterception.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            
            Console.WriteLine(person.Firstname);

            var animal = new Animal();
            animal.Type = "asdasd";
        }
    }
}
