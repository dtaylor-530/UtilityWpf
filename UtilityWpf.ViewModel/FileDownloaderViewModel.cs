using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;


namespace UtilityWpf.ViewModel
{
    public class FileDownloaderViewModel : IDisposable
    {


        public ReadOnlyReactiveProperty<int> Progress { get; }
        public ReadOnlyReactiveProperty<Tuple<string, bool>> Completed { get; }


        private WebClient client;


        public void Dispose()
        {
            client.Dispose();

        }

        public FileDownloaderViewModel(IObservable<Tuple<Uri, string>> files)
        {
            client = new WebClient();

            var ddd = Observable.FromEventPattern<AsyncCompletedEventHandler, AsyncCompletedEventArgs>(
            h => client.DownloadFileCompleted += h,
            h => client.DownloadFileCompleted -= h);

            Completed = Observable.Create<Tuple<string, bool>>(observer => ddd.WithLatestFrom(files, (a, b) => new { a, b }).Subscribe(_ =>
            Task.Run(() => FileHelper.CheckFile(_.b.Item2)).ToObservable().Subscribe(__ => observer.OnNext(Tuple.Create(_.b.Item1.ToString(), __)))
        )).ToReadOnlyReactiveProperty();


            Progress = Observable.FromEventPattern<DownloadProgressChangedEventHandler, DownloadProgressChangedEventArgs>(
     h => client.DownloadProgressChanged += h,
     h => client.DownloadProgressChanged -= h).Select(e => e.EventArgs.ProgressPercentage).Merge(ddd.Select(_ => 0))
     .ToReadOnlyReactiveProperty();

            files.Subscribe(_ =>
            {
                client.DownloadFileAsync(_.Item1, _.Item2);
            });
        }





    }


    public static class FileHelper
    {

        public static bool CheckFile(string sink)
        {

            System.IO.FileInfo info = new System.IO.FileInfo(sink);
            if (info.Length > 0)
            {
                return true;
                //Kaliko.Logger.Write(string.Format("File {0} downloaded to {1} @ {2}", source, sink, DateTime.Now), Kaliko.Logger.Severity.Info);
            }
            else
            {
                System.IO.File.Delete(sink);
                return false;
            }

        }
    }
}
