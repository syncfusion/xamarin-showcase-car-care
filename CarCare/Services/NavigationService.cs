using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCare
{
    public class NavigationService : INavigationService
    {
        protected readonly Dictionary<Type, Type> MappingPageAndViewModel;

        protected Application CurrentApplication
        {
            get { return Application.Current; }
        }

        public async Task NavigateToBackPage(object parameter = null)
        {
            if (parameter != null)
            {
                var navigationStack = CurrentApplication.MainPage?.Navigation.NavigationStack;
                var page = navigationStack[navigationStack.Count - 2];

                if (parameter is TimeLog timeLog)
                {
                    if (page is ProjectDetailsPage)
                    {
                        (page.BindingContext as ProjectDetailsPageViewModel).TimeLogs.Add(timeLog);
                    }
                }
                else if (parameter is Project project)
                {
                    if (page is ProjectPage)
                    {
                        (page.BindingContext as ProjectPageViewModel).Projects.Add(project);
                    }
                }
            }
            await CurrentApplication.MainPage?.Navigation.PopAsync();
        }

        public void InsertPageBefore(Type pageToBeInserted, object parentPage)
        {
            Page page = CreateAndBindPage(pageToBeInserted, null, null);
            if (page != null)
            {
                CurrentApplication.MainPage?.Navigation.InsertPageBefore(page, parentPage as ContentPage);
            }
        }

        public void RemovePage(Type pageType)
        {
            Page page = CreateAndBindPage(pageType, null, null);
            CurrentApplication.MainPage?.Navigation.RemovePage(page);
        }

        public NavigationService()
        {
            MappingPageAndViewModel = new Dictionary<Type, Type>();

            SetPageViewModelMappings();
        }

        public Task NavigateToAsync<TViewModel>(object parameter = null, object item = null) where TViewModel : ViewModelBase
        {
            return NavigateToAsync(typeof(TViewModel), parameter, item);
        }

        protected async Task NavigateToAsync(Type viewModelType, object parameter, object item)
        {
            Page page = CreateAndBindPage(viewModelType, parameter, item);
            if (page is LoginPage)
            {
                CurrentApplication.MainPage = new NavigationPage(page)
                {
                    BarBackgroundColor = (Color)CurrentApplication.Resources["GreenColor"],
                    BarTextColor = (Color)CurrentApplication.Resources["WhiteGreenColor"],
                };
            }
            else if (page is ProjectPage projectPage)
            {
                CurrentApplication.MainPage = new NavigationPage(projectPage)
                {
                    BarBackgroundColor = (Color)CurrentApplication.Resources["GreenColor"],
                    BarTextColor = (Color)CurrentApplication.Resources["WhiteGreenColor"],
                };
                var viewModel = projectPage.BindingContext as ProjectPageViewModel;
                viewModel.IsBusy = true;
                projectPage.Projects.IsVisible = false;
                await viewModel.AddCarDetails();
                var projectsInDb = await App.DataBaseService.FetchFromTable("ProjectDetail");
                viewModel.Projects = new ObservableCollection<Project>(projectsInDb as List<Project>);
                viewModel.IsBusy = false;
                projectPage.Projects.IsVisible = true;
            }
            else if (page is NewBookingPage || page is AddTimeLogPage || page is ProjectDetailsPage)
            {
                var mainPage = CurrentApplication.MainPage as NavigationPage;
                await mainPage.PushAsync(page);
            }
        }

        protected Page CreateAndBindPage(Type viewModelType, object parameter, object item)
        {
            Type pageType = GetPageForViewModel(viewModelType);

            if (pageType == null)
            {
                Console.WriteLine($"Mapping type for {viewModelType} is not a page");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            ViewModelBase viewModel = Activator.CreateInstance(viewModelType) as ViewModelBase;
            if (parameter != null)
            {
                if (viewModel is ProjectDetailsPageViewModel projectViewModel)
                {
                    projectViewModel.Project = parameter as Project;
                    var timeLogs = projectViewModel.Project.TimeLogs;
                    if (timeLogs != null && timeLogs.Count > 0)
                    {
                        foreach (var timeLog in timeLogs)
                        {
                            projectViewModel.TimeLogs.Add(timeLog);
                        }
                    }

                    if (item != null)
                    {
                        projectViewModel.CarDetail = item as Project;
                    }
                }
                else if (viewModel is TimeLogPageViewModel timeLogViewModel)
                {
                    timeLogViewModel.TimeLog = parameter as TimeLog;
                }
            }
            page.BindingContext = viewModel;
            return page;
        }

        protected Type GetPageForViewModel(Type viewModelType)
        {
            if (!MappingPageAndViewModel.ContainsKey(viewModelType))
            {
                throw new KeyNotFoundException($"No map for ${viewModelType} was found on navigation mappings");
            }

            return MappingPageAndViewModel[viewModelType];
        }

        private void SetPageViewModelMappings()
        {
            MappingPageAndViewModel.Add(typeof(LoginPageViewModel), typeof(LoginPage));
            MappingPageAndViewModel.Add(typeof(ProjectPageViewModel), typeof(ProjectPage));
            MappingPageAndViewModel.Add(typeof(NewBookingPageViewModel), typeof(NewBookingPage));
            MappingPageAndViewModel.Add(typeof(ProjectDetailsPageViewModel), typeof(ProjectDetailsPage));
            MappingPageAndViewModel.Add(typeof(TimeLogPageViewModel), typeof(AddTimeLogPage));
        }
    }
}
