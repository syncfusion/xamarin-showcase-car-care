using SQLite;

namespace CarCare
{
    [Table("User")]
    public class User : BaseModel
    {
        private int userID;
        private string userPassword, userName;
        private bool isUserLogged;

        [PrimaryKey, AutoIncrement]
        public int UserID
        {
            get
            {
                return userID;
            }

            set
            {
                this.userID = value;
                OnPropertyChanged(nameof(UserID));
            }
        }

        [Unique, NotNull]
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

        [NotNull]
        public string UserPassword
        {
            get
            {
                return userPassword;
            }

            set
            {
                this.userPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }

        public bool HasUserLogged
        {
            get
            {
                return isUserLogged;
            }

            set
            {
                this.isUserLogged = value;
                OnPropertyChanged(nameof(HasUserLogged));
            }
        }
    }
}