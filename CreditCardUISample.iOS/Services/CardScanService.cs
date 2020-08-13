using System;
using Card.IO;
using CreditCardUISample.iOS.Services;
using CreditCardUISample.Services;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(CardScanService))]
namespace CreditCardUISample.iOS.Services
{
    public class CardScanService:ICardIOPaymentViewControllerDelegate,ICardScanService
    {
        public IntPtr Handle { get; }
        private UIViewController rootViewController;
        private Card.IO.CreditCardInfo cardInfo;
        public void Dispose()
        {
            
        }

        public void UserDidCancelPaymentViewController(CardIOPaymentViewController paymentViewController)
        {
            Console.WriteLine("Scanning Cancelled!");
        }

        public void UserDidProvideCreditCardInfo(Card.IO.CreditCardInfo cardInfo, CardIOPaymentViewController paymentViewController)
        {
       
            if (cardInfo == null)
            {
                Console.WriteLine("Scanning Cancelled!");
            }
            else 
            {
                this.cardInfo = cardInfo;
            }

            paymentViewController.DismissViewController(true, null);
        }
        

        public void StartCapture()
        {
            InitCardService();
            var paymentViewController = new CardIOPaymentViewController(this);
            rootViewController.PresentViewController(paymentViewController, true, null);
        }

        public CreditCardInfo GetCardInfo()
        {
            if (cardInfo != null)
            {
                CreditCardInfo info=new CreditCardInfo();
                info.Name = cardInfo.CardholderName;
                info.Number = cardInfo.CardNumber;
                if (!String.IsNullOrWhiteSpace(cardInfo.ExpiryMonth.ToString()) &&
                    !String.IsNullOrWhiteSpace(cardInfo.ExpiryYear.ToString()))
                {
                    info.ExpDate = cardInfo.ExpiryMonth + "/" + cardInfo.ExpiryYear;
                }

                info.CVV = cardInfo.Cvv;
                return info;
            }

            return null;
        }


        private void InitCardService()
        {
            // Init rootViewController
            var window = UIApplication.SharedApplication.KeyWindow;
            rootViewController = window.RootViewController;
            while (rootViewController.PresentedViewController != null)
            {
                rootViewController = rootViewController.PresentedViewController;
            }
        }
    }
}