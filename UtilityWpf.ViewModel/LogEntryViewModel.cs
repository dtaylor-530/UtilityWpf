﻿using DynamicData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.ViewModel
{
    //public class LogEntryService : IDisposable //, ILogEntryService
    //{
    //    //private readonly ILogger _logger;
    //    private readonly ISourceList<LogEntry> _source = new SourceList<LogEntry>();
    //    private readonly IDisposable _disposer;
    //    private readonly object _locker = new object();

    //    public IObservableList<LogEntry> Items { get; }


    //    public LogEntryService(log4net.Repository.Hierarchy.Logger logger)
    //    {
    //        _logger = logger;
    //        Items = _source.AsObservableList();

    //        var loader = ReactiveLogAppender.LogEntryObservable
    //                        .Buffer(TimeSpan.FromMilliseconds(250))
    //                        .Synchronize(_locker)
    //                        .Subscribe(_source.AddRange);

    //        //limit size of cache to prevent too many items being created
    //        var sizeLimiter = _source.LimitSizeTo(10000).Subscribe();

    //        // alternatively could expire by time
    //        //var timeExpirer = _source.ExpireAfter(le => TimeSpan.FromSeconds(le.Level == LogLevel.Debug ? 5 : 60), TimeSpan.FromSeconds(5), TaskPoolScheduler.Default)
    //        //                            .Subscribe(removed => logger.Debug("{0} log items have been automatically removed", removed.Count()));

    //        _disposer = new CompositeDisposable(sizeLimiter, _source, loader);
    //        logger.Info("Log cache has been constructed");
    //    }




    //    public void Dispose()
    //    {
    //        _disposer.Dispose();
    //    }



    //}



    //public static class Helper
    //{
    //    public static void Remove(this log4net.Repository.Hierarchy.Logger logger, IEnumerable<LogEntry> items)
    //    {
    //        try
    //        {
    //            //lock (_locker)
    //            //{
    //            var itemsToRemove = items as LogEntry[] ?? items.ToArray();
    //            logger.Info("Removing {0} log entry items", itemsToRemove.Length);
    //            _source.RemoveMany(itemsToRemove);
    //            _logger.Info("Removed {0} log entry items", itemsToRemove.Length);
    //            //}
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.Error(ex, "Problem removing log entries: {0}", ex.Message);
    //        }
    //    }
    //}
}
