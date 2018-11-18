using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using UtilityHelper;
using UtilityHelper.NonGeneric;
using UtilityWpf.View;

namespace UtilityWpf.View
{
    //move to attached

    //public class PropertyListControl : ListBoxEx
    //{


    //    public string Property
    //    {
    //        get { return (string)GetValue(PropertyProperty); }
    //        set { SetValue(PropertyProperty, value); }
    //    }


    //    public static readonly DependencyProperty PropertyProperty = DependencyProperty.Register("Property", typeof(string), typeof(PropertyListControl), new PropertyMetadata(null, PropertyChanged));



    //    private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    //    {
    //        (d as PropertyListControl).PropertyChanges.OnNext((string)e.NewValue);
    //    }
    //    ISubject<string> PropertyChanges = new Subject<string>();

    //    static PropertyListControl()
    //    {
    //        // DefaultStyleKeyProperty.OverrideMetadata(typeof(ListBoxEx), new FrameworkPropertyMetadata(typeof(ListBoxEx)));
    //    }

    //    public PropertyListControl()
    //    {
    //        //Uri resourceLocater = new Uri("/UtilityWpf.View;component/Themes/PropertyControl.xaml", System.UriKind.Relative);
    //        //ResourceDictionary resourceDictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
    //        //Style = resourceDictionary["PropertyControlStyle"] as Style;
    //        PropertyChanges.Subscribe(_ =>
    //        {
    //            if (ItemsSource.First().GetType().GetProperties().SingleOrDefault(a => a.Name == _).PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
    //                ItemsSourceSubject.OnNext(ItemsSource.GetPropValues<IEnumerable<object>>(_).SelectMany(a => a));
    //            else
    //                ItemsSourceSubject.OnNext(ItemsSource.GetPropValues<object>(_));
    //        });
    //    }

    //}
}
