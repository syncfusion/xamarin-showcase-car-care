using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace CarCare.iOS
{
    public class ToastiOS : CarCare.IToastService
    {
        public Task ShowAlert(string message)
        {
            UIAlertController alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            NSTimer alertDelay = NSTimer.CreateScheduledTimer(2, (obj) =>
            {
                if (alert != null)
                {
                    alert.DismissViewController(true, null);
                }
            });
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
            return Task.Run(() => "");
        }
    }
}