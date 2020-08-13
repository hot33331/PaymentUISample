using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CreditCardUISample.Services;
using CreditCardUISample.ViewModels;
using Xamarin.Forms;

namespace CreditCardUISample.Views
{
    [DesignTimeVisible(false)]
    public partial class CreditCardPage : ContentPage
    {
        public ICardScanService CardScanService { get; }
        
        private CreditCardPageViewModel vm;
        public CreditCardPage()
        {
            InitializeComponent();
            vm=new CreditCardPageViewModel();
            this.BindingContext = vm;
            CardScanService = DependencyService.Get<ICardScanService>();
        }
        
        async void ScanCard(object sender, EventArgs args)
        {
            if (CardScanService != null)
            {
                CardScanService.StartCapture();
                CreditCardInfo info = CardScanService.GetCardInfo();
                if (info != null)
                {
                    vm.CardCvv = info.CVV;
                    vm.CardNumber = info.Number;
                    vm.CardExpirationDate = info.ExpDate;
                    vm.CardHolderName = info.Name;
                }

            }
        }
    }
}
