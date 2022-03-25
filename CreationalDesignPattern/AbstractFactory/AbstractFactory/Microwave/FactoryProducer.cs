using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Microwave
{
    public class FactoryProducer
    {

        public static AbstractUtensilsFactory GetFactory(string factoryType)
        {
            if (factoryType == "Microwave") return new MicrowaveSafeFactory();
            else if (factoryType == "Non-Microwave") return new MicrowaveNoSafeFactory();
            else return null;

        }
    }
}
