using Syncfusion.ListView.XForms;
using Xamarin.Forms;

namespace CarCare
{
    public partial class ProjectPage : ContentPage
    {
        public SfListView Projects => ProjectList;

        public ProjectPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //Removing loginpage once user logged in
            var navigationStack = Navigation.NavigationStack;
            if (navigationStack[0] is LoginPage)
            {
                App.NavigationService.RemovePage(typeof(LoginPage));
            }
        }
    }
}