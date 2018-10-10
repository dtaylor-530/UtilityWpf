using DynamicData;
using DynamicData.Binding;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilityWpf.Common;

namespace UtilityWpf.ViewModel
{

    public class ServiceViewModel<T>
    {

        public ObservableCollection<T> Items { get; }// => _items;
        //private readonly ReadOnlyObservableCollection<T> _items;

        public ServiceViewModel(IService<T> service)
        {
            //service
            //     .Resource
    
            //     .ToObservableChangeSet()
            //     .Bind(out _items)
            //     .DisposeMany();

            service.Resource.Subscribe(_ =>
            {
                Items.Add( _ );
            });
        }




    }

    public class ServiceViewModel<T,R> where R : IComparable
    {

        public ReadOnlyObservableCollection<T> Items => _items;
        private readonly ReadOnlyObservableCollection<T> _items;

        public ServiceViewModel(IService<T> service, Func<T, R> sort,bool byDescending=true)
        {
            SortExpressionComparer<T> comparer = (byDescending)?
               SortExpressionComparer<T>.Descending((t) => sort(t)):
               SortExpressionComparer<T>.Ascending((t) => sort(t));

            service
                 .Resource
                 .ToObservableChangeSet()
                 .Sort(comparer)

                 .Bind(out _items)
                 .DisposeMany();
        }



    }





    //public class ServiceToSelectionViewModel<T>:OutputViewModel<T>
    //{
    //    public ReadOnlyReactiveCollection<T> Collection { get; }
    //    public ValueViewModel<T> SelectedItem { get; }


    //    public ServiceToSelectionViewModel(IService<T> dataservice, IDispatcherService ds)
    //    {
    //        var cs = dataservice.Resource;
    //        Collection = cs.ToReadOnlyReactiveCollection();

    //        SelectedItem = new ValueViewModel<T>(Collection.Selected, ds.UI);

    //        Output = SelectedItem.Output;
    //    }


    //}
    //public class ServiceToSelectionViewModel<T, R>: OutputViewModel<T> where R:IComparable
    //{

    //    public SelectableCollectionViewModel<T,R> Collection { get; }
    //    public ValueViewModel<T> SelectedItem { get; }


    //    public ServiceToSelectionViewModel(IService<T> dataservice,, IDispatcherService ds)
    //    {
    //        var cs = dataservice.Resource;
    //        Collection = new SelectableCollectionViewModel<T,R>(cs, sort,ds.UI);

    //        SelectedItem =new ValueViewModel<T>( Collection.Output,ds.UI);

    //        Output = SelectedItem.Output;

    //    }
    //}
}