using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using SQLiteNetExtensionsAsync.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCare
{
    public class ProjectPageViewModel : ViewModelBase
    {
        #region Properties
        private bool isBusy;
        private ObservableCollection<Project> projects;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public ObservableCollection<Project> Projects
        {
            get
            {
                return projects;
            }

            set
            {
                projects = value;
                OnPropertyChanged(nameof(Projects));
            }
        }
        #endregion

        #region Command
        public ICommand LogoutCommand { get; set; }

        public ICommand AddProjectCommand { get; set; }

        public ICommand ProjectItemTappedCommand { get; set; }
        #endregion

        #region Constructor
        public ProjectPageViewModel()
        {
            AddProjectCommand = new Command(AddProjectToDB);
            ProjectItemTappedCommand = new Command(ViewProjectDetail);
            LogoutCommand = new Command(Logout);
        }
        #endregion

        #region methods
        /// <summary>
        /// Adding dummy project datas
        /// </summary>
        public async Task AddCarDetails()
        {
            var projectsInDB = await App.DataBaseService.FetchFromTable("ProjectDetail");
            if (projectsInDB is List<Project> projectList && projectList.Count <= 0)
            {
                try
                {
                    DateTime today = DateTime.Now;
                    await App.DataBaseService.SaveToDatabase(new Project
                    {
                        CustomerName = "John",
                        CarMake = CarMake.Chevrolet,
                        CarModel = "",
                        BookedDate = today,
                        TypeOfService = "General service",
                        CarPhotos = new List<Photo>
                            {
                                new Photo { CarPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car1.jpg" : "Car1Tablet.jpg" },
                                new Photo { CarPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car2.jpg" : "Car2Tablet.jpg" }
                        }
                    });
                    await App.DataBaseService.SaveToDatabase(
                        new Project
                        {
                            CustomerName = "Peter",
                            CarMake = CarMake.Chevrolet,
                            ServiceStatus = "In progress",
                            TypeOfService = "Oil Service",
                            BookedDate = today.AddDays(-1),
                            CarPhotos = new List<Photo>
                            {
                                new Photo { CarPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car3.jpg" : "Car3Tablet.jpg" },
                                new Photo { CarPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car4.jpg" : "Car4Tablet.jpg" },
                            },
                            TimeLogs = new List<TimeLog>
                            {
                                new TimeLog { UserName = "Michael", LoggedDate = today, PartName = "Bells", Hours = 1, Price = 10 },
                                new TimeLog { UserName = "Robert", LoggedDate = today, PartName = "Coil Springs", Hours = 1, Price = 10 }
                            }
                        });
                    await App.DataBaseService.SaveToDatabase(
                        new Project
                        {
                            CustomerName = "Fern",
                            CarMake = CarMake.Chevrolet,
                            ServiceStatus = "Hold",
                            TypeOfService = "Water Service",
                            BookedDate = DateTime.Now.AddDays(-1),
                            CarPhotos = new List<Photo>
                            {
                                new Photo { CarPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car1.jpg" : "Car1Tablet.jpg" },
                                new Photo { CarPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car3.jpg" : "Car3Tablet.jpg" },
                            },
                            TimeLogs = new List<TimeLog>
                            {
                                new TimeLog { UserName = "William", LoggedDate = today.AddDays(-1), PartName = "Oil Filter", Hours = 1, Price = 10 },
                                new TimeLog { UserName = "James", LoggedDate = today, PartName = "Engine Oil Tank", Hours = 1, Price = 10 }
                            }
                        });
                    await App.DataBaseService.SaveToDatabase(
                        new Project
                        {
                            CustomerName = "Marry",
                            CarMake = CarMake.Chevrolet,
                            ServiceStatus = "Ready for delivery",
                            TypeOfService = "General Service",
                            BookedDate = DateTime.Now.AddDays(-2),
                            CarPhotos = new List<Photo>
                            {
                                new Photo { CarPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car3.jpg" : "Car3Tablet.jpg" },
                                new Photo { CarPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car1.jpg" : "Car1Tablet.jpg" },
                            },
                            TimeLogs = new List<TimeLog>
                            {
                                new TimeLog { UserName = "George", LoggedDate = today.AddDays(-2), PartName = "Oil Filter", Hours = 1, Price = 10 },
                                new TimeLog { UserName = "Henry", LoggedDate = today.AddDays(-1), PartName = "Engine Oil Tank", Hours = 1, Price = 10 },
                                new TimeLog { UserName = "Thomas", LoggedDate = today.AddDays(-1), PartName = "Bells", Hours = 1, Price = 10 },
                            }
                        });
                    return;
                }
                catch (Exception exception)
                {
                    await DependencyService.Get<IToastService>().ShowAlert(exception.Message);
                    return;
                }
            }
            return;
        }

        /// <summary>
        /// Method called when Add Project button is clicked
        /// </summary>
        private async void AddProjectToDB(object car)
        {
            var project = await App.DataBaseService.FetchFromTable("ProjectDetail");
            if (project != null)
            {
                await App.NavigationService.NavigateToAsync<NewBookingPageViewModel>();
            }
            else
            {
                await DependencyService.Get<IToastService>().ShowAlert("Wrong info");
            }

        }

        /// <summary>
        /// Method called when Logout button clicked
        /// </summary>
        private async void Logout(object page)
        {
            if (page is ProjectPage projectPage)
            {
                Preferences.Clear();
                App.NavigationService.InsertPageBefore(typeof(LoginPageViewModel), projectPage);
                await Application.Current.MainPage.Navigation.PopAsync(true);
            }
        }

        /// <summary>
        /// Method called when user tapped on any project in project page
        /// </summary>
        private async void ViewProjectDetail(object car)
        {
            if ((car as Syncfusion.ListView.XForms.ItemTappedEventArgs).ItemData is Project projectDetail)
            {
                try
                {
                    var project = await App.DatabaseConnection.GetWithChildrenAsync<Project>(projectDetail.ProjectID, true);
                    await App.NavigationService.NavigateToAsync<ProjectDetailsPageViewModel>(project, projectDetail);
                }
                catch (Exception exception)
                {
                    await DependencyService.Get<IToastService>().ShowAlert(exception.Message);
                }
            }
        }
        #endregion
    }
}