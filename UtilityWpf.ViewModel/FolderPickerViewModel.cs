using DynamicData.Binding;
using PropertyTools.DataAnnotations;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace UtilityWpf.ViewModel
{

    public class FolderPickerViewModel : NPC
    {

        private string _folder=string.Empty;

        [DirectoryPath]
        [AutoUpdateText]
        public string Folder
        {
            get { return _folder; }
            set
            {
                if (_folder != value)
                {
                    _folder = value;
                    OnPropertiesChanged(nameof(Folder));
                }
            }
        }


        public FolderPickerViewModel(string folder = "")
        {
            Folder = folder;
            Output= this.WhenValueChanged(_ => _.Folder).StartWith(Folder??"").Where(_ => _ != null);
        }

        public IObservable<string> Output { get; }
    }





}
