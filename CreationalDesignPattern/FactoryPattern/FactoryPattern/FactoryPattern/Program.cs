using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryPattern
{
    internal class Program
    {

        /// <summary>
        /// Problem of the code below
        /// 1, Tight Coupling(Program vs subclass creditCard)
        /// 2, Violating the Open for extension Closed for Modification of SOLID Principles
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
           
            string cardType = "Titanium";

            //Traditional Code
            Console.WriteLine("-----Traditional Code-------");
            ICreditCard card = null;
            if(cardType == "MoneyBack")
            {
                card = new MoneyBack();
            }else if(cardType == "Titanium")
            {
                card = new Titanium();
            }else if (cardType == "Platium")
            {
                card = new Platium();
            }

            if(card!= null)
            {
                Console.WriteLine($"CardType: {card.GetCardType()}");
                Console.WriteLine($"CreditLimit: {card.GetCardLimit()}");
                Console.WriteLine($"AnnualCharge: {card.GetAnnualCharge()}");
            }
            Console.WriteLine("-----DONE-------");

            //Factory Pattern
            Console.WriteLine("------Factory Pattern----");
            var creditCard = CreditCardFactory.GetCreditCard(cardType);
            Console.WriteLine($"CardType: {creditCard.GetCardType()}");
            Console.WriteLine($"CreditLimit: {creditCard.GetCardLimit()}");
            Console.WriteLine($"AnnualCharge: {creditCard.GetAnnualCharge()}");

            Console.WriteLine("-----DONE-------");
        }
    }
}
