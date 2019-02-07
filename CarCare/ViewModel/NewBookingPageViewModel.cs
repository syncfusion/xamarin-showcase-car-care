using Syncfusion.XForms.DataForm;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using SQLite;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace CarCare
{
    public class NewBookingPageViewModel : ViewModelBase
    {
        #region Properties
        private ObservableCollection<Photo> carPhotos;

        public ObservableCollection<Photo> CarPhotos
        {
            get
            {
                return carPhotos;
            }

            set
            {
                carPhotos = value;
                OnPropertyChanged(nameof(CarPhotos));
            }
        }

        private Project project;

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
        #endregion

        #region Command
        public ICommand NewBookingCommand { get; set; }

        public ICommand TakePhotoCommand { get; set; }

        public ICommand DataFormItemGeneratedCommand { get; set; }
        #endregion

        #region Methods
        public NewBookingPageViewModel()
        {
            Project = new Project()
            {
                BookedDate = DateTime.Now,
                CarPhotos = new List<Photo>(),
                TimeLogs = new List<TimeLog>()
            };
            CarPhotos = new ObservableCollection<Photo>();
            NewBookingCommand = new Command(NewBookingAdded);
            TakePhotoCommand = new Command(TakePhotoAndSave);
            DataFormItemGeneratedCommand = new Command(AutoGeneratingDataFormItem);
        }

        /// <summary>
        /// Method called when user added new project
        /// </summary>
        private async void NewBookingAdded()
        {
            foreach (var item in CarPhotos)
            {
                Project.CarPhotos.Add(new Photo { CarPhotoPath = item.CarPhotoPath });
            }

            if (CarPhotos.Count <= 0)
            {
                string dummyPhotoPath = (Device.Idiom == TargetIdiom.Phone) ? "Car1.jpg" : "Car1Tablet.jpg";
                Project.CarPhotos.Add(new Photo { CarPhotoPath = dummyPhotoPath });
            }

            try
            {
                await App.DataBaseService.SaveToDatabase(Project);
                await App.NavigationService.NavigateToBackPage(Project);

                await DependencyService.Get<IToastService>().ShowAlert("New booking has been placed");
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
        /// Method called when user taken a photo and saved in db
        /// </summary>
        private async void TakePhotoAndSave()
        {
            try
            {
                var cameraStatus = PermissionStatus.Unknown;
                var storageStatus = PermissionStatus.Unknown;

                cameraStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
                storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                if (cameraStatus != PermissionStatus.Granted || storageStatus != PermissionStatus.Granted)
                {
                    cameraStatus = await Utils.CheckPermissions(Permission.Camera);
                    storageStatus = await Utils.CheckPermissions(Permission.Storage);
                }

                if (cameraStatus == PermissionStatus.Granted && storageStatus == PermissionStatus.Granted)
                {
                    var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                    {
                        Directory = "CarCare",
                        SaveToAlbum = true,
                        PhotoSize = PhotoSize.Small,
                        DefaultCamera = CameraDevice.Rear,
                        RotateImage = true,
                        CompressionQuality = 92
                    });
                    CarPhotos.Add(new Photo { CarPhotoPath = file.Path });
                }
                else
                {
                    await DependencyService.Get<IToastService>().ShowAlert("Permission denied.");
                    return;
                }
            }
            catch (Exception exception)
            {
                await DependencyService.Get<IToastService>().ShowAlert("Permission denied." + exception.Message);
            }
        }

        /// <summary>
        /// Method called to change default range for date in DataForm
        /// </summary>
        private void AutoGeneratingDataFormItem(object attachedObject)
        {
            var eventArgs = attachedObject as AutoGeneratingDataFormItemEventArgs;
            var dataFormItem = eventArgs.DataFormItem;
            if (dataFormItem != null)
            {
                if (dataFormItem.Name == "BookedDate" || dataFormItem.Name == "DueDate" || dataFormItem.Name == "DeliveredDate")
                {
                    var datePickerItem = (dataFormItem as DataFormDateItem);
                    if (datePickerItem != null)
                    {
                        datePickerItem.MinimumDate = new DateTime(2015, 1, 1);
                        datePickerItem.MaximumDate = new DateTime(2020, 1, 1);
                    }
                }
                else if (dataFormItem.Name == "TypeOfService")
                {
                    var list = new List<string>
                    {
                        "General service",
                        "Oil service",
                        "Water service"
                    };
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        (dataFormItem as DataFormDropDownItem).ItemsSource = list;
                        (dataFormItem as DataFormDropDownItem).PlaceHolderText = "Select service type";
                    }
                    else
                    {
                        (dataFormItem as DataFormPickerItem).ItemsSource = list;
                        (dataFormItem as DataFormPickerItem).Title = "Service type";
                        (dataFormItem as DataFormPickerItem).PlaceHolderText = "Select service type";
                    }
                }
            }
        }
        #endregion
    }
}