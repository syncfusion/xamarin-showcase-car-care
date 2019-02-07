using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using SQLite;
using Syncfusion.XForms.DataForm;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CarCare
{
    public class TimeLogPageViewModel : ViewModelBase
    {
        #region Properties
        private TimeLog timeLogs;

        public TimeLog TimeLog
        {
            get
            {
                return timeLogs;
            }

            set
            {
                timeLogs = value;
                OnPropertyChanged(nameof(TimeLog));
            }
        }
        #endregion

        #region Command
        public ICommand AddTimeLogCommand { get; set; }

        public ICommand DataFormItemGeneratedCommand { get; set; }
        #endregion

        #region Methods
        public TimeLogPageViewModel()
        {
            AddTimeLogCommand = new Command(AddTimeLog);
            DataFormItemGeneratedCommand = new Command(AutoGeneratingDataFormItem);
        }

        /// <summary>
        /// Method called when project when user added timelog
        /// </summary>
        private async void AddTimeLog()
        {
            var project = TimeLog.ProjectDetail;
            project.TimeLogs.Add(TimeLog);
            TimeLog.UserName = Preferences.Get("userName", string.Empty);
            try
            {
                await App.DataBaseService.SaveToDatabase(project);
                await App.NavigationService.NavigateToBackPage(TimeLog);
                await DependencyService.Get<IToastService>().ShowAlert("TimeLog added Successfully");
            }
            catch (NotNullConstraintViolationException exception)
            {
                await DependencyService.Get<IToastService>().ShowAlert(exception.Message.Substring(36) + " should not be empty");
            }
            catch (Exception exception)
            {
                await DependencyService.Get<IToastService>().ShowAlert(exception.Message);
            }
        }

        /// <summary>
        /// Changing enum property values for dataform
        /// </summary>
        private void AutoGeneratingDataFormItem(object attachedObject)
        {
            var eventArgs = attachedObject as AutoGeneratingDataFormItemEventArgs;
            var dataFormItem = eventArgs.DataFormItem;
            if (dataFormItem != null)
            {
                if (dataFormItem.Name == "Status")
                {
                    var list = new List<string>
                    {
                        "In progress",
                        "Hold",
                        "Not started",
                        "Ready for delivery",
                        "Delivered"
                    };
                    (dataFormItem as DataFormPickerItem).ItemsSource = list;
                    (dataFormItem as DataFormPickerItem).Title = "Service status";
                    (dataFormItem as DataFormPickerItem).PlaceHolderText = "Select service status";
                }
            }
        }
        #endregion
    }
}