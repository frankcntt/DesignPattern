namespace DesignMethodPattern
{
    public class CreditCardPlatinum: CreditCardFactory
    {
        protected override ICreditCard MakeCreditCard()
        {
            return new Platinum();
        }
    }
}
