using System.Threading.Tasks;

namespace CarCare
{
    /// <summary>
    /// Service for showing alert when Booking/Timelog added
    /// </summary>
    public interface IToastService
    {
        Task ShowAlert(string message);
    }
}