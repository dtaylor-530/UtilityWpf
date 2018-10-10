using DynamicData.Binding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UtilityWpf.ViewModel

{
    public class ViewContainer : AbstractNotifyPropertyChanged
    {
        public ViewContainer(string title, object content)
        {
            Title = title;
            Content = content;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public string Title { get; }
        public object Content { get; }
    }
}