using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using UtilityHelper;


namespace UtilityWpf.View
{

    public class CollectionEditor : ListBoxEx
    {


        public static readonly DependencyProperty ContentTemplateProperty = DependencyProperty.Register("ContentTemplate", typeof(DataTemplate), typeof(CollectionEditor));

        public static readonly DependencyProperty ButtonsProperty = DependencyProperty.Register("Buttons", typeof(List<ViewModel.ButtonDefinition>), typeof(CollectionEditor));

        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(object), typeof(CollectionEditor), new PropertyMetadata(null, InputChanged));


        public IEnumerable Buttons
        {
            get { return (IEnumerable)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }


        public DataTemplate ContentTemplate
        {
            get { return (DataTemplate)GetValue(ContentTemplateProperty); }
            set { SetValue(ContentTemplateProperty, value); }
        }


        public object Input
        {
            get { return (object)GetValue(InputProperty); }
            set { SetValue(InputProperty, value); }

        }

        private static void InputChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as CollectionEditor).InputSubject.OnNext((DatabaseCommand)e.NewValue);
        }


        protected ISubject<DatabaseCommand> InputSubject = new Subject<DatabaseCommand>();


        static CollectionEditor()
        {
     
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CollectionEditor), new FrameworkPropertyMetadata(typeof(CollectionEditor)));

        }



        public CollectionEditor()
        {

            Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/CollectionEditor.xaml", System.UriKind.Relative);
            ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
            Style = resourceDictionary["CollectionEditorStyle"] as Style;

            DeletedSubject.OnNext(new object());
            InputSubject.WithLatestFrom(SelectedItemSubject, (input, item) => new { input, item }).Subscribe(_ =>
                {

                    switch (_.input)
                    {
                        case (DatabaseCommand.Delete):
                            DeletedSubject.OnNext(_.item);
                            break;
                        case (DatabaseCommand.Update):
                            {

                            }
                            break;
                    }
                });


            Action<DatabaseCommand> av = (a) => this.Dispatcher.InvokeAsync(() => InputSubject.OnNext(a), System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));

            var items = ButtonDefinitionHelper.GetCommandOutput<DatabaseCommand>(typeof(DatabaseCommand))
                ?.Select(meas =>
                new ViewModel.ButtonDefinition
                {
                    Command = new RelayCommand(() => av(meas.Value())),
                    Content = meas.Key
                });

            if (items == null)
                Console.WriteLine("measurements-service equals null in collectionviewmodel");
            else
                this.Dispatcher.InvokeAsync(() => Buttons = items.ToList(), System.Windows.Threading.DispatcherPriority.Background, default(System.Threading.CancellationToken));


        }
    }
}
