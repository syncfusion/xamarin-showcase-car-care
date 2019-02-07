using Xamarin.Forms;

namespace CarCare
{
    public partial class NewBookingPage : ContentPage
    {
        public NewBookingPage()
        {
            InitializeComponent();
            //Registering picker control for Enum properties
            if (Device.RuntimePlatform == Device.UWP)
            {
                DataForm.RegisterEditor("TypeOfService", "DropDown");
                DataForm.RegisterEditor("ServiceStatus", "DropDown");
            }
            else
            {
                DataForm.RegisterEditor("TypeOfService", "Picker");
                DataForm.RegisterEditor("ServiceStatus", "Picker");
            }
        }
    }
}