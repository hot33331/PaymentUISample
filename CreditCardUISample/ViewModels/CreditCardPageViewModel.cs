using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CreditCardUISample.Services;
using Xamarin.Forms;

namespace CreditCardUISample.ViewModels
{
    public class CreditCardPageViewModel: INotifyPropertyChanged
    {
        public string CardNumber { get; set; } 
        public string CardCvv { get; set; } 
        public string CardExpirationDate { get; set; }
        public string CardHolderName { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;

       
       
    }
}
