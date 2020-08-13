using System;
using System.ComponentModel;
using CreditCardUISample.ViewModels;
using PayPal.Forms;
using PayPal.Forms.Abstractions;
using Xamarin.Forms;

namespace CreditCardUISample.Views
{
    [DesignTimeVisible(false)]
    public partial class CreditCardPage : ContentPage
    {
        private CreditCardPageViewModel vm;
        public CreditCardPage()
        {
            InitializeComponent();
            vm=new CreditCardPageViewModel();
            this.BindingContext = vm;
        }
        
        async void ScanCard(object sender, EventArgs args)
        {

            var result = await CrossPayPalManager.Current.ScanCard();
                if (result.Status==PayPalStatus.Successful)
                {
                    vm.CardCvv = result.Card.Cvv;
                    vm.CardNumber = result.Card.CardNumber;
                    vm.CardExpirationDate = result.Card.ExpiryMonth + "/" + result.Card.ExpiryYear;
                }

           
        }
    }
}
