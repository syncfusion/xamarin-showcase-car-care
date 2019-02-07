using Xamarin.Forms;

namespace CarCare
{
    public partial class ProjectDetailsPage : ContentPage
    {
        public ProjectDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            TimeLogColumn.DisplayBinding = new Binding("Hours", converter: new DoubletoTimeConverter());
            //Hiding Datagrid if there are no timelogs to show
            var timeLogs = (BindingContext as ProjectDetailsPageViewModel).TimeLogs;
            DataGrid.IsVisible = timeLogs?.Count > 0;
            TimeLogLabel.IsVisible = timeLogs?.Count <= 0;
        }
    }
}