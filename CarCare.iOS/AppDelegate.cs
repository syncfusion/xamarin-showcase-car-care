using System;
using System.Collections.Generic;
using System.Linq;
using CarCare.iOS;
using Foundation;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfDataGrid.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.DataForm;
using Syncfusion.XForms.iOS.TabView;
using Syncfusion.XForms.iOS.TextInputLayout;
using UIKit;
using Xamarin.Forms;
using Syncfusion.XForms.iOS.Buttons;

[assembly: Dependency(typeof(ToastiOS))]

namespace CarCare.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            SfTextInputLayoutRenderer.Init();
            SfTabViewRenderer.Init();
            SfListViewRenderer.Init();
            SfDataGridRenderer.Init();
            SfDataFormRenderer.Init();
			SfRotatorRenderer.Init();
            SfComboBoxRenderer.Init();
            SfBusyIndicatorRenderer.Init();
            SfButtonRenderer.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
