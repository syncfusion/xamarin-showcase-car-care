using System.Threading.Tasks;
using Android.App;
using Android.Widget;

namespace CarCare.Droid
{
    public class ToastAndroid : IToastService
    {
        string alertMessage;
        public Task ShowAlert(string message)
        {
            alertMessage = message;
            var activity = MainActivity.Instance;
            activity.RunOnUiThread(() =>
            {
                Toast.MakeText(Application.Context, alertMessage, ToastLength.Short).Show();

            });
            return Task.Run(() => { });
        }
    }
}