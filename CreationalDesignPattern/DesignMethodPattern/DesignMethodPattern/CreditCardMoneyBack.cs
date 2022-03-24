namespace DesignMethodPattern
{
    public class CreditCardMoneyBack: CreditCardFactory
    {
        protected override ICreditCard MakeCreditCard()
        {
            return new MoneyBack();
        }
    }
}
