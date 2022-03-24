using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignMethodPattern
{
    public abstract class CreditCardFactory
    {
        protected abstract ICreditCard MakeCreditCard();
        public ICreditCard GetCreditCard()
        {
            return MakeCreditCard();
        }
    }
}
