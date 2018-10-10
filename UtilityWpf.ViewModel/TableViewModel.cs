
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Collections.ObjectModel;


namespace UtilityWpf.ViewModel
{
   // public class TableViewModel
   // {


   //         //private Func<double, string> formatter;

   //         public LiveCharts.SeriesCollection SeriesCollection { get; private set; }

   //         //public Func<double, string> Formatter => formatter;
   //         //public double EventDate { get; set; }

   //         public TableViewModel(IObservable<KeyValuePair<string,Tuple<DateTime, double>>> observable, string name, IScheduler scheduler)
   //         {

   //             observable
   //                  .ObserveOn(scheduler)
   //                  .Buffer(TimeSpan.FromSeconds(1))
   //                  .Subscribe(_ =>

   //                  {
   //                      foreach (var kvp in _.ToLookup(a => a.Key, a => a.Value))
   //                      {
   //                          SeriesCollection.GetValueOrNew(kvp.Key).Add(kvp.First());
   //                      }
   // .Values.Add(new DateModel
   // {
   //     DateTime = _.Item1,
   //     Value = _.Item2,
   // });


   //                  }


   //                 , ex =>
   //                 Console.WriteLine("Error in graph view model"));//.Dispose();


   //         }

   //         public TimeChartViewModel(IObservable<KeyValuePair<string, Tuple<DateTime, double>>> observable, IScheduler scheduler)
   //         {
   //             Initialise();
   //             observable
   //                 .ObserveOn(scheduler)
   //                 .Subscribe(x =>
   //                 {


   //                     SeriesCollection.GetLineOrNew(x.Key)
   //.Values.Add(new DateModel
   //{
   //    DateTime = x.Value.Item1,
   //    Value = x.Value.Item2,
   //});
   //                 }

   //                 , ex =>
   //                Console.WriteLine("Error in graph view model"));//.Dispose();

   //         }


       



   //     }








}
