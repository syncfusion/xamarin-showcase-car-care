using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CarCare
{
    public interface INavigationService
    {
        Task NavigateToAsync<TViewModel>(object parameter = null, object item = null) where TViewModel : ViewModelBase;

        Task NavigateToBackPage(object parameter);

        void RemovePage(Type page);

        void InsertPageBefore(Type pageTypeToBeInserted, object parentPage);

    }
}