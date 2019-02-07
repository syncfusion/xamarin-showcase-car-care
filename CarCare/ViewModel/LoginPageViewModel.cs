using System;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Collections.Generic;

namespace CarCare
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region properties
        private User user;

        public User User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        #endregion

        #region Command
        public ICommand LoginCommand { get; set; }
        #endregion

        #region Constructor
        public LoginPageViewModel()
        {
            LoginCommand = new Command(LoggedIn);
            User = new User()
            {
                UserName = "demo",
                UserPassword = "demo",
                HasUserLogged = true
            };

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            App.DataBaseService.SaveToDatabase(User);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
        #endregion

        #region methods
        /// <summary>
        /// Method called when user logged in
        /// </summary>
        public async void LoggedIn()
        {
            try
            {
                var userExists = await App.DataBaseService.FetchFromTable("UserDetail");
                if (userExists != null && userExists is List<User> list)
                {
                    Preferences.Set("userName", User.UserName);
                    Preferences.Set("hasUserLogged", User.HasUserLogged);
                    await App.NavigationService.NavigateToAsync<ProjectPageViewModel>();
                }
                else
                {
                    await DependencyService.Get<IToastService>().ShowAlert("User not exists");
                }
            }
            catch (Exception exception)
            {
                await DependencyService.Get<IToastService>().ShowAlert(exception.Message);
            }
        }
        #endregion
    }
}