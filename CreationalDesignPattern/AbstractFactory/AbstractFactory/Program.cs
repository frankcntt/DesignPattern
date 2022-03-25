using AbstractFactory.Microwave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //AnimalFactory factory = AnimalFactory.GetAnimalFactory("Sea");
            //var animal = factory.GetAnimal("Shark");
            //Console.WriteLine($"{animal.GetType().Name} =>  Speak {animal.Speak()}");

            //factory = AnimalFactory.GetAnimalFactory("Land");
            //animal = factory.GetAnimal("Dog");
            //Console.WriteLine($"{animal.GetType().Name} =>  Speak {animal.Speak()}");

            var factory = FactoryProducer.GetFactory("Non-Microwave");
            var microwave = factory.GetBowl();
            Console.WriteLine($"Type: {microwave.GetType()}");
            Console.WriteLine($"Size: {microwave.GetSize()}");
            Console.WriteLine($"Price: {microwave.GetPrice()}");
        }
    }
}
