
using DynamicData.Binding;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UtilityWpf.ViewModel
{

    //public interface IChildren<T>
    //{

    //    IEnumerable<T> Children { get; set; }

    //}

    //// Selectable / Hideable / Disableable
    public class SEObject<T>:NPC, UtilityWpf.ViewModel.IContainer<T>
    {
        InteractiveCollectionViewModel<T> collection;


        public SEObject(T @object, string childrenpath, System.Windows.Threading.Dispatcher dispatcher)
        {
            Object = @object;
            var children = ReflectionHelper.GetPropValue<System.Collections.IEnumerable>(@object, childrenpath)?.Cast<T>();
            if (children != null)
            {
                collection = new InteractiveCollectionViewModel<T>(children, childrenpath, dispatcher);
              
                collection.Selected.Concat(collection.ChildSubject).Subscribe(_ => ChildSelected = _);

            }
        }
        public ICollection<IContainer<T>> Children => collection?.Items ?? new List<IContainer<T>>();


        private bool _isExpanded;
        private bool _isSelected;
        private T _iscSelected;

        public bool IsExpanded
        {
            get { return _isExpanded; }
            set
            {
                //if (value != _isExpanded)
                //{
                _isExpanded = value;
                OnPropertyChanged(nameof(IsExpanded));
                //}
            }


        }

        public virtual bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                //if (value != _isSelected)
                //{
                _isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                //}
            }
        }

        public virtual T ChildSelected
        {
            get { return _iscSelected; }
            set
            {
                //if (value != _isSelected)
                //{
                
                _iscSelected = value;
                OnPropertyChanged(nameof(IsSelected));
                //}
            }
        }

        public bool HasItems => (collection?.Items?? new List<IContainer<T>>()).Count>0;

        public T Object { get; private set; }
    }



}
