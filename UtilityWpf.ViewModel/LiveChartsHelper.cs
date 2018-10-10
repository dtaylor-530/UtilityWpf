
using LiveCharts;
using LiveCharts.Defaults;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using LiveCharts.Configurations;
using System.Reactive.Concurrency;
using System.Reactive.Linq;


namespace UtilityWpf.ViewModel
{





    public static class LiveChartsHelper
    {



        public static void AddSeries(this SeriesCollection seriesCollection, string name, List<Tuple<DateTime, double>> line = null)
        {

            if (line != null)
                seriesCollection.Add(line.ToLineSeries(name));

            else
                seriesCollection.Add(new LiveCharts.Wpf.LineSeries
                {
                    Title = name,
                    Values = new ChartValues<DateModel>()

                }); //MakeSeries(name,line));

        }


        public static List<Tuple<DateTime, double>> ToTupleList(this LiveCharts.Wpf.LineSeries series)
        {
            return series.Values.Cast<DateModel>().Select(_ => Tuple.Create(_.DateTime, _.Value)).ToList();

        }

        public static List<Tuple<DateTime, double?>> ToNullableTupleList(this LiveCharts.Wpf.LineSeries series)
        {
            return series.Values.Cast<DateModel>().Select(_ => new Tuple<DateTime, double?>(_.DateTime, _.Value)).ToList();

        }

        public static LiveCharts.Wpf.LineSeries ToLineSeries(this List<Tuple<DateTime, double>> lst, string title = null)
        {

            return new LiveCharts.Wpf.LineSeries
            {
                Title = title,

                Values = new ChartValues<DateModel>(

                          lst.Select(_ =>
                          new DateModel
                          {
                              DateTime = _.Item1,
                              Value = _.Item2
                          })
                      )

            };


        }




        public static LiveCharts.Definitions.Series.ISeriesView GetLineOrNew(this SeriesCollection seriesCollection, string title)
        {
            LiveCharts.Definitions.Series.ISeriesView result;

            while (true)
            {
                result = seriesCollection.SingleOrDefault(_ => _.Title == title);
                if (result == null)
                    seriesCollection.AddSeries(title);
                else
                    break;
            }

            return result;
        }




        public static void TransformAll(this SeriesCollection seriesCollection, Func<LiveCharts.Wpf.LineSeries, LiveCharts.Wpf.LineSeries> transform)
        {

            foreach (LiveCharts.Wpf.LineSeries series in seriesCollection)
            {
                seriesCollection.Add(transform(series));
            }


        }




    }
}