using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Syncfusion.XForms.DataForm;

namespace CarCare
{
    [Table("Project")]
    public class Project : INotifyPropertyChanged
    {
        private int projectID;
        private string customerName, carModel, typeOfService = "General service", serviceStatus = "Not started";
        private DateTime bookedDate = DateTime.Now, dueDate, deliveredDate;
        private CarMake carMake;
        private List<Photo> carPhotos;
        private List<TimeLog> timeLogs;

        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        [Display(AutoGenerateField = false)]
        public int ProjectID
        {
            get
            {
                return projectID;
            }

            set
            {
                this.projectID = value;
                OnPropertyChanged(nameof(ProjectID));
            }
        }

        [ForeignKey(typeof(User))]
        [Display(Name = "Customer name", Prompt = "Enter customer name", Order = 1)]
        [NotNull]
        public string CustomerName
        {
            get
            {
                return customerName;
            }

            set
            {
                this.customerName = value;
                OnPropertyChanged(nameof(CustomerName));
            }
        }

        [Display(Name = "Car make", Prompt = "Select car manufacturer", Order = 2)]
        public CarMake CarMake
        {
            get
            {
                return carMake;
            }

            set
            {
                carMake = value;
                OnPropertyChanged(nameof(CarMake));
            }
        }

        [Display(Name = "Car model", Prompt = "Enter car model", Order = 3)]
        [Ignore]
        public string CarModel
        {
            get
            {
                return carModel;
            }

            set
            {
                this.carModel = value;
                OnPropertyChanged(nameof(CarModel));
            }
        }

        [Display(Name = "Type of service", Prompt = "Select type of service", Order = 4)]
        public string TypeOfService
        {
            get
            {
                return typeOfService;
            }

            set
            {
                this.typeOfService = value;
                SetDueDate();
                OnPropertyChanged(nameof(TypeOfService));
            }
        }

        [Display(AutoGenerateField = false)]
        public string ServiceStatus
        {
            get
            {
                return serviceStatus;
            }

            set
            {
                serviceStatus = value;
                OnPropertyChanged(nameof(ServiceStatus));
            }
        }

        [Display(AutoGenerateField = false)]
        public DateTime BookedDate
        {
            get
            {
                return bookedDate;
            }

            set
            {
                bookedDate = value;
                SetDueDate();
                OnPropertyChanged(nameof(BookedDate));
            }
        }

        [Display(AutoGenerateField = false)]
        public DateTime DueDate
        {
            get
            {
                return dueDate;
            }

            set
            {
                dueDate = value;
                OnPropertyChanged(nameof(DueDate));
            }
        }

        [Display(AutoGenerateField = false)]
        public DateTime DeliveredDate
        {
            get
            {
                return deliveredDate;
            }

            set
            {
                deliveredDate = value;
                OnPropertyChanged(nameof(DeliveredDate));
            }
        }

        [Display(AutoGenerateField = false)]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Photo> CarPhotos
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

        [Display(AutoGenerateField = false)]
        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<TimeLog> TimeLogs
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

        private void SetDueDate()
        {
            switch (TypeOfService.ToLower())
            {
                case "oil service":
                    DueDate = BookedDate.AddDays(2);
                    break;
                case "water service":
                    DueDate = BookedDate.AddDays(1);
                    break;
                default:
                    DueDate = BookedDate.AddDays(3);
                    break;
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}