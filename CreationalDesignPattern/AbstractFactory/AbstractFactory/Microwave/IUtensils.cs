using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory.Microwave
{
    public interface IUtensils
    {
        string GetType();
        double GetPrice();
        double GetSize();
    }
    public class Plate : IUtensils
    {
        public double GetPrice()
        {
            return 200000;
        }

        public double GetSize()
        {
            return 12;
        }

        string IUtensils.GetType()
        {
            return "Plate";
        }
    }
    public class Bowl : IUtensils
    {
        public double GetPrice()
        {
            return 120000;
        }

        public double GetSize()
        {
            return 10;
        }

        string IUtensils.GetType()
        {
            return "Bowl";
        }
    }
    public class PlateMV : IUtensils
    {
        public double GetPrice()
        {
            return 300000;
        }

        public double GetSize()
        {
            return 30;
        }

        string IUtensils.GetType()
        {
            return "Plate_MV";
        }
    }
    public class BowlMV : IUtensils
    {
        public double GetPrice()
        {
            return 400000;
        }

        public double GetSize()
        {
            return 11;
        }

        string IUtensils.GetType()
        {
            return "Bowl_MV";
        }
    }
}
