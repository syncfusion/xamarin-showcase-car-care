using Xamarin.Forms;

namespace CarCare
{
    public partial class AddTimeLogPage : ContentPage
    {
        public AddTimeLogPage()
        {
            InitializeComponent();
            //Registering picker control for Enum property
            DataForm.RegisterEditor("Status", "Picker");
        }
    }
}