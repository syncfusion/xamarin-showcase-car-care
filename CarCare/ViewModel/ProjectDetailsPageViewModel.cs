using Syncfusion.SfDataGrid.XForms;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarCare
{
    public class ProjectDetailsPageViewModel : ViewModelBase
    {
        #region properties
        private Project project, carDetail;
        private string deliveredDate = "---";
        private ObservableCollection<TimeLog> timeLogs;

        public Project CarDetail
        {
            get
            {
                return carDetail;
            }

            set
            {
                carDetail = value;
                OnPropertyChanged(nameof(CarDetail));
            }
        }
        public Project Project
        {
            get
            {
                return project;
            }

            set
            {
                project = value;
                OnPropertyChanged(nameof(Project));
            }
        }

        public ObservableCollection<TimeLog> TimeLogs
        {
            get
            {
                return timeLogs;
            }

            set
            {
                timeLogs = value;
                OnPropertyChanged(nameof(TimeLogs));
            }
        }

        public string ProjectDeliveredDate
        {
            get
            {
                return deliveredDate;
            }

            set
            {
                deliveredDate = value;
                OnPropertyChanged(nameof(ProjectDeliveredDate));
            }
        }

        public ObservableCollection<string> ProjectStatusList { get; set; }

        #endregion

        #region Command
        public ICommand AddPartsCommand { get; set; }
        public ICommand GridLoadedCommand { get; set; }
        public ICommand ComboboxSelectionCommand { get; set; }
        #endregion

        #region constructor
        public ProjectDetailsPageViewModel()
        {
            TimeLogs = new ObservableCollection<TimeLog>();
            AddPartsCommand = new Command<SfDataGrid>(AddPartDetails);
            GridLoadedCommand = new Command(GridLoaded);
            ComboboxSelectionCommand = new Command(SelectionChanged);
            ProjectStatusList = new ObservableCollection<string>
            {
                "In progress",
                "Hold",
                "Ready for delivery",
                "Delivered",
                "Not started"
            };
        }

        #endregion

        #region Methods
        /// <summary>
        /// Method called when Add TimeLog button in navigation bar clicked
        /// </summary>
        private async void AddPartDetails(SfDataGrid dataGrid)
        {
            var timeLog = new TimeLog
            {
                ProjectID = Project.ProjectID,
                ProjectDetail = Project
            };
            await App.NavigationService.NavigateToAsync<TimeLogPageViewModel>(timeLog);
        }

        /// <summary>
        /// Method called when grid intially loaded
        /// </summary>
        private void GridLoaded(object attachedObject)
        {
            if ((attachedObject as SfDataGrid) != null)
            {
                var dataGrid = attachedObject as SfDataGrid;
                dataGrid.CellRenderers.Remove("TableSummary");
                dataGrid.CellRenderers.Add("TableSummary", new CustomTableSummary());
            }
        }

        /// <summary>
        /// Method called when project status changed
        /// </summary>
        private async void SelectionChanged(object eventArguments)
        {
            string serviceStatus = (eventArguments as SelectionChangedEventArgs).Value.ToString();
            Project.ServiceStatus = serviceStatus;
            if (serviceStatus.ToLower() == "delivered")
            {
                ProjectDeliveredDate = DateTime.Now.ToString("dd/MM/yyyy");
                Project.DeliveredDate = DateTime.Now;
            }
            else
            {
                ProjectDeliveredDate = "---";
            }

            try
            {
                await App.DataBaseService.SaveToDatabase(Project);
                CarDetail.ServiceStatus = serviceStatus;
            }
            catch (Exception exception)
            {
                await DependencyService.Get<IToastService>().ShowAlert(exception.Message);
            }
        }
        #endregion
    }
}