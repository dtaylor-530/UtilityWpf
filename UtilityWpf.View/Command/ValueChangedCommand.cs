using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UtilityWpf.ViewModel;

namespace UtilityWpf.View
{
    public class ValueChangedCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        protected event Action<KeyValuePair<string, double>> Event;
        
        public void Execute(object parameter)
        {
            var kvp = (parameter as View.SliderItemsControl.KeyValuePairRoutedEventArgs).KeyValuePair;

            Event.Invoke(kvp);
        }



    }
}
