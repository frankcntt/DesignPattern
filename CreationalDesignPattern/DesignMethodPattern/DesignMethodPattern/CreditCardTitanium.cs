namespace DesignMethodPattern
{
    public class CreditCardTitanium: CreditCardFactory
    {
        protected override ICreditCard MakeCreditCard()
        {
            return new Titanium();
        }
    }
}
