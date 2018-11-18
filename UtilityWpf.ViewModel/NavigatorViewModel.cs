
using System.Windows.Input;
using DynamicData.Binding;
using DynamicData.Operators;
using UtilityWpf.ViewModel;
using Reactive.Bindings;
using System;
using System.Reactive.Linq;
using DynamicData;

namespace UtilityWpf.ViewModel
{


    public class NavigatorVM
    {
        public ReactiveCommand NextCommand { get; } = new ReactiveCommand();

        public ReactiveCommand PreviousCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<int> Size { get; protected set; }

    }

    

    public class PageNavigatorViewModel:NavigatorVM, IOutputService<DynamicData.PageRequest>
    {
          public IObservable<DynamicData.PageRequest> Output { get;  set; }

        public PageNavigatorViewModel(IObservable<int> currentPage, IObservable<int> pageSize)
        {
            Size = new ReactiveProperty<int>(pageSize);

            var obs = currentPage.DistinctUntilChanged().CombineLatest(Size, (a, b) =>
            new { page = a, size = b });

           Output = NextCommand.WithLatestFrom(obs, (a, b) => new PageRequest(b.page + 1, b.size)).Merge
  (PreviousCommand.WithLatestFrom(obs, (a, b) =>
  new PageRequest(b.page - 1, b.size))).StartWith(new PageRequest(1, 25)).ToReactiveProperty();


        }




    }
}