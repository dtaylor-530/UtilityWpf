using DynamicData.Binding;
using PropertyTools.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityWpf.ViewModel
{
    public class FilePickerViewModel : NPC, IOutputViewModel<string>
    {

        private string file;

        [InputFilePath(".txt")]
        [FilterProperty("Filter")]
        [AutoUpdateText]
        public string File
        {
            get { return file; }
            set
            {
                if (file != value)
                {
                    file = value;

                    OnPropertiesChanged(nameof(File));
                }
            }
        }

        public IObservable<string> Output { get => this.WhenValueChanged(_ => _.File).Where(_ => _ != null); }



        //[Browsable(false)]
        //public ICommand SaveCommand
        //{
        //    get
        //    {
        //        return new RelayCommand(() =>
        //        {
        //            string csv = MatchedData.ToCsv();
        //            System.IO.Directory.CreateDirectory(OutputFolder);

        //            File.WriteAllText(OutputFolder + "\\" + "output.csv", csv);
        //        }, AlwaysTrue);
        //    }
        //}


    }
}
