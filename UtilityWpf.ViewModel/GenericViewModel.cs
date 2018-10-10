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
using UtilityHelper;

namespace UtilityWpf.ViewModel
{


    public class GenericViewModel<T, R> : AbstractNotifyPropertyChanged, ISelectableItems<T>
    {
        //private T _selectedItem;


        //public T SelectedItem
        //{
        //    get => _selectedItem;
        //    set => SetAndRaise(ref _selectedItem, value);
        //}


        private SelectableObject<T> _selectedItem;


        public SelectableObject<T> SelectedItem
        {
            get => _selectedItem;
            set => SetAndRaise(ref _selectedItem, value);
        }


        public string Title { get; set; }


        private readonly ReadOnlyObservableCollection<SelectableObject<T>> _items;


        public ReadOnlyObservableCollection<SelectableObject<T>> Items => _items;


        public GenericViewModel(IObservableCache<T, R> observable,
            IObservable<Func<T, bool>> filter,
            IScheduler scheduler, string title = null)
        {

            observable.Connect()
                //.Subscribe(_=>
                //Console.WriteLine("sdffsdffffffff!"));
                //.Filter(filter)

                .Transform(s =>
                {
                    var so = new SelectableObject<T>(s);
                    so.WhenValueChanged(t => t.IsSelected)
                        .Throttle(TimeSpan.FromMilliseconds(250))
                       .Subscribe(b =>
                       {
                           this.SelectedItem = this.Items.FirstOrDefault(sof => sof.IsSelected == true);
                       });
                    return so;
                })

                  .ObserveOn(scheduler)
                  .Bind(out _items)
              .DisposeMany()
                .Subscribe(
                _ => Console.WriteLine("generic view model changed"),
                ex => Console.WriteLine("Error in generic view model"));





            Title = title;

        }


    }

    public interface ISelectableItems<T>
    {
        SelectableObject<T> SelectedItem { get; set; }
        //ICollection<SelectableObject<T>> Items { get; set; }
    }


    public static class Helper
    {


        var x = new Filter.Model.Observable<PredictionsViewModel>();



        public static IObservable<T> GetSelectedObjectSteam<T, R>(this GenericViewModel<T, R> si)
        {
            //si.WhenValueChanged(t => t.SelectedItem).Subscribe(_ =>
            //Console.WriteLine());

            //si.WhenValueChanged(t => t.SelectedItem)
            //    .Throttle(TimeSpan.FromMilliseconds(250))
            //    .Where(_ => _ != null)
            //     .Select(_ => _.Object).Subscribe(_ =>
            //Console.WriteLine());


            return si.WhenValueChanged(t => t.SelectedItem)
                .Throttle(TimeSpan.FromMilliseconds(250))
                .Where(_ => _ != null)
                 .Select(_ => _.Object);



        }


        //public static SelectableObject<T> AddSelectable<T>(this ISelectableItems<T> si, T s)
        //{
        //    var so = new SelectableObject<T>(s);
        //    so.WhenValueChanged(t => t.IsSelected)
        //        .Throttle(TimeSpan.FromMilliseconds(250))
        //       .Subscribe(b =>
        //       {
        //           si.SelectedItem = si.Items.FirstOrDefault(sof => sof.IsSelected == true);
        //       });
        //    return so;

        //}
    }

}




