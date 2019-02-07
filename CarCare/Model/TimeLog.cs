using System;
using SQLite;
using SQLiteNetExtensions.Attributes;
using Syncfusion.XForms.DataForm;

namespace CarCare
{
    [Table("TimeLog")]
    public class TimeLog : BaseModel
    {
        private int timeLogId, projectID;
        private string partName, userName;
        private double hours, price;
        private Project projectDetail;
        private DateTime loggedDate = DateTime.Now;

        [PrimaryKey, AutoIncrement]
        [Display(AutoGenerateField = false)]
        public int TimeLogID
        {
            get
            {
                return timeLogId;
            }

            set
            {
                this.timeLogId = value;
                OnPropertyChanged(nameof(TimeLogID));
            }
        }

        [Display(AutoGenerateField = false)]
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                this.userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        [Display(Prompt = "Enter the part name", Name = "Part name")]
        [NotNull]
        public string PartName
        {
            get
            {
                return partName;
            }

            set
            {
                this.partName = value;
                OnPropertyChanged(nameof(PartName));
            }
        }

        [SQLite.MaxLength(8)]
        [Display(Prompt = "Enter the hours Spent", Name = "Time log")]
        public double Hours
        {
            get
            {
                return hours;
            }

            set
            {
                this.hours = value;
                OnPropertyChanged(nameof(Hours));
            }
        }

        [Display(Prompt = "Enter the Price of parts")]
        public double Price
        {
            get
            {
                return price;
            }

            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }


        [Display(Prompt = "Enter the date", Name = "Date")]
        public DateTime LoggedDate
        {
            get
            {
                return loggedDate;
            }

            set
            {
                loggedDate = value;
                OnPropertyChanged(nameof(LoggedDate));
            }
        }

        [Display(AutoGenerateField = false)]
        [ForeignKey(typeof(Project))]
        public int ProjectID
        {
            get
            {
                return projectID;
            }

            set
            {
                projectID = value;
                OnPropertyChanged(nameof(ProjectID));
            }
        }

        [Display(AutoGenerateField = false)]
        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Project ProjectDetail
        {
            get
            {
                return projectDetail;
            }

            set
            {
                projectDetail = value;
                OnPropertyChanged(nameof(ProjectDetail));
            }
        }
    }
}