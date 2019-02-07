using System.ComponentModel;

namespace CarCare
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region events
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        #endregion
    }
}