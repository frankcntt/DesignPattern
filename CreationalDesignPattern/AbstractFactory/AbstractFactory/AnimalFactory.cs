using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public abstract class AnimalFactory
    {
        public abstract IAnimal GetAnimal(string animalType);
        public static  AnimalFactory GetAnimalFactory(string factoryType)
        {
            if (factoryType == "Sea")
                return new  SeaAnimalFactory();
            else return new LandAnimalFactory();
        }
    }
}
