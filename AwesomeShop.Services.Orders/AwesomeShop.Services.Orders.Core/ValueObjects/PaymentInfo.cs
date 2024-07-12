namespace AwesomeShop.Services.Orders.Core.ValueObjects
{
    public class PaymentInfo
    {
        public PaymentInfo(string cardNumber, string cardHolderName, string expiryDate, string cvv)
        {
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            ExpiryDate = expiryDate;
            CVV = cvv;
        }

        public string CardNumber { get; private set; }
        public string CardHolderName { get; private set; }
        public string ExpiryDate { get; private set; }
        public string CVV { get; private set; }

        public override bool Equals(object obj)
        {
            return obj is PaymentInfo info &&
                   CardNumber == info.CardNumber &&
                   CardHolderName == info.CardHolderName &&
                   ExpiryDate == info.ExpiryDate &&
                   CVV == info.CVV;
        }

        public override int GetHashCode()
        {
            return System.HashCode.Combine(CardNumber, CardHolderName, ExpiryDate, CVV);
        }
    }
}
