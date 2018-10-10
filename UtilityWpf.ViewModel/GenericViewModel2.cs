using DynamicData;
using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;



namespace UtitlityWpf.ViewModel
{


    public class GenericViewModel<T> : AbstractNotifyPropertyChanged
    {
        private T _selectedItem;


        public T SelectedItem
        {
            get => _selectedItem;
            set => SetAndRaise(ref _selectedItem, value);
        }


        public ObservableCollection<T> Items { get; }


        public GenericViewModel(IObservable<T> observable,
            IObservable<Func<T, bool>> filter,
            IObservable<T> selection,
            IScheduler scheduler)
        {

            observable
                 .ObserveOn(scheduler)
                 .Subscribe(_ => Items.Add(_), ex => Console.WriteLine("Error in generic view model"));//.Dispose();



            selection = this.WhenValueChanged(t => t.SelectedItem)
                .Throttle(TimeSpan.FromMilliseconds(250));
            //  .Select(_ => BuildFilter(_));
        }






    }


}
