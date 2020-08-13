namespace CreditCardUISample.Services
{
    public interface ICardScanService
    {
        void StartCapture();

        CreditCardInfo GetCardInfo();
    }
}