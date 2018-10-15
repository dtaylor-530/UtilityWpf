﻿using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace UtilityWpf.ViewModel
{

    // suitable for use with ComboBox/listbox
    public class CollectionViewModel<T> :OutputViewModel<T>
    {

        public ObservableCollection<T> Items { get; set; } = new ObservableCollection<T>();

        private IDisposable disposable;

        public CollectionViewModel(IObservable<T> measurements, IScheduler ui)
        {
           // measurements.Subscribe(_ =>
           //Console.WriteLine(_.Key));
            Output = new ReactiveProperty<T>();
            if (measurements != null)
             disposable=   measurements.ObserveOn(ui).Subscribe(
                          meas => { Items.Add(meas); },
                  ex => Console.WriteLine("error in observable")
               , () => Console.WriteLine("Observer has unsubscribed from timed observable"));
            else
                Console.WriteLine("measurements-service equals null in collectionviewmodel");


        }


        public void Dispose()
        {
            disposable?.Dispose();

        }




        public CollectionViewModel(IEnumerable<T> measurements, Dispatcher dispatcher)
        {
            Output = new ReactiveProperty<T>();
            if (measurements != null)
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    foreach (var meas in measurements)
                        Items.Add(meas);
                });
            else
                Console.WriteLine("measurements-service equals null in collectionviewmodel");

        }


 
    }
}
