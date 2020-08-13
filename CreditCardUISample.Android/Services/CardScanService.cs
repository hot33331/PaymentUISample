using System;
using Android.App;
using Android.Content;
using Android.Views;
using Card.IO;
using CreditCardUISample.Droid.Services;
using CreditCardUISample.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(CardScanService))]

namespace CreditCardUISample.Droid.Services
{
    public class CardScanService : ICardScanService
    {
        private Activity activity;

        public void StartCapture()
        {
            InitCardService();

            var intent = new Intent(activity, typeof(CardIOActivity));
            intent.PutExtra(CardIOActivity.ExtraRequireExpiry, true);
            intent.PutExtra(CardIOActivity.ExtraRequireCvv, true);
            intent.PutExtra(CardIOActivity.ExtraRequirePostalCode, false);
            intent.PutExtra(CardIOActivity.ExtraUseCardioLogo, true);

            activity.StartActivityForResult(intent, 101);
        }

        public CreditCardInfo GetCardInfo()
        {
            if (InfoShareHelper.Instance.CardInfo != null)
            {
                var cardInfo = InfoShareHelper.Instance.CardInfo;
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
            // Init current activity
            var context = Forms.Context;
            activity = context as Activity;
        }
    }

    public class InfoShareHelper
    {
        private static InfoShareHelper instance = null;
        private static readonly object padlock = new object();

        public CreditCard CardInfo { get; set; }

        public static InfoShareHelper Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new InfoShareHelper();
                    }

                    return instance;
                }
            }
        }
    }
}