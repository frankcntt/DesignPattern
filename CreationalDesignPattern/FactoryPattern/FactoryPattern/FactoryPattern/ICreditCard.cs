using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    public interface ICreditCard
    {
        string GetCardType();
        int GetCardLimit();
        int GetAnnualCharge();
    }
    class MoneyBack : ICreditCard
    {
        public int GetAnnualCharge()
        {
            return 500;
        }

        public int GetCardLimit()
        {
           return 15000;
        }

        public string GetCardType()
        {
            return "MoneyBack";
        }
    }
    class Titanium : ICreditCard
    {
        public int GetAnnualCharge()
        {
            return 1500;
        }

        public int GetCardLimit()
        {
            return 25000;
        }

        public string GetCardType()
        {
            return "Titanium";
        }
    }
    class Platium : ICreditCard
    {
        public int GetAnnualCharge()
        {
            return 2000;
        }

        public int GetCardLimit()
        {
            return 35000;
        }

        public string GetCardType()
        {
            return "Platium";
        }
    }
}
