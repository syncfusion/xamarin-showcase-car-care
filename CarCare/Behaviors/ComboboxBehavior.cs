using Syncfusion.XForms.ComboBox;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CarCare
{
    public class ComboboxBehavior : Behavior<SfComboBox>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(DataFormBehavior), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create("CommandParameter", typeof(object), typeof(DataFormBehavior), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public SfComboBox AssociatedObject { get; private set; }

        protected override void OnAttachedTo(SfComboBox bindable)
        {
            base.OnAttachedTo(bindable);
            AssociatedObject = bindable;
            bindable.BindingContextChanged += OnBindingContextChanged;
            bindable.SelectionChanged += Bindable_SelectionChanged;
        }

        protected override void OnDetachingFrom(SfComboBox bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= OnBindingContextChanged;
            bindable.SelectionChanged -= Bindable_SelectionChanged;
            AssociatedObject = null;
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

        private void Bindable_SelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (Command == null)
            {
                return;
            }

            var parameter = selectionChangedEventArgs;
            if (Command.CanExecute(parameter))
            {
                Command.Execute(parameter);
            }
        }
    }
}