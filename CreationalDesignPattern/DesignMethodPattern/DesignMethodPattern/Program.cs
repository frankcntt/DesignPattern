using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignMethodPattern
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------Factory Method--------------");
            ICreditCard card = new CreditCardMoneyBack().GetCreditCard();
            Console.WriteLine($"CardType: {card.GetCardType()}");
            Console.WriteLine($"CreditLimit: {card.GetCreditLimit()}");
            Console.WriteLine($"AnnualCharge: {card.GetAnnualCharge()}");
        }
    }
}
