using Syncfusion.SfDataGrid.XForms;
using Syncfusion.XForms.DataForm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarCare
{
    public class DataGridBehavior : Behavior<SfDataGrid>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(DataGridBehavior), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(DataGridBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public SfDataGrid AssociatedObject { get; private set; }

        protected override void OnAttachedTo(SfDataGrid bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.GridLoaded += OnGridLoaded;
        }

        protected override void OnDetachingFrom(SfDataGrid bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.GridLoaded -= OnGridLoaded;
            AssociatedObject = null;
        }

        private void OnGridLoaded(object sender, GridLoadedEventArgs e)
        {
            if (Command == null)
            {
                return;
            }

            var parameter = sender;
            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}