
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Configurations;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reactive.Concurrency;
using System.Reactive.Linq;


namespace UtilityWpf.ViewModel
{
    using static LiveChartsHelper;


    public class TimeChartViewModel// : INPCBase
    {
        private Func<double, string> formatter;

        public SeriesCollection SeriesCollection { get; private set; }

        public Func<double, string> Formatter => formatter;
        public double EventDate { get; set; }

        private IDisposable disposable;

        public TimeChartViewModel(IObservable<KeyValuePair<DateTime, double>> observable, string name, IScheduler scheduler)

        {
            Initialise();

            disposable=observable
                 .ObserveOn(scheduler)
                 .Subscribe(_ =>
                 {
                     SeriesCollection.GetLineOrNew(name)
                     .Values.Add(new DateModel
                     {
                         DateTime = _.Key,
                         Value = _.Value,
                     });
                 }
                , ex =>
                Console.WriteLine("Error in graph view model"));//.Dispose();


        }

        public TimeChartViewModel(IEnumerable<KeyValuePair<DateTime, double>> series, string name, System.Windows.Threading.Dispatcher dispatcher)
        {
            Initialise();

            dispatcher.Invoke(() =>
            {
                var line = SeriesCollection.GetLineOrNew(name);
                foreach (var _ in series)
                    line.Values.Add(new DateModel
                    {
                        DateTime = _.Key,
                        Value = _.Value,
                    });
            });

        }


        public TimeChartViewModel(IObservable<KeyValuePair<string, KeyValuePair<DateTime, double>>> observable, IScheduler scheduler)
        {
            Initialise();
            observable.Subscribe(ff =>
                        Console.WriteLine("price subscription " + ff.Value));


            disposable= observable
                .ObserveOn(scheduler)
                .Subscribe(_ =>
                {
                    SeriesCollection.GetLineOrNew(_.Key)
                    .Values.Add(
                        new DateModel
                        {
                            DateTime = _.Value.Key,
                            Value = _.Value.Value,
                        });
                    //NotifyChanged(nameof(SeriesCollection));
                }
                , ex =>
               Console.WriteLine("Error in graph view model"));//.Dispose();

        }


        private void Initialise()
        {

            var dayConfig = LiveCharts.Configurations.Mappers.Xy<DateModel>()
            .X(dayModel =>
            (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
            .Y(dayModel => dayModel.Value);

            if (dayConfig != null)
                formatter = value =>
                {
                    if (value < 0) return null; else return new System.DateTime((long)((value) * TimeSpan.FromHours(1).Ticks)).ToString("t");
                };

            SeriesCollection = new LiveCharts.SeriesCollection(dayConfig);

        }




        public void Dispose()
        {
           if(disposable!=null) disposable.Dispose();

        }
    }



    public class TimeChartViewModel2// : INPCBase
    {
        private Func<double, string> formatter;

        public SeriesCollection SeriesCollection { get; private set; }

        public Func<double, string> Formatter => formatter;
        public double EventDate { get; set; }

        private IDisposable disposable;

        public TimeChartViewModel2(IObservable<KeyValuePair<DateTime, double>> observable, string name, IScheduler scheduler)

        {
            Initialise();

            disposable = observable
                 .ObserveOn(scheduler)
                 .Subscribe(_ =>
                 {
                     SeriesCollection.GetLineOrNew(name)
                     .Values.Add(new DateModel
                     {
                         DateTime = _.Key,
                         Value = _.Value,
                     });
                 }
                , ex =>
                Console.WriteLine("Error in graph view model"));//.Dispose();
        }



        public TimeChartViewModel2(IEnumerable<KeyValuePair<DateTime,Tuple< double,double>>> series, string name,string name2, System.Windows.Threading.Dispatcher dispatcher)
        {
            Initialise();

            dispatcher.Invoke(() =>
            {
                var line1 = SeriesCollection.GetLineOrNew(name);
                foreach (var _ in series)
                    line1.Values.Add(new DateModel
                    {
                        DateTime = _.Key,
                        Value = _.Value.Item1,
                    });

                var line2 = SeriesCollection.GetLineOrNew(name2);
                foreach (var _ in series)
                    line2.Values.Add(new DateModel
                    {
                        DateTime = _.Key,
                        Value = _.Value.Item2,
                    });
            });

        }
        public TimeChartViewModel2(SortedList<DateTime, Tuple<double, double>> series, string name,string name2, System.Windows.Threading.Dispatcher dispatcher)
        {
            Initialise();

            dispatcher.Invoke(() =>
            {
                var line1 = SeriesCollection.GetLineOrNew(name);
                foreach (var _ in series)
                    line1.Values.Add(new DateModel
                    {
                        DateTime = _.Key,
                        Value = _.Value.Item1,
                    });

                var line2 = SeriesCollection.GetLineOrNew(name2);
                foreach (var _ in series)
                    line2.Values.Add(new DateModel
                    {
                        DateTime = _.Key,
                        Value = _.Value.Item2,
                    });
            });

        }


        //public TimeChartViewModel2(IObservable<KeyValuePair<string, KeyValuePair<DateTime, double>>> observable, IScheduler scheduler)
        //{
        //    Initialise();
        //    observable.Subscribe(ff =>
        //                Console.WriteLine("price subscription " + ff.Value));


        //    disposable = observable
        //        .ObserveOn(scheduler)
        //        .Subscribe(_ =>
        //        {
        //            SeriesCollection.GetLineOrNew(_.Key)
        //            .Values.Add(
        //                new DateModel
        //                {
        //                    DateTime = _.Value.Key,
        //                    Value = _.Value.Value,
        //                });
        //            //NotifyChanged(nameof(SeriesCollection));
        //        }
        //        , ex =>
        //       Console.WriteLine("Error in graph view model"));//.Dispose();

        //}


        private void Initialise()
        {

            var dayConfig = LiveCharts.Configurations.Mappers.Xy<DateModel>()
            .X(dayModel =>
            (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
            .Y(dayModel => dayModel.Value);

            if (dayConfig != null)
                formatter = value =>
                {
                    if (value < 0) return null; else return new System.DateTime((long)((value) * TimeSpan.FromHours(1).Ticks)).ToString("t");
                };

            SeriesCollection = new LiveCharts.SeriesCollection(dayConfig);

        }




        public void Dispose()
        {
            if (disposable != null) disposable.Dispose();

        }
    }





    public class GraphViewModel //: INotifyPropertyChanged
    {
        private Func<double, string> formatter;

        public SeriesCollection SeriesCollection { get; private set; }

        public Func<double, string> Formatter => formatter;

        public double EventDate { get; set; }


        public GraphViewModel(Dictionary<string, List<Tuple<DateTime, double>>> lines)
        {

            foreach (var kvp in lines)
                SeriesCollection.AddSeries(kvp.Key, kvp.Value.ToList());

            //EventDate = (line.Select(_ => _.Item1).Max() - line.Select(_ => _.Item1).Min()).Ticks;

            //NotifyChanged(nameof(Series));

            Initialise();

        }



        public GraphViewModel()
        {

            Initialise();



        }



        private void Initialise()
        {

            var dayConfig = LiveCharts.Configurations.Mappers.Xy<DateModel>()
            .X(dayModel =>
            (double)dayModel.DateTime.Ticks / TimeSpan.FromHours(1).Ticks)
            .Y(dayModel => dayModel.Value);

            if (dayConfig != null)
                formatter = value =>
                {
                    if (value < 0) return null; else return new System.DateTime((long)((value) * TimeSpan.FromHours(1).Ticks)).ToString("t");
                };

            SeriesCollection = new LiveCharts.SeriesCollection(dayConfig);

        }




        public void AddValue(string title, DateTime dt, double value)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                SeriesCollection.GetLineOrNew(title)
             .Values.Add(new DateModel
             {
                 DateTime = dt,
                 Value = value
             });

            });
        }


        public void AddValues(string title, IEnumerable<Tuple<DateTime, double>> values)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var l = SeriesCollection.GetLineOrNew(title);
                foreach (var val in values)
                    l.Values.Add(new DateModel
                    {
                        DateTime = val.Item1,
                        Value = val.Item2
                    });

            });
        }


        internal void RemoveLines(Func<LiveCharts.Definitions.Series.ISeriesView, bool> p)
        {
            foreach (LiveCharts.Definitions.Series.ISeriesView ls in SeriesCollection)
            {
                if (p(ls))
                    SeriesCollection.Remove(ls);

            }
        }




        #region INotifyPropertyChanged Implementation
        /// <summary>
        /// Occurs when any properties are changed on this object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;


        /// <summary>
        /// A helper method that raises the PropertyChanged event for a property.
        /// </summary>
        /// <param name="propertyNames">The names of the properties that changed.</param>
        public virtual void NotifyChanged(params string[] propertyNames)
        {
            foreach (string name in propertyNames)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }


        protected void RaisePropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string caller = "")
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));

        }



        #endregion
    }





}
