using InOut.Mobile.Models;
using InOut.Mobile.ViewModels;
using Xamarin.Forms;

namespace InOut.Mobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}