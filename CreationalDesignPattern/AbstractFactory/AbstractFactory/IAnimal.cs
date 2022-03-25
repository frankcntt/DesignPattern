using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    public interface IAnimal
    {
        string Speak();
    }
    public class Cat : IAnimal
    {
        public string Speak()
        {
            return "MEO MEO";
        }
    }
    public class Dog: IAnimal
    {
        public string Speak()
        {
            return "GAU GAU";
        }
    }
    public class Lion : IAnimal
    {
        public string Speak()
        {
            return "Gu Gu";
        }
    }
    public class Octopus : IAnimal
    {
        public string Speak()
        {
            return "Octopus ";
        }
    }
    public class Shark : IAnimal
    {
        public string Speak()
        {
            return "Can't Speak";
        }
    }
}
