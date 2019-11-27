using DynamicData;
using Reactive.Bindings;
using System;
using System.Reactive.Linq;

namespace UtilityWpf.ViewModel
{
    public class NavigatorVM
    {
        public ReactiveCommand NextCommand { get; } = new ReactiveCommand();

        public ReactiveCommand PreviousCommand { get; } = new ReactiveCommand();

        public ReactiveProperty<int> Size { get; protected set; }
    }

    public class NavigatorViewModel : NavigatorVM, IOutputService<DynamicData.PageRequest>
    {
        public IObservable<DynamicData.PageRequest> Output { get; set; }

        public NavigatorViewModel(IObservable<int> currentPage, IObservable<int> pageSize)
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